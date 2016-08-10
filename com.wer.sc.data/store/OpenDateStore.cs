using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store
{
    public class OpenDateStore
    {
        private String path;
        public OpenDateStore(String path)
        {
            this.path = path;
        }

        public void Save(List<int> openDates)
        {
            String[] openDateArr = new string[openDates.Count];
            for (int i = 0; i < openDates.Count; i++)
            {
                int arr = openDates[i];
                openDateArr[i] = arr.ToString();
            }

            DirectoryInfo dir = Directory.GetParent(path);
            if (!dir.Exists)
                dir.Create();

            StreamWriter writer = File.CreateText(path);
            try
            {
                for (int i = 0; i < openDates.Count; i++)
                {
                    writer.WriteLine(openDateArr[i]);
                }
            }
            finally
            {
                writer.Close();
            }
        }

        public List<int> Load()
        {
            if (!File.Exists(path))
                return new List<int>();
            String[] strs = File.ReadAllLines(path);
            List<int> openDates = new List<int>();
            for (int i = 0; i < strs.Length; i++)
            {
                String line = strs[i];
                openDates.Add(int.Parse(line));
            }
            return openDates;
        }
    }
}
