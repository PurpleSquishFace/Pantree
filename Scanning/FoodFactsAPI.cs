using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Pantree.Core.Scanning
{
    public class FoodFactsAPI
    {
        private string ApiUrl { get => $"https://world.openfoodfacts.org/api/v0/product/{BarcodeString}.json"; }
        public string BarcodeString { get; private set; }
        public FoodFactsProduct Product { get; private set; }
        public bool LoadSuccessful { get; set; }

        public FoodFactsAPI(string barcode)
        {
            BarcodeString = barcode;
        }

        public bool CallAPI()
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(ApiUrl)
            };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "Pantree - Windows - Version 1.0");
            HttpResponseMessage response = client.GetAsync(ApiUrl).Result;
            LoadSuccessful = response.IsSuccessStatusCode;

            if (LoadSuccessful)
            {
                JObject obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);

                if (int.Parse(obj["status"].ToString()) != 0)
                {
                    string productCode = obj["code"].ToString();

                    var productNameObj = obj["product"]["product_name"];
                    string productName = productNameObj == null ? string.Empty : productNameObj.ToString();

                    var imageUrlObj = obj["product"]["image_url"];
                    string imageUrl = imageUrlObj == null ? string.Empty : imageUrlObj.ToString();

                    var ingredientsObj = obj["product"]["ingredients_text"];
                    string ingredients = ingredientsObj == null ? string.Empty : ingredientsObj.ToString();

                    Product = new FoodFactsProduct(productCode, productName, imageUrl, ingredients);
                }
                else
                {
                    LoadSuccessful = false;
                }
            }

            return LoadSuccessful;
        }
    }
}
