using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab13
{
    public class CustomSerializer
    {
        private readonly ISerializer _serializer;

        public CustomSerializer(ISerializer serializer)
        {
            _serializer = serializer;
        }

        public void Serialize<T>(T obj)
        {
            _serializer.Serialize(obj);
        }

        public T Deserialize<T>()
        {
            return _serializer.Deserialize<T>();
        }
    }
}
