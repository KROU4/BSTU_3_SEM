using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab14
{
    class Client
    {
        private string name;
        private VideoChannelPool channelPool;

        public Client(string name, VideoChannelPool channelPool)
        {
            this.name = name;
            this.channelPool = channelPool;
        }

        public void UseVideoChannel()
        {
            int channelId = channelPool.AcquireChannel(5000);

            if (channelId != 0)
            {
                Thread.Sleep(3000); 
                channelPool.ReleaseChannel(channelId);
            }
            else
            {
                Console.WriteLine($"{name} ушел, так как не удалось получить доступ к видеоканалу");
            }
        }
    }
}
