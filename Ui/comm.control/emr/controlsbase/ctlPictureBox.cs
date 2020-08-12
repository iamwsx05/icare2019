using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Common.Entity;

namespace Common.Controls.Emr
{
    /// <summary>
    /// ctlPictureBox
    /// </summary>
    public class ctlPictureBox : System.Windows.Forms.PictureBox, IRuntimeDesignControl, IPictureBox
    {
        /// <summary>
        /// 字体
        /// </summary>
        public Font TextFont { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        public ctlPictureBox()
        {
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.BackColor = Color.Transparent;
            this.AllowDrop = true;
            this.SizeMode = PictureBoxSizeMode.StretchImage;

        }

        #region IRuntimeDesignControl 成员

        /// <summary>
        /// Z轴深度
        /// </summary>
        [Browsable(false)]
        public decimal ZIndex { get; set; }


        /// <summary>
        /// 编辑值
        /// </summary>
        [Browsable(false)]
        public object EditObject { get; set; }

        /// <summary>
        /// 运行时只读
        /// </summary>
        [Browsable(false)]
        public bool RunTimeReadOnly { get; set; }

        /// <summary>
        /// 展示方式
        /// </summary>
        [Browsable(false)]
        public int PresentationMode { get; set; }

        //
        // 摘要:
        //     获取或设置 System.ComponentModel.Component 的 System.ComponentModel.ISite。
        //
        // 返回结果:
        //     与 System.ComponentModel.Component 关联的 System.ComponentModel.ISite（如果有）。如果
        //     System.ComponentModel.Component 未封装在 System.ComponentModel.IContainer 中，System.ComponentModel.Component
        //     没有与其关联的 System.ComponentModel.ISite 或者 System.ComponentModel.Component 已从其
        //     System.ComponentModel.IContainer 中移除，则为 null。
        //System.ComponentModel.ISite Site { get; set; }


        /// <summary>
        /// 用于存储当前控件是否可见值，如只用Visible字段的话，在设计时若把Visible=false则控件会消失
        /// 所以要另开字段存储：只供设计时使用
        /// Visible则在控件加载时使用
        /// </summary>
        [Browsable(false)]
        public bool Visible4Design { get; set; }

        /// <summary>
        /// 是否显示下划线
        /// </summary>
        [Browsable(false)]
        public bool ShowUnderLine { get; set; }

        /// <summary>
        /// 是否资料调用
        /// </summary>
        [Browsable(false)]
        public bool Referencetype { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        [Browsable(false)]
        public bool Essential { get; set; }

        /// <summary>
        /// 默认行数
        /// </summary>
        //[Browsable(false)]
        //public int DefaultRows { get; set; }

        /// <summary>
        /// 掩码类型
        /// </summary>
        [Browsable(false)]
        public int MaskType { set; get; }

        /// <summary>
        /// 同组必填控件组号(1,2,3... a,b,c...)
        /// </summary>
        [Browsable(false)]
        public string EssentialGroupNo { set; get; }

        #endregion
    }
}
