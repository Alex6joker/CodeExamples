using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * Для подключения БД MySQL к проекту Xna
 * необходимо добавить в References MySql.Data и System.Data,
 * если MySql.Data отсутствует в списке, то необхдимо произвести установку
 * MySql connector - библиотеки для подключения к БД MySql.
 * 
 * На запросы SELECT возвращается массив объектов String
 * Запросы UPDATE и DELETE данные не возвращают, главным
 * атрибутом правильности работы является отработка алгоритма запроса к БД
*/
using MySql.Data.MySqlClient;

namespace SAPR
{
    class DataBase
    {
        private string ConnectionString; // Строка, содержащая данные для соединения
        private MySqlConnection Connection; // Переменная, содержащая методы и данное о соединении
        private MySqlCommand Command;   // Содержит команду, сожержащую в себе запрос к БД
        private MySqlDataReader Reader; // Переменная для чтения результатов запроса
        private Int32 Rows; // Количество записей в таблице (будет хранить в себе число компонентов)
        private Int32 Fields; // Количество столбцов в возращенном результате

        public DataBase(String ConnStr)
        {
            ConnectionString = ConnStr;
            Connection = new MySqlConnection(ConnectionString);       
            // Рекомендуется .Open() помещать в try catch
            Connection.Open();
            /*  Примеры запросов
                String[] Qur = MakeQuery("SELECT `Car`, `Age`, `Price` FROM `Autos` WHERE 2 ORDER BY 'Car'");
                String[] a = RepeatLastQuery();
                String[] b = MakeQuery("CALL `ChangeAutoName`('Mitsubishi L200', '1')");
            */
            Rows = 0;
            Fields = 0;
        }

        public Int32 GetLastQueryRowsCount()
        {
            return Rows;
        }
        public Int32 GetLastQueryFieldsCount()
        {
            return Fields;
        }

        public void CloseConnection()
        {   // При завершении работы закрываем соединение
            Connection.Close();
        }

        public String[] MakeQuery(String Query)
        {   // Общая функция для совершения запросов к БД
            // Записываем запрос в переменную типа MySqlCommand
            Command = Connection.CreateCommand();
            Command.CommandText = Query;

            Rows = 0;
            Fields = 0;
            // Производим запрос к БД
            return CallToMySQL();
        }

        private String[] CallToMySQL()
        {
            Reader = Command.ExecuteReader();
            String[] Result = new String[1];
            Byte[] ByteVar = {0};

            // Общая длина массива String[]
            int TotalLenght = 1;
            // Считываем результат запроса
            while (Reader.Read())
            {
                Rows++;
                if(Fields == 0)
                    Fields = Reader.FieldCount;
                for (int i = 0; i < Reader.FieldCount; i++)
                {
                    Result[TotalLenght - 1] = Reader.GetString(i);
                    int len = Result[TotalLenght - 1].Length;
                    TotalLenght++;
                    // Если результатов больше, чем размер массива, то расширяем его
                    if (TotalLenght > Result.Length)
                        Array.Resize(ref Result, Result.Length + 1);
                }
            }
            Array.Resize(ref Result, Result.Length - 1);
            Reader.Close();

            return Result;
        }

        public String[] RepeatLastQuery()
        {
            Rows = 0;
            Fields = 0;
            if (Command.CommandText.Length != 0)
                return CallToMySQL();
            else
                return null;
        }
    }
}
