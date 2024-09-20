using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria.control
{
    internal interface I_Metodos_Comuns
    {
        void Insere_Dados(Object aux);

        void Apaga_Dados(int aux);

        Object Buscar_Id(int valor);

        DataTable Buscar_Todos();

        DataTable Buscar_Filtro(String dados);

        void Atualizar_Dados(Object aux);
    }
}
