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

        public T GetItem<T>(int itemID)
        {
            T item = default;
            string sql = "SELECT * FROM Products.tbl_Items WHERE ItemID = @itemID;";

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                item = connection.QuerySingleOrDefault<T>(sql, new { itemID});
            }

            return item;
        }

        public void DeleteItem(tbl_Items item)
        {
            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Delete(item);
            }
        }

        public void UpdateItemQuantity(int itemID, int quantity)
        {
            string sql = "UPDATE Products.tbl_Items SET Quantity = @quantity WHERE ItemID = @itemID;";

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Query(sql, new { itemID, quantity });
            }
        }

        public List<T> GetShoppingList<T>(int userID)
        {
            List<T> list = new List<T>();
            string sql = "SELECT T1.ShoppingListID, T1.UserID, T1.ItemID, T1.Purchased, T2.ItemName " +
                "FROM Products.tbl_ShoppingList T1 INNER JOIN Products.tbl_Items T2 ON T1.ItemID = T2.ItemID " +
                "WHERE T1.UserID = @userID AND T2.Deleted <> 1";

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                list = connection.Query<T>(sql, new { userID }).ToList();
            }

            return list;
        }

        public T GetShoppingListItem<T>(int itemID, int userID)
        {
            T item = default;
            string sql = "SELECT * FROM Products.tbl_ShoppingList WHERE ItemID = @itemID AND UserID = @userID;";

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                item = connection.QuerySingleOrDefault<T>(sql, new { itemID, userID });
            }

            return item;
        }

        public int AddShoppingListItem(tbl_ShoppingList listItem)
        {
            int shoppingListID;
            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                if (listItem.ShoppingListID == 0)
                {
                    shoppingListID = (int)connection.Insert(listItem);
                }
                else
                {
                    connection.Update(listItem);
                    shoppingListID = listItem.ShoppingListID;
                }
            }
            return shoppingListID;
        }

        public void UpdatePurchasedShoppingListItem(int shoppingListID, bool purchased)
        {
            string sql = "UPDATE Products.tbl_ShoppingList SET Purchased = @purchased WHERE ShoppingListID = @shoppingListID;";

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Query(sql, new { shoppingListID, purchased });
            }
        }

        public void RemoveShoppingListItem(int shoppingListID)
        {
            string sql = "DELETE FROM Products.tbl_ShoppingList WHERE ShoppingListID = @shoppingListID";

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Query(sql, new { shoppingListID });
            }
        }
    }
}
