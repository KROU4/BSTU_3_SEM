using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab14
{
    static class Class1
    {
        public static void CalculateAndWriteToFile(int n)
        {
            using (var writer = new System.IO.StreamWriter("output.txt"))
            {
                for (int i = 1; i <= n; i++)
                {
                    Thread.Sleep(500);

                    writer.WriteLine(i);
                    Console.WriteLine($"Вычисление: {i}");
                }
            }
        }

    }
}
