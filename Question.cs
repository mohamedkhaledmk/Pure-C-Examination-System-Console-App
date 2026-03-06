using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public abstract class Question
    {
        public string Header
        {
            get;
            set => field = value ?? "N/A";
        } = "N/A";

        public string Body
        {
            get;
            set => field = value ?? "Empty Body";
        } = "Empty Body";

        public int Marks
        {
            get;
            set => field = value > 0 ? value : 0;
        } = 0;

        public AnswerList Answers { get; set; }
        public Answer CorrectAnswer { get; set; }

        public abstract void Display();
        public abstract bool CheckAnswers(Answer stdAns);

        public override bool Equals(object? obj)
        {
            if (obj is Question rhsQuestion)
            {
                if (rhsQuestion.CorrectAnswer != CorrectAnswer ||
                    Answers.Count != rhsQuestion.Answers.Count ||
                    Header != rhsQuestion.Header ||
                    Body != rhsQuestion.Body)
                    return false;

                for (int i = 0; i < Answers.Count; i++)
                {
                    if (Answers[i] != rhsQuestion.Answers[i])
                        return false;
                }

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

        protected Question(string h, string b, int m, AnswerList answers, Answer correctAns)
        {
            Header = h;
            Body = b;
            Marks = m;

            Answers = new AnswerList();
            if (answers != null)
            {
                foreach (var ans in answers)
                    Answers.Add(ans);
            }

            CorrectAnswer = correctAns;
        }
    }
}
