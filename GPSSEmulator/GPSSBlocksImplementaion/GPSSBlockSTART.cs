using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSSEmu.GPSSBlocksImplementaion
{
    class GPSSBlockSTART: GPSSBlocks
    {
        public GPSSBlockSTART()
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
}
