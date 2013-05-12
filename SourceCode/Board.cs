using System;
using System.Linq;

namespace BalloonsPopsGame
{
    public class Board
    {
        public const int GameBoardRows = 5 ;
        public const int GameBoardCols = 10;

        //Create props and Constructor with good getters and setters + exceptions 
        public static byte[,] Generate(byte rows, byte columns)
        {
            byte[,] temp = new byte[rows, columns];
            Random randNumber = new Random();
            for (byte row = 0; row < rows; row++)
            {
                for (byte column = 0; column < columns; column++)
                {
                    byte tempByte = (byte)randNumber.Next(1, 5);
                    temp[row, column] = tempByte;
                }
            }
            return temp;
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