using System;
using System.Collections.Generic;

namespace VarikArtem.Models
{
    public partial class Level
    {
        public Level()
        {
            Programmers = new HashSet<Programmer>();
        }
        
        public int Id { get; set; }
        public string? Name { get; set; }
        public Level(int id, string name)
        {
            Id=id;
            Name=name;
        }
        public virtual ICollection<Programmer> Programmers { get; set; }
    }
}
