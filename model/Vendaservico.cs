using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria.model
{
    internal class VendaServico

    {
        public String nomevendaservico {  get; set; }
        public int codvendaservico {  get; set; }
        
        public Funcionario funcionario { get; set; }

        public DateTime data { get; set; }

        public Cliente cliente { get; set; }

        public Animal animal { get; set; }
    }
}
