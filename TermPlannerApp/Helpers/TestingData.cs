using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TermPlannerApp.Models;

namespace TermPlannerApp.Helpers
{
    public abstract class TestingData
    {
        public static void LoadTestData()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                LoadTermData(conn);
            }


        }

        private static void LoadTermData(SQLiteConnection conn)
        {
            conn.CreateTable<Term>();
            var terms = conn.Table<Term>();
            if (terms.Any()) return;
            DateTime d1 = DateTime.Now;
            for (int t = 1; t < 3; t++)
            {
                Term term = new Term();
                term.Name = $"{d1.Year} - term {t}";
                term.Start = d1.AddDays(1);
                term.End = d1.AddMonths(6);
                conn.Insert(term);
                var d2 = term.Start;
                for (int c = 1; c < 7; c++)
                {
                    Course course = new Course
                    {
                        Name = $"Course {c}",
                        TermId = term.Id,
                        InstructorName = "Bill Winters",
                        InstructorPhone = "615-745-9454",
                        InstructorEmail = "wwinte4@wgu.edu",
                        Start = d2,
                        End = d2.AddDays(14),
                        Status = "Registered"
                    };
                    
                    conn.Insert(course);

                    for (int a = 1; a < 3; a++)
                    {
                        Assessment assessment = new Assessment();
                        assessment.Type = a == 1 ? "Objective Assessment" : "Performance Assessment";
                        assessment.Name = $"{course.Name} {assessment.Type}";
                        assessment.Start = course.End;
                        assessment.End = course.End;
                        assessment.CourseId = course.Id;
                    }
                    d2 = course.End.AddDays(1);
                }

                d1= d1.AddMonths(6);
            }


        }
    }
}
