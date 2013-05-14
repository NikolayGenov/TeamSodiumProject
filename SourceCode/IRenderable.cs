using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalloonsPopsGame
{
    interface IRenderable
    {
        void Display(string textToDisplay);

        string Read();
    }
}