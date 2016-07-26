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

        public TickData GetTickData(String code, int date)
        {
            String path = GetCodePath(code, date);
            if (!File.Exists(path))
                return null;
            String[] lines = File.ReadAllLines(path);
            return ReadLinesToTickData(lines);
        }

        private String GetCodePath(String code, int date)
        {
            return dataPath + "\\" + code + "\\" + code + "_" + date + ".csv";
        }

        private TickData ReadLinesToTickData(string[] lines)
        {
            TickData data = new TickData(lines.Length);
            for (int i = 0; i < lines.Length; i++)
            {
                String line = lines[i];
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