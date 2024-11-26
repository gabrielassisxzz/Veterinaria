using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Veterinaria.conection;
using Veterinaria.model;

namespace Veterinaria.control
{
    internal class C_Imagens : I_Metodos_Comuns
    {
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_imagens;
        SqlDataAdapter da_imagens;

        String sqlInsere = "insert into Imagens(descricao, foto, produto_id) values(@pdescricao, @pfoto, @pcodProduto)";
        String sqlApaga = "delete from Imagens where codImagem = @pcod";
        String sqlAtualiza = "update Imagens set descricao = @pdescricao, foto = @pfoto, produto_id = @pcodProduto where codImagem = @pcod";
        String sqlTodos = "select * from Imagens";
        String sqlFiltro = "select * from Imagens where descricao like @parametro";

        
        public List<Imagens> DadosImagens()
        {
            List<Imagens> lista_imagens = new List<Imagens>();
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_imagens;
            conn.Open();
            try
            {
                dr_imagens = cmd.ExecuteReader();
                while (dr_imagens.Read())
                {
                    Imagens aux = new Imagens();
                    aux.codimagens = Int32.Parse(dr_imagens["codimagens"].ToString());
                    aux.descricao = dr_imagens["descricao"].ToString();
                    aux.foto = (byte[])dr_imagens["foto"];
                    aux.produto = new Produto { codproduto = Int32.Parse(dr_imagens["codProduto"].ToString()) };

                    lista_imagens.Add(aux);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Erro ao carregar imagens: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return lista_imagens;
        }

        
        public List<Imagens> BuscarImagens(string parametro)
        {
            List<Imagens> lista_imagens = new List<Imagens>();
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("@parametro", "%" + parametro + "%");

            SqlDataReader dr_imagens;
            conn.Open();
            try
            {
                dr_imagens = cmd.ExecuteReader();
                while (dr_imagens.Read())
                {
                    Imagens aux = new Imagens();
                    aux.codimagens = Int32.Parse(dr_imagens["codImagem"].ToString());
                    aux.descricao = dr_imagens["descricao"].ToString();
                    aux.foto = (byte[])dr_imagens["foto"];
                    aux.produto = new Produto { codproduto = Int32.Parse(dr_imagens["codProduto"].ToString()) };

                    lista_imagens.Add(aux);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar imagens: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return lista_imagens;
        }

        
        public void Insere_Dados(object aux)
        {
            Imagens imagem = (Imagens)aux;

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            string sqlInsere = "INSERT INTO Imagens (descricao, foto, codProduto) VALUES (@pdescricao, @pfoto, @pcodProduto)";
            cmd = new SqlCommand(sqlInsere, conn);

            cmd.Parameters.AddWithValue("@pdescricao", imagem.descricao);
            cmd.Parameters.AddWithValue("@pfoto", imagem.foto);
            cmd.Parameters.AddWithValue("@pcodProduto", imagem.produto.codproduto);


            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Imagem inserida com sucesso.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inserir a imagem: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }




        
        public void Atualiza_Dados(object aux)
        {
            Imagens imagem = (Imagens)aux;
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlAtualiza, conn);

            cmd.Parameters.AddWithValue("@pcod", imagem.codimagens);
            cmd.Parameters.AddWithValue("@pdescricao", imagem.descricao);
            cmd.Parameters.AddWithValue("@pfoto", imagem.foto);
            cmd.Parameters.AddWithValue("@pcodProduto", imagem.produto.codproduto);

            conn.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Imagem atualizada com sucesso.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar imagem: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        
        public void Apaga_Dados(int aux)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlApaga, conn);
            cmd.Parameters.AddWithValue("@pcod", aux);

            conn.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();

                if (i > 0)
                {
                    MessageBox.Show("Imagem apagada com sucesso.");
                }
                else
                {
                    MessageBox.Show("Nenhuma imagem encontrada com o código informado.");
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show($"Erro ao apagar imagem: {ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                conn.Close();
            }
        }


        public object Buscar_Id(int valor)
        {
            throw new NotImplementedException();
        }

        public DataTable Buscar_Todos()
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
