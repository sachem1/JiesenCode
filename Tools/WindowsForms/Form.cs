using Jiesen.Framework;
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
        WorkLog workLog = new WorkLog();
        public event Action<string> Log;
        private void OnLog(string msg)
        {
            Action<string> handle = Log;
            if (handle != null)
                handle(msg);
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            workLog.StartWriteLog();
        }


        private void ShowLog(string msg)
        {
            rtbMsg.BeginInvoke(new Action(() =>
            {
                LogWrite(msg);
            }));
        }

        private void LogWrite(string msg)
        {
           
            this.rtbMsg.AppendText(msg + "\r\n");
            this.rtbMsg.ScrollToCaret();
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
            
            workLog.WriteLog += WorkLog_WriteLog;


        }

        private void WorkLog_WriteLog(string obj)
        {
            ShowLog(obj);
        }

        private void Form_Log(string obj)
        {
            this.rtbMsg.Invoke(new Action(() =>
            {
                rtbMsg.AppendText(obj + "\r\n");
            }));
        }
    }
}
