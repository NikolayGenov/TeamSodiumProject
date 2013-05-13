using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalloonsPopsGame
{
    public class GameObject
    {
        public int NumValue { get; set; }

        public Coords Coordinates { get; set; }

        public GameObject(int numValue, Coords coordinates)
        {
            this.NumValue = numValue;
            this.Coordinates = coordinates;
        }
    }
}