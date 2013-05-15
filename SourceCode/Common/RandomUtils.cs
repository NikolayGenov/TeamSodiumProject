// *********************************************
// <copyright file="RandomUtils.cs" company="Telerik Academy">
// Copyright (c) 2013 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************************************

namespace BalloonsPopsGame.Common
{
    using System;

    /// <summary>
    /// Class that have a random generator for numbers in a given range.
    /// </summary>
    internal static class RandomUtils
    {
        /// <summary>
        /// Creates new static Random generator of the class Random   
        /// </summary>
        private static readonly Random RandomNumber = new Random();

        /// <summary>
        /// Generates a random integer in the given range - value is in range - greater or equal than start and less than end.
        /// </summary>
        internal static int GenerateRandomNumber(int start, int end)
        {
            int randomByteNumber = RandomNumber.Next(start, end);
            return randomByteNumber;
        }
    }
}