using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;

namespace TelloLibrary.Tello
{
    class Command
    {
        private UdpClient _client;
        private IPEndPoint _endpoint;
        private static ushort sequence;
        private int intervalTimeMilliseconds;
        public byte[] bytes;
        public float rx, ry, lx, ly, speed;
        public CancellationTokenSource cancelTokens;

        public Command(Connection con, UdpClient client)
        {
            _client = client;
            _endpoint = con.getEndPoint();
            rx = 0.0f;
            ry = 0.0f;
            lx = 0.0f;
            ly = 0.0f;
            speed = 0.0f;
            sequence = 1;
            intervalTimeMilliseconds = 50;
        }
        public bool Connect()
        {
            _client.Connect(_endpoint);
            Thread.Sleep(300);
            byte[] connectPacket = Encoding.UTF8.GetBytes("conn_req:\x00\x00");
            connectPacket[connectPacket.Length - 2] = 0x96;
            connectPacket[connectPacket.Length - 1] = 0x17;
            _client.Send(connectPacket, connectPacket.Length);
            startTask();
            return true;
        }
        public async void receiveAck()
        {
            var result = await _client.ReceiveAsync();
            bytes = result.Buffer.ToArray();
            string Message = Encoding.ASCII.GetString(result.Buffer, 0, result.Buffer.Length);
            if (Message.StartsWith("conn_ack"))
            {
                Console.WriteLine("receive:conn_ack");
                queryAttAngle();
                setMaxHeight(50);
            }
        }
        public void takeOff()
        {
            var packet = new byte[] { 0xcc, 0x58, 0x00, 0x7c, 0x68, 0x54, 0x00, 0xe4, 0x01, 0xc2, 0x16 };
            setPacketSequence(packet);
            setPacketCRCs(packet);
            _client.Send(packet,packet.Length);
        }
        public void land()
        {
            var packet = new byte[] { 0xcc, 0x60, 0x00, 0x27, 0x68, 0x55, 0x00, 0xe5, 0x01, 0x00, 0xba, 0xc7 };
            packet[9] = 0x00;
            setPacketSequence(packet);
            setPacketCRCs(packet);
            _client.Send(packet,packet.Length);
        }
        private static byte[] createJoyPacket(float fRx, float fRy, float fLx, float fLy, float speed)
        {
            var packet = new byte[] { 0xcc, 0xb0, 0x00, 0x7f, 0x60, 0x50, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x12, 0x16, 0x01, 0x0e, 0x00, 0x25, 0x54 };
            short axis1 = (short)(660.0F * fRx + 1024.0F);//RightX center=1024 left =364 right =-364
            short axis2 = (short)(660.0F * fRy + 1024.0F);//RightY down =364 up =-364
            short axis3 = (short)(660.0F * fLy + 1024.0F);//LeftY down =364 up =-364
            short axis4 = (short)(660.0F * fLx + 1024.0F);//LeftX left =364 right =-364
            short axis5 = (short)(660.0F * speed + 1024.0F);//Speed. 

            if (speed > 0.1f) axis5 = 0x7fff;

            long packedAxis = ((long)axis1 & 0x7FF) | (((long)axis2 & 0x7FF) << 11) | ((0x7FF & (long)axis3) << 22) | ((0x7FF & (long)axis4) << 33) | ((long)axis5 << 44);
            packet[9] = ((byte)(int)(0xFF & packedAxis));
            packet[10] = ((byte)(int)(packedAxis >> 8 & 0xFF));
            packet[11] = ((byte)(int)(packedAxis >> 16 & 0xFF));
            packet[12] = ((byte)(int)(packedAxis >> 24 & 0xFF));
            packet[13] = ((byte)(int)(packedAxis >> 32 & 0xFF));
            packet[14] = ((byte)(int)(packedAxis >> 40 & 0xFF));
            var now = DateTime.Now;
            packet[15] = (byte)now.Hour;
            packet[16] = (byte)now.Minute;
            packet[17] = (byte)now.Second;
            packet[18] = (byte)(now.Millisecond & 0xff);
            packet[19] = (byte)(now.Millisecond >> 8);
            CRC.calcUCRC(packet, 4);
            CRC.calcCrc(packet, packet.Length);

            return packet;
        }
        public void queryAttAngle()
        {
            var packet = new byte[] { 0xcc, 0x58, 0x00, 0x7c, 0x48, 0x59, 0x10, 0x06, 0x00, 0xe9, 0xb3 };
            setPacketSequence(packet);
            setPacketCRCs(packet);
            _client.Send(packet,packet.Length);
        }
        public void setMaxHeight(int height)
        {                                                      //crc    typ  cmdL  cmdH  seqL  seqH  heiL  heiH  crc   crc
            var packet = new byte[] { 0xcc, 0x68, 0x00, 0x27, 0x68, 0x58, 0x00, 0x00, 0x00, 0x00, 0x00, 0x5b, 0xc5 };
            packet[9] = (byte)(height & 0xff);
            packet[10] = (byte)((height >> 8) & 0xff);
            setPacketSequence(packet);
            setPacketCRCs(packet);
            _client.Send(packet,packet.Length);
        }
        public void requestIframe()
        {
            var iframePacket = new byte[] { 0xcc, 0x58, 0x00, 0x7c, 0x60, 0x25, 0x00, 0x00, 0x00, 0x6c, 0x95 };
            _client.Send(iframePacket,iframePacket.Length);
        }
        private static void setPacketSequence(byte[] packet)
        {
            packet[7] = (byte)(sequence & 0xff);
            packet[8] = (byte)((sequence >> 8) & 0xff);
            sequence++;
        }
        private static void setPacketCRCs(byte[] packet)
        {
            CRC.calcUCRC(packet, 4);
            CRC.calcCrc(packet, packet.Length);
        }
        public void startTask()
        {
            cancelTask();
            Thread.Sleep(intervalTimeMilliseconds);
            queryAttAngle();
            setMaxHeight(50);
            cancelTokens = new CancellationTokenSource();
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    try
                    {
                        if (cancelTokens.Token.IsCancellationRequested) break;

                        var packet = createJoyPacket(rx,ry, lx, ly, speed);
                        _client.Send(packet, packet.Length);
                        //requestIframe(); //request video
                        Thread.Sleep(intervalTimeMilliseconds);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception:" + ex.Message);
                    }
                }
            }, cancelTokens.Token);
        }

        public void cancelTask()
        {
            if(cancelTokens != null)
                cancelTokens.Cancel();
        }
    }
}
