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
       

        static void Main(string[] args)
        {
            GameEngine.StartGame();
        }
    }
}