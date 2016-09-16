using com.wer.sc.data.store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader
{
    public class TickDataReader : ITickDataReader
    {
        private String dataPath;
        private DataPathUtils utils;
        public TickDataReader(String dataPath)
        {
            this.dataPath = dataPath;
            this.utils = new DataPathUtils(dataPath);
        }

        public List<int> GetTickDates(String code)
        {
            string path = utils.GetTickPath(code);
            if (!Directory.Exists(path))
                return new List<int>();
            String[] files = Directory.GetFiles(path);                        
            List<int> ticks = new List<int>();
            foreach (String file in files)
            {
                int startIndex = file.IndexOf('_') + 1;
                ticks.Add(int.Parse(file.Substring(startIndex, 8)));
            }
            ticks.Sort();
            return ticks;
        }


        public TickData GetTickData(String code, int date)
        {
            string realPath = utils.GetTickPath(code, date);
            TickDataStore store = new TickDataStore(realPath);
            TickData tickData = store.load();
            tickData.Code = code.ToUpper();
            return tickData;
        }
    }
}