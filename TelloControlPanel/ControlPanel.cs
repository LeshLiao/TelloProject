using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TelloLibrary;
using TelloLibrary.Tello;

namespace TelloControlPanel
{
    public partial class ControlPanel : Form
    {
        public Swarm mySwarm;
        public ControlPanel()
        {
            InitializeComponent();
            KeyPreview = true;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            mySwarm = new Swarm();
            mySwarm.addDrone(new Tello("192.168.0.183", 8889,9000));
            mySwarm.addDrone(new Tello("192.168.10.1" , 8889,9001));
        }
        private void button7_Click(object sender, EventArgs e)
        {
            mySwarm.connect();
            updateDroneStatus();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            mySwarm.takeOff();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            mySwarm.land();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            mySwarm.disconnect();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.NumPad8)
            {
                mySwarm.forwardKeyDown();
            }
            else if (e.KeyCode == Keys.NumPad5)
            {
                mySwarm.backKeyDown();
            }
            else if (e.KeyCode == Keys.NumPad4)
            {
                mySwarm.leftKeyDown();
            }
            else if (e.KeyCode == Keys.NumPad6)
            {
                mySwarm.rightKeyDown();
            }
            else if (e.KeyCode == Keys.Add || e.KeyCode == Keys.T)
            {
                mySwarm.takeOff();
            }
            else if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.L)
            {
                mySwarm.land();
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.NumPad8)
            {
                mySwarm.forwardKeyUp();
            }
            else if (e.KeyCode == Keys.NumPad5)
            {
                mySwarm.backKeyUp();
            }
            else if (e.KeyCode == Keys.NumPad4)
            {
                mySwarm.leftKeyUp();
            }
            else if (e.KeyCode == Keys.NumPad6)
            {
                mySwarm.rightKeyUp();
            }
        }
        public void updateDroneStatus()
        {
            CancellationTokenSource cancelTokens = new CancellationTokenSource();
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    //setLabel1TextSafe("Battery:" + mySwarm.getbatteryPercentage().ToString());
                    Thread.Sleep(1000);
                }
            }, cancelTokens.Token);
        }
        private void setLabel1TextSafe(string txt)
        {
            if (label1.InvokeRequired)
                label1.Invoke(new Action(() => label1.Text = txt));
            else
                label1.Text = txt;
        }
    }
}
