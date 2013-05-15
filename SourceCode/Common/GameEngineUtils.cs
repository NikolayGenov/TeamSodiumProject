using System;
using System.Text;

namespace BalloonsPopsGame.Common
{
    public static class GameEngineUtils
    {
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

        public static string StartMessage()
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine("Welcome to “Balloons Pops” game. Please try to pop the balloons.");
            output.AppendLine("Use 'top' to view the top scoreboard");
            output.AppendLine("'restart' to start a new game and 'exit' to quit the game.");
            return output.ToString();
        }

        public static bool IsValidNme(string playerName)
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