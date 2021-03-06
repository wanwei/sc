﻿using com.wer.sc.data.opentime;
using com.wer.sc.data.utils;
using com.wer.sc.plugin.historydata.csv;
using com.wer.sc.utils.ui.proceed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cnfutures.generator
{
    public class Step_DayStartTime : IStep
    {
        private string code;

        private List<int> dates;

        private DataLoader dataLoader;

        public Step_DayStartTime(string code, DataLoader dataLoader)
        {
            this.code = code;
            this.dataLoader = dataLoader;
        }

        public int ProgressStep
        {
            get
            {
                return 10;
            }
        }

        public string StepDesc
        {
            get
            {
                return "更新" + code + "的开盘时间";
            }
        }

        public string Proceed()
        {
            List<DayOpenTime> result = GetAllDayStartTimes();
            if (result == null)
                return code + "的开盘时间已经是最新的，不需要更新";
            string path = CsvHistoryDataPathUtils.GetDayOpenTimePath(dataLoader.PluginSrcDataPath, code);
            CsvUtils_DayStartTime.Save(path, result);
            return "更新完成" + code + "的开盘时间";
        }

        /// <summary>
        /// 得到该合约的所有开盘时间，如果返回空，则表示现在数据已经是最新的
        /// </summary>
        /// <returns></returns>
        public List<DayOpenTime> GetAllDayStartTimes()
        {
            List<DayOpenTime> dayStartTimes = dataLoader.Plugin_HistoryData.GetDayOpenTime(code);
            DataLoader_OpenDate dataLoader_OpenDate = dataLoader.DataLoader_OpenDate;
            IOpenDateReader openDateReader = dataLoader_OpenDate.GetOpenDateReader();
            int firstIndex = 0;
            if (dayStartTimes != null && dayStartTimes.Count != 0)
            {
                int lastDate = dayStartTimes[dayStartTimes.Count - 1].Date;
                if (lastDate == openDateReader.LastOpenDate)
                    return null;
                int lastIndex = openDateReader.GetOpenDateIndex(lastDate);
                firstIndex = lastIndex + 1;
            }
            List<int> openDates = openDateReader.GetAllOpenDates();
            List<DayOpenTime> updateStartTimes = CalcDayOpenTime(openDates, firstIndex, openDates.Count - 1, dataLoader.DataLoader_OpenTime);

            List<DayOpenTime> result = new List<DayOpenTime>();
            if (dayStartTimes != null)
                result.AddRange(dayStartTimes);
            result.AddRange(updateStartTimes);
            return result;
        }

        private List<DayOpenTime> CalcDayOpenTime(List<int> openDates, int startIndex, int endIndex, DataLoader_OpenTime dataLoader_OpenTime)
        {
            List<DayOpenTime> dayStartTimes = new List<DayOpenTime>();
            for (int i = startIndex; i <= endIndex; i++)
            {
                int date = openDates[i];

                List<double[]> openTime = dataLoader_OpenTime.GetOpenTime(code, date);
                double startTime = openTime[0][0];
                double endTime = openTime[openTime.Count - 1][1];
                if (startTime > 0.18)
                {
                    if (i == 0)
                        throw new ArgumentException("传入的" + date + "有夜盘，必须传入其之前的日期");
                    dayStartTimes.Add(new DayOpenTime(date, openDates[i - 1] + startTime, openDates[i] + endTime));
                }
                else
                {
                    dayStartTimes.Add(new DayOpenTime(date, date + startTime, date + endTime));
                }
            }
            return dayStartTimes;
        }
        public override string ToString()
        {
            return StepDesc;
        }
    }
}