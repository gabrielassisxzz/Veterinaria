using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria.model
{
    internal class Vendasproduto
    {
        public Vendas vendas { get; set; }
        public Produto produto { get; set; }
        public double quantv {  get; set; }
        public double valorv { get; set; }
    }
}
