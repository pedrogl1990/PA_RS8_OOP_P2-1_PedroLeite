using RSGymPT.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utility;

namespace RSGymPT.Classes
{
    internal class Navegacao : INavegacao
    {
        #region Propriedades

        public string[] MenuPrincipal;
        public string[] SubMenuPedido;
        public string[] SubMenuPedidoAlterar;
        public string[] SubMenuPT;
        public string[] SubMenuUtilizador;

        string[] INavegacao.MenuPrincipal => throw new NotImplementedException();

        string[] INavegacao.SubMenuPedido => throw new NotImplementedException();

        string[] INavegacao.SubMenuPedidoAlterar => throw new NotImplementedException();

        string[] INavegacao.SubMenuPT => throw new NotImplementedException();

        string[] INavegacao.SubMenuUtilizador => throw new NotImplementedException();

        #endregion

        #region Construtores

        public Navegacao()
        {
            MenuPrincipal = new string[] { "Pedido", "Personal Trainer", "Utilizador" };
            SubMenuPedido = new string[] { "Registar Pedido", "Alterar Pedido", "Eliminar Pedido", "Consultar Pedido", "Terminar Pedido", "Terminar Sessão", "Menu Principal" };
            SubMenuPedidoAlterar = new string[] { "Cancelar Pedido", "Modificar Data do Pedido" };
            SubMenuPT = new string[] { "Consultar", "Menu Principal" };
            SubMenuUtilizador = new string[] { "Consultar", "Logout", "Menu Principal" };
        }

        public Navegacao(string[] menuPrincipal, string[] subMenuPedido, string[] subMenuPedidoAlterar, string[] subMenuPT, string[] subMenuUtilizador)
        {
            MenuPrincipal = menuPrincipal;
            SubMenuPedido = subMenuPedido;
            SubMenuPedidoAlterar = subMenuPedidoAlterar;
            SubMenuPT = subMenuPT;
            SubMenuUtilizador = subMenuUtilizador;
        }

        #endregion

        #region Métodos

        // Método para listar os diferentes menus

        public void ListarMenu(string[] menu)
        {
            for (int i = 0; i < menu.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {menu[i]}");
            }
        }

        // Método que utilizo para obter a escolha de navegação do utilizador

        public int LerOpcao(string ajuda)
        {
            int opcao;

            Console.WriteLine($"\nEscolhe a opção pretendida{ajuda}\n");
            Console.Write("A tua opção: ");
            Int32.TryParse(Console.ReadLine(), out opcao);
            return opcao;
        }

        // Método que constroi o menu principal da aplicação

        public int ConstrutorMenuPrincipal(string user)
        {
            int opcao;
            Utility2.EscreverTitulo("RSGymPT - Menu Principal");
            Login.Log(user);
            Console.WriteLine();
            ListarMenu(MenuPrincipal);
            opcao = LerOpcao(" (numérica e entre 1 e 3)");
            while (opcao < 1 || opcao > 3)
            {
                Utility2.EscreverTitulo("RSGymPT - Menu Principal");
                Login.Log(user);
                Console.WriteLine();
                ListarMenu(MenuPrincipal);
                opcao = LerOpcao(" (numérica e entre 1 e 3)");
            }
            return opcao;
        }

        // Método utilizado para listar todos os utilizadores iniciais na aplicação.

        public int userId(string user)
        {
            Utilizador utilizadores = new Utilizador();
            Utilizador[] utilizadoresIniciais = utilizadores.UtilizadoresDefault(utilizadores.GenerateID());

            foreach (var userInfo in utilizadoresIniciais)
            {
                if (userInfo.NomeUtilizador == user)
                {
                    return userInfo.Id;
                }
            }
            return -1;
        }

        // Método que constroi o menu de pedidos da aplicação
        // A cada uma das funcionalidades corresponde o respetivo método de execução das mesmas que se se encomentram na classe Pedidos
        // neste método é feita a validação se a aplicação continuará a correr ou se será feito o logout, ou se o utilizador vai voltar ao menu principal


        public Tuple<bool, bool> ConstrutorMenuPedido(string user, bool continuarCorrer, bool voltarMenu, Pedido pedido)
        {
            int opcao = 0;
            while (opcao != 6)
            {
                bool opcaoInvalida = true;
                Utility2.EscreverTitulo("RSGymPT - Menu de Pedidos");
                Login.Log(user);
                Console.WriteLine();
                ListarMenu(SubMenuPedido);
                opcao = LerOpcao(" (numérica e entre 1 e 7)");

                switch (opcao)
                {
                    case 1:
                        Console.Clear();
                        Login.Log(user);
                        Console.WriteLine();
                        pedido.AdicionaPedido(Login.Log2(user), userId(user), pedido.LerCodPT(user), pedido.LerData(user));
                        opcaoInvalida = false;
                        break;
                    case 2:
                        Console.Clear();
                        ListarMenu(SubMenuPedidoAlterar);
                        opcao = LerOpcao(" (numérica e entre 1 e 2)");
                        while (opcao < 1 || opcao > 2)
                        {
                            Console.Clear();
                            ListarMenu(SubMenuPedidoAlterar);
                            opcao = LerOpcao(" (numérica e entre 1 e 2)");
                        }
                        if (opcao == 1)
                        {
                            Console.Clear();
                            Login.Log(user);
                            Console.WriteLine();
                            pedido.CancelarPedido(user, userId(user));
                        }
                        else if (opcao == 2)
                        {
                            Console.Clear();
                            Login.Log(user);
                            Console.WriteLine();
                            pedido.AlterarDataPedido(user, userId(user));
                        }
                        opcaoInvalida = false;
                        break;
                    case 3:
                        Console.Clear();
                        Login.Log(user);
                        Console.WriteLine();
                        pedido.RemoverPedido(user, userId(user));
                        opcaoInvalida = false;
                        break;
                    case 4:
                        Console.Clear();
                        Login.Log(user);
                        Console.WriteLine();
                        pedido.ConsultarPedidos(pedido.NumeroPedidos());
                        opcaoInvalida = false;
                        break;
                    case 5:
                        Console.Clear();
                        Login.Log(user);
                        Console.WriteLine();
                        pedido.TerminarPedido(user, userId(user));
                        opcaoInvalida = false;
                        break;
                    case 6:
                        Utility2.MensagemDepedida();
                        break;
                    case 7:
                        continuarCorrer = true;
                        voltarMenu = true;
                        opcaoInvalida = false;
                        Utility2.ReencaminharMenu();
                        return Tuple.Create(continuarCorrer, voltarMenu);
                }
                if (opcao != 6 && opcaoInvalida)
                {
                    Utility2.OpcaoValida();
                    Console.ReadKey();
                }
                else if (opcao != 6)
                {
                    Console.WriteLine("\nPressiona qualquer tecla para voltares novamente ao menu de pedido");
                    Console.ReadKey();
                }
            }
            continuarCorrer = false;
            voltarMenu = false;
            return Tuple.Create(continuarCorrer, voltarMenu);

        }

        // Método que constroi o menu de Personal Trainers quando o utilizador o seleciona
        // neste método é feita a validação se a aplicação continuará a correr ou se será feito o logout, ou se o utilizador vai voltar ao menu principal


        public Tuple<bool, bool> ConstrutorMenuPTs(string user, PersonalTrainer pts, PersonalTrainer[] ptsDeafault, bool continuarCorrer, bool voltarMenu)
        {
            Utility2.EscreverTitulo("RSGymPT - Menu de PT's");
            Login.Log(user);
            Console.WriteLine();
            ListarMenu(SubMenuPT);
            int opcao = LerOpcao("");

            while (opcao != 1 && opcao != 2)
            {
                Utility2.EscreverTitulo("RSGymPT - Menu de PT's");
                Login.Log(user);
                Utility2.OpcaoValida();
                ListarMenu(SubMenuPT);
                opcao = LerOpcao("");
            }
            if (opcao == 1)
            {
                Console.Clear();
                Login.Log(user);
                Console.WriteLine();
                pts.ListarPts(ptsDeafault);
                continuarCorrer = false;
                voltarMenu = true;
                Console.ReadKey();
                Utility2.ReencaminharMenu();
                return Tuple.Create(continuarCorrer, voltarMenu);
            }
            if (opcao == 2)
            {
                continuarCorrer = false;
                voltarMenu = true;
                Utility2.ReencaminharMenu();
                return Tuple.Create(continuarCorrer, voltarMenu);
            }
            voltarMenu = true;
            continuarCorrer = false;
            return Tuple.Create(continuarCorrer, voltarMenu);
        }

        // Método que constroi o menu de Personal Trainers quando o utilizador o seleciona
        // valida ainda a opção que o utilizador seleciona dentro do menu, e executa a funcionalidade correspondente
        // neste método é feita a validação se a aplicação continuará a correr ou se será feito o logout, ou se o utilizador vai voltar ao menu principal

        public Tuple<bool, bool> ConstrutorMenuUtilizador(string user, Utilizador[] utilizadores, Login login, bool continuarCorrer, bool voltarMenu)
        {
            Utility2.EscreverTitulo("RSGymPT - Menu de Utilizadores");
            Login.Log(user);
            Console.WriteLine();
            ListarMenu(SubMenuUtilizador);
            int opcao = LerOpcao(" (numérica e entre 1 e 3)");

            while (opcao < 1 || opcao > 3)
            {
                Utility2.EscreverTitulo("RSGymPT - Menu de Utilizadores");
                Login.Log(user);
                Utility2.OpcaoValida();
                ListarMenu(SubMenuUtilizador);
                opcao = LerOpcao(" (numérica e entre 1 e 3)");
            }

            if (opcao == 1)
            {
                Console.Clear();
                Login.Log(user);
                Console.WriteLine();
                Console.WriteLine("Lista de Utilizadores no RSGymPT:\n");
                foreach (var uti in utilizadores)
                {
                    Console.WriteLine($"Id User: {uti.Id}\tNome User: {uti.Nome}\tUtilizador: {uti.NomeUtilizador}");
                }
                continuarCorrer = false;
                voltarMenu = true;
                Console.ReadKey();
                Utility2.ReencaminharMenu();
                return Tuple.Create(continuarCorrer, voltarMenu);
            }
            else if (opcao == 2)
            {
                Console.WriteLine("\nObrigado por preferires o RSGymPT!\n");
                Console.WriteLine("Volta sempre...");
                continuarCorrer = true;
                voltarMenu = false;
                return Tuple.Create(continuarCorrer, voltarMenu);
            }
            voltarMenu = true;
            continuarCorrer = false;
            Utility2.ReencaminharMenu();
            return Tuple.Create(continuarCorrer, voltarMenu);
        }
    }
    #endregion
}

