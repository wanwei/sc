using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// 实时数据读取，能得到实时的K线、tick和分时线数据
    /// </summary>
    public interface IRealTimeDataReader
    {
        /// <summary>
        /// 得到指定周期的K线
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        IKLineData GetKLineData(KLinePeriod period);

        /// <summary>
        /// 得到当前的分时线
        /// </summary>
        /// <returns></returns>
        ITimeLineData GetTimeLineData();

        /// <summary>
        /// 得到今日的TICK数据
        /// </summary>
        /// <returns></returns>
        ITickData GetTickData();

        /// <summary>
        /// 得到当前时间
        /// </summary>
        double Time { get; }
    }
}