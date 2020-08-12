using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraPrinting.Control;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;

namespace Common.Controls
{
    public partial class ucPrintControl : ucBase
    {
        public XtraReport XR { get; set; }

        #region PrintingSystem
        /// <summary>
        /// PrintingSystem
        /// </summary>
        public PrintingSystemBase PrintingSystem
        {
            get
            {
                return this.printControl.PrintingSystem;
            }
            set
            {
                this.printControl.PrintingSystem = value;
            }
        }
        #endregion

        #region 是否显示工具栏
        /// <summary>
        /// 是否显示工具栏
        /// </summary>
        [DescriptionAttribute("ShowToolBar"),
         DisplayNameAttribute("ShowToolBar")]
        public bool ShowToolBar
        {
            get
            {
                return this.previewBar1.Visible;
            }
            set
            {
                this.previewBar1.Visible = value;
            }
        }
        #endregion

        #region 是否显示状态栏
        /// <summary>
        /// 是否显示状态栏
        /// </summary>
        [DescriptionAttribute("ShowStatusBar"),
         DisplayNameAttribute("ShowStatusBar")]
        public bool ShowStatusBar
        {
            get
            {
                return this.previewBar2.Visible;
            }
            set
            {
                this.previewBar2.Visible = value;
            }
        }
        #endregion

        #region ExportPdf
        /// <summary>
        /// ExportPdf
        /// </summary>
        public void ExportPdf()
        {
            if (PrintingSystem == null)
            {
                DialogBox.Msg("报表内容为空。");
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF file(*.pdf)|*.pdf";
                //saveFileDialog.Filter = "BMP file(*.bmp)|*.bmp";
                saveFileDialog.RestoreDirectory = true;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        //// Get its Image export options. 
                        //ImageExportOptions imageOptions = this.XR.ExportOptions.Image;
                        //// Set Image-specific export options. 
                        //imageOptions.Format = System.Drawing.Imaging.ImageFormat.Bmp;
                        //this.XR.ExportToImage(saveFileDialog.FileName);
                        PrintingSystem.ExportToPdf(saveFileDialog.FileName);
                    }
                    catch (Exception ex)
                    {
                        DialogBox.Msg(ex.Message);
                    }
                }
            }
        }
        #endregion

        #region ExportXls()
        /// <summary>
        /// ExportXls()
        /// </summary>
        public void ExportXls()
        {
            if (PrintingSystem == null)
            {
                DialogBox.Msg("报表内容为空。");
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Xls(*.xls)|*.xls";
                saveFileDialog.RestoreDirectory = true;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        PrintingSystem.ExportToXls(saveFileDialog.FileName);
                    }
                    catch (Exception ex)
                    {
                        DialogBox.Msg(ex.Message);
                    }
                }
            }
        }
        #endregion

        #region PrintDoc
        /// <summary>
        /// PrintDoc
        /// </summary>
        public void PrintDoc()
        {
            if (XR == null)
            {
                DialogBox.Msg("报表内容为空。");
            }
            else
            {
                XR.PrintDialog();
            }
        }
        #endregion

        #region ctor
        /// <summary>
        /// ctor
        /// </summary>
        public ucPrintControl()
        {
            InitializeComponent();
        }
        #endregion

    }
}
