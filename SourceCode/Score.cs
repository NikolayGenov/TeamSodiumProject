using System;
using System.Linq;

namespace BalloonsPopsGame
{
    public struct Score : IComparable<Score>
    {
        public int value;
        public string name;

        public Score(int value, string name)
            : this()
        {
            this.value = value;
            this.name = name;
        }

        public int CompareTo(Score other)
        {
            return this.value.CompareTo(other.value);
        }
    }
}