using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSSEmu.Tables
{
    public class TableOfDevices : GPSSTable
    {
        static TableOfDevices InstanseOfTable;

        TableOfDevices()
        {
            BlocksList = new TableList_Number_Data[0];
        }

        public static GPSSTable getInstanse()
        {
            if (InstanseOfTable == null)
                InstanseOfTable = new TableOfDevices();
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
                // Для таблицы устройств нас интересует только первый параметр
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

        public override void ReadTableRecord(Int32 RecordNumber)
        {

        }
    }
}
