using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.control;

namespace Veterinaria.model
{
    class Funcionariotelefone
    {
        public Telefone telefone { get; set; }

        public Funcionario funcionario { get; set; }


        public string numerotelefone => telefone?.numerotelefone;
        public string nomefuncioario => funcionario?.nomefuncionario;

    }
}
