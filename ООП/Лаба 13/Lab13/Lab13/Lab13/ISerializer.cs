using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab13
{
    public interface ISerializer
    {
        public void Serialize<T>(T obj);
        public T Deserialize<T>();
    }
}
