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
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
        }

        private void Options_Load(object sender, EventArgs e)
        {
            nudWidth.Value = Properties.Settings.Default.OutputWidth;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.OutputWidth = (Int32)nudWidth.Value;
            Properties.Settings.Default.Save();
            this.Close();
        }

    }
}
