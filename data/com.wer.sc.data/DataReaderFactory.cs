using com.wer.sc.data.cache;
using com.wer.sc.data.navigate;
using com.wer.sc.data.reader;
using com.wer.sc.data.reader.realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    public class DataReaderFactory
    {
        private String dataPath;

        private DataPathUtils pathUtils;
        private CommonDataReader_Code codeReader;
        private CommonDataReader_OpenDate openDateReader;
        private CommonDataReaderMgr_OpenDate openDateReaderMgr;
        private CommonDataReader_OpenTime openTimeReader;
        private HistoryDataReader_Tick tickDataReader;
        private HistoryDataReader_KLine klineDataReader;
        private HistoryDataReader_TimeLine realDataReader;
        private DataNavigate3 dataNavigate;
        private DataCacheFactory cacheFactory;
        private DataNavigateMgr dataNavigateMgr;
        private IRealTimeDataNavigaterFactory realTimeDataNavigaterFactory;
        
        public DataReaderFactory(String dataPath)
        {
            this.dataPath = dataPath;
            this.pathUtils = new DataPathUtils(dataPath);
            this.codeReader = new CommonDataReader_Code(PathUtils.GetCodePath());
            this.openDateReader = new CommonDataReader_OpenDate(PathUtils.GetOpenDatePath());            
            this.openTimeReader = new CommonDataReader_OpenTime(dataPath);
            this.tickDataReader = new HistoryDataReader_Tick(dataPath);
            this.klineDataReader = new HistoryDataReader_KLine(dataPath);
            this.realDataReader = new HistoryDataReader_TimeLine(this);
            this.dataNavigate = new DataNavigate3(this);
            this.cacheFactory = new DataCacheFactory(this);
            this.dataNavigateMgr = new DataNavigateMgr(this);
            this.realTimeDataNavigaterFactory = new RealTimeDataNavigateFactory(this);
            this.openDateReaderMgr = new CommonDataReaderMgr_OpenDate(this);
        }

        public ICommonDataReader_Code CodeReader
        {
            get { return codeReader; }
        }

        public ICommonDataReader_OpenDate OpenDateReader
        {
            get { return openDateReader; }
        }

        public ICommonDataReader_OpenDate GetOpenDateReader(string code)
        {
            return openDateReaderMgr.GetOpenDateReader(code);
        }

        public ICommonDataReader_OpenTime OpenTimeReader
        {
            get
            {
                return openTimeReader;
            }
        }

        public IHistoryDataReader_KLine KLineDataReader
        {
            get { return klineDataReader; }
        }

        public HistoryDataReader_TimeLine TimeLineDataReader
        {
            get { return realDataReader; }
        }

        public HistoryDataReader_Tick TickDataReader
        {
            get { return tickDataReader; }
        }

        public DataNavigate3 DataNavigate
        {
            get { return dataNavigate; }
        }

        public DataPathUtils PathUtils
        {
            get { return pathUtils; }
        }

        public DataCacheFactory CacheFactory
        {
            get
            {
                return cacheFactory;
            }
        }

        public DataNavigateMgr DataNavigateMgr
        {
            get
            {
                return dataNavigateMgr;
            }
        }

        public IRealTimeDataNavigaterFactory RealTimeDataNavigaterFactory
        {
            get
            {
                return realTimeDataNavigaterFactory;
            }
        }
    }
}
