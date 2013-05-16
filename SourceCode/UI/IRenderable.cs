// **********************************************************
// <copyright file="IRenderable.cs" company="Telerik Academy">
// Copyright (c) 2013 Telerik Academy. All rights reserved.
// </copyright>
//
// **********************************************************

namespace BalloonsPopsGame.UI
{
    /// <summary>
    /// Interface for rendering stuff and output them on the given source.
    /// Can display text or read some input.
    /// </summary>
    public interface IRenderable
    {
        /// <summary>
        /// Display given text on the instance.
        /// </summary>
        /// <param name="textToDisplay">Given string to display.</param>
        void Display(string textToDisplay);

        /// <summary>
        /// Read some input from a given source.
        /// </summary>
        /// <returns>Returns a string value.</returns>
        string Read();
    }
}