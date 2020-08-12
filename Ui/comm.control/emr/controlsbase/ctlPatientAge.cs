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
    public class ctlPatientAge : ctlBasePatientInfoControl, IRuntimeDesignControl, IFormCtrl
    {
        public override EnumPatientInfoType InfoType
        {
            get
            {
                return EnumPatientInfoType.年龄;
            }
        }
        
        #region IEfCtrl

        /// <summary>
        /// 项目代码(名称)
        /// </summary>
        private string _ItemName = "PatientAge";
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
        private string _ItemCaption = "年龄";
        /// <summary>
        /// 项目标题(描述)
        /// </summary>
        public override string ItemCaption
        {
            get { return _ItemCaption; }
            set { _ItemCaption = value; }
        }
        
        #endregion
    }
}
