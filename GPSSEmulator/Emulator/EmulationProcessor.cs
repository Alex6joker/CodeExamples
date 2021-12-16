using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GPSSEmu.Tables;
using GPSSEmu.GPSSBlocksImplementaion;

namespace GPSSEmu.Emulator
{
    /// <summary>
    /// Класс выполняет все основные операции эмуляции выполнения GPSS кода.
    /// </summary>
    public class EmulationProcessor
    {
        Int64 StartBlockParam;      // Условие окончания моделирования - достижение данного значения счетчиком.
        Int64 CurrentTimeMoment;    // Текущий момент времени выполнения моделирования.
        Int64 StartTime;
        Int64 Counter;              // Счетчик уничтоженных транзактов - условие выхода из моделирования.
        GPSSTable[] TablesArray;    // Таблица блоков GPSS
        List<GPSSBlocks> TableOfAllBlock; // Экземпляры блоков GPSS, построчно
        List<Transact> ListOfTransacts; // Список тразнактов

        public EmulationProcessor(GPSSTable[] nTablesArray, List<GPSSBlocks> nTableOfAllBlock)
        {
            StartBlockParam = 0;
            CurrentTimeMoment = 0;
            Counter = 0;
            StartTime = 0;
            TablesArray = nTablesArray;
            TableOfAllBlock = nTableOfAllBlock;
            ListOfTransacts = new List<Transact>();
        }

        /// <summary>
        /// Функция начала выполнения процесса эмуляции выполнения GPSS кода
        /// </summary>
        public void StartEmulationProcess()
        {
            Boolean EndEarlier = false;
            // Забираем параметр блока START. Он может быть только один, как и сам параметр блока.
            StartBlockParam = TableOfAllBlock.ElementAt((Int32)TablesArray[5].BlocksList[0].LineInSourceCode).Parameters[0];
            for (CurrentTimeMoment = 0; Counter < StartBlockParam; CurrentTimeMoment++)
            {
                if (!EmulationProcess())
                {
                    EndEarlier = true;
                    break;
                }
            }
            // Пересчет статистики в блоках для данного момента времени
            if (!EndEarlier)
                CurrentTimeMoment--;
            CheckAllGENERATEBlocks();
            StartWorkWithAllTransactions(true);
            UpdateBlocksStats(true);
            StartBlockParam = 0;
            Transact.sID = 1;
        }

        public Object[] GetEmulationProcessStatistic()
        {
            Object[] EmulationProcessStatistic = new Object[0];

            // Сбор общей информации
            Array.Resize<Object>(ref EmulationProcessStatistic, EmulationProcessStatistic.Length + 1);
            EmulationProcessStatistic[EmulationProcessStatistic.Length - 1] =
                new String[] { StartTime.ToString(), CurrentTimeMoment.ToString(),
                    (TableOfAllBlock.Count - 1 - TablesArray[5].BlocksList.Length).ToString(),
                    (TablesArray[1].BlocksList.Length).ToString(),
                    (TablesArray[2].BlocksList.Length).ToString()};


            Array.Resize<Object>(ref EmulationProcessStatistic, EmulationProcessStatistic.Length + 1);
            EmulationProcessStatistic[EmulationProcessStatistic.Length - 1] =
                new String[] { "END_TAB" };

            // Сбор информации о переменных            
            // Сначала получаем список переменных для данного типа блока
            for (int i = 0; i < 2; i++)
            {
                List<String> L = TableOfVariables.getInstanse().GetVariableListByType(TablesArray[i].GetType());
                // Полученный список переменных нужно соотнести с таким же списком значений переменных
                List<Int64> Indexs = new List<long>(L.Count);
                for (int j = 0; j < L.Count; j++)
                {
                    Indexs.Add(TableOfVariables.getInstanse().TryFindNecessaryVariableInList(L.ElementAt<string>(j), TablesArray[i].GetType()));
                }
                for (int j = 0; j < L.Count; j++)
                {
                    Array.Resize<Object>(ref EmulationProcessStatistic, EmulationProcessStatistic.Length + 1);
                    EmulationProcessStatistic[EmulationProcessStatistic.Length - 1] =
                    new String[] { L.ElementAt<string>(j), Indexs.ElementAt<Int64>(j).ToString(), TablesArray[i].GetType().ToString() };
                }
            }
            Array.Resize<Object>(ref EmulationProcessStatistic, EmulationProcessStatistic.Length + 1);
            EmulationProcessStatistic[EmulationProcessStatistic.Length - 1] =
                new String[] { "END_TAB" };
            
            for (Int64 GPSSBlockIndex = 0; GPSSBlockIndex < TableOfAllBlock.Count; GPSSBlockIndex++)
            {
                String[] Stats;
                try
                {
                    Stats = ((IStatisticRecalculation)(TableOfAllBlock.ElementAt((Int32)GPSSBlockIndex))).
                        GetStatisticParameters();
                }
                catch (InvalidCastException ex)
                {
                    continue;
                }
                Array.Resize<Object>(ref EmulationProcessStatistic, EmulationProcessStatistic.Length + 1);
                EmulationProcessStatistic[EmulationProcessStatistic.Length - 1] = Stats;
            }

            for (int Table = 0; Table < TablesArray.Length; Table++)
                TablesArray[Table].DestroyTable();
            
            return EmulationProcessStatistic;
        }

        /// <summary>
        /// Функция выполнения процесса эмуляции выполнения GPSS кода
        /// </summary>
        Boolean EmulationProcess()
        {
            Boolean MustContinue = true;
            // Проход по всем блокам GENERATE
            CheckAllGENERATEBlocks();
            // Обработка всех транзактов
            if (!StartWorkWithAllTransactions(false))
                MustContinue = false;
            // Пересчет статистики в блоках для данного момента времени
            UpdateBlocksStats(false);
            return MustContinue;
        }

        /// <summary>
        /// Метод выполняет проход по всем блокам,
        /// вызывая их методы пересчета статистики для данного момента времени
        /// </summary>
        void UpdateBlocksStats(Boolean IsOneMoreTurn)
        {
            for (Int64 GPSSBlockIndex = 0; GPSSBlockIndex < TableOfAllBlock.Count; GPSSBlockIndex++)
            {
                try
                {
                    ((IStatisticRecalculation)(TableOfAllBlock.ElementAt((Int32)GPSSBlockIndex))).
                        StatisticRecalculation(CurrentTimeMoment, ref ListOfTransacts,ref TableOfAllBlock, IsOneMoreTurn);
                }
                catch(InvalidCastException ex)
                {
                    continue;
                }
            }                    
        }

        /// <summary>
        /// Метод осуществляет подготовку обработки транзактов
        /// </summary>
        Boolean StartWorkWithAllTransactions(Boolean IsOneMoreTurn)
        {
            // Запоминаем старую емкость списка, так как транзакты могут удаляться в блоке TERMINATE
            Int32 OldListCount = ListOfTransacts.Count;
            Int32 Offset = 0;   // Длина списка изменичива, необходимо делать поправку

            if (IsOneMoreTurn)
            {
                for (Int32 TransactIndex = 0; TransactIndex < ListOfTransacts.Count; TransactIndex++)
                {
                    WorkWithAllTransactions(ListOfTransacts.ElementAt<Transact>(TransactIndex), true);
                }
                return false;
            }

            for (Int32 TransactIndex = 0; TransactIndex < ListOfTransacts.Count; TransactIndex++)
            {
                if (!WorkWithAllTransactions(ListOfTransacts.ElementAt<Transact>(TransactIndex), false))
                    return false;
                // Анализ того, насколько изменилась длина списка
                Offset = OldListCount - ListOfTransacts.Count;
                if (Offset != 0) // Если длина списка изменилась, то необходим сдвиг на Offset параметра цикла
                {
                    OldListCount = ListOfTransacts.Count;
                    TransactIndex -= Offset;
                }
            }
            return true;
        }

        /// <summary>
        /// Метод осуществляет обработку всех транзактов,
        /// которые существуют, по очереди на данный момент времени
        /// </summary>
        Boolean WorkWithAllTransactions(Transact T, Boolean IsOneMoreTurn)
        {   // Вызываем обработчик вхождения транзакта в соответствующий блок GPSS,
            // пока блок не сообщит нам, что транзакт удерживается или уничтожен

            // Если после TERMINATE достигнуто условие окончания, то продолжать не нужно
            if (Counter >= StartBlockParam && !IsOneMoreTurn)
                return false;
            if (Counter >= StartBlockParam && IsOneMoreTurn)
            {
                while (TableOfAllBlock.ElementAt<GPSSBlocks>((Int32)T.CodeLine).TransactIn(ref TableOfAllBlock, ref TablesArray,
                ref T, ref Counter, ref ListOfTransacts, CurrentTimeMoment))
                    return false;
            }

            while (TableOfAllBlock.ElementAt<GPSSBlocks>((Int32)T.CodeLine).TransactIn(ref TableOfAllBlock, ref TablesArray,
                ref T, ref Counter, ref ListOfTransacts, CurrentTimeMoment))
            {
                if (Counter >= StartBlockParam && !IsOneMoreTurn)
                    return false;
            }
            return true;
        }
        
        /// <summary>
        /// Метод осущетвляет проход по всем блокам GENERATE программы,
        /// проверяет, нужно ли создавать новые транзакты и создает их при необходимости
        /// </summary>
        void CheckAllGENERATEBlocks()
        {
            for(Int64 Index = 0; Index < TablesArray[3].BlocksList.Length; Index++)
            {   // Только классы с интерфейсом ITransactGeneration могут выполнить нужный метод
                ((ITransactGeneration)(TableOfAllBlock.ElementAt((Int32)TablesArray[3].BlocksList[Index].Number))).
                    CheckCreateTransactionConditions(ref ListOfTransacts, CurrentTimeMoment);
            }
        }
    }
}
