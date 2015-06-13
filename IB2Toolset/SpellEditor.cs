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
    public partial class SpellEditor : Form
    {
        private Module mod = new Module();
        //private Game game;
        private ParentForm prntForm;
        private int selectedLbxIndex = 0;

        public SpellEditor(Module m, ParentForm pf)
        {
            InitializeComponent();
            mod = m;
            //game = g;
            prntForm = pf;
            refreshListBox();
        }
        private void refreshListBox()
        {
            lbxSpells.BeginUpdate();
            lbxSpells.DataSource = null;
            lbxSpells.DataSource = prntForm.spellsList;
            lbxSpells.DisplayMember = "name";
            lbxSpells.EndUpdate();
        }
        private void btnAddSpell_Click(object sender, EventArgs e)
        {
            Spell newTS = new Spell();
            newTS.name = "newSpell";
            newTS.tag = "newSpellTag_" + prntForm.mod.nextIdNumber.ToString();
            prntForm.spellsList.Add(newTS);
            refreshListBox();
        }
        private void btnRemoveSpell_Click(object sender, EventArgs e)
        {
            if (lbxSpells.Items.Count > 0)
            {
                try
                {
                    // The Remove button was clicked.
                    int selectedIndex = lbxSpells.SelectedIndex;
                    //mod.ModuleContainersList.containers.RemoveAt(selectedIndex);
                    prntForm.spellsList.RemoveAt(selectedIndex);
                }
                catch { }
                selectedLbxIndex = 0;
                lbxSpells.SelectedIndex = 0;
                refreshListBox();
            }
        }
        private void btnDuplicateSpell_Click(object sender, EventArgs e)
        {
            Spell newCopy = prntForm.spellsList[selectedLbxIndex].DeepCopy();
            newCopy.tag = "newSpellTag_" + prntForm.mod.nextIdNumber.ToString();
            prntForm.spellsList.Add(newCopy);
            refreshListBox();
        }
        private void lbxSpells_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((lbxSpells.SelectedIndex >= 0) && (prntForm.spellsList != null))
            {
                selectedLbxIndex = lbxSpells.SelectedIndex;
                lbxSpells.SelectedIndex = selectedLbxIndex;
                propertyGrid1.SelectedObject = prntForm.spellsList[selectedLbxIndex];
            }
        }
        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            refreshListBox();
        }
        private void checkForNewSpells()
        {
            bool foundOne = false;
            foreach (PlayerClass cl in prntForm.playerClassesList)
            {
                foreach (Spell sp in prntForm.spellsList)
                {
                    foreach (SpellAllowed sa in cl.spellsAllowed)
                    {
                        if (sa.tag == sp.tag)
                        {
                            foundOne = true;
                            break;
                        }
                    }
                    if (!foundOne)
                    {
                        SpellAllowed newSA = new SpellAllowed();
                        newSA.name = sp.name;
                        newSA.tag = sp.tag;
                        cl.spellsAllowed.Add(newSA);
                    }
                    else
                    {
                        foundOne = false;
                    }
                }
            }
        }
        private void checkForDeletedSpells()
        {
            bool foundOne = false;
            foreach (PlayerClass cl in prntForm.playerClassesList)
            {
                for (int i = cl.spellsAllowed.Count - 1; i >= 0; i--)
                {
                    foreach (Spell sp in prntForm.spellsList)
                    {
                        if (sp.tag == cl.spellsAllowed[i].tag)
                        {
                            foundOne = true;
                            break;
                        }
                    }
                    if (!foundOne)
                    {
                        cl.spellsAllowed.RemoveAt(i);
                    }
                    else
                    {
                        foundOne = false;
                    }
                }
            }
        }
        private void SpellEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            checkForNewSpells();
            checkForDeletedSpells();
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            prntForm.spellsList = prntForm.spellsList.OrderBy(o => o.name).ToList();
            refreshListBox();
        }        
    }
}
