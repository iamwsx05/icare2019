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
using com.digitalwave.iCare.gui.LIS.QC.Control;

namespace com.digitalwave.iCare.gui.LIS
{
    public class frmQCSetup : frmMDI_Child_Base
    {
        // Fields
        private DataGridViewTextBoxColumn begindate;
        private DataGridViewTextBoxColumn BValue;
        private ctlLISDeviceComboBox cboApparatusType;
        private ctlConcentrationCombox cboConcentration;
        private ctlCheckMethodCombox cboExamineMethod;
        private ctlVendorCombox cboManOrigin;
        private DataGridViewTextBoxColumn checkitemsid;
        internal ButtonXP cmdExit;
        internal ButtonXP cmdSave;
        private DataGridViewTextBoxColumn code;
        private IContainer components;
        private DataGridViewTextBoxColumn Concentration;
        private DataGridViewTextBoxColumn cv;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private DataGridView dgvSetup;
        private DataTable dtbResult;
        private DateTimePicker dtpBegindate;
        private DateTimePicker dtpEnddate;
        private DataGridViewTextBoxColumn enddate;
        private GroupBox groupBox1;
        private DataGridViewTextBoxColumn groupnum;
        private int idConcentration;
        private DataGridViewTextBoxColumn intSeq;
        private DataGridViewTextBoxColumn itemsname;
        private Label lblApparatusType;
        private Label lblBegindate;
        private Label lblBValue;
        private Label lblCode;
        private Label lblConcentration;
        private Label lblCV;
        private Label lblEnddate;
        private Label lblExamineMethod;
        private Label lblItemsname;
        private Label lblManGroupNum;
        private Label lblManOrigin;
        private Label lblNotice;
        private Label lblSD;
        private Label lblSortNum;
        private Label lblUnit;
        private Label lblWaveLength;
        private Label m_lblSeq;
        internal List<clsLisQCBatchVO> m_objBatchSets;
        internal List<clsLisQCConcentrationVO> m_objConcentrations;
        //private clsDcl_QCBatchManage m_objDomain;
        private DataGridViewTextBoxColumn m_strDeviceId;
        private string m_strDeviceSampleID;
        private TextBox m_txtCheckItemsID;
        private TextBox m_txtDeviceId;
        private DataGridViewTextBoxColumn method;
        private clsLisQCConcentrationVO objConcentrations;
        private Panel panel1;
        private Panel panel2;
        private DataGridViewTextBoxColumn sd;
        private DataGridViewTextBoxColumn sortNum;
        private DataGridViewTextBoxColumn source;
        private TextBox txtBValue;
        private TextBox txtCode;
        private TextBox txtCV;
        private TextBox txtGroupNum;
        private TextBox txtItems;
        private TextBox txtSD;
        private TextBox txtSeq;
        private TextBox txtSortNum;
        private TextBox txtUnit;
        private TextBox txtWaveLength;
        private DataGridViewTextBoxColumn type;
        private DataGridViewTextBoxColumn unit;
        private DataGridViewTextBoxColumn wavelength;

        // Methods
        public frmQCSetup()
        {
            this.components = null;
            //this.m_objDomain = new clsDcl_QCBatchManage();
            this.idConcentration = 0;
            this.m_strDeviceSampleID = null;
            this.InitializeComponent();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            if (this.dtbResult != null && this.dtbResult.Rows.Count > 0)
            {
                long num = (new weCare.Proxy.ProxyLis02()).Service.m_lngInsertBatchSet(this.m_objBatchSets, this.m_objConcentrations);
                if (num > 0L)
                {
                    MessageBox.Show("保存成功!", "质控管理", MessageBoxButtons.OK);
                    base.DialogResult = DialogResult.OK;
                    base.Close();
                }
                else
                {
                    MessageBox.Show("保存失败！", "质控管理", MessageBoxButtons.OK);
                }
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtSeq.Text))
            {
                MessageBox.Show("请选择一个质控项目！");
            }
            else
            {
                this.idConcentration = this.cboConcentration.SelectedIndex;
                this.m_strDeviceSampleID = this.txtCode.Text;
                Cursor.Current = Cursors.WaitCursor;
                if (this.txtSortNum.Text == "")
                {
                    MessageBox.Show("排序号没填!");
                    this.txtSortNum.Focus();
                }
                else
                {
                    if (string.IsNullOrEmpty(this.cboConcentration.Text) || this.cboConcentration.Value < 0)
                    {
                        MessageBox.Show("浓度没填!");
                        this.cboConcentration.Focus();
                    }
                    else
                    {
                        if (this.txtBValue.Text.Trim() != string.Empty)
                        {
                            double num;
                            if (!double.TryParse(this.txtBValue.Text.Trim(), out num))
                            {
                                MessageBox.Show("请输入数字!", this.lblBValue.Text);
                                this.txtBValue.Focus();
                                this.txtBValue.SelectAll();
                                return;
                            }
                        }
                        if (this.txtSD.Text.Trim() != string.Empty)
                        {
                            double num;
                            if (!double.TryParse(this.txtSD.Text.Trim(), out num))
                            {
                                MessageBox.Show("请输入数字!", this.lblCV.Text);
                                this.txtSD.Focus();
                                this.txtSD.SelectAll();
                                return;
                            }
                        }
                        if (this.txtCV.Text.Trim() != string.Empty)
                        {
                            double num;
                            if (!double.TryParse(this.txtCV.Text.Trim(), out num))
                            {
                                MessageBox.Show("请输入数字!", this.lblCV.Text);
                                this.txtCV.Focus();
                                this.txtCV.SelectAll();
                                return;
                            }
                        }
                        int num2 = 0;
                        int.TryParse(this.txtSeq.Text, out num2);
                        int num3 = -1;
                        for (int i = 0; i < this.m_objBatchSets.Count; i++)
                        {
                            if (this.m_objBatchSets[i].m_intSeq == num2)
                            {
                                num3 = i;
                                break;
                            }
                        }
                        if (num3 < 0)
                        {
                            MessageBox.Show("没有找到与之相对应的质控批！");
                            Cursor.Current = Cursors.Default;
                        }
                        else
                        {
                            this.m_objBatchSets[num3].m_intSeq = int.Parse(this.txtSeq.Text.ToString().Trim());
                            this.m_objBatchSets[num3].m_strDeviceId = this.cboApparatusType.SelectedValue.ToString().Trim();
                            this.m_objBatchSets[num3].m_strCheckItemName = this.txtItems.Text.ToString().Trim();
                            this.m_objBatchSets[num3].m_strSampleLotNo = this.txtGroupNum.Text.ToString().Trim();
                            this.m_objBatchSets[num3].m_strSampleSource = this.cboManOrigin.Text.ToString().Trim();
                            this.m_objBatchSets[num3].m_strCheckmethodName = this.cboExamineMethod.Text.ToString().Trim();
                            double dblWaveLength = 0.0;
                            double.TryParse(this.txtWaveLength.Text.ToString(), out dblWaveLength);
                            this.m_objBatchSets[num3].m_dblWaveLength = dblWaveLength;
                            this.m_objBatchSets[num3].m_strResultUnit = this.txtUnit.Text.ToString().Trim();
                            this.m_objBatchSets[num3].m_dtBegin = this.dtpBegindate.Value;
                            this.m_objBatchSets[num3].m_dtEnd = this.dtpEnddate.Value;
                            this.m_objBatchSets[num3].m_strSortNum = this.txtSortNum.Text.ToString().Trim();
                            this.m_objBatchSets[num3].m_strCheckItemId = this.m_txtCheckItemsID.Text.ToString();
                            this.m_objBatchSets[num3].m_strDeviceId = this.m_txtDeviceId.Text.ToString();
                            num3 = -1;
                            for (int i = 0; i < this.m_objConcentrations.Count; i++)
                            {
                                if (this.m_objConcentrations[i].m_intQCBatchSeq == num2)
                                {
                                    num3 = i;
                                    break;
                                }
                            }
                            if (num3 < 0)
                            {
                                this.objConcentrations = new clsLisQCConcentrationVO();
                                this.m_objConcentrations.Add(this.objConcentrations);
                            }
                            else
                            {
                                this.objConcentrations = this.m_objConcentrations[num3];
                            }
                            this.objConcentrations.m_intQCBatchSeq = num2;
                            this.objConcentrations.m_strConcentration = this.cboConcentration.Text.ToString().Trim();
                            if (!string.IsNullOrEmpty(this.objConcentrations.m_strConcentration))
                            {
                                this.objConcentrations.m_intConcentrationSeq = this.cboConcentration.Value;
                            }
                            this.objConcentrations.m_strDeviceSampleId = this.txtCode.Text.ToString().Trim();
                            double num4 = 0.0;
                            double.TryParse(this.txtBValue.Text.ToString(), out num4);
                            this.objConcentrations.m_dblAVG = num4;
                            double.TryParse(this.txtSD.Text.ToString(), out num4);
                            this.objConcentrations.m_dblSD = num4;
                            double.TryParse(this.txtCV.Text.ToString(), out num4);
                            this.objConcentrations.m_dblCV = num4;
                            this.CreateDataTable(this.m_objBatchSets, this.m_objConcentrations);
                            this.m_mthReset();
                            Cursor.Current = Cursors.Default;
                        }
                    }
                }
            }
        }

        public void CreateDataTable(List<clsLisQCBatchVO> m_objBatchSets, List<clsLisQCConcentrationVO> m_objConcentrations)
        {
            DataColumn[] columns = new DataColumn[]{new DataColumn("itemsname", typeof(string)),
                                                        new DataColumn("unit", typeof(string)),
                                                        new DataColumn("sortnum", typeof(int)),
                                                        new DataColumn("method", typeof(string)),
                                                        new DataColumn("type", typeof(string)),
                                                        new DataColumn("wavelength", typeof(string)),
                                                        new DataColumn("source", typeof(string)),
                                                        new DataColumn("groupnum", typeof(string)),
                                                        new DataColumn("Concentration", typeof(string)),
                                                        new DataColumn("code", typeof(string)),
                                                        new DataColumn("begindate", typeof(string)),
                                                        new DataColumn("enddate", typeof(string)),
                                                        new DataColumn("BValue", typeof(string)),
                                                        new DataColumn("sd", typeof(string)),
                                                        new DataColumn("cv", typeof(string)),
                                                        new DataColumn("intSeq", typeof(string)),
                                                        new DataColumn("checkitemsid", typeof(string)),
                                                        new DataColumn("m_strDeviceId", typeof(string)) };
            this.dtbResult = new DataTable();
            this.dtbResult.Columns.AddRange(columns);
            for (int i = 0; i < m_objBatchSets.Count; i++)
            {
                DataRow dr = this.dtbResult.NewRow();
                dr["itemsname"] = m_objBatchSets[i].m_strCheckItemName;
                dr["unit"] = m_objBatchSets[i].m_strResultUnit;
                dr["sortnum"] = m_objBatchSets[i].m_strSortNum;
                dr["method"] = m_objBatchSets[i].m_strCheckmethodName;
                dr["type"] = m_objBatchSets[i].m_strDeviceName;
                dr["wavelength"] = m_objBatchSets[i].m_dblWaveLength;
                dr["source"] = m_objBatchSets[i].m_strSampleSource;
                dr["groupnum"] = m_objBatchSets[i].m_strSampleLotNo;
                dr["begindate"] = m_objBatchSets[i].m_dtBegin.ToShortDateString();
                dr["enddate"] = m_objBatchSets[i].m_dtEnd.ToShortDateString();
                dr["intSeq"] = m_objBatchSets[i].m_intSeq;
                dr["checkitemsid"] = m_objBatchSets[i].m_strCheckItemId;
                dr["m_strDeviceId"] = m_objBatchSets[i].m_strDeviceId;
                for (int j = 0; j < m_objConcentrations.Count; j++)
                {
                    if (m_objBatchSets[i].m_intSeq == m_objConcentrations[j].m_intQCBatchSeq)
                    {
                        dr["Concentration"] = m_objConcentrations[j].m_strConcentration;
                        dr["code"] = m_objConcentrations[j].m_strDeviceSampleId;
                        dr["BValue"] = m_objConcentrations[j].m_dblAVG;
                        dr["sd"] = m_objConcentrations[j].m_dblSD;
                        dr["cv"] = m_objConcentrations[j].m_dblCV;
                        this.dtbResult.Rows.Add(dr);
                        break;
                    }
                }
                if (!(dr["Concentration"].ToString() != ""))
                {
                    dr["Concentration"] = "";
                    dr["code"] = "";
                    dr["BValue"] = "";
                    dr["sd"] = "";
                    dr["cv"] = "";
                    this.dtbResult.Rows.Add(dr);
                }
            }
            this.dtbResult.DefaultView.Sort = "sortnum asc";
            this.dgvSetup.DataSource = this.dtbResult;
        }

        private void dgvSetup_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgvSetup.SelectedRows.Count > 0)
            {
                DataRow row = ((DataRowView)this.dgvSetup.SelectedRows[0].DataBoundItem).Row;
                this.txtSeq.Text = row["intSeq"].ToString();
                this.txtItems.Text = row["itemsname"].ToString();
                this.txtBValue.Text = row["Bvalue"].ToString();
                this.txtCode.Text = row["code"].ToString();
                this.txtCV.Text = row["cv"].ToString();
                this.txtGroupNum.Text = row["groupnum"].ToString();
                this.txtSD.Text = row["sd"].ToString();
                this.txtSortNum.Text = row["sortnum"].ToString();
                this.txtUnit.Text = row["unit"].ToString();
                this.txtWaveLength.Text = row["wavelength"].ToString();
                this.cboApparatusType.Text = row["type"].ToString();
                if (string.IsNullOrEmpty(row["Concentration"].ToString().Trim()))
                {
                    this.cboConcentration.SelectedIndex = this.idConcentration;
                }
                else
                {
                    this.cboConcentration.Text = row["Concentration"].ToString();
                }
                if (string.IsNullOrEmpty(row["code"].ToString().Trim()))
                {
                    this.txtCode.Text = this.m_strDeviceSampleID;
                }
                else
                {
                    this.txtCode.Text = row["code"].ToString();
                }
                this.m_txtCheckItemsID.Text = row["checkitemsid"].ToString();
                this.cboExamineMethod.Text = row["method"].ToString();
                this.cboManOrigin.Text = row["source"].ToString();
                this.m_txtDeviceId.Text = row["m_strDeviceId"].ToString();
                DateTime value;
                DateTime.TryParse(row["begindate"].ToString(), out value);
                DateTime value2;
                DateTime.TryParse(row["enddate"].ToString(), out value2);
                try
                {
                    this.dtpBegindate.Value = value;
                    this.dtpEnddate.Value = value2;
                }
                catch
                {
                    this.dtpBegindate.Value = DateTime.Now;
                    this.dtpEnddate.Value = DateTime.Now;
                }
            }
        }

        private void dgvSetup_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dgvSetup.SelectedRows.Count >= 1)
            {
                DataRow row = ((DataRowView)this.dgvSetup.SelectedRows[0].DataBoundItem).Row;
                this.txtSeq.Text = row["intSeq"].ToString();
                this.txtItems.Text = row["itemsname"].ToString();
                this.txtBValue.Text = row["Bvalue"].ToString();
                this.txtCode.Text = row["code"].ToString();
                this.txtCV.Text = row["cv"].ToString();
                this.txtGroupNum.Text = row["groupnum"].ToString();
                this.txtSD.Text = row["sd"].ToString();
                this.txtSortNum.Text = row["sortnum"].ToString();
                this.txtUnit.Text = row["unit"].ToString();
                this.txtWaveLength.Text = row["wavelength"].ToString();
                this.cboApparatusType.Text = row["type"].ToString();
                this.cboConcentration.Text = row["Concentration"].ToString();
                this.txtCode.Text = row["code"].ToString();
                this.m_txtCheckItemsID.Text = row["checkitemsid"].ToString();
                this.cboExamineMethod.Text = row["method"].ToString();
                this.cboManOrigin.Text = row["source"].ToString();
                this.m_txtDeviceId.Text = row["m_strDeviceId"].ToString();
                DateTime value;
                DateTime.TryParse(row["begindate"].ToString(), out value);
                DateTime value2;
                DateTime.TryParse(row["enddate"].ToString(), out value2);
                try
                {
                    this.dtpBegindate.Value = value;
                    this.dtpEnddate.Value = value2;
                }
                catch
                {
                    this.dtpBegindate.Value = DateTime.Now;
                    this.dtpEnddate.Value = DateTime.Now;
                }
            }
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private void frmQCSetup_KeyDown(object sender, KeyEventArgs e)
        {
            this.m_mthShortCutKey(e.KeyCode);
            this.m_mthSetKeyTab(e);
        }

        private void frmQCSetup_Load(object sender, EventArgs e)
        {
            this.dgvSetup.AutoGenerateColumns = false;
            this.m_mthSetEnter2Tab(new Control[] { this.dgvSetup, this.lblNotice, this.txtItems, this.txtGroupNum, this.txtSeq });
        }

        private void InitializeComponent()
        {
            DataGridViewColumn[] columnArray;
            this.groupBox1 = new GroupBox();
            this.panel2 = new Panel();
            this.m_lblSeq = new Label();
            this.m_txtDeviceId = new TextBox();
            this.m_txtCheckItemsID = new TextBox();
            this.txtSeq = new TextBox();
            this.lblNotice = new Label();
            this.txtItems = new TextBox();
            this.lblItemsname = new Label();
            this.cboApparatusType = new ctlLISDeviceComboBox();
            this.cboConcentration = new ctlConcentrationCombox();
            this.cboExamineMethod = new ctlCheckMethodCombox();
            this.cboManOrigin = new ctlVendorCombox();
            this.txtCode = new TextBox();
            this.cmdExit = new ButtonXP();
            this.cmdSave = new ButtonXP();
            this.txtCV = new TextBox();
            this.lblCV = new Label();
            this.txtSD = new TextBox();
            this.lblSD = new Label();
            this.txtBValue = new TextBox();
            this.lblBValue = new Label();
            this.lblEnddate = new Label();
            this.lblBegindate = new Label();
            this.dtpEnddate = new DateTimePicker();
            this.dtpBegindate = new DateTimePicker();
            this.lblCode = new Label();
            this.lblConcentration = new Label();
            this.txtGroupNum = new TextBox();
            this.lblManGroupNum = new Label();
            this.lblManOrigin = new Label();
            this.txtWaveLength = new TextBox();
            this.lblWaveLength = new Label();
            this.lblApparatusType = new Label();
            this.lblExamineMethod = new Label();
            this.txtSortNum = new TextBox();
            this.lblSortNum = new Label();
            this.txtUnit = new TextBox();
            this.lblUnit = new Label();
            this.panel1 = new Panel();
            this.dgvSetup = new DataGridView();
            this.itemsname = new DataGridViewTextBoxColumn();
            this.unit = new DataGridViewTextBoxColumn();
            this.sortNum = new DataGridViewTextBoxColumn();
            this.method = new DataGridViewTextBoxColumn();
            this.type = new DataGridViewTextBoxColumn();
            this.wavelength = new DataGridViewTextBoxColumn();
            this.source = new DataGridViewTextBoxColumn();
            this.groupnum = new DataGridViewTextBoxColumn();
            this.Concentration = new DataGridViewTextBoxColumn();
            this.code = new DataGridViewTextBoxColumn();
            this.begindate = new DataGridViewTextBoxColumn();
            this.enddate = new DataGridViewTextBoxColumn();
            this.BValue = new DataGridViewTextBoxColumn();
            this.sd = new DataGridViewTextBoxColumn();
            this.cv = new DataGridViewTextBoxColumn();
            this.intSeq = new DataGridViewTextBoxColumn();
            this.checkitemsid = new DataGridViewTextBoxColumn();
            this.m_strDeviceId = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn17 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn18 = new DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSetup)).BeginInit();
            this.SuspendLayout();
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Dock = DockStyle.Bottom;
            this.groupBox1.Location = new Point(0, 0x113);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x385, 0xdf);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "质控设置";
            this.panel2.Controls.Add(this.m_lblSeq);
            this.panel2.Controls.Add(this.m_txtDeviceId);
            this.panel2.Controls.Add(this.m_txtCheckItemsID);
            this.panel2.Controls.Add(this.txtSeq);
            this.panel2.Controls.Add(this.lblNotice);
            this.panel2.Controls.Add(this.txtItems);
            this.panel2.Controls.Add(this.lblItemsname);
            this.panel2.Controls.Add(this.cboApparatusType);
            this.panel2.Controls.Add(this.cboConcentration);
            this.panel2.Controls.Add(this.cboExamineMethod);
            this.panel2.Controls.Add(this.cboManOrigin);
            this.panel2.Controls.Add(this.txtCode);
            this.panel2.Controls.Add(this.cmdExit);
            this.panel2.Controls.Add(this.cmdSave);
            this.panel2.Controls.Add(this.txtCV);
            this.panel2.Controls.Add(this.lblCV);
            this.panel2.Controls.Add(this.txtSD);
            this.panel2.Controls.Add(this.lblSD);
            this.panel2.Controls.Add(this.txtBValue);
            this.panel2.Controls.Add(this.lblBValue);
            this.panel2.Controls.Add(this.lblEnddate);
            this.panel2.Controls.Add(this.lblBegindate);
            this.panel2.Controls.Add(this.dtpEnddate);
            this.panel2.Controls.Add(this.dtpBegindate);
            this.panel2.Controls.Add(this.lblCode);
            this.panel2.Controls.Add(this.lblConcentration);
            this.panel2.Controls.Add(this.txtGroupNum);
            this.panel2.Controls.Add(this.lblManGroupNum);
            this.panel2.Controls.Add(this.lblManOrigin);
            this.panel2.Controls.Add(this.txtWaveLength);
            this.panel2.Controls.Add(this.lblWaveLength);
            this.panel2.Controls.Add(this.lblApparatusType);
            this.panel2.Controls.Add(this.lblExamineMethod);
            this.panel2.Controls.Add(this.txtSortNum);
            this.panel2.Controls.Add(this.lblSortNum);
            this.panel2.Controls.Add(this.txtUnit);
            this.panel2.Controls.Add(this.lblUnit);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(3, 0x13);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x37f, 0xc9);
            this.panel2.TabIndex = 0;
            this.m_lblSeq.AutoSize = true;
            this.m_lblSeq.Location = new Point(12, 0x4f);
            this.m_lblSeq.Name = "m_lblSeq";
            this.m_lblSeq.Size = new Size(0x4d, 14);
            this.m_lblSeq.TabIndex = 0x12;
            this.m_lblSeq.Text = "质控批序号";
            this.m_txtDeviceId.Location = new Point(0x2c7, 0x43);
            this.m_txtDeviceId.Name = "m_txtDeviceId";
            this.m_txtDeviceId.ReadOnly = true;
            this.m_txtDeviceId.Size = new Size(0x35, 0x17);
            this.m_txtDeviceId.TabIndex = 14;
            this.m_txtDeviceId.Visible = false;
            this.m_txtCheckItemsID.Location = new Point(0x2fd, 0x43);
            this.m_txtCheckItemsID.Name = "m_txtCheckItemsID";
            this.m_txtCheckItemsID.ReadOnly = true;
            this.m_txtCheckItemsID.Size = new Size(0x35, 0x17);
            this.m_txtCheckItemsID.TabIndex = 0x49;
            this.m_txtCheckItemsID.Visible = false;
            this.txtSeq.Location = new Point(0x5f, 0x49);
            this.txtSeq.Name = "txtSeq";
            this.txtSeq.ReadOnly = true;
            this.txtSeq.Size = new Size(0x6f, 0x17);
            this.txtSeq.TabIndex = 0x18;
            this.lblNotice.AutoSize = true;
            this.lblNotice.ForeColor = Color.FromArgb(0xc0, 0, 0xc0);
            this.lblNotice.Location = new Point(0x2a6, 0x93);
            this.lblNotice.Name = "lblNotice";
            this.lblNotice.Size = new Size(0xbd, 14);
            this.lblNotice.TabIndex = 0x23;
            this.lblNotice.Text = "要新增质控请到新增项目界面";
            this.lblNotice.MouseLeave += new EventHandler(this.lblNotice_MouseLeave);
            this.lblNotice.MouseEnter += new EventHandler(this.lblNotice_MouseEnter);
            this.txtItems.Location = new Point(0x5f, 10);
            this.txtItems.MaxLength = 0x20;
            this.txtItems.Name = "txtItems";
            this.txtItems.ReadOnly = true;
            this.txtItems.Size = new Size(0x6f, 0x17);
            this.txtItems.TabIndex = 0x16;
            this.lblItemsname.AutoSize = true;
            this.lblItemsname.Location = new Point(0x3b, 0x13);
            this.lblItemsname.Name = "lblItemsname";
            this.lblItemsname.Size = new Size(0x23, 14);
            this.lblItemsname.TabIndex = 0x10;
            this.lblItemsname.Text = "项目";
            this.cboApparatusType.DisplayMember = "DEVICENAME_VCHR";
            this.cboApparatusType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboApparatusType.FormattingEnabled = true;
            this.cboApparatusType.Location = new Point(0x20c, 0x5b);
            this.cboApparatusType.Name = "cboApparatusType";
            this.cboApparatusType.Size = new Size(120, 0x16);
            this.cboApparatusType.TabIndex = 10;
            this.cboApparatusType.ValueMember = "DEVICEID_CHR";
            this.cboConcentration.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboConcentration.FormattingEnabled = true;
            this.cboConcentration.Location = new Point(0x13a, 0x19);
            this.cboConcentration.Name = "cboConcentration";
            this.cboConcentration.Size = new Size(0x63, 0x16);
            this.cboConcentration.TabIndex = 3;
            this.cboConcentration.Value = -2147483648;
            this.cboExamineMethod.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboExamineMethod.FormattingEnabled = true;
            this.cboExamineMethod.Location = new Point(0x20c, 0x7b);
            this.cboExamineMethod.Name = "cboExamineMethod";
            this.cboExamineMethod.Size = new Size(120, 0x16);
            this.cboExamineMethod.TabIndex = 11;
            this.cboManOrigin.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboManOrigin.FormattingEnabled = true;
            this.cboManOrigin.Location = new Point(0x20c, 0x99);
            this.cboManOrigin.Name = "cboManOrigin";
            this.cboManOrigin.Size = new Size(120, 0x16);
            this.cboManOrigin.TabIndex = 12;
            this.txtCode.Location = new Point(0x13a, 0x3a);
            this.txtCode.MaxLength = 0x20;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new Size(0x63, 0x17);
            this.txtCode.TabIndex = 4;
            this.cmdExit.Anchor = 0;
            this.cmdExit.BackColor = Color.FromArgb(0, 0xec, 0xe9, 0xd8);
            this.cmdExit.DefaultScheme = true;
            this.cmdExit.DialogResult = 0;
            this.cmdExit.Hint = "";
            this.cmdExit.Location = new Point(0x2c7, 0x5e);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Scheme = 0;
            this.cmdExit.Size = new Size(0x6b, 0x21);
            this.cmdExit.TabIndex = 15;
            this.cmdExit.Text = "确定(ESC)";
            this.cmdExit.Click += new EventHandler(this.cmdExit_Click);
            this.cmdSave.Anchor = 0;
            this.cmdSave.BackColor = Color.FromArgb(0, 0xec, 0xe9, 0xd8);
            this.cmdSave.DefaultScheme = true;
            this.cmdSave.DialogResult = 0;
            this.cmdSave.Hint = "";
            this.cmdSave.Location = new Point(0x2c7, 0x20);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Scheme = 0;
            this.cmdSave.Size = new Size(0x6b, 0x21);
            this.cmdSave.TabIndex = 13;
            this.cmdSave.Text = "保存(F3)";
            this.cmdSave.Click += new EventHandler(this.cmdSave_Click);
            this.txtCV.Location = new Point(0x5f, 0xa2);
            this.txtCV.MaxLength = 0x20;
            this.txtCV.Name = "txtCV";
            this.txtCV.Size = new Size(0x6f, 0x17);
            this.txtCV.TabIndex = 2;
            this.txtCV.Enter += new EventHandler(this.txtCV_Enter);
            this.lblCV.AutoSize = true;
            this.lblCV.Location = new Point(30, 0xab);
            this.lblCV.Name = "lblCV";
            this.lblCV.Size = new Size(0x3f, 14);
            this.lblCV.TabIndex = 0x15;
            this.lblCV.Text = "变异系数";
            this.txtSD.Location = new Point(0x5f, 0x85);
            this.txtSD.MaxLength = 0x20;
            this.txtSD.Name = "txtSD";
            this.txtSD.Size = new Size(0x6f, 0x17);
            this.txtSD.TabIndex = 1;
            this.lblSD.AutoSize = true;
            this.lblSD.Location = new Point(0x2c, 0x8e);
            this.lblSD.Name = "lblSD";
            this.lblSD.Size = new Size(0x31, 14);
            this.lblSD.TabIndex = 20;
            this.lblSD.Text = "标准差";
            this.txtBValue.Location = new Point(0x5f, 0x68);
            this.txtBValue.MaxLength = 0x20;
            this.txtBValue.Name = "txtBValue";
            this.txtBValue.Size = new Size(0x6f, 0x17);
            this.txtBValue.TabIndex = 0;
            this.lblBValue.AutoSize = true;
            this.lblBValue.Location = new Point(0x3b, 0x71);
            this.lblBValue.Name = "lblBValue";
            this.lblBValue.Size = new Size(0x23, 14);
            this.lblBValue.TabIndex = 0x13;
            this.lblBValue.Text = "靶值";
            this.lblEnddate.AutoSize = true;
            this.lblEnddate.Location = new Point(0xf8, 160);
            this.lblEnddate.Name = "lblEnddate";
            this.lblEnddate.Size = new Size(0x3f, 14);
            this.lblEnddate.TabIndex = 0x1d;
            this.lblEnddate.Text = "结束日期";
            this.lblBegindate.AutoSize = true;
            this.lblBegindate.Location = new Point(250, 0x81);
            this.lblBegindate.Name = "lblBegindate";
            this.lblBegindate.Size = new Size(0x3f, 14);
            this.lblBegindate.TabIndex = 0x1c;
            this.lblBegindate.Text = "开始日期";
            this.dtpEnddate.CustomFormat = "yyyy-MM-dd";
            this.dtpEnddate.Format = DateTimePickerFormat.Custom;
            this.dtpEnddate.Location = new Point(0x13a, 0x9a);
            this.dtpEnddate.Name = "dtpEnddate";
            this.dtpEnddate.Size = new Size(0x63, 0x17);
            this.dtpEnddate.TabIndex = 7;
            this.dtpBegindate.CustomFormat = "yyyy-MM-dd";
            this.dtpBegindate.Format = DateTimePickerFormat.Custom;
            this.dtpBegindate.Location = new Point(0x13a, 0x7a);
            this.dtpBegindate.Name = "dtpBegindate";
            this.dtpBegindate.Size = new Size(0x63, 0x17);
            this.dtpBegindate.TabIndex = 6;
            this.dtpBegindate.Value = new DateTime(0x7d8, 12, 4, 0, 0, 0, 0);
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new Point(220, 0x41);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new Size(0x5b, 14);
            this.lblCode.TabIndex = 0x1a;
            this.lblCode.Text = "仪器接收编码";
            this.lblConcentration.AutoSize = true;
            this.lblConcentration.Location = new Point(0x114, 0x1f);
            this.lblConcentration.Name = "lblConcentration";
            this.lblConcentration.Size = new Size(0x23, 14);
            this.lblConcentration.TabIndex = 0x19;
            this.lblConcentration.Text = "浓度";
            this.txtGroupNum.Location = new Point(0x5f, 0x29);
            this.txtGroupNum.MaxLength = 0x20;
            this.txtGroupNum.Name = "txtGroupNum";
            this.txtGroupNum.ReadOnly = true;
            this.txtGroupNum.Size = new Size(0x6f, 0x17);
            this.txtGroupNum.TabIndex = 0x17;
            this.lblManGroupNum.AutoSize = true;
            this.lblManGroupNum.Location = new Point(30, 50);
            this.lblManGroupNum.Name = "lblManGroupNum";
            this.lblManGroupNum.Size = new Size(0x3f, 14);
            this.lblManGroupNum.TabIndex = 0x11;
            this.lblManGroupNum.Text = "质控批号";
            this.lblManOrigin.AutoSize = true;
            this.lblManOrigin.Location = new Point(0x1bc, 0xa1);
            this.lblManOrigin.Name = "lblManOrigin";
            this.lblManOrigin.Size = new Size(0x4d, 14);
            this.lblManOrigin.TabIndex = 0x22;
            this.lblManOrigin.Text = "质控物来源";
            this.txtWaveLength.Location = new Point(0x20c, 0x18);
            this.txtWaveLength.MaxLength = 0x20;
            this.txtWaveLength.Name = "txtWaveLength";
            this.txtWaveLength.Size = new Size(120, 0x17);
            this.txtWaveLength.TabIndex = 8;
            this.lblWaveLength.AutoSize = true;
            this.lblWaveLength.Location = new Point(0x1e8, 0x21);
            this.lblWaveLength.Name = "lblWaveLength";
            this.lblWaveLength.Size = new Size(0x23, 14);
            this.lblWaveLength.TabIndex = 30;
            this.lblWaveLength.Text = "波长";
            this.lblApparatusType.AutoSize = true;
            this.lblApparatusType.Location = new Point(0x1ca, 0x63);
            this.lblApparatusType.Name = "lblApparatusType";
            this.lblApparatusType.Size = new Size(0x3f, 14);
            this.lblApparatusType.TabIndex = 0x20;
            this.lblApparatusType.Text = "检测仪器";
            this.lblExamineMethod.AutoSize = true;
            this.lblExamineMethod.Location = new Point(0x1ca, 0x83);
            this.lblExamineMethod.Name = "lblExamineMethod";
            this.lblExamineMethod.Size = new Size(0x3f, 14);
            this.lblExamineMethod.TabIndex = 0x21;
            this.lblExamineMethod.Text = "检测方法";
            this.txtSortNum.Location = new Point(0x13a, 90);
            this.txtSortNum.MaxLength = 0x20;
            this.txtSortNum.Name = "txtSortNum";
            this.txtSortNum.Size = new Size(0x63, 0x17);
            this.txtSortNum.TabIndex = 5;
            this.lblSortNum.AutoSize = true;
            this.lblSortNum.Location = new Point(0x106, 0x60);
            this.lblSortNum.Name = "lblSortNum";
            this.lblSortNum.Size = new Size(0x31, 14);
            this.lblSortNum.TabIndex = 0x1b;
            this.lblSortNum.Text = "排序号";
            this.txtUnit.Location = new Point(0x20c, 0x3a);
            this.txtUnit.MaxLength = 0x20;
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Size = new Size(120, 0x17);
            this.txtUnit.TabIndex = 9;
            this.lblUnit.AutoSize = true;
            this.lblUnit.Location = new Point(0x1e5, 0x43);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new Size(0x23, 14);
            this.lblUnit.TabIndex = 0x1f;
            this.lblUnit.Text = "单位";
            this.panel1.Controls.Add(this.dgvSetup);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x385, 0x113);
            this.panel1.TabIndex = 1;
            this.dgvSetup.AllowUserToAddRows = false;
            this.dgvSetup.AllowUserToDeleteRows = false;
            this.dgvSetup.BackgroundColor = SystemColors.Info;
            this.dgvSetup.BorderStyle = BorderStyle.Fixed3D;
            this.dgvSetup.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dgvSetup.ColumnHeadersHeight = 40;
            this.dgvSetup.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvSetup.Columns.AddRange(new DataGridViewColumn[] {
            this.itemsname, this.unit, this.sortNum, this.method, this.type, this.wavelength, this.source, this.groupnum, this.Concentration, this.code, this.begindate, this.enddate, this.BValue, this.sd, this.cv, this.intSeq,
            this.checkitemsid, this.m_strDeviceId
         });
            this.dgvSetup.Dock = DockStyle.Fill;
            this.dgvSetup.GridColor = SystemColors.ActiveCaptionText;
            this.dgvSetup.Location = new Point(0, 0);
            this.dgvSetup.MultiSelect = false;
            this.dgvSetup.Name = "dgvSetup";
            this.dgvSetup.ReadOnly = true;
            this.dgvSetup.RowHeadersVisible = false;
            this.dgvSetup.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvSetup.RowTemplate.Height = 0x17;
            this.dgvSetup.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvSetup.Size = new Size(0x385, 0x113);
            this.dgvSetup.TabIndex = 0;
            this.dgvSetup.CellClick += new DataGridViewCellEventHandler(this.dgvSetup_CellClick);
            this.dgvSetup.SelectionChanged += new EventHandler(this.dgvSetup_SelectionChanged);
            this.itemsname.DataPropertyName = "itemsname";
            this.itemsname.HeaderText = "项目";
            this.itemsname.MaxInputLength = 0x147;
            this.itemsname.Name = "itemsname";
            this.itemsname.ReadOnly = true;
            this.itemsname.Width = 80;
            this.unit.DataPropertyName = "unit";
            this.unit.HeaderText = "单位";
            this.unit.Name = "unit";
            this.unit.ReadOnly = true;
            this.unit.Width = 60;
            this.sortNum.DataPropertyName = "sortNum";
            this.sortNum.HeaderText = "序号";
            this.sortNum.Name = "sortNum";
            this.sortNum.ReadOnly = true;
            this.sortNum.Width = 0x2d;
            this.method.DataPropertyName = "method";
            this.method.HeaderText = "检测方法";
            this.method.Name = "method";
            this.method.ReadOnly = true;
            this.type.DataPropertyName = "type";
            this.type.HeaderText = "检测仪器";
            this.type.Name = "type";
            this.type.ReadOnly = true;
            this.wavelength.DataPropertyName = "wavelength";
            this.wavelength.HeaderText = "检测波长";
            this.wavelength.Name = "wavelength";
            this.wavelength.ReadOnly = true;
            this.wavelength.Visible = false;
            this.source.DataPropertyName = "source";
            this.source.HeaderText = "质控物来源";
            this.source.Name = "source";
            this.source.ReadOnly = true;
            this.source.Width = 120;
            this.groupnum.DataPropertyName = "groupnum";
            this.groupnum.HeaderText = "质控批号";
            this.groupnum.Name = "groupnum";
            this.groupnum.ReadOnly = true;
            this.groupnum.Width = 0x2d;
            this.Concentration.DataPropertyName = "Concentration";
            this.Concentration.HeaderText = "浓度";
            this.Concentration.Name = "Concentration";
            this.Concentration.ReadOnly = true;
            this.Concentration.Width = 0x2d;
            this.code.DataPropertyName = "code";
            this.code.HeaderText = "仪器接收编码";
            this.code.Name = "code";
            this.code.ReadOnly = true;
            this.code.Width = 0x41;
            this.begindate.DataPropertyName = "begindate";
            this.begindate.HeaderText = "启用日期";
            this.begindate.Name = "begindate";
            this.begindate.ReadOnly = true;
            this.enddate.DataPropertyName = "enddate";
            this.enddate.HeaderText = "结束日期";
            this.enddate.Name = "enddate";
            this.enddate.ReadOnly = true;
            this.BValue.DataPropertyName = "BValue";
            this.BValue.HeaderText = "靶值(X)";
            this.BValue.Name = "BValue";
            this.BValue.ReadOnly = true;
            this.BValue.Width = 80;
            this.sd.DataPropertyName = "sd";
            this.sd.HeaderText = "标准差(SD)";
            this.sd.Name = "sd";
            this.sd.ReadOnly = true;
            this.sd.Width = 80;
            this.cv.DataPropertyName = "cv";
            this.cv.HeaderText = "变异系数(CV)";
            this.cv.Name = "cv";
            this.cv.ReadOnly = true;
            this.cv.Width = 90;
            this.intSeq.DataPropertyName = "intSeq";
            this.intSeq.HeaderText = "质控批序号";
            this.intSeq.Name = "intSeq";
            this.intSeq.ReadOnly = true;
            this.intSeq.Visible = false;
            this.checkitemsid.DataPropertyName = "checkitemsid";
            this.checkitemsid.HeaderText = "检验项目ID";
            this.checkitemsid.Name = "checkitemsid";
            this.checkitemsid.ReadOnly = true;
            this.checkitemsid.Visible = false;
            this.m_strDeviceId.DataPropertyName = "m_strDeviceId";
            this.m_strDeviceId.HeaderText = "仪器ID";
            this.m_strDeviceId.Name = "m_strDeviceId";
            this.m_strDeviceId.ReadOnly = true;
            this.m_strDeviceId.Visible = false;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Column1";
            this.dataGridViewTextBoxColumn1.HeaderText = "项目";
            this.dataGridViewTextBoxColumn1.MaxInputLength = 0x147;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 80;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Column2";
            this.dataGridViewTextBoxColumn2.HeaderText = "单位";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 50;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Column3";
            this.dataGridViewTextBoxColumn3.HeaderText = "排序号";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 50;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Column4";
            this.dataGridViewTextBoxColumn4.HeaderText = "检测方法";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 120;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Column5";
            this.dataGridViewTextBoxColumn5.HeaderText = "仪器型号";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Column6";
            this.dataGridViewTextBoxColumn6.HeaderText = "检测波长";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Visible = false;
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Column7";
            this.dataGridViewTextBoxColumn7.HeaderText = "质控物来源";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 120;
            this.dataGridViewTextBoxColumn8.DataPropertyName = "Column8";
            this.dataGridViewTextBoxColumn8.HeaderText = "质控批号";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 0x2d;
            this.dataGridViewTextBoxColumn9.DataPropertyName = "Column9";
            this.dataGridViewTextBoxColumn9.HeaderText = "浓度";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Width = 0x2d;
            this.dataGridViewTextBoxColumn10.DataPropertyName = "Column10";
            this.dataGridViewTextBoxColumn10.HeaderText = "仪器接收编码";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Width = 140;
            this.dataGridViewTextBoxColumn11.DataPropertyName = "Column11";
            this.dataGridViewTextBoxColumn11.HeaderText = "启用日期";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.DataPropertyName = "Column12";
            this.dataGridViewTextBoxColumn12.HeaderText = "结束日期";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn13.DataPropertyName = "Column13";
            this.dataGridViewTextBoxColumn13.HeaderText = "靶值";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            this.dataGridViewTextBoxColumn13.Width = 80;
            this.dataGridViewTextBoxColumn14.DataPropertyName = "Column14";
            this.dataGridViewTextBoxColumn14.HeaderText = "标准差（SD）";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.ReadOnly = true;
            this.dataGridViewTextBoxColumn14.Width = 120;
            this.dataGridViewTextBoxColumn15.DataPropertyName = "Column15";
            this.dataGridViewTextBoxColumn15.HeaderText = "变异系数（CV）";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.ReadOnly = true;
            this.dataGridViewTextBoxColumn15.Width = 0x84;
            this.dataGridViewTextBoxColumn16.DataPropertyName = "intSeq";
            this.dataGridViewTextBoxColumn16.HeaderText = "质控批序号";
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.ReadOnly = true;
            this.dataGridViewTextBoxColumn16.Visible = false;
            this.dataGridViewTextBoxColumn17.DataPropertyName = "checkitemsid";
            this.dataGridViewTextBoxColumn17.HeaderText = "检验项目ID";
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            this.dataGridViewTextBoxColumn17.Visible = false;
            this.dataGridViewTextBoxColumn18.DataPropertyName = "m_strDeviceId";
            this.dataGridViewTextBoxColumn18.HeaderText = "仪器ID";
            this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
            this.dataGridViewTextBoxColumn18.Visible = false;
            this.AutoScaleDimensions = new SizeF(7f, 14f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(0x385, 0x1f2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 10.5F);
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmQCSetup";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "检验质控设置";
            this.KeyDown += new KeyEventHandler(this.frmQCSetup_KeyDown);
            this.Load += new EventHandler(this.frmQCSetup_Load);
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSetup)).EndInit();
            this.ResumeLayout(false);
        }

        private void lblNotice_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }

        private void lblNotice_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Default;
        }

        public void m_mthReset()
        {
            this.txtUnit.Clear();
            this.txtBValue.Clear();
            this.txtCV.Clear();
            this.txtGroupNum.Clear();
            this.txtSD.Clear();
            this.txtCode.Clear();
            this.txtSortNum.Clear();
            this.txtWaveLength.Clear();
            this.cboApparatusType.Text = "";
            this.cboConcentration.Text = "";
            this.cboExamineMethod.Text = "";
            this.cboManOrigin.Text = "";
            this.txtSeq.Clear();
            this.dtpBegindate.Value = DateTime.Now;
            this.dtpEnddate.Value = DateTime.Now;
        }

        public void m_mthShortCutKey(Keys p_eumKeyCode)
        {
            if (p_eumKeyCode == Keys.F4)
            {
                this.cmdSave_Click(null, null);
            }
            if (p_eumKeyCode == Keys.Escape)
            {
                this.cmdExit_Click(null, null);
            }
        }

        private void txtCV_Enter(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtBValue.Text.Trim()) && !string.IsNullOrEmpty(this.txtSD.Text.Trim()))
            {
                try
                {
                    double num = double.Parse(this.txtBValue.Text.Trim());
                    double num2 = double.Parse(this.txtSD.Text.Trim());
                    double num3 = num2 * 100.0 / num;
                    this.txtCV.Text = num3.ToString("0.0");
                }
                catch
                {
                }
            }
        }
    }
}
