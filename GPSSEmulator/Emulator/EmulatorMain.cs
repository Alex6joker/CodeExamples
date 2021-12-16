using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GPSSEmu.GPSSBlocksImplementaion;
using GPSSEmu.Tables;

namespace GPSSEmu.Emulator
{
    /// <summary>
    /// Один из классов верхнего уровня. Содержит методы по инициированию построения таблиц блоков GPSS
    /// Входные данные: построчно разделенный список (исходный код программы GPSS).
    /// </summary>
    public class EmulatorMain
    {
        List<String> LineByLineSourceCode;
        GPSSTable[] TablesArray;
        List<GPSSBlocks> TableOfAllBlock; // Таблица блоков GPSS, построчно

        public EmulatorMain(List<String> nLineByLineSourceCode)
        {
            LineByLineSourceCode = nLineByLineSourceCode;
            TablesArray = new GPSSTable[] { TableOfQUEUEs.getInstanse(), TableOfDevices.getInstanse(),
                TableOfSTORAGEs.getInstanse(), TableOfGENERATE.getInstanse(), TableOfVariables.getInstanse(),
                TableOfSTART.getInstanse()};
            TableOfAllBlock = new List<GPSSBlocks>();

            DetermineVariablesAndDeleteThemFromSorceCode();
            // Подготовка всех вспомогательных таблиц.
            CreateTablesGPSSSourceCode();
            // После создания основных таблиц создаются блоки GPSS.
            CreateBlocksByGPSSSourceCode();
            // Запуск процесса эмуляции
            EmulationProcessor Processor = new EmulationProcessor(TablesArray, TableOfAllBlock);
            Processor.StartEmulationProcess();
            Object[] Stats = Processor.GetEmulationProcessStatistic();
            EmulationStaticticsForm StatsForm = new EmulationStaticticsForm(Stats);
            StatsForm.Show();
        }

        public void DetermineVariablesAndDeleteThemFromSorceCode()
        {   // Определяем переменные, в дальнейшем не требуется знать номер строки исходного кода
            List<String> Vars = LineByLineSourceCode.Where(w => w.Contains("EQU")).ToList<String>();
            for (int i = 0; i < Vars.Count; i++ )
                new GPSSBlocksTableChoser().CreateGPSSBlockTable(Vars.ElementAt<String>(i), 0);
            
            // Удаляем из списка переменные            
            LineByLineSourceCode = LineByLineSourceCode.Where(w => !w.Contains("EQU")).ToList<String>();
        }

        public void CreateTablesGPSSSourceCode()
        {
            GPSSTable Block = null;
            for(Int32 SourceCodeLine = 0; SourceCodeLine < LineByLineSourceCode.Count; SourceCodeLine++)
            {
                Block = new GPSSBlocksTableChoser().CreateGPSSBlockTable(LineByLineSourceCode.ElementAt<String>(SourceCodeLine), SourceCodeLine);
            }
        }

        /// <summary>
        /// Метод создает посылки для создания блоков GPSS и включает их в соответствующую таблицу.
        /// </summary>
        public void CreateBlocksByGPSSSourceCode()
        {
            for(Int32 SourceCodeLine = 0; SourceCodeLine < LineByLineSourceCode.Count; SourceCodeLine++)
            {
                TableOfAllBlock.Add(new GPSSBlocksCreateChoser().CreateGPSSBlock(LineByLineSourceCode.ElementAt<String>(SourceCodeLine), SourceCodeLine));
                
            }
        }
    }
}
