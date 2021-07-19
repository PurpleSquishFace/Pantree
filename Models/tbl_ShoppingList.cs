using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pantree.Core.Models
{
    [Table("Products.tbl_ShoppingList")]
    public class tbl_ShoppingList
    {
        [Key]
        public int ShoppingListID { get; set; }
        public int UserID { get; set; }
        public int ItemID { get; set; }
        public bool Purchased { get; set; }
    }
}
