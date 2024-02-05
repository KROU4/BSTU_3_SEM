using System;
using System.Linq;

namespace Lab10
{
    public class Program
    {
        static void Main()
        {
            #region Task1
            string[] months = { "June", "July", "May", "December", "January", "February", "March", "April", "August", "September", "October", "November" };

            MonthQueries monthQueries = new(months);

            monthQueries.DisplayMonthsWithLength(5);
            monthQueries.DisplaySummerWinterMonths();
            monthQueries.DisplaySortedMonths();
            monthQueries.DisplayMonthsWithU();
            #endregion

            #region Task2
            List<Student> students = new()
        {
            new Student("Иван", "Иванов", "Иванович", new DateTime(2000, 5, 15), "ул. Ленина, д. 10", "+7 (123) 456-7890", "Информатика", 2, "ИТ-201"),
            new Student("Мария", "Петрова", "Сергеевна", new DateTime(2001, 8, 20), "ул. Гагарина, д. 5", "+7 (987) 654-3210", "Физика", 3, "ФЗ-301"),
            new Student("Алексей", "Сидоров", "Александрович", new DateTime(1999, 3, 10), "пр. Победы, д. 25", "+7 (555) 111-2222", "Математика", 4, "МТ-401"),
            new Student("Елена", "Козлова", "Игоревна", new DateTime(2002, 2, 5), "ул. Советская, д. 7", "+7 (999) 876-5432", "Биология", 1, "БИ-101"),
            new Student("Дмитрий", "Белов", "Дмитриевич", new DateTime(2000, 12, 1), "пр. Ленина, д. 30", "+7 (777) 333-4444", "Химия", 2, "ХИ-201"),
            new Student("Анна", "Кузнецова", "Анатольевна", new DateTime(1998, 6, 25), "ул. Кирова, д. 15", "+7 (111) 222-3333", "Филология", 4, "ФЛ-401"),
            new Student("Павел", "Смирнов", "Павлович", new DateTime(2003, 9, 12), "пр. Гагарина, д. 12", "+7 (555) 555-5555", "Экономика", 1, "ЭК-101"),
            new Student("Ольга", "Новикова", "Сергеевна", new DateTime(1999, 11, 7), "ул. Жукова, д. 3", "+7 (444) 777-8888", "История", 3, "ИС-301"),
            new Student("Игорь", "Тимофеев", "Игоревич", new DateTime(2001, 4, 18), "пр. Пушкина, д. 22", "+7 (666) 999-0000", "Иностранные языки", 2, "ИЯ-201"),
            new Student("Наталья", "Григорьева", "Николаевна", new DateTime(1998, 7, 3), "ул. Мира, д. 8", "+7 (555) 999-1111", "География", 4, "ГЕ-401")
        };
            #endregion

            #region Task3
            // Запрос 1: список студентов заданной специальности по алфавиту
            string targetSpecialty = "Информатика";
            var specialtyStudents = students.Where(s => s.Faculty == targetSpecialty).OrderBy(s => s.LastName);
            Console.WriteLine($"Список студентов по алфавиту, учащихся на факультете {targetSpecialty}:");
            PrintStudents(specialtyStudents);

            // Запрос 2: список заданной учебной группы и факультета
            string targetFaculty = "Физика";
            string targetGroup = "ФЗ-301";
            var groupAndFacultyStudents = students.Where(s => s.Faculty == targetFaculty && s.Group == targetGroup);
            Console.WriteLine($"\nСписок студентов из группы {targetGroup} на факультете {targetFaculty}:");
            PrintStudents(groupAndFacultyStudents);

            // Запрос 3: самый молодой студент
            var youngestStudent = students.OrderBy(s => s.BirthDate).First();
            Console.WriteLine($"\nСамый молодой студент: {youngestStudent.FirstName} {youngestStudent.LastName}");

            // Запрос 4: количество студентов заданной группы упорядоченных по фамилии
            string targetGroupForCount = "ЭК-101";
            var countAndOrderedStudents = students.Where(s => s.Group == targetGroupForCount).OrderBy(s => s.LastName);
            Console.WriteLine($"\nКоличество студентов в группе {targetGroupForCount} их фамилии упорядочены:");
            PrintStudents(countAndOrderedStudents);

            // Запрос 5: первый студент с заданным именем
            string targetFirstName = "Павел";
            var firstStudentWithFirstName = students.FirstOrDefault(s => s.FirstName == targetFirstName);
            if (firstStudentWithFirstName != null)
            {
                Console.WriteLine($"\nПервый студент с именем {targetFirstName}: {firstStudentWithFirstName.FirstName} {firstStudentWithFirstName.LastName}");
            }
            else
            {
                Console.WriteLine($"Студент с именем {targetFirstName} не найден.");
            }
            #endregion

            #region Task4
            /*	Придумайте и напишите свой собственный запрос, в котором было бы не менее 5 операторов из разных категорий: 
             *	условия, проекций, упорядочивания, группировки, агрегирования, кванторов и разбиения */

            /* Запрос выберет студентов с курсом 3 или ниже, вернет только их имена и курсы, упорядочит по курсу, сгруппирует по курсу 
             * и подсчитает количество студентов в каждой группе */
            var customQueryResult = students
                .Where(student => student.Course <= 3)  // Условие
                .OrderBy(student => student.Course)  // Упорядочивание
                .Skip(2) // Разбиение
                .Select(student => new { student.FirstName, student.LastName })  // Проекция
                .Count();  // Агрегирование

            Console.WriteLine($"\nРезультат пользовательского запроса: {customQueryResult}");
            #endregion

            #region Task5
            // Объединие списка студентов с фамилией, начинающейся на 'И', с их номерами групп
            var joinQueryResult = students
                .Where(s => s.LastName.StartsWith("И"))
                .Join(students,
                      student => student.Id,
                      otherStudent => otherStudent.Id,
                      (student, otherStudent) => new { student.FirstName, student.LastName, otherStudent.Group });

            Console.WriteLine("\nРезультат запроса с оператором Join:");
            foreach (var result in joinQueryResult)
            {
                Console.WriteLine($"{result.FirstName} {result.LastName}, Группа: {result.Group}");
            }
            #endregion

        }
        static void PrintStudents(IEnumerable<Student> students)
        {
            foreach (var student in students)
            {
                Console.WriteLine($"{student.FirstName} {student.LastName}, Группа: {student.Group}, Факультет: {student.Faculty}");
            }
        }

    }

}