namespace com.wer.sc.data.cache
{
    /// <summary>
    /// CODE数据
    /// </summary>
    public interface IDataCache_Code
    {
        int StartDate { get; }

        int EndDate { get; }

        IOpenDateReader GetCodeOpenDateReader();

        int MaxCacheDateCount { get; set; }

        IDataCache_CodeDate GetCache_CodeDate(int date);
    }
}