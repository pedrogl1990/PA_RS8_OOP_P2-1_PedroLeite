using Microsoft.Win32;
using RSGymPT.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace RSGymPT.Classes
{
    internal class Pedido : IPedido
    {
        #region Properties

        private static int ultimoIdPedido = 0;

        public int IdPedido { get; }

        public string User { get; }

        public DateTime DataHora { get; set; }

        public string Estado { get; set; }

        public int IdUser { get; }

        public string CodPT { get; set; }

        List<Pedido> IPedido.pedidos => throw new NotImplementedException();

        public List<Pedido> pedidos;


        #endregion

        #region Constructors

        public Pedido()
        {
            IdUser = 0;
            DataHora = DateTime.Now;
            IdPedido = 0;
            User = "";
            Estado = "";
            CodPT = "";
            pedidos = new List<Pedido>();
        }

        public Pedido(string user, int idUser, string codPT, DateTime dataHora)
        {
            ultimoIdPedido++;
            IdPedido = ultimoIdPedido;
            IdUser = idUser;
            User = user;
            DataHora = dataHora;
            Estado = "AGENDADO";
            CodPT = codPT;
        }



        #endregion

        #region Methods

        // Método usado para ler e validar a sigla do Personal Trainer quando esta é solicitada
        // Possui uma funcionalidade extra que lista os PT's quando o user não sabe o código destes

        public string LerCodPT(string user)
        {
            PersonalTrainer personalTrainer = new PersonalTrainer();
            PersonalTrainer[] listaPts = personalTrainer.PTsDefault(personalTrainer.GenerateID());
            Utility2.LeituraDaTaPT("Insere a sigla do PT pretendido: ", "Não sabes a sigla do teu PT? Nós ajudamos! Escreve 'ajuda' para consultar!\n");
            CodPT = Console.ReadLine().ToUpper();
            if (CodPT == "AJUDA")
            {
                Console.WriteLine();
                personalTrainer.ListarPts(listaPts);
                Console.ReadKey();
            }
            var ptValido = listaPts.FirstOrDefault(pt => pt.SiglaPT == CodPT);

            while (ptValido == null)
            {
                Console.Clear();
                Login.Log(user);
                Console.WriteLine();
                Utility2.LeituraDaTaPT("Insere a sigla do PT pretendido: ", "Não sabes a sigla do teu PT? Nós ajudamos! Escreve 'ajuda' para consultar!\n");
                CodPT = Console.ReadLine().ToUpper();
                if (CodPT == "AJUDA")
                {
                    Console.WriteLine();
                    personalTrainer.ListarPts(listaPts);
                    Console.ReadKey();
                }
                ptValido = listaPts.FirstOrDefault(pt => pt.SiglaPT == CodPT);
            }
            return CodPT;
        }

        // Método usado para ler e validar a data para as sessões quando esta é solicitada.
        // Se a data for inferior á data do dia ou tiver um formato inválido, a mesma será constantemente pedida.

        public DateTime LerData(string user)
        {
            DateTime dataHora;
            bool dataValida;
            Console.Clear();
            Login.Log(user);
            Utility2.LeituraDaTaPT("\nInsire a Data e Horas pretendidas", "(formato -> dd/MM/aaaa HH:mm):\n");
            dataValida = DateTime.TryParse(Console.ReadLine(), out dataHora);
            while (!dataValida || dataHora < DateTime.Now)
            {
                Console.Clear();
                Login.Log(user);
                Utility2.LeituraDaTaPT("\nInsire a Data e Horas pretendidas", "(formato -> dd/MM/aaaa HH:mm):\n");
                dataValida = DateTime.TryParse(Console.ReadLine(), out dataHora);
            }
            return dataHora;
        }

        //Método para criar um novo pedido de sessão de PT com todas as informações necessárias

        public void AdicionaPedido(string user, int idUser, string codPT, DateTime dataHora)
        {
            pedidos.Add(new Pedido(user, idUser, codPT, dataHora));
            Console.WriteLine("\nPedido adicionado com sucesso");
            Console.ReadKey();
        }

        // Método que cancela um pedido desde que este esteja "AGENDADO", pertença ao usuário que solicita este cancelamento e cujo o ID exista

        public void CancelarPedido(string user, int userId)
        {
            bool idValido;
            int idPedido;
            Console.Write("Id do pedido a cancelar: ");
            idValido = Int32.TryParse(Console.ReadLine(), out idPedido);
            while (!idValido || idPedido < 0)
            {
                Console.Clear();
                Login.Log(user);
                Utility2.NaoValido("\nInsere um id válido (numérico e superior a 0)\n");
                Console.Write("Id do pedido a cancelar: ");
                idValido = Int32.TryParse(Console.ReadLine(), out idPedido);
            }
            var pedido = pedidos.FirstOrDefault(p => p.IdPedido == idPedido && p.Estado == "AGENDADO" && p.IdUser == userId);
            if (pedido != null)
            {
                pedido.Estado = "CANCELADO";
                Console.WriteLine("\nPedido cancelado com sucesso");
            }
            else {
                Console.WriteLine("\nPossiveis erros:");
                Utility2.NaoValido("Id não encontrado\nPedido não se encontra 'Agendado'\nEste pedido não te pertence");
            }
        }

        // Método que altera a data de um pedido desde que este esteja "AGENDADO", pertença ao usuário que solicita esta alteração e cujo o ID exista


        public void AlterarDataPedido(string user, int userId)
        {
            bool idValido;
            int idPedido;
            Console.Write("Id do pedido a alterar: ");
            idValido = Int32.TryParse(Console.ReadLine(), out idPedido);
            while (!idValido || idPedido < 0)
            {
                Console.Clear();
                Login.Log(user);
                Utility2.NaoValido("\nInsere um id válido (numérico ou superior a 0)\n");
                Console.Write("Id do pedido a alterar: ");
                idValido = Int32.TryParse(Console.ReadLine(), out idPedido);
            }
            var pedido = pedidos.FirstOrDefault(p => p.IdPedido == idPedido && p.Estado == "AGENDADO" && p.IdUser == userId);
            if (pedido != null)
            {
                pedido.DataHora = LerData(user);
                Console.WriteLine("\nPedido alterado com sucesso");
            }
            else
            {
                Console.WriteLine("\nPossiveis erros:");
                Utility2.NaoValido("Id não encontrado\nPedido não se encontra 'Agendado'\nEste pedido não te pertence");
            }
        }

        // Método que remove em definitivo um pedido desde que este esteja "AGENDADO", pertença ao usuário que solicita esta eliminação e cujo o ID exista

        public void RemoverPedido(string user, int userId)
        {
            bool idValido;
            int idPedido;
            Console.Write("Id do pedido a remover: ");
            idValido = Int32.TryParse(Console.ReadLine(), out idPedido);
            while (!idValido || idPedido < 0)
            {
                Console.Clear();
                Login.Log(user);
                Utility2.NaoValido("\nInsere um id válido (numérico ou superior a 0)\n");
                Console.Write("Id do pedido a remover: ");
                idValido = Int32.TryParse(Console.ReadLine(), out idPedido);
            }
            var pedido = pedidos.FirstOrDefault(p => p.IdPedido == idPedido && p.Estado == "AGENDADO" && p.IdUser == userId);
            if (pedido != null)
            {
                pedidos.Remove(pedido);
                Console.WriteLine("\nPedido removido com sucesso");
            } else
            {
                Console.WriteLine("\nPossiveis erros:");
                Utility2.NaoValido("Pedido não agendado\nPedido não encontrado\nEste pedido não te pertence");
            }
        }

        // Método que criei para contar o numero de pedidos para controlar a mensagem que aparece no ato de consulta caso ainda não existam quaisqueres pedidos
        public int NumeroPedidos()
        {
            int contagemPedidos;
            return contagemPedidos = pedidos.Count();
        }

        // Método que consulta todos os pedidos de sessões de PT's existentes. Caso não haja nenhuma sessão marcada mostra uma mensagem a dizer que não tem sessões criadas.
        public void ConsultarPedidos(int contagemPedidos)
        {
            if (contagemPedidos > 0)
            {
                Console.WriteLine("Listagem de pedidos no RSGymPT:\n");
                foreach (var pedido in pedidos)
                {
                    Console.WriteLine($"Id Pedido: {pedido.IdPedido} | Id Usuário: {pedido.IdUser} | Sigla PT: {pedido.CodPT} | Data e hora do pedido: {pedido.DataHora} | Estado: {pedido.Estado}");
                }
            }
            else
            {
                Utility2.NaoValido("Não tens pedidos de sessões criadas");
            }
        }

        // Método que termina um pedido desde que este esteja "AGENDADO", pertença ao usuário que solicita este término e cujo o ID exista. Altera ainda a data para a data atual com a hora do término


        public void TerminarPedido(string user, int userId)
        {
            bool idValido;
            int idPedido;
            Console.Write("Id do pedido a terminar: ");
            idValido = Int32.TryParse(Console.ReadLine(), out idPedido);
            while (!idValido || idPedido < 0)
            {
                Console.Clear();
                Login.Log(user);
                Utility2.NaoValido("\nInsere um id válido (numérico ou superior a 0)\n");
                Console.Write("Id do pedido a terminar: ");
                idValido = Int32.TryParse(Console.ReadLine(), out idPedido);
            }
            var pedido = pedidos.FirstOrDefault(p => p.IdPedido == idPedido && p.Estado == "AGENDADO" && p.IdUser == userId);
            if (pedido != null)
            {
                pedido.Estado = "TERMINADO";
                pedido.DataHora = DateTime.Now;
                Console.WriteLine("\nPedido terminado com sucesso");
            }
            else
            {
                Console.WriteLine("\nPossiveis erros:");
                Utility2.NaoValido("Id não encontrado\nPedido não se encontra 'Agendado'\nEste pedido não te pertence");
            }
        }

        #endregion
    }
}
