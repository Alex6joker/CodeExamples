using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSSEmu.GPSSBlocksImplementaion
{
    class GPSSBlockADVANCE : GPSSBlocks
    {
        List<AdvancedTransactions> advancedTransactions;
        public GPSSBlockADVANCE()
        {
            NumberOfParameters = 2;
            ThisBlockParametersTypes = new Type[] {typeof(Int64), typeof(Int64)};
            Parameters = new Int64[NumberOfParameters];
            advancedTransactions = new List<AdvancedTransactions>();
        }

        public override void WriteBlockInfo(Object[] Params, Int32 Line)
        {
            LineInSourceCode = Line;
            GettedParameters = new List<Object>();
            ParametersAsserter.getInstanse().SetGPSSBlockParameters(this, NumberOfParameters, ref GettedParameters, Params);
            WriteParametersInfo();
        }

        Int64 SetNewGenerationPeriod()
        {
            return Parameters[1] == 0 ? Parameters[0] :
                Parameters[0] + new Random(DateTime.Now.Millisecond).Next((int)Parameters[1] * -1, (int)Parameters[1]);
        }

        public override Boolean TransactIn(ref List<GPSSBlocks> TableOfAllBlock,
            ref Tables.GPSSTable[] TablesArray, ref Transact T, ref Int64 Counter, ref List<Transact> TList, Int64 CurrentTimeMoment)
        {   // Из блока ADVANCE выход транзакта есть только тогда
            // когда было произведено удержание транзакта на определенное коичество времени
            if (!TryFindTransactInAdvancedStruct(T))
            {   // Транзакта нет в списке отслеживаемых, добавляем
                AdvancedTransactions NewT = new AdvancedTransactions();
                NewT.T = T;
                NewT.StartTime = CurrentTimeMoment;
                NewT.EndTime = CurrentTimeMoment + SetNewGenerationPeriod();
                advancedTransactions.Add(NewT);
                return false;
            }
            else
            {
                AdvancedTransactions ThisT = GetTransactInAdvancedStruct(T);
                if (ThisT.EndTime != CurrentTimeMoment)
                {
                    return false;
                }
                else
                {
                    advancedTransactions.Remove(ThisT);
                    T.CodeLine++;
                    Enters++;
                    return true;
                }
            }
        }

        bool TryFindTransactInAdvancedStruct(Transact T)
        {
            for (int i = 0; i < advancedTransactions.Count; i++)
            {
                if (advancedTransactions[i].T.TransactID == T.TransactID)
                    return true;
            }
                return false;
        }

        AdvancedTransactions GetTransactInAdvancedStruct(Transact T)
        {
            int i = 0;
            for (i = 0; i < advancedTransactions.Count; i++)
            {
                if (advancedTransactions[i].T.TransactID == T.TransactID)
                    break;
            }
            return advancedTransactions[i];
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
                        Tables.TableOfVariables.getInstanse().GetType());
                    Parameters[ParameterIndex] = Tables.TableOfVariables.getInstanse().BlocksList[P].ElementNumber;
                }
            }
        }
    }

    class AdvancedTransactions
    {
        public Transact T;
        public Int64 StartTime;
        public Int64 EndTime;
    }
}
