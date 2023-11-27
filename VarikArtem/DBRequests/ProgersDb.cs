using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using VarikArtem.Models;

namespace VarikArtem.DBRequests
{
    internal static class ProgersDb
    {
        public static string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Proger;Integrated Security=True;TrustServerCertificate=True;";

        public static string GetProgersCommandString = "SELECT * FROM Programmer";

        public static string AddProgersCommandString = "insert into Programmer (name, age, experience, id_level, id_company) values( ";

        static public List<Programmer> GetProgrammers()
        {
            List<Programmer> programmers = new List<Programmer>();
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = new SqlCommand(GetProgersCommandString, conn);
            SqlDataReader dtreader = command.ExecuteReader();
            while (dtreader.Read())
            {
                int Id = dtreader.GetInt32(0);
                string Name = dtreader.GetString(1);
                int Age = dtreader.GetInt32(2);
                int Exper = dtreader.GetInt32(3);
                int IdLevel = dtreader.GetInt32(4);
                int IdCompany = dtreader.GetInt32(5);
                programmers.Add(new Programmer(Id, Name, Age, Exper, IdLevel, IdCompany));
            }
            conn.Close();
            return programmers;
        }
        static public void AddProgrammers(Programmer programmer)
        {
            string AddProgersCommandString = $"insert into Programmer (name, age, experience, id_level, id_company) " +
                $"values( '{programmer.Name}', {programmer.Age}, {programmer.Experience}, {programmer.IdLevel},{programmer.IdCompany})";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = new SqlCommand(AddProgersCommandString, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
        static public void RemoveProgrammer(Programmer programmer)
        {
            string RemoveProgersCommandString = $"DELETE FROM Programmer WHERE id = {programmer.Id}";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = new SqlCommand(RemoveProgersCommandString, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
        static public void EditProgrammer(Programmer programmer)
        {
            string EditProgersCommandString = $"update Programmer set name = '{programmer.Name}', age = {programmer.Age}, experience = {programmer.Experience}, id_level = {programmer.IdLevel}, id_company = {programmer.IdCompany} where id = {programmer.Id};";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand sqlCommand = new SqlCommand(EditProgersCommandString, conn);
            sqlCommand.ExecuteNonQuery();
            conn.Close();
        }
    }
}
