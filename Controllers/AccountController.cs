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

        public IActionResult Index()
        {
            var user = CurrentUser.Details(UserID);

            return View(user);
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

        [HttpPost]
        public IActionResult ProfileImage()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadProfileImage(IFormFile profileImage)
        {
            ImageService ImageService = new ImageService();
            var profileImageID = CurrentUser.Details(UserID).ProfileImageID;

            if (profileImageID == null)
            {
                await ImageService.AddProfileImage(UserID, profileImage);
            }
            else
            {
                await ImageService.UpdateProfileImage(UserID, (int)profileImageID, profileImage);
            }

            return PartialView("ProfileImage");
        }

        [HttpPost]
        public IActionResult Friends()
        {
            var model = new FriendMaster
            {
                UserList = UserService.GetFriends(UserID)
            };

            return PartialView(model);
        }

        [HttpPost]
        public IActionResult Details()
        {
            var user = CurrentUser.Details(UserID);

            return PartialView(user);
        }

        [HttpPost]
        public IActionResult UpdateDetails(UserEdit details)
        {
            details.UserId = UserID;
            UserService.UpdateUserDetails(details);

            var user = CurrentUser.Details(UserID);
            return PartialView("Details", user);
        }

        #region Friends
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SearchUsers(FriendMaster search)
        {
            search.UserList = UserService.SearchUsers(search.SearchName, UserID);
            search.CurrentUser = CurrentUser.Details(UserID).id;
            //search.FriendIDs = UserService.GetFriends(UserID).Select(i => i.Id).ToList();

            return PartialView("_FriendSearchResult", search);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult RemoveFriend(int FriendUserID)
        {
            UserService.RemoveFriend(FriendUserID, UserID);

            return RedirectToAction("Friends");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BlockFriend(int FriendUserID) 
        {
            UserService.BlockFriend(FriendUserID, UserID);

            return RedirectToAction("Friends");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendFriendRequest(int UserID)
        {
            UserService.SendFriendRequest(UserID, this.UserID);

            return RedirectToAction("Friends");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AcceptFriend(int FriendUserID)
        {
            UserService.AcceptFriendRequest(FriendUserID, UserID);

            return RedirectToAction("Friends");
        }
        #endregion
    }
}