using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab15
{
    class Warehouse
    {
        static BlockingCollection
<string>
 warehouse = new();
        static Random random = new();
        static CancellationTokenSource cts = new();
        public static void AAAAsync()
        {
            for (int i = 1; i <= 1; i++)
            {
                string supplierName = $"Поставщик {i}";
                int initialSpeed = random.Next(1000, 3000);
                Task.Run(() => Supplier(supplierName, initialSpeed, cts.Token));
            }
for (int i = 1; i <= 100; i++)
        {
            string buyerName = $"Покупатель {i}";
            Task.Run(() => Buyer(buyerName, cts.Token));
        }

        Thread.Sleep(3000);
        cts.Cancel();

    }
    static void Supplier(string name, int initialSpeed, CancellationToken cancellationToken)
    {
        int speed = initialSpeed;
        while (!cancellationToken.IsCancellationRequested)
        {
            Thread.Sleep(speed);
            string product = $"Товар {random.Next(100000, 90000000)}";
            warehouse.Add(product);
            Console.WriteLine($"{name} добавил на склад: {product}");
            speed = Math.Max(500, speed - 100);
            PrintWarehouseState();
        }
    }

    static void Buyer(string name, CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            if (warehouse.TryTake(out string product))
            {
                Console.WriteLine($"{name} купил: {product}");
            }
            else
            {
                Console.WriteLine($"{name}: Товара нет, уходит");
            }
            Thread.Sleep(2000);
            PrintWarehouseState();
        }
    }

    static void PrintWarehouseState()
    {
        Console.WriteLine($"Состояние склада: {string.Join(", ", warehouse)}");
    }

}
}