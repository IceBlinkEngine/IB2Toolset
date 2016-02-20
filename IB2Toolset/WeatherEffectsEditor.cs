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
    public partial class WeatherEffectsEditor : Form
    {
        private Module mod = new Module();
        //private Game game;
        private ParentForm prntForm;
        private int selectedLbxIndex = 0;

        public WeatherEffectsEditor(Module m, ParentForm pf)
        {
            InitializeComponent();
            mod = m;
            //game = g;
            prntForm = pf;
            refreshListBox();
        }
        private void refreshListBox()
        {
            lbxWeatherEffects.BeginUpdate();
            lbxWeatherEffects.DataSource = null;
            lbxWeatherEffects.DataSource = prntForm.weatherEffectsList;
            lbxWeatherEffects.DisplayMember = "name";
            lbxWeatherEffects.EndUpdate();
        }
        private void btnAddTrait_Click(object sender, EventArgs e)
        {
            WeatherEffect newTS = new WeatherEffect();
            newTS.name = "newWeatherEffect";
            newTS.tag = "newWeatherEffectTag_" + prntForm.mod.nextIdNumber.ToString();

            for (int i = 4; i < 10; i++)
                {
                    FullScreenEffectLayer newLayer = new FullScreenEffectLayer();
                    newLayer.fullScreenEffectOrderNumber = i + 1;
                    newTS.WeatherLayers.Add(newLayer);
                }
            
            prntForm.weatherEffectsList.Add(newTS);
            refreshListBox();
        }
        private void btnRemoveTrait_Click(object sender, EventArgs e)
        {
            if (lbxWeatherEffects.Items.Count > 0)
            {
                try
                {
                    // The Remove button was clicked.
                    int selectedIndex = lbxWeatherEffects.SelectedIndex;
                    //mod.ModuleContainersList.containers.RemoveAt(selectedIndex);
                    prntForm.weatherEffectsList.RemoveAt(selectedIndex);
                }
                catch { }
                selectedLbxIndex = 0;
                lbxWeatherEffects.SelectedIndex = 0;
                refreshListBox();
            }
        }        
        private void btnDuplicateTrait_Click(object sender, EventArgs e)
        {
            WeatherEffect newCopy = prntForm.weatherEffectsList[selectedLbxIndex].DeepCopy();
            newCopy.tag = "newWeatherEffectTag_" + prntForm.mod.nextIdNumber.ToString();
            prntForm.weatherEffectsList.Add(newCopy);
            refreshListBox();
        }
        private void lbxTraits_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if ((lbxWeatherEffects.SelectedIndex >= 0) && (prntForm.weatherEffectsList != null))
            {
                selectedLbxIndex = lbxWeatherEffects.SelectedIndex;
                lbxWeatherEffects.SelectedIndex = selectedLbxIndex;
                propertyGrid1.SelectedObject = prntForm.weatherEffectsList[selectedLbxIndex];
            }
        } 
        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            refreshListBox();
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
        private void TraitEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            checkForNewTraits();
            checkForDeletedTraits();
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            prntForm.weatherEffectsList = prntForm.weatherEffectsList.OrderBy(o => o.name).ToList();
            refreshListBox();
        }       
    }
}
