using System;
using System.Collections.Generic;

namespace BalloonsPopsGame
{
    class BalloonsPops
    {
        public const int GameBoardRows = 5;
        public const int GameBoardCols = 10;
        public const int StartColorRange = 1;
        public const int EndColorRange = 4;

        // Separate to two parts - CheckForGameEnd and PopBalloons methods
        static bool Doit(Board board)
        {
            bool isWinner = true;
            Stack<int> stek = new Stack<int>();
            int rowsLength = board.GameBoardRows;
            int colsLength = board.GameBoardCols;

            for (int row = 0; row < rowsLength; row++) 
            {
                for (int col = 0; col < colsLength; col++)
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
                        board.Field[k, row].NumValue = stek.Pop(); 
                    }
                    catch (Exception)
                    {
                        board.Field[k, row].NumValue = 0;
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
            string temp = null;
            int userMoves = 0;
            //Extract game loop ?
            while (temp != "EXIT")
            {
                Console.WriteLine("Enter a row and column: ");                
                temp = Console.ReadLine();
                temp = temp.ToUpper().Trim();
                 
                //Use contants 
                switch (temp) 
                {
                    case "RESTART":
                        board = new Board();
                        Console.WriteLine(board);
                        userMoves = 0;
                        break;
                    case "TOP":
                        ScoreBoard.SortAndPrintChartFive(topFive);
                        break;
                    default :
                        if ((temp.Length == 3) && (temp[0] >= '0' && temp[0] <= '9') && (temp[2] >= '0' && temp[2] <= '9') && (temp[1] == ' ' || temp[1] == '.' || temp[1] == ','))
                        {
                            int userRow, userColumn;
                            userRow = int.Parse(temp[0].ToString());
                            if (userRow > 4) 
                            {
                                Console.WriteLine("Wrong input ! Try Again ! ");
                                continue;
                            }
                            userColumn = int.Parse(temp[2].ToString());
                            
                            if (board.Change(userRow, userColumn))
                            {
                                Console.WriteLine("cannot pop missing ballon!");
                                continue;
                            }
                            userMoves++;
                            if (Doit(board))
                            {
                                Console.WriteLine("Gratz ! You completed it in {0} moves.", userMoves);
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
            Console.WriteLine("Good Bye! ");
        }
    }
}