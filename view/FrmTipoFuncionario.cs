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
    public partial class FrmTipofuncionario : Form
    {
        DataTable Tabela_tipofuncionarios;
        Boolean novo = true;
        int posicao;
        List<Tipofuncionario> lista_tipofuncionario = new List<Tipofuncionario>();
        public FrmTipofuncionario()
        {
            InitializeComponent();

            //Carregar o Datagrid de Raças.
            CarregaTabela();

            lista_tipofuncionario = carregaListaTipofuncionario();

            if (lista_tipofuncionario.Count - 1 > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }


        }

        private void atualizaCampos()
        {
            txtCodigo.Text = lista_tipofuncionario[posicao].codtipofuncionario.ToString();
            txtTipoFuncionario.Text = lista_tipofuncionario[posicao].nometipofuncionario.ToString();
        }

        List<Tipofuncionario> carregaListaTipofuncionario()
        {
            List<Tipofuncionario> lista = new List<Tipofuncionario>();

            C_Tipofuncionario cr = new C_Tipofuncionario();
            lista = cr.DadosTipofuncionario();

            return lista;
        }

        List<Tipofuncionario> carregaListaTipofuncionarioFiltro()
        {
            List<Tipofuncionario> lista = new List<Tipofuncionario>();

            C_Tipofuncionario cr = new C_Tipofuncionario();
            lista = cr.DadosTipofuncionarioFiltro(txtBuscar.Text);

            return lista;
        }

        public void CarregaTabela()
        {
            C_Tipofuncionario cr = new C_Tipofuncionario();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Todos();
            Tabela_tipofuncionarios = dt;
            dataGridView1.DataSource = Tabela_tipofuncionarios;

        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int index = e.RowIndex;
            DataGridViewRow dr = dataGridView1.Rows[index];
            txtCodigo.Text = dr.Cells[0].Value.ToString();
            txtTipoFuncionario.Text = dr.Cells[1].Value.ToString();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            limparCampos();

            ativarCampos();

            AtivaBotoes();

            novo = true;
        }

        private void AtivaBotoes()
        {
            btnNovo.Enabled = false;
            btnApagar.Enabled = false;
            btnEditar.Enabled = false;

            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void ativarCampos()
        {
            txtTipoFuncionario.Enabled = true;

        }

        private void limparCampos()
        {
            txtCodigo.Text = "";
            txtTipoFuncionario.Text = "";
        }

        private void desativaCampos()
        {
            txtTipoFuncionario.Enabled = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Tipofuncionario tipofuncionario = new Tipofuncionario();

            tipofuncionario.nometipofuncionario = txtTipoFuncionario.Text;

            C_Tipofuncionario c_Tipofuncionario = new C_Tipofuncionario();

            if (novo == true)
            {
                c_Tipofuncionario.Insere_Dados(tipofuncionario);
            }
            else
            {
                tipofuncionario.codtipofuncionario = Int32.Parse(txtCodigo.Text);
                c_Tipofuncionario.Atualizar_Dados(tipofuncionario);
            }

            lista_tipofuncionario = carregaListaTipofuncionarioFiltro();
            if (lista_tipofuncionario.Count - 1 > 0)
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
            C_Tipofuncionario tipofuncionario = new C_Tipofuncionario();


            if (txtCodigo.Text != "")
            {
                int valor = Int32.Parse(txtCodigo.Text);
                tipofuncionario.Apaga_Dados(valor);
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

        private void btnProximo_Click(object sender, EventArgs e)
        {
            int total = lista_tipofuncionario.Count - 1;
            if (total > posicao)
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
            posicao = lista_tipofuncionario.Count - 1;
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //Foi definido um atributo chamado cr do tipo C_Tipofuncionario
            C_Tipofuncionario cr = new C_Tipofuncionario();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Filtro(txtBuscar.Text.ToString() + "%");
            Tabela_tipofuncionarios = dt;

            //Adiciona os dados do DataTable para o DataGridView
            dataGridView1.DataSource = Tabela_tipofuncionarios;

            //Carrega a Lista_tipofuncionario com o valor da consulta com parâmetro
            lista_tipofuncionario = carregaListaTipofuncionarioFiltro();

            if (lista_tipofuncionario.Count >= 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void txtTipofuncionario_TextChanged(object sender, EventArgs e)
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
