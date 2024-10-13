using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Veterinaria.control;
using Veterinaria.model;

namespace Veterinaria.view
{
    public partial class FrmCliente : Form
    {
        DataTable Tabela_Cliente;
        Boolean novo;
        int posicao;
        List<Cliente> lista_Cliente = new List<Cliente>();
        List<Rua> ruas = new List<Rua>();
        List<Bairro> bairros = new List<Bairro>();
        List<Cidade> cidades = new List<Cidade>();
        List<Pais> paises = new List<Pais>();
        List<Cep> ceps = new List<Cep>();
        List<Estado> estados = new List<Estado>();
        private int posicao_bairro;
        private int posicao_rua;
        private int posicao_cep;
        private int posicao_estado;
        private int posicao_cidade;
        private int posicao_pais;


        public FrmCliente()
        {
            InitializeComponent();
            CarregaTabela();

            lista_Cliente = carregaListaCliente();
            if (posicao < dataGridView1.Rows.Count)
            {
                dataGridView1.Rows[posicao].Selected = true;
            }

            preencheComboRua();
            preencheComboBairro();
            preencheComboCidade();
            preencheComboPais();
            preencheComboCep();
            preencheComboEstado();
        }

        private void atualizaCampos()
        {
            txtCodigo.Text = lista_Cliente[posicao].codcliente.ToString();
            txtCliente.Text = lista_Cliente[posicao].nomecliente.ToString();
            cmbBairro.Text = lista_Cliente[posicao].bairro.nomebairro.ToString();
            cmbRua.Text = lista_Cliente[posicao].rua.nomerua.ToString();
            cmbCEP.Text = lista_Cliente[posicao].cep.ToString();
            cmbCidade.Text = lista_Cliente[posicao].cidade.nomecidade.ToString();
            cmbEstado.Text = lista_Cliente[posicao].estado.nomeestado.ToString();
            cmbPais.Text = lista_Cliente[posicao].pais.nomepais.ToString();
            txtNumero.Text = lista_Cliente[posicao].numerocasa.ToString();
            txtCnpj.Text = lista_Cliente[posicao].cpf.ToString();
        }

        List<Cliente> carregaListaCliente()
        {
            List<Cliente> lista = new List<Cliente>();

            C_Cliente c_Cliente = new C_Cliente();
            lista = c_Cliente.DadosCliente();

            return lista;
        }

        List<Cliente> carregaListaClienteFiltro()
        {
            List<Cliente> lista = new List<Cliente>();

            C_Cliente c_Cliente = new C_Cliente();
            lista = c_Cliente.DadosclienteFiltro(txtBuscar.Text);

            return lista;
        }

        private void CarregaTabela()
        {
            C_Cliente c_Cliente = new C_Cliente();
            DataTable dt = new DataTable();
            dt = c_Cliente.Buscar_Todos();
            Tabela_Cliente = dt;
            dataGridView1.DataSource = Tabela_Cliente;
        }

        private void preencheComboEstado()
        {
            C_Estado c_Estado = new C_Estado();

            estados = c_Estado.DadosEstado();
            foreach (Estado estado in estados)
            {
                cmbEstado.Items.Add(estado.nomeestado);
            }
        }

        private void preencheComboCep()
        {
            C_Cep c_Cep = new C_Cep();

            ceps = c_Cep.DadosCep();
            foreach (Cep cep in ceps)
            {
                cmbCEP.Items.Add(cep.numerocep);
            }
        }

        private void preencheComboPais()
        {
            C_Pais c_Pais = new C_Pais();

            paises = c_Pais.DadosPais();
            foreach (Pais pais in paises)
            {
                cmbPais.Items.Add(pais.nomepais);
            }
        }

        private void preencheComboCidade()
        {
            C_Cidade c_Cidade = new C_Cidade();

            cidades = c_Cidade.DadosCidade();
            foreach (Cidade c in cidades)
            {
                cmbCidade.Items.Add(c.nomecidade);
            }

        }

        private void preencheComboBairro()
        {
            C_Bairro c_Bairro = new C_Bairro();

            bairros = c_Bairro.DadosBairro();
            foreach (Bairro b in bairros)
            {
                cmbBairro.Items.Add(b.nomebairro);
            }
        }

        private void preencheComboRua()
        {
            C_Rua c_Rua = new C_Rua();

            ruas = c_Rua.DadosRua();
            foreach (Rua rua in ruas)
            {
                cmbRua.Items.Add(rua.nomerua);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrmCliente_Load(object sender, EventArgs e)
        {

        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            limparCampos();
            ativarCampos();
            ativaBotoes();

            novo = true;
        }

        private void ativaBotoes()
        {
            btnNovo.Enabled = false;
            btnApagar.Enabled = false;
            btnEditar.Enabled = false;
            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void ativarCampos()
        {
            txtCliente.Enabled = true;
            cmbBairro.Enabled = true;
            cmbRua.Enabled = true;
            cmbCEP.Enabled = true;
            cmbCidade.Enabled = true;
            cmbEstado.Enabled = true;
            cmbPais.Enabled = true;
            txtNumero.Enabled = true;
            txtCnpj.Enabled = true;
        }

        private void limparCampos()
        {
            txtCodigo.Text = "";
            txtCliente.Text = "";
            cmbBairro.Text = "";
            cmbRua.Text = "";
            cmbCEP.Text = "";
            cmbCidade.Text = "";
            cmbEstado.Text = "";
            cmbPais.Text = "";
            txtNumero.Text = "";
            txtCnpj.Text = "";
        }

        private void desativaCampos()
        {
            txtCliente.Enabled = false;
            cmbBairro.Enabled = false;
            cmbRua.Enabled = false;
            cmbCEP.Enabled = false;
            cmbCidade.Enabled = false;
            cmbEstado.Enabled = false;
            cmbPais.Enabled = false;
            txtNumero.Enabled = false;
            txtCnpj.Enabled = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Cliente Nova_Cliente = new Cliente();

            Bairro bairroCopia = new Bairro();
            bairroCopia.codbairro = bairros[posicao_bairro].codbairro;
            Nova_Cliente.bairro = bairroCopia;

            Bairro bairro = new Bairro();
            posicao_bairro = cmbBairro.SelectedIndex;
            bairro.codbairro = bairros[posicao_bairro].codbairro;
            Nova_Cliente.bairro = bairro;

            Nova_Cliente.nomecliente = txtCliente.Text;

            Cliente Cliente = new Cliente();
            Rua rua = new Rua();
            posicao_rua = cmbRua.SelectedIndex;
            rua.codrua = ruas[posicao_rua].codrua;
            Nova_Cliente.rua = rua;


            Cep cep = new Cep();
            posicao_cep = cmbCEP.SelectedIndex;
            cep.codcep = ceps[posicao_cep].codcep;
            Nova_Cliente.cep = cep;

            Cidade cidade = new Cidade();
            posicao_cidade = cmbCidade.SelectedIndex;
            cidade.codcidade = cidades[posicao_cidade].codcidade;

            Nova_Cliente.cidade = cidade;

            Estado estado = new Estado();
            posicao_estado = cmbEstado.SelectedIndex;
            estado.codestado = estados[posicao_estado].codestado;
            Nova_Cliente.estado = estado;


            Pais pais = new Pais();
            posicao_pais = cmbPais.SelectedIndex;
            pais.codpais = paises[posicao_pais].codpais;
            Nova_Cliente.pais = pais;


            Nova_Cliente.numerocasa = txtNumero.Text;
            Nova_Cliente.cpf = txtCnpj.Text;

            C_Cliente c_Cliente = new C_Cliente();
            if (novo == true)
            {
                c_Cliente.Insere_Dados(Nova_Cliente);
            }
            else
            {
                Nova_Cliente.codcliente = Int32.Parse(txtCliente.Text);
                c_Cliente.Atualizar_Dados(Nova_Cliente);
            }

            CarregaTabela();
            desativaCampos();
            desativaBotoes();
        }

        private void desativaBotoes()
        {
            btnNovo.Enabled = true;
            btnApagar.Enabled = true;
            btnEditar.Enabled = true;
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limparCampos();
            desativaBotoes();
            desativaCampos();
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            // Verifica se o campo txtCodigo não está vazio
            if (!string.IsNullOrEmpty(txtCodigo.Text))
            {
                try
                {

                    C_Cliente Cliente = new C_Cliente();


                    int valor = Int32.Parse(txtCodigo.Text);


                    Cliente.Apaga_Dados(valor);


                    CarregaTabela();


                    MessageBox.Show("Cliente apagada com sucesso.");


                    limparCampos();


                    desativaCampos();
                    desativaBotoes();
                }
                catch (FormatException)
                {

                    MessageBox.Show("O código informado não é válido.");
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Erro ao apagar Cliente: " + ex.Message);
                }
            }
            else
            {

                MessageBox.Show("Por favor, insira o código da Cliente a ser apagada.");
            }
        }



        private void btnEditar_Click(object sender, EventArgs e)
        {
            ativarCampos();
            ativaBotoes();
            novo = false;
        }

        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows[posicao].Selected = false;
            posicao = 0;
            atualizaCampos();
            dataGridView1.Rows[posicao].Selected = true;
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows[posicao].Selected = false;
            posicao = lista_Cliente.Count - 1;
            atualizaCampos();
            dataGridView1.Rows[posicao].Selected = true;
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (posicao > 0)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao--;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (lista_Cliente.Count - 1 > posicao)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao++;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }



        private void btnBuscar_Click(object sender, EventArgs e)
        {
            C_Cliente c_Cliente = new C_Cliente();
            DataTable dt = new DataTable();
            dt = c_Cliente.Buscar_Filtro(txtBuscar.Text.ToString() + "%");
            Tabela_Cliente = dt;
            dataGridView1.DataSource = Tabela_Cliente;

            lista_Cliente = carregaListaClienteFiltro();

            if (lista_Cliente.Count - 1 > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow dr = dataGridView1.Rows[index];
            txtCodigo.Text = dr.Cells[0].Value.ToString();
            txtCliente.Text = dr.Cells[1].Value.ToString();
            cmbBairro.Text = dr.Cells[2].Value.ToString();
            cmbRua.Text = dr.Cells[3].Value.ToString();
            cmbCEP.Text = dr.Cells[4].Value.ToString();
            cmbCidade.Text = dr.Cells[5].Value.ToString();
            cmbEstado.Text = dr.Cells[6].Value.ToString();
            cmbPais.Text = dr.Cells[7].Value.ToString();
            txtNumero.Text = dr.Cells[8].Value.ToString();
            txtCnpj.Text = dr.Cells[9].Value.ToString();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza que deseja sair?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)

            {
                this.Close();
            }
        }
    }
}
