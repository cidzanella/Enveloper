using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnveloperLibrary
{
    public interface IPrinter
    {
        // modelo da impressora
        int Modelo { get; set; }
        
        // porta de comunicação com a impressora
        string Porta { get; set; }
        
        // Conectar impressora
        bool Conectar();
        
        // Desconectar impressora
        bool Desconectar();
        
        // Imprimir texto
        bool Imprimir(string texto, string formato);
        
        // Avançar papel
        bool AvancarPapel(int NumeroLinhas);
        
        // Cortar papel
        bool CortarPapel();
        
        // Abrir gaveta
        bool AbrirGaveta();

    }
}
