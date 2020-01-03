using System;
using System.Data;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    public class clsDcl_InvoiceManage : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region ���캯��
        public clsDcl_InvoiceManage()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #endregion

        #region GetProxy 
        /// <summary>
        /// GetProxy
        /// </summary>
        weCare.Proxy.ProxyOP01 proxy
        {
            get
            {
                return new weCare.Proxy.ProxyOP01();
            }
        }
        #endregion 

        // ���﷢Ʊ����
        #region ��ӷ�Ʊ����		����		2004-8-23
        /// <summary>
        /// ��ӷ�Ʊ����
        /// </summary>
        /// <param name="p_objRecord">[������ص��ֶβ������]</param>
        /// <param name="p_strRecordID">��Ʊ������ˮ��</param>
        /// <returns>���ز����Ƿ�ɹ�����[С�ڵ��ڣ������ɹ������ڣ����ɹ�]</returns>
        public long m_lngDoAddNewT_opr_opinvoiceman(clsT_opr_opinvoiceman_VO p_objRecord, out string p_strRecordID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc));
            lngRes = proxy.Service.m_lngDoAddNewT_opr_opinvoiceman(p_objRecord, out p_strRecordID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���Ϸ�Ʊ		����		2004-8-23
        /// <summary>
        /// ���Ϸ�Ʊ
        /// </summary>
        /// <param name="p_objRecord">[ֻ��Ҫm_strAPPID_CHR��m_strCANCELUSERID_CHR]</param>
        /// <returns>���ز����Ƿ�ɹ�����[С�ڵ��ڣ������ɹ������ڣ����ɹ�]</returns>
        public long m_lngModifyT_opr_opinvoiceman(clsT_opr_opinvoiceman_VO p_objRecord)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc));
            lngRes = proxy.Service.m_lngModifyT_opr_opinvoiceman(p_objRecord);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ѯ���췢Ʊ		����		2004-8-23
        /// <summary>
        /// ��ѯ����ķ�Ʊ
        /// </summary>
        /// <param name="p_strStartapply_dat">��ѯ��������ʼ����ʱ��</param>
        /// <param name="p_strEndapply_dat">��ѯ��������������ʱ��</param>
        /// <param name="p_strAppid_chr">��ѯ����������</param>
        /// <param name="p_objResultArr"></param>
        /// <returns>���ز����Ƿ�ɹ�����[С�ڵ��ڣ������ɹ������ڣ����ɹ�]</returns>
        public long m_lngGetApplyInvoice(string p_strStartapply_dat, string p_strEndapply_dat, string p_strAppuserid_chr, int p_typeid, out clsT_opr_opinvoiceman_VO[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc));
            lngRes = proxy.Service.m_lngGetApplyInvoice(p_strStartapply_dat, p_strEndapply_dat, p_strAppuserid_chr, p_typeid, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        /// <summary>
        /// ���ݷ�Ʊ������ˮ�Ų��Ҷ�Ӧ��¼��Ϣ
        /// </summary>
        /// <param name="p_strAppid_chr">��Ʊ������ˮ��</param>
        /// <param name="p_objResult"></param>
        /// <returns>���ز����Ƿ�ɹ�����[С�ڵ��ڣ������ɹ������ڣ����ɹ�]</returns>
        public long m_lngGetApplyInvoice(string p_strAppid_chr, out clsT_opr_opinvoiceman_VO p_objResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc));
            lngRes = proxy.Service.m_lngGetApplyInvoice(p_strAppid_chr, out p_objResult);
            //objSvc.Dispose();
            return lngRes;
        }
        /// <summary>
        /// ��ѯ����ķ�Ʊ
        /// </summary>
        /// <param name="p_strQueryCondition">��ѯ����</param>
        /// <param name="p_objResultArr"></param>
        /// <returns>���ز����Ƿ�ɹ�����[С�ڵ��ڣ������ɹ������ڣ����ɹ�]</returns>
        public long m_lngGetApplyInvoice(string p_strQueryCondition, out clsT_opr_opinvoiceman_VO[] p_objResultArr)
        {
            long lngRes = 0;
            //ȷ��Sql����ѯ���ֺϷ�
            p_strQueryCondition = " 1=1 AND " + p_strQueryCondition;
            //com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc));
            lngRes = proxy.Service.m_lngGetApplyInvoice(p_strQueryCondition, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡԱ����ˮ��	-����ְ����
        /// <summary>
        /// ��ȡԱ����ˮ��	-����ְ����
        /// </summary>
        /// <param name="p_strEmployeeNO">ְ����</param>
        /// <param name="p_strEmployeeID">ְ����ˮ�� [out����]</param>
        /// <returns></returns>
        public long m_lngGetEmployeeIDByNO(string p_strEmployeeNO, out string p_strEmployeeID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc));
            lngRes = proxy.Service.m_lngGetEmployeeIDByNO(p_strEmployeeNO, out p_strEmployeeID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ���ݹ��Ų�����		����		2004-8-23
        /// <summary>
        /// ���ݹ������Ա������
        /// </summary>
        /// <param name="p_strNO">����</param>
        /// <param name="p_strName">���ơ�[out������]</param>
        /// <returns>���ز����Ƿ�ɹ�����[С�ڵ��ڣ������ɹ������ڣ����ɹ�]</returns>
        public long m_lngGetEmployeeNameByNO(string p_strNO, out string p_strName)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc));
            lngRes = proxy.Service.m_lngGetApplyName(p_strNO, out p_strName);
            //objSvc.Dispose();
            return lngRes;
        }
        /// <summary>
        /// ���ݹ������Ա������
        /// </summary>
        /// <param name="p_strNO">����</param>
        /// <param name="p_dtResult">�������š�����2���ֶεı�[Appuserid_chr��AppuserName_chr]��[out ����]</param>
        /// <returns>���ز����Ƿ�ɹ�����[С�ڵ��ڣ������ɹ������ڣ����ɹ�]</returns>
        public long m_lngGetEmployeeNameByNO(string p_strNO, out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = null;
            //com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc));
            lngRes = proxy.Service.m_lngGetApplyName(p_strNO, out p_dtResult);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ��ȡְ������ -������ˮ��
        /// <summary>
        /// ������ˮ�����Ա������
        /// </summary>
        /// <param name="p_strID">��ˮ��</param>
        /// <param name="p_strEmployeeName">ְ�����ơ�[out ����]</param>
        /// <returns>���ز����Ƿ�ɹ�����[С�ڵ��ڣ������ɹ������ڣ����ɹ�]</returns>
        public long m_lngGetEmployeeNameByID(string p_strID, out string p_strEmployeeName)
        {
            long lngRes = 0;
            p_strEmployeeName = "";
            //com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc));
            lngRes = proxy.Service.m_lngGetEmployeeNameByID(p_strID, out p_strEmployeeName);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region	��鷢Ʊ�����Ƿ��Ѿ�������		����		2004-8-23	[ע�⣺�Ѿ����ϵķ�Ʊ������������]
        /// <summary>
        /// ��鷢Ʊ�����Ƿ��Ѿ�������
        /// </summary>
        /// <param name="p_strMinInvoiceNo">��ʼ��Ʊ��</param>
        /// <param name="p_strMaxInvoiceNo">������Ʊ��</param>
        /// <param name="p_typeid">Ʊ������ 0-��ͨ 1-����Ʊ��</param>
        /// <param name="IsUsed">�Ƿ��õı�־ [out ����]</param>
        /// <returns>���ز����Ƿ�ɹ�����[С�ڵ��ڣ������ɹ������ڣ����ɹ�]</returns>
        /// <remarks>
        /// ע�⣺
        ///		�������������Ĭ�����Ѿ�ռ�ã�
        ///		�� IsUsed = true
        /// </remarks>
        public long m_lngCheckInvoiceNOIsUsed(string p_strMinInvoiceNo, string p_strMaxInvoiceNo, int p_typeid, out bool p_blnIsUsed)
        {
            p_blnIsUsed = true;
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc));
            lngRes = proxy.Service.m_lngCheckInvoiceNOIsUsed(p_strMinInvoiceNo, p_strMaxInvoiceNo, p_typeid, out p_blnIsUsed);
            return lngRes;
        }
        #endregion

        #region ����Ӧ��Ʊ������ˮ���Ƿ�����
        /// <summary>
        /// ����Ӧ��Ʊ������ˮ���Ƿ�����
        /// </summary>
        /// <param name="p_strAppid_chr">��Ʊ������ˮ��</param>
        /// <param name="p_blnIsUsed">�Ƿ����� [out ����]</param>
        /// <returns>���ز����Ƿ�ɹ�����[С�ڵ��ڣ������ɹ������ڣ����ɹ�]</returns>
        public long m_lngCheckInvoiceNOIsCancel(string p_strAppid_chr, out bool p_blnIsUsed)
        {
            p_blnIsUsed = true;
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc));
            lngRes = proxy.Service.m_lngCheckInvoiceNOIsCancel(p_strAppid_chr, out p_blnIsUsed);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        //���﷢Ʊ�˻�
        #region ��÷�Ʊ��Ϣ		����		2004-8-27
        /// <summary>
        /// ���ݷ�Ʊ�Ż�����ﴦ����Ʊ��Ϣ [������Ч�ķ�Ʊ ��Ʊ״̬��1-��Ч��0-���ϡ�2-��Ʊ]
        /// </summary>
        /// <param name="p_strINVOICENO_VCHR">��Ʊ��</param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        public long m_lngGetInfoByNoForReturn(string p_strINVOICENO_VCHR, out clsT_opr_outpatientrecipeinv_VO p_objResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc));
            lngRes = proxy.Service.m_lngGetInfoByNoForReturn(p_strINVOICENO_VCHR, out p_objResult);
            //objSvc.Dispose();
            return lngRes;
        }
        /// <summary>
        /// ��������Ż�÷�Ʊ��Ϣ [������Ч�ķ�Ʊ ��Ʊ״̬��1-��Ч��0-���ϡ�2-��Ʊ]
        /// </summary>
        /// <param name="p_NO_STR">����� [�����λ]</param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        public long m_lngGetInfoBySeqidForReturn(string p_NO_STR, out clsT_opr_outpatientrecipeinv_VO p_objResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc));
            lngRes = proxy.Service.m_lngGetInfoBySeqidForReturn(p_NO_STR, out p_objResult);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ��Ʊ�˻�		����		2004-8-27
        /// <summary>
        /// ��Ʊ�˻�[��Ʊ]
        /// </summary>
        /// <param name="p_strINVOICENO_VCHR">��Ʊ��</param>
        /// <param name="p_strOPREMP_CHR">������ID</param>
        /// <returns></returns>
        public long m_lngReturnTicket(string p_strINVOICENO_VCHR, string p_strOPREMP_CHR, ref string Seqid, int intFlag)
        {
            long lngRes = 0;
            try
            {
                //com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc objSvc =
                //    (com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc));
                lngRes = proxy.Service.m_lngReturnTicket(p_strINVOICENO_VCHR, p_strOPREMP_CHR, ref Seqid, intFlag);
                //objSvc.Dispose();
            }
            catch
            {

            }
            return lngRes;
        }
        #endregion

        #region ��Ʊ�˻ؼ���Ƿ��Ѿ���ҩ				
        /// <summary>
        /// ��Ʊ�˻ؼ���Ƿ��Ѿ���ҩ	
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strINVOICENO_VCHR">��Ʊ��</param>
        /// <returns></returns>
        public long m_lngReturnTicketCheckOutSendMed(string p_strINVOICENO_VCHR, out string p_strStatus)
        {
            p_strStatus = string.Empty;
            long lngRes = 0;
            try
            {
                //com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc objSvc =
                //    (com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc));
                lngRes = proxy.Service.m_lngReturnTicketCheckOutSendMed(p_strINVOICENO_VCHR, out p_strStatus);
                //objSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLog = new com.digitalwave.Utility.clsLogText();
                objLog.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        //���﷢Ʊ�ָ�
        #region ��÷�Ʊ��Ϣ		����		2004-8-27
        /// <summary>
        /// ���ݷ�Ʊ�Ż�����ﴦ����Ʊ��Ϣ [�Ѿ���Ʊ�ķ�Ʊ ��Ʊ״̬��1-��Ч��0-���ϡ�2-��Ʊ]
        /// </summary>
        /// <param name="p_strINVOICENO_VCHR">��Ʊ��</param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        public long m_lngGetInfoByNoForResume(string p_strINVOICENO_VCHR, out clsT_opr_outpatientrecipeinv_VO p_objResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc));
            lngRes = proxy.Service.m_lngGetInfoByNoForResume(p_strINVOICENO_VCHR, out p_objResult);
            //objSvc.Dispose();
            return lngRes;
        }
        /// <summary>
        /// ��������Ż�÷�Ʊ��Ϣ [�Ѿ���Ʊ�ķ�Ʊ ��Ʊ״̬��1-��Ч��0-���ϡ�2-��Ʊ]
        /// </summary>
        /// <param name="p_NO_STR">����� [�����λ]</param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        public long m_lngGetInfoBySeqidForResume(string p_NO_STR, out clsT_opr_outpatientrecipeinv_VO p_objResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc));
            lngRes = proxy.Service.m_lngGetInfoBySeqidForResume(p_NO_STR, out p_objResult);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ��Ʊ�ָ�		����		2004-8-27
        /// <summary>
        /// ��Ʊ�˻�[��Ʊ]
        /// </summary>
        /// <param name="p_strINVOICENO_VCHR">��Ʊ��</param>
        /// <param name="p_strOPREMP_CHR">������ID</param>
        /// <returns></returns>
        public long m_lngResumeTicket(string p_strINVOICENO_VCHR, string p_strOPREMP_CHR, ref string Seqid)
        {

            long lngRes = 0;
            try
            {
                //com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc objSvc =
                //    (com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc));
                lngRes = proxy.Service.m_lngResumeTicket(p_strINVOICENO_VCHR, p_strOPREMP_CHR, ref Seqid);
                //objSvc.Dispose();
            }
            catch
            {

            }
            return lngRes;
        }
        #endregion

        #region ���ݿ��Ų����Ʊ��
        public long m_mthFindInvoiceByCardID(string strCardID, out DataTable dt, int flag, int p_intFlag)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc));
            lngRes = proxy.Service.m_mthFindInvoiceByCardID(strCardID, out dt, flag, p_intFlag);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ��ȡ�����Ϣ
        public long m_mthGetInvoiceAuditingInfo(string strID, out DataTable dt, int flag)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc));
            lngRes = proxy.Service.m_mthGetInvoiceAuditingInfo(strID, out dt, flag);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ������Ʊ�Ƿ���ҩƷ
        public long m_CheckIsContainMed(string p_strInvNo, ref bool p_blContians)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc));
            lngRes = proxy.Service.m_CheckIsContainMed(p_strInvNo, ref p_blContians);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ���������Ϣ

        public long m_mthAddInvoiceAuditingInfo(clsInvAuditing_VO objResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc));
            lngRes = proxy.Service.m_mthAddInvoiceAuditingInfo(objResult);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ��֤����
        public long m_mthGetEmployeeInfo(string strID, out DataTable dt, string strEx)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc));
            lngRes = proxy.Service.m_mthGetEmployeeInfo(strID, out dt, strEx);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �ж��Ƿ��Ʊ
        /// <summary>
        /// �ж��Ƿ��Ʊ
        /// </summary>
        /// <param name="seqid"></param>
        /// <returns></returns>
        public bool m_blnChecksplit(string invono)
        {
            //com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc));

            bool ret = proxy.Service.m_blnChecksplit(invono);
            //objSvc.Dispose();
            return ret;
        }
        #endregion

        #region �����ڲ����кŻ�ȡͬ��ַ�Ʊ����
        /// <summary>
        /// �����ڲ����кŻ�ȡͬ��ַ�Ʊ����
        /// </summary>
        /// <param name="seqid"></param>
        /// <param name="dtRecord"></param>
        /// <returns></returns>
        public long m_lngGetsplitinvoinfo(string seqid, out DataTable dtRecord)
        {
            //com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc objSvc =
            //                (com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc));

            long ret = proxy.Service.m_lngGetsplitinvoinfo(seqid, out dtRecord);
            //objSvc.Dispose();
            return ret;
        }
        #endregion

        #region ��ȡ�����Ϣ
        internal long m_mthFindInvoiceByInvoNo(string strCardID, out string p_strCreateEmpID, out string invoNo)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc));
            lngRes = proxy.Service.m_mthFindInvoiceByInvoNo(strCardID, out p_strCreateEmpID, out invoNo);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ��ȡ��Ʊ������Ϣ
        /// <summary>
        /// ��ȡ��Ʊ������Ϣ
        /// </summary>
        /// <param name="invono"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngChargeItemTypeByInvoice(string invono, out DataTable dt)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsCalPatientChargeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsCalPatientChargeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsCalPatientChargeSvc));

            lngRes = (new weCare.Proxy.ProxyOP()).Service.m_lngChargeItemTypeByInvoice(invono, out dt);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡ��Ʊ������Ϣ
        /// <summary>
        /// ��ȡ��Ʊ������Ϣ
        /// </summary>
        /// <param name="m_strflag"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngComputationByScope(string m_strflag, out DataTable dt)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsCalPatientChargeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsCalPatientChargeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsCalPatientChargeSvc));

            lngRes = (new weCare.Proxy.ProxyOP()).Service.m_lngComputationByScope(m_strflag, out dt);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
    }
}
