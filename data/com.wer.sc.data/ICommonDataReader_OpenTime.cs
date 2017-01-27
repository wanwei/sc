using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// 开盘时间读取器，用来获取品种及其开盘时间的相关数据
    /// </summary>
    public interface ICommonDataReader_OpenTime
    {
        /// <summary>
        /// 得到当日的开盘收盘时间
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        DayOpenTime GetOpenTime(string code, int date);

        /// <summary>
        /// 得到时间对应的开盘日期，如果该时间不开盘，则返回-1
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        int GetOpenDate(string code, double time);

        /// <summary>
        /// 得到时间对应的开盘日期，如果该时间不开盘，则返回该时间之后最近的开盘日
        /// </summary>
        /// <param name="code"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        int GetRecentOpenDate(string code, double time);
    }
}
