using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IB2Toolset
{
    public partial class ModuleNameDialog : Form
    {
        private string mModText;
        public string ModText
        {
            get
            {
                return mModText;
            }
            set
            {
                mModText = value;
            }
        }

        public ModuleNameDialog()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtModName.Text != string.Empty)
            {
                ModText = txtModName.Text;
            }
            else
            {
                MessageBox.Show("Provide the new Module's name");
                return;
            }

            this.Close();
        }
    }
}
