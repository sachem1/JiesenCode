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

namespace WindowsForms
{
    public partial class Form : System.Windows.Forms.Form
    {
        private readonly int Max_Item_Count = 10000;
        public Form()
        {
            InitializeComponent();
        }
        private delegate void InvokeCallback(string msg);

        public event Action<string> Log;
        private void OnLog(string msg)
        {
            Action<string> handle = Log;
            if (handle != null)
                handle(msg);
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 11000; i++)
            {
                Thread.Sleep(1000);
                Log(i.ToString());
          }
        }

        private void WriteLog(string msg)
        {
            var t = new Thread(delegate ()
                {
                    Thread.Sleep(200);
                    rtbMsg.Invoke((MethodInvoker)delegate ()
                    {
                        m_comm_MessageEvent(msg);
                    });
                })
            { IsBackground = true };
            t.Start();
        }

        private void APMDoing(IAsyncResult result)
        {
            var delegateWrite = (Func<int, string>)result.AsyncState;

            var finalResult = delegateWrite.EndInvoke(result);
            rtbMsg.AppendText(finalResult);
        }

        public void m_comm_MessageEvent(string msg)
        {
            if (rtbMsg.InvokeRequired)
            {
                InvokeCallback msgCallback = m_comm_MessageEvent;
                rtbMsg.Invoke(msgCallback, msg);
            }
            else
            {
                rtbMsg.AppendText(msg);
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            Log += new Action<string>(Form_Log);
        }

        private void Form_Log(string obj)
        {
            this.rtbMsg.Invoke(new Action(()=> {
                rtbMsg.AppendText(obj+"\r\n");
            }) );
        }
    }
}
