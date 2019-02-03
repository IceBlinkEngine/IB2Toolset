using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;

namespace IB2Toolset
{
    public partial class TriggerEventsForm : DockContent
    {
        public ParentForm prntForm;
        public bool userPressedE1tag = false;
        public bool userPressedE2tag = false;
        public bool userPressedE3tag = false;
        public List<string> containersList = new List<string>();
        public List<string> convosList = new List<string>();
        public List<string> logictreesList = new List<string>();
        public List<string> encountersList = new List<string>();
        public List<string> scriptsList = new List<string>();
        public List<string> ibscriptsList = new List<string>();
        public List<string> areasList = new List<string>();


        public TriggerEventsForm(ParentForm pf)
        {
            InitializeComponent();
            prntForm = pf;
        }

        #region Event Handlers
        private void chkTrigEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (prntForm.currentSelectedTrigger != null)
            {
                prntForm.currentSelectedTrigger.Enabled = chkTrigEnabled.Checked;
            }
        }
        private void chkTrigOnce_CheckedChanged(object sender, EventArgs e)
        {
            if (prntForm.currentSelectedTrigger != null)
            {
                prntForm.currentSelectedTrigger.DoOnceOnly = chkTrigOnce.Checked;
            }
        }
        private void chkTrigActiveSearch_CheckedChanged(object sender, EventArgs e)
        {
            if (prntForm.currentSelectedTrigger != null)
            {
                prntForm.currentSelectedTrigger.RequiresActiveSearch = chkTrigActiveSearch.Checked;
            }
        }

        private void chkTrigConnectedDiscovery_CheckedChanged(object sender, EventArgs e)
        {
            if (prntForm.currentSelectedTrigger != null)
            {
                prntForm.currentSelectedTrigger.ConnectedDiscovery = chkTrigConnectedDiscovery.Checked;
            }
        }

        private void txtTrigTag_TextChanged(object sender, EventArgs e)
        {
            if (prntForm.currentSelectedTrigger != null)
            {
                prntForm.currentSelectedTrigger.TriggerTag = txtTrigTag.Text;
            }
        }

        private void chkTriggeredByPc_CheckedChanged(object sender, EventArgs e)
        {  
             if (prntForm.currentSelectedTrigger != null)  
             {  
                 prntForm.currentSelectedTrigger.canBeTriggeredByPc = chkTriggeredByPc.Checked;  
             }  
         }  
         private void chkTriggeredByCreatures_CheckedChanged(object sender, EventArgs e)
         {  
             if (prntForm.currentSelectedTrigger != null)  
             {  
                 prntForm.currentSelectedTrigger.canBeTriggeredByCreature = chkTriggeredByCreatures.Checked;  
             }  
         }
        private void chkEncounterTriggerOnEveryStep_CheckedChanged(object sender, EventArgs e)
        {
            if (prntForm.currentSelectedTrigger != null)
            {
                prntForm.currentSelectedTrigger.encounterTriggerOnEveryStep = chkEncounterTriggerOnEveryStep.Checked;
            }
        }
        private void numTriggerCallsAllowed_ValueChanged(object sender, EventArgs e)
         {  
             if (prntForm.currentSelectedTrigger != null)  
             {  
                 prntForm.currentSelectedTrigger.numberOfScriptCallsRemaining = (int)numTriggerCallsAllowed.Value;  
             }  
         }  

        #region EVENT 1
        private void chkE1enabled_CheckedChanged(object sender, EventArgs e)
        {
            if (prntForm.currentSelectedTrigger != null)
            {
                prntForm.currentSelectedTrigger.EnabledEvent1 = chkE1enabled.Checked;
            }
        }
        private void chkE1once_CheckedChanged(object sender, EventArgs e)
        {
            if (prntForm.currentSelectedTrigger != null)
            {
                prntForm.currentSelectedTrigger.DoOnceOnlyEvent1 = chkE1once.Checked;
            }
        }
        private void cmbE1type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((prntForm.currentSelectedTrigger != null) && (cmbE1type.SelectedItem != null))
            {
                prntForm.currentSelectedTrigger.Event1Type = cmbE1type.SelectedItem.ToString();
                refreshCmbE1tag();
            }
        }
        private void cmbE1tag_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((prntForm.currentSelectedTrigger != null) && (cmbE1tag.SelectedItem != null))
            {
                if (userPressedE1tag)
                {
                    prntForm.currentSelectedTrigger.Event1FilenameOrTag = cmbE1tag.SelectedItem.ToString();
                    userPressedE1tag = false;
                    loadE1script();
                }
            }
        }
        private void cmbE1tag_MouseDown(object sender, MouseEventArgs e)
        {
            userPressedE1tag = true;
        }
        private void btnE1reset_Click(object sender, EventArgs e)
        {
            prntForm.currentSelectedTrigger.Event1Parm1 = "none";
            prntForm.currentSelectedTrigger.Event1Parm2 = "none";
            prntForm.currentSelectedTrigger.Event1Parm3 = "none";
            prntForm.currentSelectedTrigger.Event1Parm4 = "none";
            txtE1parm1.Text = "none";
            txtE1parm2.Text = "none";
            txtE1parm3.Text = "none";
            txtE1parm4.Text = "none";
            prntForm.currentSelectedTrigger.Event1TransPointX = 0;
            prntForm.currentSelectedTrigger.Event1TransPointY = 0;
            numE1x.Value = 0;
            numE1y.Value = 0;
        }
        private void txtE1parm1_TextChanged(object sender, EventArgs e)
        {
            if (prntForm.currentSelectedTrigger != null)
            {
                prntForm.currentSelectedTrigger.Event1Parm1 = txtE1parm1.Text;
            }
        }        
        private void txtE1parm2_TextChanged(object sender, EventArgs e)
        {
            if (prntForm.currentSelectedTrigger != null)
            {
                prntForm.currentSelectedTrigger.Event1Parm2 = txtE1parm2.Text;
            }
        }
        private void txtE1parm3_TextChanged(object sender, EventArgs e)
        {
            if (prntForm.currentSelectedTrigger != null)
            {
                prntForm.currentSelectedTrigger.Event1Parm3 = txtE1parm3.Text;
            }
        }
        private void txtE1parm4_TextChanged(object sender, EventArgs e)
        {
            if (prntForm.currentSelectedTrigger != null)
            {
                prntForm.currentSelectedTrigger.Event1Parm4 = txtE1parm4.Text;
            }
        }
        private void txtE1parm1_MouseDown(object sender, MouseEventArgs e)
        {
            loadE1script();
        }
        private void txtE1parm2_MouseDown(object sender, MouseEventArgs e)
        {
            loadE1script();
        }
        private void txtE1parm3_MouseDown(object sender, MouseEventArgs e)
        {
            loadE1script();
        }
        private void txtE1parm4_MouseDown(object sender, MouseEventArgs e)
        {
            loadE1script();
        }
        private void numE1x_ValueChanged(object sender, EventArgs e)
        {
            if (prntForm.currentSelectedTrigger != null)
            {
                prntForm.currentSelectedTrigger.Event1TransPointX = (int)numE1x.Value;
            }
        }
        private void numE1y_ValueChanged(object sender, EventArgs e)
        {
            if (prntForm.currentSelectedTrigger != null)
            {
                prntForm.currentSelectedTrigger.Event1TransPointY = (int)numE1y.Value;
            }
        }
        #endregion

        #region EVENT 2
        private void chkE2enabled_CheckedChanged(object sender, EventArgs e)
        {
            if (prntForm.currentSelectedTrigger != null)
            {
                prntForm.currentSelectedTrigger.EnabledEvent2 = chkE2enabled.Checked;
            }
        }

        private void e2GcTrue_CheckedChanged(object sender, EventArgs e)
        {
            if (prntForm.currentSelectedTrigger != null)
            {
                prntForm.currentSelectedTrigger.Event2RequiresTrueReturnCheck = e2GcTrue.Checked;
            }
        }

        //e2GcTrue

        private void chkE2once_CheckedChanged(object sender, EventArgs e)
        {
            if (prntForm.currentSelectedTrigger != null)
            {
                prntForm.currentSelectedTrigger.DoOnceOnlyEvent2 = chkE2once.Checked;
            }
        }
        private void cmbE2type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((prntForm.currentSelectedTrigger != null) && (cmbE2type.SelectedItem != null))
            {
                prntForm.currentSelectedTrigger.Event2Type = cmbE2type.SelectedItem.ToString();
                refreshCmbE2tag();
            }
        }
        private void cmbE2tag_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((prntForm.currentSelectedTrigger != null) && (cmbE2tag.SelectedItem != null))
            {
                if (userPressedE2tag)
                {
                    prntForm.currentSelectedTrigger.Event2FilenameOrTag = cmbE2tag.SelectedItem.ToString();
                    userPressedE2tag = false;
                    loadE2script();
                }
            }
        }
        private void cmbE2tag_MouseDown(object sender, MouseEventArgs e)
        {
            userPressedE2tag = true;
        }
        private void btnE2reset_Click(object sender, EventArgs e)
        {
            prntForm.currentSelectedTrigger.Event2Parm1 = "none";
            prntForm.currentSelectedTrigger.Event2Parm2 = "none";
            prntForm.currentSelectedTrigger.Event2Parm3 = "none";
            prntForm.currentSelectedTrigger.Event2Parm4 = "none";
            txtE2parm1.Text = "none";
            txtE2parm2.Text = "none";
            txtE2parm3.Text = "none";
            txtE2parm4.Text = "none";
            prntForm.currentSelectedTrigger.Event2TransPointX = 0;
            prntForm.currentSelectedTrigger.Event2TransPointY = 0;
            numE2x.Value = 0;
            numE2y.Value = 0;
        }
        private void txtE2parm1_TextChanged(object sender, EventArgs e)
        {
            if (prntForm.currentSelectedTrigger != null)
            {
                prntForm.currentSelectedTrigger.Event2Parm1 = txtE2parm1.Text;
            }
        }
        private void txtE2parm2_TextChanged(object sender, EventArgs e)
        {
            if (prntForm.currentSelectedTrigger != null)
            {
                prntForm.currentSelectedTrigger.Event2Parm2 = txtE2parm2.Text;
            }
        }
        private void txtE2parm3_TextChanged(object sender, EventArgs e)
        {
            if (prntForm.currentSelectedTrigger != null)
            {
                prntForm.currentSelectedTrigger.Event2Parm3 = txtE2parm3.Text;
            }
        }
        private void txtE2parm4_TextChanged(object sender, EventArgs e)
        {
            if (prntForm.currentSelectedTrigger != null)
            {
                prntForm.currentSelectedTrigger.Event2Parm4 = txtE2parm4.Text;
            }
        }
        private void txtE2parm1_MouseDown(object sender, MouseEventArgs e)
        {
            loadE2script();
        }
        private void txtE2parm2_MouseDown(object sender, MouseEventArgs e)
        {
            loadE2script();
        }
        private void txtE2parm3_MouseDown(object sender, MouseEventArgs e)
        {
            loadE2script();
        }
        private void txtE2parm4_MouseDown(object sender, MouseEventArgs e)
        {
            loadE2script();
        }
        private void numE2x_ValueChanged(object sender, EventArgs e)
        {
            if (prntForm.currentSelectedTrigger != null)
            {
                prntForm.currentSelectedTrigger.Event2TransPointX = (int)numE2x.Value;
            }
        }
        private void numE2y_ValueChanged(object sender, EventArgs e)
        {
            if (prntForm.currentSelectedTrigger != null)
            {
                prntForm.currentSelectedTrigger.Event2TransPointY = (int)numE2y.Value;
            }
        }
        #endregion

        #region EVENT 3
        private void chkE3enabled_CheckedChanged(object sender, EventArgs e)
        {
            if (prntForm.currentSelectedTrigger != null)
            {
                prntForm.currentSelectedTrigger.EnabledEvent3 = chkE3enabled.Checked;
            }
        }
        private void chkE3once_CheckedChanged(object sender, EventArgs e)
        {
            if (prntForm.currentSelectedTrigger != null)
            {
                prntForm.currentSelectedTrigger.DoOnceOnlyEvent3 = chkE3once.Checked;
            }
        }

        private void e3GcTrue_CheckedChanged(object sender, EventArgs e)
        {
            if (prntForm.currentSelectedTrigger != null)
            {
                prntForm.currentSelectedTrigger.Event3RequiresFalseReturnCheck = e3GcTrue.Checked;
            }
        }

        private void cmbE3type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((prntForm.currentSelectedTrigger != null) && (cmbE3type.SelectedItem != null))
            {
                prntForm.currentSelectedTrigger.Event3Type = cmbE3type.SelectedItem.ToString();
                refreshCmbE3tag();
            }
        }
        private void cmbE3tag_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((prntForm.currentSelectedTrigger != null) && (cmbE3tag.SelectedItem != null))
            {
                if (userPressedE3tag)
                {
                    prntForm.currentSelectedTrigger.Event3FilenameOrTag = cmbE3tag.SelectedItem.ToString();
                    userPressedE3tag = false;
                    loadE3script();
                }
            }
        }
        private void cmbE3tag_MouseDown(object sender, MouseEventArgs e)
        {
            userPressedE3tag = true;
        }
        private void btnE3reset_Click(object sender, EventArgs e)
        {
            prntForm.currentSelectedTrigger.Event3Parm1 = "none";
            prntForm.currentSelectedTrigger.Event3Parm2 = "none";
            prntForm.currentSelectedTrigger.Event3Parm3 = "none";
            prntForm.currentSelectedTrigger.Event3Parm4 = "none";
            txtE3parm1.Text = "none";
            txtE3parm2.Text = "none";
            txtE3parm3.Text = "none";
            txtE3parm4.Text = "none";
            prntForm.currentSelectedTrigger.Event3TransPointX = 0;
            prntForm.currentSelectedTrigger.Event3TransPointY = 0;
            numE3x.Value = 0;
            numE3y.Value = 0;
        }
        private void txtE3parm1_TextChanged(object sender, EventArgs e)
        {
            if (prntForm.currentSelectedTrigger != null)
            {
                prntForm.currentSelectedTrigger.Event3Parm1 = txtE3parm1.Text;
            }
        }
        private void txtE3parm2_TextChanged(object sender, EventArgs e)
        {
            if (prntForm.currentSelectedTrigger != null)
            {
                prntForm.currentSelectedTrigger.Event3Parm2 = txtE3parm2.Text;
            }
        }
        private void txtE3parm3_TextChanged(object sender, EventArgs e)
        {
            if (prntForm.currentSelectedTrigger != null)
            {
                prntForm.currentSelectedTrigger.Event3Parm3 = txtE3parm3.Text;
            }
        }
        private void txtE3parm4_TextChanged(object sender, EventArgs e)
        {
            if (prntForm.currentSelectedTrigger != null)
            {
                prntForm.currentSelectedTrigger.Event3Parm4 = txtE3parm4.Text;
            }
        }
        private void txtE3parm1_MouseDown(object sender, MouseEventArgs e)
        {
            loadE3script();
        }
        private void txtE3parm2_MouseDown(object sender, MouseEventArgs e)
        {
            loadE3script();
        }
        private void txtE3parm3_MouseDown(object sender, MouseEventArgs e)
        {
            loadE3script();
        }
        private void txtE3parm4_MouseDown(object sender, MouseEventArgs e)
        {
            loadE3script();
        }
        private void numE3x_ValueChanged(object sender, EventArgs e)
        {
            if (prntForm.currentSelectedTrigger != null)
            {
                prntForm.currentSelectedTrigger.Event3TransPointX = (int)numE3x.Value;
            }
        }
        private void numE3y_ValueChanged(object sender, EventArgs e)
        {
            if (prntForm.currentSelectedTrigger != null)
            {
                prntForm.currentSelectedTrigger.Event3TransPointY = (int)numE3y.Value;
            }
        }
        #endregion

        #endregion
        
        #region Methods
        public void refreshLists()
        {
            containersList.Clear();
            foreach (Container c in prntForm.containersList)
            {
                containersList.Add(c.containerTag);
            }
            
            convosList.Clear();
            convosList = new List<string>(prntForm.mod.moduleConvosList);

            logictreesList.Clear();
            logictreesList = new List<string>(prntForm.mod.moduleLogicTreesList);

            ibscriptsList.Clear();
            ibscriptsList = new List<string>(prntForm.mod.moduleIBScriptsList);

            encountersList.Clear();
            foreach (Encounter c in prntForm.encountersList)
            {
                encountersList.Add(c.encounterName);
            }

            scriptsList.Clear();
            scriptsList = new List<string>(prntForm.scriptList);

            areasList.Clear();
            areasList = new List<string>(prntForm.mod.moduleAreasList);
        }
        public void refreshTriggers()
        {
            if (prntForm.currentSelectedTrigger != null)
            {
                Trigger trg = prntForm.currentSelectedTrigger;
                chkTrigEnabled.Checked = trg.Enabled;
                chkTrigOnce.Checked = trg.DoOnceOnly;
                chkTrigActiveSearch.Checked = trg.RequiresActiveSearch;
                chkTrigConnectedDiscovery.Checked = trg.ConnectedDiscovery;
                txtTrigTag.Text = trg.TriggerTag;
                chkTriggeredByPc.Checked = trg.canBeTriggeredByPc;
                chkTriggeredByCreatures.Checked = trg.canBeTriggeredByCreature;
                numTriggerCallsAllowed.Value = trg.numberOfScriptCallsRemaining;
                chkEncounterTriggerOnEveryStep.Checked = trg.encounterTriggerOnEveryStep;

                chkE1enabled.Checked = trg.EnabledEvent1;
                chkE1once.Checked = trg.DoOnceOnlyEvent1;
                cmbE1type.SelectedItem = trg.Event1Type;
                cmbE1tag.SelectedItem = trg.Event1FilenameOrTag;
                txtE1parm1.Text = trg.Event1Parm1;
                txtE1parm2.Text = trg.Event1Parm2;
                txtE1parm3.Text = trg.Event1Parm3;
                txtE1parm4.Text = trg.Event1Parm4;
                numE1x.Value = trg.Event1TransPointX;
                numE1y.Value = trg.Event1TransPointY;

                chkE2enabled.Checked = trg.EnabledEvent2;
                e2GcTrue.Checked = trg.Event2RequiresTrueReturnCheck;
                e3GcTrue.Checked = trg.Event3RequiresFalseReturnCheck;
                chkE2once.Checked = trg.DoOnceOnlyEvent2;
                cmbE2type.SelectedItem = trg.Event2Type;
                cmbE2tag.SelectedItem = trg.Event2FilenameOrTag;
                txtE2parm1.Text = trg.Event2Parm1;
                txtE2parm2.Text = trg.Event2Parm2;
                txtE2parm3.Text = trg.Event2Parm3;
                txtE2parm4.Text = trg.Event2Parm4;
                numE2x.Value = trg.Event2TransPointX;
                numE2y.Value = trg.Event2TransPointY;

                chkE3enabled.Checked = trg.EnabledEvent3;
                chkE3once.Checked = trg.DoOnceOnlyEvent3;
                cmbE3type.SelectedItem = trg.Event3Type;
                cmbE3tag.SelectedItem = trg.Event3FilenameOrTag;
                txtE3parm1.Text = trg.Event3Parm1;
                txtE3parm2.Text = trg.Event3Parm2;
                txtE3parm3.Text = trg.Event3Parm3;
                txtE3parm4.Text = trg.Event3Parm4;
                numE3x.Value = trg.Event3TransPointX;
                numE3y.Value = trg.Event3TransPointY;
            }
        }
        public void refreshCmbE1tag()
        {
            refreshLists();
            if ((string)cmbE1type.SelectedItem == "none")
            {
                cmbE1tag.BeginUpdate();
                cmbE1tag.DataSource = null;
                cmbE1tag.EndUpdate();

                gbE1parms.Enabled = false;
                gbE1transition.Enabled = false;
            }
            else if ((string)cmbE1type.SelectedItem == "container")
            {
                cmbE1tag.BeginUpdate();
                cmbE1tag.DataSource = null;
                cmbE1tag.DataSource = containersList;
                cmbE1tag.DisplayMember = "ContainerTag";
                cmbE1tag.EndUpdate();

                gbE1parms.Enabled = false;
                gbE1transition.Enabled = false;
            }
            else if ((string)cmbE1type.SelectedItem == "conversation")
            {
                cmbE1tag.BeginUpdate();
                cmbE1tag.DataSource = null;
                cmbE1tag.DataSource = convosList;
                cmbE1tag.EndUpdate();

                gbE1parms.Enabled = false;
                gbE1transition.Enabled = false;
            }
            else if ((string)cmbE1type.SelectedItem == "logictree")
            {
                cmbE1tag.BeginUpdate();
                cmbE1tag.DataSource = null;
                cmbE1tag.DataSource = logictreesList;
                cmbE1tag.EndUpdate();

                gbE1parms.Enabled = true;
                gbE1transition.Enabled = false;
            }
            else if ((string)cmbE1type.SelectedItem == "ibscript")
            {
                cmbE1tag.BeginUpdate();
                cmbE1tag.DataSource = null;
                cmbE1tag.DataSource = ibscriptsList;
                cmbE1tag.EndUpdate();

                gbE1parms.Enabled = true;
                gbE1transition.Enabled = false;
            }
            else if ((string)cmbE1type.SelectedItem == "encounter")
            {
                cmbE1tag.BeginUpdate();
                cmbE1tag.DataSource = null;
                cmbE1tag.DataSource = encountersList;
                cmbE1tag.DisplayMember = "encounterName";
                cmbE1tag.EndUpdate();

                gbE1parms.Enabled = false;
                gbE1transition.Enabled = false;
            }
            else if ((string)cmbE1type.SelectedItem == "script")
            {
                cmbE1tag.BeginUpdate();
                cmbE1tag.DataSource = null;
                cmbE1tag.DataSource = scriptsList;
                cmbE1tag.EndUpdate();

                gbE1parms.Enabled = true;
                gbE1transition.Enabled = false;
            }
            else if ((string)cmbE1type.SelectedItem == "transition")
            {
                cmbE1tag.BeginUpdate();
                cmbE1tag.DataSource = null;
                cmbE1tag.DataSource = areasList;
                cmbE1tag.EndUpdate();

                gbE1parms.Enabled = false;
                gbE1transition.Enabled = true;
            }
            else
            {
                gbE1parms.Enabled = false;
                gbE1transition.Enabled = false;
            }
        }
        public void refreshCmbE2tag()
        {
            refreshLists();
            if ((string)cmbE2type.SelectedItem == "none")
            {
                cmbE2tag.BeginUpdate();
                cmbE2tag.DataSource = null;
                cmbE2tag.EndUpdate();

                gbE2parms.Enabled = false;
                gbE2transition.Enabled = false;
            }
            else if ((string)cmbE2type.SelectedItem == "container")
            {
                cmbE2tag.BeginUpdate();
                cmbE2tag.DataSource = null;
                cmbE2tag.DataSource = containersList;
                cmbE2tag.DisplayMember = "ContainerTag";
                cmbE2tag.EndUpdate();

                gbE2parms.Enabled = false;
                gbE2transition.Enabled = false;
            }
            else if ((string)cmbE2type.SelectedItem == "conversation")
            {
                cmbE2tag.BeginUpdate();
                cmbE2tag.DataSource = null;
                cmbE2tag.DataSource = convosList;
                cmbE2tag.EndUpdate();

                gbE2parms.Enabled = false;
                gbE2transition.Enabled = false;
            }
            else if ((string)cmbE2type.SelectedItem == "logictree")
            {
                cmbE2tag.BeginUpdate();
                cmbE2tag.DataSource = null;
                cmbE2tag.DataSource = logictreesList;
                cmbE2tag.EndUpdate();

                gbE2parms.Enabled = true;
                gbE2transition.Enabled = false;
            }
            else if ((string)cmbE2type.SelectedItem == "ibscript")
            {
                cmbE2tag.BeginUpdate();
                cmbE2tag.DataSource = null;
                cmbE2tag.DataSource = ibscriptsList;
                cmbE2tag.EndUpdate();

                gbE2parms.Enabled = true;
                gbE2transition.Enabled = false;
            }
            else if ((string)cmbE2type.SelectedItem == "encounter")
            {
                cmbE2tag.BeginUpdate();
                cmbE2tag.DataSource = null;
                cmbE2tag.DataSource = encountersList;
                cmbE2tag.DisplayMember = "encounterName";
                cmbE2tag.EndUpdate();

                gbE2parms.Enabled = false;
                gbE2transition.Enabled = false;
            }
            else if ((string)cmbE2type.SelectedItem == "script")
            {
                cmbE2tag.BeginUpdate();
                cmbE2tag.DataSource = null;
                cmbE2tag.DataSource = scriptsList;
                cmbE2tag.EndUpdate();

                gbE2parms.Enabled = true;
                gbE2transition.Enabled = false;
            }
            else if ((string)cmbE2type.SelectedItem == "transition")
            {
                cmbE2tag.BeginUpdate();
                cmbE2tag.DataSource = null;
                cmbE2tag.DataSource = areasList;
                cmbE2tag.EndUpdate();

                gbE2parms.Enabled = false;
                gbE2transition.Enabled = true;
            }
            else
            {
                gbE2parms.Enabled = false;
                gbE2transition.Enabled = false;
            }
        }
        public void refreshCmbE3tag()
        {
            refreshLists();
            if ((string)cmbE3type.SelectedItem == "none")
            {
                cmbE3tag.BeginUpdate();
                cmbE3tag.DataSource = null;
                cmbE3tag.EndUpdate();

                gbE3parms.Enabled = false;
                gbE3transition.Enabled = false;
            }
            else if ((string)cmbE3type.SelectedItem == "container")
            {
                cmbE3tag.BeginUpdate();
                cmbE3tag.DataSource = null;
                cmbE3tag.DataSource = containersList;
                cmbE3tag.DisplayMember = "ContainerTag";
                cmbE3tag.EndUpdate();

                gbE3parms.Enabled = false;
                gbE3transition.Enabled = false;
            }
            else if ((string)cmbE3type.SelectedItem == "conversation")
            {
                cmbE3tag.BeginUpdate();
                cmbE3tag.DataSource = null;
                cmbE3tag.DataSource = convosList;
                cmbE3tag.EndUpdate();

                gbE3parms.Enabled = false;
                gbE3transition.Enabled = false;
            }
            else if ((string)cmbE3type.SelectedItem == "logictree")
            {
                cmbE3tag.BeginUpdate();
                cmbE3tag.DataSource = null;
                cmbE3tag.DataSource = logictreesList;
                cmbE3tag.EndUpdate();

                gbE3parms.Enabled = true;
                gbE3transition.Enabled = false;
            }
            else if ((string)cmbE3type.SelectedItem == "ibscript")
            {
                cmbE3tag.BeginUpdate();
                cmbE3tag.DataSource = null;
                cmbE3tag.DataSource = ibscriptsList;
                cmbE3tag.EndUpdate();

                gbE3parms.Enabled = true;
                gbE3transition.Enabled = false;
            }
            else if ((string)cmbE3type.SelectedItem == "encounter")
            {
                cmbE3tag.BeginUpdate();
                cmbE3tag.DataSource = null;
                cmbE3tag.DataSource = encountersList;
                cmbE3tag.DisplayMember = "encounterName";
                cmbE3tag.EndUpdate();

                gbE3parms.Enabled = false;
                gbE3transition.Enabled = false;
            }
            else if ((string)cmbE3type.SelectedItem == "script")
            {
                cmbE3tag.BeginUpdate();
                cmbE3tag.DataSource = null;
                cmbE3tag.DataSource = scriptsList;
                cmbE3tag.EndUpdate();

                gbE3parms.Enabled = true;
                gbE3transition.Enabled = false;
            }
            else if ((string)cmbE3type.SelectedItem == "transition")
            {
                cmbE3tag.BeginUpdate();
                cmbE3tag.DataSource = null;
                cmbE3tag.DataSource = areasList;
                cmbE3tag.EndUpdate();

                gbE3parms.Enabled = false;
                gbE3transition.Enabled = true;
            }
            else
            {
                gbE3parms.Enabled = false;
                gbE3transition.Enabled = false;
            }
        }
        public void loadE1script()
        {
            if ((prntForm.currentSelectedTrigger != null) && (cmbE1tag.SelectedItem != null) && (cmbE1type.SelectedItem != null))
            {
                if ((string)cmbE1type.SelectedItem == "script")
                {
                    loadScriptText(cmbE1tag.SelectedItem.ToString());
                }                               
            }
        }
        public void loadE2script()
        {
            if ((prntForm.currentSelectedTrigger != null) && (cmbE2tag.SelectedItem != null) && (cmbE2type.SelectedItem != null))
            {
                if ((string)cmbE2type.SelectedItem == "script")
                {
                    loadScriptText(cmbE2tag.SelectedItem.ToString());
                }
            }
        }
        public void loadE3script()
        {
            if ((prntForm.currentSelectedTrigger != null) && (cmbE3tag.SelectedItem != null) && (cmbE3type.SelectedItem != null))
            {
                if ((string)cmbE3type.SelectedItem == "script")
                {
                    loadScriptText(cmbE3tag.SelectedItem.ToString());
                }
            }
        }
        private void loadScriptText(string scriptFilename)
        {
            //load script into rtxt for browsing
            string jobDir = "";
            if (prntForm.mod.moduleName != "NewModule")
            {
                if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\scripts\\" + scriptFilename))
                {
                    jobDir = prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\scripts";
                }
                else if (File.Exists(prntForm._mainDirectory + "\\default\\NewModule\\scripts\\" + scriptFilename))
                {
                    jobDir = prntForm._mainDirectory + "\\default\\NewModule\\scripts";
                }
                else
                {
                    prntForm.logText("couldn't find the script file." + Environment.NewLine);
                    //prntForm.game.errorLog("couldn't find the script file.");
                }
            }
            else
            {
                //jobDir = prntForm._mainDirectory + "\\data\\scripts";
            }
            try
            {
                rtxtScript.LoadFile(jobDir + "\\" + scriptFilename, RichTextBoxStreamType.PlainText);
            }
            catch (Exception ex)
            {
                prntForm.logText("Failed to preview script of the selected row." + Environment.NewLine);
                //prntForm.game.errorLog("failed to preview script of selected row: " + ex.ToString());
            }
        }
        #endregion
    }
}
