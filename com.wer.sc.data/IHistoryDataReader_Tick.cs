using System.Collections.Generic;

namespace com.wer.sc.data
{
    /// <summary>
    /// TICK历史数据读取器
    /// </summary>
    public interface IHistoryDataReader_Tick
    {
        /// <summary>
        /// 得到某日的tick数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        TickData GetTickData(string code, int date);

        /// <summary>
        /// 得到某只股票或期货的所有tick日期
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        List<int> GetTickDates(string code);
    }
}