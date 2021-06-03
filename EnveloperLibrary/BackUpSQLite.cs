using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection; //para buscar o caminho do aplicativo (assembly) em execução


namespace EnveloperLibrary
{
    public class BackUpSQLite
    {
        // string para o arquivo origem no caminho do aplicativo em execução
        string appPathOrigem="";
        string filePathOrigem="";
        string dbFileOrigem = "EnveloperDB.db";

        // string para o arquivo destino 
        string bkupPathDestino = @"C:\EnveloperBkUp\";
        
        string filePathDestino = "";
        string dbFileDestino = "EnveloperDB.db";

        string dbFileDestinoOld = "EnveloperDB_Old.db";
        string filePathDestinoOld = "";

        //

        // construtor monta caminho para o arquivo origem
        public BackUpSQLite()
        {
            // Origem - busca o caminho do aplicativo em execução
            appPathOrigem = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            
            // Origem - monta caminho completo com nome do arquivo de log
            filePathOrigem = Path.Combine(appPathOrigem, dbFileOrigem);

            // Destino - busca o caminho do aplicativo em execução
            // Testa se diretório existe, senão cria
            if (Directory.Exists(bkupPathDestino) == false)
            {
                DirectoryInfo di = Directory.CreateDirectory(bkupPathDestino);
            }

            // Destino - monta caminho completo com nome do arquivo de log
            filePathDestino = Path.Combine(bkupPathDestino, dbFileDestino);

            // Destino old - monta caminho completo com nome do arquivo para cópia do BD de backup anterior 
            filePathDestinoOld = Path.Combine(bkupPathDestino, dbFileDestinoOld);

        }

        // copia o arquivo SQLite.db para a pasta C:\EnveloperBkUp\ substituindo o arquivo existente
        public void BackUp()
        {
            if (File.Exists(filePathDestinoOld) == true)
            {
                File.Delete(filePathDestinoOld);
            }

            if (File.Exists(filePathDestino) == true)
            {
                File.Copy(filePathDestino, filePathDestinoOld);
                File.Delete(filePathDestino);
            }

            File.Copy(filePathOrigem, filePathDestino);
        }
    }
}
