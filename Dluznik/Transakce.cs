using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dluznik
{
    public class Transakce : ATable
    {
        public Transakce(){}
        public int Rok { get; set; }
        public int Mesic { get; set; }
        public int IDPredmet { get; set; }
        public int Mnozstvi { get; set; }
        public bool Zaplaceno { get; set; }
        public int IDSeznamu { get; set; }

    }
}
