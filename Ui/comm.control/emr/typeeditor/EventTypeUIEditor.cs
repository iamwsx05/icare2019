using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using DevExpress.XtraTreeList.Nodes;

namespace Common.Controls.Emr
{
    public partial class EventTypeUIEditor : System.Windows.Forms.Form//frmBaseNormal
    {
        public string eventdefine { get; private set; }

        public EventTypeUIEditor(string propertyname, string eventstring)
        {
            InitializeComponent();

            eventdefine = eventstring;

            this.Text = "事件编辑器 " + propertyname;
        }

        private void EventUIEditor_Load(object sender, EventArgs e)
        {
            //List<Type> list = AddinFinder.Find(Application.StartupPath, typeof(IEventAddin));

            //foreach (Type t in list)
            //{
            //    string fileName = t.Assembly.ManifestModule.ScopeName;
            //    TreeListNode nodePar = this.treeList1.FindNodeByFieldValue("name", fileName);

            //    if (nodePar == null)
            //    {
            //        nodePar = this.treeList1.AppendNode(fileName, null);
            //        nodePar.SetValue("name", fileName);
            //    }

            //    TreeListNode child = this.treeList1.AppendNode(t.FullName, nodePar);
            //    child.SetValue("name", t.FullName);
            //    child.Tag = t;
            //    nodePar.ExpandAll();
            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.xtraTabControl1.SelectedTabPage.Name == "tabDLL")
            {
                eventdefine = string.Format("DLL{0}{1}", ConstValue.EVENT_STRING_SPLITER + this.lblDLL.Text, ConstValue.EVENT_STRING_SPLITER + this.lblClassName.Text);
            }

            this.DialogResult = DialogResult.OK;
        }

        private void treeList1_AfterFocusNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                this.lblDLL.Text = e.Node.ParentNode.GetValue("name").ToString();
                this.lblClassName.Text = e.Node.GetValue("name").ToString();
            }
        }
    }
}
