using Microsoft.AspNetCore.Mvc;
using Pantree.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pantree.Core.Controllers
{
    public class ShoppingList : BaseController
    {
        public IActionResult Index()
        {
            var service = new ProductService();
            var model = service.GetShoppingList(UserID);

            return View(model);
        }

        [HttpPost]
        public IActionResult RemoveFromShoppingList(int ShoppingListID)
        {
            var service = new ProductService();
            service.RemoveShoppingListItem(ShoppingListID);

            return RedirectToAction("Index");
        }
    }
}
