using com.wer.sc.data.historydata;
using com.wer.sc.data.opentime;
using com.wer.sc.data.utils;
using com.wer.sc.utils.ui.proceed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cnfutures.generator
{
    public class Step_DayStartTime : IStep
    {
        private string code;

        private List<int> dates;

        //private DataLoader dataLoader;

        public Step_DayStartTime(string code)
        {
            this.code = code;
        }

        public int ProgressStep
        {
            get
            {
                return 10;
            }
        }

        public string StepDesc
        {
            get
            {
                return "更新" + code + "的开盘时间";
            }
        }

        public string Proceed()
        {
            return "";
        }
    }
}