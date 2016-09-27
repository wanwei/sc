using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store
{
    public class TimeLineDataStore_Csv
    {
        private string path;

        public TimeLineDataStore_Csv(String path)
        {
            this.path = path;
        }

        public void Save(ITimeLineData data)
        {
            string[] contents = new string[data.Length];
            for (int i = 0; i < contents.Length; i++)
            {
                data.BarPos = i;
                contents[i] = data.ToString();
            }
            File.WriteAllLines(path, contents);
        }

        public ITimeLineData Load()
        {
            String[] lines = File.ReadAllLines(path);
            return LoadKLineData(lines);
        }

        public static ITimeLineData LoadKLineData(string[] lines)
        {
            //TimeLineData data = new TimeLineData(lines.Length);
            //for (int i = 0; i < lines.Length; i++)
            //{
            //    String line = lines[i].Trim();
            //    String[] dataArr = line.Split(',');
            //    data.arr_time[i] = double.Parse(dataArr[0]);
            //    data.arr_start[i] = float.Parse(dataArr[1]);
            //    data.arr_high[i] = float.Parse(dataArr[2]);
            //    data.arr_low[i] = float.Parse(dataArr[3]);
            //    data.arr_end[i] = float.Parse(dataArr[4]);
            //    data.arr_mount[i] = (int)float.Parse(dataArr[5]);
            //    data.arr_money[i] = float.Parse(dataArr[6]);
            //    data.arr_hold[i] = int.Parse(dataArr[7]);
            //}
            //return data;
            return null;
        }
    }
}
