using Pantree.Core.AppModels;
using Pantree.Core.DataAccess;
using Pantree.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Pantree.Core.Services
{
    public class UserService
    {
        private DataConnection db = new DataConnection();
        private UserManager<AppUser> UserManager { get; }
        private SignInManager<AppUser> SignInManager { get; }

        #region Constructors
        public UserService()
        {

        }

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        #endregion               

        public AspNetUser GetUser(int userID)
        {
            var user = db.GetUser<AspNetUser>(userID);
            return user;
        }

        public AspNetUser GetUser(string userName)
        {
            var user = db.GetUser<AspNetUser>(userName);
            return user;
        }
        
        public async Task<bool> CreateUser(UserRegister newUser)
        {
            bool success = false;
            try
            {
                AppUser user = await UserManager.FindByNameAsync(newUser.UserName);

                if (user == null)
                {
                    user = newUser.Adapt<AppUser>();
                    user.CreatedDate = DateTime.Now;

                    IdentityResult result = await UserManager.CreateAsync(user, newUser.Password);

                    success = result.Succeeded;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return success;
        }

        public async Task<bool> LogIn(UserLogin user)
        {
            var result = await SignInManager.PasswordSignInAsync(user.UserName, user.Password, user.RemainLoggedIn, false);
            return result.Succeeded;
        }

        public async void Logout()
        {
            await SignInManager.SignOutAsync();
        }

        public int GetUserID(ClaimsPrincipal user)
        {
            var userID = UserManager.GetUserId(user);
            return int.Parse(userID);
        }

        public List<FriendView> GetFriends(int userID)
        {
            var list = db.GetUserFriends<FriendView>(userID);
            return list;
        }

        public List<FriendView> SearchUserName(string searchTerm)
        {
            if (searchTerm.Length < 4) return new List<FriendView>();
            return db.SearchUsers<FriendView>(searchTerm);
        }
                
        #region Sample database updates

        //public List<User> GetUsers()
        //{
        //    var list = db.GetAllUsers<User>();
        //    return list;
        //}

        //public List<UserView> GetUsersView()
        //{
        //    var list = db.GetAllUsers<UserView>();
        //    return list;
        //}

        //public User GetUser(int userID)
        //{
        //    var user = db.GetUser<User>(userID);
        //    return user;
        //}

        //public UserView GetUserView(int userID)
        //{
        //    var user = db.GetUser<UserView>(userID);
        //    return user;
        //}

        //public void InsertUser<T>(T user)
        //{
        //    User newUser = user.Adapt<User>();
        //    newUser.CreatedBy = 1000;
        //    newUser.CreatedDate = DateTime.Now;
        //    db.SaveUser(newUser);
        //}

        //public void InsertUsers<T>(IEnumerable<T> users)
        //{
        //    List<User> newUsers = new List<User>();
        //    users.ToList().ForEach(i => newUsers.Add(i.Adapt<User>()));
        //    newUsers.ForEach(i => i.CreatedDate = DateTime.Now);
        //    newUsers.ForEach(i => i.CreatedBy = 1000);
        //    db.SaveUsers(newUsers);
        //}

        //public void UpdateUser(User user)
        //{
        //    db.SaveUser(user);
        //}

        //public void UpdateUsers(IEnumerable<User> users)
        //{
        //    db.SaveUsers(users, true);
        //}
        #endregion
    }
}
