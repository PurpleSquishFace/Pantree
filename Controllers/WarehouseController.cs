using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pantree.Core.AppModels;
using Pantree.Core.Services;

namespace Pantree.Core.Controllers
{
    public class WarehouseController : BaseController
    {
        private readonly WarehouseService WarehouseService = new WarehouseService();
        private readonly ProductService ProductService = new ProductService();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Location(int id)
        {
            var model = WarehouseService.GetLocation(id, UserID);

            foreach (var store in model.Stores)
            {
                store.Items = ProductService.GetItems(store.StoreID);
            }

            return View(model);
        }

        public IActionResult Store(int id)
        {
            var model = WarehouseService.GetStore(id);
            return View(model);
        }

        public IActionResult CreateLocation()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateLocation(LocationCreate location)
        {
            if (WarehouseService.CreateLocation(location, UserID))
            {
                return RedirectToAction("Index");
            }

            return View(location);
        }

        public IActionResult CreateStore()
        {
            var model = new StoreCreate();
            model.Locations = WarehouseService.GetLocations(UserID);

            return View(model);
        }

        [HttpPost]
        public IActionResult CreateStore(StoreCreate store)
        {
            if (WarehouseService.CreateStore(store, UserID))
            {
                return RedirectToAction("Index");
            }

            return View(store);
        }

        [HttpPost]
        public JsonResult GetStores(int param)
        {
            var list = WarehouseService.GetStores(param, UserID);

            return Json(list);
        }
    }
}