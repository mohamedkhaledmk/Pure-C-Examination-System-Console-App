using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;

namespace ConsoleApp1
{
    public class TrueFalseQuestion:Question
    {
        public TrueFalseQuestion(string h,string b,int m,AnswerList answers ,Answer CorrectAnswer):base(h,b,m,answers,CorrectAnswer)
        {

        }
        public override void Display()
        {
            Console.WriteLine("True/False Question");
            {
                Console.WriteLine($"The Question Header is : {Header}\t\t {Marks} Marks\n The Question Body is{Body} \n The Options Are : ");
                foreach (var ans in Answers)
                    Console.WriteLine($"- {ans}");
            }
        }
        public override bool CheckAnswers(Answer StudentAnswer)
        {
            return StudentAnswer == CorrectAnswer;
        }
        

    }
}
