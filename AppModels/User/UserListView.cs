using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pantree.Core.AppModels
{
    public class UserListView
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }

        public string ListName
        {
            get
            {
                return $"{UserName} ({Name})"; 
            }
        }
    }
}
