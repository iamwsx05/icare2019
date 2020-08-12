using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using DevExpress.XtraEditors;

namespace Common.Controls.Emr
{
    public class StringArrayEditor : UITypeEditor
    {
        public StringArrayEditor()
        {

        }

        protected object GetInstance(ITypeDescriptorContext context)
        {
            BaseControlWarpper ctl = context.Instance as BaseControlWarpper;
            if (ctl.文本值 == null)
            {
                return "";
            }
            else
            {
                return ctl.文本值;
            }
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (context != null &&
                GetInstance(context) != null &&
                provider != null)
            {
                IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (edSvc != null)
                {
                    MemoEdit memoEdit = new MemoEdit();
                    memoEdit.Dock = DockStyle.Fill;
                    memoEdit.Text = GetInstance(context).ToString();

                    using (Form form = new Form())
                    {
                        form.Text = "编辑文本值";
                        form.Size = new System.Drawing.Size(400, 300);
                        form.ShowIcon = false;
                        form.MinimizeBox = false;
                        form.MaximizeBox = false;
                        //form.StartPosition = FormStartPosition.CenterParent;
                        form.StartPosition = FormStartPosition.Manual;
                        Point p = weCare.Core.Utils.Function.ScreenCursorPosition();
                        form.Location = new Point(p.X - form.Width, p.Y);
                        form.Controls.Add(memoEdit);
                        edSvc.ShowDialog(form);
                        return memoEdit.Text;
                    }
                }
            }
            return value;
        }
    }
}
