using System;
using System.Linq;

namespace BalloonsPopsGame
{
    public struct Score : IComparable<Score>
    {
        public int Value { get; private set; } //??Private - could it be change after that ? - User with the name name , different value ?

        public string Name { get; private set; } //Eventually - exceptions and longer setter

        public Score(int value, string name) : this()
        {
            this.Value = value;
            this.Name = name;
        }
     
        public int CompareTo(Score other)
        {
            return this.Value.CompareTo(other.Value);
        }
    }
}