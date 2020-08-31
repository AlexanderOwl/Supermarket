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
        private Statistic _statistic = new Statistic();
        private int _iteration = 1;
      
        public void Menu(List<Product> productsInShop, ref DateTime date, List<Customer> customers)
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine($"{date.DayOfWeek}, {date.ToShortDateString()}");
            Console.WriteLine("Welcome to console SuperPuperMarket!");
            Console.WriteLine("0 - exit" +
                "\n1 - day stat" +
                "\n2 - week stat" +
                "\n3 - open cashdesk" +
                "\n4 - next day");
            char key = Console.ReadKey().KeyChar;
            switch (key)
            {
                case '1':
                    {
                        // статистика дня
                        TodaySoldProducts(date, _iteration);
                        break;
                    }
                case '2':
                    {
                        // статистика недели
                        WeeklySold();
                        break;
                    }
                case '3':
                    {
                        Console.WriteLine();
                        OpenShop(productsInShop, date, customers);
                        date = date.AddDays(1);
                        //добавить день
                        break;
                    }
                case '4':
                    {
                        date = date.AddDays(1);
                        //добавить день
                        break;
                    }
                default: break;
            }
            _iteration++;
        }

        public void OpenShop(List<Product> productsInShop, DateTime date, List<Customer> customers)
        {
            productsInShop = Welcome(date);
            ShelfShow(productsInShop);
            customers = Queue();
            Seller(customers, productsInShop);
            date.AddDays(1);
        }

        public List<Product> Welcome(DateTime date)
        {
            Console.WriteLine(date.DayOfWeek+", "+date.ToShortDateString());
          ///--------------------------------------------------
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
            string[] names = new string[] { "Mary", "Nency", "Michale", "Fred", "Jina", "Marcus", "Peter", "Helen", "Oliv" };
            List<Customer> customersQueue = new List<Customer>();
            for (int i = 0; i < 2; i++)
            {
                Customer customer = new Customer(names[rdm.Next(0, 8)], i);
                //удалить одинаковых людей
                customersQueue.Add(customer);
            }
            return customersQueue;
        }

        public void Seller(List<Customer> customers, List<Product> availableProducts)
        {
            _statistic.TodaySold = new List<Product>();
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
                    availableProducts = GenerateCheck(toBuy, sum, availableProducts, item.Cash, true);
                }
                else
                {
                    GenerateCheck(toBuy, sum, availableProducts, item.Cash, false);
                    Console.WriteLine($"\nSorry, but you have only {item.Cash}$, that's not enough.");
                    Pause();
                }
            }
            List<Product> val = new List<Product>();
            val = _statistic.TodaySold;
            _statistic.WeeklyJournal.Add(_iteration, val);                        
        }

        public List<Product> GenerateCheck(List<Product> productsToBuy, int amount, List<Product> productsInShop, int cash, bool enoughMoney)
        {            
            List<Product> toBuy = new List<Product>();
            Console.WriteLine("\n\nYour reciepe");
            foreach (Product prod in productsToBuy)
            {
                Console.WriteLine($"{prod.Name} (x{prod.Amount}) - {prod.Amount * prod.Price}$");
                if (enoughMoney)
                {
                    toBuy.Add(prod);
                    foreach (var avProd in productsInShop)
                    {
                        if (avProd.Name == prod.Name)
                        {
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
            if (enoughMoney) 
            {
                AddStatisticToDays(toBuy);
                Console.WriteLine($"\n Yours {cash}, your change - {cash - amount}$ ");
                Pause();
            }
            return productsInShop;
        }
        
        public void AddStatisticToDays(List<Product> sold)
        {
            if (_statistic.TodaySold.Count == 0)
            {
                _statistic.TodaySold = sold;
            }
            else
            {
                foreach (Product item in sold)
                {
                    bool repeat = false;
                    foreach (Product prod in _statistic.TodaySold)
                    {
                        if (prod.Name == item.Name)
                        {
                            prod.Amount += item.Amount;
                            repeat = true;
                            break;
                        }
                    }
                    if (!repeat)
                    {
                        _statistic.TodaySold.Add(item);
                    }
                }
            }
        }

        public void TodaySoldProducts(DateTime date, int iteration)
        {
            int amount = 0;
            if (_statistic.TodaySold.Count == 0 && _iteration == 1)
            {
                Console.WriteLine("\nOur shop haven't worked yet!");
            }
            else if (_statistic.TodaySold.Count == 0 && _iteration > 1)
            {
                Console.WriteLine("\nNobody have bought smth yet!");
            }
            else
            {
                Console.WriteLine("\nToday our store sold:");
                foreach (Product prod in _statistic.TodaySold)
                {
                    amount += prod.Price * prod.Amount;
                    Console.WriteLine($"{prod.Name}\t{prod.Amount}\t{prod.Price * prod.Amount}");
                }
                Console.WriteLine("-------------------------------");
                Console.WriteLine($"Amount: {amount}$ \n");
            }
            Pause();
        }

        public void WeeklySold()
       {
            List<Product> weekDevision = new List<Product>();
            int amount = 0, generalAmount = 0;
            int count = 1;
            if (_statistic.WeeklyJournal.Count == 0)
            {
                Console.WriteLine("\nNobody have bought smth yet!\n");
            }
            else
            {
                
                foreach (var pair in _statistic.WeeklyJournal)
                {

                    int weeksNum = count / 7 + 1;
                    if (count % 7 == 1)
                    {
                        weekDevision = pair.Value;
                        if (count != 1)
                        {
                            Console.WriteLine("-------------------------------");
                            Console.WriteLine($"Amount: {amount}$ ");
                            generalAmount += amount;
                            amount = 0;
                        }
                        Console.WriteLine($"\n\nWeek {weeksNum}:");
                    }
                    if (count % 7 == 0)
                    {
                        foreach (Product prod in weekDevision)
                        {
                            Console.WriteLine($"{prod.Name}\t{prod.Amount}\t{prod.Price * prod.Amount}$");
                            amount += prod.Price * prod.Amount;
                        }
                    }
                    else if (_iteration % 7 != 0 && weeksNum == _iteration / 7 + 1)
                    {
                        foreach (Product prod in weekDevision)
                        {
                            Console.WriteLine($"{prod.Name}\t{prod.Amount}\t{prod.Price * prod.Amount}$");
                            amount += prod.Price * prod.Amount;
                            generalAmount += amount;
                        }
                    }
                    count++;
                }
                Console.WriteLine("-------------------------------");
                Console.WriteLine($"\nGeneral: {generalAmount}$ \n");
            }
        }
    }  
}
