using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Veterinaria.conection;
using Veterinaria.model;

namespace Veterinaria.control
{
    internal class C_FuncionarioTelefone : I_Metodos_Comuns
    {
        SqlConnection conn;
        SqlCommand cmd;
        private string sqlInserirFuncionarioTelefone = "INSERT INTO funcionariotelefone (codtelefonefk, codfuncionariofk) VALUES (@codTelefone, @codFuncionario)";
        private string sqlAtualizarFuncionarioTelefone = "UPDATE funcionariotelefone SET codtelefonefk = @codTelefone WHERE codfuncionariofk = @codFuncionario";
        private string sqlTodos = "SELECT ft.codtelefonefk, ft.codfuncionariofk, t.numerotelefone, f.nomefuncionario " +
                                  "FROM funcionariotelefone ft " +
                                  "LEFT JOIN telefone t ON t.codtelefone = ft.codtelefonefk " +
                                  "LEFT JOIN funcionario f ON f.codfuncionario = ft.codfuncionariofk";

        public List<Funcionariotelefone> DadosFuncionarioTelefone()
        {
            List<Funcionariotelefone> lista_funcionarioTelefone = new List<Funcionariotelefone>();
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            try
            {
                conn.Open();
                SqlDataReader dr_funcionarioTelefone = cmd.ExecuteReader();
                while (dr_funcionarioTelefone.Read())
                {
                    Funcionariotelefone funcionarioTelefone = new Funcionariotelefone
                    {
                        telefone = new Telefone
                        {
                            codtelefone = int.Parse(dr_funcionarioTelefone["codtelefonefk"].ToString()),
                            numerotelefone = dr_funcionarioTelefone["numerotelefone"].ToString()
                        },
                        funcionario = new Funcionario
                        {
                            codfuncionario = int.Parse(dr_funcionarioTelefone["codfuncionariofk"].ToString()),
                            nomefuncionario = dr_funcionarioTelefone["nomefuncionario"].ToString()
                        }
                    };
                    lista_funcionarioTelefone.Add(funcionarioTelefone);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados de telefone e funcionário: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return lista_funcionarioTelefone;
        }

        public void InserirFuncionarioTelefone(Funcionariotelefone funcionarioTelefone)
        {
            if (funcionarioTelefone == null || funcionarioTelefone.telefone == null || funcionarioTelefone.funcionario == null)
            {
                MessageBox.Show("Erro: O objeto 'funcionarioTelefone', 'telefone' ou 'funcionario' está nulo.");
                return;
            }

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlInserirFuncionarioTelefone, conn);

            cmd.Parameters.AddWithValue("@codTelefone", funcionarioTelefone.telefone.codtelefone);
            cmd.Parameters.AddWithValue("@codFuncionario", funcionarioTelefone.funcionario.codfuncionario);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("inseriu");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar associação de telefone e funcionário: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void AtualizarFuncionarioTelefone(Funcionariotelefone funcionarioTelefone)
        {
            if (funcionarioTelefone == null || funcionarioTelefone.telefone == null || funcionarioTelefone.funcionario == null)
            {
                throw new ArgumentNullException("Os objetos 'funcionarioTelefone', 'telefone' ou 'funcionario' não podem ser nulos.");
            }

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlAtualizarFuncionarioTelefone, conn);

            cmd.Parameters.AddWithValue("@codTelefone", funcionarioTelefone.telefone.codtelefone);
            cmd.Parameters.AddWithValue("@codFuncionario", funcionarioTelefone.funcionario.codfuncionario);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("atualizei");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar associação de telefone e funcionário: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void Apaga_Dados(int codTelefone, int codFuncionario)
        {
            string query = "DELETE FROM funcionariotelefone WHERE codtelefonefk = @codTelefone AND codfuncionariofk = @codFuncionario";

            Conexao conexao = new Conexao();
            using (var conn = conexao.ConectarBanco())
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@codTelefone", codTelefone);
                    cmd.Parameters.AddWithValue("@codFuncionario", codFuncionario);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("apaguei");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Erro ao apagar dados: " + ex.Message);
                    }
                }
            }
        }

        public DataTable Buscar_Todos()
        {
            DataTable tabela = new DataTable();
            using (SqlConnection conn = new Conexao().ConectarBanco())
            using (SqlCommand cmd = new SqlCommand(sqlTodos, conn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(tabela);
            }

            return tabela;
        }

        public void Atualizar_Dados(object aux)
        {
            if (aux is Funcionariotelefone funcionarioTelefone)
            {
                AtualizarFuncionarioTelefone(funcionarioTelefone);
            }
            else
            {
                throw new ArgumentException("O objeto fornecido não é uma instância válida de Funcionariotelefone.");
            }
        }

        public void Insere_Dados(object aux)
        {
            if (aux is Funcionariotelefone funcionarioTelefone)
            {
                InserirFuncionarioTelefone(funcionarioTelefone);
            }
            else
            {
                throw new ArgumentException("O objeto fornecido não é uma instância válida de Funcionariotelefone.");
            }
        }

        public object Buscar_Id(int valor)
        {
            throw new NotImplementedException();
        }

        public DataTable Buscar_Filtro(string dados)
        {
            throw new NotImplementedException();
        }

        public void Apaga_Dados(int aux)
        {
            throw new NotImplementedException();
        }
    }
}
