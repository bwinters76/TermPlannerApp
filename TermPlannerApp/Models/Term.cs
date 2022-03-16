using System;
using System.Collections.Generic;
using System.Text;

namespace C971Application.Models
{
    public class Term
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public List<Course> Courses { get; set; }

        public Term(int id, string name, DateTime start, DateTime end)
        {
            Id = id;
            Name = name;
            Start = start;
            End = end;
        }
    }
}
