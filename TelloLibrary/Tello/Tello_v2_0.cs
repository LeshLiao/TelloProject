using System;
using System.Net.Sockets;

namespace TelloLibrary.Tello
{
    public class Tello_v2_0 : IDrone
    {
        private Command _command;
        private Connection _connection;
        private Listener _listener;
        private UdpClient _client;
        public Tello_v2_0(string ipAddress,int port)
        {
            _connection = new Connection(ipAddress,port);
        }
        public Tello_v2_0(string ipAddress, int port,int clientPort)
        {
            _connection = new Connection(ipAddress, port);
            _client = new UdpClient(clientPort);
            setClient(_client);
        }
        private void setClient(UdpClient client)
        {
            _command = new Command(_connection, client);
            _listener = new Listener(client);
        }
        public void connect()
        {
            _listener.Listensing();
            _command.Connect();
        }
        public void disconnect()
        {
            _listener.stopListensing();
            _command.cancelTask();
        }
        public string getStatus()
        {
            return _connection.getIpAddress()+",Battery:" +_listener.getBattery();
        }
        public void takeOff()
        {
            _command.takeOff();
        }
        public void land()
        {
            _command.land();
        }
        public void forwardKeyDown()
        {
            _command.ry = 10.0f;
        }
        public void backKeyDown()
        {
            _command.ry = -10.0f;
        }
        public void upKeyDown()
        {
            _command.ly = 10.0f;
        }
        public void downKeyDown()
        {
            _command.ly = -10.0f;
        }

        public void rightKeyDown()
        {
            _command.rx = 10.0f;
        }
        public void leftKeyDown()
        {
            _command.rx = -10.0f;
        }
        public void forwardKeyUp()
        {
            _command.ry = 0.0f;
        }
        public void backKeyUp()
        {
            _command.ry = 0.0f;
        }
        public void rightKeyUp()
        {
            _command.rx = 0.0f;
        }
        public void leftKeyUp()
        {
            _command.rx = 0.0f;
        }

        public void upKeyUp()
        {
            _command.ly = 0.0f;
        }
        public void downKeyUp()
        {
            _command.ly = 0.0f;
        }

        public void turnRightKeyDown()
        {
            _command.lx = 10.0f;
        }
        public void turnRightKeyUp()
        {
            _command.lx = 0.0f;
        }
        public void turnLeftKeyDown()
        {
            _command.lx = -10.0f;
        }
        public void turnLeftKeyUp()
        {
            _command.lx = 0.0f;
        }

    }
}
