using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace Common.Controls.Emr
{
    interface IObjectPropertyWrapper : ICloneable, INotifyPropertyChanged
    {
        [Browsable(false)]
        object WrappedObject { get; set; }

        [Browsable(false)]
        Type WrappedType { get; }
    }

    public enum enumBodyExamStyle
    {
        X = 1,
        BC = 2,
        CT = 3
    }
    
    public interface ICpNode
    {
        string NodeName { get; set; }

        //string NodeCaption { get; set; }

        string NodeType { get; set; }

        string NodeDays { get; set; }

        string ParentNodeName { get; set; }

        //event HandleNodeMouseLeftClick NodeMouseLeftClick;

        //event HandleNodeMouseRightClick NodeMouseRightClick;

        //event HandleNodeReturnClick NodeReturnClick;
    }

    /// <summary>
    /// IFormCtrl
    /// </summary>
    public interface IFormCtrl
    {
        /// <summary>
        /// 项目名称(代码)
        /// </summary>
        string ItemName { get; set; }

        /// <summary>
        /// 项目类型
        /// </summary>
        string ItemType { get; set; }

        /// <summary>
        /// 项目标题(描述)
        /// </summary>
        string ItemCaption { get; set; }

        /// <summary>
        /// 父节点名
        /// </summary>
        string ParentNode { get; set; }

        /// <summary>
        /// 值变化标志
        /// </summary>
        bool ValueChangedFlag { get; set; }
        
        /// <summary>
        /// 计算类型
        /// </summary>
        string CalProperty { get; set; }

        /// <summary>
        /// 行缩进字符个数
        /// </summary>
        int RowShrinkDigit { get; set; }       


    }  

    public interface IPictureBox
    {
        string FileName { get; set; }
    }

    public interface ICombox
    {
        string DictionaryName { get; set; }
        //List<string> Items { get; set; }
        event HandleItemMouseClick ItemMouseClick;
        DevExpress.XtraEditors.Controls.ComboBoxItemCollection Items { get; }
    }

}
