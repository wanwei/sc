﻿using com.wer.sc.data;
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
        String Code { get; }

        /// <summary>
        /// 设置或获取数据周期
        /// </summary>
        KLinePeriod Period
        {
            get;
        }

        /// <summary>
        /// 获得当前数据
        /// </summary>
        /// <returns></returns>
        KLineData GetKLineData();

        /// <summary>
        /// 得到当前的Charts
        /// </summary>
        /// <returns></returns>
        IKLineChart GetCurrentChart();

        int StartIndex { get; }

        int EndIndex { get; set; }

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

        void ChangeData(KLineData klineData);

        void ChangeData(String code, int startDate, int endDate, KLinePeriod period);
        
        //event DataChangeHandler DataChange;
    }

    public delegate void DataChangeHandler(object sender, DataChangeArgs e);

    public class DataChangeArgs
    {

    }
}