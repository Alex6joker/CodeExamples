using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSSEmu.GPSSBlocksImplementaion
{
    public class GPSSBlockGENERATE : GPSSBlocks, ITransactGeneration
    {
        Int64 Family;           // Случайное значение "семейства" транзактов, сгенерированных данным блоком.
        /// <summary>
        /// 0 - среднее значение интервала времени;
        /// 1 - разброс или модификатор среднего значения(по умолчанию ноль); 
        /// 2 - время появления первого транзакта; 
        /// 3 - общее число генерируемых транзактов;
        /// 4 - уровень приоритета каждого транзакта;(от 0 до 127,значение по умолчанию 0); 
        /// 5 - число параметров (по умолчанию 12);
        /// </summary>
        String StringParameter;             // 6 - тип параметра ( F - полнословный, Н - полусловный - по умолчанию ). 
        Boolean IsLimited;
        Int64 GenerationPeriod;
        Int64 GENERATEBlockTimeMoment;  // Локальное время в блоке GENERATE. Сбрасывается при каждом создании транзакта.

        public GPSSBlockGENERATE()
        {
            IsLimited = false;
            Family = new Random(new Random().Next()).Next();
            NumberOfParameters = 7;
            ThisBlockParametersTypes = new Type[] { typeof(Int64), typeof(Int64),
                typeof(Int64), typeof(Int64), typeof(Int64), typeof(Int64), typeof(String)};
            Parameters = new Int64[NumberOfParameters - 1];
        }

        public override void WriteBlockInfo(Object[] Params, Int32 Line)
        {
            LineInSourceCode = Line;
            GettedParameters = new List<Object>();
            ParametersAsserter.getInstanse().SetGPSSBlockParameters(this, NumberOfParameters, ref GettedParameters, Params);
            WriteParametersInfo();
            GenerationPeriod = SetNewGenerationPeriod();
            GENERATEBlockTimeMoment += Parameters[2]; // Установка времени появления первого транзакта
        }

        Int64 SetNewGenerationPeriod()
        {
            return Parameters[1] == 0 ? Parameters[0] :
                Parameters[0] + new Random(DateTime.Now.Millisecond).Next((int)Parameters[1]*-1, (int)Parameters[1]);
        }

        public override Boolean TransactIn(ref List<GPSSBlocks> TableOfAllBlock,
            ref Tables.GPSSTable[] TablesArray, ref Transact T, ref Int64 Counter, ref List<Transact> TList, Int64 CurrentTimeMoment)
        {   // Из блока GENERATE транзакт только выходит на следующий
            T.CodeLine++;
            Enters++;
            return true;
        }

        public void CheckCreateTransactionConditions(ref List<Transact> TransactList, Int64 CurrentTimeMoment)
        {   // Когда подходит время для генерации транзакта, необходимо его создать
            if(GENERATEBlockTimeMoment == GenerationPeriod)
            {
                TransactList.Add(new Transact(this.LineInSourceCode));

                GENERATEBlockTimeMoment = 1;
                GenerationPeriod = SetNewGenerationPeriod();
            }
            else
            {
                if (GENERATEBlockTimeMoment <= CurrentTimeMoment)
                    GENERATEBlockTimeMoment++;
            }
        }

        void WriteParametersInfo()
        {
            for(Int32 ParameterIndex = 0; ParameterIndex < ThisBlockParametersTypes.Length; ParameterIndex++)
            {
                Object Parameter = GettedParameters.ElementAt<Object>(ParameterIndex);
                if(ParameterIndex != 6)
                {
                    try
                    {
                        Parameters[ParameterIndex] = (long)Convert.ChangeType(Parameter, ThisBlockParametersTypes[ParameterIndex]);
                    }
                    catch(IndexOutOfRangeException ex)
                    {
                        Array.Resize<Int64>(ref Parameters, Parameters.Length + 1);
                        Parameters[ParameterIndex] = (long)Convert.ChangeType(Parameter, ThisBlockParametersTypes[ParameterIndex]);
                    }
                    catch(InvalidCastException ex)
                    {
                        if (ThisBlockParametersTypes[ParameterIndex] == typeof(Int64))
                            Parameters[ParameterIndex] = 0;
                        else if (ThisBlockParametersTypes[ParameterIndex] == typeof(String))
                            StringParameter = String.Empty;
                    }
                    catch (FormatException ex)
                    {
                        Int64 P = Tables.TableOfVariables.getInstanse().TryFindNecessaryVariableInList(
                            Parameter.ToString(), Tables.TableOfVariables.getInstanse().GetType());
                        Parameters[ParameterIndex] = Tables.TableOfVariables.getInstanse().BlocksList[P].ElementNumber;
                    }
                }
                else
                {
                    if (Parameter == null)
                        StringParameter = "H";
                    else
                        StringParameter = (String)Convert.ChangeType(Parameter, ThisBlockParametersTypes[ThisBlockParametersTypes.Length - 1]);
                }
            }
        }
    }
}
