using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Entity;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Common.Controls
{
    public partial class frmBase : System.Windows.Forms.Form
    {
        public frmBase()
        {
            GlobalParm.IsPopupOpening = true;
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            this.ImeMode = System.Windows.Forms.ImeMode.OnHalf;
            this.AutoScaleMode = AutoScaleMode.None; 
            if (!DesignMode) this.CreateController();
        }

        /// <summary>
        /// ValueChanged
        /// </summary>
        public bool ValueChanged { get; set; }

        /// <summary>
        /// 是否取消退出
        /// </summary>
        public bool isCancelExit { get; set; }

        /// <summary>
        /// 窗体打开方式
        /// </summary>
        public string FormOperName { get; set; }

        /// <summary>
        /// 是否反射打开
        /// </summary>
        public bool ReflectOpen { get; set; }

        private bool isLoading = true;

        /// <summary>
        /// 暂停重绘.控件
        /// </summary>
        public virtual void PauseRedraw()
        { }

        /// <summary>
        /// 开始重绘.控件
        /// </summary>
        public virtual void StartRedraw()
        { }

        /// <summary>
        /// 窗体控制器
        /// </summary>
        protected BaseController Controller;

        /// <summary>
        /// 创建窗体控制器
        /// </summary>
        protected virtual void CreateController()
        { }

        /// <summary>
        /// 检查数据是否更改
        /// </summary>
        /// <returns></returns>
        protected virtual bool CheckDataChanged()
        {
            return false;
        }

        private void ResizeMar()
        {
            Rectangle rec = Screen.GetWorkingArea(this);
            this.marqueeProgressBarControl.Location = new Point((rec.Width - marqueeProgressBarControl.Width) / 2 - GlobalParm.MdiParentSubW, (rec.Height - marqueeProgressBarControl.Height) / 2 - GlobalParm.MdiParentSubH);
        }

        public void BeginLoading()
        {
            this.marqueeProgressBarControl.Show();
            this.marqueeProgressBarControl.BringToFront();
        }

        public void CloseLoading()
        {
            this.marqueeProgressBarControl.Hide();
        }

        private void frmBase_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;
            isLoading = false;
            ResizeMar();
            GlobalParm.IsPopupOpening = false;
            this.defaultLookAndFeel.LookAndFeel.SkinName = GlobalLogin.SkinName;
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(GlobalLogin.SkinName);
            //if (!string.IsNullOrEmpty(GlobalLogin.SkinMaskColorValue))
            //{
            //    this.defaultLookAndFeel.LookAndFeel.SkinMaskColor = GlobalLogin.SkinMaskColor;
            //    DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinMaskColors(GlobalLogin.SkinMaskColor, GlobalLogin.SkinMaskColor2);
            //}
        }

        private void frmBase_Activated(object sender, EventArgs e)
        {
            uiHelper.frmCurr = this;
        }

        private void frmBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.isCancelExit = false;
        }

    }
}
