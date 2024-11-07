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
using Veterinaria.view;

namespace Veterinaria
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           

        }





        private void ruaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRua frmrua = new FrmRua();
            frmrua.ShowDialog();
        }

        private void telefoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTelefone frmtelefone = new FrmTelefone();
            frmtelefone.ShowDialog();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tipoAnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTipoAnimal frmtipoanimal = new FrmTipoAnimal();
            frmtipoanimal.ShowDialog();
        }

        private void bairroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBairro frmbairro = new FrmBairro();
            frmbairro.ShowDialog();
        }

        private void cepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCep frmcep = new FrmCep();
            frmcep.ShowDialog();
        }

        private void marcaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMarca frmMarca = new FrmMarca();
            frmMarca.ShowDialog();
        }

        private void tipoProdutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTipoProduto frmTipoProduto = new FrmTipoProduto();
            frmTipoProduto.ShowDialog();
        }

        private void tipoServicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTipoServico frmtipoServico = new FrmTipoServico();
            frmtipoServico.ShowDialog();
        }

        private void cidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCidade frmCidade = new FrmCidade();
                frmCidade.ShowDialog();
        }

        private void tipoFuncionarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTipofuncionario frmTipoFipofuncioario = new FrmTipofuncionario();
              frmTipoFipofuncioario.ShowDialog();
        }

        private void cidAnimalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCidAnimal frmCidAnimal = new FrmCidAnimal();
            frmCidAnimal.ShowDialog();
        }

        private void paisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPais frmPais = new FrmPais();
            frmPais.ShowDialog();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void raçaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRaca frmRaca = new FrmRaca();
            frmRaca.ShowDialog();   
        }

        private void lojaToolStripMenuItem_Click(object sender, EventArgs e)
        {
          FrmLoja loja = new FrmLoja();
            loja.ShowDialog();  
        }

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCliente frmCliente = new FrmCliente();
            frmCliente.ShowDialog();
        }

        private void animalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAnimal frmAnimal = new FrmAnimal();
                frmAnimal.ShowDialog();
        }

        private void funcionarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmFuncionario frmFuncionario = new FrmFuncionario();
            frmFuncionario.ShowDialog();
        }

        private void produtoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProduto frmProduto = new FrmProduto();
            frmProduto.ShowDialog();
        }

        private void cadastroToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void imagensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmImagens frmImgs = new FrmImagens();
            frmImgs.ShowDialog();
        }
    }
}
