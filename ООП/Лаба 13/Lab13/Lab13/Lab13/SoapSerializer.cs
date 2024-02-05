using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;

namespace Lab13
{
    public class SoapSerializer : ISerializer
    {
        readonly string soappath = "C:\\Users\\KROU4\\YandexDisk\\Уник\\ООП\\Лаба 13\\Lab13\\file.soap";
        SoapFormatter serializer = new();

        public void Serialize<T>(T obj)
        {
            using (FileStream fs = new(soappath, FileMode.OpenOrCreate))
            {
                serializer.Serialize(fs, obj);
            }
        }

        public T Deserialize<T>()
        {
            using (FileStream fs = new(soappath, FileMode.OpenOrCreate))
            {
                return (T)serializer.Deserialize(fs);
            }
        }
    }


    public class SoapSerializerArray
    {
        readonly string soappath = "C:\\Users\\KROU4\\YandexDisk\\Уник\\ООП\\Лаба 13\\Lab13\\filearray.soap";
        SoapFormatter serializer = new();

        public void Serialize<T>(T[] array)
        {
            using (FileStream fs = new FileStream(soappath, FileMode.OpenOrCreate))
            {
                serializer.Serialize(fs, array);
            }
        }

        public T[] Deserialize<T>()
        {
            using (FileStream fs = new FileStream(soappath, FileMode.OpenOrCreate))
            {
                object obj = serializer.Deserialize(fs);
                return (T[])obj;
            }
        }
    }

}
