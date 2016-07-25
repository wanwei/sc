using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// k线数据类，该类描述了一套完整的k线数据
    /// </summary>
    public class KLineData
    {
        private KLinePeriod period;

        public String code;

        public double[] arr_time;

        public float[] arr_start;

        public float[] arr_high;

        public float[] arr_low;

        public float[] arr_end;

        public int[] arr_mount;

        public float[] arr_money;

        public int[] arr_hold;

        private int barPos;
        public KLineData()
        {

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

        public double FullTime
        {
            get
            {
                return arr_time[barPos];
            }
        }

        public int Date
        {
            get { return (int)arr_time[barPos]; }
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
                return arr_start[barPos];
            }
        }

        public float High
        {
            get
            {
                return arr_high[BarPos];
            }
        }

        public float Low
        {
            get
            {
                return arr_low[BarPos];
            }
        }

        public float End
        {
            get
            {
                return arr_end[BarPos];
            }
        }

        public int Mount
        {
            get
            {
                return arr_mount[BarPos];
            }
        }

        public float Money
        {
            get
            {
                return arr_money[BarPos];
            }
        }

        public int Hold
        {
            get
            {
                return arr_hold[BarPos];
            }
        }

        public int Length
        {
            get
            {
                return arr_end.Length;
            }
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

        public KLinePeriod Period
        {
            get
            {
                if (period == null)
                    period = GetPeriod(arr_time[0], arr_time[1]);
                return period;
            }
        }

        public KLineData SubData(int start, int end)
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

        public static KLineData Merge(List<KLineData> dataList)
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
                KLineData d1 = dataList[i];
                Copy(data, offset, d1, 0, d1.Length);
                offset += d1.Length;
            }

            return data;
        }

        private static void Copy(KLineData targetData, int targetIndex, KLineData srcData, int srcIndex, int length)
        {
            for (int i = srcIndex; i < srcIndex + length; i++)
            {
                int currentTargetIndex = targetIndex + srcIndex + i;
                targetData.arr_time[currentTargetIndex] = srcData.arr_time[i];
                targetData.arr_start[currentTargetIndex] = srcData.arr_start[i];
                targetData.arr_high[currentTargetIndex] = srcData.arr_high[i];
                targetData.arr_low[currentTargetIndex] = srcData.arr_low[i];
                targetData.arr_end[currentTargetIndex] = srcData.arr_end[i];
                targetData.arr_mount[currentTargetIndex] = srcData.arr_mount[i];
                targetData.arr_money[currentTargetIndex] = srcData.arr_money[i];
                targetData.arr_hold[currentTargetIndex] = srcData.arr_hold[i];
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
    }
}
