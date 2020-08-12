using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.ViewInfo;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using DevExpress.XtraTreeList;
using System.ComponentModel.Design;

namespace Common.Controls.Emr
{
    /// <summary>
    /// 工具箱服务：使用XtraTreeList
    /// </summary>
    public class ToolboxServiceXtraTree : DevExpress.XtraEditors.PanelControl, IToolboxService // DevExpress.XtraTreeList.TreeList, IToolboxService
    {
        #region IToolboxService 成员

        public void AddCreator(ToolboxItemCreatorCallback creator, string format, System.ComponentModel.Design.IDesignerHost host)
        {

        }

        public void AddCreator(ToolboxItemCreatorCallback creator, string format)
        {

        }

        public void AddLinkedToolboxItem(ToolboxItem toolboxItem, string category, System.ComponentModel.Design.IDesignerHost host)
        {

        }

        public void AddLinkedToolboxItem(ToolboxItem toolboxItem, System.ComponentModel.Design.IDesignerHost host)
        {

        }

        public void AddToolboxItem(ToolboxItem toolboxItem, string category)
        {

        }     

        public void AddToolboxItem(ToolboxItem toolboxItem)
        {
            
        }

        public CategoryNameCollection CategoryNames
        {
            get
            {
                return null;
            }
        }

        public ToolboxItem DeserializeToolboxItem(object serializedObject, System.ComponentModel.Design.IDesignerHost host)
        {
            return null;
        }

        public ToolboxItem DeserializeToolboxItem(object serializedObject)
        {
            return null;
        }

        public ToolboxItem GetSelectedToolboxItem(System.ComponentModel.Design.IDesignerHost host)
        {
            return GetSelectedToolboxItem();
        }

        public ToolboxItem GetSelectedToolboxItem()
        {
            return CpObject.ToolboxItem;
        }

        public ToolboxItemCollection GetToolboxItems(string category, System.ComponentModel.Design.IDesignerHost host)
        {
            return GetToolboxItems();
        }

        public ToolboxItemCollection GetToolboxItems(string category)
        {
            return GetToolboxItems();
        }

        public ToolboxItemCollection GetToolboxItems(System.ComponentModel.Design.IDesignerHost host)
        {
            return GetToolboxItems();
        }

        public ToolboxItemCollection GetToolboxItems()
        {
            List<ToolboxItem> list = new List<ToolboxItem>();
            //foreach (RowInfo ri in this.ViewInfo.RowsInfo.Rows)
            //{
            //    if (ri.Node.Tag != null)
            //    {
            //        list.Add(ri.Node.Tag as ToolboxItem);
            //    }
            //}

            return new ToolboxItemCollection(list.ToArray());
        }

        public bool IsSupported(object serializedObject, System.Collections.ICollection filterAttributes)
        {
            return true;
        }

        public bool IsSupported(object serializedObject, System.ComponentModel.Design.IDesignerHost host)
        {
            return true;
        }

        public bool IsToolboxItem(object serializedObject, System.ComponentModel.Design.IDesignerHost host)
        {
            return true;
        }

        public bool IsToolboxItem(object serializedObject)
        {
            return true;
        }

        public void RemoveCreator(string format, System.ComponentModel.Design.IDesignerHost host)
        {

        }

        public void RemoveCreator(string format)
        {

        }

        public void RemoveToolboxItem(ToolboxItem toolboxItem, string category)
        {
            RemoveToolboxItem(toolboxItem);
        }

        public void RemoveToolboxItem(ToolboxItem toolboxItem)
        {
            if (CpObject.ToolboxItem != null)
            {
                CpObject.ToolboxItem = null;
            }
        }

        public string SelectedCategory
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        public void SelectedToolboxItemUsed()
        {
            CpObject.ToolboxItem = null;
        }

        public object SerializeToolboxItem(ToolboxItem toolboxItem)
        {
            return null;
        }

        public bool SetCursor()
        {
            if (CpObject.ToolboxItem == null)
            {
                designPanel.Cursor = Cursors.Default;
            }
            else
            {
                designPanel.Cursor = Cursors.Cross;
            }
            return true;
        }

        public void SetSelectedToolboxItem(ToolboxItem toolboxItem)
        {

        }

        public void AddControl(ToolboxItem tbi)
        {
            IToolboxUser tbu = host.GetDesigner(host.RootComponent) as IToolboxUser;
            tbu.ToolPicked(tbi);
        }

        #endregion


        public Control designPanel = null;
        public IDesignerHost host;

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

    }
}
