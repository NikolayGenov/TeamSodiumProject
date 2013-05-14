using System;
using System.Collections.Generic;

namespace BalloonsPopsGame
{
    class BalloonsPops
    {
        public const int GameBoardRows = 5;
        public const int GameBoardCols = 10;
        public const int StartColorRange = 1;
        public const int EndColorRange = 5;


        /*
         * 
         *BUG fixed
         * 
         * WHEN enter coordinates to generate part of the matrix
         * and it doesn't destro the stuff the right way
         * 
         *Note to self - check if can replace everytime printing the NumValue ???
         */
        // Separate to two parts - CheckForGameEnd and PopBalloons methods
        static bool Doit(Board board)
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

        static void Main(string[] args)
        {
            //Use ScoreBoard here
            string[,] topFive = new string[5, 2];
            //And Board here
            Board board = new Board(); //Repalce new matrix

            Console.WriteLine(board); //Print the board - repalce PRINT MATRIX -  Board.PrintMatrix(matrix);
            int userMoves = 0;
            //Extract game loop ?
            while (true)
            {
                Console.WriteLine("Enter a row and column: ");                
                string inputString = Console.ReadLine();
                inputString = inputString.ToUpper().Trim();
                 
                //Use contants 
                switch (inputString) 
                {
                    case "RESTART":
                        board = new Board();
                        Console.WriteLine(board);
                        userMoves = 0;
                        break;

                    case "TOP":
                        ScoreBoard.SortAndPrintChartFive(topFive);
                        break;

                    case "EXIT":
                        Console.WriteLine("Good Bye!");
                        return;

                    default :
                        if ((inputString.Length == 3) && (inputString[0] >= '0' && inputString[0] <= '9') && (inputString[2] >= '0' && inputString[2] <= '9') && (inputString[1] == ' ' || inputString[1] == '.' || inputString[1] == ','))
                        {
                            int userRow, userColumn;
                            userRow = int.Parse(inputString[0].ToString());
                            if (userRow > 4) 
                            {
                                Console.WriteLine("Wrong input! Try Again!");
                                continue;
                            }
                            userColumn = int.Parse(inputString[2].ToString());
                            
                            if (board.Change(userRow, userColumn))
                            {
                                Console.WriteLine("Cannot pop missing ballon!");
                                continue;
                            }
                            userMoves++;
                            if (Doit(board))
                            {
                                Console.WriteLine("Congtratulations! You completed it in {0} moves.", userMoves);
                                if (ScoreBoard.SignIfSkilled(topFive, userMoves))
                                {
                                    ScoreBoard.SortAndPrintChartFive(topFive);
                                }
                                else 
                                {
                                    Console.WriteLine("I am sorry you are not skillful enough for TopFive chart!");
                                }
                                board = new Board();
                                userMoves = 0;
                            }
                            Console.WriteLine(board);
                            break;
                        }
                        else
                        { 
                            Console.WriteLine("Wrong input ! Try Again ! ");
                            break;
                        }
                }
            }
        }
    }
}