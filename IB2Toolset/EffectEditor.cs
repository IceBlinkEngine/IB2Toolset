using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using IceBlinkCore;
using System.IO;
using System.Threading;

namespace IB2Toolset
{
    public partial class EffectEditor : Form
    {
        private Module mod = new Module();
        //private Game game;
        private ParentForm prntForm;
        private int selectedLbxIndex = 0;

        public EffectEditor(Module m, ParentForm pf)
        {
            InitializeComponent();
            mod = m;
            //game = g;
            prntForm = pf;
            refreshListBox();
        }
        private void refreshListBox()
        {
            lbxEffects.BeginUpdate();
            lbxEffects.DataSource = null;
            lbxEffects.DataSource = prntForm.effectsList;
            lbxEffects.DisplayMember = "EffectName";
            lbxEffects.EndUpdate();
        }
        private void lbxEffects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((lbxEffects.SelectedIndex >= 0) && (prntForm.effectsList != null))
            {
                selectedLbxIndex = lbxEffects.SelectedIndex;
                lbxEffects.SelectedIndex = selectedLbxIndex;
                propertyGrid1.SelectedObject = prntForm.effectsList[selectedLbxIndex];
            }
        }
        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            refreshListBox();
        }
        private void btnAddEffect_Click_1(object sender, EventArgs e)
        {
            Effect newE = new Effect();
            newE.name = "newEffect";
            newE.tag = "newEffectTag_" + prntForm.mod.nextIdNumber.ToString();
            prntForm.effectsList.Add(newE);
            refreshListBox();
        }
        private void btnRemoveEffect_Click_1(object sender, EventArgs e)
        {
            if (lbxEffects.Items.Count > 0)
            {
                try
                {
                    // The Remove button was clicked.
                    int selectedIndex = lbxEffects.SelectedIndex;
                    //mod.ModuleContainersList.containers.RemoveAt(selectedIndex);
                    prntForm.effectsList.RemoveAt(selectedIndex);
                }
                catch { }
                selectedLbxIndex = 0;
                lbxEffects.SelectedIndex = 0;
                refreshListBox();
            }
        }
        private void btnDuplicateEffect_Click_1(object sender, EventArgs e)
        {
            Effect newCopy = prntForm.effectsList[selectedLbxIndex].DeepCopy();
            newCopy.tag = "newEffectTag_" + prntForm.mod.nextIdNumber.ToString();
            prntForm.effectsList.Add(newCopy);
            refreshListBox();
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            prntForm.effectsList = prntForm.effectsList.OrderBy(o => o.name).ToList();
            refreshListBox();
        }
    }
}
