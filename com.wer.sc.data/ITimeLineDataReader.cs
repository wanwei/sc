using System.Collections.Generic;
using com.wer.sc.data.store;

namespace com.wer.sc.data
{
    /// <summary>
    /// 分时线数据读取器
    /// </summary>
    public interface ITimeLineDataReader
    {
        ITimeLineData GetData(string code, int date);

        List<ITimeLineData> GetData(string code, int startDate, int endDate);
    }
}