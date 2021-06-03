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
using System.Diagnostics;

namespace EnveloperUI
{
    public partial class ResponsavelForm : Form
    {
        public ResponsavelForm()
        {
            InitializeComponent();
        }

        private void ResponsavelForm_Shown(object sender, EventArgs e)
        {
            CarregarUsuario();
            ApresentarResponsaveis();
        }

        private void ApresentarResponsaveis()
        {
            // apresenta lista de operadores responsáveis já selecionados
            foreach (var operador in SuporteUI.OperadorResponsaveis)
            {
                listaResponsaveis.Items.Add(operador);
            }
            listaResponsaveis.DisplayMember = "UsuarioNome"; //define propriedade do objeto que será apresentado na lista para seleção
            listaResponsaveis.ValueMember = "Id"; //define propriedade do objeto que será usado como valor (identificador)
        }

        /// <summary>
        /// Apresenta o painel Usuario da tab com a lista dados da tabela Usuario carregadas do BD
        /// </summary>
        private void CarregarUsuario()
        {
            // carregar lista de usuários da table Usuario do BD
            // carregar dados do bd e apresentar na listbox
            UsuarioModel usuario = new UsuarioModel();
            List<UsuarioModel> listaUsuarioBD = new List<UsuarioModel>();
            // carrega listaUsuario do form com lista de objetos da classe Usuario 
            listaUsuarioBD = usuario.CarregarUsuario();
            comboUsuario.DataSource = listaUsuarioBD;
            comboUsuario.DisplayMember = "UsuarioNome"; //define propriedade do objeto que será apresentado na lista para seleção
            comboUsuario.ValueMember = "Id"; //define propriedade do objeto que será usado como valor (identificador)
            comboUsuario.Focus();
        }

        private void LimparCampos()
        {
            usuarioSenha.Text = "Senha";
            usuarioSenha.UseSystemPasswordChar = false;
            comboUsuario.SelectedItem = -1;
            comboUsuario.Focus();
        }
        private void DesmarcarLista()
        {
            // deseleciona todos itens da lista de responsáveis
            listaResponsaveis.Items.Clear();
        }

        private void ResponsavelForm_Load(object sender, EventArgs e)
        {
            // desloca um pouco a posição para ajustar a janela mãe
            this.Left += 70;
            this.Top += 20;
        }

        private void limparSelecao_Click(object sender, EventArgs e)
        {
            DesmarcarLista();
            LimparCampos();
        }

        /// <summary>
        /// Registra a lista de usuários selecionados para gravar no banco de dados quando gravar Envelope
        /// </summary>
        private void registrarResponsavel_Click(object sender, EventArgs e)
        {
            RegistrarListaResponsaveis();
            this.Close();
        }

        private void RegistrarListaResponsaveis()
        {
            // varre a lista de responsáveis selecionados e guarda na classe de suporte para acessar do form Envelope na hora de gravar no banco
            List<UsuarioModel> responsaveis = new List<UsuarioModel>();
            foreach (UsuarioModel item in listaResponsaveis.Items)
            {
                responsaveis.Add(item);
            }
            SuporteUI.OperadorResponsaveis = responsaveis;
        }

        private void adicionarResponsavel_Click(object sender, EventArgs e)
        {
            adicionarResponsavelLista();
        }

        /// Valida a senha digitada e acidiona usuário na lista de responsáveis
        private void adicionarResponsavelLista()
        {
            string tipoAcesso;
            // declarar objeto Usuario
            UsuarioModel usuario = new UsuarioModel();
            // verificar usuário
            tipoAcesso = usuario.VerificarLogin(comboUsuario.Text, usuarioSenha.Text);
            if (tipoAcesso != "")
            {
                listaResponsaveis.Items.Add(comboUsuario.SelectedItem); //senha conferiu, adiciona usuário a lista de responsáveis
                listaResponsaveis.DisplayMember = "UsuarioNome"; //define propriedade do objeto que será apresentado na lista para seleção
                listaResponsaveis.ValueMember = "Id"; //define propriedade do objeto que será usado como valor (identificador)
                LimparCampos();
            }
            else
            {
                MessageBox.Show("Usuário ou Senha não conferem. Verifique ...", "Responsável", MessageBoxButtons.OK, MessageBoxIcon.Information);
                usuarioSenha.Focus();
            }
        }

        private void usuarioSenha_Enter(object sender, EventArgs e)
        {
            usuarioSenha.UseSystemPasswordChar = true;
            usuarioSenha.SelectAll();
        }

        private void usuarioSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                adicionarResponsavelLista();
                e.SuppressKeyPress = true;
            }
        }

        private void excluirResponsavel_Click(object sender, EventArgs e)
        {
            excluirResponsavelLista(listaResponsaveis.SelectedIndex);
        }

        private void excluirResponsavelLista(int indice)
        {
            listaResponsaveis.Items.RemoveAt(indice);
        }

        private void listaResponsaveis_DoubleClick(object sender, EventArgs e)
        {
            excluirResponsavelLista(listaResponsaveis.SelectedIndex);
        }

        private void comboUsuario_Enter(object sender, EventArgs e)
        {
            comboUsuario.DroppedDown = true;
        }

        private void comboUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            /*
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
                e.SuppressKeyPress = true;
            } */
        }

        private void comboUsuario_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SendKeys.Send("{TAB}");
            Cursor.Current = Cursors.Default;
        }

        private void ResponsavelForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool listasIguais = false;
            bool itemEncontrado = true; // no primeiro item não encontrado, interrompe o laço
            int tamanhoListaAtual = listaResponsaveis.Items.Count;
            int tamanhoListaPrevia = SuporteUI.OperadorResponsaveis.Count;

            // testa se as listas tem o mesmo número de elementos, se não tiver já são diferentes
            if (tamanhoListaAtual == tamanhoListaPrevia)
            {

                listasIguais = true;
                // pega um por um responsavel da lista atual e procura na lista prévia
                foreach (var responsavelAtual in listaResponsaveis.Items)
                {
                    // varre a lista prévia procurando o responsavel da lista atual
                    foreach (var responsavelPrevio in SuporteUI.OperadorResponsaveis)
                    {
                        if (responsavelPrevio == responsavelAtual )
                        {
                            itemEncontrado = true; // responsavel na lista atual encontrado na lista prévia
                        }
                        else
                        {
                            itemEncontrado = false;
                        }
                    }
                }
            }

            // se listas não são iguais, então alerta usuário que a lista foi alterada e não foi registrada
            if (!listasIguais || !itemEncontrado)
            {
                DialogResult resposta = new DialogResult();
                resposta = MessageBox.Show("Houve uma alteração na lista de responsáveis desde o último registro, gostaria de registrar esta nova lista?",
                                            "Registrar Responsáveis", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (resposta == DialogResult.Yes)
                {
                    RegistrarListaResponsaveis();
                }
            }

        }
    }
}

