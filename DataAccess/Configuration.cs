using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pantree.Core.DataAccess
{
    public static class Configuration
    {
        public static IConfiguration Config;
        public static string ConnectionString => Config.GetConnectionString("ApplicationDatabase");
    }
}
