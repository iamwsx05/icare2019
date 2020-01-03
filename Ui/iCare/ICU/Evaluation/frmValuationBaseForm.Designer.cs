using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace iCare.ICU.Evaluation
{
    partial class frmValuationBaseForm
    {
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmValuationBaseForm));
            this.trvActivityTime = new System.Windows.Forms.TreeView();
            this.m_pdcPrintDocument = new System.Drawing.Printing.PrintDocument();
            this.clmBedNO_BaseForm = new System.Windows.Forms.ColumnHeader();
            this.m_cmdSave = new PinkieControls.ButtonXP();
            this.m_cmdDelete = new PinkieControls.ButtonXP();
            this.m_cmdNew = new PinkieControls.ButtonXP();
            this.m_cmdClear = new PinkieControls.ButtonXP();
            this.m_cmdPrintPreview = new PinkieControls.ButtonXP();
            this.m_cmdPrint = new PinkieControls.ButtonXP();
            this.m_cmdParamSetting = new PinkieControls.ButtonXP();
            this.m_pnlNewBase.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_pnlNewBase
            // 
            this.m_pnlNewBase.Controls.Add(this.m_cmdParamSetting);
            this.m_pnlNewBase.Controls.Add(this.trvActivityTime);
            this.m_pnlNewBase.Controls.Add(this.m_cmdPrint);
            this.m_pnlNewBase.Controls.Add(this.m_cmdPrintPreview);
            this.m_pnlNewBase.Controls.Add(this.m_cmdClear);
            this.m_pnlNewBase.Controls.Add(this.m_cmdNew);
            this.m_pnlNewBase.Controls.Add(this.m_cmdDelete);
            this.m_pnlNewBase.Controls.Add(this.m_cmdSave);
            this.m_pnlNewBase.Location = new System.Drawing.Point(2, 3);
            this.m_pnlNewBase.Size = new System.Drawing.Size(857, 89);
            this.m_pnlNewBase.Controls.SetChildIndex(this.m_ctlPatientInfo, 0);
            this.m_pnlNewBase.Controls.SetChildIndex(this.m_cmdSave, 0);
            this.m_pnlNewBase.Controls.SetChildIndex(this.m_cmdDelete, 0);
            this.m_pnlNewBase.Controls.SetChildIndex(this.m_cmdNew, 0);
            this.m_pnlNewBase.Controls.SetChildIndex(this.m_cmdClear, 0);
            this.m_pnlNewBase.Controls.SetChildIndex(this.m_cmdPrintPreview, 0);
            this.m_pnlNewBase.Controls.SetChildIndex(this.m_cmdPrint, 0);
            this.m_pnlNewBase.Controls.SetChildIndex(this.trvActivityTime, 0);
            this.m_pnlNewBase.Controls.SetChildIndex(this.m_cmdParamSetting, 0);
            // 
            // m_ctlPatientInfo
            // 
            this.m_ctlPatientInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ctlPatientInfo.Dock = System.Windows.Forms.DockStyle.None;
            this.m_ctlPatientInfo.Location = new System.Drawing.Point(191, 34);
            this.m_ctlPatientInfo.m_BlnIsShowHomePlace = true;
            this.m_ctlPatientInfo.m_BlnIsShowMarriage = true;
            this.m_ctlPatientInfo.m_BlnIsShowPatientAge = true;
            this.m_ctlPatientInfo.m_BlnIsShowPatientName = true;
            this.m_ctlPatientInfo.m_BlnIsShowSex = true;
            this.m_ctlPatientInfo.Size = new System.Drawing.Size(665, 57);
            // 
            // trvActivityTime
            // 
            this.trvActivityTime.BackColor = System.Drawing.Color.White;
            this.trvActivityTime.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.trvActivityTime.ForeColor = System.Drawing.Color.Black;
            this.trvActivityTime.HideSelection = false;
            this.trvActivityTime.Location = new System.Drawing.Point(0, 34);
            this.trvActivityTime.Name = "trvActivityTime";
            this.trvActivityTime.ShowRootLines = false;
            this.trvActivityTime.Size = new System.Drawing.Size(196, 57);
            this.trvActivityTime.TabIndex = 449;
            this.trvActivityTime.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvActivityTime_AfterSelect);
            // 
            // m_pdcPrintDocument
            // 
            this.m_pdcPrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.m_pdcPrintDocument_PrintPage);
            this.m_pdcPrintDocument.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.m_pdcPrintDocument_EndPrint);
            this.m_pdcPrintDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.m_pdcPrintDocument_BeginPrint);
            // 
            // clmBedNO_BaseForm
            // 
            this.clmBedNO_BaseForm.Width = 80;
            // 
            // m_cmdSave
            // 
            this.m_cmdSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdSave.DefaultScheme = true;
            this.m_cmdSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdSave.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdSave.ForeColor = System.Drawing.Color.Black;
            this.m_cmdSave.Hint = "";
            this.m_cmdSave.Location = new System.Drawing.Point(201, 60);
            this.m_cmdSave.Name = "m_cmdSave";
            this.m_cmdSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdSave.Size = new System.Drawing.Size(76, 25);
            this.m_cmdSave.TabIndex = 10000009;
            this.m_cmdSave.Text = "保存";
            this.m_cmdSave.Click += new System.EventHandler(this.m_cmdSave_Click);
            // 
            // m_cmdDelete
            // 
            this.m_cmdDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdDelete.DefaultScheme = true;
            this.m_cmdDelete.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdDelete.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdDelete.ForeColor = System.Drawing.Color.Black;
            this.m_cmdDelete.Hint = "";
            this.m_cmdDelete.Location = new System.Drawing.Point(283, 60);
            this.m_cmdDelete.Name = "m_cmdDelete";
            this.m_cmdDelete.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdDelete.Size = new System.Drawing.Size(76, 25);
            this.m_cmdDelete.TabIndex = 10000010;
            this.m_cmdDelete.Text = "删除";
            this.m_cmdDelete.Click += new System.EventHandler(this.m_cmdDelete_Click);
            // 
            // m_cmdNew
            // 
            this.m_cmdNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdNew.DefaultScheme = true;
            this.m_cmdNew.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdNew.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdNew.ForeColor = System.Drawing.Color.Black;
            this.m_cmdNew.Hint = "";
            this.m_cmdNew.Location = new System.Drawing.Point(365, 60);
            this.m_cmdNew.Name = "m_cmdNew";
            this.m_cmdNew.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdNew.Size = new System.Drawing.Size(76, 25);
            this.m_cmdNew.TabIndex = 10000011;
            this.m_cmdNew.Text = "新建";
            this.m_cmdNew.Click += new System.EventHandler(this.m_cmdNew_Click);
            // 
            // m_cmdClear
            // 
            this.m_cmdClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdClear.DefaultScheme = true;
            this.m_cmdClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdClear.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdClear.ForeColor = System.Drawing.Color.Black;
            this.m_cmdClear.Hint = "";
            this.m_cmdClear.Location = new System.Drawing.Point(447, 60);
            this.m_cmdClear.Name = "m_cmdClear";
            this.m_cmdClear.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdClear.Size = new System.Drawing.Size(76, 25);
            this.m_cmdClear.TabIndex = 10000012;
            this.m_cmdClear.Text = "清空";
            this.m_cmdClear.Click += new System.EventHandler(this.m_cmdClear_Click);
            // 
            // m_cmdPrintPreview
            // 
            this.m_cmdPrintPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdPrintPreview.DefaultScheme = true;
            this.m_cmdPrintPreview.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdPrintPreview.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdPrintPreview.ForeColor = System.Drawing.Color.Black;
            this.m_cmdPrintPreview.Hint = "";
            this.m_cmdPrintPreview.Location = new System.Drawing.Point(529, 60);
            this.m_cmdPrintPreview.Name = "m_cmdPrintPreview";
            this.m_cmdPrintPreview.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdPrintPreview.Size = new System.Drawing.Size(76, 25);
            this.m_cmdPrintPreview.TabIndex = 10000013;
            this.m_cmdPrintPreview.Text = "打印预览";
            this.m_cmdPrintPreview.Click += new System.EventHandler(this.m_cmdPrintPreview_Click);
            // 
            // m_cmdPrint
            // 
            this.m_cmdPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdPrint.DefaultScheme = true;
            this.m_cmdPrint.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdPrint.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdPrint.ForeColor = System.Drawing.Color.Black;
            this.m_cmdPrint.Hint = "";
            this.m_cmdPrint.Location = new System.Drawing.Point(611, 60);
            this.m_cmdPrint.Name = "m_cmdPrint";
            this.m_cmdPrint.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdPrint.Size = new System.Drawing.Size(76, 25);
            this.m_cmdPrint.TabIndex = 10000014;
            this.m_cmdPrint.Text = "打印";
            this.m_cmdPrint.Click += new System.EventHandler(this.m_cmdPrint_Click);
            // 
            // m_cmdParamSetting
            // 
            this.m_cmdParamSetting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdParamSetting.DefaultScheme = true;
            this.m_cmdParamSetting.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdParamSetting.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdParamSetting.ForeColor = System.Drawing.Color.Black;
            this.m_cmdParamSetting.Hint = "";
            this.m_cmdParamSetting.Location = new System.Drawing.Point(693, 60);
            this.m_cmdParamSetting.Name = "m_cmdParamSetting";
            this.m_cmdParamSetting.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdParamSetting.Size = new System.Drawing.Size(105, 25);
            this.m_cmdParamSetting.TabIndex = 10000015;
            this.m_cmdParamSetting.Text = "配置检验项目";
            this.m_cmdParamSetting.Click += new System.EventHandler(this.m_cmdSetParman_Click);
            // 
            // frmValuationBaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(861, 673);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmValuationBaseForm";
            this.Text = "智能评分父窗体";
            this.Load += new System.EventHandler(this.frmValuationBaseForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmValuationBaseForm_Closing);
            this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.frmValuationBaseForm_HelpRequested);
            this.m_pnlNewBase.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private PinkieControls.ButtonXP m_cmdParamSetting;
    }
}
