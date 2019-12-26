using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
using PinkieControls;

namespace com.digitalwave.iCare.gui.LIS
{
    public class ctlQCDataEditorNew : UserControl
    {
        // Fields
        private IContainer components;
        private Label label1;
        private Label label2;
        private ButtonXP m_cmdAddRowAfter;
        private ButtonXP m_cmdAddRowPrev;
        private ButtonXP m_cmdDataImpot;
        private ButtonXP m_cmdDeleteRow;
        private ButtonXP m_cmdSave;
        internal DataTable m_dtbSource;
        internal DataGridView m_dtgEditor;
        internal DateTimePicker m_dtpEndDate;
        internal DateTimePicker m_dtpStartDate;
        private DataView m_dtvSource;
        private Label m_lblDeviceSampleID;
        private List<clsLisQCDataVO> m_lstAddedVOS;
        private List<clsLisQCDataVO> m_lstChangedVOS;
        private List<clsLisQCDataVO> m_lstDeletedVOS;
        private List<clsLisQCDataVO> m_lstWorkedData;
        private clsQCBatchNew m_objBatch;
        //private clsDcl_QCDataBusiness m_objDomain;
        private string m_strFixDeviceSampleID;
        private string m_strFlashConn;
        private string m_strLocalConn;
        private string m_strLocalCV;
        private string m_strLocalSD;
        private string m_strLocalX;
        private TextBox m_txtDeviceSampleID;
        private Panel panel1;

        // Methods
        public ctlQCDataEditorNew()
        {
            this.m_dtvSource = new DataView();
            this.m_dtbSource = new DataTable("datalist");
            this.m_lstWorkedData = new List<clsLisQCDataVO>();
            this.m_lstChangedVOS = new List<clsLisQCDataVO>();
            this.m_lstAddedVOS = new List<clsLisQCDataVO>();
            this.m_lstDeletedVOS = new List<clsLisQCDataVO>();
            // this.m_objDomain = new clsDcl_QCDataBusiness();
            this.m_strFixDeviceSampleID = null;
            this.m_strLocalX = "本室均值";
            this.m_strLocalSD = "标准差";
            this.m_strLocalCV = "变异系数(%)";
            this.m_strLocalConn = "刷新当前浓度?";
            this.m_strFlashConn = "刷新";
            this.components = null;
            this.InitializeComponent();
            this.m_dtvSource.Table = this.m_dtbSource;
        }

        private void ctlQCDataEditorNew_Load(object sender, EventArgs e)
        {
            try
            {
                this.m_strFixDeviceSampleID = (new weCare.Proxy.ProxyOP()).Service.m_strGetSysParam("6001");
            }
            catch
            {
                this.m_strFixDeviceSampleID = string.Empty;
            }
            if (string.IsNullOrEmpty(this.m_strFixDeviceSampleID))
            {
                this.m_lblDeviceSampleID.Visible = false;
                this.m_txtDeviceSampleID.Visible = false;
                this.m_txtDeviceSampleID.Clear();
            }
            else
            {
                this.m_lblDeviceSampleID.Visible = true;
                this.m_txtDeviceSampleID.Visible = true;
                this.m_txtDeviceSampleID.Clear();
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

        private void InitializeComponent()
        {
            this.panel1 = new Panel();
            this.m_txtDeviceSampleID = new TextBox();
            this.label2 = new Label();
            this.m_lblDeviceSampleID = new Label();
            this.label1 = new Label();
            this.m_dtpEndDate = new DateTimePicker();
            this.m_dtpStartDate = new DateTimePicker();
            this.m_cmdAddRowAfter = new ButtonXP();
            this.m_cmdDataImpot = new ButtonXP();
            this.m_cmdSave = new ButtonXP();
            this.m_cmdAddRowPrev = new ButtonXP();
            this.m_cmdDeleteRow = new ButtonXP();
            this.m_dtgEditor = new DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgEditor)).BeginInit();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.m_txtDeviceSampleID);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.m_lblDeviceSampleID);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.m_dtpEndDate);
            this.panel1.Controls.Add(this.m_dtpStartDate);
            this.panel1.Controls.Add(this.m_cmdAddRowAfter);
            this.panel1.Controls.Add(this.m_cmdDataImpot);
            this.panel1.Controls.Add(this.m_cmdSave);
            this.panel1.Controls.Add(this.m_cmdAddRowPrev);
            this.panel1.Controls.Add(this.m_cmdDeleteRow);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new Point(0x1f6, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x86, 0x1e9);
            this.panel1.TabIndex = 6;
            this.m_txtDeviceSampleID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtDeviceSampleID.Location = new Point(0x17, 0x8b);
            this.m_txtDeviceSampleID.MaxLength = 20;
            this.m_txtDeviceSampleID.Name = "m_txtDeviceSampleID";
            this.m_txtDeviceSampleID.Size = new Size(0x60, 0x17);
            this.m_txtDeviceSampleID.TabIndex = 6;
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.ForeColor = SystemColors.ActiveCaption;
            this.label2.Location = new Point(0x3b, 0x43);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x15, 14);
            this.label2.TabIndex = 5;
            this.label2.Text = "到";
            this.m_lblDeviceSampleID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblDeviceSampleID.AutoSize = true;
            this.m_lblDeviceSampleID.ForeColor = SystemColors.ActiveCaption;
            this.m_lblDeviceSampleID.Location = new Point(4, 0x7b);
            this.m_lblDeviceSampleID.Name = "m_lblDeviceSampleID";
            this.m_lblDeviceSampleID.Size = new Size(0x54, 14);
            this.m_lblDeviceSampleID.TabIndex = 5;
            this.m_lblDeviceSampleID.Text = "仪器标本号:";
//            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right))); ;
            this.label1.AutoSize = true;
            this.label1.ForeColor = SystemColors.ActiveCaption;
            this.label1.Location = new Point(4, 15);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x62, 14);
            this.label1.TabIndex = 5;
            this.label1.Text = "接收数据日期:";
            this.m_dtpEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_dtpEndDate.CustomFormat = "yyyy-MM-dd";
            this.m_dtpEndDate.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.m_dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpEndDate.Location = new Point(0x17, 0x57);
            this.m_dtpEndDate.Name = "m_dtpEndDate";
            this.m_dtpEndDate.Size = new Size(0x60, 0x17);
            this.m_dtpEndDate.TabIndex = 4;
            this.m_dtpStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_dtpStartDate.CustomFormat = "yyyy-MM-dd";
            this.m_dtpStartDate.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.m_dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpStartDate.Location = new Point(0x17, 0x27);
            this.m_dtpStartDate.Name = "m_dtpStartDate";
            this.m_dtpStartDate.Size = new Size(0x60, 0x17);
            this.m_dtpStartDate.TabIndex = 4;
            this.m_cmdAddRowAfter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdAddRowAfter.BackColor = Color.FromArgb(0, 0xec, 0xe9, 0xd8);
            this.m_cmdAddRowAfter.DefaultScheme = true;
            this.m_cmdAddRowAfter.DialogResult = 0;
            this.m_cmdAddRowAfter.Hint = "";
            this.m_cmdAddRowAfter.Location = new Point(0x17, 0x188);
            this.m_cmdAddRowAfter.Name = "m_cmdAddRowAfter";
            this.m_cmdAddRowAfter.Scheme = 0;
            this.m_cmdAddRowAfter.Size = new Size(0x60, 0x21);
            this.m_cmdAddRowAfter.TabIndex = 1;
            this.m_cmdAddRowAfter.Text = "插入（↓）";
            this.m_cmdAddRowAfter.Visible = false;
            this.m_cmdAddRowAfter.Click += new EventHandler(this.m_cmdAddRowAfter_Click);
            this.m_cmdDataImpot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdDataImpot.BackColor = Color.FromArgb(0, 0xec, 0xe9, 0xd8);
            this.m_cmdDataImpot.DefaultScheme = true;
            this.m_cmdDataImpot.DialogResult = 0;
            this.m_cmdDataImpot.Hint = "";
            this.m_cmdDataImpot.Location = new Point(0x17, 170);
            this.m_cmdDataImpot.Name = "m_cmdDataImpot";
            this.m_cmdDataImpot.Scheme = 0;
            this.m_cmdDataImpot.Size = new Size(0x60, 0x21);
            this.m_cmdDataImpot.TabIndex = 2;
            this.m_cmdDataImpot.Text = "接收数据";
            this.m_cmdDataImpot.Click += new EventHandler(this.m_cmdDataImpot_Click);
            this.m_cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdSave.BackColor = Color.FromArgb(0, 0xec, 0xe9, 0xd8);
            this.m_cmdSave.DefaultScheme = true;
            this.m_cmdSave.DialogResult = 0;
            this.m_cmdSave.Hint = "";
            this.m_cmdSave.Location = new Point(0x17, 0xdb);
            this.m_cmdSave.Name = "m_cmdSave";
            this.m_cmdSave.Scheme = 0;
            this.m_cmdSave.Size = new Size(0x60, 0x21);
            this.m_cmdSave.TabIndex = 2;
            this.m_cmdSave.Text = "保存";
            this.m_cmdSave.Click += new EventHandler(this.m_cmdSave_Click);
            this.m_cmdAddRowPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdAddRowPrev.BackColor = Color.FromArgb(0, 0xec, 0xe9, 0xd8);
            this.m_cmdAddRowPrev.DefaultScheme = true;
            this.m_cmdAddRowPrev.DialogResult = 0;
            this.m_cmdAddRowPrev.Hint = "";
            this.m_cmdAddRowPrev.Location = new Point(0x17, 0x161);
            this.m_cmdAddRowPrev.Name = "m_cmdAddRowPrev";
            this.m_cmdAddRowPrev.Scheme = 0;
            this.m_cmdAddRowPrev.Size = new Size(0x60, 0x21);
            this.m_cmdAddRowPrev.TabIndex = 0;
            this.m_cmdAddRowPrev.Text = "插入（↑）";
            this.m_cmdAddRowPrev.Visible = false;
            this.m_cmdAddRowPrev.Click += new EventHandler(this.m_cmdAddRowPrev_Click);
            this.m_cmdDeleteRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdDeleteRow.BackColor = Color.FromArgb(0, 0xec, 0xe9, 0xd8);
            this.m_cmdDeleteRow.DefaultScheme = true;
            this.m_cmdDeleteRow.DialogResult = 0;
            this.m_cmdDeleteRow.Hint = "";
            this.m_cmdDeleteRow.Location = new Point(0x17, 0x10c);
            this.m_cmdDeleteRow.Name = "m_cmdDeleteRow";
            this.m_cmdDeleteRow.Scheme = 0;
            this.m_cmdDeleteRow.Size = new Size(0x60, 0x21);
            this.m_cmdDeleteRow.TabIndex = 3;
            this.m_cmdDeleteRow.Text = "删除整行";
            this.m_cmdDeleteRow.Click += new EventHandler(this.m_cmdDeleteRow_Click);
            this.m_dtgEditor.AllowUserToAddRows = false;
            this.m_dtgEditor.AllowUserToDeleteRows = false;
            this.m_dtgEditor.AllowUserToResizeColumns = false;
            this.m_dtgEditor.AllowUserToResizeRows = false;
            this.m_dtgEditor.BackgroundColor = SystemColors.Window;
            this.m_dtgEditor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_dtgEditor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_dtgEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_dtgEditor.EditMode = 0;
            this.m_dtgEditor.Location = new Point(0, 0);
            this.m_dtgEditor.MultiSelect = false;
            this.m_dtgEditor.Name = "m_dtgEditor";
            this.m_dtgEditor.RowTemplate.Height = 0x17;
            this.m_dtgEditor.SelectionMode = 0;
            this.m_dtgEditor.Size = new Size(0x1f6, 0x1e9);
            this.m_dtgEditor.TabIndex = 7;
            this.m_dtgEditor.CellClick += new DataGridViewCellEventHandler(this.m_dtgEditor_CellClick);
            this.m_dtgEditor.RowsAdded += new DataGridViewRowsAddedEventHandler(this.m_dtgEditor_RowsAdded);
            this.m_dtgEditor.CellValidating += new DataGridViewCellValidatingEventHandler(this.m_dtgEditor_CellValidating);
            this.AutoScaleDimensions = new SizeF(7f, 14f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.m_dtgEditor);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "ctlQCDataEditorNew";
            this.Size = new Size(0x27c, 0x1e9);
            this.Load += new EventHandler(this.ctlQCDataEditorNew_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgEditor)).EndInit();
            this.ResumeLayout(false);
        }

        private void InitializeStyleEditor()
        {
            DataGridViewDateTimeColumn column;
            clsLisQCBatchVO hvo;
            DataGridViewTextBoxColumn column2;
            DataGridViewTextBoxColumn column3;
            List<clsLisQCBatchVO>.Enumerator enumerator;
            bool flag;
            this.m_dtgEditor.DataSource = null;
            this.m_dtgEditor.Columns.Clear();
            column = new DataGridViewDateTimeColumn();
            column.DataPropertyName = "date";
            column.HeaderText = "日期";
            column.Name = "date";
            column.Visible = false;
            column.SortMode = 0;
            this.m_dtgEditor.Columns.Add(column);
            column = new DataGridViewDateTimeColumn();
            column.DataPropertyName = "strdate";
            column.HeaderText = "日期";
            column.Name = "strdate";
            column.ReadOnly = true;
            column.SortMode = 0;
            this.m_dtgEditor.Columns.Add(column);
            enumerator = this.m_objBatch.GetQCBatchSet().GetEnumerator();
        Label_00CA:
            try
            {
                goto Label_01AE;
            Label_00CF:
                hvo = enumerator.Current;
                column2 = new DataGridViewTextBoxColumn();
                column2.Name = hvo.m_intSeq.ToString();
                column2.DataPropertyName = hvo.m_intSeq.ToString();
                column2.HeaderText = hvo.m_strCheckItemName;
                column2.SortMode = 0;
                this.m_dtgEditor.Columns.Add(column2);
                column3 = new DataGridViewTextBoxColumn();
                column3.Name = hvo.m_intSeq.ToString() + "obj";
                column3.DataPropertyName = hvo.m_intSeq.ToString() + "obj";
                column3.HeaderText = ((int)hvo.m_intSeq) + "VO";
                column3.Visible = false;
                column3.ReadOnly = true;
                column3.SortMode = 0;
                this.m_dtgEditor.Columns.Add(column3);
            Label_01AE:
                if (enumerator.MoveNext())
                {
                    goto Label_00CF;
                }
                goto Label_01CF;
            }
            finally
            {
            Label_01C0:
                enumerator.Dispose();
            }
        Label_01CF:
            return;
        }

        private void m_cmdAddRowAfter_Click(object sender, EventArgs e)
        {
            this.m_mthAddRowAt(1); 
        }

        private void m_cmdAddRowPrev_Click(object sender, EventArgs e)
        {
            this.m_mthAddRowAt(0); 
        }

        private void m_cmdDataImpot_Click(object sender, EventArgs e)
        {
            if (!this.m_objBatch.IsNull)
            {
                string str = string.Empty;
                DateTime dteEnd = this.m_dtpEndDate.Value;
                DateTime dteBegin = this.m_objBatch.DateBegin;
                bool result;
                if (!(dteEnd < dteBegin))
                    result = !(this.m_dtpStartDate.Value > this.m_objBatch.DateEnd.Date);
                else
                    result = false;

                if (!result)
                {
                    str = "请选择 ";
                    dteBegin = this.m_objBatch.DateBegin;
                    str += dteBegin.ToString("yyyy-MM-dd");
                    str += " -- ";
                    dteEnd = this.m_objBatch.DateEnd;
                    str += dteEnd.ToString("yyyy-MM-dd");
                    str += " 范围内的接收日期！";
                    MessageBox.Show(str, "接收数据操作", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    string p_strStartDat = null;
                    string p_strEndDat = null;
                    dteBegin = this.m_objBatch.DateBegin;
                    if (this.m_dtpStartDate.Value >= this.m_objBatch.DateBegin)
                    {
                        dteBegin = this.m_dtpStartDate.Value;
                        p_strStartDat = dteBegin.ToString("yyyy-MM-dd 00:00:00");
                    }
                    else
                    {
                        dteBegin = this.m_objBatch.DateBegin;
                        p_strStartDat = dteBegin.ToString("yyyy-MM-dd 00:00:00");
                    }
                    dteEnd = this.m_dtpEndDate.Value;
                    if (dteEnd.Date <= this.m_objBatch.DateEnd.Date)
                    {
                        p_strEndDat = this.m_dtpEndDate.Value.ToString("yyyy-MM-dd 23:59:59");
                    }
                    else
                    {
                        p_strEndDat = this.m_objBatch.DateEnd.ToString("yyyy-MM-dd 23:59:59");
                    }

                    clsLisQCDataVO[] array2 = null;
                    long num = 0L;
                    if (string.IsNullOrEmpty(this.m_strFixDeviceSampleID))
                    {
                        num = (new weCare.Proxy.ProxyLis02()).Service.m_lngReceiveDeviceQCData(p_strStartDat, p_strEndDat, this.m_objBatch.SeqArr, out array2);
                    }
                    else
                    {
                        string text2 = this.m_txtDeviceSampleID.Text.Trim();
                        if (string.IsNullOrEmpty(text2))
                        {
                            text2 = this.m_strFixDeviceSampleID;
                        }
                        num = (new weCare.Proxy.ProxyLis02()).Service.m_lngReceiveDeviceQCDataBySampleID(text2, p_strStartDat, p_strEndDat, this.m_objBatch.SeqArr, out array2);
                    }
                    if (num <= 0L)
                    {
                        MessageBox.Show("接收数据失败！", "接收数据操作", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        if (array2 != null && array2.Length > 0)
                        {
                            this.m_mthAddDataTable(array2);
                        }
                    }
                }
            }
        }

        private void m_cmdDeleteRow_Click(object sender, EventArgs e)
        {
            DataGridViewRow currentRow = this.m_dtgEditor.CurrentRow;
            DataRow row = ((DataRowView)currentRow.DataBoundItem).Row;
            if (row != null)
            {
                string s = row["strdate"].ToString().Trim();
                DateTime minValue = DateTime.MinValue;
                if (DateTime.TryParse(s, out minValue))
                {
                    DialogResult dialogResult = MessageBox.Show(this, "确定删除整行吗？", "删除操作", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        int[] seqArr = this.m_objBatch.SeqArr;
                        for (int i = 0; i < seqArr.Length; i++)
                        {
                            int num = seqArr[i];
                            clsLisQCDataVO clsLisQCDataVO = row[num.ToString() + "obj"] as clsLisQCDataVO;
                            if (clsLisQCDataVO != null)
                            {
                                if (clsLisQCDataVO.m_intSeq != -1)
                                {
                                    row[num.ToString() + "obj"] = DBNull.Value;
                                    this.m_lstDeletedVOS.Add(clsLisQCDataVO);
                                }
                                else
                                {
                                    this.m_lstAddedVOS.Remove(clsLisQCDataVO);
                                }
                            }
                            row[num.ToString()] = DBNull.Value;
                            row[num.ToString() + "obj"] = DBNull.Value;
                        }
                    }
                }
            }
        }

        private void m_cmdSave_Click(object sender, EventArgs e)
        {
            clsLisQCDataVO[] array = null;
            clsLisQCDataVO[] p_objUpdateArr = null;
            int[] array2 = null;
            int[] array3 = null;
            if (this.m_lstAddedVOS != null && this.m_lstAddedVOS.Count > 0)
            {
                array = this.m_lstAddedVOS.ToArray();
            }
            if (this.m_lstChangedVOS != null && this.m_lstChangedVOS.Count > 0)
            {
                p_objUpdateArr = this.m_lstChangedVOS.ToArray();
            }
            if (this.m_lstDeletedVOS != null && this.m_lstDeletedVOS.Count > 0)
            {
                array2 = new int[this.m_lstDeletedVOS.Count];
                for (int i = 0; i < this.m_lstDeletedVOS.Count; i++)
                {
                    array2[i] = this.m_lstDeletedVOS[i].m_intSeq;
                }
            }
            long num = (new weCare.Proxy.ProxyLis02()).Service.m_lngSaveAllQCData(array, p_objUpdateArr, array2, out array3);
            if (num > 0L)
            {
                if (array3 != null && array3.Length > 0)
                {
                    for (int i = 0; i < array3.Length; i++)
                    {
                        array[i].m_intSeq = array3[i];
                    }
                    this.m_lstWorkedData.AddRange(array);
                    this.m_lstAddedVOS.Clear();
                }
                if (this.m_lstChangedVOS != null)
                {
                    this.m_lstChangedVOS.Clear();
                }
                if (this.m_lstDeletedVOS != null)
                {
                    foreach (clsLisQCDataVO current in this.m_lstDeletedVOS)
                    {
                        this.m_lstWorkedData.Remove(current);
                    }
                    this.m_lstDeletedVOS.Clear();
                }
                this.m_objBatch.DataChanged -= new EventHandler(this.m_objBatch_DataChanged);
                this.m_objBatch.UpdateDatas(this.m_lstWorkedData);
                this.m_objBatch.DataChanged += new EventHandler(this.m_objBatch_DataChanged);
            }
            else
            {
                MessageBox.Show("保存失败！", "保存操作", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void m_dtbSource_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            string a = e.Row["strdate"].ToString().Trim();
            if (a == this.m_strLocalX || a == this.m_strLocalSD || a == this.m_strLocalCV || a == this.m_strLocalConn)
            {
                e.Row.CancelEdit();
            }
            else
            {
                string columnName = e.Column.ColumnName;
                this.m_dtbSource.ColumnChanged -= new DataColumnChangeEventHandler(this.m_dtbSource_ColumnChanged);
                if (!columnName.Contains("date") && !columnName.Contains("obj"))
                {
                    string columnName2 = e.Column.ColumnName;
                    clsLisQCDataVO clsLisQCDataVO = e.Row[columnName2 + "obj"] as clsLisQCDataVO;
                    if (clsLisQCDataVO != null)
                    {
                        if (clsLisQCDataVO.m_intSeq == -1)
                        {
                            if (e.Row[e.Column].ToString() == string.Empty)
                            {
                                this.m_lstAddedVOS.Remove(clsLisQCDataVO);
                            }
                            else
                            {
                                try
                                {
                                    double dlbResult = Convert.ToDouble(e.Row[e.Column]);
                                    clsLisQCDataVO.m_dlbResult = dlbResult;
                                }
                                catch
                                {
                                }
                            }
                        }
                        else
                        {
                            if (e.Row[e.Column].ToString() != string.Empty)
                            {
                                try
                                {
                                    double dlbResult = Convert.ToDouble(e.Row[e.Column]);
                                    clsLisQCDataVO.m_dlbResult = dlbResult;
                                }
                                catch
                                {
                                }
                                this.m_lstChangedVOS.Add(clsLisQCDataVO);
                            }
                            else
                            {
                                try
                                {
                                    clsLisQCDataVO item = e.Row[columnName2 + "obj"] as clsLisQCDataVO;
                                    this.m_lstDeletedVOS.Add(item);
                                    e.Row[columnName2 + "obj"] = DBNull.Value;
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                    if (clsLisQCDataVO == null && e.Row[e.Column].ToString() != string.Empty)
                    {
                        if (e.Row["date"].ToString() == string.Empty)
                        {
                            MessageBox.Show("请先输入日期！");
                            e.Row[e.Column] = DBNull.Value;
                        }
                        else
                        {
                            clsLisQCDataVO clsLisQCDataVO2 = new clsLisQCDataVO();
                            clsLisQCDataVO2.m_intSeq = -1;
                            clsLisQCDataVO2.m_intQCBatchSeq = Convert.ToInt32(columnName2);
                            clsLisQCDataVO2.m_intConcentrationSeq = -1;
                            try
                            {
                                clsLisQCDataVO2.m_dlbResult = Convert.ToDouble(e.Row[e.Column]);
                            }
                            catch
                            {
                            }
                            try
                            {
                                clsLisQCDataVO2.m_datQCDate = Convert.ToDateTime(e.Row["date"]);
                            }
                            catch
                            {
                            }
                            e.Row[columnName2 + "obj"] = clsLisQCDataVO2;
                            this.m_lstAddedVOS.Add(clsLisQCDataVO2);
                        }
                    }
                }
                if (e.Column.ColumnName == "date")
                {
                    DateTime dateTime = DBAssist.ToDateTime(e.Row["date"]);
                    if (dateTime != DBAssist.NullDateTime)
                    {
                        int[] seqArr = this.m_objBatch.SeqArr;
                        for (int i = 0; i < seqArr.Length; i++)
                        {
                            int num = seqArr[i];
                            clsLisQCDataVO clsLisQCDataVO3 = e.Row[num.ToString() + "obj"] as clsLisQCDataVO;
                            if (clsLisQCDataVO3 != null)
                            {
                                clsLisQCDataVO3.m_datQCDate = dateTime;
                                e.Row[num.ToString() + "obj"] = clsLisQCDataVO3;
                                this.m_lstChangedVOS.Add(clsLisQCDataVO3);
                            }
                        }
                    }
                }
                if (!columnName.Contains("date") && !columnName.Contains("obj"))
                {
                    this.m_mthComputeSDXCV(columnName);
                }
                this.m_dtbSource.ColumnChanged += new DataColumnChangeEventHandler(this.m_dtbSource_ColumnChanged);
            }
        }

        private void m_dtgEditor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataRow row = ((DataRowView)this.m_dtgEditor.Rows[e.RowIndex].DataBoundItem).Row;
                if (row["strdate"].ToString().Trim() == this.m_strLocalConn)
                {
                    string name = this.m_dtgEditor.Columns[e.ColumnIndex].Name;
                    if (!name.Contains("date"))
                    {
                        row = ((DataRowView)this.m_dtgEditor.Rows[e.RowIndex - 1].DataBoundItem).Row;
                        if (row[name] != DBNull.Value)
                        {
                            double dblCV = Convert.ToDouble(row[name]);
                            row = ((DataRowView)this.m_dtgEditor.Rows[e.RowIndex - 2].DataBoundItem).Row;
                            if (row[name] != DBNull.Value)
                            {
                                double dblSD = Convert.ToDouble(row[name]);
                                row = ((DataRowView)this.m_dtgEditor.Rows[e.RowIndex - 3].DataBoundItem).Row;
                                if (row[name] != DBNull.Value)
                                {
                                    double dblAVG = Convert.ToDouble(row[name]);
                                    if (MessageBox.Show("是否刷新当前浓度！", "质控管理", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                                    {
                                        int num = 0;
                                        int.TryParse(name, out num);
                                        List<clsLisQCConcentrationVO> concentrations = this.m_objBatch.GetConcentrations(num);
                                        if (concentrations != null && concentrations.Count > 0)
                                        {
                                            clsLisQCConcentrationVO clsLisQCConcentrationVO = concentrations[0];
                                            clsLisQCConcentrationVO.m_enmStatus = enmQCStatus.Natrural;
                                            clsLisQCConcentrationVO.m_dblCV = dblCV;
                                            clsLisQCConcentrationVO.m_dblAVG = dblAVG;
                                            clsLisQCConcentrationVO.m_dblSD = dblSD;
                                            long num2 = (new weCare.Proxy.ProxyLis02()).Service.m_lngUpdateSDXCV(clsLisQCConcentrationVO);
                                            if (num2 > 0L)
                                            {
                                                concentrations[0] = clsLisQCConcentrationVO;
                                                this.m_objBatch.UpdateConcentrations(concentrations, num);
                                                MessageBox.Show("刷新当前浓度成功！", "质控管理", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                            }
                                            else
                                            {
                                                MessageBox.Show("刷新当前浓度失败！", "质控管理", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("刷新当前浓度失败！原因是未设置浓度。", "质控管理", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                        }
                                    }
                                }
                            }
                        }
                        this.m_dtgEditor.CurrentCell = this.m_dtgEditor[e.ColumnIndex, e.RowIndex - 1];
                    }
                }
            }
        }

        private void m_dtgEditor_CellEnter(object sender, DataGridViewCellEventArgs e)
        { 
        }

        private void m_dtgEditor_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            this.m_dtgEditor.Rows[e.RowIndex].ErrorText = "";
            if (!this.m_dtgEditor.Rows[e.RowIndex].IsNewRow)
            {
                DataRow row = ((DataRowView)this.m_dtgEditor.Rows[e.RowIndex].DataBoundItem).Row;
                if (row != null)
                {
                    string a = row["strdate"].ToString().Trim();
                    if (!(a == this.m_strLocalConn))
                    {
                        string name = this.m_dtgEditor.Columns[e.ColumnIndex].Name;
                        if (!name.Contains("date") && !name.Contains("obj"))
                        {
                            double num;
                            if ((!double.TryParse(e.FormattedValue.ToString(), out num) || num < 0.0) && e.FormattedValue.ToString() != string.Empty)
                            {
                                e.Cancel = true;
                                this.m_dtgEditor.Rows[e.RowIndex].ErrorText = "输入的数不是整数！";
                            }
                        }
                    }
                }
            }
        }

        private void m_dtgEditor_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int count = this.m_dtgEditor.Rows.Count;
            for (int i = e.RowIndex; i < count; i++)
            {
                DataGridViewRow dgvRow = this.m_dtgEditor.Rows[i];
                if ((DataRowView)dgvRow.DataBoundItem != null)
                {
                    DataRow row = ((DataRowView)dgvRow.DataBoundItem).Row;
                    if (row != null)
                    {
                        string a = row["strdate"].ToString().Trim();
                        if (a == this.m_strLocalX || a == this.m_strLocalSD || a == this.m_strLocalCV)
                        {
                            dgvRow.DefaultCellStyle.ForeColor = Color.Magenta;
                        }
                        if (a == this.m_strLocalConn)
                        {
                            dgvRow.DefaultCellStyle.BackColor = Color.Gray;
                            dgvRow.DefaultCellStyle.SelectionBackColor = Color.LightGray;
                            dgvRow.DefaultCellStyle.ForeColor = Color.Blue;
                        }
                    }
                }
            }
        }

        public void m_mthAddDataTable(clsLisQCDataVO[] p_objQCDataArr)
        {
            if (p_objQCDataArr != null && p_objQCDataArr.Length > 0)
            {
                List<clsLisQCDataVO> list = new List<clsLisQCDataVO>();
                list.AddRange(p_objQCDataArr);
                List<clsQCDataPairItem> qCDataPairItemList = clsQCDataPairItem.GetQCDataPairItemList(list);
                foreach (clsQCDataPairItem current in qCDataPairItemList)
                {
                    string strFilter = string.Empty;
                    strFilter = "date >= '";
                    strFilter += current.QCDate.ToString("yyyy-MM-dd 00:00:00");
                    strFilter += "' and date <= '";
                    strFilter += current.QCDate.ToString("yyyy-MM-dd 23:59:59") + "'";

                    DataRow[] drr = this.m_dtbSource.Select(strFilter);
                    if (drr != null && drr.Length > 0)
                    {
                        DataRow dataRow = drr[0];
                        int[] seqArr = this.m_objBatch.SeqArr;
                        for (int i = 0; i < seqArr.Length; i++)
                        {
                            int p_intBatchSeq = seqArr[i];
                            clsLisQCDataVO qcDataVo = current[p_intBatchSeq];
                            if (qcDataVo != null)
                            {
                                if (dataRow[p_intBatchSeq.ToString()] == DBNull.Value)
                                {
                                    dataRow[p_intBatchSeq.ToString()] = qcDataVo.m_dlbResult;
                                }
                            }
                        }
                    }
                    else
                    {
                        DataRow dataRow = this.m_dtbSource.NewRow();
                        dataRow["date"] = current.QCDate.Date;
                        dataRow["strdate"] = current.QCDate.Date.ToString("yyyy-MM-dd");
                        int[] seqArr = this.m_objBatch.SeqArr;

                        for (int i = 0; i < seqArr.Length; i++)
                        {
                            int p_intBatchSeq = seqArr[i];
                            clsLisQCDataVO qcDataVo = current[p_intBatchSeq];
                            if (qcDataVo != null)
                            {
                                dataRow[p_intBatchSeq.ToString()] = qcDataVo.m_dlbResult;
                                dataRow[p_intBatchSeq.ToString() + "obj"] = qcDataVo;
                            }
                        }
                        this.m_dtbSource.Rows.Add(dataRow);
                    }
                }
            }
        }

        private bool m_mthAddQCData(List<clsLisQCDataVO> p_objArr)
        {
            bool result;
            foreach (clsLisQCDataVO current in p_objArr)
            {
                long num = (new weCare.Proxy.ProxyLis02()).Service.m_lngInsertQCData(current, out current.m_intSeq);
                if (num <= 0L)
                {
                    result = false;
                    return result;
                }
            }
            result = true;
            return result;
        }

        private void m_mthAddRowAt(int pos)
        {
            DataGridViewRow currentRow = this.m_dtgEditor.CurrentRow;
            if (currentRow != null)
            {
                DataRow row = this.m_dtbSource.NewRow();
                if (currentRow.Index + pos > -1)
                {
                    this.m_dtbSource.Rows.InsertAt(row, currentRow.Index + pos);
                }
            }
        }

        private void m_mthComputeSDXCV(string p_strColName)
        {
            if (!string.IsNullOrEmpty(p_strColName))
            {
                if (!p_strColName.Contains("date") && !p_strColName.Contains("obj"))
                {
                    DateTime dateBegin = this.m_objBatch.DateBegin;
                    List<double> list = new List<double>();
                    int i = 0;
                    int num = this.m_dtbSource.Rows.Count - 4;
                    double item = 0.0;
                    for (i = 0; i < num; i++)
                    {
                        DataRow dataRow = this.m_dtbSource.Rows[i];
                        string text = dataRow[p_strColName].ToString().Trim();
                        if (!string.IsNullOrEmpty(text))
                        {
                            if (double.TryParse(text, out item))
                            {
                                list.Add(item);
                            }
                        }
                    }
                    if (list.Count > 0)
                    {
                        double num2;
                        double num3;
                        double num4;
                        clsLISPublic.m_lngCalculateSDXCV(list.ToArray(), out num2, out num3, out num4);
                        this.m_dtbSource.Rows[i][p_strColName] = num2.ToString("0.00");

                        i++;
                        this.m_dtbSource.Rows[i][p_strColName] = num3.ToString("0.000");

                        i++;
                        this.m_dtbSource.Rows[i][p_strColName] = num4.ToString("0.00");

                    }
                    else
                    {
                        this.m_dtbSource.Rows[i][p_strColName] = "";

                        i++;
                        this.m_dtbSource.Rows[i][p_strColName] = "";

                        i++;
                        this.m_dtbSource.Rows[i][p_strColName] = "";

                    }
                }
            }
        }

        public void m_mthConstructDataTable()
        {
            this.m_dtbSource = new DataTable("datalist");
            this.m_dtbSource.Columns.Add("date", typeof(DateTime));
            this.m_dtbSource.Columns.Add("strdate", typeof(string));
            this.m_dtvSource.Table = this.m_dtbSource;
            this.m_dtvSource.Sort = "date asc";
            List<clsLisQCBatchVO> qCBatchSet = this.m_objBatch.GetQCBatchSet();
            foreach (clsLisQCBatchVO current in qCBatchSet)
            {
                this.m_dtbSource.Columns.Add(current.m_intSeq.ToString(), typeof(string)); 
                this.m_dtbSource.Columns.Add(current.m_intSeq.ToString() + "obj", typeof(clsLisQCDataVO)); 
            } 
        }

        public void m_mthDataFilter(double p_dblX, double p_dblSD, ref List<double> p_lisDblData)
        {
            return;
        }

        private bool m_mthDeleteQCData(List<clsLisQCDataVO> p_objArr)
        {
            bool result;
            foreach (clsLisQCDataVO current in p_objArr)
            {
                long num = (new weCare.Proxy.ProxyLis02()).Service.m_lngDeleteQCData(current.m_intSeq);
                if (num <= 0L)
                {
                    result = false;
                    return result;
                }
            }
            result = true;
            return result;
        }

        private void m_mthFillDataTable()
        {
            List<clsQCDataPairItem> lstDataPair = clsQCDataPairItem.GetQCDataPairItemList(m_lstWorkedData);
            m_dtbSource.Rows.Clear();
            List<int> seqArr = new List<int>();
            DateTime dteBegin = m_objBatch.DateBegin;
            DateTime dteEnd;

            while (true)
            {
                dteEnd = this.m_objBatch.DateEnd;

                if (!(dteBegin <= dteEnd.Date))
                {
                    break;
                }

                DataRow dataRow = this.m_dtbSource.NewRow();
                dataRow["date"] = dteBegin.Date;
                dataRow["strdate"] = dteBegin.Date.ToString("yyyy-MM-dd");

                foreach (clsQCDataPairItem pair in lstDataPair)
                {
                    dteEnd = pair.QCDate;
                    if (dteEnd.Date == dteBegin.Date)
                    {
                        seqArr.Clear();
                        seqArr.AddRange(m_objBatch.SeqArr); // 20190804
                        //seqArr.Add(this.m_objBatch.SeqArr[0]) ;
                        //seqArr.Add(Convert.ToInt16(this.m_objBatch.SeqArr[0].ToString() + "1") );
                        //seqArr.Add(Convert.ToInt16(this.m_objBatch.SeqArr[0].ToString() + "2") );

                        for (int i = 0; i < seqArr.Count; i++)
                        {
                            int p_intBatchSeq = seqArr[i];

                            if (pair[p_intBatchSeq] != null)
                            {
                                dataRow[p_intBatchSeq.ToString()] = pair[p_intBatchSeq].m_dlbResult;
                                dataRow[p_intBatchSeq.ToString() + "obj"] = pair[p_intBatchSeq];
                            }
                        }
                    }
                }
                this.m_dtbSource.Rows.Add(dataRow);
                dteBegin = dteBegin.AddDays(1.0);
            }

            dteEnd = this.m_objBatch.DateEnd;
            dteBegin = dteEnd.Date;


            DataRow dataRow2 = this.m_dtbSource.NewRow();
            dataRow2["date"] = dteBegin.AddHours(20.0);
            dataRow2["strdate"] = this.m_strLocalX;
            this.m_dtbSource.Rows.Add(dataRow2);
            DataRow dataRow3 = this.m_dtbSource.NewRow();
            dataRow3["date"] = dteBegin.AddHours(21.0);
            dataRow3["strdate"] = this.m_strLocalSD;
            this.m_dtbSource.Rows.Add(dataRow3);
            DataRow dataRow4 = this.m_dtbSource.NewRow();
            dataRow4["date"] = dteBegin.AddHours(22.0);
            dataRow4["strdate"] = this.m_strLocalCV;
            this.m_dtbSource.Rows.Add(dataRow4);
            DataRow dataRow5 = this.m_dtbSource.NewRow();
            dataRow5["date"] = dteBegin.AddHours(23.0);
            dataRow5["strdate"] = this.m_strLocalConn;
            this.m_dtbSource.Rows.Add(dataRow5);

            List<double> list = null;
            clsLisQCConcentrationVO clsLisQCConcentrationVO = null;
            seqArr.Clear();
            seqArr.AddRange(m_objBatch.SeqArr); // 20190804
            //seqArr.Add(this.m_objBatch.SeqArr[0]);
            //seqArr.Add(Convert.ToInt16(this.m_objBatch.SeqArr[0].ToString() + "1"));
            //seqArr.Add(Convert.ToInt16(this.m_objBatch.SeqArr[0].ToString() + "2"));
            for (int i = 0; i < seqArr.Count; i++)
            {
                int p_intBatchSeq = seqArr[i];
                dataRow5[p_intBatchSeq.ToString()] = this.m_strFlashConn;
                list = this.m_objBatch.m_mthGetDatas(p_intBatchSeq);
                if (list != null && list.Count > 0)
                {
                    List<clsLisQCConcentrationVO> concentrations = this.m_objBatch.GetConcentrations(p_intBatchSeq);
                    if (concentrations != null && concentrations.Count > 0)
                    {
                        clsLisQCConcentrationVO = concentrations[0];
                    }
                    if (clsLisQCConcentrationVO != null)
                    {
                        this.m_mthDataFilter(clsLisQCConcentrationVO.m_dblAVG, clsLisQCConcentrationVO.m_dblSD, ref list);
                    }
                    double num;
                    double num2;
                    double num3;
                    clsLISPublic.m_lngCalculateSDXCV(list.ToArray(), out num, out num2, out num3);
                    dataRow2[p_intBatchSeq.ToString()] = num.ToString("0.00");
                    dataRow3[p_intBatchSeq.ToString()] = num2.ToString("0.00");
                    dataRow4[p_intBatchSeq.ToString()] = num3.ToString("0.00");
                }
            }
            this.m_dtbSource.AcceptChanges();
        }

        private bool m_mthIsDataUnchanged(List<clsLisQCDataVO> p_lstPrev, List<clsLisQCDataVO> p_lstAfter)
        {
            bool result;
            if (p_lstPrev == null && p_lstAfter == null)
            {
                result = true;
            }
            else
            {
                if ((p_lstPrev == null && p_lstAfter != null) || (p_lstAfter == null && p_lstPrev != null))
                {
                    result = false;
                }
                else
                {
                    if (p_lstPrev.Count != p_lstAfter.Count)
                    {
                        result = false;
                    }
                    else
                    {
                        for (int i = 0; i < p_lstPrev.Count; i++)
                        {
                            if (p_lstPrev[i] != null || p_lstAfter[i] != null)
                            {
                                if ((p_lstPrev[i] == null && p_lstAfter[i] != null) || (p_lstAfter[i] == null && p_lstPrev[i] != null))
                                {
                                    result = false;
                                    return result;
                                }
                                if (!p_lstPrev[i].m_mthEquals(p_lstAfter[i]))
                                {
                                    result = false;
                                    return result;
                                }
                            }
                        }
                        result = true;
                    }
                }
            }
            return result;
        }

        private bool m_mthUpdateQCData(List<clsLisQCDataVO> p_objArr)
        {
            bool result;
            foreach (clsLisQCDataVO current in p_objArr)
            {
                long num = (new weCare.Proxy.ProxyLis02()).Service.m_lngUpdateQCData(current);
                if (num <= 0L)
                {
                    result = false;
                    return result;
                }
            }
            result = true;
            return result;
        }

        private void m_objBatch_ConcentrationChanged(object sender, EventArgs e)
        {
            this.m_objBatch_Loaded(null, null); 
        }

        private void m_objBatch_DataChanged(object sender, EventArgs e)
        {
            this.m_dtbSource.ColumnChanged -= new DataColumnChangeEventHandler(this.m_dtbSource_ColumnChanged);
            this.m_lstWorkedData = this.m_objBatch.GetDatas();
            this.m_lstChangedVOS.Clear();
            this.m_lstAddedVOS.Clear();
            this.m_lstDeletedVOS.Clear();
            this.m_dtpStartDate.Value = DateTime.Now.Date;
            this.m_dtpEndDate.Value = DateTime.Now;
            this.m_mthFillDataTable();
            this.m_dtbSource.ColumnChanged += new DataColumnChangeEventHandler(this.m_dtbSource_ColumnChanged);
        }

        private void m_objBatch_Loaded(object sender, EventArgs e)
        {
            this.m_lstWorkedData = this.m_objBatch.GetDatas();
            this.m_lstChangedVOS.Clear();
            this.m_lstAddedVOS.Clear();
            this.m_lstDeletedVOS.Clear();
            this.m_cmdAddRowAfter.Enabled = true;
            this.m_cmdAddRowPrev.Enabled = true;
            this.m_cmdDeleteRow.Enabled = true;
            this.m_cmdSave.Enabled = true;
            this.m_dtgEditor.Enabled = true;
            this.m_dtpStartDate.Value = DateTime.Now.Date;
            this.m_dtpEndDate.Value = DateTime.Now;
            this.m_cmdDataImpot.Enabled = true;
            this.m_dtgEditor.DataSource = null;
            this.InitializeStyleEditor();
            this.m_mthConstructDataTable();
            this.m_mthFillDataTable();
            this.m_dtgEditor.DataSource = this.m_dtvSource;
            this.m_dtbSource.ColumnChanged += new DataColumnChangeEventHandler(this.m_dtbSource_ColumnChanged);
        }

        private void m_objBatch_Reloaded(object sender, EventArgs e)
        {
            this.m_objBatch_DataChanged(null, null); 
        }

        private void m_objBatch_Reseted(object sender, EventArgs e)
        {
            this.m_lstChangedVOS.Clear();
            this.m_lstAddedVOS.Clear();
            this.m_lstDeletedVOS.Clear();
            this.m_lstWorkedData.Clear();
            this.m_dtgEditor.DataSource = null;
            this.m_dtgEditor.Columns.Clear();
            this.m_dtvSource.Table = null;
            this.m_dtbSource = null;
            this.m_cmdAddRowAfter.Enabled = false;
            this.m_cmdAddRowPrev.Enabled = false;
            this.m_cmdDeleteRow.Enabled = false;
            this.m_cmdSave.Enabled = false;
            this.m_dtgEditor.Enabled = false;
            this.m_dtpStartDate.Value = DateTime.Now.Date;
            this.m_dtpEndDate.Value = DateTime.Now;
            this.m_cmdDataImpot.Enabled = false;
        }

        // Properties
        internal clsQCBatchNew ObjBatch
        {
            set
            {
                if (value != null)
                {
                    this.m_objBatch = value;
                    if (this.m_objBatch.IsNull)
                    {
                        this.m_objBatch_Reseted(null, null);
                    }
                    else
                    {
                        this.m_objBatch_Loaded(null, null);
                    }
                    this.m_objBatch.Reseted += new EventHandler(this.m_objBatch_Reseted);
                    this.m_objBatch.Loaded += new EventHandler(this.m_objBatch_Loaded);
                    this.m_objBatch.Reloaded += new EventHandler(this.m_objBatch_Reloaded);
                    this.m_objBatch.DataChanged += new EventHandler(this.m_objBatch_DataChanged);
                    return;
                }
                throw new ArgumentNullException();
            }
        }
    }

    public class DataGridViewDateTimeColumn : DataGridViewColumn
    {
        public DataGridViewDateTimeColumn()
            : base(new DataGridViewDateTimeCell())
        {

        }
        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                if (value != null && !value.GetType().IsAssignableFrom(typeof(DataGridViewDateTimeCell)))
                {
                    throw new InvalidCastException("不是DataGridViewDateTimeCell");
                }
                base.CellTemplate = value;
            }
        }
    }

    public class DataGridViewDateTimeCell : DataGridViewTextBoxCell
    {
        public DataGridViewDateTimeCell()
        {
            this.Style.Format = "d";
        }
        public override void InitializeEditingControl(int rowIndex, object
       initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue,
                dataGridViewCellStyle);
            DataGridViewDateTimeEditingControl ctl =
                DataGridView.EditingControl as DataGridViewDateTimeEditingControl;
            try
            {
                ctl.Value = (DateTime)this.Value;
            }
            catch
            {
                ctl.Value = DateTime.Now;
            }
        }

        public override Type EditType
        {
            get
            {
                return typeof(DataGridViewDateTimeEditingControl);
            }
        }

        public override Type ValueType
        {
            get
            {
                return typeof(DateTime);
            }
        }

        public override object DefaultNewRowValue
        {
            get
            {
                return DateTime.Now;
            }
        }
    }

    public class DataGridViewDateTimeEditingControl : DateTimePicker, IDataGridViewEditingControl
    {
        protected int rowIndex;
        protected DataGridView dataGridView;
        protected bool valueChanged = false;

        public DataGridViewDateTimeEditingControl()
        {

        }

        //重写基类
        protected override void OnValueChanged(EventArgs e)
        {
            base.OnValueChanged(e);
            NotifyDataGridViewOfValueChange();
        }
        //  当text值发生变化时，通知DataGridView
        private void NotifyDataGridViewOfValueChange()
        {
            valueChanged = true;
            dataGridView.NotifyCurrentCellDirty(true);
        }
        /// <summary>
        /// 在Cell被编辑的时候光标显示

        /// </summary>
        public Cursor EditingPanelCursor
        {
            get
            {
                return Cursors.IBeam;
            }
        }
        /// <summary>
        /// 获取或设置所在的DataGridView
        /// </summary>
        public DataGridView EditingControlDataGridView
        {
            get
            {
                return dataGridView;
            }

            set
            {
                dataGridView = value;
            }
        }

        /// <summary>
        /// 获取或设置格式化后的值

        /// </summary>
        public object EditingControlFormattedValue
        {
            set
            {
                Text = value.ToString();
                NotifyDataGridViewOfValueChange();
            }
            get
            {
                return this.Text;
            }

        }
        /// <summary>
        /// 获取控件的Text值

        /// </summary>
        /// <param name="context">错误上下文</param>
        /// <returns></returns>
        public virtual object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return Text;
        }

        /// <summary>
        /// 编辑键盘
        /// </summary>
        /// <param name="keyData"></param>
        /// <param name="dataGridViewWantsInputKey"></param>
        /// <returns></returns>
        public bool EditingControlWantsInputKey(
       Keys key, bool dataGridViewWantsInputKey)
        {
            // Let the DateTimePicker handle the keys listed.
            switch (key & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Home:
                case Keys.End:
                case Keys.PageDown:
                case Keys.PageUp:
                    return true;
                default:
                    return false;
            }
        }

        public void PrepareEditingControlForEdit(bool selectAll)
        {
        }
        public virtual bool RepositionEditingControlOnValueChange
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// 控件所在行
        /// </summary>
        public int EditingControlRowIndex
        {
            get
            {
                return this.rowIndex;
            }

            set
            {
                this.rowIndex = value;

            }
        }
        /// <summary>
        /// 设置样式
        /// </summary>
        /// <param name="dataGridViewCellStyle"></param>
        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
            this.CalendarForeColor = dataGridViewCellStyle.ForeColor;
            this.CalendarMonthBackground = dataGridViewCellStyle.BackColor;
        }
        /// <summary>
        /// 是否值发生了变化
        /// </summary>
        public bool EditingControlValueChanged
        {
            get
            {
                return valueChanged;
            }

            set
            {
                this.valueChanged = value;
            }
        }
    }
}
