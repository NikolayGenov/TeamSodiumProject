using System;
using System.Linq;

namespace BalloonsPopsGame
{
    public struct Score : IComparable<Score>
    {
        public int Value;
        public string Name;

        public Score(int value, string name)
        {
            Value = value;
            Name = name;
        }

        public int CompareTo(Score other)
        {
            return Value.CompareTo(other.Value);
        }
    }
}