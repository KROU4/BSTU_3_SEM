using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

public class Product : Dictionary<object, object>, IDictionary<object, object>
{
    // Дополнительные методы для управления коллекцией
    public void AddProduct(object key, object value)
    {
        this.Add(key, value);
    }

    public void RemoveProduct(object key)
    {
        this.Remove(key);
    }

    public bool ContainsProduct(object key)
    {
        return this.ContainsKey(key);
    }

    // Метод для демонстрации работы с коллекцией
    public void DisplayProducts()
    {
        foreach (var kvp in this)
        {
            Console.WriteLine($"[{kvp.Key}: {kvp.Value}]");
        }
    }
}

class Program
{
    static void Main()
    {
        // Часть 1: Работа с классом Product
        Product product1 = new Product();
        Product product2 = new Product();

        product1.AddProduct("Item1", 3);
        product1.AddProduct("Item2", 4);
        product1.AddProduct("Item3", 5);

        product2.AddProduct("Item4", 6);
        product2.AddProduct("Item5", 7);

        Console.WriteLine("Product 1:");
        product1.DisplayProducts();

        Console.WriteLine("Product 2:");
        product2.DisplayProducts();

        // Часть 2: Универсальная коллекция
        var genericCollection = new List<int> { 1, 2, 3, 4, 5 };

        // a. Выведите коллекцию на консоль
        Console.WriteLine("Generic Collection:");
        foreach (var item in genericCollection)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();

        // b. Удалите из коллекции n последовательных элементов 
        int n = 2;
        genericCollection.RemoveRange(0, n);

        // c. Добавьте другие элементы
        genericCollection.Add(6);
        genericCollection.Insert(0, 0);

        // d. Создайте вторую коллекцию и заполните ее данными из первой коллекции
        var secondCollection = new Dictionary<int, int>();
        for (int i = 0; i < genericCollection.Count; i++)
        {
            secondCollection[i] = genericCollection[i];
        }

        // e. Выведите вторую коллекцию на консоль
        Console.WriteLine("Second Collection:");
        foreach (var kvp in secondCollection)
        {
            Console.Write($"[{kvp.Key}: {kvp.Value}] ");
        }
        Console.WriteLine();

        // f. Найдите во второй коллекции заданное значение
        int searchValue = 4;
        var foundItem = secondCollection.FirstOrDefault(x => x.Value == searchValue);
        if (foundItem.Key != 0)
        {
            Console.WriteLine($"Found item with value {searchValue} at index {foundItem.Key}");
        }
        else
        {
            Console.WriteLine($"Item with value {searchValue} not found.");
        }

        // Часть 3: ObservableCollection
        var observableCollection = new ObservableCollection<Product>();
        observableCollection.CollectionChanged += CollectionChangedEventHandler;

        // Добавление и удаление элементов для демонстрации события
        var product3 = new Product();
        var product4 = new Product();

        observableCollection.Add(product3);
        observableCollection.Add(product4);

        observableCollection.Remove(product3);

        Console.ReadLine(); // Чтобы консольное окно не закрылось сразу после выполнения
    }

    // Обработчик события CollectionChanged
    static void CollectionChangedEventHandler(object sender, NotifyCollectionChangedEventArgs e)
    {
        Console.WriteLine("Collection changed:");

        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            foreach (var newItem in e.NewItems)
            {
                Console.WriteLine($"Added: {newItem}");
            }
        }
        else if (e.Action == NotifyCollectionChangedAction.Remove)
        {
            foreach (var oldItem in e.OldItems)
            {
                Console.WriteLine($"Removed: {oldItem}");
            }
        }
    }
}
