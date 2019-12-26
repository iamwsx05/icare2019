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
    public class frmQCBatchConcentrationSet : frmMDI_Child_Base
    {
        // Fields
        internal ColumnHeader chConcentrationID;
        internal ColumnHeader chConcentrationName;
        internal ColumnHeader chCV;
        internal ColumnHeader chDeviceSampleID;
        internal ColumnHeader chSD;
        internal ColumnHeader chStatus;
        internal ColumnHeader chX;
        private IContainer components;
        internal Label label12;
        internal Label label13;
        internal Label label14;
        internal Label label2;
        internal Label label3;
        private bool m_blnDBErr;
        private bool m_blnNew;
        internal ctlConcentrationCombox m_cboConcentration;
        internal ButtonXP m_cmdCancelDelete;
        internal ButtonXP m_cmdDelete;
        internal ButtonXP m_cmdNew;
        internal ButtonXP m_cmdReturn;
        internal ButtonXP m_cmdSave;
        internal GroupBox m_gpbHead;
        private int m_intQCBatchSeq;
        internal ListView m_lsvConcentration;
        private List<clsLisQCConcentrationVO> m_objDeletedQCContrations;
        //private clsDcl_QCDataBusiness m_objDomain;
        private List<clsLisQCConcentrationVO> m_objQCContrations;
        internal Panel m_pnlBottom;
        internal TextBox m_txtCV;
        internal TextBox m_txtDeviceSampleID;
        internal TextBox m_txtSD;
        internal TextBox m_txtX;

        // Methods
        public frmQCBatchConcentrationSet(int p_intQCBatchSeq, clsLisQCConcentrationVO[] p_objQCContrationVOArr)
        {
            this.m_blnDBErr = false;
            this.m_blnNew = false;
            this.components = null;
            this.InitializeComponent();

            this.m_intQCBatchSeq = p_intQCBatchSeq;
            if (p_objQCContrationVOArr != null)
            {
                foreach (clsLisQCConcentrationVO var in p_objQCContrationVOArr)
                {
                    if (var != null)
                        this.m_objQCContrations.Add(var);
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

        private void frmQCBatchConcentrationSet_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.m_blnDBErr)
            {
                this.DialogResult = DialogResult.Cancel;
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void frmQCBatchConcentrationSet_Load(object sender, EventArgs e)
        {
            m_mthLoadDeletedData();
            m_mthShowList();
            this.Text = this.Text + " - 质控批序号:" + this.m_intQCBatchSeq.ToString();
        }

        private void frmReportQuery_KeyDown(object sender, KeyEventArgs e)
        {
            this.m_mthShortCutKey(e.KeyCode);
            this.m_mthSetKeyTab(e); 
        }

        private void InitializeComponent()
        {
            ColumnHeader[] headerArray;
            this.m_gpbHead = new GroupBox();
            this.m_txtCV = new TextBox();
            this.m_txtSD = new TextBox();
            this.m_txtX = new TextBox();
            this.label14 = new Label();
            this.label13 = new Label();
            this.label12 = new Label();
            this.m_cboConcentration = new ctlConcentrationCombox();
            this.label3 = new Label();
            this.m_txtDeviceSampleID = new TextBox();
            this.label2 = new Label();
            this.m_lsvConcentration = new ListView();
            this.chConcentrationID = new ColumnHeader();
            this.chConcentrationName = new ColumnHeader();
            this.chDeviceSampleID = new ColumnHeader();
            this.chX = new ColumnHeader();
            this.chSD = new ColumnHeader();
            this.chCV = new ColumnHeader();
            this.chStatus = new ColumnHeader();
            this.m_pnlBottom = new Panel();
            this.m_cmdCancelDelete = new ButtonXP();
            this.m_cmdReturn = new ButtonXP();
            this.m_cmdSave = new ButtonXP();
            this.m_cmdNew = new ButtonXP();
            this.m_cmdDelete = new ButtonXP();
            this.m_gpbHead.SuspendLayout();
            this.m_pnlBottom.SuspendLayout();
            base.SuspendLayout();
            this.m_gpbHead.Controls.Add(this.m_txtCV);
            this.m_gpbHead.Controls.Add(this.m_txtSD);
            this.m_gpbHead.Controls.Add(this.m_txtX);
            this.m_gpbHead.Controls.Add(this.label14);
            this.m_gpbHead.Controls.Add(this.label13);
            this.m_gpbHead.Controls.Add(this.label12);
            this.m_gpbHead.Controls.Add(this.m_cboConcentration);
            this.m_gpbHead.Controls.Add(this.label3);
            this.m_gpbHead.Controls.Add(this.m_txtDeviceSampleID);
            this.m_gpbHead.Controls.Add(this.label2);
            this.m_gpbHead.Dock = DockStyle.Top;
            this.m_gpbHead.Location = new Point(0, 0);
            this.m_gpbHead.Name = "m_gpbHead";
            this.m_gpbHead.Size = new Size(0x257, 0x72);
            this.m_gpbHead.TabIndex = 1;
            this.m_gpbHead.TabStop = false;
            this.m_txtCV.ImeMode = ImeMode.Off;
            this.m_txtCV.Location = new Point(0x1c0, 0x2c);
            this.m_txtCV.MaxLength = 5;
            this.m_txtCV.Name = "m_txtCV";
            this.m_txtCV.Size = new Size(120, 0x17);
            this.m_txtCV.TabIndex = 0x1a;
            this.m_txtCV.Enter += new EventHandler(this.m_txtCV_Enter);
            this.m_txtSD.ImeMode = ImeMode.Off;
            this.m_txtSD.Location = new Point(0x120, 0x44);
            this.m_txtSD.MaxLength = 8;
            this.m_txtSD.Name = "m_txtSD";
            this.m_txtSD.Size = new Size(120, 0x17);
            this.m_txtSD.TabIndex = 0x19;
            this.m_txtX.ImeMode = ImeMode.Off;
            this.m_txtX.Location = new Point(0x120, 0x2c);
            this.m_txtX.MaxLength = 10;
            this.m_txtX.Name = "m_txtX";
            this.m_txtX.Size = new Size(120, 0x17);
            this.m_txtX.TabIndex = 0x18;
            this.label14.AutoSize = true;
            this.label14.Location = new Point(0x1a2, 0x30);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x23, 14);
            this.label14.TabIndex = 0x1d;
            this.label14.Text = "CV：";
            this.label13.AutoSize = true;
            this.label13.Location = new Point(0x102, 0x48);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x23, 14);
            this.label13.TabIndex = 0x1c;
            this.label13.Text = "SD：";
            this.label12.AutoSize = true;
            this.label12.Location = new Point(0xf4, 0x30);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x31, 14);
            this.label12.TabIndex = 0x1b;
            this.label12.Text = "真值：";
            this.m_cboConcentration.DropDownStyle = ComboBoxStyle.DropDownList;
            this.m_cboConcentration.Enabled = false;
            this.m_cboConcentration.FormattingEnabled = true;
            this.m_cboConcentration.Location = new Point(0x6c, 0x2c);
            this.m_cboConcentration.Name = "m_cboConcentration";
            this.m_cboConcentration.Size = new Size(120, 0x16);
            this.m_cboConcentration.TabIndex = 1;
            this.m_cboConcentration.Value = -2147483648;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x3a, 0x30);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x2a, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "浓度:";
            this.m_txtDeviceSampleID.Enabled = false;
            this.m_txtDeviceSampleID.Location = new Point(0x6c, 0x44);
            this.m_txtDeviceSampleID.MaxLength = 0x19;
            this.m_txtDeviceSampleID.Name = "m_txtDeviceSampleID";
            this.m_txtDeviceSampleID.Size = new Size(120, 0x17);
            this.m_txtDeviceSampleID.TabIndex = 2;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 0x48);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x54, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "仪器标本号:";
            this.m_lsvConcentration.Columns.AddRange(new ColumnHeader[] { this.chConcentrationID, this.chConcentrationName, this.chDeviceSampleID, this.chX, this.chSD, this.chCV, this.chStatus });
            this.m_lsvConcentration.Dock = DockStyle.Fill;
            this.m_lsvConcentration.FullRowSelect = true;
            this.m_lsvConcentration.GridLines = true;
            this.m_lsvConcentration.HideSelection = false;
            this.m_lsvConcentration.Location = new Point(0, 0x72);
            this.m_lsvConcentration.MultiSelect = false;
            this.m_lsvConcentration.Name = "m_lsvConcentration";
            this.m_lsvConcentration.Size = new Size(0x257, 0xa9);
            this.m_lsvConcentration.TabIndex = 6;
            this.m_lsvConcentration.UseCompatibleStateImageBehavior = false;
            this.m_lsvConcentration.View = View.Details;
            this.m_lsvConcentration.SelectedIndexChanged += new EventHandler(this.m_lsvConcentration_SelectedIndexChanged);
            this.chConcentrationID.Text = "浓度ID";
            this.chConcentrationID.Width = 0x36;
            this.chConcentrationName.Text = "浓度";
            this.chConcentrationName.Width = 100;
            this.chDeviceSampleID.Text = "仪器标本号";
            this.chDeviceSampleID.Width = 120;
            this.chX.Text = "真值";
            this.chSD.Text = "SD";
            this.chSD.Width = 0x48;
            this.chCV.Text = "CV";
            this.chCV.Width = 70;
            this.chStatus.Text = "状态";
            this.m_pnlBottom.Controls.Add(this.m_cmdCancelDelete);
            this.m_pnlBottom.Controls.Add(this.m_cmdReturn);
            this.m_pnlBottom.Controls.Add(this.m_cmdSave);
            this.m_pnlBottom.Controls.Add(this.m_cmdNew);
            this.m_pnlBottom.Controls.Add(this.m_cmdDelete);
            this.m_pnlBottom.Dock = DockStyle.Bottom;
            this.m_pnlBottom.Location = new Point(0, 0x11b);
            this.m_pnlBottom.Name = "m_pnlBottom";
            this.m_pnlBottom.Size = new Size(0x257, 0x55);
            this.m_pnlBottom.TabIndex = 7;
            this.m_cmdCancelDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdCancelDelete.BackColor = Color.FromArgb(0, 0xec, 0xe9, 0xd8);
            this.m_cmdCancelDelete.DefaultScheme = true;
            this.m_cmdCancelDelete.DialogResult = 0;
            this.m_cmdCancelDelete.Enabled = false;
            this.m_cmdCancelDelete.Hint = "";
            this.m_cmdCancelDelete.Location = new Point(0x24, 20);
            this.m_cmdCancelDelete.Name = "m_cmdCancelDelete";
            this.m_cmdCancelDelete.Scheme = 0;
            this.m_cmdCancelDelete.Size = new Size(100, 0x26);
            this.m_cmdCancelDelete.TabIndex = 0x10;
            this.m_cmdCancelDelete.Text = "取消删除";
            this.m_cmdCancelDelete.Click += new EventHandler(this.m_cmdCancelDelete_Click);
            this.m_cmdReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdReturn.BackColor = Color.FromArgb(0, 0xec, 0xe9, 0xd8);
            this.m_cmdReturn.DefaultScheme = true;
            this.m_cmdReturn.DialogResult = 0;
            this.m_cmdReturn.Hint = "";
            this.m_cmdReturn.Location = new Point(0x1d8, 20);
            this.m_cmdReturn.Name = "m_cmdReturn";
            this.m_cmdReturn.Scheme = 0;
            this.m_cmdReturn.Size = new Size(100, 0x26);
            this.m_cmdReturn.TabIndex = 15;
            this.m_cmdReturn.Text = "返回";
            this.m_cmdReturn.Click += new EventHandler(this.m_cmdReturn_Click);
            this.m_cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdSave.BackColor = Color.FromArgb(0, 0xec, 0xe9, 0xd8);
            this.m_cmdSave.DefaultScheme = true;
            this.m_cmdSave.DialogResult = 0;
            this.m_cmdSave.Enabled = false;
            this.m_cmdSave.Hint = "";
            this.m_cmdSave.Location = new Point(0x100, 20);
            this.m_cmdSave.Name = "m_cmdSave";
            this.m_cmdSave.Scheme = 0;
            this.m_cmdSave.Size = new Size(100, 0x26);
            this.m_cmdSave.TabIndex = 12;
            this.m_cmdSave.Text = "保存";
            this.m_cmdSave.Click += new EventHandler(this.m_cmdSave_Click);
            this.m_cmdNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdNew.BackColor = Color.FromArgb(0, 0xec, 0xe9, 0xd8);
            this.m_cmdNew.DefaultScheme = true;
            this.m_cmdNew.DialogResult = 0;
            this.m_cmdNew.Hint = "";
            this.m_cmdNew.Location = new Point(0x93, 20);
            this.m_cmdNew.Name = "m_cmdNew";
            this.m_cmdNew.Scheme = 0;
            this.m_cmdNew.Size = new Size(100, 0x26);
            this.m_cmdNew.TabIndex = 11;
            this.m_cmdNew.Text = "新增";
            this.m_cmdNew.Click += new EventHandler(this.m_cmdNew_Click);
            this.m_cmdDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdDelete.BackColor = Color.FromArgb(0, 0xec, 0xe9, 0xd8);
            this.m_cmdDelete.DefaultScheme = true;
            this.m_cmdDelete.DialogResult = 0;
            this.m_cmdDelete.Enabled = false;
            this.m_cmdDelete.Hint = "";
            this.m_cmdDelete.Location = new Point(0x16b, 20);
            this.m_cmdDelete.Name = "m_cmdDelete";
            this.m_cmdDelete.Scheme = 0;
            this.m_cmdDelete.Size = new Size(100, 0x26);
            this.m_cmdDelete.TabIndex = 14;
            this.m_cmdDelete.Text = "删除";
            this.m_cmdDelete.Click += new EventHandler(this.m_cmdDelete_Click);
            this.AutoScaleDimensions = new SizeF(7f, 14f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(0x257, 0x170);
            this.Controls.Add(this.m_lsvConcentration);
            this.Controls.Add(this.m_pnlBottom);
            this.Controls.Add(this.m_gpbHead);
            this.Font = new Font("宋体", 10.5f);
            this.KeyPreview = true;
            this.Name = "frmQCBatchConcentrationSet";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "质控批浓度设置";
            this.Load += new EventHandler(this.frmQCBatchConcentrationSet_Load);
            this.FormClosed += new FormClosedEventHandler(this.frmQCBatchConcentrationSet_FormClosed);
            this.KeyDown += new KeyEventHandler(this.frmReportQuery_KeyDown);
            this.m_gpbHead.ResumeLayout(false);
            this.m_gpbHead.PerformLayout();
            this.m_pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false); 
        }

        private bool m_blnModifyCheck()
        {
            if (DBAssist.IsNull(this.m_cboConcentration.Value))
            {
                MessageBox.Show("浓度不能为空！", "iCare");
                this.m_cboConcentration.Focus();
                return false;
            }
            if (this.m_txtX.Text.Trim() != string.Empty)
            {
                double b;
                if (!double.TryParse(this.m_txtX.Text.Trim(), out b))
                {
                    MessageBox.Show("请输入数字!", "iCare");
                    this.m_txtX.Focus();
                    this.m_txtX.SelectAll();
                    return false;
                }
            }
            if (this.m_txtSD.Text.Trim() != string.Empty)
            {
                double b;
                if (!double.TryParse(this.m_txtSD.Text.Trim(), out b))
                {
                    MessageBox.Show("请输入数字!", "iCare");
                    this.m_txtSD.Focus();
                    this.m_txtSD.SelectAll();
                    return false;
                }
            }
            if (this.m_txtCV.Text.Trim() != string.Empty)
            {
                double b;
                if (!double.TryParse(this.m_txtCV.Text.Trim(), out b))
                {
                    MessageBox.Show("请输入数字!", "iCare");
                    this.m_txtCV.Focus();
                    this.m_txtCV.SelectAll();
                    return false;
                }
            }
            return true;
        }

        private bool m_blnNewCheck()
        {
            if (!this.m_blnModifyCheck())
            {
                return false;
            }
            foreach (clsLisQCConcentrationVO obj in this.m_objQCContrations)
            {
                if (obj.m_intConcentrationSeq == this.m_cboConcentration.Value)
                {
                    MessageBox.Show("不能加入重复的浓度！", "iCare");
                    this.m_cboConcentration.Focus();
                    return false;
                }
            }
            return true;
        }

        private void m_cmdCancelDelete_Click(object sender, EventArgs e)
        {
            if (this.m_lsvConcentration.SelectedItems.Count <= 0)
                return;
            Cursor.Current = Cursors.WaitCursor;
            this.m_cmdCancelDelete.Enabled = false;

            clsLisQCConcentrationVO obj = (clsLisQCConcentrationVO)this.m_lsvConcentration.SelectedItems[0].Tag;
            clsLisQCConcentrationVO objCopy = new clsLisQCConcentrationVO();
            obj.m_mthCopyTo(objCopy);
            objCopy.m_enmStatus = enmQCStatus.Natrural;

            long lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngUpdateQCConcentration(objCopy);

            if (lngRes > 0)
            {//成功
                obj.m_enmStatus = enmQCStatus.Natrural;
                this.m_objDeletedQCContrations.Remove(obj);
                this.m_objQCContrations.Add(obj);
                //
                this.m_lsvConcentration.SelectedItems[0].SubItems[6].Text = "";
                this.m_lsvConcentration.SelectedItems[0].ForeColor = this.m_lsvConcentration.ForeColor;
                this.m_lsvConcentration_SelectedIndexChanged(this.m_lsvConcentration, null);
            }
            else
            {//失败
                clsCommonDialog.m_mthShowDBError();
            }

            this.m_cmdCancelDelete.Enabled = true;
            Cursor.Current = Cursors.Default; 
        }

        private void m_cmdDelete_Click(object sender, EventArgs e)
        {
            if (this.m_lsvConcentration.SelectedItems.Count <= 0)
                return;
            Cursor.Current = Cursors.WaitCursor;
            this.m_cmdDelete.Enabled = false;

            clsLisQCConcentrationVO obj = (clsLisQCConcentrationVO)this.m_lsvConcentration.SelectedItems[0].Tag;
            clsLisQCConcentrationVO objCopy = new clsLisQCConcentrationVO();
            obj.m_mthCopyTo(objCopy);
            objCopy.m_enmStatus = enmQCStatus.Delete;

            long lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngUpdateQCConcentration(objCopy);

            if (lngRes > 0)
            {//成功
                obj.m_enmStatus = enmQCStatus.Delete;
                this.m_objQCContrations.Remove(obj);
                this.m_objDeletedQCContrations.Add(obj);
                //
                this.m_lsvConcentration.SelectedItems[0].SubItems[6].Text = "╳";
                this.m_lsvConcentration.SelectedItems[0].ForeColor = Color.Red;
                this.m_lsvConcentration_SelectedIndexChanged(this.m_lsvConcentration, null);
            }
            else
            {//失败
                clsCommonDialog.m_mthShowDBError();
            }

            this.m_cmdDelete.Enabled = true;
            Cursor.Current = Cursors.Default; 
        }

        private void m_cmdNew_Click(object sender, EventArgs e)
        {
            this.m_cboConcentration.Value = DBAssist.NullInt;
            this.m_cboConcentration.Enabled = true;
            this.m_txtDeviceSampleID.Clear();
            this.m_txtDeviceSampleID.Enabled = true;
            this.m_txtCV.Enabled = true;
            this.m_txtSD.Enabled = true;
            this.m_txtX.Enabled = true;

            this.m_cmdNew.Enabled = false;
            this.m_cmdDelete.Enabled = false;
            this.m_cmdSave.Enabled = true;
            this.m_cmdReturn.Enabled = true;
            this.m_cmdCancelDelete.Enabled = false;

            //使当前ListView具有焦点的行失去焦点
            if (this.m_lsvConcentration.FocusedItem != null)
            {
                this.m_lsvConcentration.FocusedItem.Selected = false;
                this.m_lsvConcentration.FocusedItem.Focused = false;
            }

            //设置光标焦点
            this.m_cboConcentration.Focus();

            //设置新增标志
            this.m_blnNew = true;
        }

        private void m_cmdReturn_Click(object sender, EventArgs e)
        {
            base.Close(); 
        }

        public void m_cmdSave_Click(object sender, EventArgs e)
        {
            if (this.m_lsvConcentration.SelectedItems.Count <= 0
                && !this.m_blnNew)
                return;

            Cursor.Current = Cursors.WaitCursor;
            this.m_cmdSave.Enabled = false;

            if (this.m_blnNew)
            {//新增的保存
                if (this.m_blnNewCheck())
                {
                    clsLisQCConcentrationVO objQCConcentration = new clsLisQCConcentrationVO();
                    objQCConcentration.m_enmStatus = enmQCStatus.Natrural;
                    objQCConcentration.m_intConcentrationSeq = m_cboConcentration.Value;
                    objQCConcentration.m_intQCBatchSeq = m_intQCBatchSeq;
                    objQCConcentration.m_strConcentration = m_cboConcentration.Text;
                    objQCConcentration.m_strDeviceSampleId = m_txtDeviceSampleID.Text.Trim();
                    try { objQCConcentration.m_dblAVG = Convert.ToDouble(m_txtX.Text.Trim()); }
                    catch { objQCConcentration.m_dblAVG = DBAssist.NullDouble; }
                    try { objQCConcentration.m_dblSD = Convert.ToDouble(m_txtSD.Text.Trim()); }
                    catch { objQCConcentration.m_dblSD = DBAssist.NullDouble; }

                    try { objQCConcentration.m_dblCV = Convert.ToDouble(m_txtCV.Text.Trim()); }
                    catch { objQCConcentration.m_dblCV = DBAssist.NullDouble; }

                    long lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngInsertQCConcentration(objQCConcentration);

                    if (lngRes > 0)
                    {//成功
                        //更新状态标志
                        this.m_blnNew = false;
                        //加入到集合
                        this.m_objQCContrations.Add(objQCConcentration);
                        //添加新项
                        ListViewItem item = new ListViewItem(objQCConcentration.m_intConcentrationSeq.ToString());
                        item.SubItems.Add(objQCConcentration.m_strConcentration);
                        item.SubItems.Add(objQCConcentration.m_strDeviceSampleId);
                        item.SubItems.Add(DBAssist.ToString(objQCConcentration.m_dblAVG));
                        item.SubItems.Add(DBAssist.ToString(objQCConcentration.m_dblSD));
                        item.SubItems.Add(DBAssist.ToString(objQCConcentration.m_dblCV));
                        item.SubItems.Add("");
                        item.Tag = objQCConcentration;
                        this.m_lsvConcentration.Items.Add(item);
                        this.m_lsvConcentration.Focus();
                        item.Selected = true;
                        item.Focused = true;
                    }
                    else
                    {//失败
                        clsCommonDialog.m_mthShowDBError();
                    }
                }
            }
            else if (this.m_blnModifyCheck())
            {//修改的保存
                clsLisQCConcentrationVO objConcentration = (clsLisQCConcentrationVO)this.m_lsvConcentration.SelectedItems[0].Tag;

                clsLisQCConcentrationVO objTemp = new clsLisQCConcentrationVO();
                objConcentration.m_mthCopyTo(objTemp);
                objTemp.m_strDeviceSampleId = this.m_txtDeviceSampleID.Text.Trim();
                try { objTemp.m_dblAVG = Convert.ToDouble(m_txtX.Text.Trim()); }
                catch { objTemp.m_dblAVG = DBAssist.NullDouble; }
                try { objTemp.m_dblSD = Convert.ToDouble(m_txtSD.Text.Trim()); }
                catch { objTemp.m_dblSD = DBAssist.NullDouble; }

                try { objTemp.m_dblCV = Convert.ToDouble(m_txtCV.Text.Trim()); }
                catch { objTemp.m_dblCV = DBAssist.NullDouble; }

                long lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngUpdateQCConcentration(objTemp);

                if (lngRes > 0)
                {//成功
                    objTemp.m_mthCopyTo(objConcentration);
                    this.m_lsvConcentration.SelectedItems[0].SubItems[2].Text = objConcentration.m_strDeviceSampleId;
                    this.m_lsvConcentration.SelectedItems[0].SubItems[3].Text = DBAssist.ToString(objConcentration.m_dblAVG);
                    this.m_lsvConcentration.SelectedItems[0].SubItems[4].Text = DBAssist.ToString(objConcentration.m_dblSD);
                    this.m_lsvConcentration.SelectedItems[0].SubItems[5].Text = DBAssist.ToString(objConcentration.m_dblCV);
                }
                else
                {//失败
                    clsCommonDialog.m_mthShowDBError();
                }
            }
            this.m_cmdSave.Enabled = true;
            Cursor.Current = Cursors.Default; 
        }

        private void m_lsvConcentration_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_lsvConcentration.SelectedItems.Count <= 0)
                return;
            //变更状态标志
            this.m_blnNew = false;

            clsLisQCConcentrationVO obj = (clsLisQCConcentrationVO)this.m_lsvConcentration.SelectedItems[0].Tag;

            this.m_cboConcentration.Value = obj.m_intConcentrationSeq;
            this.m_txtDeviceSampleID.Text = obj.m_strDeviceSampleId;
            this.m_txtCV.Text = DBAssist.ToString(obj.m_dblCV);
            this.m_txtSD.Text = DBAssist.ToString(obj.m_dblSD);
            this.m_txtX.Text = DBAssist.ToString(obj.m_dblAVG);
            if (obj.m_enmStatus == enmQCStatus.Natrural)
            {
                this.m_cboConcentration.Enabled = false;
                this.m_txtDeviceSampleID.Enabled = true;
                this.m_txtCV.Enabled = true;
                this.m_txtSD.Enabled = true;
                this.m_txtX.Enabled = true;

                this.m_cmdNew.Enabled = true;
                this.m_cmdDelete.Enabled = true;
                this.m_cmdSave.Enabled = true;
                this.m_cmdReturn.Enabled = true;
                this.m_cmdCancelDelete.Enabled = false;
            }
            else
            {
                this.m_cboConcentration.Enabled = false;
                this.m_txtDeviceSampleID.Enabled = false;
                this.m_txtCV.Enabled = false;
                this.m_txtSD.Enabled = false;
                this.m_txtX.Enabled = false;

                this.m_cmdNew.Enabled = true;
                this.m_cmdDelete.Enabled = false;
                this.m_cmdSave.Enabled = false;
                this.m_cmdReturn.Enabled = true;
                this.m_cmdCancelDelete.Enabled = true;
            }
        }

        private void m_mthLoadDeletedData()
        {
            clsLisQCConcentrationVO[] objs = null;
            long lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngFindDelQCConcentration(this.m_intQCBatchSeq, out objs);
            {
                if (lngRes > 0)
                {
                    if (objs != null)
                        this.m_objDeletedQCContrations.AddRange(objs);
                }
                else
                {
                    clsCommonDialog.m_mthShowDBError();
                    this.WindowState = FormWindowState.Minimized;
                    this.ShowInTaskbar = false;
                    this.m_blnDBErr = true;
                    this.Close();
                }
            } 
        }

        private void m_mthShortCutKey(Keys p_eumKeyCode)
        {
            return;
        }

        private void m_mthShowList()
        {
            this.m_lsvConcentration.BeginUpdate();//开始更新列表
            foreach (clsLisQCConcentrationVO qcConcentration in this.m_objQCContrations)
            {
                ListViewItem item = new ListViewItem(qcConcentration.m_intConcentrationSeq.ToString());
                item.SubItems.Add(qcConcentration.m_strConcentration);
                item.SubItems.Add(qcConcentration.m_strDeviceSampleId);
                item.SubItems.Add(DBAssist.ToString(qcConcentration.m_dblAVG));
                item.SubItems.Add(DBAssist.ToString(qcConcentration.m_dblSD));
                item.SubItems.Add(DBAssist.ToString(qcConcentration.m_dblCV));
                item.SubItems.Add("");
                item.Tag = qcConcentration;
                this.m_lsvConcentration.Items.Add(item);
            }
            foreach (clsLisQCConcentrationVO obj in this.m_objDeletedQCContrations)
            {
                ListViewItem item = new ListViewItem(obj.m_intConcentrationSeq.ToString());
                item.SubItems.Add(obj.m_strConcentration);
                item.SubItems.Add(obj.m_strDeviceSampleId);
                item.SubItems.Add(DBAssist.ToString(obj.m_dblAVG));
                item.SubItems.Add(DBAssist.ToString(obj.m_dblSD));
                item.SubItems.Add(DBAssist.ToString(obj.m_dblCV));
                item.SubItems.Add("╳");
                item.Tag = obj;
                item.ForeColor = Color.Red;
                this.m_lsvConcentration.Items.Add(item);
            }
            this.m_lsvConcentration.EndUpdate();//结束更新列表
            if (this.m_lsvConcentration.Items.Count > 0)
            {
                this.m_lsvConcentration.Focus();
                this.m_lsvConcentration.Items[0].Selected = true;
            }
        }

        private void m_txtCV_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(m_txtX.Text.Trim()) || string.IsNullOrEmpty(m_txtSD.Text.Trim()))
                return;

            try
            {
                double dblX = double.Parse(m_txtX.Text.Trim());
                double dblSD = double.Parse(m_txtSD.Text.Trim());

                double dblCV = dblSD * 100 / dblX;
                m_txtCV.Text = dblCV.ToString("0.0");
            }
            catch
            {

            }
        }

        // Properties
        public List<clsLisQCConcentrationVO> QCContrations
        {
            get
            { 
                return this.m_objQCContrations; 
            }
        }
    }
}
