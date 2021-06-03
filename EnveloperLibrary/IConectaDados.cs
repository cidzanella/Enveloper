using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnveloperLibrary.Models;

namespace EnveloperLibrary
{
    public interface IConectaDados
    {
        // métodos para abrir conexão ao dados
        bool Conectar();
        // métodos para fechar conexão ao dados
        bool Desconectar();
    }
}
