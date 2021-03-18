using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TelloLibrary.Tello
{
    class Listener
    {
        private UdpClient _client;
        private CancellationTokenSource cancelTokens;
        private Status _status;
        public byte[] bytes;
        public Listener(UdpClient client)
        {
            _client = client;
            _status = new Status();
        }
        public void Listensing()
        {
            cancelTokens = new CancellationTokenSource();
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    try
                    {
                        if (cancelTokens.Token.IsCancellationRequested)
                        {
                            break;
                        }
                        var result = await _client.ReceiveAsync();
                        bytes = result.Buffer.ToArray();
                        string message = Encoding.ASCII.GetString(result.Buffer, 0, result.Buffer.Length);
                        if (message.StartsWith("conn_ack"))
                        {
                            Console.WriteLine("receive:conn_ack");
                            continue;
                        }

                        int cmdId = ((int)bytes[5] | ((int)bytes[6] << 8));
                        if (cmdId == 0x56 && bytes.Length >= 35)  //state command
                        {
                            updateStatus(bytes.Skip(9).ToArray());
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Receive thread error");
                        //disconnect();
                        break;
                    }
                }
            }, cancelTokens.Token);
        }
        public void stopListensing()
        {
            if (cancelTokens != null)
                cancelTokens.Cancel();
        }
        public void updateStatus(byte[] data)
        {
            var index = 0;
            try
            {
                _status.height = (Int16)(data[index] | (data[index + 1] << 8)); index += 2;
                _status.northSpeed = (Int16)(data[index] | (data[index + 1] << 8)); index += 2;
                _status.eastSpeed = (Int16)(data[index] | (data[index + 1] << 8)); index += 2;
                _status.flySpeed = ((int)Math.Sqrt(Math.Pow(_status.northSpeed, 2.0D) + Math.Pow(_status.eastSpeed, 2.0D)));
                _status.verticalSpeed = (Int16)(data[index] | (data[index + 1] << 8)); index += 2;
                _status.flyTime = data[index] | (data[index + 1] << 8); index += 2;
                _status.imuState = (data[index] >> 0 & 0x1) == 1 ? true : false;
                _status.pressureState = (data[index] >> 1 & 0x1) == 1 ? true : false;
                _status.downVisualState = (data[index] >> 2 & 0x1) == 1 ? true : false;
                _status.powerState = (data[index] >> 3 & 0x1) == 1 ? true : false;
                _status.batteryState = (data[index] >> 4 & 0x1) == 1 ? true : false;
                _status.gravityState = (data[index] >> 5 & 0x1) == 1 ? true : false;
                _status.windState = (data[index] >> 7 & 0x1) == 1 ? true : false;
                index += 1;
                _status.imuCalibrationState = data[index]; index += 1;
                _status.batteryPercentage = data[index]; index += 1;
                _status.droneFlyTimeLeft = data[index] | (data[index + 1] << 8); index += 2;
                _status.droneBatteryLeft = data[index] | (data[index + 1] << 8); index += 2;
                _status.flying = (data[index] >> 0 & 0x1) == 1 ? true : false;
                _status.onGround = (data[index] >> 1 & 0x1) == 1 ? true : false;
                _status.eMOpen = (data[index] >> 2 & 0x1) == 1 ? true : false;
                _status.droneHover = (data[index] >> 3 & 0x1) == 1 ? true : false;
                _status.outageRecording = (data[index] >> 4 & 0x1) == 1 ? true : false;
                _status.batteryLow = (data[index] >> 5 & 0x1) == 1 ? true : false;
                _status.batteryLower = (data[index] >> 6 & 0x1) == 1 ? true : false;
                _status.factoryMode = (data[index] >> 7 & 0x1) == 1 ? true : false;
                index += 1;
                _status.flyMode = data[index]; index += 1;
                _status.throwFlyTimer = data[index]; index += 1;
                _status.cameraState = data[index]; index += 1;
                //if (paramArrayOfByte.length >= 22)
                _status.electricalMachineryState = data[index]; index += 1; //(paramArrayOfByte[21] & 0xFF);
                //if (paramArrayOfByte.length >= 23)
                _status.frontIn = (data[index] >> 0 & 0x1) == 1 ? true : false;//22
                _status.frontOut = (data[index] >> 1 & 0x1) == 1 ? true : false;
                _status.frontLSC = (data[index] >> 2 & 0x1) == 1 ? true : false;
                index += 1;
                _status.temperatureHeight = (data[index] >> 0 & 0x1);//23
                //wifiStrength = Tello.wifiStrength;//Wifi str comes in a cmd.
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception:updateData(): data length:" + data.Length.ToString() + ",index=" + index.ToString());
            }
        }
        public string getBattery()
        {
            return _status.batteryPercentage.ToString();
        }
    }
}
