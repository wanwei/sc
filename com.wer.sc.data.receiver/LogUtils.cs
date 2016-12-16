using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.receiver
{
    public class LogUtils
    {
        private List<String> logList = new List<string>();
        private DelegateOnWriteLog onWriteLog;
        public List<string> LogList
        {
            get
            {
                return logList;
            }
        }

        public void WriteLog(object content)
        {
            LogList.Add(content.ToString());
            if (onWriteLog != null)
                onWriteLog(this, content);
        }

        public DelegateOnWriteLog OnWriteLog
        {
            get { return onWriteLog; }
            set { onWriteLog = value; }
        }
    }

    public delegate void DelegateOnWriteLog(object sender, Object log);

}
