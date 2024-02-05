using System;
using System.Linq;

class Director
{
    public event Action<double> Promote;
    public event Action<double> Penalty;

    public void IncreaseSalary(double amount)
    {
        Console.WriteLine($"\nПовышение зарплаты на {amount}");
        Promote?.Invoke(amount);
    }

    public void DeductSalary(double amount)
    {
        Console.WriteLine($"Штраф в размере {amount}");
        Penalty?.Invoke(amount);
    }
}

class Worker
{
    public string Name { get; set; }
    public double Salary { get; set; }

    public Worker(string name, double salary)
    {
        Name = name;
        Salary = salary;
    }

    public void HandlePromotion(double amount) => Console.WriteLine($"{Name}: Зарплата повышена. Новая зарплата: {Salary += amount}");

    public void HandlePenalty(double amount) => Console.WriteLine($"{Name}: Зарплата уменьшена. Новая зарплата: {Salary -= amount}");
}

class StringProcessor
{
    public delegate string StringOperation(string input);

    public static string RemovePunctuation(string input) => new string(input.ToCharArray().Where(c => !char.IsPunctuation(c)).ToArray());

    public static string AddSymbol(string input, char symbol) => input + symbol;

    public static string ToUpperCase(string input) => input.ToUpper();

    public static string RemoveExtraSpaces(string input) => string.Join(" ", input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

    public static string CustomProcessing(string input, Func<char, bool> condition) => new string(input.ToCharArray().Where(condition).ToArray());

    static void Main()
    {
        string originalString = "Hello,  world! This is an example.";

        StringOperation processString = RemovePunctuation;
        DisplayProcessedString("Remove Punctuation", processString, originalString);

        processString = (input) => AddSymbol(input, '*');
        DisplayProcessedString("Add Symbol", processString, originalString);

        processString = ToUpperCase;
        DisplayProcessedString("To Upper Case", processString, originalString);

        processString = RemoveExtraSpaces;
        DisplayProcessedString("Remove Extra Spaces", processString, originalString);

        processString = (input) => CustomProcessing(input, char.IsLetter);
        DisplayProcessedString("Custom Processing (Letters only)", processString, originalString);

        Director director = new Director();

        Worker worker1 = new Worker("Токарь 1", 5000);
        Worker worker2 = new Worker("Токарь 2", 6000);
        Worker student1 = new Worker("Студент 1", 3000);

        Action<double> promoteAction = (amount) =>
        {
            worker1.HandlePromotion(amount);
            worker2.HandlePromotion(amount);
            student1.HandlePromotion(amount);
        };

        Action<double> penaltyAction = (amount) =>
        {
            worker1.HandlePenalty(amount);
            worker2.HandlePenalty(amount);
        };

        director.Promote += promoteAction;
        director.Penalty += penaltyAction;

        director.IncreaseSalary(1000);
        director.DeductSalary(500);

        Console.ReadLine();
    }

    static void DisplayProcessedString(string operationName, StringOperation operation, string input)
    {
        string processedString = operation(input);
        Console.WriteLine($"{operationName}: {processedString}");
    }
}
