using System;
using System.Collections.Generic;
using System.Linq;

namespace BalloonsPopsGame
{
    class ScoreBoard
    {
        public const int ScoreBoardSize = 5;

        public static void SortAndPrintChartFive(string[,] tableToSort)
        {
            List<Score> klasirane = new List<Score>();

            for (int i = 0; i < 5; ++i)
            {
                if (tableToSort[i, 0] == null)
                {
                    break;
                }

                klasirane.Add(new Score(int.Parse(tableToSort[i, 0]), tableToSort[i, 1]));
            }

            klasirane.Sort();
            Console.WriteLine("---------TOP FIVE CHART-----------");
            for (int i = 0; i < klasirane.Count; ++i)
            {
                Score slot = klasirane[i];
                Console.WriteLine("{2}.   {0} with {1} moves.", slot.Name, slot.Value, i + 1);
            }
            Console.WriteLine("----------------------------------");
        }
    }
}