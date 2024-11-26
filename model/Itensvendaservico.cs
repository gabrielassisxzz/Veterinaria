using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria.model
{
    internal class Itensvendaservico
    {

        public Tiposervico tiposervico { get; set; }

        public VendaServico vendaservico { get; set; }

        public Double quantidade { get; set; }

        public Double valor { get; set; }

        public CidAnimal cidanimal  { get; set; }
    }
}
