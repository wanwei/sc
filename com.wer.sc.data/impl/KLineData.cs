using com.wer.sc.data.utils;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.wer.sc.data
{
    /// <summary>
    /// k线数据类，该类描述了一套完整的k线数据
    /// </summary>
    public class KLineData : IKLineData
    {
        private KLinePeriod period;

        private String code;

        public double[] arr_time;

        public float[] arr_start;

        public float[] arr_high;

        public float[] arr_low;

        public float[] arr_end;

        public int[] arr_mount;

        public float[] arr_money;

        public int[] arr_hold;

        private int barPos;

        private KLineDataIndeier indeier;

        public KLineData()
        {
            //this.indeier = new KLineDataIndeier(this);
            //this.indeier = new KLineDataIndeier(this);
        }

        public KLineData(int length)
        {
            arr_time = new double[length];
            arr_start = new float[length];
            arr_high = new float[length];
            arr_low = new float[length];
            arr_end = new float[length];
            arr_mount = new int[length];
            arr_money = new float[length];
            arr_hold = new int[length];
        }

        public string Code
        {
            get
            {
                return code;
            }

            set
            {
                code = value;
            }
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
            this.barPos = IndexOfTime(time);
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
            get { return (int)Arr_Time[barPos]; }
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
                return Arr_Start[barPos];
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

        public void ChangeChart(IKLineChart chart, int index)
        {
            ReadOnlyList_TmpValue<double> timelist = (ReadOnlyList_TmpValue<double>)Arr_Time;
            ReadOnlyList_TmpValue<float> startlist = (ReadOnlyList_TmpValue<float>)Arr_Start;
            ReadOnlyList_TmpValue<float> highlist = (ReadOnlyList_TmpValue<float>)Arr_High;
            ReadOnlyList_TmpValue<float> lowlist = (ReadOnlyList_TmpValue<float>)Arr_Low;
            ReadOnlyList_TmpValue<float> endlist = (ReadOnlyList_TmpValue<float>)Arr_End;
            ReadOnlyList_TmpValue<int> mountlist = (ReadOnlyList_TmpValue<int>)Arr_Mount;
            ReadOnlyList_TmpValue<float> moneylist = (ReadOnlyList_TmpValue<float>)Arr_Money;
            ReadOnlyList_TmpValue<int> holdlist = (ReadOnlyList_TmpValue<int>)Arr_Hold;
            if (chart == null)
            {
                timelist.ClearTmpValue();
                startlist.ClearTmpValue();
                highlist.ClearTmpValue();
                lowlist.ClearTmpValue();
                endlist.ClearTmpValue();
                mountlist.ClearTmpValue();
                moneylist.ClearTmpValue();
                holdlist.ClearTmpValue();
            }
            else
            {
                timelist.SetTmpValue(index, chart.Time);
                startlist.SetTmpValue(index, chart.Start);
                highlist.SetTmpValue(index, chart.High);
                lowlist.SetTmpValue(index, chart.Low);
                endlist.SetTmpValue(index, chart.End);
                mountlist.SetTmpValue(index, chart.Mount);
                moneylist.SetTmpValue(index, chart.Money);
                holdlist.SetTmpValue(index, chart.Hold);
            }
        }

        /// <summary>
        /// 修改当前chart，
        /// </summary>
        /// <param name="chart"></param>
        public void ChangeChart(IKLineChart chart)
        {
            ChangeChart(chart, BarPos);
        }

        public int Length
        {
            get
            {
                return arr_end.Length;
            }
        }

        public KLinePeriod Period
        {
            get
            {
                if (period == null)
                    period = GetPeriod(arr_time[0], arr_time[1]);
                return period;
            }
        }

        public IKLineData GetRange(int start, int end)
        {
            KLineData data = this;
            KLineData d1 = new KLineData(end - start + 1);
            for (int i = start; i <= end; i++)
            {
                d1.arr_time[i - start] = data.arr_time[i];
                d1.arr_start[i - start] = data.arr_start[i];
                d1.arr_high[i - start] = data.arr_high[i];
                d1.arr_low[i - start] = data.arr_low[i];
                d1.arr_end[i - start] = data.arr_end[i];
                d1.arr_mount[i - start] = data.arr_mount[i];
                d1.arr_money[i - start] = data.arr_money[i];
                d1.arr_hold[i - start] = data.arr_hold[i];
            }
            return d1;
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

        #region 得到完整数据

        private ReadOnlyList_TmpValue<double> list_Time;

        public IList<double> Arr_Time
        {
            get
            {
                if (list_Time == null)
                    list_Time = new ReadOnlyList_TmpValue<double>(arr_time);
                return list_Time;
            }
        }

        private ReadOnlyList_TmpValue<float> list_Start;

        public IList<float> Arr_Start
        {
            get
            {
                if (list_Start == null)
                    list_Start = new ReadOnlyList_TmpValue<float>(arr_start);
                return list_Start;
            }
        }

        private ReadOnlyList_TmpValue<float> list_High;

        public IList<float> Arr_High
        {
            get
            {
                if (list_High == null)
                    list_High = new ReadOnlyList_TmpValue<float>(arr_high);
                return list_High;
            }
        }

        private ReadOnlyList_TmpValue<float> list_Low;

        public IList<float> Arr_Low
        {
            get
            {
                if (list_Low == null)
                    list_Low = new ReadOnlyList_TmpValue<float>(arr_low);
                return list_Low;
            }
        }

        private ReadOnlyList_TmpValue<float> list_End;

        public IList<float> Arr_End
        {
            get
            {
                if (list_End == null)
                    list_End = new ReadOnlyList_TmpValue<float>(arr_end);
                return list_End;
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

        private ReadOnlyList_TmpValue<float> list_Money;

        public IList<float> Arr_Money
        {
            get
            {
                if (list_Money == null)
                    list_Money = new ReadOnlyList_TmpValue<float>(arr_money);
                return list_Money;
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

        private ReadOnlyList_TmpValue<float> list_height;

        /// <summary>
        /// 得到每个k线的振幅数组
        /// </summary>
        public IList<float> Arr_Height
        {
            get
            {
                if (list_height != null)
                    return list_height;

                float[] arr_height = new float[Length];
                for (int i = 0; i < arr_height.Length; i++)
                    arr_height[i] = Arr_High[i] - Arr_Low[i];
                list_height = new ReadOnlyList_TmpValue<float>(arr_height);
                return list_height;
            }
        }

        private ReadOnlyList_TmpValue<float> list_heightPercent;

        /// <summary>
        /// 得到每个k线的振幅百分比数组
        /// </summary>
        public IList<float> Arr_HeightPercent
        {
            get
            {
                if (list_heightPercent != null)
                    return list_heightPercent;
                float[] arr_HeightPercent = new float[Length];
                for (int i = 0; i < arr_HeightPercent.Length; i++)
                {
                    arr_HeightPercent[i] = (float)NumberUtils.percent(Math.Abs(Arr_Start[i] - Arr_End[i]), Arr_End[i]);
                }
                list_heightPercent = new ReadOnlyList_TmpValue<float>(arr_HeightPercent);
                return list_heightPercent;
            }
        }

        private ReadOnlyList_TmpValue<float> list_blockHigh;

        public IList<float> Arr_BlockHigh
        {
            get
            {
                if (list_blockHigh != null)
                    return list_blockHigh;
                float[] arr_blockhigh = new float[Length];
                for (int i = 0; i < Length; i++)
                {
                    arr_blockhigh[i] = Arr_Start[i] > Arr_End[i] ? Arr_Start[i] : Arr_End[i];
                }
                list_blockHigh = new ReadOnlyList_TmpValue<float>(arr_blockhigh);
                return list_blockHigh;
            }
        }


        private ReadOnlyList_TmpValue<float> list_blockLow;

        public IList<float> Arr_BlockLow
        {
            get
            {
                if (list_blockLow != null)
                    return list_blockLow;
                float[] arr_blocklow = new float[Length];
                for (int i = 0; i < Length; i++)
                {
                    arr_blocklow[i] = Arr_Start[i] < Arr_End[i] ? Arr_Start[i] : Arr_End[i];
                }
                list_blockLow = new ReadOnlyList_TmpValue<float>(arr_blocklow);
                return list_blockLow;
            }
        }

        private ReadOnlyList_TmpValue<float> list_blockHeight;

        public IList<float> Arr_BlockHeight
        {
            get
            {
                if (list_blockHeight != null)
                    return list_blockHeight;
                float[] arr_blockheight = new float[Length];
                for (int i = 0; i < Length; i++)
                {
                    arr_blockheight[i] = Math.Abs(Arr_Start[i] - Arr_End[i]);
                }
                list_blockHeight = new ReadOnlyList_TmpValue<float>(arr_blockheight);
                return list_blockHeight;
            }
        }

        private ReadOnlyList_TmpValue<float> list_percentBlockHeight;

        public IList<float> Arr_BlockHeightPercent
        {
            get
            {
                if (list_percentBlockHeight != null)
                    return list_percentBlockHeight;
                float[] arr_percentBlockHeight = new float[Length];
                for (int i = 0; i < Length; i++)
                {
                    arr_percentBlockHeight[i] = (float)NumberUtils.percent(Math.Abs(Arr_Start[i] - Arr_End[i]), Arr_End[i]);
                }
                list_percentBlockHeight = new ReadOnlyList_TmpValue<float>(arr_percentBlockHeight);
                return list_percentBlockHeight;
            }
        }

        private ReadOnlyList_TmpValue<float> list_upPercent;

        public IList<float> Arr_UpPercent
        {
            get
            {
                if (list_upPercent != null)
                    return list_upPercent;
                float[] arr_UpPercent = new float[Length];
                for (int i = 0; i < Length; i++)
                {
                    if (i == 0)
                        arr_UpPercent[i] = (float)NumberUtils.percent(Arr_End[i] - Arr_Start[i], Arr_Start[i]);
                    else
                        arr_UpPercent[i] = (float)NumberUtils.percent(Arr_End[i] - Arr_End[i - 1], Arr_End[i - 1]);
                }
                list_upPercent = new ReadOnlyList_TmpValue<float>(arr_UpPercent);
                return list_upPercent;
            }
        }

        #endregion

        public static IKLineData Merge(List<IKLineData> dataList)
        {
            int len = 0;
            for (int i = 0; i < dataList.Count; i++)
            {
                len += dataList[i].Length;
            }
            KLineData data = new KLineData(len);
            int offset = 0;
            for (int i = 0; i < dataList.Count; i++)
            {
                IKLineData d1 = dataList[i];
                Copy(data, offset, d1, 0, d1.Length);
                offset += d1.Length;
            }

            return data;
        }

        private static void Copy(KLineData targetData, int targetIndex, IKLineData srcData, int srcIndex, int length)
        {
            for (int i = srcIndex; i < srcIndex + length; i++)
            {
                int currentTargetIndex = targetIndex + srcIndex + i;
                targetData.arr_time[currentTargetIndex] = srcData.Arr_Time[i];
                targetData.arr_start[currentTargetIndex] = srcData.Arr_Start[i];
                targetData.arr_high[currentTargetIndex] = srcData.Arr_High[i];
                targetData.arr_low[currentTargetIndex] = srcData.Arr_Low[i];
                targetData.arr_end[currentTargetIndex] = srcData.Arr_End[i];
                targetData.arr_mount[currentTargetIndex] = srcData.Arr_Mount[i];
                targetData.arr_money[currentTargetIndex] = srcData.Arr_Money[i];
                targetData.arr_hold[currentTargetIndex] = srcData.Arr_Hold[i];
            }
        }

        override
        public String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(FullTime).Append(",");
            sb.Append(Start).Append(",");
            sb.Append(High).Append(",");
            sb.Append(Low).Append(",");
            sb.Append(End).Append(",");
            sb.Append(Mount).Append(",");
            sb.Append(Money).Append(",");
            sb.Append(Hold);
            return sb.ToString();
        }

        public string PrintAll()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("barpos:").Append(BarPos).Append("/r/n");
            for (int i = 0; i < Length; i++)
            {
                sb.Append(Arr_Time[i]).Append(",");
                sb.Append(Arr_Start[i]).Append(",");
                sb.Append(Arr_High[i]).Append(",");
                sb.Append(Arr_Low[i]).Append(",");
                sb.Append(Arr_End[i]).Append(",");
                sb.Append(Arr_Mount[i]).Append(",");
                sb.Append(Arr_Money[i]).Append(",");
                sb.Append(Arr_Hold[i]).Append("/r/n");
            }
            return sb.ToString();
        }

        public static KLinePeriod GetPeriod(double time1, double time2)
        {

            KLinePeriod period = new KLinePeriod();
            double timeBetween = time2 - time1;
            if (timeBetween < 0.0001)
            {
                period.Period = (int)(timeBetween * 1000000);
                period.PeriodType = KLinePeriod.TYPE_SECOND;
            }
            else if (timeBetween < 0.01)
            {
                period.Period = (int)(timeBetween * 10000);
                period.PeriodType = KLinePeriod.TYPE_MINUTE;
            }
            else if (timeBetween < 1)
            {
                period.Period = (int)(timeBetween * 100);
                period.PeriodType = KLinePeriod.TYPE_HOUR;
            }
            else
            {
                period.Period = (int)timeBetween;
                period.PeriodType = KLinePeriod.TYPE_DAY;
            }
            return period;
        }

        public int IndexOfTime(double time)
        {
            if (this.indeier == null)
                this.indeier = new KLineDataIndeier(this, null);
            return this.indeier.GetTimeIndex(time);
            //return -1;
        }
    }
}