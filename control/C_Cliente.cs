using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Veterinaria.conection;
using Veterinaria.model;

namespace Veterinaria.control
{
    internal class C_Cliente : I_Metodos_Comuns
    {
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_cliente;
        SqlDataAdapter da_cliente;

        String sqlInsere = "insert into cliente(nomecliente,codbairrofk,codruafk,codcepfk,codcidadefk,codestadofk,codpaisfk,numerocasa,cpf) values(@pnome,@pbairro,@prua,@pcep,@pcidade,@pestado,@ppais,@pnumerocasa,@pcpf)";
        String sqlApaga = "delete from cliente where codcliente = @pcod";
        String sqlAtualiza = "update cliente set nomecliente = @pnome, codbairrofk = @pbairro, codruafk = @prua,codcepfk = @pcep,codcidadefk = @pcidade,codestadofk = @pestado,codpaisfk = @ppais,numerocasa = @pnumerocasa,cpf = @pcpf where codcliente = @pcod";
        String sqlTodos = "select l.codcliente,l.nomecliente,b.nomebairro,r.nomerua,c.numerocep,ci.nomecidade,e.nomeestado,p.nomepais,l.numerocasa,l.cpf " +
            "from cliente l " +
            "left join bairro b on b.codbairro = l.codbairrofk " +
            "left join rua r on r.codrua = l.codruafk " +
            "left join cep c on l.codcepfk = c.codcep " +
            "left join cidade ci on l.codcidadefk = ci.codcidade " +
            "left join estado e on l.codestadofk = e.codestado " +
            "left join pais p on l.codpaisfk = p.codpais";


        public List<Cliente> DadosCliente()
        {
            //Cria uma lista do tipo Raça - Array
            List<Cliente> lista_cliente = new List<Cliente>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_cliente;
            conn.Open();
            try
            {
                dr_cliente = cmd.ExecuteReader();
                while (dr_cliente.Read())
                {
                    Cliente aux = new Cliente();
                    aux.ToString();
                    aux.codcliente = Int32.Parse(dr_cliente["codcliente"].ToString());
                    aux.nomecliente = dr_cliente["nomecliente"].ToString();
                    Bairro bairro = new Bairro();
                    bairro.nomebairro = dr_cliente["nomebairro"].ToString();
                    aux.bairro = bairro;
                    Rua rua = new Rua();
                    rua.nomerua = dr_cliente["nomerua"].ToString();
                    aux.rua = rua;
                    Cep cep = new Cep();
                    cep.numerocep = dr_cliente["numerocep"].ToString();
                    aux.cep = cep;
                    Cidade cidade = new Cidade();
                    cidade.nomecidade = dr_cliente["nomecidade"].ToString();
                    aux.cidade = cidade;
                    Estado estado = new Estado();
                    estado.nomeestado = dr_cliente["nomeestado"].ToString();
                    aux.estado = estado;
                    Pais pais = new Pais();
                    pais.nomepais = dr_cliente["nomepais"].ToString();
                    aux.pais = pais;
                    aux.numerocasa = dr_cliente["numerocasa"].ToString();
                    aux.cpf = dr_cliente["cpf"].ToString();

                    lista_cliente.Add(aux);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return lista_cliente;
        }
        public List<Cliente> DadosclienteFiltro(String parametro)
        {
            //Cria uma lista do tipo Raça - Array
            List<Cliente> lista_cliente = new List<Cliente>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("pnomecliente", parametro + "%");

            SqlDataReader dr_cliente;
            conn.Open();
            try
            {
                dr_cliente = cmd.ExecuteReader();
                while (dr_cliente.Read())
                {
                    Cliente aux = new Cliente();
                    aux.codcliente = Int32.Parse(dr_cliente["codcliente"].ToString());
                    aux.nomecliente = dr_cliente["nomecliente"].ToString();
                    Bairro bairro = new Bairro();
                    bairro.nomebairro = dr_cliente["nomebairro"].ToString();
                    aux.bairro = bairro;
                    Rua rua = new Rua();
                    rua.nomerua = dr_cliente["nomerua"].ToString();
                    aux.rua = rua;
                    Cep cep = new Cep();
                    cep.numerocep = dr_cliente["numerocep"].ToString();
                    aux.cep = cep;
                    Cidade cidade = new Cidade();
                    cidade.nomecidade = dr_cliente["nomecidade"].ToString();
                    aux.cidade = cidade;
                    Estado estado = new Estado();
                    estado.nomeestado = dr_cliente["nomeestado"].ToString();
                    aux.estado = estado;
                    Pais pais = new Pais();
                    pais.nomepais = dr_cliente["nomepais"].ToString();
                    aux.pais = pais;
                    aux.numerocasa = dr_cliente["numerocasa"].ToString();
                    aux.cpf = dr_cliente["cpf"].ToString();

                    lista_cliente.Add(aux);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return lista_cliente;
        }
        public void Apaga_Dados(int aux)
        {
            //criando a Conexao com o banco de dados

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlApaga, conn);
            cmd.Parameters.AddWithValue("@pcod", aux);
            cmd.CommandType = CommandType.Text;
            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Apaguei");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                conn.Close();
            }
        }

        String sqlFiltro = "select l.codcliente,l.nomecliente,b.nomebairro,r.nomerua,c.numerocep,ci.nomecidade,e.nomeestado,p.nomepais,l.numerocasa,l.cpf " +
            "from cliente l " +
            "left join bairro b on b.codbairro = l.codbairrofk " +
            "left join rua r on r.codrua = l.codruafk " +
            "left join cep c on l.codcepfk = c.codcep " +
            "left join cidade ci on l.codcidadefk = ci.codcidade " +
            "left join estado e on l.codestadofk = e.codestado " +
            "left join pais p on l.codpaisfk = p.codpais " +
            "where l.nomecliente like @pnomecliente";

        public DataTable Buscar_Filtro(String pcliente)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("pnomecliente", pcliente);

            conn.Open();

            da_cliente = new SqlDataAdapter(cmd);

            dt_cliente = new DataTable();
            da_cliente.Fill(dt_cliente);

            conn.Close();
            return dt_cliente;
        }
        public object Buscar_Id(int valor)
        {
            throw new NotImplementedException();
        }
        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            conn.Open();

            da_cliente = new SqlDataAdapter(cmd);

            dt_cliente = new DataTable();
            da_cliente.Fill(dt_cliente);

            return dt_cliente;
        }
        public void Insere_Dados(object aux)
        {
            Cliente cliente = new Cliente();
            cliente = (Cliente)aux;

            //criando a Conexao com o banco de dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pnome", cliente.nomecliente);
            cmd.Parameters.AddWithValue("@pbairro", cliente.bairro.codbairro);
            cmd.Parameters.AddWithValue("@prua", cliente.rua.codrua);
            cmd.Parameters.AddWithValue("@pcep", cliente.cep.codcep);
            cmd.Parameters.AddWithValue("@pcidade", cliente.cidade.codcidade);
            cmd.Parameters.AddWithValue("@pestado", cliente.estado.codestado);
            cmd.Parameters.AddWithValue("@ppais", cliente.pais.codpais);
            cmd.Parameters.AddWithValue("@pnumerocasa", cliente.numerocasa);
            cmd.Parameters.AddWithValue("@pcpf", cliente.cpf);

            cmd.CommandType = CommandType.Text;
            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Inseriu");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                conn.Close();
            }

        }
        public void Atualiza_Dados(object aux)
        {
            Cliente dados = new Cliente();
            dados = (Cliente)aux;

            //criando a Conexao com o banco de dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcod", dados.codcliente);
            cmd.Parameters.AddWithValue("@pnome", dados.nomecliente);
            cmd.Parameters.AddWithValue("@pbairro", dados.bairro.codbairro);
            cmd.Parameters.AddWithValue("@prua", dados.rua.codrua);
            cmd.Parameters.AddWithValue("@pcep", dados.cep.codcep);
            cmd.Parameters.AddWithValue("@pcidade", dados.cidade.codcidade);
            cmd.Parameters.AddWithValue("@pestado", dados.estado.codestado);
            cmd.Parameters.AddWithValue("@ppais", dados.pais.codpais);
            cmd.Parameters.AddWithValue("@pnumerocasa", dados.numerocasa);
            cmd.Parameters.AddWithValue("@pcpf", dados.cpf);

            cmd.CommandType = CommandType.Text;
            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Inseriu");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                conn.Close();
            }
        }

        public void Atualizar_Dados(object aux)
        {
            throw new NotImplementedException();
        }
    }
}