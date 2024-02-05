using System;
using System.Collections.Generic;
using System.IO;

namespace Lab12
{
    class Program
    {
        static void Main()
        {
            string basePath = "C:\\Users\\KROU4\\YandexDisk\\Уник\\ООП\\Лаба 12\\Lab12\\Lab12";
            VDILog log = new(Path.Combine(basePath, "VDIlogfile.txt"));

            List<(string description, Action action)> operations = new List<(string, Action)>
            {
                ("DiskInfo", () =>
                {
                    VDIDiskInfo.GetDiskInformation();
                    log.WriteLog("VDIDiskInfo", "GetDiskInformation()");
                }),
                ("FileInfo", () =>
                {
                    VDIFileInfo fileInfo = new();
                    string path = "example.txt";
                    fileInfo.GetFileDetails(Path.Combine(basePath, path));
                    log.WriteLog("VDIFileInfo", path);
                }),
                ("DirInfo", () =>
                {
                    VDIDirInfo dirInfo = new();
                    string directory = "C:\\Users\\KROU4\\YandexDisk\\Уник\\ООП\\Лаба 12\\Lab12\\Lab12";
                    dirInfo.GetDirInfo(directory);
                    log.WriteLog("VDIDirInfo", directory);
                }),
                ("FileManager", () =>
                {
                    string diskpath = @"F:\";
                    VDIFileManager fileManager = new(diskpath);
                    fileManager.ReadDirectoryInfo();
                    log.WriteLog("VDIFileManager", "ReadDirectoryInfo()");
                    Console.ReadLine();

                    fileManager.CopyandDelete();
                    log.WriteLog("VDIFileManager", "CopyandDelete()");

                    Console.ReadLine();
                    string destinationDir = @"F:\DestinationDirectory";
                    string extension = ".txt";
                    fileManager.CopyFilesByExtension(basePath, destinationDir, extension);
                    log.WriteLog("VDIFileManager", $"CopyFilesByExtension({basePath},{destinationDir},{extension})");
                    Console.ReadLine();
                    fileManager.CreateAndExtractArchive();
                    log.WriteLog("VDIFileManager", "CreateAndExtractArchive()");
                    Console.ReadLine();
                }),
                ("SearchLogByTimeRange", () =>
                {
                    DateTime startTime, endTime;
                    Console.WriteLine("Введите начальную дату и время (гггг-мм-дд чч:мм:сс): ");
                    while (!DateTime.TryParse(Console.ReadLine(), out startTime))
                    {
                        Console.WriteLine("Некорректный формат даты. Попробуйте снова.");
                    }

                    Console.WriteLine("Введите конечную дату и время (гггг-мм-дд чч:мм:сс): ");
                    while (!DateTime.TryParse(Console.ReadLine(), out endTime))
                    {
                        Console.WriteLine("Некорректный формат даты. Попробуйте снова.");
                    }
                    List<string> logEntriesForTimeRange = log.SearchLogByDate(startTime, endTime);
                    Console.WriteLine($"Log Entries between {startTime} and {endTime}:");
                    foreach (var entry in logEntriesForTimeRange)
                    {
                        Console.WriteLine(entry);
                    }
                }),
                ("SearchLogByKeyword", () =>
                {
                    Console.WriteLine("Введите ключевое слово для поиска: ");
                    string? keyword = Console.ReadLine();
                    List<string> logEntriesByKeyword = log.SearchLogByKeyword(keyword);
                    Console.WriteLine($"Log Entries containing keyword '{keyword}':");
                    foreach (var entry in logEntriesByKeyword)
                    {
                        Console.WriteLine(entry);
                    }
                }),
                ("GetLogEntryCount", () =>
                {
                    int logEntryCount = log.GetLogEntryCount();
                    Console.WriteLine($"Total Log Entries: {logEntryCount}\n\n");
                }),
                ("DeleteLogEntriesBefore", () =>
                {
                    DateTime currentHour = DateTime.Now;
                    log.DeleteLogEntriesBefore(currentHour);
                    Console.WriteLine($"Log Entries for the current hour ({currentHour.Hour}:00:00 and later):");
                    log.ReadLog(); // Выводим оставшиеся записи
                }),
                ("ReadLog", () =>
                {
                    log.ReadLog();
                })
            };

            Console.Clear();
            Console.WriteLine("Программа автоматически начинается с первого пункта и выполняет операции после нажатия Enter.");
            Console.WriteLine("Введите номер задания, с которого начать (1-9): ");

            int startIndex;
            while (!int.TryParse(Console.ReadLine(), out startIndex) || startIndex < 1 || startIndex > operations.Count)
            {
                Console.WriteLine("Введите корректное число (1-9): ");
            }

            Console.WriteLine("\nНажмите Enter для продолжения...");

            Console.ReadLine();

            for (int i = startIndex - 1; i < operations.Count; i++)
            {
                Console.Clear();
                Console.WriteLine($"Выполнение операции {i + 1}...");
                operations[i].action.Invoke();
                Console.WriteLine("Нажмите Enter для продолжения...");
                Console.ReadLine();
            }
        }
    }
}
