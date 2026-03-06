using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public delegate void ExamStartedHandler(object sender, ExamEventArgs e);
    public enum ExamMode { Starting,Queued,Finished};
    public abstract class Exam : ICloneable, IComparable<Exam>
    {
        public int Time { get; set; }
        public int NumberOfQuestions { get; set; }
        public List<Question> Questions { get; set; }
        public Dictionary<Question, Answer> QuestionAnswerDirectory { get; set; }
        public Subject Subject { get; set; }
        public ExamMode Mode
        {
            get; 
            set
            {
                field = value;
                if (value == ExamMode.Starting)
                    ExamStarted?.Invoke(this, new ExamEventArgs(Subject, this));
            }
        }

        //EventHandler
        public event ExamStartedHandler ExamStarted;

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
            int i = 1;
            foreach (var (q, a) in QuestionAnswerDirectory )
                Console.WriteLine($"  Question {i++}:\n Q: {q}  \nAns:  {a}");
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
