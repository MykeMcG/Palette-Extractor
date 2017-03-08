using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using PaletteExtractor.Backend;

namespace PaletteExtractor.UI
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    OpenFileDialog openFileDialog;

    public MainWindow()
    {
      InitializeComponent();
    }

    #region Event Handlers

    private void mnuExit_Click(object sender, RoutedEventArgs e)
    {
      this.Close();
    }

    private void mnuOptions_Click(object sender, RoutedEventArgs e)
    {
      var dialog = new OptionsWindow();
      dialog.ShowDialog();
    }

    private void btnBrowse_Click(object sender, RoutedEventArgs e)
    {
      openFileDialog          = new OpenFileDialog();
      openFileDialog.Filter   = "Image files|*.png;*.jpg;*.jpeg;*.bmp;*.gif";
      openFileDialog.FileName = string.Empty;
      openFileDialog.ShowDialog();
      txtInput.Text = openFileDialog.FileName;
    }

    private void btnGo_Click(object sender, RoutedEventArgs e)
    {
      try
      {
        String inputPath         = txtInput.Text;
        BitmapFile file          = new BitmapFile(inputPath);
        Bitmap outputImage       = file.ExtractPalette(Properties.Settings.Default.OutputWidth);
        var saveFileDialog       = new SaveFileDialog();
        saveFileDialog.Filter    = "Image files|*.png;*.jpg;*.jpeg;*.bmp;*.gif";
        saveFileDialog.FileName  = "Output.png";
        saveFileDialog.ShowDialog();
        String outputPath = saveFileDialog.FileName;
        outputImage.Save(outputPath);
        MessageBox.Show("Done!");
      } 
      catch (ArgumentException)
      {
        MessageBox.Show("File not found");
      } catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    #endregion Event Handlers

  }
}
