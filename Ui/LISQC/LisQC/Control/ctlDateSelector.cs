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
    public class ctlDateSelector : UserControl
    {
        // Fields
        private IContainer components;
        private Form frm;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Button m_cmd;
        private ButtonXP m_cmdCancel;
        private ButtonXP m_cmdConfirm;
        internal DateTime m_datEnd;
        internal DateTime m_datStart;
        private DateTimePicker m_dtp1;
        private DateTimePicker m_dtp2;
        private DateTimePicker m_dtp3;
        private DateTimePicker m_dtp4;
        private DateTimePicker m_dtp5;
        private Panel m_pnl1;
        private Panel m_pnl2;
        private Panel m_pnl3;
        private Panel m_pnlFrm;
        private RadioButton m_radDateSect;
        private RadioButton m_radMonth;
        private RadioButton m_radMonthSect;
        private SelectStyle m_selectStyle;

        // Events
        public event EventHandler ValueChanged;

        // Methods
        public ctlDateSelector()
        {
            this.components = null; 
            this.InitializeComponent();
            this.frm = new Form();
            this.frm.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.frm.Size = new Size(0xe4, 0x9c);
            this.frm.Controls.Add(this.m_pnlFrm);
            this.frm.MinimizeBox = false;
            this.frm.MaximizeBox = false;
            this.frm.StartPosition = 0;
            this.frm.CancelButton = this.m_cmdCancel;
            this.frm.AcceptButton = this.m_cmdConfirm;
            this.m_pnlFrm.Visible = true;
            this.m_pnlFrm.Dock = DockStyle.Fill;
            this.m_datStart = DateTime.Now;
            this.m_datEnd = DateTime.Now;
            this.m_selectStyle = 0;
            this.m_mthAdjustRad();
            this.m_mthAdjustDat();
            this.m_mthAdjustDtp();
            this.m_mthAdjustText(); 
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
            this.m_cmd = new Button();
            this.m_pnlFrm = new Panel();
            this.m_pnl2 = new Panel();
            this.m_dtp2 = new DateTimePicker();
            this.m_dtp3 = new DateTimePicker();
            this.label2 = new Label();
            this.m_cmdCancel = new ButtonXP();
            this.m_pnl3 = new Panel();
            this.label1 = new Label();
            this.m_dtp4 = new DateTimePicker();
            this.m_dtp5 = new DateTimePicker();
            this.m_cmdConfirm = new ButtonXP();
            this.groupBox1 = new GroupBox();
            this.m_radMonth = new RadioButton();
            this.m_radDateSect = new RadioButton();
            this.m_radMonthSect = new RadioButton();
            this.m_pnl1 = new Panel();
            this.m_dtp1 = new DateTimePicker();
            this.m_pnlFrm.SuspendLayout();
            this.m_pnl2.SuspendLayout();
            this.m_pnl3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.m_pnl1.SuspendLayout();
            base.SuspendLayout();
            this.m_cmd.Dock = DockStyle.Fill;
            this.m_cmd.FlatStyle = FlatStyle.Popup;
            this.m_cmd.Location = new Point(0, 0);
            this.m_cmd.Name = "m_cmd";
            this.m_cmd.Size = new Size(0x19c, 0xf5);
            this.m_cmd.TabIndex = 1;
            this.m_cmd.UseVisualStyleBackColor = true;
            this.m_cmd.Click += new EventHandler(this.m_cmd_Click);
            this.m_pnlFrm.Controls.Add(this.m_pnl2);
            this.m_pnlFrm.Controls.Add(this.m_cmdCancel);
            this.m_pnlFrm.Controls.Add(this.m_pnl3);
            this.m_pnlFrm.Controls.Add(this.m_cmdConfirm);
            this.m_pnlFrm.Controls.Add(this.groupBox1);
            this.m_pnlFrm.Controls.Add(this.m_pnl1);
            this.m_pnlFrm.Location = new Point(0x4c, 20);
            this.m_pnlFrm.Name = "m_pnlFrm";
            this.m_pnlFrm.Size = new Size(0xe0, 0x88);
            this.m_pnlFrm.TabIndex = 2;
            this.m_pnlFrm.Visible = false;
            this.m_pnl2.Controls.Add(this.m_dtp2);
            this.m_pnl2.Controls.Add(this.m_dtp3);
            this.m_pnl2.Controls.Add(this.label2);
            this.m_pnl2.Location = new Point(0, 40);
            this.m_pnl2.Name = "m_pnl2";
            this.m_pnl2.Size = new Size(0xe0, 0x34);
            this.m_pnl2.TabIndex = 8;
            this.m_dtp2.CustomFormat = "yyyy-MM";
            this.m_dtp2.Format = DateTimePickerFormat.Custom;
            this.m_dtp2.Location = new Point(0x18, 0x10);
            this.m_dtp2.Name = "m_dtp2";
            this.m_dtp2.Size = new Size(80, 0x17);
            this.m_dtp2.TabIndex = 5;
            this.m_dtp3.CustomFormat = "yyyy-MM";
            this.m_dtp3.Format = DateTimePickerFormat.Custom;
            this.m_dtp3.Location = new Point(120, 0x10);
            this.m_dtp3.Name = "m_dtp3";
            this.m_dtp3.Size = new Size(80, 0x17);
            this.m_dtp3.TabIndex = 6;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x69, 0x13);
            this.label2.Name = "label2";
            this.label2.Size = new Size(14, 14);
            this.label2.TabIndex = 15;
            this.label2.Text = "-";
            this.m_cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdCancel.BackColor = Color.FromArgb(0, 0xec, 0xe9, 0xd8);
            this.m_cmdCancel.DefaultScheme = true;
            this.m_cmdCancel.DialogResult = 0;
            this.m_cmdCancel.Hint = "";
            this.m_cmdCancel.Location = new Point(0x74, 0x60);
            this.m_cmdCancel.Name = "m_cmdCancel";
            this.m_cmdCancel.Scheme = 0;
            this.m_cmdCancel.Size = new Size(100, 0x1c);
            this.m_cmdCancel.TabIndex = 13;
            this.m_cmdCancel.Text = "取消(ESC)";
            this.m_cmdCancel.Click += new EventHandler(this.m_cmdCancel_Click);
            this.m_pnl3.Controls.Add(this.label1);
            this.m_pnl3.Controls.Add(this.m_dtp4);
            this.m_pnl3.Controls.Add(this.m_dtp5);
            this.m_pnl3.Location = new Point(0, 40);
            this.m_pnl3.Name = "m_pnl3";
            this.m_pnl3.Size = new Size(0xe0, 0x34);
            this.m_pnl3.TabIndex = 7;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x68, 20);
            this.label1.Name = "label1";
            this.label1.Size = new Size(14, 14);
            this.label1.TabIndex = 14;
            this.label1.Text = "-";
            this.m_dtp4.CustomFormat = "yyyy-MM-dd";
            this.m_dtp4.Format = DateTimePickerFormat.Custom;
            this.m_dtp4.Location = new Point(8, 0x10);
            this.m_dtp4.Name = "m_dtp4";
            this.m_dtp4.Size = new Size(0x60, 0x17);
            this.m_dtp4.TabIndex = 7;
            this.m_dtp5.CustomFormat = "yyyy-MM-dd";
            this.m_dtp5.Format = DateTimePickerFormat.Custom;
            this.m_dtp5.Location = new Point(120, 0x10);
            this.m_dtp5.Name = "m_dtp5";
            this.m_dtp5.Size = new Size(0x60, 0x17);
            this.m_dtp5.TabIndex = 8;
            this.m_cmdConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdConfirm.BackColor = Color.FromArgb(0, 0xec, 0xe9, 0xd8);
            this.m_cmdConfirm.DefaultScheme = true;
            this.m_cmdConfirm.DialogResult = 0;
            this.m_cmdConfirm.Hint = "";
            this.m_cmdConfirm.Location = new Point(8, 0x60);
            this.m_cmdConfirm.Name = "m_cmdConfirm";
            this.m_cmdConfirm.Scheme = 0;
            this.m_cmdConfirm.Size = new Size(0x68, 0x1c);
            this.m_cmdConfirm.TabIndex = 12;
            this.m_cmdConfirm.Text = "确定(Enter)";
            this.m_cmdConfirm.Click += new EventHandler(this.m_cmdConfirm_Click);
            this.groupBox1.Controls.Add(this.m_radMonth);
            this.groupBox1.Controls.Add(this.m_radDateSect);
            this.groupBox1.Controls.Add(this.m_radMonthSect);
            this.groupBox1.Dock = DockStyle.Top;
            this.groupBox1.Location = new Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xe0, 40);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.m_radMonth.AutoSize = true;
            this.m_radMonth.Checked = true;
            this.m_radMonth.Location = new Point(0x21, 0x10);
            this.m_radMonth.Name = "m_radMonth";
            this.m_radMonth.Size = new Size(0x35, 0x12);
            this.m_radMonth.TabIndex = 0;
            this.m_radMonth.TabStop = true;
            this.m_radMonth.Text = "月份";
            this.m_radMonth.UseVisualStyleBackColor = true;
            this.m_radMonth.CheckedChanged += new EventHandler(this.m_radMonth_CheckedChanged);
            this.m_radDateSect.AutoSize = true;
            this.m_radDateSect.Location = new Point(0xb6, 0x10);
            this.m_radDateSect.Name = "m_radDateSect";
            this.m_radDateSect.Size = new Size(0x43, 0x12);
            this.m_radDateSect.TabIndex = 2;
            this.m_radDateSect.Text = "时间段";
            this.m_radDateSect.UseVisualStyleBackColor = true;
            this.m_radDateSect.Visible = false;
            this.m_radDateSect.CheckedChanged += new EventHandler(this.m_radDateSect_CheckedChanged);
            this.m_radMonthSect.AutoSize = true;
            this.m_radMonthSect.Location = new Point(0x71, 0x10);
            this.m_radMonthSect.Name = "m_radMonthSect";
            this.m_radMonthSect.Size = new Size(0x43, 0x12);
            this.m_radMonthSect.TabIndex = 1;
            this.m_radMonthSect.Text = "月份段";
            this.m_radMonthSect.UseVisualStyleBackColor = true;
            this.m_radMonthSect.CheckedChanged += new EventHandler(this.m_radMonthSect_CheckedChanged);
            this.m_pnl1.Controls.Add(this.m_dtp1);
            this.m_pnl1.Location = new Point(0, 40);
            this.m_pnl1.Name = "m_pnl1";
            this.m_pnl1.Size = new Size(0xe0, 0x34);
            this.m_pnl1.TabIndex = 8;
            this.m_dtp1.CustomFormat = "yyyy-MM";
            this.m_dtp1.Format = DateTimePickerFormat.Custom;
            this.m_dtp1.Location = new Point(0x4c, 0x10);
            this.m_dtp1.Name = "m_dtp1";
            this.m_dtp1.Size = new Size(80, 0x17);
            this.m_dtp1.TabIndex = 4;
            base.AutoScaleDimensions = new SizeF(7f, 14f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.m_pnlFrm);
            base.Controls.Add(this.m_cmd);
            this.Font = new Font("宋体", 10.5f);
            base.Name = "ctlDateSelector";
            base.Size = new Size(0x19c, 0xf5);
            this.m_pnlFrm.ResumeLayout(false);
            this.m_pnl2.ResumeLayout(false);
            this.m_pnl2.PerformLayout();
            this.m_pnl3.ResumeLayout(false);
            this.m_pnl3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.m_pnl1.ResumeLayout(false);
            this.ResumeLayout(false); 
        }

        private void m_cmd_Click(object sender, EventArgs e)
        {
            Point point;
            this.m_mthAdjustRad();
            this.m_mthAdjustDtp();
            point = base.Parent.PointToScreen(base.Location);
            this.frm.Location = new Point(point.X, point.Y);
            this.m_radMonth_CheckedChanged(null, null);
            this.m_radMonthSect_CheckedChanged(null, null);
            this.m_radDateSect_CheckedChanged(null, null);
            this.frm.ShowDialog(); 
        }

        private void m_cmdCancel_Click(object sender, EventArgs e)
        {
            this.frm.Close(); 
        }

        private void m_cmdConfirm_Click(object sender, EventArgs e)
        {
            bool flag;
            DateTime time;
            if (this.m_radMonth.Checked ==false )
            {
                goto Label_005D;
            }
            this.m_selectStyle = 0;
            this.m_datStart = this.m_dtp1.Value.Date;
            this.m_datEnd = Convert.ToDateTime(this.m_dtp1.Value.ToString("yyyy-MM-dd 23:59:59"));
            goto Label_0149;
        Label_005D:
            if (this.m_radMonthSect.Checked == false) 
            {
                goto Label_00DD;
            }
            if (this.m_dtp2.Value <= this.m_dtp3.Value)  
            {
                goto Label_0097;
            }
            goto Label_018E;
        Label_0097:
            this.m_selectStyle = SelectStyle.MonthSectStyle;
            this.m_datStart = this.m_dtp2.Value.Date;
            this.m_datEnd = Convert.ToDateTime(this.m_dtp3.Value.ToString("yyyy-MM-dd 23:59:59"));
            goto Label_0149;
        Label_00DD:
            if (this.m_dtp4.Value <= this.m_dtp5.Value)  
            {
                goto Label_0105;
            }
            goto Label_018E;
        Label_0105:
            this.m_selectStyle = SelectStyle.DateSectStyle;
            this.m_datStart = this.m_dtp4.Value.Date;
            this.m_datEnd = Convert.ToDateTime(this.m_dtp5.Value.ToString("yyyy-MM-dd 23:59:59"));
        Label_0149:
            this.m_mthAdjustDat();
            this.m_mthAdjustRad();
            this.m_mthAdjustDtp();
            this.m_mthAdjustText();
            this.frm.Close();
            if (this.ValueChanged == null)  
            {
                goto Label_018E;
            }
            this.ValueChanged(this, null);
        Label_018E:
            return;
        }

        private void m_mthAdjustDat()
        {
            SelectStyle style;
            switch (this.m_selectStyle)
            {
                case SelectStyle.MonthStyle:
                    goto Label_001F;

                case SelectStyle.MonthSectStyle:
                    goto Label_0082;

                case SelectStyle.DateSectStyle:
                    goto Label_00E2;
            }
            goto Label_013C;
        Label_001F:
            this.m_datStart = new DateTime(this.m_datStart.Year, this.m_datStart.Month, 1);
            this.m_datEnd = new DateTime(this.m_datStart.Year, this.m_datStart.Month, DateTime.DaysInMonth(this.m_datStart.Year, this.m_datStart.Month));
            goto Label_013E;
        Label_0082:
            this.m_datStart = new DateTime(this.m_datStart.Year, this.m_datStart.Month, 1);
            this.m_datEnd = new DateTime(this.m_datEnd.Year, this.m_datEnd.Month, DateTime.DaysInMonth(this.m_datEnd.Year, this.m_datEnd.Month));
            goto Label_013E;
        Label_00E2:
            this.m_datStart = new DateTime(this.m_datStart.Year, this.m_datStart.Month, this.m_datStart.Day);
            this.m_datEnd = new DateTime(this.m_datEnd.Year, this.m_datEnd.Month, this.m_datEnd.Day);
            goto Label_013E;
        Label_013C:;
        Label_013E:
            return;
        }

        private void m_mthAdjustDtp()
        {
            this.m_dtp1.Value = this.m_datStart;
            this.m_dtp2.Value = this.m_datStart;
            this.m_dtp3.Value = this.m_datEnd;
            this.m_dtp4.Value = this.m_datStart;
            this.m_dtp5.Value = this.m_datEnd;
            return;
        }

        private void m_mthAdjustRad()
        {
            SelectStyle style;
            switch (this.m_selectStyle)
            {
                case SelectStyle.MonthStyle:
                    goto Label_001C;

                case SelectStyle.MonthSectStyle:
                    goto Label_002B;

                case SelectStyle.DateSectStyle:
                    goto Label_003A;
            }
            goto Label_0049;
        Label_001C:
            this.m_radMonth.Checked = true;
            goto Label_004B;
        Label_002B:
            this.m_radMonthSect.Checked = true;
            goto Label_004B;
        Label_003A:
            this.m_radDateSect.Checked = true;
            goto Label_004B;
        Label_0049:;
        Label_004B:
            return;
        }

        private  void m_mthAdjustText()
        {
            SelectStyle style;
            switch (this.m_selectStyle)
            {
                case SelectStyle.MonthStyle:
                    goto Label_001F;

                case SelectStyle.MonthSectStyle:
                    goto Label_003D;

                case SelectStyle.DateSectStyle:
                    goto Label_0075;
            }
            goto Label_00AD;
        Label_001F:
            this.m_cmd.Text = this.m_datStart.ToString("yyyy-MM");
            goto Label_00AF;
        Label_003D:
            this.m_cmd.Text = this.m_datStart.ToString("yyyy-MM") + " - " + this.m_datEnd.ToString("yyyy-MM");
            goto Label_00AF;
        Label_0075:
            this.m_cmd.Text = this.m_datStart.ToString("yyyy-MM-dd") + " - " + this.m_datEnd.ToString("yyyy-MM-dd");
            goto Label_00AF;
        Label_00AD:;
        Label_00AF:
            return;
        }

        private void m_radDateSect_CheckedChanged(object sender, EventArgs e)
        {
            bool flag;
            if (this.m_radDateSect.Checked == false)  
            {
                goto Label_0048;
            }
            this.m_pnl1.Visible = false;
            this.m_pnl2.Visible = false;
            this.m_pnl3.Visible = true;
            this.m_dtp4.Focus();
        Label_0048:
            return;
        }

        private void m_radMonth_CheckedChanged(object sender, EventArgs e)
        {
            bool flag;
            if (this.m_radMonth.Checked == false)  
            {
                goto Label_0048;
            }
            this.m_pnl1.Visible = true;
            this.m_pnl2.Visible = false;
            this.m_pnl3.Visible = false;
            this.m_dtp1.Focus();
        Label_0048:
            return;
        }

        private void m_radMonthSect_CheckedChanged(object sender, EventArgs e)
        {
            bool flag;
            if (this.m_radMonthSect.Checked == false)  
            {
                goto Label_0048;
            }
            this.m_pnl1.Visible = false;
            this.m_pnl2.Visible = true;
            this.m_pnl3.Visible = false;
            this.m_dtp2.Focus();
        Label_0048:
            return;
        }

        public void SetDate(DateTime datStart, DateTime datEnd, SelectStyle selectStyle)
        {
            this.m_selectStyle = selectStyle;
            this.m_datStart = datStart;
            this.m_datEnd = datEnd;
            this.m_mthAdjustDat();
            this.m_mthAdjustRad();
            this.m_mthAdjustDtp();
            this.m_mthAdjustText();
            return;
        }

        // Properties
        public DateTime DateEnd
        {
            get
            {
                DateTime time;
                time = this.m_datEnd;
            Label_000A:
                return time;
            }
        }

        public SelectStyle DateSelectStyle
        {
            get
            {
                SelectStyle style;
                style = this.m_selectStyle;
            Label_000A:
                return style;
            }
            set
            {
                this.m_selectStyle = value;
                this.m_mthAdjustDat();
                this.m_mthAdjustRad();
                this.m_mthAdjustDtp();
                this.m_mthAdjustText();
                return;
            }
        }

        public DateTime DateStart
        {
            get
            {
                DateTime time;
                time = this.m_datStart;
            Label_000A:
                return time;
            }
        }

        public string Text
        {
            get
            {
                string str;
                str = this.m_cmd.Text;
            Label_000F:
                return str;
            }
        }

        // Nested Types
        public enum SelectStyle
        {
            MonthStyle,
            MonthSectStyle,
            DateSectStyle
        }
    }
}
