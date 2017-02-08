using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.transfer
{
    /// <summary>
    /// K线时间时间获取接口
    /// 该接口用来得到指定日一只股票或期货的K线时间队列
    /// 比如20140105的M03，1分钟时间队列为
    /// 20140105090000,20140105090001,......
    /// </summary>
    public interface IKLineTimeListGetter
    {
        /// <summary>
        /// 得到指定id，日期和周期的K线事件队列
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        List<double> GetKLineTimes(string code, int date, KLinePeriod period);
    }
}
