using RSGymPT.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSGymPT.Interfaces
{
    internal interface IPedido
    {

        #region Properties

        int IdPedido { get; }

        string User { get; }

        DateTime DataHora { get; }

        string Estado { get; }

        int IdUser { get; }

        string CodPT { get;}

        List<Pedido> pedidos { get; }

        #endregion

        #region Methods

        string LerCodPT(string user);
        DateTime LerData(string user);
        void AdicionaPedido(string user, int idUser, string codPT, DateTime dataHora);
        void CancelarPedido(string user, int userId);
        void AlterarDataPedido(string user, int userId);
        void RemoverPedido(string user, int userId);
        int NumeroPedidos();
        void ConsultarPedidos(int contagemPedidos);
        void TerminarPedido(string user, int userId);

        #endregion
    }
}
