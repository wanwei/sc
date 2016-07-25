using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace com.wer.sc.data.cnfutures.transfer
{
    /// <summary>
    /// 数据生成
    /// </summary>
    public class DataGenerator_TickData
    {
        private bool isCancel = false;
        public bool IsCancel
        {
            get
            {
                return isCancel;
            }

            set
            {
                isCancel = value;
            }
        }

        private DataProviderImpl_CodeInfo dataReader_CodeInfo;

        private TargetPathConfig targetPathConfig;

        private SrcPathConfig srcPathConfig;

        private String[] varieties;

        private List<int> openDates;

        private TickDataTransfer tickDataTransfer;

        private DataProvider_CnFutures dataProvider;

        public DataGenerator_TickData(String srcPath, String targetPath, String[] varieties)
        {
            String configPath = Environment.CurrentDirectory + "\\com.wer.sc.data.cnfutures\\";
            dataReader_CodeInfo = new DataProviderImpl_CodeInfo(configPath);
            this.varieties = varieties;
            if (this.varieties == null)
                varieties = dataReader_CodeInfo.GetVarieties().ToArray();
            String path = srcPath + "\\DL\\";
            openDates = DataProviderImpl_OpenDate.GetOpenDates(path);

            this.targetPathConfig = new TargetPathConfig(targetPath);
            this.srcPathConfig = new SrcPathConfig(srcPath, dataReader_CodeInfo);
            this.tickDataTransfer = new TickDataTransfer(dataReader_CodeInfo, srcPath, targetPath);
            this.dataProvider = new DataProvider_CnFutures(configPath, srcPath, "");
        }

        public void Generate()
        {
            Thread thread1 = new Thread(new ThreadStart(GenerateInternal));
            thread1.Start();
        }

        private void GenerateInternal()
        {
            GenerateInfo generateInfo = DataPrepare();
            if (IsCancel)
                return;
            Generate(generateInfo);
        }

        #region prepare

        private GenerateInfo DataPrepare()
        {
            GenerateInfo generate = GetGeneraters();
            if (AfterPrepared != null)
                AfterPrepared(generate);
            return generate;
        }

        private GenerateInfo GetGeneraters()
        {
            GenerateInfo generateInfo = new GenerateInfo();
            for (int i = 0; i < varieties.Length; i++)
            {
                generateInfo.generates.Add(GetGenerater(varieties[i]));
            }
            return generateInfo;
        }

        private GenerateInfo_Variety GetGenerater(String variety)
        {
            GenerateInfo_Variety g = new GenerateInfo_Variety();
            g.variety = variety;
            //以13结尾的表示指数，如M13表示豆粕指数。只有当日指数生成了，才认为当日数据已全部生成
            String indexCode = variety + "13";
            List<int> updatedDates = targetPathConfig.GetUpdatedDates(indexCode);
            List<int> dates = new List<int>();
            for (int i = 0; i < openDates.Count; i++)
            {
                int date = openDates[i];
                if (!updatedDates.Contains(date))
                    dates.Add(date);
            }
            g.dates = dates;
            return g;
        }

        #endregion

        #region generate

        private void Generate(GenerateInfo generate)
        {
            for (int i = 0; i < generate.generates.Count; i++)
            {
                Generate_Variety(generate.generates[i]);
                if (IsCancel)
                    return;
            }
            AfterGenerated(new GeneratedArgs());
        }

        private void Generate_Variety(GenerateInfo_Variety generate_variety)
        {
            List<int> dates = generate_variety.dates;
            for (int i = 0; i < dates.Count; i++)
            {
                if (IsCancel)
                    return;
                int date = dates[i];
                Generate(generate_variety.variety, date);

                int nextIndex = i + 1;
                if (nextIndex >= dates.Count)
                    continue;
                if ((nextIndex) % 100 == 0)
                {
                    if (AfterGeneratedPeriod != null)
                    {
                        GeneratedPeriodArgs args = new GeneratedPeriodArgs();
                        args.nextStartDate = date;
                        int endIndex = i + 100;
                        endIndex = endIndex >= dates.Count ? dates.Count - 1 : endIndex;
                        args.nextEndDate = dates[endIndex];
                        args.variety = generate_variety.variety;
                        AfterGeneratedPeriod(args);
                    }
                }
            }
        }

        private void Generate(String variety, int date)
        {
            List<CodeInfo> codes = dataReader_CodeInfo.GetCodes(variety);
            //mi结尾是主连，13结尾是指数
            for (int i = 0; i < codes.Count; i++)
            {
                CodeInfo code = codes[i];
                String upperCode = code.code.ToUpper();
                if (upperCode.EndsWith("MI") || upperCode.EndsWith("13"))
                    continue;
                GenerateNormal(code.code, date);
            }
            GenerateMain(codes, variety + "MI", date);
            GenerateIndex(codes, variety + "13", date);
        }

        private void GenerateNormal(String code, int date)
        {
            TickData data = tickDataTransfer.GetTickData(code, date);
            String path = targetPathConfig.GetFilePath(code, date);
            string[] contents = new string[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                data.BarPos = i;
                contents[i] = data.ToString();
            }
            File.WriteAllLines(path, contents);
        }

        private void GenerateMain(List<CodeInfo> codes, String code, int date)
        {            
            for(int i = 0; i < codes.Count; i++)
            {

            }
        }

        private void GenerateIndex(List<CodeInfo> codes, string code, int date)
        {
            Thread.Sleep(2);
        }

        #endregion

        public AfterPreparedHandler AfterPrepared;

        public AfterGeneratedPeriodHandler AfterGeneratedPeriod;

        public AfterGeneratedHandler AfterGenerated;
    }
    public delegate void AfterPreparedHandler(GenerateInfo generateInfo);
    public delegate void AfterGeneratedPeriodHandler(GeneratedPeriodArgs args);
    public delegate void AfterGeneratedHandler(GeneratedArgs args);

    public class GeneratedArgs
    {
    }

    public class GeneratedPeriodArgs
    {
        public String variety;

        public int generatedStartDate;

        public int generatedEndDate;

        public int nextStartDate;

        public int nextEndDate;
    }

    public class GenerateInfo
    {
        public List<GenerateInfo_Variety> generates = new List<GenerateInfo_Variety>();

        public int GetPeriodCount()
        {
            int max = 0;
            for (int i = 0; i < generates.Count; i++)
            {
                max += generates[i].GetCalcPeriodCount();
            }
            return max;
        }
    }

    public class GenerateInfo_Variety
    {
        public String variety;

        public List<int> dates;

        public int GetCalcPeriodCount()
        {
            if (dates.Count % 100 == 0)
                return (dates.Count / 100);
            return (dates.Count / 100) + 1;
        }
    }

    public class SrcPathConfig
    {
        private String srcPath;
        private DataProviderImpl_CodeInfo dataReader_CodeInfo;
        public SrcPathConfig(String srcPath, DataProviderImpl_CodeInfo dataReader_CodeInfo)
        {
            this.srcPath = srcPath;
            this.dataReader_CodeInfo = dataReader_CodeInfo;
        }

        public String GetCodePath(String code, int date)
        {
            String path = srcPath + "\\" + dataReader_CodeInfo.GetBelongMarket(code) + "\\" + code + "_" + date + ".csv";
            return path;
        }
    }

    public class TargetPathConfig
    {
        private String rootPath;

        public TargetPathConfig(String rootPath)
        {
            this.rootPath = rootPath;
        }

        public String GetPath(String code)
        {
            return rootPath + code + "\\";
        }

        public String GetFilePath(String code, int date)
        {
            return GetPath(code) + code + "_" + date + ".csv";
        }

        public List<int> GetUpdatedDates(String code)
        {
            String path = GetPath(code);
            return DataProviderImpl_OpenDate.GetOpenDates(path);
        }
    }
}
