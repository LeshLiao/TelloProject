using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;


namespace TelloLibrary.Tello
{
    class Connection
    {
        private IPEndPoint _endpoint;
        private string _ipAddress;
        private int _port;
        public Connection(string ipAddress,int port)
        {
            _ipAddress = ipAddress;
            _port = port;
            _endpoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
        }
        public IPEndPoint getEndPoint()
        {
            return _endpoint;
        }
        public string getIpAddress()
        {
            return _ipAddress;
        }
    }
}
