using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace EnveloperLibrary.Models
{
    public class TurnoModel
    {
        /// <summary>
        /// Identificador único para Turno
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Representa os diferentes turnos de trabalho
        /// </summary>
        public string TurnoNome { get; set; }

        /// <summary>
        /// Método que pesquisa tabela Turno no BD e devolve uma lista de objetos do tipo Turno
        /// </summary>
        /// <returns></returns>
        public List<TurnoModel> CarregarTurno()
        {
            // lista de objetos do tipo Turno para armazenar dados recuperados do BD
            List<TurnoModel> listaTurno = new List<TurnoModel>();

            // conectar ao banco de dados
            ConectaSQLite conSQL = new ConectaSQLite();
            conSQL.Conectar();

            string query = "SELECT id, nome FROM turno ORDER BY nome";

            // consulta com DataReader
            SQLiteCommand comando = new SQLiteCommand(query, conSQL.conDB);
            SQLiteDataReader dados = comando.ExecuteReader();
            while (dados.Read()) // acessa dados um por vez
            {
                TurnoModel turno = new TurnoModel();
                turno.Id = dados.GetInt32(0);
                turno.TurnoNome = dados.GetString(1);
                listaTurno.Add(turno);
            }
            conSQL.Desconectar();
            return listaTurno;
        }

        /// <summary>
        /// Excluir Turno do banco de dados, recebe nome do Turno como parâmetro
        /// </summary>
        public bool ExcluirTurno(int idTurno)
        {
            // conectar ao banco de dados
            ConectaSQLite conSQL = new ConectaSQLite();
            conSQL.Conectar();

            string query = $"DELETE FROM turno WHERE id ='{idTurno}'";
            int colunasAlteradas = 0;

            // comando com DataReader
            SQLiteCommand comando = new SQLiteCommand(query, conSQL.conDB);
            colunasAlteradas = comando.ExecuteNonQuery();
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
        /// Adiciona Turno do banco de dados, recebe nome do Turno como parâmetro
        /// </summary>
        public bool AdicionarTurno(string nomeTurno)
        {
            // conectar ao banco de dados
            ConectaSQLite conSQL = new ConectaSQLite();
            conSQL.Conectar();

            string query = $"INSERT INTO turno ('nome') VALUES ('{nomeTurno}')";
            int colunasAlteradas = 0;

            // comando com DataReader
            SQLiteCommand comando = new SQLiteCommand(query, conSQL.conDB);
            colunasAlteradas = comando.ExecuteNonQuery();
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
