using Microsoft.AspNetCore.Http;
using Pantree.Core.Helpers;
using Pantree.Core.Models;
using Pantree.Core.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pantree.Core.AppModels
{
    public static class CurrentUser
    {
        private static ISession Session { get; set; }

        private static SessionUser SessionUser
        {
            get { return Session.GetObject<SessionUser>("CurrentUser"); }
            set { Session.SetObject("CurrentUser", value); }
        }
        
        public static void Set(ISession session, UserService userService, string userName)
        {
            Session = session;

            var user = userService.GetUser(userName);
            var locations = GetLocations(user.id);
            var profileImage = GetProfileImage(user.ProfileImageID);

            SessionUser = new SessionUser(user, locations, profileImage);
        }

        public static void Set(ISession session, UserService userService, int userID)
        {
            Session = session;

            var user = userService.GetUser(userID);
            var locations = GetLocations(user.id);
            var profileImage = GetProfileImage(user.ProfileImageID);

            SessionUser = new SessionUser(user, locations, profileImage);
        }

        public static void UpdateLocations(int userID)
        {
            var sessionUser = SessionUser;

            if (sessionUser.Details.id == userID)
            {
                var locations = GetLocations(sessionUser.Details.id);
                sessionUser.Locations = locations;

                SessionUser = sessionUser;
            }
        }

        public static void UpdateProfileImage(int userID, int profileImageID)
        {
            var sessionUser = SessionUser;

            if (sessionUser.Details.id == userID)
            {
                var profileImage = GetProfileImage(profileImageID);
                sessionUser.ProfileImage = profileImage;

                SessionUser = sessionUser;
            }
        }

        private static List<LocationView> GetLocations(int userID)
        {
            WarehouseService warehouseService = new WarehouseService();
            var locations = warehouseService.GetLocations(userID);

            foreach (var location in locations)
            {
                location.Stores = warehouseService.GetStores(location.LocationID, userID);
            }

            return locations;
        }

        private static ProfileImageView GetProfileImage(int? profileImageID)
        {
            ImageService imageService = new ImageService();
            var profileImage = imageService.GetProfileImage(profileImageID);
            return profileImage;
        }

        public static AspNetUser Details(int userID)
        {
            if (SessionUser != null && SessionUser.Details.id == userID)
            {
                return SessionUser.Details;
            }
            else
            {
                return null;
            }
        }

        public static List<LocationView> Locations(int userID)
        {
            if (SessionUser.Details.id == userID)
            {
                return SessionUser.Locations;
            }
            else
            {
                return Enumerable.Empty<LocationView>().ToList();
            }
        }

        public static ProfileImageView ProfileImage(int userID)
        {
            if (SessionUser.Details.id == userID)
            {
                return SessionUser.ProfileImage;
            }
            else
            {
                return null;
            }
        }

        public static void Clear()
        {
            if (Session != null)
            {
                SessionUser = null;
            }
        }
    }

    public class SessionUser
    {
        public AspNetUser Details { get; set; }

        public ProfileImageView ProfileImage { get; set; }
        
        public List<LocationView> Locations { get; set; }

        public SessionUser(AspNetUser user, List<LocationView> locations, ProfileImageView profileImage)
        {
            Details = user;
            Locations = locations;
            ProfileImage = profileImage;
        }
    }
}
