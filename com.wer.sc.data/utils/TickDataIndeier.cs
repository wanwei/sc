using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.utils
{
    /// <summary>
    /// tick数据索引器
    /// 索引器会索引出
    /// </summary>
    public class TickDataIndeier
    {
        private ITickData tickData;
        private IKLineData klineData;

        //1天k线的开盘时间分割线，记录的是k线index
        private List<int> klineOpenSplits = new List<int>();

        //tick数据基于k线周期的分割点
        private List<int> tickSplitIndeies = new List<int>();

        //通过时间查找对应的分割号
        private Dictionary<double, int> dicTimeTickSplitIndex = new Dictionary<double, int>();

        private Dictionary<int, int> dicKLineIndexTickSplitIndex = new Dictionary<int, int>();

        public TickDataIndeier(ITickData tickData, IKLineData klineData)
        {
            this.tickData = tickData;
            this.klineData = klineData;
            this.InitKLineSplits();
            this.DoIndex();
        }

        private void InitKLineSplits()
        {
            for (int i = 1; i < klineData.Length; i++)
            {
                double lasttime = klineData.Arr_Time[i - 1];
                double time = klineData.Arr_Time[i];
                if (lasttime < time)
                {
                    TimeSpan span = TimeUtils.Substract(time, lasttime);
                    int minutes = span.Minutes + span.Hours * 60;
                    if (minutes > 10)
                    {
                        klineOpenSplits.Add(i);
                    }
                }
            }
        }

        private void DoIndex()
        {
            int currentTickIndex = 0;
            int startKLineIndex = 0;

            int endKLineIndex = klineOpenSplits[0] - 1;
            //第一个时间段的分割点以0记,单独添加
            //这样方便通过tick数据生成k线数据
            //AddTickSplitIndex(0, 0);

            double endTime = klineData.Arr_Time[0];
            currentTickIndex = CalcTickSplitIndex(0, endTime);
            AddTickSplitIndex(currentTickIndex, 0);

            currentTickIndex = DoIndex_Period(startKLineIndex, endKLineIndex, currentTickIndex);
            //按k线开盘时间段依次索引
            for (int i = 0; i < klineOpenSplits.Count; i++)
            {
                startKLineIndex = klineOpenSplits[i];
                if (i == klineOpenSplits.Count - 1)
                    endKLineIndex = klineData.Length - 1;
                else
                    endKLineIndex = klineOpenSplits[i + 1] - 1;
                currentTickIndex = DoIndex_Period(startKLineIndex, endKLineIndex, currentTickIndex);
            }
        }

        private void AddTickSplitIndex(int tickIndex, int klineIndex)
        {
            tickSplitIndeies.Add(tickIndex);
            dicKLineIndexTickSplitIndex.Add(klineIndex, tickIndex);
            dicTimeTickSplitIndex.Add(klineData.Arr_Time[klineIndex], tickIndex);
        }

        private int DoIndex_Period(int startKLineIndex, int endKLineIndex, int currentTickIndex)
        {
            for (int i = startKLineIndex; i <= endKLineIndex; i++)
            {
                if (i == klineData.Length - 1)
                    return tickData.Length - 1;

                double endTime = klineData.Arr_Time[i + 1];
                currentTickIndex = CalcTickSplitIndex(currentTickIndex < 0 ? 0 : currentTickIndex, endTime);
                AddTickSplitIndex(currentTickIndex, i + 1);
            }
            return currentTickIndex;
        }

        private int CalcTickSplitIndex(int tickIndex, double endTime)
        {
            while (tickIndex < tickData.Length && tickData.Arr_Time[tickIndex] < endTime)
                tickIndex += 1;
            return tickIndex - 1;
        }

        public int GetTickSplitIndex(int index)
        {
            if (dicKLineIndexTickSplitIndex.ContainsKey(index))
                return dicKLineIndexTickSplitIndex[index];
            return -1;
        }

        public int GetTickSplitIndex(double time)
        {
            if (dicTimeTickSplitIndex.ContainsKey(time))
                return dicTimeTickSplitIndex[time];
            return -1;
        }

        public int GetTickIndex(double time)
        {
            double splitTime = Math.Round(time, 4);
            int index = GetTickSplitIndex(splitTime);
            while (index < tickData.Length)
            {
                double currentTime = tickData.Arr_Time[index];
                if (currentTime >= time)
                    return index;
                index++;
            }
            return index - 1;
        }

        public List<int> GetAllTickSplitIndex()
        {
            return tickSplitIndeies;
        }
    }
}