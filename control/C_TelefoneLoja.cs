using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Veterinaria.conection;
using Veterinaria.model;

namespace Veterinaria.control
{
    internal class C_TelefoneLoja : I_Metodos_Comuns
    {
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_telefoneLoja;
        SqlDataAdapter da_telefoneLoja;

        private string sqlInserirTelefoneLoja = "INSERT INTO telefoneloja (codtelefonefk, codlojafk) VALUES (@codTelefone, @codLoja)";
        private string sqlAtualizarTelefoneLoja = "UPDATE telefoneloja SET codtelefonefk = @codTelefone WHERE codlojafk = @codLoja";
        private string sqlTodos = "SELECT tl.codtelefonefk, tl.codlojafk, t.numerotelefone, l.nomeloja " +
                                  "FROM telefoneloja tl " +
                                  "LEFT JOIN telefone t ON t.codtelefone = tl.codtelefonefk " +
                                  "LEFT JOIN loja l ON l.codloja = tl.codlojafk";

        public List<Telefoneloja> DadosTelefoneLoja()
        {
            List<Telefoneloja> lista_telefoneLoja = new List<Telefoneloja>();
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            try
            {
                conn.Open();
                SqlDataReader dr_telefoneLoja = cmd.ExecuteReader();
                while (dr_telefoneLoja.Read())
                {
                    Telefoneloja telefoneLoja = new Telefoneloja
                    {
                        telefone = new Telefone
                        {
                            codtelefone = int.Parse(dr_telefoneLoja["codtelefonefk"].ToString()),
                            numerotelefone = dr_telefoneLoja["numerotelefone"].ToString()
                        },
                        loja = new Loja
                        {
                            codloja = int.Parse(dr_telefoneLoja["codlojafk"].ToString()),
                            nomeloja = dr_telefoneLoja["nomeloja"].ToString()
                        }
                    };
                    lista_telefoneLoja.Add(telefoneLoja);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados de telefone e loja: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return lista_telefoneLoja;
        }

        public void InserirTelefoneLoja(Telefoneloja telefoneLoja)
        {
            if (telefoneLoja == null || telefoneLoja.loja == null || telefoneLoja.telefone == null)
            {
                MessageBox.Show("Erro: O objeto 'telefoneLoja', 'loja' ou 'telefone' está nulo.");
                return;
            }

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlInserirTelefoneLoja, conn);

            cmd.Parameters.AddWithValue("@codTelefone", telefoneLoja.telefone.codtelefone);
            cmd.Parameters.AddWithValue("@codLoja", telefoneLoja.loja.codloja);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("inseriu");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar associação de telefone e loja: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void AtualizarTelefoneLoja(Telefoneloja telefoneLoja)
        {
            if (telefoneLoja == null || telefoneLoja.loja == null || telefoneLoja.telefone == null)
            {
                throw new ArgumentNullException("Os objetos 'telefoneLoja', 'loja' ou 'telefone' não podem ser nulos.");
            }

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlAtualizarTelefoneLoja, conn);

            cmd.Parameters.AddWithValue("@codTelefone", telefoneLoja.telefone.codtelefone);
            cmd.Parameters.AddWithValue("@codLoja", telefoneLoja.loja.codloja);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Associação de telefone e loja atualizada com sucesso.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar associação de telefone e loja: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void Apaga_Dados(int codTelefone, int codLoja)
        {
            string query = "DELETE FROM telefoneloja WHERE codtelefonefk = @codTelefone AND codlojafk = @codLoja";

            Conexao conexao = new Conexao();
            using (var conn = conexao.ConectarBanco())
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@codTelefone", codTelefone);
                    cmd.Parameters.AddWithValue("@codLoja", codLoja);

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
            if (aux is Telefoneloja telefoneLoja)
            {
                AtualizarTelefoneLoja(telefoneLoja);
            }
            else
            {
                throw new ArgumentException("O objeto fornecido não é uma instância válida de Telefoneloja.");
            }
        }

        public void Insere_Dados(object aux)
        {
            if (aux is Telefoneloja telefoneLoja)
            {
                InserirTelefoneLoja(telefoneLoja);
            }
            else
            {
                throw new ArgumentException("O objeto fornecido não é uma instância válida de Telefoneloja.");
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
