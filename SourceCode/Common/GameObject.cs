// *********************************************
// <copyright file="GameObject.cs" company="Telerik Academy">
// Copyright (c) 2013 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************************************
 
namespace BalloonsPopsGame.Common
{
    using System;
    
    /// <summary>
    /// Saves all the information about one game object - row position, column position and the numeric value that it has.
    /// </summary>
    internal class GameObject
    {
        private int rowPosition;
        private int colPosition;

        internal GameObject(int numValue, int rowPosition, int colPosition)
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

        internal int NumValue { get; set; }

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