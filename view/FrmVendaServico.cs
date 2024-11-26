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
    public partial class FrmVendaServico : Form
    {

        DataTable Tabela_vendaServico;
        bool novo = true;
        int posicao = 0;
        List<VendaServico> lista_vendaServico = new List<VendaServico>();
        List<Cliente> clientes = new List<Cliente>();
        List<Funcionario> funcionarios = new List<Funcionario>();
        List<Animal> animais = new List<Animal>();

        public FrmVendaServico()
        {
            InitializeComponent();
            CarregaTabela();
            preencheComboCliente();
            preencheComboFuncionario();
            preencheComboAnimal();

            lista_vendaServico = carregaListaVendaServico();
            if (lista_vendaServico.Count > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void atualizaCampos()
        {
            txtCodigo.Text = lista_vendaServico[posicao].codvendaservico.ToString();
            txtData.Value = lista_vendaServico[posicao].data;
            cmbCliente.SelectedValue = lista_vendaServico[posicao].cliente.codcliente;
            cmbFuncionario.SelectedValue = lista_vendaServico[posicao].funcionario.codfuncionario;
            cmbAnimal.SelectedValue = lista_vendaServico[posicao].animal.codanimal;
        }

        private void CarregaTabela()
        {
            try
            {
                C_VendaServico c_VendaServico = new C_VendaServico();
                Tabela_vendaServico = c_VendaServico.Buscar_Todos();
                dataGridView1.DataSource = Tabela_vendaServico;

                if (Tabela_vendaServico.Rows.Count == 0)
                {
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message);
            }
        }

        private List<VendaServico> carregaListaVendaServico()
        {
            C_VendaServico c_VendaServico = new C_VendaServico();
            return c_VendaServico.DadosVendaServico();
        }

        private void preencheComboCliente()
        {
            C_Cliente c_Cliente = new C_Cliente();
            clientes = c_Cliente.DadosCliente();
            cmbCliente.DataSource = clientes;
            cmbCliente.DisplayMember = "nomecliente";
            cmbCliente.ValueMember = "codcliente";
            cmbCliente.SelectedIndex = -1;
        }

        private void preencheComboFuncionario()
        {
            C_Funcionario c_Funcionario = new C_Funcionario();
            funcionarios = c_Funcionario.DadosFuncionario();
            cmbFuncionario.DataSource = funcionarios;
            cmbFuncionario.DisplayMember = "nomefuncionario";
            cmbFuncionario.ValueMember = "codfuncionario";
            cmbFuncionario.SelectedIndex = -1;
        }

        private void preencheComboAnimal()
        {
            C_Animal c_Animal = new C_Animal();
            animais = c_Animal.Dadosanimal();
            cmbAnimal.DataSource = animais;
            cmbAnimal.DisplayMember = "nomeanimal";
            cmbAnimal.ValueMember = "codanimal";
            cmbAnimal.SelectedIndex = -1;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            limparCampos();
            ativarCampos();
            btnSalvar.Enabled = true;
            novo = true;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                C_VendaServico c_VendaServico = new C_VendaServico();
                VendaServico vendaServico = new VendaServico
                {
                    data = txtData.Value
                };

                if (int.TryParse(cmbCliente.SelectedValue?.ToString(), out int clienteId))
                {
                    vendaServico.cliente = new Cliente { codcliente = clienteId };
                }

                if (int.TryParse(cmbFuncionario.SelectedValue?.ToString(), out int funcionarioId))
                {
                    vendaServico.funcionario = new Funcionario { codfuncionario = funcionarioId };
                }

                if (int.TryParse(cmbAnimal.SelectedValue?.ToString(), out int animalId))
                {
                    vendaServico.animal = new Animal { codanimal = animalId };
                }

                if (novo)
                {
                    c_VendaServico.InserirVendaServico(vendaServico);
                }
                else
                {
                    c_VendaServico.AtualizarVendaServico(vendaServico);
                }

                CarregaTabela();
                limparCampos();
                desativaCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar venda de serviço: " + ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limparCampos();
            desativaCampos();
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodigo.Text))
            {
                try
                {
                    C_VendaServico c_VendaServico = new C_VendaServico();
                    int valor = Int32.Parse(txtCodigo.Text);
                    c_VendaServico.Apaga_Dados(valor);

                    CarregaTabela();
                    limparCampos();
                    desativaCampos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao apagar venda de serviço: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor, insira o código da venda de serviço a ser apagada.");
            }
        }

        private void limparCampos()
        {
            txtCodigo.Text = "";
            txtData.Value = DateTime.Now;
            cmbCliente.SelectedIndex = -1;
            cmbFuncionario.SelectedIndex = -1;
            cmbAnimal.SelectedIndex = -1;
        }

        private void ativarCampos()
        {
            txtData.Enabled = true;
            cmbCliente.Enabled = true;
            cmbFuncionario.Enabled = true;
            cmbAnimal.Enabled = true;
        }

        private void desativaCampos()
        {
            txtData.Enabled = false;
            cmbCliente.Enabled = false;
            cmbFuncionario.Enabled = false;
            cmbAnimal.Enabled = false;
        }



        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodigo.Text))
            {
                ativarCampos();
                novo = false; 
            }
            else
            {
                MessageBox.Show("Selecione um registro para editar.");
            }
        }

        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            if (lista_vendaServico.Count > 0)
            {
                posicao = 0; 
                atualizaCampos();
                SelecionarLinhaDataGrid();
            }
            else
            {
                MessageBox.Show("Não há registros para navegar.");
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (lista_vendaServico.Count > 0 && posicao > 0)
            {
                posicao--; 
                atualizaCampos();
                SelecionarLinhaDataGrid();
            }
            else
            {
               
            }
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (lista_vendaServico.Count > 0 && posicao < lista_vendaServico.Count - 1)
            {
                posicao++; 
                atualizaCampos();
                SelecionarLinhaDataGrid();
            }
            else
            {
                
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            if (lista_vendaServico.Count > 0)
            {
                posicao = lista_vendaServico.Count - 1; 
                atualizaCampos();
                SelecionarLinhaDataGrid();
            }
            else
            {
                MessageBox.Show("Não há registros para navegar.");
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string termoBusca = txtBuscar.Text.Trim(); 
            if (!string.IsNullOrEmpty(termoBusca))
            {
                int index = lista_vendaServico.FindIndex(v =>
                    v.cliente.nomecliente.IndexOf(termoBusca, StringComparison.OrdinalIgnoreCase) >= 0);

                if (index >= 0)
                {
                    posicao = index; 
                    atualizaCampos();
                    SelecionarLinhaDataGrid();
                }
                else
                {
                    MessageBox.Show("Nenhum registro encontrado com o termo especificado.");
                }
            }
            else
            {
                MessageBox.Show("Digite um termo de busca.");
            }
        }

       
        private void SelecionarLinhaDataGrid()
        {
            dataGridView1.ClearSelection();
            if (posicao >= 0 && posicao < dataGridView1.Rows.Count)
            {
                dataGridView1.Rows[posicao].Selected = true;
                dataGridView1.FirstDisplayedScrollingRowIndex = posicao;
            }
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

