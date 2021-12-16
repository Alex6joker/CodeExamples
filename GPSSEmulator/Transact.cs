using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSSEmu
{
    /// <summary>
    /// Описывает транзакт.
    /// </summary>
    public class Transact
    {
        public static Int64 sID = 1;
        public Int64 TransactID;
        public Int64 CodeLine;

        public Transact(Int64 nCodeLine)
        {
            TransactID = sID;
            CodeLine = nCodeLine;
            sID++;
        }
    }
}
