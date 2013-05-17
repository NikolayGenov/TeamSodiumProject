// **********************************************************
// <copyright file="BalloonsPops.cs" company="Telerik Academy">
// Copyright (c) 2013 Telerik Academy. All rights reserved.
// </copyright>
//
// **********************************************************
 
namespace BalloonsPopsGame
{
    using BalloonsPopsGame.Common;
    using BalloonsPopsGame.UI;

    /// <summary>
    /// The class where the game can be started
    /// </summary>
    public class BalloonsPops
    {
        /// <summary>
        /// The method where the game is being started from the GameEngine class.
        /// </summary>
        public static void Main()
        {
            IRenderable console = new ConsoleRenderer();
            GameEngine.StartGame(console, false);
        }
    }
}