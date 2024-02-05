using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab11
{
    class Transport
    {
        public string? Type { get; set; }
        public int Year { get; set; }

        public Transport()
        {
            Type = "BMW";
            Year = 2012;
        }

        public Transport(string type, int year)
        {
            Type = type;
            Year = year;
        }
        public void IncreaseYear(int yearsToAdd)
        {
            Year += yearsToAdd;
            Console.WriteLine($"Год выпуска увеличен на {yearsToAdd} лет. Новый год выпуска: {Year}");
        }

        public override string ToString()
        {
            return $"Тип транспорта: {Type}, Год выпуска: {Year}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Transport otherTransport = (Transport)obj;
            return Type == otherTransport.Type && Year == otherTransport.Year;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, Year);
        }
    }

}
