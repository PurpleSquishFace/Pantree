using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pantree.Core.Scanning
{
    public class FoodFactsProduct
    {
        public string ProductCode { get; private set; }
        public string ProductName { get; private set; }
        public string ImageURL { get; private set; }
        public string IngredientList { get; private set; }

        public FoodFactsProduct(string productCode, string productName, string imageUrl, string ingredients)
        {
            ProductCode = productCode;
            ProductName = productName == string.Empty ? "No product name found" : productName;
            ImageURL = imageUrl == string.Empty ? "/Images/Placeholder.jpg" : imageUrl;
            IngredientList = ingredients == string.Empty ? "No ingredient list found" : ingredients;
        }
    }
}
