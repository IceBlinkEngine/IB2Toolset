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
    public partial class TraitEditor : Form
    {
        private Module mod = new Module();
        //private Game game;
        private ParentForm prntForm;
        private int selectedLbxIndex = 0;

        public TraitEditor(Module m, ParentForm pf)
        {
            InitializeComponent();
            mod = m;
            //game = g;
            prntForm = pf;
            refreshListBox();
        }
        private void refreshListBox()
        {
            lbxTraits.BeginUpdate();
            lbxTraits.DataSource = null;
            lbxTraits.DataSource = prntForm.traitsList;
            lbxTraits.DisplayMember = "name";
            lbxTraits.EndUpdate();
        }
        private void btnAddTrait_Click(object sender, EventArgs e)
        {
            Trait newTS = new Trait();
            newTS.name = "newTrait";
            newTS.tag = "newTraitTag_" + prntForm.mod.nextIdNumber.ToString();
            prntForm.traitsList.Add(newTS);
            refreshListBox();
        }
        private void btnRemoveTrait_Click(object sender, EventArgs e)
        {
            if (lbxTraits.Items.Count > 0)
            {
                try
                {
                    // The Remove button was clicked.
                    int selectedIndex = lbxTraits.SelectedIndex;
                    //mod.ModuleContainersList.containers.RemoveAt(selectedIndex);
                    prntForm.traitsList.RemoveAt(selectedIndex);
                }
                catch { }
                selectedLbxIndex = 0;
                lbxTraits.SelectedIndex = 0;
                refreshListBox();
            }
        }        
        private void btnDuplicateTrait_Click(object sender, EventArgs e)
        {
            Trait newCopy = prntForm.traitsList[selectedLbxIndex].DeepCopy();
            newCopy.tag = "newTraitTag_" + prntForm.mod.nextIdNumber.ToString();
            prntForm.traitsList.Add(newCopy);
            refreshListBox();
        }
        private void lbxTraits_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if ((lbxTraits.SelectedIndex >= 0) && (prntForm.traitsList != null))
            {
                selectedLbxIndex = lbxTraits.SelectedIndex;
                lbxTraits.SelectedIndex = selectedLbxIndex;
                propertyGrid1.SelectedObject = prntForm.traitsList[selectedLbxIndex];
            }
        } 
        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            refreshListBox();
        }
        private void checkForChangedTraits()
        {
            foreach (PlayerClass cl in prntForm.playerClassesList)
            {
                foreach (Trait tr in prntForm.traitsList)
                {
                    foreach (TraitAllowed ta in cl.traitsAllowed)
                    {
                        if (ta.tag == tr.tag)
                        {
                            ta.name = tr.name;
                            ta.useableinsituation = tr.useableInSituation;
                            ta.associatedSpellTag = tr.associatedSpellTag;
                        }
                    }
                }
            }
        }
        private void checkForNewTraits()
        {
            bool foundOne = false;
            foreach (PlayerClass cl in prntForm.playerClassesList)
            {
                foreach (Trait tr in prntForm.traitsList)
                {
                    foreach (TraitAllowed ta in cl.traitsAllowed)
                    {
                        if (ta.tag == tr.tag)
                        {
                            foundOne = true;
                            break;
                        }
                    }
                    if (!foundOne)
                    {
                        TraitAllowed newTA = new TraitAllowed();
                        newTA.name = tr.name;
                        newTA.tag = tr.tag;
                        newTA.useableinsituation = tr.useableInSituation;
                        newTA.associatedSpellTag = tr.associatedSpellTag;
                        cl.traitsAllowed.Add(newTA);
                    }
                    else
                    {
                        foundOne = false;
                    }
                }
            }
        }
        private void checkForDeletedTraits()
        {
            bool foundOne = false;
            foreach (PlayerClass cl in prntForm.playerClassesList)
            {
                for (int i = cl.traitsAllowed.Count - 1; i >= 0; i--)
                {
                    foreach (Trait tr in prntForm.traitsList)
                    {
                        if (tr.tag == cl.traitsAllowed[i].tag)
                        {
                            foundOne = true;
                            break;
                        }
                    }
                    if (!foundOne)
                    {
                        cl.traitsAllowed.RemoveAt(i);
                    }
                    else
                    {
                        foundOne = false;
                    }
                }
            }
        }
        private void TraitEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            checkForNewTraits();
            checkForChangedTraits();
            checkForDeletedTraits();
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            prntForm.traitsList = prntForm.traitsList.OrderBy(o => o.name).ToList();
            refreshListBox();
        }       
    }
}
