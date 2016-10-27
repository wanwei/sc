using com.wer.sc.data.store;
using com.wer.sc.data.update;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader
{
    public class KLineDataReader : IKLineDataReader
    {
        private string dataPath;
        private DataPathUtils utils;

        public KLineDataReader(string dataPath)
        {
            this.dataPath = dataPath;
            this.utils = new DataPathUtils(this.dataPath);
        }

        public IKLineData GetAllData(string code, KLinePeriod period)
        {
            String path = utils.GetKLineDataPath(code, period);
            KLineDataStore store = new KLineDataStore(path);
            return store.LoadAll();
        }

        public IKLineData GetData(String code, int startDate, int endDate, KLinePeriod period)
        {
            if (period.PeriodType == KLinePeriod.TYPE_MINUTE)
            {
                if (period.Period == 1 || period.Period == 15)
                    return LoadKLineData(code, startDate, endDate, period);
                IKLineData data = LoadKLineData(code, startDate, endDate, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1));
                return DataTransfer_KLine2KLine.Transfer(data, period);
            }
            if (period.PeriodType == KLinePeriod.TYPE_HOUR)
            {
                if (period.Period == 1)
                    return LoadKLineData(code, startDate, endDate, period);
                IKLineData data = LoadKLineData(code, startDate, endDate, new KLinePeriod(KLinePeriod.TYPE_HOUR, 1));
                return DataTransfer_KLine2KLine.Transfer(data, period);
            }
            if (period.PeriodType == KLinePeriod.TYPE_DAY)
            {
                if (period.Period == 1)
                    return LoadKLineData(code, startDate, endDate, period);
                IKLineData data = LoadKLineData(code, startDate, endDate, new KLinePeriod(KLinePeriod.TYPE_DAY, 1));
                return DataTransfer_KLine2KLine.Transfer(data, period);
            }
            //return LoadKLineData(code, startDate, endDate, period);
            throw new ArgumentException("暂未实现");
        }

        private IKLineData LoadKLineData(string code, int startDate, int endDate, KLinePeriod period)
        {
            String path = utils.GetKLineDataPath(code, period);
            KLineDataStore store = new KLineDataStore(path);
            KLineData data = (KLineData)store.Load(startDate, endDate);
            if (data == null)
                return data;
            data.Code = code;
            return data;
        }

        public int GetFirstDate(String code, KLinePeriod period)
        {
            String path = utils.GetKLineDataPath(code, period);
            if (!File.Exists(path))
                return -1;
            KLineDataStore store = new KLineDataStore(path);
            double time = store.GetFirstTime();
            return (int)time;
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
