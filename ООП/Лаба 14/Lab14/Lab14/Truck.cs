using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab14
{
    class Truck
    {
        private string name;
        private int loadingSpeed;
        private Warehouse warehouse;
        public static List<string> products = new();
        public volatile bool loadingComplete = false;
        private static Mutex warehouseMutex = new Mutex();

        public Truck(string name, int loadingSpeed, Warehouse warehouse)
        {
            this.name = name;
            this.loadingSpeed = loadingSpeed;
            this.warehouse = warehouse;
        }

        public void LoadWarehouse()
        {
            while (!loadingComplete)
            {
                string? product;
                warehouseMutex.WaitOne();
                if (products.Count > 0)
                {
                    Random random = new();
                    int randomIndex = random.Next(0, products.Count);
                    product = products[randomIndex];
                    products.RemoveAt(randomIndex);
                }
                else
                {
                    Console.WriteLine($"{name} закончил загрузку.");
                    loadingComplete = true;
                    warehouseMutex.ReleaseMutex();
                    return;
                }
                warehouse.AddProduct(product);
                warehouseMutex.ReleaseMutex();

                Console.WriteLine($"{name} добавлен товар: {product}");
                Thread.Sleep(loadingSpeed);
            }
        }
        public void UnloadWarehouse()
        {
            while (warehouse.ProductsCount > 0)
            {
                warehouseMutex.WaitOne();
                if (warehouse.TryRemoveProduct(out string? product))
                {
                    Console.WriteLine($"{name} разгружает товар: {product}");
                    warehouseMutex.ReleaseMutex();
                    Thread.Sleep(loadingSpeed);
                }
                else
                {
                    warehouseMutex.ReleaseMutex();
                    Console.WriteLine($"{name} ждет товар на складе.");
                    Thread.Sleep(1000);
                }
            }
        }

        public static void GenerateProductFromFile()
        {
            string fileName = "C:\\Users\\KROU4\\YandexDisk\\Уник\\ООП\\Лаба 14\\Lab14\\products.txt";
            try
            {
                using (StreamReader reader = new(fileName))
                {
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        products.Add(line);
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"Ошибка при чтении файла: {e.Message}");
            }
            foreach (var item in products)
            {
                Console.WriteLine("fdssd " + item);
            }
        }
    }
}