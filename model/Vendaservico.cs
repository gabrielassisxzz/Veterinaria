using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria.model
{
    internal class Vendaservico
    {
        public int codvendaservico {  get; set; }
        public Funcionario funcionario { get; set; }
        public DateTime datavc {  get; set; }
        public Cliente cliente { get; set; }
        public Animal animal { get; set; }
    }
}
