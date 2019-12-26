using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using com.digitalwave.GUI_Base;			//申请单用到
using com.digitalwave.iCare.BIHOrder.Control;
using com.digitalwave.iCare.common;
using com.digitalwave.iCare.gui.HIS;	//申请单用到
using com.digitalwave.iCare.gui.LIS;
using weCare.Core.Entity;
using iCare;
using iCare.CustomForm;					//申请单用到 
using com.digitalwave.iCare.BIHOrder;
using com.digitalwave.Emr.Signature_gui;

namespace iCare.Anaesthesia.Requisition
{
    public class frmNoticePrint : frmMDI_Child_Base
    {
        // Fields
        private IContainer components;
        public DataTable dtRelust;
        private GroupBox groupBox1;
        public bool IsPrintLandscape;
        internal PinkieControls.ButtonXP m_cmdExit;
        internal PinkieControls.ButtonXP m_cmdPrint;
        internal PinkieControls.ButtonXP m_cmdPrintPreview;
        internal Sybase.DataWindow.DataWindowControl m_dwdRep;
        private string m_strFileName;
        public string M_strOprationType;
        public string p_strPBName;

        // Methods
        public frmNoticePrint()
        {
            this.components = null;
            this.dtRelust = null;
            this.p_strPBName = string.Empty;
            this.IsPrintLandscape = false;
            this.M_strOprationType = null;
            this.m_strFileName = Application.StartupPath + @"\Picture\茶山log.bmp";
            this.InitializeComponent();
            this.m_dwdRep.LibraryList = Application.StartupPath + @"\pb_anaesthesia.pbl";
            return;
        }

        private void frmNoticePrint_Load(object sender, EventArgs e)
        {
            Exception exception;
            bool flag;
        Label_0001:
            try
            {
                if (this.p_strPBName.Trim() != "anaesthesia_workarrge" && this.p_strPBName.Trim() != "anaesthesia_workarrge_gy")
                {
                    goto Label_0047;
                }
                this.m_dwdRep.Dock = DockStyle.Fill;
            Label_0047:
                this.m_dwdRep.DataWindowObject = this.p_strPBName;
                if (this.dtRelust == null)
                {
                    goto Label_00FB;
                }
                this.m_dwdRep.SetRedrawOff();
                this.m_dwdRep.Retrieve(this.dtRelust);
                this.m_dwdRep.Sort();
                this.m_dwdRep.CalculateGroups();
                this.m_dwdRep.SetRedrawOn();
                this.m_dwdRep.Refresh();
                this.m_dwdRep.Modify("txtoprationtype.text='" + this.M_strOprationType + "'");
                this.m_dwdRep.Modify("p_logo.filename='" + this.m_strFileName + "'");
            Label_00FB:
                goto Label_0117;
            }
            catch (Exception exception1)
            {
            Label_00FE:
                exception = exception1;
                MessageBox.Show(exception.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                goto Label_0117;
            }
        Label_0117:
            return;
        }

        private void InitializeComponent()
        {
            this.m_dwdRep = new Sybase.DataWindow.DataWindowControl();
            this.m_cmdExit = new PinkieControls.ButtonXP();
            this.m_cmdPrint = new PinkieControls.ButtonXP();
            this.m_cmdPrintPreview = new PinkieControls.ButtonXP();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_dwdRep
            // 
            this.m_dwdRep.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.m_dwdRep.BorderStyle = Sybase.DataWindow.DataWindowBorderStyle.None;
            this.m_dwdRep.DataWindowObject = "";
            this.m_dwdRep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_dwdRep.LibraryList = "";
            this.m_dwdRep.Location = new System.Drawing.Point(8, 45);
            this.m_dwdRep.Name = "m_dwdRep";
            this.m_dwdRep.ScrollBars = Sybase.DataWindow.DataWindowScrollBars.Both;
            this.m_dwdRep.Size = new System.Drawing.Size(845, 651);
            this.m_dwdRep.TabIndex = 34;
            this.m_dwdRep.Text = "dataWindowControl1";
            // 
            // m_cmdExit
            // 
            this.m_cmdExit.AccessibleDescription = "退出";
            this.m_cmdExit.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.m_cmdExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_cmdExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_cmdExit.DefaultScheme = true;
            this.m_cmdExit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdExit.Font = new System.Drawing.Font("宋体", 10.5F);
            this.m_cmdExit.Hint = "";
            this.m_cmdExit.Location = new System.Drawing.Point(527, 11);
            this.m_cmdExit.Name = "m_cmdExit";
            this.m_cmdExit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdExit.Size = new System.Drawing.Size(66, 28);
            this.m_cmdExit.TabIndex = 12;
            this.m_cmdExit.Text = "退 出";
            this.m_cmdExit.Click += new System.EventHandler(this.m_cmdExit_Click);
            // 
            // m_cmdPrint
            // 
            this.m_cmdPrint.AccessibleDescription = "打印";
            this.m_cmdPrint.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.m_cmdPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_cmdPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_cmdPrint.DefaultScheme = true;
            this.m_cmdPrint.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdPrint.Font = new System.Drawing.Font("宋体", 10.5F);
            this.m_cmdPrint.Hint = "";
            this.m_cmdPrint.Location = new System.Drawing.Point(311, 11);
            this.m_cmdPrint.Name = "m_cmdPrint";
            this.m_cmdPrint.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdPrint.Size = new System.Drawing.Size(66, 28);
            this.m_cmdPrint.TabIndex = 11;
            this.m_cmdPrint.Text = "打 印";
            this.m_cmdPrint.Click += new System.EventHandler(this.m_cmdPrint_Click);
            // 
            // m_cmdPrintPreview
            // 
            this.m_cmdPrintPreview.AccessibleDescription = "预览";
            this.m_cmdPrintPreview.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.m_cmdPrintPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_cmdPrintPreview.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_cmdPrintPreview.DefaultScheme = true;
            this.m_cmdPrintPreview.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdPrintPreview.Font = new System.Drawing.Font("宋体", 10.5F);
            this.m_cmdPrintPreview.Hint = "";
            this.m_cmdPrintPreview.Location = new System.Drawing.Point(421, 11);
            this.m_cmdPrintPreview.Name = "m_cmdPrintPreview";
            this.m_cmdPrintPreview.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdPrintPreview.Size = new System.Drawing.Size(66, 28);
            this.m_cmdPrintPreview.TabIndex = 35;
            this.m_cmdPrintPreview.Text = "预 览";
            this.m_cmdPrintPreview.Click += new System.EventHandler(this.m_cmdPrintPreview_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_cmdPrint);
            this.groupBox1.Controls.Add(this.m_cmdPrintPreview);
            this.groupBox1.Controls.Add(this.m_cmdExit);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1002, 41);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "打印选项";
            // 
            // frmNoticePrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1002, 701);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.m_dwdRep);
            this.Font = new System.Drawing.Font("宋体", 10.5F);
            this.MaximizeBox = false;
            this.Name = "frmNoticePrint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "打印通知单";
            this.Load += new System.EventHandler(this.frmNoticePrint_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void m_cmdExit_Click(object sender, EventArgs e)
        {
            base.Close();
            return;
        }

        private void m_cmdPrint_Click(object sender, EventArgs e)
        {
            bool flag;
            if (this.IsPrintLandscape == false)
            {
                goto Label_0022;
            }
            this.m_dwdRep.PrintProperties.Orientation = 0;
        Label_0022:
            this.m_dwdRep.Print();
            return;
        }

        private void m_cmdPrintPreview_Click(object sender, EventArgs e)
        {
            this.m_dwdRep.PrintProperties.ShowPreviewRulers = !this.m_dwdRep.PrintProperties.ShowPreviewRulers;
            this.m_dwdRep.PrintProperties.Preview = !this.m_dwdRep.PrintProperties.Preview;
            return;
        }
    }
}
