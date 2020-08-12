using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.Entity;
using Common.Utils;

namespace Common.Controls
{
    public partial class frmPathNodes : Form
    {
        private class EntityNode
        {
            public string NodeName { get; set; }
            public string NodeDesc { get; set; }
        }

        public string NodeName { get; set; }

        private List<EntityNode> Nodes { get; set; }

        public frmPathNodes(List<EntityCPNode> _Nodes)
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;
            if (!DesignMode)
            {
                EntityNode vo = new EntityNode();
                Nodes = new List<EntityNode>();
                foreach (EntityCPNode item in _Nodes)
                {
                    vo = new EntityNode();
                    vo.NodeName = item.NodeName;
                    vo.NodeDesc = "第" + item.NodeDays + "天" + item.NodeDesc;
                    Nodes.Add(vo);
                }
            }
        }

        private void frmPathNodes_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;
            if (Nodes != null)
            {
                this.gcItem.DataSource = Nodes;
            }
        }

        private void frmPathNodes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void gvItem_DoubleClick(object sender, EventArgs e)
        {
            int rowHandle = gvItem.FocusedRowHandle;
            if (rowHandle < 0) return;
            if (gvItem.FocusedRowHandle >= 0)
            {
                NodeName = gvItem.GetRowCellValue(gvItem.FocusedRowHandle, "NodeName").ToString();
                if (!string.IsNullOrEmpty(NodeName))
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
        }


    }
}
