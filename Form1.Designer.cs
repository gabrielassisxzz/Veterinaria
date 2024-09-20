namespace Veterinaria
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ruaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.telefoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cidadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tipoAnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bairroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.marcaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tipoProdutoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tipoServicoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tipoFuncionarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cidAnimalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.raçaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(236, 92);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(279, 22);
            this.textBox1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(588, 87);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 1;
            this.button1.Text = "Inserir";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(588, 138);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 28);
            this.button2.TabIndex = 2;
            this.button2.Text = "Apagar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(236, 154);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(279, 22);
            this.textBox2.TabIndex = 3;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(588, 190);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 28);
            this.button3.TabIndex = 4;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(588, 237);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(100, 28);
            this.button4.TabIndex = 5;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.raçaToolStripMenuItem,
            this.ruaToolStripMenuItem,
            this.telefoneToolStripMenuItem,
            this.cidadeToolStripMenuItem,
            this.paisToolStripMenuItem,
            this.tipoAnToolStripMenuItem,
            this.bairroToolStripMenuItem,
            this.cepToolStripMenuItem,
            this.marcaToolStripMenuItem,
            this.tipoProdutoToolStripMenuItem,
            this.tipoServicoToolStripMenuItem,
            this.tipoFuncionarioToolStripMenuItem,
            this.cidAnimalToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(156, 554);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // ruaToolStripMenuItem
            // 
            this.ruaToolStripMenuItem.Name = "ruaToolStripMenuItem";
            this.ruaToolStripMenuItem.Size = new System.Drawing.Size(123, 24);
            this.ruaToolStripMenuItem.Text = "Rua";
            this.ruaToolStripMenuItem.Click += new System.EventHandler(this.ruaToolStripMenuItem_Click);
            // 
            // telefoneToolStripMenuItem
            // 
            this.telefoneToolStripMenuItem.Name = "telefoneToolStripMenuItem";
            this.telefoneToolStripMenuItem.Size = new System.Drawing.Size(123, 24);
            this.telefoneToolStripMenuItem.Text = "Telefone";
            this.telefoneToolStripMenuItem.Click += new System.EventHandler(this.telefoneToolStripMenuItem_Click);
            // 
            // cidadeToolStripMenuItem
            // 
            this.cidadeToolStripMenuItem.Name = "cidadeToolStripMenuItem";
            this.cidadeToolStripMenuItem.Size = new System.Drawing.Size(123, 24);
            this.cidadeToolStripMenuItem.Text = "Cidade";
            this.cidadeToolStripMenuItem.Click += new System.EventHandler(this.cidadeToolStripMenuItem_Click);
            // 
            // paisToolStripMenuItem
            // 
            this.paisToolStripMenuItem.Name = "paisToolStripMenuItem";
            this.paisToolStripMenuItem.Size = new System.Drawing.Size(123, 24);
            this.paisToolStripMenuItem.Text = "Pais";
            this.paisToolStripMenuItem.Click += new System.EventHandler(this.paisToolStripMenuItem_Click);
            // 
            // tipoAnToolStripMenuItem
            // 
            this.tipoAnToolStripMenuItem.Name = "tipoAnToolStripMenuItem";
            this.tipoAnToolStripMenuItem.Size = new System.Drawing.Size(123, 24);
            this.tipoAnToolStripMenuItem.Text = "TipoAnimal";
            this.tipoAnToolStripMenuItem.Click += new System.EventHandler(this.tipoAnToolStripMenuItem_Click);
            // 
            // bairroToolStripMenuItem
            // 
            this.bairroToolStripMenuItem.Name = "bairroToolStripMenuItem";
            this.bairroToolStripMenuItem.Size = new System.Drawing.Size(123, 24);
            this.bairroToolStripMenuItem.Text = "Bairro";
            this.bairroToolStripMenuItem.Click += new System.EventHandler(this.bairroToolStripMenuItem_Click);
            // 
            // cepToolStripMenuItem
            // 
            this.cepToolStripMenuItem.Name = "cepToolStripMenuItem";
            this.cepToolStripMenuItem.Size = new System.Drawing.Size(123, 24);
            this.cepToolStripMenuItem.Text = "Cep";
            this.cepToolStripMenuItem.Click += new System.EventHandler(this.cepToolStripMenuItem_Click);
            // 
            // marcaToolStripMenuItem
            // 
            this.marcaToolStripMenuItem.Name = "marcaToolStripMenuItem";
            this.marcaToolStripMenuItem.Size = new System.Drawing.Size(123, 24);
            this.marcaToolStripMenuItem.Text = "Marca";
            this.marcaToolStripMenuItem.Click += new System.EventHandler(this.marcaToolStripMenuItem_Click);
            // 
            // tipoProdutoToolStripMenuItem
            // 
            this.tipoProdutoToolStripMenuItem.Name = "tipoProdutoToolStripMenuItem";
            this.tipoProdutoToolStripMenuItem.Size = new System.Drawing.Size(123, 24);
            this.tipoProdutoToolStripMenuItem.Text = "TipoProduto";
            this.tipoProdutoToolStripMenuItem.Click += new System.EventHandler(this.tipoProdutoToolStripMenuItem_Click);
            // 
            // tipoServicoToolStripMenuItem
            // 
            this.tipoServicoToolStripMenuItem.Name = "tipoServicoToolStripMenuItem";
            this.tipoServicoToolStripMenuItem.Size = new System.Drawing.Size(123, 24);
            this.tipoServicoToolStripMenuItem.Text = "TipoServico";
            this.tipoServicoToolStripMenuItem.Click += new System.EventHandler(this.tipoServicoToolStripMenuItem_Click);
            // 
            // tipoFuncionarioToolStripMenuItem
            // 
            this.tipoFuncionarioToolStripMenuItem.Name = "tipoFuncionarioToolStripMenuItem";
            this.tipoFuncionarioToolStripMenuItem.Size = new System.Drawing.Size(123, 24);
            this.tipoFuncionarioToolStripMenuItem.Text = "TipoFuncionario";
            this.tipoFuncionarioToolStripMenuItem.Click += new System.EventHandler(this.tipoFuncionarioToolStripMenuItem_Click);
            // 
            // cidAnimalToolStripMenuItem
            // 
            this.cidAnimalToolStripMenuItem.Name = "cidAnimalToolStripMenuItem";
            this.cidAnimalToolStripMenuItem.Size = new System.Drawing.Size(123, 24);
            this.cidAnimalToolStripMenuItem.Text = "CidAnimal";
            this.cidAnimalToolStripMenuItem.Click += new System.EventHandler(this.cidAnimalToolStripMenuItem_Click);
            // 
            // raçaToolStripMenuItem
            // 
            this.raçaToolStripMenuItem.Name = "raçaToolStripMenuItem";
            this.raçaToolStripMenuItem.Size = new System.Drawing.Size(143, 24);
            this.raçaToolStripMenuItem.Text = "Raça";
            this.raçaToolStripMenuItem.Click += new System.EventHandler(this.raçaToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ruaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem telefoneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tipoAnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bairroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cepToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem marcaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tipoProdutoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tipoServicoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cidadeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tipoFuncionarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cidAnimalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem raçaToolStripMenuItem;
    }
}

