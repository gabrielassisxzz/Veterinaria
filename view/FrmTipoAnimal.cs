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
    public partial class FrmTipoAnimal : Form
    {
        DataTable Tabela_tipoanimals;
        Boolean novo = true;
        int posicao;
        List<Tipoanimal> lista_tipoanimal = new List<Tipoanimal>();
        public FrmTipoAnimal()
        {
            InitializeComponent();

            //Carregar o Datagrid de Raças.
            CarregaTabela();

            lista_tipoanimal = carregaListaTipoAnimal();

            if (lista_tipoanimal.Count - 1 > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }


        }

        private void atualizaCampos()
        {
            txtCodigo.Text = lista_tipoanimal[posicao].codtipoanimal.ToString();
            txtTipoAnimal.Text = lista_tipoanimal[posicao].nometipoanimal.ToString();
        }

        List<Tipoanimal> carregaListaTipoAnimal()
        {
            List<Tipoanimal> lista = new List<Tipoanimal>();

            C_Tipoanimal cr = new C_Tipoanimal();
            lista = cr.DadosTipoanimal();

            return lista;
        }

        List<Tipoanimal> carregaListaTipoAnimalFiltro()
        {
            List<Tipoanimal> lista = new List<Tipoanimal>();

            C_Tipoanimal cr = new C_Tipoanimal();
            lista = cr.DadosTipoanimalFiltro(txtBuscar.Text);

            return lista;
        }

        public void CarregaTabela()
        {
            C_Tipoanimal cr = new C_Tipoanimal();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Todos();
            Tabela_tipoanimals = dt;
            dataGridView1.DataSource = Tabela_tipoanimals;

        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int index = e.RowIndex;
            DataGridViewRow dr = dataGridView1.Rows[index];
            txtCodigo.Text = dr.Cells[0].Value.ToString();
            txtTipoAnimal.Text = dr.Cells[1].Value.ToString();
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
            txtTipoAnimal.Enabled = true;

        }

        private void limparCampos()
        {
            txtCodigo.Text = "";
            txtTipoAnimal.Text = "";
        }

        private void desativaCampos()
        {
            txtTipoAnimal.Enabled = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Tipoanimal tipoanimal = new Tipoanimal();

            tipoanimal.nometipoanimal = txtTipoAnimal.Text;

            C_Tipoanimal c_TipoAnimal = new C_Tipoanimal();

            if (novo == true)
            {
                c_TipoAnimal.Insere_Dados(tipoanimal);
            }
            else
            {
                tipoanimal.codtipoanimal = Int32.Parse(txtCodigo.Text);
                c_TipoAnimal.Atualizar_Dados(tipoanimal);
            }

            lista_tipoanimal = carregaListaTipoAnimalFiltro();
            if (lista_tipoanimal.Count - 1 > 0)
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
            C_Tipoanimal tipoanimal = new C_Tipoanimal();


            if (txtCodigo.Text != "")
            {
                int valor = Int32.Parse(txtCodigo.Text);
                tipoanimal.Apaga_Dados(valor);
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
            int total = lista_tipoanimal.Count - 1;
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
            posicao = lista_tipoanimal.Count - 1;
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
            //Foi definido um atributo chamado cr do tipo C_TipoAnimal
            C_Tipoanimal cr = new C_Tipoanimal();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Filtro(txtBuscar.Text.ToString() + "%");
            Tabela_tipoanimals = dt;

            //Adiciona os dados do DataTable para o DataGridView
            dataGridView1.DataSource = Tabela_tipoanimals;

            //Carrega a Lista_tipoanimal com o valor da consulta com parâmetro
            lista_tipoanimal = carregaListaTipoAnimalFiltro();

            if (lista_tipoanimal.Count >= 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void txtTipoAnimal_TextChanged(object sender, EventArgs e)
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