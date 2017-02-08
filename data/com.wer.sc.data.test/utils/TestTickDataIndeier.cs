using com.wer.sc.data.utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.test.utils
{
    [TestClass]
    public class TestTickDataIndeier
    {
        [TestMethod]
        public void TestTickDataIndex()
        {
            //ITickData tickData;
            //IKLineData klineData;

            //TickDataIndeier dataIndeier = new TickDataIndeier();

            DataReaderFactory fac = ResourceLoader.GetDefaultDataReaderFactory();
            string code = "m05";
            //Print(GetDataIndeies(fac, code, 20141229));
            //Print(GetDataIndeies(fac, code, 20141229));
            //Print(GetDataIndeies(fac, code, 20140506));
        }

        //private List<int> GetDataIndeies(DataReaderFactory fac, string code, int date)
        //{
        //    IKLineData minuteKLineData = fac.KLineDataReader.GetData(code, date, date, new KLinePeriod(KLineTimeType.MINUTE, 1));
        //    TickData tickData = fac.TickDataReader.GetTickData(code, date);
        //    TickDataIndeier indeier = new TickDataIndeier(tickData);
        //    return indeier.GetAllTickSplitIndex();
        //}

        private void Print(List<int> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(i + ":" + list[i]);
            }
        }
    }
}
