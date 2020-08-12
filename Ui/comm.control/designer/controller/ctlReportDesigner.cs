using Common.Controls;
using Common.Utils;
using DevExpress.XtraTreeList.Nodes;
using System.Collections.Generic;
using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using weCare.Core.Entity;
using weCare.Core.Utils;
using DevExpress.XtraReports.UI;

namespace Common.Controls
{
    public class ctlReportDesigner : BaseController
    {
        #region Override

        /// <summary>
        /// UI.Viewer
        /// </summary>
        private frmReportDesigner Viewer = null;

        /// <summary>
        /// SetUI
        /// </summary>
        /// <param name="child"></param>
        public override void SetUI(frmBase child)
        {
            base.SetUI(child);
            Viewer = (frmReportDesigner)child;
        }
        #endregion

        #region 变量.属性

        /// <summary>
        /// 报表类型ID
        /// </summary>
        internal int rptTypeId { get; set; }

        /// <summary>
        /// 报表vo
        /// </summary>
        internal EntitySysReport reportVo { get; set; }

        /// <summary>
        /// 电子病历打印模板vo
        /// </summary>
        internal EntityEmrPrintTemplate templateVo { get; set; }

        #endregion

        #region 方法

        #region Init
        /// <summary>
        /// Init
        /// </summary>
        internal void Init()
        {
            if (this.rptTypeId == 1)
            {
                if (reportVo.rptFile == null)
                {
                    XtraReport xr = new XtraReport();
                    xr.DataSource = reportVo.dataSource;
                    Viewer.xrDesignPanel.OpenReport(xr);
                }
                else
                {
                    MemoryStream ms = new MemoryStream(reportVo.rptFile);
                    XtraReport xr = new XtraReport();
                    xr.LoadLayout(ms);
                    xr.DataSource = reportVo.dataSource;
                    Viewer.xrDesignPanel.OpenReport(xr);
                }
                Viewer.Text += " - " + reportVo.rptName;
            }
            else if (this.rptTypeId == 2)
            {
                if (templateVo.templateFile == null)
                {
                    XtraReport xr = new XtraReport();
                    xr.DataSource = templateVo.dataSource;
                    Viewer.xrDesignPanel.OpenReport(xr);
                }
                else
                {
                    MemoryStream ms = new MemoryStream(templateVo.templateFile);
                    XtraReport xr = new XtraReport();
                    xr.LoadLayout(ms);
                    xr.DataSource = templateVo.dataSource;
                    Viewer.xrDesignPanel.OpenReport(xr);
                }
                Viewer.Text += " - " + templateVo.templateName;
            }
        }
        #endregion

        #region New
        /// <summary>
        /// New
        /// </summary>
        internal void New()
        {
            Viewer.xrDesignPanel.OpenReport(new XtraReport());
        }
        #endregion

        #region Import
        /// <summary>
        /// Import
        /// </summary>
        internal void Import()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "(*.repx)|*.repx";
            ofd.ShowDialog();
            string fileName = ofd.FileName;
            if (fileName != "")
            {
                Viewer.xrDesignPanel.OpenReport(fileName);
            }
        }
        #endregion

        #region Export
        /// <summary>
        /// Export
        /// </summary>
        internal void Export()
        {
            if (Viewer.xrDesignPanel.Report != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog(); //创建一个打开对话
                saveFileDialog.Filter = "Report Files(*.repx)|*.repx";
                saveFileDialog.FilterIndex = 2;
                saveFileDialog.RestoreDirectory = true; //保存对话框是否记忆上次打开的目录 
                saveFileDialog.Title = "导出";
                saveFileDialog.FileName = "Report1";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Viewer.xrDesignPanel.Report.SaveLayout(saveFileDialog.FileName.Trim());
                    if (DialogBox.Msg("导出成功，是否现在打开导出的文档？", MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process p = new System.Diagnostics.Process();
                        p.StartInfo.FileName = saveFileDialog.FileName;
                        p.Start();
                    }
                }
            }
        }
        #endregion

        #region Save
        /// <summary>
        /// Save
        /// </summary>
        internal void Save()
        {
            uiHelper.BeginLoading(Viewer);
            try
            {
                MemoryStream ms = new MemoryStream();
                Viewer.xrDesignPanel.Report.SaveLayout(ms);
                ms.Seek(0, SeekOrigin.Begin);
                byte[] bytData = new byte[ms.Length];
                ms.Read(bytData, 0, bytData.Length);
                ms.Close();

                if (this.rptTypeId == 1)
                {
                    this.reportVo.rptFile = ms.ToArray();
                    using (ProxyFormDesign proxy = new ProxyFormDesign())
                    {
                        EntitySysReport updateVo = new EntitySysReport();
                        updateVo.rptId = this.reportVo.rptId;
                        updateVo.rptFile = this.reportVo.rptFile;
                        if (proxy.Service.SaveReportFile(updateVo) > 0)
                        {
                            Viewer.IsSave = true;
                            Viewer.xrDesignPanel.ReportState = DevExpress.XtraReports.UserDesigner.ReportState.Saved;
                            DialogBox.Msg("保存成功！");
                        }
                        else
                        {
                            DialogBox.Msg("保存失败。");
                        }
                    }
                }
                else if (this.rptTypeId == 2)
                {
                    this.templateVo.templateFile = ms.ToArray();
                    using (ProxyFormDesign proxy = new ProxyFormDesign())
                    {
                        object obj = this.templateVo.dataSource;
                        this.templateVo.dataSource = null;
                        if (proxy.Service.UpdateFormPrintTemplate(this.templateVo) > 0)
                        {
                            Viewer.IsSave = true;
                            Viewer.xrDesignPanel.ReportState = DevExpress.XtraReports.UserDesigner.ReportState.Saved;
                            DialogBox.Msg("保存成功！");
                        }
                        else
                        {
                            DialogBox.Msg("保存失败。");
                        }
                        this.templateVo.dataSource = obj;
                    }
                }
            }
            catch (Exception ex)
            {
                DialogBox.Msg(ex.Message);
            }
            finally
            {
                uiHelper.CloseLoading(Viewer);
            }
        }
        #endregion

        #region FormClosing
        /// <summary>
        /// FormClosing
        /// </summary>
        /// <param name="vo"></param>
        public void FormClosing()
        {
            if (Viewer.xrDesignPanel.ReportState == DevExpress.XtraReports.UserDesigner.ReportState.Changed)
            {
                if (DialogBox.Msg("报表已修改,是否保存?", MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Save();
                }
            }
            Viewer.DialogResult = DialogResult.OK;
        }
        #endregion

        #endregion
    }
}
