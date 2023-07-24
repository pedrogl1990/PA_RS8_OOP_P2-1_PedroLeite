using RSGymPT.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace RSGymPT.Interfaces
{
    internal interface INavegacao
    {

        #region Properties
        string[] MenuPrincipal { get;}
        string[] SubMenuPedido { get;}
        string[] SubMenuPedidoAlterar { get;}
        string[] SubMenuPT { get;}
        string[] SubMenuUtilizador { get;}

        #endregion

        #region Methods

        void ListarMenu(string[] menu);
        int LerOpcao(string ajuda);
        int ConstrutorMenuPrincipal(string user);
        int userId(string user);
        Tuple<bool, bool> ConstrutorMenuPedido(string user, bool log, bool voltarMenu, Pedido pedido);
        Tuple<bool, bool> ConstrutorMenuPTs(string user, PersonalTrainer pts, PersonalTrainer[] ptsDeafault, bool continuarCorrer, bool voltarMenu);
        Tuple<bool, bool> ConstrutorMenuUtilizador(string user, Utilizador[] utilizadores, Login login, bool continuarCorrer, bool voltarMenu);

        #endregion
    }
}
