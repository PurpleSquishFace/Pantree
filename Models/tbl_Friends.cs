using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pantree.Core.Models
{
    [Table("Users.tbl_Friends")]
    public class tbl_Friends
    {
        [Key]
        public int FriendID { get; set; }
        public int UserID_1 { get; set; }
        public int UserID_2 { get; set; }
        public bool Accepted { get; set; }
        public bool Blocked { get; set; }
        public DateTime DateRequested { get; set; }
        public DateTime? DateAccepted { get; set; }
    }
}
