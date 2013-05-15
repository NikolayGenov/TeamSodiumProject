using System;
using System.Text;

namespace BalloonsPopsGame.Common
{
    internal class Board
    {
        private int gameBoardRows;
        private int gameBoardCols;

        public GameObject[,] Field { get ; private set; }

        public Board(int gameBoardRows = GameEngine.GameBoardRows, int gameBoardCols = GameEngine.GameBoardCols,
            int startRange = GameEngine.StartColorRange, int endRange = GameEngine.EndColorRange)
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
                if (0 > value && value > GameEngine.GameBoardRows)
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
                if (0 > value && value > GameEngine.GameBoardCols)
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
                    this.Field[row, column] = new GameObject(randomNumber, row, column);
                }
            }
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
    }
}