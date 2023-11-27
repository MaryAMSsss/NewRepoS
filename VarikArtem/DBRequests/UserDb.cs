using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarikArtem.Models;

namespace VarikArtem.DBRequests
{
    internal class UserDb
    {
        private ProgerContext db = new ProgerContext();
        //запихнуть в список
        public List<User> GetUsers()
        {
            return db.Users.ToList();
        }
        //добавление нового программиста в бд
        public void AddUser(User user)
        {
            if (db.Users.Contains(user))
            {
                Console.WriteLine("В базе данных уже есть такой пользователь!");
                return;
            }
            else
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }
        //удаление по ID
        public void RemoveProgrammer(int id)
        {
            Programmer programmer = db.Programmers.FirstOrDefault(p => p.Id == id);
            RemoveProgrammer(programmer);
        }
        //удаление по name
        public void RemoveProgrammer(string name)
        {
            Programmer programmer = db.Programmers.FirstOrDefault(p => p.Name == name);
            RemoveProgrammer(programmer);
        }
        //удаление основа для перегрузки методов
        public void RemoveProgrammer(Programmer programmer)
        {
            if (programmer == null)
            {
                Console.WriteLine("Необходимо удалить пользователя");
            }
            else
            {
                db.Programmers.Remove(programmer);
                db.SaveChanges();
            }
        }
        //редактирование
        public void EditProgrammer(int id, string name, int age, int experience, int idLevel, int idCompany)
        {
            Programmer programmer = db.Programmers.FirstOrDefault(p => p.Id == id);

            if (programmer == null)
            {
                Console.WriteLine("Нет пользователя с таким айди");
            }

            else
            {
                programmer.Name = name;
                programmer.Age = age;
                programmer.Experience = experience;
                programmer.IdLevel = idLevel;
                programmer.IdCompany = idCompany;
                db.Programmers.Update(programmer);
                db.SaveChanges();
            }
        }
    }
}

