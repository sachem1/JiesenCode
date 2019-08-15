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
        public Form()
        {
            InitializeComponent();
        }
        private delegate void InvokeCallback(string msg);
        private void btnStart_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 11000; i++)
            {
                Thread.Sleep(2000);
                WriteLog(i.ToString());
            }
        }

        private void WriteLog(string msg)
        {
            Task.Factory.StartNew(() => { m_comm_MessageEvent(msg + "\r\n"); });
            
        }

        private void APMDoing(IAsyncResult result)
        {
            var delegateWrite=(Func<int,string>)result.AsyncState;

            var finalResult = delegateWrite.EndInvoke(result);
            rtbMsg.AppendText(finalResult);
        }

        void m_comm_MessageEvent(string msg)
        {

            if (rtbMsg.InvokeRequired)
            {

                InvokeCallback msgCallback = new InvokeCallback(m_comm_MessageEvent);

                rtbMsg.Invoke(msgCallback, new object[] { msg });
            }

            else
            {
                rtbMsg.AppendText(msg);
            }

        }
    }
}
