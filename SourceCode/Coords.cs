using System;

namespace BalloonsPopsGame
{
    public class Coords
    {
        private int row;
        private int col;

        public Coords(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }

        public int Row
        {
            get
            {
                return this.row;
            }
            set
            {
                if (0 > value && value > BalloonsPops.GameBoardRows)
                {
                    throw new ArgumentOutOfRangeException("The value for rows that you have entered is out of the board range");
                }
                this.row = value;
            }
        }

        public int Col
        {
            get
            {
                return this.col;
            }
            set
            {
                if (0 > value && value > BalloonsPops.GameBoardCols)
                {
                    throw new ArgumentOutOfRangeException("The value for cols that you have entered is out of the board range");
                }
                this.col = value;
            }
        }
        //TODO Add ++ and --
    }
}