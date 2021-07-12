using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pantree.Core.Models
{
    [Table("Storage.tbl_SharedLocations")]
    public class tbl_SharedLocations
    {
        [Key]
        public int SharedID { get; set; }
        public int SharedLocationID { get; set; }
        public int SharedUserID { get; set; }
    }
}
