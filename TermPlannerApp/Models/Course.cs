using System;
using System.Collections.Generic;
using System.Text;

namespace C971Application.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Status { get; set; }
        public Instructor Instructor { get; set; }
        public string Notes { get; set; }
        public List<Assessment> Assessments { get; set; }
    }
}
