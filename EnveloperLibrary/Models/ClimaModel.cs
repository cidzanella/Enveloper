using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using EnveloperLibrary;

namespace EnveloperLibrary.Models
{
    public class ClimaModel
    {

        /// <summary>
        /// Identificador único para Clima
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Representa os tipos de climas do dia
        /// </summary>
        public string ClimaNome { get; set; }

        /// <summary>
        /// Método que pesquisa tabela Clima no BD e devolve uma lista de objetos do tipo Clima
        /// </summary>
        /// <returns></returns>
        public List<ClimaModel> CarregarClima()
        {
        // lista de objetos do tipo Clima para armazenar dados recuperados do BD
            List<ClimaModel> listaClima = new List<ClimaModel>();

            // conectar ao banco de dados
            ConectaSQLite conSQL = new ConectaSQLite();
            conSQL.Conectar();
     
            string query = "SELECT id, nome FROM clima ORDER BY nome";

            // consulta com DataReader
            SQLiteCommand comando = new SQLiteCommand(query, conSQL.conDB);
            SQLiteDataReader dados = comando.ExecuteReader();
            while (dados.Read()) // acessa dados um por vez
            {
                ClimaModel clima = new ClimaModel();
                clima.Id = dados.GetInt32(0);
                clima.ClimaNome = dados.GetString(1);
                listaClima.Add(clima);
            }
            comando.Dispose();
            dados.Close();
            conSQL.Desconectar();
            return listaClima;
        }

        /// <summary>
        /// Excluir clima do banco de dados, recebe nome do Clima como parâmetro
        /// </summary>
        public bool ExcluirClima(int idClima)
        {
            // conectar ao banco de dados
            ConectaSQLite conSQL = new ConectaSQLite();
            conSQL.Conectar();

            string query = $"DELETE FROM clima WHERE id ='{idClima}'";
            int colunasAlteradas = 0;

            // comando com DataReader
            SQLiteCommand comando = new SQLiteCommand(query, conSQL.conDB);

            try
            {
                colunasAlteradas = comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                comando.Dispose();
                conSQL.Desconectar();
            }

            // testa se alterou uma coluna no BD (se excluiu)
            if (colunasAlteradas==1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Adiciona clima do banco de dados, recebe nome do Clima como parâmetro
        /// </summary>
        public bool AdicionarClima(string nomeClima)
        {
            // conectar ao banco de dados
            ConectaSQLite conSQL = new ConectaSQLite();
            conSQL.Conectar();

            string query = $"INSERT INTO clima ('nome') VALUES ('{nomeClima}')";
            int colunasAlteradas = 0;

            // comando com DataReader
            SQLiteCommand comando = new SQLiteCommand(query, conSQL.conDB);
            colunasAlteradas = comando.ExecuteNonQuery();
            comando.Dispose();
            conSQL.Desconectar();
            // testa se alterou uma coluna no BD (se excluiu)
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