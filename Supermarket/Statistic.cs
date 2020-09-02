using System.Collections.Generic;
using System;

namespace Supermarket
{
    class Statistic
    {
        public List<Product> TodaySold = new List<Product>();
        public Dictionary<int, List<Product>> WeeklyJournal = new Dictionary<int, List<Product>>();

        public void AddStatisticToDays(List<Product> sold)
        {
            if (TodaySold.Count == 0)
            {
                TodaySold = sold;
            }
            else
            {
                foreach (Product item in sold)
                {
                    bool repeat = false;
                    foreach (Product prod in TodaySold)
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
                        TodaySold.Add(item);
                    }
                }
            }
        }

        public void TodaySoldProducts(DateTime date, int iteration)
        {
            int amount = 0;
            if (TodaySold.Count == 0)
            {
                Console.WriteLine("\nNo data for your request!");
            }
            else
            {
                Console.WriteLine($"\nYesterday, {date.ToShortDateString()}, our store sold:");
                foreach (Product prod in TodaySold)
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
        }

        public void AddStatisticToWeeks(int day, List<Product> sold)
        {
            WeeklyJournal.Add(day, sold);
        }

        public void WeeklySold(int iter)
        {
            List<Product> weekDevision = new List<Product>();
            int amount = 0, generalAmount = 0;
            int count = 1;
            if (WeeklyJournal.Count == 0)
            {
                Console.WriteLine("\nNobody have bought smth yet!\n");
            }
            else
            {
                foreach (var pair in WeeklyJournal)
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
                    if (count % 7 != 0 && weeksNum == iter / 7 + 1)
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
                        if (count == iter)
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
        }
    }
}