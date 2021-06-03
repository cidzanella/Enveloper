using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnveloperLibrary
{
    /// <summary>
    ///  Vendas anual por mês a mês: tarde, noite, total, media tarde, media noite, media diaria, media dia semana, media sabado, media domingo, media final de semana
    /// </summary>
    public class SalesMonthly
    {
        public string VendasPorDia(int mes, int ano)
        {
            string _retorno = "";
            string _query;
            string _mesAno = mes.ToString("00") + "/" + ano;
            string _dataInicial = "01/" + _mesAno;
            string _dataFinal = "31/" + _mesAno;
            double _totalMes = 0;

            _query = $"SELECT data_fechamento_caixa AS Dia, SUM(faturamento) AS TotalDia FROM envelope " +
                $"WHERE substr(data_fechamento_caixa,4,7) = '{_mesAno}' GROUP BY data_fechamento_caixa";

            ConectaSQLite conSQL = new ConectaSQLite();
            conSQL.Conectar();

            SQLiteCommand comando = new SQLiteCommand(_query, conSQL.conDB);
            SQLiteDataReader dados = comando.ExecuteReader();

            _retorno += $"\n [ FATURAMENTO TOTAL {_mesAno} ] \n";

            while (dados.Read())
            {
                if (double.TryParse(dados["TotalDia"].ToString(), out double TotalDia))
                {
                    _totalMes += TotalDia;
                    _retorno += dados["Dia"].ToString() + " - R$" + TotalDia.ToString("F") + "\n";
                }
            }
            _retorno += "Total Mensal: R$ " + _totalMes.ToString("F");
            
            dados.Close();
            comando.Dispose();
            conSQL.Desconectar();
            return _retorno;
        }

        public string VendasAnualPorMes(int ano)
        {
            string _retorno = "";
            string _query;
            string _dataInicial = "01/01/" + ano;
            string _dataFinal = "31/12/" + ano;

            _query = $"SELECT substr(data_fechamento_caixa,4,2) as Mes, SUM(faturamento) AS TotalMes FROM envelope " +
                $"WHERE data_fechamento_caixa BETWEEN '{_dataInicial}' AND '{_dataFinal}' GROUP BY substr(data_fechamento_caixa,4,2) ";
            ConectaSQLite conSQL = new ConectaSQLite();
            conSQL.Conectar();

            SQLiteCommand comando = new SQLiteCommand(_query, conSQL.conDB);
            SQLiteDataReader dados = comando.ExecuteReader();

            _retorno += $"\n [ FATURAMENTO MENSAL ANUAL {ano} ] \n";

            while (dados.Read())
            {
                if (double.TryParse(dados["TotalMes"].ToString(), out double TotalMes))
                {
                    _retorno += dados["Mes"].ToString() + " - R$" + TotalMes.ToString("F") + " \n ";
                }
            }
            dados.Close();
            comando.Dispose();
            conSQL.Desconectar();
            return _retorno;
        }


    }
}
