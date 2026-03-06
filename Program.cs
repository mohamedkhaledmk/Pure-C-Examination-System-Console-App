using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Sample data to demonstrate the exam system.

            // Create subject and students
            var subject = new Subject("C# OOP");
            var student1 = new Student("Ahmed", 1);
            var student2 = new Student("Sara", 2);
            subject.Enroll(student1);
            subject.Enroll(student2);

            // Answers for questions
            var tfAnswers = new AnswerList();
            tfAnswers.Add(new Answer(1, "True"));
            tfAnswers.Add(new Answer(2, "False"));

            var mcAnswers = new AnswerList();
            mcAnswers.Add(new Answer(1, "Option A"));
            mcAnswers.Add(new Answer(2, "Option B"));
            mcAnswers.Add(new Answer(3, "Option C"));

            // Questions
            var q1 = new TrueFalseQuestion("Q1", "C# is an object oriented language.", 2, tfAnswers, tfAnswers.GetById(1)!);
            var q2 = new ChooseOneQuestion("Q2", "Which option is B?", 3, mcAnswers, mcAnswers.GetById(2)!);

            var questions = new List<Question> { q1, q2 };

            // Student answers (for demo, assume student1 answers correctly, student2 incorrectly)
            var answersForExam = new Dictionary<Question, Answer>
            {
                { q1, tfAnswers.GetById(1)! },
                { q2, mcAnswers.GetById(2)! }
            };

            var exam = new FinalExam(30, questions.Count, questions, answersForExam, subject);

            // Subscribe students to exam started event
            exam.ExamStarted += student1.onExamStarted;
            exam.ExamStarted += student2.onExamStarted;

            exam.ShowExam();
            exam.Start();

            Console.WriteLine();
            Console.WriteLine("Exam correction:");
            int totalGrade = exam.CorrectExam();
            Console.WriteLine($"Total grade = {totalGrade}");

            Console.WriteLine();
            exam.Finish();

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
