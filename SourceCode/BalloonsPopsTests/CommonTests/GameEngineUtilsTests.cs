using System;
using System.IO;
using BalloonsPopsGame.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BalloonsPopsTests.CommonTests
{
    [TestClass]
    public class GameEngineUtilsTests
    {
        int rowNumber = 0;
        int colNumber = 0;

        [TestMethod]
        public void TestAreValidNumbers_IncorrectInput_ExtraNumbers()
        {
            string input = "3 3 3";
            Assert.AreEqual(false, GameEngineUtils.AreValidNumbers(input, out rowNumber, out colNumber));
        }

        [TestMethod]
        public void TestAreValidNumbers_IncorrectInput_LessNumbers()
        {
            string input = "3";
            int rowNumber = 0;
        
            Assert.AreEqual(false, GameEngineUtils.AreValidNumbers(input, out rowNumber, out colNumber));
        }

        [TestMethod]
        public void TestAreValidNumbers_IncorrectInputString()
        {
            string input = "345";
     
            Assert.AreEqual(false, GameEngineUtils.AreValidNumbers(input, out rowNumber, out colNumber));
        }

        [TestMethod]
        public void TestAreValidNumbers_IncorrectInputSeparators()
        {
            string input = "3-4";
            Assert.AreEqual(false, GameEngineUtils.AreValidNumbers(input, out rowNumber, out colNumber));
        }

        [TestMethod]
        public void TestAreValidNumbers_CorrectInput_WhiteSpaceSeparator()
        {
            string input = "3 4";
        
            Assert.AreEqual(true, GameEngineUtils.AreValidNumbers(input, out rowNumber, out colNumber));
        }

        [TestMethod]
        public void TestAreValidNumbers_CorrectInput_CommaSeparator()
        {
            string input = "3,4";
      
            Assert.AreEqual(true, GameEngineUtils.AreValidNumbers(input, out rowNumber, out colNumber));
        }

        [TestMethod]
        public void TestAreValidNumbers_CorrectInput_DotSeparator()
        {
            string input = "3.4";
            
            Assert.AreEqual(true, GameEngineUtils.AreValidNumbers(input, out rowNumber, out colNumber));
        }

        [TestMethod]
        public void TestAreValidNumbers_CorrectInput_ExtraWhiteSpaces()
        {
            string input = "     5       0     ";
           
            Assert.AreEqual(true, GameEngineUtils.AreValidNumbers(input, out rowNumber, out colNumber));
        }

        [TestMethod]
        public void TestStartMessage()
        {
            StreamReader streamReader = new StreamReader("StartMessage.txt");
            string expectedMessage;
            using (streamReader)
            {
                expectedMessage = streamReader.ReadToEnd();
            }

            Assert.AreEqual(expectedMessage, GameEngineUtils.StartMessage());
        }

        [TestMethod]
        public void TestIsValidName_InvalidName_Null()
        {
            string name = null;
            Assert.AreEqual(false, GameEngineUtils.IsValidName(name));
        }

        [TestMethod]
        public void TestIsValidName_InvalidName_EmptyString()
        {
            string name = string.Empty;
            Assert.AreEqual(false, GameEngineUtils.IsValidName(name));
        }

        [TestMethod]
        public void TestIsValidName_InvalidName_WhiteSpace()
        {
            string name = " ";
            Assert.AreEqual(false, GameEngineUtils.IsValidName(name));
        }

        [TestMethod]
        public void TestIsValidName_ValidName()
        {
            string name = "John Snow";
            Assert.AreEqual(true, GameEngineUtils.IsValidName(name));
        }
    }
}