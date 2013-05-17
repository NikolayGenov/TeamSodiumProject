using System;
using System.IO;
using System.Text;
using BalloonsPopsGame.Common;
using BalloonsPopsGame.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BalloonsPopsTests.CommonTests
{
    [TestClass]
    public class GameEngineTests
    {
        [TestMethod]
        public void GameEngineTests_StartGame()
        {
            StringBuilder commandInput = new StringBuilder();
            for (int i = 0; i < GameEngine.GameBoardRows; i++)
            {
                for (int j = 0; j < GameEngine.GameBoardRows; j++)
                {
                    commandInput.AppendFormat("{0} {1}", i, j).AppendLine();
                }
                commandInput.AppendLine();
            }
            
            commandInput.AppendLine("Gosho");
            commandInput.AppendLine("top");
            commandInput.AppendLine("restart");
            commandInput.AppendLine("exit");
            StreamReader reader = new StreamReader(new MemoryStream(Encoding.ASCII.GetBytes(commandInput.ToString())));
            
            Console.SetIn(reader);
            IRenderable console = new ConsoleRenderer();
            GameEngine.StartGame(console,false);
        
        }
    }
}