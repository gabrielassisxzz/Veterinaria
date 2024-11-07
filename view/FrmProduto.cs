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
    public partial class FrmProduto : Form
    {
        DataTable Tabela_produtos;
        Boolean novo = true;
        int posicao = 0;
        List<Produto> lista_produto = new List<Produto>();
        List<Marca> marcas = new List<Marca>();
        List<Tipoproduto> tipoprodutos = new List<Tipoproduto>();
        private int posicao_produto;
        private int posicao_tipoproduto;
        private string posicao_ovalor;
        private int posicao_marca;
        private string posicao_quantidade;

        public FrmProduto()
        {
            InitializeComponent();

            CarregaTabela();
            preencheComboMarca();
            preencheComboTipoproduto();

            lista_produto = carregaListaProduto();

            if (lista_produto.Count - 1 > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void atualizaCampos()
        {
            txtCodigo.Text = lista_produto[posicao].codproduto.ToString();
            txtProduto.Text = lista_produto[posicao].nomeproduto.ToString();
            cmbMarca.SelectedValue = lista_produto[posicao].marca.codmarca;
            txtQuantidade.Text = lista_produto[posicao].quantidadeproduto.ToString();
            txtValor.Text = lista_produto[posicao].valorproduto.ToString();
            cmbTipoProduto.SelectedValue = lista_produto[posicao].tipoproduto.codtipoproduto;
        }

        private void CarregaTabela()
        {
            try
            {
                C_Produto c_Produto = new C_Produto();
                Tabela_produtos = c_Produto.Buscar_Todos();
                dataGridView1.DataSource = Tabela_produtos;

                if (Tabela_produtos.Rows.Count > 0)
                {
                    //MessageBox.Show("Dados carregados com sucesso!");
                }
                else
                {
                    MessageBox.Show("Nenhum dado encontrado.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message);
            }
        }

        List<Produto> carregaListaProduto()
        {
            List<Produto> lista = new List<Produto>();
            C_Produto c_Produto = new C_Produto();
            lista = c_Produto.DadosProduto();
            return lista;
        }

        List<Produto> carregaListaProdutoFiltro()
        {
            List<Produto> lista = new List<Produto>();
            C_Produto c_Produto = new C_Produto();
            lista = c_Produto.DadosProdutoFiltro(txtBuscar.Text);
            return lista;
        }

        private void preencheComboMarca()
        {
            C_Marca c_Marca = new C_Marca();
            marcas = c_Marca.DadosMarca();
            cmbMarca.DataSource = marcas;
            cmbMarca.DisplayMember = "nomemarca";
            cmbMarca.ValueMember = "codmarca";
            cmbMarca.SelectedIndex = -1; 
        }

        private void preencheComboTipoproduto()
        {
            C_Tipoproduto c_Tipoproduto = new C_Tipoproduto();
            tipoprodutos = c_Tipoproduto.DadosTipoproduto();
            cmbTipoProduto.DataSource = tipoprodutos;
            cmbTipoProduto.DisplayMember = "nometipoproduto";
            cmbTipoProduto.ValueMember = "codtipoproduto";
            cmbTipoProduto.SelectedIndex = -1; 
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
            txtProduto.Enabled = true;
            cmbTipoProduto.Enabled = true;
            cmbMarca.Enabled = true;
            txtQuantidade.Enabled = true;
            txtValor.Enabled = true;
        }

        private void limparCampos()
        {
            txtCodigo.Text = "";
            txtProduto.Text = "";
            cmbTipoProduto.SelectedIndex = -1;
            cmbMarca.SelectedIndex = -1;
            txtValor.Text = "";
            txtQuantidade.Text = "";
        }

        private void desativaCampos()
        {
            txtProduto.Enabled = false;
            cmbTipoProduto.Enabled = false;
            cmbMarca.Enabled = false;
            txtValor.Enabled = false;
            txtQuantidade.Enabled = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                C_Produto c_Produto = new C_Produto();
                Produto produto = new Produto();
                produto.nomeproduto = txtProduto.Text;

                if (int.TryParse(cmbMarca.SelectedValue?.ToString(), out int marcaId))
                {
                    produto.marca = new Marca { codmarca = marcaId };
                }
                produto.quantidadeproduto = Double.Parse(txtQuantidade.Text);
                produto.valorproduto = Double.Parse(txtValor.Text);

                if (int.TryParse(cmbTipoProduto.SelectedValue?.ToString(), out int tipoProdutoId))
                {
                    produto.tipoproduto = new Tipoproduto { codtipoproduto = tipoProdutoId };
                }

                if (novo)
                {
                    c_Produto.InserirProduto(produto);
                    //MessageBox.Show("Produto inserido com sucesso!");
                }
                else
                {
                    c_Produto.Atualizar_Dados(produto);
                    //MessageBox.Show("Produto atualizado com sucesso!");
                }

                CarregaTabela();
                limparCampos();
                desativaCampos();
                desativaBotoes();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Erro ao salvar produto: " + ex.Message);
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
                    C_Produto Produto = new C_Produto();
                    int valor = Int32.Parse(txtCodigo.Text);
                    Produto.Apaga_Dados(valor);

                    CarregaTabela();

                    MessageBox.Show("Produto apagado com sucesso.");
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
                    MessageBox.Show("Erro ao apagar Produto: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor, insira o código do Produto a ser apagado.");
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
            if (lista_produto.Count - 1 > posicao)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao++;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows[posicao].Selected = false;
            posicao = lista_produto.Count - 1;
            atualizaCampos();
            dataGridView1.Rows[posicao].Selected = true;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBuscar.Text))
            {
                MessageBox.Show("Por favor, insira o nome do produto para buscar.");
                return;
            }

            try
            {
                lista_produto = carregaListaProdutoFiltro();
                if (lista_produto.Count > 0)
                {
                    posicao = 0;
                    atualizaCampos();
                    dataGridView1.DataSource = lista_produto;
                    dataGridView1.Rows[posicao].Selected = true;
                }
                else
                {
                    MessageBox.Show("Nenhum produto encontrado com esse nome.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar produto: " + ex.Message);
            }
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0 && index < dataGridView1.Rows.Count)
            {
                DataGridViewRow dr = dataGridView1.Rows[index];
                txtCodigo.Text = dr.Cells[0].Value.ToString();
                txtProduto.Text = dr.Cells[1].Value.ToString();
                cmbTipoProduto.SelectedValue = Convert.ToInt32(dr.Cells[2].Value);
                cmbMarca.SelectedValue = Convert.ToInt32(dr.Cells[3].Value);
                txtQuantidade.Text = dr.Cells[4].Value.ToString();
                txtValor.Text = dr.Cells[5].Value.ToString();
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

