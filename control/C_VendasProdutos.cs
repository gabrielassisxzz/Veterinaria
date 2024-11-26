using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Veterinaria.conection;
using Veterinaria.model;

namespace Veterinaria.control
{
    internal class C_VendasProdutos : I_Metodos_Comuns
    {
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_vendasProdutos;
        SqlDataAdapter da_vendasProdutos;

        private string sqlInserirVendaProduto = "INSERT INTO vendasprodutos (codvendafk, codprodutofk, quantv, valorv) VALUES (@codVenda, @codProduto, @quantidade, @valor)";
        private string sqlAtualizarVendaProduto = "UPDATE vendasprodutos SET quantv = @quantidade, valorv = @valor WHERE codvendafk = @codVenda AND codprodutofk = @codProduto";
        private string sqlTodos = "SELECT vp.codvendafk, vp.codprodutofk, vp.quantv, vp.valorv, v.datavenda, p.nomeproduto " +
                                  "FROM vendasprodutos vp " +
                                  "LEFT JOIN vendas v ON v.codvenda = vp.codvendafk " +
                                  "LEFT JOIN produto p ON p.codproduto = vp.codprodutofk";

        public List<VendasProdutos> DadosVendasProdutos()
        {
            List<VendasProdutos> lista_vendasProdutos = new List<VendasProdutos>();
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            try
            {
                conn.Open();
                SqlDataReader dr_vendasProdutos = cmd.ExecuteReader();
                while (dr_vendasProdutos.Read())
                {
                    VendasProdutos vendaProdutos = new VendasProdutos
                    {
                        vendas = new Vendas { codvenda = int.Parse(dr_vendasProdutos["codvendafk"].ToString()) },
                        produto = new Produto
                        {
                            codproduto = int.Parse(dr_vendasProdutos["codprodutofk"].ToString()),
                            nomeproduto = dr_vendasProdutos["nomeproduto"].ToString()
                        },
                        quantidadevendasprodutos = double.Parse(dr_vendasProdutos["quantv"].ToString()),
                        valorvendaprodutos = double.Parse(dr_vendasProdutos["valorv"].ToString())
                    };
                    lista_vendasProdutos.Add(vendaProdutos);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar vendas de produtos: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return lista_vendasProdutos;
        }

        public void InserirVendaProduto(VendasProdutos venda)
        {
            if (venda == null || venda.vendas == null || venda.produto == null)
            {
                MessageBox.Show("Erro: O objeto 'venda', 'vendas' ou 'produto' está nulo.");
                return;
            }

            if (!VerificarVendaExiste(venda.vendas.codvenda))
            {
                MessageBox.Show("Erro: A venda associada não existe.");
                return;
            }

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlInserirVendaProduto, conn);

            cmd.Parameters.AddWithValue("@codVenda", venda.vendas.codvenda);
            cmd.Parameters.AddWithValue("@codProduto", venda.produto.codproduto);
            cmd.Parameters.AddWithValue("@quantidade", venda.quantidadevendasprodutos);
            cmd.Parameters.AddWithValue("@valor", venda.valorvendaprodutos);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Inseriu");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar venda: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        private bool VerificarVendaExiste(int codVenda)
        {
            string sqlVerificaVenda = "SELECT COUNT(*) FROM vendas WHERE codvenda = @codVenda";
            using (SqlConnection conn = new Conexao().ConectarBanco())
            using (SqlCommand cmd = new SqlCommand(sqlVerificaVenda, conn))
            {
                cmd.Parameters.AddWithValue("@codVenda", codVenda);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }


        public void AtualizarVendaProduto(VendasProdutos venda)
        {
            if (venda == null || venda.vendas == null || venda.produto == null)
            {
                throw new ArgumentNullException("Os objetos 'venda', 'vendas' ou 'produto' não podem ser nulos.");
            }

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlAtualizarVendaProduto, conn);

            cmd.Parameters.AddWithValue("@codVenda", venda.vendas.codvenda);
            cmd.Parameters.AddWithValue("@codProduto", venda.produto.codproduto);
            cmd.Parameters.AddWithValue("@quantidade", venda.quantidadevendasprodutos);
            cmd.Parameters.AddWithValue("@valor", venda.valorvendaprodutos);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Venda de produto atualizada com sucesso.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar venda de produto: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void Apaga_Dados(int idVenda)
        {
            string sqlDelete = "DELETE FROM vendasprodutos WHERE codvendafk = @idVenda";
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlDelete, conn);
            cmd.Parameters.AddWithValue("@idVenda", idVenda);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Venda apagada com sucesso.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao apagar venda: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public object Buscar_Id(int codVenda)
        {
            VendasProdutos vendaProduto = null;
            string sql = "SELECT vp.codvendafk, vp.codprodutofk, vp.quantv, vp.valorv, v.datavenda, p.nomeproduto " +
                         "FROM vendasprodutos vp " +
                         "LEFT JOIN vendas v ON v.codvenda = vp.codvendafk " +
                         "LEFT JOIN produto p ON p.codproduto = vp.codprodutofk " +
                         "WHERE vp.codvendafk = @codVenda";

            using (SqlConnection conn = new Conexao().ConectarBanco())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@codVenda", codVenda);
                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        vendaProduto = new VendasProdutos
                        {
                            vendas = new Vendas { codvenda = int.Parse(dr["codvendafk"].ToString()) },
                            produto = new Produto
                            {
                                codproduto = int.Parse(dr["codprodutofk"].ToString()),
                                nomeproduto = dr["nomeproduto"].ToString()
                            },
                            quantidadevendasprodutos = double.Parse(dr["quantv"].ToString()),
                            valorvendaprodutos = double.Parse(dr["valorv"].ToString())
                        };
                    }
                }
            }

            return vendaProduto;
        }


        public DataTable Buscar_Todos()
        {
            DataTable tabela = new DataTable();
            string sql = "SELECT vp.codvendafk, vp.codprodutofk, vp.quantv, vp.valorv, v.datavenda, p.nomeproduto " +
                         "FROM vendasprodutos vp " +
                         "LEFT JOIN vendas v ON v.codvenda = vp.codvendafk " +
                         "LEFT JOIN produto p ON p.codproduto = vp.codprodutofk";

            using (SqlConnection conn = new Conexao().ConectarBanco())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(tabela);
            }

            return tabela;
        }


        public DataTable Buscar_Filtro(string dados)
        {
            DataTable tabela = new DataTable();
            string sql = "SELECT vp.codvendafk, vp.codprodutofk, vp.quantv, vp.valorv, v.datavenda, p.nomeproduto " +
                         "FROM vendasprodutos vp " +
                         "LEFT JOIN vendas v ON v.codvenda = vp.codvendafk " +
                         "LEFT JOIN produto p ON p.codproduto = vp.codprodutofk " +
                         "WHERE p.nomeproduto LIKE @dados OR v.datavenda LIKE @dados";

            using (SqlConnection conn = new Conexao().ConectarBanco())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@dados", "%" + dados + "%");
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(tabela);
                }
            }

            return tabela;
        }


        public void Atualizar_Dados(object aux)
        {
            if (aux is VendasProdutos venda)
            {
                AtualizarVendaProduto(venda);
            }
            else
            {
                throw new ArgumentException("O objeto fornecido não é uma instância válida de VendasProdutos.");
            }
        }


        public void Insere_Dados(object aux)
        {
            if (aux is VendasProdutos venda)
            {
                InserirVendaProduto(venda);
            }
            else
            {
                throw new ArgumentException("O objeto fornecido não é uma instância válida de VendasProdutos.");
            }
        }

    }
}
