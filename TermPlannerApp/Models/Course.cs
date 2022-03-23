using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TermPlannerApp.Models
{
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int TermId { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Status { get; set; }
        public string InstructorName { get; set; }
        public string InstructorEmail { get; set; }
        public string InstructorPhone { get; set; }
        public string Notes { get; set; }
        public bool EnableNotifications { get; set; }
    }
}
