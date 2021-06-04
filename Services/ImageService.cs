using Microsoft.AspNetCore.Http;
using Pantree.Core.AppModels;
using Pantree.Core.DataAccess;
using Pantree.Core.Helpers;
using Pantree.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pantree.Core.Services
{
    public class ImageService
    {
        private DataConnection db = new DataConnection();

        public ImageService()
        {

        }

        public async Task<bool> AddProfileImage(int userID, IFormFile profileImage)
        {
            bool success;
            try
            {
                var image = new tbl_ProfileImage
                {
                    ProfileImage = await profileImage.GetBytes(),
                    AlternativeText = $"{CurrentUser.Details(userID).Name}'s Profile Image",
                    CreatedDate = DateTime.Now
                };

                int profileImageID = db.SaveProfileImage(image);
                db.UpdateUserProfileImage(userID, profileImageID);
                //CurrentUser.UpdateProfileImage(userID);

                success = true;
            }
            catch (Exception e)
            {
                throw e;
            }

            return success;
        }

        public bool UpdateProfileImage(int userID, IFormFile profileImage)
        {
            throw new NotImplementedException();
        }
    }
}
