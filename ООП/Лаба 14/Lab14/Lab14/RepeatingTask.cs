using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Lab14
{
    class RepeatingTask
    {
        private Timer timer;

        public RepeatingTask(int interval)
        {
            timer = new(interval);
            timer.Elapsed += OnTimerElapsed;
            timer.Start();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Задача выполняется...");
            Counter++;
        }

        public int Counter { get; private set; }

        public void Stop()
        {
            timer.Stop();
            Console.WriteLine($"Задача остановлена. Counter = {Counter}");
        }
    }
}
