using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pantree.Core.AppModels
{
    public class FriendMaster
    {
        public List<FriendView> UserList { get; set; }

        public List<int> FriendIDs { get; set; }

        public int CurrentUser { get; set; }

        [Required]
        [Display(Name = "Enter Username")]
        public string SearchName { get; set; }
    }
}
