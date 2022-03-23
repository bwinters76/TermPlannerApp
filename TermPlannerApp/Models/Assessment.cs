using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TermPlannerApp.Models
{
    public class Assessment
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool EnableNotifications { get; set; }
    }
}
