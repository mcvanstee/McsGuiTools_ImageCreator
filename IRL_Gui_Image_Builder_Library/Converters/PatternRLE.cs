namespace IRL_Gui_Image_Builder_Library.Converters
{
    public class PatternRLE
    {
        public int StartIndex { get; private set; } = 0;
        public int EndIndex { get; private set; }
        public int PatternLength { get; private set; } = 0; // RLE pattern length
        public int PatternCount { get; private set; } = 0; // RLE pattern count
        public byte[] Pattern { get; private set; } = [];
        public byte[] PatternReversed { get; private set; } = [];
        public int Count { get; set; } = 0;
        public int ReversedCount { get; set; } = 0;
        

        public PatternRLE(byte[] pattern, int patternLength, int patternCount, int index)
        {
            Pattern = pattern;
            PatternLength = patternLength;
            PatternCount = patternCount;
            StartIndex = index;
            EndIndex = index + (patternCount * patternLength) - 1;
            // Create reversed pattern
            PatternReversed = new byte[pattern.Length];

            for (int i = 0; i < (patternCount * patternLength); i += patternLength)
            {
                PatternReversed[i] = pattern[Pattern.Length - 3 - i];
                PatternReversed[i + 1] = pattern[Pattern.Length - 2 - i];
                PatternReversed[i + 2] = pattern[Pattern.Length - 1 - i];
            }
        }
    }
}
