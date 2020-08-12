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
    public partial class ctlHospitalName : ctlBasePatientInfoControl, IRuntimeDesignControl, IFormCtrl
    {
        public ctlHospitalName()
        {
            this.Font = new Font("华文宋体", 15.0f);
            this.TextAlign = ContentAlignment.MiddleCenter;
            this.ShowUnderLine = false;
            this.AutoSize = true;
        }

        //private int m_intNums = 0;
        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    this.PresentationMode = 2;
        //    base.OnPaint(e);
        //    if (!DesignMode)
        //    {
        //        if (m_intNums == 0)
        //        {                    
        //            this.Text = this.GetDataText();
        //            m_intNums++;
        //        }
        //    }            
        //}

        public override EnumPatientInfoType InfoType
        {
            get
            {
                return EnumPatientInfoType.医院名称;
            }
        }

        [Browsable(false)]
        public string EssentialGroupNo { get; set; }

        #region IEfCtrl

        /// <summary>
        /// 项目代码(名称)
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 项目类型
        /// </summary>
        public string ItemType { get; set; }

        /// <summary>
        /// 项目标题(描述)
        /// </summary>
        public string ItemCaption { get; set; }

        /// <summary>
        /// 父节点名
        /// </summary>
        public string ParentNode { get; set; }
        
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
