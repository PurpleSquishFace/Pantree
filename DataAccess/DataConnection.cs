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
    public class DataConnection
    {
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

        public void SaveLocation(tbl_Locations location)
        {
            using(var connection = new SqlConnection(Configuration.ConnectionString))
            {
                if (location.LocationID == 0)
                {
                    connection.Insert(location);
                }
                else
                {
                    connection.Update(location);
                }
            }
        }

        public List<T> GetLocations<T>(int userID)
        {
            List<T> list = new List<T>();
            string sql = "SELECT * FROM Storage.tbl_Locations WHERE UserID = @userID";

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                list = connection.Query<T>(sql, new { userID }).ToList();
            }

            return list;
        }

        public T GetLocation<T>(int locationID)
        {
            T location = default;
            string sql = "SELECT * FROM Storage.tbl_Locations WHERE LocationID = @locationID";

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                location = connection.QuerySingleOrDefault<T>(sql, new { locationID });
            }

            return location;
        }

        public void SaveStore(tbl_Stores store)
        {
            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                if (store.StoreID == 0)
                {
                    connection.Insert(store);
                }
                else
                {
                    connection.Update(store);
                }
            }
        }

        public List<T> GetStores<T>(int locationID, int userID)
        {
            List<T> list = new List<T>();
            string sql = "SELECT * FROM Storage.tbl_Stores WHERE LocationID = @locationID AND UserID = @userID";

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                list = connection.Query<T>(sql, new { locationID, userID }).ToList();
            }

            return list;
        }

        public T GetStore<T>(int storeID)
        {
            T store = default;
            string sql = "SELECT * FROM Storage.tbl_Stores WHERE StoreID = @storeID";

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                store = connection.QuerySingleOrDefault<T>(sql, new { storeID });
            }

            return store;
        }

        public int SaveProduct(tbl_Products product)
        {
            int productID;
            using(var connection = new SqlConnection(Configuration.ConnectionString))
            {
                if (product.ProductID == 0)
                {
                    productID = (int)connection.Insert(product);
                }
                else
                {
                    connection.Update(product);
                    productID = product.ProductID;
                }
            }
            return productID;
        }

        public T GetProduct<T>(string productCode)
        {
            T product = default;
            string sql = "SELECT * FROM Products.tbl_Products WHERE ProductCode = @productCode";

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                product = connection.QuerySingleOrDefault<T>(sql, new { productCode });
            }

            return product;
        }

        public T GetProduct<T>(int productID)
        {
            T product = default;
            string sql = "SELECT * FROM Products.tbl_Products WHERE ProductID = @productID";

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                product = connection.QuerySingleOrDefault<T>(sql, new { productID });
            }

            return product;
        }

        public void SaveItem(tbl_Items item)
        {
            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                if (item.ItemID == 0)
                {
                    connection.Insert(item);
                }
                else
                {
                    connection.Update(item);
                }
            }
        }

        public List<T> GetItems<T>(int storeID)
        {
            List<T> list = new List<T>();
            string sql = "SELECT * FROM Products.tbl_Items WHERE StoreID = @storeID";

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                list = connection.Query<T>(sql, new { storeID }).ToList();
            }

            return list;
        }

        public T GetItem<T>(int productID, int storeID)
        {
            T item = default;
            string sql = "SELECT * FROM Products.tbl_Items WHERE ProductID = @productID AND StoreID = @storeID";

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                item = connection.QuerySingleOrDefault<T>(sql, new { productID, storeID });
            }

            return item;
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
    }
}
