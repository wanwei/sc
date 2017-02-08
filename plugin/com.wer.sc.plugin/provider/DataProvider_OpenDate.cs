using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.provider
{
    public class DataProvider_OpenDate
    {
        private List<int> openDates;

        private String configPath;

        public DataProvider_OpenDate(String configPath)
        {
            this.configPath = configPath;
        }

        public List<int> GetOpenDates()
        {
            if (this.openDates != null)
                return this.openDates;

            String[] lines = File.ReadAllLines(configPath + "opendates.csv");
            this.openDates = new List<int>();
            for (int i = 0; i < lines.Length; i++)
            {
                int opendate = int.Parse(lines[i]);
                openDates.Add(opendate);
            }
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
