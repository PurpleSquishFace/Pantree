using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pantree.Core.AppModels;
using Pantree.Core.Models;
using Pantree.Core.Services;

namespace Pantree.Core.Controllers
{
    public class AccountController : BaseController
    {
        private UserService UserService { get; }

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            UserService = new UserService(userManager, signInManager);
        }

        public IActionResult Manage()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegister user)
        {
            if (await UserService.CreateUser(user))
            {
                CurrentUser.Set(HttpContext.Session, UserService, user.UserName);
                return RedirectToAction("Manage", "Account");
            }

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(UserLogin user)
        {
            if (await UserService.LogIn(user))
            {
                CurrentUser.Set(HttpContext.Session, UserService, user.UserName);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult Logout()
        {
            UserService.Logout();
            CurrentUser.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}