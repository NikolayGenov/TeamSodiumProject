using System;
using BalloonsPopsGame.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BalloonsPopsTests.CommonTests
{
    [TestClass]
    public class BoardTests
    {
        public const int GameBoardRows = 5;          
        public const int GameBoardCols = 10;     
        public const int StartColorRange = 1;
        public const int EndColorRange = 5;

        [TestMethod]
        public void BoardTests_Construtor()
        {
            Board firstBoard = new Board(GameBoardRows, GameBoardCols, StartColorRange, StartColorRange);
            Board secondBoard = new Board(GameBoardRows, GameBoardCols, StartColorRange, StartColorRange);
            Assert.AreEqual(firstBoard.ToString(), secondBoard.ToString(), "Problem with the constructor");
        }

        [TestMethod]
        public void BoardTests_RowsCols()
        {
            Board board = new Board(GameBoardRows, GameBoardCols, StartColorRange, EndColorRange);
            bool rowsCheck = board.BoardRows == GameBoardRows;
            bool colsCheck = board.BoardCols == GameBoardCols;
            Assert.IsTrue(rowsCheck && colsCheck, "Cols and rows are not set correctly");
        }
      
        [TestMethod]
        public void BoardTests_FieldTest()
        {
            Board board = new Board(GameBoardRows, GameBoardCols, StartColorRange, EndColorRange);

            bool isField = board.Field is GameObject[,];
            Assert.IsTrue(isField, "Field is not array of game objects");
        }

        [TestMethod]
        public void BoardTests_IsInFieldTrue()
        {
            Board board = new Board(GameBoardRows, GameBoardCols, StartColorRange, EndColorRange);

            bool isInFieldTrue = board.IsInField(GameBoardRows - 2, GameBoardCols - 2);
            Assert.IsTrue(isInFieldTrue, "Problem with IsInField method!");
        }

        [TestMethod]
        public void BoardTests_IsInFieldFalse()
        {
            Board board = new Board(GameBoardRows, GameBoardCols, StartColorRange, EndColorRange);

            bool isInFieldFalse = board.IsInField(GameBoardRows + 2, GameBoardCols + 2);
            Assert.IsFalse(isInFieldFalse, "Problem with IsInField method!");
        }

        [TestMethod]
        public void BoardTests_IsInFieldFalse1()
        {
            Board board = new Board(GameBoardRows, GameBoardCols, StartColorRange, EndColorRange);

            bool isInFieldFalse = board.IsInField(GameBoardRows + 2, GameBoardCols - 2);
            Assert.IsFalse(isInFieldFalse, "Problem with IsInField method!");
        }
        [TestMethod]
        public void BoardTests_IsEmptyTrue()
        {
            Board board = new Board(GameBoardRows, GameBoardCols, 0, 0);

            bool isEmptyTrue = board.IsEmpty();
            Assert.IsTrue(isEmptyTrue, "Problem with IsEmpty method!");
        }

        [TestMethod]
        public void BoardTests_IsEmptyFalse()
        {
            Board board = new Board(GameBoardRows, GameBoardCols, StartColorRange, StartColorRange);

            bool isEmptyFalse = board.IsEmpty();
            Assert.IsFalse(isEmptyFalse, "Problem with IsEmpty method!");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BoardTests_PropertyExceptionRows()
        {
            Board board = new Board(-4, 2, StartColorRange, EndColorRange);
        
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BoardTests_PropertyExceptionCols()
        {
            Board board = new Board(4, -2, StartColorRange, EndColorRange);
         
        }
    }
}