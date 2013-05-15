// **********************************************************
// <copyright file="GameObject.cs" company="Telerik Academy">
// Copyright (c) 2013 Telerik Academy. All rights reserved.
// </copyright>
//
// **********************************************************
 
namespace BalloonsPopsGame.Common
{
    using System;
    
    /// <summary>
    /// Saves all the information about one game object - row position, column position and the numeric value that it has.
    /// </summary>
    public class GameObject
    {
        /// <summary>
        /// Keep the row position of the given GameObject.
        /// </summary>
        private int rowPosition;

        /// <summary>
        /// Keep the column position of the given GameObject.
        /// </summary>
        private int colPosition;

        /// <summary>
        /// Keep the numeric value of the given GameObject.
        /// </summary>
        private int numValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameObject"/> class with given coordinates - row and column and a numeric value.
        /// </summary>
        public GameObject(int numValue, int rowPosition, int colPosition)
        {
            this.NumValue = numValue;
            this.RowPosition = rowPosition;
            this.ColPosition = colPosition;
        }

        /// <summary>
        /// Gets or sets the row position and if it sets it - it first check if it's in range.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the column position and if it sets it - it first check if it's in range.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the numeric value and if it sets it - it first check if it's in range.
        /// </summary>
        public int NumValue
        {
            get
            {
                return this.numValue;
            }
            
            set
            {
                if (0 > value && value >= GameEngine.EndColorRange)
                {
                    throw new ArgumentException("Invalid numeric value is not valid");
                }
                
                this.numValue = value;
            }
        }

        /// <summary>
        /// Overrides the ToString and if the numeric value is zero - the object is missing - it outputs a whitespace.
        /// </summary>
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