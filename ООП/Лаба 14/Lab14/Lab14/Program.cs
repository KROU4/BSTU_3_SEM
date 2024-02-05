using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace Lab14
{
    class Program
    {
        static void Main()
        {

            /*            // Задание 1

                        string filePath = "C:\\Users\\KROU4\\YandexDisk\\Уник\\ООП\\Лаба 14\\Lab14\\TextFile1.txt";

                        Process[] processes = Process.GetProcesses();

                        using (StreamWriter writer = new(filePath, false))
                        {
                            foreach (Process process in processes)
                            {
                                try
                                {
                                    string line = $"ID: {process.Id}, Имя: {process.ProcessName}, Приоритет: {process.BasePriority}, " +
                                                  $"Время запуска: {process.StartTime}, Текущее состояние: {process.Responding}, " +
                                                  $"Процессор использовал {process.TotalProcessorTime} времени";

                                    Console.WriteLine(line);
                                    writer.WriteLine(line);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Ошибка: {ex.Message}");
                                }
                            }
                        }*/

            /*
                        Console.ReadLine();

                        // Задание 2

                        AppDomain currentDomain = AppDomain.CurrentDomain;

                        Console.WriteLine("Информация о текущем домене приложения:");
                        Console.WriteLine($"Имя: {currentDomain.FriendlyName}");
                        Console.WriteLine($"Детали конфигурации: {currentDomain.SetupInformation}");

                        Console.WriteLine("Загруженные сборки в домене:");
                        foreach (var assembly in currentDomain.GetAssemblies())
                        {
                            Console.WriteLine(assembly.FullName);
                        }


                        AppDomain newDomain = AppDomain.CreateDomain("NewAppDomain");
                        newDomain.Load("Lab14");
                        AppDomain.Unload(newDomain);

                        Console.WriteLine("Информация о текущем домене приложения:");
                        Console.WriteLine($"Имя: {currentDomain.FriendlyName}");
                        Console.WriteLine($"Детали конфигурации: {currentDomain.SetupInformation}");


                        Console.ReadLine();*/


            // Задание 3
            Console.Write("Введите значение n для поиска простых чисел от 1 до n: ");

            if (int.TryParse(Console.ReadLine(), out int n))
            {
                Console.WriteLine($"Вы ввели: {n}");
            }
            else
            {
                Console.WriteLine("Некорректный ввод");
                throw new Exception();
            }

            PrimeCalculator primeNumbersThread = new(n);
            primeNumbersThread.SetThreadName("MyPrimeThread");
            primeNumbersThread.DisplayThreadInfo();
            primeNumbersThread.Start();
            Thread.Sleep(3000);
            primeNumbersThread.Pause();
            Console.WriteLine("Поток приостановлен на 3 секунды...");
            primeNumbersThread.DisplayThreadInfo();
            Thread.Sleep(3000);

            primeNumbersThread.Resume();
            primeNumbersThread.Join();

            Console.ReadLine();

            // Задание 4

            Console.WriteLine("Вывод четных чисел, затем нечетных");
            NumberPrinter evenPrinter = new("C:\\Users\\KROU4\\YandexDisk\\Уник\\ООП\\Лаба 14\\Lab14\\even_numbers.txt", 2, 20, 300);
            NumberPrinter oddPrinter = new("C:\\Users\\KROU4\\YandexDisk\\Уник\\ООП\\Лаба 14\\Lab14\\odd_numbers.txt", 1, 20, 200);

            Thread evenThread = new(evenPrinter.PrintNumbers);
            Thread oddThread = new(oddPrinter.PrintNumbers);

            evenThread.Priority = ThreadPriority.Highest;

            oddThread.Start();
            evenThread.Start();

            oddThread.Join();
            evenThread.Join();

            Console.WriteLine("\nПоследовательный вывод четных и нечетных чисел");
            evenPrinter = new NumberPrinter("C:\\Users\\KROU4\\YandexDisk\\Уник\\ООП\\Лаба 14\\Lab14\\even_numbers.txt", 2, 20, 100);
            oddPrinter = new NumberPrinter("C:\\Users\\KROU4\\YandexDisk\\Уник\\ООП\\Лаба 14\\Lab14\\odd_numbers.txt", 1, 20, 200);

            evenThread = new Thread(evenPrinter.PrintNumbers);
            oddThread = new Thread(oddPrinter.PrintNumbers);

            evenThread.Start();
            evenThread.Join();
            oddThread.Start();
            oddThread.Join();

            Console.ReadLine();


            // Задание 5

            RepeatingTask repeatingTask = new(1000);
            Thread.Sleep(5000);
            repeatingTask.Stop();


            Console.ReadLine();


        }
    }
}