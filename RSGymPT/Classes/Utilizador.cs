using Microsoft.Win32;
using RSGymPT.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RSGymPT.Classes
{

    internal class Utilizador : Pessoa, IUtilizador
    {
    #region Properties
        public string NomeUtilizador { get; set; }
        public string Password { get; set; }
    #endregion

    #region Constructors

        public Utilizador() : base()
        {
            NomeUtilizador = "";
            Password = "";
        }

        public Utilizador(int id, string name, string utilizador, string password) : base(id, name)
        {
            NomeUtilizador = utilizador;
            Password = password;
        }

        #endregion

        #region Methods
        // Método que cria 3 utilizadores default com o o arranque da aplicação


        public Utilizador[] UtilizadoresDefault(int id)
        {
            Utilizador[] usersInicial = new Utilizador[]
            {
                new Utilizador {Id = GenerateID(), Nome = "user1", NomeUtilizador = "user1user", Password = "user1234"},
                new Utilizador {Id = GenerateID(), Nome = "user2", NomeUtilizador = "user2user", Password = "user1234"},
            };
                return usersInicial;
        }

        #endregion
    }

}
