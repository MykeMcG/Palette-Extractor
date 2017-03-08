using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace PaletteExtractor.UI
{
  /// <summary>
  /// Interaction logic for OptionsWindow.xaml
  /// </summary>
  public partial class OptionsWindow : Window
  {
    public OptionsWindow()
    {
      InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      txtOutputWidth.Text = Properties.Settings.Default.OutputWidth.ToString();
    }

    private void btnOk_Click(object sender, RoutedEventArgs e)
    {
      //TODO: Validate user input
      int newOutputWidth = 0;
      int.TryParse(txtOutputWidth.Text, out newOutputWidth);

      if (newOutputWidth < 1)
      {
        string messageBoxHeader = (string)Application.Current.Resources["strInvalidOutputWidthMessageboxHeader"];
        string messageBoxBody = (string)Application.Current.Resources["strInvalidOutputWidthMessageboxBody"];
        MessageBox.Show(messageBoxBody, messageBoxHeader, MessageBoxButton.OK, MessageBoxImage.Exclamation);
        return;
      }

      Properties.Settings.Default.OutputWidth = int.Parse(txtOutputWidth.Text);
      Properties.Settings.Default.Save();
      this.Close();
    }
  }
}
