using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using EnveloperLibrary;

namespace EnveloperLibrary.Models
{
    public class UsuarioModel
    {
        
        /// <summary>
        /// Identificador único para Usuário
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome do usuário operador do caixa
        /// </summary>
        public string UsuarioNome { get; set; }
        
        /// <summary>
        /// Senha do usuário operador do caixa
        /// </summary>
        public string UsuarioSenha { get; set; }

        /// <summary>
        /// Tipo de permissão acesso deste usuario ao sistema
        /// </summary>
        public int UsuarioPermissao { get; set; }

        /// <summary>
        /// Local de trabalho, PDV habilitado para este usuario 
        /// </summary>
        public int UsuarioPDV { get; set; }

        // recebe usuário e senha e verifica se confere com cadastro do bd, retorna tipo de acesso se encontrar usuário
        public string VerificarLogin(string usuarioNome, string usuarioSenha)
        {
            // se tipoAcesso for vazio então não achou usuário
            string tipoAcesso = "";

            // conectar ao banco de dados
            ConectaSQLite conSQL = new ConectaSQLite();
            conSQL.Conectar();

            // tem duas formas de fazer a consulta SQL, com DataAdapter e com Command
            string query = $"SELECT * FROM usuario WHERE nome ='{usuarioNome}' AND senha = '{usuarioSenha}'";

            // consulta com DataAdapter
            SQLiteDataAdapter dados = new SQLiteDataAdapter(query, conSQL.conDB);
            DataTable tabela = new DataTable();
            dados.Fill(tabela);
            if (tabela.Rows.Count < 1) 
            {
                // acessoVerificado = false;
            }

            // consulta com Command
            SQLiteCommand comando = new SQLiteCommand(query, conSQL.conDB);
            SQLiteDataReader resultado = comando.ExecuteReader();
            if (resultado.HasRows)
            {
                // acessa dados um por vez
                while (resultado.Read())
                {
                    // cria objeto permissao para consultar o nome do tipo da permissao para apresentar na tela
                    PermissaoModel permissao = new PermissaoModel();
                    tipoAcesso = permissao.ConsultarPermissao(resultado.GetInt32(3));
                }
            } 
            comando.Dispose();
            resultado.Close();
            conSQL.Desconectar();
            return tipoAcesso; // se tipoAcesso for vazio então não achou usuário
        }
        /// <summary>
        /// Método que pesquisa tabela Usuario no BD e devolve uma lista de objetos do tipo Usuario
        /// </summary>
        /// <returns></returns>
        public List<UsuarioModel> CarregarUsuario()
        {
            // lista de objetos do tipo Usuario para armazenar dados recuperados do BD
            List<UsuarioModel> listaUsuario = new List<UsuarioModel>();

            // conectar ao banco de dados
            ConectaSQLite conSQL = new ConectaSQLite();
            conSQL.Conectar();

            string query = "SELECT id, nome, senha, pdv_id, permissao_id FROM usuario";

            // consulta com DataReader
            SQLiteCommand comando = new SQLiteCommand(query, conSQL.conDB);
            SQLiteDataReader dados = comando.ExecuteReader();
            while (dados.Read()) // acessa dados um por vez
            {
                UsuarioModel usuario = new UsuarioModel();
                usuario.Id = dados.GetInt32(0);
                usuario.UsuarioNome = dados.GetString(1);
                usuario.UsuarioSenha= dados.GetString(2);
                usuario.UsuarioPDV = dados.GetInt32(3);
                usuario.UsuarioPermissao = dados.GetInt32(4);
                listaUsuario.Add(usuario);
            }
            comando.Dispose();
            dados.Close();
            conSQL.Desconectar();
            return listaUsuario;

        }

        /// <summary>
        /// Excluir Usuario do banco de dados, recebe nome do Usuario como parâmetro
        /// </summary>
        public bool ExcluirUsuario(int idUsuario)
        {
            // conectar ao banco de dados
            ConectaSQLite conSQL = new ConectaSQLite();
            conSQL.Conectar();

            string query = $"DELETE FROM Usuario WHERE id ='{idUsuario}'";
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
        /// Adiciona Usuario do banco de dados, recebe nome do Usuario como parâmetro
        /// </summary>
        public bool AdicionarUsuario(string nomeUsuario, string senhaUsuario, int permissaoUsuario, int pdvUsuario)
        {
            // conectar ao banco de dados
            ConectaSQLite conSQL = new ConectaSQLite();
            conSQL.Conectar();

            string query = $"INSERT INTO Usuario (nome, senha, permissao_id, pdv_id) " +
                           $"VALUES ('{nomeUsuario}', '{senhaUsuario}', {permissaoUsuario}, {pdvUsuario})";
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
        /// Atualiza Usuario do banco de dados, recebe nome do Usuario como parâmetro
        /// </summary>
        public bool AtualizarUsuario(string nomeUsuario, string senhaUsuario, int permissaoUsuario, int pdvUsuario)
        {
            // conectar ao banco de dados
            ConectaSQLite conSQL = new ConectaSQLite();
            conSQL.Conectar();

            string query = $"UPDATE Usuario " +
                           $"SET senha='{senhaUsuario}', permissao_id={permissaoUsuario}, pdv_id={pdvUsuario} " + 
                           $"WHERE nome='{nomeUsuario}'";
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
