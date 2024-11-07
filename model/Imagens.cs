using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria.model
{
    internal class Imagens
    {
        public int codimagens {  get; set; }

        public String descricao {  get; set; }

        public byte[] foto { get; set; }

        public Produto produto { get; set; }
    }
}
