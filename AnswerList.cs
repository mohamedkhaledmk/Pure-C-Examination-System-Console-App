using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    internal class AnswerList
    {
        public int Count { get; set; };
        private List<Answer> Answers;
        public AnswerList()
        {
            Answers = new List<Answer>(10);
            Count = 0;
        }
        public AnswerList(List<Answer> answers):this()
        {
            foreach(var x in answers)
            {
                Answers.Add(x);
                Count++;
            }
        }
        public Answer? this[int idx]
        {
            get 
            {
                if (idx < Answers.Count())
                    return Answers.ElementAt(idx);
                return null;
            }
            set 
            {
                if(value is Answer ansVal)
                {
                    Answers[idx] =ansVal;
                }
            }
        }
        public void Add(Answer answer)
        {
            Answers.Add(answer);
            Count++;
        }
        public Answer GetById(int id)
        {
            foreach(Answer ans in Answers)
            {
                if(ans.Id==id)
                {
                    return ans;
                }
            }
            return null;
        }
    }
}
