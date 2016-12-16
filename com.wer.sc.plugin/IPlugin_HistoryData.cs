using com.wer.sc.data;
using System;
using System.Collections.Generic;

namespace com.wer.sc.plugin
{
    /// <summary>
    /// 历史数据提供者插件
    /// 该插件的作用是给系统提供市场的历史数据，系统会将插件提供的数据更新到系统的数据中心。
    /// 这些数据可以用来做回归测试，也可以在实盘交易中根据历史数据进行分析。
    /// 
    /// 现在提供以下数据：
    /// 1.所有股票或期货的信息
    /// 2.所有交易的日期
    /// 3.一只股票的所有开盘日及每日的开始时间
    /// 4.Tick数据：对于期货市场，是1秒两次或四次的交易数据。股票数据基本是成交明细数据
    /// 5.K线数据
    /// 6.确认哪些数据需要更新
    /// </summary>
    public interface IPlugin_HistoryData
    {

        /// <summary>
        /// 得到插件的名称
        /// 对于历史数据插件，不同的插件名称不能相同
        /// </summary>
        /// <returns></returns>
        String GetName();

        /// <summary>
        /// 得到插件的描述信息
        /// </summary>
        /// <returns></returns>
        String GetDescription();

        /// <summary>
        /// 返回最终在数据中心中的保存路径
        /// </summary>
        /// <returns></returns>
        String GetDataPath();

        /// <summary>
        /// 该插件提供的所有股票或期货信息
        /// </summary>
        /// <returns></returns>
        List<CodeInfo> GetCodes();

        /// <summary>
        /// 得到所有开盘日
        /// </summary>
        /// <returns></returns>
        List<int> GetOpenDates();

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
        List<DayStartTime> GetDayStartTime(String code);

        /// <summary>
        /// 得到股票或期货的Tick数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        ITickData GetTickData(String code, int date);

        /// <summary>
        /// 得到股票或期货的K线数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="klinePeriod"></param>
        /// <returns></returns>
        IKLineData GetKLineData(String code, int startDate, int endDate, KLinePeriod klinePeriod);

        /// <summary>
        /// 得到哪些数据需要更新
        /// </summary>
        /// <returns></returns>
        NeedsToUpdate GetNeedsToUpdate();
    }    

    public class NeedsToUpdate
    {
        private bool isTickUpdate = false;

        private List<KLinePeriod> klinePeriods = new List<KLinePeriod>();

        public bool IsTickUpdate
        {
            get
            {
                return isTickUpdate;
            }

            set
            {
                isTickUpdate = value;
            }
        }

        public List<KLinePeriod> KlinePeriods
        {
            get
            {
                return klinePeriods;
            }
        }
    }
}