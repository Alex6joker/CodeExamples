using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSSEmu.GPSSBlocksImplementaion
{
    interface ITransactGeneration
    {
        void CheckCreateTransactionConditions(ref List<Transact> TransactList, Int64 CurrentTimeMoment);
    }
}
