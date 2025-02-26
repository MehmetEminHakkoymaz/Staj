using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.Services
{
    public class UserService
    {
        private static string connectionString = "Data Source=UserDatabase.db;Version=3;";

        public User Login(string username, string password)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                var user = connection.QueryFirstOrDefault<User>(
                    "SELECT * FROM Users WHERE Username = @username AND Password = @password",
                    new { username, password });
                return user;
            }
        }

        public bool CreateUser(User user)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Execute(@"
                        INSERT INTO Users (Username, Password, Role) 
                        VALUES (@Username, @Password, @Role)", user);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public List<User> GetAllUsers()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return connection.Query<User>("SELECT * FROM Users").ToList();
            }
        }

        public bool UpdateUser(User user)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Execute(@"
                        UPDATE Users 
                        SET Password = @Password, Role = @Role 
                        WHERE Id = @Id", user);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteUser(int id)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Execute("DELETE FROM Users WHERE Id = @id", new { id });
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
