using com.wer.sc.data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.mockdata
{
    public class AssertUtils
    {
        public static void AssertKLineDataResult(IKLineData klineData, String txt)
        {
            string[] periodArr = txt.Split('\r');
            Assert.AreEqual(periodArr.Length, klineData.Length);
            for (int i = 0; i < klineData.Length; i++)
            {
                klineData.BarPos = i;
                string periodStr = periodArr[i].Trim();
                Assert.AreEqual(periodStr, klineData.ToString());
            }
        }

        public static void AssertTickDataResult(ITickData tickData, string txt)
        {
            string[] periodArr = txt.Split('\r');
            Assert.AreEqual(periodArr.Length, tickData.Length);
            for (int i = 0; i < tickData.Length; i++)
            {
                tickData.BarPos = i;
                string periodStr = periodArr[i].Trim();
                Assert.AreEqual(periodStr, tickData.ToString());
            }
        }

        public static void AssertDates(List<int> dates, string txt)
        {
            string[] periodArr = txt.Split('\r');
            for (int i = 0; i < dates.Count; i++)
            {
                Assert.AreEqual(int.Parse(periodArr[i]), dates[i]);
            }
        }

        public static void AssertList<T>(IList<T> list, string txt)
        {
            string[] periodArr = txt.Split('\r');
            for (int i = 0; i < list.Count; i++)
            {
                Assert.AreEqual(periodArr[i].Trim(), list[i].ToString());
            }
        }

        public static void PrintList<T>(IList<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list);
            }
        }
    }
}
