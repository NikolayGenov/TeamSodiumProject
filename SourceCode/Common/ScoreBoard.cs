using System;
using System.Linq;
using System.Text;
using Wintellect.PowerCollections;

namespace BalloonsPopsGame.Common
{
    internal class ScoreBoard
    {
        public const int GameBoardRows = 5;
        public const int GameBoardCols = 10;
        public const int StartColorRange = 1;
        public const int EndColorRange = 5;
        private readonly OrderedMultiDictionary<int, string> scoreBoard;

        private int playersToPrint;

        private string playerName;

        public ScoreBoard(int playersToPrint)
        {
            this.PlayersToPrint = playersToPrint;
            this.scoreBoard = new OrderedMultiDictionary<int, string>(true);
        }

        public string PlayerName
        {
            get
            {
                return this.playerName;
            }
            set
            {
                //TODO
                this.playerName = value;
            }
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

        internal bool IsTopPlayer(int numberOfMoves)
        {
            if (this.scoreBoard.Count <= 5)
            {
                return true;
            }
            int[] topPlayerMoves = this.scoreBoard.Keys.ToArray();
            if (topPlayerMoves[PlayersToPrint] <= numberOfMoves)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        internal void AddPlayer(string playerName, int numberOfMoves)
        {
            this.PlayerName = playerName;
            this.scoreBoard.Add(numberOfMoves, playerName);
        }
    }
}