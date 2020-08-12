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
    [ToolboxBitmap(typeof(ctlBasePatientInfoControl), "Icon.m.png")]
    public class ctlPatientWeight : ctlBasePatientInfoControl, IRuntimeDesignControl, IFormCtrl
    {
        public override EnumPatientInfoType InfoType
        {
            get
            {
                return EnumPatientInfoType.体重;
            }
        }

        #region IEfCtrl

        /// <summary>
        /// 项目代码(名称)
        /// </summary>
        private string _ItemName = "PatientWeight";
        /// <summary>
        /// 项目代码(名称)
        /// </summary>
        public override string ItemName
        {
            get { return _ItemName; }
            set { _ItemName = value; }
        }

        /// <summary>
        /// 项目标题(描述)
        /// </summary>
        private string _ItemCaption = "体重";
        /// <summary>
        /// 项目标题(描述)
        /// </summary>
        public override string ItemCaption
        {
            get { return _ItemCaption; }
            set { _ItemCaption = value; }
        }
        
        /// <summary>
        /// 计算类型
        /// </summary>
        [Browsable(false)]
        public string CalProperty { get; set; }

        /// <summary>
        /// 行缩进字符个数
        /// </summary>
        [Browsable(false)]
        public int RowShrinkDigit { get; set; }
        
        #endregion
    }
}
