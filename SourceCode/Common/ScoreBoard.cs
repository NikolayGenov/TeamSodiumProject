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

    /// <summary>
    /// Creates a score board which represents a list of ordered by their result player with their names.
    /// </summary>
    public class ScoreBoard
    {
        /// <summary>
        /// It is used because of it's properties to be ordered and to have multiple entries for one key value.
        /// Holds the player name as a string and players result as a integer key.
        /// </summary>
        private readonly OrderedMultiDictionary<int, string> scoreBoard;

        /// <summary>
        /// Holds the value for the players Name.
        /// </summary>
        private string playerName;

        /// <summary>
        /// Holds the number of players we want to show in the top list of players string.
        /// </summary>
        private int numberOfPlayersToShow;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScoreBoard"/> class with a given top number of players to show in the top list and the dictionary
        /// to save the values.
        /// </summary>
        /// <param name="numberOfPlayersToShow">Given by parameter - To show that number of players when we ask for top players.</param>
        public ScoreBoard(int numberOfPlayersToShow)
        {
            this.NumberOfPlayersToShow = numberOfPlayersToShow;
            this.scoreBoard = new OrderedMultiDictionary<int, string>(true);
        }
        
        /// <summary>
        /// Gets the number of players we would like to show in the top list and check if that number is valid.
        /// </summary>
        public int NumberOfPlayersToShow
        {
            get
            {
                return this.numberOfPlayersToShow;
            }

            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Can't take as argument zero or less players to show");
                }
                
                this.numberOfPlayersToShow = value;
            }
        }

        /// <summary>
        /// Gets the player name and check if it's valid.
        /// </summary>
        public string PlayerName
        {
            get
            {
                return this.scoreBoard.Values.ToString();
            }
            
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Invalid players name");
                }

                this.playerName = value;
            }
        }

        /// <summary>
        /// Check if by given number of moves, the player can be added in the top list of players.
        /// </summary>
        public bool IsTopPlayer(int numberOfMoves)
        {
            if (this.scoreBoard.Count < this.NumberOfPlayersToShow)
            {
                return true;
            }

            int[] topPlayerMoves = this.scoreBoard.Keys.ToArray();
            if (topPlayerMoves[this.NumberOfPlayersToShow - 1] >= numberOfMoves)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Adds a player to the scoreBoard by given name and number of moves
        /// </summary>
        public void AddPlayer(string playerName, int numberOfMoves)
        {
            this.PlayerName = playerName;
            this.scoreBoard.Add(numberOfMoves, playerName);
        }

        /// <summary>
        /// Overrides the method and return as a string the top list of players depending on how much players there is in the list.
        /// For the same result - players are displayed in a list.
        /// Everything is formatted.
        /// </summary>
        public override string ToString()
        {
            const int FormatStringNumberDashes = 34;
            StringBuilder outputBoard = new StringBuilder(); 
            outputBoard.AppendLine("---------TOP FIVE CHART-----------");
            
            if (this.scoreBoard.Count == 0)
            {
                outputBoard.AppendLine("The scoreboard is empty.");
            }
            else
            {
                int position = 1;
                foreach (var user in this.scoreBoard)
                {
                    string userName = user.Value.ToString();
                    int userScore = user.Key;
                    outputBoard.AppendFormat("{0}. {1} with {2} moves", position, userName, userScore).AppendLine();
                   
                    if (position == this.NumberOfPlayersToShow)
                    {
                        break;
                    }

                    position++;
                }
            }
            
            outputBoard.AppendLine(new string('-', FormatStringNumberDashes));
            return outputBoard.ToString();
        }
    }
}