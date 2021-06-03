using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace EnveloperLibrary
{
    /// <summary>
    /// Busca no DB dados de venda e monta um relatório em texto simples que pode ser enviado por email ou telegram
    /// </summary>
    public class SalesReport
    {
        string faturamentoMensal;
        string faturamentoDiario;

        /*
        */

        private void FaturmamentoDiario()
        {
            // conecta banco de dados
            var conSQL = new ConectaSQLite();
            conSQL.Conectar();

            // monta query de consulta

            // executa consulta

            // retorna 


        }
    }
}
