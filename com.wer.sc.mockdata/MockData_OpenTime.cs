using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.mockdata
{
    public class MockData_OpenTime
    {
        public static List<double[]> GetOpenTime(String code, int date)
        {
            if (date <= 20141226)
                return GetOpenTime_Normal();
            if (date == 20150105)
                return GetOpenTime_Normal();
            if (date <= 20150508)
                return GetOpenTime_NightEarly();
            return GetOpenTime_Night();
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

        private static List<double[]> GetOpenTime_Night()
        {
            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .210000, .233000 });
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });
            return openTime;
        }
    }
}
