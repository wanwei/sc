using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.impl
{
    /// <summary>
    /// 动态K线数据
    /// 该类用于实时的自动化交易，仅能够表示一天的数据
    /// </summary>
    public class KLineData_Dynamic : KLineData_Abstract
    {
        private List<double[]> openTime;

        private KLinePeriod period;        

        public List<double> list_time;        

        public List<float> list_start;

        public List<float> list_high;

        public List<float> list_low;

        public List<float> list_end;

        public List<int> list_mount;

        public List<float> list_money;

        public List<int> list_hold;

        public KLineData_Dynamic(List<double[]> openTime, KLinePeriod period)
        {
            this.list_time = TimeUtils.GetKLineTimes(openTime, period);
        }

        public void NextTick(ITickBar tick)
        {
            //this.BarPos = 0;
            //TODO
        }

        public override IList<double> Arr_Time { get { return list_time; } }

        public override IList<float> Arr_Start { get { return list_start; } }

        public override IList<float> Arr_High { get { return list_high; } }

        public override IList<float> Arr_Low { get { return list_low; } }

        public override IList<float> Arr_End { get { return list_end; } }

        public override IList<int> Arr_Mount { get { return list_mount; } }

        public override IList<float> Arr_Money { get { return list_money; } }

        public override IList<int> Arr_Hold { get { return list_hold; } }
    }
}
