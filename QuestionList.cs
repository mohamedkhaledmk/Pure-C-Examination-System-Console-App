using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    internal class QuestionList:List<Question>
    {
        public new virtual void Add(Question q)
        {
            base.Add(q);
        }
    }
}
