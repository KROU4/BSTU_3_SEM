using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lab13
{
    public class JsonSerialize : ISerializer
    {
        readonly string jsonpath = "C:\\Users\\KROU4\\YandexDisk\\Уник\\ООП\\Лаба 13\\Lab13\\file.json";
        public void Serialize<T>(T obj)
        {
                using (StreamWriter writer = new(jsonpath))
                {
                    string json = JsonSerializer.Serialize(obj);
                    writer.Write(json);
                }
        }

        public T Deserialize<T>()
        {

                using (StreamReader reader = new(jsonpath))
                {
                    string json = reader.ReadToEnd();
                    return JsonSerializer.Deserialize<T>(json);
                }
            
        }

    }


    public class JsonSerializerArray
    {
        readonly string jsonpath = "C:\\Users\\KROU4\\YandexDisk\\Уник\\ООП\\Лаба 13\\Lab13\\file.json";

        public void Serialize<T>(T[] array)
        {
            using (FileStream fs = new(jsonpath, FileMode.OpenOrCreate))
            using (StreamWriter writer = new(fs))
            {
                string jsonresult = JsonSerializer.Serialize(array);
                writer.Write(jsonresult);
            }
        }

        public T[] Deserialize<T>()
        {
            using (FileStream fs = new(jsonpath, FileMode.OpenOrCreate))
            using (StreamReader reader = new(fs))
            {
                string jsonContent = reader.ReadToEnd();
                return JsonSerializer.Deserialize<T[]>(jsonContent);
            }
        }
    }
    }
