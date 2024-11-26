using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Veterinaria.conection;
using Veterinaria.model;

namespace Veterinaria.control
{
    internal class C_Vendas : I_Metodos_Comuns
    {
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_vendas;
        SqlDataAdapter da_vendas;

        
        String sqlInsere = "insert into vendas(datavenda, codclientefk, codfuncionariofk, codlojafk) values(@pdatavenda, @pcodcliente, @pcodfuncionario, @pcodloja)";
        String sqlApaga = "delete from vendas where codvenda = @pcod";
        String sqlAtualiza = "update vendas set datavenda = @pdatavenda, codclientefk = @pcodcliente, codfuncionariofk = @pcodfuncionario, codlojafk = @pcodloja where codvenda = @pcod";
        String sqlTodos = "select v.codvenda, v.datavenda, c.nomecliente as cliente, f.nomefuncionario as funcionario, l.nomeloja as loja " +
                        "from vendas v " +
                        "left join cliente c on c.codcliente = v.codclientefk " +
                        "left join funcionario f on f.codfuncionario = v.codfuncionariofk " +
                        "left join loja l on l.codloja = v.codlojafk";


       
        public List<Vendas> DadosVendas()
        {
            List<Vendas> lista_vendas = new List<Vendas>();
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            try
            {
                conn.Open();
                SqlDataReader dr_vendas = cmd.ExecuteReader();
                while (dr_vendas.Read())
                {
                    Vendas venda = new Vendas
                    {
                        codvenda = int.Parse(dr_vendas["codvenda"].ToString()),
                        datavenda = DateTime.Parse(dr_vendas["datavenda"].ToString()),
                        cliente = new Cliente { nomecliente = dr_vendas["cliente"].ToString() },
                        funcionario = new Funcionario { nomefuncionario = dr_vendas["funcionario"].ToString() },
                        loja = new Loja { nomeloja = dr_vendas["loja"].ToString() }
                    };
                    lista_vendas.Add(venda);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar vendas: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return lista_vendas;
        }

       
        public void Apaga_Dados(int codVenda)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlApaga, conn);
            cmd.Parameters.AddWithValue("@pcod", codVenda);

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
                MessageBox.Show("Erro ao apagar venda: " + ex.Message);
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
            da_vendas = new SqlDataAdapter(cmd);
            dt_vendas = new DataTable();

            try
            {
                da_vendas.Fill(dt_vendas);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar vendas: " + ex.Message);
            }

            return dt_vendas;
        }

        
        public void InserirVenda(Vendas venda)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pdatavenda", venda.datavenda);
            cmd.Parameters.AddWithValue("@pcodcliente", venda.cliente.codcliente);
            cmd.Parameters.AddWithValue("@pcodfuncionario", venda.funcionario.codfuncionario);
            cmd.Parameters.AddWithValue("@pcodloja", venda.loja.codloja);

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
                MessageBox.Show("Erro ao inserir venda: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

     
        public void AtualizarVenda(Vendas venda)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcod", venda.codvenda);
            cmd.Parameters.AddWithValue("@pdatavenda", venda.datavenda);
            cmd.Parameters.AddWithValue("@pcodcliente", venda.cliente.codcliente);
            cmd.Parameters.AddWithValue("@pcodfuncionario", venda.funcionario.codfuncionario);
            cmd.Parameters.AddWithValue("@pcodloja", venda.loja.codloja);

            try
            {
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("Venda atualizada com sucesso!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar venda: " + ex.Message);
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
