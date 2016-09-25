using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.impl
{
    public class TimeLineData_RealTime : ITimeLineData
    {
        private ITimeLineData timeLineData;
        private int barPos;

        private ReadOnlyList_TmpValue<double> list_time;
        private ReadOnlyList_TmpValue<float> list_price;
        private ReadOnlyList_TmpValue<int> list_mount;
        private ReadOnlyList_TmpValue<int> list_hold;
        private ReadOnlyList_TmpValue<float> list_upPercent;
        private ReadOnlyList_TmpValue<float> list_upRange;        

        public TimeLineData_RealTime(ITimeLineData timeLineData)
        {
            this.timeLineData = timeLineData;
            this.list_time = new ReadOnlyList_TmpValue<double>(timeLineData.Arr_Time);
            this.list_price = new ReadOnlyList_TmpValue<float>(timeLineData.Arr_Price);
            this.list_mount = new ReadOnlyList_TmpValue<int>(timeLineData.Arr_Mount);
            this.list_hold = new ReadOnlyList_TmpValue<int>(timeLineData.Arr_Hold);
            this.list_upPercent = new ReadOnlyList_TmpValue<float>(timeLineData.Arr_UpPercent);
            this.list_upRange = new ReadOnlyList_TmpValue<float>(timeLineData.Arr_UpRange);
        }

        public void ChangeChart(ITimeLineChart chart, int index)
        {
            ReadOnlyList_TmpValue<double> timelist = (ReadOnlyList_TmpValue<double>)Arr_Time;
            ReadOnlyList_TmpValue<float> pricelist = (ReadOnlyList_TmpValue<float>)Arr_Price;
            ReadOnlyList_TmpValue<int> mountlist = (ReadOnlyList_TmpValue<int>)Arr_Mount;
            ReadOnlyList_TmpValue<int> holdlist = (ReadOnlyList_TmpValue<int>)Arr_Hold;
            ReadOnlyList_TmpValue<float> upPercentlist = (ReadOnlyList_TmpValue<float>)Arr_UpPercent;
            ReadOnlyList_TmpValue<float> upRangelist = (ReadOnlyList_TmpValue<float>)Arr_UpRange;

            if (chart == null)
            {
                timelist.ClearTmpValue();
                pricelist.ClearTmpValue();
                mountlist.ClearTmpValue();
                holdlist.ClearTmpValue();
                upPercentlist.ClearTmpValue();
                upRangelist.ClearTmpValue();
            }
            else
            {
                timelist.SetTmpValue(index, chart.Time);
                pricelist.SetTmpValue(index, chart.Price);
                mountlist.SetTmpValue(index, chart.Mount);
                holdlist.SetTmpValue(index, chart.Hold);
                upPercentlist.SetTmpValue(index, chart.UpPercent);
                upRangelist.SetTmpValue(index, chart.UpRange);
            }
        }

        /// <summary>
        /// 修改当前chart，
        /// </summary>
        /// <param name="chart"></param>
        public void ChangeChart(ITimeLineChart chart)
        {
            ChangeChart(chart, BarPos);
        }

        public string Code
        {
            get
            {
                return timeLineData.Code;
            }
        }

        public float YesterdayEnd
        {
            get
            {
                return timeLineData.YesterdayEnd;
            }
        }

        public int Date
        {
            get
            {
                return timeLineData.Date;
            }
        }

        public int BarPos
        {
            get { return barPos; }
            set { this.barPos = value; }
        }

        public int IndexOfTime(double time)
        {
            return timeLineData.IndexOfTime(time);
        }

        public void SetBarPosByTime(double time)
        {
            this.barPos = IndexOfTime(time);
        }

        public double Time
        {
            get
            {
                return Arr_Time[barPos];
            }
        }

        public int Hold
        {
            get
            {
                return Arr_Hold[barPos];
            }
        }

        public int Mount
        {
            get
            {
                return Arr_Mount[barPos];
            }
        }

        public float Price
        {
            get
            {
                return Arr_Price[barPos];
            }
        }

        public float UpPercent
        {
            get
            {
                return Arr_UpPercent[barPos];
            }
        }

        public float UpRange
        {
            get
            {
                return Arr_UpRange[barPos];
            }
        }

        public ITimeLineChart GetCurrentChart()
        {
            return null;
        }

        public ITimeLineChart GetCurrentChart(int index)
        {
            return null;
        }

        #region 完整数据信息

        public int Length
        {
            get
            {
                return timeLineData.Length;
            }
        }

        public IList<double> Arr_Time
        {
            get
            {
                return list_time;
            }
        }

        public IList<float> Arr_Price
        {
            get
            {
                return list_price;
            }
        }

        public IList<int> Arr_Mount
        {
            get
            {
                return list_mount;
            }
        }

        public IList<int> Arr_Hold
        {
            get
            {
                return list_hold;
            }
        }

        public IList<float> Arr_UpPercent
        {
            get
            {
                return list_upPercent;
            }
        }

        public IList<float> Arr_UpRange
        {
            get
            {
                return list_upRange;
            }
        }

        #endregion

        public string PrintAll()
        {
            return null;
        }
    }
}
