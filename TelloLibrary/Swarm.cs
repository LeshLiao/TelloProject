using System;
using System.Collections.Generic;
using System.Threading;

namespace TelloLibrary
{
    public class Swarm
    {
        List<IDrone> Drones = new List<IDrone>();
        public void addDrone(IDrone drone)
        {
            Drones.Add(drone);
        }
        public void connect() 
        { 
            foreach (var drone in Drones) 
                drone.connect();  
        }
        public void disconnect()
        {
            foreach (var drone in Drones)
                drone.disconnect();
        }
        public void takeOff()
        {
            foreach (var drone in Drones)
                drone.takeOff();
        }
        public void land()
        {
            foreach (var drone in Drones)
                drone.land();
        }
        public void forwardKeyDown()
        {
            foreach (var drone in Drones)
                drone.forwardKeyDown();
        }
        public void backKeyDown()
        {
            foreach (var drone in Drones)
                drone.backKeyDown();
        }
        public void rightKeyDown()
        {
            foreach (var drone in Drones)
                drone.rightKeyDown();
        }
        public void leftKeyDown()
        {
            foreach (var drone in Drones)
                drone.leftKeyDown();
        }
        public void forwardKeyUp()
        {
            foreach (var drone in Drones)
                drone.forwardKeyUp();
        }
        public void backKeyUp()
        {
            foreach (var drone in Drones)
                drone.backKeyUp();
        }
        public void rightKeyUp()
        {
            foreach (var drone in Drones)
                drone.rightKeyUp();
        }
        public void leftKeyUp()
        {
            foreach (var drone in Drones)
                drone.leftKeyUp();
        }
    }
}
