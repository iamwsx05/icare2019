using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Drawing;
using System.ComponentModel;
using System.Collections;

namespace Common.Controls.Emr
{
    public class DesignPanel : UserControl, IDesignerPanel
    {
        public IDesignerHost designerHost;      

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            if (drgevent.Effect == DragDropEffects.Copy)
            {
                (designerHost.RootComponent as Control).SuspendLayout();

                IToolboxService tbsvc = designerHost.GetService(typeof(IToolboxService)) as IToolboxService;
                IToolboxUser tbu = designerHost.GetDesigner(designerHost.RootComponent) as IToolboxUser;
                ISelectionService selsvc = designerHost.GetService(typeof(ISelectionService)) as ISelectionService;

                DesignerTransaction transaction = designerHost.CreateTransaction("DoDragDrop");

                //避免当前有选中控件,会把新创建的控件放到选中控件中
                selsvc.SetSelectedComponents(null);

                ToolboxItem item = drgevent.Data.GetData(typeof(ToolboxItem)) as ToolboxItem;
                tbu.ToolPicked(item);
                ICollection components = item.CreateComponents();
                tbsvc.SelectedToolboxItemUsed();

                Control createdControl = selsvc.PrimarySelection as Control;
                Point location1 = PointToScreen(this.Location);
                createdControl.Location = new Point(drgevent.X - location1.X + this.Left, drgevent.Y - location1.Y + this.Top);
                selsvc.SetSelectedComponents(null);
                selsvc.SetSelectedComponents(new Control[] { createdControl });
                
                transaction.Commit();
                ((IDisposable)transaction).Dispose();

                (designerHost.RootComponent as Control).ResumeLayout();
            }
            else
            {
                base.OnDragDrop(drgevent);
            }
        }

        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            drgevent.Effect = DragDropEffects.Copy;
        }

        protected override void OnDragLeave(EventArgs e)
        {
            base.OnDragLeave(e);
        }
    }
}
