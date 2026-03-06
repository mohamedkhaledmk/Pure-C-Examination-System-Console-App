using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class FinalExam : Exam
    {
        public FinalExam(int t, int n, List<Question> q, Dictionary<Question, Answer> qad, Subject sub) : base(t, n, q, qad, sub)
        {
        }

        public override void ShowExam()
        {
            Console.WriteLine($"Subject{Subject}\t\tExam time is:{Time}\nExam has{NumberOfQuestions} Questions\n");
            for (int i = 0; i < NumberOfQuestions; i++)
            {
                Console.WriteLine($"Question n.o.{i + 1}");
                Console.WriteLine($"{Questions[i]}");
            }
        }
        public override void Finish()
        {
            base.Finish();
        }
    }
}