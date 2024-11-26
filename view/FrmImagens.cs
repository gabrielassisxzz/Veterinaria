using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Veterinaria.control;
using Veterinaria.model;

namespace Veterinaria.view
{
    public partial class FrmImagens : Form
    {
        DataTable tabela_imagens;
        private List<Imagens> lista_imagens = new List<Imagens>();
        private List<Produto> lista_produto = new List<Produto>();
        private bool novo = true;
        int posicao;

        public FrmImagens()
        {
            InitializeComponent();
            CarregarComboProdutos();
            CarregaTabela();
        }

        
        private void CarregarComboProdutos()
        {
            C_Produto c_produto = new C_Produto();
            lista_produto = c_produto.DadosProduto(); 

        
            lista_produto.Insert(0, new Produto { codproduto = 0, nomeproduto ="" });

            cmbProduto.DataSource = lista_produto;
            cmbProduto.DisplayMember = "nomeproduto"; 
            cmbProduto.ValueMember = "codproduto";    

           
            cmbProduto.SelectedIndex = 0;
        }

        
        private void LimparCampos()
        {
            txtCodigo.Text = string.Empty;
            txtDescrição.Text = string.Empty;
            cmbProduto.SelectedIndex = -1;
            Picture.Image = null;
        }

       
        public byte[] ImageToByteArray(PictureBox picture)
        {
            if (picture.Image != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    picture.Image.Save(ms, ImageFormat.Png);
                    return ms.ToArray();
                }
            }
            return null;
        }


        private void CarregaTabela()
        {
            C_Imagens c_imagens = new C_Imagens();
            lista_imagens = c_imagens.DadosImagens(); 

            
            if (lista_imagens.Count > 0)
            {
                dataGridView1.DataSource = lista_imagens; 
            }
            else
            {
                MessageBox.Show("Nenhuma imagem encontrada.");
            }
        }

        public byte[] ConvertPictureBoxToByteArray(PictureBox picture)
        {
         
            if (picture.Image == null)
            {
                MessageBox.Show("Nenhuma imagem foi carregada no PictureBox.");
                return null;
            }

            using (MemoryStream ms = new MemoryStream())
            {
                picture.Image.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }


 
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Imagens imagem = new Imagens();
            imagem.descricao = txtDescrição.Text;

          
            byte[] foto = ConvertPictureBoxToByteArray(Picture);
            if (foto == null)
            {
                MessageBox.Show("Por favor, carregue uma imagem antes de salvar.");
                return;
            }
            imagem.foto = foto;

            if (cmbProduto.SelectedItem is Produto produtoSelecionado)
            {
                imagem.produto = produtoSelecionado;
            }
            else
            {
                MessageBox.Show("Selecione um produto válido.");
                return;
            }

            C_Imagens c_imagens = new C_Imagens();

            if (novo)
            {
                c_imagens.Insere_Dados(imagem);
            }
            else
            {
               
                imagem.codimagens = int.Parse(txtCodigo.Text);
                c_imagens.Atualiza_Dados(imagem);
            }

            LimparCampos();
            CarregaTabela();
            MessageBox.Show("Imagem salva com sucesso!");
        }



        
        private void btnCarregarImagem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Picture.Image = Image.FromFile(openFileDialog1.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar a imagem: " + ex.Message);
                }
            }
        }

        
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string termoBusca = txtBuscar.Text.Trim();
            C_Imagens c_imagens = new C_Imagens();

            lista_imagens = c_imagens.BuscarImagens(termoBusca);
            dataGridView1.DataSource = lista_imagens;

            if (lista_imagens.Count == 0)
            {
                posicao = 0;
                MessageBox.Show("Nenhum resultado encontrado.");
            }
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
            txtDescrição.Enabled = true;

        }

        private void limparCampos()
        {
            txtCodigo.Text = "";
            txtDescrição.Text = "";
        }

        private void desativaCampos()
        {
            txtDescrição.Enabled = false;
        }


      
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }


        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            if (lista_imagens.Count > 0)
            {
                posicao = 0;
                atualizaCampos();
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (posicao > 0)
            {
                posicao--;
                atualizaCampos();
            }
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (posicao < lista_imagens.Count - 1)
            {
                posicao++;
                atualizaCampos();
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            if (lista_imagens.Count > 0)
            {
                posicao = lista_imagens.Count - 1;
                atualizaCampos();
            }
        }

private void atualizaCampos()
{
    if (lista_imagens.Count > 0 && posicao >= 0 && posicao < lista_imagens.Count)
    {
        Imagens imagem = lista_imagens[posicao];
        txtCodigo.Text = imagem.codimagens.ToString();
        txtDescrição.Text = imagem.descricao;
        cmbProduto.SelectedValue = imagem.produto.codproduto;

        if (imagem.foto != null)
        {
            using (MemoryStream ms = new MemoryStream(imagem.foto))
            {
                Picture.Image = Image.FromStream(ms);
            }
        }
        else
        {
            Picture.Image = null;
        }

       
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

        private void btnApagar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) 
            {
                
                var linhaSelecionada = dataGridView1.SelectedRows[0];

                
                var item = linhaSelecionada.DataBoundItem;
                if (item != null)
                {
                    
                    Imagens imagemSelecionada = (Imagens)item;
                    MessageBox.Show($"Imagem selecionada: {imagemSelecionada.codimagens} - {imagemSelecionada.descricao}");

                    try
                    {
                        C_Imagens c_imagens = new C_Imagens();
                        c_imagens.Apaga_Dados(imagemSelecionada.codimagens); 

                        CarregaTabela(); 
                        MessageBox.Show("Imagem apagada com sucesso!");

                        LimparCampos(); 
                        desativaCampos(); 
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao apagar: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("A linha selecionada não contém um item válido.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma imagem para apagar.");
            }
        }


        private void btnEditar_Click(object sender, EventArgs e)
        {
            ativarCampos();
            
            novo = false;
        }
    }
}
