namespace com.wer.sc.data
{
    public interface IKLineChart
    {
        /// <summary>
        /// 得到当前
        /// </summary>
        string Code { get; }

        double Time { get; }

        float Start { get; }

        float High { get; }

        float Low { get; }

        float End { get; }

        int Mount { get; }

        float Money { get; }

        int Hold { get; }

        float BlockHeight { get; }

        float BlockHigh { get; }

        float BlockLow { get; }

        float BlockMiddle { get; }

        float BottomShadow { get; }

        float Height { get; }

        float Middle { get; }

        float TopShadow { get; }

        bool isRed();
    }
}