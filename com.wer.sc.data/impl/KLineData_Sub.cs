using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.impl
{
    public class KLineData_Sub : IKLineData
    {
        private IKLineData klineData;

        private int startIndex;
        private int endIndex;

        public KLineData_Sub(IKLineData klineData, int startIndex, int endIndex)
        {
            this.klineData = klineData;
            this.startIndex = startIndex;
            this.endIndex = endIndex;

            this.arr_time = new ReadOnlyList_Sub<double>(klineData.Arr_Time, startIndex, endIndex);
            this.arr_start = new ReadOnlyList_Sub<float>(klineData.Arr_Start, startIndex, endIndex);
            this.arr_high = new ReadOnlyList_Sub<float>(klineData.Arr_High, startIndex, endIndex);
            this.arr_low = new ReadOnlyList_Sub<float>(klineData.Arr_Low, startIndex, endIndex);
            this.arr_end = new ReadOnlyList_Sub<float>(klineData.Arr_End, startIndex, endIndex);
            this.arr_mount = new ReadOnlyList_Sub<int>(klineData.Arr_Mount, startIndex, endIndex);
            this.arr_money = new ReadOnlyList_Sub<float>(klineData.Arr_Money, startIndex, endIndex);
            this.arr_hold = new ReadOnlyList_Sub<int>(klineData.Arr_Hold, startIndex, endIndex);
            this.arr_height = new ReadOnlyList_Sub<float>(klineData.Arr_Height, startIndex, endIndex);
            this.arr_heightPercent = new ReadOnlyList_Sub<float>(klineData.Arr_HeightPercent, startIndex, endIndex);
            this.arr_blockHigh = new ReadOnlyList_Sub<float>(klineData.Arr_BlockHigh, startIndex, endIndex);
            this.arr_BlockLow = new ReadOnlyList_Sub<float>(klineData.Arr_BlockLow, startIndex, endIndex);
            this.arr_blockHeight = new ReadOnlyList_Sub<float>(klineData.Arr_BlockHeight, startIndex, endIndex);
            this.arr_blockHeightPercent = new ReadOnlyList_Sub<float>(klineData.Arr_BlockHeightPercent, startIndex, endIndex);
            this.arr_upPercent = new ReadOnlyList_Sub<float>(klineData.Arr_UpPercent, startIndex, endIndex);

        }

        public string Code
        {
            get
            {
                return klineData.Code;
            }
        }

        public int BarPos
        {
            get
            {
                return klineData.BarPos - startIndex;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public double FullTime
        {
            get
            {
                return Arr_Time[BarPos];
            }
        }

        public int Date
        {
            get { return (int)Arr_Time[BarPos]; }
        }

        public double Time
        {
            get
            {
                return FullTime - Date;
            }
        }

        public float Start
        {
            get
            {
                return Arr_Start[BarPos];
            }
        }

        public float High
        {
            get
            {
                return Arr_High[BarPos];
            }
        }

        public float Low
        {
            get
            {
                return Arr_Low[BarPos];
            }
        }

        public float End
        {
            get
            {
                return Arr_End[BarPos];
            }
        }

        public int Mount
        {
            get
            {
                return Arr_Mount[BarPos];
            }
        }

        public float Money
        {
            get
            {
                return Arr_Money[BarPos];
            }
        }

        public int Hold
        {
            get
            {
                return Arr_Hold[BarPos];
            }
        }

        public IKLineChart GetCurrentChart()
        {
            return new KLineChart_KLineData(this, BarPos);
        }

        public int Length
        {
            get
            {
                return endIndex - startIndex + 1;
            }
        }

        public KLinePeriod Period
        {
            get
            {
                return klineData.Period;
            }
        }

        public IKLineData GetRange(int start, int end)
        {
            return new KLineData_Sub(this, start, end);
        }

        public IKLineChart GetAggrChart(int startIndex, int endIndex)
        {
            KLineChart chart = new KLineChart();
            chart.SetTime(this.Arr_Time[startIndex]);
            chart.SetStart(this.Arr_Start[startIndex]);
            chart.SetEnd(this.Arr_End[endIndex]);
            chart.SetHold(this.Arr_Hold[endIndex]);

            float high = float.MinValue;
            float low = float.MaxValue;
            int mount = 0;
            float money = 0;
            for (int i = startIndex; i <= endIndex; i++)
            {
                float chigh = this.Arr_High[i];
                float clow = this.Arr_Low[i];
                high = high < chigh ? chigh : high;
                low = low > clow ? clow : low;
                mount += this.Arr_Mount[i];
                money += this.Arr_Money[i];
            }
            chart.SetHigh(high);
            chart.SetLow(low);
            chart.SetMount(mount);
            chart.SetMoney(money);
            return chart;
        }

        public int IndexOfTime(double time)
        {
            throw new NotImplementedException();
        }

        public string PrintAll()
        {
            throw new NotImplementedException();
        }

        #region 得到完整数据

        private IList<double> arr_time;

        public IList<double> Arr_Time
        {
            get
            {
                return null;
            }
        }

        private IList<float> arr_start;

        public IList<float> Arr_Start
        {
            get
            {
                return null;
            }
        }

        private IList<float> arr_high;
        public IList<float> Arr_High
        {
            get
            {
                return null;
            }
        }

        private IList<float> arr_low;

        public IList<float> Arr_Low
        {
            get
            {
                return null;
            }
        }
        private IList<float> arr_end;

        public IList<float> Arr_End
        {
            get
            {
                return null;
            }
        }

        private IList<int> arr_mount;

        public IList<int> Arr_Mount
        {
            get
            {
                return null;
            }
        }
        private IList<float> arr_money;

        public IList<float> Arr_Money
        {
            get
            {
                return null;
            }
        }
        private IList<int> arr_hold;
        public IList<int> Arr_Hold
        {
            get
            {
                return null;
            }
        }
        private IList<float> arr_height;
        /// <summary>
        /// 得到每个k线的振幅数组
        /// </summary>
        public IList<float> Arr_Height
        {
            get
            {
                return null;
            }
        }
        private IList<float> arr_heightPercent;
        /// <summary>
        /// 得到每个k线的振幅百分比数组
        /// </summary>
        public IList<float> Arr_HeightPercent
        {
            get
            {
                return null;
            }
        }
        private IList<float> arr_blockHigh;
        public IList<float> Arr_BlockHigh
        {
            get
            {
                return null;
            }
        }
        private IList<float> arr_BlockLow;
        public IList<float> Arr_BlockLow
        {
            get
            {
                return null;
            }
        }
        private IList<float> arr_blockHeight;
        public IList<float> Arr_BlockHeight
        {
            get
            {
                return null;
            }
        }
        private IList<float> arr_blockHeightPercent;
        public IList<float> Arr_BlockHeightPercent
        {
            get
            {
                return null;
            }
        }

        private IList<float> arr_upPercent;

        public IList<float> Arr_UpPercent
        {
            get
            {
                return null;
            }
        }

        #endregion
    }
}