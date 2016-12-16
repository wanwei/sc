using com.wer.sc.data.store;
using com.wer.sc.plugin;
using com.wer.sc.plugin.historydata;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.generator
{
    /// <summary>
    /// 
    /// </summary>
    public class OpenDateReader_DataCenter : IOpenDateReader_HistoryData
    {
        private IPlugin_HistoryData plugin_HistoryData;

        private DataPathUtils dataPathUtils;

        public OpenDateReader_DataCenter(IPlugin_HistoryData plugin_HistoryData)
        {
            this.plugin_HistoryData = plugin_HistoryData;
            this.dataPathUtils = new DataPathUtils(plugin_HistoryData.GetDataPath());
        }

        public int GetLastOpenDate_KLineData(string code, KLinePeriod period)
        {
            string path = dataPathUtils.GetKLineDataPath(code, period);
            KLineDataStore store = new KLineDataStore(path);
            return (int)store.GetLastTime();
        }

        public int GetLastOpenDate_TickData(string code)
        {
            string path = dataPathUtils.GetTickPath(code);
            return GetLastOpenDate(path);
        }

        public List<int> GetOpenDates_KLineData(string code, KLinePeriod period)
        {
            string path = dataPathUtils.GetKLineDataPath(code, period);
            KLineDataStore store = new KLineDataStore(path);
            return store.GetAllOpenDate();
        }

        public List<int> GetOpenDates_TickData(string code)
        {
            string path = dataPathUtils.GetTickPath(code);
            return GetOpenDates(path);
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
