using com.wer.sc.data.store;
using com.wer.sc.data.test.Properties;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    public abstract class MockDataProvider_Abstract : Plugin_DataProvider
    {
        private string dataPathDir = "";

        public string DataPathDir
        {
            get
            {
                return dataPathDir;
            }

            set
            {
                dataPathDir = value;
            }
        }

        public MockDataProvider_Abstract() : base(null)
        {

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
            return CodeStore.GetCodes(GetCodeResource().Split('\r'));
        }

        override
        public string GetDataPath()
        {
            return @"d:\sctest\mockdata\" + dataPathDir;
        }

        override
        public List<int> GetOpenDates()
        {
            string[] openDateStrs = GetOpenDateResource().Split('\r');
            List<int> openDates = new List<int>(openDateStrs.Length);
            for (int i = 0; i < openDateStrs.Length; i++)
                openDates.Add(int.Parse(openDateStrs[i]));
            return openDates;
        }

        public override List<int> GetOpenDates(String code)
        {
            return GetOpenDates();
        }

        override
        public List<double[]> GetOpenTime(String code, int date)
        {
            return GetOpenTime_M(date);
        }

        public static List<double[]> GetOpenTime_M(int date)
        {
            if (date <= 20141226)
                return GetOpenTime_Normal();
            if (date == 20150105)
                return GetOpenTime_Normal();
            return GetOpenTime_NightEarly();
        }

        private static List<double[]> GetOpenTime_Normal()
        {
            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });
            return openTime;
        }

        private static List<double[]> GetOpenTime_NightEarly()
        {
            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .210000, .02300 });
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });
            return openTime;
        }

        override
        public TickData GetTickData(String code, int date)
        {
            return ResourceLoader.GetDefaultDataReaderFactory().TickDataReader.GetTickData(code, date);
        }

        public abstract string GetCodeResource();

        public abstract string GetOpenDateResource();
    }
}
