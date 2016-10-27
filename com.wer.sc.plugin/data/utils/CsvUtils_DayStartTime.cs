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
        public static void Save(string path, List<DayStartTime> data)
        {
            string[] contents = new string[data.Count];
            for (int i = 0; i < contents.Length; i++)
            {
                contents[i] = data[i].ToString();
            }
            FileUtils.EnsureParentDirExist(path);
            File.WriteAllLines(path, contents);
        }

        public static List<DayStartTime> Load(string path)
        {
            if (!File.Exists(path))
                return null;
            String[] lines = File.ReadAllLines(path);
            return LoadByLines(lines);
        }

        public static List<DayStartTime> LoadByContent(string content)
        {
            string[] lines = content.Split('\r');
            return LoadByLines(lines);
        }

        public static List<DayStartTime> LoadByLines(string[] lines)
        {
            List<DayStartTime> data = new List<DayStartTime>(lines.Length);
            for (int i = 0; i < lines.Length; i++)
            {
                String line = lines[i].Trim();
                if (line.Equals(""))
                    continue;
                String[] dataArr = line.Split(',');
                DayStartTime startTime = new DayStartTime();
                startTime.Date = int.Parse(dataArr[0]);
                startTime.Start = double.Parse(dataArr[1]);
                data.Add(startTime);
            }
            return data;
        }
    }
}
