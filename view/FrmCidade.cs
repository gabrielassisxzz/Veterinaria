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

    public partial class FrmCidade : Form
    {
        DataTable Tabela_cidades;
        Boolean novo = true;
        int posicao;
        List<Cidade> lista_cidade = new List<Cidade>();
        public FrmCidade()
        {
            InitializeComponent();

            //Carregar o Datagrid de Raças.
            CarregaTabela();

            lista_cidade = carregaListaCidade();

            if (lista_cidade.Count - 1 > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }


        }

        private void atualizaCampos()
        {
            txtCodigo.Text = lista_cidade[posicao].codcidade.ToString();
            txtCidade.Text = lista_cidade[posicao].nomecidade.ToString();
        }

        List<Cidade> carregaListaCidade()
        {
            List<Cidade> lista = new List<Cidade>();

            C_Cidade cr = new C_Cidade();
            lista = cr.DadosCidade();

            return lista;
        }

        List<Cidade> carregaListaCidadeFiltro()
        {
            List<Cidade> lista = new List<Cidade>();

            C_Cidade cr = new C_Cidade();
            lista = cr.DadosCidadeFiltro(txtBuscar.Text);

            return lista;
        }

        public void CarregaTabela()
        {
            C_Cidade cr = new C_Cidade();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Todos();
            Tabela_cidades = dt;
            dataGridView1.DataSource = Tabela_cidades;

        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int index = e.RowIndex;
            DataGridViewRow dr = dataGridView1.Rows[index];
            txtCodigo.Text = dr.Cells[0].Value.ToString();
            txtCidade.Text = dr.Cells[1].Value.ToString();
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
            txtCidade.Enabled = true;

        }

        private void limparCampos()
        {
            txtCodigo.Text = "";
            txtCidade.Text = "";
        }

        private void desativaCampos()
        {
            txtCidade.Enabled = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Cidade cidade = new Cidade();

            cidade.nomecidade = txtCidade.Text;

            C_Cidade c_Cidade = new C_Cidade();

            if (novo == true)
            {
                c_Cidade.Insere_Dados(cidade);
            }
            else
            {
                cidade.codcidade = Int32.Parse(txtCodigo.Text);
                c_Cidade.Atualizar_Dados(cidade);
            }

            lista_cidade = carregaListaCidadeFiltro();
            if (lista_cidade.Count - 1 > 0)
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
            C_Cidade cidade = new C_Cidade();


            if (txtCodigo.Text != "")
            {
                int valor = Int32.Parse(txtCodigo.Text);
                cidade.Apaga_Dados(valor);
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
            int total = lista_cidade.Count - 1;
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
            posicao = lista_cidade.Count - 1;
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
            //Foi definido um atributo chamado cr do tipo C_Cidade
            C_Cidade cr = new C_Cidade();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Filtro(txtBuscar.Text.ToString() + "%");
            Tabela_cidades = dt;

            //Adiciona os dados do DataTable para o DataGridView
            dataGridView1.DataSource = Tabela_cidades;

            //Carrega a Lista_cidade com o valor da consulta com parâmetro
            lista_cidade = carregaListaCidadeFiltro();

            if (lista_cidade.Count >= 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
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
