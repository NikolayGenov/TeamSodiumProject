using System;
using System.Collections.Generic;
using BalloonsPopsGame.UI;

namespace BalloonsPopsGame.Common
{
    public class GameEngine
    {
        internal const int GameBoardRows = 5;
        internal const int GameBoardCols = 10;
        internal const int StartColorRange = 1;
        internal const int EndColorRange = 5;
        internal const int ScoreBoardSize = 5;
       
        private Board board = null;
        private readonly IRenderable console = null;
        private readonly ScoreBoard scoreBoard = new ScoreBoard(ScoreBoardSize);
        private string userInput = string.Empty;
        private int numberOfMoves = 0;

        private GameEngine()
        {
            this.console = new ConsoleRenderer();
            try
            { 
                this.StartNewGame();
            }
            //TODO - check stuff that can happened here
            catch (Exception)
            {
                throw new Exception("MASSIVE TODO");
            }
        }

        public static GameEngine StartGame()
        {
            return new GameEngine();
        }
     
        private void StartNewGame()
        {
            this.board = new Board();

            this.numberOfMoves = 0;
            this.BeginGame();
        }

        private void BeginGame()
        {
            bool hasEnded = false;
            this.DisplayInitialInfo();
            while (!hasEnded)
            {
                this.userInput = GetUserInput(this.userInput);
                
                switch (this.userInput)
                {
                    case "RESTART":
                        this.StartNewGame();
                        break;
                    case "TOP":
                        this.console.Display(scoreBoard.ToString());
                        break;
                    case "EXIT":
                        hasEnded = true;
                        break;
                    default:
                        {
                            int rowPosition = 0, colPosition = 0;
                            bool areValidCoordinates = AreValidCoordinates(this.userInput, out rowPosition, out colPosition);
                            if (areValidCoordinates)
                            {
                                PlayGame(rowPosition, colPosition);
                            }
                            else
                            {
                                this.console.Display("Wrong input! Try Again!");
                            }
                            break;
                        }
                }
            }
        }

        private void DisplayInitialInfo()
        {
            this.console.Display(GameEngineUtils.StartMessage());
            this.console.Display(this.board.ToString());
        }
  
        private string GetUserInput(string userInput)
        {
            this.console.Display("Enter a row and column: ");
            userInput = this.console.Read().ToUpper().Trim();
            return userInput;
        }
        
        private void PlayGame(int rowPosition, int colPosition)
        {
            bool canPopObjects = this.CanPopObjects(rowPosition, colPosition);
            if (canPopObjects)
            {
                this.PopObjects(rowPosition, colPosition);
                this.MoveObjectsDown();
                this.numberOfMoves++;
            }
            else
            {
                this.console.Display("Cannot pop missing ballon!");
            }

            bool isGameFinished = this.board.IsEmpty();
            if (isGameFinished)
            {
                this.ProcessPlayerByResult(numberOfMoves);
                this.StartNewGame();
            }
            this.console.Display(board.ToString());
        }
        
        private bool AreValidCoordinates(string userInput, out int rowPosition, out int colPosition)
        {
            bool areValidNumbers = GameEngineUtils.AreValidNumbers(userInput, out rowPosition, out colPosition);
            bool isInFeild = this.board.IsInField(rowPosition, colPosition);
            if (areValidNumbers && isInFeild)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
  
        private void ProcessPlayerByResult(int numberOfMoves)
        {
            this.console.Display(string.Format("Congtratulations! You completed it in {0} moves.", numberOfMoves));

            if (this.scoreBoard.IsTopPlayer(numberOfMoves))
            {
                this.AddPlayerToScoreBoard(numberOfMoves);
                this.console.Display(scoreBoard.ToString());
            }
            else 
            {
                this.console.Display("I am sorry you are not skillful enough for TopFive chart!");
            }
        }
  
        private void AddPlayerToScoreBoard(int numberOfMoves)
        {
            console.Display("Type in your name.");
            string playerName = console.Read();
            this.scoreBoard.AddPlayer(playerName, numberOfMoves);
        }
       
        private bool CanPopObjects(int rowPosition, int colPosition)
        {
            if (this.board.Field[rowPosition, colPosition].NumValue == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void PopObjects(int rowPosition, int colPosition)
        {
            int searchedValue = this.board.Field[rowPosition, colPosition].NumValue;
            this.PopEqualNeighborObjects(rowPosition, colPosition, searchedValue);
            this.board.Field[rowPosition, colPosition].NumValue = 0;
        }

        private void PopEqualNeighborObjects(int rowPosition, int colPosition, int searchedValue)
        { 
            bool isInField = this.board.IsInField(rowPosition, colPosition);
            if (!isInField)
            {
                return;
            }
            
            int elementsValue = this.board.Field[rowPosition, colPosition].NumValue;

            if (elementsValue == searchedValue)
            {
                this.board.Field[rowPosition, colPosition].NumValue = 0;
            }
            else
            {
                return;
            }

            this.PopEqualNeighborObjects(rowPosition, colPosition - 1, elementsValue); //Left
            this.PopEqualNeighborObjects(rowPosition - 1, colPosition, elementsValue); //Up
            this.PopEqualNeighborObjects(rowPosition, colPosition + 1, elementsValue); //Right
            this.PopEqualNeighborObjects(rowPosition + 1, colPosition, elementsValue); //Down
        }

        private void MoveObjectsDown()
        {
            Stack<int> columnStack = new Stack<int>();
            int rowsLength = this.board.GameBoardRows;
            int colsLength = this.board.GameBoardCols;

            for (int colPos = 0; colPos < colsLength; colPos++)
            {
                for (int rowPos = 0; rowPos < rowsLength; rowPos++)
                {
                    if (this.board.Field[rowPos, colPos].NumValue != 0)
                    {
                        //Addes new value in the column stack 
                        columnStack.Push(this.board.Field[rowPos, colPos].NumValue);
                    }
                }

                //Calculate where the stack ends to replace the rest with zeroes
                int endOfStack = rowsLength - columnStack.Count;

                //Moves the values from the bottom of the column to the top
                for (int rowPos = rowsLength - 1; rowPos >= endOfStack; rowPos--)
                {
                    this.board.Field[rowPos, colPos].NumValue = columnStack.Pop();
                }
                //Replace the top with zeroes where needed
                for (int rowPos = endOfStack - 1; rowPos >= 0; rowPos--)
                {
                    this.board.Field[rowPos, colPos].NumValue = 0;
                }
            }
        }
    }
}