using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.opentime
{
    /// <summary>
    /// 日开盘时间接口
    /// </summary>
    public interface IDayStartTimeReader
    {
        /// <summary>
        /// 得到开盘日的缓存接口
        /// </summary>
        /// <returns></returns>
        IOpenDateReader GetOpenDateCache();

        /// <summary>
        /// 得到所有开盘时间
        /// </summary>
        /// <returns></returns>
        List<double> GetAllStartTimes();

        /// <summary>
        /// 得到指定起止时间的开盘时间
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        List<double> GetStartTimes(int startDate, int endDate);

        /// <summary>
        /// 得到从startDate到最新开盘日的开盘时间
        /// </summary>
        /// <param name="startDate"></param>
        /// <returns></returns>
        List<double> GetStartTimes(int startDate);

        /// <summary>
        /// 根据时间得到开盘日
        /// </summary>
        /// <param name="startTime"></param>
        /// <returns></returns>
        int GetOpenDate(double time);

        /// <summary>
        /// 得到指定日开盘时间
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        double GetStartTime(int date);

        /// <summary>
        /// 验证该时间是否是当日的开盘时间
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        bool IsStartTime(double time);
    }
}