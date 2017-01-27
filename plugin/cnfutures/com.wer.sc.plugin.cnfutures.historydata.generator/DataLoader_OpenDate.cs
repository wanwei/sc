using com.wer.sc.data.opentime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cnfutures.generator
{
    public class DataLoader_OpenDate
    {
        private IOpenDateReader openDateReader;
        private List<int> openDates;
        private String srcDataPath;

        public DataLoader_OpenDate(string srcDataPath)
        {
            this.srcDataPath = srcDataPath;
        }

        public List<int> GetOpenDates()
        {
            initOpenDates();
            return this.openDates;
        }

        private void initOpenDates()
        {
            if (this.openDates != null)
                return;
            String path = srcDataPath + "\\DL";
            this.openDates = GetOpenDates(path);
            this.openDateReader = new OpenDateCache(openDates);
        }

        public IOpenDateReader GetOpenDateReader()
        {
            initOpenDates();
            return openDateReader;
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
