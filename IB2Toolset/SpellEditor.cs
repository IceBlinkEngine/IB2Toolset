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

namespace IB2miniToolset
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
            lbxSpells.DataSource = prntForm.mod.moduleSpellsList;
            lbxSpells.DisplayMember = "name";
            lbxSpells.EndUpdate();
        }
        private void btnAddSpell_Click(object sender, EventArgs e)
        {
            Spell newTS = new Spell();
            newTS.name = "newSpell";
            newTS.tag = "newSpellTag_" + prntForm.mod.nextIdNumber.ToString();
            prntForm.mod.moduleSpellsList.Add(newTS);
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
                    prntForm.mod.moduleSpellsList.RemoveAt(selectedIndex);
                }
                catch { }
                selectedLbxIndex = 0;
                lbxSpells.SelectedIndex = 0;
                refreshListBox();
            }
        }
        private void btnDuplicateSpell_Click(object sender, EventArgs e)
        {
            Spell newCopy = prntForm.mod.moduleSpellsList[selectedLbxIndex].DeepCopy();
            newCopy.tag = "newSpellTag_" + prntForm.mod.nextIdNumber.ToString();
            prntForm.mod.moduleSpellsList.Add(newCopy);
            refreshListBox();
        }
        private void lbxSpells_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((lbxSpells.SelectedIndex >= 0) && (prntForm.mod.moduleSpellsList != null))
            {
                selectedLbxIndex = lbxSpells.SelectedIndex;
                lbxSpells.SelectedIndex = selectedLbxIndex;
                propertyGrid1.SelectedObject = prntForm.mod.moduleSpellsList[selectedLbxIndex];
            }
        }
        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            refreshListBox();
        }
        private void checkForNewSpells()
        {
            bool foundOne = false;
            foreach (PlayerClass cl in prntForm.mod.modulePlayerClassList)
            {
                foreach (Spell sp in prntForm.mod.moduleSpellsList)
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
            foreach (PlayerClass cl in prntForm.mod.modulePlayerClassList)
            {
                for (int i = cl.spellsAllowed.Count - 1; i >= 0; i--)
                {
                    foreach (Spell sp in prntForm.mod.moduleSpellsList)
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
            prntForm.mod.moduleSpellsList = prntForm.mod.moduleSpellsList.OrderBy(o => o.name).ToList();
            refreshListBox();
        }        
    }
}
