using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using Veterinaria.conection;
using Veterinaria.control;
using Veterinaria.model;

namespace Veterinaria.view
{
    public partial class FrmItensVendaServico : Form
    {
        private DataTable Tabela_itens;
        private List<Itensvendaservico> lista_itens = new List<Itensvendaservico>();
        private List<Tiposervico> lista_tiposervico = new List<Tiposervico>();
        private List<CidAnimal> lista_cidanimal = new List<CidAnimal>();
        private int posicao = 0;
        private bool novo = true;

        public FrmItensVendaServico()
        {
            InitializeComponent();
            CarregaTabela();
            PreencheComboTipoServico();
            PreencheComboVendaServico();
            PreencheComboCidAnimal();
            lista_itens = CarregaListaItens();

            if (lista_itens.Count > 0)
            {
                posicao = 0;
                AtualizaCampos();
            }
        }

        private void AtualizaCampos()
        {
            if (lista_itens.Count > 0 && posicao >= 0 && posicao < lista_itens.Count)
            {
                var itemAtual = lista_itens[posicao];
                txtQuantidade.Text = itemAtual.quantidade.ToString("F2");
                txtValor.Text = itemAtual.valor.ToString("F2");
                cmbTipoServico.SelectedValue = itemAtual.tiposervico.codtiposervico;
                cmbVendaServico.SelectedValue = itemAtual.vendaservico.codvendaservico;
                cmbCidAnimal.SelectedValue = itemAtual.cidanimal.Codcidanimal;

                if (dataGridView1.Rows.Count > posicao)
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[posicao].Selected = true;
                    dataGridView1.CurrentCell = dataGridView1.Rows[posicao].Cells[0];
                }
            }
        }

        private void CarregaTabela()
        {
            try
            {
                C_ItensVendaServico c_Itens = new C_ItensVendaServico();
                Tabela_itens = c_Itens.Buscar_Todos();
                dataGridView1.DataSource = Tabela_itens;

                lista_itens = c_Itens.DadosItensVendaServico();
                posicao = lista_itens.Count > 0 ? 0 : -1;
                AtualizaCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message);
            }
        }

        private List<Itensvendaservico> CarregaListaItens()
        {
            C_ItensVendaServico c_Itens = new C_ItensVendaServico();
            return c_Itens.DadosItensVendaServico();
        }

        private void PreencheComboTipoServico()
        {
            C_TipoServico c_TipoServico = new C_TipoServico();
            lista_tiposervico = c_TipoServico.DadosTipoServico();
            cmbTipoServico.DataSource = lista_tiposervico;
            cmbTipoServico.DisplayMember = "DescricaoTipoServico";
            cmbTipoServico.ValueMember = "codTipoServico";
            cmbTipoServico.SelectedIndex = -1;
        }

        private void PreencheComboVendaServico()
        {
            C_VendaServico c_VendaServico = new C_VendaServico();
            var lista_vendaServico = c_VendaServico.DadosVendaServico();
            cmbVendaServico.DataSource = lista_vendaServico;
            cmbVendaServico.DisplayMember = "DescricaoVendaServico";
            cmbVendaServico.ValueMember = "codVendaServico";
            cmbVendaServico.SelectedIndex = -1;
        }

        private void PreencheComboCidAnimal()
        {
            C_CidAnimal c_CidAnimal = new C_CidAnimal();
            lista_cidanimal = c_CidAnimal.DadosCidAnimal();
            cmbCidAnimal.DataSource = lista_cidanimal;
            cmbCidAnimal.DisplayMember = "DescricaoCidAnimal";
            cmbCidAnimal.ValueMember = "codCidAnimal";
            cmbCidAnimal.SelectedIndex = -1;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimparCampos();
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
            cmbTipoServico.Enabled = true;
            cmbVendaServico.Enabled = true;
            cmbCidAnimal.Enabled = true;
            txtQuantidade.Enabled = true;
            txtValor.Enabled = true;
        }

        private void LimparCampos()
        {
            cmbTipoServico.SelectedIndex = -1;
            cmbVendaServico.SelectedIndex = -1;
            cmbCidAnimal.SelectedIndex = -1;
            txtQuantidade.Text = "";
            txtValor.Text = "";
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbTipoServico.SelectedIndex == -1 || cmbVendaServico.SelectedIndex == -1 || cmbCidAnimal.SelectedIndex == -1)
                {
                    MessageBox.Show("Por favor, selecione todos os campos necessários.");
                    return;
                }

                Itensvendaservico item = new Itensvendaservico
                {
                    tiposervico = new Tiposervico { codtiposervico = (int)cmbTipoServico.SelectedValue },
                    vendaservico = new VendaServico { codvendaservico = (int)cmbVendaServico.SelectedValue },
                    cidanimal = new CidAnimal { Codcidanimal = (int)cmbCidAnimal.SelectedValue },
                    quantidade = double.Parse(txtQuantidade.Text),
                    valor = double.Parse(txtValor.Text)
                };

                C_ItensVendaServico c_Itens = new C_ItensVendaServico();

                if (novo)
                {
                    c_Itens.InserirItensVendaServico(item);
                }
                else
                {
                    c_Itens.AtualizarItensVendaServico(item);
                }

                CarregaTabela();
                LimparCampos();
                DesativaBotoes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar item: " + ex.Message);
            }
        }

        private void DesativaBotoes()
        {
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparCampos();
            DesativaBotoes();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0 && index < dataGridView1.Rows.Count)
            {
                DataGridViewRow dr = dataGridView1.Rows[index];
                txtQuantidade.Text = dr.Cells[1].Value.ToString();
                txtValor.Text = dr.Cells[2].Value.ToString();
                cmbTipoServico.SelectedValue = Convert.ToInt32(dr.Cells[3].Value);
                cmbVendaServico.SelectedValue = Convert.ToInt32(dr.Cells[4].Value);
                cmbCidAnimal.SelectedValue = Convert.ToInt32(dr.Cells[5].Value);
            }
        }

        private void CarregarTipoServico()
        {
            using (SqlConnection conn = new Conexao().ConectarBanco())
            {
                string sql = "SELECT codtiposervico, nome FROM tiposervico";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbTipoServico.DataSource = dt;
                cmbTipoServico.DisplayMember = "nome";
                cmbTipoServico.ValueMember = "codtiposervico";
            }
        }

        private void CarregarVendaServico()
        {
            using (SqlConnection conn = new Conexao().ConectarBanco())
            {
                string sql = "SELECT codvendaservico, nome FROM vendaservico";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbVendaServico.DataSource = dt;
                cmbVendaServico.DisplayMember = "nome";
                cmbVendaServico.ValueMember = "codvendaservico";
            }
        }



        private void btnApagar_Click(object sender, EventArgs e)
        {

        }


        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (posicao >= 0 && posicao < lista_itens.Count)
                {
                    
                    AtivarCampos();
                    novo = false; 

                   
                    var itemAtual = lista_itens[posicao];
                    txtQuantidade.Text = itemAtual.quantidade.ToString("F2");
                    txtValor.Text = itemAtual.valor.ToString("F2");
                    cmbTipoServico.SelectedValue = itemAtual.tiposervico.codtiposervico;
                    cmbVendaServico.SelectedValue = itemAtual.vendaservico.codvendaservico;
                    cmbCidAnimal.SelectedValue = itemAtual.cidanimal.Codcidanimal;

                   
                    btnSalvar.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Selecione um item para editar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao editar o item: " + ex.Message);
            }
        }


        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            if (lista_itens.Count > 0)
            {
                posicao = 0;
                AtualizaCampos();
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (lista_itens.Count > 0 && posicao > 0)
            {
                posicao--;
                AtualizaCampos();
            }
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (lista_itens.Count > 0 && posicao < lista_itens.Count - 1)
            {
                posicao++;
                AtualizaCampos();
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            if (lista_itens.Count > 0)
            {
                posicao = lista_itens.Count - 1;
                AtualizaCampos();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string termoBusca = txtBuscar.Text.Trim();
            if (!string.IsNullOrEmpty(termoBusca))
            {
               
                int index = lista_itens.FindIndex(i =>
                    i.vendaservico.codvendaservico.ToString().IndexOf(termoBusca, StringComparison.OrdinalIgnoreCase) >= 0);

                if (index >= 0)
                {
                    posicao = index;
                    AtualizaCampos();
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


        private void toolStripButton1_Click(object sender, EventArgs e)
            {
                if (MessageBox.Show("Tem certeza que deseja sair?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)

                {
                    this.Close();
                }
            }
        }
    }



