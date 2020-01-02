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
using System.Net;

namespace com.digitalwave.iCare.gui.LIS
{
    public class frmQCResult : frmMDI_Child_Base
    {
        // Fields
        private IContainer components;
        private DataGridViewTextBoxColumn concentration;
        private DataGridViewTextBoxColumn data_seq_int;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataTable dtDeviceInfo;
        private int intSaveTytle;
        private Label label1;
        private Label label3;
        private Label label4;
        internal ButtonXP m_btnDelete;
        internal ButtonXP m_btnSave;
        private ComboBox m_cboDeviceInfo;
        private ComboBox m_cboQCLot;
        internal ButtonXP m_cmdQueryQCResult;
        private DataGridView m_dgMonthResult;
        private DataGridView m_dgResult;
        private DataTable m_dtbSource;
        private DateTimePicker m_dtDate;
        private DataTable m_dtQCSetResult;
        private List<clsLisQCDataVO> m_lstAddedVOS;
        private List<clsLisQCDataVO> m_lstChangedVOS;
        private List<clsLisQCDataVO> m_lstDeletedVOS;
        private clsQCBatchNew m_objBatch;
        private clsQCBatch m_objContoller;
        //private clsDcl_QCDataBusiness m_objDomain;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private Panel panel5;
        private DataGridViewTextBoxColumn QCCheckName;
        private DataGridViewTextBoxColumn QCCoefficient;
        private DataGridViewTextBoxColumn QCDate;
        private DataGridViewTextBoxColumn QCDeviation;
        private DataGridViewTextBoxColumn QCPlacement;
        private DataGridViewTextBoxColumn QCResult;
        private DataGridViewTextBoxColumn QCTarget;
        private DataGridViewTextBoxColumn Tag;

        // Methods
        public frmQCResult()
        {
            this.m_objContoller = new clsQCBatch();
            this.dtDeviceInfo = null;
            this.m_objBatch = new clsQCBatchNew();
            //this.m_objDomain = new clsDcl_QCDataBusiness();
            this.intSaveTytle = 0;
            this.m_dtQCSetResult = null;
            this.m_dtbSource = new DataTable("datalist");
            this.m_lstChangedVOS = new List<clsLisQCDataVO>();
            this.m_lstAddedVOS = new List<clsLisQCDataVO>();
            this.m_lstDeletedVOS = new List<clsLisQCDataVO>();
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

        private void frmQCResult_Load(object sender, EventArgs e)
        {
            this.m_dgResult.AutoGenerateColumns = false;
            this.m_dgMonthResult.AutoGenerateColumns = false;
            this.m_mthInfo();
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle style;
            DataGridViewCellStyle style2;
            DataGridViewCellStyle style3;
            DataGridViewCellStyle style4;
            DataGridViewCellStyle style5;
            DataGridViewCellStyle style6;
            DataGridViewCellStyle style7;
            ComponentResourceManager manager;
            DataGridViewColumn[] columnArray;
            style = new DataGridViewCellStyle();
            style2 = new DataGridViewCellStyle();
            style3 = new DataGridViewCellStyle();
            style4 = new DataGridViewCellStyle();
            style5 = new DataGridViewCellStyle();
            style6 = new DataGridViewCellStyle();
            style7 = new DataGridViewCellStyle();
            manager = new ComponentResourceManager(typeof(frmQCResult));
            this.panel1 = new Panel();
            this.panel3 = new Panel();
            this.panel5 = new Panel();
            this.m_dgMonthResult = new DataGridView();
            this.panel4 = new Panel();
            this.m_dgResult = new DataGridView();
            this.panel2 = new Panel();
            this.m_btnDelete = new ButtonXP();
            this.m_btnSave = new ButtonXP();
            this.m_cmdQueryQCResult = new ButtonXP();
            this.m_cboQCLot = new ComboBox();
            this.label4 = new Label();
            this.m_cboDeviceInfo = new ComboBox();
            this.label3 = new Label();
            this.m_dtDate = new DateTimePicker();
            this.label1 = new Label();
            this.QCCheckName = new DataGridViewTextBoxColumn();
            this.QCResult = new DataGridViewTextBoxColumn();
            this.QCPlacement = new DataGridViewTextBoxColumn();
            this.QCTarget = new DataGridViewTextBoxColumn();
            this.QCDeviation = new DataGridViewTextBoxColumn();
            this.QCCoefficient = new DataGridViewTextBoxColumn();
            this.Tag = new DataGridViewTextBoxColumn();
            this.concentration = new DataGridViewTextBoxColumn();
            this.data_seq_int = new DataGridViewTextBoxColumn();
            this.QCDate = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dgMonthResult)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dgResult)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x3af, 0x21a);
            this.panel1.TabIndex = 0;
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BorderStyle = BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Location = new Point(0, 0x4d);
            this.panel3.Name = "panel3";
            this.panel3.Size = new Size(0x3af, 0x1cd);
            this.panel3.TabIndex = 1;
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BorderStyle = BorderStyle.Fixed3D;
            this.panel5.Controls.Add(this.m_dgMonthResult);
            this.panel5.Location = new Point(0x25a, -2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new Size(0x151, 0x1cb);
            this.panel5.TabIndex = 1;
            this.m_dgMonthResult.AllowUserToAddRows = false;
            this.m_dgMonthResult.AllowUserToDeleteRows = false;
            this.m_dgMonthResult.AllowUserToResizeRows = false;
            this.m_dgMonthResult.BackgroundColor = SystemColors.ButtonHighlight;
            this.m_dgMonthResult.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_dgMonthResult.Columns.AddRange(new DataGridViewColumn[] { this.QCDate, this.dataGridViewTextBoxColumn1, this.dataGridViewTextBoxColumn2 });
            this.m_dgMonthResult.Dock = DockStyle.Fill;
            this.m_dgMonthResult.Location = new Point(0, 0);
            this.m_dgMonthResult.MultiSelect = false;
            this.m_dgMonthResult.Name = "m_dgMonthResult";
            this.m_dgMonthResult.ReadOnly = true;
            this.m_dgMonthResult.RowHeadersVisible = false;
            this.m_dgMonthResult.RowTemplate.Height = 0x17;
            this.m_dgMonthResult.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.m_dgMonthResult.Size = new Size(0x14d, 0x1c7);
            this.m_dgMonthResult.TabIndex = 0;
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Bottom)));
            this.panel4.BorderStyle = BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.m_dgResult);
            this.panel4.Location = new Point(0, -2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new Size(0x25e, 0x1cb);
            this.panel4.TabIndex = 0;
            this.m_dgResult.AllowUserToAddRows = false;
            this.m_dgResult.AllowUserToDeleteRows = false;
            this.m_dgResult.AllowUserToResizeRows = false;
            this.m_dgResult.BackgroundColor = SystemColors.ButtonHighlight;
            this.m_dgResult.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_dgResult.Columns.AddRange(new DataGridViewColumn[] { this.QCCheckName, this.QCResult, this.QCPlacement, this.QCTarget, this.QCDeviation, this.QCCoefficient, this.Tag, this.concentration, this.data_seq_int });
            this.m_dgResult.Dock = DockStyle.Fill;
            this.m_dgResult.EditMode = 0;
            this.m_dgResult.Location = new Point(0, 0);
            this.m_dgResult.MultiSelect = false;
            this.m_dgResult.Name = "m_dgResult";
            this.m_dgResult.RowTemplate.Height = 0x17;
            this.m_dgResult.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.m_dgResult.Size = new Size(0x25a, 0x1c7);
            this.m_dgResult.TabIndex = 0;
            this.m_dgResult.CellValidating += new DataGridViewCellValidatingEventHandler(this.m_dgResult_CellValidating);
            this.m_dgResult.CellEndEdit += new DataGridViewCellEventHandler(this.m_dgResult_CellEndEdit);
            this.m_dgResult.CellValueChanged += new DataGridViewCellEventHandler(this.m_dgResult_CellValueChanged);
            this.m_dgResult.SelectionChanged += new EventHandler(this.m_dgResult_SelectionChanged);
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.m_btnDelete);
            this.panel2.Controls.Add(this.m_btnSave);
            this.panel2.Controls.Add(this.m_cmdQueryQCResult);
            this.panel2.Controls.Add(this.m_cboQCLot);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.m_cboDeviceInfo);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.m_dtDate);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new Point(-1, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x3af, 0x51);
            this.panel2.TabIndex = 0;
            this.m_btnDelete.BackColor = Color.FromArgb(0, 0xd4, 0xd0, 200);
            this.m_btnDelete.DefaultScheme = true;
            this.m_btnDelete.DialogResult = DialogResult.Cancel;
            this.m_btnDelete.Hint = "";
            this.m_btnDelete.Location = new Point(0x2f6, 9);
            this.m_btnDelete.Name = "m_btnDelete";
            this.m_btnDelete.Scheme = 0;
            this.m_btnDelete.Size = new Size(0x60, 0x21);
            this.m_btnDelete.TabIndex = 10;
            this.m_btnDelete.Text = "删除";
            this.m_btnDelete.Click += new EventHandler(this.m_btnDelete_Click);
            this.m_btnSave.BackColor = Color.FromArgb(0, 0xd4, 0xd0, 200);
            this.m_btnSave.DefaultScheme = true;
            this.m_btnSave.DialogResult = DialogResult.Cancel;
            this.m_btnSave.Hint = "";
            this.m_btnSave.Location = new Point(0x272, 8);
            this.m_btnSave.Name = "m_btnSave";
            this.m_btnSave.Scheme = 0;
            this.m_btnSave.Size = new Size(0x60, 0x21);
            this.m_btnSave.TabIndex = 9;
            this.m_btnSave.Text = "保存";
            this.m_btnSave.Click += new EventHandler(this.m_btnSave_Click);
            this.m_cmdQueryQCResult.BackColor = Color.FromArgb(0, 0xd4, 0xd0, 200);
            this.m_cmdQueryQCResult.DefaultScheme = true;
            this.m_cmdQueryQCResult.DialogResult = DialogResult.Cancel;
            this.m_cmdQueryQCResult.Hint = "";
            this.m_cmdQueryQCResult.Location = new Point(0x1ec, 9);
            this.m_cmdQueryQCResult.Name = "m_cmdQueryQCResult";
            this.m_cmdQueryQCResult.Scheme = 0;
            this.m_cmdQueryQCResult.Size = new Size(0x60, 0x21);
            this.m_cmdQueryQCResult.TabIndex = 8;
            this.m_cmdQueryQCResult.Text = "查询";
            this.m_cmdQueryQCResult.Click += new EventHandler(this.m_cmdQueryQCResult_Click);
            this.m_cboQCLot.FormattingEnabled = true;
            this.m_cboQCLot.Location = new Point(0x3f, 0x2d);
            this.m_cboQCLot.Name = "m_cboQCLot";
            this.m_cboQCLot.Size = new Size(0xa6, 0x16);
            this.m_cboQCLot.TabIndex = 7;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x16, 0x31);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x23, 14);
            this.label4.TabIndex = 6;
            this.label4.Text = "批号";
            this.m_cboDeviceInfo.FormattingEnabled = true;
            this.m_cboDeviceInfo.Location = new Point(0x3f, 9);
            this.m_cboDeviceInfo.Name = "m_cboDeviceInfo";
            this.m_cboDeviceInfo.Size = new Size(0xa6, 0x16);
            this.m_cboDeviceInfo.TabIndex = 5;
            this.m_cboDeviceInfo.SelectedIndexChanged += new EventHandler(this.m_cboDeviceInfo_SelectedIndexChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x16, 14);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x23, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "仪器";
            this.m_dtDate.CustomFormat = "yyyy-MM-dd";
            this.m_dtDate.Format = DateTimePickerFormat.Custom;
            this.m_dtDate.Location = new Point(0x165, 8);
            this.m_dtDate.Name = "m_dtDate";
            this.m_dtDate.Size = new Size(0x61, 0x17);
            this.m_dtDate.TabIndex = 1;
            this.m_dtDate.ValueChanged += new EventHandler(this.m_dtDate_ValueChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x120, 12);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x3f, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "查询日期";
            this.QCCheckName.DataPropertyName = "device_check_item_name_vchr";
            this.QCCheckName.HeaderText = "质控项目";
            this.QCCheckName.Name = "QCCheckName";
            this.QCCheckName.ReadOnly = true;
            this.QCResult.DataPropertyName = "result_num";
            style.Format = "N4";
            style.NullValue = null;
            this.QCResult.DefaultCellStyle = style;
            this.QCResult.HeaderText = "结果";
            this.QCResult.Name = "QCResult";
            this.QCPlacement.DataPropertyName = "placement";
            style2.Format = "N2";
            style2.NullValue = null;
            this.QCPlacement.DefaultCellStyle = style2;
            this.QCPlacement.HeaderText = "落点";
            this.QCPlacement.Name = "QCPlacement";
            this.QCPlacement.ReadOnly = true;
            this.QCPlacement.Width = 70;
            this.QCTarget.DataPropertyName = "avg_num";
            style3.Format = "N4";
            style3.NullValue = null;
            this.QCTarget.DefaultCellStyle = style3;
            this.QCTarget.HeaderText = "靶值";
            this.QCTarget.Name = "QCTarget";
            this.QCTarget.ReadOnly = true;
            this.QCDeviation.DataPropertyName = "sd_num";
            style4.Format = "N4";
            style4.NullValue = null;
            this.QCDeviation.DefaultCellStyle = style4;
            this.QCDeviation.HeaderText = "标准差";
            this.QCDeviation.Name = "QCDeviation";
            this.QCDeviation.ReadOnly = true;
            this.QCCoefficient.DataPropertyName = "cv_num";
            style5.Format = "N4";
            style5.NullValue = null;
            this.QCCoefficient.DefaultCellStyle = style5;
            this.QCCoefficient.HeaderText = "变异系数";
            this.QCCoefficient.Name = "QCCoefficient";
            this.QCCoefficient.ReadOnly = true;
            this.Tag.DataPropertyName = "qcbatch_seq_int";
            this.Tag.HeaderText = "Tag";
            this.Tag.Name = "Tag";
            this.Tag.Visible = false;
            this.concentration.DataPropertyName = "concentration_seq_int";
            this.concentration.HeaderText = "concentration";
            this.concentration.Name = "concentration";
            this.concentration.Visible = false;
            this.data_seq_int.DataPropertyName = "data_seq_int";
            this.data_seq_int.HeaderText = "data_seq_int";
            this.data_seq_int.Name = "data_seq_int";
            this.data_seq_int.Visible = false;
            this.QCDate.DataPropertyName = "qcdate_dat";
            this.QCDate.HeaderText = "质控日期";
            this.QCDate.Name = "QCDate";
            this.QCDate.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "result_num";
            style6.Format = "N4";
            style6.NullValue = null;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = style6;
            this.dataGridViewTextBoxColumn1.HeaderText = "结果";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "placement";
            style7.Format = "N2";
            style7.NullValue = null;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = style7;
            this.dataGridViewTextBoxColumn2.HeaderText = "落点";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.AutoScaleDimensions = new SizeF(7f, 14f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(0x3af, 0x21a);
            this.Controls.Add(this.panel1);
            this.Font = new Font("宋体", 10.5f);
            //this.Icon = (Icon)manager.GetObject("$this.Icon");
            this.Name = "frmQCResult";
            this.Text = "质控结果";
            this.Load += new EventHandler(this.frmQCResult_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_dgMonthResult)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_dgResult)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
        }

        private void m_btnDelete_Click(object sender, EventArgs e)
        {
            int num;
            clsLisQCDataVO avo;
            bool flag;
            if (this.m_dgResult.Rows.Count > 0)
            {
                goto Label_001E;
            }
            goto Label_01BF;
        Label_001E:
            num = 0;
            goto Label_01A5;
        Label_0025:
            if (string.IsNullOrEmpty(this.m_dgResult.Rows[num].Cells["data_seq_int"].EditedFormattedValue.ToString()))
            {
                goto Label_014A;
            }
            avo = new clsLisQCDataVO();
            avo.m_dlbResult = Convert.ToDouble(this.m_dgResult.Rows[num].Cells["QCResult"].Value.ToString());
            avo.m_intConcentrationSeq = Convert.ToInt32(this.m_dgResult.Rows[num].Cells["concentration"].Value.ToString());
            avo.m_intQCBatchSeq = Convert.ToInt32(this.m_dgResult.Rows[num].Cells["Tag"].Value.ToString());
            avo.m_intSeq = Convert.ToInt32(this.m_dgResult.Rows[num].Cells["data_seq_int"].Value.ToString().Trim());
            this.m_lstDeletedVOS.Add(avo);
        Label_014A:
            this.m_dgResult.Rows[num].Cells["QCResult"].Value = DBNull.Value;
            this.m_dgResult.Rows[num].Cells["QCPlacement"].Value = DBNull.Value;
            num += 1;
        Label_01A5:
            if (num < this.m_dgResult.Rows.Count)
            {
                goto Label_0025;
            }
        Label_01BF:
            return;
        }

        private void m_btnSave_Click(object sender, EventArgs e)
        {
            long num;
            clsLisQCDataVO[] avoArray;
            clsLisQCDataVO[] avoArray2;
            int[] numArray;
            int[] numArray2;
            int num2;
            bool flag;
            num = 0L;
            avoArray = null;
            avoArray2 = null;
            numArray = null;
            numArray2 = null;
            if (this.m_lstAddedVOS != null && this.m_lstAddedVOS.Count > 0)
            {
                avoArray = this.m_lstAddedVOS.ToArray();
            }
            else
            {
                goto Label_003B;
            }
        Label_003B:
            if (this.m_lstChangedVOS != null && this.m_lstChangedVOS.Count > 0)
            {
                avoArray2 = this.m_lstChangedVOS.ToArray();
            }
            else
            {
                goto Label_0069;
            }
        Label_0069:
            if (this.m_lstDeletedVOS != null && this.m_lstDeletedVOS.Count > 0)
            {
                numArray = new int[this.m_lstDeletedVOS.Count];
                num2 = 0;
                goto Label_00C0;
            }
            else
            {
                goto Label_00D6;
            }
        Label_00A2:
            numArray[num2] = this.m_lstDeletedVOS[num2].m_intSeq;
            num2 += 1;
        Label_00C0:
            if (num2 < this.m_lstDeletedVOS.Count)
            {
                goto Label_00A2;
            }
        Label_00D6:
            long rec = (new weCare.Proxy.ProxyLis02()).Service.m_lngSaveAllQCData(avoArray, avoArray2, numArray, out numArray2);
            if (rec <= 0)
            {
                goto Label_0156;
            }
            if (numArray2 != null && numArray2.Length > 0)
            {
                this.m_lstAddedVOS.Clear();
            }
            else
            {
                goto Label_011B;
            }
        Label_011B:
            if (this.m_lstChangedVOS == null)
            {
                goto Label_0136;
            }
            this.m_lstChangedVOS.Clear();
        Label_0136:
            if (this.m_lstDeletedVOS == null)
            {
                goto Label_0153;
            }
            this.m_lstDeletedVOS.Clear();
        Label_0153:
            goto Label_016C;
        Label_0156:
            MessageBox.Show("保存失败", "质控结果提示");
        Label_016C:
            return;
        }

        private void m_cboDeviceInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.m_mthQCLot(); 
        }

        private void m_cmdQueryQCResult_Click(object sender, EventArgs e)
        {
            DataTable table;
            clsLisQCBatchSchVO hvo;
            int num;
            double num2;
            double num3;
            double num4;
            double num5;
            string str;
            string str2;
            string str3;
            string str4;
            string str5;
            DataColumn column;
            DataColumn column2;
            int num6;
            int num7;
            int num8;
            clsLisQCDataVO[] avoArray;
            int[] numArray;
            string str6;
            long num9;
            int num10;
            clsLisQCDataVO avo;
            bool flag;
            DateTime time;
            int num11;
            this.m_dgMonthResult.DataSource = null;
            this.m_dgResult.DataSource = null;
            this.m_lstAddedVOS.Clear();
            this.m_lstChangedVOS.Clear();
            this.m_lstDeletedVOS.Clear();
            table = null;
            this.m_dtQCSetResult = null;
            hvo = new clsLisQCBatchSchVO();
            num = 0;
            this.intSaveTytle = 0;
            num4 = 0.0;
            num5 = 0.0;
            str = null;
            hvo.m_strQCDevice = this.m_cboDeviceInfo.SelectedValue.ToString();
            if (!string.IsNullOrEmpty(this.m_cboQCLot.Text))
            {
                goto Label_00B8;
            }
            MessageBox.Show("请选择质控批号", "质控结果信息提示");
            goto Label_0797;
        Label_00B8:
            hvo.m_strQCSampleLotNO = this.m_cboQCLot.Text.ToString();
            str2 = this.m_dtDate.Value.ToString("yyyy-MM");
            str3 = DateTime.DaysInMonth(this.m_dtDate.Value.Year, this.m_dtDate.Value.Month).ToString();
            hvo.m_datQueryBegin = DateTime.Parse(str2 + "-01 00:00:00");
            hvo.m_datQueryEnd = DateTime.Parse(str2 + "-" + str3 + " 23:59:59");
            str4 = this.m_dtDate.Value.ToString("yyyy-MM-dd 00:00:00");
            str5 = this.m_dtDate.Value.ToString("yyyy-MM-dd 23:59:59");
            this.m_objContoller.m_mthQueryResult(hvo, str4, str5, out table, out this.m_dtQCSetResult);

            if (table != null && table.Rows.Count > 0)
            {
                column = null;
                column = this.m_dtQCSetResult.Columns.Add("placement", typeof(string));
                column2 = null;
                column2 = this.m_dtQCSetResult.Columns.Add("result_num", typeof(string));
                this.m_dtQCSetResult.Columns.Add("data_seq_int", typeof(string));
                num6 = 0;
                num7 = 0;
                goto Label_0414;
            }
            else
            {
                goto Label_0439;
            }
        Label_0239:
            num = Convert.ToInt32(table.Rows[num7]["qcbatch_seq_int"].ToString().Trim());
            double.TryParse(table.Rows[num7]["result_num"].ToString().Trim(), out num5);
            num8 = 0;
            goto Label_03F0;
        Label_0292:
            if (Convert.ToInt32(this.m_dtQCSetResult.Rows[num8]["qcbatch_seq_int"].ToString().Trim()) != num)
            {
                goto Label_03E9;
            }
            double.TryParse(this.m_dtQCSetResult.Rows[num8]["avg_num"].ToString().Trim(), out num3);
            double.TryParse(this.m_dtQCSetResult.Rows[num8]["sd_num"].ToString().Trim(), out num2);
            if (num2 == 0.0)
            {
                goto Label_034F;
            }
            num2 = 1.7976931348623157E+308;
        Label_034F:
            num4 = (num5 - num3) / num2;
            str = num4.ToString("0.00");
            this.m_dtQCSetResult.Rows[num8]["placement"] = str;
            this.m_dtQCSetResult.Rows[num8]["result_num"] = (double)num5;
            this.m_dtQCSetResult.Rows[num8]["data_seq_int"] = table.Rows[num7]["data_seq_int"].ToString().Trim();
            goto Label_040D;
        Label_03E9:
            num8 += 1;
        Label_03F0:
            if (num8 < this.m_dtQCSetResult.Rows.Count)
            {
                goto Label_0292;
            }
        Label_040D:
            num7 += 1;
        Label_0414:
            if (num7 < table.Rows.Count)
            {
                goto Label_0239;
            }
            this.intSaveTytle = 1;
            goto Label_0785;
        Label_0439:
            if (this.m_dtQCSetResult != null && this.m_dtQCSetResult.Rows.Count > 0 && table.Rows.Count > 0)
            {
                avoArray = null;
                numArray = new int[this.m_dtQCSetResult.Rows.Count];
                str6 = null;
                num9 = 0L;
                column = null;
                column = this.m_dtQCSetResult.Columns.Add("placement", typeof(string));
                column2 = null;
                column2 = this.m_dtQCSetResult.Columns.Add("result_num", typeof(string));
                this.m_dtQCSetResult.Columns.Add("data_seq_int", typeof(string));
                num10 = 0;
                goto Label_055C;
            }
            else
            {
                goto Label_0785;
            }
        Label_04FD:
            numArray[num10] = Convert.ToInt32(this.m_dtQCSetResult.Rows[num10]["qcbatch_seq_int"].ToString().Trim());
            str6 = this.m_dtQCSetResult.Rows[0]["devicesample_id_vchr"].ToString().Trim();
            num10 += 1;
        Label_055C:
            if (num10 < this.m_dtQCSetResult.Rows.Count)
            {
                goto Label_04FD;
            }
        (new weCare.Proxy.ProxyLis02()).Service.m_lngReceiveDeviceQCDataBySampleID(str6, str4, str5, numArray, out avoArray);
            if (avoArray != null && avoArray.Length > 0)
            {
                num7 = 0;
                goto Label_076B;
            }
            else
            {
                goto Label_077D;
            }
        Label_05B6:
            num5 = avoArray[num7].m_dlbResult;
            num8 = 0;
            goto Label_0747;
        Label_05CB:
            num6 = Convert.ToInt32(this.m_dtQCSetResult.Rows[num8]["qcbatch_seq_int"].ToString().Trim());
            if (num6 != avoArray[num7].m_intQCBatchSeq)
            {
                goto Label_0740;
            }
            avo = new clsLisQCDataVO();
            avo.m_intQCBatchSeq = num6;
            avo.m_dlbResult = num5;
            avo.m_intConcentrationSeq = avoArray[num7].m_intConcentrationSeq;
            avo.m_datQCDate = this.m_dtDate.Value.Date;
            double.TryParse(this.m_dtQCSetResult.Rows[num8]["avg_num"].ToString().Trim(), out num3);
            double.TryParse(this.m_dtQCSetResult.Rows[num8]["sd_num"].ToString().Trim(), out num2);
            if (num2 != 0.0)
            {
                goto Label_06D6;
            }
            num2 = 1.7976931348623157E+308;
        Label_06D6:
            num4 = (num5 - num3) / num2;
            str = num4.ToString("0.00");
            this.m_dtQCSetResult.Rows[num8]["placement"] = str;
            this.m_dtQCSetResult.Rows[num8]["result_num"] = (double)num5;
            this.m_lstAddedVOS.Add(avo);
            goto Label_0764;
        Label_0740:
            num8 += 1;
        Label_0747:
            if (num8 < this.m_dtQCSetResult.Rows.Count)
            {
                goto Label_05CB;
            }
        Label_0764:
            num7 += 1;
        Label_076B:
            if (num7 < avoArray.Length)
            {
                goto Label_05B6;
            }
        Label_077D:
            this.intSaveTytle = 2;
        Label_0785:
            this.m_dgResult.DataSource = this.m_dtQCSetResult;
        Label_0797:
            return;
        }

        private void m_dgResult_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            double num;
            double num2;
            double num3;
            double num4;
            string str;
            bool flag;
            clsLisQCDataVO avo;
            int num5;
            bool flag2;
            DateTime time;
            num = 0.0;
            num2 = 0.0;
            num3 = 0.0;
            num4 = 0.0;
            str = null;
            flag = false;
            if (this.m_dgResult.Rows.Count > 0)
            {
                goto Label_004E;
            }
            goto Label_0414;
        Label_004E:
            if (string.IsNullOrEmpty(this.m_dgResult.CurrentRow.Cells["QCResult"].EditedFormattedValue.ToString()))
            {
                goto Label_00B0;
            }
            num = Convert.ToDouble(this.m_dgResult.CurrentRow.Cells["QCResult"].Value.ToString().Trim());
            goto Label_00B6;
        Label_00B0:
            goto Label_0414;
        Label_00B6:
            if (!string.IsNullOrEmpty(this.m_dgResult.CurrentRow.Cells["QCTarget"].EditedFormattedValue.ToString().Trim()))
            {
                goto Label_00FB;
            }
            num2 = 0.0;
            goto Label_012C;
        Label_00FB:
            num2 = Convert.ToDouble(this.m_dgResult.CurrentRow.Cells["QCTarget"].EditedFormattedValue.ToString().Trim());
        Label_012C:
            if (!string.IsNullOrEmpty(this.m_dgResult.CurrentRow.Cells["QCDeviation"].EditedFormattedValue.ToString().Trim()))
            {
                goto Label_0171;
            }
            num3 = 1.7976931348623157E+308;
            goto Label_01A2;
        Label_0171:
            num3 = Convert.ToDouble(this.m_dgResult.CurrentRow.Cells["QCDeviation"].EditedFormattedValue.ToString().Trim());
        Label_01A2:
            num4 = (num - num2) / num3;
            str = num4.ToString("0.00");
            this.m_dgResult.CurrentRow.Cells["QCPlacement"].Value = str;
            avo = new clsLisQCDataVO();
            avo.m_dlbResult = num;
            avo.m_intConcentrationSeq = Convert.ToInt32(this.m_dgResult.CurrentRow.Cells["concentration"].Value.ToString());
            avo.m_intQCBatchSeq = Convert.ToInt32(this.m_dgResult.CurrentRow.Cells["Tag"].Value.ToString());
            avo.m_datQCDate = this.m_dtDate.Value.Date;
            if (!string.IsNullOrEmpty(this.m_dgResult.CurrentRow.Cells["data_seq_int"].EditedFormattedValue.ToString()))
            {
                goto Label_034E;
            }
            if (this.m_lstAddedVOS.Count <= 0)
            {
                goto Label_0338;
            }
            num5 = 0;
            goto Label_0305;
        Label_02B8:
            if (this.m_lstAddedVOS[num5].m_intQCBatchSeq != avo.m_intQCBatchSeq)
            {
                goto Label_02FE;
            }
            this.m_lstAddedVOS.RemoveAt(num5);
            this.m_lstAddedVOS.Add(avo);
            flag = true;
        Label_02FE:
            num5 += 1;
        Label_0305:
            if (num5 < this.m_lstAddedVOS.Count)
            {
                goto Label_02B8;
            }
            if (flag)
            {
                goto Label_0335;
            }
            this.m_lstAddedVOS.Add(avo);
            flag = false;
        Label_0335:
            goto Label_0348;
        Label_0338:
            this.m_lstAddedVOS.Add(avo);
        Label_0348:
            goto Label_0414;
        Label_034E:
            avo.m_intSeq = Convert.ToInt32(this.m_dgResult.CurrentRow.Cells["data_seq_int"].Value.ToString().Trim());
            if (this.m_lstChangedVOS.Count <= 0)
            {
                goto Label_0403;
            }
            num5 = 0;
            goto Label_03EB;
        Label_03A1:
            if (this.m_lstChangedVOS[num5].m_intQCBatchSeq != avo.m_intQCBatchSeq)
            {
                goto Label_03E4;
            }
            this.m_lstChangedVOS.RemoveAt(num5);
            this.m_lstChangedVOS.Add(avo);
        Label_03E4:
            num5 += 1;
        Label_03EB:
            if (num5 < this.m_lstChangedVOS.Count)
            {
                goto Label_03A1;
            }
            goto Label_0413;
        Label_0403:
            this.m_lstChangedVOS.Add(avo);
        Label_0413:;
        Label_0414:
            return;
        }

        private void m_dgResult_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == this.m_dgResult.Columns["QCResult"].Index)
            {
                this.m_dgResult.Rows[e.RowIndex].ErrorText = "";
                if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
                {
                    return;
                }
                try
                {
                    decimal num = 0;
                    decimal.TryParse(e.FormattedValue.ToString(), out num);
                }
                catch
                {
                    e.Cancel = true;
                    this.m_dgResult.Rows[e.RowIndex].ErrorText = "输入的不是数字";
                }
            }
        }

        private void m_dgResult_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            return;
        }

        private void m_dgResult_SelectionChanged(object sender, EventArgs e)
        {
            clsLisQCBatchSchVO hvo;
            string str;
            string str2;
            DataTable table;
            List<clsLisQCConcentrationVO> list;
            clsLisQCConcentrationVO nvo;
            DataColumn column;
            double num;
            double num2;
            string str3;
            int num3;
            bool flag;
            DateTime time;
            int num4;
            hvo = new clsLisQCBatchSchVO();
            if (this.m_dgResult.CurrentRow != null)
            {
                goto Label_0024;
            }
            goto Label_0243;
        Label_0024:
            hvo.m_intQCBatchSeq = Convert.ToInt32(this.m_dgResult.CurrentRow.Cells["Tag"].Value.ToString());
            str = this.m_dtDate.Value.ToString("yyyy-MM");
            str2 = DateTime.DaysInMonth(this.m_dtDate.Value.Year, this.m_dtDate.Value.Month).ToString();
            hvo.m_datQueryBegin = DateTime.Parse(str + "-01 00:00:00");
            hvo.m_datQueryEnd = DateTime.Parse(str + "-" + str2 + " 23:59:59");
            table = null;
            this.m_objContoller.m_mthQueryItemQCResult(hvo, out table);
            if (table != null && table.Rows.Count > 0)
            {
                list = null;
                nvo = null;
                this.m_objContoller.m_lngQueryQCInfo(hvo.m_intQCBatchSeq, out list);
                if (list != null && list.Count > 0)
                {
                    nvo = list[0];
                }
                else
                {
                    goto Label_0146;
                }
            }
            else
            {
                goto Label_0243;
            }
        Label_0146:
            column = null;
            column = table.Columns.Add("placement", typeof(string));
            num = 0.0;
            num2 = 0.0;
            str3 = null;
            if (nvo.m_dblSD != 0.0)
            {
                goto Label_01AB;
            }
            nvo.m_dblSD = 1.7976931348623157E+308;
        Label_01AB:
            num3 = 0;
            goto Label_021D;
        Label_01B0:
            double.TryParse(table.Rows[num3]["result_num"].ToString().Trim(), out num);
            num2 = (num - nvo.m_dblAVG) / nvo.m_dblSD;
            str3 = num2.ToString("0.00");
            table.Rows[num3]["placement"] = str3;
            num3 += 1;
        Label_021D:
            if (num3 < table.Rows.Count)
            {
                goto Label_01B0;
            }
            this.m_dgMonthResult.DataSource = table;
        Label_0243:
            return;
        }

        private void m_dtDate_ValueChanged(object sender, EventArgs e)
        {
            this.m_mthQCLot();
        }

        public void m_mthDataFilter(double p_dblX, double p_dblSD, ref List<double> p_lisDblData)
        {
            return;
        }

        private void m_mthInfo()
        {
            string str = Dns.GetHostName();
            (new weCare.Proxy.ProxyLis03()).Service.m_lngQueryDeviceInfo(str, out this.dtDeviceInfo);
            if (this.dtDeviceInfo != null && this.dtDeviceInfo.Rows.Count > 0)
            {
                this.m_cboDeviceInfo.ValueMember = "deviceid_chr";
                this.m_cboDeviceInfo.DisplayMember = "devicename_vchr";
                this.m_cboDeviceInfo.DataSource = this.dtDeviceInfo;
            }
            else
            {
                this.m_objContoller.m_mthGetAllCheckItemInfo(out this.dtDeviceInfo);
                if (this.dtDeviceInfo != null && this.dtDeviceInfo.Rows.Count > 0)
                {
                    this.m_cboDeviceInfo.ValueMember = "deviceid_chr";
                    this.m_cboDeviceInfo.DisplayMember = "devicename_vchr";
                    this.m_cboDeviceInfo.DataSource = this.dtDeviceInfo;
                }
            }
        }

        private void m_mthQCLot()
        {
            DataTable table = null;
            clsLisQCBatchSchVO hvo = new clsLisQCBatchSchVO();
            string str = this.m_dtDate.Value.ToString("yyyy-MM");
            string str2 = DateTime.DaysInMonth(this.m_dtDate.Value.Year, this.m_dtDate.Value.Month).ToString();
            hvo.m_datQueryBegin = DateTime.Parse(str + "-01 00:00:00");
            hvo.m_datQueryEnd = DateTime.Parse(str + "-" + str2 + " 23:59:59");
            hvo.m_strQCDevice = this.m_cboDeviceInfo.SelectedValue.ToString().Trim();
            (new weCare.Proxy.ProxyLis03()).Service.m_lngQueryQCLot(hvo, out table);
            if (table != null && table.Rows.Count > 0)
            {
                this.m_cboQCLot.ValueMember = "qcsample_lotno_vchr";
                this.m_cboQCLot.DisplayMember = "qcsample_lotno_vchr";
                this.m_cboQCLot.DataSource = table;
            }
        }
    }
}
