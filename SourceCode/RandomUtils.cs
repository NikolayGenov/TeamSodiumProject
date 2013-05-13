using System;

namespace BalloonsPopsGame
{
    public class RandomUtils
    {
        public static int GenerateRandomNumber(int start, int end)
        {
            Random randomNumber = new Random();
            int randomByteNumber = randomNumber.Next(start, end);
            return randomByteNumber;
        }
    }
}