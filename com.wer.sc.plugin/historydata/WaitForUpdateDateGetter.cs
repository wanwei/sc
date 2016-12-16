using com.wer.sc.data;
using com.wer.sc.data.opentime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.historydata
{
    /// <summary>
    /// 该类根据现在已经更新的数据得到需要更新的Tick和KLine的日期
    /// </summary>
    public class WaitForUpdateDateGetter
    {
        private IOpenDateReader_HistoryData historyDataInfoLoader;

        private List<int> openDates;

        public WaitForUpdateDateGetter(IOpenDateReader_HistoryData historyDataInfoLoader, List<int> openDates)
        {
            //this.historyDataInfoLoader = new HistoryDataInfoLoader(srcDataPath);
            this.historyDataInfoLoader = historyDataInfoLoader;
            this.openDates = openDates;
        }

        public List<int> GetWaitForUpdateOpenDates_TickData(string code)
        {
            List<int> codeOpenDates = this.historyDataInfoLoader.GetOpenDates_TickData(code);
            if (codeOpenDates == null || codeOpenDates.Count == 0)
                return this.openDates;
            OpenDateCache cache = new OpenDateCache(codeOpenDates);
            return GetWaitForUpdateOpenDates(this.openDates, cache);
        }

        public List<int> GetWaitForUpdateOpenDates_TickData_FillUp(string code)
        {
            List<int> codeOpenDates = this.historyDataInfoLoader.GetOpenDates_TickData(code);
            if (codeOpenDates == null || codeOpenDates.Count == 0)
                return this.openDates;
            OpenDateCache cache = new OpenDateCache(codeOpenDates);
            return GetWaitForUpdateOpenDates_FillUp(this.openDates, cache);
        }

        public List<int> GetWaitForUpdateOpenDates_KLineData(string code, KLinePeriod period)
        {
            List<int> codeOpenDates = this.historyDataInfoLoader.GetOpenDates_KLineData(code, period);
            if (codeOpenDates == null || codeOpenDates.Count == 0)
                return this.openDates;
            OpenDateCache cache = new OpenDateCache(codeOpenDates);
            return GetWaitForUpdateOpenDates(this.openDates, cache);
        }

        public List<int> GetWaitForUpdateOpenDates_KLineData_FillUp(string code, KLinePeriod period)
        {
            List<int> codeOpenDates = this.historyDataInfoLoader.GetOpenDates_KLineData(code, period);
            if (codeOpenDates == null || codeOpenDates.Count == 0)
                return this.openDates;
            OpenDateCache cache = new OpenDateCache(codeOpenDates);
            return GetWaitForUpdateOpenDates_FillUp(this.openDates, cache);
        }

        private static List<int> GetWaitForUpdateOpenDates(List<int> allOpenDates, OpenDateCache currentOpenDateCache)
        {
            if (currentOpenDateCache.GetAllOpenDates().Count == 0)
                return allOpenDates;
            int date = currentOpenDateCache.LastOpenDate;
            int index = allOpenDates.IndexOf(date);
            if (index == allOpenDates.Count - 1)
                return new List<int>();
            int startIndex = index + 1;
            return allOpenDates.GetRange(startIndex, allOpenDates.Count - startIndex);
        }

        private static List<int> GetWaitForUpdateOpenDates_FillUp(List<int> allOpenDates, OpenDateCache currentOpenDateCache)
        {
            List<int> waitForUpdateOpenDates = new List<int>();
            for (int i = 0; i < allOpenDates.Count; i++)
            {
                int openDate = allOpenDates[i];
                if (!currentOpenDateCache.IsOpen(openDate))
                {
                    waitForUpdateOpenDates.Add(openDate);
                }
            }
            return waitForUpdateOpenDates;
        }
    }
}
