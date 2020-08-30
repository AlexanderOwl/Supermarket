using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Supermarket
{
    class Program
    {
        static void Main(string[] args)
        {
            Shop shop = new Shop();
            List<Product> productsInShop = new List<Product>();
            List<Customer> customers = new List<Customer>();
            DateTime date = new DateTime();
            date = DateTime.Today;
         
            // do
            // {
            productsInShop = shop.Welcome(date);
            shop.ShelfShow(productsInShop);
            customers = shop.Queue();
            shop.Seller(customers, productsInShop);
            date.AddDays(1);
            //} while();
        }
        
    }
}
