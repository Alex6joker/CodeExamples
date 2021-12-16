using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplo
{
    public class TokenRingClientsInfoCollector
    {
        MessageQueueManager QueueManager; // Менеджер работы с очередью сообщений
        System.Timers.Timer TimerForCollect;

        public TokenRingClientsInfoCollector(MessageQueueManager nQueueManager)
        {
            QueueManager = nQueueManager;
            TimerForCollect = new System.Timers.Timer(2000);
            TimerForCollect.Elapsed += new System.Timers.ElapsedEventHandler(ElapsedTimer_TimeToCollect);
        }

        void ElapsedTimer_TimeToCollect(object sender, System.Timers.ElapsedEventArgs e)
        {
            QueueManager.AddStringMessageToQueue(((int)TokenRingMessageTypes.MSG_GET_ALL_CLIENTS_INFO).ToString());
        }

        public void StartTimer()
        {
            TimerForCollect.Start();
        }

        void StopTimer()
        {
            TimerForCollect.Stop();
        }
    }
}
