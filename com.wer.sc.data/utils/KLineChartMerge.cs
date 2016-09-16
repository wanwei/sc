using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.utils
{
    public class KLineChartMerge
    {
        public static void Merge(KLineChart chart, IKLineChart chart2)
        {
            if (chart.High < chart2.High)
                chart.SetHigh(chart2.High);
            if (chart.Low < chart2.Low)
                chart.SetLow(chart2.Low);
            chart.SetEnd(chart2.End);
            chart.SetMount(chart.Mount + chart2.Mount);
            chart.SetMoney(chart.Money + chart2.Money);
            chart.SetHold(chart2.Hold);
        }
    }
}