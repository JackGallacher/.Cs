using System;
using System.Windows.Forms;

namespace Workshop_14
{
    public partial class Form2 : Form
    {
        public Form2(string About)
        {
            InitializeComponent();
            label1.Text = About;//Sets the label to the passed value.
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
