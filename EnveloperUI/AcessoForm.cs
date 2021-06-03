using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EnveloperLibrary;
using EnveloperLibrary.Models;


namespace EnveloperUI
{
    public partial class AcessoForm : Form
    {
        public AcessoForm()
        {
            InitializeComponent();
        }

        private void AcessoForm_Load(object sender, EventArgs e)
        {
            this.Left += 70;
            this.Top += 20;
            this.AcceptButton = AcessarSistema;
        }

        private void AcessarSistema_Click(object sender, EventArgs e)
        {
            string tipoAcesso;
            // declarar objeto Usuario
            UsuarioModel usuario = new UsuarioModel();
            // verificar usuário
            tipoAcesso = usuario.VerificarLogin(nomeUsuario.Text, senhaUsuario.Text);
            if (tipoAcesso != "")
            {
                SuporteUI.OperadorAtivoNome = nomeUsuario.Text;
                SuporteUI.OperadorAtivoTipoAcesso = tipoAcesso;
                this.DialogResult = DialogResult.OK;
                this.Close();
            } else
            {
                MessageBox.Show("Usuário ou Senha não conferem. Verifique ...", "Acesso Negado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                nomeUsuario.Focus();
            }
        }

        private void senhaUsuario_Enter(object sender, EventArgs e)
        {
            senhaUsuario.Text = "";
            senhaUsuario.PasswordChar = '*';
        }

        private void senhaUsuario_Leave(object sender, EventArgs e)
        {
            if (senhaUsuario.Text == "")
            {
                senhaUsuario.PasswordChar = (char)0;
                senhaUsuario.Text = "Senha";
            }
        }

        private void nomeUsuario_Leave(object sender, EventArgs e)
        {
            if (nomeUsuario.Text == "")
            {
                nomeUsuario.Text = "Usuário";
            }

        }

        private void nomeUsuario_Enter(object sender, EventArgs e)
        {
        }
    }
}
