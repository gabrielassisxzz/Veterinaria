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
    internal class C_Tipoproduto : I_Metodos_Comuns
    {
        //Variáveis Globais da Classe
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_tipoprodutos;
        SqlDataAdapter da_tipoproduto;



        public List<Tipoproduto> DadosTipoproduto()
        {
            //Cria uma Lista do tipo Raça - Array
            List<Tipoproduto> lista_tipoproduto = new List<Tipoproduto>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_tipoproduto;
            conn.Open();

            try
            {
                dr_tipoproduto = cmd.ExecuteReader();
                while (dr_tipoproduto.Read())
                {
                    Tipoproduto aux = new Tipoproduto();
                    aux.codtipoproduto = Int32.Parse(dr_tipoproduto["codtipoproduto"].ToString());
                    aux.nometipoproduto = dr_tipoproduto["nometipoproduto"].ToString();

                    lista_tipoproduto.Add(aux);
                }
            }
            catch (Exception ex)
            {
            }

            return lista_tipoproduto;
        }

        public List<Tipoproduto> DadosTipoprodutoFiltro(String parametro)
        {
            //Cria uma Lista do tipo Raça - Array
            List<Tipoproduto> lista_tipoproduto = new List<Tipoproduto>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlFiltro, conn);

            //Adiciona o valor a ser pesquisado no parâmetro
            cmd.Parameters.AddWithValue("pnometipoproduto", parametro + "%");

            SqlDataReader dr_tipoproduto;
            conn.Open();

            try
            {
                dr_tipoproduto = cmd.ExecuteReader();
                while (dr_tipoproduto.Read())
                {
                    Tipoproduto aux = new Tipoproduto();
                    aux.codtipoproduto = Int32.Parse(dr_tipoproduto["codtipoproduto"].ToString());
                    aux.nometipoproduto = dr_tipoproduto["nometipoproduto"].ToString();

                    lista_tipoproduto.Add(aux);
                }
            }
            catch (Exception ex)
            {
            }

            return lista_tipoproduto;
        }


        String sqlApaga = "delete from tipoproduto where codtipoproduto = @pcod";
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

        String sqlTodos = "select * from tipoproduto";
        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);
            //Abrir Conexão
            conn.Open();

            //Criar o DataAdapter
            da_tipoproduto = new SqlDataAdapter(cmd);

            dt_tipoprodutos = new DataTable();
            da_tipoproduto.Fill(dt_tipoprodutos);

            return dt_tipoprodutos;
        }

        String sqlFiltro = "select * from tipoproduto where nometipoproduto like @pnometipoproduto";
        public DataTable Buscar_Filtro(String ptipoproduto)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("pnometipoproduto", ptipoproduto);
            //Abrir Conexão
            conn.Open();

            //Criar o DataAdapter
            da_tipoproduto = new SqlDataAdapter(cmd);

            dt_tipoprodutos = new DataTable();
            da_tipoproduto.Fill(dt_tipoprodutos);

            //Finaliza a Conexão
            conn.Close();
            return dt_tipoprodutos;
        }



        String sqlInsere = "insert into tipoproduto(nometipoproduto) values (@pnome)";
        public void Insere_Dados(Object aux)
        {
            Tipoproduto tipoproduto = new Tipoproduto();
            tipoproduto = (Tipoproduto)aux; //casting

            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pnome", tipoproduto.nometipoproduto);

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

        String sqlAtualiza = "update tipoproduto set nometipoproduto = @pnome where" +
            " codtipoproduto = @pcod";
        public void Atualizar_Dados(object aux)
        {
            Tipoproduto dados = new Tipoproduto();
            dados = (Tipoproduto)aux;


            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcod", dados.codtipoproduto);
            cmd.Parameters.AddWithValue("@pnome", dados.nometipoproduto);

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
