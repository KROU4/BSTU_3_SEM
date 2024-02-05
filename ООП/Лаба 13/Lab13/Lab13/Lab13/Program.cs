using System.Xml;
using System.Xml.Linq;


namespace Lab13
{

    [Serializable]
    public class Transport
    {
        public string? Type { get; set; }
        public Transport()
        {
            Type = "Honda";
        }

        public override string ToString()
        {
            return $"Тип транспорта: {Type}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Transport otherTransport = (Transport)obj;
            return Type == otherTransport.Type;
        }

        public override int GetHashCode()
        {
            return Math.Abs(Type?.GetHashCode() ?? 0);
        }
    }


    [Serializable]
    public class Engine
    {
        public string? Type { get; set; }
        public Engine()
        {
            Type = "G3";
        }

        public override string ToString()
        {
            return $"Тип двигателя: {Type}";
        }
    }

    [Serializable]
    public class Car : Transport
    {
        public Engine? CarEngine { get; set; }
        [NonSerialized]
        public int Speed;
        public Car()
        {
            Speed = 0;
            CarEngine = new Engine { Type ="Q9" };
        }

        public override string ToString()
        {
            return $"{base.ToString()}, {CarEngine}, Скорость: {Speed}";
        }
    }

    class Program
    {
        static void Main()
        {
            Car car1 = new()
            {
                Type = "Sedan",
                CarEngine = new Engine { Type = "V6" },
                Speed = 100
            };
            BinarySerializer binarySerializer = new();
            binarySerializer.Serialize(car1);
            Car car1des = binarySerializer.Deserialize<Car>();
            Console.WriteLine(car1des.ToString());

            Car car2 = new()
            {
                Type = "BMW",
                CarEngine = new Engine { Type = "K1" },
                Speed = 143
            };
            XmlSerialize xmlSerialize = new();
            xmlSerialize.Serialize(car2);
            Car car2des = xmlSerialize.Deserialize<Car>();
            Console.WriteLine(car2des.ToString());

            Car car3 = new()
             {
                 Type = "Lada",
                 CarEngine = new Engine { Type = "P7" },
                 Speed = 234
             };
                         /*SoapSerializer soapSerializer = new();
             soapSerializer.Serialize(car3);
             Car car3des = soapSerializer.Deserialize<Car>();
             Console.WriteLine(car3des.ToString());*/

            Car car4 = new()
            {
                Type = "Volkswagen",
                CarEngine = new Engine { Type = "L12" },
                Speed = 123
            };
            JsonSerialize jsonSerializer = new();
            jsonSerializer.Serialize(car4);
            Car car4des = jsonSerializer.Deserialize<Car>();
            Console.WriteLine(car4des.ToString());

            Car car5 = new()
            {
                Type = "Lamborghini",
                CarEngine = new Engine { Type = "F32" },
                Speed = 234
            };

            ISerializer serializer = new BinarySerializer();
            CustomSerializer serializationManager = new(serializer);
            serializationManager.Serialize(car5);
            Car car5des = serializer.Deserialize<Car>();
            Console.WriteLine(car5des.ToString());

            Console.WriteLine("\n\n");
            Car[] array = new Car[] {car1, car2, car3, car4, car5};
            XmlSerializerArray binarySerializerArray = new();
            binarySerializerArray.Serialize(array);
            Car[] arraynew = binarySerializerArray.Deserialize<Car>();
            foreach (var item in arraynew)
            {
                Console.WriteLine(item.ToString());
            }



            string xmlFilePath = "C:\\Users\\KROU4\\YandexDisk\\Уник\\ООП\\Лаба 13\\Lab13\\file.xml";

            XmlDocument xmlDocument = new();
            xmlDocument.Load(xmlFilePath);
            XmlElement? xRoot = xmlDocument.DocumentElement;

            XmlNode? typeNode = xRoot?.SelectSingleNode("*");
            if (typeNode != null)
            {
                Console.WriteLine("Значение элемента <Type>: " + typeNode.InnerText);
            }

            XmlNode? carEngineTypeNode = xRoot?.SelectSingleNode("/Car/CarEngine/Type");
            if (carEngineTypeNode != null)
            {
                Console.WriteLine("Значение элемента <CarEngine><Type>: " + carEngineTypeNode.InnerText);
            }





            XDocument xmlDoc = new(
              new XElement("Cars",
                  new XElement("Car",
                      new XElement("Type", "BMW"),
                      new XElement("Speed", 143),
                      new XElement("CarEngine",
                          new XElement("Type", "K1")
                      )
                  ),
                  new XElement("Car",
                      new XElement("Type", "Toyota"),
                      new XElement("Speed", 120),
                      new XElement("CarEngine",
                          new XElement("Type", "V8")
                      )
                  )
              )
          );


            // Выбор всех типов автомобилей
            var carTypes = xmlDoc.Descendants("Car")
                                .Select(car => car.Element("Type").Value);

            Console.WriteLine("\nТипы автомобилей:");
            foreach (var type in carTypes)
            {
                Console.WriteLine(type);
            }

            // Выбор быстрых автомобилей (со скоростью выше 130)
            var fastCars = xmlDoc.Descendants("Car")
                                .Where(car => (int)car.Element("Speed") > 130)
                                .Select(car => car.Element("Type").Value);

            Console.WriteLine("\nБыстрые автомобили:");
            foreach (var type in fastCars)
            {
                Console.WriteLine(type);
            }
        }

    }
}