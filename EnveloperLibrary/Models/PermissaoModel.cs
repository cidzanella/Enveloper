using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace EnveloperLibrary.Models
{
    public class PermissaoModel
    {
        /// <summary>
        /// Identificador único para Permissão
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Representa os tipos de permissão de acesso ao sistema
        /// </summary>
        public string PermissaoNome { get; set; }
        
        /// <summary>
        /// Método que pesquisa tabela Permissao no BD e devolve uma lista de objetos do tipo Permissao
        /// </summary>
        /// <returns></returns>
        public List<PermissaoModel> CarregarPermissao()
        {
            // lista de objetos do tipo Permissão para armazenar dados recuperados do BD
            List<PermissaoModel> listaPermissao = new List<PermissaoModel>();

            // conectar ao banco de dados
            ConectaSQLite conSQL = new ConectaSQLite();
            conSQL.Conectar();

            string query = "SELECT id, nome FROM permissao ORDER BY nome";

            // consulta com DataReader
            SQLiteCommand comando = new SQLiteCommand(query, conSQL.conDB);
            SQLiteDataReader dados = comando.ExecuteReader();
            while (dados.Read()) // acessa dados um por vez
            {
                PermissaoModel permissao = new PermissaoModel();
                permissao.Id = dados.GetInt32(0);
                permissao.PermissaoNome = dados.GetString(1);
                listaPermissao.Add(permissao);
            }
            dados.Close();
            comando.Dispose();
            conSQL.Desconectar();
            return listaPermissao;
        }
        /// <summary>
        /// Excluir Permissao do banco de dados, recebe nome do Permissao como parâmetro
        /// </summary>
        public bool ExcluirPermissao(int idPermissao)
        {
            // conectar ao banco de dados
            ConectaSQLite conSQL = new ConectaSQLite();
            conSQL.Conectar();

            string query = $"DELETE FROM Permissao WHERE id ='{idPermissao}'";
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
        /// Adiciona Permissao do banco de dados, recebe nome do Permissao como parâmetro
        /// </summary>
        public bool AdicionarPermissao(string nomePermissao)
        {
            // conectar ao banco de dados
            ConectaSQLite conSQL = new ConectaSQLite();
            conSQL.Conectar();

            string query = $"INSERT INTO permissao ('nome') VALUES ('{nomePermissao}')";
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
        /// Método que pesquisa tabela Permissao no BD pelo ID e devolve a descrição do tipo Permissao
        /// </summary>
        /// <returns></returns>
        public string ConsultarPermissao(int idPermissao)
        {
            PermissaoModel permissao = new PermissaoModel();

            // conectar ao banco de dados
            ConectaSQLite conSQL = new ConectaSQLite();
            conSQL.Conectar();

            string query = $"SELECT id, nome FROM permissao WHERE id={idPermissao}";

            // consulta com DataReader
            SQLiteCommand comando = new SQLiteCommand(query, conSQL.conDB);
            SQLiteDataReader dados = comando.ExecuteReader();
            dados.Read(); // acessa dados 
            if (dados.HasRows)
            {
                permissao.Id = dados.GetInt32(0);
                permissao.PermissaoNome = dados.GetString(1);

            }
            dados.Close();
            comando.Dispose();
            conSQL.Desconectar();
            return permissao.PermissaoNome;
        }
    }
}
