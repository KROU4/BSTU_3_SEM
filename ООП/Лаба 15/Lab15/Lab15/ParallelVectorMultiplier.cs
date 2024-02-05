using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab15
{
    class SequentialVectorMultiplier
    {
        public void Multiply(int[] vector, int multiplier)
        {
            for (int i = 0; i < vector.Length; i++)
            {
                vector[i] = vector[i] * multiplier;
            }
        }
    }

    class ParallelVectorMultiplier
    {
        public void Multiply(int[] vector, int multiplier)
        {
            Task task = new(() =>
            {
                Console.WriteLine($"Идентификатор задачи: {Task.CurrentId}");
                for (int i = 0; i < vector.Length; i++)
                {
                    vector[i] = vector[i] * multiplier;
                }
            });

            task.Start();
            task.Wait();
            Console.WriteLine($"Статус задачи: {task.Status}");
        }
        public void Multiply(int[] vector, int multiplier, CancellationToken cancellationToken)
        {

            Task task = new(() =>
            {
                Console.WriteLine($"Идентификатор задачи: {Task.CurrentId}");

                for (int i = 0; i < vector.Length; i++)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        Console.WriteLine("Задача отменена.");
                        return;
                    }
                    Thread.Sleep(200);
                    vector[i] = vector[i] * multiplier;
                }

            }, cancellationToken);

            task.Start();

            Console.WriteLine($"Статус задачи: {task.Status}");
        }
    }
}