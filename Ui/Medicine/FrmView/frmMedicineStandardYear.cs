using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// 设置中标年份
    /// </summary>
    public partial class frmMedicineStandardYear : Form
    {
        public delegate void OnReturn(string strYear);
        /// <summary>
        /// 返回选择的年份
        /// </summary>
        public event OnReturn OnReturnValue;
        /// <summary>
        /// 药品ID
        /// </summary>
        private string m_strMedicineID = string.Empty;

        /// <summary>
        /// 设置中标年份
        /// </summary>
        /// <param name="p_strMedicineID">药品ID</param>
        /// <param name="p_intYear">系统设定的年份</param>
        /// <param name="p_strStandardDate">初始化传入的年份</param>
        public frmMedicineStandardYear(string p_strMedicineID,int p_intYear,string p_strStandardDate)
        {
            InitializeComponent();

            m_strMedicineID = p_strMedicineID;
            m_cklbYear.Items.Clear();
            int iYearNow = DateTime.Now.Year;
            for (int i1 = iYearNow - p_intYear; i1 <= iYearNow + p_intYear; i1++)
            {
                m_cklbYear.Items.Add(i1);
            }

            if (p_strStandardDate.Length > 0 && m_cklbYear.Items.Count > 0)
            {
                for (int j1 = 0; j1 < m_cklbYear.Items.Count; j1++)
                {
                    m_cklbYear.SetItemChecked(j1, false);
                }
                string[] m_strStandarddateArr = p_strStandardDate.Split('*');
                foreach (string s in m_strStandarddateArr)
                {
                    for (int i1 = 0; i1 < m_cklbYear.Items.Count; i1++)
                    {
                        if (m_cklbYear.Items[i1].ToString() == s)
                        {
                            m_cklbYear.SetItemChecked(i1, true);
                            break;
                        }
                    }
                }
            }
            else
            {
                for (int j1 = 0; j1 < m_cklbYear.Items.Count; j1++)
                {
                    m_cklbYear.SetItemChecked(j1, false);
                }
            }
        }

        private void m_btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void m_btnSet_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            if (OnReturnValue != null)
            {
                string strReturn = "";
                for (int i1 = 0; i1 < m_cklbYear.CheckedItems.Count; i1++)
                {
                    strReturn += m_cklbYear.CheckedItems[i1].ToString() + "*";
                }
                if (strReturn.Length > 0)
                    strReturn = strReturn.Substring(0, strReturn.Length - 1);
                clsControlMedicine m_objCtl = new clsControlMedicine();
                long intRes = m_objCtl.m_lngSaveStandardYear(m_strMedicineID, strReturn);
                if(intRes > 0)
                    OnReturnValue(strReturn);
            }
            Close();
        }

        private void frmMedicineStandardYear_Load(object sender, EventArgs e)
        {
            m_cklbYear.TopIndex = (int)Math.Floor(m_cklbYear.Items.Count / 2.0);
        }
    }
}