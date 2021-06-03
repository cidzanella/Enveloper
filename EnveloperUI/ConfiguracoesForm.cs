using EnveloperLibrary.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace EnveloperUI
{
    public partial class ConfiguracaoForm : Form
    {
        const string textoInicialNovoClima = "Informe um novo tipo de Clima";
        const string textoInicialNovoTurno = "Informe um novo Turno";
        const string textoInicialNovoPDV = "Informe um novo PDV";
        const string textoInicialNovaPermissao = "Informe um novo tipo de Permissão";
        const string textoInicialNovoUsuario = "Nickname";
        const string textoInicialSenhaA = "Senha";
        const string textoInicialSenhaB = "Confirme a senha";
        const string textoInicialCombo = "Selecione ...";

        /// <summary>
        /// Função que muda cursor para WAIT ou para NORMAL de acordo com o estado atual do cursor
        /// </summary>
        private void ToggleCursorWait()
        {
            if (Cursor.Current == Cursors.WaitCursor)
            {
                Cursor.Current = Cursors.Default;
            }
            else
            {
                Cursor.Current = Cursors.WaitCursor;
            }
        }

        public ConfiguracaoForm()
        {
            InitializeComponent();
        }

        private void ConfigurarClima_Click(object sender, EventArgs e)
        {
            ApresentarClima();
            // Listbox ComboBox armazenando objetos
            //
            // Como a lista no form é uma lista de objetos da classe ClimaModel 
            // Usamos DisplayMember (campo apresentado) e ValueMember (campo de identificação)
            // No ValueMember guardamos o ID associado a cada item na lista e pode ser acessado através de listaClima.SelectedValue
            // Então para um SQL baseado no ID basta acessar esta informação do SelectedValue
            //
            // Para acessar os dados de cada propriedade da classe precisa:
            // Ou criar um objeto da classe e ai acessar a propriedade
            // Ou fazer um cast do item selecionado da Lista para a classe e ai acessar a propriedade
            //
            // Exemplo criando objeto da classe:
            // ClimaModel clima = new ClimaModel();
            // clima = (ClimaModel) listaClima.SelectedItem;
            // Debug.WriteLine("clima.ID = " + clima.Id);
            // Debug.WriteLine("clima.ClimaNome = " + clima.ClimaNome);
            //
            // Exemplo fazendo cast direto
            // Debug.WriteLine("Cast Direto: " + ((ClimaModel)listaClima.SelectedItem).Id);
            // Debug.WriteLine("Cast Direto: " + ((ClimaModel)listaClima.SelectedItem).ClimaNome);


        }

        private void ConfigurarPDV_Click(object sender, EventArgs e)
        {
            ApresentarPDV();
        }

        private void ConfigurarPermissoes_Click(object sender, EventArgs e)
        {
            ApresentarPermissao();
        }

        private void ConfigurarTurno_Click(object sender, EventArgs e)
        {
            ApresentarTurno();
        }

        private void ConfigurarUsuario_Click(object sender, EventArgs e)
        {
            ApresentarUsuario();
        }

        private void Retornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConfiguracaoForm_Load(object sender, EventArgs e)
        {
            tabConfiguracao.Top = -25;
            tabConfiguracao.Left = 146;
        }

        /// <summary>
        /// Apresenta o painel Clima da tab com a lista dados da tabela Clima carregadas do BD
        /// </summary>
        private void ApresentarClima()
        {
            // carregar lista de climas da table Clima do BD
            // carregar dados do bd e apresentar na listbox
            ClimaModel clima = new ClimaModel();
            List<ClimaModel> listaClimaBD = new List<ClimaModel>();
            // carrega listaClima do form com lista de objetos da classe Clima 
            listaClimaBD = clima.CarregarClima();
            listaClima.DataSource = listaClimaBD;
            listaClima.DisplayMember = "ClimaNome"; //define propriedade do objeto que será apresentado na lista para seleção
            listaClima.ValueMember = "Id"; //define propriedade do objeto que será usado como valor (identificador)
            // Apresentar painel Clima
            tabConfiguracao.SelectedIndex = 0;
            novoClima.Text = textoInicialNovoClima;
            novoClima.Focus();
            novoClima.SelectAll();
        }
        
        /// <summary>
        /// Apresenta o painel PDV da tab com a lista dados da tabela PDV carregadas do BD
        /// </summary>
        private void ApresentarPDV()
        {
            // carregar lista de pdvs da table PDV do BD
            // carregar dados do bd e apresentar na listbox
            PdvModel pdv = new PdvModel();
            List<PdvModel> listaPdvBD = new List<PdvModel>();
            // carrega listaPDV do form com lista de objetos da classe PDV
            listaPdvBD = pdv.CarregarPDV();
            listaPDV.DataSource = listaPdvBD;
            listaPDV.DisplayMember = "PdvNome"; //define propriedade do objeto que será apresentado na lista para seleção
            listaPDV.ValueMember = "Id"; //define propriedade do objeto que será usado como valor (identificador)
            // Apresentar painel PDV
            tabConfiguracao.SelectedIndex = 1;
            novoPDV.Text = textoInicialNovoPDV;
            novoPDV.Focus();
            novoPDV.SelectAll();
        }
        
        /// <summary>
        /// Apresenta o painel Permissao da tab com a lista dados da tabela Permissao carregadas do BD
        /// </summary>
        private void ApresentarPermissao()
        {
            // carregar lista de permissões da table Permissao do BD
            // carregar dados do bd e apresentar na listbox
            PermissaoModel permissao = new PermissaoModel();
            List<PermissaoModel> listaPermissaoBD = new List<PermissaoModel>();
            // carrega listaPermissao do form com lista de objetos da classe Permissao 
            listaPermissaoBD = permissao.CarregarPermissao();
            listaPermissao.DataSource = listaPermissaoBD;
            listaPermissao.DisplayMember = "PermissaoNome"; //define propriedade do objeto que será apresentado na lista para seleção
            listaPermissao.ValueMember = "Id"; //define propriedade do objeto que será usado como valor (identificador)
            // Apresentar painel Permissao
            tabConfiguracao.SelectedIndex = 2;
            novaPermissao.Text = textoInicialNovaPermissao;
            novaPermissao.Focus();
            novaPermissao.SelectAll();
        }

        /// <summary>
        /// Apresenta o painel Turno da tab com a lista dados da tabela Turno carregadas do BD
        /// </summary>
        private void ApresentarTurno()
        {
            // carregar lista de Turno da table Turno do BD
            // carregar dados do bd e apresentar na listbox
            TurnoModel turno = new TurnoModel();
            List<TurnoModel> listaTurnoBD = new List<TurnoModel>();
            // carrega listaTurno do form com lista de objetos da classe Turno 
            listaTurnoBD = turno.CarregarTurno();
            listaTurno.DataSource = listaTurnoBD;
            listaTurno.DisplayMember = "TurnoNome"; //define propriedade do objeto que será apresentado na lista para seleção
            listaTurno.ValueMember = "Id"; //define propriedade do objeto que será usado como valor (identificador)
            // Apresentar painel Turno
            tabConfiguracao.SelectedIndex = 3;
            novoTurno.Text = textoInicialNovoTurno;
            novoTurno.Focus();
            novoTurno.SelectAll();
        }

        /// <summary>
        /// Apresenta o painel Usuario da tab com a lista dados da tabela Usuario carregadas do BD
        /// </summary>
        private void ApresentarUsuario()
        {
            // carregar lista de usuários da table Usuario do BD
            // carregar dados do bd e apresentar na listbox
            UsuarioModel usuario = new UsuarioModel();
            List<UsuarioModel> listaUsuarioBD = new List<UsuarioModel>();
            // carrega listaUsuario do form com lista de objetos da classe Usuario 
            listaUsuarioBD = usuario.CarregarUsuario();
            listaUsuario.DataSource = listaUsuarioBD;
            listaUsuario.DisplayMember = "UsuarioNome"; //define propriedade do objeto que será apresentado na lista para seleção
            listaUsuario.ValueMember = "Id"; //define propriedade do objeto que será usado como valor (identificador)
            // carregar comboBox Permissao
            PermissaoModel permissao = new PermissaoModel();
            List<PermissaoModel> listaPermissaoBD = new List<PermissaoModel>();
            listaPermissaoBD = permissao.CarregarPermissao();
            comboUsuarioPermissao.DataSource = listaPermissaoBD;
            comboUsuarioPermissao.DisplayMember = "PermissaoNome";
            comboUsuarioPermissao.ValueMember = "Id";
            // carregar combobox PDV
            PdvModel pdv = new PdvModel();
            List<PdvModel> listaPdvBD = new List<PdvModel>();
            listaPdvBD = pdv.CarregarPDV();
            comboUsuarioPDV.DataSource = listaPdvBD;
            comboUsuarioPDV.DisplayMember = "PdvNome";
            comboUsuarioPDV.ValueMember = "Id";

            LimparCamposUsuario();
        }

        private void LimparCamposUsuario()
        {
            // Apresentar painel Usuário
            tabConfiguracao.SelectedIndex = 4;
            usuarioNickname.Text = textoInicialNovoUsuario;
            usuarioSenhaA.UseSystemPasswordChar = false;
            usuarioSenhaA.Text = textoInicialSenhaA;
            usuarioSenhaB.UseSystemPasswordChar = false;
            usuarioSenhaB.Text = textoInicialSenhaB;
            comboUsuarioPDV.SelectedIndex = -1;
            comboUsuarioPDV.Text = textoInicialCombo;
            comboUsuarioPermissao.SelectedIndex = -1;
            comboUsuarioPermissao.Text = textoInicialCombo;
            usuarioNickname.Focus();
            usuarioNickname.SelectAll();

        }
        private void ConfiguracaoForm_Shown(object sender, EventArgs e)
        {
            // apresenta primeira tab - Clima - carrega lista do bd
            ApresentarClima();
        }
        
        private void RemoverClimaBD(ClimaModel item)
        {

            if (MessageBox.Show($"Deseja remover o tipo de clima {item.ClimaNome}?", "Confirmação", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                // confirmou exclusão
                ClimaModel clima = new ClimaModel();
                bool climaExcluido=false ;

                ToggleCursorWait();
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    climaExcluido = clima.ExcluirClima(item.Id);
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Falha ao remover {item.ClimaNome}! Erro: {e.Message}.", "Falha", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                ToggleCursorWait();

                if (climaExcluido)
                {
                    ApresentarClima();
                }
                else
                {
                    MessageBox.Show($"Falha ao remover {item.ClimaNome}!", "Falha", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void RemoverPdvBD(PdvModel item)
        {
            if (MessageBox.Show($"Deseja remover o PDV {item.PdvNome}?", "Confirmação", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                // confirmou exclusão
                PdvModel pdv = new PdvModel();
                if (pdv.ExcluirPDV(item.Id))
                {
                    ApresentarPDV();
                }
                else
                {
                    MessageBox.Show($"Falha ao remover {item.PdvNome}!", "Falha", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void RemoverPermissaoBD(PermissaoModel item)
        {
            if (MessageBox.Show($"Deseja remover a permissão do tipo {item.PermissaoNome}?", "Confirmação", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                // confirmou exclusão
                PermissaoModel permissao = new PermissaoModel();
                if (permissao.ExcluirPermissao(item.Id))
                {
                    ApresentarPermissao();
                }
                else
                {
                    MessageBox.Show($"Falha ao remover {item.PermissaoNome}!", "Falha", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void RemoverTurnoBD(TurnoModel item)
        {
            if (MessageBox.Show($"Deseja remover o turno {item.TurnoNome}?", "Confirmação", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                // confirmou exclusão
                TurnoModel turno = new TurnoModel();
                if (turno.ExcluirTurno(item.Id))
                {
                    ApresentarTurno();
                }
                else
                {
                    MessageBox.Show($"Falha ao remover {item.TurnoNome}!", "Falha", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void RemoverUsuarioBD(UsuarioModel item)
        {
            if (MessageBox.Show($"Deseja remover o Usuário {item.UsuarioNome}?", "Confirmação", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                // confirmou exclusão
                UsuarioModel usuario = new UsuarioModel();
                if (usuario.ExcluirUsuario(item.Id))
                {
                    ApresentarUsuario();
                }
                else
                {
                    MessageBox.Show($"Falha ao remover {item.UsuarioNome}!", "Falha", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        private void RemoverTipoClima_Click(object sender, EventArgs e)
        {
            RemoverClimaBD((ClimaModel)listaClima.SelectedItem);
        }

        private void RemoverPDV_Click(object sender, EventArgs e)
        {
            RemoverPdvBD((PdvModel) listaPDV.SelectedItem);
        }

        private void RemoverPermissao_Click(object sender, EventArgs e)
        {
            RemoverPermissaoBD((PermissaoModel) listaPermissao.SelectedItem);
        }

        private void RemoverTurno_Click(object sender, EventArgs e)
        {
            RemoverTurnoBD((TurnoModel) listaTurno.SelectedItem);
        }

        private void RemoverUsuario_Click(object sender, EventArgs e)
        {
            RemoverUsuarioBD((UsuarioModel) listaUsuario.SelectedItem);
        }

        private void AdicionarTipoClima_Click(object sender, EventArgs e)
        {
            AdicionarClimaBD(novoClima.Text);
        }

        private void AdicionarPDV_Click(object sender, EventArgs e)
        {
            AdicionarPdvBD(novoPDV.Text);
        }

        private void AdicionarPermissao_Click(object sender, EventArgs e)
        {
            AdicionarPermissaoBD(novaPermissao.Text);   
        }

        private void AdicionarTurno_Click(object sender, EventArgs e)
        {
            AdicionarTurnoBD(novoTurno.Text);
        }

        /// Valida se foram preenchidos os campos para grava no BD
        /// Recebe como parâmetro se operação é NovoUsuário ou Atualização (Insert ou Update)
        private bool ValidaCamposUsuario(bool novoUsuario)
        {
            bool camposValidos = true;
            // verifica se Usuario informado já está na lista
            int indice = listaUsuario.FindString(usuarioNickname.Text);

            // se novo então adicionar novo registro
            if (novoUsuario)
            {
                if (indice != -1) // usuário não pode estar na lista
                {
                    camposValidos = false;
                    MessageBox.Show("Usuário informado já existe na lista. Verifique ...", "Adicionar Usuário", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            } else // se não é novo, então está atualizando usuário existente
            {
                if (indice == -1) // usuário tem que estar na lista
                {
                    camposValidos = false;
                    MessageBox.Show("Selecione um usuário da lista para atualizar dados.", "Atualizar Usuário", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            // testa se os campos foram preenchidos e informado um novo tipo de Usuario 
            if (usuarioNickname.Text == textoInicialNovoUsuario || usuarioNickname.Text.Length == 0)
            {
                camposValidos = false;
                MessageBox.Show("Nome de Usuário informado não é válido. Verifique ...", "Adicionar/Atualizar Usuário", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (usuarioSenhaA.Text != usuarioSenhaB.Text)
            {
                camposValidos = false;
                MessageBox.Show("Não foi possível confirmar a senha. Informe e confirme a senha novamente.", "Confirmação de Senha", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                usuarioSenhaA.Text = "";
                usuarioSenhaB.Text = "";
                usuarioSenhaA.Focus();
            }
            if (comboUsuarioPDV.SelectedIndex == -1 || comboUsuarioPermissao.SelectedIndex == -1)
            {
                camposValidos = false;
                MessageBox.Show("Selecione um tipo de Permissão e um PDV para este usuário. Verifique ...", "Adicionar Usuário", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return camposValidos;
        }

        private void AdicionarUsuario_Click(object sender, EventArgs e)
        {
            // testa se campos foram preenchidos corretamente e informa que é um novo registro, adicionando usuário
            if (ValidaCamposUsuario(true))
            { 
                AdicionarUsuarioBD(usuarioNickname.Text, usuarioSenhaA.Text, usuarioSenhaB.Text, (int)comboUsuarioPermissao.SelectedValue, (int)comboUsuarioPDV.SelectedValue);
            }
        }
        private void listaClima_DoubleClick(object sender, EventArgs e)
        {
            RemoverClimaBD((ClimaModel)listaClima.SelectedItem);
        }

        private void listaPermissao_DoubleClick(object sender, EventArgs e)
        {
            RemoverPermissaoBD((PermissaoModel)listaPermissao.SelectedItem);
        }

        private void listaPDV_DoubleClick(object sender, EventArgs e)
        {
            RemoverPdvBD((PdvModel)listaPDV.SelectedItem);
        }

        private void listaTurno_DoubleClick(object sender, EventArgs e)
        {
            RemoverTurnoBD((TurnoModel)listaTurno.SelectedItem);
        }

        private void listaUsuario_DoubleClick(object sender, EventArgs e)
        {
            RemoverUsuarioBD((UsuarioModel)listaUsuario.SelectedItem);
        }

        // Adiciona novo clima no banco de dados através da classe ClimaModel
        private void AdicionarClimaBD(string novo)
        {
            // verifica se novo clima informado já está na lista
            int indice = listaClima.FindString(novo);
            // testa se foi informado um novo tipo de clima
            if (novo == textoInicialNovoClima || novo.Length == 0 || indice != -1)
            {
                MessageBox.Show("Novo clima informado não é válido. Verifique ...", "Adicionar Clima", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // adiciona o novo clima e reapresenta lista
                ClimaModel clima = new ClimaModel();
                if (clima.AdicionarClima(novo))
                {
                    ApresentarClima();
                }
                else
                {
                    MessageBox.Show($"Falha ao adicionar clima {novo} a lista!", "Falha", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        // Adiciona novo PDV no banco de dados através da classe PdvModel
        private void AdicionarPdvBD(string novo)
        {
            // verifica se novo PDV informado já está na lista
            int indice = listaPDV.FindString(novo);
            // testa se foi informado um novo tipo de PDV
            if (novo == textoInicialNovoPDV || novo.Length == 0 || indice != -1)
            {
                MessageBox.Show("Novo PDV informado não é válido. Verifique ...", "Adicionar PDV", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // adiciona o novo PDV e reapresenta lista
                PdvModel pdv = new PdvModel();
                if (pdv.AdicionarPDV(novo))
                {
                    ApresentarPDV();
                }
                else
                {
                    MessageBox.Show($"Falha ao adicionar PDV {novo} a lista!", "Falha", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        // Adiciona nova Permissão no banco de dados através da classe PermissaoModel
        private void AdicionarPermissaoBD(string novo)
        {
            // verifica se novo Permissao informado já está na lista
            int indice = listaPermissao.FindString(novo);
            // testa se foi informado um novo tipo de Permissao
            if (novo == textoInicialNovaPermissao || novo.Length == 0 || indice != -1)
            {
                MessageBox.Show("Nova Permissão informada não é válida. Verifique ...", "Adicionar Permissão", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // adiciona o novo Permissao e reapresenta lista
                PermissaoModel Permissao = new PermissaoModel();
                if (Permissao.AdicionarPermissao(novo))
                {
                    ApresentarPermissao();
                }
                else
                {
                    MessageBox.Show($"Falha ao adicionar Permissão {novo} a lista!", "Falha", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        // Adiciona novo Turno no banco de dados através da classe TurnoModel
        private void AdicionarTurnoBD(string novo)
        {
            // verifica se novo Turno informado já está na lista
            int indice = listaTurno.FindString(novo);
            // testa se foi informado um novo tipo de Turno
            if (novo == textoInicialNovoTurno || novo.Length == 0 || indice != -1)
            {
                MessageBox.Show("Novo Turno informado não é válido. Verifique ...", "Adicionar Turno", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // adiciona o novo Turno e reapresenta lista
                TurnoModel turno = new TurnoModel();
                if (turno.AdicionarTurno(novo))
                {
                    ApresentarTurno();
                }
                else
                {
                    MessageBox.Show($"Falha ao adicionar Turno {novo} a lista!", "Falha", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        // Adiciona novo Usuario no banco de dados através da classe UsuarioModel
        private void AdicionarUsuarioBD(string nome, string senhaA ,string senhaB, int permissao, int pdv)
        {
            // adiciona o novo Usuario e reapresenta lista
            UsuarioModel usuario = new UsuarioModel();
            if (usuario.AdicionarUsuario(nome, senhaA, permissao, pdv))
            {
                ApresentarUsuario();
            }
            else
            {
                MessageBox.Show($"Falha ao adicionar Usuário {nome} a lista!", "Falha", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Atualiza Usuario no banco de dados através da classe UsuarioModel
        private void AtualizarUsuarioBD(string nome, string senhaA, string senhaB, int permissao, int pdv)
        {
            // adiciona o novo Usuario e reapresenta lista
            UsuarioModel usuario = new UsuarioModel();
            if (usuario.AtualizarUsuario(nome, senhaA, permissao, pdv))
            {
                ApresentarUsuario();
            }
            else
            {
                MessageBox.Show($"Falha ao adicionar Usuário {nome} a lista!", "Falha", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void novoClima_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                AdicionarClimaBD(novoClima.Text);
            }
        }

        private void novaPermissao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AdicionarPermissaoBD(novaPermissao.Text);
            }
        }

        private void novoPDV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AdicionarPdvBD(novoPDV.Text);
            }
        }

        private void novoTurno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AdicionarTurnoBD(novoTurno.Text);
            }
        }

        private void usuarioSenhaA_Enter(object sender, EventArgs e)
        {
            usuarioSenhaA.UseSystemPasswordChar = true;
        }

        private void usuarioSenhaB_Enter(object sender, EventArgs e)
        {
            usuarioSenhaB.UseSystemPasswordChar = true;
        }

        private void NovoRegistro_Click(object sender, EventArgs e)
        {
            LimparCamposUsuario();
        }

        private void listaUsuario_Click(object sender, EventArgs e)
        {
            ApresentarUsuarioDetalhes( (UsuarioModel) listaUsuario.SelectedItem);
        }

        private void ApresentarUsuarioDetalhes(UsuarioModel usuario)
        {
            usuarioNickname.Text = usuario.UsuarioNome;
            usuarioSenhaA.UseSystemPasswordChar = true;
            usuarioSenhaA.Text = usuario.UsuarioSenha;
            usuarioSenhaB.UseSystemPasswordChar = true;
            usuarioSenhaB.Text = usuario.UsuarioSenha;
            comboUsuarioPDV.SelectedValue = usuario.UsuarioPDV;
            comboUsuarioPermissao.SelectedValue = usuario.UsuarioPermissao;
        }

        private void listaUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void atualizarUsuario_Click(object sender, EventArgs e)
        {
            // testa se campos foram preenchidos corretamente e informa que é uma atualização registro, adicionando usuário
            if (ValidaCamposUsuario(false))
            {
                AtualizarUsuarioBD(usuarioNickname.Text, usuarioSenhaA.Text, usuarioSenhaB.Text, (int)comboUsuarioPermissao.SelectedValue, (int)comboUsuarioPDV.SelectedValue);
            }

        }

        private void VerificarDisponibilidade_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void tabUsuario_Click(object sender, EventArgs e)
        {

        }
    }
}
