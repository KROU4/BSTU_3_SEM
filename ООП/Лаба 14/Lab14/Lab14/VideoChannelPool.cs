using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab14
{
    class VideoChannelPool
    {
        private Semaphore semaphore;
        private List<int> availableChannels;

        public VideoChannelPool(int initialChannels)
        {
            semaphore = new Semaphore(initialChannels, initialChannels);
            availableChannels = new List<int>();

            for (int i = 1; i <= initialChannels; i++)
            {
                availableChannels.Add(i);
            }
        }

        public int AcquireChannel(int timeout)
        {
            int channelId = 0;

            if (semaphore.WaitOne(timeout))
            {
                lock (availableChannels)
                {
                    channelId = availableChannels[0];
                    availableChannels.RemoveAt(0);
                }

                Console.WriteLine($"Клиент получил доступ к каналу {channelId}");
            }
            else
            {
                Console.WriteLine("Клиент не получил доступ к каналу (время ожидания истекло)");
            }

            return channelId;
        }

        public void ReleaseChannel(int channelId)
        {
            lock (availableChannels)
            {
                availableChannels.Add(channelId);
            }

            semaphore.Release();
            Console.WriteLine($"Канал {channelId} освобожден");
        }
    }
}
