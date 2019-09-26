using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using IceBlinkCore;
using WeifenLuo.WinFormsUI.Docking;

namespace IB2Toolset
{
    public partial class IceBlinkProperties : DockContent
    {
        public ParentForm prntForm;

        public IceBlinkProperties(ParentForm pf)
        {
            InitializeComponent();
            prntForm = pf;
        }

        private void propertyGrid1_PropertyValueChanged_1(object s, PropertyValueChangedEventArgs e)
        {
            prntForm.frmBlueprints.UpdateTreeViewCreatures();
            prntForm.frmBlueprints.UpdateTreeViewItems();
            prntForm.frmBlueprints.UpdateTreeViewProps();
            //e.ChangedItem.PropertyDescriptor.Name
            //brightburn
           
                if (prntForm.mod.wp_selectedProp != null && prntForm.mod.wp_selectedProp.ImageFileName != "none")
                {
                    if (prntForm.mod.wp_selectedProp.WayPointList.Count > 1)
                    {

                        foreach (WayPoint wp in prntForm.mod.wp_selectedProp.WayPointList)
                        {

                            if (wp.departureTime == null)
                            {
                                return;
                            }
                            if (wp.departureTime.Contains("time") || wp.departureTime.Contains("Time") || wp.departureTime.Contains("TIME"))
                            {
                                return;
                            }
                        }

                        if (prntForm.mod.wp_selectedProp.MoverType == "daily" || prntForm.mod.wp_selectedProp.MoverType == "weekly" || prntForm.mod.wp_selectedProp.MoverType == "monthly" || prntForm.mod.wp_selectedProp.MoverType == "yearly")
                        {
                            List<WayPoint> newList = new List<WayPoint>();
                            for (int i = prntForm.mod.wp_selectedProp.WayPointList.Count - 1; i >= 0; i--)
                            {
                                List<string> timeUnitsList = new List<string>();
                                int currentTimeInInterval = 0;

                                //convert the string from the toolset for departure time into separate ints, filtering out ":" and blanks
                                //format in toolset is number:number:number
                                //with these ranges [0 to 336]:[0 to 23]:[0 to 59]
                                //actually it's 1 to 336 for intuitive feeling, but below code treats zero and 1 the same way
                                //think: 1 equals monday, 2 equals tuesday and so forth
                                timeUnitsList = prntForm.mod.wp_selectedProp.WayPointList[i].departureTime.Split(':').Select(x => x.Trim()).ToList();

                                int dayCounter = Convert.ToInt32(timeUnitsList[0]);
                                int hourCounter = Convert.ToInt32(timeUnitsList[1]);
                                int minuteCounter = Convert.ToInt32(timeUnitsList[2]);

                                //catch entries of zero
                                //counter is reduced by one to make below calculation work the same for day/minutes/hours
                                if ((dayCounter == 0) || (dayCounter == 1))
                                {
                                    dayCounter = 0;
                                }
                                else
                                {
                                    dayCounter = (dayCounter - 1);
                                }

                                //turn the the three counters into one number for departure time (in minutes)
                                int convertedDepartureTime = dayCounter * 1440 + hourCounter * 60 + minuteCounter;

                                bool wasInserted = false;
                                //foreach (WayPoint wp2 in newList)
                                for (int j = newList.Count - 1; j >= 0; j--)
                                {
                                    //departure time determine
                                    List<string> timeUnitsList2 = new List<string>();
                                    int currentTimeInInterval2 = 0;

                                    //convert the string from the toolset for departure time into separate ints, filtering out ":" and blanks
                                    //format in toolset is number:number:number
                                    //with these ranges [0 to 336]:[0 to 23]:[0 to 59]
                                    //actually it's 1 to 336 for intuitive feeling, but below code treats zero and 1 the same way
                                    //think: 1 equals monday, 2 equals tuesday and so forth
                                    timeUnitsList2 = newList[j].departureTime.Split(':').Select(x => x.Trim()).ToList();

                                    int dayCounter2 = Convert.ToInt32(timeUnitsList2[0]);
                                    int hourCounter2 = Convert.ToInt32(timeUnitsList2[1]);
                                    int minuteCounter2 = Convert.ToInt32(timeUnitsList2[2]);

                                    //catch entries of zero
                                    //counter is reduced by one to make below calculation work the same for day/minutes/hours
                                    if ((dayCounter2 == 0) || (dayCounter2 == 1))
                                    {
                                        dayCounter2 = 0;
                                    }
                                    else
                                    {
                                        dayCounter2 = (dayCounter2 - 1);
                                    }

                                    //turn the the three counters into one number for departure time (in minutes)
                                    int convertedDepartureTime2 = dayCounter2 * 1440 + hourCounter2 * 60 + minuteCounter2;

                                    //if waypoint org earlier/smaller, nothing
                                    //if waypoint org later/higher, insert at+1
                                    if (convertedDepartureTime > convertedDepartureTime2)
                                    {
                                        newList.Insert(j + 1, prntForm.mod.wp_selectedProp.WayPointList[i].DeepCopy());
                                        wasInserted = true;
                                        break;
                                    }
                                    //when through whole lsot and never larger, place at zero
                                }

                                if (!wasInserted)
                                {
                                    newList.Insert(0, prntForm.mod.wp_selectedProp.WayPointList[i].DeepCopy());
                                }

                            }

                            newList[0].departureTime = "1:0:1";
                            newList[newList.Count - 1].X = newList[0].X;
                            newList[newList.Count - 1].Y = newList[0].Y;
                            //int relevantTimePerStep = 0;
                       
                            if (prntForm.mod.wp_selectedProp.MoverType == "daily")
                            {
                                newList[newList.Count - 1].departureTime = "1:23:59";
                            }
                            if (prntForm.mod.wp_selectedProp.MoverType == "weekly")
                            {
                                newList[newList.Count - 1].departureTime = "7:23:59";
                            }
                            if (prntForm.mod.wp_selectedProp.MoverType == "monthly")
                            {
                                newList[newList.Count - 1].departureTime = "28:23:59";
                            }
                            if (prntForm.mod.wp_selectedProp.MoverType == "yearly")
                            {
                                newList[newList.Count - 1].departureTime = "336:23:59";
                            }

                            prntForm.mod.wp_selectedProp.WayPointList.Clear();
                            foreach (WayPoint wp in newList)
                            {
                                prntForm.mod.wp_selectedProp.WayPointList.Add(wp.DeepCopy());
                            }

                        try
                        {
                            if (prntForm.mod.wp_selectedProp.WayPointList.Count > prntForm.mod.currentlySelectedWayPointIndex)
                            {
                                prntForm.frmIceBlinkProperties.propertyGrid1.SelectedObject = prntForm.mod.wp_selectedProp.WayPointList[prntForm.mod.currentlySelectedWayPointIndex];
                            }
                        }
                        catch
                        { }

                    }

                }
                }





        }
    }
}
