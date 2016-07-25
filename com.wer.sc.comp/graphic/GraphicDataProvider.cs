using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic
{
    /// <summary>
    /// 图像数据提供
    /// </summary>
    public interface GraphicDataProvider
    {
        /// <summary>
        /// 获得当前数据
        /// </summary>
        /// <returns></returns>
        KLineData GetKLineData();

        /// <summary>
        /// 得到当前的Charts
        /// </summary>
        /// <returns></returns>
        KLineChart GetCurrentChart();

        int StartIndex
        {
            get;
        }

        int EndIndex
        {
            get;
            set;
        }


        String Code
        {
            get;
        }

        /// <summary>
        /// 设置或获取数据周期
        /// </summary>
        KLinePeriod Period
        {
            get;
        }

        /// <summary>
        /// 设置或获取当前时间
        /// </summary>
        float CurrentTime
        {
            get;
            set;
        }

        /// <summary>
        /// 设置或获取K线数量
        /// </summary>
        int BlockMount
        {
            get;
            set;
        }

        // 将创建的委托和特定事件关联,在这里特定的事件为KeyDown
        //event DataChangeHandler DataChange;
    }

    public delegate void DataChangeHandler(object sender, DataChangeArgs e);

    public class DataChangeArgs
    {

    }
}