using com.wer.sc.data;
using com.wer.sc.data.opentime;
using com.wer.sc.data.transfer;
using com.wer.sc.data.utils;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.historydata.csv
{
    /// <summary>
    /// 历史数据获取插件的抽象实现，这个实现的基础在于用户已经将已有数据以CSV格式保存到指定目录
    /// 
    /// 用户需要将数据保存成以下方式：
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
    ///     
    /// 注：默认周期超过1分钟的K线数据都用1分钟数据生成，秒级K线需要自己生成。
    /// </summary>
    public abstract class Plugin_HistoryData_CsvProvider : IPlugin_HistoryData
    {        
        public Plugin_HistoryData_CsvProvider()
        {
        }

        /// <summary>
        /// 该插件提供的所有股票或期货信息
        /// </summary>
        /// <returns></returns>
        public virtual List<CodeInfo> GetCodes()
        {
            return CsvUtils_Code.Load(CsvHistoryDataPathUtils.GetCodesPath(GetPluginSrcDataPath()));
        }

        /// <summary>
        /// 得到所有开盘日
        /// </summary>
        /// <returns></returns>
        public virtual List<int> GetOpenDates()
        {
            return CsvUtils_OpenDate.Load(CsvHistoryDataPathUtils.GetOpenDatesPath(GetPluginSrcDataPath()));
        }

        /// <summary>
        /// 得到所有开盘日的开盘时间
        /// 实现该方法的原因：
        /// 系统需要有一个方法来获取指定日期的K线，比如获取20130106的1分钟K线
        /// 由于所有1分钟K线是保存在一个文件里的，系统无法获取20130106开盘那根K线的起始位置。
        /// 所以此处需要获取开盘时间数据
        /// 
        /// 各个市场的开盘时间数据很混乱：
        /// 比如中国期货市场就有夜盘，而夜盘在交易时间上算是第二天，所以20160105可能在20160104就开盘了
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public virtual List<DayOpenTime> GetDayOpenTime(String code)
        {
            return CsvUtils_DayStartTime.Load(CsvHistoryDataPathUtils.GetDayOpenTimePath(GetPluginSrcDataPath(), code));
        }

        /// <summary>
        /// 得到股票或期货的Tick数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public virtual ITickData GetTickData(String code, int date)
        {
            return CsvUtils_TickData.Load(CsvHistoryDataPathUtils.GetTickDataPath(GetPluginSrcDataPath(), code, date));
        }

        /// <summary>
        /// 得到股票或期货的K线数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="klinePeriod"></param>
        /// <returns></returns>
        public virtual IKLineData GetKLineData(String code, int startDate, int endDate, KLinePeriod klinePeriod)
        {
            List<int> openDates = GetOpenDates();
            OpenDateCache cache = new OpenDateCache(openDates);
            IList<int> resultOpenDates = cache.GetOpenDates(startDate, endDate);

            //如果存在该周期的源数据直接生成，否则用1分钟K线生成
            if (Exist(code, resultOpenDates[0], klinePeriod))
                return GetKLineData(code, klinePeriod, resultOpenDates);

            IKLineData oneMinuteKLine = GetKLineData(code, KLinePeriod.KLinePeriod_1Minute, resultOpenDates);
            return DataTransfer_KLine2KLine.Transfer(oneMinuteKLine, klinePeriod, new DayStartTimeCache(GetDayOpenTime(code)));
        }

        private IKLineData GetKLineData(string code, KLinePeriod klinePeriod, IList<int> resultOpenDates)
        {
            List<IKLineData> klineDataList = new List<IKLineData>();
            for (int i = 0; i < resultOpenDates.Count; i++)
            {
                IKLineData klineData = GetKLineData(code, resultOpenDates[i], klinePeriod);
                if (klineData != null)
                    klineDataList.Add(klineData);
            }

            return KLineData.Merge(klineDataList);
        }

        /// <summary>
        /// 得到单日的K线数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public virtual IKLineData GetKLineData(string code, int date, KLinePeriod period)
        {
            string path = CsvHistoryDataPathUtils.GetKLineDataPath(GetPluginSrcDataPath(), code, date, period);
            return CsvUtils_KLineData.Load(path);
        }

        private bool Exist(string code, int date, KLinePeriod period)
        {
            string path = CsvHistoryDataPathUtils.GetKLineDataPath(GetPluginSrcDataPath(), code, date, period);
            return File.Exists(path);
        }

        public abstract string GetName();
        public abstract string GetDescription();
        public abstract string GetPluginSrcDataPath();
        public abstract string GetDataPath();
        public abstract NeedsToUpdate GetNeedsToUpdate();
    }
}
