using com.wer.sc.ana;
using com.wer.sc.comp.graphic;
using com.wer.sc.data;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin
{
    /// <summary>
    /// k线模型
    /// </summary>
    public abstract class Plugin_KLineModel
    {
        private KLineData data;

        private KLineTrade trade;

        private String code;

        public Plugin_KLineModel()
        {
        }

        public void init(String code, KLineData data, KLineTradeFee fee, int defaultHand, float initMoney)
        {
            initTrader(data, fee, defaultHand, initMoney);
            init(code, data);
        }

        /// <summary>
        /// 得到当前正在交易的股票或期货代码
        /// </summary>
        /// <returns></returns>
        public String Code
        {
            get
            {
                return code;
            }
        }

        #region can be overide

        /// <summary>
        /// 该方法用于子类重载，在模型开始运行前会运行该方法
        /// </summary>
        public virtual void ModelStart()
        {

        }

        /// <summary>
        /// 该方法用于子类重载，在模型开始运行后会运行该方法
        /// </summary>
        public virtual void ModelEnd()
        {

        }

        /// <summary>
        /// 该方法用于子类重载，可以得到注入的模型
        /// 注入模型和组合模型(CompoundModels)不同之处在于
        /// 注入模型可以引用不同周期的数据，比如现在基于1分钟数据做的模型，也可以将基于15分钟线的模型拿来进行分析
        /// 组合模型仅仅是为了多模型复用，让一个模型使用其它模型里面的逻辑。
        /// </summary>
        /// <returns></returns>
        public virtual List<KLineModelImport> GetModelImports()
        {
            return null;
        }

        /// <summary>
        /// 该方法用于子类重载，主要用于多模型间复用
        /// </summary>
        /// <returns></returns>
        public virtual List<Plugin_KLineModel> GetCompoundModels()
        {
            return null;
        }

        /// <summary>
        /// 初始化交易器
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fee"></param>
        /// <param name="defaultHand"></param>
        /// <param name="initMoney"></param>
        public virtual void initTrader(KLineData data, KLineTradeFee fee, int defaultHand, float initMoney)
        {
            this.trade = new KLineTrade(data, fee, defaultHand, initMoney);
        }

        /**
         * 初始化模型
         * 该方法用于注入的和组合模型
         * @param code
         * @param data
         */
        public virtual void init(String code, KLineData data)
        {
            /**
             * compoundmodel不需要支持交易
             */
            this.code = code;
            this.data = data;
            List<Plugin_KLineModel> models = GetCompoundModels();
            if (models != null)
            {
                for (int i = 0; i < models.Count; i++)
                {
                    models[i].init(code, data);
                }
            }
        }

        #endregion

        public void ModelLoop()
        {
            List<Plugin_KLineModel> models = GetCompoundModels();
            if (models != null)
            {
                for (int i = 0; i < models.Count; i++)
                {
                    models[i].ModelLoop();
                }
            }
            Loop();
        }

        public abstract void Loop();

        public int Length
        {
            get
            {
                return data.arr_start.Length;
            }
        }

        #region 交易

        public bool AutoFilter
        {
            get
            {
                return trade.AutoFilter;
            }
            set
            {
                if (this.trade == null)
                    return;
                trade.AutoFilter = value;
            }
        }

        public void bk()
        {
            if (this.trade == null)
                return;
            trade.bk();
        }

        public void bk(int cnt)
        {
            if (this.trade == null)
                return;
            trade.bk(cnt);
        }

        public void bp()
        {
            if (this.trade == null)
                return;
            trade.bp();
        }

        public void bp(int cnt)
        {
            if (this.trade == null)
                return;
            trade.bp(cnt);
        }

        public void sk()
        {
            if (this.trade == null)
                return;
            trade.sk();
        }

        public void sk(int cnt)
        {
            if (this.trade == null)
                return;
            trade.sk(cnt);
        }

        public void sp()
        {
            if (this.trade == null)
                return;
            trade.sp();
        }

        public void sp(int cnt)
        {
            if (this.trade == null)
                return;
            trade.sp(cnt);
        }

        #endregion

        #region 数据属性

        /// <summary>
        /// 得到当前
        /// </summary>
        public int BarPos
        {
            get
            {
                return data.BarPos;
            }
        }

        public void NextBarPos()
        {
            this.data.BarPos++;
        }


        /// <summary>
        /// 得到当前对应的Chart
        /// </summary>
        /// <returns></returns>
        public KLineChart_KLineData Chart
        {
            get
            {
                return new KLineChart_KLineData(this.data, BarPos);
            }
        }

        public KLineData KLineData
        {
            get { return this.data; }
        }

        public float Start
        {
            get
            {
                return Arr_Start[BarPos];
            }
        }

        public float High
        {
            get
            {
                return Arr_High[BarPos];
            }
        }

        public float Low
        {
            get
            {
                return Arr_Low[BarPos];
            }
        }

        public float End
        {
            get
            {
                return Arr_End[BarPos];
            }
        }

        public int Mount
        {
            get
            {
                return Arr_Mount[BarPos];
            }
        }

        public float Hold
        {
            get
            {
                return Arr_Hold[BarPos];
            }
        }

        public double FullTime
        {
            get
            {
                return Arr_Time[BarPos];
            }
        }

        public float Time
        {
            get
            {
                return (float)(Arr_Time[BarPos] - Date);
            }
        }

        public int Date
        {
            get
            {
                return (int)Arr_Time[BarPos];
            }
        }

        /// <summary>
        /// 得到当前k线block的高价位
        /// </summary>
        public float BlockHigh
        {
            get
            {
                return arr_blockhigh[BarPos];
            }
        }

        /// <summary>
        /// 得到当前k线block的低价位
        /// </summary>
        public float BlockLow
        {
            get
            {
                return arr_blocklow[BarPos];
            }
        }

        public float[] Arr_Start
        {
            get
            {
                return data.arr_start;
            }
        }

        public float[] Arr_High
        {
            get
            {
                return data.arr_high;
            }
        }

        public float[] Arr_Low
        {
            get
            {
                return data.arr_low;
            }
        }

        public float[] Arr_End
        {
            get
            {
                return data.arr_end;
            }
        }

        public int[] Arr_Mount
        {
            get
            {
                return data.arr_mount;
            }
        }

        public int[] Arr_Hold
        {
            get
            {
                return data.arr_hold;
            }
        }

        public double[] Arr_Time
        {
            get
            {
                return data.arr_time;
            }
        }

        private float[] arr_height;

        /// <summary>
        /// 得到每个k线的振幅数组
        /// </summary>
        public float[] Arr_Height
        {
            get
            {
                if (arr_height != null)
                    return arr_height;
                arr_height = new float[Length];
                for (int i = 0; i < arr_height.Length; i++)
                {
                    arr_height[i] = Arr_High[i] - Arr_Low[i];
                }
                return arr_height;
            }
        }

        private float[] arr_HeightPercent;

        /**
         * 当日振幅
         * @return
         */
        public float[] Arr_HeightPercent
        {
            get
            {
                if (arr_HeightPercent != null)
                    return arr_HeightPercent;
                arr_HeightPercent = new float[Length];
                for (int i = 0; i < arr_HeightPercent.Length; i++)
                {
                    arr_HeightPercent[i] = (float)NumberUtils.percent(Math.Abs(Arr_Start[i] - Arr_End[i]), Arr_End[i]);
                }
                return arr_HeightPercent;
            }
        }

        private float[] arr_blockhigh;

        /**
         * block的高
         * @return
         */
        public float[] Arr_BlockHigh
        {
            get
            {
                if (arr_blockhigh != null)
                    return arr_blockhigh;
                arr_blockhigh = new float[Length];
                for (int i = 0; i < Length; i++)
                {
                    arr_blockhigh[i] = Arr_Start[i] > Arr_End[i] ? Arr_Start[i] : Arr_End[i];
                }

                return arr_blockhigh;
            }
        }

        private float[] arr_blocklow;

        /**
         * block的低价数组
         * @return
         */
        public float[] Arr_BlockLow
        {
            get
            {
                if (arr_blocklow != null)
                    return arr_blocklow;
                arr_blocklow = new float[Length];
                for (int i = 0; i < Length; i++)
                {
                    arr_blocklow[i] = Arr_Start[i] < Arr_End[i] ? Arr_Start[i] : Arr_End[i];
                }

                return arr_blocklow;
            }
        }

        private float[] arr_blockheight;

        /**
         * block的高度
         * @return
         */
        public float[] Arr_BlockHeight
        {
            get
            {
                if (arr_blockheight != null)
                    return arr_blockheight;
                arr_blockheight = new float[Length];
                for (int i = 0; i < Length; i++)
                {
                    arr_blockheight[i] = Math.Abs(Arr_Start[i] - Arr_End[i]);
                }

                return arr_blockheight;
            }
        }

        private float[] arr_percentBlockHeight;

        /**
         * block的高度
         * @return
         */
        public float[] Arr_BlockHeightPercent
        {
            get
            {
                if (arr_percentBlockHeight != null)
                    return arr_percentBlockHeight;
                arr_percentBlockHeight = new float[Length];
                for (int i = 0; i < Length; i++)
                {
                    arr_percentBlockHeight[i] = (float)NumberUtils.percent(Math.Abs(Arr_Start[i] - Arr_End[i]), Arr_End[i]);
                }

                return arr_percentBlockHeight;
            }
        }

        private float[] arr_UpPercent;

        /**
         * 当前k线涨幅数组
         * @return
         */
        public float[] Arr_UpPercent
        {
            get
            {
                if (arr_UpPercent != null)
                    return arr_UpPercent;
                arr_UpPercent = new float[Length];
                for (int i = 0; i < Length; i++)
                {
                    if (i == 0)
                        arr_UpPercent[i] = (float)NumberUtils.percent(Arr_End[i] - Arr_Start[i], Arr_Start[i]);
                    else
                        arr_UpPercent[i] = (float)NumberUtils.percent(Arr_End[i] - Arr_End[i - 1], Arr_End[i - 1]);
                }
                return arr_UpPercent;
            }
        }

        #endregion

        #region 数据操作及获取

        public void AddData(float[] objs, float value)
        {
            objs[BarPos] = value;
        }

        public void AddData(List<float> objs, float value)
        {
            objs.Add(value);
        }

        public void AddData(List<int> objs, int value)
        {
            objs.Add(value);
        }

        public float RefData(float[] objs, int len)
        {
            return objs[BarPos - len];
        }

        public float RefData(List<float> objs, int len)
        {
            return objs[objs.Count - 1 - len];
        }

        public int RefData(List<int> objs, int len)
        {
            return objs[objs.Count - 1 - len];
        }

        public int RefData(int len)
        {
            return (int)Arr_Time[BarPos - len];
        }

        #endregion

        #region 外部函数

        public float height(int len)
        {
            float high2 = High;
            float low2 = Low;
            int pos = BarPos;
            for (int i = pos - len + 1; i < pos; i++)
            {
                float chigh = Arr_High[i];
                float clow = Arr_Low[i];
                high2 = high2 > chigh ? high2 : chigh;
                low2 = low2 < clow ? low2 : clow;
            }
            return high2 - low2;
        }

        public float height(float[] values, int len)
        {
            int pos = BarPos;
            float high = values[pos];
            float low = values[pos];
            for (int i = pos - len + 1; i < pos; i++)
            {
                float chigh = values[i];
                float clow = values[i];
                high = high > chigh ? high : chigh;
                low = low < clow ? low : clow;
            }
            return high - low;
        }

        public float ma(float[] values, int len)
        {
            float ma = 0;
            int startindex = BarPos - len + 1;
            int endIndex = BarPos;
            startindex = startindex < 0 ? 0 : startindex;
            for (int i = startindex; i < endIndex; i++)
                ma += values[i];
            return (float)Math.Round(ma / (endIndex - startindex), 3);
        }

        public float Max(float[] values, int len)
        {
            float max = 0;
            int startindex = BarPos - len + 1;
            int endIndex = BarPos;
            startindex = startindex < 0 ? 0 : startindex;
            for (int i = startindex; i < endIndex; i++)
            {
                if (max < values[i])
                    max = values[i];
            }
            return max;
        }

        public int MaxBars(float[] values, int len)
        {
            int index = -1;
            float max = 0;
            int startindex = BarPos - len + 1;
            int endIndex = BarPos;
            startindex = startindex < 0 ? 0 : startindex;
            for (int i = startindex; i <= endIndex; i++)
            {
                if (max < values[i])
                {
                    max = values[i];
                    index = i;
                }
            }
            return index;
        }

        public float Min(float[] values, int len)
        {
            float min = 0;
            int startindex = BarPos - len + 1;
            int endIndex = BarPos;
            startindex = startindex < 0 ? 0 : startindex;
            for (int i = startindex; i <= endIndex; i++)
            {
                if (min > values[i])
                    min = values[i];
            }
            return min;
        }

        public int MinBars(float[] values, int len)
        {
            int index = -1;
            float min = float.MaxValue;
            int startindex = BarPos - len + 1;
            int endIndex = BarPos;
            startindex = startindex < 0 ? 0 : startindex;
            for (int i = startindex; i <= endIndex; i++)
            {
                if (min > values[i])
                {
                    min = values[i];
                    index = i;
                }
            }
            return index;
        }

        public float Lowest(float[] values, int len)
        {
            return values[LowestBars(values, len)];
        }

        public float Highest(float[] values, int len)
        {
            return values[HighestBars(values, len)];
        }

        public int LowestBars(float[] values, int len)
        {
            float low = float.MaxValue;
            int lowIndex = 0;

            int startindex = BarPos - len + 1;
            int endIndex = BarPos;
            for (int i = startindex; i < endIndex; i++)
            {
                float value = values[i];
                if (value < low)
                {
                    lowIndex = i;
                    low = value;
                }
            }
            return lowIndex;
        }

        public int HighestBars(float[] values, int len)
        {
            float high = float.MinValue;
            int highIndex = 0;

            int startindex = BarPos - len + 1;
            int endIndex = BarPos;
            for (int i = startindex; i < endIndex; i++)
            {
                float value = values[i];
                if (value > high)
                {
                    highIndex = i;
                    high = value;
                }
            }
            return highIndex;
        }

        public float AveragePrice(float[] values, int len)
        {
            //TODO
            return 0;
        }

        /**
         * 数组1是否和数组2相交
         * @param values1
         * @param values2
         * @return 0未相交；1数组1向上穿过数组2；2数组1向下穿过数组2
         */
        public int Cross(float[] values1, float[] values2)
        {
            float value1 = values1[BarPos];
            float value1Pre = values1[BarPos - 1];

            float value2 = values2[BarPos];
            float value2Pre = values2[BarPos - 1];

            if (value1 > value2 && value1Pre < value2Pre)
                return 1;
            if (value1 < value2 && value1Pre > value2Pre)
                return -1;
            return 0;
        }
        #endregion

        #region 画图

        public List<PolyLineArray> polyLines = new List<PolyLineArray>();

        public void AddPolyLine(PolyLineArray polyLine)
        {
            polyLines.Add(polyLine);
        }

        public List<PolyLineList> polyLineList = new List<PolyLineList>();

        public void AddPolyLine(PolyLineList polyLine)
        {
            polyLineList.Add(polyLine);
        }

        public void ClearPolyLine()
        {
            polyLines.Clear();
            polyLineList.Clear();
        }

        public List<PointArray> points = new List<PointArray>();

        public void AddPoint(PointArray polyLine)
        {
            points.Add(polyLine);
        }

        public List<PointList> pointLists = new List<PointList>();

        public void AddPoint(PointList polyLine)
        {
            pointLists.Add(polyLine);
        }

        public void ClearPoints()
        {
            points.Clear();
            pointLists.Clear();
        }

        #endregion
    }
}
