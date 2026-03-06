using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{

    internal class ExamEventArgs:EventArgs
    {
        public Subject Subject { get; }    
        public Exam Exam { get; }
    }
}
