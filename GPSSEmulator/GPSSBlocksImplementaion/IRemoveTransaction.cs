using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSSEmu.GPSSBlocksImplementaion
{
    interface IRemoveTransaction
    {
        void RemoveTransactFromList(Transact T, Int64 CurrentTimeMoment);
    }
}
