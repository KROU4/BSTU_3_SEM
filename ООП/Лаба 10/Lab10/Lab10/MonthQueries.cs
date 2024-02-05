using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    public class MonthQueries
    {
        private readonly string[] months;

        public MonthQueries(string[] months)
        {
            this.months = months;
        }

        public void DisplayMonthsWithLength(int n)
        {
            var monthsWithLengthN = months.Where(month => month.Length == n);
            Console.WriteLine($"Месяцы с длиной строки равной {n}:");
            foreach (var month in monthsWithLengthN)
            {
                Console.WriteLine(month);
            }
        }

        public void DisplaySummerWinterMonths()
        {
            var summerWinterMonths = months.Where(month => month == "June" || month == "July" || month == "December" || month == "January" || month == "February");
            Console.WriteLine("\nЛетние и зимние месяцы:");
            foreach (var month in summerWinterMonths)
            {
                Console.WriteLine(month);
            }
        }

        public void DisplaySortedMonths()
        {
            var sortedMonths = months.OrderBy(month => month);
            Console.WriteLine("\nМесяцы в алфавитном порядке:");
            foreach (var month in sortedMonths)
            {
                Console.WriteLine(month);
            }
        }

        public void DisplayMonthsWithU()
        {
            var monthsWithU = months.Where(month => month.Contains('u') && month.Length >= 4).Count();
            Console.WriteLine($"\nКоличество месяцев, содержащих 'u' и имеющих длину не менее 4-х: {monthsWithU}");
        }
    }
}