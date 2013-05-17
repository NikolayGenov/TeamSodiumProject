using System;
using BalloonsPopsGame.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BalloonsPopsTests.CommonTests
{
    [TestClass]
    public class GameObjectTests
    {
        public const int GameBoardRows = 5;
        public const int GameBoardCols = 10;
        public const int StartColorRange = 1;
        public const int EndColorRange = 5;

        [TestMethod]
        public void GameObjectTests_Construtor()
        {
            GameObject firstObject = new GameObject(3, 5, 3);
            GameObject secondObject = new GameObject(3, 5, 3);
            Assert.AreEqual(firstObject.ToString(), secondObject.ToString(), "Problem with the constructor");
        }

        [TestMethod]
        public void GameObjectTests_GetRowPos()
        {
            GameObject firstObject = new GameObject(3, 2, 3);

            Assert.AreEqual(2, firstObject.RowPosition, "Problem with the getter of row position");
        }

        [TestMethod]
        public void GameObjectTests_GetColPos()
        {
            GameObject firstObject = new GameObject(3, 2, 4);

            Assert.AreEqual(4, firstObject.ColPosition, "Problem with the getter of column position");
        }

        [TestMethod]
        public void GameObjectTests_ToString()
        {
            GameObject objectOne = new GameObject(0, 5, 3);

            Assert.AreEqual(".", objectOne.ToString(), "Problem with the ToString()");
        }

        [TestMethod]
        public void GameObjectTests_ToStringTwo()
        {
            GameObject objectOne = new GameObject(1, 5, 3);

            Assert.AreEqual("1", objectOne.ToString(), "Problem with the ToString()");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GameObjectTests_PropertyExceptionRowPos()
        {
            GameObject objectOne = new GameObject(1, -1, 3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GameObjectTests_PropertyExceptionColPos()
        {
            GameObject objectOne = new GameObject(1, 1, -3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GameObjectTests_PropertyExceptionNumValue()
        {
            GameObject objectOne = new GameObject(-1, 1, 3);
        }
    }
}