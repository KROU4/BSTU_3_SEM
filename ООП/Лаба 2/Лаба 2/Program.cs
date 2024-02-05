using System;

public class Student
{
    private static int nextId = 1;
    private readonly int id;
    private string lastName;
    private string firstName;
    private string middleName;
    private DateTime dateOfBirth;
    private string address;
    private string phoneNumber;
    private string faculty;
    private int course;
    private string group;

    // Конструктор без параметров
    public Student()
    {
        id = nextId++;
    }

    // Конструктор с параметрами для инициализации объекта
    public Student(string lastName, string firstName, string middleName, DateTime dateOfBirth,
                   string address, string phoneNumber, string faculty, int course, string group)
        : this() // Вызываем конструктор без параметров для установки id
    {
        LastName = lastName;
        FirstName = firstName;
        MiddleName = middleName;
        DateOfBirth = dateOfBirth;
        Address = address;
        PhoneNumber = phoneNumber;
        Faculty = faculty;
        Course = course;
        Group = group;
    }

    // Свойства с проверкой корректности
    public int Id { get { return id; } }
    public string LastName
    {
        get { return lastName; }
        set
        {
            if (!string.IsNullOrEmpty(value))
                lastName = value;
        }
    }
    public string FirstName
    {
        get { return firstName; }
        set
        {
            if (!string.IsNullOrEmpty(value))
                firstName = value;
        }
    }
    public string MiddleName
    {
        get { return middleName; }
        set
        {
            if (!string.IsNullOrEmpty(value))
                middleName = value;
        }
    }
    public DateTime DateOfBirth
    {
        get { return dateOfBirth; }
        set
        {
            if (value <= DateTime.Now)
                dateOfBirth = value;
        }
    }
    public string Address
    {
        get { return address; }
        set
        {
            if (!string.IsNullOrEmpty(value))
                address = value;
        }
    }
    public string PhoneNumber
    {
        get { return phoneNumber; }
        set
        {
            if (!string.IsNullOrEmpty(value))
                phoneNumber = value;
        }
    }
    public string Faculty
    {
        get { return faculty; }
        set
        {
            if (!string.IsNullOrEmpty(value))
                faculty = value;
        }
    }
    public int Course
    {
        get { return course; }
        set
        {
            if (value >= 1 && value <= 5)
                course = value;
        }
    }
    public string Group
    {
        get { return group; }
        set
        {
            if (!string.IsNullOrEmpty(value))
                group = value;
        }
    }

    // Метод для вывода информации о студенте
    public static void DisplayStudentInfo(Student student)
    {
        Console.WriteLine($"Student ID: {student.Id}");
        Console.WriteLine($"Name: {student.LastName} {student.FirstName} {student.MiddleName}");
        Console.WriteLine($"Date of Birth: {student.DateOfBirth.ToShortDateString()}");
        Console.WriteLine($"Address: {student.Address}");
        Console.WriteLine($"Phone Number: {student.PhoneNumber}");
        Console.WriteLine($"Faculty: {student.Faculty}");
        Console.WriteLine($"Course: {student.Course}");
        Console.WriteLine($"Group: {student.Group}");
    }

    // Метод для расчета возраста студента
    public int CalculateAge()
    {
        DateTime today = DateTime.Today;
        int age = today.Year - DateOfBirth.Year;
        if (DateOfBirth.Date > today.AddYears(-age)) age--;
        return age;
    }

    // Переопределение метода Equals для сравнения по id
    public override bool Equals(object obj)
    {
        if (obj is Student otherStudent)
        {
            return this.Id == otherStudent.Id;
        }
        return false;
    }

    // Переопределение метода GetHashCode для вычисления хэша по id
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    // Переопределение метода ToString для вывода имени студента
    public override string ToString()
    {
        return $"{LastName} {FirstName} {MiddleName}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        Student student1 = new Student("Иванов", "Иван", "Иванович", new DateTime(2000, 5, 10),
                                       "ул. Пушкина, д. Колотушкина", "+375 (44) 49-48-232", "Информатика", 2, "ИИ-101");
        Student student2 = new Student("Петров", "Петр", "Петрович", new DateTime(1999, 8, 25),
                                       "ул. Лермонтова, д. 15", "+375 (29) 977-59-51", "Физика", 3, "ФИ-201");
        Student student3 = new Student("Сидоров", "Сидор", "Сидорович", new DateTime(2001, 3, 5),
                                       "ул. Гоголя, д. 20", "+375 (29) 673-53-44", "Информатика", 2, "ИИ-101");

        Console.WriteLine("Student 1:");
        Student.DisplayStudentInfo(student1);
        Console.WriteLine($"Age: {student1.CalculateAge()}");

        Console.WriteLine("\nStudent 2:");
        Student.DisplayStudentInfo(student2);
        Console.WriteLine($"Age: {student2.CalculateAge()}");

        Console.WriteLine("\nStudent 3:");
        Student.DisplayStudentInfo(student3);
        Console.WriteLine($"Age: {student3.CalculateAge()}");

        // Проверка сравнения объектов
        Console.WriteLine("\nComparing students:");
        Console.WriteLine($"student1.Equals(student2): {student1.Equals(student2)}");
        Console.WriteLine($"student1.Equals(student3): {student1.Equals(student3)}");

        // Создание массива объектов
        Student[] students = new Student[3];
        students[0] = student1;
        students[1] = student2;
        students[2] = student3;

        // Вывод списка студентов заданного факультета (например, "Информатика")
        string targetFaculty = "Информатика";
        Console.WriteLine($"\nStudents from {targetFaculty} faculty:");
        foreach (var student in students)
        {
            if (student.Faculty == targetFaculty)
            {
                Console.WriteLine(student);
            }
        }

        // Вывод списка учебной группы (например, "ИИ-101")
        string targetGroup = "ИИ-101";
        Console.WriteLine($"\nStudents from group {targetGroup}:");
        foreach (var student in students)
        {
            if (student.Group == targetGroup)
            {
                Console.WriteLine(student);
            }
        }
    }
}
