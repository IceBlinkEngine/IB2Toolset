using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace IB2Toolset
{
    public partial class PlayerEditor : Form
    {
        public ParentForm prntForm;
        public bool refreshingList = false;
        public Bitmap iconBitmap;
        public Player pc = new Player();
        public List<ItemRefs> itemsHeadList = new List<ItemRefs>();
        public List<ItemRefs> itemsNeckList = new List<ItemRefs>();
        public List<ItemRefs> itemsBodyList = new List<ItemRefs>();
        public List<ItemRefs> itemsMainHandList = new List<ItemRefs>();
        public List<ItemRefs> itemsOffHandList = new List<ItemRefs>();
        public List<ItemRefs> itemsRing1List = new List<ItemRefs>();
        public List<ItemRefs> itemsRing2List = new List<ItemRefs>();
        public List<ItemRefs> itemsFeetList = new List<ItemRefs>();
        public List<ItemRefs> itemsAmmoList = new List<ItemRefs>();

        public PlayerEditor(ParentForm pf)
        {
            InitializeComponent();
            prntForm = pf;
        }

        #region Event Handlers    
        private void PlayerEditor_Load(object sender, EventArgs e)
        {
            refreshForm();
        }
        private void btnLoadPlayer_Click(object sender, EventArgs e)
        {
            if (prntForm.mod.moduleName != "NewModule")
            {
                openFileDialog1.InitialDirectory = prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\data";
            }
            else
            {
                openFileDialog1.InitialDirectory = prntForm._mainDirectory + "\\default\\NewModule\\data";
            }
            //Empty the FileName text box of the dialog
            openFileDialog1.Filter = "Json (*.json)|*.json|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string PcFilename = Path.GetFullPath(openFileDialog1.FileName);
                try
                {
                    pc = loadPlayerFile(PcFilename);
                    LoadPlayerToken();
                    refreshForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not load this Player file, may be corrupt or wrong type... try another - Error: " + ex.ToString());
                }
            }            
        }
        private void btnSavePlayer_Click(object sender, EventArgs e)
        {
            try
            {
                pc.HeadRefs = (ItemRefs)cmbHead.SelectedItem;
                pc.NeckRefs = (ItemRefs)cmbNeck.SelectedItem;
                pc.BodyRefs = (ItemRefs)cmbBody.SelectedItem;
                pc.MainHandRefs = (ItemRefs)cmbMainHand.SelectedItem;
                pc.OffHandRefs = (ItemRefs)cmbOffHand.SelectedItem;
                pc.RingRefs = (ItemRefs)cmbRing1.SelectedItem;
                pc.Ring2Refs = (ItemRefs)cmbRing2.SelectedItem;
                pc.FeetRefs = (ItemRefs)cmbFeet.SelectedItem;
                pc.AmmoRefs = (ItemRefs)cmbAmmo.SelectedItem;
                
                savePlayerFile(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\data\\" + pc.tag + ".json");
                MessageBox.Show("Using player's 'tag' for filename, Saved as: " + pc.tag + ".json");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not save this Player file - Error: " + ex.ToString());
            }
        }
        private void btnSelectIcon_Click(object sender, EventArgs e)
        {
            try
            {
                string name = prntForm.GetImageFilename("tkn_");
                if (name != "none")
                {
                    pc.tokenFilename = name;
                    LoadPlayerToken();
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed to load token...Error: " + ex.ToString());
            }
        }
        private void cbxKnownSpells_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //A nice trick to deal with events that you cannot process when they are raised is to delay 
            //the processing. Which you can do with the Control.BeginInvoke() method, it runs as soon 
            //as all events are dispatched, side-effects are complete and the UI thread goes idle again.
            //http://stackoverflow.com/questions/4454058/no-itemchecked-event-in-a-checkedlistbox/4454594#4454594
            //
            this.BeginInvoke((MethodInvoker)delegate
            {
                if (!refreshingList)
                {
                    pc.knownSpellsTags.Clear();
                    foreach (object itemChecked in cbxKnownSpells.CheckedItems)
                    {
                        Spell chkdSpell = (Spell)itemChecked;
                        pc.knownSpellsTags.Add(chkdSpell.tag);
                    }
                    
                }
            });
        }
        private void cbxKnownTraits_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //A nice trick to deal with events that you cannot process when they are raised is to delay 
            //the processing. Which you can do with the Control.BeginInvoke() method, it runs as soon 
            //as all events are dispatched, side-effects are complete and the UI thread goes idle again.
            //http://stackoverflow.com/questions/4454058/no-itemchecked-event-in-a-checkedlistbox/4454594#4454594
            //
            this.BeginInvoke((MethodInvoker)delegate
            {
                if (!refreshingList)
                {
                    pc.knownTraitsTags.Clear();
                    pc.knownUsableTraitsTags.Clear();
                    pc.knownOutsideCombatUsableTraitsTags.Clear();
                    pc.knownInCombatUsableTraitsTags.Clear();

                    foreach (object itemChecked in cbxKnownTraits.CheckedItems)
                    {
                        Trait chkdTrait = (Trait)itemChecked;
                        pc.knownTraitsTags.Add(chkdTrait.tag);
                        if (chkdTrait.associatedSpellTag != "none" && chkdTrait.associatedSpellTag != "")
                        {
                            pc.knownUsableTraitsTags.Add(chkdTrait.associatedSpellTag);
                            if (chkdTrait.useableInSituation == "InCombat")
                            {
                                pc.knownInCombatUsableTraitsTags.Add(chkdTrait.associatedSpellTag);
                            }
                            if (chkdTrait.useableInSituation == "OutOfCombat")
                            {
                                pc.knownOutsideCombatUsableTraitsTags.Add(chkdTrait.associatedSpellTag);
                            }
                            if (chkdTrait.useableInSituation == "Always")
                            {
                                pc.knownInCombatUsableTraitsTags.Add(chkdTrait.associatedSpellTag);
                                pc.knownOutsideCombatUsableTraitsTags.Add(chkdTrait.associatedSpellTag);
                            }
                        }
                    }

                }
            });
        }
        #endregion

        #region Methods
        public void fillItemLists()
        {
            itemsHeadList.Clear();
            itemsHeadList.Add(new ItemRefs());
            foreach (Item it in prntForm.itemsList)
            {
                if (it.category == "Head")
                {
                    itemsHeadList.Add(prntForm.createItemRefsFromItem(it));
                }
            }
            
            itemsNeckList.Clear();
            itemsNeckList.Add(new ItemRefs());
            foreach (Item it in prntForm.itemsList)
            {
                if (it.category == "Neck")
                {
                    itemsNeckList.Add(prntForm.createItemRefsFromItem(it));
                }
            }

            itemsBodyList.Clear();
            itemsBodyList.Add(new ItemRefs());
            foreach (Item it in prntForm.itemsList)
            {
                if (it.category == "Armor")
                {
                    itemsBodyList.Add(prntForm.createItemRefsFromItem(it));
                }
            }

            itemsMainHandList.Clear();
            itemsMainHandList.Add(new ItemRefs());
            foreach (Item it in prntForm.itemsList)
            {
                if ((it.category == "Melee") || (it.category == "Ranged"))
                {
                    itemsMainHandList.Add(prntForm.createItemRefsFromItem(it));
                }
            }

            itemsOffHandList.Clear();
            itemsOffHandList.Add(new ItemRefs());
            foreach (Item it in prntForm.itemsList)
            {
                if (it.category == "Shield")
                {
                    itemsOffHandList.Add(prntForm.createItemRefsFromItem(it));
                }
            }

            itemsRing1List.Clear();
            itemsRing1List.Add(new ItemRefs());
            foreach (Item it in prntForm.itemsList)
            {
                if (it.category == "Ring")
                {
                    itemsRing1List.Add(prntForm.createItemRefsFromItem(it));
                }
            }

            itemsRing2List.Clear();
            itemsRing2List.Add(new ItemRefs());
            foreach (Item it in prntForm.itemsList)
            {
                if (it.category == "Ring")
                {
                    itemsRing2List.Add(prntForm.createItemRefsFromItem(it));
                }
            }

            itemsFeetList.Clear();
            itemsFeetList.Add(new ItemRefs());
            foreach (Item it in prntForm.itemsList)
            {
                if (it.category == "Feet")
                {
                    itemsFeetList.Add(prntForm.createItemRefsFromItem(it));
                }
            }

            itemsAmmoList.Clear();
            itemsAmmoList.Add(new ItemRefs());
            foreach (Item it in prntForm.itemsList)
            {
                if (it.category == "Ammo")
                {
                    itemsAmmoList.Add(prntForm.createItemRefsFromItem(it));
                }
            }
        }
        public void refreshForm()
        {
            fillItemLists();
            propertyGrid1.SelectedObject = pc;
            refreshSpellsKnown();
            refreshTraitsKnown();
            refreshTokenDisplay();
            refreshCmbItems();
            refreshCmbSelected();
        }
        public void savePlayerFile(string filename)
        {
            string json = JsonConvert.SerializeObject(pc, Newtonsoft.Json.Formatting.Indented);
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.Write(json.ToString());
            }
        }
        public Player loadPlayerFile(string filename)
        {
            Player toReturn = null;

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(filename))
            {
                JsonSerializer serializer = new JsonSerializer();
                toReturn = (Player)serializer.Deserialize(file, typeof(Player));
            }
            return toReturn;
        }
        public void refreshTokenDisplay()
        {
            try
            {
                if (pc.tag != "newTag")
                {
                    pbIcon.BackgroundImage = (Image)iconBitmap;
                    if (iconBitmap == null) { MessageBox.Show("returned a null icon bitmap"); }
                }
            }
            catch { }
        }
        public void LoadPlayerToken()
        {
            if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + pc.tokenFilename + ".png"))
            {
                iconBitmap = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + pc.tokenFilename + ".png");
            }
            else if (File.Exists(prntForm._mainDirectory + "\\default\\NewModule\\graphics\\" + pc.tokenFilename + ".png"))
            {
                iconBitmap = new Bitmap(prntForm._mainDirectory + "\\default\\NewModule\\graphics\\" + pc.tokenFilename + ".png");
            }
            else
            {
                iconBitmap = new Bitmap(prntForm._mainDirectory + "\\default\\NewModule\\graphics\\" + "missingtexture.png");
            }
            refreshTokenDisplay();
        }
        public void refreshSpellsKnown()
        {
            cbxKnownSpells.BeginUpdate();
            cbxKnownSpells.DataSource = null;
            cbxKnownSpells.DataSource = prntForm.spellsList;
            cbxKnownSpells.DisplayMember = "name";
            cbxKnownSpells.EndUpdate();

            //uncheck all first
            for (int i = 0; i < cbxKnownSpells.Items.Count; i++)
            {
                cbxKnownSpells.SetItemChecked(i, false);
            }
            //iterate and check ones in list
            refreshingList = true;
            for (int i = 0; i < cbxKnownSpells.Items.Count; i++)
            {
                Spell thisSpell = (Spell)cbxKnownSpells.Items[i];
                if (pc.knownSpellsTags.Contains((string)thisSpell.tag))
                {
                    cbxKnownSpells.SetItemChecked(i, true);
                }
            }
            refreshingList = false;
        }
        public void refreshTraitsKnown()
        {
            cbxKnownTraits.BeginUpdate();
            cbxKnownTraits.DataSource = null;
            cbxKnownTraits.DataSource = prntForm.traitsList;
            cbxKnownTraits.DisplayMember = "name";
            cbxKnownTraits.EndUpdate();

            //uncheck all first
            for (int i = 0; i < cbxKnownTraits.Items.Count; i++)
            {
                cbxKnownTraits.SetItemChecked(i, false);
            }
            //iterate and check ones in list
            refreshingList = true;
            for (int i = 0; i < cbxKnownTraits.Items.Count; i++)
            {
                Trait thisTrait = (Trait)cbxKnownTraits.Items[i];
                if (pc.knownTraitsTags.Contains((string)thisTrait.tag))
                {
                    cbxKnownTraits.SetItemChecked(i, true);
                }
            }
            refreshingList = false;            
        }
        public void refreshCmbSelected()
        {
            foreach (ItemRefs itref in itemsBodyList)
            {
                if (itref.resref == pc.BodyRefs.resref)
                {
                    cmbBody.SelectedItem = itref;
                }
            }
            foreach (ItemRefs itref in itemsFeetList)
            {
                if (itref.resref == pc.FeetRefs.resref)
                {
                    cmbFeet.SelectedItem = itref;
                }
            }
            foreach (ItemRefs itref in itemsHeadList)
            {
                if (itref.resref == pc.HeadRefs.resref)
                {
                    cmbHead.SelectedItem = itref;
                }
            }
            foreach (ItemRefs itref in itemsMainHandList)
            {
                if (itref.resref == pc.MainHandRefs.resref)
                {
                    cmbMainHand.SelectedItem = itref;
                }
            }
            foreach (ItemRefs itref in itemsNeckList)
            {
                if (itref.resref == pc.NeckRefs.resref)
                {
                    cmbNeck.SelectedItem = itref;
                }
            }
            foreach (ItemRefs itref in itemsOffHandList)
            {
                if (itref.resref == pc.OffHandRefs.resref)
                {
                    cmbOffHand.SelectedItem = itref;
                }
            }
            foreach (ItemRefs itref in itemsRing1List)
            {
                if (itref.resref == pc.RingRefs.resref)
                {
                    cmbRing1.SelectedItem = itref;
                }
            }
            foreach (ItemRefs itref in itemsRing2List)
            {
                if (itref.resref == pc.Ring2Refs.resref)
                {
                    cmbRing2.SelectedItem = itref;
                }
            }
            foreach (ItemRefs itref in itemsAmmoList)
            {
                if (itref.resref == pc.AmmoRefs.resref)
                {
                    cmbAmmo.SelectedItem = itref;
                }
            }
            this.Invalidate();
        }
        public void refreshCmbItems()
        {
            cmbHead.BeginUpdate();
            cmbHead.DataSource = null;
            cmbHead.DataSource = itemsHeadList;
            cmbHead.DisplayMember = "name";
            cmbHead.EndUpdate();
            cmbHead.SelectedItem = pc.HeadRefs;

            cmbNeck.BeginUpdate();
            cmbNeck.DataSource = null;
            cmbNeck.DataSource = itemsNeckList;
            cmbNeck.DisplayMember = "name";
            cmbNeck.EndUpdate();
            cmbNeck.SelectedItem = pc.NeckRefs;
            
            cmbBody.BeginUpdate();
            cmbBody.DataSource = null;
            cmbBody.DataSource = itemsBodyList;
            cmbBody.DisplayMember = "name";
            cmbBody.EndUpdate();
            cmbBody.SelectedItem = pc.BodyRefs;
            
            cmbMainHand.BeginUpdate();
            cmbMainHand.DataSource = null;
            cmbMainHand.DataSource = itemsMainHandList;
            cmbMainHand.DisplayMember = "name";            
            cmbMainHand.SelectedItem = pc.MainHandRefs;
            cmbMainHand.EndUpdate();
            
            cmbOffHand.BeginUpdate();
            cmbOffHand.DataSource = null;
            cmbOffHand.DataSource = itemsOffHandList;
            cmbOffHand.DisplayMember = "name";
            cmbOffHand.EndUpdate();
            cmbOffHand.SelectedItem = pc.OffHandRefs;
            
            cmbRing1.BeginUpdate();
            cmbRing1.DataSource = null;
            cmbRing1.DataSource = itemsRing1List;
            cmbRing1.DisplayMember = "name";
            cmbRing1.EndUpdate();
            cmbRing1.SelectedItem = pc.RingRefs;
            
            cmbRing2.BeginUpdate();
            cmbRing2.DataSource = null;
            cmbRing2.DataSource = itemsRing2List;
            cmbRing2.DisplayMember = "name";
            cmbRing2.EndUpdate();
            cmbRing2.SelectedItem = pc.Ring2Refs;
            
            cmbFeet.BeginUpdate();
            cmbFeet.DataSource = null;
            cmbFeet.DataSource = itemsFeetList;
            cmbFeet.DisplayMember = "name";
            cmbFeet.EndUpdate();
            cmbFeet.SelectedItem = pc.FeetRefs;

            cmbAmmo.BeginUpdate();
            cmbAmmo.DataSource = null;
            cmbAmmo.DataSource = itemsAmmoList;
            cmbAmmo.DisplayMember = "name";
            cmbAmmo.EndUpdate();
            cmbAmmo.SelectedItem = pc.AmmoRefs;
        }
        #endregion
    }
}
