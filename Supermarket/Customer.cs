using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket
{
    class Customer
    {
        public List<Product> ProductsList = new List<Product>();
        public int Cash;
        public int QueueNumber;
        public string Name;
        private Storage _storage = new Storage();

        public Customer(string Name, int QueueNumber)
        {
            this.QueueNumber = QueueNumber;
            this.Name = Name;
            this.ProductsList = GenerateBuyList();
            this.Cash = AmountGenerator(20, 100);
        }

        public List<Product> GenerateBuyList()
        {
            List<Product> possibleProducts = _storage.ProductGenerator();
            Random rdm = new Random();
            foreach (Product item in possibleProducts)
            {
                if (AmountGenerator(0, 5) <= 4)
                {
                    item.Amount = rdm.Next(1, 5);
                    ProductsList.Add(item);
                }
            }
            return ProductsList;
        }

        int AmountGenerator(int min, int max)
        {
            Random rdm = new Random();
            int amount = rdm.Next(min, max);
            return amount;
        }
    }
}
