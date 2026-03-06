using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class QuestionList:List<Question>
    {
        public readonly string FileName;
        public QuestionList(string fn)
        {
            FileName = fn;
        }
        //i think it it would be better if i made add new virtual function
        public new void Add(Question q)
        {
            base.Add(q);
            using (StreamWriter sw = new StreamWriter(FileName,true))
            {
                sw.WriteLine(q);
            };
        }
    }
}
