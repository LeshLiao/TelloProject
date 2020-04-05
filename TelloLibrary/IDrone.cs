using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace TelloLibrary
{
    public interface IDrone
    {
        void connect();
        void disconnect();
        void takeOff();
        void land();
        void forwardKeyDown();
        void backKeyDown();
        void rightKeyDown();
        void leftKeyDown();
        void forwardKeyUp();
        void backKeyUp();
        void rightKeyUp();
        void leftKeyUp();

    }
}
