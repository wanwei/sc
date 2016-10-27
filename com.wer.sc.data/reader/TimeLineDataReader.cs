using com.wer.sc.data.store;
using com.wer.sc.data.update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader
{
    public class TimeLineDataReader : ITimeLineDataReader
    {
        private DataReaderFactory dataReaderFactory;
        private string dataPath;
        private DataPathUtils utils;

        public TimeLineDataReader(DataReaderFactory dataReaderFactory)
        {
            this.dataReaderFactory = dataReaderFactory;
            this.utils = dataReaderFactory.PathUtils;
        }

        public ITimeLineData GetData(String code, int date)
        {
            String path = utils.GetKLineDataPath(code, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1));
            KLineDataStore store = new KLineDataStore(path);

            KLineDataIndexResult result = store.LoadIndex();
            if (!result.IndexDic.Keys.Contains(date))
                return null;
            List<ITimeLineData> dataList = GetData(code, date, date, store, result);
            if (dataList.Count == 0)
                return null;
            return dataList[0];
        }

        public List<ITimeLineData> GetData(String code, int startDate, int endDate)
        {
            String path = utils.GetKLineDataPath(code, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1));
            KLineDataStore store = new KLineDataStore(path);
            KLineDataIndexResult result = store.LoadIndex();
            return GetData(code, startDate, endDate, store, result);
        }

        private List<ITimeLineData> GetData(String code, int startDate, int endDate, KLineDataStore store, KLineDataIndexResult result)
        {
            int startIndex = store.GetStartIndex(startDate, result);
            IKLineData data = store.Load(startDate, endDate, result);

            int lastEndIndex = startIndex - 1;
            IKLineData lastEndData = store.LoadByIndex(lastEndIndex, lastEndIndex);
            ((KLineData)data).Code = code;
            float lastEndPrice = lastEndData.Arr_End[0];
            return DataTransfer_KLine2TimeLine.ConvertTimeLineDataList(data, lastEndPrice, dataReaderFactory.OpenDateReader);
        }
    }

}
