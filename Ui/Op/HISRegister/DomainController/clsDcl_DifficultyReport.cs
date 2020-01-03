using System;
using weCare.Core.Entity;
using System.Data;
using System.Collections;
namespace com.digitalwave.iCare.gui.HIS

{
    /// <summary>
    /// clsDcl_DifficultyReport ��ժҪ˵����
    /// </summary>
    public class clsDcl_DifficultyReport : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDcl_DifficultyReport()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region GetProxy 
        /// <summary>
        /// GetProxy
        /// </summary>
        weCare.Proxy.ProxyOP proxy
        {
            get
            {
                return new weCare.Proxy.ProxyOP();
            }
        }
        #endregion

        #region ��ȡ����

        public long m_mthGetManiReportData(DateTime date, DateTime date2, out System.Data.DataTable dt, out System.Data.DataTable dt2)
        {

            dt = null;
            dt2 = null;
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsDifficultyReportSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDifficultyReportSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDifficultyReportSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_mthGetManiReportData( date, date2, out dt, out dt2);
           // objSvc.Dispose();
            return lngRes;


        }
        #endregion

        #region ��ȡ���ݣ������±���

        public long m_mthGetAllDataOfMonth(DateTime date, DateTime date2, out System.Data.DataTable dt, out System.Data.DataTable dt1, out System.Data.DataTable dt2)
        {

            dt = null;
            dt2 = null;
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsDifficultyReportOfMonthSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDifficultyReportOfMonthSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDifficultyReportOfMonthSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_mthGetAllDataOfMonth( date, date2, out dt, out dt1, out dt2);
           // objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ��ȡԱ����������
        public long m_mthGetDepartmentByID(string strEmpID, out DataTable p_dt)
        {
            p_dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));
            long lngRes = proxy.Service.m_mthGetDepartmentByID( strEmpID, out p_dt);
           // objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ��ȡԱ����������
        public long m_mthGetDepartment(out DataTable p_dt)
        {
            p_dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));
            long lngRes = proxy.Service.m_mthGetDepartment( out p_dt);
           // objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ���ݲ���ID����ҽ��
        public long m_mthGetDocByDepID(string ID, out DataTable p_dt)
        {
            p_dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));
            long lngRes = proxy.Service.m_mthGetDocByDepID( ID, out p_dt);
           // objSvc.Dispose();
            return lngRes;

        }
        #endregion
        #region ���ҵ���ͳ����Ϣ
        public long m_mthGetSingleWorkLoad(string strID, DateTime strBeginDate, DateTime strEndDate, int flag, out clsSingleWorkLoadSubItem_VO[] objSubArr)
        {
            objSubArr = null;
            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));
            long lngRes = proxy.Service.m_mthGetSingleWorkLoad(strID, strBeginDate, strEndDate, flag, out objSubArr);
           // objSvc.Dispose();
            return lngRes;

        }
        #endregion
        #region ����Ա��ID�����ڻ�ȡ�������͸�����
        /// <summary>
        /// ����Ա��ID�����ڻ�ȡ�������͸�����
        /// </summary>
        /// <param name="m_strID"></param>
        /// <param name="m_strBeginDate"></param>
        /// <param name="m_strEndDate"></param>
        /// <param name="m_objTable"></param>
        /// <returns></returns>
        public long m_lngGetRecipeCountByIDAndDate(string m_strID, DateTime m_strBeginDate, DateTime m_strEndDate, out DataTable m_objTable)
        {
            long lngRes = -1;
            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));
            lngRes = proxy.Service.m_lngGetRecipeCountByIDAndDate( m_strID, m_strBeginDate, m_strEndDate, out m_objTable);
           // objSvc.Dispose();
            return lngRes;

        }
        #endregion

        #region ͳ���շ�Ա����������
        /// <summary>
        /// ͳ���շ�Ա����������
        /// </summary>
        /// <param name="strBeginDate"></param>
        /// <param name="strEndDate"></param>
        /// <param name="objSubArr"></param>
        /// <returns></returns>
        public long m_mthGetCheckManWorkLoad(DateTime strBeginDate, DateTime strEndDate, out clsChargeWork_VO[] objSubArr)
        {
            objSubArr = null;
            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));
            long lngRes = proxy.Service.m_mthGetCheckManWorkLoad(strBeginDate, strEndDate, out objSubArr);
           // objSvc.Dispose();
            return lngRes;

        }
        #endregion

        #region ͳ���շ�Ա����������
        /// <summary>
        /// ͳ���շ�Ա����������
        /// </summary>
        /// <param name="strBeginDate"></param>
        /// <param name="strEndDate"></param>
        /// <param name="objSubArr"></param>
        /// <returns></returns>
        public long m_mthGetCheckManWorkLoad(DateTime strBeginDate, DateTime strEndDate, out DataTable dt)
        {
            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));

            long lngRes = proxy.Service.m_mthGetCheckManWorkLoad(strBeginDate, strEndDate, out dt);
           // objSvc.Dispose();
            return lngRes;

        }
        #endregion

        #region ͳ���շ�Ա������ͳ�Ʊ���Ʊ��(���������飬����շ�Աͬ����׼����ʱ��������һ���Ժ���Ҫͬһ����) @@@@@
        /// <summary>
        /// ͳ���շ�Ա������ͳ�Ʊ���Ʊ��(���������飬����շ�Աͬ����׼����ʱ��������һ���Ժ���Ҫͬһ����) @@@@@
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetCheckinvoicenums(string BeginDate, string EndDate, out DataTable dt)
        {
            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));
            long lngRes = proxy.Service.m_lngGetCheckinvoicenums(BeginDate, EndDate, out dt);
           // objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���ҵ���ͳ����ϢNEW
        public long m_mthGetSingleWorkLoad_New(string strID, DateTime strBeginDate, DateTime strEndDate, int flag, out clsSingleWorkLoadSubItem_VO[] objSubArr, string p_identityId)
        {
            objSubArr = null;
            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));
            long lngRes = proxy.Service.m_mthGetSingleWorkLoad_New(strID, strBeginDate, strEndDate, flag, out objSubArr, p_identityId);
           // objSvc.Dispose();
            return lngRes;

        }
        #endregion

        #region ��ȡ��Ĺ���������
        public long m_mthGetGroupWorkLoad(string strGroupID, DateTime strBeginDate, DateTime strEndDate, int flag, int intflag, out clsSingleWorkLoadSubItem_VO[] objSubArr)
        {
            objSubArr = null;
            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));
            long lngRes = proxy.Service.m_mthGetGroupWorkLoad(strGroupID, strBeginDate, strEndDate, flag, intflag, out objSubArr);
           // objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ��ȡ�������ļ�¼��
        public long m_mthGetCount(out DataTable dt)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));
            long lngRes = proxy.Service.m_mthGetCount(out dt);
           // objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���ݽ���ʱ��ͳ��������������¼��
        /// <summary>
        /// ���ݽ���ʱ��ͳ��������������¼��
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_mthGetCount(string BeginDate, string EndDate, int intflag, out DataTable dt)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));
            long lngRes = proxy.Service.m_mthGetCount(BeginDate, EndDate, intflag, out dt);
           // objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���ݽ���ʱ��ͳ��רҵ�飭>ҽ����������
        /// <summary>
        /// ���ݽ���ʱ��ͳ��רҵ�飭>ҽ����������
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetSeeDoctorPersonNums(string BeginDate, string EndDate, int intflag, out DataTable dt)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));
            long lngRes = proxy.Service.m_lngGetSeeDoctorPersonNums(BeginDate, EndDate, intflag, out dt);
           // objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡ�����ֶε���
        public long m_mthReportColumns(out DataTable dt, string strEx)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));
            long lngRes = proxy.Service.m_mthReportColumns(out dt, strEx);
           // objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ��������������ҩ���
        public long m_mthGetUsingMedicine(int Flag, out DataTable dt, string strID, DateTime date, DateTime date2, string strEx)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));
            long lngRes = proxy.Service.m_mthGetUsingMedicine(Flag, out dt, strID, date, date2, strEx);
           // objSvc.Dispose();
            return lngRes;
        }
        #endregion
    }
}
