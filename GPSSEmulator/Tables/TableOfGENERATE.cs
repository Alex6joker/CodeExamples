using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSSEmu.Tables
{
    /// <summary>
    /// Таблица блоков GENERATE.
    /// Представляет интерес только номер строки исходного кода.
    /// </summary>
    class TableOfGENERATE : GPSSTable
    {
        static TableOfGENERATE InstanseOfTable;

        TableOfGENERATE()
        {
            BlocksList = new TableList_Number_Data[0];
        }

        public static GPSSTable getInstanse()
        {
            if (InstanseOfTable == null)
                InstanseOfTable = new TableOfGENERATE();
            return InstanseOfTable;
        }

        public override void DestroyTable()
        {
            InstanseOfTable = null;
        }

        public override void AddTableRecord(String[] Record)
        {
            // Блок GENERATE может прислать только те имена переменных, которые были ранее определены в EQU
            // Number - номер строки исходного кода.
            TableList_Number_Data NewQueue = new TableList_Number_Data().AddInformationToTableListStruct(
            Convert.ToInt32(Record[Record.Length - 1]), new String(new Char[] { '0' }), this.GetType(), Convert.ToInt32(Record[Record.Length - 1]));
            // Для таблицы GENERATE нас интересует только номер строки, добавление безусловное
            Array.Resize<TableList_Number_Data>(ref BlocksList, BlocksList.Length + 1);
            BlocksList[BlocksList.Length - 1] = NewQueue;
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
