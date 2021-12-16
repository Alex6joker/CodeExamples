using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSSEmu.Tables
{
    public abstract class GPSSTable
    {
        public TableList_Number_Data[] BlocksList; // Список блоков для соответствующей таблицы

        public abstract void DestroyTable();

        public abstract void AddTableRecord(String[] Record);

        public abstract void ReadTableRecord(Int32 RecordNumber);
    }
}
