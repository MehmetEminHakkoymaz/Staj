using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Dapper;
using System.IO;

namespace WpfApp1.Data
{
    public class DatabaseHelper
    {
        private static string dbPath = "UserDatabase.db";
        private static string connectionString = $"Data Source={dbPath};Version=3;";

        public static void InitializeDatabase()
        {
            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Execute(@"
                        CREATE TABLE Users (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Username TEXT NOT NULL UNIQUE,
                            Password TEXT NOT NULL,
                            Role TEXT NOT NULL
                        )");

                    // Admin kullanıcısını oluştur
                    connection.Execute(@"
                        INSERT INTO Users (Username, Password, Role) 
                        VALUES (@username, @password, @role)",
                        new { username = "admin", password = "admin123", role = "admin" });
                }
            }
        }
    }
}
