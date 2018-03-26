using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dluznik
{
    public class Osoba : ATable
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
