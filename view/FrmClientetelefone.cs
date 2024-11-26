using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Veterinaria.control;
using Veterinaria.model;

namespace Veterinaria.view
{
    public partial class FrmClienteTelefone : Form
    {
        private List<Clientetelefone> lista_clienteTelefone = new List<Clientetelefone>();
        private List<Telefone> lista_telefone = new List<Telefone>();
        private List<Cliente> lista_cliente = new List<Cliente>();
        private int posicao = 0;
        private bool novo = true;

        public FrmClienteTelefone()
        {
            InitializeComponent();
            CarregarDados();
        }


        private void CarregarDados()
        {
            try
            {
                C_ClienteTelefone c_ClienteTelefone = new C_ClienteTelefone();
                lista_clienteTelefone = c_ClienteTelefone.DadosClienteTelefone();

                PreencherComboTelefone();
                PreencherComboCliente();

                if (lista_clienteTelefone.Count > 0)
                {

                    AtualizarTabela();
                }
                else
                {
                    dataGridView1.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message);
            }
        }


        private void AtualizarTabela()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = lista_clienteTelefone;

            
            if (dataGridView1.Columns["telefone"] != null)
                dataGridView1.Columns["telefone"].Visible = false;

            if (dataGridView1.Columns["cliente"] != null)
                dataGridView1.Columns["cliente"].Visible = false;
        }

        private void PreencherComboTelefone()
        {
            try
            {
                C_Telefone c_Telefone = new C_Telefone();
                lista_telefone = c_Telefone.DadosTelefone();

                if (lista_telefone.Count > 0)
                {
                    cmbTelefone.DataSource = lista_telefone;
                    cmbTelefone.DisplayMember = "numerotelefone";
                    cmbTelefone.ValueMember = "codtelefone";
                    cmbTelefone.SelectedIndex = -1;
                }
                else
                {
                }
            }
            catch (Exception ex)
            {

            }
        }


        private void PreencherComboCliente()
        {
            try
            {
                C_Cliente c_Cliente = new C_Cliente();
                lista_cliente = c_Cliente.DadosCliente();

                if (lista_cliente.Count > 0)
                {
                    cmbCliente.DataSource = lista_cliente;
                    cmbCliente.DisplayMember = "nomecliente";
                    cmbCliente.ValueMember = "codcliente";
                    cmbCliente.SelectedIndex = -1;
                }
                else
                {
                   
                }
            }
            catch (Exception ex)
            {
               
            }
        }


        private void AtualizarCampos()
        {
            if (posicao >= 0 && posicao < lista_clienteTelefone.Count)
            {
                var atual = lista_clienteTelefone[posicao];
                cmbTelefone.SelectedValue = atual.telefone.codtelefone;
                cmbCliente.SelectedValue = atual.cliente.codcliente;
                SelecionarLinhaDataGrid();
            }
        }

        private void SelecionarLinhaDataGrid()
        {
            if (dataGridView1.Rows.Count > 0 && posicao >= 0 && posicao < dataGridView1.Rows.Count)
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[posicao].Selected = true;
                dataGridView1.FirstDisplayedScrollingRowIndex = posicao;
            }
        }

        private void LimparCampos()
        {
            cmbTelefone.SelectedIndex = -1;
            cmbCliente.SelectedIndex = -1;
        }
        private void btnNovo_Click(object sender, EventArgs e)
        {
            AtivarCampos();
            AtivaBotoes();
            novo = true;
        }

        private void AtivaBotoes()
        {
            btnNovo.Enabled = false;
            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void AtivarCampos()
        {
            cmbTelefone.Enabled = true;
            cmbCliente.Enabled = true;
        }

        private void DesativaCampos()
        {
            cmbTelefone.Enabled = false;
            cmbCliente.Enabled = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbTelefone.SelectedIndex == -1 || cmbCliente.SelectedIndex == -1)
                {
                    MessageBox.Show("Por favor, selecione um telefone e um cliente.");
                    return;
                }

                Clientetelefone clienteTelefone = new Clientetelefone
                {
                    telefone = new Telefone { codtelefone = (int)cmbTelefone.SelectedValue },
                    cliente = new Cliente { codcliente = (int)cmbCliente.SelectedValue }
                };

                C_ClienteTelefone c_ClienteTelefone = new C_ClienteTelefone();

                if (novo)
                    c_ClienteTelefone.InserirClienteTelefone(clienteTelefone);
                else
                    c_ClienteTelefone.AtualizarClienteTelefone(clienteTelefone);

                CarregarDados();
                LimparCampos();
                novo = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar dados: " + ex.Message);
            }

        }
        private void desativaBotoes()
        {
            btnNovo.Enabled = true;
            btnApagar.Enabled = true;
            btnEditar.Enabled = true;

            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            AtivarCampos();
            AtivaBotoes();
            novo = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

            desativaBotoes();
            DesativaCampos();
            LimparCampos();
            novo = true;
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            try
            {

                if (lista_clienteTelefone == null || lista_clienteTelefone.Count == 0)
                {
                    MessageBox.Show("Não há registros para apagar.");
                    return;
                }


                if (posicao < 0 || posicao >= lista_clienteTelefone.Count)
                {
                    MessageBox.Show("Selecione um registro válido para apagar.");
                    return;
                }

                var clienteTelefone = lista_clienteTelefone[posicao];

                
                DialogResult confirmacao = MessageBox.Show(
                    $"Deseja realmente excluir a relação entre o cliente '{clienteTelefone.cliente.nomecliente}' e o telefone '{clienteTelefone.telefone.numerotelefone}'?",
                    "Confirmação",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmacao == DialogResult.Yes)
                {

                    var c_ClienteTelefone = new C_ClienteTelefone();
                    c_ClienteTelefone.Apaga_Dados(clienteTelefone.telefone.codtelefone, clienteTelefone.cliente.codcliente);


                    CarregarDados();
                    LimparCampos();

                    MessageBox.Show("Apaguei");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao apagar registro: " + ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                posicao = e.RowIndex;
                AtualizarCampos();
            }
        }

        
        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            if (lista_clienteTelefone.Count > 0)
            {
                posicao = 0;
                AtualizarCampos();
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (lista_clienteTelefone.Count > 0 && posicao > 0)
            {
                posicao--;
                AtualizarCampos();
            }
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (lista_clienteTelefone.Count > 0 && posicao < lista_clienteTelefone.Count - 1)
            {
                posicao++;
                AtualizarCampos();
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            if (lista_clienteTelefone.Count > 0)
            {
                posicao = lista_clienteTelefone.Count - 1;
                AtualizarCampos();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string termoBusca = txtBuscar.Text.Trim();
            if (!string.IsNullOrEmpty(termoBusca))
            {
                int index = lista_clienteTelefone.FindIndex(c => c.cliente.nomecliente.Contains(termoBusca));

                if (index >= 0)
                {
                    posicao = index;
                    AtualizarCampos();
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
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza que deseja sair?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)

            {
                this.Close();
            }
        }
    }
}
