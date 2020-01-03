using System;
using weCare.Core.Entity;
using System.Data;
namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// ��ȡ��ӡ������Դ��
    /// </summary>

    public class clsDomainConrol_Print : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDomainConrol_Print()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        public long m_lngGetPrintSource(string RegisterID, out System.Data.DataTable dtbSource)
        {
            dtbSource = new System.Data.DataTable();
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetCurRegisterByID(RegisterID, out dtbSource);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #region �Һ�Ա�������� �����ʣ�
        /// <summary>
        /// �Һ�Ա��������
        /// </summary>
        /// <param name="dtTolSource">������Ч����</param>
        /// <param name="date">��������</param>
        /// <param name="strempno">������ID</param>
        /// <param name="dtRestoreDetail">�����˺�����</param>
        /// <returns></returns>
        public long m_lngEndReport(out DataTable dtTolSource, string date, string strempno, out DataTable dtRestoreDetail)
        {
            dtTolSource = new System.Data.DataTable();
            dtRestoreDetail = new System.Data.DataTable();
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngEndReport(out dtTolSource, date, strempno, out dtRestoreDetail);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region �Һ�Ա�������� ����ʷ��
        /// <summary>
        /// �Һ�Ա��������
        /// </summary>
        /// <param name="dtTolSource">������Ч����</param>
        /// <param name="date">��������</param>
        /// <param name="strempno">������ID</param>
        /// <param name="dtRestoreDetail">�����˺�����</param>
        /// <returns></returns>
        public long m_lngHistoryReport(out DataTable dtTolSource, string date, string strempno, out DataTable dtRestoreDetail)
        {
            dtTolSource = new System.Data.DataTable();
            dtRestoreDetail = new System.Data.DataTable();
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngHistoryReport(out dtTolSource, date, strempno, out dtRestoreDetail);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion


        #region ��ʷ��ѯ
        public long m_lngGetHistory(string startDate, string endDate, string checkMan, out DataTable dt)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetHistorRegister(startDate, endDate, checkMan, out dt);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }
        #endregion

        public long m_lngGetCheckOutSource(string RegisterID, out System.Data.DataTable dtbSource, string date, out System.Data.DataTable dtbSourcedetail, out string strregno)
        {
            dtbSource = new System.Data.DataTable();
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetCheckOutRep(out dtbSource, date, out dtbSourcedetail, out strregno);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        public long m_lngGetCheckMan(out DataTable dtCheckMan)
        {
            dtCheckMan = new System.Data.DataTable();
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetCheckMan(out dtCheckMan);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        public long m_lngGetCheckOutSourceP(string RegisterID, out System.Data.DataTable dtbSource, string date, string regemp, out System.Data.DataTable dtbSourcedetail, out string strregno)
        {
            dtbSource = new System.Data.DataTable();
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetCheckOutRegP(out dtbSource, date, regemp, out dtbSourcedetail, out strregno);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        //�Һ��ս�(��,ͣ��)
        public long m_lngGetCheckOutReg(string checkoutdate, string checkoutempid, DataTable dtTolSource, DataTable dtRestoreDetail1)
        {
            //dtbSource = new System.Data.DataTable();
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngCheckOut(checkoutdate, checkoutempid, dtTolSource, dtRestoreDetail1);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        #region �Һ��ս�(��)
        /// <summary>
        /// �ҺŽ���(��)
        /// </summary>
        /// <param name="OperID">�տ�ԱID</param>
        /// <param name="CheckDate">����ʱ��</param>
        /// <returns></returns>
        public long m_lngGetCheckOutReg(string OperID, out string CheckDate)
        {
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //                                                    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));

            long l = (new weCare.Proxy.ProxyOP02()).Service.m_lngCheckOut(OperID, out CheckDate);
            //objSvc.Dispose();
            //objSvc = null;
            return l;
        }
        #endregion

        #region ���Һ�Ա�ڵ����Ƿ�����
        /// <summary>
		/// ���Һ�Ա�ڵ����Ƿ�����
		/// </summary>
		/// <param name="checkoutdate"></param>
		/// <param name="checkoutempid"></param>
		/// <returns></returns>
		public long m_lngCheckEnd(string checkoutempid, string checkoutdate)
        {
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngCheckEnd(checkoutempid, checkoutdate);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        #endregion

        #region ��ʷ���� �Ź��� 2004-9-9
        /// <summary>
        /// ��ʷ���� �Ź��� 2004-9-9
        /// </summary>
        /// <param name="empID"></param>
        /// <param name="p_fromtDate"></param>
        /// <param name="p_toDate"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngQulHistory(string empID, string p_fromtDate, string p_toDate, out clscheckoutreg_VO[] p_objResultArr)
        {
            //dtbSource = new System.Data.DataTable();
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngQulHistory(empID, p_fromtDate, p_toDate, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion



        public long m_lngGetCheckOutHis(out System.Data.DataTable dtbSource, string CHECKOUTDATE, string CHECKOUTREGID, out System.Data.DataTable dtbSourcedetail)
        {
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetCheckOutH(out dtbSource, CHECKOUTDATE, CHECKOUTREGID, out dtbSourcedetail);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        #region ����Һű���  �Ź���  2004-9-10
        /// <summary>
        /// ����Һű���  �Ź���  2004-9-10
        /// </summary>
        /// <param name="p_strFirstDate"></param>
        /// <param name="p_strLastDate"></param>
        /// <param name="p_strempno"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngFindByDateReport(string p_strFirstDate, string p_strLastDate, out System.Data.DataTable p_tabReport)
        {
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngFindByDateReport(p_strFirstDate, p_strLastDate, out p_tabReport);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ����Һű���2  �Ź���  2004-9-10
        /// <summary>
        /// ����Һű���  �Ź���  2004-9-10
        /// </summary>
        /// <param name="p_strFirstDate"></param>
        /// <param name="p_strLastDate"></param>
        /// <param name="p_strempno"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngFindByDateReport2(string p_strFirstDate, string p_strLastDate, out System.Data.DataTable p_tabReport)
        {
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngFindByDateReport2(p_strFirstDate, p_strLastDate, out p_tabReport);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ��������˴��ձ��� 2005-03-02
        /// <summary>
        /// ��������˴��ձ��� 2005-03-02
        /// </summary>
        /// <param name="p_strFirstDate"></param>
        /// <param name="p_strLastDate"></param>
        /// <param name="p_tabReport"></param>
        /// <returns></returns>
        public long m_lngDepIncomerpt(string p_strFirstDate, string p_strLastDate, out System.Data.DataTable p_tabReport, out DataTable p_depdt)
        {
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngDepIncomerpt(p_strFirstDate, p_strLastDate, out p_tabReport, out p_depdt);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region �����м���������������ݱ�������ҹҺ�ͳ��ͼ��
        /// <summary>
        /// �����м���������������ݱ�������ҹҺ�ͳ��ͼ��
        /// </summary>
        /// <param name="m_dtpStartDate"></param>
        /// <param name="m_dtpEndDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_GetRegReportPicture(string m_dtpStartDate, string m_dtpEndDate, out System.Data.DataTable dt)
        {
            long lngRes = 0;
            //  dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));

            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetRegReportPicture(m_dtpStartDate, m_dtpEndDate, out dt);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;

        }
        #endregion �����м���������������ݱ�

        #region �����м���������������ݱ�(����ҽ���Һ�ͳ��ͼ)
        /// <summary>
        /// �����м���������������ݱ�(����ҽ���Һ�ͳ��ͼ)
        /// </summary>
        /// <param name="m_dtpStartDate"></param>
        /// <param name="m_dtpEndDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_GetRegReportDoctPicture(string m_dtpStartDate, string m_dtpEndDate, out System.Data.DataTable dt)
        {
            long lngRes = 0;
            //  dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));

            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetRegReportDoctPicture(m_dtpStartDate, m_dtpEndDate, out dt);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;

        }
        #endregion �����м���������������ݱ�

        #region ��ȡָ���Һ�Ա�ķ�Ʊ��Ϣ(δ�ᡢ�ѽ�)
        /// <summary>
        /// ��ȡָ���Һ�Ա�ķ�Ʊ��Ϣ(δ�ᡢ�ѽ�)
        /// </summary>
        /// <param name="EmpID"></param>
        /// <param name="BalDate"></param>
        /// <param name="Flag">0 δ�� 1 �ѽ�</param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetRegisterInvoInfo(string EmpID, string BalDate, int Flag, out DataTable dt)
        {
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //                                                       (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));

            long l = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetRegisterInvoInfo(EmpID, BalDate, Flag, out dt);
            //objSvc.Dispose();
            //objSvc = null;

            return l;
        }
        #endregion

    }
}
