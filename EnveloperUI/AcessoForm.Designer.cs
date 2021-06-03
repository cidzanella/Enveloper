namespace EnveloperUI
{
    partial class AcessoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AcessoForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.AcessarSistema = new System.Windows.Forms.Button();
            this.senhaUsuario = new System.Windows.Forms.TextBox();
            this.nomeUsuario = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.usuarioImagem = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.usuarioImagem)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Location = new System.Drawing.Point(108, 109);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(253, 253);
            this.panel1.TabIndex = 0;
            // 
            // AcessarSistema
            // 
            this.AcessarSistema.BackColor = System.Drawing.Color.Gray;
            this.AcessarSistema.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AcessarSistema.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.AcessarSistema.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.AcessarSistema.Location = new System.Drawing.Point(174, 324);
            this.AcessarSistema.Name = "AcessarSistema";
            this.AcessarSistema.Size = new System.Drawing.Size(122, 28);
            this.AcessarSistema.TabIndex = 42;
            this.AcessarSistema.Text = "ACESSAR";
            this.AcessarSistema.UseVisualStyleBackColor = false;
            this.AcessarSistema.Click += new System.EventHandler(this.AcessarSistema_Click);
            // 
            // senhaUsuario
            // 
            this.senhaUsuario.BackColor = System.Drawing.SystemColors.Window;
            this.senhaUsuario.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.senhaUsuario.Font = new System.Drawing.Font("Segoe UI", 15.75F);
            this.senhaUsuario.ForeColor = System.Drawing.Color.Gray;
            this.senhaUsuario.Location = new System.Drawing.Point(139, 279);
            this.senhaUsuario.Name = "senhaUsuario";
            this.senhaUsuario.Size = new System.Drawing.Size(192, 28);
            this.senhaUsuario.TabIndex = 41;
            this.senhaUsuario.Text = "Senha";
            this.senhaUsuario.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.senhaUsuario.Enter += new System.EventHandler(this.senhaUsuario_Enter);
            this.senhaUsuario.Leave += new System.EventHandler(this.senhaUsuario_Leave);
            // 
            // nomeUsuario
            // 
            this.nomeUsuario.BackColor = System.Drawing.SystemColors.Window;
            this.nomeUsuario.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nomeUsuario.Font = new System.Drawing.Font("Segoe UI", 15.75F);
            this.nomeUsuario.ForeColor = System.Drawing.Color.Gray;
            this.nomeUsuario.Location = new System.Drawing.Point(139, 234);
            this.nomeUsuario.Name = "nomeUsuario";
            this.nomeUsuario.Size = new System.Drawing.Size(192, 28);
            this.nomeUsuario.TabIndex = 40;
            this.nomeUsuario.Text = "Usuário";
            this.nomeUsuario.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nomeUsuario.Enter += new System.EventHandler(this.nomeUsuario_Enter);
            this.nomeUsuario.Leave += new System.EventHandler(this.nomeUsuario_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Gray;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(155, 192);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 21);
            this.label1.TabIndex = 44;
            this.label1.Text = "ACESSO AO SISTEMA";
            // 
            // usuarioImagem
            // 
            this.usuarioImagem.BackColor = System.Drawing.Color.Gray;
            this.usuarioImagem.Image = ((System.Drawing.Image)(resources.GetObject("usuarioImagem.Image")));
            this.usuarioImagem.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.usuarioImagem.Location = new System.Drawing.Point(203, 119);
            this.usuarioImagem.Margin = new System.Windows.Forms.Padding(2);
            this.usuarioImagem.Name = "usuarioImagem";
            this.usuarioImagem.Size = new System.Drawing.Size(65, 64);
            this.usuarioImagem.TabIndex = 43;
            this.usuarioImagem.TabStop = false;
            // 
            // AcessoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(470, 470);
            this.Controls.Add(this.AcessarSistema);
            this.Controls.Add(this.senhaUsuario);
            this.Controls.Add(this.nomeUsuario);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.usuarioImagem);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AcessoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Acesso";
            this.Load += new System.EventHandler(this.AcessoForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.usuarioImagem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button AcessarSistema;
        private System.Windows.Forms.TextBox senhaUsuario;
        private System.Windows.Forms.TextBox nomeUsuario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox usuarioImagem;
    }
}