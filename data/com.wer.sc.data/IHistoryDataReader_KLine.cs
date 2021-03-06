﻿namespace com.wer.sc.data
{
    /// <summary>
    /// K线历史数据读取器
    /// </summary>
    public interface IHistoryDataReader_KLine
    {
        /// <summary>
        /// 得到某股票或期货的所有K线数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        IKLineData GetAllData(string code, KLinePeriod period);

        /// <summary>
        /// 得到某股票或期货的一段时间的K线数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        IKLineData GetData(string code, int startDate, int endDate, KLinePeriod period);

        /// <summary>
        /// 得到历史数据里的第一个日子
        /// </summary>
        /// <param name="code"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        int GetFirstDate(string code, KLinePeriod period);

        /// <summary>
        /// 得到历史数据里最后的一个日子
        /// </summary>
        /// <param name="code"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        int GetLastDate(string code, KLinePeriod period);
    }
}