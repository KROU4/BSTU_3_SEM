using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab14
{
    class Warehouse
    {
        private List<string> products = new();
        private object lockObject = new();

        public int ProductsCount
        {
            get
            {
                lock (lockObject)
                {
                    return products.Count;
                }
            }
        }

        public void AddProduct(string product)
        {
            lock (lockObject)
            {
                products.Add(product);
                Console.WriteLine($"Добавлен товар: {product}");
            }
        }

        public bool TryRemoveProduct(out string? product)
        {
            lock (lockObject)
            {
                if (products.Count > 0)
                {
                    product = products[0];
                    products.RemoveAt(0);
                    Console.WriteLine($"Убран товар: {product}");
                    return true;
                }
                else
                {
                    product = null;
                    return false;
                }
            }
        }

    }

}