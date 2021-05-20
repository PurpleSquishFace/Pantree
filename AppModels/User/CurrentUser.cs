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

            SessionUser = new SessionUser(user, locations);
        }

        public static void Set(ISession session, UserService userService, int userID)
        {
            Session = session;

            var user = userService.GetUser(userID);
            var locations = GetLocations(user.id);

            SessionUser = new SessionUser(user, locations);
        }

        public static void UpdateLocations(int userID)
        {
            if (SessionUser.Details.id == userID)
            {
                SessionUser.Locations = GetLocations(SessionUser.Details.id);
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

        public List<LocationView> Locations { get; set; }

        public SessionUser(AspNetUser user, List<LocationView> locations)
        {
            Details = user;
            Locations = locations;
        }
    }
}
