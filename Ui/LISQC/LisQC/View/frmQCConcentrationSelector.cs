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
    public class frmQCConcentrationSelector : Form
    {
        // Fields
        private IContainer components;
        private ButtonXP m_cmdCancel;
        private ButtonXP m_cmdConfirm;
        private GroupBox m_gpbList;
        private List<clsLisQCConcentrationVO> m_lstConcentration = new List<clsLisQCConcentrationVO>();
        private int max = 0;
        private int min = 0;
        private Panel panel1;

        // Methods
        public frmQCConcentrationSelector(List<clsLisQCConcentrationVO> lstConcentration, int minSelected, int maxSelected)
        {
            InitializeComponent();
            if (lstConcentration != null)
            {
                foreach (clsLisQCConcentrationVO var in lstConcentration)
                {
                    if (minSelected == 1 && maxSelected == 1)
                    {
                        RadioButton rad = new RadioButton();
                        rad.Text = var.m_strConcentration;
                        rad.Tag = var;
                        rad.Dock = DockStyle.Top;
                        this.m_gpbList.Controls.Add(rad);
                    }
                    else
                    {
                        CheckBox chk = new CheckBox();
                        chk.Text = var.m_strConcentration;
                        chk.Tag = var;
                        chk.Dock = DockStyle.Top;
                        this.m_gpbList.Controls.Add(chk);
                    }
                }
            }
            this.min = minSelected;
            this.max = maxSelected;
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    this.Dispose(disposing);
        //}

        private void InitializeComponent()
        {
            this.panel1 = new Panel();
            this.m_cmdConfirm = new ButtonXP();
            this.m_cmdCancel = new ButtonXP();
            this.m_gpbList = new GroupBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            this.panel1.Controls.Add(this.m_cmdConfirm);
            this.panel1.Controls.Add(this.m_cmdCancel);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 0x9d);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0xb7, 0x2e);
            this.panel1.TabIndex = 0;
            this.m_cmdConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdConfirm.BackColor = Color.FromArgb(0, 0xd4, 0xd0, 200);
            this.m_cmdConfirm.DefaultScheme = true;
            this.m_cmdConfirm.DialogResult = 0;
            this.m_cmdConfirm.Hint = "";
            this.m_cmdConfirm.Location = new Point(12, 8);
            this.m_cmdConfirm.Name = "m_cmdConfirm";
            this.m_cmdConfirm.Scheme = 0;
            this.m_cmdConfirm.Size = new Size(0x4c, 0x21);
            this.m_cmdConfirm.TabIndex = 0x17;
            this.m_cmdConfirm.Text = "确定";
            this.m_cmdConfirm.Click += new EventHandler(this.m_cmdConfirm_Click);
            this.m_cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdCancel.BackColor = Color.FromArgb(0, 0xd4, 0xd0, 200);
            this.m_cmdCancel.DefaultScheme = true;
            this.m_cmdCancel.DialogResult = DialogResult.Cancel;
            this.m_cmdCancel.Hint = "";
            this.m_cmdCancel.Location = new Point(0x60, 8);
            this.m_cmdCancel.Name = "m_cmdCancel";
            this.m_cmdCancel.Scheme = 0;
            this.m_cmdCancel.Size = new Size(0x4c, 0x21);
            this.m_cmdCancel.TabIndex = 0x18;
            this.m_cmdCancel.Text = "取消(ESC)";
            this.m_gpbList.Dock = DockStyle.Fill;
            this.m_gpbList.Location = new Point(0, 0);
            this.m_gpbList.Name = "m_gpbList";
            this.m_gpbList.Size = new Size(0xb7, 0x9d);
            this.m_gpbList.TabIndex = 1;
            this.m_gpbList.TabStop = false;
            this.AutoScaleDimensions = new SizeF(6f, 12f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.CancelButton = this.m_cmdCancel;
            this.ClientSize = new Size(0xb7, 0xcb);
            this.Controls.Add(this.m_gpbList);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmQCConcentrationSelector";
            this.StartPosition = 0;
            this.Text = "浓度选择";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void m_cmdConfirm_Click(object sender, EventArgs e)
        {
            m_lstConcentration.Clear();
            foreach (Control ctl in this.m_gpbList.Controls)
            {
                CheckBox chk = ctl as CheckBox;
                if (chk != null && chk.Checked)
                {
                    this.m_lstConcentration.Add(chk.Tag as clsLisQCConcentrationVO);
                }

                RadioButton rad = ctl as RadioButton;
                if (rad != null && rad.Checked)
                {
                    this.m_lstConcentration.Add(rad.Tag as clsLisQCConcentrationVO);
                }
            }
            if (this.m_lstConcentration.Count < this.min)
            {
                MessageBox.Show("你必需先择最少 " + this.min.ToString() + " 个浓度!", "iCare");
                return;
            }
            if (this.m_lstConcentration.Count > this.max)
            {
                MessageBox.Show("你最多只能先择 " + this.max.ToString() + " 个浓度!", "iCare");
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // Properties
        public List<clsLisQCConcentrationVO> Concentrations
        {
            get { return m_lstConcentration; }
        }
    }
}
