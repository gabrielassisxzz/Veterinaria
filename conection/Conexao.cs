using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria.conection
{
    
    internal class Conexao
    {
        //Declaração de Atribututos
        SqlConnection conn;
        String strConnection = @"Server=.;Database=Veterinaria_Unifunec;
                                Trusted_Connection=True";
        
        //Método para conexao no banco de dados
        public SqlConnection ConectarBanco()
        {
            //Cria a conexao com banco
            conn = new SqlConnection(strConnection);
            return conn;
        }
    }
}
