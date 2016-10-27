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
            this.loader = new UpdateDataInfoLoader(srcDataPath, openDates);
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
        private HistoryDataInfoLoader historyDataInfoLoader;

        private List<int> openDates;

        public UpdateDataInfoLoader(string srcDataPath, List<int> openDates)
        {
            this.historyDataInfoLoader = new HistoryDataInfoLoader(srcDataPath);
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

    /// <summary>
    /// 更新数据信息类
    /// 该类可以表示已经更新的数据，也可以表示未更新的数据
    /// </summary>
    public class UpdateDataInfo
    {
        public String code;

        public List<int> dates;
    }

    /// <summary>
    /// 历史数据信息装载
    /// 该类用来得到当前历史数据更新状况
    /// </summary>
    public class HistoryDataInfoLoader
    {
        private String srcDataPath;

        public HistoryDataInfoLoader(String srcDataPath)
        {
            this.srcDataPath = srcDataPath;
        }

        public List<int> GetOpenDates_TickData(string code)
        {
            return GetOpenDates(GetTickDataPath(code));
        }

        public int GetLastOpenDate_TickData(string code)
        {
            return GetLastOpenDate(GetTickDataPath(code));
        }

        private string GetTickDataPath(string code)
        {
            return srcDataPath + "\\" + code + "\\tick\\";
        }

        public List<int> GetOpenDates_KLineData(string code, KLinePeriod period)
        {
            return GetOpenDates(GetKLineDataPath(code, period));
        }

        public int GetLastOpenDate_KLineData(string code, KLinePeriod period)
        {
            return GetLastOpenDate(GetKLineDataPath(code, period));
        }

        private string GetKLineDataPath(string code, KLinePeriod period)
        {
            return srcDataPath + "\\" + code + "\\kline\\" + period.ToEngString() + "\\";
        }

        private static int GetLastOpenDate(string path)
        {
            if (!Directory.Exists(path))
                return -1;
            String[] files = Directory.GetFiles(path);
            int lastOpenDate = -1;
            foreach (String file in files)
            {
                int openDate;
                int index = file.LastIndexOf('_');
                bool isInt = int.TryParse(file.Substring(index + 1, 8), out openDate);
                if (isInt && openDate > lastOpenDate)
                {
                    lastOpenDate = openDate;
                }
            }
            return lastOpenDate;
        }

        /// <summary>
        /// 得到该目录下数据的
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static List<int> GetOpenDates(string path)
        {
            if (!Directory.Exists(path))
                return new List<int>();
            String[] files = Directory.GetFiles(path);
            List<int> openDates = new List<int>();
            foreach (String file in files)
            {
                int openDate;
                int index = file.LastIndexOf('_');
                bool isInt = int.TryParse(file.Substring(index + 1, 8), out openDate);
                if (isInt)
                    openDates.Add(openDate);
            }
            return openDates;
        }
    }
}
