using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Lab11
{

    class Program
    {
        static void Main()
        {
           // Исследование пользовательского класса
            Reflector.ExploreClass("Lab11.Student", "C:\\Users\\KROU4\\YandexDisk\\Уник\\ООП\\Лаба 11\\Lab11\\Lab11\\StudentInfo.txt");
            Reflector.ExploreClass("Lab11.Transport", "C:\\Users\\KROU4\\YandexDisk\\Уник\\ООП\\Лаба 11\\Lab11\\Lab11\\TransportInfo.txt");

            // Исследование класса из стандартной библиотеки .NET
            Reflector.ExploreClass("System.String", "C:\\Users\\KROU4\\YandexDisk\\Уник\\ООП\\Лаба 11\\Lab11\\Lab11\\StringInfo.txt");

            // Создание объекта с использованием рефлектора
            Student sampleInstance = Reflector.Create<Student>();
            sampleInstance.PrintInfo();
            Transport anotherInstance = Reflector.Create<Transport>();
            anotherInstance.IncreaseYear(2021);

            // Вызов метода с параметром
            Reflector.InvokeMethod("Lab11.Student", "CalculateAge", "C:\\Users\\KROU4\\YandexDisk\\Уник\\ООП\\Лаба 11\\Lab11\\Lab11\\MethodParameterValues.txt");
            Reflector.InvokeMethod("Lab11.Transport", "IncreaseYear", "C:\\Users\\KROU4\\YandexDisk\\Уник\\ООП\\Лаба 11\\Lab11\\Lab11\\MethodParameterValues2.txt");

           
        }
    }

}