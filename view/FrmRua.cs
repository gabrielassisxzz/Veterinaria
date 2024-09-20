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
    public partial class FrmRua : Form
    {
        DataTable Tabela_ruas;
        Boolean novo = true;
        int posicao;
        List<Rua> lista_rua = new List<Rua>();
        public FrmRua()
        {
            InitializeComponent();

            //Carregar o Datagrid de Raças.
            CarregaTabela();

            lista_rua = carregaListaRua();

            if (lista_rua.Count - 1 > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }


        }

        private void atualizaCampos()
        {
            txtCodigo.Text = lista_rua[posicao].codrua.ToString();
            txtRua.Text = lista_rua[posicao].nomerua.ToString();
        }

        List<Rua> carregaListaRua()
        {
            List<Rua> lista = new List<Rua>();

            C_Rua cr = new C_Rua();
            lista = cr.DadosRua();

            return lista;
        }

        List<Rua> carregaListaRuaFiltro()
        {
            List<Rua> lista = new List<Rua>();

            C_Rua cr = new C_Rua();
            lista = cr.DadosRuaFiltro(txtBuscar.Text);

            return lista;
        }

        public void CarregaTabela()
        {
            C_Rua cr = new C_Rua();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Todos();
            Tabela_ruas = dt;
            dataGridView1.DataSource = Tabela_ruas;

        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int index = e.RowIndex;
            DataGridViewRow dr = dataGridView1.Rows[index];
            txtCodigo.Text = dr.Cells[0].Value.ToString();
            txtRua.Text = dr.Cells[1].Value.ToString();
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
            txtRua.Enabled = true;

        }

        private void limparCampos()
        {
            txtCodigo.Text = "";
            txtRua.Text = "";
        }

        private void desativaCampos()
        {
            txtRua.Enabled = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Rua rua = new Rua();

            rua.nomerua = txtRua.Text;

            C_Rua c_Rua = new C_Rua();

            if (novo == true)
            {
                c_Rua.Insere_Dados(rua);
            }
            else
            {
                rua.codrua = Int32.Parse(txtCodigo.Text);
                c_Rua.Atualizar_Dados(rua);
            }

            lista_rua = carregaListaRuaFiltro();
            if (lista_rua.Count - 1 > 0)
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
            C_Rua rua = new C_Rua();


            if (txtCodigo.Text != "")
            {
                int valor = Int32.Parse(txtCodigo.Text);
                rua.Apaga_Dados(valor);
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
            int total = lista_rua.Count - 1;
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
            posicao = lista_rua.Count - 1;
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
            //Foi definido um atributo chamado cr do tipo C_Rua
            C_Rua cr = new C_Rua();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Filtro(txtBuscar.Text.ToString() + "%");
            Tabela_ruas = dt;

            //Adiciona os dados do DataTable para o DataGridView
            dataGridView1.DataSource = Tabela_ruas;

            //Carrega a Lista_rua com o valor da consulta com parâmetro
            lista_rua = carregaListaRuaFiltro();

            if (lista_rua.Count >= 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void txtRua_TextChanged(object sender, EventArgs e)
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

        private void toolStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void txtRua_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void txtCodigo_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
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
