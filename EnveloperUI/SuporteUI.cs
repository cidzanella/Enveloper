using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnveloperLibrary.Models;

namespace EnveloperUI
{
    public static class SuporteUI
    {
        private static string _operadorAtivoNome = "Usuário";
        private static string _operadorAtivoTipoAcesso = "Operador Padrão";
        private static List<UsuarioModel> _operadorResponsaveis = new List<UsuarioModel>();

        public static string OperadorAtivoNome
        {
            // forma tradicional de codificar o get com chaves e return
            get
            {
                return _operadorAtivoNome;
            }
            set
            {
                _operadorAtivoNome = value;
            }
        }
        public static string OperadorAtivoTipoAcesso
        {
            // forma curta de codificar o get sem chaves e return, usando o operador lambda
            get => _operadorAtivoTipoAcesso;
            set
            {
                _operadorAtivoTipoAcesso = value;
            }
        }
        public static List<UsuarioModel> OperadorResponsaveis
        {
            get
            {
                return _operadorResponsaveis;

            }
            set
            {
                _operadorResponsaveis = value;
            }
        }
    }
}
