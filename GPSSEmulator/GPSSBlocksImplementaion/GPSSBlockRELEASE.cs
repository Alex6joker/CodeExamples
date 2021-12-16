using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSSEmu.GPSSBlocksImplementaion
{
    /// <summary>
    /// Класс работы устройства. Имеет только один параметр.
    /// Освобождает устройство.
    /// </summary>
    class GPSSBlockRELEASE : GPSSBlocks
    {
        public GPSSBlockRELEASE()
        {
            NumberOfParameters = 1;
            Parameters = new Int64[NumberOfParameters];
            ThisBlockParametersTypes = new Type[] {typeof(Int64)};
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
        {   // Из блока RELEASE транзакт только выходит на следующий
            T.CodeLine++;
            // Удаление данного транзакта из устройства им занимаемого:
            // Находим номер строки исходного кода
            Int64 L = TablesArray[1].BlocksList[(Int32)TablesArray[1].BlocksList[Parameters[0]].Number].LineInSourceCode;
            // Получаем экземпляр устройства и удаляем из него транзакт
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
                        Tables.TableOfDevices.getInstanse().GetType());
                    Parameters[ParameterIndex] = Tables.TableOfDevices.getInstanse().BlocksList[P].ElementNumber;
                }
            }
        }
    }
}
