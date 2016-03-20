using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Workshop_14
{
    public partial class Form3 : Form
    {
        public static string Signature;//What we will save our signature to.
        public Form3()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Signature = richTextBox1.Text; //Sets it to what is in the text box on Form3.  
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
