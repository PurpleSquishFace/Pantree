using Pantree.Core.Scanning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pantree.Core.AppModels
{
    public class ResultView
    {
        public string Code { get; set; }
        public bool ScanSuccessful { get; set; }
        public bool LoadProductSuccessful { get; set; }
        public ProductView ProductView { get; set; }
        public ItemStoreSelect ItemStoreSelect { get; set; }

        public ResultView()
        {

        }

        public ResultView(ProductView productView)
        {
            ProductView = productView;
        }
    }
}
