using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    internal class Subject
    {
        public string Name { get; set; }
        public List<Student> EnrolledStudents;

        //Methods
        public bool Enroll(Student stud)
        {
            if (!EnrolledStudents.Contains(stud))
            {
                EnrolledStudents.Add(stud);
                return true;
            }
                return false;
        }

    }
}
