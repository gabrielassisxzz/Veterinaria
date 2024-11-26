using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Veterinaria.control;
using Veterinaria.model;

namespace Veterinaria.view
{
    public partial class FrmVendasProdutos : Form
    {
        private DataTable Tabela_vendas;
        private List<VendasProdutos> lista_venda = new List<VendasProdutos>();
        private List<Produto> lista_produto = new List<Produto>();
        private int posicao = 0;
        private bool novo = true;


        public FrmVendasProdutos()
        {
            InitializeComponent();
            CarregaTabela();
            PreencheComboProduto();
            PreencheComboVendas();
            lista_venda = CarregaListaVendas();

            if (lista_venda.Count > 0)
            {
                posicao = 0;
                AtualizaCampos();
            }
        }

        private void AtualizaCampos()
        {
            if (lista_venda.Count > 0 && posicao >= 0 && posicao < lista_venda.Count)
            {
                var vendaAtual = lista_venda[posicao];
                txtQuantidade.Text = vendaAtual.quantidadevendasprodutos.ToString("F2");
                txtValor.Text = vendaAtual.valorvendaprodutos.ToString("F2");
                cmbProduto.SelectedValue = vendaAtual.produto.codproduto;
                cmbVenda.SelectedValue = vendaAtual.vendas.codvenda;

                
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
                C_VendasProdutos c_Venda = new C_VendasProdutos();
                Tabela_vendas = c_Venda.Buscar_Todos();
                dataGridView1.DataSource = Tabela_vendas;

                lista_venda = c_Venda.DadosVendasProdutos(); 
                posicao = lista_venda.Count > 0 ? 0 : -1; 
                AtualizaCampos();

                if (Tabela_vendas.Rows.Count == 0)
                {
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message);
            }
        }



        private List<VendasProdutos> CarregaListaVendas()
        {
            C_VendasProdutos c_Venda = new C_VendasProdutos();
            return c_Venda.DadosVendasProdutos();
        }

        private void PreencheComboProduto()
        {
            C_Produto c_Produto = new C_Produto();
            lista_produto = c_Produto.DadosProduto();
            cmbProduto.DataSource = lista_produto;
            cmbProduto.DisplayMember = "nomeproduto";
            cmbProduto.ValueMember = "codproduto";
            cmbProduto.SelectedIndex = -1;
        }

        private void PreencheComboVendas()
        {
            C_Vendas c_Venda = new C_Vendas();
            var lista_vendas = c_Venda.DadosVendas(); 
            cmbVenda.DataSource = lista_vendas;
            cmbVenda.DisplayMember = "DescricaoVenda"; 
            cmbVenda.ValueMember = "codvenda"; 
            cmbVenda.SelectedIndex = -1;
            
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
            cmbProduto.Enabled = true;
            txtQuantidade.Enabled = true;
            txtValor.Enabled = true;
        }

        private void LimparCampos()
        {
            cmbProduto.SelectedIndex = -1;
            txtQuantidade.Text = "";
            txtValor.Text = "";
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbProduto.SelectedIndex == -1 || cmbVenda.SelectedIndex == -1)
                {
                    MessageBox.Show("Por favor, selecione um produto e uma venda.");
                    return;
                }

                VendasProdutos venda = new VendasProdutos
                {
                    vendas = new Vendas { codvenda = (int)cmbVenda.SelectedValue },
                    produto = new Produto { codproduto = (int)cmbProduto.SelectedValue },
                    quantidadevendasprodutos = double.Parse(txtQuantidade.Text),
                    valorvendaprodutos = double.Parse(txtValor.Text)
                };

                C_VendasProdutos c_Venda = new C_VendasProdutos();

                if (novo)
                {
                    c_Venda.InserirVendaProduto(venda);
                }
                else
                {
                    c_Venda.AtualizarVendaProduto(venda);
                }

                CarregaTabela();
                LimparCampos();
                DesativaBotoes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar venda: " + ex.Message);
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
                cmbProduto.SelectedValue = Convert.ToInt32(dr.Cells[3].Value);
                cmbVenda.SelectedValue = Convert.ToInt32(dr.Cells[3].Value);
            }
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            
            {
                try
                {
                    if (cmbVenda.SelectedIndex == -1)
                    {
                        
                        return;
                    }

                    int codVenda = (int)cmbVenda.SelectedValue;
                    C_VendasProdutos c_Venda = new C_VendasProdutos();
                    c_Venda.Apaga_Dados(codVenda);

                    MessageBox.Show("Apaguei");
                    CarregaTabela();
                    LimparCampos();
                }
                catch (Exception ex)
                {
                    
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            AtivarCampos();
            AtivaBotoes();
            novo = false; 
        }

        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            if (lista_venda.Count > 0)
            {
                posicao = 0;
                AtualizaCampos();
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (posicao > 0)
            {
                posicao--;
                AtualizaCampos();
            }
           
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (posicao < lista_venda.Count - 1)
            {
                posicao++;
                AtualizaCampos();
            }
           
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            if (lista_venda.Count > 0)
            {
                posicao = lista_venda.Count - 1;
                AtualizaCampos();
            }
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string searchTerm = txtBuscar.Text; 
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                MessageBox.Show("Por favor, insira um termo de busca.");
                return;
            }

            try
            {
                ;
                if (lista_venda.Count > 0)
                {
                    posicao = 0;
                    AtualizaCampos();
                }
                else
                {
                    
                }
            }
            catch (Exception ex)
            {
                
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
