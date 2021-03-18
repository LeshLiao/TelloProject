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

        void upKeyDown();
        void downKeyDown();


        void forwardKeyUp();
        void backKeyUp();
        void rightKeyUp();
        void leftKeyUp();

        void upKeyUp();
        void downKeyUp();

        void turnLeftKeyDown();
        void turnLeftKeyUp();
        void turnRightKeyDown();
        void turnRightKeyUp();

        string getStatus();

    }
}
