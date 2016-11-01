using com.wer.sc.data;
using com.wer.sc.data.opentime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.historydata
{
    /// <summary>
    /// 历史数据准备类
    /// 该类会帮助获取需要更新哪些数据
    /// </summary>
    public class HistoryData_PrepareForUpdate
    {
        private List<CodeInfo> codes;
        private List<int> openDates;

        private UpdateDataInfoLoader loader;

        public HistoryData_PrepareForUpdate(string srcDataPath, List<CodeInfo> codes, List<int> openDates)
        {
            this.codes = codes;
            this.openDates = openDates;
            //this.historyDataInfoLoader = new HistoryDataInfoLoader(srcDataPath);
            //this.loader = new UpdateDataInfoLoader(srcDataPath, openDates);
        }

        /// <summary>
        /// 得到还需要更新的Tick数据
        /// 返回一个数据更新信息的队列，每个元素记录了一支股票或期货需要更新的数据
        /// </summary>
        /// <param name="isFillUp">是否将所有缺的数据全部补上，如果isFillUp为false，那么会从现在的历史数据中最新的数据开始更新，否则会将会补全所有数据</param>
        /// <returns></returns>
        public List<UpdateDataInfo> GetTickNewData(bool isFillUp)
        {
            List<UpdateDataInfo> newDataList = new List<UpdateDataInfo>(codes.Count);
            for (int i = 0; i < codes.Count; i++)
            {
                UpdateDataInfo info = new UpdateDataInfo();
                info.code = codes[i].Code;
                if (isFillUp)
                    info.dates = loader.GetWaitForUpdateOpenDates_TickData_FillUp(codes[i].Code);
                else
                    info.dates = loader.GetWaitForUpdateOpenDates_TickData(codes[i].Code);
                newDataList.Add(info);
            }
            return newDataList;
        }

        /// <summary>
        /// 得到还需要更新的K线数据
        /// 返回一个数据更新信息的队列，每个元素记录了一支股票或期货需要更新的数据
        /// </summary>
        /// <param name="period"></param>
        /// <param name="isFillUp"></param>
        /// <returns></returns>
        public List<UpdateDataInfo> GetKLineNewData(KLinePeriod period, bool isFillUp)
        {
            List<UpdateDataInfo> newDataList = new List<UpdateDataInfo>(codes.Count);
            for (int i = 0; i < codes.Count; i++)
            {
                UpdateDataInfo info = new UpdateDataInfo();
                info.code = codes[i].Code;
                if (isFillUp)
                    info.dates = loader.GetWaitForUpdateOpenDates_KLineData_FillUp(codes[i].Code, period);
                else
                    info.dates = loader.GetWaitForUpdateOpenDates_KLineData(codes[i].Code, period);
                newDataList.Add(info);
            }
            return newDataList;
        }
    }

    /// <summary>
    /// 得到需要更新的Tick和KLine数据
    /// </summary>
    public class UpdateDataInfoLoader
    {
        private IHistoryDataOpenDateLoader historyDataInfoLoader;

        private List<int> openDates;

        public UpdateDataInfoLoader(IHistoryDataOpenDateLoader historyDataInfoLoader, List<int> openDates)
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