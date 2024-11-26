using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Veterinaria.conection;
using Veterinaria.model;

namespace Veterinaria.control
{
    internal class C_ItensVendaServico : I_Metodos_Comuns
    {
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_itensVendaServico;
        SqlDataAdapter da_itensVendaServico;

        private string sqlInserirItensVendaServico = "INSERT INTO itensvendaservico (codtiposervicofk, codvendaservicofk, quant, valor, codcidanimalfk) VALUES (@codTipoServico, @codVendaServico, @quantidade, @valor, @codCidAnimal)";
        private string sqlAtualizarItensVendaServico = "UPDATE itensvendaservico SET quant = @quantidade, valor = @valor, codcidanimalfk = @codCidAnimal WHERE codtiposervicofk = @codTipoServico AND codvendaservicofk = @codVendaServico";
        private string sqlTodos = "SELECT ivs.codtiposervicofk, ivs.codvendaservicofk, ivs.quant, ivs.valor, ivs.codcidanimalfk, ca.nomecidanimal " +
                                  "FROM itensvendaservico ivs " +
                                  "LEFT JOIN tiposervico ts ON ts.codtiposervico = ivs.codtiposervicofk " +
                                  "LEFT JOIN vendaservico vs ON vs.codvendaservico = ivs.codvendaservicofk " +
                                  "LEFT JOIN cidanimal ca ON ca.codcidanimal = ivs.codcidanimalfk";






        public List<Itensvendaservico> DadosItensVendaServico()
        {
            List<Itensvendaservico> lista_itensVendaServico = new List<Itensvendaservico>();
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            try
            {
                conn.Open();
                SqlDataReader dr_itens = cmd.ExecuteReader();
                while (dr_itens.Read())
                {
                    Itensvendaservico item = new Itensvendaservico
                    {
                        tiposervico = new Tiposervico
                        {
                            codtiposervico = int.Parse(dr_itens["codtiposervicofk"].ToString()),

                        },
                        vendaservico = new VendaServico
                        {
                            codvendaservico = int.Parse(dr_itens["codvendaservicofk"].ToString()),

                        },
                        cidanimal = new CidAnimal
                        {
                            Codcidanimal = int.Parse(dr_itens["codcidanimalfk"].ToString()),
                            Nomecidanimal = dr_itens["nomecidanimal"].ToString()
                        },
                        quantidade = double.Parse(dr_itens["quant"].ToString()),
                        valor = double.Parse(dr_itens["valor"].ToString())
                    };
                    lista_itensVendaServico.Add(item);
                }
            }
            catch (Exception ex)
            {
               
            }
            finally
            {
                conn.Close();
            }

            return lista_itensVendaServico;
        }

        public void InserirItensVendaServico(Itensvendaservico item)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlInserirItensVendaServico, conn);

            cmd.Parameters.AddWithValue("@codTipoServico", item.tiposervico.codtiposervico);
            cmd.Parameters.AddWithValue("@codVendaServico", item.vendaservico.codvendaservico);
            cmd.Parameters.AddWithValue("@quantidade", item.quantidade);
            cmd.Parameters.AddWithValue("@valor", item.valor);
            cmd.Parameters.AddWithValue("@codCidAnimal", item.cidanimal.Codcidanimal);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Inseriu");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inserir item: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void AtualizarItensVendaServico(Itensvendaservico item)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlAtualizarItensVendaServico, conn);

            cmd.Parameters.AddWithValue("@codTipoServico", item.tiposervico.codtiposervico);
            cmd.Parameters.AddWithValue("@codVendaServico", item.vendaservico.codvendaservico);
            cmd.Parameters.AddWithValue("@quantidade", item.quantidade);
            cmd.Parameters.AddWithValue("@valor", item.valor);
            cmd.Parameters.AddWithValue("@codCidAnimal", item.cidanimal.Codcidanimal);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Atualizei.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar item: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void Apaga_Dados(int idVendaServico)
        {
            string sqlDelete = "DELETE FROM itensvendaservico WHERE codvendaservicofk = @idVendaServico";
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlDelete, conn);
            cmd.Parameters.AddWithValue("@idVendaServico", idVendaServico);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Apaguei");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao apagar item: " + ex.Message);
            }
            finally
            {
                conn.Close();
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




        public void Insere_Dados(object aux)
        {
            if (aux is Itensvendaservico item)
            {
                InserirItensVendaServico(item);
            }
            else
            {
                
            }
        }

        public void Atualizar_Dados(object aux)
        {
            if (aux is Itensvendaservico item)
            {
                AtualizarItensVendaServico(item);
            }
            else
            {
                
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

    }
}