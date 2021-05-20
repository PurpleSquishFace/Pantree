using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pantree.Core.Models
{
    [Table("Storage.tbl_Locations")]
    public class tbl_Locations
    {
        [Key]
        public int LocationID { get; set; }
        public int UserID { get; set; }
        public string LocationName { get; set; }
        public string LocationDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
