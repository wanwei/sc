using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.provider
{
    public class DataProvider_TickData
    {
        private String dataPath;

        public DataProvider_TickData(String dataPath)
        {
            this.dataPath = dataPath;
        }

        public List<int> GetOpenDates(String code)
        {
            String codePath = GetCodePath(code);
            if (!Directory.Exists(codePath))
                return new List<int>();
            String[] openDateFiles = Directory.GetFiles(codePath);
            List<int> openDates = new List<int>();
            for (int i = 0; i < openDateFiles.Length; i++)
            {
                String file = openDateFiles[i];
                int startIndex = file.LastIndexOf('_') + 1;
                if (startIndex < 0)
                    continue;
                int openDate;
                bool isInt = int.TryParse(file.Substring(startIndex, 8), out openDate);
                if (isInt)
                    openDates.Add(openDate);
            }
            return openDates;
        }

        public TickData GetTickData(String code, int date)
        {
            String path = GetCodePath(code, date);
            if (!File.Exists(path))
                return null;
            String[] lines = File.ReadAllLines(path);
            return ReadLinesToTickData(lines);
        }

        private String GetCodePath(String code)
        {
            return dataPath + "\\" + code + "\\";
        }

        private String GetCodePath(String code, int date)
        {
            return dataPath + "\\" + code + "\\" + code + "_" + date + ".csv";
        }

        public static TickData ReadLinesToTickData(string[] lines)
        {
            TickData data = new TickData(lines.Length);
            for (int i = 0; i < lines.Length; i++)
            {
                String line = lines[i].Trim();
                if (line.Equals(""))
                    continue;
                String[] dataArr = line.Split(',');
                if (dataArr.Length < 5)
                    continue;
                data.arr_time[i] = double.Parse(dataArr[0]);
                data.arr_price[i] = float.Parse(dataArr[1]);
                data.arr_mount[i] = int.Parse(dataArr[2]);
                data.arr_totalMount[i] = int.Parse(dataArr[3]);
                data.arr_add[i] = int.Parse(dataArr[4]);
                data.arr_buyPrice[i] = (int)float.Parse(dataArr[5]);
                data.arr_buyMount[i] = int.Parse(dataArr[6]);
                data.arr_sellPrice[i] = (int)float.Parse(dataArr[7]);
                data.arr_sellMount[i] = int.Parse(dataArr[8]);
                data.arr_isBuy[i] = dataArr[9].Equals("1");
            }
            return data;
        }
    }
}