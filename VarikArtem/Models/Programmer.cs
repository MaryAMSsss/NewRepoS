using System.Collections.Generic;

namespace VarikArtem.Models
{
    public partial class Programmer
    {
        public Programmer()
        {
            Users = new HashSet<User>();
        }
        public Programmer(int id, string? name, int? age, int? experience, int? idLevel, int? idCompany)
        {
            Id = id;
            Name = name;
            Logo = resPath+imagePath;
            Age = age;
            Experience = experience;
            IdLevel = idLevel;
            IdCompany = idCompany;
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string Logo { get; set; }
        public string resPath = "C:\\Users\\razum\\Desktop\\Предметы\\VarikArtem\\VarikArtem\\Resourses\\";

        public string imagePath = "picture.png";
        public int? Age { get; set; }
        public int? Experience { get; set; }
        public int? IdLevel { get; set; }
        public int? IdCompany { get; set; }

        public virtual Company? IdCompanyNavigation { get; set; }
        public virtual Level? IdLevelNavigation { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
