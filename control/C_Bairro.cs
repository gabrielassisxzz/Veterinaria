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
    internal class C_Bairro : I_Metodos_Comuns
    {
        //Variáveis Globais da Classe
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_bairros;
        SqlDataAdapter da_bairro;



        public List<Bairro> DadosBairro()
        {
            //Cria uma Lista do tipo Raça - Array
            List<Bairro> lista_bairro = new List<Bairro>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_bairro;
            conn.Open();

            try
            {
                dr_bairro = cmd.ExecuteReader();
                while (dr_bairro.Read())
                {
                    Bairro aux = new Bairro();
                    aux.codbairro = Int32.Parse(dr_bairro["codbairro"].ToString());
                    aux.nomebairro = dr_bairro["nomebairro"].ToString();

                    lista_bairro.Add(aux);
                }
            }
            catch (Exception ex)
            {
            }

            return lista_bairro;
        }

        public List<Bairro> DadosBairroFiltro(String parametro)
        {
            //Cria uma Lista do tipo Raça - Array
            List<Bairro> lista_bairro = new List<Bairro>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlFiltro, conn);

            //Adiciona o valor a ser pesquisado no parâmetro
            cmd.Parameters.AddWithValue("pnomebairro", parametro + "%");

            SqlDataReader dr_bairro;
            conn.Open();

            try
            {
                dr_bairro = cmd.ExecuteReader();
                while (dr_bairro.Read())
                {
                    Bairro aux = new Bairro();
                    aux.codbairro = Int32.Parse(dr_bairro["codbairro"].ToString());
                    aux.nomebairro = dr_bairro["nomebairro"].ToString();

                    lista_bairro.Add(aux);
                }
            }
            catch (Exception ex)
            {
            }

            return lista_bairro;
        }


        String sqlApaga = "delete from bairro where codbairro = @pcod";
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

        String sqlTodos = "select * from bairro";
        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);
            //Abrir Conexão
            conn.Open();

            //Criar o DataAdapter
            da_bairro = new SqlDataAdapter(cmd);

            dt_bairros = new DataTable();
            da_bairro.Fill(dt_bairros);

            return dt_bairros;
        }

        String sqlFiltro = "select * from bairro where nomebairro like @pnomebairro";
        public DataTable Buscar_Filtro(String pbairro)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("pnomebairro", pbairro);
            //Abrir Conexão
            conn.Open();

            //Criar o DataAdapter
            da_bairro = new SqlDataAdapter(cmd);

            dt_bairros = new DataTable();
            da_bairro.Fill(dt_bairros);

            //Finaliza a Conexão
            conn.Close();
            return dt_bairros;
        }



        String sqlInsere = "insert into bairro(nomebairro) values (@pnome)";
        public void Insere_Dados(Object aux)
        {
            Bairro bairro = new Bairro();
            bairro = (Bairro)aux; //casting

            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pnome", bairro.nomebairro);

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

        String sqlAtualiza = "update bairro set nomebairro = @pnome where" +
            " codbairro = @pcod";
        public void Atualizar_Dados(object aux)
        {
            Bairro dados = new Bairro();
            dados = (Bairro)aux;


            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcod", dados.codbairro);
            cmd.Parameters.AddWithValue("@pnome", dados.nomebairro);

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
