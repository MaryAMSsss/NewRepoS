using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarikArtem.Models;

namespace VarikArtem.DBRequests
{
    internal static class CompaniesDB
    {
        public static string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Proger;Integrated Security=True;TrustServerCertificate=True;";
        public static string GetCompaniesString = "select * from Company;";
        static public List<Company> GetCompanies()
        {
            List<Company> companies = new List<Company>();
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = new SqlCommand(GetCompaniesString, conn);
            SqlDataReader dtreader = command.ExecuteReader();
            while (dtreader.Read())
            {
                int Id = dtreader.GetInt32(0);
                string Name = dtreader.GetString(1);
                companies.Add(new Company(Id, Name));
            }
            conn.Close();
            return companies;
        }
    }
}
