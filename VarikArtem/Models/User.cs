﻿using System;
using System.Collections.Generic;

namespace VarikArtem.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public int? IdProgrammer { get; set; }
        public virtual Programmer? IdProgrammerNavigation { get; set; }
    }
}
