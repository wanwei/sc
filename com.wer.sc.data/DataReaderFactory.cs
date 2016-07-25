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
        private TickDataReader tickDataReader;
        private KLineDataReader klineDataReader;

        public DataReaderFactory(String dataPath)
        {
            this.dataPath = dataPath;            
            this.pathUtils = new DataPathUtils(dataPath);
            this.codeReader = new CodeReader(PathUtils.GetCodePath());
            this.tickDataReader = new TickDataReader(dataPath);
            this.klineDataReader = new KLineDataReader(dataPath);
        }

        public CodeReader CodeReader
        {
            get { return codeReader; }
        }

        public KLineDataReader KLineDataReader
        {
            get { return klineDataReader; }
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
