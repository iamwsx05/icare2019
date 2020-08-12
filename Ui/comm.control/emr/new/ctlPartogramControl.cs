using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using DevExpress.XtraReports.UI;
using Common.Entity;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Common.Controls.Emr
{
    /// <summary>
    /// 产程进程图控件
    /// </summary>
    public partial class ctlPartogramControl : UserControl
    {
        /// <summary>
        /// 画图工具
        /// </summary>
        PartogramDrawer objDrawer = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ctlPartogramControl()
        {
            InitializeComponent();
            objDrawer = new PartogramDrawer();
        }

        /// <summary>
        /// 控件加载时就先画出初始表格
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.orgSizePerUnit = this.objDrawer.fltSizePerUnit;
            this.DrawImage();
            this.ShowImage();
        }

        float orgSizePerUnit { get; set; }

        /// <summary>
        /// 放大
        /// </summary>
        public void ZoomIn()
        {
            if (this.objDrawer.fltSizePerUnit < 30)
            {
                this.objDrawer.fltSizePerUnit += 0.5f;
                this.DrawImage();
                this.ShowImage();
            }
        }
        /// <summary>
        /// 缩小
        /// </summary>
        public void ZoomOut()
        {
            if (this.objDrawer.fltSizePerUnit > 8)
            {
                this.objDrawer.fltSizePerUnit -= 0.5f;
                this.DrawImage();
                this.ShowImage();
            }
        }

        /// <summary>
        /// 原始大小
        /// </summary>
        public void Zoom()
        {
            this.objDrawer.fltSizePerUnit = this.orgSizePerUnit;
            this.DrawImage();
            this.ShowImage();
        }

        /// <summary>
        /// 显示画出来的图片
        /// </summary>
        public void ShowImage()
        {
            Image imgResult = this.objDrawer.m_imgGetImage();
            if (imgResult != null)
            {
                this.picContainer.Width = imgResult.Width;
                this.picContainer.Height = imgResult.Height;
                this.picContainer.Image = imgResult;
            }
            else
            {
                this.picContainer.Image = null;
            }
        }

        /// <summary>
        /// 显示警戒区域
        /// </summary>
        /// <param name="p_dtmRecordDate">宫口开大3cm时的记录时间</param>
        public void ShowWarnArea(DateTime p_dtmRecordDate)
        {
            this.objDrawer.m_mthDrawWarnArea(p_dtmRecordDate);
            this.objDrawer.m_mthDrawWarnLine(p_dtmRecordDate);
            this.Refresh();
        }

        /// <summary>
        /// 画图
        /// </summary>
        public void DrawImage()
        {
            this.objDrawer.m_mthDraw();
        }

        /// <summary>
        /// 获取打印用的图像
        /// </summary>
        /// <returns></returns>
        public Image GetPrintImage()
        {
            if (objDrawer != null)
            {
                return this.objDrawer.m_imgGetPrintImage();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 设定画图数据
        /// </summary>
        /// <param name="partogramData"></param>
        public void SetData(EmrPartogramData partogramData)
        {
            objDrawer.partogramData = partogramData;
            this.ShowImage();
        }

        /// <summary>
        /// 在数据点上弹出提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picContainer_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.objDrawer.lstShapeData != null && this.objDrawer.lstShapeData.Count > 0)
                {
                    List<clsShape> lstCurrentShape = this.objDrawer.lstShapeData.FindAll(t => t.rectArea.Contains(e.Location));
                    if (lstCurrentShape != null && lstCurrentShape.Count > 0)
                    {
                        Point pnt = this.PointToScreen(e.Location);
                        pnt.Offset(this.xscContainer.AutoScrollPosition.X, this.xscContainer.AutoScrollPosition.Y);
                        string strMessage = string.Empty;
                        foreach (clsShape objCurrent in lstCurrentShape)
                        {
                            strMessage += objCurrent.strMessage;
                        }
                        ttcMsg.ShowHint(strMessage, pnt);
                    }
                    else
                    {
                        ttcMsg.HideHint();
                    }
                }
                else
                {
                    ttcMsg.HideHint();
                }
            }
            catch (Exception ex)
            {
                Log.Output("产程进程图鼠标移动至：" + e.Location.ToString() + "导致" + ex.Message);
            }
            finally
            {

            }
        }

        /// <summary>
        /// 双击定位数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picContainer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (evtItemClicked != null)
            {
                clsDoubleClickedEventArgs objArgs = this.objDrawer.m_objGetArgs(e.X, e.Y);
                if (objArgs != null)
                {
                    this.ttcMsg.HideHint();
                    if (evtItemClicked != null)
                    {
                        evtItemClicked(sender, objArgs);
                    }
                }
            }
        }

        #region 双击事件
        public delegate void dgtMouseDoubleClickedEventHandler(object sender, clsDoubleClickedEventArgs args);
        public event dgtMouseDoubleClickedEventHandler evtItemClicked;
        #endregion
    }
}
