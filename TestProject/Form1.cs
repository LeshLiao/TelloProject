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

namespace TestProject
{
    public partial class Form1 : Form
    {
        CancellationTokenSource cancelTokens;
        Task task1;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public void startTask(string str)
        {
            cancelTask();
            Thread.Sleep(500);

            cancelTokens = new CancellationTokenSource();

            task1 = Task.Factory.StartNew(async () =>
            {
                int number = 0;
                while (true)
                {
                    try
                    {
                        number++;
                        if (cancelTokens.Token.IsCancellationRequested) break;



                        Console.WriteLine("Task:"+str+"("+ number.ToString()+")");

                        Thread.Sleep(500);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception:" + ex.Message);
                    }
                }
            }, cancelTokens.Token);
            
        }
        public void cancelTask()
        {
            if(cancelTokens != null)
                cancelTokens.Cancel();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            startTask("first");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cancelTask();
        }
    }
}
