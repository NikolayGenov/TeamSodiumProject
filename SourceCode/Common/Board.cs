// **********************************************************
// <copyright file="Board.cs" company="Telerik Academy">
// Copyright (c) 2013 Telerik Academy. All rights reserved.
// </copyright>
//
// **********************************************************
 
namespace BalloonsPopsGame.Common
{
    using System;
    using System.Text;

    /// <summary>
    /// Represents the gaming board which have given rows and columns and have Field with GameObjects.
    /// </summary>
    public class Board
    {
        /// <summary>
        /// The number of rows that the board have. 
        /// </summary>
        private int boardRows;

        /// <summary>
        /// The number of columns that the board have. 
        /// </summary>
        private int boardCols;

        /// <summary>
        /// Initializes a new instance of the <see cref="Board"/> class. Creates a new Field which is the main part of the logic and it randomly generates itself.
        /// </summary>
        /// <param name="boardRows">Number of rows that the board must have.</param>
        /// <param name="boardCols">Number of columns that the board must have.</param>
        /// <param name="startRange">The start range for generating random numbers.</param>
        /// <param name="endRange">The end range for generating random numbers.</param>
        public Board(int boardRows, int boardCols, int startRange, int endRange)
        {
            this.BoardRows = boardRows;
            this.BoardCols = boardCols;
            this.Field = new GameObject[this.BoardRows, this.BoardCols];
            this.Generate(startRange, endRange);
        }

        /// <summary>
        /// Gets the gaming field filled with GameObjects
        /// Made two dimensional array with private set
        /// </summary>
        public GameObject[,] Field { get; private set; }

        /// <summary>
        /// Gets or sets the number of rows.If sets first checks if the value is in a given range.
        /// </summary>
        public int BoardRows
        {
            get
            {
                return this.boardRows;
            }
            
            set
            {
                if (0 > value || value > GameEngine.GameBoardRows)
                {
                    throw new ArgumentException("The given value for game rows is invalid");
                }

                this.boardRows = value;
            }
        }

        /// <summary>
        ///  Gets or sets the number of columns.If sets first checks if the value is in a given range.
        /// </summary>
        public int BoardCols
        {
            get
            {
                return this.boardCols;
            }

            set
            {
                if (0 > value || value > GameEngine.GameBoardCols)
                {
                    throw new ArgumentException("The given value for game cols is invalid");
                }

                this.boardCols = value;
            }
        }

        /// <summary>
        /// Overrides the method and creates the game board as a string with some formatting
        /// </summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            string offset = "   "; // x3 white spaces.

            // For printing the number of columns above the upper border.
            string colIndeces = this.GetColumnIndecesAsString();           
            result.AppendFormat(offset + colIndeces);

            // For printing the upper border.
            string upperBorder = this.GetHorizontalBorderAsString();
            result.AppendLine();
            result.Append(offset + upperBorder);
            
            // For printing the inner part of the matrix field and the row indeces.
            result.AppendLine();
            for (int i = 0; i < this.BoardRows; i++)
            {
                result.AppendFormat("{0} | ", i);
                for (int j = 0; j < this.BoardCols; j++)
                {
                    result.AppendFormat("{0} ", this.Field[i, j]);
                }

                result.AppendLine("| ");
            }

            // For printing the lower border.
            string lowerBorder = this.GetHorizontalBorderAsString();
            result.Append(offset + lowerBorder);
            result.AppendLine();

            return result.ToString();
        }

        /// <summary>
        /// Checks if the given row and column positions are in the range of the given field.
        /// </summary>
        public bool IsInField(int rowPosition, int colPosition)
        {
            bool isRowPosInField = (rowPosition >= 0) && (rowPosition < this.BoardRows);
            bool isColPosInFeild = (colPosition >= 0) && (colPosition < this.BoardCols);
            if (isRowPosInField && isColPosInFeild)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if the whole game Field is empty - contains only GameObjects with numeric value 0
        /// </summary>
        public bool IsEmpty()
        {
            bool isEmpty = true;
            int rowsLength = this.BoardRows;
            int colsLength = this.BoardCols;
            for (int row = 0; (row < rowsLength) && isEmpty; row++)
            {
                for (int col = 0; col < colsLength; col++)
                {
                    if (this.Field[row, col].NumValue != 0)
                    {
                        isEmpty = false;
                        break;
                    }
                }
            }

            return isEmpty;
        }

        /// <summary>
        /// Randomly generated the whole field with different numerical value for each game object
        /// </summary>
        /// <param name="startRange">Starts from that value - lower bound</param>
        /// <param name="endRange">Ends but can't reach this value - upper bound</param>
        private void Generate(int startRange, int endRange)
        {
            for (int row = 0; row < this.BoardRows; row++)
            {
                for (int column = 0; column < this.BoardCols; column++)
                {
                    int randomNumber = RandomUtils.GenerateRandomNumber(startRange, endRange);
                    this.Field[row, column] = new GameObject(randomNumber, row, column);
                }
            }
        }

        /// <summary>
        /// Helping function for the formatting of the output - for the column indexes
        /// </summary>
        private string GetColumnIndecesAsString()
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < this.BoardCols; i++)
            {
                result.AppendFormat(" {0}", i);
            }

            return result.ToString();
        }

        /// <summary>
        /// Helping function for the formatting of the output - for Horizontal line of dashes
        /// </summary>
        private string GetHorizontalBorderAsString()
        {
            StringBuilder result = new StringBuilder();
            int borderLength = (this.BoardCols * 2) + 1;
            for (int column = 0; column < borderLength; column++)
            {
                result.Append("-");
            }

            return result.ToString();
        }
    }
}