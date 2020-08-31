using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            this.ProductsList = ShopingListGenerator(_storage.ProductGenerator());
            this.Cash = AmountGenerator(20, 1000);
        }
        static List<Product> ShopingListGenerator(List<Product> uvailableShopProducts)
        {
            int minNumOfPositions = uvailableShopProducts.Count/2;
            int maxNumOfPositions = uvailableShopProducts.Count;
            Random rdm = new Random();
            int NumOfPositions = rdm.Next(minNumOfPositions, maxNumOfPositions);
            Random randomOrderBy = new Random();
            List<Product> shopingList = uvailableShopProducts.OrderBy(x => randomOrderBy.Next()).ToList();
            shopingList.RemoveRange(0, NumOfPositions);
            Random amountRandom = new Random((int)DateTime.Now.Ticks);
            foreach (var product in shopingList)
            {
                
                foreach (var storageProduct in uvailableShopProducts)
                {
                    if(product == storageProduct)
                    {
                        product.Amount = amountRandom.Next(1, storageProduct.Amount/2);
                        Thread.Sleep(20);
                        break;
                    }
                }
            }
            return shopingList;
        }

        int AmountGenerator(int min, int max)
        {
            Random rdm = new Random();
            int amount = rdm.Next(min, max);
            return amount;
        }
    }
}
