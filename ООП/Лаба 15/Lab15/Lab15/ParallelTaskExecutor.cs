using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab15
{
    class ParallelTaskExecutor
    {
        public void ExecuteTasks()
        {
            Parallel.Invoke(
                Print,
                () =>
                {
                    Console.WriteLine($"Выполняется задача {Task.CurrentId}");
                    Thread.Sleep(1000);
                },
                () => Square(5)
            );
        }
        void Print()
        {
            Console.WriteLine($"Выполняется задача {Task.CurrentId}");
            Thread.Sleep(1000);
        }
        void Square(int n)
        {
            Console.WriteLine($"Выполняется задача {Task.CurrentId}");
            Thread.Sleep(1000);
            Console.WriteLine($"Результат {n * n}");
        }
    }

}
