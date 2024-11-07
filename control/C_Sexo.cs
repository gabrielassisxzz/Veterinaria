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
    internal class C_Sexo : I_Metodos_Comuns
    {
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_sexo;
        SqlDataAdapter da_sexo;

        String sqlInsere = "insert into sexo(nomesexo) values(@pnome)";
        String sqlApaga = "delete from sexo where codsexo = @pcod";
        String sqlAtualiza = "update sexo set nomesexo = @pnome where codsexo = @pcod";
        String sqlTodos = "select * from sexo";


        public List<Sexo> DadosSexo()
        {
            //Cria uma lista do tipo Raça - Array
            List<Sexo> lista_sexo = new List<Sexo>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_sexo;
            conn.Open();
            try
            {
                dr_sexo = cmd.ExecuteReader();
                while (dr_sexo.Read())
                {
                    Sexo aux = new Sexo();
                    aux.codsexo = Int32.Parse(dr_sexo["codsexo"].ToString());
                    aux.nomesexo = dr_sexo["nomesexo"].ToString();

                    lista_sexo.Add(aux);
                }
            }
            catch (Exception ex)
            {

            }

            return lista_sexo;
        }
        public List<Sexo> DadosSexoFiltro(String parametro)
        {
            //Cria uma lista do tipo Raça - Array
            List<Sexo> lista_sexo = new List<Sexo>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("pnomesexo", parametro + "%");

            SqlDataReader dr_sexo;
            conn.Open();
            try
            {
                dr_sexo = cmd.ExecuteReader();
                while (dr_sexo.Read())
                {
                    Sexo aux = new Sexo();
                    aux.codsexo = Int32.Parse(dr_sexo["codsexo"].ToString());
                    aux.nomesexo = dr_sexo["nomesexo"].ToString();

                    lista_sexo.Add(aux);
                }
            }
            catch (Exception ex)
            {

            }

            return lista_sexo;
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

        String sqlFiltro = "select * from sexo where nomesexo like @pnomesexo";

        public DataTable Buscar_Filtro(String psexo)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("pnomesexo", psexo);

            conn.Open();

            da_sexo = new SqlDataAdapter(cmd);

            dt_sexo = new DataTable();
            da_sexo.Fill(dt_sexo);

            conn.Close();
            return dt_sexo;
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

            da_sexo = new SqlDataAdapter(cmd);

            dt_sexo = new DataTable();
            da_sexo.Fill(dt_sexo);

            return dt_sexo;
        }
        public void Insere_Dados(object aux)
        {
            Sexo sexo = new Sexo();
            sexo = (Sexo)aux;

            //criando a Conexao com o banco de dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pnome", sexo.nomesexo);

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
            Atualiza_Dados(sexo);
        }
        public void Atualiza_Dados(object aux)
        {
            Sexo dados = new Sexo();
            dados = (Sexo)aux;

            //criando a Conexao com o banco de dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcod", dados.codsexo);
            cmd.Parameters.AddWithValue("@pnome", dados.nomesexo);

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
   

