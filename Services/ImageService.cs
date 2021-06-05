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
                CurrentUser.UpdateProfileImage(userID, profileImageID);

                success = true;
            }
            catch (Exception e)
            {
                throw e;
            }

            return success;
        }

        public async Task<bool> UpdateProfileImage(int userID, int profileImageID, IFormFile profileImage)
        {
            bool success;
            try
            {
                var image = db.GetProfileImage<tbl_ProfileImage>(profileImageID);
                image.ProfileImage = await profileImage.GetBytes();
                image.CreatedDate = DateTime.Now;

                db.SaveProfileImage(image);
                CurrentUser.UpdateProfileImage(userID, profileImageID);

                success = true;
            }
            catch (Exception e)
            {
                throw e;
            }

            return success;
        }

        public ProfileImageView GetProfileImage(int? profileImageID)
        {
            if (profileImageID == null)
            {
                return null;
            }
            else
            {
                return db.GetProfileImage<ProfileImageView>((int)profileImageID);
            }
        }
    }
}
