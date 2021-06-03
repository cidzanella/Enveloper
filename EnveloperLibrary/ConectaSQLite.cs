using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using EnveloperLibrary.Models;

namespace EnveloperLibrary
{
    public class ConectaSQLite: IConectaDados
    {
        // conectar ao banco de dados
        private SQLiteConnection _conDB = new SQLiteConnection("Datasource=EnveloperDB.db");
        
        /// <summary>
        /// Propriedade que dá acesso a Conexão com o banco de dados
        /// </summary>
        public SQLiteConnection conDB
        {
            get => _conDB;
        }

        /// <summary>
        public bool Conectar()
        {
            try
            {
                _conDB.Open();
                _conDB.DefaultTimeout = 05; // 05 segundos, especifica o tempo limite que um comando tem para executar antes de sair por timeout - evita aguardar os 30s default
                return true;
            } 
            catch 
            {
                return false;
            }
        }

        public bool Desconectar()
        {
            try
            {
                _conDB.Close();
                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}
