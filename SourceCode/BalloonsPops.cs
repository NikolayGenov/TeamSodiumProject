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
        static bool Doit(byte[,] matrix)
        {
            bool isWinner = true;
            Stack<byte> stek = new Stack<byte>();
            int columnLenght = matrix.GetLength(0);
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                for (int i = 0; i < columnLenght; i++)
                {
                    if (matrix[i, j] != 0)
                    {
                        isWinner = false;
                        stek.Push(matrix[i, j]);
                    }
                }

                for (int k = columnLenght - 1; (k >= 0); k--)
                {
                    try
                    {
                        matrix[k, j] = stek.Pop(); 
                    }
                    catch (Exception)
                    {
                        matrix[k, j] = 0;
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
            byte[,] matrix = Board.Generate(5, 10);

            Board.PrintMatrix(matrix);
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
                        matrix = Board.Generate(5, 10);
                        Board.PrintMatrix(matrix);
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
                            
                            if (Board.Change(matrix, userRow, userColumn))
                            {
                                Console.WriteLine("cannot pop missing ballon!");
                                continue;
                            }
                            userMoves++;
                            if (Doit(matrix))
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
                                matrix = Board.Generate(5, 10);
                                userMoves = 0;
                            }
                            Board.PrintMatrix(matrix);
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