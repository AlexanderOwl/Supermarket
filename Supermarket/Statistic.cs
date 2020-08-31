using System.Collections.Generic;

namespace Supermarket
{
    class Statistic
    {
        public List<Product> TodaySold = new List<Product>();
        public Dictionary<int, List<Product>> WeeklyJournal = new Dictionary<int, List<Product>>();
        public List<Product> WeeklySold = new List<Product>();
    }
}