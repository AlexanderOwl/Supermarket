using System;
using System.Collections.Generic;

public class Statistic
{
    public List<Product> TodaySold;// = new List<Product>();
    public Dictionary<int, List<Product>> WeeklyJournal = new Dictionary<int, List<Product>>();
    public List<Product> weeklySold = new List<Product>();

    public Statistic()
	{
	}
}
