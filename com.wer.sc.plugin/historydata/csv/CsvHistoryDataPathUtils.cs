using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.historydata.csv
{
    /// <summary>
    /// 提供历史数据的插件的数据保存路径
    /// 
    /// 提供给SC系统的历史数据可以以CSV保存在指定目录，通过插件系统会自动将这些数据更新成SC识别的格式
    /// 
    /// 数据目录：
    ///     --opendates.csv
    ///     --codes.csv
    ///     --opentime.config
    ///     --m01
    ///         --tick
    ///             --M01_20040102.csv
    ///             --M01_20040105.csv
    ///             --......
    ///         --kline
    ///             --1minute
    ///                 --m01_1minute_20040102.csv
    ///                 --m01_1minute_20040105.csv
    ///             --......
    ///         --m01_daystarttime.csv
    ///     --m03
    ///     --......
    /// </summary>
    public class CsvHistoryDataPathUtils
    {
        /// <summary>
        /// 得到保存所有股票或期货数据的路径
        /// </summary>
        /// <returns></returns>
        public static string GetCodesPath(String pluginSrcDataPath)
        {
            return pluginSrcDataPath + "codes.csv";
        }

        public static string GetOpenDatesPath(String pluginSrcDataPath)
        {
            return pluginSrcDataPath + "opendates.csv";
        }

        public static String GetDayOpenTimePath(String pluginSrcDataPath, String code)
        {
            return pluginSrcDataPath + "\\" + code + "\\" + code + "_dayopentime" + ".csv";
        }

        public static String GetTickDataPath(String pluginSrcDataPath, String code, int date)
        {
            return pluginSrcDataPath + "\\" + code + "\\tick" + "\\" + code + "_" + date + ".csv";
        }

        public static String GetKLineDataPath(String pluginSrcDataPath, String code, int date, KLinePeriod period)
        {
            return pluginSrcDataPath + "\\" + code + "\\kline\\" + period.ToEngString() + "\\" + code + "_" + period.ToEngString() + "_" + date + ".csv";
        }
    }
}
