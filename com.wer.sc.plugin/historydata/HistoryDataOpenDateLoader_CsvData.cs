using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.historydata
{
    /// <summary>
    /// 历史数据信息装载
    /// 该类用来得到当前历史数据更新状况
    /// </summary>
    public class HistoryDataOpenDateLoader_CsvData : IHistoryDataOpenDateLoader
    {
        private String srcDataPath;

        public HistoryDataOpenDateLoader_CsvData(String srcDataPath)
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