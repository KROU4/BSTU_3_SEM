using System;
using System.Security.Cryptography;

public enum BattleUnitType
{
    Entity,
    Human,
    Transformer
}

public struct AdditionalInfo
{
    public string Description { get; set; }

    public AdditionalInfo(string description)
    {
        Description = description;
    }
}

public class Entity
{
    public string Name { get; set; }
    public int YearOfBirth { get; set; }
    public AdditionalInfo Info { get; set; }

    public Entity(string name, int yearOfBirth, AdditionalInfo info)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Name cannot be empty or null");
        }

        if (yearOfBirth < 0)
        {
            throw new ArgumentOutOfRangeException("Year of birth cannot be negative");
        }

        Name = name;
        YearOfBirth = yearOfBirth;
        Info = info;
    }

    public override string ToString()
    {
        return $"Entity: {Name}, Year of Birth: {YearOfBirth}, Info: {Info.Description}";
    }
}

public class Machine
{
    public string Model { get; set; }
    public int ProductionYear { get; set; }

    public Machine(string model, int productionYear)
    {
        if (string.IsNullOrEmpty(model))
        {
            throw new ArgumentException("Model cannot be empty or null");
        }

        if (productionYear < 0)
        {
            throw new ArgumentOutOfRangeException("Production year cannot be negative");
        }

        Model = model;
        ProductionYear = productionYear;
    }

    public override string ToString()
    {
        return $"Machine: {Model}, Production Year: {ProductionYear}";
    }
}

public class Transformer : Machine
{
    public int Power { get; set; }

    public Transformer(string model, int productionYear, int power) : base(model, productionYear)
    {
        if (power < 0)
        {
            throw new ArgumentOutOfRangeException("Power cannot be negative");
        }

        Power = power;
    }

    public override string ToString()
    {
        return $"Transformer: {Model}, Production Year: {ProductionYear}, Power: {Power}";
    }
}

public class BattleUnitContainer
{
    private List<object> units = new List<object>();

    public void AddUnit(object unit)
    {
        if (unit == null)
        {
            throw new ArgumentNullException("unit", "Unit cannot be null");
        }

        units.Add(unit);
    }

    public void RemoveUnit(object unit)
    {
        units.Remove(unit);
    }

    public void PrintUnits()
    {
        Console.WriteLine("Список боевых единиц:");
        foreach (var unit in units)
        {
            Console.WriteLine(unit.ToString());
        }
    }

    public int GetTotalUnits()
    {
        return units.Count;
    }

    public int GetTotalTransformers()
    {
        return units.OfType<Transformer>().Count();
    }

    public int GetTotalHumans()
    {
        return units.OfType<Human>().Count();
    }

    public string FindHumanByYearOfBirth(int yearToFind)
    {
        var human = units.OfType<Human>().FirstOrDefault(h => h is Human && (h as Human).YearOfBirth == yearToFind);
        return human != null ? (human as Human).Name : null;
    }

    public string FindTransformerByPower(int powerToFind)
    {
        var transformer = units.OfType<Transformer>().FirstOrDefault(t => t is Transformer && (t as Transformer).Power == powerToFind);
        return transformer != null ? (transformer as Transformer).Model : null;
    }
}

public class BattleUnitController
{
    private BattleUnitContainer container = new BattleUnitContainer();

    public void AddUnit(object unit)
    {
        container.AddUnit(unit);
    }

    public void RemoveUnit(object unit)
    {
        container.RemoveUnit(unit);
    }

    public void PrintUnits()
    {
        container.PrintUnits();
    }

    public int GetTotalBattleUnits()
    {
        return container.GetTotalUnits();
    }

    public int GetTotalTransformers()
    {
        return container.GetTotalTransformers();
    }

    public int GetTotalHumans()
    {
        return container.GetTotalHumans();
    }

    public string FindHumanByYearOfBirth(int yearToFind)
    {
        return container.FindHumanByYearOfBirth(yearToFind);
    }

    public string FindTransformerByPower(int powerToFind)
    {
        return container.FindTransformerByPower(powerToFind);
    }
}


public class Program
{
    static void Main(string[] args)
    {
        int totalTransformers = 0;

        try
        {
            BattleUnitController controller = new BattleUnitController();

            try
            {
                // Блок 1: Обработка исключений при создании юнитов
                Human human1 = new Human("John", -1990, new AdditionalInfo("General"));
                Human human2 = new Human();
                Transformer transformer1 = new Transformer("Optimus Prime", 1984, -100);
                Transformer transformer2 = new Transformer("Bumblebee", 2007, -80);

                controller.AddUnit(human1);
                controller.AddUnit(human2);
                controller.AddUnit(transformer1);
                controller.AddUnit(transformer2);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("ArgumentException caught in Block 1: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Other Exception caught in Block 1: " + ex.Message);
            }

            try
            {
                // Блок 2: Обработка исключений при работе с юнитами
                controller.PrintUnits();

                int totalBattleUnits = controller.GetTotalBattleUnits();
                int totalHumans = controller.GetTotalHumans();

                Console.WriteLine($"Общее количество боевых единиц: {totalBattleUnits}");
                Console.WriteLine($"Количество трансформеров: {totalTransformers}");
                Console.WriteLine($"Количество людей: {totalHumans}");

                int yearToFind = 1990;
                string foundHuman = controller.FindHumanByYearOfBirth(yearToFind);
                if (foundHuman != null)
                {
                    Console.WriteLine($"Человек с годом рождения {yearToFind}: {foundHuman}");
                }
                else
                {
                    Console.WriteLine($"Человек с годом рождения {yearToFind} не найден.");
                }

                int powerToFind = -100;
                string foundTransformer = controller.FindTransformerByPower(powerToFind);
                if (foundTransformer != null)
                {
                    Console.WriteLine($"Трансформер с мощностью {powerToFind}: {foundTransformer}");
                }
                else
                {
                    Console.WriteLine($"Трансформер с мощностью {powerToFind} не найден.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in Block 2: " + ex.Message);
            }

            try
            {
                // Блок 3: Обработка исключений вне контекста юнитов
                Assert(totalTransformers >= 3, "Total transformers cannot be negative");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in Block 3: " + ex.Message);
            }
        }
        finally
        {
            Console.WriteLine("Finally block executed.");
        }
    }
    static void Assert(bool condition, string message)
    {
        if (!condition)
        {
            throw new Exception("Assertion failed: " + message);
        }
    }
}