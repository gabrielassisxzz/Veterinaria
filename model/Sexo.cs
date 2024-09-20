using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria.model
{
    internal class Sexo
    {
        public int codsexo {  get; set; }
        public String nomesexo { get; set; }
        
        //Construtor Padrão
        public Sexo() { }

        public Sexo(int codsexo, string nomesexo)
        {
            this.codsexo = codsexo;
            this.nomesexo = nomesexo;
        }
    }
}
