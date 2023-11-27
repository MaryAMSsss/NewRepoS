using System;
using System.Collections.Generic;

namespace VarikArtem.Models
{
    public partial class Company
    {
        public Company()
        {
            Programmers = new HashSet<Programmer>();
        }

        public Company( int id, string name)
        {
            Id = id;
            CompanyName = name;
        }
        public int Id { get; set; }
        public string? CompanyName { get; set; }

        public virtual ICollection<Programmer> Programmers { get; set; }
    }
}
