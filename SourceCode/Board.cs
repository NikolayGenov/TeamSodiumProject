using System;
using System.Linq;
using System.Text;

namespace BalloonsPopsGame
{
    public class Board
    {
        public int GameBoardRows { get; private set; }

        public int GameBoardCols { get; private set; }

        public GameObject[,] Field { get ; set; }//Private set ?
        
        public Board(int gameBoardRows = BalloonsPops.GameBoardRows, int gameBoardCols = BalloonsPops.GameBoardCols,
            int startRange = BalloonsPops.StartColorRange, int endRange= BalloonsPops.EndColorRange)
        {
            this.GameBoardRows = gameBoardRows;
            this.GameBoardCols = gameBoardCols;
            this.Field = new GameObject[GameBoardRows, GameBoardCols];
            this.Generate(startRange, endRange);
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

        //Turn to ToString method
        public static void PrintMatrix(int[,] matrix)
        {
            Console.Write("    ");
            for (int column = 0; column < matrix.GetLongLength(1); column++)
            {
                Console.Write(column + " ");
            }

            Console.Write("\n   ");
            for (int column = 0; column < matrix.GetLongLength(1) * 2 + 1; column++)
            {
                Console.Write("-");
            }

            Console.WriteLine();         // trinket stuff for PrintMatrix() till here

            for (int i = 0; i < matrix.GetLongLength(0); i++)
            {
                Console.Write(i + " | ");
                for (int j = 0; j < matrix.GetLongLength(1); j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        Console.Write("  ");
                        continue;
                    }

                    Console.Write(matrix[i, j] + " ");
                }
                Console.Write("| ");
                Console.WriteLine();
            }

            Console.Write("   ");     //some trinket stuff again
            for (int column = 0; column < matrix.GetLongLength(1) * 2 + 1; column++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
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

         void CheckRight( int row, int column, int searchedItem)
        {
            int newRow = row;
            int newColumn = column + 1;
            try
            {
                if (this.Field[newRow, newColumn].NumValue == searchedItem)
                {
                    this.Field[newRow, newColumn].NumValue = 0;
                    CheckRight( newRow, newColumn, searchedItem);
                }
                else
                    return;
            }
            catch (IndexOutOfRangeException)
            {
                return;
            }
        }

         void CheckUp( int row, int column, int searchedItem)
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

         void CheckDown( int row, int column, int searchedItem)
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