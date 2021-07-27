using Microsoft.AspNetCore.Mvc;
using Pantree.Core.AppModels;
using Pantree.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pantree.Core.Controllers
{
    public class ShoppingListController : BaseController
    {
        readonly ProductService ProductService = new ProductService();

        public IActionResult Index()
        {
            var model = ProductService.GetShoppingList(UserID);
            return View(model);
        }

        [HttpPost]
        public IActionResult RemoveFromShoppingList(int ShoppingListID)
        {
            ProductService.RemoveShoppingListItem(ShoppingListID);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdatePurchasedListItem(ShoppingListEdit shoppingListEdit)
        {
            ProductService.UpdateShoppingListPurchased(shoppingListEdit);
            return RedirectToAction("Index");
        }
    }
}
