using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSSEmu
{
    /// <summary>
    /// Отвечает за присваивание в общем виде параметров блоку GPSS
    /// </summary>
    public class ParametersAsserter
    {
        static ParametersAsserter InstanceOfParametersAsserter;

        public static ParametersAsserter getInstanse()
        {
            if (InstanceOfParametersAsserter == null)
                InstanceOfParametersAsserter = new ParametersAsserter();
            return InstanceOfParametersAsserter;
        }

        public void SetGPSSBlockParameters(GPSSBlocksImplementaion.GPSSBlocks GPSSBlock, Int32 ParametersCount,
            ref List<Object> GPSSBlockParamsReferenсes, Object[] ParamsToAssert)
        {
            for(Int32 i = 0; i < ParametersCount; i++)
            {
                try
                {
                    GPSSBlockParamsReferenсes.Add(ParamsToAssert[i]);
                }
                catch(IndexOutOfRangeException ex)
                {   // Неуказанные параметры в GPSS равны 0, посылаем "ничего"
                    GPSSBlockParamsReferenсes.Add(null);
                }
            }
        }
    }
}
