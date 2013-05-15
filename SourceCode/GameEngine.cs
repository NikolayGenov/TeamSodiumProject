using System;

namespace BalloonsPopsGame
{
    class GameEngine
    {
        private Board board = null;
        private readonly IRenderable console = null;
        private ScoreBoard topFive = new ScoreBoard();

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
            //string[,] topFive = new string[5, 2];
            //ScoreBoard topFive = new ScoreBoard();

            //And Board here

            this.console.Display(this.board.ToString());
            int numberOfMoves = 0;
            //Extract game loop ?
            while (true)
            {
                Console.WriteLine("Enter a row and column: ");                
                string inputString = Console.ReadLine();
                inputString = inputString.ToUpper().Trim();
                // TODO GET USER INPUT FUNCUTION
                 
                //Use contants 
                switch (inputString) 
                {
                    case "RESTART":
                        this.StartNewGame();
                        break;

                    case "TOP":
                        Console.WriteLine(topFive);
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
                            bool canPopObjects = CanPopObjects(userRow, userColumn);
                            if (canPopObjects)
                            {
                                this.PopObjects(userRow, userColumn);
                                this.board.MoveObjectsDown();
                            }
                            else
                            {
                                Console.WriteLine("Cannot pop missing ballon!");
                                continue;
                            }
                            numberOfMoves++;
                            
                            bool isGameFinished = this.board.IsEmpty();
                            if (isGameFinished)
                            {
                                Console.WriteLine("Congtratulations! You completed it in {0} moves.", numberOfMoves);
                                if (topFive.IsForTopFive(numberOfMoves))
                                {
                                    Console.WriteLine(topFive);
                                }
                                else 
                                {
                                    Console.WriteLine("I am sorry you are not skillful enough for TopFive chart!");
                                }

                                this.StartNewGame();
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

        private bool CanPopObjects(int rowPosition, int colPosition)
        {
            if (this.board.Field[rowPosition, colPosition].NumValue == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void PopObjects(int rowPosition, int colPosition)
        {
            int searchedValue = this.board.Field[rowPosition, colPosition].NumValue;
            PopEqualNeighborObjects(rowPosition, colPosition, searchedValue);
            this.board.Field[rowPosition, colPosition].NumValue = 0;
        }

        private void PopEqualNeighborObjects(int rowPosition, int colPosition, int searchedValue)
        { 
            bool isInField = this.board.IsInField(rowPosition, colPosition);
            if (!isInField)
            {
                return;
            }

            int elementsValue = this.board.Field[rowPosition, colPosition].NumValue;

            if (elementsValue == searchedValue)
            {
                this.board.Field[rowPosition, colPosition].NumValue = 0;
            }
            else
            {
                return;
            }

            PopEqualNeighborObjects(rowPosition, colPosition - 1, elementsValue); //Left
            PopEqualNeighborObjects(rowPosition - 1, colPosition, elementsValue); //Up
            PopEqualNeighborObjects(rowPosition, colPosition + 1, elementsValue); //Right
            PopEqualNeighborObjects(rowPosition + 1, colPosition, elementsValue); //Down
        }
    }
}