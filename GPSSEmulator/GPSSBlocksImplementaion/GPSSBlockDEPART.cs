using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSSEmu.GPSSBlocksImplementaion
{
    /// <summary>
    /// Очередь интерусует второй параметр (насколько освобождает очередь элемент)
    /// только во время эмуляции, но не при создании.
    /// </summary>
    class GPSSBlockDEPART : GPSSBlocks
    {
        public GPSSBlockDEPART()
        {
            NumberOfParameters = 2;
            Parameters = new Int64[NumberOfParameters];
            ThisBlockParametersTypes = new Type[] { typeof(Int64), typeof(Int64)};
        }

        public override void WriteBlockInfo(Object[] Params, Int32 Line)
        {
            LineInSourceCode = Line;
            GettedParameters = new List<Object>();
            ParametersAsserter.getInstanse().SetGPSSBlockParameters(this, NumberOfParameters, ref GettedParameters, Params);
            WriteParametersInfo();
        }

        public override Boolean TransactIn(ref List<GPSSBlocks> TableOfAllBlock,
            ref Tables.GPSSTable[] TablesArray, ref Transact T, ref Int64 Counter, ref List<Transact> TList, Int64 CurrentTimeMoment)
        {   // Из блока DEPART транзакт только выходит на следующий
            T.CodeLine++;
            // Удаление данного транзакта из очереди:
            // Находим номер строки исходного кода
            Int64 L = 0;
            for(int i = 0; i < TablesArray[0].BlocksList.Length; i++)
                if (Parameters[0] == TablesArray[0].BlocksList[i].Number)
                    L = TablesArray[0].BlocksList[i].LineInSourceCode;
            // Получаем экземпляр очереди и удаляем из нее транзакт
            ((IRemoveTransaction)TableOfAllBlock.ElementAt<GPSSBlocks>((Int32)L)).RemoveTransactFromList(T, CurrentTimeMoment);
            Enters++;
            return true;
        }

        void WriteParametersInfo()
        {
            for (Int32 ParameterIndex = 0; ParameterIndex < ThisBlockParametersTypes.Length; ParameterIndex++)
            {
                Object Parameter = GettedParameters.ElementAt<Object>(ParameterIndex);
                try
                {
                    if (Parameter != null)
                        Parameters[ParameterIndex] = (Int64)Convert.ChangeType(Parameter, ThisBlockParametersTypes[ParameterIndex]);
                }
                catch (FormatException ex)
                {   // При ошибке такого формата следует знать, что поступила строка (имя переменной)
                    // Таблица переменных должна быть готова к данному времени
                    Int64 P = Tables.TableOfVariables.getInstanse().TryFindNecessaryVariableInList(Parameter.ToString(),
                        Tables.TableOfQUEUEs.getInstanse().GetType());
                    Parameters[ParameterIndex] = Tables.TableOfQUEUEs.getInstanse().BlocksList[P].ElementNumber;
                }
            }
            if (Parameters[Parameters.Length - 1] == 0)
                Parameters[Parameters.Length - 1] = 1; // Стандартное заполнение очереди - по 1
        }
    }
}
