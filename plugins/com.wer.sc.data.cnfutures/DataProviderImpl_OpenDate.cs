using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cnfutures
{
    public class DataProviderImpl_OpenDate
    {
        private List<int> openDates;

        private DataProviderConfig config;

        public DataProviderImpl_OpenDate(DataProviderConfig config)
        {
            this.config = config;
        }

        public List<int> GetOpenDates()
        {
            if (this.openDates != null)
                return this.openDates;
            String path = config.GetMarketPath("DL");
            this.openDates = GetOpenDates(path);
            return this.openDates;
        }

        public static List<int> GetOpenDates(string path)
        {
            if (!Directory.Exists(path))
                return new List<int>();
            String[] dirs = Directory.GetDirectories(path);
            List<int> openDates = new List<int>();
            foreach (String dir in dirs)
            {
                int openDate;
                int index = dir.LastIndexOf('\\');
                bool isInt = int.TryParse(dir.Substring(index + 1), out openDate);
                if (isInt)
                    openDates.Add(openDate);
            }
            return openDates;
        }
    }
}
