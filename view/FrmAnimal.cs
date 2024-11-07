using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Veterinaria.control;
using Veterinaria.model;

namespace Veterinaria.view
{
    public partial class FrmAnimal : Form
    {
        private DataTable Tabela_Animal;
        private bool novo;
        private int posicao;
        private List<Animal> lista_animal = new List<Animal>();
        private List<Sexo> Sexos = new List<Sexo>();
        private List<Raca> Racas = new List<Raca>();
        private List<Tipoanimal> Tipoanimals = new List<Tipoanimal>();
        private List<Cliente> Clientes = new List<Cliente>();
        private int posicao_sexo;
        private int posicao_raca;
        private int posicao_tipoanimal;
        private int posicao_cliente;

        public FrmAnimal()
        {
            InitializeComponent();
            CarregaTabela();

            lista_animal = carregaListaAnimal();
            if (lista_animal.Count > 0)
            {
                posicao = 0;
                dataGridView1.Rows[posicao].Selected = true;
                atualizaCampos();
            }

            preencheComboSexo();
            preencheComboRaca();
            preencheComboTipoAnimal();
            preencheComboCliente();
        }

        private void atualizaCampos()
        {
            if (posicao >= 0 && posicao < lista_animal.Count)
            {
                txtCodigo.Text = lista_animal[posicao].codanimal.ToString();
                txtAnimal.Text = lista_animal[posicao].nomeanimal;

                cmbSexo.SelectedIndex = Sexos.FindIndex(s => s.codsexo == lista_animal[posicao].sexo.codsexo);
                cmbRaca.SelectedIndex = Racas.FindIndex(r => r.codraca == lista_animal[posicao].raca.codraca);
                cmbTipoAnimal.SelectedIndex = Tipoanimals.FindIndex(t => t.codtipoanimal == lista_animal[posicao].tipoanimal.codtipoanimal);
                cmbCliente.SelectedIndex = Clientes.FindIndex(c => c.codcliente == lista_animal[posicao].cliente.codcliente);
            }
        }

        private List<Animal> carregaListaAnimal()
        {
            C_Animal c_Animal = new C_Animal();
            return c_Animal.Dadosanimal();
        }

        private List<Animal> carregaListaAnimalFiltro()
        {
            C_Animal c_Animal = new C_Animal();
            return c_Animal.DadosanimalFiltro(txtBuscar.Text);
        }

        private void CarregaTabela()
        {
            C_Animal c_Animal = new C_Animal();
            Tabela_Animal = c_Animal.Buscar_Todos();
            dataGridView1.DataSource = Tabela_Animal;
        }

        private void preencheComboSexo()
        {
            C_Sexo c_Sexo = new C_Sexo();
            Sexos = c_Sexo.DadosSexo();

            foreach (Sexo sexo in Sexos) { 
                cmbSexo.Items.Add(sexo.nomesexo);
            }
            
        }

        private void preencheComboRaca()
        {
            C_Raca c_Raca = new C_Raca();
            Racas = c_Raca.DadosRaca();
            foreach (Raca raca in Racas)
            {
                cmbRaca.Items.Add(raca.nomeraca);
            }
        }

        private void preencheComboTipoAnimal()
        {
            C_Tipoanimal c_TipoAnimal = new C_Tipoanimal();
            Tipoanimals = c_TipoAnimal.DadosTipoanimal();
            foreach (Tipoanimal tipoanimal in Tipoanimals)
            {
                cmbTipoAnimal.Items.Add(tipoanimal.nometipoanimal);
            }
        }

        private void preencheComboCliente()
        {
            C_Cliente c_Cliente = new C_Cliente();
            Clientes = c_Cliente.DadosCliente();
            cmbCliente.Items.AddRange(Clientes.Select(c => c.nomecliente).ToArray());
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
            txtAnimal.Enabled = true;
            cmbTipoAnimal.Enabled = true;
            cmbSexo.Enabled = true;
            cmbRaca.Enabled = true;
            cmbCliente.Enabled = true;
        }

        private void limparCampos()
        {
            txtCodigo.Text = "";
            txtAnimal.Text = "";
            cmbTipoAnimal.SelectedIndex = -1;
            cmbSexo.SelectedIndex = -1;
            cmbRaca.SelectedIndex = -1;
            cmbCliente.SelectedIndex = -1;
        }

        private void desativaCampos()
        {
            txtAnimal.Enabled = false;
            cmbTipoAnimal.Enabled = false;
            cmbSexo.Enabled = false;
            cmbRaca.Enabled = false;
            cmbCliente.Enabled = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (cmbRaca.SelectedIndex < 0 || cmbTipoAnimal.SelectedIndex < 0 || cmbSexo.SelectedIndex < 0 || cmbCliente.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, preencha todos os campos.");
                return;
            }

            Animal Nova_Animal = new Animal
            {
                nomeanimal = txtAnimal.Text,
                raca = Racas[cmbRaca.SelectedIndex],
                tipoanimal = Tipoanimals[cmbTipoAnimal.SelectedIndex],
                sexo = Sexos[cmbSexo.SelectedIndex],
                cliente = Clientes[cmbCliente.SelectedIndex]
            };

            C_Animal c_Animal = new C_Animal();
            if (novo)
            {
                c_Animal.Insere_Dados(Nova_Animal);
            }
            else
            {
                Nova_Animal.codanimal = int.Parse(txtCodigo.Text);
                c_Animal.Atualizar_Dados(Nova_Animal);
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
            if (!string.IsNullOrEmpty(txtCodigo.Text))
            {
                try
                {
                    C_Animal animal = new C_Animal();
                    int valor = int.Parse(txtCodigo.Text);
                    animal.Apaga_Dados(valor);
                    CarregaTabela();
                    MessageBox.Show("Animal apagado com sucesso.");
                    limparCampos();
                    desativaCampos();
                    desativaBotoes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao apagar Animal: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor, insira o código do Animal a ser apagado.");
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
            posicao = 0;
            atualizarPosicao();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (posicao > 0)
            {
                posicao--;
                atualizarPosicao();
            }
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (posicao < lista_animal.Count - 1)
            {
                posicao++;
                atualizarPosicao();
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            posicao = lista_animal.Count - 1;
            atualizarPosicao();
        }

        private void atualizarPosicao()
        {
            if (posicao >= 0 && posicao < lista_animal.Count)
            {
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            lista_animal = carregaListaAnimalFiltro();
            CarregaTabela();

            if (lista_animal.Count > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza que deseja sair?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow dr = dataGridView1.Rows[e.RowIndex];
                txtCodigo.Text = dr.Cells[0].Value?.ToString();
                txtAnimal.Text = dr.Cells[1].Value?.ToString();
                cmbSexo.SelectedIndex = Sexos.FindIndex(s => s.codsexo.ToString() == dr.Cells[2].Value?.ToString());
                cmbTipoAnimal.SelectedIndex = Tipoanimals.FindIndex(t => t.codtipoanimal.ToString() == dr.Cells[3].Value?.ToString());
                cmbRaca.SelectedIndex = Racas.FindIndex(r => r.codraca.ToString() == dr.Cells[4].Value?.ToString());
                cmbCliente.SelectedIndex = Clientes.FindIndex(c => c.codcliente.ToString() == dr.Cells[5].Value?.ToString());
            }
        }

        private void FrmAnimal_Load(object sender, EventArgs e)
        {

        }
    }
}
