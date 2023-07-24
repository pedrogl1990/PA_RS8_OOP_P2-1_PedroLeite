using RSGymPT.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSGymPT.Classes
{
    internal class Pessoa: IPessoa
    {

        #region Properties
        public int Id { get; set; }
        public string Nome { get; set; }

        #endregion

        #region Constructors

        public Pessoa()
        {
            Id = 0;
            Nome = "";
        }     
        
        public Pessoa(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        #endregion

        #region Methods

        //método criado para gerar os ID's automáticos

        public int GenerateID()
        {
            return Id++;
        }

        #endregion
    }
}
