using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria.model
{
    internal class Produto
    {
        public int codproduto {  get; set; }
        public String nomeproduto { get; set; }
        public Marca marca { get; set; }
        public double quantidade { get; set; }
        public double valor {  get; set; }
        public Tipoproduto tipoproduto { get; set; }
    }
}
