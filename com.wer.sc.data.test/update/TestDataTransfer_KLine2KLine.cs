using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    [TestClass]
    public class TestDataTransfer_KLine2KLine
    {
        [TestMethod]
        public void TestTransferKLine_Minute()
        {
            IKLineData data_1min = GetKLineData_1Minute();

            //转换成5分钟线
            IKLineData data = DataTransfer_KLine2KLine.Transfer(data_1min, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 5));
            Assert.AreEqual("20131216.09,3341,3343,3336,3338,70038,0,1659332", data.ToString());
            data.BarPos = 10;
            Assert.AreEqual("20131216.095,3357,3364,3356,3363,31524,0,1667394", data.ToString());
            data.BarPos = 20;
            Assert.AreEqual("20131216.1055,3356,3357,3354,3355,10224,0,1679436", data.ToString());
            data.BarPos = 45;
            Assert.AreEqual("20131217.09,3365,3378,3365,3375,122100,0,1650550", data.ToString());
            Assert.AreEqual(540, data.Length);

            //转换成15分钟
            data = DataTransfer_KLine2KLine.Transfer(data_1min, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 15));
            Assert.AreEqual("20131216.09,3341,3351,3335,3346,170132,0,1661910", data.ToString());
            data.BarPos = 10;
            Assert.AreEqual("20131216.1345,3347,3352,3347,3349,27012,0,1670890", data.ToString());
            data.BarPos = 14;
            Assert.AreEqual("20131216.1445,3348,3353,3347,3348,60546,0,1659396", data.ToString());
            data.BarPos = 15;
            Assert.AreEqual("20131217.09,3365,3378,3365,3370,201756,0,1660740", data.ToString());            
            Assert.AreEqual(180, data.Length);

            //转换成1小时
            data = DataTransfer_KLine2KLine.Transfer(data_1min, new KLinePeriod(KLinePeriod.TYPE_HOUR, 1));
            Assert.AreEqual("20131216.09,3341,3364,3335,3360,449244,0,1671458", data.ToString());
            data.BarPos = 1;
            Assert.AreEqual("20131216.1,3360,3362,3353,3355,129188,0,1679724", data.ToString());
            data.BarPos = 2;
            Assert.AreEqual("20131216.1115,3355,3356,3343,3350,201658,0,1675118", data.ToString());
            data.BarPos = 3;
            Assert.AreEqual("20131216.1415,3351,3357,3347,3348,158338,0,1659396", data.ToString());
            data.BarPos = 4;
            Assert.AreEqual("20131217.09,3365,3378,3364,3368,381392,0,1673526", data.ToString());                   
        }

        [TestMethod]
        public void TestTransferKLine_Day()
        {
            IKLineData data_1min = GetKLineData_1Minute();
            IKLineData data = DataTransfer_KLine2KLine.Transfer_Day(data_1min, new KLinePeriod(KLinePeriod.TYPE_DAY, 1),0.18);

            Assert.AreEqual("20131216,3341,3364,3335,3348,938428,0,1659396", data.ToString());
            data.BarPos = 1;
            Assert.AreEqual("20131217,3365,3395,3362,3386,1231522,0,1625218", data.ToString());
            data.BarPos = 2;
            Assert.AreEqual("20131218,3400,3406,3383,3393,989908,0,1617380", data.ToString());
            data.BarPos = 3;
            Assert.AreEqual("20131219,3371,3383,3367,3381,765966,0,1616592", data.ToString());
            data.BarPos = 11;
            Assert.AreEqual("20131231,3352,3373,3350,3368,1049534,0,1556822", data.ToString());           
        }

        public static IKLineData GetKLineData_1Minute()
        {
            MockDataProvider dataProvider = new MockDataProvider();
            dataProvider.Append = true;
            List<int> dates = dataProvider.GetOpenDates();
            List<TickData> dataList = new List<TickData>();
            for (int i = 0; i < dates.Count; i++)
            {
                int date = dates[i];
                TickData tickData = dataProvider.GetTickData("m05", date);
                if (tickData == null)
                    continue;
                dataList.Add(tickData);
            }

            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });
            return DataTransfer_Tick2KLine.Transfer(dataList, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1), openTime);
        }
    }
}
