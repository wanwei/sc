using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// 引用的实时数据
    /// </summary>
    public class ReferedRealTimeData
    {
        private bool isReferTickData = false;

        private bool isReferTimeLineData = false;

        private List<KLinePeriod> referedKLinePeriods = new List<KLinePeriod>();

        public ReferedRealTimeData()
        {

        }

        public bool IsReferTickData
        {
            get
            {
                return isReferTickData;
            }

            set
            {
                isReferTickData = value;
            }
        }

        public bool IsReferTimeLineData
        {
            get
            {
                return isReferTimeLineData;
            }

            set
            {
                isReferTimeLineData = value;
            }
        }

        public List<KLinePeriod> ReferedKLinePeriods
        {
            get
            {
                return referedKLinePeriods;
            }
        }
    }
}
