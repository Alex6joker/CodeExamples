using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSSEmu.Tables
{
    class TableOfVariables : GPSSTable
    {
        static TableOfVariables InstanseOfTable;
        public List<String> ListOfQueueVariables;      // Список переменных, созданных строго при блоке QUEUE
        public List<String> ListOfStorageVariables;    // Список переменных, созданных строго при блоке STORAGE
        public List<String> ListOfDeviceVariables;     // Список переменных, созданных строго при блоке SEIZE
        public List<String> ListOfEquVariables;        // Список переменных, созданных строго при блоке EQU
 
        TableOfVariables()
        {
            ListOfQueueVariables = new List<string>();
            ListOfStorageVariables = new List<string>();
            ListOfDeviceVariables = new List<string>();
            ListOfEquVariables = new List<string>();
            BlocksList = new TableList_Number_Data[0];
        }

        public static TableOfVariables getInstanse()
        {
            if (InstanseOfTable == null)
                InstanseOfTable = new TableOfVariables();
            return InstanseOfTable;
        }

        public override void DestroyTable()
        {
            InstanseOfTable = null;
        }

        public override void AddTableRecord(String[] Record)
        {
            ListOfEquVariables.Add(Record[0]);
            TableList_Number_Data NewQueue = new TableList_Number_Data().AddInformationToTableListStruct(BlocksList.Length,
                Record[1], this.GetType(), Convert.ToInt32(Record[Record.Length - 1]));
                // Для таблицы очередей нас интересует только первый параметр
                Array.Resize<TableList_Number_Data>(ref BlocksList, BlocksList.Length + 1);
                BlocksList[BlocksList.Length - 1] = NewQueue;
        }

        public override void ReadTableRecord(Int32 RecordNumber)
        {

        }

        public String GetVariableNameByNumericIndex(List<String> L, Int64 Index)
        {
            return L.ElementAt<String>((Int32)Index);
        }

        public Int64 AddTableRecord(String NewRecord, Type ListType)
        {
            List<String> Returning = null;
            if (ListType == typeof(Tables.TableOfQUEUEs))
            {
                ListOfQueueVariables.Add(NewRecord);
                Returning = ListOfQueueVariables;
            }                
            if (ListType == typeof(Tables.TableOfSTORAGEs))
            {
                ListOfStorageVariables.Add(NewRecord);
                Returning = ListOfStorageVariables;
            } 
            if (ListType == typeof(Tables.TableOfDevices))
            {
                ListOfDeviceVariables.Add(NewRecord);
                Returning = ListOfDeviceVariables;
            }
            return Returning.IndexOf(NewRecord);
        }

        /// <summary>
        /// Метод попытки поиска переменной в списке переменных
        /// и получения (при наличии) соответствующего номера
        /// </summary>
        /// <param name="VariableName"></param>
        /// <param name="ListType"></param>
        /// <returns></returns>
        public Int64 TryFindNecessaryVariableInList(String VariableName, Type ListType)
        {   // Сначала поиск продится в соответствующей таблице, затем в таблице EQU
            if (ListType == typeof(Tables.TableOfQUEUEs))
                if (ListOfQueueVariables.Contains(VariableName))
                    return ListOfQueueVariables.IndexOf(VariableName);
            if (ListType == typeof(Tables.TableOfSTORAGEs))
                if (ListOfStorageVariables.Contains(VariableName))
                    return ListOfStorageVariables.IndexOf(VariableName);
            if (ListType == typeof(Tables.TableOfDevices))
                if (ListOfDeviceVariables.Contains(VariableName))
                    return ListOfDeviceVariables.IndexOf(VariableName);

            // Не найдено определений, возможно было дано определение в начале программы через EQU
            if (ListOfEquVariables.Contains(VariableName))
                return ListOfEquVariables.IndexOf(VariableName);

            // Не было найдено совпадений, значит необходимо определить индекс для переменной и добавить
            // в соответствующий список переменных
            else return AddTableRecord(VariableName, ListType);
        }

        public List<String> GetVariableListByType(Type ListType)
        {   // Сначала поиск продится в соответствующей таблице, затем в таблице EQU
            if (ListType == typeof(Tables.TableOfQUEUEs))
                    return ListOfQueueVariables;
            if (ListType == typeof(Tables.TableOfSTORAGEs))
                    return ListOfStorageVariables;
            if (ListType == typeof(Tables.TableOfDevices))
                    return ListOfDeviceVariables;
            return null;
        }
    }
}
