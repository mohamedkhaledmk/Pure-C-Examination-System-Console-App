using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class Student
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public Student(string n,int i)
        {
            Name = n;
            Id = i;
        }
        public Student(Student std)
        {
            Name = std.Name;
            Id = std.Id;
        }

        public void onExamStarted(object sender,ExamEventArgs e)
        {

        }
    }
}
