using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pantree.Core.AppModels;
using Pantree.Core.Scanning;
using Pantree.Core.Services;

namespace Pantree.Core.Controllers
{
    public class ProductController : BaseController
    {
        readonly ProductService ProductService = new ProductService();
        readonly WarehouseService WarehouseService = new WarehouseService();

        [HttpPost]
        public IActionResult Scan(IFormFile imageFile)
        {
            var model = new ResultView();
            var barcode = new BarcodeScanner(imageFile);

            if (barcode.ReadBarcode())
            {
                model.ScanSuccessful = barcode.ScanSuccessful;

                var product = ProductService.GetProduct(barcode.OutputCode);

                if (product == null)
                {
                    var apiObj = new FoodFactsAPI(barcode.OutputCode);

                    if (apiObj.CallAPI())
                    {
                        model.LoadProductSuccessful = apiObj.LoadSuccessful;
                        model.ProductView = ProductService.CreateAndGetProduct(apiObj.Product);
                    }
                }
                else
                {
                    model.LoadProductSuccessful = true;
                    model.ProductView = product;
                }

                model.ItemStoreSelect = new ItemStoreSelect(
                    model.ProductView.ProductID,
                    WarehouseService.GetLocations(UserID)
                );
            }

            return PartialView("ScanResult", model);
        }

        [HttpPost]
        public IActionResult ScanWithCode(string code)
        {
            var model = new ResultView
            {
                ScanSuccessful = true
            };

            var product = ProductService.GetProduct(code);

            if (product == null)
            {
                var apiObj = new FoodFactsAPI(code);

                if (apiObj.CallAPI())
                {
                    model.LoadProductSuccessful = apiObj.LoadSuccessful;
                    model.ProductView = ProductService.CreateAndGetProduct(apiObj.Product);
                }
            }
            else
            {
                model.LoadProductSuccessful = true;
                model.ProductView = product;
            }

            model.ItemStoreSelect = model.ProductView == null ? new ItemStoreSelect() : new ItemStoreSelect(
                model.ProductView.ProductID,
                WarehouseService.GetLocations(UserID)
            );

            return PartialView("ScanResult", model);
        }

        [HttpPost]
        public IActionResult AddItemForm(ItemStoreSelect selected)
        {
            var model = new ItemCreate();

            var store = WarehouseService.GetStore(selected.StoreID);
            var item = ProductService.GetItem(selected.ProductID, selected.StoreID);
            var product = ProductService.GetProduct(selected.ProductID);

            model.ProductID = item == null ? product.ProductID : item.ProductID;
            model.ItemName = item == null ? product.ProductName : item.ItemName;
            model.CurrentQuantity = item == null ? 0 : item.Quantity;
            model.Notes = item == null ? string.Empty : item.Notes;
            model.StoreID = store.StoreID;
            model.StoreName = store.StoreName;

            return PartialView("AddItemForm", model);
        }

        [HttpPost]
        public IActionResult AddItem(ItemCreate item)
        {
            ProductService.SaveItem(item);

            return RedirectToAction("Index", "Home");
        }
    }
}