using System;
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

        // TODO: Refactor this raw version of the method to something better.
        public override string ToString()
        {
            StringBuilder fieldString = new StringBuilder();

            fieldString.Append("    ");
            for (int column = 0; column < this.Field.GetLength(1); column++)
            {
                fieldString.Append(column + " ");
            }

            fieldString.Append("\n   ");
            for (int column = 0; column < this.Field.GetLength(1) * 2 + 1; column++)
            {
                fieldString.Append("-");
            }
            
            fieldString.AppendLine();

            for (int i = 0; i < this.Field.GetLength(0); i++)
            {
                fieldString.Append(i + " | ");
                for (int j = 0; j < this.Field.GetLength(1); j++)
                {
                    fieldString.Append(this.Field[i, j] + " ");
                }

                fieldString.AppendLine("| ");
                //Console.WriteLine();
            }

            fieldString.Append("   ");
            for (int column = 0; column < this.Field.GetLength(1) * 2 + 1; column++)
            {
                fieldString.Append("-");
            }

            fieldString.AppendLine();

            return fieldString.ToString();
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
    }
}