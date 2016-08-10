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
        private RealDataReader realDataReader;        

        public DataReaderFactory(String dataPath)
        {
            this.dataPath = dataPath;
            this.pathUtils = new DataPathUtils(dataPath);
            this.codeReader = new CodeReader(PathUtils.GetCodePath());
            this.openDateReader = new OpenDateReader(PathUtils.GetOpenDatePath());
            this.tickDataReader = new TickDataReader(dataPath);
            this.klineDataReader = new KLineDataReader(dataPath);
            this.realDataReader = new RealDataReader(dataPath);
        }

        public CodeReader CodeReader
        {
            get { return codeReader; }
        }

        public OpenDateReader OpenDateReader
        {
            get { return openDateReader; }
        }

        public KLineDataReader KLineDataReader
        {
            get { return klineDataReader; }
        }

        public RealDataReader RealDataReader
        {
            get { return realDataReader; }
        }

        public TickDataReader TickDataReader
        {
            get { return tickDataReader; }
        }

        public DataPathUtils PathUtils
        {
            get { return pathUtils; }
        }
    }
}
