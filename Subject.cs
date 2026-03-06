using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
     public class Subject
    {
        public string Name { get; set; }
        public List<Student> EnrolledStudents { get; }

        public Subject(string name)
        {
            Name = name;
            EnrolledStudents = new List<Student>();
        }

        public Subject(string name, IEnumerable<Student> students)
        {
            Name = name;
            EnrolledStudents = new List<Student>(students ?? Array.Empty<Student>());
        }

        //Methods
        public bool Enroll(Student stud)
        {
            if (stud == null) return false;

            if (!EnrolledStudents.Contains(stud))
            {
                EnrolledStudents.Add(stud);
                return true;
            }
                return false;
        }
        public void NotifyStudents(Exam e)
        {
            foreach (var student in EnrolledStudents)
            {
                e.ExamStarted += student.onExamStarted;
            }
        }

    }
}
