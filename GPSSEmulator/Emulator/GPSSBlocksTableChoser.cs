using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GPSSEmu.GPSSBlocksImplementaion;
using GPSSEmu.Tables;

namespace GPSSEmu.Emulator
{
    /// <summary>
    /// Данный класс представляет собой простую фабрику выбора создания таблицы GPSS нужного типа блока.
    /// </summary>
    class GPSSBlocksTableChoser
    {
        // Список блоков является неполным
        String[] ArrayOfGPSSBlocks = new String[] {
            "QUEUE", "DEPART",
            "STORAGE", "ENTER","LEAVE",
            "SEIZE", "RELEASE",
            "GENERATE", "START",
            "EQU"};

        public GPSSTable CreateGPSSBlockTable(String Type, Int32 SourceCodeLine)
        {
            GPSSTable GPSSBlockTable = null;
            String BlockType = new BlocksTypeTextSplitter().ExtractBlockType(Type);
            String[] BlockParams = new BlocksTypeTextSplitter().ExtractBlockParameters(Type);

            if (ArrayOfGPSSBlocks.Contains<String>(BlockType))
            {
                // Генерируем соответствующий блок
                if (BlockType.Equals(ArrayOfGPSSBlocks.ElementAt<String>(0)))
                {
                    GPSSBlockTable = TableOfQUEUEs.getInstanse();
                    Array.Resize<String>(ref BlockParams, BlockParams.Length + 1);
                    BlockParams[BlockParams.Length - 1] = SourceCodeLine.ToString();
                    GPSSBlockTable.AddTableRecord(BlockParams);
                }
                else if (BlockType.Equals(ArrayOfGPSSBlocks.ElementAt<String>(1)))
                {
                    GPSSBlockTable = TableOfQUEUEs.getInstanse();
                    Array.Resize<String>(ref BlockParams, BlockParams.Length + 1);
                    BlockParams[BlockParams.Length - 1] = SourceCodeLine.ToString();
                    GPSSBlockTable.AddTableRecord(BlockParams);
                }
                else if (BlockType.Equals(ArrayOfGPSSBlocks.ElementAt<String>(2)))
                {
                    GPSSBlockTable = TableOfSTORAGEs.getInstanse();
                    Array.Resize<String>(ref BlockParams, BlockParams.Length + 1);
                    BlockParams[BlockParams.Length - 1] = SourceCodeLine.ToString();
                    GPSSBlockTable.AddTableRecord(BlockParams);
                }
                else if (BlockType.Equals(ArrayOfGPSSBlocks.ElementAt<String>(3)))
                {
                    GPSSBlockTable = TableOfSTORAGEs.getInstanse();
                    Array.Resize<String>(ref BlockParams, BlockParams.Length + 1);
                    BlockParams[BlockParams.Length - 1] = SourceCodeLine.ToString();
                    GPSSBlockTable.AddTableRecord(BlockParams);
                }
                else if (BlockType.Equals(ArrayOfGPSSBlocks.ElementAt<String>(4)))
                {
                    GPSSBlockTable = TableOfSTORAGEs.getInstanse();
                    Array.Resize<String>(ref BlockParams, BlockParams.Length + 1);
                    BlockParams[BlockParams.Length - 1] = SourceCodeLine.ToString();
                    GPSSBlockTable.AddTableRecord(BlockParams);
                }
                else if (BlockType.Equals(ArrayOfGPSSBlocks.ElementAt<String>(5)))
                {
                    GPSSBlockTable = TableOfDevices.getInstanse();
                    Array.Resize<String>(ref BlockParams, BlockParams.Length + 1);
                    BlockParams[BlockParams.Length - 1] = SourceCodeLine.ToString();
                    GPSSBlockTable.AddTableRecord(BlockParams);
                }
                else if (BlockType.Equals(ArrayOfGPSSBlocks.ElementAt<String>(6)))
                {
                    GPSSBlockTable = TableOfDevices.getInstanse();
                    Array.Resize<String>(ref BlockParams, BlockParams.Length + 1);
                    BlockParams[BlockParams.Length - 1] = SourceCodeLine.ToString();
                    GPSSBlockTable.AddTableRecord(BlockParams);
                }
                else if (BlockType.Equals(ArrayOfGPSSBlocks.ElementAt<String>(7)))
                {
                    GPSSBlockTable = TableOfGENERATE.getInstanse();
                    Array.Resize<String>(ref BlockParams, BlockParams.Length + 1);
                    BlockParams[BlockParams.Length - 1] = SourceCodeLine.ToString();
                    GPSSBlockTable.AddTableRecord(BlockParams);
                }
                else if (BlockType.Equals(ArrayOfGPSSBlocks.ElementAt<String>(8)))
                {
                    GPSSBlockTable = TableOfSTART.getInstanse();
                    Array.Resize<String>(ref BlockParams, BlockParams.Length + 1);
                    BlockParams[BlockParams.Length - 1] = SourceCodeLine.ToString();
                    GPSSBlockTable.AddTableRecord(BlockParams);
                }
                else if (BlockType.Equals(ArrayOfGPSSBlocks.ElementAt<String>(9)))
                {
                    GPSSBlockTable = TableOfVariables.getInstanse();
                    Array.Resize<String>(ref BlockParams, BlockParams.Length + 1);
                    BlockParams[BlockParams.Length - 1] = SourceCodeLine.ToString();
                    GPSSBlockTable.AddTableRecord(BlockParams);
                }
            }

            return GPSSBlockTable;
        }
    }
}
