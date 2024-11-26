using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Veterinaria.control;
using Veterinaria.model;

namespace Veterinaria.view
{
    public partial class FrmTelefoneLoja : Form
    {
        private List<Telefoneloja> lista_telefoneLoja = new List<Telefoneloja>();
        private List<Telefone> lista_telefone = new List<Telefone>();
        private List<Loja> lista_loja = new List<Loja>();
        private int posicao = 0;
        private bool novo = true;

        public FrmTelefoneLoja()
        {
            InitializeComponent();
            CarregarDados();
        }

        private void CarregarDados()
        {
            try
            {
                C_TelefoneLoja c_TelefoneLoja = new C_TelefoneLoja();
                lista_telefoneLoja = c_TelefoneLoja.DadosTelefoneLoja();

                PreencherComboTelefone();
                PreencherComboLoja();

                if (lista_telefoneLoja.Count > 0)
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
            dataGridView1.DataSource = lista_telefoneLoja;

            // Aqui você pode esconder as colunas 'telefone' e 'loja' como no seu outro formulário
            if (dataGridView1.Columns["telefone"] != null)
                dataGridView1.Columns["telefone"].Visible = false;

            if (dataGridView1.Columns["loja"] != null)
                dataGridView1.Columns["loja"].Visible = false;
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

        private void PreencherComboLoja()
        {
            try
            {
                C_Loja c_Loja = new C_Loja();
                lista_loja = c_Loja.DadosLoja();

                if (lista_loja.Count > 0)
                {
                    cmbLoja.DataSource = lista_loja;
                    cmbLoja.DisplayMember = "nomeloja";
                    cmbLoja.ValueMember = "codloja";
                    cmbLoja.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar lojas: " + ex.Message);
            }
        }

        private void AtualizarCampos()
        {
            if (posicao >= 0 && posicao < lista_telefoneLoja.Count)
            {
                var atual = lista_telefoneLoja[posicao];
                cmbTelefone.SelectedValue = atual.telefone.codtelefone;
                cmbLoja.SelectedValue = atual.loja.codloja;
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
            cmbLoja.SelectedIndex = -1;
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
            cmbLoja.Enabled = true;
        }

        private void DesativaCampos()
        {
            cmbTelefone.Enabled = false;
            cmbLoja.Enabled = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbTelefone.SelectedIndex == -1 || cmbLoja.SelectedIndex == -1)
                {
                    MessageBox.Show("Por favor, selecione um telefone e uma loja.");
                    return;
                }

                Telefoneloja telefoneLoja = new Telefoneloja
                {
                    telefone = new Telefone { codtelefone = (int)cmbTelefone.SelectedValue },
                    loja = new Loja { codloja = (int)cmbLoja.SelectedValue }
                };

                C_TelefoneLoja c_TelefoneLoja = new C_TelefoneLoja();

                if (novo)
                    c_TelefoneLoja.InserirTelefoneLoja(telefoneLoja);
                else
                    c_TelefoneLoja.AtualizarTelefoneLoja(telefoneLoja);

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
                if (lista_telefoneLoja == null || lista_telefoneLoja.Count == 0)
                {
                    MessageBox.Show("Não há registros para apagar.");
                    return;
                }

                if (posicao < 0 || posicao >= lista_telefoneLoja.Count)
                {
                    MessageBox.Show("Selecione um registro válido para apagar.");
                    return;
                }

                var telefoneLoja = lista_telefoneLoja[posicao];

                DialogResult confirmacao = MessageBox.Show(
                    $"Deseja realmente excluir a relação entre a loja '{telefoneLoja.loja.nomeloja}' e o telefone '{telefoneLoja.telefone.numerotelefone}'?",
                    "Confirmação",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmacao == DialogResult.Yes)
                {
                    var c_TelefoneLoja = new C_TelefoneLoja();
                    c_TelefoneLoja.Apaga_Dados(telefoneLoja.telefone.codtelefone, telefoneLoja.loja.codloja);

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
            if (lista_telefoneLoja.Count > 0)
            {
                posicao = 0;
                AtualizarCampos();
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (lista_telefoneLoja.Count > 0 && posicao > 0)
            {
                posicao--;
                AtualizarCampos();
            }
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (lista_telefoneLoja.Count > 0 && posicao < lista_telefoneLoja.Count - 1)
            {
                posicao++;
                AtualizarCampos();
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            if (lista_telefoneLoja.Count > 0)
            {
                posicao = lista_telefoneLoja.Count - 1;
                AtualizarCampos();
            }
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string termoBusca = txtBuscar.Text.Trim();
            if (!string.IsNullOrEmpty(termoBusca))
            {
                int index = lista_telefoneLoja.FindIndex(c => c.telefone.numerotelefone.Contains(termoBusca));

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