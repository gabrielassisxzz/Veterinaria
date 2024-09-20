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
    internal class C_Telefone : I_Metodos_Comuns
    {
        //Variáveis Globais da Classe
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_telefones;
        SqlDataAdapter da_telefone;



        public List<Telefone> DadosTelefone()
        {
            //Cria uma Lista do tipo Raça - Array
            List<Telefone> lista_telefone = new List<Telefone>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_telefone;
            conn.Open();

            try
            {
                dr_telefone = cmd.ExecuteReader();
                while (dr_telefone.Read())
                {
                    Telefone aux = new Telefone();
                    aux.codtelefone = Int32.Parse(dr_telefone["codtelefone"].ToString());
                    aux.numerotelefone = dr_telefone["numerotelefone"].ToString();

                    lista_telefone.Add(aux);
                }
            }
            catch (Exception ex)
            {
            }

            return lista_telefone;
        }

        public List<Telefone> DadosTelefoneFiltro(String parametro)
        {
            //Cria uma Lista do tipo Raça - Array
            List<Telefone> lista_telefone = new List<Telefone>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlFiltro, conn);

            //Adiciona o valor a ser pesquisado no parâmetro
            cmd.Parameters.AddWithValue("pnumerotelefone", parametro + "%");

            SqlDataReader dr_telefone;
            conn.Open();

            try
            {
                dr_telefone = cmd.ExecuteReader();
                while (dr_telefone.Read())
                {
                    Telefone aux = new Telefone();
                    aux.codtelefone = Int32.Parse(dr_telefone["codtelefone"].ToString());
                    aux.numerotelefone = dr_telefone["numerotelefone"].ToString();

                    lista_telefone.Add(aux);
                }
            }
            catch (Exception ex)
            {
            }

            return lista_telefone;
        }


        String sqlApaga = "delete from telefone where codtelefone = @pcod";
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

        String sqlTodos = "select * from telefone";
        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);
            //Abrir Conexão
            conn.Open();

            //Criar o DataAdapter
            da_telefone = new SqlDataAdapter(cmd);

            dt_telefones = new DataTable();
            da_telefone.Fill(dt_telefones);

            return dt_telefones;
        }

        String sqlFiltro = "select * from telefone where numerotelefone like @pnumerotelefone";
        public DataTable Buscar_Filtro(String ptelefone)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("pnumerotelefone", ptelefone);
            //Abrir Conexão
            conn.Open();

            //Criar o DataAdapter
            da_telefone = new SqlDataAdapter(cmd);

            dt_telefones = new DataTable();
            da_telefone.Fill(dt_telefones);

            //Finaliza a Conexão
            conn.Close();
            return dt_telefones;
        }



        String sqlInsere = "insert into telefone(numerotelefone) values (@pnumero)";
        public void Insere_Dados(Object aux)
        {
            Telefone telefone = new Telefone();
            telefone = (Telefone)aux; //casting

            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pnumero", telefone.numerotelefone);

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

        String sqlAtualiza = "update telefone set numerotelefone = @pnumero where" +
            " codtelefone = @pcod";
        public void Atualizar_Dados(object aux)
        {
            Telefone dados = new Telefone();
            dados = (Telefone)aux;


            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcod", dados.codtelefone);
            cmd.Parameters.AddWithValue("@pnumero", dados.numerotelefone);

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

