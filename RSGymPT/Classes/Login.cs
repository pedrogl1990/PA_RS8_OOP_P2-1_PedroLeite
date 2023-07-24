using RSGymPT.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;
using Utility;

namespace RSGymPT.Classes
{
    internal class Login : ILogin
    {
        #region Properties
        public string Username { get; set; }
        public string Password { get; set; }

        #endregion

        #region Constructor

        public Login()
        {
            Username = "";
            Password = "";
        }

        public Login(string username, string password)
        {
            Username = username;
            Password = password;
        }
        #endregion

        #region Methods

        // Método que escreve a saudação inicial antes de entrar no menu do login
        public void Saudacao()
        {
            Utility2.EscreverTitulo("Sê Bem-Vindo(a) ao RSGymPT");
            Console.WriteLine("Let's work together!");
            Thread.Sleep(3000);
        }

        //Metodo que carrega o menu de Login inicial pedindo o username e passwod

        public int LoginInicial()
        {
            int option;

            do
            {
                Console.Clear();
                Console.WriteLine("Seleciona a opção pretendida: \n");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Sair\n");
                Console.Write("A tua opção: ");
                Int32.TryParse(Console.ReadLine(), out option);

            } while (option != 1 && option != 2);
            return option;
        }

        public Tuple<string, string> DadosLogin(int option)
        {
            if (option == 1)
            {
                Console.Clear();
                Console.WriteLine("Por favor introduz os teus dados de Login\n");
                Console.Write("User: ");
                Username = Console.ReadLine();
                Console.Write("Password: ");
                Password = Console.ReadLine();
                return Tuple.Create(Username, Password);
            }
            else if (option == 2)
            {
                Utility2.MensagemDepedida();
            }
            return Tuple.Create(Username, Password);
        }

        // Estes dois métodos estáticos designados por Log e Log2 foram criados com o propósito de colocar sempre visivel o nome do usernarme ao longo do programa

        public static void Log(string username)
        {
            Console.BackgroundColor= ConsoleColor.DarkMagenta;
            Console.WriteLine($"User Logged: {username}");
            Console.ResetColor();
        }

        public static string Log2(string username)
        {
            return username;
        }

        // Este método valida os dados colocados pelo utilizador no ato de autenticação. Se estiverem certos permitem o avanço, caso contrário fica preso num
        // while até passar os dados que são válidos

        public string ValidacaoDados(string user, string password, Utilizador[] utilizadores)
        {
            do
            {
                Utilizador utilizador = Array.Find(utilizadores, element => element.NomeUtilizador == Username);
                if (utilizador != null && utilizador.Password.Equals(Password))
                {
                    Utility2.MensagemBoasVindas(Username);
                    return Username;
                }
                else
                {
                    bool check = true;
                    while (check)
                    {
                        Console.Clear();
                        Utility2.NaoValido("Ups, login mal sucedido\n");
                        Console.WriteLine("Verifica se escreveste bem o user e a password");
                        Console.ReadKey();
                        DadosLogin(1);
                       
             
                        utilizador = Array.Find(utilizadores, element => element.NomeUtilizador == Username);

                        if (utilizador != null && utilizador.Password.Equals(Password))
                        {
                            check = false;
                            Console.Clear();
                            Utility2.MensagemBoasVindas(Username);
                        }
                        else
                        {
                            check = true;
                            
                        }   
                    }
                    return Username;
                }
            } while (Username == null || Password == null);
        }


    }

    #endregion
}


