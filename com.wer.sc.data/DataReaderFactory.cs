using com.wer.sc.data.cache;
using com.wer.sc.data.navigate;
using com.wer.sc.data.reader;
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
        private CodeReader codeReader;
        private OpenDateReader openDateReader;
        private TickDataReader tickDataReader;
        private KLineDataReader klineDataReader;
        private TimeLineDataReader realDataReader;
        private DataNavigate dataNavigate;
        private DataCacheFactory cacheFactory;
        private DataNavigateMgr dataNavigateMgr;

        public DataReaderFactory(String dataPath)
        {
            this.dataPath = dataPath;
            this.pathUtils = new DataPathUtils(dataPath);
            this.codeReader = new CodeReader(PathUtils.GetCodePath());
            this.openDateReader = new OpenDateReader(PathUtils.GetOpenDatePath());
            this.tickDataReader = new TickDataReader(dataPath);
            this.klineDataReader = new KLineDataReader(dataPath);
            this.realDataReader = new TimeLineDataReader(this);
            this.dataNavigate = new DataNavigate(this);
            this.cacheFactory = new DataCacheFactory(this);
            this.dataNavigateMgr = new DataNavigateMgr(this);
        }

        public ICodeReader CodeReader
        {
            get { return codeReader; }
        }

        public IOpenDateReader OpenDateReader
        {
            get { return openDateReader; }
        }

        public IKLineDataReader KLineDataReader
        {
            get { return klineDataReader; }
        }

        public TimeLineDataReader RealDataReader
        {
            get { return realDataReader; }
        }

        public TickDataReader TickDataReader
        {
            get { return tickDataReader; }
        }

        public DataNavigate DataNavigate
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
    }
}
