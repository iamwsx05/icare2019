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
    public class frmCheckItemSelector : Form
    {
        // Fields
        private IContainer components;
        private clsLISCheckItemNode item;
        private ButtonXP m_cmdCancel;
        private ButtonXP m_cmdConfirm;
        private Panel m_pnlBottom;
        private string m_strDeviceID;
        private ctlCheckItemTree m_trvCheckItem ;

        // Methods
        public frmCheckItemSelector()
        {
            this.components = null; 
            this.InitializeComponent(); 
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private void frmCheckItemSelector_Load(object sender, EventArgs e)
        {
            this.m_trvCheckItem.StrDeviceID = this.m_strDeviceID;
            this.m_trvCheckItem.m_mthLoadData(); 
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.m_trvCheckItem = new ctlCheckItemTree();
            this.m_pnlBottom = new Panel();
            this.m_cmdConfirm = new ButtonXP();
            this.m_cmdCancel = new ButtonXP();
            this.m_pnlBottom.SuspendLayout();
            this.SuspendLayout();
            this.m_trvCheckItem.Dock = DockStyle.Fill;
            this.m_trvCheckItem.ImageIndex = 0;
            this.m_trvCheckItem.Location = new Point(0, 0);
            this.m_trvCheckItem.Name = "m_trvCheckItem";
            this.m_trvCheckItem.SelectedImageIndex = 0;
            this.m_trvCheckItem.Size = new Size(0x1fd, 400);
            this.m_trvCheckItem.TabIndex = 0;
            this.m_trvCheckItem.DoubleClick += new EventHandler(this.m_trvCheckItem_DoubleClick);
            this.m_pnlBottom.Controls.Add(this.m_cmdConfirm);
            this.m_pnlBottom.Controls.Add(this.m_cmdCancel);
            this.m_pnlBottom.Dock = DockStyle.Bottom;
            this.m_pnlBottom.Location = new Point(0, 400);
            this.m_pnlBottom.Name = "m_pnlBottom";
            this.m_pnlBottom.Size = new Size(0x1fd, 0x48);
            this.m_pnlBottom.TabIndex = 1;
            this.m_cmdConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdConfirm.BackColor = Color.FromArgb(0, 0xec, 0xe9, 0xd8);
            this.m_cmdConfirm.DefaultScheme = true;
            this.m_cmdConfirm.DialogResult = DialogResult.Cancel;
            this.m_cmdConfirm.Hint = "";
            this.m_cmdConfirm.Location = new Point(0x114, 20);
            this.m_cmdConfirm.Name = "m_cmdConfirm";
            this.m_cmdConfirm.Scheme = 0;
            this.m_cmdConfirm.Size = new Size(0x62, 0x21);
            this.m_cmdConfirm.TabIndex = 0x13;
            this.m_cmdConfirm.Text = "确定";
            this.m_cmdConfirm.Click += new EventHandler(this.m_cmdConfirm_Click);
            this.m_cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdCancel.BackColor = Color.FromArgb(0, 0xec, 0xe9, 0xd8);
            this.m_cmdCancel.DefaultScheme = true;
            this.m_cmdCancel.DialogResult = DialogResult.Cancel;
            this.m_cmdCancel.Hint = "";
            this.m_cmdCancel.Location = new Point(0x184, 20);
            this.m_cmdCancel.Name = "m_cmdCancel";
            this.m_cmdCancel.Scheme = 0;
            this.m_cmdCancel.Size = new Size(0x62, 0x21);
            this.m_cmdCancel.TabIndex = 20;
            this.m_cmdCancel.Text = "取消(ESC)";
            this.m_cmdCancel.Click += new EventHandler(this.m_cmdCancel_Click);
            this.AutoScaleDimensions = new SizeF(7f, 14f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(0x1fd, 0x1d8);
            this.Controls.Add(this.m_trvCheckItem);
            this.Controls.Add(this.m_pnlBottom);
            this.Font = new Font("宋体", 10.5f);
            this.KeyPreview = true;
            this.Name = "frmCheckItemSelector";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "项目选择";
            this.Load += new EventHandler(this.frmCheckItemSelector_Load);
            this.m_pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false); 
        }

        private void m_cmdCancel_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
            base.Close(); 
        }

        private void m_cmdConfirm_Click(object sender, EventArgs e)
        {
            if (!m_mthDoOK())
            {
                MessageBox.Show("请选定一个检验项目.", "iCare");
            }
        }

        private bool m_mthDoOK()
        {
            if (this.m_trvCheckItem.SelectedNode != null)
            {
                clsLISCheckItemNode node = (this.m_trvCheckItem.SelectedNode.Tag as clsLISCheckItemNode);
                if (node != null)
                {
                    this.item = node;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    return true;
                }
            }
            return false;
        }

        private void m_trvCheckItem_DoubleClick(object sender, EventArgs e)
        {
            this.m_mthDoOK(); 
        }

        // Properties
        public clsLISCheckItemNode SelectedCheckItem
        {
            get
            {
                return this.item;
            }
        }

        public string StrDeviceID
        {
            get
            { 
                return this.m_strDeviceID; 
            }
            set
            {
                this.m_strDeviceID = value;
                this.m_trvCheckItem.StrDeviceID = this.m_strDeviceID; 
            }
        }
    }
}
