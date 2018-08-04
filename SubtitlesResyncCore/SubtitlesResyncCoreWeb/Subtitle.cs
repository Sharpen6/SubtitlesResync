using System;
using System.Collections.Generic;
using System.Text;

namespace SubtitlesResyncCore
{
    public class Subtitle
    {
        public TimeRange AppreanceTime { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return AppreanceTime + "\r\n" + Text;
        }
    }
}
