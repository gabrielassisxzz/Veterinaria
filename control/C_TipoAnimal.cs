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
    internal class C_Tipoanimal : I_Metodos_Comuns
    {
        //Variáveis Globais da Classe
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_tipoanimals;
        SqlDataAdapter da_tipoanimal;



        public List<Tipoanimal> DadosTipoanimal()
        {
            //Cria uma Lista do tipo Raça - Array
            List<Tipoanimal> lista_tipoanimal = new List<Tipoanimal>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_tipoanimal;
            conn.Open();

            try
            {
                dr_tipoanimal = cmd.ExecuteReader();
                while (dr_tipoanimal.Read())
                {
                    Tipoanimal aux = new Tipoanimal();
                    aux.codtipoanimal = Int32.Parse(dr_tipoanimal["codtipoanimal"].ToString());
                    aux.nometipoanimal = dr_tipoanimal["nometipoanimal"].ToString();

                    lista_tipoanimal.Add(aux);
                }
            }
            catch (Exception ex)
            {
            }

            return lista_tipoanimal;
        }

        public List<Tipoanimal> DadosTipoanimalFiltro(String parametro)
        {
            //Cria uma Lista do tipo Raça - Array
            List<Tipoanimal> lista_tipoanimal = new List<Tipoanimal>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlFiltro, conn);

            //Adiciona o valor a ser pesquisado no parâmetro
            cmd.Parameters.AddWithValue("pnometipoanimal", parametro + "%");

            SqlDataReader dr_tipoanimal;
            conn.Open();

            try
            {
                dr_tipoanimal = cmd.ExecuteReader();
                while (dr_tipoanimal.Read())
                {
                    Tipoanimal aux = new Tipoanimal();
                    aux.codtipoanimal = Int32.Parse(dr_tipoanimal["codtipoanimal"].ToString());
                    aux.nometipoanimal = dr_tipoanimal["nometipoanimal"].ToString();

                    lista_tipoanimal.Add(aux);
                }
            }
            catch (Exception ex)
            {
            }

            return lista_tipoanimal;
        }


        String sqlApaga = "delete from tipoanimal where codtipoanimal = @pcod";
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

        String sqlTodos = "select * from tipoanimal";
        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);
            //Abrir Conexão
            conn.Open();

            //Criar o DataAdapter
            da_tipoanimal = new SqlDataAdapter(cmd);

            dt_tipoanimals = new DataTable();
            da_tipoanimal.Fill(dt_tipoanimals);

            return dt_tipoanimals;
        }

        String sqlFiltro = "select * from tipoanimal where nometipoanimal like @pnometipoanimal";
        public DataTable Buscar_Filtro(String ptipoanimal)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("pnometipoanimal", ptipoanimal);
            //Abrir Conexão
            conn.Open();

            //Criar o DataAdapter
            da_tipoanimal = new SqlDataAdapter(cmd);

            dt_tipoanimals = new DataTable();
            da_tipoanimal.Fill(dt_tipoanimals);

            //Finaliza a Conexão
            conn.Close();
            return dt_tipoanimals;
        }



        String sqlInsere = "insert into tipoanimal(nometipoanimal) values (@pnome)";
        public void Insere_Dados(Object aux)
        {
            Tipoanimal tipoanimal = new Tipoanimal();
            tipoanimal = (Tipoanimal)aux; //casting

            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pnome", tipoanimal.nometipoanimal);

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

        String sqlAtualiza = "update tipoanimal set nometipoanimal = @pnome where" +
            " codtipoanimal = @pcod";
        public void Atualizar_Dados(object aux)
        {
            Tipoanimal dados = new Tipoanimal();
            dados = (Tipoanimal)aux;


            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcod", dados.codtipoanimal);
            cmd.Parameters.AddWithValue("@pnome", dados.nometipoanimal);

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
