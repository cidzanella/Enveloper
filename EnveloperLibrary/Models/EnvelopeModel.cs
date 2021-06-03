using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Globalization;
using System.Data;

namespace EnveloperLibrary.Models
{
    /// <summary>
    /// Representa um envelope de fechamento de caixa, contendo informações sobre abertura e 
    /// fechamento bem como movimento do dia, com vendas, sangrias e reforços, 
    /// além de dados de clima
    /// </summary>
    public class EnvelopeModel
    {
        private static readonly double _registroPassagemCaixaNaoEncontrado = -0.001; //valor a ser retornado quando não localizar registro de Passagem Caixa no BD

        public double RegistroPassagemCaixaNaoEncontrado 
        { 
            get
            {
                return _registroPassagemCaixaNaoEncontrado;
            }
        }
        //public static double RegistroPassagemCaixaNaoEncontrado => _registroPassagemCaixaNaoEncontrado;
        
        public int Id { get; set; }
        public string Rotulo { get; set; }
        public double DinheiroInicial { get; set; }
        public double DinheiroInicialDiferenca { get; set; }  // envelope_inicial_diferenca indica a diferença entre o dinheiro inicial encontrado no caixa e o dinheiro passagem caixa anterior, se for diferente de 0 então não bateu)
        public double DinheiroFinal { get; set; }
        public double VendasCartao { get; set; }
        public double SangriaTotalCaixa { get; set; }
        public double ReforcoTotalCaixa { get; set;}
        public double Faturamento { get; set; }
        public double DiferencaFechamento { get; set; } //indica que o fechamento do caixa não bateu, sobrou ou faltou dinheiro
        public double EnvelopeDinheiro { get; set; }
        public double PassagemCaixaDinheiro { get; set; }
        public bool   EnvelopeConferido { get; set; } //indica se o envelope foi conferido pela ADM
        public double EnvelopeDinheiroDiferenca { get; set; } //indica a diferença entre o valor esperado e o valor encontrado no envelope
        public string DataAberturaCaixa { get; set; }
        public string HoraAberturaCaixa { get; set; }
        public string DataFechamentoCaixa { get; set; }
        public string HoraFechamentoCaixa { get; set; }
        public string TemperaturaDia { get; set; }
        public string Observacao  { get; set; }
        public string Operador { get; set; }
        public string Pdv { get; set; } 
        public int Clima { get; set; } 
        public int Turno { get; set; }
        public bool AtencaoFlagVerificar { get; set; } // indica que algo não bateu mas operador prosseguiu assim mesmo
        public string AtencaoDescricao { get; set; } // dica do que não bateu no fechamento e que foi sinalizado para o operador, mas este prosseguiu
        
        /// <summary>
        /// Busca no BD e retorna o Dinheiro Passagem do último registro completo de envelope, sendo completo siginifca que o envelope foi o último fechamento de caixa
        /// </summary>
        /// <returns> Retorna -0.001 caso não encontre registro no BD referente a último fechamento de caixa </returns>
        public double BuscarDinheiroPassagemCaixaUltimoFechamento()
        {
            // Busca no BD o dinheiro de passagem deixado no último fechamento de caixa para verificar se bate com o dinheiro incial encontrado no caixa
            double dinheiroPassagem = RegistroPassagemCaixaNaoEncontrado; //retorna -0.001 caso não encontre registro no BD referente a último fechamento de caixa 

            try
            {
                // conexão SQL
                ConectaSQLite conSQL = new ConectaSQLite();
                conSQL.Conectar();
                // seleciona o último registro da tabela envelope que tem dado no campo passagem do caixa (significa que foi envelop de caixa fechado e não apenas aberto)
                string query = "SELECT passagem_caixa_dinheiro FROM envelope WHERE passagem_caixa_dinheiro NOTNULL ORDER BY id DESC LIMIT 1";
                // string query = "SELECT * FROM envelope";
                // consulta com Command
                SQLiteCommand comando = new SQLiteCommand(query, conSQL.conDB);
                SQLiteDataReader dados = comando.ExecuteReader();

                if (dados.Read())
                {
                    double.TryParse(dados["passagem_caixa_dinheiro"].ToString(), out dinheiroPassagem);
                }

                // fecha conexão SQL e retorna
                comando.Dispose();
                dados.Close();
                conSQL.Desconectar();
            }
            catch (Exception e)
            {
                LogRegister.Log("EnvelopeModel.BuscarDinheiroPassagemCaixaUltimoFechamento - " + e.Message);
                throw e;
            }

            return dinheiroPassagem;
        }

        // Busca no BD os dados completos do envelope pelo ID
        public EnvelopeModel BuscarEnvelope(int Id)
        {
            EnvelopeModel envelope = new EnvelopeModel();

            // conexão SQL
            ConectaSQLite conSQL = new ConectaSQLite();
            conSQL.Conectar();
            // seleciona o último registro da tabela envelope que tem dado no campo passagem do caixa (significa que foi envelop de caixa fechado e não apenas aberto)
            string query = $"SELECT * FROM envelope WHERE id={Id}";
            // consulta com Command
            SQLiteCommand comando = new SQLiteCommand(query, conSQL.conDB);
            SQLiteDataReader dados = comando.ExecuteReader();

            if (dados.Read())
            {
                //double.TryParse(dados["passagem_caixa_dinheiro"].ToString(), out envelope.PassagemCaixaDinheiro);

                // Poderia usar esta forma de acessar os dados do BD, enumerando os campos da tabela numa sequência
                // usuario.UsuarioSenha= dados.GetString(2);
                // usuario.UsuarioPDV = dados.GetInt32(3);

                // Esta é uma outra forma de acessar os dados, especificando o nome do campo da tabela
                try
                {
                    envelope.Id = int.Parse(dados["id"].ToString());
                    envelope.Rotulo = dados["envelope_rotulo"].ToString();
                    envelope.DinheiroInicial = double.Parse(dados["dinheiro_inicial"].ToString());
                    envelope.DinheiroInicialDiferenca = double.Parse(dados["dinheiro_inicial_diferenca"].ToString());
                    envelope.DinheiroFinal = double.Parse(dados["dinheiro_final"].ToString());
                    envelope.VendasCartao = double.Parse(dados["vendas_cartao"].ToString());
                    envelope.SangriaTotalCaixa = double.Parse(dados["sangria_total_caixa"].ToString());
                    envelope.ReforcoTotalCaixa = double.Parse(dados["reforco_total_caixa"].ToString());
                    envelope.Faturamento = double.Parse(dados["faturamento"].ToString());
                    envelope.DiferencaFechamento = double.Parse(dados["diferenca_fechamento"].ToString());
                    envelope.EnvelopeDinheiro = double.Parse(dados["envelope_dinheiro"].ToString());
                    envelope.PassagemCaixaDinheiro = double.Parse(dados["passagem_caixa_dinheiro"].ToString());
                    envelope.EnvelopeConferido = bool.Parse(dados["envelope_conferido"].ToString());
                    envelope.EnvelopeDinheiroDiferenca = double.Parse(dados["envelope_dinheiro_diferenca"].ToString());
                    envelope.DataAberturaCaixa = dados["data_abertura_caixa"].ToString();
                    envelope.HoraAberturaCaixa = dados["hora_abertura_caixa"].ToString();
                    envelope.DataFechamentoCaixa = dados["data_fechamento_caixa"].ToString();
                    envelope.HoraFechamentoCaixa = dados["hora_fechamento_caixa"].ToString();
                    envelope.TemperaturaDia = dados["temperatura_turno"].ToString();
                    envelope.Observacao = dados["observacao"].ToString();
                    envelope.Operador = dados["operador"].ToString();
                    envelope.Pdv = dados["pdv"].ToString();
                    envelope.Clima = int.Parse(dados["clima_id"].ToString());
                    envelope.Turno = int.Parse(dados["turno_id"].ToString());
                    envelope.AtencaoFlagVerificar = bool.Parse(dados["atencao_flag_verificar"].ToString());
                    envelope.AtencaoDescricao = dados["atencao_descricao"].ToString();
                }
                catch (Exception e)
                {
                    LogRegister.Log("EnvelopeModel.BuscarEnvelope - " + e.Message);
                    throw e;
                }
            }

            // fecha conexão SQL e retorna
            comando.Dispose();
            dados.Close();
            conSQL.Desconectar();
            return envelope;
        }

        // recebe um novo envelope referente a abertura do caixa e registra estes dados iniciais no BD, incluso diferença de troco na abertura
        // retorna Rotulo do Envelope com o ID do registro criado
        public string RegistrarAberturaCaixa(EnvelopeModel envelope) 
        {
            string rotuloEnvelope ="";
            string query;
            // insere novo registro envelope com dados de abertura do caixa (iniciar novo envelope)
            query = "INSERT INTO envelope (dinheiro_inicial, dinheiro_inicial_diferenca, data_abertura_caixa, hora_abertura_caixa, observacao, pdv, operador) ";
            query += string.Format(CultureInfo.InvariantCulture, " VALUES ({0:0.00},{1:0.00},'{2}','{3}','{4}','{5}','{6}')", 
                envelope.DinheiroInicial, envelope.DinheiroInicialDiferenca, envelope.DataAberturaCaixa, envelope.HoraAberturaCaixa,
                envelope.Observacao, envelope.Pdv, envelope.Operador);

            try
            {
                ConectaSQLite conSQL = new ConectaSQLite();
                conSQL.Conectar();
                SQLiteCommand comando = new SQLiteCommand(query, conSQL.conDB);
                int colunasAlteradas = comando.ExecuteNonQuery();
                if (colunasAlteradas == 1)
                {
                    comando.CommandText = "SELECT last_insert_rowid()";
                    envelope.Id = (Int32)(Int64)comando.ExecuteScalar();
                    rotuloEnvelope = $"#{envelope.Id}";
                }
                comando.Dispose();
                conSQL.Desconectar();
            }
            catch (Exception e)
            {
                LogRegister.Log("EnvelopeModel.RegistrarAberturaCaixa - " + e.Message);
                throw e;
            }
            // retorna um rótulo id para o envelope
            return rotuloEnvelope;
        }

        // recebe um envelope atualizado referente a abertura do caixa (quando quer alterar valor dinheiro inicial)
        // registra estes dados iniciais no BD, incluso diferença de troco na abertura
        public bool AtualizarAberturaCaixa(EnvelopeModel envelope)
        {
            string query;
            int colunasAlteradas = 0;

            // constrói string de consulta SQL com formatação dos valores usando . no lugar de ,
            query = "UPDATE envelope SET ";
            query += string.Format(CultureInfo.InvariantCulture, "dinheiro_inicial = {0:0.00}, ", envelope.DinheiroInicial); 
            query += string.Format(CultureInfo.InvariantCulture, "dinheiro_inicial_diferenca = {0:0.00}, ", envelope.DinheiroInicialDiferenca);
            query += $"data_abertura_caixa = '{envelope.DataAberturaCaixa}', " +
                    $"hora_abertura_caixa = '{envelope.HoraAberturaCaixa}', " +
                    $"observacao = '{envelope.Observacao}', " +
                    $"pdv = '{envelope.Pdv}', " +
                    $"operador = '{envelope.Operador}', " +
                    $"atencao_flag_verificar = {envelope.AtencaoFlagVerificar}, " +
                    $"atencao_descricao = '{envelope.AtencaoDescricao}' " +
                    $"WHERE id={envelope.Id}";

            try
            {
                ConectaSQLite conSQL = new ConectaSQLite();
                conSQL.Conectar();

                SQLiteCommand comando = new SQLiteCommand(query, conSQL.conDB);

                colunasAlteradas = comando.ExecuteNonQuery();
                comando.Dispose();
                conSQL.Desconectar();
            }
            catch (Exception e)
            {
                LogRegister.Log("EnvelopeModel.AtualizarAberturaCaixa - " + e.Message);
                throw e;
            }


            if (colunasAlteradas == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //
        // recebe um envelope de fechamento, preenchido, referente ao caixa aberto e registra estes dados finais no BD
        // retorna status de sucesso ou não
        public bool RegistrarFechamentoCaixa(EnvelopeModel envelope)
        {
            int colunasAlteradas=0;
            string query;

            query = $"UPDATE envelope SET envelope_rotulo = '{envelope.Rotulo}', ";
            query += string.Format(CultureInfo.InvariantCulture, "dinheiro_final = {0:0.00}, ", envelope.DinheiroFinal);
            query += string.Format(CultureInfo.InvariantCulture, "vendas_cartao  = {0:0.00}, ", envelope.VendasCartao);
            query += string.Format(CultureInfo.InvariantCulture, "sangria_total_caixa  = {0:0.00}, ", envelope.SangriaTotalCaixa);
            query += string.Format(CultureInfo.InvariantCulture, "reforco_total_caixa  = {0:0.00}, ", envelope.ReforcoTotalCaixa);
            query += string.Format(CultureInfo.InvariantCulture, "faturamento  = {0:0.00}, ", envelope.Faturamento);
            query += string.Format(CultureInfo.InvariantCulture, "diferenca_fechamento  = {0:0.00}, ", envelope.DiferencaFechamento);
            query += string.Format(CultureInfo.InvariantCulture, "passagem_caixa_dinheiro  = {0:0.00}, ", envelope.PassagemCaixaDinheiro);
            query += string.Format(CultureInfo.InvariantCulture, "envelope_dinheiro  = {0:0.00}, ", envelope.EnvelopeDinheiro);
            query += string.Format(CultureInfo.InvariantCulture, "envelope_conferido = {0}, ", envelope.EnvelopeConferido);
            query += string.Format(CultureInfo.InvariantCulture, "envelope_dinheiro_diferenca = {0:0.00}, ", envelope.EnvelopeDinheiroDiferenca);
            query += $"data_fechamento_caixa = '{envelope.DataFechamentoCaixa}', " +
                $"hora_fechamento_caixa = '{envelope.HoraFechamentoCaixa}', " +
                $"observacao = '{envelope.Observacao}', " +
                $"temperatura_turno = {envelope.TemperaturaDia}, " +
                $"clima_id = {envelope.Clima}, " +
                $"turno_id = {envelope.Turno}, " +
                $"atencao_flag_verificar = {envelope.AtencaoFlagVerificar}, " +
                $"atencao_descricao = '{envelope.AtencaoDescricao}' " +
                $"WHERE id={envelope.Id}";

            try
            {
                ConectaSQLite conSQL = new ConectaSQLite();
                conSQL.Conectar();

                SQLiteCommand comando = new SQLiteCommand(query, conSQL.conDB);

                colunasAlteradas = comando.ExecuteNonQuery();
                comando.Dispose();
                conSQL.Desconectar();
            }
            catch (Exception e )
            {
                LogRegister.Log("EnvelopeModel.RegistrarFechamentoCaixa - " + e.Message);
                throw e;
            }

            if (colunasAlteradas == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // aciona flag de atenção no envelope e indica o motivo da atenção
        // será usado para verificação de envetuais problemas no envelope
        public bool AcionarFlagAtencao(EnvelopeModel envelope)
        {
            string query;
            // aciona flag para TRUE e faz um append da string com o conteúdo de atencao_descricao já existente no bd
            // caso conteúdo do bd seja nulo usa string vazia
            query = $"UPDATE envelope SET " +
                $"atencao_flag_verificar = {envelope.AtencaoFlagVerificar}, " +
                $"atencao_descricao = ifnull(atencao_descricao,'') || '{envelope.AtencaoDescricao} / ' " +
                $"WHERE id={envelope.Id}";

            ConectaSQLite conSQL = new ConectaSQLite();
            conSQL.Conectar();

            SQLiteCommand comando = new SQLiteCommand(query, conSQL.conDB);

            int colunasAlteradas = comando.ExecuteNonQuery();
            comando.Dispose();
            conSQL.Desconectar();

            if (colunasAlteradas == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
