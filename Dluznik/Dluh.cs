using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dluznik
{
    public class Dluh : ATable
    {
        public int IDOsoby { get; set; }
        public int Castka { get; set; }
        public bool Zaplaceno { get; set; }
        
        public int Rok { get; set; }
        public int Mesic { get; set; }
        public int Den { get; set; }
        public int Sazba { get; set; }

    }
}
