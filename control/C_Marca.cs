using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Veterinaria.conection;
using Veterinaria.model;

namespace Veterinaria.control
{
    internal class C_Marca : I_Metodos_Comuns
    {
        //Variáveis Globais da Classe
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_marcas;
        SqlDataAdapter da_marca;



        public List<Marca> DadosMarca()
        {
            //Cria uma Lista do tipo Raça - Array
            List<Marca> lista_marca = new List<Marca>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_marca;
            conn.Open();

            try
            {
                dr_marca = cmd.ExecuteReader();
                while (dr_marca.Read())
                {
                    Marca aux = new Marca();
                    aux.codmarca = Int32.Parse(dr_marca["codmarca"].ToString());
                    aux.nomemarca = dr_marca["nomemarca"].ToString();

                    lista_marca.Add(aux);
                }
            }
            catch (Exception ex)
            {
            }

            return lista_marca;
        }

        public List<Marca> DadosMarcaFiltro(String parametro)
        {
            //Cria uma Lista do tipo Raça - Array
            List<Marca> lista_marca = new List<Marca>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlFiltro, conn);

            //Adiciona o valor a ser pesquisado no parâmetro
            cmd.Parameters.AddWithValue("pnomemarca", parametro + "%");

            SqlDataReader dr_marca;
            conn.Open();

            try
            {
                dr_marca = cmd.ExecuteReader();
                while (dr_marca.Read())
                {
                    Marca aux = new Marca();
                    aux.codmarca = Int32.Parse(dr_marca["codmarca"].ToString());
                    aux.nomemarca = dr_marca["nomemarca"].ToString();

                    lista_marca.Add(aux);
                }
            }
            catch (Exception ex)
            {
            }

            return lista_marca;
        }


        String sqlApaga = "delete from marca where codmarca = @pcod";
        public void Apaga_Dados(int aux)
        {
            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlApaga, conn);
            cmd.Parameters.AddWithValue("@pcod", aux);

            cmd.CommandType = CommandType.Text;
            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Apaguei");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro");
            }
            finally
            {
                conn.Close();
            }

        }



        public object Buscar_Id(int valor)
        {
            throw new NotImplementedException();
        }

        String sqlTodos = "select * from marca";
        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);
            //Abrir Conexão
            conn.Open();

            //Criar o DataAdapter
            da_marca = new SqlDataAdapter(cmd);

            dt_marcas = new DataTable();
            da_marca.Fill(dt_marcas);

            return dt_marcas;
        }

        String sqlFiltro = "select * from marca where nomemarca like @pnomemarca";
        public DataTable Buscar_Filtro(String pmarca)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("pnomemarca", pmarca);
            //Abrir Conexão
            conn.Open();

            //Criar o DataAdapter
            da_marca = new SqlDataAdapter(cmd);

            dt_marcas = new DataTable();
            da_marca.Fill(dt_marcas);

            //Finaliza a Conexão
            conn.Close();
            return dt_marcas;
        }



        String sqlInsere = "insert into marca(nomemarca) values (@pnome)";
        public void Insere_Dados(Object aux)
        {
            Marca marca = new Marca();
            marca = (Marca)aux; //casting

            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pnome", marca.nomemarca);

            cmd.CommandType = CommandType.Text;
            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Inseriu");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro");
            }
            finally
            {
                conn.Close();
            }

        }

        String sqlAtualiza = "update marca set nomemarca = @pnome where" +
            " codmarca = @pcod";
        public void Atualizar_Dados(object aux)
        {
            Marca dados = new Marca();
            dados = (Marca)aux;


            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcod", dados.codmarca);
            cmd.Parameters.AddWithValue("@pnome", dados.nomemarca);

            // cmd.CommandType = CommandType.Text;
            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Atualizei");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro");
            }
            finally
            {
                conn.Close();
            }

        }
    }
}
