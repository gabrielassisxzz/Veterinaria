
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
    public partial class FrmCep : Form
    {
        DataTable Tabela_ceps;
        Boolean novo = true;
        int posicao;
        List<Cep> lista_cep = new List<Cep>();
        public FrmCep()
        {
            InitializeComponent();
            CarregaTabela();

            lista_cep = carregaListaCep();

            if (lista_cep.Count - 1 > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void atualizaCampos()
        {
            txtCodigo.Text = lista_cep[posicao].codcep.ToString();
            txtCep.Text = lista_cep[posicao].numerocep.ToString();
        }

        List<Cep> carregaListaCep()
        {
            List<Cep> lista = new List<Cep>();

            C_Cep cr = new C_Cep();
            lista = cr.DadosCep();

            return lista;
        }
        List<Cep> carregaListaCepFiltro()
        {
            List<Cep> lista = new List<Cep>();

            C_Cep cr = new C_Cep();
            lista = cr.DadosCepFiltro(txtBuscar.Text);

            return lista;
        }

        public void CarregaTabela()
        {
            C_Cep cr = new C_Cep();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Todos();
            Tabela_ceps = dt;
            dataGridView1.DataSource = Tabela_ceps;
        }

        private void FrmCep_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows[posicao].Selected = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow dr = dataGridView1.Rows[index];
            txtCodigo.Text = dr.Cells[0].Value.ToString();
            txtCep.Text = dr.Cells[1].Value.ToString();
        }

        private void limparCampos()
        {
            txtCodigo.Text = "";
            txtCep.Text = "";
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            limparCampos();
            ativarCampos();
            novo = true;

            AtivaBotoes();
        }

        private void AtivaBotoes()
        {
            btnNovo.Enabled = false;
            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
            btnEditar.Enabled = false;
            btnApagar.Enabled = false;
        }

        private void desativaCampos()
        {
            txtCep.Enabled = false;
        }
        private void ativarCampos()
        {
            txtCep.Enabled = true;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Cep cep = new Cep();
            cep.numerocep = txtCep.Text;

            C_Cep c_Cep = new C_Cep();
            if (novo == true)
            {
                c_Cep.Insere_Dados(cep);
            }
            else
            {
                cep.codcep = Int32.Parse(txtCodigo.Text);
                c_Cep.Atualiza_Dados(cep);
            }
            lista_cep = carregaListaCepFiltro();
            if (lista_cep.Count - 1 > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }

            CarregaTabela();

            desativaCampos();
            desativaBotoes();
        }
        private void desativaBotoes()
        {
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;
            btnEditar.Enabled = true;
            btnApagar.Enabled = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limparCampos();

            desativaBotoes();

            desativaCampos();
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            C_Cep cep = new C_Cep();
            if (txtCodigo.Text != "")
            {
                int valor = Int32.Parse(txtCodigo.Text);
                cep.Apaga_Dados(valor);

                CarregaTabela();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            ativarCampos();
            AtivaBotoes();
            novo = false;
        }

        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows[posicao].Selected = false;
            posicao = 0;
            atualizaCampos();
            dataGridView1.Rows[posicao].Selected = true;
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows[posicao].Selected = false;
            posicao = lista_cep.Count - 1;
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
            if (lista_cep.Count - 1 > posicao)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao++;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            C_Cep cr = new C_Cep();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Filtro(txtBuscar.Text.ToString() + "%");
            Tabela_ceps = dt;
            dataGridView1.DataSource = Tabela_ceps;

            dataGridView1.DataSource = Tabela_ceps;

            lista_cep = carregaListaCepFiltro();

            if (lista_cep.Count - 1 > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void txtCep_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
    
