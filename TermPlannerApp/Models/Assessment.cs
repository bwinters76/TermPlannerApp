using System;
using System.Collections.Generic;
using System.Text;

namespace C971Application.Models
{
    public class Assessment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
