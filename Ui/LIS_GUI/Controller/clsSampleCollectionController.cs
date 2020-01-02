using System;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;
using weCare.Core.Entity;
using System.Windows.Forms;
using System.Data;


namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// �����ɼ������Controller
    /// </summary>
    public class clsSampleCollectionController : com.digitalwave.GUI_Base.clsController_Base
    {
        private frmSampleCollection m_frmViewer;

        #region ���캯��
        public clsSampleCollectionController()
        {
        }
        #endregion

        #region override
        public override void Set_GUI_Apperance(frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_frmViewer = (frmSampleCollection)frmMDI_Child_Base_in;
        }

        #endregion

        #region ���ݱ걾�Ų�ѯ�걾״̬

        /// <summary>
        /// ���ݱ걾�Ų�ѯ�걾״̬
        /// </summary>
        /// <param name="strSampleID"></param>
        /// <param name="intStatus"></param>
        /// <returns></returns>
        public long m_lngFindStatusBySampleID(string strSampleID, out int intStatus)
        {
            long lngRes = 0;
            lngRes = new clsDomainController_SampleManage().m_lngFindStatusBySampleID(strSampleID, out intStatus);
            return lngRes;
        }

        #endregion

        #region �޸�������

        /// <summary>
        /// �޸�������
        /// </summary>
        /// <param name="strSampleID"></param>
        /// <param name="strAppID"></param>
        /// <returns></returns>
        public long m_lngModifyBarCode(string strSampleID, string strAppID)
        {
            long lngRes = 0;
            lngRes = new clsDomainController_SampleManage().m_lngModifyBarCode(strSampleID, strAppID);
            return lngRes;
        }

        #endregion

        #region BarCode�Ƿ����

        /// <summary>
        /// BarCode�Ƿ����
        /// </summary>
        /// <param name="p_strBarCode"></param>
        /// <returns></returns>
        public bool m_blnBarCodeExsit(string p_strBarCode)
        {
            long lngRes = 0;

             clsT_OPR_LIS_SAMPLE_VO[] objSampleVOArr = null;

            try
            {
                lngRes = new clsDomainController_SampleManage().m_lngGetSampleVOByBarcode(p_strBarCode, out objSampleVOArr);
            }
            catch
            {
                lngRes = 0;
            }

            if (objSampleVOArr != null && objSampleVOArr.Length > 0 && lngRes > 0)
            {
                return true;
            }
            else if (lngRes <= 0)
            {
                throw new Exception("�������ݿ�ʧ��!");
            }
            return false;
        }

        #endregion

        #region ����������Ϣ

        /// <summary>
        /// ����������Ϣ
        /// </summary>
        public long m_lngSave(string p_strAppID, ref clsT_OPR_LIS_SAMPLE_VO p_objSampleVO)
        {
            long lngRes = 0;
            try
            {
                lngRes = new clsDomainController_SampleManage().m_lngAddNewSampleAndModifyAppSampleGroup(p_strAppID, ref p_objSampleVO);
            }
            catch
            {
                lngRes = 0;
            }
            return lngRes;
        }

        #endregion

        #region ��������Id��ȡ����VO

        /// <summary>
        /// ��������Id��ȡ����VO
        /// </summary>
        /// <param name="p_strSampleID"></param>
        /// <param name="p_objSampleVO"></param>
        /// <returns></returns>
        public long m_lngGetSampleBySampleID(string p_strSampleID, out clsT_OPR_LIS_SAMPLE_VO p_objSampleVO)
        {
            long lngRes = 0;
            p_objSampleVO = null;
            clsT_OPR_LIS_SAMPLE_VO[] objSampleVOArr = null;
            try
            {
                lngRes = new clsDomainController_SampleManage().m_lngGetSampleVOArrBySampleID(p_strSampleID, out objSampleVOArr);
                if (lngRes > 0 && objSampleVOArr != null && objSampleVOArr.Length > 0)
                {
                    p_objSampleVO = objSampleVOArr[0];
                }
            }
            catch
            {
                lngRes = 0;
                p_objSampleVO = null;
            }
            return lngRes;
        }

        #endregion

        #region �������뵥Id��ȡ����������VO

        /// <summary>
        /// �������뵥Id��ȡ����������VO
        /// </summary>
        /// <param name="p_ApplicationID"></param>
        /// <param name="p_objSampleGroupVOArr"></param>
        /// <returns></returns>
        public long m_lngGetSampleRemark(string p_ApplicationID, out clsSampleGroup_VO[] p_objSampleGroupVOArr)
        {
            long lngRes = 0;
            p_objSampleGroupVOArr = null;
            try
            {
                lngRes = new clsDomainController_SampleGroupManage().m_lngGetSampleRemark(p_ApplicationID, out p_objSampleGroupVOArr);
                if (lngRes <= 0)
                {
                    return -1;
                }
            }
            catch
            {
                lngRes = 0;
                p_objSampleGroupVOArr = null;
            }
            return lngRes;
        }

        #endregion

        #region ��ȡ������Ϣ

        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="p_blnConfig"></param>
        /// <param name="p_strSetID"></param>
        /// <returns></returns>
        public long m_lngGetCollocate(out bool p_blnConfig, string p_strSetID)
        {
            p_blnConfig = false;
            long lngRes = 0;
            try
            {
                lngRes = new clsDomainController_SampleGroupManage().m_lngGetCollocate(out p_blnConfig, p_strSetID);
                if (lngRes <= 0)
                {
                    return -1;
                }
            }
            catch
            {
                lngRes = 0;
                p_blnConfig = false;
            }
            return lngRes;
        }

        #endregion

        #region �ı伱��״̬

        /// <summary>
        /// �ı伱��״̬
        /// </summary>
        /// <param name="p_objApplVO"></param>
        /// <returns></returns>
        public long m_lngChangeEmergency(clsLisApplMainVO p_objApplVO)
        {
            long lngRes = 0;
            try
            {
                lngRes = (new clsDomainController_ApplicationManage()).UpdateEmergencyStatus(p_objApplVO);
                if (lngRes < 0)
                {
                    return -1;
                }
            }
            catch
            {

            }
            return lngRes;
        }

        #endregion

        #region סԺ�������뵥 �ɼ���ѯ��BIH��

        /// <summary>
        /// סԺ�����ɼ���ѯ
        /// </summary>
        /// <param name="p_strFromDate">��ʼʱ��</param>
        /// <param name="p_strToDate">��ֹʱ��</param>
        /// <param name="p_strAppDeptID">���벡��Id</param>
        /// <param name="p_intStatus">����״̬</param>
        /// <param name="p_strPatientName">��������</param>
        /// <param name="p_strPatientCardID">���߿���</param>
        /// <param name="p_strHosipitalNO">סԺ��</param>
        /// <param name="p_objAppVOArr">�������뵥��������</param>
        /// <returns></returns>
        public long m_lngQuery(string p_strFromDate, string p_strToDate, string p_strAppDeptID, int p_intStatus, string p_strPatientName, string p_strPatientCardID, string p_strHosipitalNO, String bedNo, out clsLisApplMainVO[] p_objAppVOArr)
        {
            long lngRes = 0;
            p_objAppVOArr = null;
            try
            {
                lngRes = new clsDomainController_ApplicationManage().m_lngGetAppAndSampleInfo(p_intStatus, p_strAppDeptID, p_strFromDate, p_strToDate, p_strPatientName, p_strPatientCardID, p_strHosipitalNO, bedNo, out p_objAppVOArr);
            }
            catch
            {
                lngRes = 0;
            }
            return lngRes;
        }

        #endregion

        #region �������뵥��ѯ

        /// <summary>
        /// ����������뵥 �ɼ���ѯ
        /// </summary>
        /// <param name="p_strFromDate"></param>
        /// <param name="p_strToDate"></param>
        /// <param name="p_strAppDeptID"></param>
        /// <param name="p_intStatus"></param>
        /// <param name="p_strPatientName"></param>
        /// <param name="p_strPatientCardID"></param>
        /// <param name="p_objAppVOArr"></param>
        /// <returns></returns>
        public long m_lngQuery(string p_strFromDate, string p_strToDate, string p_strAppDeptID, int p_intStatus, string p_strPatientName, string p_strPatientCardID, string p_strAcceptStatus, int p_intSampleBackeStatus, out clsLisApplMainVO[] p_objAppVOArr)
        {
            long lngRes = 0;
            p_objAppVOArr = null;
            try
            {
                lngRes = new clsDomainController_ApplicationManage().m_lngGetAppAndSampleInfo(p_intStatus, p_strAppDeptID, p_strFromDate, p_strToDate, p_strPatientName, p_strPatientCardID, p_strAcceptStatus, p_intSampleBackeStatus, out p_objAppVOArr);
            }
            catch
            {
                lngRes = 0;
            }
            return lngRes;
        }

        #endregion


        #region ���Ҳ���
        /// <summary>
        /// ���Ҳ���	���������ַ���
        /// </summary>
        /// <param name="strCode">�����ַ���</param>
        /// <param name="p_objResultArr">��������	[out ����]</param>
        public long m_lngFindArea(string strCode, out clsBIHArea[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = new clsDomainController_SampleManage().m_lngFindArea(strCode, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region �޸Ĳ�����Ա
        /// <summary>
        /// �޸Ĳ�����Ա
        /// </summary>
        /// <param name="p_strEmpId"></param>
        /// <param name="p_strSampleId"></param>
        public void m_mthUpdateCollector(string p_strEmpId, string p_strSampleId, string p_strApplicationID)
        {
            long lngRes = 0;
            lngRes = new clsDomainController_SampleManage().m_lngInsertCollector(p_strEmpId, p_strSampleId, p_strApplicationID);
        }
        #endregion
    }
}
