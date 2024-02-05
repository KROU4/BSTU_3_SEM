using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab15
{
    class ArrayGenerator
    {
        static public void GenerateArraySequential(int size, int[] resultArray)
        {
            for (int i = 0; i < size; i++)
            {
                resultArray[i] = i * i + i;
            }
        }

        static public void GenerateArrayParallel(int size, int[] resultArray)
        {
            Parallel.For(0, size, i =>
            {
                resultArray[i] = i * i + i;
            });
        }
    }
}
