using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class Answer:IComparable<Answer>
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;

        public Answer()
        {
            Text = string.Empty;
        }

        public Answer(int id, string text)
        {
            Id = id;
            Text = text;
        }
        public int CompareTo(Answer? ans)
        {
            return Id.CompareTo(ans?.Id??0);
        }
        public override bool Equals(object? obj)
        {
            if(obj is Answer objAns)
                return Id==objAns.Id;
            return false;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Text);
        }
        public override string ToString()
        {
            return $"Id is {Id}\nText is {Text}";
        }
    }
}
