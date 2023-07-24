using RSGymPT.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace RSGymPT
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Utility2.UnicodeConsola();
            try
            {
                Utilizador utilizadoresDefault = new Utilizador();
                PersonalTrainer ptsDefault = new PersonalTrainer();
                Login novoLogin = new Login();
                Navegacao navegacao = new Navegacao();
                Pedido pedido= new Pedido();

                Utilizador[] usersIniciais = utilizadoresDefault.UtilizadoresDefault(utilizadoresDefault.GenerateID());
                PersonalTrainer[] ptsIniciais = ptsDefault.PTsDefault(ptsDefault.GenerateID());
                bool continuarCorrer = true;
                bool voltarAMenu = true;
                novoLogin.Saudacao();
                while (continuarCorrer)
                {
                    voltarAMenu = true;
                    int option = novoLogin.LoginInicial();
                    Tuple<string, string> strings = novoLogin.DadosLogin(option);
                    string username = strings.Item1;
                    string password = strings.Item2;
                    username = novoLogin.ValidacaoDados(username, password, usersIniciais);

                    while (voltarAMenu)
                    {
                    int optionMenu = navegacao.ConstrutorMenuPrincipal(username);
                    if (optionMenu == 1)
                    {
                            Tuple<bool, bool> bools = navegacao.ConstrutorMenuPedido(username, continuarCorrer, voltarAMenu, pedido);
                            continuarCorrer = bools.Item1;
                            voltarAMenu = bools.Item2;
                    }
                    else if (optionMenu == 2)
                    {
                            Tuple<bool, bool> bools2 = navegacao.ConstrutorMenuPTs(username, ptsDefault, ptsIniciais, continuarCorrer, voltarAMenu);
                            continuarCorrer = bools2.Item1;
                            voltarAMenu = bools2.Item2;
                        }
                    else if (optionMenu == 3)
                    {
                            Tuple<bool, bool> bools3 = navegacao.ConstrutorMenuUtilizador(username, usersIniciais, novoLogin, continuarCorrer, voltarAMenu);
                            continuarCorrer = bools3.Item1;
                            voltarAMenu = bools3.Item2;
                        }
                    Console.ReadLine();

                    }
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Ups, algum erro ocorreu! Contacta-nos através do email errors@rsgym.pt");
            }

        }
    }
}
