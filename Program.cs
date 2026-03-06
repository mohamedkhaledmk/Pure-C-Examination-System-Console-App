using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //  1. Subject & Students
            var subject = new Subject("C# OOP");
            var student1 = new Student("Ahmed", 1);
            var student2 = new Student("Sara", 2);
            subject.Enroll(student1);
            subject.Enroll(student2);

            //  2. Build Questions

            // --- TrueFalse ---
            var tfAnswers = new AnswerList();
            tfAnswers.Add(new Answer(1, "True"));
            tfAnswers.Add(new Answer(2, "False"));
            var q1 = new TrueFalseQuestion("TF-Q1", "C# supports multiple inheritance.", 2, tfAnswers, tfAnswers.GetById(2)!);

            // --- ChooseOne ---
            var coAnswers = new AnswerList();
            coAnswers.Add(new Answer(1, "Compile-time"));
            coAnswers.Add(new Answer(2, "Runtime"));
            coAnswers.Add(new Answer(3, "Link-time"));
            var q2 = new ChooseOneQuestion("CO-Q1", "Polymorphism is resolved at?", 3, coAnswers, coAnswers.GetById(2)!);

            // --- ChooseAll ---
            var caAnswers = new AnswerList();
            caAnswers.Add(new Answer(1, "Abstraction"));
            caAnswers.Add(new Answer(2, "Encapsulation"));
            caAnswers.Add(new Answer(3, "Compilation"));
            caAnswers.Add(new Answer(4, "Polymorphism"));

            var caCorrect = new AnswerList();
            caCorrect.Add(new Answer(1, "Abstraction"));
            caCorrect.Add(new Answer(2, "Encapsulation"));
            caCorrect.Add(new Answer(4, "Polymorphism"));

            var q3 = new ChooseAllQuestion("CA-Q1", "Which are OOP pillars?", 4, caAnswers, caCorrect);

            //  3. QuestionList (file logging test)
            var questionList = new QuestionList("questions_log.txt");
            questionList.Add(q1);
            questionList.Add(q2);
            questionList.Add(q3);
            Console.WriteLine("QuestionList logged to questions_log.txt\n");

            //  4. PracticeExam — student answers correctly
            Console.WriteLine("══════════ PRACTICE EXAM (all correct) ══════════");
            var practiceQuestions = new List<Question> { q1, q2 };

            var practiceAnswers = new Dictionary<Question, Answer>
            {
                { q1, tfAnswers.GetById(2)! }, 
                { q2, coAnswers.GetById(2)! }    
            };

            var practiceExam = new PracticeExam(30, practiceQuestions.Count, practiceQuestions, practiceAnswers, subject);

            subject.NotifyStudents(practiceExam);

            practiceExam.ShowExam();
            practiceExam.Start();   
            Console.WriteLine();

            int practiceGrade = practiceExam.CorrectExam();
            Console.WriteLine($"CorrectExam() returned: {practiceGrade}"); 
            Console.WriteLine();
            practiceExam.Finish();

            //  5. PracticeExam — student answers wrong
            Console.WriteLine("\n══════════ PRACTICE EXAM (all wrong) ══════════");
            var wrongAnswers = new Dictionary<Question, Answer>
            {
                { q1, tfAnswers.GetById(1)! },   
                { q2, coAnswers.GetById(1)! }    
            };

            var practiceExam2 = new PracticeExam(30, practiceQuestions.Count, practiceQuestions, wrongAnswers, subject);
            subject.NotifyStudents(practiceExam2);
            practiceExam2.ShowExam();
            practiceExam2.Start();
            Console.WriteLine();
            int grade2 = practiceExam2.CorrectExam();
            Console.WriteLine($"CorrectExam() returned: {grade2}");
            Console.WriteLine();
            practiceExam2.Finish();

            //  6. FinalExam
            Console.WriteLine("\n══════════ FINAL EXAM ══════════");
            var finalQuestions = new List<Question> { q1, q2 };
            var finalAnswers = new Dictionary<Question, Answer>
            {
                { q1, tfAnswers.GetById(2)! },
                { q2, coAnswers.GetById(3)! }  
            };

            var finalExam = new FinalExam(60, finalQuestions.Count, finalQuestions, finalAnswers, subject);
            subject.NotifyStudents(finalExam);
            finalExam.ShowExam();
            finalExam.Start();
            Console.WriteLine();

            finalExam.Finish();


            Console.WriteLine("\n══════════ ChooseAll CheckAnswer test ══════════");

            // Correct set
            var studentCorrectAll = new AnswerList();
            studentCorrectAll.Add(new Answer(1, "Abstraction"));
            studentCorrectAll.Add(new Answer(2, "Encapsulation"));
            studentCorrectAll.Add(new Answer(4, "Polymorphism"));

            bool allCorrect = q3.CheckAnswer(studentCorrectAll);
            Console.WriteLine($"ChooseAll correct set  → {allCorrect}");   

            // Wrong set
            var studentWrongAll = new AnswerList();
            studentWrongAll.Add(new Answer(1, "Abstraction"));
            studentWrongAll.Add(new Answer(3, "Compilation"));

            bool allWrong = q3.CheckAnswer(studentWrongAll);
            Console.WriteLine($"ChooseAll wrong set    → {allWrong}");     

            //  8. Repository<T>
            Console.WriteLine("\n══════════ Repository ══════════");
            var repo = new Repository<Exam>();
            repo.Add(practiceExam);
            repo.Add(finalExam);
            repo.Sort();
            foreach (var e in repo.GetAll())
                Console.WriteLine(e);

            //  9. ICloneable / IComparable
            Console.WriteLine("\n══════════ Clone & Compare ══════════");
            var cloned = (Exam)practiceExam.Clone();
            Console.WriteLine($"Clone equals original: {cloned.Equals(practiceExam)}");
            Console.WriteLine($"CompareTo (same): {practiceExam.CompareTo(finalExam)}");

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}