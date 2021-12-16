using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GPSSEmu.GPSSBlocksImplementaion;

namespace GPSSEmu.Emulator
{
    /// <summary>
    /// Данный класс представляет собой простую фабрику выбора создания блока GPSS нужного типа.
    /// </summary>
    class GPSSBlocksCreateChoser
    {
        // Список блоков является неполным
        String[] ArrayOfGPSSBlocks = new String[] {
            "SIMULATE","GENERATE","SEIZE", "RELEASE","START",
            "TERMINATE","ADVANCE","QUEUE", "DEPART","FUNCTION",
            "SPLIT","ASSEMBLE","GATHER","ENTER", "LEAVE",
            "STORAGE","TEST"};

        public GPSSBlocks CreateGPSSBlock(String Type, Int32 SourceCodeLine)
        {
            GPSSBlocks GPSSBlock = null;

            String BlockType = new BlocksTypeTextSplitter().ExtractBlockType(Type);
            String[] BlockParams = new BlocksTypeTextSplitter().ExtractBlockParameters(Type);

            if (ArrayOfGPSSBlocks.Contains<String>(BlockType))
            {
                // Генерируем соответствующий блок
                if (BlockType.Equals(ArrayOfGPSSBlocks.ElementAt<String>(0)))
                {
                    GPSSBlock = new GPSSBlockSIMULATE();
                    GPSSBlock.WriteBlockInfo((Object[])BlockParams, SourceCodeLine);
                }                    
                else if (BlockType.Equals(ArrayOfGPSSBlocks.ElementAt<String>(1)))
                {
                    GPSSBlock = new GPSSBlockGENERATE();
                    GPSSBlock.WriteBlockInfo((Object[])BlockParams, SourceCodeLine);
                }                    
                else if (BlockType.Equals(ArrayOfGPSSBlocks.ElementAt<String>(2)))
                {
                    GPSSBlock = new GPSSBlockSEIZE();
                    GPSSBlock.WriteBlockInfo((Object[])BlockParams, SourceCodeLine);
                }                    
                else if (BlockType.Equals(ArrayOfGPSSBlocks.ElementAt<String>(3)))
                {
                    GPSSBlock = new GPSSBlockRELEASE();
                    GPSSBlock.WriteBlockInfo((Object[])BlockParams, SourceCodeLine);
                }
                else if (BlockType.Equals(ArrayOfGPSSBlocks.ElementAt<String>(4)))
                {
                    GPSSBlock = new GPSSBlockSTART();
                    GPSSBlock.WriteBlockInfo((Object[])BlockParams, SourceCodeLine);
                }
                else if (BlockType.Equals(ArrayOfGPSSBlocks.ElementAt<String>(5)))
                {
                    GPSSBlock = new GPSSBlockTERMINATE();
                    GPSSBlock.WriteBlockInfo((Object[])BlockParams, SourceCodeLine);
                }
                else if (BlockType.Equals(ArrayOfGPSSBlocks.ElementAt<String>(6)))
                {
                    GPSSBlock = new GPSSBlockADVANCE();
                    GPSSBlock.WriteBlockInfo((Object[])BlockParams, SourceCodeLine);
                }
                else if (BlockType.Equals(ArrayOfGPSSBlocks.ElementAt<String>(7)))
                {
                    GPSSBlock = new GPSSBlockQUEUE();
                    GPSSBlock.WriteBlockInfo((Object[])BlockParams, SourceCodeLine);
                }
                else if (BlockType.Equals(ArrayOfGPSSBlocks.ElementAt<String>(8)))
                {
                    GPSSBlock = new GPSSBlockDEPART();
                    GPSSBlock.WriteBlockInfo((Object[])BlockParams, SourceCodeLine);
                }
            }
            return GPSSBlock;
        }
    }
}
