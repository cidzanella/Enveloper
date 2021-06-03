using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace EnveloperLibrary
{
    /// <summary>
    /// Vendas a cada 7 dias: tarde, noite, total, media tarde, media noite, media diaria, media dia semana, media sabado, media domingo, media final de semana
    /// </summary>
    public class SalesQuarterly
    {
        public double VendasQuarto1 { get; set; } = 0;
        public double VendasQuarto2 { get; set; } = 0;
        public double VendasQuarto3 { get; set; } = 0;
        public double VendasQuarto4 { get; set; } = 0;
        public double ProjecaoQuarto1 { get; set; } = 0;
        public double ProjecaoQuarto2 { get; set; } = 0;

        public string Vendas()
        {
            string _retorno = "";
            // quantas semanas fechou até hoje?
            //          semana #1 dia 8 mostra vendas de 1-7
            //          semana #2 dia 15 mostra vendas de 1-7 / 8-14
            //          semana #3 dia 22 mostra vendas de 1-7 / 8-14 / 15-21
            //          semana #4 dia 29 mostra vendas de 1-7 / 8-14 / 15-21 / 22-28

            int _dia = 29; // DateTime.Today.Day;

            string _mesAno = "10/2020"; // DateTime.Today.Month.ToString("00") + "/" + DateTime.Today.Year;

            _retorno += $" \n[ FATURAMENTO POR QUARTO {_mesAno} ] \n";

            switch (_dia)
            {
                case 8:
                    _retorno += VendasPrimeiroQuarto(_mesAno);
                    break;
                case 15:
                    _retorno += VendasPrimeiroQuarto(_mesAno);
                    _retorno += "\n" + VendasSegundoQuarto(_mesAno);
                    break;
                case 22:
                    _retorno += VendasPrimeiroQuarto(_mesAno);
                    _retorno += "\n" + VendasSegundoQuarto(_mesAno);
                    _retorno += "\n" + VendasTerceiroQuarto(_mesAno);
                    break;
                case 29:
                    _retorno += VendasPrimeiroQuarto(_mesAno);
                    _retorno += "\n" + VendasSegundoQuarto(_mesAno);
                    _retorno += "\n" + VendasTerceiroQuarto(_mesAno);
                    _retorno += "\n" + VendasQuartoQuarto(_mesAno);
                    break;
            }
            return _retorno;
        }

        private string VendasPrimeiroQuarto(string mesAno)
        {
            string _retorno = "";
            string _query;
            string _dataInicial =  mesAno.Substring(3,4) + "-" + mesAno.Substring(0,2) + "-01";
            string _dataFinal = mesAno.Substring(3, 4) + "-" + mesAno.Substring(0, 2) + "-07";


            _query = $"SELECT SUM(faturamento) AS Total FROM envelope " +
                     $"WHERE (substr(data_fechamento_caixa, 7, 4) || '-' || substr(data_fechamento_caixa, 4, 2) || '-' || substr(data_fechamento_caixa, 1, 2)) " +
                     $"BETWEEN '{_dataInicial}' AND '{_dataFinal}'";
            // Nov 
            //SELECT data_fechamento_caixa FROM envelope WHERE (substr(data_fechamento_caixa, 7, 4) || '-' || substr(data_fechamento_caixa, 4, 2) || '-' || substr(data_fechamento_caixa, 1, 2)) BETWEEN '2020-10-01' AND '2020-10-04'
            ConectaSQLite conSQL = new ConectaSQLite();
            conSQL.Conectar();

            SQLiteCommand comando = new SQLiteCommand(_query, conSQL.conDB);
            SQLiteDataReader dados = comando.ExecuteReader();

            while (dados.Read())
            {
                if (double.TryParse(dados["Total"].ToString(), out double saida))
                {
                    VendasQuarto1 = saida;
                    ProjecaoQuarto1 = VendasQuarto1 * 4;
                    _retorno = "#1 Quarto: R$" + VendasQuarto1.ToString("F");
                    _retorno += " - Projeção: R$" + ProjecaoQuarto1.ToString("F");
                }
            }
            dados.Close();
            comando.Dispose();
            conSQL.Desconectar();
            return _retorno;
        }

        private string VendasSegundoQuarto(string mesAno)
        {
            string _retorno = "";
            string _query;
            string _dataInicial = mesAno.Substring(3, 4) + "-" + mesAno.Substring(0, 2) + "-08";
            string _dataFinal = mesAno.Substring(3, 4) + "-" + mesAno.Substring(0, 2) + "-14";

            _query = $"SELECT SUM(faturamento) AS Total FROM envelope " +
                     $"WHERE (substr(data_fechamento_caixa, 7, 4) || '-' || substr(data_fechamento_caixa, 4, 2) || '-' || substr(data_fechamento_caixa, 1, 2)) " +
                     $"BETWEEN '{_dataInicial}' AND '{_dataFinal}'";

            ConectaSQLite conSQL = new ConectaSQLite();
            conSQL.Conectar();

            SQLiteCommand comando = new SQLiteCommand(_query, conSQL.conDB);
            SQLiteDataReader dados = comando.ExecuteReader();

            while (dados.Read() )
            {
                if (double.TryParse(dados["Total"].ToString(), out double saida))
                {
                    VendasQuarto2 = saida;
                    ProjecaoQuarto2 = VendasQuarto1 + (VendasQuarto2 * 3);
                    _retorno = "#2 Quarto: R$" + VendasQuarto2.ToString("F");
                    _retorno += " - Projeção: R$" + ProjecaoQuarto2.ToString("F");
                }
            }
            dados.Close();
            comando.Dispose();
            conSQL.Desconectar();
            return _retorno;
        }

        private string VendasTerceiroQuarto(string mesAno)
        {
            string _retorno = "";
            string _query;
            string _dataInicial = mesAno.Substring(3, 4) + "-" + mesAno.Substring(0, 2) + "-15";
            string _dataFinal = mesAno.Substring(3, 4) + "-" + mesAno.Substring(0, 2) + "-21";

            _query = $"SELECT SUM(faturamento) AS Total FROM envelope " +
                     $"WHERE (substr(data_fechamento_caixa, 7, 4) || '-' || substr(data_fechamento_caixa, 4, 2) || '-' || substr(data_fechamento_caixa, 1, 2)) " +
                     $"BETWEEN '{_dataInicial}' AND '{_dataFinal}'";

            ConectaSQLite conSQL = new ConectaSQLite();
            conSQL.Conectar();

            SQLiteCommand comando = new SQLiteCommand(_query, conSQL.conDB);
            SQLiteDataReader dados = comando.ExecuteReader();

            while (dados.Read())
            {
                if (double.TryParse(dados["Total"].ToString(), out double saida))
                {
                    VendasQuarto3 = saida;
                    _retorno = "#3 Quarto: R$" + VendasQuarto3.ToString("F");
                }
            }
            dados.Close();
            comando.Dispose();
            conSQL.Desconectar();
            return _retorno;
        }

        private string VendasQuartoQuarto(string mesAno)
        {
            string _retorno = "";
            string _query;
            string _dataInicial = mesAno.Substring(3, 4) + "-" + mesAno.Substring(0, 2) + "-22";
            string _dataFinal = mesAno.Substring(3, 4) + "-" + mesAno.Substring(0, 2) + "-28";

            _query = $"SELECT SUM(faturamento) AS Total FROM envelope " +
                     $"WHERE (substr(data_fechamento_caixa, 7, 4) || '-' || substr(data_fechamento_caixa, 4, 2) || '-' || substr(data_fechamento_caixa, 1, 2)) " +
                     $"BETWEEN '{_dataInicial}' AND '{_dataFinal}'";

            ConectaSQLite conSQL = new ConectaSQLite();
            conSQL.Conectar();

            SQLiteCommand comando = new SQLiteCommand(_query, conSQL.conDB);
            SQLiteDataReader dados = comando.ExecuteReader();

            while (dados.Read())
            {
                if (double.TryParse(dados["Total"].ToString(), out double saida))
                {
                    VendasQuarto4 = saida;
                    _retorno = "#4 Quarto: R$" + VendasQuarto4.ToString("F");
                }
            }
            dados.Close();
            comando.Dispose();
            conSQL.Desconectar();
            return _retorno;
        }

    }
}
