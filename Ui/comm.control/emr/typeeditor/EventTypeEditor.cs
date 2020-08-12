using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace Common.Controls.Emr
{
    internal class EventTypeEditor : UITypeEditor
    {
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            string editField = context.PropertyDescriptor.DisplayName;

            string strValue = string.Empty;

            if (value != null)
            {
                strValue = value.ToString();
            }

            EventTypeUIEditor frmEditor = new EventTypeUIEditor(editField, strValue);

            IWindowsFormsEditorService editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (editorService != null)
            {
                if (editorService.ShowDialog(frmEditor) == System.Windows.Forms.DialogResult.OK)
                {
                    return frmEditor.eventdefine;
                }
            }
            return base.EditValue(context, provider, value);
        }

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return false;
        }
    }
}
