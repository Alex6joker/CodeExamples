using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSSEmu.Tables
{
    /// <summary>
    /// Таблицу очередей прежде всего интересует именно номер очереди (имя переменной)
    /// </summary>
    public class TableOfQUEUEs : GPSSTable
    {
        static TableOfQUEUEs InstanseOfTable;

        TableOfQUEUEs()
        {
            BlocksList = new TableList_Number_Data[0];
        }

        public static GPSSTable getInstanse()
        {
            if (InstanseOfTable == null)
                InstanseOfTable = new TableOfQUEUEs();
            return InstanseOfTable;
        }
        public override void DestroyTable()
        {
            InstanseOfTable = null;
        }

        public override void AddTableRecord(String[] Record)
        {
            TableList_Number_Data NewQueue = new TableList_Number_Data().AddInformationToTableListStruct(BlocksList.Length,
                Record[0], this.GetType(), Convert.ToInt32(Record[Record.Length - 1]));
            if (!IsAlrearyExists(NewQueue.ElementNumber))
            {
                // Для таблицы очередей нас интересует только первый параметр
                Array.Resize<TableList_Number_Data>(ref BlocksList, BlocksList.Length + 1);
                BlocksList[BlocksList.Length - 1] = NewQueue;
            }            
        }

        /// <summary>
        /// Возвращает, существует ли уже такая запись в таблице.
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        Boolean IsAlrearyExists(Int64 NewQueueID)
        {
            for (Int32 QueuesListIndex = 0; QueuesListIndex < BlocksList.Length; QueuesListIndex++)
                if (BlocksList[QueuesListIndex].ElementNumber == NewQueueID)
                    return true;
            return false;
        }

        /// <summary>
        /// Возвращает номер строки исходного кода, в котором находится
        /// соответствующий блок QUEUE
        /// </summary>
        /// <param name="NumericQueueName"></param>
        /// <returns></returns>
        public Int64 FindElement(Int64 NumericQueueName)
        {
            for (int i = 0; i < BlocksList.Length; i++)
                if (NumericQueueName == BlocksList[i].ElementNumber)
                    return BlocksList[i].Number;
            return -1;
        }

        public override void ReadTableRecord(Int32 RecordNumber) { }
    }    
}
