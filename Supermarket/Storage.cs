using System;
using System.Collections.Generic;
using System.Linq;

namespace Supermarket
{
    class Storage
    {
        public List<Product> Products = new List<Product>();

        public List<Product> ProductGenerator(DateTime date)
        {
            //DateTime date = new DateTime();
           //date = DateTime.Today;
            Random rdm = new Random();
            int min = 1;
            int max = 20;
            List<Product> products = new List<Product>()
            {

                
                new Product("Battery A", "Small", rdm.Next(min, max), 7, date.AddDays(6)),
                new Product("'Orbit'", "Small", rdm.Next(min, max), 1, date.AddDays(3)),
                new Product("KitKat", "Small", rdm.Next(min, max), 3, date.AddDays(1)), 
                new Product("Battery AA", "Small", rdm.Next(min, max), 7, date.AddDays(6)),
                new Product("Condoms", "Small", rdm.Next(min, max), 3, date.AddDays(3)),
                new Product("Twix", "Small", rdm.Next(min, max), 3, date.AddDays(1)),
                new Product("Cookies", "Middle", rdm.Next(min, max), 3, date.AddDays(3)),
                new Product("Rise", "Middle", rdm.Next(min, max), 3, date.AddDays(1)),
                new Product("Soda 1.5", "Middle", rdm.Next(min, max), 4, date.AddDays(4)),
                new Product("Cola 2.0", "Middle", rdm.Next(min, max), 4, date.AddDays(4)),
                new Product("Sprite 2.0", "Middle", rdm.Next(min, max), 4, date.AddDays(4)),
               // new Product("Fanta 2.0", "Middle", rdm.Next(min, max), 4, date.AddDays(4)),
                new Product("Chocolate", "Middle", rdm.Next(min, max), 4, date.AddDays(2)),
                new Product("Fanta 2.0", "Middle", rdm.Next(min, max), 4, date.AddDays(4)),
                new Product("Milk 1.0", "Middle", rdm.Next(min, max), 3, date.AddDays(5)),
               // new Product("Chair", "Large", rdm.Next(min, max), 10, date.AddDays(7)),
                new Product("Pillow", "Large", rdm.Next(min, max), 10, date.AddDays(7)),
                new Product("Chair", "Large", rdm.Next(min, max), 10, date.AddDays(4)),
                new Product("Vase", "Large", rdm.Next(min, max), 15, date.AddDays(7)),  
                new Product("Computer", "Large", rdm.Next(min, max), 25, date.AddDays(7)),
                new Product("Microwave", "Large", rdm.Next(min, max), 30, date.AddDays(4))

            };


            Random randomOrderBy = new Random((int)DateTime.Now.Ticks);
            List<Product> storageList = products.OrderBy(x => randomOrderBy.Next()).ToList();


            return storageList;

        }


    }
}
