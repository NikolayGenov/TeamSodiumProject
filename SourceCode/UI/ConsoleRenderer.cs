// **********************************************************
// <copyright file="ConsoleRenderer.cs" company="Telerik Academy">
// Copyright (c) 2013 Telerik Academy. All rights reserved.
// </copyright>
//
// **********************************************************

namespace BalloonsPopsGame.UI
{
    using System;

    /// <summary>
    /// Class that inherits the Render interface for the methods, and implements them with the windows standard console.
    /// </summary>
    public class ConsoleRenderer : IRenderable
    {
        /// <summary>
        /// Display on the console the text given as a parameter.
        /// </summary>
        public void Display(string textToDisplay)
        {
            Console.WriteLine(textToDisplay);
        }

        /// <summary>
        /// Read from the console some input and returns it as string.
        /// </summary>
        public string Read()
        {
            string textFromConsole = Console.ReadLine();
            return textFromConsole;
        }
    }
}