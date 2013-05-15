using System;
using System.Collections.Generic;
using System.Text;

namespace BalloonsPopsGame
{
    public class Board
    {
        private int gameBoardRows;
        private int gameBoardCols;

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
                    int randomNumber = RandomUtils.GenerateRandomNumber(1, 2);
                    this.Field[row, column] = new GameObject(randomNumber, new Coords(row, column));
                }
            }
        }

        private string GetColumnIndecesAsString()
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < gameBoardCols; i++)
            {
                result.AppendFormat(" {0}", i);
            }

            return result.ToString();
        }

        private string GetHorizontalBorderAsString()
        {
            StringBuilder result = new StringBuilder();
            int borderLength = (this.gameBoardCols * 2) + 1;
            for (int column = 0; column < borderLength; column++)
            {
                result.Append("-");
            }

            return result.ToString();
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            string offset = "   "; // x3 white spaces.

            // For printing the number of columns above the upper border.
            string colIndeces = GetColumnIndecesAsString();           
            result.AppendFormat(offset + colIndeces);

            // For printing the upper border.
            string upperBorder = GetHorizontalBorderAsString();
            result.AppendLine();
            result.Append(offset + upperBorder);
            
            // For printing the inner part of the matrix field and the row indeces.
            result.AppendLine();
            for (int i = 0; i < this.gameBoardRows; i++)
            {
                result.AppendFormat("{0} | ", i);
                for (int j = 0; j < this.gameBoardCols; j++)
                {
                    result.AppendFormat("{0} ", this.Field[i, j]);
                }

                result.AppendLine("| ");
            }

            // For printing the lower border.
            string lowerBorder = GetHorizontalBorderAsString();
            result.Append(offset + lowerBorder);
            result.AppendLine();

            return result.ToString();
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