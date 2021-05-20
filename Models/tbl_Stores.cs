using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pantree.Core.Models
{
    [Table("Storage.tbl_Stores")]
    public class tbl_Stores
    {
        [Key]
        public int StoreID { get; set; }
        public int UserID { get; set; }
        public int LocationID { get; set; }
        public string StoreName { get; set; }
        public string StoreDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
