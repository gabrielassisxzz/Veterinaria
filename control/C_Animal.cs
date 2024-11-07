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
    internal class C_Animal : I_Metodos_Comuns
    {
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_animal;
        SqlDataAdapter da_animal;

        String sqlInsere = "insert into animal(nomeanimal,codsexofk,codracafk,codtipoanimalfk,codclientefk) values(@pnome,@psexo,@praca,@ptipoanimal,@pcliente)";
        String sqlApaga = "delete from animal where codanimal = @pcod";
        String sqlAtualiza = "update animal set nomeanimal = @pnome, codsexofk = @psexo, codracafk = @praca,codtipoanimalfk = @ptipoanimal,codclientefk = @pcliente where codanimal = @pcod";
        String sqlTodos = "select a.codanimal, a.nomeanimal, s.nomesexo, r.nomeraca, t.nometipoanimal, c.nomecliente " +
                          " from animal a " +
                          " left join sexo s on s.codsexo = a.codsexofk " +
                          " left join raca r on r.codraca = a.codracafk " +
                          " left join tipoanimal t on t.codtipoanimal = a.codtipoanimalfk " +
                          " left join cliente c on c.codcliente = a.codclientefk";


        public List<Animal> Dadosanimal()
        {
            //Cria uma lista do tipo Raça - Array
            List<Animal> lista_animal = new List<Animal>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_animal;
            conn.Open();
            try
            {
                dr_animal = cmd.ExecuteReader();
                while (dr_animal.Read())
                {
                    Animal aux = new Animal();
                    aux.ToString();
                    aux.codanimal = Int32.Parse(dr_animal["codanimal"].ToString());
                    aux.nomeanimal = dr_animal["nomeanimal"].ToString();
                    Sexo Sexo = new Sexo();
                    Sexo.nomesexo = dr_animal["nomeSexo"].ToString();
                    aux.sexo = Sexo;
                    Raca Raca = new Raca();
                    Raca.nomeraca = dr_animal["nomeRaca"].ToString();
                    aux.raca = Raca;
                    Tipoanimal tipoanimal = new Tipoanimal();
                    tipoanimal.nometipoanimal = dr_animal.ToString();
                    aux.tipoanimal = tipoanimal; 
                    Cliente Cliente = new Cliente();
                    Cliente.nomecliente = dr_animal["nomeCliente"].ToString();
                    aux.cliente = Cliente;


                    lista_animal.Add(aux);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return lista_animal;
        }
        public List<Animal> DadosanimalFiltro(String parametro)
        {
            //Cria uma lista do tipo Raça - Array
            List<Animal> lista_animal = new List<Animal>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("pnomeanimal", parametro + "%");

            SqlDataReader dr_animal;
            conn.Open();
            try
            {
                dr_animal = cmd.ExecuteReader();
                while (dr_animal.Read())
                {
                    Animal aux = new Animal();
                    aux.codanimal = Int32.Parse(dr_animal["codanimal"].ToString());
                    aux.nomeanimal = dr_animal["nomeanimal"].ToString();
                    Sexo Sexo = new Sexo();
                    Sexo.nomesexo = dr_animal["nomeSexo"].ToString();
                    aux.sexo = Sexo;
                    Raca Raca = new Raca();
                    Raca.nomeraca = dr_animal["nomeRaca"].ToString();
                    aux.raca = Raca;
                    Tipoanimal tipoanimal = new Tipoanimal();
                    tipoanimal.nometipoanimal = dr_animal.ToString();
                    aux.tipoanimal = tipoanimal;
                    Cliente Cliente = new Cliente();
                    Cliente.nomecliente = dr_animal["nomeCliente"].ToString();
                    aux.cliente = Cliente;


                    lista_animal.Add(aux);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return lista_animal;
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

        String sqlFiltro = "select a.codanimal, a.nomeanimal, s.nomesexo, r.nomeraca, t.nometipoanimal, c.nomecliente" +
                          " from animal a" +  
                          " left join sexo s on s.codsexo = a.codsexofk" +
                          " left join raca r on r.codraca = a.codracafk" +
                          " left join tipoanimal t on t.codtipoanimal = a.codtipoanimalfk" +
                          " left join cliente c on c.codcliente = a.codclientefk" +
                          " where a.nomeanimal like @pnomeanimal";



        public DataTable Buscar_Filtro(string panimal)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("pnomeanimal", panimal);

            conn.Open();

            da_animal = new SqlDataAdapter(cmd);

            dt_animal = new DataTable();
            da_animal.Fill(dt_animal);

            conn.Close();
            return dt_animal;
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

            da_animal = new SqlDataAdapter(cmd);

            dt_animal = new DataTable();
            da_animal.Fill(dt_animal);
            return dt_animal;
        }

        public void Insere_Dados(object aux)
        {
            Animal animal = new Animal();
            animal = (Animal)aux;

            //criando a Conexao com o banco de dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pnome", animal.nomeanimal);
            cmd.Parameters.AddWithValue("@psexo", animal.sexo.codsexo);
            cmd.Parameters.AddWithValue("@praca", animal.raca.codraca);
            cmd.Parameters.AddWithValue("@ptipoanimal", animal.tipoanimal.codtipoanimal);
            cmd.Parameters.AddWithValue("@pcliente", animal.cliente.codcliente);


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
            Animal dados = new Animal();
            dados = (Animal)aux;

            //criando a Conexao com o banco de dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcod", dados.codanimal);
            cmd.Parameters.AddWithValue("@pnome", dados.nomeanimal);
            cmd.Parameters.AddWithValue("@psexo", dados.sexo.codsexo);
            cmd.Parameters.AddWithValue("@praca", dados.raca.codraca);
            cmd.Parameters.AddWithValue("@ptipoanimal", dados.tipoanimal.codtipoanimal);
            cmd.Parameters.AddWithValue("@pcliente", dados.cliente.codcliente);



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
    }
}
