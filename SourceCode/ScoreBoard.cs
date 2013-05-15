using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wintellect.PowerCollections;
namespace BalloonsPopsGame
{
    class ScoreBoard
    {
        public const int ScoreBoardSize = 3;

        private OrderedMultiDictionary<string, int> scoreChart;

        //Create props and Constructor with good getters and setters + exceptions 
        public ScoreBoard()
        {
            this.scoreChart = new OrderedMultiDictionary<string, int>(true);
        }

        public OrderedMultiDictionary<string, int> ScoreChart
        {
            get
            {
                return this.scoreChart;
            }
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append("---------TOP FIVE CHART-----------");

            for (int i = 0; i < this.ScoreChart.Count; i++)
            {
                List<string> playerNames = new List<string>(this.ScoreChart.Keys);
                List<int> playerMoves = new List<int>(this.ScoreChart.Values);

                result.AppendLine();
                result.AppendFormat("{0}. {1} with {2} moves", i + 1, playerNames[i], playerMoves[i]);
            }

            result.AppendLine();
            result.Append("---------------------------------");

            return result.ToString();
        }

        public bool IsForTopFive(int currentPlayerMoves)
        {
            Console.WriteLine("Type in your name.");
            string playerName = Console.ReadLine();
            this.ScoreChart.Add(playerName, currentPlayerMoves);
            List<int> topFivePlayersMoves = new List<int>(this.scoreChart.Values);
            List<string> playersNames = new List<string>(this.ScoreChart.Keys);

            int count = playersNames.Count; 
            string lastPlayerName = playersNames[count-1];

            while (this.ScoreChart.Count > ScoreBoardSize)
            {
                this.ScoreChart.Remove(lastPlayerName);
            }

            return true;
        }
    }   
}