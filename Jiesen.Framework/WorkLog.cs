using System;
using System.Threading;

namespace Jiesen.Framework
{
    public class WorkLog
    {
        public event Action<string> WriteLog;
        
        public Thread WorkThread ;

        private void OnLog(string msg)
        {
            WriteLog?.Invoke(msg);
        }

        public void StartWriteLog()
        {
            WorkThread = new Thread(() =>
              {
                  for (int i = 0; i < 100; i++)
                  {
                      Thread.Sleep(500);
                      OnLog("第" + i + "个");

                  }
              })
            { IsBackground = true };
            WorkThread.Start();
        }
    }
}