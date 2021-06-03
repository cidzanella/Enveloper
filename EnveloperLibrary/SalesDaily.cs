using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EnveloperLibrary
{
    /// <summary>
    /// Vendas por dia no mês até hoje: tarde, noite, total, media tarde, media noite, media diaria, media dia semana, media sabado, media domingo, media final de semana
    /// </summary>
    public class SalesDaily
    {
        string _mesBase;

        public int Turnos { get; set; }
        public int Dias { get; set; }
        public double Total { get; set; }
        public double MediaTurno { get; set; }
        public double MediaDia { get; set; }
        public double MinimoTurno { get; set; }
        public double MaximoTurno { get; set; }

        // busca vendas no mês até hoje
        public SalesDaily VendasDiarias([Optional] string mesBase)
        {
            // objeto da classe que será retornado com os dados de faturamento do DB
            SalesDaily retorno = new SalesDaily();

            // caso não especifique um mês assume mês atual
            if (mesBase == null)
            {
                _mesBase = DateTime.Now.Month.ToString("00");
            } else
            {
                _mesBase = mesBase;
            }

            // adiciona 0 na frente do mês para formatar com duas casa: Jan=01, Fev=02, ...
            if (_mesBase.Length < 2)
            {
                _mesBase = "0" + _mesBase;
            }
            string _mesAno = _mesBase + "/" + DateTime.Today.Year;

            // monta sql de consulta
            string query = $"SELECT min(faturamento) AS MininoTurno, " +
                $"max(faturamento) AS MaximoTurno,  " +
                $"avg(faturamento) AS MediaTurno, " +
                $"sum(faturamento) AS Total, " +
                $"count(id) AS Turnos " +
                $"FROM envelope WHERE substr(data_fechamento_caixa,4,7) = '{_mesAno}'";

            // abre conexão 
            ConectaSQLite conSQL = new ConectaSQLite();
            conSQL.Conectar();

            // executa sql de consulta
            SQLiteCommand comando = new SQLiteCommand(query, conSQL.conDB);
            SQLiteDataReader dados = comando.ExecuteReader();
            
            // monta objeto de retorno
            while (dados.Read())
            {
                retorno.Turnos = int.Parse(dados["Turnos"].ToString());
                if (retorno.Turnos > 0)
                {
                    retorno.Dias = retorno.Turnos / 2;
                    retorno.Total = double.Parse(dados["Total"].ToString());
                    retorno.MediaTurno = double.Parse(dados["MediaTurno"].ToString());
                    retorno.MediaDia = retorno.Total / retorno.Dias;
                    retorno.MinimoTurno = double.Parse(dados["MininoTurno"].ToString());
                    retorno.MaximoTurno = double.Parse(dados["MaximoTurno"].ToString());
                }
            }

            // fecha e encerra conexão
            comando.Dispose();
            dados.Close();
            conSQL.Desconectar();
            
            // retorna objeto 
            return retorno;
        }

        // retorna o faturamento total do dia
        public string VendasHoje()
        {
            string retorno = "[ VENDAS HOJE ] ";
            double vendas = 0;
            double totalVendas = 0;
            string hoje = DateTime.Today.ToString("dd/MM/yyyy");
            
            // monta string de busca 
            string query = $"SELECT faturamento FROM envelope WHERE data_fechamento_caixa = '{hoje}'";

            // conecta 
            ConectaSQLite conSQL = new ConectaSQLite();
            conSQL.Conectar();

            // executa query 
            SQLiteCommand comando = new SQLiteCommand(query, conSQL.conDB);
            SQLiteDataReader dados = comando.ExecuteReader();
            
            // monta retorno
            while (dados.Read())
            {
                vendas = dados.GetFloat(0);
                retorno += "\n Turno: R$" + vendas.ToString("F");
                totalVendas += vendas;
            }

            retorno += "\n Total: R$" + totalVendas.ToString("F");

            // fecha e encerra conexão
            comando.Dispose();
            dados.Close();
            conSQL.Desconectar();

            // retorna venda do dia
            return retorno;
        }

        private void VendasTarde()
        {

        }

        private void VendasNoite()
        {

        }

        private void MediaVendasTarde()
        {

        }

        private void MediaVendasNoite()
        {

        }

        private void MediaVendasDiaSemana()
        {

        }

        private void MediaVendasFinalSemana()
        {

        }

        private void MediaVendasSabados()
        {

        }

        private void MediaVendasDomingos()
        {

        }

    }
}
