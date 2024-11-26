using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Veterinaria.conection;
using Veterinaria.model;

namespace Veterinaria.control
{
    internal class C_VendaServico : I_Metodos_Comuns
    {
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_vendaServico;
        SqlDataAdapter da_vendaServico;

        String sqlInsere = "insert into vendaservico(datavs, codclientefk, codfuncionariofk, codanimalfk) values(@pdatavs, @pcodcliente, @pcodfuncionario, @pcodanimal)";
        String sqlApaga = "delete from vendaservico where codvendaservico = @pcod";
        String sqlAtualiza = "update vendaservico set datavs = @pdatavs, codclientefk = @pcodcliente, codfuncionariofk = @pcodfuncionario, codanimalfk = @pcodanimal where codvendaservico = @pcod";
        String sqlTodos = "select vs.codvendaservico, vs.datavs, c.nomecliente as cliente, f.nomefuncionario as funcionario, a.nomeanimal as animal " +
                          "from vendaservico vs " +
                          "left join cliente c on c.codcliente = vs.codclientefk " +
                          "left join funcionario f on f.codfuncionario = vs.codfuncionariofk " +
                          "left join animal a on a.codanimal = vs.codanimalfk";

        public List<VendaServico> DadosVendaServico()
        {
            List<VendaServico> lista_vendaServico = new List<VendaServico>();
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            try
            {
                conn.Open();
                SqlDataReader dr_vendaServico = cmd.ExecuteReader();
                while (dr_vendaServico.Read())
                {
                    VendaServico vendaServico = new VendaServico
                    {
                        codvendaservico = int.Parse(dr_vendaServico["codvendaservico"].ToString()),
                        data = DateTime.Parse(dr_vendaServico["datavs"].ToString()),
                        cliente = new Cliente { nomecliente = dr_vendaServico["cliente"].ToString() },
                        funcionario = new Funcionario { nomefuncionario = dr_vendaServico["funcionario"].ToString() },
                        animal = new Animal { nomeanimal = dr_vendaServico["animal"].ToString() }
                    };
                    lista_vendaServico.Add(vendaServico);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar vendas de serviço: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return lista_vendaServico;
        }

        public void Apaga_Dados(int codVendaServico)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlApaga, conn);
            cmd.Parameters.AddWithValue("@pcod", codVendaServico);

            try
            {
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("Venda de serviço apagada com sucesso!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao apagar venda de serviço: " + ex.Message);
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
            da_vendaServico = new SqlDataAdapter(cmd);
            dt_vendaServico = new DataTable();

            try
            {
                da_vendaServico.Fill(dt_vendaServico);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar vendas de serviço: " + ex.Message);
            }

            return dt_vendaServico;
        }

        public void InserirVendaServico(VendaServico vendaServico)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pdatavs", vendaServico.data);
            cmd.Parameters.AddWithValue("@pcodcliente", vendaServico.cliente.codcliente);
            cmd.Parameters.AddWithValue("@pcodfuncionario", vendaServico.funcionario.codfuncionario);
            cmd.Parameters.AddWithValue("@pcodanimal", vendaServico.animal.codanimal);

            try
            {
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("Venda de serviço inserida com sucesso!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inserir venda de serviço: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void AtualizarVendaServico(VendaServico vendaServico)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcod", vendaServico.codvendaservico);
            cmd.Parameters.AddWithValue("@pdatavs", vendaServico.data);
            cmd.Parameters.AddWithValue("@pcodcliente", vendaServico.cliente.codcliente);
            cmd.Parameters.AddWithValue("@pcodfuncionario", vendaServico.funcionario.codfuncionario);
            cmd.Parameters.AddWithValue("@pcodanimal", vendaServico.animal.codanimal);

            try
            {
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("Venda de serviço atualizada com sucesso!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar venda de serviço: " + ex.Message);
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
