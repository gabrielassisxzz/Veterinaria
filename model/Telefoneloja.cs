using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.control;

namespace Veterinaria.model
{
    class Telefoneloja
    {
        public Telefone telefone { get; set; }

        public Loja loja { get; set; }

        public string numerotelefone => telefone?.numerotelefone;
        public string nomeloja => loja?.nomeloja;
    }
}
