// **********************************************************
// <copyright file="GameEngine.cs" company="Telerik Academy">
// Copyright (c) 2013 Telerik Academy. All rights reserved.
// </copyright>
//
// **********************************************************

namespace BalloonsPopsGame.Common
{
    using System;
    using System.Collections.Generic;
    using BalloonsPopsGame.UI;

    /// <summary>
    /// Class which simulates the game and it's logic with given abstraction.
    /// It can be created, started a new game and then with given user commands it can be played.
    /// </summary>
    public class GameEngine
    {
        /// <summary>
        /// Holds the number of board rows.
        /// </summary>        
        public const int GameBoardRows = 5;

        /// <summary>
        /// Holds the number of board columns.
        /// </summary>  
        public const int GameBoardCols = 10;

        /// <summary>
        /// Holds the start of range of randomly generated color object range.
        /// </summary>  
        public const int StartColorRange = 1;

        /// <summary>
        /// Holds the end of range of randomly generated color object range.
        /// </summary>  
        public const int EndColorRange = 5;

        /// <summary>
        /// Holds the size of the score board - how many players we want to see in top.
        /// </summary>
        public const int ScoreBoardSize = 5;

        /// <summary>
        /// Holds the instance of the Renderer interface.
        /// </summary>
        private readonly IRenderable console = null;

        /// <summary>
        /// Holds the instance of the class ScoreBoard with given score board size.
        /// </summary>
        private readonly ScoreBoard scoreBoard = new ScoreBoard(ScoreBoardSize);

        /// <summary>
        /// Holds the instance of the class Board.
        /// </summary>
        private Board board = null;

        /// <summary>
        /// Holds the user input string.
        /// </summary>
        private string userInput = string.Empty;

        /// <summary>
        /// Holds the number of moves that the player has made so far.
        /// </summary>
        private int numberOfMoves = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameEngine"/> class from being created and starts a new game on given Renderer
        /// </summary>
        private GameEngine(IRenderable console)
        {
            this.console = console;
            this.StartNewGame(isGameRunning: false);
        }

        /// <summary>
        /// The method where the game can be started by returning new GameEngine.
        /// </summary>
        public static GameEngine StartGame(IRenderable console, bool isGameRuning)
        {
            return new GameEngine(console);
        }

        /// <summary>
        /// Starts a new game by making a new board and reset the number of moves and calls the game loop method.
        /// </summary>
        private void StartNewGame(bool isGameRunning = false)
        {
            this.board = new Board(GameBoardRows, GameBoardCols, StartColorRange, EndColorRange);
            this.numberOfMoves = 0;
            if (!isGameRunning)
            {
                this.BeginGame();
            }
            else
            {
                this.DisplayInitialInfo();
            }
        }

        /// <summary>
        /// The main game loop method where the game begins and loops in the while loop depending on the user input.
        /// </summary>
        private void BeginGame()
        {
            bool hasEnded = false;
            this.DisplayInitialInfo();
            while (!hasEnded)
            {
                this.userInput = this.GetUserInput(this.userInput);

                switch (this.userInput)
                {
                    case "RESTART":
                        this.StartNewGame(isGameRunning: true);
                        break;
                    case "TOP":
                        this.console.Display(this.scoreBoard.ToString());
                        break;
                    case "EXIT":
                        hasEnded = true;
                        break;
                    default:
                        {
                            int rowPosition = 0, colPosition = 0;
                            bool areValidCoordinates = this.AreValidCoordinates(this.userInput, out rowPosition, out colPosition);
                            if (areValidCoordinates)
                            {
                                this.PlayGame(rowPosition, colPosition);
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

        /// <summary>
        /// Display the initial info that the user must see - instructions and the board itself.
        /// </summary>
        private void DisplayInitialInfo()
        {
            this.console.Display(GameEngineUtils.StartMessage());
            this.console.Display(this.board.ToString());
        }

        /// <summary>
        /// Get the user input as a trimmed uppercase string.
        /// </summary>
        private string GetUserInput(string userInput)
        {
            this.console.Display("Enter a row and column: ");
            userInput = this.console.Read().ToUpper().Trim();
            return userInput;
        }

        /// <summary>
        /// Check if the given coordinates from the user are valid and returns a boolean result for that.
        /// </summary>
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

        /// <summary>
        /// If the coordinates are valid it process them and pops objects if possible
        /// </summary>
        private void PlayGame(int rowPosition, int colPosition)
        {
            bool canPopObjects = this.CanPopObjects(rowPosition, colPosition);
            if (canPopObjects)
            {
                this.PopObjects(rowPosition, colPosition);
                this.MoveObjectsDown();
                this.numberOfMoves++;
                this.console.Display(this.board.ToString());
            }
            else
            {
                this.console.Display("Cannot pop missing ballon!");
            }

            this.ProcessIfEndOfGame();
        }

        /// <summary>
        /// Check if the object can be popped or it's already popped.
        /// </summary>
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

        /// <summary>
        /// If objects can be popped - it pops all of them - row and column as well.
        /// </summary>
        private void PopObjects(int rowPosition, int colPosition)
        {
            int searchedValue = this.board.Field[rowPosition, colPosition].NumValue;
            this.PopEqualNeighborObjects(rowPosition, colPosition, searchedValue);
        }

        /// <summary>
        /// Check to pop objects in all 4 directions.
        /// </summary>
        private void PopEqualNeighborObjects(int rowPosition, int colPosition, int searchedValue)
        {
            this.PopDirections(rowPosition, colPosition, searchedValue, 0, -1); ////Left 
            this.PopDirections(rowPosition, colPosition, searchedValue, -1, 0); ////Up
            this.PopDirections(rowPosition, colPosition, searchedValue, 0, +1); ////Right
            this.PopDirections(rowPosition, colPosition, searchedValue, 1, 0); ////Down
        }

        /// <summary>
        /// Pop all possible objects in one direction.
        /// </summary>
        private void PopDirections(int rowPosition, int colPosition, int searchedValue, int rowDirection, int colDirection)
        {
            bool isInField = true;
            int elementsValue = searchedValue;
            while ((elementsValue == searchedValue) && isInField)
            {
                this.board.Field[rowPosition, colPosition].NumValue = 0;
                rowPosition += rowDirection;
                colPosition += colDirection;
                isInField = this.board.IsInField(rowPosition, colPosition);
                if (isInField)
                {
                    elementsValue = this.board.Field[rowPosition, colPosition].NumValue;
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// When the objects are popped they must be moved down by "gravity" which is made possible with Stack - data structure.
        /// </summary>
        private void MoveObjectsDown()
        {
            Stack<int> columnStack = new Stack<int>();
            int rowsLength = this.board.BoardRows;
            int colsLength = this.board.BoardCols;

            for (int colPos = 0; colPos < colsLength; colPos++)
            {
                for (int rowPos = 0; rowPos < rowsLength; rowPos++)
                {
                    if (this.board.Field[rowPos, colPos].NumValue != 0)
                    {
                        ////Addes new value in the column stack 
                        columnStack.Push(this.board.Field[rowPos, colPos].NumValue);
                    }
                }

                ////Calculate where the stack ends to replace the rest with zeroes
                int endOfStack = rowsLength - columnStack.Count;

                ////Moves the values from the bottom of the column to the top
                for (int rowPos = rowsLength - 1; rowPos >= endOfStack; rowPos--)
                {
                    this.board.Field[rowPos, colPos].NumValue = columnStack.Pop();
                }
                ////Replace the top with zeroes where needed
                for (int rowPos = endOfStack - 1; rowPos >= 0; rowPos--)
                {
                    this.board.Field[rowPos, colPos].NumValue = 0;
                }
            }
        }

        /// <summary>
        /// Check if the game as ended and if it's true - it process the play by his result and starts a new game.
        /// </summary>
        private void ProcessIfEndOfGame()
        {
            bool isGameFinished = this.board.IsEmpty();
            if (isGameFinished)
            {
                this.ProcessPlayerByResult(this.numberOfMoves);
                this.StartNewGame(isGameRunning: true);
            }
        }

        /// <summary>
        /// Taking the number of moves for the player - it adds or not the player to the top board depending of the top players there.
        /// </summary>
        private void ProcessPlayerByResult(int numberOfMoves)
        {
            this.console.Display(string.Format("Congtratulations! You completed it in {0} moves.", numberOfMoves));

            if (this.scoreBoard.IsTopPlayer(numberOfMoves))
            {
                this.AddPlayerToScoreBoard(numberOfMoves);
                this.console.Display(this.scoreBoard.ToString());
            }
            else
            {
                this.console.Display("I am sorry you are not skillful enough for TopFive chart!");
            }
        }

        /// <summary>
        /// If the player is good enough to be added to the board - it alerts it for name and adds it.
        /// </summary>
        private void AddPlayerToScoreBoard(int numberOfMoves)
        {
            string playerName = string.Empty;
            bool isValidName = false;
            do
            {
                this.console.Display("Type in your name.");
                playerName = this.console.Read();
                isValidName = GameEngineUtils.IsValidName(playerName);
            }
            while (!isValidName);
            this.scoreBoard.AddPlayer(playerName, numberOfMoves);
        }
    }
}