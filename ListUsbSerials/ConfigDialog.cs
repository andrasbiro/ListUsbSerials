using ListUsbSerials.Properties;
using System;
using System.Windows.Forms;

namespace ListUsbSerials
{
    public partial class ConfigDialog : Form
    {
        public ConfigDialog()
        {
            InitializeComponent();
            appTextBox.Text = Settings.Default["terminal"].ToString();
            argTextBox.Text = Settings.Default["teminalargs"].ToString();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Settings.Default["terminal"] = appTextBox.Text;
            Settings.Default["teminalargs"] = argTextBox.Text;
            Settings.Default.Save();
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
