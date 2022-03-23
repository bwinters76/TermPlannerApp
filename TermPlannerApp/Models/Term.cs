using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TermPlannerApp.Models
{
    public class Term
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }


        public Term()
        {
        }
    }
}
