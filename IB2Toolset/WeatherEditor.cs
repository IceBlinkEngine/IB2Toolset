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
    public partial class WeatherEditor : Form
    {
        private Module mod = new Module();
        //private Game game;
        private ParentForm prntForm;
        private int selectedLbxIndex = 0;
        private bool blockList1IndexChangedEvent = false;
        private bool blockList2IndexChangedEvent = false;
        private bool blockList3IndexChangedEvent = false;

        public WeatherEditor(Module m, ParentForm pf)
        {
            InitializeComponent();
            mod = m;
            //game = g;
            prntForm = pf;
            refreshListBox();
        }
        private void refreshListBox()
        {
            int storedIndex2 = lbxWeathers.SelectedIndex;
            lbxWeathers.BeginUpdate();
            lbxWeathers.DataSource = null;
            lbxWeathers.DataSource = prntForm.weathersList;
            lbxWeathers.DisplayMember = "name";
            lbxWeathers.SelectedIndex = storedIndex2;
            lbxWeathers.EndUpdate();
            lbxWeathers.SelectedIndex = storedIndex2;

            if (lbxWeathers.SelectedIndex >= 0)
            {
                int storedIndex = lbxWeatherTypeLists.SelectedIndex;
                lbxWeatherTypeLists.BeginUpdate();
                lbxWeatherTypeLists.DataSource = null;
                lbxWeatherTypeLists.DataSource = prntForm.weathersList[lbxWeathers.SelectedIndex].weatherTypeLists;
                lbxWeatherTypeLists.DisplayMember = "name";
                lbxWeatherTypeLists.SelectedIndex = storedIndex;
                lbxWeatherTypeLists.EndUpdate();
                lbxWeatherTypeLists.SelectedIndex = storedIndex;
            }

            if ((lbxWeatherTypeLists.SelectedIndex >= 0) && (lbxWeathers.SelectedIndex >= 0))
            {
                //if (lbxWeatherTypeListItems.SelectedIndex >= 0)
                //{
                    //prntForm.weathersList[lbxWeathers.SelectedIndex].weatherTypeLists[lbxWeatherTypeLists.SelectedIndex].weatherTypeListItems[lbxWeatherTypeListItems.SelectedIndex].name = prntForm.weathersList[lbxWeathers.SelectedIndex].weatherTypeLists[lbxWeatherTypeLists.SelectedIndex].weatherTypeListItems[lbxWeatherTypeListItems.SelectedIndex].weatherEffectName;
                //}
                lbxWeatherTypeListItems.BeginUpdate();
                lbxWeatherTypeListItems.DataSource = null;
                lbxWeatherTypeListItems.DataSource = prntForm.weathersList[lbxWeathers.SelectedIndex].weatherTypeLists[lbxWeatherTypeLists.SelectedIndex].weatherTypeListItems;
                lbxWeatherTypeListItems.DisplayMember = "combinedInfo";
                lbxWeatherTypeListItems.EndUpdate();
            }
        }




        private void refreshListBoxAdd1()
        {
            lbxWeathers.BeginUpdate();
            lbxWeathers.DataSource = null;
            lbxWeathers.DataSource = prntForm.weathersList;
            lbxWeathers.DisplayMember = "name";
            lbxWeathers.EndUpdate();
        }

        private void refreshListBoxAdd2()
        {
            if (lbxWeathers.SelectedIndex >= 0)
            {
                lbxWeatherTypeLists.BeginUpdate();
                lbxWeatherTypeLists.DataSource = null;
                lbxWeatherTypeLists.DataSource = prntForm.weathersList[lbxWeathers.SelectedIndex].weatherTypeLists;
                lbxWeatherTypeLists.DisplayMember = "name";
                lbxWeatherTypeLists.EndUpdate();
            }
        }

        private void refreshListBoxAdd3()
        {
            
            if ((lbxWeatherTypeLists.SelectedIndex >= 0) && (lbxWeathers.SelectedIndex >= 0))
            {
                lbxWeatherTypeListItems.BeginUpdate();
                lbxWeatherTypeListItems.DataSource = null;
                lbxWeatherTypeListItems.DataSource = prntForm.weathersList[lbxWeathers.SelectedIndex].weatherTypeLists[lbxWeatherTypeLists.SelectedIndex].weatherTypeListItems;
                lbxWeatherTypeListItems.DisplayMember = "combinedInfo";
                lbxWeatherTypeListItems.EndUpdate();
            }
        }

        private void refreshListBoxWeatherEffectName()
        {
            int storedIndex = lbxWeatherTypeListItems.SelectedIndex;
            if ((lbxWeatherTypeLists.SelectedIndex >= 0) && (lbxWeathers.SelectedIndex >= 0))
            {
                //if (lbxWeatherTypeListItems.SelectedIndex >= 0)
                //{
                    //prntForm.weathersList[lbxWeathers.SelectedIndex].weatherTypeLists[lbxWeatherTypeLists.SelectedIndex].weatherTypeListItems[lbxWeatherTypeListItems.SelectedIndex].name = prntForm.weathersList[lbxWeathers.SelectedIndex].weatherTypeLists[lbxWeatherTypeLists.SelectedIndex].weatherTypeListItems[lbxWeatherTypeListItems.SelectedIndex].combinedInfo;
                //}
                lbxWeatherTypeListItems.BeginUpdate();
                lbxWeatherTypeListItems.DataSource = null;
                lbxWeatherTypeListItems.DataSource = prntForm.weathersList[lbxWeathers.SelectedIndex].weatherTypeLists[lbxWeatherTypeLists.SelectedIndex].weatherTypeListItems;
                lbxWeatherTypeListItems.DisplayMember = "combinedInfo";
                lbxWeatherTypeListItems.SelectedIndex = storedIndex;
                lbxWeatherTypeListItems.EndUpdate();
            }
            lbxWeatherTypeListItems.SelectedIndex = storedIndex;
        }











        private void refreshListBox1()
        {
            
            if (lbxWeathers.SelectedIndex >= 0)
            {
                lbxWeatherTypeLists.BeginUpdate();
                lbxWeatherTypeLists.DataSource = null;
                lbxWeatherTypeLists.DataSource = prntForm.weathersList[lbxWeathers.SelectedIndex].weatherTypeLists;
                lbxWeatherTypeLists.DisplayMember = "name";
                lbxWeatherTypeLists.EndUpdate();
            }

            if ((lbxWeatherTypeLists.SelectedIndex >= 0) && (lbxWeathers.SelectedIndex >= 0))
            {
                lbxWeatherTypeListItems.BeginUpdate();
                lbxWeatherTypeListItems.DataSource = null;
                lbxWeatherTypeListItems.DataSource = prntForm.weathersList[lbxWeathers.SelectedIndex].weatherTypeLists[lbxWeatherTypeLists.SelectedIndex].weatherTypeListItems;
                lbxWeatherTypeListItems.DisplayMember = "combinedInfo";
                lbxWeatherTypeListItems.EndUpdate();
            }

            if ((lbxWeatherTypeLists.SelectedIndex < 0) && (lbxWeathers.SelectedIndex >= 0))
            {
                lbxWeatherTypeListItems.BeginUpdate();
                lbxWeatherTypeListItems.DataSource = null;
                //lbxWeatherTypeListItems.DataSource = prntForm.weathersList[lbxWeathers.SelectedIndex].weatherTypeLists[lbxWeatherTypeLists.SelectedIndex].weatherTypeListItems;
                //lbxWeatherTypeListItems.DisplayMember = "name";
                lbxWeatherTypeListItems.EndUpdate();
            }
        }

        private void refreshListBox2()
        {
            
            if ((lbxWeatherTypeLists.SelectedIndex >= 0) && (lbxWeathers.SelectedIndex >= 0))
            {
                lbxWeatherTypeListItems.BeginUpdate();
                lbxWeatherTypeListItems.DataSource = null;
                lbxWeatherTypeListItems.DataSource = prntForm.weathersList[lbxWeathers.SelectedIndex].weatherTypeLists[lbxWeatherTypeLists.SelectedIndex].weatherTypeListItems;
                lbxWeatherTypeListItems.DisplayMember = "combinedInfo";
                lbxWeatherTypeListItems.EndUpdate();
            }
        }

        private void refreshListBox3()
        {
            
               
        }


        private void btnAddWeather_Click(object sender, EventArgs e)
        {
            Weather newTS = new Weather();
            newTS.name = "newWeather";
            newTS.tag = "newWeatherTag_" + prntForm.mod.nextIdNumber.ToString();
            prntForm.weathersList.Add(newTS);
            refreshListBoxAdd1();
        }
        private void btnRemoveWeather_Click(object sender, EventArgs e)
        {
            if (lbxWeathers.Items.Count > 0)
            {
                try
                {
                    // The Remove button was clicked.
                    int selectedIndex = lbxWeathers.SelectedIndex;
                    //mod.ModuleContainersList.containers.RemoveAt(selectedIndex);
                    prntForm.weathersList.RemoveAt(selectedIndex);
                }
                catch { }
                selectedLbxIndex = 0;
                lbxWeathers.SelectedIndex = 0;
                refreshListBox();
            }
        }
        private void btnDuplicateWeather_Click(object sender, EventArgs e)
        {
            if (lbxWeathers.SelectedIndex >= 0)
            {
                Weather newCopy = prntForm.weathersList[lbxWeathers.SelectedIndex].DeepCopy();
                newCopy.tag = "newWeatherTag_" + prntForm.mod.nextIdNumber.ToString();
                prntForm.weathersList.Add(newCopy);
                refreshListBox();
            }
        }

        private void btnAddWeatherTypeList_Click(object sender, EventArgs e)
        {
            if (lbxWeathers.SelectedIndex >= 0)
            {
                WeatherTypeList newTS = new WeatherTypeList();
                if (lbxWeatherTypeLists.Items.Count == 0)
                {
                    newTS.name = "entryList";
                }
                else
                {
                    newTS.name = "newWeatherTypeList";
                }
                newTS.tag = "newWeatherTypeListTag_" + prntForm.mod.nextIdNumber.ToString();
                prntForm.weathersList[lbxWeathers.SelectedIndex].weatherTypeLists.Add(newTS);
                refreshListBoxAdd2();
            }
        }

        private void btnRemoveWeatherTypeList_Click(object sender, EventArgs e)
        {
            if ((lbxWeatherTypeLists.Items.Count > 0) && (lbxWeathers.SelectedIndex >= 0))
            {
                try
                {
                    // The Remove button was clicked.
                    int selectedIndex = lbxWeatherTypeLists.SelectedIndex;
                    //mod.ModuleContainersList.containers.RemoveAt(selectedIndex);
                    prntForm.weathersList[lbxWeathers.SelectedIndex].weatherTypeLists.RemoveAt(selectedIndex);
                }
                catch { }
                selectedLbxIndex = 0;
                lbxWeatherTypeLists.SelectedIndex = 0;
                refreshListBox();
            }
        }
        private void btnDuplicateWeatherTypeList_Click(object sender, EventArgs e)
        {
            if (lbxWeatherTypeLists.SelectedIndex >= 0)
            {
                WeatherTypeList newCopy = prntForm.weathersList[lbxWeathers.SelectedIndex].weatherTypeLists[lbxWeatherTypeLists.SelectedIndex].DeepCopy();
                newCopy.tag = "newWeatherTypeListTag_" + prntForm.mod.nextIdNumber.ToString();
                prntForm.weathersList[lbxWeathers.SelectedIndex].weatherTypeLists.Add(newCopy);
                refreshListBox();
            }
        }




        private void btnAddWeatherTypeListItem_Click(object sender, EventArgs e)
        {
            if ((lbxWeathers.SelectedIndex >= 0) && (lbxWeatherTypeLists.SelectedIndex >= 0))
            {
                WeatherTypeListItem newTS = new WeatherTypeListItem();
                newTS.name = "newWeatherTypeListItem";
                newTS.tag = "newWeatherTypeListItemTag_" + prntForm.mod.nextIdNumber.ToString();
                prntForm.weathersList[lbxWeathers.SelectedIndex].weatherTypeLists[lbxWeatherTypeLists.SelectedIndex].weatherTypeListItems.Add(newTS);
                refreshListBoxAdd3();
            }
        }

        private void btnRemoveWeatherTypeListItem_Click(object sender, EventArgs e)
        {
            if ((lbxWeatherTypeListItems.Items.Count > 0) && (lbxWeathers.SelectedIndex >= 0) && (lbxWeatherTypeLists.SelectedIndex >= 0))
            {
                try
                {
                    // The Remove button was clicked.
                    int selectedIndex = lbxWeatherTypeListItems.SelectedIndex;
                    //mod.ModuleContainersList.containers.RemoveAt(selectedIndex);
                    prntForm.weathersList[lbxWeathers.SelectedIndex].weatherTypeLists[lbxWeatherTypeLists.SelectedIndex].weatherTypeListItems.RemoveAt(selectedIndex);
                }
                catch { }
                selectedLbxIndex = 0;
                lbxWeatherTypeListItems.SelectedIndex = 0;
                refreshListBox();
            }
        }
        private void btnDuplicateWeatherTypeListItem_Click(object sender, EventArgs e)
        {
            if (lbxWeatherTypeListItems.SelectedIndex >= 0)
            {
                WeatherTypeListItem newCopy = prntForm.weathersList[lbxWeathers.SelectedIndex].weatherTypeLists[lbxWeatherTypeLists.SelectedIndex].weatherTypeListItems[lbxWeatherTypeListItems.SelectedIndex].DeepCopy();
                newCopy.tag = "newWeatherTypeListItemTag_" + prntForm.mod.nextIdNumber.ToString();
                prntForm.weathersList[lbxWeathers.SelectedIndex].weatherTypeLists[lbxWeatherTypeLists.SelectedIndex].weatherTypeListItems.Add(newCopy);
                refreshListBox();
            }
        }


        private void lbxWeathers_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if ((lbxWeathers.SelectedIndex >= 0) && (prntForm.weathersList != null) && (blockList1IndexChangedEvent == false))
            {
                selectedLbxIndex = lbxWeathers.SelectedIndex;
                lbxWeathers.SelectedIndex = selectedLbxIndex;
                propertyGrid1.SelectedObject = prntForm.weathersList[selectedLbxIndex];
                //lbxWeatherTypeLists.SelectedIndex = -1;
                //lbxWeatherTypeListItems.SelectedIndex = -1;
                blockList2IndexChangedEvent = true;
                blockList3IndexChangedEvent = true;
                refreshListBox1();
                blockList2IndexChangedEvent = false;
                blockList3IndexChangedEvent = false;
                //lbxWeatherTypeLists.SelectedIndex = -1;
                //lbxWeatherTypeListItems.SelectedIndex = -1;
            }
        }

        private void lbxWeatherTypeLists_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if ((lbxWeathers.SelectedIndex >= 0) && (lbxWeatherTypeLists.SelectedIndex >= 0) && (prntForm.weathersList != null) && (blockList2IndexChangedEvent == false))
            {
                

                selectedLbxIndex = lbxWeatherTypeLists.SelectedIndex;
                lbxWeatherTypeLists.SelectedIndex = selectedLbxIndex;
                propertyGrid1.SelectedObject = prntForm.weathersList[lbxWeathers.SelectedIndex].weatherTypeLists[selectedLbxIndex];
                //lbxWeathers.SelectedIndex = -1;
                //lbxWeatherTypeLists.SelectedIndex = -1;
                blockList1IndexChangedEvent = true;
                blockList3IndexChangedEvent = true;
                refreshListBox2();
                blockList1IndexChangedEvent = false;
                blockList3IndexChangedEvent = false;
                //lbxWeathers.SelectedIndex = -1;
                //lbxWeatherTypeLists.SelectedIndex = -1;
            }
        }
        private void lbxWeatherTypeListItems_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if ((lbxWeathers.SelectedIndex >= 0) && (lbxWeatherTypeLists.SelectedIndex >= 0) && (lbxWeatherTypeListItems.SelectedIndex >= 0) && (prntForm.weathersList != null)&& (blockList3IndexChangedEvent == false))
            {
               
                selectedLbxIndex = lbxWeatherTypeListItems.SelectedIndex;
                lbxWeatherTypeListItems.SelectedIndex = selectedLbxIndex;
                propertyGrid1.SelectedObject = prntForm.weathersList[lbxWeathers.SelectedIndex].weatherTypeLists[lbxWeatherTypeLists.SelectedIndex].weatherTypeListItems[selectedLbxIndex];
                //lbxWeatherTypeLists.SelectedIndex = -1;
                //lbxWeathers.SelectedIndex = -1;
                blockList1IndexChangedEvent = true;
                blockList2IndexChangedEvent = true;
                refreshListBox3();
                blockList1IndexChangedEvent = false;
                blockList2IndexChangedEvent = false;
                //lbxWeatherTypeLists.SelectedIndex = -1;
                //lbxWeathers.SelectedIndex = -1;
            }
        }

        private void lbxWeathers_clicked(object sender, EventArgs e)
        {

        }

        private void lbxWeatherTypeLists_clicked(object sender, EventArgs e)
        {

        }
        private void lbxWeatherTypeListItems_clicked(object sender, EventArgs e)
        {

        }




        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if ((e.ChangedItem.Label == "weatherEffectName") || (e.ChangedItem.Label == "chance") || (e.ChangedItem.Label == "duration"))
            {
                refreshListBoxWeatherEffectName();
            }
            else
            {
                refreshListBox();
            }
        }
        private void checkForNewTraits()
        {
            ///not needed here?
            /*
            bool foundOne = false;
            
                foreach (WeatherEffect tr in prntForm.weatherEffectsList)
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
                        cl.traitsAllowed.Add(newTA);
                    }
                    else
                    {
                        foundOne = false;
                    }
                }
                */
            
        }
        private void checkForDeletedTraits()
        {
            //not needed here?
            /*
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
            */
        }
        private void WeatherEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            checkForNewTraits();
            checkForDeletedTraits();
        }

        private void btnSortWeathers_Click(object sender, EventArgs e)
        {
            if (prntForm.weathersList.Count > 0)
            {
                prntForm.weathersList = prntForm.weathersList.OrderBy(o => o.name).ToList();
                refreshListBox();
            }
        }

        private void btnSortWeatherTypeLists_Click(object sender, EventArgs e)
        {
            if (lbxWeathers.SelectedIndex >= 0)
            {
                //prntForm.weathersList[lbxWeathers.SelectedIndex]
                prntForm.weathersList[lbxWeathers.SelectedIndex].weatherTypeLists = prntForm.weathersList[lbxWeathers.SelectedIndex].weatherTypeLists.OrderBy(o => o.name).ToList();
                refreshListBox();
            }
        }

        private void btnSortWeatherTypeListItems_Click(object sender, EventArgs e)
        {
            if ((lbxWeathers.SelectedIndex >= 0) && (lbxWeatherTypeLists.SelectedIndex >= 0))
                //prntForm.weathersList[lbxWeathers.SelectedIndex]
                prntForm.weathersList[lbxWeathers.SelectedIndex].weatherTypeLists[lbxWeatherTypeLists.SelectedIndex].weatherTypeListItems = prntForm.weathersList[lbxWeathers.SelectedIndex].weatherTypeLists[lbxWeatherTypeLists.SelectedIndex].weatherTypeListItems.OrderBy(o => o.name).ToList();
            refreshListBox();
        }

        private void propertyGrid1_Click(object sender, EventArgs e)
        {

        }

        private void WeatherEditor_Load(object sender, EventArgs e)
        {

        }
    }
}
