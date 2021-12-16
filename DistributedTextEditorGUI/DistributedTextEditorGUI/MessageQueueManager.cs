using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplo
{
    // Данный класс ответственен за хранение
    // символов или групп символов,
    // которые необходимо передать в порядке очереди
    // всем клиентам маркерной сети
    public class MessageQueueManager
    {   // Содержит в себе все неотправленные сообщения
        // для клиентов сети
        Queue<String> MessagesToOtherClients;

        public MessageQueueManager()
        {
            MessagesToOtherClients = new Queue<string>();
        }

        public void AddStringMessageToQueue(String Message)
        {
            lock (MessagesToOtherClients)
            {
                MessagesToOtherClients.Enqueue(Message);
            }            
        }

        public String GetMessageFromMessagesQueue()
        {
            lock (MessagesToOtherClients)
            {
                return MessagesToOtherClients.Dequeue();
            }
        }

        public int GetMessageQueueElementsCount()
        {
            lock (MessagesToOtherClients)
            {
                return MessagesToOtherClients.Count;
            }
        }

        public String PeekMessageFromMessagesQueue()
        {
            lock (MessagesToOtherClients)
            {
                return MessagesToOtherClients.Peek();
            }
        }
    }
}
