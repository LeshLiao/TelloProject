using System;

namespace TelloLibrary
{
    public class Tello
    {
        private Command _command;
        private Connection _connection;
        private Listener _listener;
        public Tello(string ipAddress,int port)
        {
            _connection = new Connection(ipAddress,port);
            _command = new Command(_connection);
            _listener = new Listener(_connection);
        }
        public void Connect()
        {
            _command.Connect();
            _listener.Listensing();
        }
        public int getbatteryPercentage()
        {
            return _listener.batteryPercentageValue;
        }
        public void Disconnect()
        {
            _command.cancelTask();
        }
        public void takeOff()
        {
            _command.takeOff();
        }
        public void land()
        {
            _command.land();
        }
        public void forward()
        {
            _command.ry = 10.0f;
        }
        public void back()
        {
            _command.ry = -10.0f;
        }
        public void right()
        {
            _command.rx = 10.0f;
        }
        public void left()
        {
            _command.rx = -10.0f;
        }
        public void forwardUp()
        {
            _command.ry = 0.0f;
        }
        public void backUp()
        {
            _command.ry = 0.0f;
        }
        public void rightUp()
        {
            _command.rx = 0.0f;
        }
        public void leftUp()
        {
            _command.rx = 0.0f;
        }
    }
}
