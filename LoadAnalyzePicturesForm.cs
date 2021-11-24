using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BigDataAnalyzer.FileOpeners;

namespace BigDataAnalyzer.Forms
{
    public partial class LoadAnalyzePicturesForm : Form
    {
        int methodsCount;
        int photoIndex;
        string defaultRectangleNamePrefix = "Область №";
        List<RectangleOnPhotos> rectangleOnPhotosList;

        public LoadAnalyzePicturesForm(int storageType, int methodsCount, Form options, object[] objToPaint)
        {
            this.DialogResult = DialogResult.None;
            this.methodsCount = methodsCount;

            InitializeComponent();

            rectangleOnPhotosList = new List<RectangleOnPhotos>();
            photoIndex = 0;

            FillDataGridColumns();

            openPhotosDialog.InitialDirectory = Path.Combine(Application.StartupPath, "..//..//Resources//Images");
        }

        #region UI
        private void startGettingColorsButton_Click(object sender, EventArgs e)
        {
            StartCalculateColors();
        }

        private void loadPhotoButton_Click(object sender, EventArgs e)
        {
            LoadPhotos();
        }

        private void pictureBoxPhoto_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                SetNextImageInPictureBox();
            }
        }

        void SetNextImageInPictureBox()
        {
            photoIndex++;
            SetImageInPictureBox();
        }

        private void pictureBoxPhoto_Paint(object sender, PaintEventArgs e)
        {
            // Draw the rectangle...
            if (pictureBoxPhoto.Image != null && rectangleListComboBox.SelectedIndex != -1)
            {
                for (int i = 0; i < rectangleOnPhotosList.Count; i++)
                {
                    if (rectangleOnPhotosList[rectangleListComboBox.SelectedIndex].rectangle != null &&
                        rectangleOnPhotosList[rectangleListComboBox.SelectedIndex].rectangle.Width > 0 &&
                        rectangleOnPhotosList[rectangleListComboBox.SelectedIndex].rectangle.Height > 0)
                    {
                        e.Graphics.FillRectangle(rectangleOnPhotosList[rectangleListComboBox.SelectedIndex].selectionBrush,
                            rectangleOnPhotosList[rectangleListComboBox.SelectedIndex].rectangle);
                    }
                }
            }
        }

        private void pictureBoxPhoto_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && rectangleListComboBox.SelectedIndex != -1)
            {
                if ((rectangleOnPhotosList[rectangleListComboBox.SelectedIndex].rectangle.Contains(e.Location)))
                {
                    //MessageBox.Show("Right click");
                }
            }
        }

        private void pictureBoxPhoto_MouseMove(object sender, MouseEventArgs e)
        {
            if (rectangleListComboBox.SelectedIndex != -1)
            {
                RectangleOnPhotos rectangleOnPhotos = new RectangleOnPhotos(rectangleOnPhotosList[rectangleListComboBox.SelectedIndex].name,
                rectangleOnPhotosList[rectangleListComboBox.SelectedIndex].type,
                rectangleOnPhotosList[rectangleListComboBox.SelectedIndex].rectangle,
                rectangleOnPhotosList[rectangleListComboBox.SelectedIndex].rectStartPoint);
                if (e.Button != MouseButtons.Left)
                    return;
                Point tempEndPoint = e.Location;
                rectangleOnPhotos.rectangle.Location = new Point(
                    Math.Min(rectangleOnPhotos.rectStartPoint.X, tempEndPoint.X),
                    Math.Min(rectangleOnPhotos.rectStartPoint.Y, tempEndPoint.Y));
                rectangleOnPhotos.rectangle.Size = new Size(
                    Math.Abs(rectangleOnPhotos.rectStartPoint.X - tempEndPoint.X),
                    Math.Abs(rectangleOnPhotos.rectStartPoint.Y - tempEndPoint.Y));
                if(rectangleOnPhotos.rectangle.Right > pictureBoxPhoto.Width)
                {
                    rectangleOnPhotos.rectangle.Width = pictureBoxPhoto.Width - rectangleOnPhotos.rectangle.X;
                }
                if(rectangleOnPhotos.rectangle.Left < 0)
                {
                    rectangleOnPhotos.rectangle.Width += rectangleOnPhotos.rectangle.Left;
                }
                if (rectangleOnPhotos.rectangle.Top < 0)
                {
                    rectangleOnPhotos.rectangle.Height += rectangleOnPhotos.rectangle.Top;
                }
                if (rectangleOnPhotos.rectangle.Bottom > pictureBoxPhoto.Height)
                {
                    rectangleOnPhotos.rectangle.Height = pictureBoxPhoto.Height - rectangleOnPhotos.rectangle.Y;
                }
                if(rectangleOnPhotos.rectangle.X < 0)
                {
                    rectangleOnPhotos.rectangle.X = 0;
                }
                if (rectangleOnPhotos.rectangle.Y < 0)
                {
                    rectangleOnPhotos.rectangle.Y = 0;
                }
                rectangleOnPhotosList[rectangleListComboBox.SelectedIndex] = rectangleOnPhotos;
                pictureBoxPhoto.Invalidate();
            }
        }

        private void pictureBoxPhoto_MouseDown(object sender, MouseEventArgs e)
        {
            if (rectangleListComboBox.SelectedIndex != -1)
            {
                RectangleOnPhotos rectangleOnPhotos = new RectangleOnPhotos(rectangleOnPhotosList[rectangleListComboBox.SelectedIndex].name,
                rectangleOnPhotosList[rectangleListComboBox.SelectedIndex].type,
                rectangleOnPhotosList[rectangleListComboBox.SelectedIndex].rectangle,
                rectangleOnPhotosList[rectangleListComboBox.SelectedIndex].rectStartPoint);

                // Determine the initial rectangle coordinates...
                rectangleOnPhotos.rectStartPoint = e.Location;

                rectangleOnPhotosList[rectangleListComboBox.SelectedIndex] = rectangleOnPhotos;
                Invalidate();
            }
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            AcceptToMainDataGrid();
        }

        #endregion

        // Load Photos form file and load them to imagelist
        void LoadPhotos()
        {
            if (openPhotosDialog.ShowDialog() == DialogResult.OK)
            {
                string[] fileNames = openPhotosDialog.FileNames;

                // Load to ImageList
                foreach (string fileName in fileNames)
                {
                    string extension = Path.GetExtension(fileName);
                    imageListPhotos.Images.Add(Image.FromFile(fileName));
                }
            }

            SetImageInPictureBox();
        }

        void StartCalculateColors()
        {
            for (int currentImageIndex = 0; currentImageIndex < imageListPhotos.Images.Count; currentImageIndex++)
            {
                CalculateColors();
                SetNextImageInPictureBox();
                pictureBoxPhoto.Update();
            }
        }

        /// <summary>
        /// Calculate concrete rectangle colors for concrete bitmap
        /// </summary>
        void CalculateColors()
        {
            // Create a Bitmap object from an image file.
            Bitmap bitmap = new Bitmap(pictureBoxPhoto.Image, pictureBoxPhoto.Size);

            foreach (RectangleOnPhotos concreteRectangle in rectangleOnPhotosList)
            {
                // Get all Photo Rect pixels
                BypassRectangleArea(concreteRectangle.rectangle, bitmap, concreteRectangle.type);
            }
        }

        void BypassRectangleArea(Rectangle rectangle, Bitmap bitmap, int type)
        {
            int divider = 4;
            int counter = 0;
            int[] sum = new int[methodsCount];

            for (int x = rectangle.X; x < rectangle.Right; x++)
            {
                for (int y = rectangle.Y; y < rectangle.Bottom; y++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);

                    sum[0] += pixelColor.B;
                    sum[1] += pixelColor.G;
                    sum[2] += pixelColor.R;
                    counter++;
                    if(counter == divider)
                    {
                        dgvPreData.Rows.Add(new object[] { sum[0] / divider, sum[1] / divider, sum[2] / divider, type });
                        sum[0] = 0;
                        sum[1] = 0;
                        sum[2] = 0;
                        counter = 0;
                    }
                }
            }
        }

        void FillDataGridColumns()
        {
            dgvPreData.Columns.Add("B", "B");
            dgvPreData.Columns.Add("G", "G");
            dgvPreData.Columns.Add("R", "R");
            dgvPreData.Columns.Add("Class", "Class");
        }

        void SetImageInPictureBox()
        {
            if (imageListPhotos.Images.Count > photoIndex)
            {
                pictureBoxPhoto.Image = imageListPhotos.Images[photoIndex];
            }
            else if (imageListPhotos.Images.Count != 0)
            {
                photoIndex = 0;
                pictureBoxPhoto.Image = imageListPhotos.Images[photoIndex];
            }
        }

        void AcceptToMainDataGrid()
        {
            //dataTable.DataSource = dgvPreData;
            //this.DialogResult = DialogResult.OK;
        }

        private void LoadAnalyzePicturesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if(dgvPreData.Rows.Count != 0)
            //{
            //    this.DialogResult = DialogResult.OK;
            //}
            //else
            //{
            //    this.DialogResult = DialogResult.None;
            //}
        }

        public DataTable GetDataTable()
        {
            return MakeDataTable(this.dgvPreData);
        }

        DataTable MakeDataTable(DataGridView dataTable)
        {
            DataTable dt = new DataTable();
            foreach (DataGridViewColumn col in dataTable.Columns)
            {
                dt.Columns.Add(col.Name);
            }

            foreach (DataGridViewRow row in dataTable.Rows)
            {
                DataRow dRow = dt.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dRow[cell.ColumnIndex] = cell.Value;
                }
                dt.Rows.Add(dRow);
            }

            return dt;
        }

        private void addRectangleButton_Click(object sender, EventArgs e)
        {
            rectangleOnPhotosList.Add(new RectangleOnPhotos(defaultRectangleNamePrefix + (rectangleOnPhotosList.Count + 1).ToString(),
                skinNoSkinCheckBox.Checked ? 0 : 1, new Rectangle(), new Point()));

            rectangleListComboBox.Items.Add(rectangleOnPhotosList[rectangleOnPhotosList.Count - 1].name);
        }

        private void rectangleListComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBoxPhoto.Invalidate();
            //pictureBoxPhoto.Update();
        }

        //if (rectangleListComboBox.SelectedIndex == -1)
        //    {
        //        rectangleListComboBox.SelectedItem = rectangleListComboBox.Items[0];
        //    }
    }
}
