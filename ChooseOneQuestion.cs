using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    internal class ChooseOneQuestion : Question
    {
        public ChooseOneQuestion(string h, string b, int m, AnswerList answers, Answer correctAns) : base(h, b, m, answers, correctAns)
        {

        }

        public override bool CheckAnswers(Answer StudentAnswer)
        {
            throw new NotImplementedException();
        }

        public override void Display()
        {
            throw new NotImplementedException();
        }
    }
}
