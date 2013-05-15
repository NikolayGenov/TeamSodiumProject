using System;

namespace BalloonsPopsGame
{
    public class GameObject
    {
        private int rowPosition;

        private int colPosition;

        internal int NumValue { get; set; }

        public GameObject(int numValue, int rowPosition, int colPosition)
        {
            this.NumValue = numValue;
            this.RowPosition = rowPosition;
            this.ColPosition = colPosition;
        }

        public int RowPosition
        {
            get
            {
                return this.rowPosition;
            }
            set
            {
                if (0 > value && value > GameEngine.GameBoardRows)
                {
                    throw new ArgumentException("Invalid number of rows for the coordinates");
                }
                this.rowPosition = value;
            }
        }

        public int ColPosition
        {
            get
            {
                return this.colPosition;
            }
            set
            {
                if (0 > value && value > GameEngine.GameBoardCols)
                {
                    throw new ArgumentException("Invalid number of cols for the coordinates");
                }
                this.colPosition = value;
            }
        }

        public override string ToString()
        {
            if (this.NumValue == 0)
            {
                return " ";
            }

            return this.NumValue.ToString();
        }
    }
}