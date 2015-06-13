using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using IceBlinkCore;
using WeifenLuo.WinFormsUI.Docking;

namespace IB2Toolset
{
    public partial class LogForm : DockContent
    {
        public ParentForm prntForm;

        public LogForm(ParentForm pf)
        {
            InitializeComponent();
            prntForm = pf;
        }
    }
}
