using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket
{
    public class Password
    {
        private static string _pass = "     ";
        static public bool askpass()
        {
            int askTry = 0;                 
            do
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("LOCKED ");
                Console.ResetColor();
                Console.Write("Input password: ");
                List<char> passChar = new List<char>();
                while (true)
                {
                    ConsoleKeyInfo cki = Console.ReadKey(true);
                    if (cki.Key == ConsoleKey.Enter)
                        break;
                    else
                    {
                        Console.Write("*");
                        passChar.Add(cki.KeyChar);
                    }
                }
                Console.WriteLine();
                string passStr = null;
                foreach (char c in passChar)
                    passStr += c;
                if (_pass == passStr)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Access allowed");
                    Console.ResetColor();
                    Console.ReadKey(true);
                    Console.Clear();
                    return true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Пароль не правильный");
                    Console.ResetColor();
                    Console.ReadKey(true);
                    Console.Clear();
                    askTry++;                  
                    
                }
            } while (askTry < 3);
            return false;
        }
    }


}
