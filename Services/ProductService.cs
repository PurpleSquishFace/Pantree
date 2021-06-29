using Mapster;
using Pantree.Core.AppModels;
using Pantree.Core.DataAccess;
using Pantree.Core.Models;
using Pantree.Core.Scanning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pantree.Core.Services
{
    public class ProductService
    {
        DataConnection db = new DataConnection();

        public ProductView GetProduct(string productCode)
        {
            var product = db.GetProduct<ProductView>(productCode);
            return product;
        }

        public ProductView GetProduct(int productID)
        {
            return db.GetProduct<ProductView>(productID);
        }

        public bool CreateProduct(FoodFactsProduct newProduct)
        {
            bool success;

            try
            {
                var product = newProduct.Adapt<tbl_Products>();
                product.CreatedDate = DateTime.Now;

                db.SaveProduct(product);                
                success = true;
            }
            catch (Exception e)
            {
                throw e;
            }

            return success;
        }

        public ProductView CreateAndGetProduct(FoodFactsProduct newProduct)
        {
            ProductView productView;
            try
            {
                var product = newProduct.Adapt<tbl_Products>();
                product.CreatedDate = DateTime.Now;

                int productID = db.SaveProduct(product);
                productView = db.GetProduct<ProductView>(productID);
            }
            catch (Exception e)
            {
                throw e;
            }
            return productView;
        }

        public bool CreateProductIfNew(FoodFactsProduct product)
        {
            if (GetProduct(product.ProductCode) == null)
            {
                return CreateProduct(product);
            }

            return false;
        }
        
        public ItemView GetItem(int productID, int storeID)
        {
            return db.GetItem<ItemView>(productID, storeID);
        }

        public List<ItemView> GetItems(int storeID)
        {
            return db.GetItems<ItemView>(storeID);
        }

        public bool SaveItem(ItemCreate newItem)
        {
            bool success;
            var savedItem = db.GetItem<ItemView>(newItem.ProductID, newItem.StoreID);

            try
            {
                if (savedItem == null)
                {
                    var item = newItem.Adapt<tbl_Items>();
                    item.CreatedDate = DateTime.Now;
                    item.LastUpdated = DateTime.Now;

                    db.SaveItem(item);
                    success = true;
                }
                else
                {
                    var item = savedItem.Adapt<tbl_Items>();
                    item.ItemName = newItem.ItemName;
                    item.Quantity += newItem.Quantity;
                    item.Notes = newItem.Notes;
                    item.LastUpdated = DateTime.Now;

                    db.SaveItem(item);
                    success = true;
                }
            }
            catch (Exception e)
            {

                throw e;
            }           

            return success;
        }

        public bool DeleteItem(int storeID)
        {
            bool success;
            try
            {
                var item = db.GetItem<tbl_Items>(storeID);
                db.DeleteItem(item);
                success = true;
            }
            catch (Exception e)
            {
                success = false;
                throw e;
            }

            return success;
        }
    }
}
