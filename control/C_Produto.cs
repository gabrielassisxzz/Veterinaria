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
    internal class C_Produto : I_Metodos_Comuns
    {
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_produto;
        SqlDataAdapter da_produto;

        String sqlInsere = "insert into produto(nomeproduto, codmarcafk, quantidade, valor, codtipoprodutofk) values(@pnome, @pcodmarca, @pquantidade, @pvalor, @pcodtipoproduto)";
        String sqlApaga = "delete from produto where codproduto = @pcod";
        String sqlAtualiza = "update produto set nomeproduto = @pnome, codmarcafk = @pcodmarca, quantidade = @pquantidade, valor = @pvalor, codtipoprodutofk = @pcodtipoproduto where codproduto = @pcod";
        String sqlTodos = "select p.codproduto, p.nomeproduto, m.nomemarca, p.quantidade, p.valor, t.nometipoproduto " +
                          "from produto p " +
                          "left join marca m on m.codmarca = p.codmarcafk " +
                          "left join tipoproduto t on t.codtipoproduto = p.codtipoprodutofk";
        String sqlFiltro = "select p.codproduto, p.nomeproduto, m.nomemarca, p.quantidade, p.valor, t.nometipoproduto " +
                           "from produto p " +
                           "left join marca m on m.codmarca = p.codmarcafk " +
                           "left join tipoproduto t on t.codtipoprodutofk = p.codtipoproduto " +
                           "where p.nomeproduto like @pnomeproduto";

        public List<Produto> DadosProduto()
        {
            List<Produto> lista_produto = new List<Produto>();
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_produto;
            conn.Open();
            try
            {

                dr_produto = cmd.ExecuteReader();
                while (dr_produto.Read())
                {
                    Produto aux = new Produto();
                        aux.ToString();
                    aux.codproduto = Int32.Parse(dr_produto["codproduto"].ToString());
                    aux.nomeproduto = dr_produto["nomeproduto"].ToString();
                    Produto quantidade = new Produto();
                    aux.quantidadeproduto = double.Parse(dr_produto["quantidade"].ToString());
                    Produto valor = new Produto();
                    aux.valorproduto = double.Parse(dr_produto["valor"].ToString());
                    Marca marca = new Marca();
                    aux.marca = new Marca { nomemarca = dr_produto["nomemarca"].ToString() };
                    aux.tipoproduto = new Tipoproduto { nometipoproduto = dr_produto["nometipoproduto"].ToString() };
                    lista_produto.Add(aux);
                }
            }


            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar produtos: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return lista_produto;
        }

        public List<Produto> DadosProdutoFiltro(string nomeProduto)
        {
            List<Produto> lista_produto = new List<Produto>();
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("@pnomeproduto", nomeProduto + "%");

            try
            {
                conn.Open();
                SqlDataReader dr_produto = cmd.ExecuteReader();
                while (dr_produto.Read())
                {
                    Produto produto = new Produto
                    {
                        codproduto = int.Parse(dr_produto["codproduto"].ToString()),
                        nomeproduto = dr_produto["nomeproduto"].ToString(),
                        quantidadeproduto = double.Parse(dr_produto["quantidade"].ToString()),
                        valorproduto = double.Parse(dr_produto["valor"].ToString()),
                        marca = new Marca { nomemarca = dr_produto["nomemarca"].ToString() },
                        tipoproduto = new Tipoproduto { nometipoproduto = dr_produto["nometipoproduto"].ToString() }
                    };
                    lista_produto.Add(produto);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar produtos: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return lista_produto;
        }

        public void Apaga_Dados(int codProduto)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlApaga, conn);
            cmd.Parameters.AddWithValue("@pcod", codProduto);

            try
            {
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("Apaguei");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao apagar produto: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);
            da_produto = new SqlDataAdapter(cmd);
            dt_produto = new DataTable();

            try
            {
                da_produto.Fill(dt_produto);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar produtos: " + ex.Message);
            }

            return dt_produto;
        }

        public void InserirProduto(Produto produto)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pnome", produto.nomeproduto);
            cmd.Parameters.AddWithValue("@pcodmarca", produto.marca.codmarca);
            cmd.Parameters.AddWithValue("@pquantidade", produto.quantidadeproduto);
            cmd.Parameters.AddWithValue("@pvalor", produto.valorproduto);
            cmd.Parameters.AddWithValue("@pcodtipoproduto", produto.tipoproduto.codtipoproduto);

            try
            {
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("Inseriu");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inserir produto: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void AtualizarProduto(Produto produto)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcod", produto.codproduto);
            cmd.Parameters.AddWithValue("@pnome", produto.nomeproduto);
            cmd.Parameters.AddWithValue("@pcodmarca", produto.marca.codmarca);
            cmd.Parameters.AddWithValue("@pquantidade", produto.quantidadeproduto);
            cmd.Parameters.AddWithValue("@pvalor", produto.valorproduto);
            cmd.Parameters.AddWithValue("@pcodtipoproduto", produto.tipoproduto.codtipoproduto);

            try
            {
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("Produto atualizado com sucesso!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar produto: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void Insere_Dados(object aux)
        {
            throw new NotImplementedException();
        }

        public object Buscar_Id(int valor)
        {
            throw new NotImplementedException();
        }

        public DataTable Buscar_Filtro(string dados)
        {
            throw new NotImplementedException();
        }

        public void Atualizar_Dados(object aux)
        {
            throw new NotImplementedException();
        }
    }
}

