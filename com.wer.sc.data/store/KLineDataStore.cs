using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store
{
    /// <summary>
    /// k线数据存储
    /// 存储格式：
    /// 从第一个字节开始直接存储数据
    /// </summary>
    public class KLineDataStore
    {
        public const int LEN_EVERYKLINE = 36;

        private String path;

        private KLineDataIndex indexer;

        public KLineDataStore(String path)
        {
            this.path = path;
            this.indexer = new KLineDataIndex(path);
        }

        public void Save(KLineData data)
        {
            DirectoryInfo dir = Directory.GetParent(path);
            if (!dir.Exists)
                dir.Create();

            byte[] bs = GetBytes(data);
            FileStream file = new FileStream(path, FileMode.Create);
            try
            {
                file.Write(bs, 0, bs.Length);
            }
            finally
            {
                file.Close();
            }
        }

        public void Append(KLineData data)
        {
            byte[] bs = GetBytes(data);
            FileStream file = new FileStream(path, FileMode.Append);
            try
            {
                file.Write(bs, 0, bs.Length);
            }
            finally
            {
                file.Close();
            }
        }

        public KLineData Load()
        {
            FileStream file = new FileStream(path, FileMode.Open);
            try
            {
                byte[] bs = new byte[file.Length];
                file.Seek(0, SeekOrigin.Begin);
                file.Read(bs, 0, bs.Length);

                return FromBytes(bs, 0, bs.Length);
            }
            finally
            {
                file.Close();
            }
        }

        public KLineData LoadByIndex(int startIndex, int endIndex)
        {
            FileStream file = new FileStream(path, FileMode.Open);
            try
            {
                byte[] bs = new byte[file.Length];
                file.Seek(0, SeekOrigin.Begin);
                file.Read(bs, 0, bs.Length);

                return FromBytes(bs, startIndex * LEN_EVERYKLINE, (endIndex - startIndex + 1) * LEN_EVERYKLINE);
            }
            finally
            {
                file.Close();
            }
        }

        public KLineData FromBytes(byte[] bs)
        {
            return FromBytes(bs, 0, bs.Length);
        }

        public KLineData FromBytes(byte[] bs, int start, int len)
        {
            int size = LEN_EVERYKLINE;
            int dataLength = len / size;
            KLineData data = new KLineData(dataLength);
            for (int i = 0; i < dataLength; i++)
            {
                int offset = i * size + start;
                data.arr_time[i] = BitConverter.ToDouble(bs, offset);
                offset += 8;

                data.arr_start[i] = BitConverter.ToSingle(bs, offset);
                offset += 4;

                data.arr_high[i] = BitConverter.ToSingle(bs, offset);
                offset += 4;

                data.arr_low[i] = BitConverter.ToSingle(bs, offset);
                offset += 4;

                data.arr_end[i] = BitConverter.ToSingle(bs, offset);
                offset += 4;

                data.arr_mount[i] = BitConverter.ToInt32(bs, offset);
                offset += 4;

                data.arr_money[i] = BitConverter.ToSingle(bs, offset);
                offset += 4;

                data.arr_hold[i] = BitConverter.ToInt32(bs, offset);
                offset += 4;
            }

            return data;
        }

        public byte[] GetBytes(KLineData data)
        {
            int size = LEN_EVERYKLINE;
            byte[] bs = new byte[size * data.Length];
            int offset = 0;
            for (int i = 0; i < data.Length; i++)
            {
                data.BarPos = i;
                byte[] tmpBs = BitConverter.GetBytes(data.FullTime);
                Array.Copy(tmpBs, 0, bs, offset, 8);
                offset += 8;

                tmpBs = BitConverter.GetBytes(data.Start);
                Array.Copy(tmpBs, 0, bs, offset, 4);
                offset += 4;

                tmpBs = BitConverter.GetBytes(data.High);
                Array.Copy(tmpBs, 0, bs, offset, 4);
                offset += 4;

                tmpBs = BitConverter.GetBytes(data.Low);
                Array.Copy(tmpBs, 0, bs, offset, 4);
                offset += 4;

                tmpBs = BitConverter.GetBytes(data.End);
                Array.Copy(tmpBs, 0, bs, offset, 4);
                offset += 4;

                tmpBs = BitConverter.GetBytes(data.Mount);
                Array.Copy(tmpBs, 0, bs, offset, 4);
                offset += 4;

                tmpBs = BitConverter.GetBytes(data.Money);
                Array.Copy(tmpBs, 0, bs, offset, 4);
                offset += 4;

                tmpBs = BitConverter.GetBytes(data.Hold);
                Array.Copy(tmpBs, 0, bs, offset, 4);
                offset += 4;
            }
            return bs;
        }

        public double GetFirstTime()
        {
            FileStream file = new FileStream(path, FileMode.Open);
            try
            {
                byte[] bs = new byte[8];
                file.Seek(0, SeekOrigin.Begin);
                file.Read(bs, 0, bs.Length);
                return BitConverter.ToDouble(bs, 0);
            }
            finally
            {
                file.Close();
            }
        }

        public double GetLastTime()
        {
            if (!File.Exists(path))
                return -1;
            FileStream file = new FileStream(path, FileMode.Open);
            try
            {
                long index = file.Length - LEN_EVERYKLINE;

                byte[] bs = new byte[8];
                file.Seek(index, SeekOrigin.Begin);
                file.Read(bs, 0, bs.Length);
                return BitConverter.ToDouble(bs, 0);
            }
            finally
            {
                file.Close();
            }
        }

        public KLineData Load(int startDate, int endDate)
        {
            KLineDataIndexResult result = LoadIndex();
            int startIndex;
            if (result.IndexDic.Keys.Contains(startDate))
                startIndex = result.IndexDic[startDate];
            else
                startIndex = FindIndex(result, startDate, true);

            int endIndex;
            if (result.IndexDic.Keys.Contains(endDate))
                endIndex = result.IndexDic[endDate];
            else
                endIndex = FindIndex(result, endDate, false);
            if (startIndex < 0 || endIndex < 0 || startIndex > endIndex)
                return null;
            return LoadByIndex(startIndex, endIndex);
        }

        private int FindIndex(KLineDataIndexResult result, int date, bool forward)
        {
            List<int> dateList = result.DateList;
            if (forward)
            {
                int lastDate = dateList[0];
                if (lastDate > date)
                    return 0;
                for (int i = 1; i < dateList.Count; i++)
                {
                    if (dateList[i] > date && dateList[i - 1] < date)
                        return i;
                }
            }
            else
            {
                int lastDate = dateList[dateList.Count - 1];
                if (lastDate < date)
                    return 0;
                for (int i = dateList.Count - 2; i >= 0; i--)
                {
                    if (dateList[i] < date && dateList[i + 1] > date)
                        return i;
                }
            }
            return -1;
        }

        private KLineDataIndexResult LoadIndex()
        {
            KLineDataIndexResult result = LoadIndex2();
            if (result == null)
            {
                DoIndex();
                return LoadIndex2();
            }
            else
            {
                int lastDate = (int)GetLastTime();
                if (result.LastDate != lastDate)
                {
                    DoIndex();
                    return LoadIndex2();
                }
                else
                    return result;
            }
        }

        private KLineDataIndexResult LoadIndex2()
        {
            return indexer.GetIndex();
        }

        /// <summary>
        /// 生成索引
        /// </summary>
        private void DoIndex()
        {
            indexer.DoIndex();
        }
    }

    public class KLineDataIndexResult
    {
        private Dictionary<int, int> indexDic = new Dictionary<int, int>();
        private List<int> dateList = new List<int>();

        public void AddIndex(int date, int index)
        {
            IndexDic.Add(date, index);
            dateList.Add(date);
        }

        public Dictionary<int, int> IndexDic
        {
            get
            {
                return indexDic;
            }
        }

        public List<int> DateList
        {
            get
            {
                return dateList;
            }
        }

        public int LastDate
        {
            get
            {
                return dateList[dateList.Count - 1];
            }
        }
    }

    public class KLineDataIndex
    {
        private String path;

        public KLineDataIndex(String path)
        {
            this.path = path;
        }

        public KLineDataIndexResult GetIndex()
        {
            String indexPath = path + ".index";
            if (!File.Exists(indexPath))
                return null;
            KLineDataIndexResult result = new KLineDataIndexResult();
            String[] lines = File.ReadAllLines(indexPath);
            for (int i = 0; i < lines.Length; i++)
            {
                String line = lines[i];
                String[] arr = line.Split(',');
                result.AddIndex(int.Parse(arr[0]), int.Parse(arr[1]));
            }
            return result;
        }

        public void DoIndex()
        {
            if (!File.Exists(path))
                return;
            String indexPath = path + ".index";
            FileStream file = new FileStream(path, FileMode.Open);
            try
            {
                double lastTime = GetTimeByIndex(file, 0);
                double time = GetTimeByIndex(file, 1);
                KLinePeriod period = KLineData.GetPeriod(lastTime, time);
                //if (period.PeriodType > KLinePeriod.TYPE_MINUTE)
                //    return;

                List<String> indeies = new List<string>(500);
                indeies.Add(((int)lastTime).ToString() + ",0");
                int len = GetLength(file);
                for (int index = 1; index < len; index++)
                {
                    time = GetTimeByIndex(file, index);

                    int changeDateType = GetChangeDateType(time, lastTime);
                    if (changeDateType >= 0)
                    {
                        indeies.Add(((int)time + changeDateType).ToString() + "," + index.ToString());
                    }

                    //停牌超过四个小时
                    lastTime = time;
                }

                File.WriteAllLines(indexPath, indeies.ToArray());
            }
            finally
            {
                file.Close();
            }
        }

        private int GetChangeDateType(double time, double lastTime)
        {
            //-1 不修改日期、 0当日开盘、1要加1
            double distance = time - lastTime;
            if (distance >= 1)
                return 0;
            if (distance < 0.04)
                return -1;
            int date = (int)time;
            int lastdate = (int)lastTime;
            if (date == lastdate)
                return 1;

            double lastt = lastTime - (int)lastTime;

            //收盘时间在晚上9点后，早上8点前都认为是夜盘，夜盘记作第二天开盘
            if (lastt > 0.21 || lastt < 0.08)
                return -1;
            return 0;
        }

        private double GetTimeByIndex(FileStream file, int index)
        {
            byte[] bs = new byte[8];
            file.Seek(index * KLineDataStore.LEN_EVERYKLINE, SeekOrigin.Begin);
            file.Read(bs, 0, bs.Length);
            return BitConverter.ToDouble(bs, 0);
        }

        private int GetLength(FileStream file)
        {
            return (int)(file.Length / KLineDataStore.LEN_EVERYKLINE);
        }
    }
}