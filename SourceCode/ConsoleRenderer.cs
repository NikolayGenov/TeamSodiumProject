using System;

namespace BalloonsPopsGame
{
    public class ConsoleRenderer : IRenderable
    {
        public
        void Display(string textToDisplay)
        {
            Console.WriteLine(textToDisplay);
        }

        public string Read()
        {
            string textFromConsole = Console.ReadLine();
            return textFromConsole;
        }
    }
}