using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wintellect.PowerCollections;

namespace BalloonsPopsGame
{
    class ScoreBoard
    {
        private OrderedMultiDictionary<int, string> scoreBoard;

        private int playersToPrint;

        public ScoreBoard(int playersToPrint)
        {
            this.PlayersToPrint = playersToPrint;
            this.scoreBoard = new OrderedMultiDictionary<int, string>(true);
        }

        public int PlayersToPrint
        {
            get
            {
                return this.playersToPrint;
            }
            private set
            {
                this.playersToPrint = value;
            }
        }

        public override string ToString()
        {
            const int FormatStringNumberDashes = 34;
            StringBuilder outputChart = new StringBuilder(); 
            
            outputChart.AppendLine("---------TOP FIVE CHART-----------");
            if (this.scoreBoard.Count == 0)
            {
                outputChart.AppendLine("The ScoreBoard is empty!");
            }
            else
            {
                int position = 1;
                foreach (var user in this.scoreBoard)
                {
                    if (position == this.PlayersToPrint)
                    {
                        break;
                    }
                    string userName = user.Value.ToString();
                    int userScore = user.Key;
                    outputChart.AppendFormat("{0}. {1} with {2} moves", position, userName.ToString(), userScore).AppendLine();
                }
            }
            
            outputChart.AppendLine(new String('-', FormatStringNumberDashes));
           
            return outputChart.ToString();
        }

        public bool IsForTopFive(int currentPlayerMoves)
        {
            Console.WriteLine("Type in your name.");
            string playerName = Console.ReadLine();
            this.scoreBoard.Add(currentPlayerMoves, playerName);
            List<int> topFivePlayersMoves = new List<int>(this.scoreBoard.Keys);
            List<string> playersNames = new List<string>(this.scoreBoard.Values);

            int count = playersNames.Count; 
            //string lastPlayerName = playersNames[count - 1];

            //while (this.ScoreChart.Count > ScoreBoardSize)
            //{
            //    this.ScoreChart.Remove(lastPlayerName);
            //}

            return true;
        }
    }
}