using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pantree.Core.AppModels
{
    public class ProfileImageView
    {
        public DateTime CreatedDate { get; set; }
        public byte[] ProfileImage { get; set; }
        public string AlternativeText { get; set; }
    }
}
