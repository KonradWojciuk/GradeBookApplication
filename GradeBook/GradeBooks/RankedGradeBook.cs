using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name, bool isWeighted) 
            : base(name, isWeighted) 
        {
            Type = Enums.GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
                throw new InvalidOperationException();

            int top20Students = (int)Math.Ceiling(Students.Count * 0.2);

            var grades = Students.OrderByDescending(s => s.AverageGrade).Select(s => s.AverageGrade).ToList();

            if (grades[top20Students - 1] <= averageGrade)
                return 'A';
            else if (grades[(top20Students * 2) - 1] <= averageGrade)
                return 'B';
            else if (grades[(top20Students * 3) - 1] <= averageGrade)
                return 'C';
            else if (grades[(top20Students * 4) - 1] <= averageGrade)
                return 'D';
            else
                return 'F';
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
                Console.WriteLine("Ranked grading requires at least 5 students.");
           
            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
                Console.WriteLine("Ranked grading requires at least 5 students.");

            base.CalculateStudentStatistics(name);
        }
    }
}
