using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

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
            List<Product> availableProducts = storage.ProductGenerator();
            return availableProducts;
        }

        public void ShelfShow(List<Product> availableProducts)
        {

            largeShelf.Products = availableProducts.Where(product => product.Size == "Large").ToList();
            middleShelf.Products = availableProducts.Where(product => product.Size == "Middle").ToList();
            smallShelf.Products = availableProducts.Where(product => product.Size == "Small").ToList();
            Random randomRemoveRange = new Random((int)DateTime.Now.Ticks);
            largeShelf.Products.RemoveRange(2, randomRemoveRange.Next(0, largeShelf.Products.Count - 2));
            Thread.Sleep(20);
            middleShelf.Products.RemoveRange(2, randomRemoveRange.Next(0, middleShelf.Products.Count - 2));
            Thread.Sleep(20);
            smallShelf.Products.RemoveRange(2, randomRemoveRange.Next(0, smallShelf.Products.Count - 2));

            Console.WriteLine();
            Console.WriteLine("GOODS");
            Console.Write("S");
            int index = 1;
            foreach (Product item in smallShelf.Products)
            {
                Console.Write($"| {item.Name} ({item.Amount} pc.) \t|");
                if (index % 3 == 0 && smallShelf.Products.Count != index)
                {
                    Console.WriteLine();
                    Console.Write(" ");
                }
                index++;
            }

            Console.WriteLine();
            Console.WriteLine("  _______________________________________________________________________");
            Console.Write("M");
            index = 1;
            foreach (Product item in middleShelf.Products)
            {
                Console.Write($"| {item.Name} ({item.Amount} pc.) \t|");
                if (index % 3 == 0 && middleShelf.Products.Count != index)
                {
                    Console.WriteLine();
                    Console.Write(" ");
                }
                index++;
            }
            Console.WriteLine();
            Console.WriteLine("  _______________________________________________________________________");
            Console.Write("L");
            index = 1;
            foreach (Product item in largeShelf.Products)
            {
                Console.Write($"| {item.Name} ({item.Amount} pc.) \t|");
                if (index % 3 == 0 && largeShelf.Products.Count != index)
                {
                    Console.WriteLine();
                    Console.Write(" ");
                }
                index++;
            }
            Console.WriteLine();

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

                                sum += prod.Price * prod.Amount;
                                toBuy.Add(prod);
                            }
                            else
                            {
                                if (avProd.Amount < prod.Amount)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($" Sorry, we have just {avProd.Amount} pc");
                                    Console.ResetColor();
                                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                                    Console.WriteLine($"Want buy? y/n");
                                    Console.ResetColor();
                                    ConsoleKey key = Console.ReadKey().Key;
                                    if (key == ConsoleKey.Y)
                                    {
                                        sum += avProd.Price * avProd.Amount;
                                        toBuy.Add(avProd);
                                    }
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"\nSorry, but we don't have this product right now");
                                    Console.ResetColor();
                                    Pause();
                                }

                            }

                            break;
                        }
                    }
                }
                if (sum <= item.Cash)
                {
                    availableProducts = GenerateCheck(toBuy, sum, availableProducts, item.Cash,true);
                }
                else
                {
                    GenerateCheck(toBuy, sum, availableProducts, item.Cash, false);
                    Console.WriteLine($"\nAmount: {sum}$ ");
                    Console.WriteLine($"\nSorry, but you have only {item.Cash}$, that's not enough.");
                    Pause();
                }
            }

            // return newProductsInShop;
        }

        public List<Product> GenerateCheck(List<Product> productsToBuy, int amount, List<Product> productsInShop, int cash, bool enoughMoney)
        {
            Console.WriteLine("\n\nYour reciepe");
            foreach (Product prod in productsToBuy)
            {
                Console.WriteLine($"{prod.Name} (x{prod.Amount}) - {prod.Amount * prod.Price}$");
                if (enoughMoney)
                {
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
            }
            Console.WriteLine("-------------------------------");
            Console.WriteLine($"Amount: {amount}$ ");
            Console.WriteLine($"\n Yours {cash}, your change - {cash - amount}$ ");
            Pause();
            return productsInShop;
        }

    }
}
