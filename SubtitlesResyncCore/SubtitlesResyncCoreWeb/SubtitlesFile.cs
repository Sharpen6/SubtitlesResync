using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitlesResyncCore
{
    public class SubtitlesFile
    {
        public List<Subtitle> Subtitles { get; set; }

        public SubtitlesFile()
        {
            Subtitles = new List<Subtitle>();
        }

        public List<Time> TimeBetweenSubtitles()
        {
            List<Time> ans = new List<Time>();
            if (Subtitles.Count > 2)
            {
                for (int i = 0; i < Subtitles.Count - 1; i++)
                {
                    ans.Add(Time.DurationBetween(Subtitles[i].AppreanceTime.End,
                                                 Subtitles[i + 1].AppreanceTime.Begin));
                }
            }
            return ans;
        }  

        public SubtitlesFile(string[] lines) : this()
        {
            int lineID;
            TimeRange subTimeRange = null;
            string subText = "";

            Subtitle sub = null;
        
            foreach (var line in lines)
            {
                if (int.TryParse(line, out lineID))
                {
                    if (sub != null)
                    {
                        sub.Text = subText;
                        Subtitles.Add(sub);
                        subText = "";
                    }
                }
                else
                {
                    if (TimeRange.IsInFormat(line))
                    {
                        subTimeRange = TimeRange.Parse(line);
                        sub = new Subtitle();
                        sub.AppreanceTime = subTimeRange;
                    }
                    else
                    {
                        if (line != "")
                        {
                            subText += line + "\r\n";
                        }
                    }
                }
            }

            if (sub != null)
            {
                sub.Text = subText;
                Subtitles.Add(sub);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Subtitles.Count; i++)
            {
                sb.AppendLine((i + 1).ToString());
                sb.AppendLine(Subtitles[i].ToString());
            }
            return sb.ToString();
        }
    }
}
