using System;
using System.Collections.Generic;
using System.Threading;

namespace TelloLibrary
{
    public class Swarm
    {
        private List<IDrone> _drones = new List<IDrone>();
        private List<string> _status = new List<string>();
        public void addDrone(IDrone drone)
        {
            _drones.Add(drone);
        }
        public void connect()
        {
            foreach (var drone in _drones)
                drone.connect();
        }
        public void disconnect()
        {
            foreach (var drone in _drones)
                drone.disconnect();
        }
        public void takeOff()
        {
            foreach (var drone in _drones)
                drone.takeOff();
        }
        public void land()
        {
            foreach (var drone in _drones)
                drone.land();
        }
        public void forwardKeyDown()
        {
            foreach (var drone in _drones)
                drone.forwardKeyDown();
        }
        public void backKeyDown()
        {
            foreach (var drone in _drones)
                drone.backKeyDown();
        }
        public void upKeyDown()
        {
            foreach (var drone in _drones)
                drone.upKeyDown();
        }
        public void downKeyDown()
        {
            foreach (var drone in _drones)
                drone.downKeyDown();
        }
        public void rightKeyDown()
        {
            foreach (var drone in _drones)
                drone.rightKeyDown();
        }
        public void leftKeyDown()
        {
            foreach (var drone in _drones)
                drone.leftKeyDown();
        }
        public void forwardKeyUp()
        {
            foreach (var drone in _drones)
                drone.forwardKeyUp();
        }
        public void backKeyUp()
        {
            foreach (var drone in _drones)
                drone.backKeyUp();
        }
        public void upKeyUp()
        {
            foreach (var drone in _drones)
                drone.upKeyUp();
        }
        public void downKeyUp()
        {
            foreach (var drone in _drones)
                drone.downKeyUp();
        }
        public void rightKeyUp()
        {
            foreach (var drone in _drones)
                drone.rightKeyUp();
        }
        public void leftKeyUp()
        {
            foreach (var drone in _drones)
                drone.leftKeyUp();
        }

        public void turnLeftKeyDown()
        {
            foreach (var drone in _drones)
                drone.turnLeftKeyDown();
        }
        public void turnLeftKeyUp()
        {
            foreach (var drone in _drones)
                drone.turnLeftKeyUp();
        }
        public void turnRightKeyDown()
        {
            foreach (var drone in _drones)
                drone.turnRightKeyDown();
        }
        public void turnRightKeyUp()
        {
            foreach (var drone in _drones)
                drone.turnRightKeyUp();
        }

        public List<string> getStatus()
        {
            _status.Clear();
            foreach (var drone in _drones)
            {
                _status.Add(drone.getStatus());
            }
            return _status;
        }
    }
}
