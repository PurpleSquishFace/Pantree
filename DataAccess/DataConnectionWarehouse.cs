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
        public T GetLocation<T>(int locationID, int userID)
        {
            T location = default;
            string sql = "SELECT *, CASE WHEN UserID = @userID THEN 1 ELSE 0 END AS LocationOwner FROM Storage.tbl_Locations WHERE LocationID = @locationID";

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                location = connection.QuerySingleOrDefault<T>(sql, new { locationID, userID });
            }

            return location;
        }

        public List<T> GetLocations<T>(int userID)
        {
            List<T> list = new List<T>();
            string sql = "SELECT *, 1 AS LocationOwner FROM Storage.tbl_Locations WHERE UserID = @userID";

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                list = connection.Query<T>(sql, new { userID }).ToList();
            }

            return list;
        }

        public void SaveLocation(tbl_Locations location)
        {
            using (var connection = new SqlConnection(Configuration.ConnectionString))
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

        public void DeleteLocation(tbl_Locations location)
        {
            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Delete(location);
            }
        }


        public T GetStore<T>(int storeID, int userID)
        {
            T store = default;
            string sql = "SELECT *, CASE WHEN UserID = @userID THEN 1 ELSE 0 END AS StoreOwner FROM Storage.tbl_Stores WHERE StoreID = @storeID";

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                store = connection.QuerySingleOrDefault<T>(sql, new { storeID, userID });
            }

            return store;
        }

        public List<T> GetStores<T>(int locationID, int userID)
        {
            List<T> list = new List<T>();
            string sql = "SELECT *, CASE WHEN UserID = @userID THEN 1 ELSE 0 END AS StoreOwner FROM Storage.tbl_Stores WHERE LocationID = @locationID;";

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                list = connection.Query<T>(sql, new { locationID, userID }).ToList();
            }

            return list;
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

        public void DeleteStore(tbl_Stores store)
        {
            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Delete(store);
            }
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

        public void ShareLocation(tbl_SharedLocations sharedLocation)
        {
            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                if (sharedLocation.SharedID == 0)
                {
                    connection.Insert(sharedLocation);
                }
                else
                {
                    connection.Update(sharedLocation);
                }
            }
        }

        public List<T> GetSharedLocations<T>(int sharedUserID)
        {
            List<T> list = new List<T>();
            string sql = "SELECT *, 0 AS LocationOwner FROM Storage.tbl_SharedLocations T1 " +
                         "INNER JOIN Storage.tbl_Locations T2 ON T1.SharedLocationID = T2.LocationID WHERE SharedUserID = @sharedUserID";

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                list = connection.Query<T>(sql, new { sharedUserID }).ToList();
            }

            return list;
        }

        public List<T> GetAllLocations<T>(int userID)
        {
            List<T> list = new List<T>();
            string sql = "SELECT *, 1 AS LocationOwner FROM Storage.tbl_Locations T1 WHERE T1.UserID = @userID UNION " +
                         "SELECT T2.*, 0 AS LocationOwner FROM Storage.tbl_SharedLocations T1 " +
                         "INNER JOIN Storage.tbl_Locations T2 ON T1.SharedLocationID = T2.LocationID " +
                         "WHERE T1.SharedUserID = @userID";

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                list = connection.Query<T>(sql, new { userID }).ToList();
            }

            return list;
        }
    }
}
