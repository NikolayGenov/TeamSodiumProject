using System;
using BalloonsPopsGame.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BalloonsPopsTests
{
    [TestClass]
    public class BallonsPopsTest
    {
        [TestMethod]
        public void Main()
        {
            GameEngine.StartGame();
        }
    }
}