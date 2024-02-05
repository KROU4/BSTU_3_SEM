using System;
using System.Collections.Generic;
using System.Linq;

public class Set
{
    // Публичное поле для хранения элементов множества
    public List<int> items = new List<int>();

    // Конструктор класса Set, принимающий параметры и добавляющий уникальные элементы в множество
    public Set(params int[] values)
    {
        items.AddRange(values.Distinct());
    }

    // Индексатор, позволяющий получить элемент множества по индексу
    public int this[int index]
    {
        get
        {
            if (index >= 0 && index < items.Count)
                return items[index];
            else
                throw new IndexOutOfRangeException();
        }
    }

    // Перегруженный оператор "+" для добавления элемента в множество
    public static Set operator +(Set set, int item)
    {
        set.items.Add(item);
        return set;
    }

    // Перегруженный оператор "+" для объединения двух множеств
    public static Set operator +(Set set1, Set set2)
    {
        Set resultSet = new Set();
        resultSet.items.AddRange(set1.items.Union(set2.items));
        return resultSet;
    }

    // Перегруженный оператор "*" для нахождения пересечения двух множеств
    public static Set operator *(Set set1, Set set2)
    {
        Set resultSet = new Set();
        resultSet.items.AddRange(set1.items.Intersect(set2.items));
        return resultSet;
    }

    // Пользовательское преобразование типа Set к int, возвращающее количество элементов множества
    public static explicit operator int(Set set)
    {
        return set.items.Count;
    }

    // Перегруженный оператор "false" для проверки, является ли множество пустым
    public static bool operator false(Set set)
    {
        return set.items.Count == 0;
    }

    // Перегруженный оператор "true" для проверки, является ли множество непустым
    public static bool operator true(Set set)
    {
        return set.items.Count > 0;
    }

    // Перегруженный оператор "!" для создания нового множества, в котором элементы инвертированы
    public static Set operator !(Set set)
    {
        Set resultSet = new Set();
        foreach (int item in set.items)
        {
            resultSet.items.Add(-item);
        }
        return resultSet;
    }

    // Переопределение метода ToString для удобного вывода множества
    public override string ToString()
    {
        return string.Join(", ", items);
    }

    // Перегруженный оператор "--" для удаления первого элемента из множества
    public static Set operator --(Set set)
    {
        if (set.items.Count > 0)
        {
            set.items.RemoveAt(0);
        }
        return set;
    }

    // Перегруженный оператор "+" для добавления текста "занятая" после каждого слова
    public static Set operator +(Set set, string text)
    {
        string[] words = text.Split(' ');
        List<string> resultWords = new List<string>();
        foreach (string word in words)
        {
            resultWords.Add(word);
            resultWords.Add("занятая");
        }
        string resultText = string.Join(" ", resultWords);
        Console.WriteLine(resultText);
        return set;
    }
}

// Класс Production для представления информации о продукции
class Production
{
    public int Id { get; set; }
    public string OrganizationName { get; set; }
}

// Класс Developer для представления информации о разработчике
class Developer
{
    public string FullName { get; set; }
    public int Id { get; set; }
    public string Department { get; set; }
}

// Статический класс StatisticOperation содержит методы для работы с классом Set
static class StatisticOperation
{
    // Метод для вычисления суммы элементов множества
    public static int Sum(Set set)
    {
        return set.items.Sum();
    }

    // Метод для вычисления разницы между максимальным и минимальным элементами множества
    public static int Difference(Set set)
    {
        if (set.items.Count == 0)
            return 0;
        return set.items.Max() - set.items.Min();
    }

    // Метод для подсчета количества элементов множества
    public static int Count(Set set)
    {
        return set.items.Count;
    }

    // Метод расширения для добавления текста "занятая" после каждого слова в строке
    public static string AddOccupied(this string text)
    {
        string[] words = text.Split(' ');
        List<string> resultWords = new List<string>();
        foreach (string word in words)
        {
            resultWords.Add(word);
            resultWords.Add("занятая");
        }
        return string.Join(" ", resultWords);
    }

    // Метод расширения для удаления повторяющихся элементов из множества
    public static Set RemoveDuplicates(this Set set)
    {
        Set resultSet = new Set(set.items.Distinct().ToArray());
        return resultSet;
    }
}

class Program
{
    static void Main()
    {
        // Создание двух множеств set1 и set2
        Set set1 = new Set(1, 2, 3, 4, 5);
        Set set2 = new Set(3, 4, 5, 6, 7);

        // Вывод множеств на экран
        Console.WriteLine("Set1: " + set1);
        Console.WriteLine("Set2: " + set2);

        // Выполнение операций с множествами
        Set resultSet = set1 + set2;
        Console.WriteLine("Union: " + resultSet);

        resultSet = set1 * set2;
        Console.WriteLine("Intersection: " + resultSet);

        // Преобразование множества set1 к int и вывод размера множества
        int size = (int)set1;
        Console.WriteLine("Set1 size: " + size);

        // Проверка, является ли множество set1 пустым
        if (!set1)
            Console.WriteLine("Set1 is empty");

        // Пример использования метода расширения для добавления "занятая" после каждого слова в строке
        string text = "This is a test";
        Console.WriteLine("Original Text: " + text);
        string modifiedText = text.AddOccupied();
        Console.WriteLine("Modified Text: " + modifiedText);

        // Применение оператора "!" для инвертирования элементов множества
        set1 = !set1;
        Console.WriteLine("Negation of Set1: " + set1);

        // Применение оператора "--" для удаления первого элемента из множества
        set1--;
        Console.WriteLine("Set1 after removing the first element: " + set1);

        // Применение метода RemoveDuplicates для удаления повторяющихся элементов
        set1 = new Set(1, 2, 2, 3, 3, 4, 4, 5, 5);
        Console.WriteLine("Set1 with duplicates: " + set1);
        set1 = set1.RemoveDuplicates();
        Console.WriteLine("Set1 after removing duplicates: " + set1);

        // Создание объектов Production и Developer
        Production production = new Production { Id = 1, OrganizationName = "ABC Company" };
        Developer developer = new Developer { FullName = "John Doe", Id = 1001, Department = "Engineering" };

        // Вывод информации о Production и Developer на экран
        Console.WriteLine("Production: Id = {0}, OrganizationName = {1}", production.Id, production.OrganizationName);
        Console.WriteLine("Developer: FullName = {0}, Id = {1}, Department = {2}", developer.FullName, developer.Id, developer.Department);
    }
}
