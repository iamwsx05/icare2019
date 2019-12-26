using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.MFZ.Controls
{

    /// <summary>
    /// 诊区列表
    /// </summary>
    public partial class ctlDiagnoseAreaCombox : ComboBox
    {

        #region 构造函数

        public ctlDiagnoseAreaCombox()
        {
            InitializeComponent();
            DropDownStyle = ComboBoxStyle.DropDownList;
        }
 
        #endregion

        #region 属 性

        /// <summary>
        /// 获取或设置当前选中的诊区ID
        /// </summary>
        public int m_intAreaID
        {
            get
            {
                if (this.SelectedItem != null)
                    return ((clsMFZDiagnoseAreaVO)this.SelectedItem).m_intDiagnoseAreaID;
                return DBAssist.NullInt;
            }
            set
            {
                this.SelectedItem = null;
                foreach (clsMFZDiagnoseAreaVO objArea in this.Items)
                {
                    if (objArea.m_intDiagnoseAreaID == value)
                    {
                        this.SelectedItem = objArea;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 获取当前选定的诊区
        /// </summary>
        public clsMFZDiagnoseAreaVO SelectedDiagnoseAreaVO
        {
            get
            {
                if (this.SelectedItem != null && (((clsMFZDiagnoseAreaVO)this.SelectedItem).m_intDiagnoseAreaID != -1))
                    return (clsMFZDiagnoseAreaVO)this.SelectedItem;
                return null;
            }
        } 

        #endregion

        #region 辅助方法

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (!this.DesignMode)
            {
                try
                {
                    m_mthLoadData();
                }
                catch { }
            }
        }

        /// <summary>
        /// 初始化诊区列表
        /// </summary>
        private void m_mthLoadData()
        {
            //加入空项
            clsMFZDiagnoseAreaVO obj = new clsMFZDiagnoseAreaVO();
            obj.m_intDiagnoseAreaID = DBAssist.NullInt;
            obj.m_strDiagnoseAreaName = string.Empty;
            obj.m_strSummary = string.Empty;
            this.Items.Add(obj);
            //加载数据
            clsMFZDiagnoseAreaVO[] objDiagnoseAreaArr = null;
            long lngRes = clsTmdDiagnoseAreaSmp.s_object.m_lngFind(out objDiagnoseAreaArr);
            if (lngRes > 0 && objDiagnoseAreaArr != null)
            {
                foreach (clsMFZDiagnoseAreaVO objArea in objDiagnoseAreaArr)
                {
                    this.Items.Add(objArea);
                }
            }
        } 

        #endregion

    }
}
