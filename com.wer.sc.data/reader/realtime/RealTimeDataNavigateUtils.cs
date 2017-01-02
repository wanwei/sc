using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader.realtime
{
    public class RealTimeDataNavigateUtils
    {
        private double time;

        private IKLineData klineData;

        private ITimeLineData timeLineData;

        private ITickData tickData;

        public void NavigateForward_Time(KLinePeriod period, int len)
        {

        }

        public void NavigateForward_Period(KLinePeriod period, int len)
        {
            int barPos = klineData.BarPos;

        }

        public void NavigateForward_Tick(int len)
        {
            int currentBarPos = tickData.BarPos;
            currentBarPos += len;
            if (currentBarPos < tickData.Length)
            {

            }
        }
    }
}