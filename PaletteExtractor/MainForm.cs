using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaletteExtractor
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private List<Color> GetColorList(Bitmap imageToProcess)
        {
            List<Color> colors   = new List<System.Drawing.Color>();
            progressBar1.Maximum = imageToProcess.Height;
            progressBar1.Value   = 0;
            for (int y = 0; y < imageToProcess.Height; y++)
            {
                for (int x = 0; x < imageToProcess.Width; x++)
                {
                    Color currentPixel = imageToProcess.GetPixel(x, y);
                    Int32 colorMatchCount = (from c in colors
                                             where c.A == currentPixel.A
                                             &&    c.R == currentPixel.R
                                             &&    c.G == currentPixel.G
                                             &&    c.B == currentPixel.B
                                             select c).Count();
                    if (colorMatchCount == 0)
                    {
                        colors.Add(currentPixel);
                    }
                }
                progressBar1.Step = 1;
                progressBar1.PerformStep();
            }
            return colors;
        }

        private Bitmap GenerateColorBitmap(List<Color> colorList)
        {
            Int32 imageWidth      = Properties.Settings.Default.OutputWidth;
            Int32 imageHeight     = Convert.ToInt32(Math.Ceiling((Double)colorList.Count / imageWidth));
            Int32 colorIndex      = 0;
            Bitmap generatedImage = new Bitmap(imageWidth, imageHeight);
            for (int y            = 0; y < imageHeight; y++)
            {

                for (int x = 0; x < imageWidth; x++)
                {
                    if (colorIndex <= colorList.Count() - 1)
                    {
                        generatedImage.SetPixel(x, y, colorList[colorIndex]);
                        colorIndex++;
                    }
                }
            }
            return generatedImage;
        }

        #region "Event Handlers"

        private void btnInput_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter   = "Image files|*.png;*.jpg;*.jpeg;*.bmp;*.gif";
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.ShowDialog();
            txtInput.Text = openFileDialog1.FileName;
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                String inputPath         = txtInput.Text;
                Bitmap inputImage        = new Bitmap(inputPath);
                this.Text                = "Palette Extractor [Getting Colors...]";
                List<Color> colors       = GetColorList(inputImage);
                this.Text                = "Palette Extractor [Generating Image...]";
                Bitmap outputImage       = GenerateColorBitmap(colors);
                saveFileDialog1.Filter   = "Image files|*.png;*.jpg;*.jpeg;*.bmp;*.gif";
                saveFileDialog1.FileName = "Output.png";
                saveFileDialog1.ShowDialog();
                String outputPath = saveFileDialog1.FileName;
                outputImage.Save(outputPath);
                this.Text = "Palette Extractor";
                MessageBox.Show("Done!");
            }
            catch (ArgumentException)
            {
                MessageBox.Show("File not found");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options dialog = new Options();
            dialog.StartPosition = FormStartPosition.CenterParent;
            dialog.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion// Event Handlers
    }
}
