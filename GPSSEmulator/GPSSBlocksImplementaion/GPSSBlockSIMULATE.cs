using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSSEmu.GPSSBlocksImplementaion
{
    class GPSSBlockSIMULATE : GPSSBlocks
    {        
        public GPSSBlockSIMULATE()
        {
            NumberOfParameters = 0;
        }
        public override void WriteBlockInfo(Object[] Params, Int32 Line)
        {
            LineInSourceCode = Line;
        }

        public override bool TransactIn(ref List<GPSSBlocks> TableOfAllBlock, ref Tables.GPSSTable[] TablesArray, ref Transact T, ref long Counter, ref List<Transact> TList, long CurrentTimeMoment)
        {
            return true;
        }
    }
}
