using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pantree.Core.AppModels
{
    public class ProductCreate
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string IngredientList { get; set; }
        public string ImageURL { get; set; }
    }
}
