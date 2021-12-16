using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSSEmu.GPSSBlocksImplementaion
{
    /// <summary>
    /// Класс работы устройства. Имеет только один параметр.
    /// Занимает устройство.
    /// </summary>
    class GPSSBlockSEIZE : GPSSBlocks, IRemoveTransaction, IStatisticRecalculation
    {
        Transact Owner; // Транзакт, занявший устройство
        Int64 TotalTime;
        Int64 WorkTime; // Время пребывания устройства занятым

        float UTIL; // Время занятости устройства по отношению ко всему времени
        float AVE_TIME; // Среднее время работы устройства
        Int64 AVAIL;    // Доступность устройства на данный момент времени
        Int64 stOWNER;  // ID транзакта-владельца устройства на данный момент времени
        public GPSSBlockSEIZE()
        {
            TotalTime = 0;
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
        {   // Из блока SEIZE есть 2 выхода
            // Если устройство занято - удерживаем транзакт
            // Если устройство свободно - занимаем устройство и пускаем транзакт дальше
            if(Owner == null)
            {
                Owner = T;
                T.CodeLine++;
                Enters++;
                return true;
            }
            else
                return false;
        }

        public void RemoveTransactFromList(Transact T, Int64 CurrentTimeMoment)
        {
            Owner = null;
        }

        public void StatisticRecalculation(Int64 CurrentTimeMoment, ref List<Transact> ListOfTransacts, ref List<GPSSBlocks> GPSSBlocks, Boolean IsEnd)
        {
            if (!TryFindOwnerTransactInList(ref ListOfTransacts))
            {
                Owner = null;
            }
            else
            {
                if (Owner != null && !IsEnd)
                    WorkTime++;
            }
            UTIL = ProtectedDivision(WorkTime, CurrentTimeMoment);
            AVE_TIME = ProtectedDivision(WorkTime, Enters);
            if(!IsEnd)
                AVAIL = Owner == null ? 1 : 0;
            stOWNER = Owner != null ? Owner.TransactID : 0;
        }

        Boolean TryFindOwnerTransactInList(ref List<Transact> ListOfTransacts)
        {
            if (Owner == null)
                return false;
            for(Int32 i = 0; i < ListOfTransacts.Count; i++)
            {
                if (ListOfTransacts[i].TransactID == Owner.TransactID)
                    return true;
            }
            return false;
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

        public String[] GetStatisticParameters()
        {
            Int64 B = 0;
            for (int i = 0; i < Tables.TableOfQUEUEs.getInstanse().BlocksList.Length; i++)
                if (Tables.TableOfQUEUEs.getInstanse().BlocksList[i].LineInSourceCode == LineInSourceCode)
                    B = i;
            
            return new String[] {Enters.ToString(),UTIL.ToString(),AVE_TIME.ToString(), AVAIL.ToString(),stOWNER.ToString(),
                Tables.TableOfVariables.getInstanse().
                GetVariableNameByNumericIndex(Tables.TableOfVariables.getInstanse().ListOfDeviceVariables, B),
                this.GetType().ToString()};
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
                    Parameters[ParameterIndex] = Tables.TableOfVariables.getInstanse().BlocksList[P].ElementNumber;
                }
            }
        }
    }
}
