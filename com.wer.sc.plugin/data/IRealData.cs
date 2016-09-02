using System.Collections.Generic;

namespace com.wer.sc.data
{
    public interface IRealData
    {
        string Code { get; }

        float YesterdayEnd { get; }

        int Date { get; }

        int BarPos { get; set; }

        double Time { get; }

        int Hold { get; }

        int Mount { get; }

        float Price { get; }

        float UpPercent { get; }

        float UpRange { get; }

        IRealChart GetCurrentChart();

        IRealChart GetCurrentChart(int index);

        #region 完整数据信息

        int Length { get; }

        IList<double> Arr_Time { get; }

        IList<float> Arr_Price { get; }

        IList<int> Arr_Mount { get; }

        IList<int> Arr_Hold { get; }

        IList<float> Arr_UpPercent { get; }

        IList<float> Arr_UpRange { get; }

        int IndexOfTime(double time);

        #endregion

        string PrintAll();
    }
}