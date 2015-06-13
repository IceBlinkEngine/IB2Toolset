using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms.Design;

namespace IBBToolset
{
    public class EventObjectSelectEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService wfes = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;

            if (wfes != null)
            {
                EventObjectSelect _eventObjSelect = new EventObjectSelect((EventObjEditorReturnObject)value, wfes);
                _eventObjSelect._wfes = wfes;
                wfes.DropDownControl(_eventObjSelect);
                value = _eventObjSelect.returnObject;
            }
            return value;
        }
    }
}
