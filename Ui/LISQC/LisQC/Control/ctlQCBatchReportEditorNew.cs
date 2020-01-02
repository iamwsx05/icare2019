using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
using PinkieControls;
using Sybase.DataWindow;
using com.digitalwave.iCare.gui.HIS;

namespace com.digitalwave.iCare.gui.LIS
{
    public class ctlQCBatchReportEditorNew : UserControl
    {
        // Fields
        private bool blISThisForm;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private IContainer components;
        private ButtonXP m_cmdDelete;
        private ButtonXP m_cmdModify;
        private ButtonXP m_cmdNew;
        private ButtonXP m_cmdPreview;
        private ButtonXP m_cmdPrint;
        private ButtonXP m_cmdPrintList;
        private ListView m_lsvReport;
        //private clsDcl_QCDataBusiness m_objDomain;
        private clsQCBatchNew m_objQCBatch;
        private Panel panel1;

        // Methods
        public ctlQCBatchReportEditorNew()
        {
            this.blISThisForm = false;
            //this.m_objDomain = new clsDcl_QCDataBusiness();
            this.components = null;
            this.InitializeComponent();
        }

        private int CompareQCBatchReportVO(clsLisQCReportVO x, clsLisQCReportVO y)
        {
            int num;
            int num2;
            bool flag;
            if (x != null)
            {
                goto Label_0021;
            }
            if (y != null)
            {
                goto Label_001C;
            }
            num2 = 0;
            goto Label_0099;
        Label_001C:
            num2 = -1;
            goto Label_0099;
        Label_0021:
            if (y != null)
            {
                goto Label_0031;
            }
            num2 = 1;
            goto Label_0099;
        Label_0031:
            num = x.m_intQCBatchSeq - y.m_intQCBatchSeq;
            if (num == 0)
            {
                goto Label_004D;
            }
            num2 = num;
            goto Label_0099;
        Label_004D:
            if (x.m_dtReport <= y.m_dtReport)
            {
                goto Label_006B;
            }
            num2 = 1;
            goto Label_0099;
        Label_006B:
            if (x.m_dtReport >= y.m_dtReport)
            {
                goto Label_0088;
            }
            num2 = -1;
            goto Label_0099;
        Label_0088:
            num2 = x.m_intSeq - y.m_intSeq;
        Label_0099:
            return num2;
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
            ColumnHeader[] headerArray;
            this.panel1 = new Panel();
            this.m_cmdPrint = new ButtonXP();
            this.m_cmdPreview = new ButtonXP();
            this.m_cmdModify = new ButtonXP();
            this.m_cmdDelete = new ButtonXP();
            this.m_cmdNew = new ButtonXP();
            this.m_lsvReport = new ListView();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader2 = new ColumnHeader();
            this.columnHeader3 = new ColumnHeader();
            this.columnHeader4 = new ColumnHeader();
            this.m_cmdPrintList = new ButtonXP();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.m_cmdPrintList);
            this.panel1.Controls.Add(this.m_cmdPrint);
            this.panel1.Controls.Add(this.m_cmdPreview);
            this.panel1.Controls.Add(this.m_cmdModify);
            this.panel1.Controls.Add(this.m_cmdDelete);
            this.panel1.Controls.Add(this.m_cmdNew);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new Point(0x1d7, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0xa7, 0x1c4);
            this.panel1.TabIndex = 0;
            this.m_cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdPrint.BackColor = Color.FromArgb(0, 0xec, 0xe9, 0xd8);
            this.m_cmdPrint.DefaultScheme = true;
            this.m_cmdPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdPrint.Hint = "";
            this.m_cmdPrint.Location = new Point(0x24, 0xcc);
            this.m_cmdPrint.Name = "m_cmdPrint";
            this.m_cmdPrint.Scheme = 0;
            this.m_cmdPrint.Size = new Size(0x60, 0x21);
            this.m_cmdPrint.TabIndex = 4;
            this.m_cmdPrint.Text = "打印";
            this.m_cmdPrint.Click += new EventHandler(this.m_cmdPrint_Click);
            this.m_cmdPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdPreview.BackColor = Color.FromArgb(0, 0xec, 0xe9, 0xd8);
            this.m_cmdPreview.DefaultScheme = true;
            this.m_cmdPreview.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdPreview.Hint = "";
            this.m_cmdPreview.Location = new Point(0x24, 160);
            this.m_cmdPreview.Name = "m_cmdPreview";
            this.m_cmdPreview.Scheme = 0;
            this.m_cmdPreview.Size = new Size(0x60, 0x21);
            this.m_cmdPreview.TabIndex = 3;
            this.m_cmdPreview.Text = "预览";
            this.m_cmdPreview.Click += new EventHandler(this.m_cmdPreview_Click);
            this.m_cmdModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdModify.BackColor = Color.FromArgb(0, 0xec, 0xe9, 0xd8);
            this.m_cmdModify.DefaultScheme = true;
            this.m_cmdModify.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdModify.Hint = "";
            this.m_cmdModify.Location = new Point(0x24, 0x74);
            this.m_cmdModify.Name = "m_cmdModify";
            this.m_cmdModify.Scheme = 0;
            this.m_cmdModify.Size = new Size(0x60, 0x21);
            this.m_cmdModify.TabIndex = 2;
            this.m_cmdModify.Text = "修改";
            this.m_cmdModify.Click += new EventHandler(this.m_cmdModify_Click);
            this.m_cmdDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdDelete.BackColor = Color.FromArgb(0, 0xec, 0xe9, 0xd8);
            this.m_cmdDelete.DefaultScheme = true;
            this.m_cmdDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdDelete.Hint = "";
            this.m_cmdDelete.Location = new Point(0x24, 0x44);
            this.m_cmdDelete.Name = "m_cmdDelete";
            this.m_cmdDelete.Scheme = 0;
            this.m_cmdDelete.Size = new Size(0x60, 0x21);
            this.m_cmdDelete.TabIndex = 1;
            this.m_cmdDelete.Text = "删除";
            this.m_cmdDelete.Click += new EventHandler(this.m_cmdDelete_Click);
            this.m_cmdNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdNew.BackColor = Color.FromArgb(0, 0xec, 0xe9, 0xd8);
            this.m_cmdNew.DefaultScheme = true;
            this.m_cmdNew.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdNew.Hint = "";
            this.m_cmdNew.Location = new Point(0x24, 20);
            this.m_cmdNew.Name = "m_cmdNew";
            this.m_cmdNew.Scheme = 0;
            this.m_cmdNew.Size = new Size(0x60, 0x21);
            this.m_cmdNew.TabIndex = 0;
            this.m_cmdNew.Text = "新增";
            this.m_cmdNew.Click += new EventHandler(this.m_cmdNew_Click);
            this.m_lsvReport.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.m_lsvReport.Columns.AddRange(new ColumnHeader[] { this.columnHeader1, this.columnHeader2, this.columnHeader3, this.columnHeader4 });
            this.m_lsvReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lsvReport.FullRowSelect = true;
            this.m_lsvReport.GridLines = true;
            this.m_lsvReport.HideSelection = false;
            this.m_lsvReport.Location = new Point(0, 0);
            this.m_lsvReport.MultiSelect = false;
            this.m_lsvReport.Name = "m_lsvReport";
            this.m_lsvReport.Size = new Size(0x1d7, 0x1c4);
            this.m_lsvReport.TabIndex = 3;
            this.m_lsvReport.UseCompatibleStateImageBehavior = false;
            this.m_lsvReport.View = System.Windows.Forms.View.Details;
            this.m_lsvReport.ItemActivate += new EventHandler(this.m_lsvReport_ItemActivate);
            this.columnHeader1.Text = "报告日期";
            this.columnHeader1.Width = 0x62;
            this.columnHeader2.Text = "质控状态";
            this.columnHeader2.Width = 0x4a;
            this.columnHeader3.Text = "违反规则";
            this.columnHeader3.Width = 0x84;
            this.columnHeader4.Text = "报告人员";
            this.columnHeader4.Width = 0x76;
            this.m_cmdPrintList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdPrintList.BackColor = Color.FromArgb(0, 0xec, 0xe9, 0xd8);
            this.m_cmdPrintList.DefaultScheme = true;
            this.m_cmdPrintList.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdPrintList.Hint = "";
            this.m_cmdPrintList.Location = new Point(0x24, 0xf9);
            this.m_cmdPrintList.Name = "m_cmdPrintList";
            this.m_cmdPrintList.Scheme = 0;
            this.m_cmdPrintList.Size = new Size(0x60, 0x21);
            this.m_cmdPrintList.TabIndex = 5;
            this.m_cmdPrintList.Text = "打印列表";
            this.m_cmdPrintList.Click += new EventHandler(this.m_cmdPrintList_Click);
            base.AutoScaleDimensions = new SizeF(7f, 14f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.m_lsvReport);
            base.Controls.Add(this.panel1);
            this.Font = new Font("宋体", 10.5f);
            this.Name = "ctlQCBatchReportEditorNew";
            this.Size = new Size(0x27e, 0x1c4);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void m_cmdDelete_Click(object sender, EventArgs e)
        {
            if (this.m_lsvReport.SelectedItems.Count <= 0)
                return;

            Cursor.Current = Cursors.WaitCursor;
            this.m_cmdDelete.Enabled = false;

            ListViewItem item = this.m_lsvReport.SelectedItems[0];
            clsLisQCReportVO obj = item.Tag as clsLisQCReportVO;
            obj.m_enmStatus = enmQCStatus.Delete;

            long lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngUpdate(obj);

            if (lngRes > 0)
            {
                this.m_objQCBatch.GetReports().Remove(obj);

                int intCurrItemIdx = item.Index;
                item.Remove();

                int next = -1;
                if (this.m_lsvReport.Items.Count > intCurrItemIdx)
                    next = intCurrItemIdx;
                else if (this.m_lsvReport.Items.Count == intCurrItemIdx && intCurrItemIdx > 0)
                    next = intCurrItemIdx - 1;

                if (next != -1)
                {
                    this.m_lsvReport.Focus();
                    this.m_lsvReport.Items[next].Selected = true;
                }
                else
                {

                }
            }
            else
            {
                obj.m_enmStatus = enmQCStatus.Natrural;
                clsCommonDialog.m_mthShowDBError();
            }

            Cursor.Current = Cursors.Default;
            this.m_cmdDelete.Enabled = true;
        }

        private void m_cmdModify_Click(object sender, EventArgs e)
        {
            if (this.m_lsvReport.SelectedItems.Count > 0)
            {
                ListViewItem item = this.m_lsvReport.SelectedItems[0];
                clsLisQCReportVO temp = new clsLisQCReportVO();
                clsLisQCReportVO curr = item.Tag as clsLisQCReportVO;
                curr.m_mthCopyTo(temp);
                frmQCReport frm = new frmQCReport(temp);
                frm.BrokenRules = this.m_objQCBatch.BrokenRules;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    clsLisQCReportVO var = frm.Report;
                    var.m_mthCopyTo(curr);
                    this.m_mthListViewItemChange(curr, item);
                }
            }
        }

        private void m_cmdNew_Click(object sender, EventArgs e)
        {
            if (m_objQCBatch == null || m_objQCBatch.Count != 1)
                return;

            m_cmdNew.Enabled = false;

            frmQCReport frm = new frmQCReport(m_objQCBatch.SeqArr[0]);
            frm.BrokenRules = m_objQCBatch.BrokenRules;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                clsLisQCReportVO var = frm.Report;
                this.m_objQCBatch.GetReports().Add(var);
                ListViewItem item = new ListViewItem();
                this.m_mthListViewItemInit(var, item);
                this.m_lsvReport.Items.Add(item);
                this.m_lsvReport.Focus();
                item.Selected = true;
            }

            m_cmdNew.Enabled = true;
        }

        private void m_cmdPreview_Click(object sender, EventArgs e)
        {
            if (this.m_lsvReport.SelectedItems.Count <= 0)
                return;
            ListViewItem item = this.m_lsvReport.SelectedItems[0];
            clsLisQCReportVO obj = item.Tag as clsLisQCReportVO;

            clsQCDailyReportToolStrategy print = new clsQCDailyReportToolStrategy(obj, this.m_objQCBatch.GetQCBatchSet()[0]);
            print.m_mthPrintPreview();
        }

        private void m_cmdPrint_Click(object sender, EventArgs e)
        {
            if (this.m_lsvReport.SelectedItems.Count <= 0)
                return;
            ListViewItem item = this.m_lsvReport.SelectedItems[0];
            clsLisQCReportVO obj = item.Tag as clsLisQCReportVO;

            clsQCDailyReportToolStrategy print = new clsQCDailyReportToolStrategy(obj, this.m_objQCBatch.GetQCBatchSet()[0]);
            print.m_mthPrint();
        }

        private void m_cmdPrintList_Click(object sender, EventArgs e)
        {
            if (this.m_objQCBatch == null)
                return;

            this.m_mthPrintQCDayReportList();
        }

        private void m_lsvReport_ItemActivate(object sender, EventArgs e)
        {
            this.m_cmdModify_Click(this.m_lsvReport, null);
        }

        private void m_mthInitList()
        {
            List<clsLisQCReportVO> list;
            clsLisQCReportVO tvo;
            ListViewItem item;
            bool flag;
            List<clsLisQCReportVO>.Enumerator enumerator;

            if (this.m_objQCBatch == null || this.m_objQCBatch.Count != 1)
            {
                goto Label_00D7;
            }
            else
            {
                goto Label_0023;
            }

        Label_0023:
            this.m_lsvReport.BeginUpdate();
            this.m_lsvReport.Items.Clear();
        Label_0040:
            try
            {
                try
                {
                    list = this.m_objQCBatch.GetReports();
                    if (list == null)
                    {
                        goto Label_00BC;
                    }
                    enumerator = list.GetEnumerator();
                Label_005F:
                    try
                    {
                        goto Label_009E;
                    Label_0061:
                        tvo = enumerator.Current;
                        if (tvo.m_intReportStats != 0)
                        {
                            goto Label_009D;
                        }
                        item = new ListViewItem();
                        this.m_mthListViewItemInit(tvo, item);
                        this.m_lsvReport.Items.Add(item);
                    Label_009D:;
                    Label_009E:
                        if (enumerator.MoveNext())
                        {
                            goto Label_0061;
                        }
                        goto Label_00BA;
                    }
                    finally
                    {
                    Label_00AB:
                        enumerator.Dispose();
                    }
                Label_00BA:;
                Label_00BC:
                    goto Label_00C4;
                }
                catch
                {
                Label_00BF:
                    goto Label_00C4;
                }
            Label_00C4:
                goto Label_00D6;
            }
            finally
            {
            Label_00C7:
                this.m_lsvReport.EndUpdate();
            }
        Label_00D6:;
        Label_00D7:
            return;
        }

        private void m_mthListViewItemChange(clsLisQCReportVO p_objReportVO, ListViewItem item)
        {
            bool flag;
            item.Text = DBAssist.ToString(p_objReportVO.m_dtReport, "yyyy-MM-dd");
            if (p_objReportVO.m_enmQCControlStatus == enmQCControlStatus.UnControl)
            {
                goto Label_0043;
            }
            item.SubItems[1].Text = "";
            goto Label_005C;
        Label_0043:
            item.SubItems[1].Text = "▲";
        Label_005C:
            item.SubItems[2].Text = p_objReportVO.m_strUnmatchedRule;
            item.SubItems[3].Text = p_objReportVO.m_strReportorName;
            item.Tag = p_objReportVO;
            return;
        }

        private void m_mthListViewItemInit(clsLisQCReportVO p_objReportVO, ListViewItem item)
        {
            bool flag;
            item.Text = DBAssist.ToString(p_objReportVO.m_dtReport, "yyyy-MM-dd");
            if (p_objReportVO.m_enmQCControlStatus == enmQCControlStatus.UnControl)
            {
                goto Label_003D;
            }
            item.SubItems.Add("");
            goto Label_0050;
        Label_003D:
            item.SubItems.Add("▲");
        Label_0050:
            item.SubItems.Add(p_objReportVO.m_strUnmatchedRule);
            item.SubItems.Add(p_objReportVO.m_strReportorName);
            item.Tag = p_objReportVO;
            return;
        }

        public void m_mthPrintQCDayReportList()
        {
            DataWindowControl control;
            clsLisQCBatchVO hvo;
            List<clsLisQCDataVO> list;
            List<clsLisQCReportVO> list2;
            clsLisQCReportVO tvo;
            clsLisQCReportVO tvo2;
            int num;
            string str;
            DateTime time;
            DateTime time2;
            bool flag;
            string[] strArray;
            DateTime time3;

            if (this.m_objQCBatch == null || this.m_objQCBatch.Count != 1)
            {
                goto Label_0422;
            }
            else
            {
                goto Label_0026;
            }
        Label_0026:
            control = new DataWindowControl();
            base.Controls.Add(control);
            control.LibraryList = Application.StartupPath + @"\pb_lis.pbl";
            control.DataWindowObject = "d_lis_qcdayreportlist";
            hvo = this.m_objQCBatch.m_objBatchSets[0];
            list = this.m_objQCBatch.m_objDatas;
            list2 = this.m_objQCBatch.m_objReports;
            list2.Sort(new Comparison<clsLisQCReportVO>(this.CompareQCBatchReportVO));
            control.Modify("t_workgroup.text = '" + hvo.m_strWorkGroupName + "'");
            control.Modify("t_device.text = '" + hvo.m_strDeviceName + "'");
            control.Modify("t_item.text = '" + hvo.m_strCheckItemName + "'");
            control.Modify("t_qcbatch.text = '" + hvo.m_strSampleLotNo + " - " + hvo.m_strSampleSource + "'");
            control.Modify("t_qcCon.text = '" + hvo.m_strReagentBatch + " - " + hvo.m_strReagent + "'");
            tvo = null;
            tvo2 = null;
            tvo2 = new clsLisQCReportVO();
            tvo2.m_intQCBatchSeq = hvo.m_intSeq;
            tvo2.m_enmQCControlStatus = enmQCControlStatus.Control;
            tvo2.m_enmStatus = enmQCStatus.Natrural;
            tvo2.m_intReportStats = 0;
            tvo2.m_strProcess = "";
            tvo2.m_strReason = "";
            tvo2.m_strUnmatchedRule = "";
            tvo2.m_strReportorId = ((com.digitalwave.GUI_Base.frmMDI_Child_Base)base.FindForm()).LoginInfo.m_strEmpID;
            tvo2.m_strReportorName = ((com.digitalwave.GUI_Base.frmMDI_Child_Base)base.FindForm()).LoginInfo.m_strEmpName;
            num = 0;
            str = "";
            time = this.m_objQCBatch.DateBegin.Date;
            time2 = this.m_objQCBatch.DateEnd.Date;
            if (time2 <= DateTime.Now.Date)
            {
                goto Label_03F8;
            }
            time2 = DateTime.Now.Date;
            goto Label_03F8;
        Label_026F:
            tvo = this.m_objGetQCDayReport(list2, time);
            if (tvo != null)
            {
                goto Label_0298;
            }
            tvo2.m_dtReport = time;
            tvo = tvo2;
        Label_0298:
            num = control.InsertRow(0);
            control.SetItemString(num, "strdate", tvo.m_dtReport.ToString("MM-dd"));
            control.SetItemString(num, "strcheckresult", this.m_strGetQCDataByDate(list, tvo.m_dtReport, tvo.m_intQCBatchSeq));
            if (tvo.m_enmQCControlStatus == enmQCControlStatus.UnControl)
            {
                goto Label_0301;
            }
            str = "在控";
            goto Label_030A;
        Label_0301:
            str = "失控";
        Label_030A:
            control.SetItemString(num, "strqcstatus", str);
            control.SetItemString(num, "strbrokerules", tvo.m_strUnmatchedRule);
            control.SetItemString(num, "strcanreport", (str == "在控" ? "可发" : "停发"));
            str = "";
            if (!string.IsNullOrEmpty(tvo.m_strReason))
            {
                goto Label_0380;
            }
            str = tvo.m_strProcess;
            goto Label_03C0;
        Label_0380:
            if (!string.IsNullOrEmpty(tvo.m_strReason))
            {
                goto Label_03A3;
            }
            str = tvo.m_strReason;
            goto Label_03BF;
        Label_03A3:
            str = tvo.m_strReason + "; " + tvo.m_strProcess;
        Label_03BF:;
        Label_03C0:
            control.SetItemString(num, "strressonandprocess", str);
            control.SetItemString(num, "strreporter", tvo.m_strReportorName);
            time = time.AddDays(1.0);
        Label_03F8:
            if (time <= time2)
            {
                goto Label_026F;
            }
            control.PrintProperties.PrinterName = "质控日报告";
            clsPublic.PrintDialog(control);
        Label_0422:
            return;
        }

        private clsLisQCReportVO m_objGetQCDayReport(List<clsLisQCReportVO> p_objQCReports, DateTime p_dtReportDate)
        {
            clsLisQCReportVO tvo;
            clsLisQCReportVO tvo2;
            int num;
            clsLisQCReportVO tvo3;
            bool flag;
            tvo = null;
            tvo2 = null;
            num = 0;
            goto Label_0074;
        Label_0009:
            tvo2 = p_objQCReports[num];
            if (tvo2.m_intReportStats != 1)
            {
                goto Label_0027;
            }
            goto Label_0070;
        Label_0027:
            if (tvo2.m_dtReport.Date != p_dtReportDate.Date)
            {
                goto Label_004C;
            }
            tvo = tvo2;
            goto Label_0083;
        Label_004C:
            if (tvo2.m_dtReport.Date <= p_dtReportDate.Date)
            {
                goto Label_006F;
            }
            goto Label_0083;
        Label_006F:;
        Label_0070:
            num += 1;
        Label_0074:
            if (num < p_objQCReports.Count)
            {
                goto Label_0009;
            }
        Label_0083:
            tvo3 = tvo;
        Label_0087:
            return tvo3;
        }

        private void m_objQCBatch_DataLoaded(object sender, EventArgs e)
        {
            if (this.Created)
            {
                this.m_mthInitList();
            }
        }

        private void m_objQCBatch_Reloaded(object sender, EventArgs e)
        {
            if (this.Created)
            {
                this.m_mthInitList();
            }
        }

        private string m_strGetQCDataByDate(List<clsLisQCDataVO> p_lstQCData, DateTime p_dtDate, int p_intQCBatch)
        {
            clsLisQCDataVO avo;
            int num;
            string str;
            bool flag;

            if (p_lstQCData == null || p_lstQCData.Count <= 0)
            {
                str = string.Empty;
                goto Label_0084;
            }
            else
            {
                goto Label_001C;
            }
        Label_001C:
            avo = null;
            num = 0;
            goto Label_006F;
        Label_0022:
            avo = p_lstQCData[num];
            if (avo.m_intQCBatchSeq != p_intQCBatch)
            {
                goto Label_006A;
            }
            if (avo.m_datQCDate.Date != p_dtDate.Date)
            {
                goto Label_0069;
            }
            str = avo.m_dlbResult.ToString();
            goto Label_0084;
        Label_0069:;
        Label_006A:
            num += 1;
        Label_006F:
            if (num < p_lstQCData.Count)
            {
                goto Label_0022;
            }
            str = string.Empty;
        Label_0084:
            return str;
        }

        protected override void OnCreateControl()
        {
            this.m_mthInitList();
        }

        // Properties
        internal clsQCBatchNew QCBatch
        {
            set
            {
                if (value != null)
                {
                    m_objQCBatch = value;
                    if (this.Created)
                    {
                        m_mthInitList();
                    }
                    m_objQCBatch.Loaded += new EventHandler(m_objQCBatch_DataLoaded);
                    m_objQCBatch.Reloaded += new EventHandler(m_objQCBatch_Reloaded);
                }
                else
                {
                    throw new System.ArgumentNullException();
                }
            }
        }
    }

    class clsCommonDialog
    {
        public static void m_mthShowDBError()
        {
            MessageBox.Show("数据库访问出错！", "iCare");
        }
        public static void m_mthShowNoAccordantResult()
        {
            MessageBox.Show("没有符合条件的记录！", "iCare");
        }
    }
}
