using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSSEmu.Tables
{
    /// <summary>
    /// Хранение информации
    /// </summary>
    public struct TableList_Number_Data
    {
        public Int64 Number;           // Номер элемента структуры
        public Int64 ElementNumber;    // Номер элемента исходного кода GPSS
        public Int64 LineInSourceCode; // Номер строки исходного кода блока GPSS

        /// <summary>
        /// Метод производит попытку добавления нового элемента программы GPSS
        /// из исходных данных
        /// </summary>
        /// <param name="Number"></param>
        /// <param name="ElementNumber"></param> 
        /// <param name="TableType">Тип таблицы (очередей, устройств, хранилищ)</param>
        public TableList_Number_Data AddInformationToTableListStruct(Int32 Number, String ElementNumber, Type TableType, Int32 Line)
        {   // Попытка конвертировать строку в число (неудача, когда имя переменной)
            TableList_Number_Data ResultStruct = new TableList_Number_Data();
            try
            {
                ResultStruct.ElementNumber = Convert.ToInt64(ElementNumber);
            }
            catch(Exception ex)
            {   // Неудачная попытка конвертирования. Входные данные - строка (имя переменной)
                // Производим попытку поиска переменной в списке переменных
                ResultStruct.ElementNumber = TableOfVariables.getInstanse().TryFindNecessaryVariableInList(ElementNumber, TableType);
            }
            ResultStruct.Number = Number;
            ResultStruct.LineInSourceCode = Line;
            return ResultStruct;
        }
    }    
}
