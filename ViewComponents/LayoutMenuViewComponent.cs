using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pantree.Core.AppModels;
using Pantree.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pantree.Core.ViewComponents
{
    public class LayoutMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            int userID = HttpContext.User.UserID<int>();
            var model = CurrentUser.Locations(userID);
            return View(model);
        }
    }
}
