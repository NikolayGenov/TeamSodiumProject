﻿using System;

namespace BalloonsPopsGame
{
    public static class RandomUtils
    {
        static readonly Random randomNumber = new Random();

        public static int GenerateRandomNumber(int start, int end)
        {
            int randomByteNumber = randomNumber.Next(start, end);
            return randomByteNumber;
        }
    }
}