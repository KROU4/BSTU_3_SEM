using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    public class Student
    {
        // Поля класса
        private static int count = 0; // Статическое поле для отслеживания количества созданных объектов
        private readonly int id; // Поле только для чтения (ID)

        // Константа
        private const int MinAge = 16;

        // Свойства
        private string? _firstName;
        public string? FirstName
        {
            get { return _firstName; }
            set
            {
                if (value?.Length < 3)
                {
                    throw new ArgumentException("Длина имени должна быть не менее трех символов");
                }
                _firstName = value;
            }
        }
        private string? _lastName;
        public string? LastName
        {
            get { return _lastName; }
            private set
            {
                if (value?.Length < 3)
                {
                    throw new ArgumentException("Длина фамилии должна быть не менее трех символов");
                }
                _lastName = value;
            }
        }
        private string? _middleName;
        public string? MiddleName
        {
            get { return _middleName; }
            private set
            {
                if (value?.Length < 3)
                {
                    throw new ArgumentException("Длина отчества должна быть не менее трех символов");
                }
                _middleName = value;
            }
        }
        private DateTime _birthDate;
        public DateTime BirthDate
        {
            get { return _birthDate; }
            private set
            {
                if (value > DateTime.Now.AddYears(-MinAge))
                {
                    throw new ArgumentException("Длина отчества должна быть не менее трех символов");
                }
                _birthDate = value;
            }
        }
        private string? _adress;
        public string? Address
        {
            get { return _adress; }
            private set
            {
                if (value?.Length < 3)
                {
                    throw new ArgumentException("Длина адреса должна быть не менее трех символов");
                }
                _adress = value;
            }
        }
        private string? _phoneNumber;
        public string? PhoneNumber
        {
            get { return _phoneNumber; }
            private set
            {
                if (value?.Length < 3)
                {
                    throw new ArgumentException("Длина номера должна быть не менее трех символов");
                }
                _phoneNumber = value;
            }
        }
        private string? _faculty;
        public string? Faculty
        {
            get { return _faculty; }
            private set
            {
                if (value?.Length < 3)
                {
                    throw new ArgumentException("Длина факультета должна быть не менее трех символов");
                }
                _faculty = value;
            }
        }
        private int _course;
        public int Course
        {
            get { return _course; }
            set
            {
                if (value > 4)
                {
                    throw new ArgumentException("Неправильно введён курс");
                }
                _course = value;
            }
        }
        private string? _group;
        public string? Group
        {
            get { return _group; }
            set
            {
                if (value?.Length < 3)
                {
                    throw new ArgumentException("Длина группы должна быть не менее трех символов");
                }
                _group = value;
            }
        }
        public int Id
        {
            get { return id; }
        }

        // Конструкторы
        // Конструктор с параметрами
        public Student(string FirstName, string? LastName, string MiddleName,
                       DateTime BirthDate, string Address, string PhoneNumber,
                       string Faculty, int Course, string Group)
        {
            if (BirthDate > DateTime.Now.AddYears(-MinAge))
            {
                throw new ArgumentException("Студент слишком молод.");
            }

            this.FirstName = FirstName;
            this.LastName = LastName;
            this.MiddleName = MiddleName;
            this.BirthDate = BirthDate;
            this.Address = Address;
            this.PhoneNumber = PhoneNumber;
            this.Faculty = Faculty;
            this.Course = Course;
            this.Group = Group;

            // Вычисление ID на основе хэша
            id = GetHashCode();
            count++; // Увеличиваем счетчик созданных объектов
        }

        // Конструктор без параметров
        public Student()
        {
            // Устанавливаем значения по умолчанию
            FirstName = "Екатерина";
            LastName = "Сидорова";
            MiddleName = "Сергеевна";
            BirthDate = new DateTime(2005, 2, 13);
            Address = "ул. Веры Хоружей, д. 44";
            PhoneNumber = "+7 (555) 134-1356";
            Faculty = "Дизайн";
            Course = 1;
            Group = "ДИ-102";

            id = GetHashCode();
            count++;
        }

        // Конструктор с параметрами по умолчанию
        public Student(string FirstName = "Паша", string? LastName = "Петровский", string MiddleName = "Викторович",
                       DateTime? BirthDate = null, string Address = "ул. Карла Маркса, д. 7", string PhoneNumber = "+7 (987) 546-2341",
                       string Faculty = "Дизайн", int Course = 3, string Group = "ДИ-302")
        {
            if (BirthDate > DateTime.Now.AddYears(-MinAge))
            {
                throw new ArgumentException("Студент слишком молод");
            }

            this.FirstName = FirstName;
            this.LastName = LastName;
            this.MiddleName = MiddleName;
            this.BirthDate = BirthDate ?? new DateTime(2002, 8, 1); // Используем null-условный оператор для установки значения по умолчанию
            this.Address = Address;
            this.PhoneNumber = PhoneNumber;
            this.Faculty = Faculty;
            this.Course = Course;
            this.Group = Group;

            id = GetHashCode();
            count++;
        }

        // Закрытый конструктор
        private Student(string FirstName, string? LastName)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            MiddleName = "Артёмович";
            BirthDate = new DateTime(2001, 1, 12);
            Address = "ул. Притыцкого, д. 51";
            PhoneNumber = "+7 (555) 143-1743";
            Faculty = "Физика";
            Course = 4;
            Group = "ФЗ-202";

            id = GetHashCode();
            count++;
        }

        // Вариант вызова закрытого конструктора
        public static Student CreateStudentWithDefaults(string firstName, string lastName)
        {
            return new Student(firstName, lastName);
        }

        // Метод для расчета возраста студента
        private void CalculateAge(out int age, ref DateTime today)
        {
            age = today.Year - BirthDate.Year;
            if (today < BirthDate.AddYears(age)) age--;
            today = DateTime.Now; // Обновляем значение today с использованием ref параметра
        }


        // Метод для вывода информации о студенте
        public void PrintInfo()
        {
            Console.WriteLine($"\nИнформация о студенте {id} (ID)");
            Console.WriteLine("==================================");
            Console.WriteLine($"Фамилия: {LastName}");
            Console.WriteLine($"Имя: {FirstName}");
            Console.WriteLine($"Отчество: {MiddleName}");
            Console.WriteLine($"Дата рождения: {BirthDate.ToShortDateString()}");
            Console.WriteLine($"Адрес: {Address}");
            Console.WriteLine($"Телефон: {PhoneNumber}");
            Console.WriteLine($"Факультет: {Faculty}");
            Console.WriteLine($"Курс: {Course}");
            Console.WriteLine($"Группа: {Group}");
            Console.WriteLine("==================================\n");
        }

        // Статический метод для вывода информации о классе
        public static void ClassInfo()
        {
            Console.WriteLine($"Количество студентов: {count}");
        }

        // Переопределение метода Equals для сравнения объектов
        public override bool Equals(object obj)
        {
            if (obj == null || obj is not Student)
                return false;

            Student other = (Student)obj;
            return id == other.id;
        }

        // Переопределение метода GetHashCode для алгоритма вычисления хэша
        public override int GetHashCode()
        {
            return Math.Abs(LastName.GetHashCode() + BirthDate.GetHashCode());
        }

        // Переопределение метода ToString для вывода информации об объекте
        public override string ToString()
        {
            DateTime today = DateTime.Now;
            CalculateAge(out int age, ref today);
            return $"Студент: {LastName} {FirstName} {MiddleName}, Возраст: {age}";
        }

    }
}
