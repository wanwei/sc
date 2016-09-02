using System.Collections.Generic;
using com.wer.sc.data.store;

namespace com.wer.sc.data
{
    public interface IRealDataReader
    {
        IRealData GetData(string code, int date);

        List<IRealData> GetData(string code, int startDate, int endDate);
    }
}