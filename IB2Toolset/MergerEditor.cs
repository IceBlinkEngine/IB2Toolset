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
    public partial class MergerEditor : Form
    {
        private Module mod = new Module();
        private ParentForm prntForm;

        private List<PlayerClass> classListImport = new List<PlayerClass>();
        private List<Race> raceListImport = new List<Race>();
        private List<Spell> spellListImport = new List<Spell>();
        private List<Trait> traitListImport = new List<Trait>();
        private List<Effect> effectListImport = new List<Effect>();
        private List<Creature> creatureListImport = new List<Creature>();
        private List<Item> itemListImport = new List<Item>();
        //private Props propListImport = new Props();

        public MergerEditor(Module m, ParentForm pf)
        {
            InitializeComponent();
            mod = m;
            prntForm = pf;
        }

        #region Handlers
        private void btnImport_Click(object sender, EventArgs e)
        {
            if (lbxImport.SelectedIndex >= 0)
            {
                if (cmbDataType.SelectedIndex == 0) //Class
                {
                    if (!classExists(classListImport[lbxImport.SelectedIndex]))
                    {
                        prntForm.mod.modulePlayerClassList.Add(classListImport[lbxImport.SelectedIndex].DeepCopy());
                        refreshImportListBox();
                        refreshMainListBox();
                    }
                }
                else if (cmbDataType.SelectedIndex == 1) //Race
                {
                    if (!raceExists(raceListImport[lbxImport.SelectedIndex]))
                    {
                        prntForm.mod.moduleRacesList.Add(raceListImport[lbxImport.SelectedIndex].DeepCopy());
                        refreshImportListBox();
                        refreshMainListBox();
                    }
                }
                else if (cmbDataType.SelectedIndex == 2) //Spell
                {
                    if (!spellExists(spellListImport[lbxImport.SelectedIndex]))
                    {
                        prntForm.mod.moduleSpellsList.Add(spellListImport[lbxImport.SelectedIndex].DeepCopy());
                        refreshImportListBox();
                        refreshMainListBox();
                    }
                }
                else if (cmbDataType.SelectedIndex == 3) //Trait
                {
                    if (!traitExists(traitListImport[lbxImport.SelectedIndex]))
                    {
                        prntForm.mod.moduleTraitsList.Add(traitListImport[lbxImport.SelectedIndex].DeepCopy());
                        refreshImportListBox();
                        refreshMainListBox();
                    }
                }
                else if (cmbDataType.SelectedIndex == 4) //Effect
                {
                    if (!effectExists(effectListImport[lbxImport.SelectedIndex]))
                    {
                        prntForm.mod.moduleEffectsList.Add(effectListImport[lbxImport.SelectedIndex].DeepCopy());
                        refreshImportListBox();
                        refreshMainListBox();
                    }
                }
                else if (cmbDataType.SelectedIndex == 5) //Creature
                {
                    if (!creatureExists(creatureListImport[lbxImport.SelectedIndex]))
                    {
                        prntForm.mod.moduleCreaturesList.Add(creatureListImport[lbxImport.SelectedIndex].DeepCopy());
                        refreshImportListBox();
                        refreshMainListBox();
                    }
                }
                else if (cmbDataType.SelectedIndex == 6) //Item
                {
                    if (!itemExists(itemListImport[lbxImport.SelectedIndex]))
                    {
                        prntForm.mod.moduleItemsList.Add(itemListImport[lbxImport.SelectedIndex].DeepCopy());
                        refreshImportListBox();
                        refreshMainListBox();
                    }
                }
                /*else if (cmbDataType.SelectedIndex == 7) //Prop
                {
                    if (!propExists(propListImport.propsList[lbxImport.SelectedIndex]))
                    {
                        prntForm.propsList.propsList.Add(propListImport.propsList[lbxImport.SelectedIndex]);
                        refreshImportListBox();
                        refreshMainListBox();
                    }
                }*/                
            }
        }
        private void btnFolderImport_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = Environment.CurrentDirectory;
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (folderBrowserDialog1.SelectedPath != "")
                {
                    string folder = folderBrowserDialog1.SelectedPath;
                    txtFolderImport.Text = folder;
                    loadAllData(folder);
                }
            }            
        }
        private void loadAllData(string folderpath)
        {
            /*try
            {
                classListImport = prntForm.loadPlayerClassesFile(folderpath + "\\playerClasses.json");
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed to open import classes file: " + ex.ToString());
            }
            try
            {
                raceListImport = prntForm.loadRacesFile(folderpath + "\\races.json");
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed to open import races file: " + ex.ToString());
            }
            try
            {
                spellListImport = prntForm.loadSpellsFile(folderpath + "\\spells.json");
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed to open import spells file: " + ex.ToString());
            }
            try
            {
                traitListImport = prntForm.loadTraitsFile(folderpath + "\\traits.json");
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed to open import traits file: " + ex.ToString());
            }
            try
            {
                effectListImport = prntForm.loadEffectsFile(folderpath + "\\effects.json");
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed to open import effects file: " + ex.ToString());
            }
            try
            {
                creatureListImport = prntForm.loadCreaturesFile(folderpath + "\\creatures.json");
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed to open import classes file: " + ex.ToString());
            }*/
            /*try
            {
                propListImport = prntForm.loadPropsFile(folderpath + "\\props.prp");
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed to open import props file: " + ex.ToString());
            }*/
            /*try
            {
                itemListImport = prntForm.loadItemsFile(folderpath + "\\items.json");
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed to open import items file: " + ex.ToString());
            }*/
        }
        private void cmbDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshImportListBox();
            refreshMainListBox();
        }
        private void pgMain_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            refreshImportListBox();
            refreshMainListBox();
        }
        private void pgImport_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            refreshImportListBox();
            refreshMainListBox();
        }
        private void lbxMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxMain.SelectedIndex >= 0)
            {
                if (cmbDataType.SelectedIndex == 0) //Class
                {
                    pgMain.SelectedObject = prntForm.mod.modulePlayerClassList[lbxMain.SelectedIndex];
                }
                else if (cmbDataType.SelectedIndex == 1) //Race
                {
                    pgMain.SelectedObject = prntForm.mod.moduleRacesList[lbxMain.SelectedIndex];
                }
                else if (cmbDataType.SelectedIndex == 2) //Spell
                {
                    pgMain.SelectedObject = prntForm.mod.moduleSpellsList[lbxMain.SelectedIndex];
                }
                else if (cmbDataType.SelectedIndex == 3) //Trait
                {
                    pgMain.SelectedObject = prntForm.mod.moduleTraitsList[lbxMain.SelectedIndex];
                }
                else if (cmbDataType.SelectedIndex == 4) //Effect
                {
                    pgMain.SelectedObject = prntForm.mod.moduleEffectsList[lbxMain.SelectedIndex];
                }
                else if (cmbDataType.SelectedIndex == 5) //Creature
                {
                    pgMain.SelectedObject = prntForm.mod.moduleCreaturesList[lbxMain.SelectedIndex];
                }
                else if (cmbDataType.SelectedIndex == 6) //Item
                {
                    pgMain.SelectedObject = prntForm.mod.moduleItemsList[lbxMain.SelectedIndex];
                }
                /*else if (cmbDataType.SelectedIndex == 7) //Prop
                {
                    pgMain.SelectedObject = prntForm.propsList[lbxMain.SelectedIndex];
                }*/   
            }
        }
        private void lbxImport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxImport.SelectedIndex >= 0)
            {
                if (cmbDataType.SelectedIndex == 0) //Class
                {
                    pgImport.SelectedObject = classListImport[lbxImport.SelectedIndex];
                }
                else if (cmbDataType.SelectedIndex == 1) //Race
                {
                    pgImport.SelectedObject = raceListImport[lbxImport.SelectedIndex];
                }
                else if (cmbDataType.SelectedIndex == 2) //Spell
                {
                    pgImport.SelectedObject = spellListImport[lbxImport.SelectedIndex];
                }
                else if (cmbDataType.SelectedIndex == 3) //Trait
                {
                    pgImport.SelectedObject = traitListImport[lbxImport.SelectedIndex];
                }
                else if (cmbDataType.SelectedIndex == 4) //Effect
                {
                    pgImport.SelectedObject = effectListImport[lbxImport.SelectedIndex];
                }
                else if (cmbDataType.SelectedIndex == 5) //Creature
                {
                    pgImport.SelectedObject = creatureListImport[lbxImport.SelectedIndex];
                }
                else if (cmbDataType.SelectedIndex == 6) //Item
                {
                    pgImport.SelectedObject = itemListImport[lbxImport.SelectedIndex];
                }
                /*else if (cmbDataType.SelectedIndex == 7) //Prop
                {
                    pgImport.SelectedObject = propListImport.propsList[lbxImport.SelectedIndex];
                }*/ 
            }
        }
        #endregion

        #region Methods
        private void refreshMainListBox()
        {
            if (cmbDataType.SelectedIndex == 0) //Class
            {
                lbxMain.BeginUpdate();
                lbxMain.DataSource = null;
                lbxMain.DataSource = prntForm.mod.modulePlayerClassList;
                lbxMain.DisplayMember = "name";
                lbxMain.EndUpdate();
            }
            else if (cmbDataType.SelectedIndex == 1) //Race
            {
                lbxMain.BeginUpdate();
                lbxMain.DataSource = null;
                lbxMain.DataSource = prntForm.mod.moduleRacesList;
                lbxMain.DisplayMember = "name";
                lbxMain.EndUpdate();
            }
            else if (cmbDataType.SelectedIndex == 2) //Spell
            {
                lbxMain.BeginUpdate();
                lbxMain.DataSource = null;
                lbxMain.DataSource = prntForm.mod.moduleSpellsList;
                lbxMain.DisplayMember = "name";
                lbxMain.EndUpdate();
            }
            else if (cmbDataType.SelectedIndex == 3) //Trait
            {
                lbxMain.BeginUpdate();
                lbxMain.DataSource = null;
                lbxMain.DataSource = prntForm.mod.moduleTraitsList;
                lbxMain.DisplayMember = "name";
                lbxMain.EndUpdate();
            }
            else if (cmbDataType.SelectedIndex == 4) //Effect
            {
                lbxMain.BeginUpdate();
                lbxMain.DataSource = null;
                lbxMain.DataSource = prntForm.mod.moduleEffectsList;
                lbxMain.DisplayMember = "name";
                lbxMain.EndUpdate();
            }
            else if (cmbDataType.SelectedIndex == 5) //Creature
            {
                lbxMain.BeginUpdate();
                lbxMain.DataSource = null;
                lbxMain.DataSource = prntForm.mod.moduleCreaturesList;
                lbxMain.DisplayMember = "cr_name";
                lbxMain.EndUpdate();
            }
            else if (cmbDataType.SelectedIndex == 6) //Item
            {
                lbxMain.BeginUpdate();
                lbxMain.DataSource = null;
                lbxMain.DataSource = prntForm.mod.moduleItemsList;
                lbxMain.DisplayMember = "name";
                lbxMain.EndUpdate();
            }
            /*else if (cmbDataType.SelectedIndex == 7) //Prop
            {
                lbxMain.BeginUpdate();
                lbxMain.DataSource = null;
                lbxMain.DataSource = prntForm.propsList.propsList;
                lbxMain.DisplayMember = "PropNameWithNotes";
                lbxMain.EndUpdate();
            }*/   
        }
        private void refreshImportListBox()
        {
            if (cmbDataType.SelectedIndex == 0) //Class
            {
                lbxImport.BeginUpdate();
                lbxImport.DataSource = null;
                lbxImport.DataSource = classListImport;
                lbxImport.DisplayMember = "name";
                lbxImport.EndUpdate();
            }
            else if (cmbDataType.SelectedIndex == 1) //Race
            {
                lbxImport.BeginUpdate();
                lbxImport.DataSource = null;
                lbxImport.DataSource = raceListImport;
                lbxImport.DisplayMember = "name";
                lbxImport.EndUpdate();
            }
            else if (cmbDataType.SelectedIndex == 2) //Spell
            {
                lbxImport.BeginUpdate();
                lbxImport.DataSource = null;
                lbxImport.DataSource = spellListImport;
                lbxImport.DisplayMember = "name";
                lbxImport.EndUpdate();
            }
            else if (cmbDataType.SelectedIndex == 3) //Trait
            {
                lbxImport.BeginUpdate();
                lbxImport.DataSource = null;
                lbxImport.DataSource = traitListImport;
                lbxImport.DisplayMember = "name";
                lbxImport.EndUpdate();
            }
            else if (cmbDataType.SelectedIndex == 4) //Effect
            {
                lbxImport.BeginUpdate();
                lbxImport.DataSource = null;
                lbxImport.DataSource = effectListImport;
                lbxImport.DisplayMember = "name";
                lbxImport.EndUpdate();
            }
            else if (cmbDataType.SelectedIndex == 5) //Creature
            {
                lbxImport.BeginUpdate();
                lbxImport.DataSource = null;
                lbxImport.DataSource = creatureListImport;
                lbxImport.DisplayMember = "cr_name";
                lbxImport.EndUpdate();
            }
            else if (cmbDataType.SelectedIndex == 6) //Item
            {
                lbxImport.BeginUpdate();
                lbxImport.DataSource = null;
                lbxImport.DataSource = itemListImport;
                lbxImport.DisplayMember = "name";
                lbxImport.EndUpdate();
            }
            /*else if (cmbDataType.SelectedIndex == 8) //Prop
            {
                lbxImport.BeginUpdate();
                lbxImport.DataSource = null;
                lbxImport.DataSource = propListImport.propsList;
                lbxImport.DisplayMember = "PropNameWithNotes";
                lbxImport.EndUpdate();
            }*/   
        }        
        private bool classExists(PlayerClass itImp)
        {
            foreach (PlayerClass it in prntForm.mod.modulePlayerClassList)
            {
                if (it.tag == itImp.tag)
                {
                    return true;
                }
            }
            return false;
        }
        private bool raceExists(Race itImp)
        {
            foreach (Race it in prntForm.mod.moduleRacesList)
            {
                if (it.tag == itImp.tag)
                {
                    return true;
                }
            }
            return false;
        }
        private bool spellExists(Spell itImp)
        {
            foreach (Spell it in prntForm.mod.moduleSpellsList)
            {
                if (it.tag == itImp.tag)
                {
                    return true;
                }
            }
            return false;
        }
        private bool traitExists(Trait itImp)
        {
            foreach (Trait it in prntForm.mod.moduleTraitsList)
            {
                if (it.tag == itImp.tag)
                {
                    return true;
                }
            }
            return false;
        }
        private bool effectExists(Effect itImp)
        {
            foreach (Effect it in prntForm.mod.moduleEffectsList)
            {
                if (it.tag == itImp.tag)
                {
                    return true;
                }
            }
            return false;
        }
        private bool creatureExists(Creature itImp)
        {
            foreach (Creature it in prntForm.mod.moduleCreaturesList)
            {
                if (it.cr_resref == itImp.cr_resref)
                {
                    return true;
                }
            }
            return false;
        }
        private bool itemExists(Item itImp)
        {
            foreach (Item it in prntForm.mod.moduleItemsList)
            {
                if (it.resref == itImp.resref)
                {
                    return true;
                }
            }
            return false;
        }
        private bool propExists(Prop itImp)
        {
            foreach (Prop it in prntForm.mod.modulePropsList)
            {
                if (it.PropTag == itImp.PropTag)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion        

        private void btnInstructions_Click(object sender, EventArgs e)
        {
            string text = "To copy/merge data from another module, first click on the '...' button"
                        + " and browse to the 'data' folder inside the module folder (the one you"
                        + " want to copy data from). Click on that 'data' folder and then click on"
                        + " the 'okay' button. Next, select a data type from the dropdown at the top"
                        + " center of this editor. Once the data type is selected you will be able"
                        + " to see and compare the individual items of that data group from your module"
                        + " compared to the module you want to copy data from. Editing the data in the"
                        + " PropertyGrid of you module's data ('Current Module Data') will actually modify the data in your module"
                        + " so feel free to edit/update data in your module while comparing to the other"
                        + " module. To copy a data item over to your module, select the item in the Import Data"
                        + " list that you want to copy and then click on the '<<<copy over selected<<<' button.";
            MessageBox.Show(text);
        }
    }
}
