using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;


namespace TelloLibrary
{
    class Connection
    {
        private UdpClient _client;
        private IPEndPoint _endpoint;
        private string _ipAddress;
        private int _port;
        public Connection(string ipAddress,int port)
        {
            _ipAddress = ipAddress;
            _port = port;
            _client = new UdpClient(port);
            _endpoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
        }
        public UdpClient getClient()
        {
            return _client;
        }
        public IPEndPoint getEndPoint()
        {
            return _endpoint;
        }

    }
}
