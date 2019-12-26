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
    public partial class ctlDeptCombox : ComboBox
    {
        #region ���캯��

        public ctlDeptCombox()
        {
            InitializeComponent();
            DropDownStyle = ComboBoxStyle.DropDownList;
        }
        public ctlDeptCombox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        } 

        #endregion

        #region ˽�г�Ա

        private int m_intAreaID;
        private string m_strDeptId;
        
        #endregion

        #region ��������

        private void mthLoadData()
        {
            this.Items.Clear();
            clsMFZDeptVO dept = new clsMFZDeptVO();
            dept.m_strDeptID = string.Empty;
            dept.m_strDeptName = string.Empty;
            dept.m_strSummary = string.Empty;
            dept.m_intDiagnoseAreaID = DBAssist.NullInt;
            Items.Add(dept);
        } 

        #endregion
        
        #region �� ��

        public string m_strDeptID
        {
            get
            {
                if (SelectedItem != null)
                {
                    clsMFZDeptVO dept = SelectedItem as clsMFZDeptVO;
                    return dept.m_strDeptID;
                }
                return string.Empty;
            }
            set
            {
                this.SelectedItem = null;
                m_strDeptId = value;
                foreach (clsMFZDeptVO dept in Items)
                {
                    if (dept.m_strDeptID == m_strDeptId)
                    {
                        this.SelectedItem = dept;
                    }
                }
            }

        }

        public int m_intDiagnoseAreaID
        {
            set
            {
                DropDownStyle = ComboBoxStyle.DropDownList;
                m_intAreaID = value;
                mthLoadData();
            }
        }

        public clsMFZDeptVO SelectedDept
        {
            get
            {
                if (this.SelectedItem != null && ((clsMFZDeptVO)this.SelectedItem).m_strDeptID != string.Empty)
                {
                    return (clsMFZDeptVO)SelectedItem;
                }
                return null;
            }
        } 

        #endregion
    }
}
