using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarikArtem.Models;

namespace VarikArtem.DBRequests
{
    internal static class LevelsDB
    {
        public static string connectionString = @"Data Source=localhost\SQLEXPRESS; Initial Catalog = Proger; Integrated Security=True; TrustServerCertificate=True;";
        public static string GetLevelsCS = "select * from Level;";

        public static List<Level> GetLevels()
        {
            List<Level> levels = new List<Level>();
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(GetLevelsCS,conn);
            SqlDataReader dtreader = cmd.ExecuteReader();
            while (dtreader.Read())
            {
                int id = dtreader.GetInt32(0);
                string levelName = dtreader.GetString(1);
                levels.Add(new Level(id,levelName));
            }
            conn.Close();
            return levels;
        }
    }
}
