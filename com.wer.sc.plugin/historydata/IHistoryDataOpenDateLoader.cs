using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.historydata
{
    /// <summary>
    /// 当前数据的保存信息
    /// </summary>
    public interface IHistoryDataOpenDateLoader
    {
        /// <summary>
        /// 得到当前的所有已经更新的tick的开盘日
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        List<int> GetOpenDates_TickData(string code);

        /// <summary>
        /// 得到最新的tick的开盘日
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        int GetLastOpenDate_TickData(string code);

        /// <summary>
        /// 得到当前的所有已经更新的K线数据的开盘日
        /// </summary>
        /// <param name="code"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        List<int> GetOpenDates_KLineData(string code, KLinePeriod period);

        /// <summary>
        /// 得到最新更新的K线数据的开盘日
        /// </summary>
        /// <param name="code"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        int GetLastOpenDate_KLineData(string code, KLinePeriod period);
    }
}