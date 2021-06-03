using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnveloperLibrary
{
    public class Printer4200th : IPrinter
    {

        // defautl modelo 4200TH
        private int _modelo=0;

        // default USB
        private string _porta = "USB";

        // define modelo da impressora
        public int Modelo 
        {
            get
            {
                return _modelo;
            }
            set 
            {
                _modelo = value;
                int retorno = MP2032.ConfiguraModeloImpressora(_modelo);
            } 
        } 

        // porta conexão impressora
        public string Porta 
        {
            get => _porta;
            set => _porta = Porta;
            
        } 

        public bool AbrirGaveta()
        {
            int retorno;
            
            // comandos para abrir a gaveta
            int charCode1 = 27;
            int charCode2 = 118;
            int charCode3 = 140;

            char specialChar1 = Convert.ToChar(charCode1);
            char specialChar2 = Convert.ToChar(charCode2);
            char specialChar3 = Convert.ToChar(charCode3);

            string comando = "" + specialChar1 + specialChar2 + specialChar3;

            retorno = MP2032.ComandoTX(comando, comando.Length);
             
            if (retorno == 1)
            {
                return true;
            }
            {
                return false;
            }
        }

        public bool AvancarPapel(int NumeroLinhas)
        {
            bool retorno = true;
            string linhas="";
            while (NumeroLinhas>0 && retorno )
            {
                linhas += " \n \r ";
                NumeroLinhas -=1;
            }
            retorno = this.Imprimir(linhas, ""); // imprime linhas em branco para avançar
            return retorno;
        }

        public bool Conectar()
        {
            int retorno;

            retorno = MP2032.IniciaPorta(_porta);
            
            if (retorno == 1)
            {
                return true;
            }
            {
                return false;
            }
        }

        public bool CortarPapel()
        {
            int retorno;

            // corte total do papel
            retorno = MP2032.AcionaGuilhotina(1);

            if (retorno == 1)
            {
                return true;
            }
            {
                return false;
            }
        }

        public bool Desconectar()
        {
            int retorno;
            
            retorno = MP2032.FechaPorta();

            if (retorno == 1)
            {
                return true;
            }
            {
                return false;
            }
        }

        public bool Imprimir(string texto, string formato)
        {
            int retorno;

            // imprime com formato normal
            retorno = MP2032.FormataTX(texto, 2, 0, 0, 0, 0);

            if (retorno == 1)
            {
                return true;
            }
            {
                return false;
            }
        }
    }
}
