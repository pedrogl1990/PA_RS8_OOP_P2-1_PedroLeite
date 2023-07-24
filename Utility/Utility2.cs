using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class Utility2
    {
        public static void UnicodeConsola()
        {
            Console.OutputEncoding = Encoding.UTF8;
        }

        public static void EscreverTitulo(string title)
        {
            Console.Clear();
            Console.ForegroundColor= ConsoleColor.Cyan;
            Console.WriteLine(new String('-', 30));
            Console.WriteLine(title.ToUpper());
            Console.WriteLine(new String('-', 30));
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void TerminarConsola()
        {
            Console.ReadLine();
            Console.WriteLine("\nObrigado por usares a app do RSGymPT");
            Console.ReadKey();
            Environment.Exit(0);
        }

        public static void OpcaoValida()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nEscolhe uma opção válida\n");
            Console.ResetColor();
        }

        public static void MensagemBoasVindas(string username)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Bem-vindo {username}!");
            Console.ResetColor();
            Console.WriteLine("\nLet's work like hell!");
            Console.ReadKey();
        }

        public static void MensagemDepedida()
        {
            Console.Clear();
            Console.WriteLine("Pena ver-te partir...volta rápido.\n");
            Console.WriteLine("Lembra-te, No Pain...No Gain!");
            Console.ReadKey();
            Environment.Exit(0);
        }

        public static void LeituraDaTaPT(string msg1, string msg2)
        {
            Console.WriteLine();
            Console.WriteLine($"{msg1}");
            Console.WriteLine($"{msg2}");
            Console.Write("--> ");
        }

        public static void NaoValido(string msg1)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{msg1}");
            Console.ResetColor();
        }

        public static void ReencaminharMenu()
        {
            Console.ForegroundColor= ConsoleColor.Green;
            Console.WriteLine("\nEstamos a reencaminhar-te para o menu principal");
            Console.ResetColor();
        }
    }
}
