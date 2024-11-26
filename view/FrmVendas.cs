using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Veterinaria.control;
using Veterinaria.model;

namespace Veterinaria.view
{
    public partial class FrmVendas : Form
    {
        DataTable Tabela_vendas;
        bool novo = true;
        int posicao = 0;
        List<Vendas> lista_vendas = new List<Vendas>();
        List<Cliente> clientes = new List<Cliente>();
        List<Funcionario> funcionarios = new List<Funcionario>();
        List<Loja> lojas = new List<Loja>();

        public FrmVendas()
        {
            InitializeComponent();
            CarregaTabela();
            preencheComboCliente();
            preencheComboFuncionario();
            preencheComboLoja();

            lista_vendas = carregaListaVendas();
            if (lista_vendas.Count > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void atualizaCampos()
        {
            txtCodigo.Text = lista_vendas[posicao].codvenda.ToString();
            txtData.Value = lista_vendas[posicao].datavenda;
            cmbCliente.SelectedValue = lista_vendas[posicao].cliente.codcliente;
            cmbFuncionario.SelectedValue = lista_vendas[posicao].funcionario.codfuncionario;
            cmbLoja.SelectedValue = lista_vendas[posicao].loja.codloja;
        }

        private void CarregaTabela()
        {
            try
            {
                C_Vendas c_Vendas = new C_Vendas();
                Tabela_vendas = c_Vendas.Buscar_Todos();
                dataGridView1.DataSource = Tabela_vendas;

                if (Tabela_vendas.Rows.Count == 0)
                {
                   // MessageBox.Show("Nenhum dado encontrado.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message);
            }
        }

        private List<Vendas> carregaListaVendas()
        {
            C_Vendas c_Vendas = new C_Vendas();
            return c_Vendas.DadosVendas();
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

        private void preencheComboLoja()
        {
            C_Loja c_Loja = new C_Loja();
            lojas = c_Loja.DadosLoja();
            cmbLoja.DataSource = lojas;
            cmbLoja.DisplayMember = "nomeloja";
            cmbLoja.ValueMember = "codloja";
            cmbLoja.SelectedIndex = -1;
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
                C_Vendas c_Vendas = new C_Vendas();
                Vendas venda = new Vendas
                {
                    datavenda = txtData.Value
                };

                if (int.TryParse(cmbCliente.SelectedValue?.ToString(), out int clienteId))
                {
                    venda.cliente = new Cliente { codcliente = clienteId };
                }

                if (int.TryParse(cmbFuncionario.SelectedValue?.ToString(), out int funcionarioId))
                {
                    venda.funcionario = new Funcionario { codfuncionario = funcionarioId };
                }

                if (int.TryParse(cmbLoja.SelectedValue?.ToString(), out int lojaId))
                {
                    venda.loja = new Loja { codloja = lojaId };
                }

                if (novo)
                {
                    c_Vendas.InserirVenda(venda);
                }
                else
                {
                    c_Vendas.AtualizarVenda(venda);
                }

                CarregaTabela();
                limparCampos();
                desativaCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar venda: " + ex.Message);
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
                    C_Vendas c_Vendas = new C_Vendas();
                    int valor = Int32.Parse(txtCodigo.Text);
                    c_Vendas.Apaga_Dados(valor);

                    CarregaTabela();
                    limparCampos();
                    desativaCampos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao apagar venda: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor, insira o código da venda a ser apagada.");
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            ativarCampos();
            novo = false;
        }

        private void limparCampos()
        {
            txtCodigo.Text = "";
            txtData.Value = DateTime.Now;
            cmbCliente.SelectedIndex = -1;
            cmbFuncionario.SelectedIndex = -1;
            cmbLoja.SelectedIndex = -1;
        }

        private void ativarCampos()
        {
            txtData.Enabled = true;
            cmbCliente.Enabled = true;
            cmbFuncionario.Enabled = true;
            cmbLoja.Enabled = true;
        }

        private void desativaCampos()
        {
            txtData.Enabled = false;
            cmbCliente.Enabled = false;
            cmbFuncionario.Enabled = false;
            cmbLoja.Enabled = false;
        }
        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            if (lista_vendas.Count > 0)
            {
                posicao = 0; 
                atualizaCampos();
                SelecionarLinhaDataGrid();
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (lista_vendas.Count > 0 && posicao > 0)
            {
                posicao--; 
                atualizaCampos();
                SelecionarLinhaDataGrid();
            }
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (lista_vendas.Count > 0 && posicao < lista_vendas.Count - 1)
            {
                posicao++; 
                atualizaCampos();
                SelecionarLinhaDataGrid();
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            if (lista_vendas.Count > 0)
            {
                posicao = lista_vendas.Count - 1; 
                atualizaCampos();
                SelecionarLinhaDataGrid();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string termoBusca = btnBuscar.Text.Trim(); 
            if (!string.IsNullOrEmpty(termoBusca))
            {
                int index = lista_vendas.FindIndex(v => v.cliente.nomecliente.Contains(termoBusca));

                if (index >= 0)
                {
                    posicao = index; 
                    atualizaCampos();
                    SelecionarLinhaDataGrid();
                }
                else
                {
                    MessageBox.Show("Nenhum registro encontrado.");
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
