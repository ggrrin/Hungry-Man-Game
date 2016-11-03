using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hungry_Man_Map_Editor
{
    public partial class NewFile : Form
    {
        public NewFile()
        {
            InitializeComponent();
        }

        private void widthNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (widthNumericUpDown.Value == 0 || heightNumericUpDown.Value == 0)
                OK.Enabled = false;
            else
                OK.Enabled = true;
        }

        private void heightNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (widthNumericUpDown.Value == 0 || heightNumericUpDown.Value == 0)
                OK.Enabled = false;
            else
                OK.Enabled = true;
        }

        private void NewFile_Load(object sender, EventArgs e)
        {
            if (widthNumericUpDown.Value == 0 || heightNumericUpDown.Value == 0)
                OK.Enabled = false;
            else
                OK.Enabled = true;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
