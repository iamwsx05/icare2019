using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.MedicineStore_Maintain
{
    /// <summary>
    /// �ֿ�����ҵ�������.
    /// </summary>
    public class clsCtl_MedicineStoreroomSet:com.digitalwave.GUI_Base.clsController_Base
    {
        #region ȫ�ֱ���

        /// <summary>
        /// ģ�������
        /// </summary>
        private clsDcl_MedicineStoreroomSet m_objDomain = null;

        /// <summary>
        /// ����
        /// </summary>
        private com.digitalwave.iCare.gui.MedicineStore_Maintain.frmMedicineStoreroomSet m_objViewer;

        #endregion

        #region ���캯��.

        /// <summary>
        /// ���캯��.
        /// </summary>
        public clsCtl_MedicineStoreroomSet()
        {
            m_objDomain = new clsDcl_MedicineStoreroomSet();
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
            m_objViewer = (frmMedicineStoreroomSet)frmMDI_Child_Base_in;
        }
        #endregion

        #region ��ȡҩƷ������Ϣ.

        /// <summary>
        /// ��ȡҩƷ������Ϣ.
        /// </summary>
        /// <param name="m_objMedicineTypeArr">���ؽ��.</param>
        internal void m_mthGetMedicineTypeInfo(out clsMS_MedicineType_VO[] m_objMedicineTypeArr)
        {
            m_objDomain.m_lngGetMedicineInfo(out m_objMedicineTypeArr);
        }
        #endregion

        #region ��ȡ�ⷿ��Ϣ.

        /// <summary>
        /// ��ȡ�ⷿ��Ϣ.
        /// </summary>
        /// <param name="m_lstMedicineRoomArr">���ؽ��.</param>
        public void m_mthGetMedicineRoomInfo(ref List<clsMS_MedicineStoreroom_VO> m_lstMedicineRoomArr)
        {
            clsMS_MedicineStoreroom_VO[] m_objMedicineRoomArr = null;
            //m_lstMedicineRoomArr = null;

            m_objDomain.m_lngGetMedicineStoreInfo(out m_objMedicineRoomArr);

            if (m_objMedicineRoomArr == null || m_objMedicineRoomArr.Length == 0)
                return;
            foreach (clsMS_MedicineStoreroom_VO obj_VO in m_objMedicineRoomArr)
            {
                m_lstMedicineRoomArr.Add(obj_VO);
            }
        }
        #endregion

        #region ��ӿ���¼.

        /// <summary>
        ///  ��ӿ���¼.
        /// </summary>
        /// <param name="p_objMedicineStore">�����ⷿ��¼.</param>
        public void m_mthInsertMedicineRoomInfo(ref clsMS_MedicineStoreroom_VO p_objMedicineStore)
        {
            m_objDomain.m_lngInsertMedicineStoreInfo(ref p_objMedicineStore);
        }
        #endregion
        #region ��ӿ���¼.

        /// <summary>
        ///  ��ӿ���¼.
        /// </summary>
        /// <param name="p_objMedicineStore">�����ⷿ��¼.</param>
        public void m_mthInsertMedStoreSetInfo(ref clsMS_MedicineStoreroom_VO p_objMedicineStore)
        {
            m_objDomain.m_lngAddNewMedStoreSetInfo(ref p_objMedicineStore);
        }
                #endregion
        #region ɾ���ⷿ��¼.

        /// <summary>
        /// ɾ���ⷿ��¼.
        /// </summary>
        /// <param name="strStoreID">Ҫɾ���Ŀⷿ��¼�ĿⷿID.</param>
        public void m_mthDeleteMedicineRoomInfo(string strStoreID)
        {
            long lngRes = m_objDomain.m_lngDeleteMedicineStoreInfo(strStoreID);

            if (lngRes > 0)
            {
                m_objViewer.m_blnIsSave = true;
                m_objViewer.m_txtMedicineStoreRoom.Text = "";
                System.Windows.Forms.ListView.CheckedListViewItemCollection lvcCollection = m_objViewer.m_lsvMedicineName.CheckedItems;
                if (lvcCollection.Count == 0)
                {
                    return;
                }
                foreach (System.Windows.Forms.ListViewItem lvi in lvcCollection)
                {
                    lvi.Checked = false;
                }
            }
        }
        #endregion
        #region ɾ���ⷿ��¼.

        /// <summary>
        /// ɾ���ⷿ��¼.
        /// </summary>
        /// <param name="strStoreID">Ҫɾ���Ŀⷿ��¼�ĿⷿID.</param>
        public void m_mthDeleteMedStoreSetInfo(string strStoreID,bool blnDeleted)
        {
            long lngRes = m_objDomain.m_lngDeleteMedStoreSetInfo(strStoreID);

            if (lngRes > 0)
            {
                m_objViewer.m_blnIsSave = true;
                System.Windows.Forms.ListView.CheckedListViewItemCollection lvcCollection = m_objViewer.m_lsvMedicineName.CheckedItems;
                if (lvcCollection.Count == 0)
                {
                    return;
                }
                if (blnDeleted == true)
                {
                    foreach (System.Windows.Forms.ListViewItem lvi in lvcCollection)
                    {
                        lvi.Checked = false;
                    }
                }
            }
        }
        #endregion
        #region ��ѯ�ⷿҩƷ��Ϣ.

        /// <summary>
        /// ��ѯ�ⷿҩƷ��Ϣ.
        /// </summary>
        /// <param name="strStoreID">��ѯ����.</param>
        /// <param name="strMedicineNameArr">���ؽ��.</param>
        public void m_mthSelectMedicineName(string strStoreID, out string[] strMedicineNameArr)
        {
            m_objDomain.m_lngSelectMedicineName(strStoreID, out strMedicineNameArr);
        }
        #endregion

        #region ��ȡָ���ֿ������õ�ҩƷ����
        /// <summary>
        /// ��ȡָ���ֿ������õ�ҩƷ����
        /// </summary>
        /// <param name="p_strStoreRoomID"></param>
        internal void m_mthSetMedicineTypeCheck(string p_strStoreRoomID)
        {
            clsMS_MedicineType_VO[] objMedType = null;
              long lngRes=0;
            if(this.m_objViewer.m_strMedStoreArr==null)
             lngRes = m_objDomain.m_lngGetStoreRoomSetCheck(p_strStoreRoomID, out objMedType);
            else
             lngRes = m_objDomain.m_lngGetMedStoreSetCheck(p_strStoreRoomID, out objMedType);

            if (objMedType == null || objMedType.Length == 0)
            {
                return;
            }

            System.Windows.Forms.ListViewItem lviTemp = null;
            for (int iType = 0; iType < objMedType.Length; iType++)
            {
                lviTemp = m_objViewer.m_lsvMedicineName.FindItemWithText(objMedType[iType].m_strMedicineTypeName_VCHR);
                if (lviTemp != null)
                {
                    lviTemp.Checked = true;
                }
            }

            m_objViewer.m_blnIsSave = false;
        }
        #endregion

        internal void m_lngGetMedStoreInfo(out DataTable m_dtMedStore)
        {
            m_objDomain.m_lngGetMedStoreInfo(out m_dtMedStore);
        }

        internal void m_lngGetDeptInfo(out DataTable m_dtDeptDesc)
        {
            m_objDomain.m_lngGetDeptInfo(out m_dtDeptDesc);
        }
    }
}
