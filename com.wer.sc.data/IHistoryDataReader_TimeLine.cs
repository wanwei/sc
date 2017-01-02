using System.Collections.Generic;
using com.wer.sc.data.store;

namespace com.wer.sc.data
{
    /// <summary>
    /// 分时线历史数据读取器
    /// </summary>
    public interface IHistoryDataReader_TimeLine
    {
        /// <summary>
        /// 读取一天的分时线数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        ITimeLineData GetData(string code, int date);

        /// <summary>
        /// 读取一段时间的分时线数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        List<ITimeLineData> GetData(string code, int startDate, int endDate);
    }
}