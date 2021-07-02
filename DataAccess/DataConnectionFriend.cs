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

        public int GetFriendUserID(int userID_1, int userID_2)
        {
            int friendID = 0;
            string sql = "SELECT FriendID FROM Users.tbl_Friends " +
                        "WHERE (UserID_1 = @userID_1 AND UserID_2 = @userID_2) " +
                        "OR (UserID_1 = @userID_2 AND UserID_2 = @userID_1)";

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                friendID = connection.QuerySingleOrDefault<int>(sql, new { userID_1, userID_2 });
            }

            return friendID;
        }

        public tbl_Friends GetUserFriend(int friendID)
        {
            tbl_Friends friendsData = new tbl_Friends();
            string sql = "SELECT * FROM Users.tbl_Friends WHERE friendID = @friendID";

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                friendsData = connection.QuerySingleOrDefault<tbl_Friends>(sql, new { friendID });
            }

            return friendsData;
        }

        public List<T> GetUserFriends<T>(int userID)
        {
            List<T> list = new List<T>();
            string sql = "EXEC Users.usp_UserFriendList @userID;";

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                list = connection.Query<T>(sql, new { userID }).ToList();
            }

            return list;
        }

        public int SendFriendRequest(tbl_Friends request)
        {
            int friendID;
            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                if (request.FriendID == 0)
                {
                    friendID = (int)connection.Insert(request);
                }
                else
                {
                    connection.Update(request);
                    friendID = request.FriendID;
                }
            }
            return friendID;
        }

        public void AcceptFriend(int friendID)
        {
            var friendData = GetUserFriend(friendID);
            friendData.Accepted = true;
            friendData.DateAccepted = DateTime.Now;

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Update(friendData);
            }
        }

        public void RemoveFriend(int friendID)
        {
            var friendData = GetUserFriend(friendID);

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Delete(friendData);
            }
        }

        public void BlockFriend(int friendID)
        {
            var friendData = GetUserFriend(friendID);
            friendData.Accepted = false;
            friendData.DateAccepted = null;
            friendData.Blocked = true;

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Update(friendData);
            }
        }

    }
}
