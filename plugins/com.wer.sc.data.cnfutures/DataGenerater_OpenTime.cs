using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.data.cnfutures
{
    public class DataGenerater_OpenTime
    {
        private Dictionary<String, OpenTimeInfo> dicOpenTime;
        private DataProviderImpl_CodeInfo codeInfoReader;
        public DataGenerater_OpenTime(DataProviderImpl_CodeInfo codeInfoReader)
        {
            OpenTimeLoader loader = new OpenTimeLoader();
            dicOpenTime = loader.GetOpenTimes();
            this.codeInfoReader = codeInfoReader;
        }

        public List<float[]> GetOpenTime(string code, int date)
        {
            String mkt = codeInfoReader.GetBelongMarket(code);
            if (mkt.Equals("DL"))
            {
            }
            return null;
        }
    }

    public class OpenTimeInfo
    {
        public String Name;

        public List<float[]> openTime;

        override
        public String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Name).Append(",");

            for (int i = 0; i < openTime.Count; i++)
            {
                if (i != 0)
                    sb.Append(";");
                float[] ot = openTime[i];
                sb.Append(Math.Round(ot[0], 6)).Append("-").Append(Math.Round(ot[1], 6));
            }
            return sb.ToString();
        }
    }


    public class OpenTimeLoader
    {
        public OpenTimeLoader()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("");
        }

        public Dictionary<String, OpenTimeInfo> GetOpenTimes()
        {
            Dictionary<String, OpenTimeInfo> dic = new Dictionary<string, OpenTimeInfo>();

            return dic;
        }
    }

    public class OpenTimeBak
    {
        private DataProvider_CnFutures dataProvider;

        private List<String> varieties = new List<string>();
        private Dictionary<String, List<DataGenerater_MainFutures>> dic = new Dictionary<string, List<DataGenerater_MainFutures>>();

        public OpenTimeBak(DataProvider_CnFutures dataProvider)
        {
            this.dataProvider = dataProvider;
            this.initMainFutures();
        }

        private void initMainFutures()
        {
            MainFuturesScan scan = new MainFuturesScan(dataProvider);
            List<DataGenerater_MainFutures> mainFutures = scan.Scan();
            for (int i = 0; i < mainFutures.Count; i++)
            {
                //MainFutures mf = mainFutures[i];
                //String catelog = dataProvider.CodeInfoReader.GetCode(mf.Code).catelog;
                //if (!varieties.Contains(catelog))
                //{
                //    varieties.Add(catelog);
                //    List<MainFutures> mfs = new List<MainFutures>();
                //    mfs.Add(mf);
                //    dic.Add(catelog, mfs);
                //}
                //else
                //{
                //    dic[catelog].Add(mf);
                //}
            }
        }

        public List<OpenTimeInfoBak> Scan(String variety)
        {
            List<OpenTimeInfoBak> openTimes = new List<OpenTimeInfoBak>();
            List<DataGenerater_MainFutures> mainFutures = dic[variety.ToUpper()];
            List<int> openDates = dataProvider.GetOpenDates();
            for (int i = 0; i < mainFutures.Count; i++)
            {
                DataGenerater_MainFutures mf = mainFutures[i];
                AddOpenTimes(mf, openDates, openTimes);
            }
            return openTimes;
        }

        private void AddOpenTimes(DataGenerater_MainFutures mf, List<int> openDates, List<OpenTimeInfoBak> openTimes)
        {
            int startIndex = openDates.IndexOf(mf.Start);
            int endIndex = openDates.IndexOf(mf.End);

            for (int i = startIndex; i <= endIndex; i++)
            {
                int openDate = openDates[i];
                //String path = dataProvider.GetCodePath(mf.Code, openDate);
                //List<float[]> ot = GetOpenTime(path);
                //if (openTimes.Count == 0)
                //{
                //    OpenTimeInfoBak openTime = new OpenTimeInfoBak();
                //    openTime.Start = openDate;
                //    openTime.End = openDate;
                //    openTime.Variety = mf.Code;
                //    openTime.openTime = ot;
                //    openTimes.Add(openTime);
                //}
                //else
                //{
                //    OpenTimeInfoBak openTime = openTimes[openTimes.Count - 1];
                //    if (IsOpenTimeEqual(ot, openTime.openTime))
                //    {
                //        openTime.End = openDate;
                //    }
                //    else
                //    {
                //        OpenTimeInfoBak newOpenTime = new OpenTimeInfoBak();
                //        newOpenTime.Start = openDate;
                //        newOpenTime.End = openDate;
                //        newOpenTime.Variety = mf.Code;
                //        newOpenTime.openTime = ot;
                //        openTimes.Add(newOpenTime);
                //    }
                //}
            }
        }

        private static Boolean IsOpenTimeEqual(List<float[]> ot1, List<float[]> ot2)
        {
            if (ot1.Count != ot2.Count)
                return false;
            for (int i = 0; i < ot1.Count; i++)
            {
                float[] f1 = ot1[i];
                float[] f2 = ot2[i];
                if (f1.Length != f2.Length)
                    return false;
                if (f1[0] != f2[0] || f1[1] != f2[1])
                    return false;
            }
            return true;
        }

        public static List<float[]> GetOpenTime(String path)
        {
            List<float[]> openTimeArr = new List<float[]>();
            IEnumerable<String> openTime = File.ReadLines(path);
            float lastTime = -1;
            int cnt = 0;
            foreach (String line in openTime)
            {
                if (cnt == 0)
                {
                    cnt++;
                    continue;
                }
                if (line.Equals(""))
                    continue;
                int startIndex = line.IndexOf(',') + 1;
                int endIndex = line.IndexOf(',', startIndex);
                String[] timeArr = line.Substring(startIndex, endIndex - startIndex).Split(':');
                float time = float.Parse(timeArr[0] + timeArr[1] + timeArr[2]);
                float currentTime = time / 1000000;
                //说明是第一条数据
                if (lastTime < 0)
                {
                    float[] ot = new float[2];
                    ot[0] = GetEndPoint(currentTime);
                    openTimeArr.Add(ot);
                }
                else
                {
                    TimeSpan span = TimeUtils.Subtract(currentTime, lastTime);
                    float between = span.Hours * 60 + span.Minutes;
                    //超过10分钟就可以认为是停牌了
                    if (between > 10 || between < 0)
                    {
                        float[] lastOt = openTimeArr[openTimeArr.Count - 1];
                        lastOt[1] = GetEndPoint(lastTime);

                        float[] ot = new float[2];
                        ot[0] = GetEndPoint(currentTime);
                        openTimeArr.Add(ot);
                    }
                }
                lastTime = currentTime;
            }

            float[] lastOt2 = openTimeArr[openTimeArr.Count - 1];
            lastOt2[1] = GetEndPoint(lastTime);

            return openTimeArr;
        }

        private static float GetEndPoint(float f)
        {
            double d = (double)f + 20100101; //Math.Round((double)f, 4) + 20100101;
            DateTime time = TimeConvert.ConvertToDateTime(d);
            DateTime result;
            int ff = time.Minute % 5;
            if (ff > 2)
                result = time.Add(new TimeSpan(0, 5 - ff, 0));
            else
                result = time.Add(new TimeSpan(0, -ff, 0));

            return GetNormalEndPoint(time);
            //double dd = (TimeConvert.ConvertToDoubleTime(result.AddSeconds(-result.Second)) - 20100101);
            ////dd = ((double)((int)(dd * 10000))) / 10000;
            //return GetNormalEndPoint((float)Math.Round(dd, 4));
        }

        private static float GetNormalEndPoint(DateTime time)
        {
            DateTime nTime = time;
            for (int i = 0; i < normalEndPoints.Length; i++)
            {
                DateTime endTime = normalEndPoints[i];
                TimeSpan span = endTime.Subtract(time);
                if (Math.Abs(span.Minutes + 60 * span.Hours) < 20)
                {
                    nTime = endTime;
                    break;
                }
            }
            return (float)Math.Round((float)(TimeConvert.ConvertToDoubleTime(nTime) - 20100101), 4);
        }

        private static DateTime[] normalEndPoints = GetNormalEndPoints();

        private static DateTime[] GetNormalEndPoints()
        {
            DateTime[] dtArr = new DateTime[8];
            dtArr[0] = Convert.ToDateTime("2010-01-01 09:00:00");
            dtArr[1] = Convert.ToDateTime("2010-01-01 10:15:00");
            dtArr[2] = Convert.ToDateTime("2010-01-01 10:30:00");
            dtArr[3] = Convert.ToDateTime("2010-01-01 11:30:00");
            dtArr[4] = Convert.ToDateTime("2010-01-01 13:30:00");
            dtArr[5] = Convert.ToDateTime("2010-01-01 15:00:00");
            dtArr[6] = Convert.ToDateTime("2010-01-01 21:00:00");
            dtArr[7] = Convert.ToDateTime("2010-01-01 23:30:00");
            return dtArr;
        }

        private static float GetNormalEndPoint(float f)
        {
            float[] ff = new float[] { 0.09f, 0.1015f, 0.1030f, 0.113f, 0.133f, 0.15f };
            for (int i = 0; i < ff.Length; i++)
            {
                if (Math.Abs(ff[i] - f) < 0.001)
                    return ff[i];
            }
            return f;
        }
    }


    public class OpenTimeInfoBak
    {
        public String Variety;

        public int Start;

        public int End;

        public List<float[]> openTime;

        override
        public String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Variety).Append(",");
            sb.Append(Start).Append(",");
            sb.Append(End).Append(",");

            for (int i = 0; i < openTime.Count; i++)
            {
                if (i != 0)
                    sb.Append(";");
                float[] ot = openTime[i];
                sb.Append(Math.Round(ot[0], 6)).Append("-").Append(Math.Round(ot[1], 6));
            }
            return sb.ToString();
        }
    }
}