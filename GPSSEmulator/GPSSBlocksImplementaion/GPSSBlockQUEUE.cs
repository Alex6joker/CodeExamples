using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSSEmu.GPSSBlocksImplementaion
{
    /// <summary>
    /// Очередь интерусует второй параметр (насколько заполняет очередь элемент)
    /// только во время эмуляции, но не при создании.
    /// </summary>
    class GPSSBlockQUEUE : GPSSBlocks, IRemoveTransaction, IStatisticRecalculation
    {
        public List<Transact> TransactionsQueue;
        Int64[] QueueCount;
        Int64[] TransactsTimeInQueue;
        Int64 DeletedTime;
        Int64 DeletedCount;
        Int64 EnteredTime;
        Boolean B;

        Int64 Enters_0;
        Int64 MAX;
        Int64 CONT;
        float AVE_CONT;
        float AVE_TIME;
        float AVE_TIME_0;

        public GPSSBlockQUEUE()
        {
            B = false;
            EnteredTime = -2;
            DeletedTime = -1;
            DeletedCount = 0;
            NumberOfParameters = 2;
            QueueCount= new long[0];
            TransactsTimeInQueue = new long[0];
            Parameters = new Int64[NumberOfParameters];
            ThisBlockParametersTypes = new Type[] { typeof(Int64), typeof(Int64)};
            TransactionsQueue = new List<Transact>();
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
        {   // Из блока QUEUE транзакт только выходит на следующий
            T.CodeLine++;
            // Добавка транзакта в очередь            
            // Теперь нужно считать время для транзакта
            Array.Resize<Int64>(ref TransactsTimeInQueue, TransactsTimeInQueue.Length + 1);            
            
            TransactionsQueue.Add(T);

            if (EnteredTime != CurrentTimeMoment)
                B = false;
            EnteredTime = CurrentTimeMoment;
            
            Enters++;
            if (MAX < TransactionsQueue.Count)
                MAX = TransactionsQueue.Count;
            return true;
        }

        Boolean IsGENERATIONPeriod(Int64 DeletedTime, Int64 CurrentTimeMoment, Int64 GENERATE)
        {
            long REM1, REM2;
            Math.DivRem(DeletedTime, GENERATE, out REM1);
            Math.DivRem(CurrentTimeMoment, GENERATE, out REM2);
            if(REM1 == 0 && REM2 == 0)
                return true;
            return false;
        }

        public void StatisticRecalculation(Int64 CurrentTimeMoment, ref List<Transact> ListOfTransacts, ref List<GPSSBlocks> GPSSBlocks, Boolean IsEnd)
        {
            Int64 GENERATE = GPSSBlocks.ElementAt<GPSSBlocks>((int)Tables.TableOfGENERATE.getInstanse().BlocksList[0].LineInSourceCode).Parameters[0];
            if (TransactionsQueue.Count == 0 && !B)
            {
                Enters_0++;
                B = true;
            }
            if (DeletedTime == CurrentTimeMoment && !IsGENERATIONPeriod(DeletedTime, CurrentTimeMoment, GENERATE))
                if (DeletedCount != 0)
                        if (Enters_0 > 1)
                            Enters_0--;
            
            if (MAX < TransactionsQueue.Count)
                MAX = TransactionsQueue.Count;
            CONT = TransactionsQueue.Count;
            //
            Array.Resize<Int64>(ref QueueCount, QueueCount.Length + 1);
            QueueCount[QueueCount.Length - 1] = TransactionsQueue.Count;
            Int64 Sum = 0;
            for(int i = 0; i < CurrentTimeMoment; i++)
            {
                Sum += QueueCount[i];
            }
            AVE_CONT = ProtectedDivision(Sum, CurrentTimeMoment);

            for (int i = 0; i < TransactionsQueue.Count; i++)
            {
                TransactsTimeInQueue[TransactsTimeInQueue.Length - 1 - i]++;
            }
            if(IsEnd)
            {
                AVE_TIME = ProtectedDivision(TransactsTimeInQueue.Sum() - TransactionsQueue.Count *2, TransactsTimeInQueue.Length);
                AVE_TIME_0 = ProtectedDivision(TransactsTimeInQueue.Sum() - TransactionsQueue.Count*2, TransactsTimeInQueue.Length - Enters_0);
            }
            else
            {
                AVE_TIME = ProtectedDivision(TransactsTimeInQueue.Sum() - TransactionsQueue.Count, TransactsTimeInQueue.Length);
                AVE_TIME_0 = ProtectedDivision(TransactsTimeInQueue.Sum() - TransactionsQueue.Count, TransactsTimeInQueue.Length - Enters_0);
            }
        }

        float ProtectedDivision(float Dividend, float Divider)
        {
            float res;
            try
            {
                res = Dividend / Divider;
            }
            catch (DivideByZeroException ex)
            {
                res = 0;
            }
            return Single.IsNaN(res) ? 0 : res;
        }

        public void RemoveTransactFromList(Transact T, Int64 CurrentTimeMoment)
        {
            if (DeletedTime != CurrentTimeMoment)
            {
                DeletedCount = 0;
                DeletedTime = CurrentTimeMoment;
            }
            DeletedCount++;

            TransactionsQueue.Remove(T);
        }

        public String[] GetStatisticParameters()
        {
            Int64 B = 0;
            for(int i = 0; i < Tables.TableOfQUEUEs.getInstanse().BlocksList.Length; i++)
                if(Tables.TableOfQUEUEs.getInstanse().BlocksList[i].LineInSourceCode == LineInSourceCode)
                    B = i;

            //Enters_0 = (Enters_0 > 1 ? Enters_0 - 1 : Enters_0).ToString();
            
            return new String[] {MAX.ToString(),CONT.ToString(),Enters.ToString(),
                (Enters_0 > 1 ? Enters_0 - 1 : Enters_0).ToString(),AVE_CONT.ToString(),AVE_TIME.ToString(), AVE_TIME_0.ToString(),
                Tables.TableOfVariables.getInstanse().GetVariableNameByNumericIndex(Tables.TableOfVariables.getInstanse().ListOfQueueVariables,
                B), this.GetType().ToString()};
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
                    Parameters[ParameterIndex] = Tables.TableOfVariables.getInstanse().BlocksList[P].ElementNumber;
                }
            }
            if (Parameters[Parameters.Length - 1] == 0)
                Parameters[Parameters.Length - 1] = 1; // Стандартное заполнение очереди - по 1
        }
    }
}
