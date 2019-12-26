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
    /// �����б�
    /// </summary>
    public partial class ctlDiagnoseAreaCombox : ComboBox
    {

        #region ���캯��

        public ctlDiagnoseAreaCombox()
        {
            InitializeComponent();
            DropDownStyle = ComboBoxStyle.DropDownList;
        }
 
        #endregion

        #region �� ��

        /// <summary>
        /// ��ȡ�����õ�ǰѡ�е�����ID
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
        /// ��ȡ��ǰѡ��������
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

        #region ��������

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
        /// ��ʼ�������б�
        /// </summary>
        private void m_mthLoadData()
        {
            //�������
            clsMFZDiagnoseAreaVO obj = new clsMFZDiagnoseAreaVO();
            obj.m_intDiagnoseAreaID = DBAssist.NullInt;
            obj.m_strDiagnoseAreaName = string.Empty;
            obj.m_strSummary = string.Empty;
            this.Items.Add(obj);
            //��������
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
