using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pantree.Core.Helpers
{
    public static class FormFileExtension
    {
        public static async Task<byte[]> GetBytes(this IFormFile formFile)
        {
            using(var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
