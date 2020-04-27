using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ReUse.Option;

namespace ReUse
{
    public class FormControl
    {
        private Boolean CountDownStarted = false;
        private Stopwatch CountDownWatch = new Stopwatch();
        private double CountDownSecond = 0;
        private Thread CountDownThread;
        private Form CountDownForm = new Form();

        public void StartCountDown(int countDownSecond, Form form, TimeUnit time = TimeUnit.Second)
        {
            CountDownStarted = true;
            CountDownWatch.Start();
            CountDownForm = form;
            CountDownSecond = countDownSecond * (int)time;
            CountDownThread = new Thread(CountDown);
            CountDownThread.IsBackground = true;
            CountDownThread.Start();
        }

        public void StopCountDown()
        {
            CountDownStarted = false;
            CountDownWatch.Reset();
            if (CountDownThread != null)
            {
                CountDownThread.Abort();
                CountDownForm.WindowState = FormWindowState.Normal;
            }
        }

        private void CountDown()
        {
            while (CountDownStarted)
            {
                double timePass = CountDownWatch.Elapsed.TotalSeconds;

                if (timePass > CountDownSecond)
                {
                    CountDownForm.Invoke((MethodInvoker)delegate
                    {
                        CountDownForm.Close();
                    });
                    Thread.Sleep(1000);
                }
                else
                    Thread.Sleep(1000);
            }
        }





    }
}
