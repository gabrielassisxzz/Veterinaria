using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Veterinaria.control;
using Veterinaria.model;


namespace Veterinaria.view
{
    public partial class FrmPais : Form
    {
        DataTable Tabela_paiss;
        Boolean novo = true;
        int posicao;
        List<Pais> lista_pais = new List<Pais>();

        public FrmPais()
        {
            InitializeComponent();

            //Carregar o Datagrid de Raças.
            CarregaTabela();

            lista_pais = carregaListaPais();

            if (lista_pais.Count - 1 > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }


        }

        private void atualizaCampos()
        {
            txtCodigo.Text = lista_pais[posicao].codpais.ToString();
            txtPais.Text = lista_pais[posicao].nomepais.ToString();
            Pais paisSelecionado = lista_pais[posicao];
            byte[] imagemBytes = paisSelecionado.bandeira;

            try
            {
                using (MemoryStream ms = new MemoryStream(imagemBytes))
                {
                    Picture.Image = Image.FromStream(ms);
                }
            }
            catch (Exception ex)
            {
                // Tratar a exceção, por exemplo, exibindo uma mensagem de erro
                MessageBox.Show("Erro ao carregar a imagem: " + ex.Message);
            }
        }
    

        List<Pais> carregaListaPais()
        {
            List<Pais> lista = new List<Pais>();

            C_Pais cr = new C_Pais();
            lista = cr.DadosPais();

            return lista;
        }

        List<Pais> carregaListaPaisFiltro()
        {
            List<Pais> lista = new List<Pais>();

            C_Pais cr = new C_Pais();
            lista = cr.DadosPaisFiltro(txtBuscar.Text);

            return lista;
        }

        public void CarregaTabela()
        {
            C_Pais cr = new C_Pais();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Todos();
            Tabela_paiss = dt;
            dataGridView1.DataSource = Tabela_paiss;

        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int index = e.RowIndex;
            DataGridViewRow dr = dataGridView1.Rows[index];
            txtCodigo.Text = dr.Cells[0].Value.ToString();
            txtPais.Text = dr.Cells[1].Value.ToString();
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
            txtPais.Enabled = true;

        }

        private void limparCampos()
        {
            txtCodigo.Text = "";
            txtPais.Text = "";
        }

        private void desativaCampos()
        {
            txtPais.Enabled = false;
        }


        public byte[] ImageToByteArray(PictureBox picture)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Picture.Image.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }


        }
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Pais pais = new Pais();
            pais.nomepais = txtPais.Text;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Verifica se o arquivo selecionado é uma imagem válida
                    if (IsImageFile(openFileDialog1.FileName))
                    {
                        using (FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read))
                        {
                            // Carrega a imagem no PictureBox primeiro
                            Picture.Image = Image.FromStream(fs);

                            // Em seguida, converte para byte array
                            pais.bandeira = ImageToByteArray(Picture);
                        }
                    }
                    else
                    {
                        MessageBox.Show("O arquivo selecionado não é uma imagem válida.");
                        return; // Sai do método se não for uma imagem válida
                    }
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show("Erro ao carregar a imagem: " + ex.Message);
                    return; // Sai do método em caso de erro
                }

                C_Pais c_Pais = new C_Pais();

                if (novo == true)
                {
                    c_Pais.Insere_Dados(pais);
                }
                else
                {
                    pais.codpais = Int32.Parse(txtCodigo.Text);
                    c_Pais.Atualizar_Dados(pais);
                }

                lista_pais = carregaListaPaisFiltro();
                if (lista_pais.Count - 1 > 0)
                {
                    posicao = 0;
                    atualizaCampos();
                    dataGridView1.Rows[posicao].Selected = true;
                }

                CarregaTabela();
                desativaCampos();
                desativaBotoes();
            }
        }

        // Método auxiliar para verificar se um arquivo é uma imagem
        private bool IsImageFile(string fileName)
        {
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif" }; // Adicione outras extensões se necessário
            string extension = Path.GetExtension(fileName).ToLower();
            return allowedExtensions.Contains(extension);
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
            C_Pais pais = new C_Pais();


            if (txtCodigo.Text != "")
            {
                int valor = Int32.Parse(txtCodigo.Text);
                pais.Apaga_Dados(valor);
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
            int total = lista_pais.Count - 1;
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
            posicao = lista_pais.Count - 1;
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
            //Foi definido um atributo chamado cr do tipo C_Pais
            C_Pais cr = new C_Pais();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Filtro(txtBuscar.Text.ToString() + "%");
            Tabela_paiss = dt;

            //Adiciona os dados do DataTable para o DataGridView
            dataGridView1.DataSource = Tabela_paiss;

            //Carrega a Lista_pais com o valor da consulta com parâmetro
            lista_pais = carregaListaPaisFiltro();

            if (lista_pais.Count >= 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }

        }

        private void btnImagem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Picture.Image = Image.FromFile(openFileDialog1.FileName);

            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza que deseja sair?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)

            {
                this.Close();
            }
        }

        private void Picture_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}

