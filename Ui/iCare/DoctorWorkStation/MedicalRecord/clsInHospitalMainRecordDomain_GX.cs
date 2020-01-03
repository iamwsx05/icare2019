using System;
using System.IO;
using System.Text;
using weCare.Core.Entity;
using System.Data;

namespace iCare
{
    /// <summary>
    /// clsInHospitalMainRecordDomain_GX ��ժҪ˵����
    /// </summary>
    public class clsInHospitalMainRecordDomain_GX
    {
        public clsInHospitalMainRecordDomain_GX()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ģ����ѯ�������ʽ
        /// <summary>
        ///ģ����ѯ�������ʽ
        /// </summary>
        /// <returns></returns>
        public long m_lngGetAnaesthesiaModeLikeID(out clsAnaesthesiaModeInOperation[] p_objAnaesthesiaModeInOperation)
        {
            //clsQuery8iServ m_objServ =
            //    (clsQuery8iServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsQuery8iServ));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetAnaesthesiaModeLikeID(out p_objAnaesthesiaModeInOperation);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }
        #endregion

        #region ��ѯ����������ơ�����
        /// <summary>
        /// ��ѯ����������ơ�����
        /// </summary>
        /// <param name="P_dtbOp"></param>
        /// <returns></returns>
        public long m_lngGetOperationDesc(out DataTable P_dtbOp)
        {
            P_dtbOp = null;

            //clsQuery8iServ objServ =
            //    (clsQuery8iServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsQuery8iServ));

            long lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetOperationDesc(out P_dtbOp);
            //objServ.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡת�����
        /// <summary>
        /// ��ȡת�����
        /// </summary>
        /// <param name="p_strPatientID">����ID</param>
        /// <param name="p_strInPatientDate">סԺ�ǼǱ��е�סԺ����</param>
        /// <param name="p_strRegisterID">סԺ�ǼǺ�</param>
        /// <param name="p_objDeptInstance">ת�����</param>
        /// <returns></returns>
        public long m_lngGetInHospitalMainTransDeptInstance(string p_strPatientID, string p_strInPatientDate, out string p_strRegisterID, out clsInHospitalMainTransDeptInstance p_objDeptInstance)
        {
            p_strRegisterID = "";
            long lngRes = 0;

            //clsPatientManagerService objServ =
            //    (clsPatientManagerService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPatientManagerService));

            lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetRegisterIDByPatient(p_strPatientID, p_strInPatientDate, out p_strRegisterID);

            lngRes = m_lngGetInHospitalMainTransDeptInstance(p_strRegisterID, out p_objDeptInstance);
            //objServ.Dispose();
            return lngRes;
        }

        /// <summary>
        /// ��ȡ���˳�Ժʱ�䣬��ʱ���ڸ��������ѯ
        /// </summary>
        /// <returns></returns>
        public long m_lngGetInHospitalMainTransDeptInstance(string p_strRegisterID, out clsInHospitalMainTransDeptInstance p_objDeptInstance)
        {
            long lngRes = 0;

            //clsInHospitalMainRecordServ_GX objServ =
            //    (clsInHospitalMainRecordServ_GX)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInHospitalMainRecordServ_GX));

            lngRes = (new weCare.Proxy.ProxyEmr03()).Service.clsInHospitalMainRecordServ_GX_m_lngGetInHospitalMainTransDeptInstance(p_strRegisterID, out p_objDeptInstance);
            //objServ.Dispose();
            return lngRes;
        }
        #endregion

        #region ��øô�סԺ��סԺ������ҳ������ʱ��
        /// <summary>
        /// ��øô�סԺ��סԺ������ҳ������ʱ��
        /// ����Ϊû�����ɹ�
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <returns></returns>
        public long m_lngGetOpenDateInfo(string p_strRegisterID, out DateTime[] p_dtmOpenDate)
        {
            p_dtmOpenDate = null;
            if (p_strRegisterID == null || p_strRegisterID == "")
                return -1;

            //clsInHospitalMainRecordServ_GX m_objServ =
            //    (clsInHospitalMainRecordServ_GX)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInHospitalMainRecordServ_GX));

            string m_strXML = "";
            int m_intRows = 0;
            long lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetOpenDateInfo(p_strRegisterID, out p_dtmOpenDate);
            //m_objServ.Dispose();
            return lngRes;
        }

        #endregion

        #region �����ݿ��ü�¼
        /// <summary>
        /// �����ݿ��ü�¼
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_objMain"></param>
        /// <returns></returns>
        public long m_lngGetInfo(string p_strRegisterID, string p_strOpenDate, bool p_blnIsSumit, out clsInHospitalMainRecord_GX_Collection p_objCollection)
        {
            p_objCollection = null;
            if (p_strRegisterID == null || p_strRegisterID == "")
                return -1;
            if (p_strOpenDate == null || p_strOpenDate == "")
                return -1;

            //clsInHospitalMainRecordServ_GX m_objServ =
            //    (clsInHospitalMainRecordServ_GX)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInHospitalMainRecordServ_GX));

            long m_lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetInfo(p_strRegisterID, p_strOpenDate, p_blnIsSumit, out p_objCollection);
            //m_objServ.Dispose();
            return m_lngRes;
        }

        /// <summary>
        /// ��ȡ��ɾ����¼
        /// </summary>
        /// <param name="p_strRegisterID"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_objCollection"></param>
        /// <returns></returns>
        public long m_lngGetDeletedInfo(string p_strRegisterID, string p_strOpenDate, out clsInHospitalMainRecord_GX_Collection p_objCollection)
        {
            p_objCollection = null;
            if (p_strRegisterID == null || p_strRegisterID == "")
                return -1;
            if (p_strOpenDate == null || p_strOpenDate == "")
                return -1;

            //clsInHospitalMainRecordServ_GX m_objServ =
            //    (clsInHospitalMainRecordServ_GX)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInHospitalMainRecordServ_GX));

            long m_lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetDelInfo(p_strRegisterID, p_strOpenDate, out p_objCollection);
            //m_objServ.Dispose();
            return m_lngRes;
        }

        #endregion

        #region ģ����ѯԱ��
        /// <summary>
        /// ģ����ѯԱ��
        /// </summary>
        /// <param name="p_strSome"></param>
        /// <param name="p_lsvEmp"></param>
        /// <returns></returns>
        public long m_lngGetEmpArrByIDOrNameLike(string p_strSome, out System.Windows.Forms.ListViewItem[] p_lsvEmpItemArr)
        {
            p_lsvEmpItemArr = null;

            //clsEMR_EmployeeManagerService m_objServ =
            //    (clsEMR_EmployeeManagerService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_EmployeeManagerService));

            DataTable dtValue = null;
            long m_lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetEmpArrByIDOrNameLike(p_strSome, out dtValue);

            if (m_lngRes > 0 && dtValue.Rows.Count > 0)
            {
                p_lsvEmpItemArr = new System.Windows.Forms.ListViewItem[dtValue.Rows.Count];
                for (int i = 0; i < dtValue.Rows.Count; i++)
                {
                    p_lsvEmpItemArr[i] = new System.Windows.Forms.ListViewItem(dtValue.Rows[i][1].ToString());
                    p_lsvEmpItemArr[i].SubItems.Add(dtValue.Rows[i][2].ToString());
                }
            }
            //m_objServ.Dispose();
            return m_lngRes;
        }
        #endregion

        #region ��������
        public long m_lngDoSave(clsInHospitalMainRecord_GX_Collection p_objCollection, bool p_bolIfAddNew)
        {
            long lngRes = 0;
            try
            {
                //clsInHospitalMainRecordServ_GX m_objServ =
                //    (clsInHospitalMainRecordServ_GX)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInHospitalMainRecordServ_GX));

                if (p_bolIfAddNew)//����
                    lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngAddNew(p_objCollection);
                else//�޸�
                    lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngModify(p_objCollection);
                //m_objServ.Dispose();
            }
            catch (Exception ex)
            {
                clsPublicFunction.ShowInformationMessageBox(ex.Message);
                return -1;
            }
            return lngRes;
        }
        #endregion

        #region �������޸�ʱ��,�޸���
        /// <summary>
        /// �������޸�ʱ��,�޸���
        /// ������ؿգ���ʾ�ü�¼�ѱ�ɾ��
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_strLastModifyDate"></param>
        /// <param name="p_strLastModifyUserID"></param>
        /// <returns></returns>
        public long m_lngGetLastModifyDateAndUser(long p_lngEMR_SEQ, out string p_strLastModifyUserID, out DateTime p_dtmLastModifyDate)
        {
            p_dtmLastModifyDate = DateTime.MinValue;
            p_strLastModifyUserID = null;

            //clsInHospitalMainRecordServ_GX m_objServ =
            //    (clsInHospitalMainRecordServ_GX)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInHospitalMainRecordServ_GX));

            long lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetLastModifyDateInfo(p_lngEMR_SEQ, out p_strLastModifyUserID, out p_dtmLastModifyDate);
            //m_objServ.Dispose();
            return lngRes;
        }
        #endregion

        #region ������ɾ��ʱ��,ɾ����
        /// <summary>
        /// ������ɾ��ʱ��,ɾ����
        /// ������ؿգ���ʾ�ü�¼�ѱ�ɾ��
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strDeactivedDate"></param>
        /// <param name="p_strDeactivedUserID"></param>
        /// <returns></returns>
        public long m_lngGetDeactivedDateAndUser(string p_strRegisterID, out DateTime p_dtmDeactivedDate, out string p_strDeactivedUserID)
        {
            p_dtmDeactivedDate = DateTime.MinValue;
            p_strDeactivedUserID = null;

            //clsInHospitalMainRecordServ_GX m_objServ =
            //    (clsInHospitalMainRecordServ_GX)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInHospitalMainRecordServ_GX));

            long lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetDeactivedDateAndUser(p_strRegisterID, out p_dtmDeactivedDate, out p_strDeactivedUserID);
            //m_objServ.Dispose();
            return lngRes;
        }
        #endregion

        #region ɾ����¼
        /// <summary>
        /// ɾ����¼
        /// </summary>
        /// <param name="p_strTableName"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_strOperatorID"></param>
        /// <returns></returns>
        public long m_lngDeleteRecord(long p_lngEMR_SEQ, string p_strOperatorID)
        {
            long lngRes = 0;
            if (p_strOperatorID == null || p_strOperatorID == "")
                return -1;

            //clsInHospitalMainRecordServ_GX m_objServ =
            //    (clsInHospitalMainRecordServ_GX)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInHospitalMainRecordServ_GX));

            lngRes = (new weCare.Proxy.ProxyEmr03()).Service.clsInHospitalMainRecordServ_GX_m_lngDeleteRecord(p_lngEMR_SEQ, p_strOperatorID);
            //m_objServ.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡͬ�����еĲ����շ���Ϣ
        /// <summary>
        /// ��ȡͬ�����еĲ����շ���Ϣ
        /// </summary>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_strInPatientDate">��Ժʱ��</param>
        /// <param name="p_objContent">�ݴ��շ���Ϣ</param>
        /// <returns></returns>
        public long m_lngGetCost(string p_strInPatientID,
            string p_strInPatientDate,
            out clsInHospitalMainRecord_GXContent p_objContent)
        {
            //clsInHospitalMainRecordServ_GX m_objServ =
            //    (clsInHospitalMainRecordServ_GX)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInHospitalMainRecordServ_GX));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetCost(p_strInPatientID, p_strInPatientDate, out p_objContent);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }
        #endregion

        #region ��ȡͬ�����еĲ���������Ϣ
        /// <summary>
        /// ��ȡͬ�����еĲ����շ���Ϣ
        /// </summary>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_strInPatientDate">��Ժʱ��</param>
        /// <param name="p_objContent">�ݴ��շ���Ϣ</param>
        /// <returns></returns>
        public long m_lngGetOpInfo(string p_strInPatientID,
            string p_strInPatientDate,
            out DataTable p_dtbOpResult)
        {
            //clsInHospitalMainRecordServ_GX m_objServ =
            //    (clsInHospitalMainRecordServ_GX)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInHospitalMainRecordServ_GX));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetOpInfo(p_strInPatientID, p_strInPatientDate, out p_dtbOpResult);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }
        #endregion

        #region ��ѯ��һ�δ�ӡʱ��
        /// <summary>
        /// ��ѯ��һ�δ�ӡʱ��
        /// </summary>
        /// <param name="p_strRegisterID"></param>
        /// <param name="p_strFirstPrintDate"></param>
        /// <returns></returns>		
        public long m_strGetFirstPrintDate(string p_strRegisterID, out string p_strFirstPrintDate)
        {
            p_strFirstPrintDate = "";
            DateTime dtmFPD = DateTime.MinValue;

            //clsInHospitalMainRecordServ_GX objServ =
            //    (clsInHospitalMainRecordServ_GX)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInHospitalMainRecordServ_GX));

            long lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetFirstPrintDate(p_strRegisterID, out dtmFPD);
            if (dtmFPD != DateTime.MinValue && dtmFPD != new DateTime(1900, 1, 1))
                p_strFirstPrintDate = dtmFPD.ToString("yyyy-MM-dd HH:mm:ss");
            //objServ.Dispose();
            return lngRes;
        }
        #endregion

        #region ���µ�ǰ���˵���סԺ���״δ�ӡʱ��
        /// <summary>
        /// ���µ�ǰ���˵���סԺ���״δ�ӡʱ��
        /// </summary>
        /// <param name="p_strRegisterID"></param>
        /// <returns></returns>		
        public long m_lngUpdateFirstPrintDate(string p_strRegisterID)
        {//���µ�һ�δ�ӡʱ��

            //clsInHospitalMainRecordServ_GX m_objServ =
            //    (clsInHospitalMainRecordServ_GX)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInHospitalMainRecordServ_GX));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr03()).Service.clsInHospitalMainRecordServ_GX_m_lngUpdateFirstPrintDate(p_strRegisterID);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }
        #endregion

        #region ��ȡ��ѯ���Ĳ�����Ϣ
        /// <summary>
        /// ��ȡ��ѯ���Ĳ�����Ϣ
        /// </summary>
        /// <param name="p_strPatientID"></param>
        /// <param name="p_objPatient"></param>
        public void m_mthInitPatientInfo(string p_strPatientID, string p_strPatientName, out clsPatient p_objPatient)
        {
            p_objPatient = null;
            if (p_strPatientID == null)
                return;

            //clsPatientManagerService objServ =
            //    (clsPatientManagerService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPatientManagerService));

            string strEMRID = null;
            long m_lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetEMRIDByHISID(p_strPatientID, out strEMRID);
            if (strEMRID != null)
            {
                clsPeopleInfo objPeo = new clsPeopleInfo();
                objPeo.m_StrLastName = p_strPatientName;
                p_objPatient = new clsPatient(strEMRID, p_strPatientID.Trim(), objPeo);
            }
        }
        #endregion

        #region ��ȡ����ICD10����Ϣ
        /// <summary>
        /// ��ȡ����ICD10����Ϣ
        /// </summary>
        /// <param name="P_dtbICD"></param>
        /// <returns></returns>		
        public long m_lngGetICDFrom8i(out DataTable P_dtbICD)
        {
            P_dtbICD = null;

            //clsQuery8iServ objServ =
            //    (clsQuery8iServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsQuery8iServ));

            long lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetICDFrom8i(out P_dtbICD);
            //objServ.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡ����Ա��ID������
        /// <summary>
        /// ��ȡ����Ա��ID������
        /// </summary>
        /// <param name="P_dtbEmp"></param>
        /// <returns></returns>		
        public long m_lngGetAllEmp(out DataTable P_dtbEmp)
        {
            P_dtbEmp = null;

            //clsEMR_EmployeeManagerService objServ =
            //    (clsEMR_EmployeeManagerService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_EmployeeManagerService));

            long lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetAllEmp(out P_dtbEmp);
            //objServ.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡ����Ա��ID������
        /// <summary>
        /// ��ȡ����Ա��ID������
        /// </summary>
        /// <param name="P_dtbEmp"></param>
        /// <returns></returns>		
        public long m_lngGetCurrentDeptEmp(string p_strDeptID, out DataTable P_dtbEmp)
        {
            P_dtbEmp = null;

            //clsEMR_EmployeeManagerService objServ =
            //    (clsEMR_EmployeeManagerService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_EmployeeManagerService));

            long lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetEmployeeByDeptID( p_strDeptID, out P_dtbEmp);
            return lngRes;
        }
        #endregion

        #region ������ҳ��ѯ

        /// <summary>
        /// ������ҳ��ѯ
        /// </summary>
        /// <param name="p_strDeptId">����ID</param>
        /// <param name="p_strPatantId">����ID</param>
        /// <param name="p_dtbResult">���ز�ѯ���</param>
        /// <returns></returns>
        public long m_lngHospitalQuery(string p_strDeptId, string p_strPatantId, out DataTable p_dtbResult)
        {
            p_dtbResult = null;

            //clsPatientManagerService objServ =
            //    (clsPatientManagerService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPatientManagerService));

            long lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngHospitalQuery(p_strDeptId, p_strPatantId, out p_dtbResult);
            return lngRes;
        }

        #endregion


        #region ��ȡ������Ժ�ǼǺ�
        /// <summary>
        /// ��ȡ������Ժ�ǼǺ�
        /// </summary>
        /// <param name="p_strPatientID">����ID</param>
        /// <param name="p_strInDate">HIS��Ժ����</param>
        /// <param name="p_strRegisterID">��Ժ�ǼǺ�</param>
        /// <returns></returns>
        public long m_lngGetRegisterIDByPatient(string p_strPatientID, string p_strInDate, out string p_strRegisterID)
        {
            string strRegisterID = "";
            long lngRes = 0;

            //clsPatientManagerService objServ =
            //    (clsPatientManagerService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPatientManagerService));

            lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetRegisterIDByPatient(p_strPatientID, p_strInDate, out p_strRegisterID);
            return lngRes;
        }
        #endregion
    }
}
