using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSSEmu.GPSSBlocksImplementaion
{
    class GPSSBlockTERMINATE : GPSSBlocks
    {
        public GPSSBlockTERMINATE()
        {
            NumberOfParameters = 1;
            ThisBlockParametersTypes = new Type[] {typeof(Int64)};
            Parameters = new Int64[NumberOfParameters];
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
        {   // Из блока TERMINATE транзакт не выходит, а только  уничтожается
            TList.Remove(T);
            T = null;
            Counter += Parameters[0];
            return false;
        }

        void WriteParametersInfo()
        {
            Object Parameter = GettedParameters.ElementAt<Object>(0);
            if (Parameter == null)
            {
                Parameters[0] = (Int64)Convert.ChangeType(Parameter, ThisBlockParametersTypes[0]);
                return;
            }
            else
            {
                try
                {
                    if (Parameter != null)
                        Parameters[0] = (Int64)Convert.ChangeType(Parameter, ThisBlockParametersTypes[0]);
                }
                catch (FormatException ex)
                {   // При ошибке такого формата следует знать, что поступила строка (имя переменной)
                    // Таблица переменных должна быть готова к данному времени
                    Int64 P = Tables.TableOfVariables.getInstanse().TryFindNecessaryVariableInList(Parameter.ToString(),
                        Tables.TableOfVariables.getInstanse().GetType());
                    Parameters[0] = Tables.TableOfVariables.getInstanse().BlocksList[P].ElementNumber;
                }
            }
        }
    }
}
