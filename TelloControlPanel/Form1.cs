using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TelloLibrary;

namespace TelloControlPanel
{
    public partial class Form1 : Form
    {
        public Tello myTello;
        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //myTello = new Tello("192.168.10.1",8889);
            myTello = new Tello("192.168.0.183", 8889);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            myTello.Connect();
            updateDroneStatus();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            myTello.takeOff();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            myTello.land();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            myTello.Disconnect();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.NumPad8)
            {
                myTello.forward();
            }
            else if (e.KeyCode == Keys.NumPad5)
            {
                myTello.back();
            }
            else if (e.KeyCode == Keys.NumPad4)
            {
                myTello.left();
            }
            else if (e.KeyCode == Keys.NumPad6)
            {
                myTello.right();
            }
            else if (e.KeyCode == Keys.Add || e.KeyCode == Keys.T)
            {
                myTello.takeOff();
            }
            else if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.L)
            {
                myTello.land();
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.NumPad8)
            {
                myTello.forwardUp();
            }
            else if (e.KeyCode == Keys.NumPad5)
            {
                myTello.backUp();
            }
            else if (e.KeyCode == Keys.NumPad4)
            {
                myTello.leftUp();
            }
            else if (e.KeyCode == Keys.NumPad6)
            {
                myTello.rightUp();
            }
        }
        public void updateDroneStatus()
        {
            CancellationTokenSource cancelTokens = new CancellationTokenSource();
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    setLabel1TextSafe("Battery:" + myTello.getbatteryPercentage().ToString());
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
