using com.wer.sc.data;
using com.wer.sc.plugin;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ana
{
    /// <summary>
    /// k线模型的运行器
    /// </summary>
    public class KLineModelRunner
    {

        private KLineDataReader reader;

        //code
        private String code;

        //数据开始时间
        private int dataStart = -1;

        //开始日期
        private int start;

        //结束日期
        private int end;

        private KLinePeriod period;

        private int defaultHand;

        //测试所用的金额
        private float initMoney;

        private KLineTradeFee fee;        

        private KLineModel model;

        public string Code
        {
            get
            {
                return code;
            }

            set
            {
                code = value;
            }
        }

        public int Start
        {
            get
            {
                return start;
            }

            set
            {
                start = value;
            }
        }
        public int End
        {
            get
            {
                return end;
            }

            set
            {
                end = value;
            }
        }

        public int DataStart
        {
            get
            {
                return dataStart;
            }

            set
            {
                dataStart = value;
            }
        }

        public int DefaultHand
        {
            get
            {
                return defaultHand;
            }

            set
            {
                defaultHand = value;
            }
        }

        public float InitMoney
        {
            get
            {
                return initMoney;
            }

            set
            {
                initMoney = value;
            }
        }

        public KLineModel Model
        {
            get
            {
                return model;
            }

            set
            {
                model = value;
            }
        }

        public KLinePeriod Period
        {
            get
            {
                return period;
            }

            set
            {
                period = value;
            }
        }

        private KLineData data;

        public KLineData Data
        {
            get
            {
                return data;
            }

            set
            {
                data = value;
            }
        }

        public KLineModelRunner(String datapath)
        {
            this.reader = new KLineDataReader(datapath);
        }

        public void run()
        {
            if (Model.GetModelImports() == null)
            {
                executeNoImport();
            }
            else
            {
                executeImport();
            }
        }        

        private void executeNoImport()
        {
            int realDataStart = DataStart < 0 ? Start : DataStart;
            data = this.reader.GetData(Code, realDataStart, End, Period);
            Model.init(Code, data, fee, DefaultHand, InitMoney);
            Model.ModelStart();
            while (true)
            {
                try
                {
                    int date = Model.Date;
                    if (date >= start)
                        Model.ModelLoop();
                }
                catch (Exception e)
                {
                    //e.StackTrace();
                }
                if (Model.BarPos == data.Length - 1)
                    break;
                else
                    Model.NextBarPos();
            }
            //		for (int i = 0; i < data.getLength(); i++) {
            //			
            //		}
            Model.ModelEnd();
        }

        private void executeImport()
        {
            //准备数据
            List<KLineModelImportWarp> importModels = importDataPrepare();
            data = this.reader.GetData(Code, start, end, Period);
            Model.init(Code, data, fee, DefaultHand, InitMoney);
            Model.ModelStart();
            while (true)
            {
                int date = Model.Date;
                bool realStart = date >= start;

                //首先将import进来的模型loop一遍			
                loopImportWarps(importModels, Model.BarPos, realStart);
                try
                {
                    if (realStart)
                        Model.ModelLoop();
                }
                catch (Exception e)
                {
                    //e.printStackTrace();
                }
                if (Model.BarPos == data.Length - 1)
                    break;
                else
                    Model.NextBarPos();
            }
            Model.ModelEnd();
        }

        private void loopImportWarps(List<KLineModelImportWarp> importModels, int mainBarPos, bool realStart)
        {
            for (int i = 0; i < importModels.Count; i++)
            {
                loopImportWarp(importModels[i], mainBarPos, realStart);
            }
        }

        private void loopImportWarp(KLineModelImportWarp importModelWarp, int mainBarPos, bool realStart)
        {
            /**
             * 修改
             * 
             */
            KLineModel importModel = importModelWarp.model.Model;
            //int barPos = importModel.getBarPos();
            //确定是否要跳转到下一个bar
            if (isNextPos(importModel))
            {
                //barPos++;
                importModel.NextBarPos();
                if (realStart)
                {
                    int barPos = importModel.BarPos;
                    importModel.Arr_Start[barPos] = Model.Start;
                    importModel.Arr_High[barPos] = Model.High;
                    importModel.Arr_Low[barPos] = Model.Low;
                    importModel.Arr_End[barPos] = Model.End;
                    importModel.Arr_Mount[barPos] = Model.Mount;
                }
                //importModel.arr_hold[barPos] = model.hold();
                //importModel.arr_time[barPos] = model.fullTime();			
            }
            else
            {
                if (realStart)
                {
                    int barPos = importModel.BarPos;
                    if (Model.High > importModel.High)
                        importModel.Arr_High[barPos] = Model.High;
                    if (Model.Low < importModel.Low)
                        importModel.Arr_Low[barPos] = Model.Low;
                    importModel.Arr_End[barPos] = Model.End;
                    importModel.Arr_Mount[barPos] += Model.Mount;
                    //importModel.arr_hold[barPos] = model.hold();
                }
            }
            if (realStart)
                importModel.ModelLoop();
        }

        private bool isNextPos(KLineModel importModel)
        {
            int nextBarPos = importModel.BarPos + 1;
            if (nextBarPos >= importModel.Length)
                return false;
            double importTime = importModel.Arr_Time[nextBarPos];
            //String importTime = importModel.fullTime();
            double time = Model.FullTime;
            if ((time > 0 && importTime > 0) || (time < 0 && importTime < 0))
            {
                return time >= importTime;
            }
            else
            {
                return Model.Date > importModel.Date;
            }
        }

        private List<KLineModelImportWarp> importDataPrepare()
        {
            List<KLineModelImportWarp> modelWarps = new List<KLineModelImportWarp>();
            List<KLineModelImport> importModels = Model.GetModelImports();
            for (int i = 0; i < importModels.Count; i++)
            {
                KLineModelImport klineModel = importModels[i];
                String importModelCode = StringUtils.IsEmpty(klineModel.Contract) ? Code : klineModel.Contract;
                int realStart = DataStart < 0 ? Start : DataStart;
                KLineData data = this.reader.GetData(importModelCode, realStart, End, klineModel.KLinePeriod);
                klineModel.Model.init(importModelCode, data);
                //klineModel.Model.setBarPos(-1);
                modelWarps.Add(new KLineModelImportWarp(klineModel, data));
            }
            return modelWarps;
        }
    }

    internal class KLineModelImportWarp
    {
        public KLineModelImport model;

        public KLineData data;

        public KLineModelImportWarp(KLineModelImport model, KLineData data)
        {
            this.model = model;
            this.data = data;
        }
    }
}