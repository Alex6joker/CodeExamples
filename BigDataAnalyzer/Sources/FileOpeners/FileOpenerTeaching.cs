using Accord.IO;
using Accord.Math;
using BigDataAnalyzer.Forms;
using BigDataAnalyzer.Painting;
using BigDataAnalyzer.Storage;
using System.Data;
using System.IO;
using System.Windows.Forms;
using ZedGraph;

namespace BigDataAnalyzer.FileOpeners
{
    public class FileOpenerTeaching
    {
        public void OpenTeachingFile(int storageType, OpenFileDialog openFileDialog, DataGridView dataTable, int methodsCount, OptionsForm optionsForm, object[] objToPaint)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileDialog.FileName;
                string extension = Path.GetExtension(filename);
                if (extension == ".xls" || extension == ".xlsx")
                {
                    ExcelReader db = new ExcelReader(filename, true, false);
                    TableSelectDialog t = new TableSelectDialog(db.GetWorksheetList());
                    string[] columnNames;

                    if (t.ShowDialog() == DialogResult.OK)
                    {
                        for (int currentMethodID = 0; currentMethodID < methodsCount; currentMethodID++)
                        {
                            StorageObject storageObject = StorageObjectGetter.getInstanse().GetStorageByMethodID(currentMethodID, storageType);

                            storageObject.SetStoredObject(db.GetWorksheet(t.Selection));

                            ((DataTable)storageObject.GetStoredObject()).ToMatrix(out columnNames);

                            //if (storageType == 1)
                            //{
                            dataTable.DataSource = ((DataTable)storageObject.GetStoredObject());

                            // Fill options data by loaded DataTable
                            optionsForm.WriteNewOptionsContent(columnNames);
                            //}
                            //else if (storageType == 3)
                            //{
                            //    this.dgvTestingSource.DataSource = ((DataTable)storageObject.GetStoredObject());
                            //    objToPaint = new object[] { graphTesting };
                            //}

                            // Create a plot from input data
                            new AIPaintingClass(currentMethodID, storageType, methodsCount, 1,
                                objToPaint,
                                new int[] { optionsForm.GetFirstDataIndex(),
                                            optionsForm.GetSecondDataIndex(),
                                            optionsForm.GetClassifierIndex() }).StartPaint();

                        }
                        new PaintingHeplers().RefreshGraph((ZedGraphControl)objToPaint[0],
                                (ZedGraphControl)StorageObjectGetter.getInstanse().GetStorageByMethodID(0, storageType).GetStoredGraphObject());
                    }
                }
            }
        }
    }
}
