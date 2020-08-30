using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket
{
    class Product
    {
        public string Name;
        public Shelf Shelf = new Shelf();
        public int Amount;
        public DateTime ExpirationDate;
        public int Price;

        public Product(string Name, string Shelf, int Amount, int Price, DateTime ExpirationDate)
        {
            this.Name = Name;
            this.Shelf.Size = Shelf;
            this.Amount = Amount;
            this.Price = Price;
            this.ExpirationDate = ExpirationDate;
        }
    }
}
