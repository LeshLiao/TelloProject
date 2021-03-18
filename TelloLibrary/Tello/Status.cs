using System;
using System.Collections.Generic;
using System.Text;

namespace TelloLibrary.Tello
{
    class Status
    {
        public int flyMode;
        public int height;
        public int verticalSpeed;
        public int flySpeed;
        public int eastSpeed;
        public int northSpeed;
        public int flyTime;
        public bool flying;
        public bool downVisualState;
        public bool droneHover;
        public bool eMOpen;
        public bool onGround;
        public bool pressureState;
        public int batteryPercentage;
        public bool batteryLow;
        public bool batteryLower;
        public bool batteryState;
        public bool powerState;
        public int droneBatteryLeft;
        public int droneFlyTimeLeft;
        public int cameraState;
        public int electricalMachineryState;
        public bool factoryMode;
        public bool frontIn;
        public bool frontLSC;
        public bool frontOut;
        public bool gravityState;
        public int imuCalibrationState;
        public bool imuState;
        public bool outageRecording;
        public int temperatureHeight;
        public int throwFlyTimer;
        public bool windState;
        public Status()
        {
        }
    }
}
