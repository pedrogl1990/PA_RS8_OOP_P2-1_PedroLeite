using RSGymPT.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSGymPT.Interfaces
{
    internal interface IUtilizador
    {

        #region Properties
        string NomeUtilizador { get; }
        string Password { get; }


        #endregion

        #region Methods
        Utilizador[] UtilizadoresDefault(int id);
        #endregion


    }
}
