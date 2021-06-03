namespace EnveloperUI
{
    partial class ResponsavelForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listaResponsaveis = new System.Windows.Forms.ListBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.registrarResponsavel = new System.Windows.Forms.Button();
            this.limparListaResponsavel = new System.Windows.Forms.Button();
            this.comboUsuario = new System.Windows.Forms.ComboBox();
            this.usuarioSenha = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.adicionarResponsavel = new System.Windows.Forms.Button();
            this.excluirResponsavel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listaResponsaveis
            // 
            this.listaResponsaveis.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.listaResponsaveis.FormattingEnabled = true;
            this.listaResponsaveis.ItemHeight = 17;
            this.listaResponsaveis.Location = new System.Drawing.Point(29, 306);
            this.listaResponsaveis.Name = "listaResponsaveis";
            this.listaResponsaveis.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listaResponsaveis.Size = new System.Drawing.Size(389, 89);
            this.listaResponsaveis.Sorted = true;
            this.listaResponsaveis.TabIndex = 40;
            this.listaResponsaveis.DoubleClick += new System.EventHandler(this.listaResponsaveis_DoubleClick);
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(25, 74);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(389, 52);
            this.label14.TabIndex = 39;
            this.label14.Text = "Selecione os operadores responsáveis pelas operações deste caixa.";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(23, 24);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(273, 32);
            this.label15.TabIndex = 38;
            this.label15.Text = "Responsáveis pelo Caixa";
            // 
            // registrarResponsavel
            // 
            this.registrarResponsavel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.registrarResponsavel.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.registrarResponsavel.Location = new System.Drawing.Point(29, 410);
            this.registrarResponsavel.Name = "registrarResponsavel";
            this.registrarResponsavel.Size = new System.Drawing.Size(125, 28);
            this.registrarResponsavel.TabIndex = 47;
            this.registrarResponsavel.Text = "Registrar Lista";
            this.registrarResponsavel.UseVisualStyleBackColor = true;
            this.registrarResponsavel.Click += new System.EventHandler(this.registrarResponsavel_Click);
            // 
            // limparListaResponsavel
            // 
            this.limparListaResponsavel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.limparListaResponsavel.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.limparListaResponsavel.Location = new System.Drawing.Point(289, 410);
            this.limparListaResponsavel.Name = "limparListaResponsavel";
            this.limparListaResponsavel.Size = new System.Drawing.Size(125, 28);
            this.limparListaResponsavel.TabIndex = 48;
            this.limparListaResponsavel.Text = "Limpar Lista";
            this.limparListaResponsavel.UseVisualStyleBackColor = true;
            this.limparListaResponsavel.Click += new System.EventHandler(this.limparSelecao_Click);
            // 
            // comboUsuario
            // 
            this.comboUsuario.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboUsuario.FormattingEnabled = true;
            this.comboUsuario.Location = new System.Drawing.Point(29, 129);
            this.comboUsuario.Name = "comboUsuario";
            this.comboUsuario.Size = new System.Drawing.Size(277, 25);
            this.comboUsuario.TabIndex = 49;
            this.comboUsuario.SelectionChangeCommitted += new System.EventHandler(this.comboUsuario_SelectionChangeCommitted);
            this.comboUsuario.Enter += new System.EventHandler(this.comboUsuario_Enter);
            this.comboUsuario.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboUsuario_KeyDown);
            // 
            // usuarioSenha
            // 
            this.usuarioSenha.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.usuarioSenha.Location = new System.Drawing.Point(29, 223);
            this.usuarioSenha.Name = "usuarioSenha";
            this.usuarioSenha.Size = new System.Drawing.Size(131, 25);
            this.usuarioSenha.TabIndex = 50;
            this.usuarioSenha.Text = "Senha";
            this.usuarioSenha.Enter += new System.EventHandler(this.usuarioSenha_Enter);
            this.usuarioSenha.KeyDown += new System.Windows.Forms.KeyEventHandler(this.usuarioSenha_KeyDown);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 168);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(380, 52);
            this.label1.TabIndex = 52;
            this.label1.Text = "Informe a senha do operador selecionado para adicionar a lista de responsáveis.";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(25, 271);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(349, 25);
            this.label2.TabIndex = 53;
            this.label2.Text = "Responsáveis pelas operações do caixa.";
            // 
            // adicionarResponsavel
            // 
            this.adicionarResponsavel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.adicionarResponsavel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.adicionarResponsavel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.adicionarResponsavel.Location = new System.Drawing.Point(166, 223);
            this.adicionarResponsavel.Name = "adicionarResponsavel";
            this.adicionarResponsavel.Size = new System.Drawing.Size(30, 25);
            this.adicionarResponsavel.TabIndex = 54;
            this.adicionarResponsavel.Text = "+";
            this.adicionarResponsavel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.adicionarResponsavel.UseVisualStyleBackColor = false;
            this.adicionarResponsavel.Click += new System.EventHandler(this.adicionarResponsavel_Click);
            // 
            // excluirResponsavel
            // 
            this.excluirResponsavel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.excluirResponsavel.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.excluirResponsavel.Location = new System.Drawing.Point(159, 410);
            this.excluirResponsavel.Name = "excluirResponsavel";
            this.excluirResponsavel.Size = new System.Drawing.Size(125, 28);
            this.excluirResponsavel.TabIndex = 55;
            this.excluirResponsavel.Text = "Excluir Responsavel";
            this.excluirResponsavel.UseVisualStyleBackColor = true;
            this.excluirResponsavel.Click += new System.EventHandler(this.excluirResponsavel_Click);
            // 
            // ResponsavelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 470);
            this.Controls.Add(this.excluirResponsavel);
            this.Controls.Add(this.adicionarResponsavel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.usuarioSenha);
            this.Controls.Add(this.comboUsuario);
            this.Controls.Add(this.limparListaResponsavel);
            this.Controls.Add(this.registrarResponsavel);
            this.Controls.Add(this.listaResponsaveis);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ResponsavelForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Responsável";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ResponsavelForm_FormClosing);
            this.Load += new System.EventHandler(this.ResponsavelForm_Load);
            this.Shown += new System.EventHandler(this.ResponsavelForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listaResponsaveis;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button registrarResponsavel;
        private System.Windows.Forms.Button limparListaResponsavel;
        private System.Windows.Forms.ComboBox comboUsuario;
        private System.Windows.Forms.TextBox usuarioSenha;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button adicionarResponsavel;
        private System.Windows.Forms.Button excluirResponsavel;
    }
}