using com.wer.sc.data.cnfutures.generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cnfutures.generator
{
    public class MockDataLoader
    {
        public static string originalDataPath = @"E:\FUTURES\CSV\TICK\";

        public static string pluginDataPath = @"E:\FUTURES\CSV\TICKADJUSTED";

        private static DataLoader dataLoader = new DataLoader(originalDataPath, pluginDataPath);

        public static TickData LoadTickData(string code, int date)
        {
            return dataLoader.DataLoader_TickData.GetOrignalTickData(code, date);
        }

        public static List<double[]> LoadOpenTime(string code, int date)
        {
            return dataLoader.DataLoader_OpenTime.GetOpenTime(code, date);
        }

        public static DataLoader DataLoader
        {
            get { return dataLoader; }
        }
    }
}
