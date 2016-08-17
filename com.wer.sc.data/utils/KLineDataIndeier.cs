using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.utils
{
    /// <summary>
    /// K线索引器
    /// </summary>
    public class KLineDataIndeier
    {
        /// <summary>
        /// k线数据里面包含的所有日期
        /// </summary>
        private List<int> dates = new List<int>();

        /// <summary>
        /// k线数据里所有日期开始位置对应节点
        /// </summary>
        private Dictionary<double, int> dicDateSplitter = new Dictionary<double, int>();

        private Dictionary<double, int> dic = new Dictionary<double, int>();

        private IKLineData klineData;

        public KLineDataIndeier(IKLineData data)
        {
            this.klineData = data;
            this.DoIndex();
        }

        public IKLineData KLineData
        {
            get
            {
                return klineData;
            }
        }

        private void DoIndex()
        {
            for (int i = 0; i < klineData.Length; i++)
            {
                double time = klineData.Arr_Time[i];
            }
        }

        public int GetTimeDate(double time)
        {

            return -1;
        }

        public int GetTimeDateIndex(double time)
        {
            return -1;
        }

        public int GetTimeIndex(double time)
        {
            //TODO 现在仅简单处理
            return GetTimeIndex(time, 0);
        }

        private int GetTimeIndex(double time, int startIndex)
        {
            while (startIndex < klineData.Length)
            {
                double t = klineData.Arr_Time[startIndex];
                if (time < t)
                    return startIndex - 1;
                if (time == t)
                    return startIndex;
                startIndex++;
            }

            return -1;
        }
    }
}
