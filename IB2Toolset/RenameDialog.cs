using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IB2miniToolset
{
    public partial class RenameDialog : Form
    {
        private string renameText;
        public string RenameText
        {
            get
            {
                return renameText;
            }
            set
            {
                renameText = value;
            }
        }

        public RenameDialog()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtModName.Text != string.Empty)
            {
                RenameText = txtModName.Text;
                DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Provide a new name");
            }
        }
    }
}
