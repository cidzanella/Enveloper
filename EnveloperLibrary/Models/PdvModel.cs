using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using EnveloperLibrary;


namespace EnveloperLibrary.Models
{
    public class PdvModel
    {
        /// <summary>
        /// Identificador único para PDV
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Representa os diferentes pontos de venda
        /// </summary>
        public string PdvNome { get; set; }

        /// <summary>
        /// Método que pesquisa tabela PDV no BD e devolve uma lista de objetos do tipo PDV
        /// </summary>
        /// <returns></returns>
        public List<PdvModel> CarregarPDV()
        {
            // lista de objetos do tipo PDV para armazenar dados recuperados do BD
            List<PdvModel> listaPdv = new List<PdvModel>();

            // conectar ao banco de dados
            ConectaSQLite conSQL = new ConectaSQLite();
            conSQL.Conectar();

            string query = "SELECT id, nome FROM pdv ORDER BY nome";

            // consulta com DataReader
            SQLiteCommand comando = new SQLiteCommand(query, conSQL.conDB);
            SQLiteDataReader dados = comando.ExecuteReader();
            while (dados.Read()) // acessa dados um por vez
            {
                PdvModel pdv = new PdvModel();
                pdv.Id = dados.GetInt32(0);
                pdv.PdvNome = dados.GetString(1);
                listaPdv.Add(pdv);
            }
            comando.Dispose();
            dados.Close();
            conSQL.Desconectar();
            return listaPdv;
        }

        /// <summary>
        /// Excluir PDV do banco de dados, recebe nome do PDV como parâmetro
        /// </summary>
        public bool ExcluirPDV(int idPDV)
        {
            // conectar ao banco de dados
            ConectaSQLite conSQL = new ConectaSQLite();
            conSQL.Conectar();

            string query = $"DELETE FROM pdv WHERE id ='{idPDV}'";
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

        /// <summary>
        /// Adiciona PDV do banco de dados, recebe nome do PDV como parâmetro
        /// </summary>
        public bool AdicionarPDV(string nomePDV)
        {
            // conectar ao banco de dados
            ConectaSQLite conSQL = new ConectaSQLite();
            conSQL.Conectar();

            string query = $"INSERT INTO pdv ('nome') VALUES ('{nomePDV}')";
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
