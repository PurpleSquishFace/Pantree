using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pantree.Core.AppModels
{
    public class UserView
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public ImageProfile ProfileImage { get; set; }
    }
}
