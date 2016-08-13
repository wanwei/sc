using com.wer.sc.data.store;
using com.wer.sc.data.update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    public class RealDataReader
    {
        private string dataPath;
        private DataPathUtils utils;

        public RealDataReader(string dataPath)
        {
            this.dataPath = dataPath;
            this.utils = new DataPathUtils(this.dataPath);
        }

        public RealData GetData(String code, int date)
        {
            String path = utils.GetKLineDataPath(code, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1));
            KLineDataStore store = new KLineDataStore(path);

            KLineDataIndexResult result = store.LoadIndex();
            if (!result.IndexDic.Keys.Contains(date))
                return null;

            return GetData(code, date, date, store, result)[0];
        }

        public List<RealData> GetData(String code, int startDate, int endDate)
        {
            String path = utils.GetKLineDataPath(code, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1));
            KLineDataStore store = new KLineDataStore(path);
            KLineDataIndexResult result = store.LoadIndex();
            return GetData(code, startDate, endDate, store, result);
        }

        public List<RealData> GetData(String code, int startDate, int endDate, KLineDataStore store, KLineDataIndexResult result)
        {
            int startIndex = store.GetStartIndex(startDate, result);
            KLineData data = store.Load(startDate, endDate, result);

            int lastEndIndex = startIndex - 1;
            KLineData lastEndData = store.LoadByIndex(lastEndIndex, lastEndIndex);
            float lastEndPrice = lastEndData.arr_end[0];
            return DataTransfer_KLine2Real.Convert(data, lastEndPrice);
        }
    }

}
