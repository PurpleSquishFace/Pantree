using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pantree.Core.AppModels
{
    public class FriendView
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string ConvertedImage {
            get {
                if (ProfileImage == null) return "";
                return Convert.ToBase64String(ProfileImage);
            }
        }
        public byte[] ProfileImage { get; set; }
        public string AlternativeText { get; set; }
    }
}
