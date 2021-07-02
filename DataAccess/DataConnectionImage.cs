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
        public T GetProfileImage<T>(int profileImageID)
        {
            T profileImage = default;
            string sql = "SELECT * FROM Images.tbl_ProfileImages WHERE ProfileImageID = @profileImageID";

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                profileImage = connection.QuerySingleOrDefault<T>(sql, new { profileImageID });
            }

            return profileImage;
        }

        public int SaveProfileImage(tbl_ProfileImage image)
        {
            int profileImageID;
            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                if (image.ProfileImageID == 0)
                {
                    profileImageID = (int)connection.Insert(image);
                }
                else
                {
                    connection.Update(image);
                    profileImageID = image.ProfileImageID;
                }
            }
            return profileImageID;
        }

        public void UpdateUserProfileImage(int userID, int profileImageID)
        {
            string sql = "UPDATE dbo.AspNetUsers SET ProfileImageID = @profileImageID WHERE Id = @userID;";

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Query(sql, new { userID, profileImageID });
            }
        }
    }
}
