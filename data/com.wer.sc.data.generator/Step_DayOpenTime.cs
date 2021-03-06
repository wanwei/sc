﻿using com.wer.sc.data.opentime;
using com.wer.sc.data.store;
using com.wer.sc.data.utils;
using com.wer.sc.plugin;
using com.wer.sc.utils.ui.proceed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.generator
{
    public class Step_DayOpenTime : IStep
    {
        private string code;

        private IPlugin_HistoryData historyData;

        private DayOpenTimeStore dayStartTimeStore;

        public Step_DayOpenTime(string code, IPlugin_HistoryData historyData, DataPathUtils utils)
        {
            this.code = code;
            this.historyData = historyData;
            this.dayStartTimeStore = new DayOpenTimeStore(utils.GetDayOpenTime(code));
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
            List<DayOpenTime> dayStartTimes = historyData.GetDayOpenTime(code);
            dayStartTimeStore.Save(dayStartTimes);
            return "更新" + code + "的开盘时间完毕";
        }
    }
}