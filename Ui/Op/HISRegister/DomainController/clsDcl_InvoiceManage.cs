using System;
using System.Data;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    public class clsDcl_InvoiceManage : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region 构造函数
        public clsDcl_InvoiceManage()
        {
            //
            // TODO: 在此处添加构造函数逻辑
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

        // 门诊发票管理
        #region 添加发票请领		徐斌辉		2004-8-23
        /// <summary>
        /// 添加发票请领
        /// </summary>
        /// <param name="p_objRecord">[作废相关的字段不必添加]</param>
        /// <param name="p_strRecordID">发票请求流水号</param>
        /// <returns>返回操作是否成功　　[小于等于０：不成功、大于０：成功]</returns>
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

        #region 作废发票		徐斌辉		2004-8-23
        /// <summary>
        /// 作废发票
        /// </summary>
        /// <param name="p_objRecord">[只需要m_strAPPID_CHR、m_strCANCELUSERID_CHR]</param>
        /// <returns>返回操作是否成功　　[小于等于０：不成功、大于０：成功]</returns>
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

        #region 查询请领发票		徐斌辉		2004-8-23
        /// <summary>
        /// 查询请领的发票
        /// </summary>
        /// <param name="p_strStartapply_dat">查询条件：起始申请时间</param>
        /// <param name="p_strEndapply_dat">查询条件：结束申请时间</param>
        /// <param name="p_strAppid_chr">查询条件：工号</param>
        /// <param name="p_objResultArr"></param>
        /// <returns>返回操作是否成功　　[小于等于０：不成功、大于０：成功]</returns>
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
        /// 根据发票请求流水号查找对应记录信息
        /// </summary>
        /// <param name="p_strAppid_chr">发票请求流水号</param>
        /// <param name="p_objResult"></param>
        /// <returns>返回操作是否成功　　[小于等于０：不成功、大于０：成功]</returns>
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
        /// 查询请领的发票
        /// </summary>
        /// <param name="p_strQueryCondition">查询条件</param>
        /// <param name="p_objResultArr"></param>
        /// <returns>返回操作是否成功　　[小于等于０：不成功、大于０：成功]</returns>
        public long m_lngGetApplyInvoice(string p_strQueryCondition, out clsT_opr_opinvoiceman_VO[] p_objResultArr)
        {
            long lngRes = 0;
            //确保Sql语句查询部分合法
            p_strQueryCondition = " 1=1 AND " + p_strQueryCondition;
            //com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInvoiceManageSvc));
            lngRes = proxy.Service.m_lngGetApplyInvoice(p_strQueryCondition, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 获取员工流水号	-根据职工号
        /// <summary>
        /// 获取员工流水号	-根据职工号
        /// </summary>
        /// <param name="p_strEmployeeNO">职工号</param>
        /// <param name="p_strEmployeeID">职工流水号 [out参数]</param>
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
        #region 根据工号查名称		徐斌辉		2004-8-23
        /// <summary>
        /// 根据工号求得员工名称
        /// </summary>
        /// <param name="p_strNO">工号</param>
        /// <param name="p_strName">名称　[out　参数]</param>
        /// <returns>返回操作是否成功　　[小于等于０：不成功、大于０：成功]</returns>
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
        /// 根据工号求得员工名称
        /// </summary>
        /// <param name="p_strNO">工号</param>
        /// <param name="p_dtResult">包含工号、名称2个字段的表[Appuserid_chr、AppuserName_chr]　[out 参数]</param>
        /// <returns>返回操作是否成功　　[小于等于０：不成功、大于０：成功]</returns>
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
        #region 获取职工名称 -根据流水号
        /// <summary>
        /// 根据流水号求得员工名称
        /// </summary>
        /// <param name="p_strID">流水号</param>
        /// <param name="p_strEmployeeName">职工名称　[out 参数]</param>
        /// <returns>返回操作是否成功　　[小于等于０：不成功、大于０：成功]</returns>
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

        #region	检查发票区间是否已经被申请		徐斌辉		2004-8-23	[注意：已经作废的发票可以重新申请]
        /// <summary>
        /// 检查发票区间是否已经被申请
        /// </summary>
        /// <param name="p_strMinInvoiceNo">起始发票号</param>
        /// <param name="p_strMaxInvoiceNo">结束发票号</param>
        /// <param name="p_typeid">票据类型 0-普通 1-行政票据</param>
        /// <param name="IsUsed">是否备用的标志 [out 参数]</param>
        /// <returns>返回操作是否成功　　[小于等于０：不成功、大于０：成功]</returns>
        /// <remarks>
        /// 注意：
        ///		如果操作错误，则默认是已经占用；
        ///		即 IsUsed = true
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

        #region 检查对应发票请求流水号是否被作废
        /// <summary>
        /// 检查对应发票请求流水号是否被作废
        /// </summary>
        /// <param name="p_strAppid_chr">发票请求流水号</param>
        /// <param name="p_blnIsUsed">是否被作废 [out 参数]</param>
        /// <returns>返回操作是否成功　　[小于等于０：不成功、大于０：成功]</returns>
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

        //门诊发票退回
        #region 获得发票信息		徐斌辉		2004-8-27
        /// <summary>
        /// 根据发票号获得门诊处方发票信息 [正常有效的发票 发票状态：1-有效、0-作废、2-退票]
        /// </summary>
        /// <param name="p_strINVOICENO_VCHR">发票号</param>
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
        /// 根据物理号获得发票信息 [正常有效的发票 发票状态：1-有效、0-作废、2-退票]
        /// </summary>
        /// <param name="p_NO_STR">物理号 [最大三位]</param>
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
        #region 发票退回		徐斌辉		2004-8-27
        /// <summary>
        /// 发票退回[退票]
        /// </summary>
        /// <param name="p_strINVOICENO_VCHR">发票号</param>
        /// <param name="p_strOPREMP_CHR">操作者ID</param>
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

        #region 发票退回检测是否已经配药				
        /// <summary>
        /// 发票退回检测是否已经配药	
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strINVOICENO_VCHR">发票号</param>
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

        //门诊发票恢复
        #region 获得发票信息		徐斌辉		2004-8-27
        /// <summary>
        /// 根据发票号获得门诊处方发票信息 [已经退票的发票 发票状态：1-有效、0-作废、2-退票]
        /// </summary>
        /// <param name="p_strINVOICENO_VCHR">发票号</param>
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
        /// 根据物理号获得发票信息 [已经退票的发票 发票状态：1-有效、0-作废、2-退票]
        /// </summary>
        /// <param name="p_NO_STR">物理号 [最大三位]</param>
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
        #region 发票恢复		徐斌辉		2004-8-27
        /// <summary>
        /// 发票退回[退票]
        /// </summary>
        /// <param name="p_strINVOICENO_VCHR">发票号</param>
        /// <param name="p_strOPREMP_CHR">操作者ID</param>
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

        #region 根据卡号查出发票号
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
        #region 获取审核信息
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
        #region 检索发票是否含有药品
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
        #region 增加审核信息

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
        #region 验证密码
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

        #region 判断是否分票
        /// <summary>
        /// 判断是否分票
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

        #region 根据内部序列号获取同组分发票数据
        /// <summary>
        /// 根据内部序列号获取同组分发票数据
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

        #region 获取审核信息
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

        #region 获取发票分类信息
        /// <summary>
        /// 获取发票分类信息
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

        #region 获取发票核算信息
        /// <summary>
        /// 获取发票核算信息
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
