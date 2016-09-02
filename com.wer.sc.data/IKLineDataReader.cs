namespace com.wer.sc.data
{
    public interface IKLineDataReader
    {
        IKLineData GetData(string code, int startDate, int endDate, KLinePeriod period);

        int GetFirstDate(string code, KLinePeriod period);

        int GetLastDate(string code, KLinePeriod period);
    }
}