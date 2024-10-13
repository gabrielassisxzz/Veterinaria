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
    public partial class FrmClientetelefone : Form
    {
        DataTable Tabela_Clientetelefone;
        Boolean novo;
        int posicao; 
        List<Clientetelefone> Lista_Clientetelefone = new List<Clientetelefone> ();
        List<Telefone> Lista_Telefone = new List<Telefone> ();
        private int posicao_cliente;
        private int posicao_telefone;

        public FrmClientetelefone()
        {

        }

        private void atualizaCampos()
        {
            txtCodigo.Text = Lista_Clientetelefone[posicao].telefone.ToString();
            txtCodigo.Text = Lista_Clientetelefone[posicao].cliente.ToString();

        }

        List<Clientetelefone> carregaListaClientetelefone()
        {
            List<Clientetelefone> lista = new List<Clientetelefone>();

            C_Cliente c_Clientetelefone = new C_Cliente();
           

            return lista;
        }

        private void grbEndereco_Enter(object sender, EventArgs e)
        {

        }

        private void btnNovo_Click(object sender, EventArgs e)
        {

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void btnApagar_Click(object sender, EventArgs e)
        {

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

        }

        private void btnPrimeiro_Click(object sender, EventArgs e)
        {

        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {

        }

        private void btnProximo_Click(object sender, EventArgs e)
        {

        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

        }
    }
}
