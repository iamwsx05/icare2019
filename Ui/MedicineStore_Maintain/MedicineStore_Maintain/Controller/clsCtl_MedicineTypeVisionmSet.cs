using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using com.digitalwave.iCare.gui.MedicineStore;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.MedicineStore_Maintain
{
    /// <summary>
    /// ҩƷ������ʾ����
    /// </summary>
    public class clsCtl_MedicineTypeVisionmSet : com.digitalwave.GUI_Base.clsController_Base
    {
        #region ȫ�ֱ���
        /// <summary>
        /// ģ�������
        /// </summary>
        private clsDcl_MedicineTypeVisionmSet m_objDomain = null;

        /// <summary>
        /// ����
        /// </summary>
        private com.digitalwave.iCare.gui.MedicineStore_Maintain.frmMedicineTypeVisionmSet m_objViewer;


        #endregion
        
        #region ���캯��.

        /// <summary>
        /// ҩƷ��������
        /// </summary>
        public clsCtl_MedicineTypeVisionmSet()
        {
            m_objDomain = new clsDcl_MedicineTypeVisionmSet();
        }
        #endregion

        #region ���ô������
        /// <summary>
        /// ���ô������.
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmMedicineTypeVisionmSet)frmMDI_Child_Base_in;
        }
        #endregion

        #region ��ȡ������������
        /// <summary>
        /// ��ȡ������������
        /// </summary>
        internal void m_mthGetAllMedicineTypeVisionm()
        {
            
            m_objDomain.m_lngGetAllMedicineTypeVisionm(out m_objViewer.dtbTypeVisionm);
            m_objViewer.m_dgvTypeVisionm.DataSource = m_objViewer.dtbTypeVisionm;
        }
        #endregion

        #region ����
        public long m_mthSaverMedicineType()
        {
            long lngRes = 0;
            int iRowLength = m_objViewer.dtbTypeVisionm.Rows.Count;
            string strSeriesID = string.Empty;
            clsMS_MedicineTypeVisionmSet[] objMedicineType = new clsMS_MedicineTypeVisionmSet[iRowLength];
            DataRow dr = null;
            for (int iOr = 0; iOr < iRowLength; iOr++)
            {
                dr = m_objViewer.dtbTypeVisionm.Rows[iOr];
                objMedicineType[iOr] = new clsMS_MedicineTypeVisionmSet();
                objMedicineType[iOr].m_strMedicineTypeid = dr["medicinetypeid_chr"].ToString();
                if (dr["lotno_int"] == DBNull.Value)
                {
                    objMedicineType[iOr].m_intLotno = 0;
                }
                else
                {
                    objMedicineType[iOr].m_intLotno = Convert.ToBoolean(dr["lotno_int"]) ? 1 : 0;
                }
                if (dr["validperiod_int"] == DBNull.Value)
                {
                    objMedicineType[iOr].m_intValidperiod = 0;
                }
                else
                {
                    objMedicineType[iOr].m_intValidperiod = Convert.ToBoolean(dr["validperiod_int"]) ? 1 : 0;
                }
            
            }

            lngRes = m_objDomain.m_lngSaverMedicineType(objMedicineType);

            if (lngRes > 0)
            {
                System.Windows.Forms.MessageBox.Show("����ɹ�", "ҩƷ��������", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Asterisk);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("����ʧ��", "ҩƷ��������", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
            return lngRes;
        }
        #endregion
    }
}
