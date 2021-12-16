using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSSEmu.GPSSBlocksImplementaion
{
    /// <summary>
    /// Только для блоков QUEUE, STORAGE, SEIZE
    /// </summary>
    interface IStatisticRecalculation
    {
        void StatisticRecalculation(Int64 CurrentTimeMoment, ref List<Transact> ListOfTransacts, ref List<GPSSBlocks> GPSSBlocks, Boolean IsEnd);

        String[] GetStatisticParameters();
    }
}
