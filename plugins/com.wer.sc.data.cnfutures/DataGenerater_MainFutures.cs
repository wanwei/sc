using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cnfutures
{
    public class MainFuturesScan
    {
        private String providerDataPath;

        private DataProviderImpl_CodeInfo codeReader;

        private DataProvider_CnFutures dataProvider;

        public MainFuturesScan(DataProvider_CnFutures dataProvider)
        {
            //this.providerDataPath = dataProvider.ProviderDataPath;
            //this.codeReader = dataProvider.CodeInfoReader;
            this.dataProvider = dataProvider;
        }

        public void Save()
        {
            //dataProvider.GetConfigPath();
        }

        public List<DataGenerater_MainFutures> Scan()
        {
            //String path = dataProvider.GetConfigPath() + "mainfutures.csv";
            //String[] lines = File.ReadAllLines(path);
            //List<MainFutures> main = new List<MainFutures>();
            //for(int i = 0; i<lines.Length; i++)
            //{
            //    String[] strs = lines[i].Split(',');
            //    MainFutures ma = new MainFutures();
            //    ma.Code = strs[0];
            //    ma.Start = int.Parse(strs[1]);
            //    ma.End = int.Parse(strs[2]);
            //    main.Add(ma);
            //}
            //return main;
            return null;
        }

        public List<DataGenerater_MainFutures> Scan(String variety, List<int> openDates)
        {
            List<DataGenerater_MainFutures> mainFutures = new List<DataGenerater_MainFutures>();
            List<CodeInfo> codes = codeReader.GetCodes(variety.ToUpper());
            String lastMainCode = "";
            for (int i = 0; i < openDates.Count; i++)
            {
                int openDate = openDates[i];
                String mainCode = GetMainCode(providerDataPath, codes, openDate);
                if (mainCode == null)
                    continue;
                if (!mainCode.Equals(lastMainCode))
                {
                    bool addNew = true;
                    if (mainFutures.Count > 0)
                    {
                        DataGenerater_MainFutures lastMain = mainFutures[mainFutures.Count - 1];
                        lastMain.End = openDates[i - 1];
                        if (mainFutures.Count > 1 && lastMain.Last < 25)
                        {
                            DataGenerater_MainFutures lastMain2 = mainFutures[mainFutures.Count - 2];
                            if (lastMain2.Code.Equals(mainCode))
                            {
                                mainFutures.RemoveAt(mainFutures.Count - 1);
                                addNew = false;
                            }
                        }
                    }
                    if (addNew)
                    {
                        DataGenerater_MainFutures main = new DataGenerater_MainFutures();
                        main.Code = mainCode;
                        main.Start = openDate;
                        mainFutures.Add(main);
                    }
                }
                lastMainCode = mainCode;
            }
            DataGenerater_MainFutures lMain = mainFutures[mainFutures.Count - 1];
            lMain.End = openDates[openDates.Count - 1];

            return mainFutures;
        }

        private String GetMainCode(String path, List<CodeInfo> codes, int openDate)
        {
            int maxHold = 0;
            //long max = 0;
            String mainCode = null;
            for (int i = 0; i < codes.Count; i++)
            {
                String code = codes[i].code;
                if (code.Contains("MI") || code.Contains("13"))
                    continue;
                String p = GetPath(path, code, openDate);
                if (!File.Exists(p))
                    continue;
                IEnumerable<String> enums = File.ReadLines(p);
                int hold = GetHold(p);
                if (hold > maxHold)
                {
                    maxHold = hold;
                    mainCode = code;
                }
                //FileInfo f = new FileInfo(p);
                //if (!f.Exists)
                //    continue;
                //long l = f.Length;
                //if (l > max)
                //{
                //    max = l;
                //    mainCode = code;
                //}
            }
            return mainCode;
        }

        private int GetHold(String path)
        {
            IEnumerable<String> lines = File.ReadLines(path);
            int cnt = 0;
            foreach (String line in lines)
            {
                if (cnt == 1)
                {
                    int startIndex = findHoldStart(line);
                    int endIndex = line.IndexOf(',', startIndex);
                    return int.Parse(line.Substring(startIndex, endIndex - startIndex));
                }
                cnt++;
            }

            return -1;
        }

        private int findHoldStart(String line)
        {
            int startIndex = line.IndexOf(',', 0) + 1;
            startIndex = line.IndexOf(',', startIndex) + 1;
            startIndex = line.IndexOf(',', startIndex) + 1;
            startIndex = line.IndexOf(',', startIndex) + 1;
            startIndex = line.IndexOf(',', startIndex) + 1;
            return startIndex;
        }

        private String GetPath(String path, String code, int date)
        {
            return path + codeReader.GetBelongMarket(code) + "\\" + date + "\\" + code + "_" + date + ".csv";
        }
    }

    public class DataGenerater_MainFutures
    {
        public String Code;

        public int Start;

        public int End;

        public int Last
        {
            get { return End - Start; }
        }

        override
        public String ToString()
        {
            return Code + "," + Start + "," + End;
        }
    }
}
