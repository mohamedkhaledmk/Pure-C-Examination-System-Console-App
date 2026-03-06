using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class ChooseAllQuestion : Question
    {
        AnswerList CorrectAnswers;
        public ChooseAllQuestion(string h, string b, int m, AnswerList answers, AnswerList correctAns) : base(h, b, m, answers, null)
        {
            
        }

        public bool CheckAnswer(AnswerList studentAnswer)
        {
            if (studentAnswer == null || CorrectAnswers == null)
                return false;

            if (studentAnswer.Count != CorrectAnswers.Count)
                return false;

            foreach (Answer correct in CorrectAnswers)
            {
                bool found = false;
                foreach (Answer student in studentAnswer)
                {
                    if (student.Equals(correct))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    return false;
            }

            return true;
        }

        public override bool CheckAnswer(Answer stdAns)
        {
            throw new NotImplementedException();
        }

        public override void Display()
        {
            Console.WriteLine($"The Question Header is : {Header}\t\t {Marks} Marks");
            Console.WriteLine($"The Question Body is {Body}");
            Console.WriteLine("The Options Are :");

            foreach (var ans in Answers)
                Console.WriteLine($"- {ans}");
        }
    }
}
