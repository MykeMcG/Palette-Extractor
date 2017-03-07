using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaletteExtractor.Backend;

namespace PaletteExtractor
{
  public partial class MainForm : Form
  {
    public MainForm()
    {
      InitializeComponent();
    }

    #region Event Handlers

    private void btnInput_Click(object sender, EventArgs e)
    {
      openFileDialog1.Filter = "Image files|*.png;*.jpg;*.jpeg;*.bmp;*.gif";
      openFileDialog1.FileName = string.Empty;
      openFileDialog1.ShowDialog();
      txtInput.Text = openFileDialog1.FileName;
      BitmapFile x = new BitmapFile(openFileDialog1.FileName);
    }

    private async void btnGo_Click(object sender, EventArgs e)
    {
      try
      {
        String inputPath         = txtInput.Text;
        BitmapFile file = new BitmapFile(inputPath);
        Bitmap outputImage       = file.ExtractPalette(Properties.Settings.Default.OutputWidth);
        saveFileDialog1.Filter = "Image files|*.png;*.jpg;*.jpeg;*.bmp;*.gif";
        saveFileDialog1.FileName = "Output.png";
        saveFileDialog1.ShowDialog();
        String outputPath = saveFileDialog1.FileName;
        outputImage.Save(outputPath);
        this.Text = "Palette Extractor";
        MessageBox.Show("Done!");
      } catch (ArgumentException)
      {
        MessageBox.Show("File not found");
      } catch (Exception ex)
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

    #endregion Event Handlers
  }
}
