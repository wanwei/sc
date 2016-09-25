using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    public class TimeLineData : ITimeLineData
    {
        public String code;

        public int date;

        /// <summary>
        /// 昨日收盘
        /// </summary>
        public float yesterdayEnd;

        /// <summary>
        /// 全日时间
        /// </summary>
        public double[] arr_time;

        /// <summary>
        /// 全日价格
        /// </summary>
        public float[] arr_price;

        /// <summary>
        /// 全日成交
        /// </summary>
        public int[] arr_mount;

        /// <summary>
        /// 全天持仓数据
        /// </summary>
        public int[] arr_hold;

        private int barPos;

        public TimeLineData(int date, float yesterdayEnd, int length)
        {
            this.date = date;
            this.yesterdayEnd = yesterdayEnd;
            arr_time = new double[length];
            arr_price = new float[length];
            arr_mount = new int[length];
            arr_hold = new int[length];
        }

        public String Code
        {
            get { return code; }
        }

        public int BarPos
        {
            get
            {
                return barPos;
            }

            set
            {
                barPos = value;
            }
        }

        public void SetBarPosByTime(double time)
        {
            int index = IndexOfTime(time);
            this.barPos = index;
        }

        public float YesterdayEnd
        {
            get
            {
                return yesterdayEnd;
            }
        }

        public double FullTime
        {
            get
            {
                return Arr_Time[barPos];
            }
        }

        public int Date
        {
            get { return (int)Arr_Time[Length - 1]; }
        }

        public double Time
        {
            get
            {
                return FullTime - Date;
            }
        }

        public float Price
        {
            get
            {
                return Arr_Price[barPos];
            }
        }

        public float UpRange
        {
            get
            {
                return Arr_UpRange[BarPos];
            }
        }

        public float UpPercent
        {
            get
            {
                return Arr_UpPercent[BarPos];
            }
        }

        public int Mount
        {
            get
            {
                return Arr_Mount[BarPos];
            }
        }

        public int Hold
        {
            get
            {
                return arr_hold[BarPos];
            }
        }

        public ITimeLineChart GetCurrentChart()
        {
            return new TimeLineChart_RealData(this, BarPos);
        }

        public ITimeLineChart GetCurrentChart(int index)
        {
            return new TimeLineChart_RealData(this, index);
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

        public int Length
        {
            get
            {
                return arr_price.Length;
            }
        }

        public IList<double> Arr_Time
        {
            get
            {
                return arr_time;
            }
        }

        private ReadOnlyList_TmpValue<float> list_Price;

        public IList<float> Arr_Price
        {
            get
            {
                if (list_Price == null)
                    list_Price = new ReadOnlyList_TmpValue<float>(arr_price);
                return list_Price;
            }
        }

        private ReadOnlyList_TmpValue<int> list_Mount;

        public IList<int> Arr_Mount
        {
            get
            {
                if (list_Mount == null)
                    list_Mount = new ReadOnlyList_TmpValue<int>(arr_mount);
                return list_Mount;
            }
        }

        private ReadOnlyList_TmpValue<int> list_Hold;

        public IList<int> Arr_Hold
        {
            get
            {
                if (list_Hold == null)
                    list_Hold = new ReadOnlyList_TmpValue<int>(arr_hold);
                return list_Hold;
            }
        }

        private float[] arr_UpPercent;
        private ReadOnlyList_TmpValue<float> list_UpPercent;

        public IList<float> Arr_UpPercent
        {
            get
            {
                if (arr_UpPercent == null)
                {
                    arr_UpPercent = new float[Length];
                    for (int i = 0; i < Length; i++)
                    {
                        float p = arr_price[i];
                        arr_UpPercent[i] = (float)Math.Round((p - yesterdayEnd) / p * 100, 2);
                    }
                    list_UpPercent = new ReadOnlyList_TmpValue<float>(arr_UpPercent);
                }
                return list_UpPercent;
            }
        }

        private float[] arr_UpRange;
        private ReadOnlyList_TmpValue<float> list_UpRange;

        public IList<float> Arr_UpRange
        {
            get
            {
                if (arr_UpRange == null)
                {
                    arr_UpRange = new float[Length];
                    for (int i = 0; i < Length; i++)
                    {
                        float p = arr_price[i];
                        arr_UpRange[i] = (float)Math.Round(p - yesterdayEnd, 2);
                    }
                    list_UpRange = new ReadOnlyList_TmpValue<float>(arr_UpRange);
                }
                return list_UpRange;
            }
        }

        public int IndexOfTime(double time)
        {
            double t = Math.Round(time, 4);
            return this.Arr_Time.IndexOf(t);
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(FullTime).Append(",");
            sb.Append(Price).Append(",");
            sb.Append(UpRange).Append(",");
            sb.Append(UpPercent).Append(",");
            sb.Append(Mount).Append(",");
            sb.Append(Hold);
            return sb.ToString();
        }

        public string PrintAll()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("barpos:").Append(BarPos).Append("/r/n");
            for (int i = 0; i < Length; i++)
            {
                sb.Append(Arr_Time).Append(",");
                sb.Append(Arr_Price).Append(",");
                sb.Append(Arr_UpRange).Append(",");
                sb.Append(Arr_UpPercent).Append(",");
                sb.Append(Arr_Mount).Append(",");
                sb.Append(Arr_Hold).Append("/r/n");
            }
            return sb.ToString();
        }
    }
}
