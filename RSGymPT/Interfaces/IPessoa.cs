using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSGymPT.Interfaces
{
    internal interface IPessoa
    {

        #region Properties
        int Id { get; }
        string Nome { get; }
        #endregion


        #region Methods
        int GenerateID();
        #endregion


    }
}
