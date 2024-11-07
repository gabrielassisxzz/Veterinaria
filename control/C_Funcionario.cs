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
    internal class C_Funcionario : I_Metodos_Comuns
    {
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_Funcionario;
        SqlDataAdapter da_Funcionario;

            String sqlInsere = "insert into Funcionario(nomeFuncionario,codTipoFuncionariofk,codLojafk) values(@pnome,@pTipoFuncionario,@pLoja)";
            String sqlApaga = "delete from Funcionario where codFuncionario = @pcod";
            String sqlAtualiza = "update Funcionario set nomeFuncionario = @pnome, codTipoFuncionariofk = @pTipoFuncionario, codLojafk = @pLoja where codFuncionario = @pcod";
            String sqlTodos = "select a.codFuncionario, a.nomeFuncionario, b.nomeTipoFuncionario, c.nomeLoja " +
                          "from Funcionario a " +
                          "left join TipoFuncionario b on b.codTipoFuncionario = a.codTipoFuncionariofk " +
                          "left join Loja c on c.codLoja = a.codLojafk ";




        public List<Funcionario> DadosFuncionario()
        {
            //Cria uma lista do tipo Raça - Array
            List<Funcionario> lista_Funcionario = new List<Funcionario>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_Funcionario;
            conn.Open();
            try
            {
                dr_Funcionario = cmd.ExecuteReader();
                while (dr_Funcionario.Read())
                {
                    Funcionario aux = new Funcionario();
                    aux.ToString();
                    aux.codfuncionario = Int32.Parse(dr_Funcionario["codFuncionario"].ToString());
                    aux.nomefuncionario = dr_Funcionario["nomeFuncionario"].ToString();
                    Tipofuncionario TipoFuncionario = new Tipofuncionario();
                    TipoFuncionario.nometipofuncionario = dr_Funcionario["nomeTipoFuncionario"].ToString();
                    aux.tipofuncionario = TipoFuncionario;
                    Loja Loja = new Loja();
                    Loja.nomeloja = dr_Funcionario["nomeLoja"].ToString();
                    aux.loja = Loja;


                    lista_Funcionario.Add(aux);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return lista_Funcionario;
        }
        public List<Funcionario> DadosFuncionarioFiltro(String parametro)
        {
            //Cria uma lista do tipo Raça - Array
            List<Funcionario> lista_Funcionario = new List<Funcionario>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("pnomeFuncionario", parametro + "%");

            SqlDataReader dr_Funcionario;
            conn.Open();
            try
            {
                dr_Funcionario = cmd.ExecuteReader();
                while (dr_Funcionario.Read())
                {
                    Funcionario aux = new Funcionario();
                    aux.codfuncionario = Int32.Parse(dr_Funcionario["codFuncionario"].ToString());
                    aux.nomefuncionario = dr_Funcionario["nomeFuncionario"].ToString();
                    Tipofuncionario TipoFuncionario = new Tipofuncionario();
                    TipoFuncionario.nometipofuncionario = dr_Funcionario["nomeTipoFuncionario"].ToString();
                    aux.tipofuncionario = TipoFuncionario;
                    Loja Loja = new Loja();
                    Loja.nomeloja = dr_Funcionario["nomeLoja"].ToString();
                    aux.loja = Loja;
                    lista_Funcionario.Add(aux);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return lista_Funcionario;
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
                MessageBox.Show(ex.Message);
            }

            finally
            {
                conn.Close();
            }
        }

        String sqlFiltro = "select a.codFuncionario,a.nomeFuncionario,b.nomeTipoFuncionario " +
            "from Funcionario a " +
            "left join TipoFuncionario b on b.codTipoFuncionario = b.codTipoFuncionariofk " +
            "left join Loja c on c.codLoja = c.codLojafk " +
            "where a.nomeFuncionario like @pnomeFuncionario";

        public DataTable Buscar_Filtro(String pFuncionario)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("pnomeFuncionario", pFuncionario);

            conn.Open();

            da_Funcionario = new SqlDataAdapter(cmd);

            dt_Funcionario = new DataTable();
            da_Funcionario.Fill(dt_Funcionario);

            conn.Close();
            return dt_Funcionario;
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

            da_Funcionario = new SqlDataAdapter(cmd);

            dt_Funcionario = new DataTable();
            da_Funcionario.Fill(dt_Funcionario);

            return dt_Funcionario;
        }
        public void Insere_Dados(object aux)
        {
            Funcionario Funcionario = new Funcionario();
            Funcionario = (Funcionario)aux;

            //criando a Conexao com o banco de dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pnome", Funcionario.nomefuncionario);
            cmd.Parameters.AddWithValue("@pTipoFuncionario", Funcionario.tipofuncionario.codtipofuncionario);
            cmd.Parameters.AddWithValue("@pLoja", Funcionario.loja.codloja);


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
                MessageBox.Show(ex.Message);
            }

            finally
            {
                conn.Close();
            }

        }
        public void Atualiza_Dados(object aux)
        {
            Funcionario dados = new Funcionario();
            dados = (Funcionario)aux;

            //criando a Conexao com o banco de dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcod", dados.codfuncionario);
            cmd.Parameters.AddWithValue("@pnome", dados.nomefuncionario);
            cmd.Parameters.AddWithValue("@pTipoFuncionario", dados.tipofuncionario.codtipofuncionario);
            cmd.Parameters.AddWithValue("@pLoja", dados.loja.codloja);


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
                MessageBox.Show(ex.Message);
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
