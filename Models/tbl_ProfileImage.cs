using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pantree.Core.Models
{
    [Table("Images.tbl_ProfileImages")]
    public class tbl_ProfileImage
    {
        [Key]
        public int ProfileImageID { get; set; }
        public byte[] ProfileImage { get; set; }
        public string AlernativeText { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
