using System;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        // Вызов различных методов для демонстрации концепций C#.
        AМетод1();
        Console.WriteLine();
        AМетод2();
        Console.WriteLine();
        AМетод3();
        Console.WriteLine();
        AМетод4();
        Console.WriteLine();
        AМетод5();
        Console.WriteLine();
        AМетод6();
        Console.WriteLine();
        AМетод7();
        Console.WriteLine();
        BМетод1();
        Console.WriteLine();
        BМетод2();
        Console.WriteLine();
        BМетод3();
        Console.WriteLine();
        BМетод4();
        Console.WriteLine();
        CМетод1();
        Console.WriteLine();
        CМетод2();
        Console.WriteLine();
        CМетод3();
        Console.WriteLine();
        CМетод4();
        Console.WriteLine();
        DМетод1();
        Console.WriteLine();
        DМетод2();
        Console.WriteLine();
        DМетод3();
        Console.WriteLine();
        DМетод4();
        Console.WriteLine();
        EМетод1();
        Console.WriteLine();
        FМетод1();
        Console.WriteLine();
        FМетод23();
    }

    static void AМетод1()
    {
        // Демонстрация различных типов переменных и их значений.
        bool a = true;
        byte b = 255;
        sbyte c = 127;
        char d = (char)71;
        float e = 3_000.5F;
        double h = 14.74;
        decimal j = 3_000.5m;
        int k = 1;
        uint l = 121321321;
        nint p = 45;
        nuint r = 90;
        long t = 9999999;
        ulong o = 18446744073709551615;
        short v = 32000;
        ushort x = 65000;

        // Вывод значений переменных.
        Console.WriteLine(a);
        Console.WriteLine(b);
        Console.WriteLine(c);
        Console.WriteLine(d);
        Console.WriteLine(e);
        Console.WriteLine(h);
        Console.WriteLine(j);
        Console.WriteLine(k);
        Console.WriteLine(l);
        Console.WriteLine(p);
        Console.WriteLine(r);
        Console.WriteLine(t);
        Console.WriteLine(o);
        Console.WriteLine(v);
        Console.WriteLine(x);
    }

    static void AМетод2()
    {
        // Неявное и явное приведение типов.
        int num = (int)3.14;
        double num1 = 5;
        char ch = (char)65;
        float f = (float)2.5;
        long l = (long)123456789;

        // Вывод результатов.
        Console.WriteLine(num);
        Console.WriteLine(num1);
        Console.WriteLine(ch);
        Console.WriteLine(f);
        Console.WriteLine(l);
    }

    static void AМетод3()
    {
        // Неявное приведение типов.
        int num = 5;
        double num1 = num;
        byte b = 10;
        int p = b;
        float f = 2.5f;
        double d = f;
        short s = 100;
        int i = s;
        char ch = 'A';
        int o = ch;

        // Вывод результатов.
        Console.WriteLine(num1);
        Console.WriteLine(p);
        Console.WriteLine(d);
        Console.WriteLine(i);
        Console.WriteLine(o);
    }

    static void AМетод4()
    {
        // Упаковка и распаковка.
        int num = 10;
        object obj = num;
        Console.WriteLine(obj);

        object obj1 = 10;
        int num1 = (int)obj1;
        Console.WriteLine(num1);
    }

    static void AМетод5()
    {
        // Использование var для вывода типов переменных.
        var num = 10;
        var str = "Hello";
        var isTrue = true;

        // Вывод выводимых типов.
        Console.WriteLine(num.GetType()); // Вывод: System.Int32
        Console.WriteLine(str.GetType()); // Вывод: System.String
        Console.WriteLine(isTrue.GetType()); // Вывод: System.Boolean
    }

    static void AМетод6()
    {
        // Nullable-типы.
        int? num = null;

        if (num.HasValue)
        {
            Console.WriteLine("Переменная num содержит значение: " + num.Value);
        }
        else
        {
            Console.WriteLine("Переменная num не содержит значение");
        }
    }

    static void AМетод7()
    {
        // Ошибка неявного преобразования типа.
        var myVar = "Hello";
        // myVar = 123; // Раскомментирование этой строки приведет к ошибке компиляции.
        Console.WriteLine(myVar);
    }

    static void BМетод1()
    {
        // Сравнение строк.
        var str1 = "Hello";
        var str2 = "World";

        if (str1 == str2)
        {
            Console.WriteLine("Строки равны");
        }
        else
        {
            Console.WriteLine("Строки не равны");
        }
    }

    static void BМетод2()
    {
        // Операции со строками.
        string str1 = "Hello";
        string str2 = "World";
        string str3 = "C#";

        string resultStr = string.Concat(str1, " ", str2, " ", str3);
        Console.WriteLine(resultStr); // Вывод: "Hello World C#"

        string copiedStr = string.Copy(str1);
        Console.WriteLine(copiedStr); // Вывод: "Hello"

        string subStr = str2.Substring(0, 3);
        Console.WriteLine(subStr); // Вывод: "Wor"

        string[] words = resultStr.Split(" ");
        foreach (string word in words)
        {
            Console.WriteLine(word);
        }

        string insertedStr = str1.Insert(3, "p");
        Console.WriteLine(insertedStr); // Вывод: "Helplo"

        string removedStr = str3.Replace("C#", "");
        Console.WriteLine(removedStr); // Вывод: ""

        int num = 42;
        Console.WriteLine($"Ответ на вопрос о жизни, вселенной и всем остальном: {num}");
    }

    static void BМетод3()
    {
        // Обработка строк.
        string emptyStr = string.Empty;
        string nullStr = null;

        if (string.IsNullOrEmpty(emptyStr))
        {
            Console.WriteLine("Пустая строка равна null или пуста");
        }

        if (string.IsNullOrEmpty(nullStr))
        {
            Console.WriteLine("Null-строка равна null или пуста");
        }

        string str1 = "Hello";
        string str2 = "World";
        bool isEqual = emptyStr == str1; // false
        bool isNotEqual = nullStr != str2; // true

        string resultStr = emptyStr + " " + str1 + " " + nullStr + " " + str2;
        Console.WriteLine(resultStr); // Вывод: " Hello  World"
    }

    static void BМетод4()
    {
        // Использование StringBuilder для манипуляции строками.
        StringBuilder sb = new StringBuilder("This is a StringBuilder string");

        sb.Remove(5, 3);
        sb.Insert(0, "New ");
        sb.Append("!");

        Console.WriteLine(sb.ToString()); // Вывод: "New is a Builder string!"
    }

    static void CМетод1()
    {
        // Демонстрация двумерного массива.
        int[,] matrix = new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write("{0,4}", matrix[i, j]);
            }
            Console.WriteLine();
        }
    }

    static void CМетод2()
    {
        // Демонстрация работы с одномерным массивом.
        string[] array = { "Hello", "world", "!" };
        Console.WriteLine("Массив строк:");
        for (int i = 0; i < array.Length; i++)
        {
            Console.WriteLine(array[i]);
        }
        Console.WriteLine("Длина массива: {0}", array.Length);

        Console.Write("Введите индекс элемента, который хотите изменить: ");
        int index = int.Parse(Console.ReadLine());
        Console.Write("Введите новое значение: ");
        string newValue = Console.ReadLine();

        array[index] = newValue;

        Console.WriteLine("Новый массив строк:");
        for (int i = 0; i < array.Length; i++)
        {
            Console.WriteLine(array[i]);
        }
    }

    static void CМетод3()
    {
        // Демонстрация ступенчатого массива.
        double[][] jaggedArray = new double[3][];
        jaggedArray[0] = new double[2];
        jaggedArray[1] = new double[3];
        jaggedArray[2] = new double[4];

        Console.WriteLine("Введите значения для ступенчатого массива:");
        for (int i = 0; i < jaggedArray.Length; i++)
        {
            for (int j = 0; j < jaggedArray[i].Length; j++)
            {
                Console.Write("Введите значение для элемента [{0}][{1}]: ", i, j);
                jaggedArray[i][j] = double.Parse(Console.ReadLine());
            }
        }

        Console.WriteLine("Ступенчатый массив значений double:");
        for (int i = 0; i < jaggedArray.Length; i++)
        {
            for (int j = 0; j < jaggedArray[i].Length; j++)
            {
                Console.Write(jaggedArray[i][j] + " ");
            }
            Console.WriteLine();
        }
    }

    static void CМетод4()
    {
        // Объявление ступенчатого массива с использованием неявного типа.
        var jaggedArray = new[]
        {
            new[] { 1.0, 2.0 },
            new[] { 3.0, 4.0, 5.0 },
            new[] { 6.0, 7.0, 8.0, 9.0 }
        };

        var str = "Hello, world!";
    }

    static void DМетод1()
    {
        // Объявление кортежа.
        var tuple = (42, "hello", 'c', "world", 1234567890UL);

        // Вывод всего кортежа.
        Console.WriteLine(tuple);
    }

    static void DМетод2()
    {
        // Обращение к элементам кортежа.
        var tuple = (42, "hello", 'c', "world", 1234567890UL);

        Console.WriteLine(tuple.Item1); // Вывод: 42
        Console.WriteLine(tuple.Item3); // Вывод: 'c'
        Console.WriteLine(tuple.Item4); // Вывод: "world"
    }

    static void DМетод3()
    {
        // Разбор кортежа.
        var tuple = Tuple.Create(42, 'c', "world");
        tuple.Deconstruct(out int num, out char letter, out string word);
        Console.WriteLine(num); // Вывод: 42
        Console.WriteLine(letter); // Вывод: 'c'
        Console.WriteLine(word); // Вывод: "world"
    }

    static void DМетод4()
    {
        // Сравнение кортежей на равенство.
        var tuple1 = (1, "hello", true);
        var tuple2 = (1, "world", true);

        if (tuple1 == tuple2)
        {
            Console.WriteLine("Кортежи равны");
        }
        else
        {
            Console.WriteLine("Кортежи не равны");
        }
    }

    static void EМетод1()
    {
        // Функция, возвращающая кортеж с максимумом, минимумом, суммой и первой буквой строки.
        int[] array = { 5, 2, 8, 1, 9 };
        string str = "hello";

        var result = GetTuple(array, str);

        Console.WriteLine($"Максимальный элемент: {result.max}");
        Console.WriteLine($"Минимальный элемент: {result.min}");
        Console.WriteLine($"Сумма элементов: {result.sum}");
        Console.WriteLine($"Первая буква строки: {result.firstLetter}");
    }

    static (int max, int min, int sum, char firstLetter) GetTuple(int[] array, string str)
    {
        // Вычисление максимума, минимума и суммы элементов массива.
        int max = array[0];
        int min = array[0];
        int sum = 0;

        foreach (int element in array)
        {
            if (element > max)
            {
                max = element;
            }
            if (element < min)
            {
                min = element;
            }
            sum += element;
        }

        // Получение первой буквы строки.
        char firstLetter = str[0];

        return (max, min, sum, firstLetter);
    }

    static void FМетод1()
    {
        // Демонстрация использования блоков checked и unchecked в локальной функции.
        Aboba();
    }

    static void Aboba()
    {
        int a = int.MaxValue;
        int b = 1;

        try
        {
            // Вызов локальной функции с блоком checked.
            int resultChecked = CalculateChecked(a, b);
            Console.WriteLine($"Результат с использованием checked: {resultChecked}");
        }
        catch (OverflowException ex)
        {
            Console.WriteLine($"Произошло переполнение в блоке checked: {ex.Message}");
        }

        // Вызов локальной функции с блоком unchecked.
        int resultUnchecked = CalculateUnchecked(a, b);
        Console.WriteLine($"Результат с использованием unchecked: {resultUnchecked}");
    }


    // Локальная функция с блоком checked.
    static int CalculateChecked(int x, int y)
    {
        checked
        {
            return x + y;
        }
    }

    // Локальная функция с блоком unchecked.
    static int CalculateUnchecked(int x, int y)
    {
        unchecked
        {
            return x + y;
        }
    }

    static void FМетод23()
    {
        // Демонстрация использования блоков checked и unchecked в локальных функциях.
        CalculateWithChecked();
        CalculateWithUnchecked();
    }

    // Локальная функция с блоком checked.
    static void CalculateWithChecked()
    {
        checked
        {
            int maxInt = int.MaxValue;
            Console.WriteLine($"Максимальное значение int в блоке checked: {maxInt}");
        }
    }

    // Локальная функция с блоком unchecked.
    static void CalculateWithUnchecked()
    {
        unchecked
        {
            int maxInt = int.MaxValue;
            Console.WriteLine($"Максимальное значение int в блоке unchecked: {maxInt}");
        }
    }
}
