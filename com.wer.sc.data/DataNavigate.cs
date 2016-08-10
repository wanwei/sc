using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// 数据导航
    /// 可指定时间并获取该时间的各种时间周期的K线数据
    /// TODO 获取分时线数据
    /// </summary>
    public class DataNavigate
    {
        private String code;

        private int blockMount;

        private DataReaderFactory dataReaderFac;

        private KLineData data;

        private float currentTime;

        private KLinePeriod period;

        private int startIndex = 200;

        private int endIndex = 300;

        private TickData todayTickData;

        private KLineData todayMinuteData;

        private KLineChart currentChart = new KLineChart();

        private int startDate;

        private int endDate;
    }
}
