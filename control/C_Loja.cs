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

        String sqlInsere = "insert into loja(nomeloja,codbairrofk,codruafk,codcepfk,codcidadefk,codestadofk,codpaisfk,numeroloja,cnpj) values(@pnome,@pbairro,@prua,@pcep,@pcidade,@pestado,@ppais,@pnumeroloja,@pcnpj)";
        String sqlApaga = "delete from loja where codloja = @pcod";
        String sqlAtualiza = "update loja set nomeloja = @pnome, codbairrofk = @pbairro, codruafk = @prua,codcepfk = @pcep,codcidadefk = @pcidade,codestadofk = @pestado,codpaisfk = @ppais,numeroloja = @pnumeroloja,cnpj = @pcnpj where codloja = @pcod";
        String sqlTodos = "select l.codloja,l.nomeloja,b.nomebairro,r.nomerua,c.numerocep,ci.nomecidade,e.nomeestado,p.nomepais,l.numeroloja,l.cnpj " +
            "from loja l " +
            "left join bairro b on b.codbairro = l.codbairrofk " +
            "left join rua r on r.codrua = l.codruafk " +
            "left join cep c on l.codcepfk = c.codcep " +
            "left join cidade ci on l.codcidadefk = ci.codcidade " +
            "left join estado e on l.codestadofk = e.codestado " +
            "left join pais p on l.codpaisfk = p.codpais";


        public List<Loja> DadosLoja()
        {
            //Cria uma lista do tipo Raça - Array
            List<Loja> lista_loja = new List<Loja>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_loja;
            conn.Open();
            try
            {
                dr_loja = cmd.ExecuteReader();
                while (dr_loja.Read())
                {
                    Loja aux = new Loja();
                    aux.ToString();
                    aux.codloja = Int32.Parse(dr_loja["codloja"].ToString());
                    aux.nomeloja = dr_loja["nomeloja"].ToString();
                    Bairro bairro = new Bairro();
                    bairro.nomebairro = dr_loja["nomebairro"].ToString();
                    aux.bairro = bairro;
                    Rua rua = new Rua();
                    rua.nomerua = dr_loja["nomerua"].ToString();
                    aux.rua = rua;
                    Cep cep = new Cep();
                    cep.numerocep = dr_loja["numerocep"].ToString();
                    aux.cep = cep;
                    Cidade cidade = new Cidade();
                    cidade.nomecidade = dr_loja["nomecidade"].ToString();
                    aux.cidade = cidade;
                    Estado estado = new Estado();
                    estado.nomeestado = dr_loja["nomeestado"].ToString();
                    aux.estado = estado;
                    Pais pais = new Pais();
                    pais.nomepais = dr_loja["nomepais"].ToString();
                    aux.pais = pais;
                    aux.numeroloja = dr_loja["numeroloja"].ToString();
                    aux.cnpj = dr_loja["cnpj"].ToString();

                    lista_loja.Add(aux);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return lista_loja;
        }
        public List<Loja> DadosLojaFiltro(String parametro)
        {
            //Cria uma lista do tipo Raça - Array
            List<Loja> lista_loja = new List<Loja>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("pnomeloja", parametro + "%");

            SqlDataReader dr_loja;
            conn.Open();
            try
            {
                dr_loja = cmd.ExecuteReader();
                while (dr_loja.Read())
                {
                    Loja aux = new Loja();
                    aux.codloja = Int32.Parse(dr_loja["codloja"].ToString());
                    aux.nomeloja = dr_loja["nomeloja"].ToString();
                    Bairro bairro = new Bairro();
                    bairro.nomebairro = dr_loja["nomebairro"].ToString();
                    aux.bairro = bairro;
                    Rua rua = new Rua();
                    rua.nomerua = dr_loja["nomerua"].ToString();
                    aux.rua = rua;
                    Cep cep = new Cep();
                    cep.numerocep = dr_loja["numerocep"].ToString();
                    aux.cep = cep;
                    Cidade cidade = new Cidade();
                    cidade.nomecidade = dr_loja["nomecidade"].ToString();
                    aux.cidade = cidade;
                    Estado estado = new Estado();
                    estado.nomeestado = dr_loja["nomeestado"].ToString();
                    aux.estado = estado;
                    Pais pais = new Pais();
                    pais.nomepais = dr_loja["nomepais"].ToString();
                    aux.pais = pais;
                    aux.numeroloja = dr_loja["numeroloja"].ToString();
                    aux.cnpj = dr_loja["cnpj"].ToString();

                    lista_loja.Add(aux);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return lista_loja;
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

        String sqlFiltro = "select l.codloja,l.nomeloja,b.nomebairro,r.nomerua,c.numerocep,ci.nomecidade,e.nomeestado,p.nomepais,l.numeroloja,l.cnpj " +
            "from loja l " +
            "left join bairro b on b.codbairro = l.codbairrofk " +
            "left join rua r on r.codrua = l.codruafk " +
            "left join cep c on l.codcepfk = c.codcep " +
            "left join cidade ci on l.codcidadefk = ci.codcidade " +
            "left join estado e on l.codestadofk = e.codestado " +
            "left join pais p on l.codpaisfk = p.codpais " +
            "where l.nomeloja like @pnomeloja";

        public DataTable Buscar_Filtro(String ploja)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("pnomeloja", ploja);

            conn.Open();

            da_loja = new SqlDataAdapter(cmd);

            dt_loja = new DataTable();
            da_loja.Fill(dt_loja);

            conn.Close();
            return dt_loja;
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

            da_loja = new SqlDataAdapter(cmd);

            dt_loja = new DataTable();
            da_loja.Fill(dt_loja);

            return dt_loja;
        }
        public void Insere_Dados(object aux)
        {
            Loja loja = new Loja();
            loja = (Loja)aux;

            //criando a Conexao com o banco de dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pnome", loja.nomeloja);
            cmd.Parameters.AddWithValue("@pbairro", loja.bairro.codbairro);
            cmd.Parameters.AddWithValue("@prua", loja.rua.codrua);
            cmd.Parameters.AddWithValue("@pcep", loja.cep.codcep);
            cmd.Parameters.AddWithValue("@pcidade", loja.cidade.codcidade);
            cmd.Parameters.AddWithValue("@pestado", loja.estado.codestado);
            cmd.Parameters.AddWithValue("@ppais", loja.pais.codpais);
            cmd.Parameters.AddWithValue("@pnumeroloja", loja.numeroloja);
            cmd.Parameters.AddWithValue("@pcnpj", loja.cnpj);

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
            Loja dados = new Loja();
            dados = (Loja)aux;

            //criando a Conexao com o banco de dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcod", dados.codloja);
            cmd.Parameters.AddWithValue("@pnome", dados.nomeloja);
            cmd.Parameters.AddWithValue("@pbairro", dados.bairro.codbairro);
            cmd.Parameters.AddWithValue("@prua", dados.rua.codrua);
            cmd.Parameters.AddWithValue("@pcep", dados.cep.codcep);
            cmd.Parameters.AddWithValue("@pcidade", dados.cidade.codcidade);
            cmd.Parameters.AddWithValue("@pestado", dados.estado.codestado);
            cmd.Parameters.AddWithValue("@ppais", dados.pais.codpais);
            cmd.Parameters.AddWithValue("@pnumeroloja", dados.numeroloja);
            cmd.Parameters.AddWithValue("@pcnpj", dados.cnpj);

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