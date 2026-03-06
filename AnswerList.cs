using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class AnswerList : IEnumerable<Answer>
    {
        private readonly List<Answer> _answers;

        public int Count => _answers.Count;

        public AnswerList()
        {
            _answers = new List<Answer>(10);
        }

        public AnswerList(List<Answer> answers) : this()
        {
            if (answers == null) return;

            foreach (var x in answers)
            {
                Add(x);
            }
        }

        public Answer? this[int idx]
        {
            get
            {
                if (idx >= 0 && idx < _answers.Count)
                    return _answers[idx];

                return null;
            }
            set
            {
                if (idx >= 0 && idx < _answers.Count && value is Answer ansVal)
                {
                    _answers[idx] = ansVal;
                }
            }
        }

        public void Add(Answer answer)
        {
            if (answer == null) return;
            _answers.Add(answer);
        }

        public Answer? GetById(int id)
        {
            foreach (Answer ans in _answers)
            {
                if (ans.Id == id)
                {
                    return ans;
                }
            }

            return null;
        }

        public IEnumerator<Answer> GetEnumerator() => _answers.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
