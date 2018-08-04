using System;
using System.Collections.Generic;
using System.Text;

namespace SubtitlesResyncCore
{
    public class TimeRange
    {
        public TimeRange(string input)
        {
            if (input.Contains("-->"))
            {
                string[] split = { "-->" };
                string[] parts = input.Split(split, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2)
                {
                    Begin = new Time(parts[0]);
                    End = new Time(parts[1]);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        internal void ApplyChange(int timeShiftDiff, int timeSpeedDiff)
        {
            Begin.ApplyChange(timeShiftDiff, timeSpeedDiff);
            End.ApplyChange(timeShiftDiff, timeSpeedDiff);
        }

        public TimeRange(Time input1, Time input2)
        {
            Begin = input1;
            End = input2;
        }

        public override string ToString()
        {
            return Begin.ToString() + " --> " + End.ToString();
        }

        public static TimeRange Parse(string line)
        {
            TimeRange tr = new TimeRange(line);
            return tr; 
        }

        internal static bool IsInFormat(string line)
        {
            if (line.Contains("-->"))
            {
                string[] splitters = { "-->" };
                string[] parts = line.Split(splitters, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2)
                    return false;

                if (Time.IsInFormat(parts[0]) && Time.IsInFormat(parts[1]))
                    return true;
                else
                    return false;
            }
            return false;
        }

        public Time Begin { get; set; }
        public Time End { get; set; }
    }
}
