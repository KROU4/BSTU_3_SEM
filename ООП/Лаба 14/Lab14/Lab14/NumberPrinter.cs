using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab14
{
    class NumberPrinter
    {
        private readonly string fileName;
        private readonly int start;
        private readonly int end;
        private readonly int sleepTime;
        public static object LockObject = new();
        private static Mutex numberMutex = new();

        public NumberPrinter(string fileName, int start, int end, int sleepTime)
        {
            this.fileName = fileName;
            this.start = start;
            this.end = end;
            this.sleepTime = sleepTime;
        }

        public void PrintNumbers()
        {
            using (StreamWriter writer = new(fileName))
            {
                for (int i = start; i <= end; i += 2)
                {

                    numberMutex.WaitOne();
                        Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId}: {i}");
                        writer.WriteLine(i);
                    Thread.Sleep(sleepTime);
                    numberMutex.ReleaseMutex();
                }
            }
        }
    }
}