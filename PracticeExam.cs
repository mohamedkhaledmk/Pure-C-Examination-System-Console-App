using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace ConsoleApp1
{
    internal class PracticeExam:Exam
    {
        public PracticeExam(int t, int n, List<Question> q, Dictionary<Question, Answer> qad, Subject sub) : base(t, n, q, qad, sub)
        {

        }
        public override void Finish()
        {
            base.Finish();
            int correctAns = 0;
            for(int i=0;i<QuestionAnswerDirectory.Count();i++)
            {
                if(QuestionAnswerDirectory.ElementAt(i).Key.CheckAnswers(QuestionAnswerDirectory.ElementAt(i).Value))
                {
                    Console.WriteLine("Correct Student Answer");
                    correctAns++;
                } else
                {
                    Console.WriteLine("Incorrect Student Answer");
                }
            }
            Console.WriteLine("-----TOTAL GRADE------");
            Console.WriteLine(correctAns/QuestionAnswerDirectory.Count());
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

    }
}
