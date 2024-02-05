using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab12
{
    public class VDILog
    {
        private readonly string logFilePath;

        public VDILog(string path)
        {
            logFilePath = path;
        }

        public void WriteLog(string action, string details)
        {
            using (StreamWriter writer = new(logFilePath, true))
            {
                writer.WriteLine($"{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")} - {action}: {details}");
            }
        }

        public void ReadLog()
        {
            using (StreamReader reader = new(logFilePath))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line != null)

                        Console.WriteLine(line);
                }
            }
        }

        public List<string> SearchLogByDate(DateTime startDate, DateTime endDate)
        {
            List<string> result = new();
            using (StreamReader reader = new(logFilePath))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    DateTime logDate = DateTime.ParseExact(line.Split('-')[0].Trim(), "MM/dd/yyyy HH:mm:ss", null);
                    if (logDate >= startDate && logDate <= endDate)
                    {
                        result.Add(line);
                    }
                }
            }
            return result;
        }

        public List<string> SearchLogByKeyword(string keyword)
        {
            List<string> result = new();
            using (StreamReader reader = new(logFilePath))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains(keyword))
                    {
                        result.Add(line);
                    }
                }
            }
            return result;
        }
        public int GetLogEntryCount()
        {
            int count = 0;
            using (StreamReader reader = new(logFilePath))
            {
                while (reader.ReadLine() != null)
                {
                    count++;
                }
            }
            return count;
        }

        public void DeleteLogEntriesBefore(DateTime cutoffTime)
        {
            List<string> remainingEntries = new();
            using (StreamReader reader = new(logFilePath))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    DateTime logDate = DateTime.ParseExact(line.Split('-')[0].Trim(), "MM/dd/yyyy HH:mm:ss", null);
                    if (logDate >= cutoffTime)
                    {
                        remainingEntries.Add(line);
                    }
                }
            }

            using (StreamWriter writer = new StreamWriter(logFilePath, false))
            {
                foreach (var entry in remainingEntries)
                {
                    writer.WriteLine(entry);
                }
            }
        }
    }

}
