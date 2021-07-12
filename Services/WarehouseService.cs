using Mapster;
using Pantree.Core.AppModels;
using Pantree.Core.DataAccess;
using Pantree.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pantree.Core.Services
{
    public class WarehouseService
    {
        DataConnection db = new DataConnection();

        public bool CreateLocation(LocationCreate newLocation, int userID)
        {
            bool success;
            try
            {
                var location = newLocation.Adapt<tbl_Locations>();
                location.CreatedDate = DateTime.Now;
                location.UserID = userID;

                db.SaveLocation(location);
                CurrentUser.UpdateLocations(userID);
                success = true;
            }
            catch (Exception e)
            {
                throw e;
            }

            return success;
        }

        public List<LocationView> GetLocations(int userID)
        {
            return db.GetLocations<LocationView>(userID);
        }

        public bool CreateStore(StoreCreate newStore, int userID)
        {
            bool success;
            try
            {
                var store = newStore.Adapt<tbl_Stores>();
                store.CreatedDate = DateTime.Now;
                store.UserID = userID;

                db.SaveStore(store);
                CurrentUser.UpdateLocations(userID);
                success = true;
            }
            catch (Exception e)
            {
                throw e;
            }

            return success;
        }

        public List<StoreView> GetStores(int locationID, int userID)
        {
            return db.GetStores<StoreView>(locationID, userID);
        }

        public StoreView GetStore (int storeID, int userID)
        {
            var store = db.GetStore<StoreView>(storeID, userID);
            store.Items = db.GetItems<ItemView>(storeID);
            return store;
        }

        public LocationView GetLocation(int locationID, int userID)
        {
            var location = db.GetLocation<LocationView>(locationID, userID);
            location.Stores = db.GetStores<StoreView>(locationID, userID);
            return location;
        }

        public bool DeleteStore(int storeID, int userID)
        {
            bool success;
            try
            {
                var store = db.GetStore<tbl_Stores>(storeID, userID);
                db.DeleteStore(store);

                CurrentUser.UpdateLocations(userID);
                success = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            
            return success;
        }

       public bool DeleteLocation(int locationID, int userID)
        {
            bool success;
            try
            {
                var location = db.GetLocation<tbl_Locations>(locationID, userID);
                db.DeleteLocation(location);

                CurrentUser.UpdateLocations(userID);

                success = true;
            }
            catch (Exception e)
            {
                throw e;
            }

            return success;
        }

        public bool ShareLocation(LocationShareCreate sharedLocation)
        {
            bool success;
            try
            {
                var locations = db.GetSharedLocations<LocationShareCreate>(sharedLocation.SharedUserID);
                if (locations.Where(i => i.SharedLocationID == sharedLocation.SharedUserID).FirstOrDefault() == null)
                {
                    var contract = sharedLocation.Adapt<tbl_SharedLocations>();
                    db.ShareLocation(contract);
                }

                success = true;
            }
            catch (Exception e)
            {
                throw e;
            }

            return success;
        }

        public List<LocationView> GetSharedLocations(int userID)
        {
            var locations = db.GetSharedLocations<LocationView>(userID);

            foreach (var location in locations)
            {
                location.Stores = db.GetStores<StoreView>(location.LocationID, userID);
            }

            return locations;
        }
        public List<LocationView> GetAllLocations(int userID)
        {
            var locations = db.GetAllLocations<LocationView>(userID);

            foreach (var location in locations)
            {
                location.Stores = db.GetStores<StoreView>(location.LocationID, userID);
            }

            return locations;
        }
    }
}
