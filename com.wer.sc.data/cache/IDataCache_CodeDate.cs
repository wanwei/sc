namespace com.wer.sc.data.cache
{
    public interface IDataCache_CodeDate
    {
        string Code { get; }

        int Date { get; }

        IKLineData GetMinuteKLineData();

        IRealData GetRealData();

        ITickData GetTickData();
    }
}