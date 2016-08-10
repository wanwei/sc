namespace com.wer.sc.data
{
    public interface IKLineChart
    {
        string Code { get; }
        float Start { get; }
        float High { get; }
        float Low { get; }
        float End { get; }
        float BlockHeight { get; }

        float BlockHigh { get; }

        float BlockLow { get; }
        float BlockMiddle { get; }
        float BottomShadow { get; }
      
        float Height { get; }
     
        float Middle { get; }
        int Mount { get; }
        
        double Time { get; }
        float TopShadow { get; }

        bool isRed();
    }
}