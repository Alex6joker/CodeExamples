using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSSEmu.GPSSBlocksImplementaion
{
    public abstract class GPSSBlocks
    {
        public Int64 LineInSourceCode;
        protected Int32 NumberOfParameters;
        public Int64 Enters; // Количество вхождений транзактов в блок

        public Int64[] Parameters;
        protected List<Object> GettedParameters;      // Список параметров произвольного типа, полученных при создании
        protected Type[] ThisBlockParametersTypes;    // Массив типов данных, к которому приводится список параметров данного блока

        public abstract void WriteBlockInfo(Object[] Params, Int32 SourceCodeLine);

        /// <summary>
        /// Обработка вхождения транзакта в блок GPSS
        /// </summary>
        public virtual Boolean TransactIn(ref List<GPSSBlocks> TableOfAllBlock, ref Tables.GPSSTable[] TablesArray,
            ref Transact T, ref Int64 Counter, ref List<Transact> TList, Int64 CurrentTimeMoment) { return false; }
    }
}
