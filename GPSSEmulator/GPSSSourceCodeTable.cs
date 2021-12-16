using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSSEmu
{
    public interface IGPSSSourceCodeTable
    {
        GPSSBlocksImplementaion.GPSSBlocks BuildTable();
    }
}
