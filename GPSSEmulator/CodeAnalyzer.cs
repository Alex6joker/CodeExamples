using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSSEmu
{
    /// <summary>
    /// Класс производит перевод содержимого TextBox в String[] для дальнейшего анализа
    /// </summary>
    public class CodeAnalyzer
    {
        String Code;

        public CodeAnalyzer(String nCode)
        {
            Code = nCode;
        }

        /// <summary>
        /// Перевод начального содержимого окна исходного кода GPSS программы
        /// к виду, содержащему только блоки, переменные и параметры.
        /// </summary>
        /// <returns>
        /// Результат: построчно разделенный исходный код GPSS программы.
        /// </returns>
        public String[] ConvertTextBoxTextToLineByLineStringArray()
        {
            String[] ResultStringArray = Code.Split(new Char[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);

            for (Int32 ResultStringArrayIndex = 0; ResultStringArrayIndex < ResultStringArray.Length; ResultStringArrayIndex++)
                if(ResultStringArray[ResultStringArrayIndex].Contains<Char>('\t'))
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    String[] SplittedLine = ResultStringArray[ResultStringArrayIndex].Split(new Char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    for (Int32 SplittedLineIndex = 0; SplittedLineIndex < SplittedLine.Length; SplittedLineIndex++)
                    {
                        if(stringBuilder.Length != 0)
                            stringBuilder.Insert(stringBuilder.Length, ' ');
                        stringBuilder.Insert(stringBuilder.Length, SplittedLine[SplittedLineIndex]);
                    }
                    ResultStringArray[ResultStringArrayIndex] = stringBuilder.ToString();
                }
            return ResultStringArray;
        }

        /// <summary>
        /// Перевод непострочно разделенного кода GPSS программы
        /// к построчному виду.
        /// </summary>
        /// <returns>
        /// Результат: массив данных исходного кода, разделенных построчно.
        /// </returns>
        public List<String> CompileSourceCodeLineByLine(String[] SourceCode)
        {
            List<String> LineByLineSourceCode = new List<String>();

            for (Int32 SourceCodeLine = 0; SourceCodeLine < SourceCode.Length; SourceCodeLine++)
                LineByLineSourceCode.Add(SourceCode[SourceCodeLine]);

            return LineByLineSourceCode;
        }
    }
}
