namespace com.wer.sc.data
{
    public interface IRealData
    {
        int BarPos { get; set; }

        double FullTime { get; }

        int Date { get; }

        double Time { get; }

        int Hold { get; }

        int Mount { get; }

        float Price { get; }
        
        float UpPerncet { get; }

        float UpRange { get; }

        int Length { get; }
    }
}