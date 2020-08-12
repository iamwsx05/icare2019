using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Forms.Design.Behavior;

namespace Common.Controls.Emr
{
    class DragDropBehavoir : Behavior
    {
        private IDesignerHost designerHost;
        System.Windows.Forms.Design.Behavior.BehaviorService bhsvc;

        public DragDropBehavoir(IDesignerHost host)
        {
            designerHost = host;
            bhsvc = designerHost.GetService(typeof(System.Windows.Forms.Design.Behavior.BehaviorService)) as System.Windows.Forms.Design.Behavior.BehaviorService;
        }

        public override void OnDragDrop(Glyph g, DragEventArgs e)
        {

            System.Windows.Forms.Design.Behavior.ControlBodyGlyph cbg = g as System.Windows.Forms.Design.Behavior.ControlBodyGlyph;
            IToolboxService tbsvc = designerHost.GetService(typeof(IToolboxService)) as IToolboxService;
            ISelectionService selsvc = designerHost.GetService(typeof(ISelectionService)) as ISelectionService;

            if (cbg != null && cbg.RelatedComponent != null && cbg.RelatedComponent is Control)
            {
                ToolboxItem item = e.Data.GetData(typeof(ToolboxItem)) as ToolboxItem;

                IToolboxUser tbu = designerHost.GetDesigner(designerHost.RootComponent) as IToolboxUser;

                (designerHost.RootComponent as Control).SuspendLayout();

                tbu.ToolPicked(item);

                if (selsvc.PrimarySelection != null && selsvc.PrimarySelection is Control)
                {
                    Control createdControl = selsvc.PrimarySelection as Control;

                    PropertyDescriptor parentProperty = TypeDescriptor.GetProperties(createdControl)["Parent"];
                    //获取粘贴到的父容器
                    ParentControlDesigner parentDesigner = null;
                    parentDesigner = designerHost.GetDesigner(cbg.RelatedComponent) as ParentControlDesigner;
                    if (parentDesigner == null)
                    {
                        parentDesigner = designerHost.GetDesigner(designerHost.RootComponent) as DocumentDesigner;
                    }

                    if (parentDesigner != null && parentDesigner.CanParent(createdControl))
                    {
                        parentProperty.SetValue(createdControl, parentDesigner.Control);

                        Point p1 = bhsvc.AdornerWindowToScreen();
                    }

                    tbsvc.SelectedToolboxItemUsed();


                    Point adroP = bhsvc.AdornerWindowToScreen();

                    createdControl.Left = e.X - adroP.X - (cbg.RelatedComponent as Control).Left;
                    createdControl.Top = e.Y - adroP.Y - (cbg.RelatedComponent as Control).Top;
                }

                (designerHost.RootComponent as Control).ResumeLayout();
            }

            bhsvc.PopBehavior(this);
        }

        public override void OnDragEnter(Glyph g, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        public override void OnDragOver(Glyph g, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }
    }
}
