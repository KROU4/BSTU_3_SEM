using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

// Обобщенный интерфейс с операциями добавить, удалить, просмотреть и поиска по предикату
public interface ICollection<T>
{
    void Add(T item);
    bool Remove(T item);
    void Display();
    List<T> FindAll(Predicate<T> match);
}

// Обобщенный класс CollectionType<T>, включающий обобщенную коллекцию и наследующий интерфейс ICollection<T>
public class CollectionType<T> : ICollection<T> where T : new() // new() означает, что тип T должен иметь публичный конструктор по умолчанию
{
    private List<T> collection = new List<T>();

    public void Add(T item)
    {
        collection.Add(item);
    }

    public bool Remove(T item)
    {
        return collection.Remove(item);
    }

    public void Display()
    {
        foreach (var item in collection)
        {
            Console.WriteLine(item);
        }
    }

    public List<T> FindAll(Predicate<T> match)
    {
        return collection.FindAll(match);
    }

    public void SaveToFile(string fileName)
    {
        // Сериализация коллекции в XML и сохранение в файл
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));
        try
        {
            using (var xmlWriter = XmlWriter.Create(fileName))
            {
                xmlSerializer.Serialize(xmlWriter, collection);
            }
        }
        finally
        {
            Console.WriteLine("SaveToFile completed.");
        }
    }

    public void LoadFromFile(string fileName)
    {
        // Десериализация коллекции из XML файла
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));
        try
        {
            using (var xmlReader = XmlReader.Create(fileName))
            {
                collection = (List<T>)xmlSerializer.Deserialize(xmlReader);
            }
        }
        finally
        {
            Console.WriteLine("LoadFromFile completed.");
        }
    }

    public void SaveToJson(string fileName)
    {
        // Сериализация коллекции в JSON и сохранение в файл
        string json = JsonConvert.SerializeObject(collection);
        try
        {
            File.WriteAllText(fileName, json);
        }
        finally
        {
            Console.WriteLine("SaveToJson completed.");
        }
    }

    public void LoadFromJson(string fileName)
    {
        // Десериализация коллекции из JSON файла
        string json = File.ReadAllText(fileName);
        try
        {
            collection = JsonConvert.DeserializeObject<List<T>>(json);
        }
        finally
        {
            Console.WriteLine("LoadFromJson completed.");
        }
    }
}

// Пример пользовательского класса, который будет использоваться в качестве параметра обобщения
public class Product
{
    public int ProductID { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }

    public override string ToString()
    {
        return $"Product ID: {ProductID}, Name: {Name}, Price: {Price:C}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Проверка использования обобщения для стандартных типов данных
        CollectionType<int> intCollection = new CollectionType<int>();
        intCollection.Add(1);
        intCollection.Add(2);
        intCollection.Add(3);

        Console.WriteLine("Int Collection:");
        intCollection.Display();

        // Создание и использование объектов Product
        Product product1 = new Product { ProductID = 1, Name = "Product A", Price = 10.99 };
        Product product2 = new Product { ProductID = 2, Name = "Product B", Price = 15.49 };
        Product product3 = new Product { ProductID = 3, Name = "Product C", Price = 8 };

        CollectionType<Product> productCollection = new CollectionType<Product>();
        productCollection.Add(product1);
        productCollection.Add(product2);
        productCollection.Add(product3);

        Console.WriteLine("\nProduct Collection:");
        productCollection.Display();

        // Пример поиска элементов по предикату
        var cheapProducts = productCollection.FindAll(p => p.Price < 10);
        Console.WriteLine("\nCheap Products:");
        foreach (var product in cheapProducts)
        {
            Console.WriteLine(product);
        }

        // Сохранение и загрузка коллекции в XML файл
        productCollection.SaveToFile("products.xml");
        productCollection = new CollectionType<Product>();
        productCollection.LoadFromFile("products.xml");

        Console.WriteLine("\nLoaded Product Collection (from XML):");
        productCollection.Display();

        // Сохранение и загрузка коллекции в JSON файл
        productCollection.SaveToJson("products.json");
        productCollection = new CollectionType<Product>();
        productCollection.LoadFromJson("products.json");

        Console.WriteLine("\nLoaded Product Collection (from JSON):");
        productCollection.Display();
    }
}
