using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Veterinaria.conection;
using Veterinaria.model;

namespace Veterinaria.control
{
    internal class C_Loja : I_Metodos_Comuns
    {
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_loja;
        SqlDataAdapter da_loja;

        String sqlInsere = "insert into loja (nomeloja, codbairrofk, codruafk, codcepfk, codcidadefk, codestadofk, codpaisfk, numeroloja, cnpj) values (@pnome, @pcodbairro, @pcodrua, @pcodcep, @pcodcidade, @pcodestado, @pcodpais, @pnumero, @pcnpj)";
        String sqlApaga = "delete from loja where codloja = @pcod";
        String sqlAtualiza = "update loja set nomeloja = @pnome, codbairrofk = @pbairro, codruafk = @prua, codcepfk = @pcep, codcidadefk = @pcidade, codestadofk = @pestado, codpaisfk = @ppais, numeroloja = @pnumero, cnpj = @pcnpj where codloja = @pcod";
        String sqlTodos = "select * from loja";
        String sqlFiltro = "select * from loja where nomeloja like @pnomeloja";

        public List<Loja> DadosLoja()
        {
            List<Loja> lista_loja = new List<Loja>();
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            try
            {
                conn.Open();
                SqlDataReader dr_loja = cmd.ExecuteReader();

                while (dr_loja.Read())
                {
                    Loja aux = new Loja
                    {
                        codloja = Int32.Parse(dr_loja["codloja"].ToString()),
                        nomeloja = dr_loja["nomeloja"].ToString()
                    };
                    lista_loja.Add(aux);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar dados: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return lista_loja;
        }

        public List<Loja> DadosLojaFiltro(string parametro)
        {
            List<Loja> lista_loja = new List<Loja>();
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("@pnomeloja", parametro + "%");

            try
            {
                conn.Open();
                SqlDataReader dr_loja = cmd.ExecuteReader();

                while (dr_loja.Read())
                {
                    Loja aux = new Loja
                    {
                        codloja = Int32.Parse(dr_loja["codloja"].ToString()),
                        nomeloja = dr_loja["nomeloja"].ToString()
                    };
                    lista_loja.Add(aux);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao filtrar dados: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return lista_loja;
        }

        public void Apaga_Dados(int aux)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlApaga, conn);
            cmd.Parameters.AddWithValue("@pcod", aux);

            try
            {
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Apagado com sucesso");
                }
                else
                {
                    MessageBox.Show("Nenhum registro foi apagado");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao apagar dados: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void Atualizar_Dados(object aux)
        {
            Loja dados = (Loja)aux;
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcod", dados.codloja);
            cmd.Parameters.AddWithValue("@pnome", dados.nomeloja);
            cmd.Parameters.AddWithValue("@pbairro", dados.bairro);
            cmd.Parameters.AddWithValue("@prua", dados.rua);
            cmd.Parameters.AddWithValue("@pcep", dados.cep);
            cmd.Parameters.AddWithValue("@pcidade", dados.cidade);
            cmd.Parameters.AddWithValue("@pestado", dados.estado);
            cmd.Parameters.AddWithValue("@ppais", dados.pais);
            cmd.Parameters.AddWithValue("@pnumero", dados.numeroloja);
            cmd.Parameters.AddWithValue("@pcnpj", dados.cnpj);

            try
            {
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Atualizado com sucesso");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar dados: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable Buscar_Filtro(string ploja)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("@pnomeloja", ploja);

            try
            {
                conn.Open();
                da_loja = new SqlDataAdapter(cmd);
                dt_loja = new DataTable();
                da_loja.Fill(dt_loja);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar filtro: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt_loja;
        }

        public object Buscar_Id(int valor)
        {
            Loja loja = null;
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            string sqlBuscaId = "select * from loja where codloja = @pcod";
            cmd = new SqlCommand(sqlBuscaId, conn);
            cmd.Parameters.AddWithValue("@pcod", valor);

            try
            {
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    loja = new Loja
                    {
                        codloja = Int32.Parse(dr["codloja"].ToString()),
                        nomeloja = dr["nomeloja"].ToString(),
                        // Adicionar os outros campos conforme necessário
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar dados por ID: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return loja;
        }

        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            try
            {
                conn.Open();
                da_loja = new SqlDataAdapter(cmd);
                dt_loja = new DataTable();
                da_loja.Fill(dt_loja);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar todos os dados: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt_loja;
        }

        public void Insere_Dados(object aux)
        {
            Loja loja = (Loja)aux;
            Conexao conexao = new Conexao();
            SqlConnection conn = conexao.ConectarBanco();

            // Definindo o comando SQL de inserção
            string sqlInsere = "INSERT INTO Loja (nomeloja, codbairrofk, codruafk, codcepfk, codcidadefk, codestadofk, codpaisfk, numeroloja, cnpj) " +
                               "VALUES (@pnome, @pcodbairro, @pcodrua, @pcodcep, @pcodcidade, @pcodestado, @pcodpais, @pnumero, @pcnpj)";

            SqlCommand cmd = new SqlCommand(sqlInsere, conn);

            // Adicionando parâmetros ao comando SQL
            cmd.Parameters.AddWithValue("@pnome", loja.nomeloja);
            cmd.Parameters.AddWithValue("@pcodbairro", loja.bairro.codbairro);
            cmd.Parameters.AddWithValue("@pcodrua", loja.rua.codrua);
            cmd.Parameters.AddWithValue("@pcodcep", loja.cep.codcep);
            cmd.Parameters.AddWithValue("@pcodcidade", loja.cidade.codcidade);
            cmd.Parameters.AddWithValue("@pcodestado", loja.estado.codestado);
            cmd.Parameters.AddWithValue("@pcodpais", loja.pais.codpais);
            cmd.Parameters.AddWithValue("@pnumero", loja.numeroloja);
            cmd.Parameters.AddWithValue("@pcnpj", loja.cnpj);

            try
            {
                conn.Open(); 
                int i = cmd.ExecuteNonQuery(); 

                if (i > 0)
                {
                    MessageBox.Show("Inserido com sucesso"); 
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Erro ao inserir dados: " + ex.Message); 
            }
            finally
            {
                conn.Close(); 
            }
        }
    }
}