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
    public partial class FrmCidAnimal : Form // Adaptado para FrmCidAnimal
    {
        DataTable Tabela_cidanimais; // Adaptado para Cidanimal
        Boolean novo = true;
        int posicao;
        List<CidAnimal> lista_cidanimal = new List<CidAnimal>(); // Adaptado para Cidanimal

        public FrmCidAnimal() // Adaptado para FrmCidAnimal
        {
            InitializeComponent();

            // Carregar o Datagrid de Cidanimais
            CarregaTabela();

            lista_cidanimal = carregaListaCidanimal(); // Adaptado para Cidanimal

            if (lista_cidanimal.Count - 1 > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limparCampos();
            desativaBotoes();
            desativaCampos();
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
            int total = lista_cidanimal.Count - 1;
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
            posicao = lista_cidanimal.Count - 1;
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

        private void atualizaCampos()
        {
            txtCodigo.Text = lista_cidanimal[posicao].Codcidanimal.ToString(); // Adaptado para Cidanimal
            txtCidAnimal.Text = lista_cidanimal[posicao].Nomecidanimal.ToString(); // Adaptado para Cidanimal
            txtDescricao.Text = lista_cidanimal[posicao].Descricao.ToString(); // Adicionado para descricao
        }

        List<CidAnimal> carregaListaCidanimal() // Adaptado para Cidanimal
        {
            List<CidAnimal> lista = new List<CidAnimal>();

            C_CidAnimal cr = new C_CidAnimal(); // Adaptado para C_Cidanimal
            lista = cr.DadosCidAnimal(); // Adaptado para Cidanimal

            return lista;
        }

        List<CidAnimal> carregaListaCidanimalFiltro() // Adaptado para Cidanimal
        {
            List<CidAnimal> lista = new List<CidAnimal>();

            C_CidAnimal cr = new C_CidAnimal(); // Adaptado para C_Cidanimal
            lista = cr.DadosCidAnimalFiltro(txtBuscar.Text); // Adaptado para Cidanimal

            return lista;
        }

        public void CarregaTabela()
        {
            C_CidAnimal cr = new C_CidAnimal(); // Adaptado para C_Cidanimal
            DataTable dt = new DataTable();
            dt = cr.Buscar_Todos(); // Adaptado para C_Cidanimal
            Tabela_cidanimais = dt; // Adaptado para Cidanimal
            dataGridView1.DataSource = Tabela_cidanimais; // Adaptado para Cidanimal
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow dr = dataGridView1.Rows[index];
            txtCodigo.Text = dr.Cells[0].Value.ToString();
            txtCidAnimal.Text = dr.Cells[1].Value.ToString(); // Adaptado para Cidanimal
            txtDescricao.Text = dr.Cells[2].Value.ToString(); // Adicionado para descricao
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

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            CidAnimal cidanimal = new CidAnimal(); // Adaptado para Cidanimal

            cidanimal.Nomecidanimal = txtCidAnimal.Text; // Adaptado para Cidanimal
            cidanimal.Descricao = txtDescricao.Text; // Adicionado para descricao

            C_CidAnimal cc = new C_CidAnimal(); // Adaptado para C_Cidanimal

            if (novo == true)
            {
                cc.Insere_Dados(cidanimal); // Adaptado para C_Cidanimal
            }
            else
            {
                cidanimal.Codcidanimal = Int32.Parse(txtCodigo.Text);
                cc.Atualizar_Dados(cidanimal); // Adaptado para C_Cidanimal
            }

            lista_cidanimal = carregaListaCidanimalFiltro(); // Adaptado para Cidanimal
            if (lista_cidanimal.Count - 1 > 0)
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

        private void btnApagar_Click(object sender, EventArgs e)
        {
            C_CidAnimal cc = new C_CidAnimal(); // Adaptado para C_Cidanimal

            if (txtCodigo.Text != "")
            {
                int valor = Int32.Parse(txtCodigo.Text);
                cc.Apaga_Dados(valor); // Adaptado para C_Cidanimal
                CarregaTabela();
            }
        }

        

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            C_CidAnimal cc = new C_CidAnimal(); // Adaptado para C_Cidanimal
            DataTable dt = new DataTable();
            dt = cc.Buscar_Filtro(txtBuscar.Text.ToString() + "%"); // Adaptado para C_Cidanimal
            Tabela_cidanimais = dt; // Adaptado para Cidanimal

            dataGridView1.DataSource = Tabela_cidanimais; // Adaptado para Cidanimal

            lista_cidanimal = carregaListaCidanimalFiltro(); // Adaptado para Cidanimal

            if (lista_cidanimal.Count >= 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        // ... (demais métodos)

        private void limparCampos()
        {
            txtCodigo.Text = "";
            txtCidAnimal.Text = ""; // Adaptado para Cidanimal
            txtDescricao.Text = ""; // Adicionado para descricao
        }

        private void ativarCampos()
        {
            txtCidAnimal.Enabled = true; // Adaptado para Cidanimal
            txtDescricao.Enabled = true; // Adicionado para descricao
        }

        private void desativaCampos()
        {
            txtCidAnimal.Enabled = false; // Adaptado para Cidanimal
            txtDescricao.Enabled = false; // Adicionado para descricao
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