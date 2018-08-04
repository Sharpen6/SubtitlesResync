using System;
using System.Collections.Generic;
using System.Text;

namespace SubtitlesResyncCore
{
    class Time
    {
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
        public int Millisecond { get; set; }

        public override string ToString()
        {
            string overall = "";
            overall += Hour.ToString().PadLeft(2, '0') + ':';
            overall += Minute.ToString().PadLeft(2, '0') + ':';
            overall += Second.ToString().PadLeft(2, '0') + ':';
            overall += Millisecond.ToString().PadLeft(3, '0');
            return overall;
        }
    }
}
