using System;
using BalloonsPopsGame.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BalloonsPopsTests.CommonTests
{
    [TestClass]
    public class ScoreBoardTests
    {
        [TestMethod]
        public void ScoreBoardTests_ConstructorString()
        {
            ScoreBoard scoreOne = new ScoreBoard(1);
            ScoreBoard scoreTwo = new ScoreBoard(1);
            Assert.AreEqual(scoreOne.ToString(), scoreTwo.ToString());
        }

        [TestMethod]
        public void ScoreBoardTests_IsTopPlayerTrue()
        {
            ScoreBoard scoreOne = new ScoreBoard(4);
            bool isTrue = scoreOne.IsTopPlayer(5);
            Assert.IsTrue(isTrue);
        }

        [TestMethod]
        public void ScoreBoardTests_IsTopPlayerTrue1()
        {
            ScoreBoard scoreOne = new ScoreBoard(3);
            scoreOne.AddPlayer("gosho", 10);
            scoreOne.AddPlayer("gosho", 15);
            scoreOne.AddPlayer("gosho", 11);
            
            bool isTrue = scoreOne.IsTopPlayer(5);
            Assert.IsTrue(isTrue);
        }

        [TestMethod]
        public void ScoreBoardTests_IsTopPlayerTrue2()
        {
            ScoreBoard scoreOne = new ScoreBoard(3);
            scoreOne.AddPlayer("gosho", 10);
            scoreOne.AddPlayer("gosho", 15);
            scoreOne.AddPlayer("gosho", 11);

            bool isTrue = scoreOne.IsTopPlayer(10);
            Assert.IsTrue(isTrue);
        }

        [TestMethod]
        
        public void ScoreBoardTests_IsTopPlayerTrueFalse()
        {
            ScoreBoard scoreOne = new ScoreBoard(3);
            scoreOne.AddPlayer("gosho", 10);
            scoreOne.AddPlayer("gosho", 15);
            scoreOne.AddPlayer("gosho", 11);
            bool isFalse = scoreOne.IsTopPlayer(20);
            Assert.IsFalse(isFalse);
        }

        [TestMethod]
        public void ScoreBoardTests_IsTopPlayerTrueFalse1()
        {
            ScoreBoard scoreOne = new ScoreBoard(3);
            scoreOne.AddPlayer("gosho", 10);
            scoreOne.AddPlayer("gosho", 15);
            scoreOne.AddPlayer("gosho", 11);
            bool isFalse = scoreOne.IsTopPlayer(20);
            Assert.IsFalse(isFalse);
        }

        [TestMethod]
        public void ScoreBoardTests_PlayersGet()
        {
            ScoreBoard scoreOne = new ScoreBoard(1);
            scoreOne.AddPlayer("gosho", 5);
            Assert.AreEqual("{gosho}", scoreOne.PlayerName);
        }
        [TestMethod]
        public void ScoreBoardTests_ToString()
        {
            ScoreBoard scoreOne = new ScoreBoard(1);
            scoreOne.AddPlayer("gosho", 5);
            ScoreBoard scoreTwo = new ScoreBoard(1);
            scoreTwo.AddPlayer("gosho", 5);
            Assert.AreEqual(scoreOne.ToString(), scoreTwo.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ScoreBoardTests_ConstructorException()
        {
            ScoreBoard scoreOne = new ScoreBoard(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ScoreBoardTests_PlayerName()
        {
            ScoreBoard scoreOne = new ScoreBoard(3);
            scoreOne.AddPlayer(" ", 10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ScoreBoardTests_PlayerName1()
        {
            ScoreBoard scoreOne = new ScoreBoard(3);
            scoreOne.AddPlayer(null, 10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ScoreBoardTests_PlayerName2()
        {
            ScoreBoard scoreOne = new ScoreBoard(3);
            scoreOne.AddPlayer(string.Empty, 10);
        }
    }
}