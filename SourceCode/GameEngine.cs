using System;

namespace BalloonsPopsGame
{
    class GameEngine
    {
        private Board board = null;
        private readonly IRenderable console = null;

        private GameEngine()
        {
            this.console = new ConsoleRenderer();
            try 
            { 
                this.StartNewGame();
            }
            //TODO - check stuff that can happened here
            catch (Exception)
            {
                throw new Exception("MASSIVE TODO");
            }
        }
    
        public static GameEngine StartGame()
        {
            return new GameEngine();
        }

        private void StartNewGame()
        {
            this.board = new Board();

            this.PlayGame();
        }

        private void PlayGame()
        {
            //Use ScoreBoard here
            string[,] topFive = new string[5, 2];
            //And Board here

            this.console.Display(this.board.ToString());
            int numberOfMoves = 0;
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
                        this.StartNewGame();
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
                            numberOfMoves++;
                            bool isGameFinished = Board.IsEmpty(board);
                            if (isGameFinished)
                            {
                                Console.WriteLine("Congtratulations! You completed it in {0} moves.", numberOfMoves);
                                if (ScoreBoard.SignIfSkilled(topFive, numberOfMoves))
                                {
                                    ScoreBoard.SortAndPrintChartFive(topFive);
                                }
                                else 
                                {
                                    Console.WriteLine("I am sorry you are not skillful enough for TopFive chart!");
                                }
                                board = new Board();
                                numberOfMoves = 0;
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

        void CheckNeighbors (int searchedItem)
        {

        }
        
        void CheckLeft(int row, int column, int searchedItem)
        {
            int newRow = row;
            int newColumn = column - 1;
            try
            {
                if (this.board.Field[newRow, newColumn].NumValue == searchedItem)
                {
                    this.board.Field[newRow, newColumn].NumValue = 0;
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
                if (this.board.Field[newRow, newColumn].NumValue == searchedItem)
                {
                    this.board.Field[newRow, newColumn].NumValue = 0;
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
                if (this.board.Field[newRow, newColumn].NumValue == searchedItem)
                {
                    this.board.Field[newRow, newColumn].NumValue = 0;
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
                if (this.board.Field[newRow, newColumn].NumValue == searchedItem)
                {
                    this.board.Field[newRow, newColumn].NumValue = 0;
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
    }
}