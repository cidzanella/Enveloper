using EnveloperLibrary;
using EnveloperLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnveloperUI
{
    public partial class EnveloperForm : Form
    {

        public EnveloperForm()
        {
            InitializeComponent();
        }

        private void dinheiroInicial_Enter(object sender, EventArgs e)
        {
            MensagemOrientacao(dinheiroInicial);
        }

        private void dinheiroFinal_Enter(object sender, EventArgs e)
        {
            MensagemOrientacao(dinheiroFinal);
        }

        private void vendasCartao_Enter(object sender, EventArgs e)
        {
            MensagemOrientacao(vendasCartao);
        }

        private void sangriaTotalCaixa_Enter(object sender, EventArgs e)
        {
            MensagemOrientacao(sangriaTotalCaixa);
        }

        private void reforcoTotalCaixa_Enter(object sender, EventArgs e)
        {
            MensagemOrientacao(reforcoTotalCaixa);
        }

        private void dinheiroEnvelope_Enter(object sender, EventArgs e)
        {
            MensagemOrientacao(dinheiroEnvelope);
        }

        private void dinheiroPassagemCaixa_Enter(object sender, EventArgs e)
        {
            MensagemOrientacao(dinheiroPassagemCaixa);
        }

        private void campoObservacao_Enter(object sender, EventArgs e)
        {
            MensagemOrientacao(campoObservacao);
        }

        private void temperaturaAtual_Enter(object sender, EventArgs e)
        {
            MensagemOrientacao(temperaturaAtual);
        }

        private void dinheiroInicial_Leave(object sender, EventArgs e)
        {
            ValidaCampo(dinheiroInicial);

        }
        /// <summary>
        /// Função que compara o troco encontrado no caixa na abertura com o troco de passagem do último fechamento de caixa
        /// </summary>
        private double DiferencaTrocoInicial()
        {

            EnvelopeModel envelope = new EnvelopeModel();

            double dinheiroInicialEncontrado, dinheiroPassagem = 0;
            double.TryParse(dinheiroInicial.Text, out dinheiroInicialEncontrado);

            try
            {
                dinheiroPassagem = envelope.BuscarDinheiroPassagemCaixaUltimoFechamento();
            }
            catch (Exception err)
            {
                MessageBox.Show("Falha ao recuperar último fechamento de caixa. Exceção: " + err.Message,
                    "Validação do Dinheiro Inicial", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            if (dinheiroPassagem == envelope.RegistroPassagemCaixaNaoEncontrado )
            {
                MessageBox.Show("Não foi possível recuperar dados do último fechamento de caixa para validação do Dinheiro Inicial. Pressione OK para prosseguir ...",
                    "Validação do Dinheiro Inicial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return dinheiroPassagem;
            }
            // retorna a diferença, se ela for zero indica que bateu a contagem do troco inicial com o do repasse
            return (dinheiroInicialEncontrado-dinheiroPassagem);
        }

        private void dinheiroFinal_Leave(object sender, EventArgs e)
        {
            ValidaCampo(dinheiroFinal);
        }

        private void vendasCartao_Leave(object sender, EventArgs e)
        {
            ValidaCampo(vendasCartao);

        }

        private void sangriaTotalCaixa_Leave(object sender, EventArgs e)
        {
            // Validar campo
            ValidaCampo(sangriaTotalCaixa);
        }

        private void reforcoTotalCaixa_Leave(object sender, EventArgs e)
        {
            // Validar campo
            ValidaCampo(reforcoTotalCaixa);
        }


        private void dinheiroEnvelope_Leave(object sender, EventArgs e)
        {
            // Validar campo
            ValidaCampo(dinheiroEnvelope);
        }

        private void dinheiroPassagemCaixa_Leave(object sender, EventArgs e)
        {
            // Validar campo
            ValidaCampo(dinheiroPassagemCaixa);
        }

        private void temperaturaAtual_Leave(object sender, EventArgs e)
        {
            // Validar campo
            ValidaCampo(temperaturaAtual);
        }

        // provoca validação de todos os campos do envelope antes de registrar
        // realiza conferências de valores
        private bool ValidaPreenchimentoEnvelope()
        {
            bool preenchimentoValido = true;
            // se alguma validação do campo retornar falso então preenchimentoValido será falso
            preenchimentoValido = (ValidaCampo(dinheiroInicial) && preenchimentoValido);
            preenchimentoValido = (ValidaCampo(dinheiroFinal) && preenchimentoValido);
            preenchimentoValido = (ValidaCampo(vendasCartao) && preenchimentoValido);
            preenchimentoValido = (ValidaCampo(sangriaTotalCaixa) && preenchimentoValido);
            preenchimentoValido = (ValidaCampo(faturamentoTotalVendas) && preenchimentoValido); //a validação deste campo já dispara ValidaValorTotalFechamento
            preenchimentoValido = (ValidaCampo(reforcoTotalCaixa) && preenchimentoValido);
            preenchimentoValido = (ValidaCampo(dinheiroEnvelope) && preenchimentoValido);
            preenchimentoValido = (ValidaCampo(dinheiroPassagemCaixa) && preenchimentoValido);
            preenchimentoValido = (ValidaCampo(temperaturaAtual) && preenchimentoValido);
            
            // testa se indicou um clima
            if ((climaChuvoso.Checked || climaLimpo.Checked || climaNublado.Checked) == false)
            {
                // como não tem nenhum clima selecionado então alerta o operador para selecionar um e indica que preenchimento não é válido
                preenchimentoValido = false;
                erroEnveloperForm.SetError(indiqueClima, "Selecione o clima predominante no turno.");  // seta erro no label do grupo de buttons
            } else
            {
                erroEnveloperForm.SetError(indiqueClima, "");  // reset erro no label do grupo de buttons
            }

            // testa se indicou um turno
            if ((turnoTarde.Checked || turnoNoite.Checked || turnoUnico.Checked) == false)
            {
                // como não tem nenhum turno selecionado então alerta o operador para selecionar um e indica que preenchimento não é válido
                preenchimentoValido = false;
                erroEnveloperForm.SetError(indiqueTurno, "Selecione o turno deste movimento de caixa."); // seta erro no label do grupo de buttons
            } else
            {
                erroEnveloperForm.SetError(indiqueTurno, "");  // reset erro no label do grupo de buttons
            }

            // retorna status de preenchimento válido ou não dos campos
            return preenchimentoValido;
        }
        /// <summary>
        /// Informa no formulário mensagem de orientação sobre preenchimento do campo
        /// <para>
        /// Recebe como parâmetro o objeto do formulário, como caixa de texto
        /// </para>
        /// </summary>
        private void MensagemOrientacao(TextBox campoFormulario)
        {
            switch (campoFormulario.Name)
            {
                case "dinheiroInicial":
                    rodapeMensagem.Text = "Faça a CONTAGEM DO DINHEIRO existente na ABERTURA DO CAIXA, anote o valor neste campo. Inicie um novo Envelope registrando abertura do caixa!";
                    break;
                case "dinheiroFinal":
                    rodapeMensagem.Text = "Faça a CONTAGEM DO DINHEIRO existente no FECHAMENTO DO CAIXA, anote o valor neste campo.";
                    break;
                case "vendasCartao":
                    rodapeMensagem.Text = "Na MÁQUINA DE CARTÃO, imprima o EXTRATO DE VENDAS do período, informe aqui o valor e guarde o extrato no envelope.";
                    break;
                case "sangriaTotalCaixa":
                    rodapeMensagem.Text = "Informe aqui a SOMA DAS SANGRIAS realizadas, guarde no envelope os COMPROVANTES destas operações.";
                    break;
                case "reforcoTotalCaixa":
                    rodapeMensagem.Text = "Informe aqui a SOMA DOS REFORÇOS DE TROCO realizados, guarde no envelope os COMPROVANTES destas operações.";
                    break;
                case "faturamentoTotalVendas":
                    rodapeMensagem.Text = "Informe aqui o VALOR TOTAL DE VENDAS indicado no ticket de fechamento do sistema PDV vendas, guarde o ticket no envelope.";
                    break;
                case "dinheiroEnvelope":
                    rodapeMensagem.Text = "Informe neste campo o valor em DINHEIRO DEPOSITADO NO ENVELOPE.";
                    break;
                case "dinheiroPassagemCaixa":
                    rodapeMensagem.Text = "Anote aqui o VALOR deixado na gaveta para a REABERTURA DO CAIXA no próximo turno.";
                    break;
                case "temperaturaAtual":
                    rodapeMensagem.Text = "Informe a TEMPERATURA ATUAL e selecione ao lado como esteve o CLIMA na maior parte do seu turno.";
                    break;
                case "campoObservacao":
                    rodapeMensagem.Text = "Utilize o campo OBSERVAÇÃO para registrar informações importantes sobre este fechamento de caixa.";
                    break;
                case "climaTurno":
                    rodapeMensagem.Text = "Selecione como esteve o CLIMA na maior parte do seu turno.";
                    break;
            }

        }

        /// <summary>
        /// Valida valor informados nos campos, particularmente se campo tem entrada numérica
        /// </summary>
        private bool ValidaCampo(TextBox campoFormulario)
        {

            bool campoValido;
            string campoTexto;
            string campoNome;


            campoNome = campoFormulario.Name;
            campoTexto = campoFormulario.Text;


            // Valida se entrada é numérica
            campoValido = double.TryParse(campoTexto, out double campoValor);
            
            // Se não é numérica, ou for numérico negativo, informa msg correspondente
            if (campoValido == false || campoValor <0 ) 
            {
                // seta false para caso de ter entrado no if por conta de valor inferior a zero
                // senão no final do método  CampoValido true irá retirar mensagem de erro do campo 
                campoValido = false; 
            
                // se já setou o erro uma vez, não insiste em manter o foco
                if (erroEnveloperForm.GetError(campoFormulario) == "")
                {
                    campoFormulario.Focus();
                    campoFormulario.SelectAll();
                }

                //1 rodapeMensagem.Text = $"O campo { campoNome.ToUpper() } deve ser númerico.";
                erroEnveloperForm.SetError(campoFormulario, "Este campo deve ser numérico, maior ou igual a zero.");
            }
            else
            {
                // como é campo numérico formata para duas casa decimais
                campoFormulario.Text = campoValor.ToString("N");
            }

            // Valida Temperatura: se for Temperatura e for númerico, verificar o range informado
            if (campoNome == "dinheiroInicial" && campoValido && caixaStatus.Text == "Caixa Aberto")
            {

                double inicial = campoValor;
                double registrado = double.Parse(caixaDinheiroRegistradoAbertura.Text.Substring(3)); //retira "R$" e converte para double

                if (inicial != registrado)
                {
                    DialogResult result;
                    // como já existe um caixa aberto, pergunta se deseja prosseguir com a abertura de um novo
                    result = MessageBox.Show($"Existe um caixa aberto com o valor de {caixaDinheiroRegistradoAbertura.Text} e um envelope iniciado. Deseja prosseguir com a alteração do valor de abertura do caixa? \n \n  " +
                        "Selecione SIM para prosseguir e alterar o valor de abertura do caixa. \n Selecione NÂO para manter o campo Dinheiro Inicial com o valor já informado na abertura deste caixa.", "Atenção, Caixa Aberto!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.No)
                    {
                        // selecionou Não então valor de abertura do caixa não será alterado, e campo DinheiroInicial deve receber o valor de abertura
                        dinheiroInicial.Text = registrado.ToString("N");                        
                    }
                    else
                    {
                        // selecionou SIM e deve prosseguir com atualização do valor inicial no caixa já aberto 
                        AtualizarValorAberturaCaixa();
                    }
                }
            }
            // Valida Temperatura: se for Temperatura e for númerico, verificar o range informado
            if (campoNome == "temperaturaAtual" && campoValido)
            {
                if (campoValor <= 0 || campoValor > 50)
                {
                    rodapeMensagem.Text = "Verifique a temperatura informada.";
                    erroEnveloperForm.SetError(campoFormulario, "Verifique a temperatura informada.");
                    campoValido = false;
                }
                // como lá em cima altera todos os campos para númerico formatado com duas casas decimais, aqui ajusta número inteiro como é temperatura
                campoFormulario.Text = campoValor.ToString();
            };

            //Valida faturamento total: se for Faturamento Total e for númerico, verificar se soma bate
            //Se está sobrando ou faltando dinheiro, não irá apontar como campo não válido, permite fechar com a diferença de caixa, mas esta ficará registrada
            if (campoNome == "faturamentoTotalVendas" && campoValido)
            {
                campoValido= ValidaValorTotalFechamento(); // verifica se bateu ou se está sobrando ou faltando dinheiro em espécie ou informaram algum valor errado.
            }

            //Valida depósito envelope e passagem de caixa 
            if (campoNome == "dinheiroPassagemCaixa" && campoValido)
            {
                // o dinheiro depositado no envelope somado ao deixado no repasse, bate com o dinheiro total do fechamento?
                // se existir diferença então não bateu, está sobrando ou faltando dinheiro em espécie ou informaram algum valor errado
                if (ValidaDepositoPassagemCaixa() == false)
                {
                    erroEnveloperForm.SetError(lblPassagemCaixa, "Erro no fechamento: confira o valor informado em Dinheiro Final, Dinheiro Envelope e Dinheiro Pasagem Caixa.");
                    campoValido = false;
                }
                else
                {
                    erroEnveloperForm.SetError(lblPassagemCaixa, "");
                }

            }

            // Se CampoValido então retira mensagem de erro do campo 
            if (campoValido)
            {
                erroEnveloperForm.SetError(campoFormulario, ""); //zera msg de erro do campo
            }

            return campoValido;
        }

        // Método que faz soma a conferência do valor depositado no envelope mais o deixado de repasse de caixa com o valor final de dinheiro do caixa 
        // dinheiro final = dinheiro envelope + dinheiro repasse 
        private bool ValidaDepositoPassagemCaixa()
        {
            double diferencaEncontrada;
            bool campoValido;

            bool campoValido1 = double.TryParse(dinheiroFinal.Text, out double final);
            bool campoValido2 = double.TryParse(dinheiroEnvelope .Text, out double envelope);
            bool campoValido3 = double.TryParse(dinheiroPassagemCaixa.Text, out double passagem);
            campoValido = campoValido1 && campoValido2 && campoValido3;

            // Se tem todoas entradas núméricas, faz conta se o fechamento bate
            if (campoValido)
            {
                diferencaEncontrada = final - (envelope + passagem);
                if (diferencaEncontrada != 0) campoValido = false;
            }
            // Se não bateu ou algum campo não é válido, acusa erro nos campos
            if (campoValido == false)
            {
                erroEnveloperForm.SetError(dinheiroFinal, "Erro no fechamento: confira o valor informado em Dinheiro Final, Dinheiro Envelope e Dinheiro Pasagem Caixa.");
                erroEnveloperForm.SetError(dinheiroEnvelope, "Erro no fechamento: confira o valor informado em Dinheiro Final, Dinheiro Envelope e Dinheiro Pasagem Caixa.");
                erroEnveloperForm.SetError(dinheiroPassagemCaixa, "Erro no fechamento: confira o valor informado em Dinheiro Final, Dinheiro Envelope e Dinheiro Pasagem Caixa.");
            }
            else
            {
                erroEnveloperForm.SetError(dinheiroFinal, "");
                erroEnveloperForm.SetError(dinheiroEnvelope, "");
                erroEnveloperForm.SetError(dinheiroPassagemCaixa, "");
            }
            return campoValido;

        }

        // Método que faz soma do valor total no fechamento e compara com o valor que deveria existir com base no valor inicial + entradas - saidas
        private bool ValidaValorTotalFechamento()
        {
            decimal dinheiroEsperado;
            decimal diferencaEncontrada;

            bool campoValido ;
            decimal inicial, total, cartao, reforco, sangria, final;

            // Valida se temos entradas númericas para todos os campos
            bool campoValido1 = decimal.TryParse(dinheiroInicial.Text, out inicial);
            bool campoValido2 = decimal.TryParse(faturamentoTotalVendas.Text, out total);
            bool campoValido3 = decimal.TryParse(vendasCartao.Text, out cartao);
            bool campoValido4 = decimal.TryParse(reforcoTotalCaixa.Text, out reforco);
            bool campoValido5 = decimal.TryParse(sangriaTotalCaixa.Text, out sangria);
            bool campoValido6 = decimal.TryParse(dinheiroFinal.Text, out final);

            campoValido = campoValido1 && campoValido2 && campoValido3 && campoValido4 && campoValido5 && campoValido6;
            // Se tem todoas entradas núméricas, faz conta se o fechamento bate
            if (campoValido)
            {
                dinheiroEsperado = inicial + total - cartao + reforco - sangria;
                diferencaEncontrada = final - dinheiroEsperado;
                diferencaFechamento.Text = diferencaEncontrada.ToString("N");
                if (diferencaEncontrada != 0)
                {
                    // a cor será sempre vermelha quando ouver diferença mesmo que decida prosseguir com os valores
                    diferencaFechamento.ForeColor = Color.Red;

                    // com diferença então não bateu, está sobrando ou faltando dinheiro em espécie ou informaram algum valor errado.
                    DialogResult result;
                    result = MessageBox.Show("Existe uma diferença no fechamento! Deseja prosseguir? Selecione: \n\r [ NÃO ] para conferir e alterar os valores informados \n\r [ SIM ] para prosseguir com os valores informados.", "Validando Fechamento do Caixa", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (result == DialogResult.No)
                    {
                        campoValido = false;
                    }
                } else
                {
                    diferencaFechamento.ForeColor = Color.Teal;
                }
            }
            else
            {
                diferencaFechamento.Text = "#erro";
            }
            return campoValido;
        }

        private void SairFecharAplicativo_Click(object sender, EventArgs e)
        {
            // no evento FormClosing vai validar se tem caixa aberto e se quer prosseguir ou não com o encerramento do programa
            Application.Exit();
        }

        // testa se tem caixa aberto antes de sair do programa e verifica se operador quer sair mesmo assim
        private bool ValidaSaidaAplicativo()
        {
            // Verifica se já existe um Caixa Aberto e alerta
            if (caixaStatus.Text == "Caixa Aberto")
            {
                DialogResult result;
                // como já existe um caixa aberto, pergunta se deseja prosseguir com a abertura de um novo
                result = MessageBox.Show("Existe um caixa aberto e um envelope iniciado que não foi registrado. Deseja encerrar o programa mesmo assim? \n \n"  +
                    "Selecione CANCELAR para continuar no sistema e completar o registro do envelope para o caixa aberto ou selecione OK para sair do sistema.", 
                    "Atenção, Envelope Não Registrado!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Cancel)
                {
                    return false;
                }
            }
            return true;
        }

        private void faturamentoTotalVendas_Enter(object sender, EventArgs e)
        {
            MensagemOrientacao(faturamentoTotalVendas);

        }

        private void faturamentoTotalVendas_Leave(object sender, EventArgs e)
        {
            // Validar campo
            ValidaCampo(faturamentoTotalVendas);
        }

        private void EnveloperForm_Load(object sender, EventArgs e)
        {
            try
            {
                // login do usuário: inicialmente igual datacaixa: pdvcascavel pdvcascavel
                //ConexaoDB meuDB = new ConexaoDB();
                // 
                //meuDB.ConectarDB();
                //meuDB.DeconectarDB
            }
            catch (Exception eErro)
            {
                MessageBox.Show($"Erro ao conectar banco de dados: {eErro.Message.ToString()}", "Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

        }

        private void IndicarResponsavel_Click(object sender, EventArgs e)
        {
            ApresentarPainelBloqueio (true);

            ResponsavelForm frm = new ResponsavelForm();
            frm.ShowDialog();
            // apresenta lista de operadores responsáveis selecionados
            string todosResponsaveis="";
            foreach (var i in SuporteUI.OperadorResponsaveis)
            {
                todosResponsaveis += i.UsuarioNome + Environment.NewLine; //  adiciona nova linha na string, também daria para usar "\r\n" 
            }
            responsaveisCaixa.Text = todosResponsaveis;
            ApresentarPainelBloqueio (false);
        }

        private void ApresentarPainelBloqueio (bool Visivel)
        {
            if (Visivel == true)
            {
                // apresentar painel bloqueio
                painelBloqueio.Location = new Point(148, 0);
                painelBloqueio.BringToFront();
                painelBloqueio.Visible = true;
            }
            else
            {
                // ocultar painel
                painelBloqueio.Visible = false;
            }
        }

        private void EnveloperForm_Shown(object sender, EventArgs e)
        {
            SolicitarLoginUsuario();
        }

        private void SolicitarLoginUsuario()
        {
            SuporteUI.OperadorAtivoNome = "Usuário";
            SuporteUI.OperadorAtivoTipoAcesso = "Acesso";
            operadorNome.Text = SuporteUI.OperadorAtivoNome;
            operadorTipoAcesso.Text = SuporteUI.OperadorAtivoTipoAcesso;

            ApresentarPainelBloqueio(true);
            AcessoForm acesso = new AcessoForm();
            if (acesso.ShowDialog() == DialogResult.Cancel)
            {
                Application.Exit();
            }
            ApresentarPainelBloqueio(false);

            operadorNome.Text = SuporteUI.OperadorAtivoNome;
            operadorTipoAcesso.Text = SuporteUI.OperadorAtivoTipoAcesso;
            if (operadorTipoAcesso.Text == "Administrador")
            {
                ConfigurarSistema.Visible = true;
                envelopeID.ReadOnly = false; // permite editar e assim habilita busca por código ID do envelope
            }
            else
            {
                ConfigurarSistema.Visible = false;
                envelopeID.ReadOnly = true; // bloqueio edição e busca para quem não é administração 
            }
        }

        private void configurarSistema_Click(object sender, EventArgs e)
        {

            ApresentarPainelBloqueio(true);

            ConfiguracaoForm frm = new ConfiguracaoForm();
            frm.ShowDialog();

            ApresentarPainelBloqueio(false);

        }

        private void dinheiroInicial_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void ValidarTeclaEnterTab(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
                //suppresses the Enter Key
                e.SuppressKeyPress = true;
            }
        }

        private void dinheiroInicial_KeyDown(object sender, KeyEventArgs e)
        {
            ValidarTeclaEnterTab(e);
            
        }

        private void dinheiroFinal_KeyDown(object sender, KeyEventArgs e)
        {
            ValidarTeclaEnterTab(e);
        }

        private void vendasCartao_KeyDown(object sender, KeyEventArgs e)
        {
            ValidarTeclaEnterTab(e);
        }

        private void sangriaTotalCaixa_KeyDown(object sender, KeyEventArgs e)
        {
            ValidarTeclaEnterTab(e);
        }

        private void reforcoTotalCaixa_KeyDown(object sender, KeyEventArgs e)
        {
            ValidarTeclaEnterTab(e);
        }

        private void faturamentoTotalVendas_KeyDown(object sender, KeyEventArgs e)
        {
            ValidarTeclaEnterTab(e);
        }

        private void dinheiroEnvelope_KeyDown(object sender, KeyEventArgs e)
        {
            ValidarTeclaEnterTab(e);
        }

        private void campoObservacao_KeyDown(object sender, KeyEventArgs e)
        {
            ValidarTeclaEnterTab(e);
        }

        private void temperaturaAtual_KeyDown(object sender, KeyEventArgs e)
        {
            ValidarTeclaEnterTab(e);
        }

        private void dinheiroPassagemCaixa_KeyDown(object sender, KeyEventArgs e)
        {
            ValidarTeclaEnterTab(e);
        }

        
        private void RegistrarEnvelope_Click(object sender, EventArgs e)
        {
            // testa se caixa aberto, se estiver fechado indica que deve ser aberto antes de registrar envelope
            if (caixaStatus.Text == "Caixa Fechado")
            {
                MessageBox.Show("Para registrar um envelope é necessário inicialmente registrar a abertura do caixa.", "Registrando envelope ...",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                dinheiroInicial.Focus();
            } else
            {

                if (ValidaPreenchimentoEnvelope() == true)
                {
                    // se validou corretamente o preenchimento dos campos do envelope, então registra no BD
                    EnvelopeModel envelope = new EnvelopeModel();
                    envelope.Id = int.Parse(envelopeID.Text.Substring(1));
                    envelope.DinheiroFinal = double.Parse(dinheiroFinal.Text) ;
                    envelope.VendasCartao = double.Parse(vendasCartao.Text);
                    envelope.SangriaTotalCaixa = double.Parse(sangriaTotalCaixa.Text);
                    envelope.ReforcoTotalCaixa = double.Parse(reforcoTotalCaixa.Text);
                    envelope.Faturamento = double.Parse(faturamentoTotalVendas.Text);
                    envelope.DiferencaFechamento = double.Parse(diferencaFechamento.Text);
                    envelope.EnvelopeDinheiro = double.Parse(dinheiroEnvelope.Text);
                    envelope.PassagemCaixaDinheiro = double.Parse(dinheiroPassagemCaixa.Text);
                    envelope.Observacao = campoObservacao.Text;
                    envelope.TemperaturaDia = temperaturaAtual.Text;
                    envelope.Clima = VerificaClimaSelecionado();
                    envelope.Turno = VerificaTurnoSelecionado();
                    envelope.EnvelopeConferido = false;
                    envelope.DinheiroInicialDiferenca = 0.00;
                    envelope.AtencaoFlagVerificar = false; // TODO - flag atencao, definir quando aciona
                    envelope.AtencaoDescricao = "";  // TODO - descrição flag atencao, texto quando aciona
                    envelope.DataFechamentoCaixa = DateTime.Now.ToString("dd/MM/yyyy");
                    envelope.HoraFechamentoCaixa = DateTime.Now.ToString("HH:mm:ss");

                    Cursor.Current = Cursors.WaitCursor;
                    try
                    {
                        if (envelope.RegistrarFechamentoCaixa(envelope) == true)
                        {
                            //  TODO - Responsáveis: após registrar o envelope, registrar os responsáveis pelo caixa na tabela
                            MessageBox.Show($"MOVIMENTO DE CAIXA REGISTRADO. \n\n ESCREVA O IDENTIFICADOR NO ENVELOPE: {envelopeID.Text} \n\n PRESSIONE OK PARA IMPRIMIR O RECIBO DE FECHAMENTO. ", $"Registrando Envelope {envelopeID.Text}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ImprimirReciboFechamentoEnvelope(envelope.Id);
                            GerarRelatoriosFaturamento();
                            ResetarFormularioEnvelope();
                            SolicitarLoginUsuario();
                        }
                        else
                        {
                            // Falha o registrar envelope no bd, verificar log para identificar exceção
                            MessageBox.Show("Falha o registrar envelope no banco de dados. Tente novamente...", "Registrando Envelope", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        Cursor.Current = Cursors.Default;
                    }
                    catch (Exception err)
                    {
                        // Falha o registrar envelope no bd, verificar log para identificar exceção
                        MessageBox.Show("Falha o registrar envelope no banco de dados. Tente novamente. Exceção: " + err.Message, "Registrando Envelope", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Não foi possível validar os valores para registrar o envelope. Verifique o preenchimento dos campos e os valores informados.", "Registrando Envelope", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

        }

        
        // limpa os campos da tela, reset para valores inciais
        private void ResetarFormularioEnvelope()
        {
            envelopeID.Text = "#0";
            caixaStatus.Text = "Caixa Fechado";
            caixaDinheiroRegistradoAbertura.Text = "R$ 0,00";
            caixaDataHora.Text = "Data - Hora";
            responsaveisCaixa.Text = "";
            dinheiroInicial.Text = "0,00";
            dinheiroFinal.Text = "0,00";
            vendasCartao.Text = "0,00";
            sangriaTotalCaixa.Text = "0,00";
            reforcoTotalCaixa.Text = "0,00";
            faturamentoTotalVendas.Text = "0,00";
            diferencaFechamento.Text = "0,00";
            dinheiroEnvelope.Text = "0,00";
            dinheiroPassagemCaixa.Text = "0,00";
            campoObservacao.Text = "";
            temperaturaAtual.Text = "";
            climaLimpo.Checked = climaNublado.Checked = climaChuvoso.Checked = false;
            turnoNoite.Checked = turnoTarde.Checked = turnoUnico.Checked = false;
            rodapeMensagem.Text = "Inicie um novo Envelope registrando abertura do caixa!";
            dinheiroInicial.SelectAll();
            dinheiroInicial.Focus();
            envelopeID.ReadOnly = false; //com caixa fechado permite editar o campo para realizar busca no BD por ID 

        }
        private void configurarSistema_Enter(object sender, EventArgs e)
        {
            LimparTextoRodape();
        }

        private void LimparTextoRodape()
        {
            rodapeMensagem.Text = "";
        }

        private void IndicarResponsavel_Enter(object sender, EventArgs e)
        {
            LimparTextoRodape();
        }

        private void RegistrarEnvelope_Enter(object sender, EventArgs e)
        {
            LimparTextoRodape();
        }

        private void SairFecharAplicativo_Enter(object sender, EventArgs e)
        {
            LimparTextoRodape();
        }

        private void painelClima_Enter(object sender, EventArgs e)
        {
            rodapeMensagem.Text = "Selecione como esteve o clima na maior parte do turno.";
        }

        private void painelTurno_Enter(object sender, EventArgs e)
        {
            rodapeMensagem.Text = "Selecione o turno deste caixa.";
        }

        private void conferirTrocoInicial_Enter(object sender, EventArgs e)
        {
            LimparTextoRodape();
        }

        // Testa se bateu troco inicial e então registra abertura do caixa no Envelope
        private void RegistrarAberturaCaixa_Click(object sender, EventArgs e)
        {
            double dinheiroInicialDiferenca;
            bool prosseguir = true;
            DialogResult result;

            // Verifica se já existe um Caixa Aberto e alerta
            if (caixaStatus.Text == "Caixa Aberto")
            {
                // como já existe um caixa aberto, pergunta se deseja prosseguir com a abertura de um novo
                result = MessageBox.Show($"Existe um caixa aberto com o valor de {caixaDinheiroRegistradoAbertura.Text} e um envelope iniciado. Deseja prosseguir com a abertura de um novo caixa? \n \n  " +
                    "Selecione CANCELAR para continuar com o caixa atual ou selecione OK para abrir um novo caixa.", "Atenção, Caixa Aberto!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Cancel)
                {
                    return;
                }
            }
            // Verifica se o campo DinheiroInicial tem valor válido ou se está com flag de erro
            // se já setou flag de erro então a string não será vazia 
            if (erroEnveloperForm.GetError(dinheiroInicial) != "")
            {
                // não é numérico, pois está com flag de erro, então não pode prosseguir com a abertura
                MessageBox.Show("Informe um valor válido no campo Dinheiro Inicial para prosseguir com o registro da abertura de caixa.", 
                    "Abertura de Caixa", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // confere o troco inicial se bate com o dinheiro da passagem
            dinheiroInicialDiferenca = DiferencaTrocoInicial();

            // se encontrou diferença entre valor inicial e valor repassado informa o operador e pergunta se deseja prosseguir
            if (dinheiroInicialDiferenca != 0)
            {
                // como não confere, sugere nova contagem do dinheiro encontrado e pergunta se deseja prosseguir com o valor informado
                result = MessageBox.Show("Verique a contagem do Dinheiro Inicial encontrado no caixa. " +
                    "Selecione CANCELAR para informar um valor diferente ou OK para prosseguir com o valor já informado.", "Troco Inicial", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Cancel)
                {
                    prosseguir = false;
                }
            }
            
            // prosseguindo para o registro da abertura do caixa no envelope
            if (prosseguir) 
            {
                // registrar abertura do caixa no envelope (iniciar novo envelope)
                EnvelopeModel envelope = new EnvelopeModel();
                if (double.TryParse(dinheiroInicial.Text, out double inicial) )
                {
                    envelope.DinheiroInicial = inicial;
                    envelope.DinheiroInicialDiferenca = dinheiroInicialDiferenca;
                    envelope.DataAberturaCaixa = DateTime.Now.ToString("dd/MM/yyyy");
                    envelope.HoraAberturaCaixa = DateTime.Now.ToString("HH:mm:ss");
                    envelope.Observacao = campoObservacao.Text;
                    envelope.Operador = operadorNome.Text;
                    envelope.Pdv = caixaPDV.Text;
                }
                // cria um novo registro no banco de dados para o caixa atual
                // retorna um id para o envelope
                Cursor.Current = Cursors.WaitCursor;
                
                try
                {
                    envelopeID.Text = envelope.RegistrarAberturaCaixa(envelope);
                }
                catch (Exception err)
                {
                    MessageBox.Show("Não foi possível registrar abertura do caixa. Tente novamente! Excessão: " + err.Message, "Registrando abertura do caixa", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (envelopeID.Text != "")
                {
                    caixaDataHora.Text = envelope.DataAberturaCaixa + " - " + envelope.HoraAberturaCaixa;
                    responsaveisCaixa.Text = operadorNome.Text;
                    caixaStatus.Text = "Caixa Aberto";
                    envelopeID.ReadOnly = true; //com caixa aberto bloqueio edição no campo e assim não permite alteração ou busca no BD por ID 
                    caixaDinheiroRegistradoAbertura.Text = "R$ " + envelope.DinheiroInicial.ToString("N"); //"#,##0.00"
                } else
                {
                    MessageBox.Show("Não foi possível registrar abertura do caixa. Tente novamente!", "Registrando abertura do caixa", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); 
                }
                Cursor.Current = Cursors.Default;
            }
        }

        // Atualiza o valor de abertura de um caixa já aberto
        private bool AtualizarValorAberturaCaixa()
        {
            double dinheiroInicialDiferenca;
            bool prosseguir = true;
            DialogResult result;
            double dinheiroInicialRegistrado = double.Parse(caixaDinheiroRegistradoAbertura.Text.Substring(3)); //retira "R$" e converte para double

            // confere o troco inicial se bate com o dinheiro da passagem
            dinheiroInicialDiferenca = DiferencaTrocoInicial();

            // se encontrou diferença entre valor inicial e valor repassado informa o operador e pergunta se deseja prosseguir
            if (dinheiroInicialDiferenca != 0)
            {
                // como não confere, sugere nova contagem do dinheiro encontrado e pergunta se deseja prosseguir com o valor informado
                result = MessageBox.Show("Verique a contagem do Dinheiro Inicial encontrado no caixa. " +
                    "Selecione CANCELAR para informar um valor diferente ou OK para prosseguir com o valor já informado.", "Troco Inicial", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Cancel)
                {
                    // selecionou CANCEL então irá informar um novo valor, a função então retorna o valor do campo DinheiroInicial para o valor de abertura anterior
                    dinheiroInicial.Text = dinheiroInicialRegistrado.ToString("N");
                    prosseguir = false;
                }
            }
            // prosseguindo para o update do valor inicial do caixa aberto
            if (prosseguir)
            {
                // registrar abertura do caixa no envelope (iniciar novo envelope)
                EnvelopeModel envelope = new EnvelopeModel();
                if (double.TryParse(dinheiroInicial.Text, out double inicial))
                {
                    envelope.Id = int.Parse(envelopeID.Text.Substring(1));
                    envelope.DinheiroInicial = inicial;
                    envelope.DinheiroInicialDiferenca = dinheiroInicialDiferenca;
                    envelope.DataAberturaCaixa = DateTime.Now.ToString("dd/MM/yyyy");
                    envelope.HoraAberturaCaixa = DateTime.Now.ToString("HH:mm:ss");
                    envelope.Observacao = campoObservacao.Text;
                    envelope.Operador = operadorNome.Text;
                    envelope.Pdv = caixaPDV.Text;
                }
                // atualiza o valor do dinheiro inicial no banco de dados 
                // para o caixa atual
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    if (envelope.AtualizarAberturaCaixa(envelope) == true)
                    {
                        // indica na tela o novo valor registrado de abertura 
                        caixaDinheiroRegistradoAbertura.Text = "R$ " + envelope.DinheiroInicial.ToString("N"); //"#,##0.00"
                    }
                    else
                    {
                        // Falha ao atualizar no BD, arquivo LOG terá registro da exceção
                        MessageBox.Show("Não foi possível atualizar valor do dinheiro inicial no banco de dados! Tente novamente ...", "Atualizando Abertura Envelope", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                } catch (Exception err)
                {
                    MessageBox.Show("Falha ao atualizar banco de dados! Tente novamente! Exceção: " + err.Message, "Atualizando Abertura Envelope", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Cursor.Current = Cursors.Default;
            }
            return prosseguir;
        }

        private void EnveloperForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // testa se tem caixa aberto antes de sair do programa
            if ( ValidaSaidaAplicativo() == false)
            {   
                // se tem caixa aberto e operador optou por não sair, cancela o FormClosing
                e.Cancel = true;
            }
            
            // faz uma cópia do arquivo SQLite 
            BackUpSQLite bkUp = new BackUpSQLite();
            bkUp.BackUp();

        }

        private string ClimaDescricao(int idClima)
        {
            switch (idClima)
            {
                case 1: return "Limpo";
                    break;
                case 2: return "Nublado";
                    break;
                case 3: return "Chuvoso";
                    break;
                default: return idClima.ToString();
            }
        }

        private string TurnoDescricao(int idTurno)
        {
            switch (idTurno)
            {
                case 1:
                    return "Manhã";
                    break;
                case 2:
                    return "Tarde";
                    break;
                case 3:
                    return "Único";
                    break;
                default: return idTurno.ToString();
            }
        }

        private int VerificaClimaSelecionado()
        {
            foreach (Control controle in painelClima.Controls)
            {
                if ( controle.GetType() == typeof(RadioButton) )
                {
                    RadioButton rb = controle as RadioButton;
                    if (rb.Checked) 
                        return int.Parse(controle.Tag.ToString());
                }
            }
            return 0; //se nenhum estiver selecionado, retorna zero
        }

        private int VerificaTurnoSelecionado()
        {
            foreach (Control controle in painelTurno.Controls)
            {
                if (controle.GetType() == typeof(RadioButton))
                {
                    RadioButton rb = controle as RadioButton;
                    if (rb.Checked)
                        return int.Parse(controle.Tag.ToString());
                }
            }
            return 0; //se nenhum estiver selecionado, retorna zero
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            LimparErroClima();
        }

        private void climaLimpo_CheckedChanged(object sender, EventArgs e)
        {
            LimparErroClima();
        }

        private void climaChuvoso_CheckedChanged(object sender, EventArgs e)
        {
            LimparErroClima();
        }

        private void LimparErroClima()
        {
            erroEnveloperForm.SetError(indiqueClima, "");
        }

        private void LimparErroTurno()
        {
            erroEnveloperForm.SetError(indiqueTurno, "");
        }

        private void turnoTarde_CheckedChanged(object sender, EventArgs e)
        {
            LimparErroTurno();
        }

        private void turnoNoite_CheckedChanged(object sender, EventArgs e)
        {
            LimparErroTurno();
        }

        private void turnoUnico_CheckedChanged(object sender, EventArgs e)
        {
            LimparErroTurno();
        }

        private void envelopeID_KeyDown(object sender, KeyEventArgs e)
        {
            if ( e.KeyCode == Keys.Enter )
            {
                string strID= envelopeID.Text;
                if (strID.Contains("#"))
                {
                    strID = envelopeID.Text.Substring(1);
                } 
                if (int.TryParse(strID, out int id))
                {
                    Cursor.Current = Cursors.WaitCursor;
                    LimparApresentacaoEnvelope();
                    ApresentarPainelBloqueio(true);
                    // buscar envelope por ID no bd
                    EnvelopeModel envelope = new EnvelopeModel();
                    
                    try
                    {
                        envelope = envelope.BuscarEnvelope(id);
                    } catch (Exception err)
                    {
                        MessageBox.Show("Não foi possível recuperar todos os dados do envelope. Exceção: " + err.Message , "Consulta Envelope", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    
                    // apresenta envelope no form
                    envelopeApresentacaoCompleta.Items.Add("Envelope ID #" + envelope.Id);
                    envelopeApresentacaoCompleta.Items.Add("Abertura: " + envelope.DataAberturaCaixa + " - " + envelope.HoraAberturaCaixa);
                    envelopeApresentacaoCompleta.Items.Add("Fechamento: " + envelope.DataFechamentoCaixa + " - " + envelope.HoraFechamentoCaixa);
                    envelopeApresentacaoCompleta.Items.Add("Dinheiro Inicial: R$" + envelope.DinheiroInicial);
                    envelopeApresentacaoCompleta.Items.Add("Dinheiro Inicial Diferença: R$" + envelope.DinheiroInicialDiferenca);
                    envelopeApresentacaoCompleta.Items.Add("Dinheiro Final: R$" + envelope.DinheiroFinal);
                    envelopeApresentacaoCompleta.Items.Add("Vendas Cartão: R$" + envelope.VendasCartao);
                    envelopeApresentacaoCompleta.Items.Add("Sangria Total: R$" + envelope.SangriaTotalCaixa);
                    envelopeApresentacaoCompleta.Items.Add("Reforço Total: R$" + envelope.ReforcoTotalCaixa);
                    envelopeApresentacaoCompleta.Items.Add("Faturamento: R$" + envelope.Faturamento);
                    envelopeApresentacaoCompleta.Items.Add("Diferença Fechamento: R$" + envelope.DiferencaFechamento);
                    envelopeApresentacaoCompleta.Items.Add("Passagem Caixa: R$" + envelope.PassagemCaixaDinheiro);
                    envelopeApresentacaoCompleta.Items.Add("Dinheiro Envelope: R$" + envelope.EnvelopeDinheiro);
                    envelopeApresentacaoCompleta.Items.Add("Envelope Conferido: " + envelope.EnvelopeConferido);
                    envelopeApresentacaoCompleta.Items.Add("Diferença Dinheiro Envelope: R$" + envelope.EnvelopeDinheiroDiferenca);
                    envelopeApresentacaoCompleta.Items.Add("Operador: " + envelope.Operador);
                    envelopeApresentacaoCompleta.Items.Add("PDV: " + envelope.Pdv);
                    envelopeApresentacaoCompleta.Items.Add($"Temperatura: {envelope.TemperaturaDia} ˚C");
                    envelopeApresentacaoCompleta.Items.Add("Clima: " + ClimaDescricao(envelope.Clima));
                    envelopeApresentacaoCompleta.Items.Add("Turno: " + TurnoDescricao(envelope.Turno));
                    envelopeApresentacaoCompleta.Items.Add("Flag Atenção: " + envelope.AtencaoFlagVerificar);
                    envelopeApresentacaoCompleta.Items.Add("Flag Atenção Descrição: " + envelope.AtencaoDescricao);
                    envelopeApresentacaoCompletaObservacao.Text = "OBS: " + envelope.Observacao;
                    
                    e.SuppressKeyPress = true;
                    Cursor.Current = Cursors.Default;
                } else
                {
                    MessageBox.Show("Informe um ID numérico para consultar no banco de dados os dados do envelope.", "Consulta Envelope", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    envelopeID.SelectAll();
                    envelopeID.Focus();
                }
            }
        }

        // Imprime na Bematech recibo de fechamento para o envelope
        private void ImprimirReciboFechamentoEnvelope(int id)
        {
            string recibo = "";
            // buscar envelope por ID no bd
            EnvelopeModel envelope = new EnvelopeModel();

            try
            {
                envelope = envelope.BuscarEnvelope(id);
            }
            catch (Exception err)
            {
                MessageBox.Show("Não foi possível recuperar todos os dados do envelope. Exceção: " + err.Message, "Consulta Envelope", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // conecta impressora
            var impressora = new Printer4200th();
            impressora.Conectar();
            // monta recibo
            recibo += "Envelope ID #" + envelope.Id + "\r \n";
            recibo += "Abertura: " + envelope.DataAberturaCaixa + " - " + envelope.HoraAberturaCaixa + "\r \n";
            recibo += "Fechamento: " + envelope.DataFechamentoCaixa + " - " + envelope.HoraFechamentoCaixa + "\r \n";
            recibo += "Dinheiro Inicial: R$" + envelope.DinheiroInicial + "\r \n";
            recibo += "Dinheiro Inicial Diferença: R$" + envelope.DinheiroInicialDiferenca + "\r \n";
            recibo += "Dinheiro Final: R$" + envelope.DinheiroFinal + "\r \n";
            recibo += "Vendas Cartão: R$" + envelope.VendasCartao + "\r \n";
            recibo += "Sangria Total: R$" + envelope.SangriaTotalCaixa + "\r \n";
            recibo += "Reforço Total: R$" + envelope.ReforcoTotalCaixa + "\r \n";
            recibo += "Faturamento: R$" + envelope.Faturamento + "\r \n";
            recibo += "Diferença Fechamento: R$" + envelope.DiferencaFechamento + "\r \n";
            recibo += "Passagem Caixa: R$" + envelope.PassagemCaixaDinheiro + "\r \n";
            recibo += "Dinheiro Envelope: R$" + envelope.EnvelopeDinheiro + "\r \n";
            recibo += "Envelope Conferido: " + envelope.EnvelopeConferido + "\r \n";
            recibo += "Diferença Dinheiro Envelope: R$" + envelope.EnvelopeDinheiroDiferenca + "\r \n";
            recibo += "Operador: " + envelope.Operador + "\r \n";
            recibo += "PDV: " + envelope.Pdv + "\r \n";
            recibo += $"Temperatura: {envelope.TemperaturaDia} ˚C" + "\r \n";
            recibo += "Clima: " + ClimaDescricao(envelope.Clima) + "\r \n";
            recibo += "Turno: " + TurnoDescricao(envelope.Turno) + "\r \n";
            recibo += "Flag Atenção: " + envelope.AtencaoFlagVerificar + "\r \n";
            recibo += "Flag Atenção Descrição: " + envelope.AtencaoDescricao + "\r \n";
            recibo += "OBS: " + envelope.Observacao + "\r \n";
            // envia rebico para impressora
            impressora.Imprimir(recibo, "");
            // corta papel
            impressora.CortarPapel();
            // fecha conexão com ipmressora
            impressora.Desconectar();
            Cursor.Current = Cursors.Default;
            
        }

        private void FecharApresentacaoEnvelope_Click(object sender, EventArgs e)
        {
            ApresentarPainelBloqueio(false);
            LimparApresentacaoEnvelope();
        }
        private void LimparApresentacaoEnvelope()
        {
            envelopeApresentacaoCompleta.Items.Clear();
            envelopeApresentacaoCompletaObservacao.Text = "";
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // modelo da impressora
            var impressora = new Printer4200th();
            impressora.Modelo = 7;            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // modelo da impressora
            var impressora = new Printer4200th();
            impressora.Porta = "USB";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // modelo da impressora
            var impressora = new Printer4200th();
            bool retorno = impressora.Conectar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // modelo da impressora
            var impressora = new Printer4200th();
            bool retorno = impressora.Desconectar();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // modelo da impressora
            var impressora = new Printer4200th();
            bool retorno = impressora.Imprimir(campoObservacao.Text, "");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // modelo da impressora
            var impressora = new Printer4200th();
            bool retorno = impressora.CortarPapel();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // modelo da impressora
            var impressora = new Printer4200th();
            bool retorno = impressora.AvancarPapel(5);
        }

        private string RelatorioFaturamentoDiario()
        {
            string report;
            var vendas = new SalesDaily();
            report = vendas.VendasHoje();
            vendas = vendas.VendasDiarias();
            report += $"\n \r [ FATURAMENTO MÊS ATUAL ATÉ: {DateTime.Today.ToShortDateString()} ] \n";
            report += $"Dias: {vendas.Dias.ToString()} \n ";
            report += $"Turnos: {vendas.Turnos.ToString()} \n ";
            report += $"Total Mês: {vendas.Total.ToString("F")} \n ";
            report += $"Média Dia: {vendas.MediaDia.ToString("F")} \n ";
            report += $"Média Turno: {vendas.MediaTurno.ToString("F")} \n ";
            report += $"Mínimo Turno: {vendas.MinimoTurno.ToString("F")} \n ";
            report += $"Máximo Turno: {vendas.MaximoTurno.ToString("F")} ";
            return report;
        }

        private string RelatorioFaturamentoQuartenal()
        {
            string report;
            var vendas = new SalesQuarterly();
            report = vendas.Vendas();
            return report;
        }

        private string RelatorioFaturamentoMensalAnual()
        {
            string report;
            var vendas = new SalesMonthly();
            report = vendas.VendasAnualPorMes(DateTime.Today.Year);
            return report;
        }

        private string RelatorioFaturamentoMensalMesAnterior()
        {
            string report;

            // mes e ano atual
            int _mes = DateTime.Today.Month;
            int _ano = DateTime.Today.Year;

            // se mes atual for janeiro então deve ajustar para dezembro do ano anterior
            if (_mes == 1)
            {
                _mes = 12;
                _ano -= 1;
            } else
            {
                _mes -= 1;
            }

            var vendas = new SalesMonthly();
            report = vendas.VendasPorDia(_mes, _ano);
            
            return report;
        }

        private void EmailRelatorio(string relatorio)
        {
            string destino = "cidzanella@hotmail.com";
            string titulo = "Enveloper " + DateTime.Now.ToShortDateString();
            var correio = new Emailer();
            correio.Enviar(destino, titulo, relatorio);
        }

        // Envia o banco de dados do backup por email
        // copia do diretório de bkup para evitar restrição de acesso ao arquivo quando banco está em uso, 
        // como envia o banco no dia 01 para usar dados do mês anterior, está ok ser o banco do último backup
        private void EmailBancoDados()
        {
            // dados para email
            string destino = "cidzanella@hotmail.com";
            string titulo = "Enveloper DataBase";
            string corpo = "Em anexo banco de dados SQLite EnveloperDB.db - " + DateTime.Now;
            string anexo = @"C:\EnveloperBkUp\EnveloperDB.db";

            var correio = new Emailer();
            correio.Enviar(destino, titulo, corpo, anexo);
        }

        private void TelegramRelatorio(string relatorio)
        {
            // implementar envio por telegram
        }

        private void btnRelatorio_Click(object sender, EventArgs e)
        {
            GerarRelatoriosFaturamento();
        }

        private void GerarRelatoriosFaturamento()
        {
            string report;
            Cursor.Current = Cursors.WaitCursor;
                
            // relatório das vendas do mês atual por dia
            report = RelatorioFaturamentoDiario();
            
            // relatório das vendas do mês atual, por quarto
            report += "\n " + RelatorioFaturamentoQuartenal();
            
            // se primeiro dia do mês gera relatório mensal do mês anterior
            if (DateTime.Today.Day == 1)
            { 
                report += "\n " + RelatorioFaturamentoMensalMesAnterior();
                report += "\n " + RelatorioFaturamentoMensalAnual();
                // na troca do mês, além de enviar relatório envia também o banco de dados para facilitar conferência dos envelopes
                EmailBancoDados();
            }

            // envia relatórios por email
            EmailRelatorio(report);
            //TelegramRelatorio(report);

            Cursor.Current = Cursors.Default;

        }

        private void envelopeID_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
