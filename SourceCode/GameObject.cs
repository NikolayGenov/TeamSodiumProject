using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalloonsPopsGame
{
   public class GameObject
    {
        private int numValue;

        public GameObject(int numValue, Coords coordinates)
        {
            this.NumValue = numValue;
            this.Coordinates = coordinates;
        }

        public int NumValue
        {
            get
            {
                return this.numValue;
            }
            set
            {
                this.numValue = value;
            }
        }

        public Coords Coordinates { get; set; }

        public override string ToString()
        {
            if (this.numValue == 0)
            {
                return " ";
            }

            return this.numValue.ToString();
        }
    }
}