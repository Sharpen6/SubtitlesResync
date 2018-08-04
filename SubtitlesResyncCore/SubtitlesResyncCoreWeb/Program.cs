using SubtitlesResyncCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitlesResyncCoreWeb
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
                Console.WriteLine("arguments: forign_language_but_synced_subs, wanted_languaged_but_de-synced_subs, output_wanted_language_and_synced");

            string[] text = File.ReadAllLines(args[0]);

            string[] text2 = File.ReadAllLines(args[1]);

            SubtitlesFile sf1 = new SubtitlesFile(text);
            SubtitlesFile sf2 = new SubtitlesFile(text2);

            SubsAligner pa = new SubsAligner(sf1, sf2);

            int currScore = pa.GetScore();
            int first_score = currScore;
            int prev_score = currScore;
            while (true)
            {
                pa.ApplyChange(100, 1);
                currScore = pa.GetScore();
                if (currScore > prev_score)
                {
                    pa.ApplyChange(-100, 1);
                    break;
                }
                else
                {
                    prev_score = currScore;
                }
            }

            int final_score = pa.GetScore();
            Console.WriteLine("Error: " + final_score);
            string outputPath = args[2];
            File.WriteAllText(outputPath, pa.Sub2.ToString());
        }
    }
}
