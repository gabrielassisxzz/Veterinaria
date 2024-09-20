
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
    internal class C_Cep : I_Metodos_Comuns
    {
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_cep;
        SqlDataAdapter da_cep;

        String sqlInsere = "insert into cep(numerocep) values(@pnumero)";
        String sqlApaga = "delete from cep where codcep = @pcod";
        String sqlAtualiza = "update cep set numerocep = @pnumero where codcep = @pcod";
        String sqlTodos = "select * from cep";


        public List<Cep> DadosCep()
        {
            //Cria uma lista do tipo Raça - Array
            List<Cep> lista_cep = new List<Cep>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_cep;
            conn.Open();
            try
            {
                dr_cep = cmd.ExecuteReader();
                while (dr_cep.Read())
                {
                    Cep aux = new Cep();
                    aux.codcep = Int32.Parse(dr_cep["codcep"].ToString());
                    aux.numerocep = dr_cep["numerocep"].ToString();

                    lista_cep.Add(aux);
                }
            }
            catch (Exception ex)
            {

            }

            return lista_cep;
        }
        public List<Cep> DadosCepFiltro(String parametro)
        {
            //Cria uma lista do tipo Raça - Array
            List<Cep> lista_cep = new List<Cep>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("pnumerocep", parametro + "%");

            SqlDataReader dr_cep;
            conn.Open();
            try
            {
                dr_cep = cmd.ExecuteReader();
                while (dr_cep.Read())
                {
                    Cep aux = new Cep();
                    aux.codcep = Int32.Parse(dr_cep["codcep"].ToString());
                    aux.numerocep = dr_cep["numerocep"].ToString();

                    lista_cep.Add(aux);
                }
            }
            catch (Exception ex)
            {

            }

            return lista_cep;
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

        String sqlFiltro = "select * from cep where numerocep like @pnumerocep";

        public DataTable Buscar_Filtro(String pcep)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("pnumerocep", pcep);

            conn.Open();

            da_cep = new SqlDataAdapter(cmd);

            dt_cep = new DataTable();
            da_cep.Fill(dt_cep);

            conn.Close();
            return dt_cep;
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

            da_cep = new SqlDataAdapter(cmd);

            dt_cep = new DataTable();
            da_cep.Fill(dt_cep);

            return dt_cep;
        }
        public void Insere_Dados(object aux)
        {
            Cep cep = new Cep();
            cep = (Cep)aux;

            //criando a Conexao com o banco de dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pnumero", cep.numerocep);

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
        public void Atualiza_Dados(object aux)
        {
            Cep dados = new Cep();
            dados = (Cep)aux;

            //criando a Conexao com o banco de dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcod", dados.codcep);
            cmd.Parameters.AddWithValue("@pnumero", dados.numerocep);

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

        public void Atualizar_Dados(object aux)
        {
            throw new NotImplementedException();
        }
    }
}