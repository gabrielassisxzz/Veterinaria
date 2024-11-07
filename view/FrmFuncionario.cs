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
    public partial class FrmFuncionario : Form
    {
        DataTable Tabela_Funcionario;
        Boolean novo;
        int posicao;
        List<Funcionario> lista_Funcionario = new List<Funcionario>();
        List<Loja> Lojas = new List<Loja>();
        List<Tipofuncionario> Tipofuncionarios = new List<Tipofuncionario>();

        private int posicao_Tipofuncionario;
        private int posicao_Loja;

        public FrmFuncionario()
        {
            InitializeComponent();

            lista_Funcionario = carregaListaFuncionario();
            if (posicao < dataGridView1.Rows.Count)
            {
                dataGridView1.Rows[posicao].Selected = true;
            }

            preencheComboLoja();
            preencheComboTipoFuncionario();



        }

        private void atualizaCampos()
        {
            txtCodigo.Text = lista_Funcionario[posicao].codfuncionario.ToString();
            txtFuncionario.Text = lista_Funcionario[posicao].nomefuncionario.ToString();
            cmbTipoFuncionario.Text = lista_Funcionario[posicao].tipofuncionario.nometipofuncionario.ToString();
            cmbLoja.Text = lista_Funcionario[posicao].loja.nomeloja.ToString();
       
        }

        List<Funcionario> carregaListaFuncionario()
        {
            List<Funcionario> lista = new List<Funcionario>();

            C_Funcionario c_Funcionario = new C_Funcionario();
            lista = c_Funcionario.DadosFuncionario();

            return lista;
        }

        List<Funcionario> carregaListaFuncionarioFiltro()
        {
            List<Funcionario> lista = new List<Funcionario>();

            C_Funcionario c_Funcionario = new C_Funcionario();
            lista = c_Funcionario.DadosFuncionarioFiltro(txtBuscar.Text);

            return lista;
        }

        private void CarregaTabela()
        {
            C_Funcionario c_Funcionario = new C_Funcionario();
            DataTable dt = new DataTable();
            dt = c_Funcionario.Buscar_Todos();
            Tabela_Funcionario = dt;
            dataGridView1.DataSource = Tabela_Funcionario;
        }
    
          private void preencheComboTipoFuncionario()
        {
            C_Tipofuncionario c_TipoFuncionario = new C_Tipofuncionario();

        Tipofuncionarios = c_TipoFuncionario.DadosTipofuncionario();
        foreach (Tipofuncionario b in Tipofuncionarios)
        {
            cmbTipoFuncionario.Items.Add(b.nometipofuncionario);
        }
    }

    private void preencheComboLoja()
    {
        C_Loja c_Loja = new C_Loja();

        Lojas = c_Loja.DadosLoja();
        foreach (Loja Loja in Lojas)
        {
            cmbLoja.Items.Add(Loja.nomeloja);
        }
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
            txtFuncionario.Enabled = true;
            cmbTipoFuncionario.Enabled = true;
            cmbLoja.Enabled = true;

        }

        private void limparCampos()
        {
            txtCodigo.Text = "";
            txtFuncionario.Text = "";
            cmbTipoFuncionario.Text = "";
            cmbLoja.Text = "";

        }

        private void desativaCampos()
        {
            txtFuncionario.Enabled = false;
            cmbTipoFuncionario.Enabled = false;
            cmbLoja.Enabled = false;

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Funcionario Nova_Funcionario = new Funcionario();

            Tipofuncionario TipofuncionarioCopia = new Tipofuncionario();
            TipofuncionarioCopia.codtipofuncionario = Tipofuncionarios[posicao_Tipofuncionario].codtipofuncionario;
            Nova_Funcionario.tipofuncionario = TipofuncionarioCopia;

            Tipofuncionario Tipofuncionario = new Tipofuncionario();
            posicao_Tipofuncionario = cmbTipoFuncionario.SelectedIndex;
            Tipofuncionario.codtipofuncionario = Tipofuncionarios[posicao_Tipofuncionario].codtipofuncionario;
            Nova_Funcionario.tipofuncionario = Tipofuncionario;

            Nova_Funcionario.nomefuncionario = txtFuncionario.Text;

            Funcionario Funcionario = new Funcionario();
            Loja Loja = new Loja();
            posicao_Loja = cmbLoja.SelectedIndex;
            Loja.codloja = Lojas[posicao_Loja].codloja;
            Nova_Funcionario.loja = Loja;




            C_Funcionario c_Funcionario = new C_Funcionario();
            if (novo == true)
            {
                c_Funcionario.Insere_Dados(Nova_Funcionario);
            }
            else
            {
                Nova_Funcionario.codfuncionario = Int32.Parse(txtFuncionario.Text);
                c_Funcionario.Atualizar_Dados(Nova_Funcionario);
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

                    C_Funcionario Funcionario = new C_Funcionario();


                    int valor = Int32.Parse(txtCodigo.Text);


                    Funcionario.Apaga_Dados(valor);


                    CarregaTabela();


                    MessageBox.Show("Funcionario apagada com sucesso.");


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

                    MessageBox.Show("Erro ao apagar Funcionario: " + ex.Message);
                }
            }
            else
            {

                MessageBox.Show("Por favor, insira o código da Funcionario a ser apagada.");
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
            dataGridView1.Rows[posicao].Selected = false;
            posicao = lista_Funcionario.Count - 1;
            atualizaCampos();
            dataGridView1.Rows[posicao].Selected = true;
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (posicao > 0)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao--;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            if (lista_Funcionario.Count - 1 > posicao)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao++;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            C_Funcionario c_Funcionario = new C_Funcionario();
            DataTable dt = new DataTable();
            dt = c_Funcionario.Buscar_Filtro(txtBuscar.Text.ToString() + "%");
            Tabela_Funcionario = dt;
            dataGridView1.DataSource = Tabela_Funcionario;

            lista_Funcionario = carregaListaFuncionarioFiltro();

            if (lista_Funcionario.Count - 1 > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica se o índice da linha é válido (evita cliques em áreas não válidas)
            if (e.RowIndex >= 0)
            {
                DataGridViewRow dr = dataGridView1.Rows[e.RowIndex];

                // Verifica e define valores para os campos, tratando valores nulos
                txtCodigo.Text = dr.Cells[0].Value?.ToString() ?? string.Empty;
                txtFuncionario.Text = dr.Cells[1].Value?.ToString() ?? string.Empty;
                cmbTipoFuncionario.Text = dr.Cells[2].Value?.ToString() ?? string.Empty;
                cmbLoja.Text = dr.Cells[3].Value?.ToString() ?? string.Empty;
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