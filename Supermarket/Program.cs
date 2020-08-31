using System;
using System.Collections.Generic;


namespace Supermarket
{
    class Program
    {
        static DateTime date = new DateTime();
        static void Main(string[] args)
        {
            Shop shop = new Shop();
            List<Product> productsInShop = new List<Product>();
            List<Customer> customers = new List<Customer>();            
            date = DateTime.Today;
            do
            {
                shop.Menu(productsInShop, ref date, customers);
                Console.Clear();
            } while(true);
        }

    }
}
