using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Veterinaria.conection;
using Veterinaria.model;

namespace Veterinaria.control
{
    internal class C_ClienteTelefone : I_Metodos_Comuns
    {
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_clienteTelefone;
        SqlDataAdapter da_clienteTelefone;

        private string sqlInserirClienteTelefone = "INSERT INTO clientetelefone (codtelefonefk, codclientefk) VALUES (@codTelefone, @codCliente)";
        private string sqlAtualizarClienteTelefone = "UPDATE clientetelefone SET codtelefonefk = @codTelefone WHERE codclientefk = @codCliente";
        private string sqlTodos = "SELECT ct.codtelefonefk, ct.codclientefk, t.numerotelefone, c.nomecliente " +
                                  "FROM clientetelefone ct " +
                                  "LEFT JOIN telefone t ON t.codtelefone = ct.codtelefonefk " +
                                  "LEFT JOIN cliente c ON c.codcliente = ct.codclientefk";

        public List<Clientetelefone> DadosClienteTelefone()
        {
            List<Clientetelefone> lista_clienteTelefone = new List<Clientetelefone>();
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            try
            {
                conn.Open();
                SqlDataReader dr_clienteTelefone = cmd.ExecuteReader();
                while (dr_clienteTelefone.Read())
                {
                    Clientetelefone clienteTelefone = new Clientetelefone
                    {
                        telefone = new Telefone
                        {
                            codtelefone = int.Parse(dr_clienteTelefone["codtelefonefk"].ToString()),
                            numerotelefone = dr_clienteTelefone["numerotelefone"].ToString()
                        },
                        cliente = new Cliente
                        {
                            codcliente = int.Parse(dr_clienteTelefone["codclientefk"].ToString()),
                            nomecliente = dr_clienteTelefone["nomecliente"].ToString()
                        }
                    };
                    lista_clienteTelefone.Add(clienteTelefone);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados de cliente e telefone: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return lista_clienteTelefone;
        }

        public void InserirClienteTelefone(Clientetelefone clienteTelefone)
        {
            if (clienteTelefone == null || clienteTelefone.cliente == null || clienteTelefone.telefone == null)
            {
                MessageBox.Show("Erro: O objeto 'clienteTelefone', 'cliente' ou 'telefone' está nulo.");
                return;
            }

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlInserirClienteTelefone, conn);

            cmd.Parameters.AddWithValue("@codTelefone", clienteTelefone.telefone.codtelefone);
            cmd.Parameters.AddWithValue("@codCliente", clienteTelefone.cliente.codcliente);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("inseriu");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar associação de cliente e telefone: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void AtualizarClienteTelefone(Clientetelefone clienteTelefone)
        {
            if (clienteTelefone == null || clienteTelefone.cliente == null || clienteTelefone.telefone == null)
            {
                throw new ArgumentNullException("Os objetos 'clienteTelefone', 'cliente' ou 'telefone' não podem ser nulos.");
            }

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlAtualizarClienteTelefone, conn);

            cmd.Parameters.AddWithValue("@codTelefone", clienteTelefone.telefone.codtelefone);
            cmd.Parameters.AddWithValue("@codCliente", clienteTelefone.cliente.codcliente);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Associação de cliente e telefone atualizada com sucesso.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar associação de cliente e telefone: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void Apaga_Dados(int codTelefone, int codCliente)
        {
            string query = "DELETE FROM clientetelefone WHERE codtelefonefk = @codTelefone AND codclientefk = @codCliente";

            Conexao conexao = new Conexao();
            using (var conn = conexao.ConectarBanco())
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@codTelefone", codTelefone);
                    cmd.Parameters.AddWithValue("@codCliente", codCliente);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
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
            if (aux is Clientetelefone clienteTelefone)
            {
                AtualizarClienteTelefone(clienteTelefone);
            }
            else
            {
                throw new ArgumentException("O objeto fornecido não é uma instância válida de Clientetelefone.");
            }
        }

        public void Insere_Dados(object aux)
        {
            if (aux is Clientetelefone clienteTelefone)
            {
                InserirClienteTelefone(clienteTelefone);
            }
            else
            {
                throw new ArgumentException("O objeto fornecido não é uma instância válida de Clientetelefone.");
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
