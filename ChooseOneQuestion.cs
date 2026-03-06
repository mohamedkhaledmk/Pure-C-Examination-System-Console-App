using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class ChooseOneQuestion : Question
    {
        public ChooseOneQuestion(string h, string b, int m, AnswerList answers, Answer correctAns) : base(h, b, m, answers, correctAns)
        {

        }

        public override bool CheckAnswers(Answer studentAnswer)
        {
            if (studentAnswer == null || CorrectAnswer == null)
                return false;

            return studentAnswer.Equals(CorrectAnswer);
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
