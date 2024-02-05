using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab14
{
    class PrimeCalculator
    {
        private readonly Thread thread;
        private readonly int n;
        private bool isPaused;

        public PrimeCalculator(int n)
        {
            this.n = n;
            thread = new Thread(CalculatePrimes);
        }
        public void SetThreadName(string name)
        {
            thread.Name = name;
        }

        public void Start()
        {
            thread.Start();
        }

        public void Pause()
        {
            isPaused = true;
        }

        public void Resume()
        {
            isPaused = false;
        }

        public void Join()
        {
            thread.Join();
        }

        public void DisplayThreadInfo()
        {
            Console.WriteLine($"\nИмя потока: {thread.Name}");
            Console.WriteLine($"Приоритет потока: {thread.Priority}");
            Console.WriteLine($"Статус потока: {thread.ThreadState}");
            Console.WriteLine($"ID потока: {thread.ManagedThreadId}\n");
            Console.WriteLine();
        }

        private void CalculatePrimes()
        {
            Console.WriteLine("Начало расчета простых чисел...");

            for (int i = 2; i <= n; i++)
            {
                if (isPaused)
                {
                    Console.WriteLine("Поток приостановлен. Ждем возобновления...");
                    while (isPaused)
                    {
                        Thread.Sleep(100);
                    }
                    Console.WriteLine("Поток возобновлен");
                }

                if (IsPrime(i))
                {
                    Console.WriteLine($"Простое число: {i}");
                    Thread.Sleep(500);
                }
            }

            Console.WriteLine("Завершение расчета простых чисел");
        }

        private bool IsPrime(int number)
        {
            if (number < 2)
                return false;

            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0)
                    return false;
            }

            return true;
        }
    }

}
