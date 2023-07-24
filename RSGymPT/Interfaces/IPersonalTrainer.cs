using RSGymPT.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSGymPT.Interfaces
{
    internal interface IPersonalTrainer
    {

        #region Properties
        string SiglaPT { get; }
        string Telemovel { get; }

        #endregion

        #region Methods
        PersonalTrainer[] PTsDefault(int id);
        void ListarPts(PersonalTrainer[] ptsIniciais);
        #endregion
    }
}
