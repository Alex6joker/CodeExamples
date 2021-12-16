using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diplo
{
    public partial class NetworkLogForm : Form
    {
        String[] TextsToAddDelete;
        String[] TextsToSymbolOrText;
        String[] HelpTexts;
        String[] CommonFile;

        public NetworkLogForm()
        {
            InitializeComponent();

            String[] nTextsToAdd = {"добавил", "удалил"};
            TextsToAddDelete = nTextsToAdd;
            String[] nTextsToSymbolOrText = { "символ", "текст" };
            TextsToSymbolOrText = nTextsToSymbolOrText;
            String[] nHelpTexts = { "в", "из" };
            HelpTexts = nHelpTexts;
            String[] nCommonFile = { "общий файл", "общего файла" };
            CommonFile = nCommonFile;
        }

        public RichTextBox GetLog()
        {
            return Log;
        }

        public void ClearLog()
        {
            if (Log.InvokeRequired)
                Log.Invoke((Action)ClearLog);
            else
                Log.Clear();
        }

        public void AddInformationToLog(LogInformationTypes InfoType, String[] Text, String Symbol, String Position)
        {
            if (Log.InvokeRequired)
                Log.Invoke((Action<LogInformationTypes, String[], String, String>)AddInformationToLog, new object[] { InfoType, Text, Symbol, Position });
            else
            {
                String UserText = "Пользователь";
                String FullUserText;
                int Add = (int)InfoType;

                if (Add == (int)LogInformationTypes.LOG_INFO_ADD_SYMBOL || Add == (int)LogInformationTypes.LOG_INFO_DELETE_SYMBOL)
                {
                    int SymblosLenght = Symbol.Length;
                    String LogLangSymbols = Symbol;
                    LogLangSymbols = ToHumanLang(LogLangSymbols);
                    for (int i = 0; i < LogLangSymbols.Length; i++)
                    {
                        String CurrentSymbol = LogLangSymbols.Substring(i, 1);
                        if (CurrentSymbol == " ")
                            CurrentSymbol = "\"Пробел\"";
                        else if (CurrentSymbol == "\n")
                            CurrentSymbol = "\"Новая строка\"";
                        else if (CurrentSymbol == "\r")
                            continue;
                        else if (CurrentSymbol == "\b")
                        {
                            int Digital = 8;
                            CurrentSymbol = Digital.ToString();
                        }
                        if (Add == (int)LogInformationTypes.LOG_INFO_ADD_SYMBOL)
                            FullUserText = String.Join(" ", new object[] { UserText, Text[0], "имя:", Text[2], TextsToAddDelete[Add],
                            TextsToSymbolOrText[0], CurrentSymbol, HelpTexts[Add], CommonFile[Add], "в позицию", (Convert.ToInt32(Position) + i).ToString()});
                        else
                            FullUserText = String.Join(" ", new object[] { UserText, Text[0], "имя:", Text[2], TextsToAddDelete[Add],
                            TextsToSymbolOrText[0], HelpTexts[Add], CommonFile[Add], "в позиции", (Convert.ToInt32(Position) + 1 + i).ToString()});
                        if (Log.Text.Length == 0)
                        {
                            Log.Text = Log.Text.Insert(Log.Text.Length, FullUserText);
                        }
                        else
                        {
                            Log.Text = Log.Text.Insert(Log.Text.Length, "\n");
                            Log.Text = Log.Text.Insert(Log.Text.Length, FullUserText);
                        }
                    }                    
                }                
            }
        }
        String ToHumanLang(String Symbols)
        {
            while (Symbols.Contains("UNIQUE_SPACE_TEXT"))
            {
                String UNIQUE_SPACE_TEXT = "UNIQUE_SPACE_TEXT";
                int UNIQUE_SPACE_TEXT_INDEX = Symbols.IndexOf(UNIQUE_SPACE_TEXT);
                Symbols = Symbols.Remove(UNIQUE_SPACE_TEXT_INDEX, UNIQUE_SPACE_TEXT.Length);
                Symbols = Symbols.Insert(UNIQUE_SPACE_TEXT_INDEX, " ");
            }
            while (Symbols.Contains("UNIQUE_NEW_LINE_TEXT"))
            {
                String UNIQUE_NEW_LINE_TEXT = "UNIQUE_NEW_LINE_TEXT";
                int UNIQUE_NEW_LINE_TEXT_INDEX = Symbols.IndexOf(UNIQUE_NEW_LINE_TEXT);
                Symbols = Symbols.Remove(UNIQUE_NEW_LINE_TEXT_INDEX, UNIQUE_NEW_LINE_TEXT.Length);
                char NewLineSymbol = new Char();
                NewLineSymbol = '\n';
                Symbols = Symbols.Insert(UNIQUE_NEW_LINE_TEXT_INDEX, NewLineSymbol.ToString());
            }
            while (Symbols.Contains(((int)Keys.Delete).ToString()))
            {
                String Delete_TEXT = ((int)Keys.Delete).ToString();
                int UNIQUE_NEW_LINE_TEXT_INDEX = Symbols.IndexOf(Delete_TEXT);
                Symbols = Symbols.Remove(UNIQUE_NEW_LINE_TEXT_INDEX, Delete_TEXT.Length);
                char BackDeleteSymbol = new Char();
                BackDeleteSymbol = '\b';
                Symbols = Symbols.Insert(UNIQUE_NEW_LINE_TEXT_INDEX, BackDeleteSymbol.ToString());
            }
            while (Symbols.Contains(((int)Keys.Back).ToString()))
            {
                String Back_TEXT = ((int)Keys.Back).ToString();
                int UNIQUE_NEW_LINE_TEXT_INDEX = Symbols.IndexOf(Back_TEXT);
                Symbols = Symbols.Remove(UNIQUE_NEW_LINE_TEXT_INDEX, Back_TEXT.Length);
                char BackDeleteSymbol = new Char();
                BackDeleteSymbol = '\b';
                Symbols = Symbols.Insert(UNIQUE_NEW_LINE_TEXT_INDEX, BackDeleteSymbol.ToString());
            }
            return Symbols;
        }

        private void NetworkLogForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void NetworkLogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = true;
            Hide();
        }
    }    

    public enum LogInformationTypes
    {
        LOG_INFO_ADD_SYMBOL = 0,
        LOG_INFO_DELETE_SYMBOL,
        LOG_INFO_CONNECTED_NEW_USER,
        LOG_INFO_DELETING_USER
    }
}
