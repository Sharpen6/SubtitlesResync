using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitlesResyncCore
{
    public class SubsAligner
    {
        public SubtitlesFile Sub1 { get; set; }
        public SubtitlesFile Sub2 { get; set; }

        public SubsAligner(SubtitlesFile s1, SubtitlesFile s2)
        {
            Sub1 = s1;
            Sub2 = s2;
        }

        public void ApplyChange(int timeShiftDiff, int timeSpeedDiff)
        {
            foreach (var sub in Sub2.Subtitles)
            {
                sub.AppreanceTime.ApplyChange(timeShiftDiff, timeSpeedDiff);
            }
        }

        public int GetScore()
        {
            Dictionary<Subtitle, Subtitle> links = new Dictionary<Subtitle, Subtitle>();
            Dictionary<Subtitle, Subtitle> confirmed = new Dictionary<Subtitle, Subtitle>();

            foreach (var sub1 in Sub1.Subtitles)
            {
                Subtitle best = Sub2.Subtitles.First();
                int bestMinError = int.MaxValue;
                int startInd = 0;
                for (int i = startInd; i < Sub2.Subtitles.Count; i++)
                {
                    Subtitle sub2 = Sub2.Subtitles[i];
               
                    int timeDiff = Time.DurationBetween(sub1.AppreanceTime.Begin, sub2.AppreanceTime.Begin).TotalMilliseconds;
                    if (timeDiff < bestMinError)
                    {
                        bestMinError = timeDiff;
                        best = sub2;
                    } else
                    {
                        break;
                    }
                }
                links.Add(sub1, best);
            }

            int error_score = 0;

            foreach (var sub2 in Sub2.Subtitles)
            {
                Subtitle best = Sub2.Subtitles.First();
                int bestMinError = int.MaxValue;
                int startInd = 0;
                for (int i = startInd; i < Sub1.Subtitles.Count; i++)
                {
                    Subtitle sub1 = Sub1.Subtitles[i];

                    int timeDiff = Time.DurationBetween(sub1.AppreanceTime.Begin, sub2.AppreanceTime.Begin).TotalMilliseconds;
                    if (timeDiff < bestMinError)
                    {
                        bestMinError = timeDiff;
                        best = sub1;
                    }
                    else
                    {
                        break;
                    }
                }

                if (links.ContainsKey(best) && links[best] == sub2)
                {
                    confirmed.Add(best, sub2);
                    error_score += Time.DurationBetween(best.AppreanceTime.Begin, sub2.AppreanceTime.Begin).TotalMilliseconds;
                }
            }
            return error_score;
        }
    }
}
