using System;

// Интерфейс для клонирования объектов
interface ICloneable
{
    bool DoClone();
}

// Абстрактный класс для транспортного средства
abstract class Transport
{
    public abstract void Move();
}

// Абстрактный класс для разумных существ
abstract class Creature
{
    public abstract void Speak();
}

// Класс "Человек" наследует класс Creature
class Human : Creature, ICloneable
{
    public override void Speak()
    {
        Console.WriteLine("Человек говорит.");
    }

    public bool DoClone()
    {
        Console.WriteLine("Человек клонируется.");
        return true;
    }

    public override string ToString()
    {
        return "Человек";
    }
}

// Класс "Машина" наследует класс Transport и реализует интерфейс ICloneable
class Car : Transport, ICloneable
{
    public override void Move()
    {
        Console.WriteLine("Машина двигается.");
    }

    public bool DoClone()
    {
        Console.WriteLine("Машина клонируется.");
        return true;
    }

    public override string ToString()
    {
        return "Машина";
    }
}

// Запечатанный (sealed) класс "Трансформер"
sealed class Transformer : Transport
{
    public override void Move()
    {
        Console.WriteLine("Трансформер двигается и преобразуется.");
    }
}

// Класс "Двигатель"
class Engine
{
    public void Start()
    {
        Console.WriteLine("Двигатель запущен.");
    }
}

// Класс "Управление авто"
class CarControl
{
    public void ControlCar(Transport car)
    {
        Console.WriteLine("Управление авто:");
        car.Move();
    }
}

// Класс "Принтер" с полиморфным методом IAmPrinting
class Printer
{
    public void IAmPrinting(ICloneable obj)
    {
        if (obj.DoClone())
        {
            Console.WriteLine("Объект успешно клонирован.");
        }
        else
        {
            Console.WriteLine("Не удалось клонировать объект.");
        }
    }
}

class Program
{
    static void Main()
    {
        // Создание объектов разных классов
        Transport car = new Car();
        Transport transformer = new Transformer();
        Creature human = new Human();
        Engine engine = new Engine();

        // Создание объекта класса Printer
        Printer printer = new Printer();

        // Создание массива ссылок на разные объекты
        object[] objects = new object[] { car, transformer, human, engine };

        // Вызов метода IAmPrinting для каждого объекта
        foreach (var obj in objects)
        {
            if (obj is ICloneable cloneable)
            {
                printer.IAmPrinting(cloneable);
            }
        }

        // Вызов метода ToString для каждого объекта
        foreach (var obj in objects)
        {
            Console.WriteLine(obj.ToString());
        }
    }
}