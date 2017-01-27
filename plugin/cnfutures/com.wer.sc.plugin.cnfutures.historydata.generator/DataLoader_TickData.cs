using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cnfutures.generator
{
    public class DataLoader_TickData
    {
        private string csvDataPath;

        private DataLoader_CodeInfo dataLoader_CodeInfo;

        public DataLoader_TickData(string csvDataPath, DataLoader_CodeInfo dataLoader_CodeInfo)
        {
            this.csvDataPath = csvDataPath;            
            this.dataLoader_CodeInfo = dataLoader_CodeInfo;
        }

        public TickData GetOrignalTickData(string code, int date)
        {
            String path = GetCodePath(code, date);
            if (!File.Exists(path))
                return null;
            String[] lines = File.ReadAllLines(path);
            return ReadLinesToTickData(lines);
        }

        public static TickData ReadLinesToTickData(string[] lines)
        {
            int cnt = GetEmptyLines(lines);
            TickData data = new TickData(lines.Length - 1 - cnt);
            for (int i = 0; i < lines.Length - 1 - cnt; i++)
            {
                String line = lines[i + 1];
                if (line.Equals(""))
                    continue;
                String[] dataArr = line.Split(',');
                if (dataArr.Length < 5)
                    continue;

                String[] dateArr = dataArr[0].Split('-');
                double date = double.Parse(dateArr[0] + Fill(dateArr[1]) + Fill(dateArr[2]));
                String[] timeArr = dataArr[1].Split(':');
                double time = double.Parse(timeArr[0] + timeArr[1] + timeArr[2]);
                double fulltime = date + time / 1000000;

                data.arr_time[i] = fulltime;
                data.arr_price[i] = float.Parse(dataArr[2]);
                data.arr_mount[i] = int.Parse(dataArr[3]);
                data.arr_totalMount[i] = int.Parse(dataArr[4]);
                data.arr_add[i] = int.Parse(dataArr[5]);
                data.arr_buyPrice[i] = (int)float.Parse(dataArr[6]);
                data.arr_buyMount[i] = int.Parse(dataArr[7]);
                data.arr_sellPrice[i] = (int)float.Parse(dataArr[12]);
                data.arr_sellMount[i] = int.Parse(dataArr[13]);
                data.arr_isBuy[i] = dataArr[18].Equals("B");
            }
            return data;
        }
        private static int GetEmptyLines(string[] lines)
        {
            int cnt = 0;
            for (int i = lines.Length - 1; i >= 0; i--)
            {
                if (lines[i].Trim().Equals(""))
                    cnt++;
                else
                    break;
            }
            return cnt;
        }

        private static String Fill(String s)
        {
            if (s.Length == 1)
                return "0" + s;
            return s;
        }

        private String GetCodePath(String code, int date)
        {
            return csvDataPath + "\\" + dataLoader_CodeInfo.GetBelongMarket(code) + "\\" + date + "\\" + code + "_" + date + ".csv";
        }        
    }
}
