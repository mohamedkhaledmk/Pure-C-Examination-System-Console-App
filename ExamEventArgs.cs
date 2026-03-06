using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{

    public class ExamEventArgs : EventArgs
    {
        public Subject Subject { get; }
        public Exam Exam { get; }
        public ExamEventArgs(Subject s, Exam e)
        {
            Subject = s;
            Exam = e;
        }
    }
}
