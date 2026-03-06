using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class TrueFalseQuestion : Question
    {
        public TrueFalseQuestion(string h, string b, int m, AnswerList answers, Answer correctAnswer)
            : base(h, b, m, answers, correctAnswer)
        {
        }

        public override void Display()
        {
            Console.WriteLine("True/False Question");
            Console.WriteLine($"The Question Header is : {Header}\t\t {Marks} Marks");
            Console.WriteLine($"The Question Body is {Body}");
            Console.WriteLine("The Options Are : ");
            foreach (var ans in Answers)
                Console.WriteLine($"- {ans}");
        }

        public override bool CheckAnswers(Answer studentAnswer)
        {
            if (studentAnswer == null || CorrectAnswer == null)
                return false;

            return studentAnswer.Equals(CorrectAnswer);
        }
    }
}
