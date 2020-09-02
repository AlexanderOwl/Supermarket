using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Supermarket
{
    class Shop
    {
        Shelf smallShelf = new Shelf();
        Shelf middleShelf = new Shelf();
        Shelf largeShelf = new Shelf();
        private Statistic _statistic = new Statistic();
        private int _iteration = 0;

        public List<Product> Menu(List<Product> productsInShop, ref DateTime date, List<Customer> customers)
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine($"{date.DayOfWeek}, {date.ToShortDateString()}");
            //List<Product> availableProducts = new ProductGenerator();
            Console.WriteLine("Welcome to console SuperPuperMarket!");
            Console.WriteLine("0 - exit" +
                "\n1 - day stat" +
                "\n2 - week stat" +
                "\n3 - open cashdesk"); 
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
                        productsInShop = OpenShop(productsInShop, date, customers);
                        date = date.AddDays(1);
                        // добавить день
                        break;
                    }
                case '0':
                    {
                        // выйти из приложения
                        Environment.Exit(0);
                        break;
                    }
                default: break;
            }
            return productsInShop;
        }

        public List<Product> OpenShop(List<Product> productsInShop, DateTime date, List<Customer> customers)
        {
            Welcome(date);
            productsInShop = ShelfShow(productsInShop);
            customers = Queue();
            productsInShop = Seller(customers, productsInShop);
            date.AddDays(1);
            _iteration++;
            return productsInShop;
        }

        public void Welcome(DateTime date)
        {
            Console.WriteLine(date.DayOfWeek + ", " + date.ToShortDateString());
            Console.WriteLine("Hi there! Our store is open!");
        }

        public List<Product> ShelfShow(List<Product> availableProducts)
        {
            Console.Clear();
            Storage storage = new Storage();
            if (availableProducts.Count == 0)
            {
                availableProducts = storage.ProductGenerator();
                //}
                largeShelf.Products = availableProducts.Where(product => product.Size == "Large").ToList();
                middleShelf.Products = availableProducts.Where(product => product.Size == "Middle").ToList();
                smallShelf.Products = availableProducts.Where(product => product.Size == "Small").ToList();
                Random randomRemoveRange = new Random((int)DateTime.Now.Ticks);
                largeShelf.Products.RemoveRange(2, randomRemoveRange.Next(0, largeShelf.Products.Count - 2));
                Thread.Sleep(20);
                middleShelf.Products.RemoveRange(2, randomRemoveRange.Next(0, middleShelf.Products.Count - 2));
                Thread.Sleep(20);
                smallShelf.Products.RemoveRange(2, randomRemoveRange.Next(0, smallShelf.Products.Count - 2));
                availableProducts.Clear();
                foreach (Product prod in smallShelf.Products)
                {
                    availableProducts.Add(prod);
                }
                foreach (Product prod in middleShelf.Products)
                {
                    availableProducts.Add(prod);
                }
                foreach (Product prod in largeShelf.Products)
                {
                    availableProducts.Add(prod);
                }
            }
            else
            {
                largeShelf.Products = availableProducts.Where(product => product.Size == "Large").ToList();
                middleShelf.Products = availableProducts.Where(product => product.Size == "Middle").ToList();
                smallShelf.Products = availableProducts.Where(product => product.Size == "Small").ToList();
                availableProducts.Clear();
                foreach (Product prod in smallShelf.Products)
                {
                    availableProducts.Add(prod);
                }
                foreach (Product prod in middleShelf.Products)
                {
                    availableProducts.Add(prod);
                }
                foreach (Product prod in largeShelf.Products)
                {
                    availableProducts.Add(prod);
                }
            }
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
            return availableProducts;
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

        public List<Product> Seller(List<Customer> customers, List<Product> availableProducts)
        {
            _statistic.TodaySold = new List<Product>();
            foreach (Customer item in customers)
            {
                Console.WriteLine($"\nHello, {item.Name}. What you would like to buy?");
                List<Product> toBuy = new List<Product>();
                int sum = 0;                
                foreach (Product prod in item.ProductsList)
                {
                    bool found = false;
                    Console.Write($"\n{prod.Name}, {prod.Amount} pc.");

                    foreach (var avProd in availableProducts)
                    {
                        if (avProd.Name == prod.Name)
                        {
                            found = true;
                            if (avProd.Amount >= prod.Amount)
                            {

                                sum += prod.Price * prod.Amount;
                                toBuy.Add(prod);
                            }
                            else if (avProd.Amount < prod.Amount)                                
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
                            break;
                        }
                    }
                    if (!found)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"\nSorry, but we don't have this product right now");
                        Console.ResetColor();
                        Pause();
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
            List<Product> val = new List<Product>(_statistic.TodaySold);
            AddStatisticToWeeks(_iteration, val);
            return availableProducts;
        }

        public List<Product> GenerateCheck(List<Product> productsToBuy, int amount, List<Product> productsInShop, int cash, bool enoughMoney)
        {           
            if (productsToBuy.Count == 0)
            {
                Console.WriteLine("Please, visit us next time!");
                Pause();
            }
            else
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
                    AddStatisticToDays(productsToBuy);
                    Console.WriteLine($"\n Yours {cash}, your change - {cash - amount}$ ");
                    Pause();
                }
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
            if (_statistic.TodaySold.Count == 0)
            {
                Console.WriteLine("\nNo data for your request!");
            }
            else
            {
                Console.WriteLine($"\nYesterday, {date.ToShortDateString()}, our store sold:");
                foreach (Product prod in _statistic.TodaySold)
                {
                    amount += prod.Price * prod.Amount;
                    Console.Write(prod.Name);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(" x ");
                    Console.ResetColor();
                    Console.Write(prod.Amount);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(" = ");
                    Console.ResetColor();
                    Console.Write(prod.Price * prod.Amount + "$");
                    Console.WriteLine();
                }
                Console.WriteLine("-------------------------------");
                Console.WriteLine($"Amount: {amount}$ \n");
            }
            Pause();
        }

        public void AddStatisticToWeeks(int day, List<Product> sold)
        {
            _statistic.WeeklyJournal.Add(day, sold);
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
                        weekDevision.Clear();
                        if (count != 1) amount = 0;
                        Console.WriteLine($"\nWeek {weeksNum}:\n");
                    }
                    /// Работаем с неполной неделей (которая сейчас)
                    /// сначала наполняем список разбиения по неделям "weekDevision"
                    if (count % 7 != 0 && weeksNum == _iteration / 7 + 1)
                    {
                        foreach (Product prod in pair.Value)
                        {
                            bool repeat = false;
                            foreach (Product item in weekDevision)
                            {
                                if (prod.Name == item.Name)
                                {
                                    int n = prod.Amount;
                                    item.Amount += n;
                                    repeat = true;
                                    break;
                                }
                            }
                            if (!repeat)
                            {
                                Product unicProd = new Product(prod.Name, prod.Size, prod.Amount, prod.Price, prod.ExpirationDate);
                                weekDevision.Add(unicProd);
                            }
                        }
                        // Выводим список разбиения по неделям "weekDevision", когда достигли последнего дня
                        if (count == _iteration)
                        {
                            foreach (Product prod in weekDevision)
                            {
                                Console.WriteLine($"{prod.Name}\t{prod.Amount}\t{prod.Price * prod.Amount}$");
                                amount += prod.Price * prod.Amount;
                            }
                            generalAmount += amount;
                            Console.WriteLine("-------------------------------");
                            Console.WriteLine($"Amount: {amount}$ \n");
                        }
                    }
                    // Работаем с полной неделей
                    else
                    {
                        foreach (Product prod in pair.Value)
                        {
                            bool repeat = false;
                            foreach (Product item in weekDevision)
                            {
                                if (prod.Name == item.Name)
                                {
                                    int n = prod.Amount;
                                    item.Amount += n;
                                    repeat = true;
                                    break;
                                }
                            }
                            if (!repeat)
                            {
                                Product unicProd = new Product(prod.Name, prod.Size, prod.Amount, prod.Price, prod.ExpirationDate);
                                weekDevision.Add(unicProd);
                            }
                        }

                        // Выводим список разбиения по неделям "weekDevision", когда достигли последнего дня
                        if (count % 7 == 0)
                        {
                            foreach (Product prod in weekDevision)
                            {
                                Console.WriteLine($"{prod.Name}\t{prod.Amount}\t{prod.Price * prod.Amount}$");
                                amount += prod.Price * prod.Amount;
                            }
                            generalAmount += amount;
                            Console.WriteLine("-------------------------------");
                            Console.WriteLine($"Amount: {amount}$ \n");
                        }
                    }
                    count++;
                }
                Console.WriteLine("-------------------------------");
                Console.WriteLine($"\nGeneral: {generalAmount}$ \n");
            }
            Pause();
        }
    }
}
