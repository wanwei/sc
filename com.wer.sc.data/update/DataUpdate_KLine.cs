using com.wer.sc.data.store;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    public class DataUpdate_KLine
    {
        private DataPathUtils utils;
        private DataProvider dataProvider;

        public DataUpdate_KLine(DataProvider dataProvider)
        {
            this.utils = new DataPathUtils(dataProvider.GetDataPath());
            this.dataProvider = dataProvider;
        }

        public void Update()
        {
            DataReaderFactory tmpFac = new DataReaderFactory(dataProvider.GetDataPath());
            List<CodeInfo> codes = tmpFac.CodeReader.GetAllCodes();
            for (int i = 0; i < codes.Count; i++)
            {
                CodeInfo code = codes[i];
                UpdateCode(code.code, tmpFac);
            }
        }

        public int GetUpdateFirstTime(String code, DataReaderFactory dataReaderFactory, KLinePeriod period)
        {
            String path = utils.GetKLineDataPath(code, period);
            if (!File.Exists(path))
                return -1;
            KLineDataStore store = new KLineDataStore(path);
            double time = store.GetFirstTime();
            return (int)time;
        }

        public int GetUpdateLastDate(String code, DataReaderFactory dataReaderFactory, KLinePeriod period)
        {
            return dataReaderFactory.KLineDataReader.GetLastDate(code, period);
        }

        public List<int> GetUpdateDates(String code, DataReaderFactory dataReaderFactory, KLinePeriod period)
        {
            List<int> openDates = dataProvider.GetOpenDates(code);
            if (openDates == null)
                return new List<int>();
            int lastDate = GetUpdateLastDate(code, dataReaderFactory, period);
            if (lastDate < 0)
                return openDates;
            int index = openDates.IndexOf(lastDate) + 1;
            return openDates.GetRange(index, openDates.Count - index);
        }

        public void UpdateCode(String code, DataReaderFactory dataReaderFactory)
        {
            //更新1分钟、15分钟、日线
            KLineData data = Update(code, dataReaderFactory, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1));
            if (data != null)
                UpdateOther(code, dataReaderFactory, data);
        }

        public KLineData Update(String code, DataReaderFactory dataReaderFactory, KLinePeriod period)
        {
            if (period.PeriodType == KLinePeriod.TYPE_MINUTE && period.Period == 1)
                return UpdateByTick(code, dataReaderFactory, period);
            return null;
        }

        private KLineData UpdateByTick(string code, DataReaderFactory dataReaderFactory, KLinePeriod period)
        {
            String path = utils.GetKLineDataPath(code, period);
            KLineDataStore store = new KLineDataStore(path);
            int lastDate = (int)store.GetLastTime();
            List<int> openDates = dataProvider.GetOpenDates();
            int lastIndex;
            if (lastDate < 0)
                lastIndex = -1;
            else
                lastIndex = openDates.IndexOf(lastDate);

            float lastPrice = -1;
            List<KLineData> klineDataList = new List<KLineData>();
            for (int i = lastIndex + 1; i < openDates.Count; i++)
            {
                int openDate = openDates[i];
                TickData tickdata = dataReaderFactory.TickDataReader.GetTickData(code, openDate);
                if (tickdata != null)
                {
                    List<double[]> openTimes = dataProvider.GetOpenTime(code, openDate);
                    KLineData klineData = DataTransfer_Tick2KLine.Transfer(tickdata, period, openTimes, lastPrice);
                    klineDataList.Add(klineData);
                    lastPrice = klineData.arr_end[klineData.Length - 1];
                }
            }
            if (klineDataList.Count == 0)
                return null;
            KLineData data = KLineData.Merge(klineDataList);
            store.Append(data);
            return data;
        }

        public KLineData UpdateByTick(string code, DataReaderFactory dataReaderFactory, KLinePeriod period, List<int> dates)
        {
            String path = utils.GetKLineDataPath(code, period);
            KLineDataStore store = new KLineDataStore(path);

            float lastPrice = -1;
            List<KLineData> klineDataList = new List<KLineData>();
            for (int i = 0; i < dates.Count; i++)
            {
                int openDate = dates[i];
                TickData tickdata = dataReaderFactory.TickDataReader.GetTickData(code, openDate);
                if (tickdata != null)
                {
                    List<double[]> openTimes = dataProvider.GetOpenTime(code, openDate);
                    KLineData klineData = DataTransfer_Tick2KLine.Transfer(tickdata, period, openTimes, lastPrice);
                    klineDataList.Add(klineData);
                    lastPrice = klineData.arr_end[klineData.Length - 1];
                }
            }
            if (klineDataList.Count == 0)
                return null;
            KLineData data = KLineData.Merge(klineDataList);
            store.Append(data);
            return data;
        }

        public KLineData UpdateByKLine(String code, DataReaderFactory dataReaderFactory, KLinePeriod period, KLineData originalData)
        {
            KLineData data_Target = DataTransfer_KLine2KLine.Transfer(originalData, period);
            String path = utils.GetKLineDataPath(code, period);
            KLineDataStore store = new KLineDataStore(path);
            store.Append(data_Target);
            return data_Target;
        }

        private void UpdateBy1Minute(String code, DataReaderFactory dataReaderFactory, KLinePeriod period)
        {
            int lastDate = dataReaderFactory.KLineDataReader.GetLastDate(code, period);
            KLineData data = dataReaderFactory.KLineDataReader.GetData(code, lastDate + 1, int.MaxValue, period);
            KLineData data_Target = DataTransfer_KLine2KLine.Transfer(data, period);
            String path = utils.GetKLineDataPath(code, period);
            KLineDataStore store = new KLineDataStore(path);
            store.Append(data_Target);
        }

        private void UpdateOther(String code, DataReaderFactory tmpFac, KLineData data)
        {
            DoUpdate(code, tmpFac, data, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 15));
            DoUpdate(code, tmpFac, data, new KLinePeriod(KLinePeriod.TYPE_HOUR, 1));
            DoUpdate(code, tmpFac, data, new KLinePeriod(KLinePeriod.TYPE_DAY, 1));
        }

        private void DoUpdate(String code, DataReaderFactory tmpFac, KLineData data, KLinePeriod period)
        {
            //TODO 检查已有文件的时间
            KLineData data_Target = DataTransfer_KLine2KLine.Transfer(data, period);
            String path = utils.GetKLineDataPath(code, period);
            KLineDataStore store = new KLineDataStore(path);
            store.Append(data_Target);
        }
    }
}