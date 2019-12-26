using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
using PinkieControls;
using System.Drawing;
using ZedGraph;
using System.Xml;
using System.IO;
using ExpressionEvaluation;

namespace com.digitalwave.iCare.gui.LIS.QC.Control
{
    public class ctlCheckItemTree : TreeView
    {
        // Fields
        private IContainer components;
        private ImageList imageList1;

        //private ImageList m_ilTreeNode;
        // private clsDcl_QCLisControl m_objDomain;
        private string m_strDeviceID;

        // Methods
        public ctlCheckItemTree()
        {
            this.components = null; 
            this.InitializeComponent();
             this.ImageList = this.imageList1;
            // this.m_objDomain = new clsDcl_QCLisControl(); 
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctlCheckItemTree));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "CLOUD.ICO");
            this.imageList1.Images.SetKeyName(1, "EARTH.ICO");
            this.imageList1.Images.SetKeyName(2, "LITENING.ICO");
            // 
            // ctlCheckItemTree
            // 
            this.Name = "ctlCheckItemTree1";
            this.Size = new System.Drawing.Size(120, 21);
            this.ResumeLayout(false);

        }

        internal void m_mthLoadData()
        {
            clsLISUserGroupNode node;
            TreeNode node2;
            TreeNode node3;
            clsLISCheckItemNode[] nodeArray;
            clsLISCheckItemNode node4;
            TreeNode node5;
            bool flag;
            IEnumerator enumerator;
            IDisposable disposable;
            clsLISCheckItemNode[] nodeArray2;
            int num;
            if (!string.IsNullOrEmpty(this.m_strDeviceID) )
            {
                goto Label_00A3;
            }
            node = null;
            (new weCare.Proxy.ProxyLis02()).Service.m_lngGetCheckItemTree(out node);
            if (node == null) 
            {
                goto Label_009D;
            }
            node2 = new TreeNode();
            this.m_mthMakeGroupNode(node, node2);
            enumerator = node2.Nodes.GetEnumerator();
        Label_0051:
            try
            {
                goto Label_006F;
            Label_0053:
                node3 = (TreeNode)enumerator.Current;
                base.Nodes.Add(node3);
            Label_006F:
                if (enumerator.MoveNext() )
                {
                    goto Label_0053;
                }
                goto Label_009B;
            }
            finally
            {
            Label_007E:
                disposable = enumerator as IDisposable;
                if ((disposable == null)  )
                {
                    goto Label_009A;
                }
                disposable.Dispose();
            Label_009A:;
            }
        Label_009B:;
        Label_009D:
            goto Label_0130;
        Label_00A3:
            nodeArray = null;
            (new weCare.Proxy.ProxyLis02()).Service.m_lngGetDeviceQCCheckItemByID(this.m_strDeviceID, out nodeArray);
            if (nodeArray != null)  
            {
                goto Label_0130;
            }
            nodeArray2 = nodeArray;
            num = 0;
            goto Label_0121;
        Label_00D2:
            node4 = nodeArray2[num];
            node5 = new TreeNode();
            node5.Text = node4.strName;
            node5.ImageIndex = 2;
            node5.SelectedImageIndex = 2;
            node5.Tag = node4;
            base.Nodes.Add(node5);
            num += 1;
        Label_0121:
            if ((num < ((int)nodeArray2.Length)) )
            {
                goto Label_00D2;
            }
        Label_0130:
            return;
        }

        private  void m_mthMakeGroupNode(clsLISUserGroupNode parent, TreeNode parentNode)
        {
            if (parent == null || parentNode == null)
            {
                return;
            }
            if (parent.objChildNodes != null)
            {
                foreach (clsLISUserGroupNode groupnode in parent.objChildNodes)
                {
                    TreeNode node = new TreeNode();
                    node.Text = groupnode.strName;
                    node.Tag = groupnode;
                    node.ImageIndex = 0;
                    node.SelectedImageIndex = 0;
                    parentNode.Nodes.Add(node);
                    m_mthMakeGroupNode(groupnode, node);
                }
            }
            if (parent.objUnitNodes != null)
            {
                foreach (clsLISApplyUnitNode unitNode in parent.objUnitNodes)
                {
                    TreeNode node = new TreeNode();
                    node.Text = unitNode.Name;
                    node.ImageIndex = 1;
                    node.SelectedImageIndex = 1;
                    node.Tag = unitNode;
                    if (unitNode.objItems != null)
                    {
                        foreach (clsLISCheckItemNode itemNode in unitNode.objItems)
                        {
                            TreeNode itemTreeNode = new TreeNode();
                            itemTreeNode.Text = itemNode.strName;
                            itemTreeNode.ImageIndex = 2;
                            itemTreeNode.SelectedImageIndex = 2;
                            itemTreeNode.Tag = itemNode;
                            node.Nodes.Add(itemTreeNode);
                        }
                    }
                    parentNode.Nodes.Add(node);
                }
            }
        }

        // Properties
        public string StrDeviceID
        {
            get
            { 
                return this.m_strDeviceID; 
            }
            set
            {
                this.m_strDeviceID = value; 
            }
        }
    }
}
