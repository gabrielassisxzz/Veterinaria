using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria.model
{
    internal class VendasProdutos
    {
        public int Id { get; set; }
        public Vendas vendas { get; set; } = new Vendas();
        public Produto produto { get; set; } = new Produto();
        public double quantidadevendasprodutos { get; set; }
        public double valorvendaprodutos { get; set; }
    }
}
