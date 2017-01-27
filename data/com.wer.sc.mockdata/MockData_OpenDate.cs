using com.wer.sc.data.opentime;
using com.wer.sc.data.utils;
using com.wer.sc.mockdata.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.mockdata
{
    public class MockData_OpenDate
    {
        private static OpenDateCache openDateCache = GetOpenDateCache();

        private static OpenDateCache GetOpenDateCache()
        {
            String[] lines = Resources.MockData_OpenDate.Split('\r');
            List<int> allOpenDates = CsvUtils_OpenDate.LoadByLines(lines);
            OpenDateCache cache = new OpenDateCache(allOpenDates);
            return cache;
        }

        public static List<int> GetAllOpenDates()
        {
            return openDateCache.GetAllOpenDates();
        }

        public static IList<int> GetOpenDates(int start, int end)
        {
            return openDateCache.GetOpenDates(start, end);
        }

        public static IOpenDateReader GetOpenDateReader()
        {
            return openDateCache;
        }
    }
}
