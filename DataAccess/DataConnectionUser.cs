using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
using Pantree.Core.Models;

namespace Pantree.Core.DataAccess
{
    public partial class DataConnection
    {
        public T GetUser<T>(int userID)
        {
            T user = default;
            string sql = "SELECT * FROM dbo.AspNetUsers WHERE Id = @userID;";

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                user = connection.QuerySingleOrDefault<T>(sql, new { userID });
            }

            return user;
        }

        public T GetUser<T>(string userName)
        {
            T user = default;
            string sql = "SELECT * FROM dbo.AspNetUsers WHERE UserName = @userName;";

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                user = connection.QuerySingleOrDefault<T>(sql, new { userName });
            }

            return user;
        }

        public List<T> GetAllUsers<T>()
        {
            List<T> list = new List<T>();
            string sql = "SELECT * FROM Users.tbl_AppUsers;";

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                list = connection.Query<T>(sql).ToList();
            }

            return list;
        }

        public void SaveUser(AspNetUser user)
        {
            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                if (user.id == 0)
                {
                    connection.Insert(user);
                }
                else
                {
                    connection.Update(user);
                }
            }
        }

        public void SaveUsers(IEnumerable<AspNetUser> users, bool update = true)
        {
            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                if (update)
                {
                    connection.Update(users);
                }
                else
                {
                    connection.Insert(users);
                }
            }
        }

        public List<T> SearchUsers<T>(string searchTerm, int userID)
        {
            List<T> list = new List<T>();
            string sql = "EXEC Users.usp_SearchFriends @searchTerm, @userID";

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                list = connection.Query<T>(sql, new { searchTerm, userID }).ToList();
            }

            return list;
        }

    }
}
