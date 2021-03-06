﻿using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;

namespace com.wer.sc.plugin.mock.historydata
{
    /// <summary>
    /// 没有加属性的类是无法搜索到的
    /// </summary>
    public class Plugin_HistoryData_Error : IPlugin_HistoryData
    {
        public List<CodeInfo> GetCodes()
        {
            throw new NotImplementedException();
        }

        public string GetDataPath()
        {
            return @"D:\SCTEST\MOCKDATA\";
        }

        public List<DayOpenTime> GetDayOpenTime(string code)
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            return "MOCK历史数据";
        }

        public string GetDescription()
        {
            return "MOCK出的历史数据，专用测试";
        }

        public IKLineData GetKLineData(string code, int startDate, int endDate, KLinePeriod klinePeriod)
        {
            throw new NotImplementedException();
        }

        public NeedsToUpdate GetNeedsToUpdate()
        {
            throw new NotImplementedException();
        }

        public List<int> GetOpenDates()
        {
            throw new NotImplementedException();
        }

        public ITickData GetTickData(string code, int date)
        {
            throw new NotImplementedException();
        }
    }
}
