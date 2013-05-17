using System;
using BalloonsPopsGame.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BalloonsPopsTests.CommonTests
{
    [TestClass]
    public class RandomUtilsTests
    {
        [TestMethod]
        public void RandomUtilsTests_Random()
        {
            int outputNumber = RandomUtils.GenerateRandomNumber(4, 5);
            Assert.AreEqual(4, outputNumber);
        }
    }
}