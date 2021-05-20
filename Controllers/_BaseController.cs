using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Pantree.Core.AppModels;
using Pantree.Core.Helpers;
using Pantree.Core.Models;
using Pantree.Core.Services;

namespace Pantree.Core.Controllers
{
    public class BaseController : Controller
    {
        public int UserID { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            UserID = HttpContext.User.UserID<int>();

            if (HttpContext.User.Identity.Name == null)
            {
                CurrentUser.Clear();
            }
            else if(CurrentUser.Details(UserID) == null)
            {
                CurrentUser.Set(HttpContext.Session, new UserService(), UserID);
            }
        }        
    }
}