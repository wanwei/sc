using com.wer.sc.data.store;
using com.wer.sc.data.test.Properties;
using com.wer.sc.data.update;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    public class ResourceLoader
    {
        public static TickData LoadTickData_M01_20131231()
        {
            String csv = Resources.m01_20131231;
            String[] lines = csv.Split("\r".ToCharArray());
            return ReadLinesToTickData("m01", lines);
        }

        public static TickData LoadTickData_AG05_20141230()
        {
            String csv = Resources.AG05_20141230;
            String[] lines = csv.Split("\r".ToCharArray());
            return ReadLinesToTickData("ag05", lines);
        }

        public static TickData LoadTickData_M05_20150106()
        {
            String csv = Resources.M05_20150106;
            String[] lines = csv.Split("\r".ToCharArray());
            return ReadLinesToTickData2(lines);
        }

        private static TickData ReadLinesToTickData2(string[] lines)
        {
            TickData data = new TickData(lines.Length - 1);
            for (int i = 0; i < lines.Length - 1; i++)
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

        public static KLineData LoadKLineData()
        {
            String csv = Resources.DLm05;
            String[] lines = csv.Split("\r".ToCharArray());
            KLineData data = new KLineData(lines.Length);
            for (int i = 0; i < lines.Length; i++)
            {
                String line = lines[i];
                if (line.Equals(""))
                    continue;
                String[] dataArr = line.Split(',');

                String[] timeArr = dataArr[0].Split('/');
                double time = double.Parse(timeArr[0] + timeArr[1] + timeArr[2]);

                data.arr_time[i] = time;
                data.arr_start[i] = float.Parse(dataArr[1]);
                data.arr_high[i] = float.Parse(dataArr[2]);
                data.arr_low[i] = float.Parse(dataArr[3]);
                data.arr_end[i] = float.Parse(dataArr[4]);
                data.arr_mount[i] = (int)float.Parse(dataArr[5]);
                data.arr_money[i] = float.Parse(dataArr[6]);
            }
            return data;
        }

        public static String DataPath
        {
            get { return System.Environment.CurrentDirectory + "\\data\\"; }
        }

        //public static List<CodeInfo> GetCodes()
        //{
        //    String csv = Resources.codes;
        //    String[] lines = csv.Split("\r".ToCharArray());
        //    List<CodeInfo> codeList = new List<CodeInfo>();
        //    TickData data = new TickData(lines.Length);
        //    //int loopLength = Append ? lines.Length - 1 : lines.Length - 101;
        //    for (int i = 0; i < lines.Length; i++)
        //    {
        //        String line = lines[i];
        //        if (line.Equals(""))
        //            continue;
        //        String[] dataArr = line.Split(',');
        //        if (dataArr.Length < 3)
        //            continue;
        //        CodeInfo strs = new CodeInfo();
        //        String d1 = dataArr[0];
        //        if (d1.StartsWith("\n"))
        //            d1 = d1.Substring(1);
        //        strs.code = d1;
        //        strs.catelog = dataArr[4];
        //        codeList.Add(strs);
        //    }
        //    return codeList;
        //}

        public static List<int> GetOpenDates()
        {
            String path = System.Environment.CurrentDirectory + "\\data\\tick\\m01\\";
            String[] files = Directory.GetFiles(path);
            List<int> dates = new List<int>();
            for (int i = 0; i < files.Length; i++)
            {
                String filePath = files[i];
                int index = filePath.LastIndexOf("m01_");
                dates.Add(int.Parse(files[i].Substring(index + 4, 8)));
            }
            return dates;
        }

        public static TickData GetTickData(String code, int date)
        {
            String path = DataPath + "tick\\" + code + "\\" + code + "_" + date + ".csv";
            if (!File.Exists(path))
                return null;
            String[] lines = File.ReadAllLines(path);
            return ReadLinesToTickData(code, lines);
        }

        public static TickData ReadLinesToTickData(string code, string[] lines)
        {
            int cnt = GetEmptyLines(lines);
            TickData data = new TickData(lines.Length - 1 - cnt);
            data.Code = code.ToUpper();
            for (int i = 0; i < lines.Length - 1 - cnt; i++)
            {
                String line = lines[i + 1];
                if (line.Equals(""))
                    continue;
                String[] dataArr = line.Split(',');
                if (dataArr.Length < 5)
                    continue;

                String[] dateArr = dataArr[0].Split('-');
                double date = double.Parse(dateArr[0] + dateArr[1] + dateArr[2]);
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

        public static KLineData GetKLineData_1Min()
        {
            String path = DataPath + "m01_1minute.kline";
            KLineDataStore store = new KLineDataStore(path);
            return store.Load();
        }

        public static String[] GetKLineData_1Min_Result()
        {
            String path = DataPath + "m01_1minute.csv";
            return File.ReadAllLines(path);
        }

        public static String[] GetKLineData_15Second_Result()
        {
            String path = DataPath + "m01_15second.csv";
            return File.ReadAllLines(path);
        }

        public static String[] GetKLineData_15Minute_Result()
        {
            String path = DataPath + "m01_15minute.csv";
            return File.ReadAllLines(path);
        }

        public static String[] GetKLineData_1Hour_Result()
        {
            String path = DataPath + "m01_1hour.csv";
            return File.ReadAllLines(path);
        }

        public static String[] GetKLineData_15Minute_Append_Result()
        {
            String path = DataPath + "m01_15minute_Append.csv";
            return File.ReadAllLines(path);
        }

        public static String[] GetResultData(String fileName)
        {
            String path = DataPath + fileName;
            return File.ReadAllLines(path);
        }

        public static DataReaderFactory GetDefaultDataReaderFactory()
        {
            return new DataReaderFactory(@"d:\scdata\cnfutures\");
        }

        public static String GetTestOutputPath()
        {
            return @"d:\sctest\datatest\";
        }

        public static String GetTestOutputPath(String path)
        {
            return @"d:\sctest\datatest\" + path;
        }
    }
}