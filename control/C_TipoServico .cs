using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Veterinaria.conection;

using Veterinaria.model;

namespace Veterinaria.control
{
    internal class C_TipoServico : I_Metodos_Comuns
    {
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_tiposervico;
        SqlDataAdapter da_tiposervico;

        String sqlInsere = "insert into tiposervico(nometiposervico, valortiposervico) values(@pnome, @pvalortiposervico)";
        String sqlApaga = "delete from tiposervico where codtiposervico = @pcod";
        String sqlAtualiza = "update tiposervico set nometiposervico = @pnome, valortiposervico = @pvalortiposervico where codtiposervico = @pcod";
        String sqlTodos = "select * from tiposervico";


        public List<Tiposervico> DadosTipoServico()
        {
            //Cria uma lista do tipo Raça - Array
            List<Tiposervico> lista_tiposervico = new List<Tiposervico>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_tiposervico;
            conn.Open();
            try
            {
                dr_tiposervico = cmd.ExecuteReader();
                while (dr_tiposervico.Read())
                {
                    Tiposervico aux = new Tiposervico();
                    aux.codtiposervico = Int32.Parse(dr_tiposervico["codtiposervico"].ToString());
                    aux.nometiposervico = dr_tiposervico["nometiposervico"].ToString();
                    aux.valortiposervico = double.Parse(dr_tiposervico["valortiposervico"].ToString());

                    lista_tiposervico.Add(aux);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            

            return lista_tiposervico;
        }
        public List<Tiposervico> DadosTipoServicoFiltro(String parametro)
        {
            //Cria uma lista do tipo Raça - Array
            List<Tiposervico> lista_tiposervico = new List<Tiposervico>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            //cmd.Parameters.AddWithValue("pnometiposervico", parametro+"%");
            //cmd.Parameters.AddWithValue("pdescricao", parametro+"%");
            cmd.Parameters.AddWithValue("@parametro", "%" + parametro + "%");

            SqlDataReader dr_tiposervico;
            conn.Open();
            try
            {
                dr_tiposervico = cmd.ExecuteReader();
                while (dr_tiposervico.Read())
                {
                    Tiposervico aux = new Tiposervico();
                    aux.codtiposervico = Int32.Parse(dr_tiposervico["codtiposervico"].ToString());
                    aux.nometiposervico = dr_tiposervico["nometiposervico"].ToString();
                    aux.valortiposervico = Int32.Parse(dr_tiposervico["valortiposervico"].ToString());

                    lista_tiposervico.Add(aux);
                }
            }
            catch (Exception ex)
            {

            }

            return lista_tiposervico;
        }
        public void Apaga_Dados(int aux)
        {
            //criando a Conexao com o banco de dados

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

        String sqlFiltro = "select * from tiposervico where nometiposervico like @parametro OR valortiposervico like @parametro";

        public DataTable Buscar_Filtro(String parametro)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);

            // Adiciona o parâmetro para nometiposervico e valortiposervico
            cmd.Parameters.AddWithValue("@parametro", "%" + parametro + "%");

            conn.Open();

            da_tiposervico = new SqlDataAdapter(cmd);

            dt_tiposervico = new DataTable();
            da_tiposervico.Fill(dt_tiposervico);

            conn.Close();
            return dt_tiposervico;
        }


        public object Buscar_Id(int valor)
        {
            throw new NotImplementedException();
        }
        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            conn.Open();

            da_tiposervico = new SqlDataAdapter(cmd);

            dt_tiposervico = new DataTable();
            da_tiposervico.Fill(dt_tiposervico);

            return dt_tiposervico;
        }
        public void Insere_Dados(object aux)
        {
            Tiposervico tiposervico = new Tiposervico();
            tiposervico = (Tiposervico)aux;

            // criando a Conexao com o banco de dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);

            // Adicione os parâmetros nometiposervico e valortiposervico
            cmd.Parameters.AddWithValue("@pnome", tiposervico.nometiposervico);
            cmd.Parameters.AddWithValue("@pvalortiposervico", tiposervico.valortiposervico);

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
                MessageBox.Show("Erro: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

       
        

        public void Atualizar_Dados(object aux)
        {
            {
                Tiposervico dados = new Tiposervico();
                dados = (Tiposervico)aux;

                //criando a Conexao com o banco de dados
                Conexao conexao = new Conexao();
                conn = conexao.ConectarBanco();

                cmd = new SqlCommand(sqlAtualiza, conn);
                cmd.Parameters.AddWithValue("@pcod", dados.codtiposervico);
                cmd.Parameters.AddWithValue("@pnome", dados.nometiposervico);
                cmd.Parameters.AddWithValue("@pvalortiposervico", dados.valortiposervico);

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
        }
    }
}