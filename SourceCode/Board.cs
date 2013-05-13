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
        public static void PrintMatrix(byte[,] matrix)
        {
            Console.Write("    ");
            for (byte column = 0; column < matrix.GetLongLength(1); column++)
            {
                Console.Write(column + " ");
            }

            Console.Write("\n   ");
            for (byte column = 0; column < matrix.GetLongLength(1) * 2 + 1; column++)
            {
                Console.Write("-");
            }

            Console.WriteLine();         // trinket stuff for PrintMatrix() till here

            for (byte i = 0; i < matrix.GetLongLength(0); i++)
            {
                Console.Write(i + " | ");
                for (byte j = 0; j < matrix.GetLongLength(1); j++)
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
            for (byte column = 0; column < matrix.GetLongLength(1) * 2 + 1; column++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }

        //Try to combine all the methods into one
        static void CheckLeft(byte[,] matrix, int row, int column, int searchedItem)
        {
            int newRow = row;
            int newColumn = column - 1;
            try
            {
                if (matrix[newRow, newColumn] == searchedItem)
                {
                    matrix[newRow, newColumn] = 0;
                    CheckLeft(matrix, newRow, newColumn, searchedItem);
                }
                else
                    return;
            }
            catch (IndexOutOfRangeException)
            {
                return;
            }
        }

        static void CheckRight(byte[,] matrix, int row, int column, int searchedItem)
        {
            int newRow = row;
            int newColumn = column + 1;
            try
            {
                if (matrix[newRow, newColumn] == searchedItem)
                {
                    matrix[newRow, newColumn] = 0;
                    CheckRight(matrix, newRow, newColumn, searchedItem);
                }
                else
                    return;
            }
            catch (IndexOutOfRangeException)
            {
                return;
            }
        }

        static void CheckUp(byte[,] matrix, int row, int column, int searchedItem)
        {
            int newRow = row + 1;
            int newColumn = column;
            try
            {
                if (matrix[newRow, newColumn] == searchedItem)
                {
                    matrix[newRow, newColumn] = 0;
                    CheckUp(matrix, newRow, newColumn, searchedItem);
                }
                else
                    return;
            }
            catch (IndexOutOfRangeException)
            {
                return;
            }
        }

        static void CheckDown(byte[,] matrix, int row, int column, int searchedItem)
        {
            int newRow = row - 1;
            int newColumn = column;
            try
            {
                if (matrix[newRow, newColumn] == searchedItem)
                {
                    matrix[newRow, newColumn] = 0;
                    CheckDown(matrix, newRow, newColumn, searchedItem);
                }
                else
                    return;
            }
            catch (IndexOutOfRangeException)
            {
                return;
            }
        }

        public static bool Change(byte[,] matrixToModify, int rowAtm, int columnAtm)
        {
            if (matrixToModify[rowAtm, columnAtm] == 0)
            {
                return true;
            }
            byte searchedTarget = matrixToModify[rowAtm, columnAtm];
            matrixToModify[rowAtm, columnAtm] = 0;
            CheckLeft(matrixToModify, rowAtm, columnAtm, searchedTarget);
            CheckRight(matrixToModify, rowAtm, columnAtm, searchedTarget);

            CheckUp(matrixToModify, rowAtm, columnAtm, searchedTarget);
            CheckDown(matrixToModify, rowAtm, columnAtm, searchedTarget);
            return false;
        }
    }
}