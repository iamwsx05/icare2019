using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// 动画视频播放UI类
    /// </summary>
    public partial class frmAnimation : Form
    {
        /// <summary>
        /// 视频AVI文件名
        /// </summary>
        private string AviFile = "";
        /// <summary>
        /// 提示信息
        /// </summary>
        private string MsgInfo = "";
        /// <summary>
        /// 构造
        /// </summary>
        public frmAnimation(string AviFileName, string MessageInfo)
        {
            InitializeComponent();
            AviFile = AviFileName;
            MsgInfo = MessageInfo;
            this.AutoSizeMode = AutoSizeMode.GrowOnly;
        }

        private void frmAnimation_Load(object sender, EventArgs e)
        {
            this.m_mthStart();
        }

        /// <summary>
        /// start
        /// </summary>
        public void m_mthStart()
        {
            Application.DoEvents();
        }
         
    }
}