using RSGymPT.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RSGymPT.Interfaces
{
    internal interface ILogin
    {
        #region Properties
        
        string Password { get; }
        string Username { get; }
        #endregion

        #region Methods

        void Saudacao();
        int LoginInicial();
        Tuple<string, string> DadosLogin(int option);
        #endregion
        string ValidacaoDados(string user, string password, Utilizador[] utilizadores);
    }
}
