using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Veterinaria.control;
using Veterinaria.model;

namespace Veterinaria.view
{
    public partial class FrmFuncionarioTelefone : Form
    {
        private List<Funcionariotelefone> lista_funcionarioTelefone = new List<Funcionariotelefone>();
        private List<Telefone> lista_telefone = new List<Telefone>();
        private List<Funcionario> lista_funcionario = new List<Funcionario>();
        private int posicao = 0;
        private bool novo = true;

        public FrmFuncionarioTelefone()
        {
            InitializeComponent();
            CarregarDados();
        }

        private void CarregarDados()
        {
            try
            {
                C_FuncionarioTelefone c_FuncionarioTelefone = new C_FuncionarioTelefone();
                lista_funcionarioTelefone = c_FuncionarioTelefone.DadosFuncionarioTelefone();

                PreencherComboTelefone();
                PreencherComboFuncionario();

                if (lista_funcionarioTelefone.Count > 0)
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
            dataGridView1.DataSource = lista_funcionarioTelefone;

            if (dataGridView1.Columns["telefone"] != null)
                dataGridView1.Columns["telefone"].Visible = false;

            if (dataGridView1.Columns["funcionario"] != null)
                dataGridView1.Columns["funcionario"].Visible = false;
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar telefones: " + ex.Message);
            }
        }

        private void PreencherComboFuncionario()
        {
            try
            {
                C_Funcionario c_Funcionario = new C_Funcionario();
                lista_funcionario = c_Funcionario.DadosFuncionario();

                if (lista_funcionario.Count > 0)
                {
                    cmbFuncionario.DataSource = lista_funcionario;
                    cmbFuncionario.DisplayMember = "nomefuncionario";
                    cmbFuncionario.ValueMember = "codfuncionario";
                    cmbFuncionario.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar funcionários: " + ex.Message);
            }
        }

        private void AtualizarCampos()
        {
            if (posicao >= 0 && posicao < lista_funcionarioTelefone.Count)
            {
                var atual = lista_funcionarioTelefone[posicao];
                cmbTelefone.SelectedValue = atual.telefone.codtelefone;
                cmbFuncionario.SelectedValue = atual.funcionario.codfuncionario;
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
            cmbFuncionario.SelectedIndex = -1;
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
            cmbFuncionario.Enabled = true;
        }

        private void DesativaCampos()
        {
            cmbTelefone.Enabled = false;
            cmbFuncionario.Enabled = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbTelefone.SelectedIndex == -1 || cmbFuncionario.SelectedIndex == -1)
                {
                    MessageBox.Show("Por favor, selecione um telefone e um funcionário.");
                    return;
                }

                Funcionariotelefone funcionarioTelefone = new Funcionariotelefone
                {
                    telefone = new Telefone { codtelefone = (int)cmbTelefone.SelectedValue },
                    funcionario = new Funcionario { codfuncionario = (int)cmbFuncionario.SelectedValue }
                };

                C_FuncionarioTelefone c_FuncionarioTelefone = new C_FuncionarioTelefone();

                if (novo)
                    c_FuncionarioTelefone.InserirFuncionarioTelefone(funcionarioTelefone);
                else
                    c_FuncionarioTelefone.AtualizarFuncionarioTelefone(funcionarioTelefone);

                CarregarDados();
                LimparCampos();
                novo = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar dados: " + ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            desativaBotoes();
            DesativaCampos();
            LimparCampos();
            novo = true;
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

        private void btnApagar_Click(object sender, EventArgs e)
        {
            try
            {
                if (lista_funcionarioTelefone == null || lista_funcionarioTelefone.Count == 0)
                {
                    MessageBox.Show("Não há registros para apagar.");
                    return;
                }

                if (posicao < 0 || posicao >= lista_funcionarioTelefone.Count)
                {
                    MessageBox.Show("Selecione um registro válido para apagar.");
                    return;
                }

                var funcionarioTelefone = lista_funcionarioTelefone[posicao];

                DialogResult confirmacao = MessageBox.Show(
                    $"Deseja realmente excluir a relação entre o funcionário '{funcionarioTelefone.funcionario.nomefuncionario}' e o telefone '{funcionarioTelefone.telefone.numerotelefone}'?",
                    "Confirmação",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmacao == DialogResult.Yes)
                {
                    var c_FuncionarioTelefone = new C_FuncionarioTelefone();
                    c_FuncionarioTelefone.Apaga_Dados(funcionarioTelefone.telefone.codtelefone, funcionarioTelefone.funcionario.codfuncionario);

                    CarregarDados();
                    LimparCampos();

                    
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
            if (lista_funcionarioTelefone.Count > 0)
            {
                posicao = 0;
                AtualizarCampos();
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (lista_funcionarioTelefone.Count > 0 && posicao > 0)
            {
                posicao--;
                AtualizarCampos();
            }
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (lista_funcionarioTelefone.Count > 0 && posicao < lista_funcionarioTelefone.Count - 1)
            {
                posicao++;
                AtualizarCampos();
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            if (lista_funcionarioTelefone.Count > 0)
            {
                posicao = lista_funcionarioTelefone.Count - 1;
                AtualizarCampos();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string termoBusca = txtBuscar.Text.Trim();
            if (!string.IsNullOrEmpty(termoBusca))
            {
                int index = lista_funcionarioTelefone.FindIndex(c => c.telefone.numerotelefone.Contains(termoBusca));

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
