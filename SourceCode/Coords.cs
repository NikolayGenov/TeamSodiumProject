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
                    throw new ArgumentException("Invalid number of rows for the coordinates");
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
                    throw new ArgumentException("Invalid number of cols for the coordinates");
                }
                this.col = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                //TODO Exceptions
                return false;
            }

            Coords coordinates = obj as Coords;
            if (coordinates == null)
            {
                return false;
            }

            if (this.Row != coordinates.Row ||
                this.Col != coordinates.Col)
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return (this.Row + this.Col).GetHashCode();
        }

        /* For removal - - LEAVE FOR NOW
        * 
        //public static Coords operator +(Coords coords1, Coords coords2)
        //{
        //    int sumOfRows = coords1.Row + coords2.Row;
        //    int sumOfCols = coords1.Col + coords2.Col;

        //    return new Coords(sumOfRows, sumOfCols);
        //}

        //public static Coords operator -(Coords coords1, Coords coords2)
        //{
        //    int differenceOfRows = Math.Abs(coords1.Row - coords2.Row);
        //    int differenceOfCols = Math.Abs(coords1.Col - coords2.Col);

        //    return new Coords(differenceOfRows, differenceOfCols);
        //}

        public static Coords operator ++(Coords coords)
        {
        return new Coords(coords.Row + 1, coords.Col + 1);
        }

        public static Coords operator --(Coords coords)
        {
        return new Coords(coords.Row - 1, coords.Col - 1);
        }
        * 
        */

        public static bool operator ==(Coords coords1, Coords coords2)
        {
            return Coords.Equals(coords1, coords2);
        }

        public static bool operator !=(Coords coords1, Coords coords2)
        {
            return !(Coords.Equals(coords1, coords2));
        }
    }
}