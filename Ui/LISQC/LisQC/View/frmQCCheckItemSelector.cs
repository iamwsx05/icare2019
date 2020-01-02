using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;
using weCare.Core.Entity;
using System.ComponentModel;
using System.Windows.Forms;
using com.digitalwave.GUI_Base;
using PinkieControls;

namespace com.digitalwave.iCare.gui.LIS
{
    public class frmQCCheckItemSelector : frmMDI_Child_Base
    {
        // Fields
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewCheckBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private IContainer components;
        private DataTable dtQCResult;
        private ButtonXP m_btnCancel;
        private ButtonXP m_btnSelectAll;
        private ButtonXP m_btnSelectNone;
        private ButtonXP m_btnSure;
        internal DataGridView m_dgQCCheckItem;
        private clsQCBatch m_objContoller;
        internal string m_strDeviceId;
        internal string[] m_strQCCheckItemID;
        internal string[] m_strQCCheckItemName;
        private Panel panel1;

        // Methods
        public frmQCCheckItemSelector()
        {
            this.m_objContoller = new clsQCBatch();
            this.dtQCResult = null;
            this.m_strQCCheckItemName = null;
            this.m_strQCCheckItemID = null;
            this.components = null;
            this.InitializeComponent();
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    this.Dispose(disposing);
        //}

        private void frmQCCheckItemSelector_Load(object sender, EventArgs e)
        {
            this.m_dgQCCheckItem.AutoGenerateColumns = false;
            this.m_mthQueryQCCheckItem();
            return;
        }

        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_dgQCCheckItem = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.m_btnSure = new PinkieControls.ButtonXP();
            this.m_btnCancel = new PinkieControls.ButtonXP();
            this.m_btnSelectAll = new PinkieControls.ButtonXP();
            this.m_btnSelectNone = new PinkieControls.ButtonXP();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dgQCCheckItem)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.m_dgQCCheckItem);
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(332, 345);
            this.panel1.TabIndex = 0;
            // 
            // m_dgQCCheckItem
            // 
            this.m_dgQCCheckItem.AllowUserToAddRows = false;
            this.m_dgQCCheckItem.AllowUserToDeleteRows = false;
            this.m_dgQCCheckItem.BackgroundColor = System.Drawing.SystemColors.Control;
            this.m_dgQCCheckItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.m_dgQCCheckItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column1,
            this.Column2});
            this.m_dgQCCheckItem.Location = new System.Drawing.Point(-2, -2);
            this.m_dgQCCheckItem.MultiSelect = false;
            this.m_dgQCCheckItem.Name = "m_dgQCCheckItem";
            this.m_dgQCCheckItem.RowHeadersVisible = false;
            this.m_dgQCCheckItem.RowTemplate.Height = 23;
            this.m_dgQCCheckItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.m_dgQCCheckItem.Size = new System.Drawing.Size(334, 345);
            this.m_dgQCCheckItem.TabIndex = 0;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "device_check_item_id_chr";
            this.Column3.HeaderText = "质控项目ID";
            this.Column3.Name = "Column3";
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "device_check_item_name_vchr";
            this.Column1.HeaderText = "质控项目";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "xuanze";
            this.Column2.HeaderText = "选择";
            this.Column2.Name = "Column2";
            // 
            // m_btnSure
            // 
            this.m_btnSure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnSure.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_btnSure.DefaultScheme = true;
            this.m_btnSure.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_btnSure.Hint = "";
            this.m_btnSure.Location = new System.Drawing.Point(12, 361);
            this.m_btnSure.Name = "m_btnSure";
            this.m_btnSure.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_btnSure.Size = new System.Drawing.Size(58, 33);
            this.m_btnSure.TabIndex = 6;
            this.m_btnSure.Text = "确定";
            this.m_btnSure.Click += new System.EventHandler(this.m_btnSure_Click);
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_btnCancel.DefaultScheme = true;
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Hint = "";
            this.m_btnCancel.Location = new System.Drawing.Point(80, 361);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_btnCancel.Size = new System.Drawing.Size(58, 33);
            this.m_btnCancel.TabIndex = 7;
            this.m_btnCancel.Text = "取消";
            // 
            // m_btnSelectAll
            // 
            this.m_btnSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnSelectAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_btnSelectAll.DefaultScheme = true;
            this.m_btnSelectAll.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_btnSelectAll.Hint = "";
            this.m_btnSelectAll.Location = new System.Drawing.Point(148, 361);
            this.m_btnSelectAll.Name = "m_btnSelectAll";
            this.m_btnSelectAll.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_btnSelectAll.Size = new System.Drawing.Size(58, 33);
            this.m_btnSelectAll.TabIndex = 8;
            this.m_btnSelectAll.Text = "全选";
            this.m_btnSelectAll.Click += new System.EventHandler(this.m_btnSelectAll_Click);
            // 
            // m_btnSelectNone
            // 
            this.m_btnSelectNone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnSelectNone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_btnSelectNone.DefaultScheme = true;
            this.m_btnSelectNone.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_btnSelectNone.Hint = "";
            this.m_btnSelectNone.Location = new System.Drawing.Point(216, 361);
            this.m_btnSelectNone.Name = "m_btnSelectNone";
            this.m_btnSelectNone.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_btnSelectNone.Size = new System.Drawing.Size(58, 33);
            this.m_btnSelectNone.TabIndex = 9;
            this.m_btnSelectNone.Text = "全清";
            this.m_btnSelectNone.Click += new System.EventHandler(this.m_btnSelectNone_Click);
            // 
            // frmQCCheckItemSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 428);
            this.Controls.Add(this.m_btnSelectNone);
            this.Controls.Add(this.m_btnSelectAll);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.m_btnSure);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 10.5F);
            this.Name = "frmQCCheckItemSelector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "质控项目选择";
            this.Load += new System.EventHandler(this.frmQCCheckItemSelector_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_dgQCCheckItem)).EndInit();
            this.ResumeLayout(false);

        }

        private void m_btnSelectAll_Click(object sender, EventArgs e)
        {
            int num;
            bool flag;
            if (this.m_dgQCCheckItem.Rows.Count > 0)
            {
                goto Label_001B;
            }
            goto Label_0064;
        Label_001B:
            num = 0;
            goto Label_004D;
        Label_001F:
            this.m_dgQCCheckItem.Rows[num].Cells[2].Value = true;
            num += 1;
        Label_004D:
            if (num < this.m_dgQCCheckItem.Rows.Count)
            {
                goto Label_001F;
            }
        Label_0064:
            return;
        }

        private void m_btnSelectNone_Click(object sender, EventArgs e)
        {
            int num;
            bool flag;
            if (this.m_dgQCCheckItem.Rows.Count > 0)
            {
                goto Label_001B;
            }
            goto Label_0064;
        Label_001B:
            num = 0;
            goto Label_004D;
        Label_001F:
            this.m_dgQCCheckItem.Rows[num].Cells[2].Value = false;
            num += 1;
        Label_004D:
            if (num < this.m_dgQCCheckItem.Rows.Count)
            {
                goto Label_001F;
            }
        Label_0064:
            return;
        }

        private void m_btnSure_Click(object sender, EventArgs e)
        {
            int num;
            int num2;
            int num3;
            bool flag;
            num = 0;
            if (this.m_dgQCCheckItem.Rows.Count > 0)
            {
                goto Label_0020;
            }
            goto Label_014E;
        Label_0020:
            num2 = 0;
            goto Label_005D;
        Label_0024:
            if (((bool)this.m_dgQCCheckItem.Rows[num2].Cells[2].Value) == false)
            {
                goto Label_0058;
            }
            num += 1;
        Label_0058:
            num2 += 1;
        Label_005D:
            if (num2 < this.m_dgQCCheckItem.Rows.Count)
            {
                goto Label_0024;
            }
            this.m_strQCCheckItemID = new string[num];
            this.m_strQCCheckItemName = new string[num];
            num3 = 0;
            num2 = 0;
            goto Label_0134;
        Label_0095:
            if (((bool)this.m_dgQCCheckItem.Rows[num2].Cells[2].Value) == false)
            {
                goto Label_012F;
            }
            this.m_strQCCheckItemID[num3] = this.m_dgQCCheckItem.Rows[num2].Cells[0].Value.ToString().Trim();
            this.m_strQCCheckItemName[num3] = this.m_dgQCCheckItem.Rows[num2].Cells[1].Value.ToString().Trim();
            num3 += 1;
        Label_012F:
            num2 += 1;
        Label_0134:
            if (num2 < this.m_dgQCCheckItem.Rows.Count)
            {
                goto Label_0095;
            }
        Label_014E:
            return;
        }

        private void m_mthQueryQCCheckItem()
        {
            (new weCare.Proxy.ProxyLis03()).Service.m_lngQCCheckItem(this.m_strDeviceId, out this.dtQCResult);
            if (this.dtQCResult != null && this.dtQCResult.Rows.Count > 0)
            {
                this.m_dgQCCheckItem.DataSource = this.dtQCResult;
                this.m_btnSelectNone_Click(null, null);
            }
        }
    }
}
