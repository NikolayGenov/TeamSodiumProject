using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalloonsPopsGame
{
    public class Board
    {
        private int gameBoardCols;

        private int gameBoardRows;

        public GameObject[,] Field { get ; set; }//Private set ?
        
        public Board(int gameBoardRows = BalloonsPops.GameBoardRows, int gameBoardCols = BalloonsPops.GameBoardCols,
            int startRange = BalloonsPops.StartColorRange, int endRange= BalloonsPops.EndColorRange)
        {
            this.GameBoardRows = gameBoardRows;
            this.GameBoardCols = gameBoardCols;
            this.Field = new GameObject[GameBoardRows, GameBoardCols];
            this.Generate(startRange, endRange);
        }
        
        public int GameBoardRows
        {
            get
            {
                return this.gameBoardRows;
            }
            set
            {
                if (0 > value && value > BalloonsPops.GameBoardRows)
                {
                    throw new ArgumentException("The given value for game rows is invalid");
                }
                this.gameBoardRows = value;
            }
        }

        public int GameBoardCols
        {
            get
            {
                return this.gameBoardCols;
            }
            set
            {
                if (0 > value && value > BalloonsPops.GameBoardCols)
                {
                    throw new ArgumentException("The given value for game cols is invalid");
                }
                this.gameBoardCols = value;
            }
        }

        private void Generate(int startRange, int endRange)
        {
            for (int row = 0; row < GameBoardRows; row++)
            {
                for (int column = 0; column < GameBoardCols; column++)
                {
                    int randomNumber = RandomUtils.GenerateRandomNumber(startRange, endRange);
                    this.Field[row, column] = new GameObject(randomNumber, new Coords(row, column));
                }
            }
        }

        public override string ToString()
        {
            int fieldRows = this.Field.GetLength(0);
            int fieldCols = this.Field.GetLength(1);
            StringBuilder resultStr = new StringBuilder();
            string borderFormat = "{0,24}"; // Upper and lower border format string.

            // For printing the number of columns above the upper border.
            StringBuilder numberOfCols = new StringBuilder();
            for (int column = 0; column < fieldCols; column++)
            {
                numberOfCols.AppendFormat("{0} ", column);
            }

            resultStr.AppendFormat(borderFormat, numberOfCols.ToString());

            // For printing the upper border.
            resultStr.AppendLine();
            StringBuilder upperBorder = new StringBuilder();
            int upperBorderLength = (fieldCols * 2) + 1;
            for (int column = 0; column < upperBorderLength; column++)
            {
                upperBorder.Append("-");
            }

            resultStr.AppendFormat(borderFormat, upperBorder.ToString());
            
            // For printing the inner part of the matrix field.
            resultStr.AppendLine();
            for (int i = 0; i < fieldRows; i++)
            {
                resultStr.AppendFormat("{0} | ", i);
                for (int j = 0; j < fieldCols; j++)
                {
                    resultStr.AppendFormat("{0} ", this.Field[i, j]);
                }

                resultStr.AppendLine("| ");
            }

            // For printing the lower border.
            StringBuilder lowerBorder = new StringBuilder();
            int lowerBorderLength = (fieldCols * 2) + 1;
            for (int column = 0; column < lowerBorderLength; column++)
            {
                lowerBorder.Append("-");
            }

            resultStr.AppendFormat(borderFormat, lowerBorder.ToString());
            resultStr.AppendLine();

            return resultStr.ToString();
        }

        //Try to combine all the methods into one
        void CheckLeft(int row, int column, int searchedItem)
        {
            int newRow = row;
            int newColumn = column - 1;
            try
            {
                if (this.Field[newRow, newColumn].NumValue == searchedItem)
                {
                    this.Field[newRow, newColumn].NumValue = 0;
                    CheckLeft(newRow, newColumn, searchedItem);
                }
                else
                    return;
            }
            catch (IndexOutOfRangeException)
            {
                return;
            }
        }

        void CheckRight(int row, int column, int searchedItem)
        {
            int newRow = row;
            int newColumn = column + 1;
            try
            {
                if (this.Field[newRow, newColumn].NumValue == searchedItem)
                {
                    this.Field[newRow, newColumn].NumValue = 0;
                    CheckRight(newRow, newColumn, searchedItem);
                }
                else
                    return;
            }
            catch (IndexOutOfRangeException)
            {
                return;
            }
        }

        void CheckUp(int row, int column, int searchedItem)
        {
            int newRow = row + 1;
            int newColumn = column;
            try
            {
                if (this.Field[newRow, newColumn].NumValue == searchedItem)
                {
                    this.Field[newRow, newColumn].NumValue = 0;
                    CheckUp(newRow, newColumn, searchedItem);
                }
                else
                    return;
            }
            catch (IndexOutOfRangeException)
            {
                return;
            }
        }

        void CheckDown(int row, int column, int searchedItem)
        {
            int newRow = row - 1;
            int newColumn = column;
            try
            {
                if (this.Field[newRow, newColumn].NumValue == searchedItem)
                {
                    this.Field[newRow, newColumn].NumValue = 0;
                    CheckDown(newRow, newColumn, searchedItem);
                }
                else
                    return;
            }
            catch (IndexOutOfRangeException)
            {
                return;
            }
        }

        public bool Change(int rowAtm, int columnAtm)
        {
            if (this.Field[rowAtm, columnAtm].NumValue == 0)
            {
                return true;
            }
            int searchedTarget = this.Field[rowAtm, columnAtm].NumValue;
            this.Field[rowAtm, columnAtm].NumValue = 0;
            CheckLeft(rowAtm, columnAtm, searchedTarget);
            CheckRight(rowAtm, columnAtm, searchedTarget);

            CheckUp(rowAtm, columnAtm, searchedTarget);
            CheckDown(rowAtm, columnAtm, searchedTarget);
            return false;
        }

        public static bool IsEmpty(Board board)
        {
            bool isWinner = true;
            Stack<int> stek = new Stack<int>();
            int rowsLength = board.GameBoardRows;
            int colsLength = board.GameBoardCols;

            for (int col = 0; col < colsLength; col++)
            {
                for (int row = 0; row < rowsLength; row++)
                {
                    if (board.Field[row, col].NumValue != 0)
                    {
                        isWinner = false;
                        stek.Push(board.Field[row, col].NumValue);
                    }
                }
                //TODO То invert that here and change K to something else
                for (int k = rowsLength - 1; k >= 0; k--)
                {
                    try
                    {
                        board.Field[k, col].NumValue = stek.Pop();
                    }
                    catch (Exception)
                    {
                        board.Field[k, col].NumValue = 0;
                    }
                }
            }
            return isWinner;
        }
    }
}