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
    public partial class RulesEditor : Form
    {
        private Module mod;
        private ParentForm prntForm;

        public string moveDiagCostInfo = "This defines the amount of movement points that are consumed for diagonal moves in combat";
        public string ArmorClassDisplayInfo = "Defines the way Armor Class is displayed. Ascending goes from 10 -> 30+ (think 3e) and Descending goes from 10 -> -10- (think 1e)";
        public string toHitBonusFromBehindInfo = "To hit bonus when attacking from behind";
        public string useLuckInfo = "Luck is an additional attribute that's heigh wehn the attribute scores of a char are low; can e.g. be checked by authors for special event or dialgoue situations";
        public string rollingSystemInfo = "The 3d6 system generates results between 3 and 18, by three times adding numbers from 1 to 6; the 6+d12 method generates results from 7 to 18 by adding 6 to a range of numbers from 1 - 12.";

        public RulesEditor(Module m, ParentForm pf)
        {
            InitializeComponent();
            mod = m;
            prntForm = pf;
            resetForm();
        }

        public void resetForm()
        {
            //Diagonal Move Cost
            if (mod.diagonalMoveCost == 1.0f)
            {
                rbtnOneSquare.Checked = true;
            }
            else
            {
                rbtnOnePointFiveSquares.Checked = true;
            }
            //Armor Class Diplay
            if (mod.ArmorClassAscending)
            {
                rbtnAscendingAC.Checked = true;
            }
            else
            {
                rbtnDescendingAC.Checked = true;
            }
            //to hit bonus from behind
            if (mod.attackFromBehindToHitModifier == 1)
            {
                rbtnPlusOneToHitFromBehind.Checked = true;
            }
            else if (mod.attackFromBehindToHitModifier == 2)
            {
                rbtnPlusTwoToHitFromBehind.Checked = true;
            }
            else if (mod.attackFromBehindToHitModifier == 3)
            {
                rbtnPlusThreeToHitFromBehind.Checked = true;
            }
            else if (mod.attackFromBehindToHitModifier == 4)
            {
                rbtnPlusFourToHitFromBehind.Checked = true;
            }

            //use Luck attribute
            if (mod.useLuck)
            {
                rbtnUseLuck.Checked = true;
            }
            else
            {
                rbtnDoNotUseLuck.Checked = true;
            }

            // decide for attribute rolling system
            if (mod.use3d6)
            {
                rbtnUse3d6.Checked = true;
            }
            else
            {
                rbtnUse6Plusd12.Checked = true;
            }
        }

        #region Diagonal Move Cost Stuff
        private void gbMoveDiagonalCost_MouseHover(object sender, EventArgs e)
        {
            rtxtInfo.Text = moveDiagCostInfo;
        }
        private void rbtnOneSquare_MouseHover(object sender, EventArgs e)
        {
            rtxtInfo.Text = moveDiagCostInfo;
        }
        private void rbtnOnePointFiveSquares_MouseHover(object sender, EventArgs e)
        {
            rtxtInfo.Text = moveDiagCostInfo;
        }
        private void rbtnOneSquare_CheckedChanged(object sender, EventArgs e)
        {
            mod.diagonalMoveCost = 1.0f;
        }
        private void rbtnOnePointFiveSquares_CheckedChanged(object sender, EventArgs e)
        {
            mod.diagonalMoveCost = 1.5f;
        }
        #endregion
        #region Armor Class Display
        private void gbArmorClassDisplay_MouseHover(object sender, EventArgs e)
        {
            rtxtInfo.Text = ArmorClassDisplayInfo;
        }
        private void rbtnAscendingAC_MouseHover(object sender, EventArgs e)
        {
            rtxtInfo.Text = ArmorClassDisplayInfo;
        }
        private void rbtnDescendingAC_MouseHover(object sender, EventArgs e)
        {
            rtxtInfo.Text = ArmorClassDisplayInfo;
        }
        private void rbtnAscendingAC_CheckedChanged(object sender, EventArgs e)
        {
            mod.ArmorClassAscending = true;
        }
        private void rbtnDescendingAC_CheckedChanged(object sender, EventArgs e)
        {
            mod.ArmorClassAscending = false;
        }
        #endregion
        #region To hit bonus from behind
        private void gbToHitBonusFromBehind_MouseHover(object sender, EventArgs e)
        {
            rtxtInfo.Text = toHitBonusFromBehindInfo;
        }
        private void rbtnPlusOneToHitFromBehind_MouseHover(object sender, EventArgs e)
        {
            rtxtInfo.Text = toHitBonusFromBehindInfo;
        }
        private void rbtnPlusTwoToHitFromBehind_MouseHover(object sender, EventArgs e)
        {
            rtxtInfo.Text = toHitBonusFromBehindInfo;
        }
        private void rbtnPlusThreeToHitFromBehind_MouseHover(object sender, EventArgs e)
        {
            rtxtInfo.Text = toHitBonusFromBehindInfo;
        }
        private void rbtnPlusFourToHitFromBehind_MouseHover(object sender, EventArgs e)
        {
            rtxtInfo.Text = toHitBonusFromBehindInfo;
        }

        private void rbtnPlusOneToHitFromBehind_CheckedChanged(object sender, EventArgs e)
        {
            mod.attackFromBehindToHitModifier = 1;
        }
        private void rbtnPlusTwoToHitFromBehind_CheckedChanged(object sender, EventArgs e)
        {
            mod.attackFromBehindToHitModifier = 2;
        }
        private void rbtnPlusThreeToHitFromBehind_CheckedChanged(object sender, EventArgs e)
        {
            mod.attackFromBehindToHitModifier = 3;
        }
        private void rbtnPlusFourToHitFromBehind_CheckedChanged(object sender, EventArgs e)
        {
            mod.attackFromBehindToHitModifier = 4;
        }

        #endregion
        #region use Luck
        private void gbUseLuck_MouseHover(object sender, EventArgs e)
        {
            rtxtInfo.Text = useLuckInfo;
        }
        private void rbtnUseLuck_MouseHover(object sender, EventArgs e)
        {
            rtxtInfo.Text = useLuckInfo;
        }
        private void rbtnDoNotUseLuck_MouseHover(object sender, EventArgs e)
        {
            rtxtInfo.Text = useLuckInfo;
        }
        private void rbtnUseLuck_CheckedChanged(object sender, EventArgs e)
        {
            mod.useLuck = true;
        }
        private void rbtnDoNotUseLuck_CheckedChanged(object sender, EventArgs e)
        {
            mod.useLuck = false;
        }

        #endregion

        #region Decide for attribute rolling system
        private void gbRollingSystem_MouseHover(object sender, EventArgs e)
        {
            rtxtInfo.Text = rollingSystemInfo;
        }
        private void rbtnUse3d6_MouseHover(object sender, EventArgs e)
        {
            rtxtInfo.Text = rollingSystemInfo;
        }
        private void rbtnUse6Plusd12_MouseHover(object sender, EventArgs e)
        {
            rtxtInfo.Text = rollingSystemInfo;
        }
        private void rbtnUse3d6_CheckedChanged(object sender, EventArgs e)
        {
            mod.use3d6 = true;
        }
        private void rbtnUse6Plusd12_CheckedChanged(object sender, EventArgs e)
        {
            mod.use3d6 = false;
        }
        #endregion

        private void splitContainer1_Panel1_MouseHover(object sender, EventArgs e)
        {
            rtxtInfo.Text = "";
        }
        private void rtxtInfo_MouseHover(object sender, EventArgs e)
        {
            rtxtInfo.Text = "";
        }
    }
}
