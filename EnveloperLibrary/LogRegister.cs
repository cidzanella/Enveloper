using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; // para funções de IO em arquivos
using System.Reflection; //para buscar o caminho do aplicativo (assembly) em execução

namespace EnveloperLibrary.Models
{
    class LogRegister
    {
        // string para o caminho do aplicativo em execução
        private static string appPath;

        // método para gravação de registro de log no arquivo de log especificado (default "LogFile.txt")
        // retorna true para sucesso ou false em caso de erro
        public static bool Log(string registry, string logFile = "LogFile.txt")
        {
            // caminho para o arquivo de log
            string filePath;
            FileStream theFile;
            
            try
            {
                // busca o caminho do aplicativo em execução
                appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                // monta caminho completo com nome do arquivo de log
                filePath = Path.Combine(appPath, logFile);

                // testa se arquivo existe, senão cria o arquivo
                if (File.Exists(filePath) == false)
                {
                    theFile = File.Create(filePath);
                    theFile.Close();
                }

                // cria um escritor para o arquivo e através do using ele será destruído automaticamente ao final do bloco
                using (StreamWriter writer = File.AppendText(filePath))
                {
                    AppendLog(registry, writer);
                }

                return true;
            }
             catch (Exception)
            {
                return false;
            }
        }

        // Monta o formato do log com o texto de registro recebido e escreve no arquivo
        private static void AppendLog(string registry, StreamWriter writer)
        {
            try
            {
                writer.WriteLine($"Log: { System.DateTime.Now.ToString() } ");
                writer.WriteLine($">>> {registry} ");
                writer.WriteLine("---------------------------------------------");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
