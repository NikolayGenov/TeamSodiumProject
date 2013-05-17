// **********************************************************
// <copyright file="GameEngineUtils.cs" company="Telerik Academy">
// Copyright (c) 2013 Telerik Academy. All rights reserved.
// </copyright>
//
// **********************************************************

namespace BalloonsPopsGame.Common
{
    using System;
    using System.Text;
    
    /// <summary>
    /// Class that have utilities in help of the GameEngine class.
    /// </summary>
    public static class GameEngineUtils
    {
        /// <summary>
        /// Check if the given number are valid and if they are, output them back.
        /// Separate the input string and if the length is okay - try to parse it.
        /// </summary>
        public static bool AreValidNumbers(string userInput, out int rowNumber, out int colNumber)
        {
            bool areValid = true;
            string rowValue = string.Empty, colValue = string.Empty;
            char[] separators = { '.', ',', ' ' };
            string[] numberArray = userInput.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            if (numberArray.Length != 2)
            {
                areValid = false;
            }
            else
            {
                rowValue = numberArray[0];
                colValue = numberArray[1];
            }
            
            bool rowIsNumber = int.TryParse(rowValue, out rowNumber);
            bool colIsNumber = int.TryParse(colValue, out colNumber);
            if (rowIsNumber && colIsNumber)
            {
                areValid = true;
            }
            else
            {
                areValid = false;
            }

            return areValid;
        }
        
        /// <summary>
        /// Make the welcoming message as a string and returns it.
        /// </summary>
        public static string StartMessage()
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine("Welcome to \"Balloons Pops\" game. Please try to pop the balloons.");
            output.AppendLine("Use 'top' to view the top scoreboard");
            output.AppendLine("'restart' to start a new game and 'exit' to quit the game.");
            return output.ToString();
        }

        /// <summary>
        /// Check if the players name is valid.
        /// </summary>
        public static bool IsValidName(string playerName)
        {
            bool isValidName = true;
            if (string.IsNullOrWhiteSpace(playerName) || string.IsNullOrEmpty(playerName))
            {
                isValidName = false;
            }
            
            return isValidName;
        }
    }
}