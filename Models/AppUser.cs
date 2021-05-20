using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pantree.Core.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public int? ProfileImageID { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
