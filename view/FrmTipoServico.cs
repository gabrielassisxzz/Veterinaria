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
    public partial class FrmTipoServico : Form
    {
        DataTable Tabela_tiposervicos;
        Boolean novo = true;
        int posicao=0;
        List<Tiposervico> lista_tiposervico = new List<Tiposervico>();
        public FrmTipoServico()
        {
            InitializeComponent();

            //Carregar o Datagrid de Raças.
            CarregaTabela();

            lista_tiposervico = carregaListaTipoServico();

            if (lista_tiposervico.Count - 1 > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }


        }

        private void atualizaCampos()
        {

            txtCodigo.Text = lista_tiposervico[posicao].codtiposervico.ToString();
            txtTipoServico.Text = lista_tiposervico[posicao].nometiposervico.ToString();
            txtValorTipoServico.Text = lista_tiposervico[posicao].valortiposervico.ToString();

        }

        List<Tiposervico> carregaListaTipoServico()
        {
            List<Tiposervico> lista = new List<Tiposervico>();

            C_TipoServico cr = new C_TipoServico();
            lista = cr.DadosTipoServico();

            return lista;
        }

        List<Tiposervico> carregaListaTipoServicoFiltro()
        {
            List<Tiposervico> lista = new List<Tiposervico>();

            C_TipoServico cr = new C_TipoServico();
            lista = cr.DadosTipoServicoFiltro(txtBuscar.Text);

            return lista;
        }

        public void CarregaTabela()
        {
            C_TipoServico cr = new C_TipoServico();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Todos();
            Tabela_tiposervicos = dt;
            dataGridView1.DataSource = Tabela_tiposervicos;

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
            txtTipoServico.Enabled = true;
            txtValorTipoServico.Enabled=true;


        }

        private void limparCampos()
        {
            txtCodigo.Text = "";
            txtTipoServico.Text = "";
        }

        private void desativaCampos()
        {
            txtTipoServico.Enabled = false;
            txtValorTipoServico.Enabled=false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Tiposervico tiposervico = new Tiposervico();

            tiposervico.nometiposervico = txtTipoServico.Text;

            tiposervico.valortiposervico = Double.Parse(txtValorTipoServico.Text);

            C_TipoServico c_TipoServico = new C_TipoServico();

            if (novo == true)
            {
                c_TipoServico.Insere_Dados(tiposervico);
            }
            else
            {
                tiposervico.codtiposervico = Int32.Parse(txtCodigo.Text);
                c_TipoServico.Atualizar_Dados(tiposervico);
            }

            lista_tiposervico = carregaListaTipoServicoFiltro();
            if (lista_tiposervico.Count - 1 > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
            lista_tiposervico = carregaListaTipoServico();

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
            C_TipoServico tiposervico = new C_TipoServico();


            if (txtCodigo.Text != "")
            {
                int valor = Int32.Parse(txtCodigo.Text);
                tiposervico.Apaga_Dados(valor);
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
            int total = lista_tiposervico.Count - 1;
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
            posicao = lista_tiposervico.Count - 1;
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
            //Foi definido um atributo chamado cr do tipo C_TipoServico
            C_TipoServico cr = new C_TipoServico();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Filtro(txtBuscar.Text.ToString() + "%");
            Tabela_tiposervicos = dt;

            //Adiciona os dados do DataTable para o DataGridView
            dataGridView1.DataSource = Tabela_tiposervicos;

            //Carrega a Lista_tiposervico com o valor da consulta com parâmetro
            lista_tiposervico = carregaListaTipoServicoFiltro();

            if (lista_tiposervico.Count >= 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void txtTipoServico_TextChanged(object sender, EventArgs e)
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

        private void txtTipoServico_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow dr = dataGridView1.Rows[index];
            txtCodigo.Text = dr.Cells[0].Value.ToString();
            txtTipoServico.Text = dr.Cells[1].Value.ToString();
            //txtValorTipoServico.Text = dr.Cells[3].Value.ToString();
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