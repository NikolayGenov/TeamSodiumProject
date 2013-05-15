// **********************************************************
// <copyright file="ScoreBoard.cs" company="Telerik Academy">
// Copyright (c) 2013 Telerik Academy. All rights reserved.
// </copyright>
//
// **********************************************************

namespace BalloonsPopsGame.Common
{
    using System;
    using System.Linq;
    using System.Text;
    using Wintellect.PowerCollections;

    public class ScoreBoard
    {
        private readonly OrderedMultiDictionary<int, string> scoreBoard;

        public ScoreBoard(int numberOfPlayersToShow)
        {
            this.NumberOfPlayersToShow = numberOfPlayersToShow;
            this.scoreBoard = new OrderedMultiDictionary<int, string>(true);
        }

        public string PlayerName { get; private set; }

        public int NumberOfPlayersToShow { get; private set; }
        
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
                    if (position == this.NumberOfPlayersToShow)
                    {
                        break;
                    }

                    string userName = user.Value.ToString();
                    int userScore = user.Key;
                    outputChart.AppendFormat("{0}. {1} with {2} moves", position, userName.ToString(), userScore).AppendLine();
                    position++;
                }
            }
            
            outputChart.AppendLine(new string('-', FormatStringNumberDashes));
           
            return outputChart.ToString();
        }
        
        internal bool IsTopPlayer(int numberOfMoves)
        {
            if (this.scoreBoard.Count <= 5)
            {
                return true;
            }
            
            int[] topPlayerMoves = this.scoreBoard.Keys.ToArray();
            if (topPlayerMoves[this.NumberOfPlayersToShow] <= numberOfMoves)
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