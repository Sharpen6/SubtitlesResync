using System;
using System.Collections.Generic;
using System.Text;

namespace SubtitlesResyncCore
{
    public class Time
    {
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
        public int Millisecond { get; set; }
        public int TotalMilliseconds
        {
            get
            {
                int total = 0;
                total += Millisecond;
                total += Second * 1000;
                total += Minute * 60000;
                total += Hour * 3600000;
                return total;
            }
        }

        public Time(int hour, int minute, int second, int millisecond)
        {
            Hour = hour;
            Minute = minute;
            Second = second;
            Millisecond = millisecond;
        }
        public Time(int milliseconds)
        {
            UpdateValuesByMilliseconds(milliseconds);
        }
    
        public Time()
        {

        }
        public Time(string sTime)
        {
            sTime = sTime.Trim();
            string[] parts = sTime.Split(':');
            if (parts.Length == 3)
            {
                if (parts[2].Contains(","))
                {
                    int hour;
                    int minute;
                    int second;
                    int millisecond;

                    if (int.TryParse(parts[0], out hour) &&
                        int.TryParse(parts[1], out minute)&&
                        int.TryParse(parts[2].Split(',')[0], out second) &&
                        int.TryParse(parts[2].Split(',')[1], out millisecond))
                    {
                        Hour = hour;
                        Minute = minute;
                        Second = second;
                        Millisecond = millisecond;
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
            else
            {
                throw new NotImplementedException();
            }
        }


        internal void ApplyChange(int timeShiftDiff, int timeSpeedDiff)
        {
            int TotalMillisecondsShifted = (TotalMilliseconds * timeSpeedDiff) + timeShiftDiff;
            UpdateValuesByMilliseconds(TotalMillisecondsShifted);
        }
        private void UpdateValuesByMilliseconds(int milliseconds)
        {
            var t = TimeSpan.FromMilliseconds(milliseconds);
            Millisecond = t.Milliseconds;
            Second = t.Seconds;
            Minute = t.Minutes;
            Hour = t.Hours;
        }
        public static bool IsInFormat(string sTime)
        {
            sTime = sTime.Trim();
            string[] parts = sTime.Split(':');
            if (parts.Length == 3)
            {
                if (parts[2].Contains(","))
                {
                    return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            string overall = "";
            overall += Hour.ToString().PadLeft(2, '0') + ':';
            overall += Minute.ToString().PadLeft(2, '0') + ':';
            overall += Second.ToString().PadLeft(2, '0') + ',';
            overall += Millisecond.ToString().PadLeft(3, '0');
            return overall;
        }

        public override bool Equals(object obj)
        {
            DateTime dt1 = ConvertToDateTime((Time)obj);
            DateTime dt2 = ConvertToDateTime(this);
            return dt1.Equals(dt2);
        }

        public static Time DurationBetween(Time t1, Time t2)
        {
            DateTime dt1 = ConvertToDateTime(t1);
            DateTime dt2 = ConvertToDateTime(t2);
            TimeSpan ts = (dt1 - dt2).Duration();
            return ConvertToTime(ts);
        }

        private static DateTime ConvertToDateTime(Time t)
        {
            DateTime dt = new DateTime(1, 1, 1, t.Hour, t.Minute, t.Second, t.Millisecond);
            return dt;
        }

        private static Time ConvertToTime(TimeSpan dt)
        {
            Time t = new Time(dt.Hours, dt.Minutes, dt.Seconds, dt.Milliseconds);
            return t;
        }

    }
}
