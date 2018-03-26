using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dluznik
{
    public class Item : ATable
    {
        public Item()
        {
        }
        public int Cena { get; set; }
        public string Nazev { get; set; }
        public override string ToString()
        {
            return Nazev;
        }
    }
}
