﻿using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cnfutures.generator.tick.generator
{
    /// <summary>
    /// 期货指数的更新
    /// </summary>
    public class DataGenerator_TickData_Index
    {
        private String pluginSrcDataPath;
        private DataLoader_CodeInfo provider_CodeInfo;
        private DataLoader_OpenTime provider_OpenTime;
        private DataLoader_TickData provider_TickData;

        public DataGenerator_TickData_Index(String pluginSrcDataPath, DataLoader dataLoader)
        {
            this.pluginSrcDataPath = pluginSrcDataPath;
            this.provider_CodeInfo = dataLoader.DataLoader_CodeInfo;
            this.provider_OpenTime = dataLoader.DataLoader_OpenTime;
            this.provider_TickData = dataLoader.DataLoader_TickData;
        }

        private TickData GetAdjustedTickData(string code, int date)
        {
            Plugin_HistoryData_CnFutures historyData = new Plugin_HistoryData_CnFutures(pluginSrcDataPath);
            return (TickData)historyData.GetTickData(code, date);
        }

        public TickData Generate(String variety, int date)
        {
            String indexCode = variety + "13";
            TickData indexdata = GetAdjustedTickData(indexCode, date);
            if (indexdata != null)
                return indexdata;

            List<CodeInfo> codes = provider_CodeInfo.GetCodes(variety);
            List<TickData> tickData = new List<TickData>();
            for (int i = 0; i < codes.Count; i++)
            {
                CodeInfo code = codes[i];
                String upperCode = code.Code.ToUpper();
                if (upperCode.EndsWith("MI") || upperCode.EndsWith("13"))
                    continue;
                TickData data = GetAdjustedTickData(code.Code, date);
                tickData.Add(data);
            }

            List<double[]> openTime = this.provider_OpenTime.GetOpenTime(codes[0].Code, date);
            return Generate(tickData, openTime);
        }

        public TickData Generate(List<TickData> tickData, List<double[]> openTime)
        {
            List<double> times = GetTimeArr(tickData, openTime);

            TickData data = new TickData(times.Count);
            int[] currentIndeies = new int[tickData.Count];
            int[] lastIndeies = new int[tickData.Count];

            int[] holds = new int[tickData.Count];
            int[] mounts = new int[tickData.Count];
            for (int i = 0; i < times.Count; i++)
            {
                double time = times[i];
                data.arr_time[i] = time;
                CalcIndeies(tickData, time, currentIndeies, lastIndeies);
                CalcMount(tickData, mounts, lastIndeies, currentIndeies);
                CalcCurrent(data, i, tickData, mounts, lastIndeies);
            }

            return data;
        }

        //private void CalcCurrentHold(List<TickData> tickData, int[] holds, int[] lastIndeies, int[] currentIndeies)
        //{
        //    for (int i = 0; i < holds.Length; i++)
        //    {
        //        int hold = holds[i];
        //        holds[i] = hold + CalcCurrentHold(tickData[i], lastIndeies[i], currentIndeies[i]);
        //    }
        //}

        //private int CalcCurrentHold(TickData data, int lastIndex, int index)
        //{
        //    int hold = 0;
        //    for (int i = lastIndex + 1; i <= index; i++)
        //    {
        //        data.BarPos = i;
        //        hold += data.Add;
        //    }
        //    return hold;
        //}

        private void CalcMount(List<TickData> tickData, int[] mounts, int[] lastIndeies, int[] currentIndeies)
        {
            for (int i = 0; i < mounts.Length; i++)
            {
                mounts[i] = CalcMount(tickData[i], lastIndeies[i], currentIndeies[i]);
            }
        }

        private int CalcMount(TickData data, int lastIndex, int index)
        {
            int mount = 0;
            for (int i = lastIndex + 1; i <= index; i++)
            {
                data.BarPos = i;
                mount += data.Mount;
            }
            return mount;
        }


        private List<double> GetTimeArr(List<TickData> tickData, List<double[]> openTime)
        {
            TickData mainTick = GetMainTickData(tickData);
            List<double> times = TimeUtils.GetKLineTimes(openTime, new KLinePeriod(KLinePeriod.TYPE_SECOND, 1));
            List<double> timeArr = new List<double>(times.Count);

            int dateStart = (int)mainTick.arr_time[0];
            int dateEnd = (int)mainTick.arr_time[mainTick.Length - 1];
            if (dateStart == dateEnd)
            {
                for (int i = 0; i < times.Count; i++)
                {
                    //times[i] = dateStart + times[i];
                    timeArr.Add(dateStart + times[i]);
                }
            }
            else
            {
                bool isNextDay = false;
                for (int i = 0; i < times.Count; i++)
                {
                    if (i != 0 && !isNextDay)
                        isNextDay = times[i - 1] > times[i];
                    int date = isNextDay ? dateEnd : dateStart;
                    //times[i] = date + times[i];
                    timeArr.Add(date + times[i]);
                }
            }

            return timeArr;
        }

        private TickData GetMainTickData(List<TickData> tickData)
        {
            TickData mainTick = tickData[0];
            for (int i = 1; i < tickData.Count; i++)
            {
                TickData tick = tickData[i];
                if (tick == null)
                    continue;
                if (mainTick == null || tick.Length > mainTick.Length)
                    mainTick = tick;
            }
            return mainTick;
        }

        private void CalcIndeies(List<TickData> data, double currentTime, int[] currentIndeies, int[] lastIndeies)
        {
            for (int i = 0; i < data.Count; i++)
            {
                TickData tickdata = data[i];
                if (tickdata == null)
                    continue;
                lastIndeies[i] = currentIndeies[i];
                int nextIndex = calcNextIndex(tickdata, currentIndeies[i], currentTime);
                currentIndeies[i] = nextIndex;
                tickdata.BarPos = nextIndex;
            }
        }

        private int calcNextIndex(TickData data, int currentTickIndex, double currentTime)
        {
            if (currentTickIndex + 1 >= data.Length)
                return currentTickIndex;

            double nextTickTime = data.arr_time[currentTickIndex + 1];
            if (nextTickTime <= currentTime)
                return currentTickIndex + 1;
            return currentTickIndex;
        }

        private bool isNextOverTime(TickData data, int currentTickIndex, double time)
        {
            if (currentTickIndex + 1 >= data.Length)
                return false;
            double nextTickTime = data.arr_time[currentTickIndex + 1];
            return nextTickTime >= time;
        }

        private void CalcCurrent(TickData data, int currentTickIndex, List<TickData> currentData, int[] mounts, int[] lastIndeies)
        {
            float price = 0;
            int mount = 0;
            int totalMount = 0;
            int hold = 0;
            int add = 0;
            for (int i = 0; i < currentData.Count; i++)
            {
                TickData tickdata = currentData[i];
                if (tickdata == null)
                    continue;
                price += tickdata.Price * tickdata.Hold;
                mount += mounts[i];
                totalMount += tickdata.TotalMount;
                hold += tickdata.Hold;
                if (currentTickIndex == 0)
                    add += tickdata.Hold;
                else
                    add += tickdata.Hold - tickdata.Arr_Hold[lastIndeies[i]];
            }
            price = (float)Math.Round((float)(price / hold), 2);
            data.arr_price[currentTickIndex] = price;
            data.arr_mount[currentTickIndex] = mount;
            data.arr_totalMount[currentTickIndex] = totalMount;
            data.arr_add[currentTickIndex] = add;
        }
    }
}