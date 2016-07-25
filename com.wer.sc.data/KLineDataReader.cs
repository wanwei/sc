using com.wer.sc.data.store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    public class KLineDataReader
    {
        private string dataPath;
        private DataPathUtils utils;

        public KLineDataReader(string dataPath)
        {
            this.dataPath = dataPath;
            this.utils = new DataPathUtils(this.dataPath);
        }

        public KLineData GetData(String code, int startDate, int endDate, KLinePeriod period)
        {
            String path = utils.GetKLineDataPath(code, period);
            KLineDataStore store = new KLineDataStore(path);
            return store.Load(startDate, endDate);
        }

        public int GetLastDate(String code, KLinePeriod period)
        {
            String path = utils.GetKLineDataPath(code, period);
            if (!File.Exists(path))
                return -1;
            KLineDataStore store = new KLineDataStore(path);
            double time = store.GetLastTime();
            return (int)time;
        }
    }
}
