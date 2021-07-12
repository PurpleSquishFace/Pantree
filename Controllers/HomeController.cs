using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pantree.Core.AppModels;
using Pantree.Core.Models;
using Pantree.Core.Helpers;
using Pantree.Core.Services;

namespace Pantree.Core.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProfileImage()
        {
            var file = CurrentUser.ProfileImage(UserID).ProfileImage;
            return File(file, "image/jpg", $"ProfileImage_Pantree{RandomString.GetRandomString(12)}.jpg");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
