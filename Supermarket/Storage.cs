using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket
{
    class Storage
    {
        //public List<Product> Products = new List<Product>();

        public List<Product> ProductGenerator()
        {
            DateTime date = new DateTime();
            date = DateTime.Today;
            Random rdm = new Random();
            List<Product> Products = new List<Product>()
            {
                new Product("Cookies", "Middle", rdm.Next(1, 10), 3, date.AddDays(3)),
                new Product("Rise", "Middle", rdm.Next(1, 10), 3, date.AddDays(1)),
                new Product("Battery AAA", "Small", rdm.Next(1, 10), 7, date.AddDays(6)),
                new Product("'Orbit'", "Small", rdm.Next(1, 10), 1, date.AddDays(3)),
                new Product("KitKat", "Small", rdm.Next(1, 10), 3, date.AddDays(1)),
                new Product("Milk 1.0", "Large", rdm.Next(1, 10), 5, date.AddDays(5)),
                new Product("Soda 1.5", "Large", rdm.Next(1, 10), 5, date.AddDays(4))
            };
            return Products;
        }

        //static int AmountGenerator()
        //{
        //    Random rdm = new Random();
        //    int amount = rdm.Next(1, 10);
        //    return amount;
        //}
    }
}
