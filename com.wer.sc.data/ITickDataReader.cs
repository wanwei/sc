using System.Collections.Generic;

namespace com.wer.sc.data
{
    public interface ITickDataReader
    {
        TickData GetTickData(string code, int date);

        List<int> GetTickDates(string code);
    }
}