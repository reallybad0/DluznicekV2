using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dluznik
{
    public class Seznam : ATable
    {
        public Seznam()
        {
        }
        public string NazevSeznamu { get; set; }

        public override string ToString()
        {
            return NazevSeznamu;
        }
    }
}
