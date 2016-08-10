
using com.wer.sc.data.test.Properties;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    public class MockDataProvider : DataProvider
    {
        private bool append = false;

        public MockDataProvider() : this(null)
        {

        }

        public MockDataProvider(PluginHelper helper) : base(helper)
        {
        }

        public bool Append
        {
            get
            {
                return append;
            }

            set
            {
                append = value;
            }
        }

        override
        public String GetName()
        {
            return "mock的数据提供者";
        }

        override
        public String GetDescription()
        {
            return "mock的数据提供者";
        }

        override
        public List<CodeInfo> GetCodes()
        {
            if (Append)
                return ResourceLoader.GetCodes();
            else
            {
                List<CodeInfo> codes = ResourceLoader.GetCodes();
                return codes.GetRange(0, codes.Count - 100);
            }
        }
        override
        public string GetDataPath()
        {
            return System.Environment.CurrentDirectory + "\\test";
        }
        override
        public List<int> GetOpenDates()
        {
            List<int> openDates = ResourceLoader.GetOpenDates();
            if (Append)
                return openDates;
            else
                return openDates.GetRange(0, 10);
        }

        public override List<int> GetOpenDates(String code)
        {
            return GetOpenDates();
        }

        override
        public List<double[]> GetOpenTime(String code, int date)
        {
            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });
            return openTime;
        }
        override
        public TickData GetTickData(String code, int date)
        {
            return ResourceLoader.GetTickData(code, date);
        }
    }
}
