using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.test.update
{
    /// <summary>
    /// 只实现了豆粕
    /// </summary>
    public class DataProvider_MockCnFuturesM : Plugin_DataProvider
    {
        public DataProvider_MockCnFuturesM(PluginHelper helper) : base(helper)
        {
        }

        public override List<CodeInfo> GetCodes()
        {
            throw new NotImplementedException();
        }

        public override string GetDataPath()
        {
            return ResourceLoader.DataPath;
        }

        public override string GetDescription()
        {
            throw new NotImplementedException();
        }

        public override string GetName()
        {
            throw new NotImplementedException();
        }

        public override List<int> GetOpenDates()
        {
            throw new NotImplementedException();
        }

        public override List<int> GetOpenDates(string code)
        {
            throw new NotImplementedException();
        }

        public override List<double[]> GetOpenTime(string code, int date)
        {
            if (date < 20141229 || date == 20150105)
                return GetOpenTime_Normal();
            if (date < 20150511)
                return GetOpenTime_NightEarly();
            return GetOpenTime_Night();
        }

        private List<double[]> openTime_Normal = GetOpenTime_Normal();

        private static List<double[]> GetOpenTime_Normal()
        {
            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });
            return openTime;
        }

        private List<double[]> openTime_NightEarly = GetOpenTime_NightEarly();

        private static List<double[]> GetOpenTime_NightEarly()
        {
            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .210000, .02300 });
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });
            return openTime;
        }

        private List<double[]> openTime_Night = GetOpenTime_Night();

        private static List<double[]> GetOpenTime_Night()
        {
            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .210000, .233000 });
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });
            return openTime;
        }

        public override TickData GetTickData(string code, int date)
        {
            throw new NotImplementedException();
        }
    }
}