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
    internal class C_CidAnimal : I_Metodos_Comuns
    {
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_cidanimal;
        SqlDataAdapter da_cidanimal;
        String sqlInsere = "insert into cidanimal(nomecidanimal, descricao) values(@pnome, @pdescricao)";
        String sqlApaga = "delete from cidanimal where codcidanimal = @pcod";
        String sqlAtualiza = "update cidanimal set nomecidanimal = @pnome, descricao = @pdescricao where codcidanimal = @pcod";
        String sqlTodos = "select * from cidanimal";


        public List<CidAnimal> DadosCidAnimal()
        {
            //Cria uma lista do tipo Raça - Array
            List<CidAnimal> lista_cidanimal = new List<CidAnimal>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_cidanimal;
            conn.Open();
            try
            {
                dr_cidanimal = cmd.ExecuteReader();
                while (dr_cidanimal.Read())
                {
                    CidAnimal aux = new CidAnimal();
                    aux.Codcidanimal = Int32.Parse(dr_cidanimal["codcidanimal"].ToString());
                    aux.Nomecidanimal = dr_cidanimal["nomecidanimal"].ToString();
                    aux.Descricao = dr_cidanimal["descricao"].ToString();

                    lista_cidanimal.Add(aux);
                }
            }
            catch (Exception ex)
            {

            }

            return lista_cidanimal;
        }
        public List<CidAnimal> DadosCidAnimalFiltro(String parametro)
        {
            //Cria uma lista do tipo Raça - Array
            List<CidAnimal> lista_cidanimal = new List<CidAnimal>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            //cmd.Parameters.AddWithValue("pnomecidanimal", parametro+"%");
            //cmd.Parameters.AddWithValue("pdescricao", parametro+"%");
            cmd.Parameters.AddWithValue("@parametro", "%" + parametro + "%");

            SqlDataReader dr_cidanimal;
            conn.Open();
            try
            {
                dr_cidanimal = cmd.ExecuteReader();
                while (dr_cidanimal.Read())
                {
                    CidAnimal aux = new CidAnimal();
                    aux.Codcidanimal = Int32.Parse(dr_cidanimal["codcidanimal"].ToString());
                    aux.Nomecidanimal = dr_cidanimal["nomecidanimal"].ToString();
                    aux.Descricao = dr_cidanimal["descricao"].ToString();

                    lista_cidanimal.Add(aux);
                }
            }
            catch (Exception ex)
            {

            }

            return lista_cidanimal;
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
                MessageBox.Show("Erro");
            }

            finally
            {
                conn.Close();
            }
        }

        String sqlFiltro = "select * from cidanimal where nomecidanimal like @parametro OR descricao like @parametro";

        public DataTable Buscar_Filtro(String parametro)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);

            // Adiciona o parâmetro para nomecidanimal e descricao
            cmd.Parameters.AddWithValue("@parametro", "%" + parametro + "%");

            conn.Open();

            da_cidanimal = new SqlDataAdapter(cmd);

            dt_cidanimal = new DataTable();
            da_cidanimal.Fill(dt_cidanimal);

            conn.Close();
            return dt_cidanimal;
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

            da_cidanimal = new SqlDataAdapter(cmd);

            dt_cidanimal = new DataTable();
            da_cidanimal.Fill(dt_cidanimal);

            return dt_cidanimal;
        }
        public void Insere_Dados(object aux)
        {
            CidAnimal cidanimal = new CidAnimal();
            cidanimal = (CidAnimal)aux;

            // criando a Conexao com o banco de dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);

            // Adicione os parâmetros nomecidanimal e descricao
            cmd.Parameters.AddWithValue("@pnome", cidanimal.Nomecidanimal);
            cmd.Parameters.AddWithValue("@pdescricao", cidanimal.Descricao);

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
                MessageBox.Show("Erro: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void Atualiza_Dados(object aux)
        {
            CidAnimal dados = new CidAnimal();
            dados = (CidAnimal)aux;

            //criando a Conexao com o banco de dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcod", dados.Codcidanimal);
            cmd.Parameters.AddWithValue("@pnome", dados.Nomecidanimal);
            cmd.Parameters.AddWithValue("@pdescricao", dados.Descricao);

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
                MessageBox.Show("Erro");
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