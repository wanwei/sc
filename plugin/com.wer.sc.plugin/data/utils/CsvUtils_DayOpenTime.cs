using com.wer.sc.data;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.utils
{
    public class CsvUtils_DayStartTime
    {
        public static void Save(string path, List<DayOpenTime> data)
        {
            string[] contents = new string[data.Count];
            for (int i = 0; i < contents.Length; i++)
            {
                contents[i] = data[i].ToString();
            }
            FileUtils.EnsureParentDirExist(path);
            File.WriteAllLines(path, contents);
        }

        public static List<DayOpenTime> Load(string path)
        {
            if (!File.Exists(path))
                return null;
            String[] lines = File.ReadAllLines(path);
            return LoadByLines(lines);
        }

        public static List<DayOpenTime> LoadByContent(string content)
        {
            string[] lines = content.Split('\r');
            return LoadByLines(lines);
        }

        public static List<DayOpenTime> LoadByLines(string[] lines)
        {
            List<DayOpenTime> data = new List<DayOpenTime>(lines.Length);
            for (int i = 0; i < lines.Length; i++)
            {
                String line = lines[i].Trim();
                if (line.Equals(""))
                    continue;
                String[] dataArr = line.Split(',');
                DayOpenTime startTime = new DayOpenTime();
                startTime.Date = int.Parse(dataArr[0]);
                startTime.Start = double.Parse(dataArr[1]);
                startTime.End = double.Parse(dataArr[2]);
                data.Add(startTime);
            }
            return data;
        }
    }
}
