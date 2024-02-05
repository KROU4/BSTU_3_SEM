using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lab11
{
    public static class Reflector
    {
        public static void ExploreClass(string className, string outputFile)
        {
            Type? type = Type.GetType(className, true, true);

            using StreamWriter? writer = new(outputFile);
            // a. Определение имени сборки
            writer.WriteLine($"Имя сборки: {type.Assembly.FullName}");

            // b. Есть ли публичные конструкторы
            bool? hasPublicConstructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public).Any();
            writer.WriteLine($"Имеет публичные конструкторы: {hasPublicConstructors}");

            // c. Все общедоступные публичные методы
            IEnumerable<string>? publicMethods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .Select(method => method.Name);
            if (publicMethods.Any())
            {
                writer.WriteLine($"Все общедоступные публичные методы:");
                foreach (string method in publicMethods)
                {
                    writer.WriteLine($"- {method}");
                }
            }
            else
            {
                writer.WriteLine($"Общедоступных публичных методов нет");
            }

            // d. Информация о полях и свойствах
            IEnumerable<string>? fieldsAndProperties = type.GetMembers()
                .Where(member => member.MemberType == MemberTypes.Field || member.MemberType == MemberTypes.Property)
                .Select(member => member.Name);
            if (fieldsAndProperties.Any())
            {
                writer.WriteLine("Поля и свойства:");
                foreach (string member in fieldsAndProperties)
                {
                    writer.WriteLine($"- {member}");
                }
            }
            else
            {
                writer.WriteLine("Полей и свойств нет");
            }

            // e. Все реализованные интерфейсы
            IEnumerable<string>? interfaces = type.GetInterfaces().Select(interf => interf.Name);
            if (interfaces.Any())
            {
                writer.WriteLine("Реализованные интерфейсы:");
                foreach (string interf in interfaces)
                {
                    writer.WriteLine($"- {interf}");
                }
            }
            else
            {
                writer.WriteLine("Реализованных интерфейсов нет");
            }

            // f. Методы с определенным параметром
            string? parameterName = "birthage";
            IEnumerable<string>? methodsWithParameterType = type.GetMethods()
                .Where(method => method.GetParameters().Any(param => param.Name == parameterName))
                .Select(method => method.Name);

            if (methodsWithParameterType.Any())
            {
                writer.WriteLine($"Методы с параметром '{parameterName}':");
                foreach (string method in methodsWithParameterType)
                {
                    writer.WriteLine($"- {method}");
                }
            }
            else
            {
                writer.WriteLine($"Нет методов с параметром '{parameterName}':");
            }

            writer.Close();
        }

        public static void InvokeMethod(string className, string methodName, string parameterFile)
        {
            Type? type = Type.GetType(className);
            MethodInfo? method = type.GetMethod(methodName);
            ParameterInfo[] parameters = method.GetParameters();

            // Чтение параметров из текстового файла
            string[] parameterValues = File.ReadAllLines(parameterFile);

            if (parameterValues.Length != parameters.Length)
            {
                Console.WriteLine("Неверное количество параметров в файле");
                return;
            }

            object[] args = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                Type parameterType = parameters[i].ParameterType;
                args[i] = Convert.ChangeType(parameterValues[i], parameterType);
            }

            // Вызов метода
            object? instance = Activator.CreateInstance(type);
            method.Invoke(instance, args);
        }


        public static T Create<T>()
        {
            Type type = typeof(T);
            ConstructorInfo[] constructors = type.GetConstructors();
            if (constructors.Length == 0)
            {
                throw new InvalidOperationException($"Тип {type.Name} не имеет публичных конструкторов");
            }

            ConstructorInfo constructor = constructors.OrderByDescending(c => c.GetParameters().Length).First();
            ParameterInfo[] parameters = constructor.GetParameters();
            object[] args = parameters.Select(param => GenerateValue(param.ParameterType)).ToArray();

            return (T)constructor.Invoke(args);
        }

        private static object GenerateValue(Type type)
        {
            if (type == typeof(int))
            {
                return 3;
            }
            else if (type == typeof(string))
            {
                return "Hello, world!";
            }

            throw new NotSupportedException($"Тип {type.Name} не может быть сгенерирован");
        }
    }
}
