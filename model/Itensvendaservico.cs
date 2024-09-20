using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria.model
{
    internal class Itensvendaservico
    {
        public Tiposervico tiposervico {  get; set; }
        public Vendaservico vendaservico { get; set; }
        public double quant {  get; set; }
        public double valor { get; set; }
        public Produto produto { get; set; }
    }
}
