using RSGymPT.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSGymPT.Classes
{
    internal class PersonalTrainer : Pessoa, IPersonalTrainer
    {

        #region Properties

        public string SiglaPT { get; set; }
        public string Telemovel { get; set; }

        #endregion

        #region Constructors

        public PersonalTrainer() : base()
        {
            SiglaPT = "";
            Telemovel = "";
        }

        public PersonalTrainer(int id, string siglaPT, string nome, string telemovel) : base(id, nome)
        {
            SiglaPT = siglaPT;
            Telemovel = telemovel;
        }

        #endregion

        #region Methods

        // Método que cria 3 personal trainers default com o o arranque da aplicação
        public PersonalTrainer[] PTsDefault(int id)
        {
            PersonalTrainer[] PTsInicial = new PersonalTrainer[]
            {
                new PersonalTrainer{ Id = GenerateID(), SiglaPT = "MF1", Nome = "Marco Oliveira Fortes", Telemovel = "900000000"}, 
                new PersonalTrainer{ Id = GenerateID(), SiglaPT = "ZB2", Nome = "Zulmira Ramires Bombada", Telemovel = "911111111"}, 
                new PersonalTrainer{ Id = GenerateID(), SiglaPT = "JC3", Nome = "João Carlos Grande", Telemovel = "922222222"}, 
            };
            return PTsInicial;
        }

        // Método que lista os Personal Trainers existentes

      public void ListarPts(PersonalTrainer [] ptsIniciais)
        {
            Console.WriteLine("Lista dos Personal Trainers no RSGymPT:\n");
                var ptsIniciaisOrdenados = ptsIniciais.OrderBy(pt => pt.Nome);

                foreach (var pt in ptsIniciaisOrdenados)
                {
                    Console.WriteLine($"Id PT: {pt.Id} | Nome PT: {pt.Nome} | Sigla PT: {pt.SiglaPT} | Telemóvel: {pt.Telemovel}");
                }
}
        #endregion
    }
}
