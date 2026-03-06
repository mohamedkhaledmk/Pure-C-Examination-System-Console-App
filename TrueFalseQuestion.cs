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
            if (answers.Count != 2)
                throw new Exception("True/False Questions must have only 2 options");
        }

        public override void Display()
        {
            Console.WriteLine($"The Question Header is : {Header}\t\t {Marks} Marks");
            Console.WriteLine($"The Question Body is {Body}");
            Console.WriteLine("The Options Are : ");
            foreach (var ans in Answers)
                Console.WriteLine($"- {ans}");
        }

        public override bool CheckAnswer(Answer studentAnswer)
        {
            if (studentAnswer == null || CorrectAnswer == null)
                return false;

            return studentAnswer.Equals(CorrectAnswer);
        }
    }
}
