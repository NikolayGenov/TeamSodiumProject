using System;
using System.Collections.Generic;
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
        
        public bool IsInField(int rowPosition, int colPosition)
        {
            bool isRowPosInField = (rowPosition >= 0) && (rowPosition < this.GameBoardRows);
            bool isColPosInFeild = (colPosition >= 0) && (colPosition < this.GameBoardCols);
            if (isRowPosInField && isColPosInFeild)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public bool IsEmpty()
        {
            bool isEmpty = true;
            int rowsLength = this.GameBoardRows;
            int colsLength = this.GameBoardCols;
            for (int row = 0; (row < rowsLength) && isEmpty; row++)
            {
                for (int col = 0; col < colsLength; col++)
                {
                    if (this.Field[row, col].NumValue != 0)
                    {
                        isEmpty = false;
                        break;
                    }
                }
            }
            return isEmpty;
        }

        bool isEmpty = true;
         
        public void MoveObjectsDown()
        { 
            Stack<int> columnStack = new Stack<int>();
            int rowsLength = this.GameBoardRows;
            int colsLength = this.GameBoardCols;

            for (int colPos = 0; colPos < colsLength; colPos++)
            {
                for (int rowPos = 0; rowPos < rowsLength; rowPos++)
                {
                    if (this.Field[rowPos, colPos].NumValue != 0)
                    {
                        //Addes new value in the column stack 
                        columnStack.Push(this.Field[rowPos, colPos].NumValue);
                    }
                }

                //Calculate where the stack ends to replace the rest with zeroes
                int endOfStack = rowsLength - columnStack.Count;
                
                //Moves the values from the bottom of the column to the top
                for (int rowPos = rowsLength - 1; rowPos >= endOfStack; rowPos--)
                {
                    this.Field[rowPos, colPos].NumValue = columnStack.Pop();
                }
                //Replace the top with zeroes where needed
                for (int rowPos = endOfStack - 1; rowPos >= 0; rowPos--)
                {
                    this.Field[rowPos, colPos].NumValue = 0;
                }
            }
        }
    }
}