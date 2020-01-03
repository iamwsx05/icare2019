using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;
using System.Data;

namespace iCare
{
    class clsCaseHistorySearchDomain
    {
        #region δ��Ŀ������ѯ

        #region ��ȡ���п���
        /// <summary>
        /// ��ȡ���п���
        /// </summary>
        /// <param name="p_objDeptArr">VO</param>
        /// <returns></returns>
        public long m_lngGetAllDept(out clsDept_Desc[] p_objDeptArr)
        {
            p_objDeptArr = null;

            //clsDepartmentManagerService objService =
            //    (clsDepartmentManagerService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsDepartmentManagerService));

            long lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetAllDept(out p_objDeptArr);
            return lngRes;
        }
        #endregion

        #region ��ѯһ��ʱ������δ��Ŀ�ĳ�Ժ����
        /// <summary>
        /// ��ѯһ��ʱ������δ��Ŀ�ĳ�Ժ����
        /// </summary>
        /// <param name="p_strOutDateBegin">��ѯ�ĳ�Ժ���ڿ�ʼʱ��</param>
        /// <param name="p_strOutDateEnd">��ѯ�ĳ�Ժ���ڽ���ʱ��</param>
        /// <param name="p_strDeptID">����ID(��Ϊ�����ѯ���п���)</param>
        /// <param name="p_dtpResult">��ѯ���</param>
        /// <returns></returns>
        public long m_lngGetUnlistOutPatient(string p_strOutDateBegin,
            string p_strOutDateEnd,
            string p_strDeptID,
            out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            //clsCaseHistorySearchService objService =
            //    (clsCaseHistorySearchService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsCaseHistorySearchService));

            long lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetUnlistOutPatient(p_strOutDateBegin, p_strOutDateEnd, p_strDeptID, out p_dtbResult);
            return lngRes;
        }
        #endregion         

        #region �����ύʱ�ޣ���ȡӦ���ύ��ʱ��
        /// <summary>
        /// �����ύʱ�ޣ���ȡӦ���ύ��ʱ��
        /// </summary>
        /// <param name="p_strRegisterID">��Ժ�ǼǺ�</param>
        /// <param name="p_strSetID">SetID</param>
        /// <param name="p_dtmMofifyDate">�޸�ʱ��</param>
        /// <returns></returns>
        public long m_lngGetAboveTimeModifyDate(string p_strRegisterID, string p_strSetID, out DateTime p_dtmMofifyDate)
        {
            p_dtmMofifyDate = DateTime.MinValue;
            if (p_strRegisterID == null || p_strRegisterID == string.Empty
                || p_strSetID == null || p_strSetID == string.Empty)
                return -1;
            long lngRes = 0;
            try
            {
                //clsPublicMiddleTier objMid =
                //    (clsPublicMiddleTier)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPublicMiddleTier));

                string strReturn = "";
                lngRes = (new weCare.Proxy.ProxyEmr()).Service.clsPublicMiddleTier_m_lngGetConfigBySettingID(p_strSetID, out strReturn);

                //clsPatientManagerService objServ =
                //    (clsPatientManagerService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPatientManagerService));

                DateTime dtOut = new DateTime(1900, 1, 1);
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetOutHospitalDate(p_strRegisterID, out dtOut);

                if (strReturn != "" && dtOut != new DateTime(1900, 1, 1))
                {
                    p_dtmMofifyDate = dtOut.AddHours(double.Parse(strReturn));
                }
            }
            catch
            {
                return -1;
            }
            return lngRes;
        }
        #endregion

        #endregion

        #region סԺʷ��ѯ

        #region ���ݲ���סԺ�Ż�ȡ���Ժ���
        /// <summary>
        /// ���ݲ���סԺ�Ż�ȡ���Ժ���
        /// </summary>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_dtbResult">��ѯ���</param>
        /// <returns></returns>
        public long m_lngGetInAndOutInfo(string p_strInPatientID, out DataTable p_dtbResult)
        {
            p_dtbResult = null;

            //clsCaseHistorySearchService objService =
            //    (clsCaseHistorySearchService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsCaseHistorySearchService));

            long lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetInAndOutInfo(p_strInPatientID, out p_dtbResult);
            return lngRes;
        }
        #endregion

        #region ��ȡ������ҳ��ϼ�������Ϣ
        /// <summary>
        /// ��ȡ������ҳ��ϼ�������Ϣ
        /// </summary>
        /// <param name="p_strRegisterID"></param>
        /// <param name="p_objCollection"></param>
        /// <returns></returns>
        public long lngGetDiagnoseAndOpInfo(string p_strRegisterID, out clsInHospitalMainRecord_GX_Collection p_objCollection)
        {
            p_objCollection = null;

            //clsCaseHistorySearchService objService =
            //    (clsCaseHistorySearchService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsCaseHistorySearchService));

            long lngRes = (new weCare.Proxy.ProxyEmr03()).Service.lngGetDiagnoseAndOpInfo(p_strRegisterID, out p_objCollection);
            return lngRes;
        }
        #endregion 

        #endregion

        #region ��Ժ���������ѯ
        /// <summary>
        /// ��ѯ��Ժ�������
        /// </summary>
        /// <param name="p_intDiagnoseResult">���ƽ��(��Ժ��ʽ)</param>
        /// <param name="p_strDeptID">����ID(Ϊ��ʱ��ѯ���п���)</param>
        /// <param name="p_dtmOutDateBegin">��ѯ��Ժʱ�俪ʼ</param>
        /// <param name="p_dtmOutDateEnd">��ѯ��Ժʱ�����</param>
        /// <param name="p_dtbResult">��ѯ���</param>
        /// <returns></returns>
        public long m_lngGetOutInfo(int p_intDiagnoseResult,
            string p_strDeptID,
            DateTime p_dtmOutDateBegin,
            DateTime p_dtmOutDateEnd,
            out DataTable p_dtbResult)
        {
            p_dtbResult = null;

            //clsCaseHistorySearchService objService =
            //    (clsCaseHistorySearchService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsCaseHistorySearchService));

            long lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetOutInfo(p_intDiagnoseResult, p_strDeptID, p_dtmOutDateBegin, p_dtmOutDateEnd, out p_dtbResult);
            return lngRes;
        }
        #endregion

        #region ��Ժ���˵ǼǱ�
        /// <summary>
        /// ��Ժ���˵ǼǱ�
        /// </summary>
        /// <param name="p_strDeptID">����ID(Ϊ��ʱ��ѯ���п���)</param>
        /// <param name="p_dtmOutDate">��ѯ��Ժʱ��</param>
        /// <param name="p_dtbResult">��ѯ���</param>
        /// <returns></returns>
        public long m_lngGetOutAndNoCatalogPatient(string p_strDeptID,
            DateTime p_dtmOutDate,
            out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            //clsCaseHistorySearchService objService =
            //    (clsCaseHistorySearchService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsCaseHistorySearchService));

            long lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetOutAndNoCatalogPatient(p_strDeptID, p_dtmOutDate, out p_dtbResult);
            return lngRes;
        }
        #endregion

        #region ������ͳ��
        /// <summary>
        /// ������ͳ��
        /// </summary>
        /// <param name="p_blnIsFirst">�Ƿ��һ���</param>
        /// <param name="p_strDeptID">����ID(Ϊ��ʱ��ѯ���п���)</param>
        /// <param name="p_dtmOutDateBegin">��ѯ��Ժʱ�俪ʼ</param>
        /// <param name="p_dtmOutDateEnd">��ѯ��Ժʱ�����</param>
        /// <param name="p_dtbResult">��ѯ���</param>
        /// <returns></returns>
        public long m_lngGetStatDiagnose(bool p_blnIsFirst,
            string p_strDeptID,
            DateTime p_dtmOutDateBegin,
            DateTime p_dtmOutDateEnd,
            out DataTable p_dtbResult)
        {
            p_dtbResult = null;

            //clsCaseHistorySearchService objService =
            //    (clsCaseHistorySearchService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsCaseHistorySearchService));

            long lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngStatDiagnose(p_blnIsFirst, p_strDeptID, p_dtmOutDateBegin, p_dtmOutDateEnd, out p_dtbResult);
            return lngRes;
        }
        #endregion

        #region ������ͳ��
        /// <summary>
        /// ������ͳ��
        /// </summary>
        /// <param name="p_strDeptID">����ID(Ϊ��ʱ��ѯ���п���)</param>
        /// <param name="p_dtmOutDateBegin">��ѯ��Ժʱ�俪ʼ</param>
        /// <param name="p_dtmOutDateEnd">��ѯ��Ժʱ�����</param>
        /// <param name="p_dtbResult">��ѯ���</param>
        /// <returns></returns>
        public long m_lngGetStatOperation(string p_strDeptID,
            DateTime p_dtmOutDateBegin,
            DateTime p_dtmOutDateEnd,
            out DataTable p_dtbResult)
        {
            p_dtbResult = null;

            //clsCaseHistorySearchService objService =
            //    (clsCaseHistorySearchService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsCaseHistorySearchService));

            long lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngStatOperation(p_strDeptID, p_dtmOutDateBegin, p_dtmOutDateEnd, out p_dtbResult);
            return lngRes;
        }
        #endregion

        #region ��Ŀ������ͳ��

        #region ��Ŀ������
        /// <summary>
        /// ��Ŀ������
        /// </summary>
        /// <param name="p_dtmBegin">��ѯ��ʼʱ��</param>
        /// <param name="p_dtmEnd">��ѯ����ʱ��</param>
        /// <param name="p_strNum">��ѯ���</param>
        /// <returns></returns>
        public long m_lngGetCatalogCaseNum(DateTime p_dtmBegin, DateTime p_dtmEnd, out string p_strNum)
        {
            p_strNum = "0";

            //clsCaseHistorySearchService objService =
            //    (clsCaseHistorySearchService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsCaseHistorySearchService));

            long lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetCatalogCaseNum(p_dtmBegin, p_dtmEnd, out p_strNum);
            return lngRes;
        }
        #endregion

        #region VIP������
        /// <summary>
        /// VIP������
        /// </summary>
        /// <param name="p_dtmBegin">��ѯ��ʼʱ��</param>
        /// <param name="p_dtmEnd">��ѯ����ʱ��</param>
        /// <param name="p_strNum">��ѯ���</param>
        /// <returns></returns>
        public long m_lngGetVipPatientNum(DateTime p_dtmBegin, DateTime p_dtmEnd, out string p_strNum)
        {
            p_strNum = "0";

            //clsQuery8iServ objService =
            //    (clsQuery8iServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsQuery8iServ));

            long lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetVipPatientNum(p_dtmBegin, p_dtmEnd, out p_strNum);
            return lngRes;
        }
        #endregion

        #region ����������
        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="p_dtmBegin">��ѯ��ʼʱ��</param>
        /// <param name="p_dtmEnd">��ѯ����ʱ��</param>
        /// <param name="p_strNum">��ѯ���</param>
        /// <returns></returns>
        public long m_lngGetDeadCaseNum(DateTime p_dtmBegin, DateTime p_dtmEnd, out string p_strNum)
        {
            p_strNum = "0";

            //clsCaseHistorySearchService objService =
            //    (clsCaseHistorySearchService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsCaseHistorySearchService));

            long lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetDeadCaseNum(p_dtmBegin, p_dtmEnd, out p_strNum);
            return lngRes;
        }
        #endregion

        #region ��Ŀ�������
        /// <summary>
        /// ��Ŀ�������
        /// </summary>
        /// <param name="p_dtmBegin">��ѯ��ʼʱ��</param>
        /// <param name="p_dtmEnd">��ѯ����ʱ��</param>
        /// <param name="p_strNum">��ѯ���</param>
        /// <returns></returns>
        public long m_lngCatalogDiagDict(DateTime p_dtmBegin, DateTime p_dtmEnd, out string p_strNum)
        {
            p_strNum = "0";

            //clsQuery8iServ objService =
            //    (clsQuery8iServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsQuery8iServ));

            long lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngCatalogDiagDict(p_dtmBegin, p_dtmEnd, out p_strNum);
            return lngRes;
        }
        #endregion

        #region ��Ŀ��������
        /// <summary>
        /// ��Ŀ��������
        /// </summary>
        /// <param name="p_dtmBegin">��ѯ��ʼʱ��</param>
        /// <param name="p_dtmEnd">��ѯ����ʱ��</param>
        /// <param name="p_strNum">��ѯ���</param>
        /// <returns></returns>
        public long m_lngGetCatalogOpNum(DateTime p_dtmBegin, DateTime p_dtmEnd, out string p_strNum)
        {
            p_strNum = "0";

            //clsCaseHistorySearchService objService =
            //    (clsCaseHistorySearchService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsCaseHistorySearchService));

            long lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetCatalogOpNum(p_dtmBegin, p_dtmEnd, out p_strNum);
            return lngRes;
        }
        #endregion

        #region �½�����������
        /// <summary>
        /// �½�����������
        /// </summary>
        /// <param name="p_dtmBegin">��ѯ��ʼʱ��</param>
        /// <param name="p_dtmEnd">��ѯ����ʱ��</param>
        /// <param name="p_strNum">��ѯ���</param>
        /// <returns></returns>
        public long m_lngNewOpDict(DateTime p_dtmBegin, DateTime p_dtmEnd, out string p_strNum)
        {
            p_strNum = "0";

            //clsQuery8iServ objService =
            //    (clsQuery8iServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsQuery8iServ));

            long lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngNewOpDict(p_dtmBegin, p_dtmEnd, out p_strNum);
            return lngRes;
        }
        #endregion

        #region �½�����������
        /// <summary>
        /// �½�����������
        /// </summary>
        /// <param name="p_dtmBegin">��ѯ��ʼʱ��</param>
        /// <param name="p_dtmEnd">��ѯ����ʱ��</param>
        /// <param name="p_strNum">��ѯ���</param>
        /// <returns></returns>
        public long m_lngNewDiagDict(DateTime p_dtmBegin, DateTime p_dtmEnd, out string p_strNum)
        {
            p_strNum = "0";

            //clsQuery8iServ objService =
            //    (clsQuery8iServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsQuery8iServ));

            long lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngNewDiagDict(p_dtmBegin, p_dtmEnd, out p_strNum);
            return lngRes;
        }
        #endregion

        #region ��Ŀ����������
        /// <summary>
        /// ��Ŀ����������
        /// </summary>
        /// <param name="p_dtmBegin">��ѯ��ʼʱ��</param>
        /// <param name="p_dtmEnd">��ѯ����ʱ��</param>
        /// <param name="p_strNum">��ѯ���</param>
        /// <returns></returns>
        public long m_lngGetCatalogOpTypeNum(DateTime p_dtmBegin, DateTime p_dtmEnd, out string p_strNum)
        {
            p_strNum = "0";

            //clsCaseHistorySearchService objService =
            //    (clsCaseHistorySearchService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsCaseHistorySearchService));

            long lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetCatalogOpTypeNum(p_dtmBegin, p_dtmEnd, out p_strNum);
            return lngRes;
        }
        #endregion

        #region ��Ŀ���������
        /// <summary>
        /// ��Ŀ���������
        /// </summary>
        /// <param name="p_dtmBegin">��ѯ��ʼʱ��</param>
        /// <param name="p_dtmEnd">��ѯ����ʱ��</param>
        /// <param name="p_strNum">��ѯ���</param>
        /// <returns></returns>
        public long m_lngCatalogDiagTypeDict(DateTime p_dtmBegin, DateTime p_dtmEnd, out string p_strNum)
        {
            p_strNum = "0";

            //clsQuery8iServ objService =
            //    (clsQuery8iServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsQuery8iServ));

            long lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngCatalogDiagTypeDict(p_dtmBegin, p_dtmEnd, out p_strNum);
            return lngRes;
        }
        #endregion

        #region �ض��������������(��V�룬E�룬M��)
        /// <summary>
        /// �ض��������������(��V�룬E�룬M��)
        /// </summary>
        /// <param name="p_dtmBegin">��ѯ��ʼʱ��</param>
        /// <param name="p_dtmEnd">��ѯ����ʱ��</param>
        /// <param name="p_strDiagType">��������</param>
        /// <param name="p_strNum">��ѯ���</param>
        /// <returns></returns>
        public long m_lngCatalogSpecifyDiagTypeDict(DateTime p_dtmBegin, DateTime p_dtmEnd, string p_strDiagType, out string p_strNum)
        {
            p_strNum = "0";

            //clsQuery8iServ objService =
            //    (clsQuery8iServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsQuery8iServ));

            long lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngCatalogSpecifyDiagTypeDict(p_dtmBegin, p_dtmEnd, p_strDiagType, out p_strNum);
            return lngRes;
        }
        #endregion

        #endregion
    }
}
