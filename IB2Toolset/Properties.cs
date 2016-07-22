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

namespace IB2miniToolset
{
    public partial class IceBlinkProperties : DockContent
    {
        public ParentForm prntForm;

        public IceBlinkProperties(ParentForm pf)
        {
            InitializeComponent();
            prntForm = pf;
        }

        private void propertyGrid1_PropertyValueChanged_1(object s, PropertyValueChangedEventArgs e)
        {
            prntForm.frmBlueprints.UpdateTreeViewCreatures();
            prntForm.frmBlueprints.UpdateTreeViewItems();
            prntForm.frmBlueprints.UpdateTreeViewProps();
        }        
    }
}
