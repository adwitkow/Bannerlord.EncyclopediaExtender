namespace Bannerlord.EncyclopediaExtender
{
    public static class Constants
    {
        public const string WholeNumberFormat = "N0";
        public const string DecimalNumberFormat = "N1";

#if LOWER_THAN_1_4
        public const string VerticalTopToBottom = "VerticalBottomToTop";
        public const string VerticalBottomToTop = "VerticalTopToBottom";
#else
        public const string VerticalTopToBottom = "VerticalTopToBottom";
        public const string VerticalBottomToTop = "VerticalBottomToTop";
#endif
    }
}
