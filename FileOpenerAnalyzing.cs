using Accord.Math;
using BigDataAnalyzer.Forms;
using BigDataAnalyzer.Painting;
using BigDataAnalyzer.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace BigDataAnalyzer.FileOpeners
{
    class FileOpenerAnalyzing
    {
        public void OpenAnalyzingFile(int storageType, OpenFileDialog openFileDialog, DataGridView dataTable, int methodsCount, Form options, object[] objToPaint)
        {
            OptionsForm optionsForm = (OptionsForm)options;
            LoadAnalyzePicturesForm analyzePicturesForm = new LoadAnalyzePicturesForm(storageType, methodsCount, options, objToPaint);

            analyzePicturesForm.ShowDialog();

            for (int currentMethodID = 0; currentMethodID < methodsCount; currentMethodID++)
            {
                StorageObject storageObject = StorageObjectGetter.getInstanse().GetStorageByMethodID(currentMethodID, storageType);

                if (analyzePicturesForm.GetDataTable().Rows.Count > 0)
                {
                    storageObject.SetStoredObject(analyzePicturesForm.GetDataTable());

                    dataTable.DataSource = ((DataTable)storageObject.GetStoredObject());

                    // Create a plot from input data
                    new AIPaintingClass(currentMethodID, storageType, methodsCount, 1,
                        objToPaint,
                        new int[] { optionsForm.GetFirstDataIndex(),
                                            optionsForm.GetSecondDataIndex(),
                                            optionsForm.GetClassifierIndex() }).StartPaint();
                }
            }
            new PaintingHeplers().RefreshGraph((ZedGraphControl)objToPaint[0],
                    (ZedGraphControl)StorageObjectGetter.getInstanse().GetStorageByMethodID(0, storageType).GetStoredGraphObject());
        }
    }
}
