using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ZXing;
using ZXing.Common;
using ZXing.Windows.Compatibility;

namespace Pantree.Core.Scanning
{
    public class BarcodeScanner
    {
        public IFormFile BarcodeImage { get; private set; }
        public DecodingOptions Options { get => new DecodingOptions { TryHarder = true }; }
        public string OutputCode { get; private set; }
        public bool ScanSuccessful { get; private set; }

        public BarcodeScanner(IFormFile imageFile)
        {
            BarcodeImage = imageFile;
        }

        public bool ReadBarcode()
        {
            using (var stream = BarcodeImage.OpenReadStream())
            {                
                try
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    using (var coreCompatImage = (Bitmap)Image.FromStream(stream))
                    {
                        var reader = new BarcodeReader
                        {
                            Options = Options
                        };

                        var result = reader.Decode(coreCompatImage);
                        OutputCode = result == null ? string.Empty : result.Text;
                        ScanSuccessful = result != null;
                        return ScanSuccessful;
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}
