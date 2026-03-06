using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace ConsoleApp1
{
    public class PracticeExam:Exam
    {
        public PracticeExam(int t, int n, List<Question> q, Dictionary<Question, Answer> qad, Subject sub) : base(t, n, q, qad, sub)
        {

        }
        public override void Finish()
        {
            base.Finish();
            int correctAns = 0;
            int i = 1;

            foreach (var pair in QuestionAnswerDirectory)
            {
                if (pair.Key.CheckAnswer(pair.Value))
                {
                    Console.WriteLine($"Question {i}: Correct Student Answer");
                    correctAns++;
                }
                else
                {
                    Console.WriteLine($"Question {i}: Incorrect Student Answer");
                }

                i++;
            }

            Console.WriteLine("-----TOTAL GRADE------");
            Console.WriteLine($"{correctAns}/{QuestionAnswerDirectory.Count()}");
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
