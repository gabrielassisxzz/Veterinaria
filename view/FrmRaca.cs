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
    public partial class FrmRaca : Form
    {
        DataTable Tabela_racas;
        Boolean novo = true;
        int posicao;
        List<Raca> lista_raca = new List<Raca>();
        public FrmRaca()
        {
            InitializeComponent();

            //Carregar o Datagrid de Raças.
            CarregaTabela();

            lista_raca = carregaListaRaca();

            if (lista_raca.Count - 1 > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }

            
        }

        private void atualizaCampos()
        {
            txtCodigo.Text = lista_raca[posicao].codraca.ToString();
            txtRaca.Text = lista_raca[posicao].nomeraca.ToString();
        }

        List<Raca> carregaListaRaca()
        {
            List<Raca> lista = new List<Raca>();

            C_Raca cr = new C_Raca();
            lista = cr.DadosRaca();

            return lista;
        }

        List<Raca> carregaListaRacaFiltro()
        {
            List<Raca> lista = new List<Raca>();

            C_Raca cr = new C_Raca();
            lista = cr.DadosRacaFiltro(txtBuscar.Text);

            return lista;
        }

        public void CarregaTabela()
        {
            C_Raca cr = new C_Raca();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Todos();
            Tabela_racas = dt;
            dataGridView1.DataSource = Tabela_racas;

        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int index = e.RowIndex;
            DataGridViewRow dr = dataGridView1.Rows[index];
            txtCodigo.Text = dr.Cells[0].Value.ToString();
            txtRaca.Text = dr.Cells[1].Value.ToString();
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
            txtRaca.Enabled = true;

        }

        private void limparCampos()
        {
            txtCodigo.Text = "";
            txtRaca.Text = "";
        }

        private void desativaCampos()
        {
            txtRaca.Enabled = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Raca raca = new Raca();
            
            raca.nomeraca = txtRaca.Text;  

            C_Raca c_Raca = new C_Raca();
            
            if(novo == true)
            {
                c_Raca.Insere_Dados(raca);
            }
            else
            {
                raca.codraca = Int32.Parse(txtCodigo.Text);
                c_Raca.Atualizar_Dados(raca);
            }

            lista_raca = carregaListaRacaFiltro();
            if (lista_raca.Count - 1 > 0)
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
            btnNovo.Enabled=true;
            btnApagar.Enabled = true;
            btnEditar.Enabled = true;

            btnSalvar.Enabled=false;
            btnCancelar.Enabled=false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limparCampos();
            desativaBotoes();
            desativaCampos();
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            C_Raca raca = new C_Raca();
            

            if(txtCodigo.Text != "")
            {
                int valor = Int32.Parse(txtCodigo.Text);
                raca.Apaga_Dados(valor);
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
            int total = lista_raca.Count - 1;
            if(total > posicao)
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
            posicao = lista_raca.Count - 1;
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
            //Foi definido um atributo chamado cr do tipo C_Raca
            C_Raca cr = new C_Raca();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Filtro(txtBuscar.Text.ToString()+"%");
            Tabela_racas = dt;

            //Adiciona os dados do DataTable para o DataGridView
            dataGridView1.DataSource = Tabela_racas;

            //Carrega a Lista_raca com o valor da consulta com parâmetro
            lista_raca = carregaListaRacaFiltro();

            if (lista_raca.Count  >= 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void txtRaca_TextChanged(object sender, EventArgs e)
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
