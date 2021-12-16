using BigDataAnalyzer.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BigDataAnalyzer.FileOpeners
{
    /// <summary>
    /// This class choose a correct file opener for concrete goal
    /// </summary>
    public class FileOpenersMediator
    {
        public void ChooseRightOpener(int storageType, OpenFileDialog openFileDialog, DataGridView[] dataTables, int methodsCount, Form options, object[] objToPaint)
        {
            if(storageType == 1)
            {
                new FileOpenerTeaching().OpenTeachingFile(storageType, openFileDialog, dataTables[0],
                    methodsCount, (OptionsForm)options, new object[] { objToPaint[0] });
            }
            else if(storageType == 3)
            {
                new FileOpenerAnalyzing().OpenAnalyzingFile(storageType, openFileDialog, dataTables[1],
                    methodsCount, (OptionsForm)options, new object[] { objToPaint[1] });
            }
            else
            {
                MessageBox.Show("Нет реализации метода выбора файлов для данного типа хранилища");
            }
        }
    }
}
