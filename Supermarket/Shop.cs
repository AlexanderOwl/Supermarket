using System;
using System.Collections.Generic;

namespace Supermarket
{
    class Shop
    {
        Shelf smallShelf = new Shelf();
        Shelf middleShelf = new Shelf();
        Shelf largeShelf = new Shelf();

        public List<Product> Welcome(DateTime date)
        {
            Console.WriteLine(date.ToShortDateString());
            Console.WriteLine("Hi there! Our store is open!");
            Storage storage = new Storage();
            List<Product> availableProducts = new List<Product>();
            availableProducts = storage.ProductGenerator();

            return availableProducts;
        }

        public void ShelfShow(/*Shelf sShelf, Shelf mShelf, Shelf lShelf, */ List<Product> availableProducts)
        {
            foreach (Product item in availableProducts)
            {
                if (item.Shelf.Size == "Large")
                {
                    largeShelf.Products.Add(item);
                }
                else if (item.Shelf.Size == "Middle")
                {
                    middleShelf.Products.Add(item);
                }
                else if (item.Shelf.Size == "Small")
                {
                    smallShelf.Products.Add(item);
                }
            }
            Console.WriteLine("Available products:");
            Console.WriteLine("Large shelf - ");
            foreach (Product item in largeShelf.Products)
            {
                Console.WriteLine($"{item.Name} ({item.Amount} pc.)");
            }
            Console.WriteLine("Middle shelf - ");
            foreach (Product item in middleShelf.Products)
            {
                Console.WriteLine($"{item.Name} ({item.Amount} pc.)");
            }
            Console.WriteLine("Small shelf - ");
            foreach (Product item in smallShelf.Products)
            {
                Console.WriteLine($"{item.Name} ({item.Amount} pc.)");
            }
            Pause();
        }

        public void Pause()
        {
            Console.WriteLine("...press any key");
            Console.ReadKey();
        }
        public List<Customer> Queue()
        {
            Random rdm = new Random();
            int queueNumber = rdm.Next(2, 5);
            string[] names = new string[] { "Mary", "Nency", "Michale", "Fred", "Jina", "Marcus", "Peter", "Helen", "Oliv" };
            List<Customer> customersQueue = new List<Customer>();
            for (int i = 0; i < queueNumber; i++)
            {
                Customer customer = new Customer(names[rdm.Next(0, 8)], i);
                //удалить одинаковых людей
                customersQueue.Add(customer);
            }
            return customersQueue;
        }

        public void Seller(List<Customer> customers, List<Product> availableProducts)
        {
            //List<Product> newProductsInShop = new List<Product>();
            //newProductsInShop = availableProducts;
            foreach (Customer item in customers)
            {
                Console.WriteLine($"\nHello, {item.Name}. What you would like to buy?");
                List<Product> toBuy = new List<Product>();
                int sum = 0;
                foreach (Product prod in item.ProductsList)
                {
                    Console.Write($"\n{prod.Name}, {prod.Amount} pc.");

                    foreach (var avProd in availableProducts)
                    {
                        if (avProd.Name == prod.Name)
                        {
                            if (avProd.Amount >= prod.Amount)
                            {
                                // Console.Write($" - costs {prod.Price}$");
                                sum += prod.Price * prod.Amount;
                                toBuy.Add(prod);
                            }
                            else
                            {
                                if (avProd.Amount < prod.Amount)
                                {
                                    Console.WriteLine($"\nSorry, but we don't have this product in this amount");
                                }
                                else
                                {
                                    Console.WriteLine($"\nSorry, but we don't have this product right now");
                                }
                                Pause();
                            }

                            break;
                        }
                    }
                }
                if (sum <= item.Cash)
                {
                    availableProducts = GenerateCheck(toBuy, sum, availableProducts, item.Cash);
                }
                else
                {
                    Console.WriteLine($"\nAmount: {sum}$ ");
                    Console.WriteLine($"\nSorry, but you have only {item.Cash}$, that's not enough.");
                    Pause();
                }
            }

            // return newProductsInShop;
        }

        public List<Product> GenerateCheck(List<Product> productsToBuy, int amount, List<Product> productsInShop, int cash)
        {
            Console.WriteLine("\n\nYour reciepe");
            foreach (Product prod in productsToBuy)
            {
                Console.WriteLine($"{prod.Name} (x{prod.Amount}) - {prod.Amount * prod.Price}$");

                foreach (var avProd in productsInShop)
                {
                    if (avProd.Name == prod.Name)
                    {
                        //int index = productsInShop.IndexOf(prod);
                        if (avProd.Amount == prod.Amount)
                        {
                            productsInShop.Remove(prod);
                        }
                        else
                        {
                            avProd.Amount = avProd.Amount - prod.Amount;
                        }

                        break;
                    }
                }
            }
            Console.WriteLine("-------------------------------");
            Console.WriteLine($"Amount: {amount}$ ");
            Console.WriteLine($"\n Yours {cash}, your change - {cash - amount}$ ");
            Pause();
            return productsInShop;
        }

    }
}
