using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public abstract class Question
    {
        protected string Header { get; set=>field = value??"N/A"; }
        protected string Body { get; set => field = value ?? "Empty Body"; }
        protected int Marks { get; set => field = (value > 0 ?value: 0); }
        protected AnswerList Answers { get; set; }
        protected Answer CorrectAnswer { get; set; }
        public abstract void Display();
        public abstract bool CheckAnswers(Answer StudentAnswer);
        public override bool Equals(object? obj)
        {
            if(obj is Question RHSQuestion)
            {
                if (RHSQuestion.CorrectAnswer != CorrectAnswer || Answers.Count() != RHSQuestion.Answers.Count() || Header!=RHSQuestion.Header || Body!=RHSQuestion.Body )
                    return false;
                for (int i = 0; i < Answers.Count(); i++)
                    if (Answers[i] != RHSQuestion.Answers[i])
                        return false;
                return true;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Header, Body, Marks, CorrectAnswer);
        }
        public override string ToString()
        {
            return $"The Question Header is : {Header}\t\t {Marks} Marks\n The Question Body is{Body} \n ";
        }
        public Question(string h,string b, int m,AnswerList answers,Answer correctAns)
        {
            Header = h;
            Body = b;
            Marks = m;
            Answers = new();
            foreach (var ans in answers)
                Answers.Add(ans);
            CorrectAnswer = correctAns;
        }
    }
}
