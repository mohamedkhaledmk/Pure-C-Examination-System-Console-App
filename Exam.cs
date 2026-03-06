using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    enum ExamMode { Starting,Queued,Finished};
    internal abstract class Exam : ICloneable, IComparable<Exam>
    {
        private int Time { get; set; }
        public int NumberOfQuestions { get; set; }
        public List<Question> Questions { get; set; }
        public Dictionary<Question, Answer> QuestionAnswerDirectory { get; set; }
        private Subject Subject { get; set; }
        ExamMode Mode;

        //ctor
        public Exam(int t, int n, List<Question> q, Dictionary<Question, Answer> qad, Subject sub)
        {
            Time = t;
            NumberOfQuestions = n;
            Questions = new();
            foreach (var x in q)
                Questions.Add(x);
            QuestionAnswerDirectory = new();
            foreach (var x in qad)
                QuestionAnswerDirectory.TryAdd(x.Key, x.Value);
            Subject = sub;
            Mode = ExamMode.Queued;
        }
        // Methods
        public abstract void ShowExam();
        public virtual void Start()
        {
            Mode = ExamMode.Starting;
            Console.WriteLine("Exam Started");
        }

        public virtual void Finish()
        {
            Mode = ExamMode.Finished;
            Console.WriteLine("Exam Finished");

        }

        public override string ToString()
        {
            return $"Exam Subject: {Subject}\t\t ,Time: {Time}\nMode:{Mode}\t\tNumber Of Questions:" +
                $"{NumberOfQuestions}\n";
        }
        public override bool Equals(object? obj)
        {
            if (obj is Exam objExam && objExam != null)
                if (objExam.Time == Time && objExam.NumberOfQuestions == NumberOfQuestions && objExam.Subject.Equals(Subject) && objExam.Mode == Mode)
                    return true;
            return false;
        }

        public override int GetHashCode() => HashCode.Combine(Time,NumberOfQuestions,Subject,Mode);

        public object Clone() => this.MemberwiseClone();

        public int CompareTo(Exam? e1)
        {
            if (e1 == null)
                return 1;
            if (e1.Time == Time)
                return e1.NumberOfQuestions.CompareTo(NumberOfQuestions);
            return e1.Time.CompareTo(Time);
        }
    }
}
