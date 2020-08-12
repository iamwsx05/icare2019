using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Common.Controls.Emr
{
    public interface IRuntimeDesignControl
    {
        /// <summary>
        /// 编辑值
        /// </summary>
        object EditObject { get; set; }

        /// <summary>
        /// 文本值
        /// </summary>
        string Text { get; set; }

        Font TextFont { get; set; }

        /// <summary>
        /// 控件实例名称
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 宽度
        /// </summary>
        int Width { get; set; }

        /// <summary>
        /// 高度
        /// </summary>
        int Height { get; set; }

        /// <summary>
        /// Tab顺序
        /// </summary>
        int TabIndex { get; set; }

        /// <summary>
        /// 停靠方式
        /// </summary>
        System.Windows.Forms.DockStyle Dock { get; set; }

        /// <summary>
        /// 锚定方式
        /// </summary>
        System.Windows.Forms.AnchorStyles Anchor { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        System.Drawing.Point Location { get; set; }
        
        /// <summary>
        /// 背景色
        /// </summary>
        System.Drawing.Color BackColor { get; set; }

        /// <summary>
        /// 背景色
        /// </summary>
        System.Drawing.Color ForeColor { get; set; }

        /// <summary>
        /// 运行时只读
        /// </summary>
        bool RunTimeReadOnly { get; set; }

        /// <summary>
        /// 展示方式
        /// </summary>
        int PresentationMode { get; set; }

        /// <summary>
        /// Z轴深度
        /// </summary>
        decimal ZIndex { get; set; }

        //
        // 摘要:
        //     获取或设置 System.ComponentModel.Component 的 System.ComponentModel.ISite。
        //
        // 返回结果:
        //     与 System.ComponentModel.Component 关联的 System.ComponentModel.ISite（如果有）。如果
        //     System.ComponentModel.Component 未封装在 System.ComponentModel.IContainer 中，System.ComponentModel.Component
        //     没有与其关联的 System.ComponentModel.ISite 或者 System.ComponentModel.Component 已从其
        //     System.ComponentModel.IContainer 中移除，则为 null。
        System.ComponentModel.ISite Site { get; set; }

        bool Visible { get; set; }

        /// <summary>
        /// 用于存储当前控件是否可见值，如只用Visible字段的话，在设计时若把Visible=false则控件会消失
        /// 所以要另开字段存储：只供设计时使用
        /// Visible则在控件加载时使用
        /// </summary>
        bool Visible4Design { get; set; }

        /// <summary>
        /// 是否显示下划线
        /// </summary>
        bool ShowUnderLine { get; set; }

        /// <summary>
        /// 是否资料调用
        /// </summary>
        bool Referencetype { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        bool Essential { get; set; }        

        /// <summary>
        /// 掩码类型
        /// </summary>
        int MaskType { set; get; }

        /// <summary>
        /// 同组必填控件组号(1,2,3... a,b,c...)
        /// </summary>
        //string EssentialGroupNo { set; get; }
        

    }
}
