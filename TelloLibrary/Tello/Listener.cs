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
        public byte[] bytes;
        public int batteryPercentageValue;

        public Listener(UdpClient client)
        {
            _client = client;
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
                            updateData(bytes.Skip(9).ToArray());
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
            if(cancelTokens != null)
                cancelTokens.Cancel();
        }
            public void updateData(byte[] data)
        {
            int flyMode;
            int height;
            int verticalSpeed;
            int flySpeed;
            int eastSpeed;
            int northSpeed;
            int flyTime;
            bool flying;
            bool downVisualState;
            bool droneHover;
            bool eMOpen;
            bool onGround;
            bool pressureState;
            int batteryPercentage;
            bool batteryLow;
            bool batteryLower;
            bool batteryState;
            bool powerState;
            int droneBatteryLeft;
            int droneFlyTimeLeft;
            int cameraState;
            int electricalMachineryState;
            bool factoryMode;
            bool frontIn;
            bool frontLSC;
            bool frontOut;
            bool gravityState;
            int imuCalibrationState;
            bool imuState;
            int lightStrength;
            bool outageRecording;
            int smartVideoExitMode;
            int temperatureHeight;
            int throwFlyTimer;
            int wifiDisturb;
            int wifiStrength;
            bool windState;

            var index = 0;
            try
            {
                height = (Int16)(data[index] | (data[index + 1] << 8)); index += 2;
                northSpeed = (Int16)(data[index] | (data[index + 1] << 8)); index += 2;
                eastSpeed = (Int16)(data[index] | (data[index + 1] << 8)); index += 2;
                flySpeed = ((int)Math.Sqrt(Math.Pow(northSpeed, 2.0D) + Math.Pow(eastSpeed, 2.0D)));
                verticalSpeed = (Int16)(data[index] | (data[index + 1] << 8)); index += 2;
                flyTime = data[index] | (data[index + 1] << 8); index += 2;
                imuState = (data[index] >> 0 & 0x1) == 1 ? true : false;
                pressureState = (data[index] >> 1 & 0x1) == 1 ? true : false;
                downVisualState = (data[index] >> 2 & 0x1) == 1 ? true : false;
                powerState = (data[index] >> 3 & 0x1) == 1 ? true : false;
                batteryState = (data[index] >> 4 & 0x1) == 1 ? true : false;
                gravityState = (data[index] >> 5 & 0x1) == 1 ? true : false;
                windState = (data[index] >> 7 & 0x1) == 1 ? true : false;
                index += 1;
                imuCalibrationState = data[index]; index += 1;
                batteryPercentage = data[index]; index += 1;
                batteryPercentageValue = batteryPercentage; //test
                droneFlyTimeLeft = data[index] | (data[index + 1] << 8); index += 2;
                droneBatteryLeft = data[index] | (data[index + 1] << 8); index += 2;
                flying = (data[index] >> 0 & 0x1) == 1 ? true : false;
                onGround = (data[index] >> 1 & 0x1) == 1 ? true : false;
                eMOpen = (data[index] >> 2 & 0x1) == 1 ? true : false;
                droneHover = (data[index] >> 3 & 0x1) == 1 ? true : false;
                outageRecording = (data[index] >> 4 & 0x1) == 1 ? true : false;
                batteryLow = (data[index] >> 5 & 0x1) == 1 ? true : false;
                batteryLower = (data[index] >> 6 & 0x1) == 1 ? true : false;
                factoryMode = (data[index] >> 7 & 0x1) == 1 ? true : false;
                index += 1;
                flyMode = data[index]; index += 1;
                throwFlyTimer = data[index]; index += 1;
                cameraState = data[index]; index += 1;
                //if (paramArrayOfByte.length >= 22)
                electricalMachineryState = data[index]; index += 1; //(paramArrayOfByte[21] & 0xFF);
                //if (paramArrayOfByte.length >= 23)
                frontIn = (data[index] >> 0 & 0x1) == 1 ? true : false;//22
                frontOut = (data[index] >> 1 & 0x1) == 1 ? true : false;
                frontLSC = (data[index] >> 2 & 0x1) == 1 ? true : false;
                index += 1;
                temperatureHeight = (data[index] >> 0 & 0x1);//23
                //wifiStrength = Tello.wifiStrength;//Wifi str comes in a cmd.
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception:updateData(): data length:" + data.Length.ToString()+",index="+ index.ToString());
            }
        }

    }
}
