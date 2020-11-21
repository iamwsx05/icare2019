using System;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.Collections;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// ��ҩ-��ҩ��
    /// ���ߣ� ����
    /// ����ʱ�䣺 2004-10-20
    /// </summary>
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsPutMedicineSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region ���캯��
        public clsPutMedicineSvc()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #endregion
        //��ҩ��
        #region ��ѯ
        #region ��ȡ��ҩ��	����ID
        /// <summary>
        /// ��ȡ��ҩ��	����ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">��ҩ��ID</param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPutMedReqByID(string p_strID, out clsT_Bih_Opr_PutMedReq_VO p_objResult)
        {
            p_objResult = new clsT_Bih_Opr_PutMedReq_VO();
            long lngRes = 0;
            string strSQL = @"
			SELECT a.*
				,(SELECT deptname_vchr FROM t_bse_deptdesc WHERE t_bse_deptdesc.deptid_chr=a.areaid_chr) AreaName
				,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.creator_chr) CreatorName
				,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.deactivator_chr) DeactivatorName
				,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.processor_chr) ProcessorName
				,DECODE(a.pstatus_int,0,'δ����',1,'�ѷ���',2,'���ַ�ҩ',3,'�ѷ�ҩ','')PStatusName
			FROM t_bih_opr_putmedreq a
			WHERE a.status_int=1 and Trim(a.putmedreqid_chr)='" + p_strID.Trim() + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResult = new clsT_Bih_Opr_PutMedReq_VO();
                    p_objResult.m_strPUTMEDREQID_CHR = dtbResult.Rows[0]["PUTMEDREQID_CHR"].ToString().Trim();
                    p_objResult.m_strAREAID_CHR = dtbResult.Rows[0]["AREAID_CHR"].ToString().Trim();
                    p_objResult.m_strDES_VCHR = dtbResult.Rows[0]["DES_VCHR"].ToString().Trim();
                    p_objResult.m_intPSTATUS_INT = Convert.ToInt32(dtbResult.Rows[0]["PSTATUS_INT"].ToString().Trim());
                    p_objResult.m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[0]["STATUS_INT"].ToString().Trim());
                    p_objResult.m_strDEACTIVATOR_CHR = dtbResult.Rows[0]["DEACTIVATOR_CHR"].ToString().Trim();
                    try
                    {
                        p_objResult.m_strDEACTIVATE_DAT = Convert.ToDateTime(dtbResult.Rows[0]["DEACTIVATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    }
                    catch { }
                    p_objResult.m_strCREATOR_CHR = dtbResult.Rows[0]["CREATOR_CHR"].ToString().Trim();
                    try
                    {
                        p_objResult.m_strCREATE_DAT = Convert.ToDateTime(dtbResult.Rows[0]["CREATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    }
                    catch { }
                    p_objResult.m_strRANGEDES_VCHR = dtbResult.Rows[0]["RANGEDES_VCHR"].ToString().Trim();
                    p_objResult.m_strPROCESSOR_CHR = dtbResult.Rows[0]["PROCESSOR_CHR"].ToString().Trim();
                    try
                    {
                        p_objResult.m_strFINISH_DAT = Convert.ToDateTime(dtbResult.Rows[0]["FINISH_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    }
                    catch { }
                    p_objResult.m_strBEDBEGIN_VCHR = dtbResult.Rows[0]["BEDBEGIN_VCHR"].ToString().Trim();
                    p_objResult.m_strBEDEND_VCHR = dtbResult.Rows[0]["BEDEND_VCHR"].ToString().Trim();
                    p_objResult.m_strBEDIDS_VCHR = dtbResult.Rows[0]["BEDBEGIN_VCHR"].ToString().Trim();
                    p_objResult.m_strBEDNAMES_VCHR = dtbResult.Rows[0]["BEDEND_VCHR"].ToString().Trim();
                    p_objResult.m_strEXECTYPE_VCHR = dtbResult.Rows[0]["EXECTYPE_VCHR"].ToString().Trim();
                    p_objResult.m_strEXECTYPENAME_VCHR = dtbResult.Rows[0]["EXECTYPENAME_VCHR"].ToString().Trim();
                    p_objResult.m_strRICHTYPE_VCHR = dtbResult.Rows[0]["RICHTYPE_VCHR"].ToString().Trim();
                    p_objResult.m_strRICHTYPENAME_VCHR = dtbResult.Rows[0]["RICHTYPENAME_VCHR"].ToString().Trim();
                    p_objResult.m_strUSAGETYPE_VCHR = dtbResult.Rows[0]["USAGETYPE_VCHR"].ToString().Trim();
                    p_objResult.m_strUSAGETYPENAME_VCHR = dtbResult.Rows[0]["USAGETYPENAME_VCHR"].ToString().Trim();
                    //���ֶ�
                    p_objResult.m_strAreaName = dtbResult.Rows[0]["AreaName"].ToString().Trim();
                    p_objResult.m_strCreatorName = dtbResult.Rows[0]["CreatorName"].ToString().Trim();
                    p_objResult.m_strDeactivatorName = dtbResult.Rows[0]["DeactivatorName"].ToString().Trim();
                    p_objResult.m_strProcessorName = dtbResult.Rows[0]["ProcessorName"].ToString().Trim();
                    p_objResult.m_strPStatusName = dtbResult.Rows[0]["PStatusName"].ToString().Trim();
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion


        #region ȡ�շ���Ŀ��Ӧ������
        /// <summary>
        /// ȡ�շ���Ŀ��Ӧ������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_ItemID"></param>
        /// <param name="strType"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedType(string p_ItemID, out string strType)
        {
            strType = "";
            long lngRes = 0;
            string strSQL = @"SELECT flaga_int
  FROM t_aid_medicinepreptype
 WHERE medicinepreptype_chr =
                             (SELECT medicinepreptype_chr
                                FROM t_bse_medicine
                               WHERE medicineid_chr = (SELECT itemsrcid_vchr
                                                         FROM t_bse_chargeitem
                                                        WHERE itemid_chr = '" + p_ItemID + "'))";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                if (dtbResult.Rows.Count > 0 && dtbResult.Rows[0] != null)
                {
                    strType = dtbResult.Rows[0]["flaga_int"].ToString();
                }
                else
                {
                    strType = "";
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region	��ȡ��ҩ��	����
        /// <summary>
        /// ��ȡ��ҩ��	���ݰ�ҩ��״̬[����]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="intPStatusArr">[����]����״̬	{0=δ����;1=�ѷ���;2=���ַ�ҩ;3=�ѷ�ҩ} {Ϊ�ջ򳤶�Ϊ0����Ϊ��ѯ����}</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPutMedReqArr(int[] intPStatusArr, out clsT_Bih_Opr_PutMedReq_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_Bih_Opr_PutMedReq_VO[0];
            long lngRes = 0;
            #region SQL
            string strSQL = @"
			SELECT a.*
				,(SELECT deptname_vchr FROM t_bse_deptdesc WHERE t_bse_deptdesc.deptid_chr=a.areaid_chr) AreaName
				,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.creator_chr) CreatorName
				,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.deactivator_chr) DeactivatorName
				,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.processor_chr) ProcessorName
				,DECODE(a.pstatus_int,0,'δ����',1,'�ѷ���',2,'���ַ�ҩ',3,'�ѷ�ҩ','')PStatusName
			FROM t_bih_opr_putmedreq a
			WHERE a.status_int=1 [PSTATUSCONDITION]
			ORDER BY a.pstatus_int,a.CREATE_DAT desc";
            string strCondition = "";
            if (intPStatusArr != null && intPStatusArr.Length > 0)
            {
                for (int i1 = 0; i1 < intPStatusArr.Length; i1++)
                {
                    if (strCondition.Trim() == "")
                        strCondition = intPStatusArr[i1].ToString().Trim();
                    else
                        strCondition += "," + intPStatusArr[i1].ToString().Trim();
                }
            }
            if (strCondition.Trim() != "")
            {
                strSQL = strSQL.Replace("[PSTATUSCONDITION]", " and pstatus_int in (" + strCondition.Trim() + ")");
            }
            else
            {
                strSQL = strSQL.Replace("[PSTATUSCONDITION]", "");
            }
            #endregion
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_Bih_Opr_PutMedReq_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        #region ������ֵ
                        p_objResultArr[i1] = new clsT_Bih_Opr_PutMedReq_VO();
                        p_objResultArr[i1].m_strPUTMEDREQID_CHR = dtbResult.Rows[i1]["PUTMEDREQID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strAREAID_CHR = dtbResult.Rows[i1]["AREAID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strDES_VCHR = dtbResult.Rows[i1]["DES_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_intPSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["PSTATUS_INT"].ToString().Trim());
                        p_objResultArr[i1].m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["STATUS_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strDEACTIVATOR_CHR = dtbResult.Rows[i1]["DEACTIVATOR_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_strDEACTIVATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["DEACTIVATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        catch { }
                        p_objResultArr[i1].m_strCREATOR_CHR = dtbResult.Rows[i1]["CREATOR_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_strCREATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CREATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        catch
                        { }
                        p_objResultArr[i1].m_strRANGEDES_VCHR = dtbResult.Rows[i1]["RANGEDES_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPROCESSOR_CHR = dtbResult.Rows[i1]["PROCESSOR_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_strFINISH_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["FINISH_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        catch { }
                        p_objResultArr[i1].m_strBEDBEGIN_VCHR = dtbResult.Rows[i1]["BEDBEGIN_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strBEDEND_VCHR = dtbResult.Rows[i1]["BEDEND_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strBEDIDS_VCHR = dtbResult.Rows[i1]["BEDBEGIN_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strBEDNAMES_VCHR = dtbResult.Rows[i1]["BEDEND_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strEXECTYPE_VCHR = dtbResult.Rows[i1]["EXECTYPE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strEXECTYPENAME_VCHR = dtbResult.Rows[i1]["EXECTYPENAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strRICHTYPE_VCHR = dtbResult.Rows[i1]["RICHTYPE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strRICHTYPENAME_VCHR = dtbResult.Rows[i1]["RICHTYPENAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUSAGETYPE_VCHR = dtbResult.Rows[i1]["USAGETYPE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUSAGETYPENAME_VCHR = dtbResult.Rows[i1]["USAGETYPENAME_VCHR"].ToString().Trim();
                        //���ֶ�
                        p_objResultArr[i1].m_strAreaName = dtbResult.Rows[i1]["AreaName"].ToString().Trim();
                        p_objResultArr[i1].m_strCreatorName = dtbResult.Rows[i1]["CreatorName"].ToString().Trim();
                        p_objResultArr[i1].m_strDeactivatorName = dtbResult.Rows[i1]["DeactivatorName"].ToString().Trim();
                        p_objResultArr[i1].m_strProcessorName = dtbResult.Rows[i1]["ProcessorName"].ToString().Trim();
                        p_objResultArr[i1].m_strPStatusName = dtbResult.Rows[i1]["PStatusName"].ToString().Trim();
                        #endregion
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        /// <summary>
        /// ��ȡ��ҩ��	���ݰ�ҩ��״̬[����]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID">����ID</param>
        /// <param name="p_strCreatorID">������ID</param>
        /// <param name="p_dtBegin">����ʱ��	[��ʼʱ��]</param>
        /// <param name="p_dtEnd">����ʱ��	[����ʱ��]</param>
        /// <param name="intPStatusArr">[����]����״̬	{0=δ����;1=�ѷ���;2=���ַ�ҩ;3=�ѷ�ҩ} {Ϊ�ջ򳤶�Ϊ0����Ϊ��ѯ����}</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPutMedReqArr(string p_strAreaID, string p_strCreatorID, System.DateTime p_dtBegin, System.DateTime p_dtEnd, int[] intPStatusArr, out clsT_Bih_Opr_PutMedReq_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_Bih_Opr_PutMedReq_VO[0];
            long lngRes = 0;
            #region SQL
            string strSQL = @"
			SELECT a.*
				,(SELECT deptname_vchr FROM t_bse_deptdesc WHERE t_bse_deptdesc.deptid_chr=a.areaid_chr) AreaName
				,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.creator_chr) CreatorName
				,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.deactivator_chr) DeactivatorName
				,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.processor_chr) ProcessorName
				,DECODE(a.pstatus_int,0,'δ����',1,'�ѷ���',2,'���ַ�ҩ',3,'�ѷ�ҩ','')PStatusName
			FROM t_bih_opr_putmedreq a
			WHERE a.status_int=1 [PSTATUSCONDITION] [AREACONDITION]	[CREATORCONDITION] [BEGINDATE] [ENDDATE]
			ORDER BY a.pstatus_int,a.CREATE_DAT desc";
            //[PSTATUSCONDITION]
            string strCondition = "";
            if (intPStatusArr != null && intPStatusArr.Length > 0)
            {
                for (int i1 = 0; i1 < intPStatusArr.Length; i1++)
                {
                    if (strCondition.Trim() == "")
                        strCondition = intPStatusArr[i1].ToString().Trim();
                    else
                        strCondition += "," + intPStatusArr[i1].ToString().Trim();
                }
            }
            if (strCondition.Trim() != "")
            {
                strSQL = strSQL.Replace("[PSTATUSCONDITION]", " and pstatus_int in (" + strCondition.Trim() + ")");
            }
            else
            {
                strSQL = strSQL.Replace("[PSTATUSCONDITION]", "");
            }
            //[AREACONDITION]
            if (p_strAreaID.Trim() == "")
            {
                strSQL = strSQL.Replace("[AREACONDITION]", "");
            }
            else
            {
                strSQL = strSQL.Replace("[AREACONDITION]", " and Trim(areaid_chr)='" + p_strAreaID.Trim() + "' ");
            }
            //[CREATORCONDITION]
            if (p_strCreatorID.Trim() == "")
            {
                strSQL = strSQL.Replace("[CREATORCONDITION]", "");
            }
            else
            {
                strSQL = strSQL.Replace("[CREATORCONDITION]", " and Trim(creator_chr)='" + p_strCreatorID.Trim() + "' ");
            }
            //[BEGINDATE]	
            string strTem = "";
            try
            {
                strTem = p_dtBegin.ToShortDateString();
            }
            catch { }
            if (strTem.Trim() == "")
            {
                strSQL = strSQL.Replace("[BEGINDATE]", "");
            }
            else
            {
                strTem += " 00:00:00";
                strSQL = strSQL.Replace("[BEGINDATE]", " and create_dat >= to_date('" + strTem + "','YYYY-MM-DD hh24:mi:ss')");
            }
            //[ENDDATE]
            strTem = "";
            try
            {
                strTem = p_dtEnd.ToShortDateString();
            }
            catch { }
            if (strTem.Trim() == "")
            {
                strSQL = strSQL.Replace("[ENDDATE]", "");
            }
            else
            {
                strTem += " 23:59:59";
                strSQL = strSQL.Replace("[ENDDATE]", " and create_dat <= to_date('" + strTem + "','YYYY-MM-DD hh24:mi:ss')");
            }
            #endregion
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_Bih_Opr_PutMedReq_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        #region ������ֵ
                        p_objResultArr[i1] = new clsT_Bih_Opr_PutMedReq_VO();
                        p_objResultArr[i1].m_strPUTMEDREQID_CHR = dtbResult.Rows[i1]["PUTMEDREQID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strAREAID_CHR = dtbResult.Rows[i1]["AREAID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strDES_VCHR = dtbResult.Rows[i1]["DES_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_intPSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["PSTATUS_INT"].ToString().Trim());
                        p_objResultArr[i1].m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["STATUS_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strDEACTIVATOR_CHR = dtbResult.Rows[i1]["DEACTIVATOR_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_strDEACTIVATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["DEACTIVATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        catch { }
                        p_objResultArr[i1].m_strCREATOR_CHR = dtbResult.Rows[i1]["CREATOR_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_strCREATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CREATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        catch
                        { }
                        p_objResultArr[i1].m_strRANGEDES_VCHR = dtbResult.Rows[i1]["RANGEDES_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPROCESSOR_CHR = dtbResult.Rows[i1]["PROCESSOR_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_strFINISH_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["FINISH_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        catch { }
                        p_objResultArr[i1].m_strBEDBEGIN_VCHR = dtbResult.Rows[i1]["BEDBEGIN_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strBEDEND_VCHR = dtbResult.Rows[i1]["BEDEND_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strBEDIDS_VCHR = dtbResult.Rows[i1]["BEDBEGIN_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strBEDNAMES_VCHR = dtbResult.Rows[i1]["BEDEND_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strEXECTYPE_VCHR = dtbResult.Rows[i1]["EXECTYPE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strEXECTYPENAME_VCHR = dtbResult.Rows[i1]["EXECTYPENAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strRICHTYPE_VCHR = dtbResult.Rows[i1]["RICHTYPE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strRICHTYPENAME_VCHR = dtbResult.Rows[i1]["RICHTYPENAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUSAGETYPE_VCHR = dtbResult.Rows[i1]["USAGETYPE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUSAGETYPENAME_VCHR = dtbResult.Rows[i1]["USAGETYPENAME_VCHR"].ToString().Trim();
                        //���ֶ�
                        p_objResultArr[i1].m_strAreaName = dtbResult.Rows[i1]["AreaName"].ToString().Trim();
                        p_objResultArr[i1].m_strCreatorName = dtbResult.Rows[i1]["CreatorName"].ToString().Trim();
                        p_objResultArr[i1].m_strDeactivatorName = dtbResult.Rows[i1]["DeactivatorName"].ToString().Trim();
                        p_objResultArr[i1].m_strProcessorName = dtbResult.Rows[i1]["ProcessorName"].ToString().Trim();
                        p_objResultArr[i1].m_strPStatusName = dtbResult.Rows[i1]["PStatusName"].ToString().Trim();
                        #endregion
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        /// <summary>
        /// ��ȡ��ҩ��	���ݰ�ҩ��״̬[����]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaName">��������[ģ����ѯ]</param>
        /// <param name="p_strCreator">����������[ģ����ѯ]</param>
        /// <param name="p_strBeginDate">��������	[��ʼ����]	{Ϊ�ջ��Ǳ�׼�����ͣ�����Ϊ��ѯ����}</param>
        /// <param name="p_strEndDate">��������	[��������]	{Ϊ�ջ��Ǳ�׼�����ͣ�����Ϊ��ѯ����}</param>
        /// <param name="intPStatusArr">[����]����״̬	{0=δ����;1=�ѷ���;2=���ַ�ҩ;3=�ѷ�ҩ} {Ϊ�ջ򳤶�Ϊ0����Ϊ��ѯ����}</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPutMedReqArr(string p_strAreaName, string p_strCreator, string p_strBeginDate, string p_strEndDate, int[] intPStatusArr, out clsT_Bih_Opr_PutMedReq_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_Bih_Opr_PutMedReq_VO[0];
            long lngRes = 0;
            #region SQL
            string strSQL = @"
			SELECT a.*
				,(SELECT deptname_vchr FROM t_bse_deptdesc WHERE t_bse_deptdesc.deptid_chr=a.areaid_chr) AreaName
				,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.creator_chr) CreatorName
				,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.deactivator_chr) DeactivatorName
				,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.processor_chr) ProcessorName
				,DECODE(a.pstatus_int,0,'δ����',1,'�ѷ���',2,'���ַ�ҩ',3,'�ѷ�ҩ','')PStatusName
			FROM t_bih_opr_putmedreq a
			WHERE a.status_int=1 [PSTATUSCONDITION] [BEGINDATE] [ENDDATE]
			ORDER BY a.pstatus_int,a.CREATE_DAT desc";

            strSQL = @"SELECT b.*  FROM (" + strSQL + ") b WHERE 1=1 [AREACONDITION]	[CREATORCONDITION]";
            //[PSTATUSCONDITION]
            string strCondition = "";
            if (intPStatusArr != null && intPStatusArr.Length > 0)
            {
                for (int i1 = 0; i1 < intPStatusArr.Length; i1++)
                {
                    if (strCondition.Trim() == "")
                        strCondition = intPStatusArr[i1].ToString().Trim();
                    else
                        strCondition += "," + intPStatusArr[i1].ToString().Trim();
                }
            }
            if (strCondition.Trim() != "")
            {
                strSQL = strSQL.Replace("[PSTATUSCONDITION]", " and pstatus_int in (" + strCondition.Trim() + ")");
            }
            else
            {
                strSQL = strSQL.Replace("[PSTATUSCONDITION]", "");
            }
            //[AREACONDITION]
            if (p_strAreaName.Trim() == "")
            {
                strSQL = strSQL.Replace("[AREACONDITION]", "");
            }
            else
            {
                strSQL = strSQL.Replace("[AREACONDITION]", " and Trim(AreaName) like '%" + p_strAreaName.Trim() + "%' ");
            }
            //[CREATORCONDITION]
            if (p_strCreator.Trim() == "")
            {
                strSQL = strSQL.Replace("[CREATORCONDITION]", "");
            }
            else
            {
                strSQL = strSQL.Replace("[CREATORCONDITION]", " and Trim(CreatorName) like '%" + p_strCreator.Trim() + "%' ");
            }
            //[BEGINDATE]	
            if (p_strBeginDate.Trim() == "")
            {
                strSQL = strSQL.Replace("[BEGINDATE]", "");
            }
            else
            {
                try { DateTime dt = Convert.ToDateTime(p_strBeginDate); }
                catch { strSQL = strSQL.Replace("[BEGINDATE]", ""); }
                strSQL = strSQL.Replace("[BEGINDATE]", " AND TRUNC(create_dat) >= TO_DATE('" + p_strBeginDate + "','YYYY-MM-DD')");
            }
            //[ENDDATE]
            if (p_strEndDate.Trim() == "")
            {
                strSQL = strSQL.Replace("[ENDDATE]", "");
            }
            else
            {
                try { DateTime dt1 = Convert.ToDateTime(p_strBeginDate); }
                catch { strSQL = strSQL.Replace("[ENDDATE]", ""); }
                strSQL = strSQL.Replace("[ENDDATE]", " AND TRUNC(create_dat) <= TO_DATE('" + p_strBeginDate + "','YYYY-MM-DD')");
            }
            #endregion
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_Bih_Opr_PutMedReq_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        #region ������ֵ
                        p_objResultArr[i1] = new clsT_Bih_Opr_PutMedReq_VO();
                        p_objResultArr[i1].m_strPUTMEDREQID_CHR = dtbResult.Rows[i1]["PUTMEDREQID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strAREAID_CHR = dtbResult.Rows[i1]["AREAID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strDES_VCHR = dtbResult.Rows[i1]["DES_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_intPSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["PSTATUS_INT"].ToString().Trim());
                        p_objResultArr[i1].m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["STATUS_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strDEACTIVATOR_CHR = dtbResult.Rows[i1]["DEACTIVATOR_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_strDEACTIVATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["DEACTIVATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        catch { }
                        p_objResultArr[i1].m_strCREATOR_CHR = dtbResult.Rows[i1]["CREATOR_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_strCREATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CREATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        catch
                        { }
                        p_objResultArr[i1].m_strRANGEDES_VCHR = dtbResult.Rows[i1]["RANGEDES_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPROCESSOR_CHR = dtbResult.Rows[i1]["PROCESSOR_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_strFINISH_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["FINISH_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        catch { }
                        p_objResultArr[i1].m_strBEDBEGIN_VCHR = dtbResult.Rows[i1]["BEDBEGIN_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strBEDEND_VCHR = dtbResult.Rows[i1]["BEDEND_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strBEDIDS_VCHR = dtbResult.Rows[i1]["BEDBEGIN_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strBEDNAMES_VCHR = dtbResult.Rows[i1]["BEDEND_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strEXECTYPE_VCHR = dtbResult.Rows[i1]["EXECTYPE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strEXECTYPENAME_VCHR = dtbResult.Rows[i1]["EXECTYPENAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strRICHTYPE_VCHR = dtbResult.Rows[i1]["RICHTYPE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strRICHTYPENAME_VCHR = dtbResult.Rows[i1]["RICHTYPENAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUSAGETYPE_VCHR = dtbResult.Rows[i1]["USAGETYPE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUSAGETYPENAME_VCHR = dtbResult.Rows[i1]["USAGETYPENAME_VCHR"].ToString().Trim();
                        //���ֶ�
                        p_objResultArr[i1].m_strAreaName = dtbResult.Rows[i1]["AreaName"].ToString().Trim();
                        p_objResultArr[i1].m_strCreatorName = dtbResult.Rows[i1]["CreatorName"].ToString().Trim();
                        p_objResultArr[i1].m_strDeactivatorName = dtbResult.Rows[i1]["DeactivatorName"].ToString().Trim();
                        p_objResultArr[i1].m_strProcessorName = dtbResult.Rows[i1]["ProcessorName"].ToString().Trim();
                        p_objResultArr[i1].m_strPStatusName = dtbResult.Rows[i1]["PStatusName"].ToString().Trim();
                        #endregion
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
        #endregion
        #region	����
        /// <summary>
        /// ���Ӱ�ҩ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID">��ˮ��	[out ����]</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewPutMedReq(out string p_strRecordID, clsT_Bih_Opr_PutMedReq_VO p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = objHRPSvc.lngGenerateID(12, "PUTMEDREQID_CHR", "T_Bih_Opr_PutMedReq", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "INSERT INTO T_Bih_Opr_PutMedReq (PUTMEDREQID_CHR,AREAID_CHR,DES_VCHR,PSTATUS_INT,STATUS_INT,CREATOR_CHR,CREATE_DAT,RANGEDES_VCHR,BEDBEGIN_VCHR,BEDEND_VCHR,EXECTYPE_VCHR,RICHTYPE_VCHR,USAGETYPE_VCHR,USAGETYPENAME_VCHR,EXECTYPENAME_VCHR,RICHTYPENAME_VCHR, medTypeId) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?, 0)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(16, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_strRecordID;//p_objRecord.m_strPUTMEDREQID_CHR;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strAREAID_CHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strDES_VCHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_intPSTATUS_INT;
                objLisAddItemRefArr[4].Value = 1;//p_objRecord.m_intSTATUS_INT;
                                                 //objLisAddItemRefArr[5].Value = p_objRecord.m_strDEACTIVATOR_CHR;
                                                 //objLisAddItemRefArr[6].Value = DateTime.Parse(p_objRecord.m_strDEACTIVATE_DAT);
                objLisAddItemRefArr[5].Value = p_objRecord.m_strCREATOR_CHR;
                objLisAddItemRefArr[6].Value = DateTime.Parse(strDateTime);
                objLisAddItemRefArr[7].Value = p_objRecord.m_strRANGEDES_VCHR;
                //objLisAddItemRefArr[10].Value = p_objRecord.m_strPROCESSOR_CHR;
                //objLisAddItemRefArr[11].Value = DateTime.Parse(p_objRecord.m_strFINISH_DAT);
                objLisAddItemRefArr[8].Value = p_objRecord.m_strBEDIDS_VCHR;
                objLisAddItemRefArr[9].Value = p_objRecord.m_strBEDNAMES_VCHR;
                objLisAddItemRefArr[10].Value = p_objRecord.m_strEXECTYPE_VCHR;
                objLisAddItemRefArr[11].Value = p_objRecord.m_strRICHTYPE_VCHR;
                objLisAddItemRefArr[12].Value = p_objRecord.m_strUSAGETYPE_VCHR;
                objLisAddItemRefArr[13].Value = p_objRecord.m_strUSAGETYPENAME_VCHR;
                objLisAddItemRefArr[14].Value = p_objRecord.m_strEXECTYPENAME_VCHR;
                objLisAddItemRefArr[15].Value = p_objRecord.m_strRICHTYPENAME_VCHR;
                long lngRecEff = -1;
                //�������Ӽ�¼
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
        #region �޸�
        /// <summary>
        /// ֻ�޸İ�ҩ��������
        /// ע�⣺
        ///		���в��޸ģ�
        ///		m_intPSTATUS_INT��m_intSTATUS_INT��m_strDEACTIVATOR_CHR��m_strDEACTIVATE_DAT��m_strCREATOR_CHR��m_strCREATE_DAT��m_strPROCESSOR_CHR��m_strFINISH_DAT
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord">����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyPutMedReqContent(clsT_Bih_Opr_PutMedReq_VO p_objRecord)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "";
            strSQL += " UPDATE T_BIH_OPR_PUTMEDREQ A ";
            strSQL += " SET";
            strSQL += "  A.AREAID_CHR = '" + p_objRecord.m_strAREAID_CHR + "'";
            strSQL += "  , A.DES_VCHR = '" + p_objRecord.m_strDES_VCHR + "'";
            //strSQL +="  , A.PSTATUS_INT = '" + p_objRecord.m_intPSTATUS_INT.ToString() + "'";
            //strSQL +="  , A.STATUS_INT = '" + p_objRecord.m_intSTATUS_INT.ToString() + "'";
            //strSQL +="  , A.DEACTIVATOR_CHR = '" + p_objRecord.m_strDEACTIVATOR_CHR + "'";
            //strSQL +="  , A.DEACTIVATE_DAT = TO_DATE('"+p_objRecord.m_strDEACTIVATE_DAT+"','YYYY-MM-DD hh24:mi:ss')";
            //strSQL +="  , A.CREATOR_CHR = '" + p_objRecord.m_strCREATOR_CHR + "'";
            //strSQL +="  , A.CREATE_DAT = TO_DATE('"+p_objRecord.m_strCREATE_DAT+"','YYYY-MM-DD hh24:mi:ss')";
            strSQL += "  , A.RANGEDES_VCHR = '" + p_objRecord.m_strRANGEDES_VCHR + "'";
            //strSQL +="  , A.PROCESSOR_CHR = '" + p_objRecord.m_strPROCESSOR_CHR + "'";
            //strSQL +="  , A.FINISH_DAT =TO_DATE('"+p_objRecord.m_strFINISH_DAT+"','YYYY-MM-DD hh24:mi:ss')";
            strSQL += "  , A.BEDBEGIN_VCHR = '" + p_objRecord.m_strBEDIDS_VCHR + "'";
            strSQL += "  , A.BEDEND_VCHR = '" + p_objRecord.m_strBEDNAMES_VCHR + "'";
            strSQL += "  , A.EXECTYPE_VCHR = '" + p_objRecord.m_strEXECTYPE_VCHR + "'";
            strSQL += "  , A.EXECTYPENAME_VCHR = '" + p_objRecord.m_strEXECTYPENAME_VCHR + "'";
            strSQL += "  , A.RICHTYPE_VCHR = '" + p_objRecord.m_strRICHTYPE_VCHR + "'";
            strSQL += "  , A.RICHTYPENAME_VCHR = '" + p_objRecord.m_strRICHTYPENAME_VCHR + "'";
            strSQL += "  , A.USAGETYPE_VCHR = '" + p_objRecord.m_strUSAGETYPE_VCHR + "'";
            strSQL += "  , A.USAGETYPENAME_VCHR = '" + p_objRecord.m_strUSAGETYPENAME_VCHR + "'";
            strSQL += "  Where Trim(A.PUTMEDREQID_CHR) ='" + p_objRecord.m_strPUTMEDREQID_CHR.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        /// <summary>
        /// �޸İ�ҩ����״̬
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intPstatus">����״̬	{0=δ����;1=�ѷ���;2=���ַ�ҩ;3=�ѷ�ҩ};if(this==2) ������������</param>
        /// <param name="p_strProcessor">������{=��Ա.id}</param>
        /// <param name="p_strID">��ҩ��ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyPutMedReqStatus(int p_intPstatus, string p_strProcessor, string p_strID)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "";
            strSQL += " UPDATE T_BIH_OPR_PUTMEDREQ A ";
            strSQL += " SET";
            strSQL += "    A.PSTATUS_INT = '" + p_intPstatus.ToString() + "'";
            strSQL += "  , A.PROCESSOR_CHR = '" + p_strProcessor + "'";
            strSQL += "  , A.FINISH_DAT =TO_DATE('" + strDateTime + "','YYYY-MM-DD hh24:mi:ss')";
            strSQL += "  Where Trim(A.PUTMEDREQID_CHR) ='" + p_strID.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
        #region	ɾ��
        /// <summary>
        /// ɾ����ҩ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeactivator">ɾ����ID</param>
        /// <param name="p_strID">��ҩ��ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeletePutMedReqByID(string p_strDeactivator, string p_strID)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "";
            strSQL += " UPDATE T_BIH_OPR_PUTMEDREQ A ";
            strSQL += " SET";
            strSQL += "  A.STATUS_INT = 0";
            strSQL += "  , A.DEACTIVATOR_CHR = '" + p_strDeactivator + "'";
            strSQL += "  , A.DEACTIVATE_DAT =TO_DATE('" + strDateTime + "','YYYY-MM-DD hh24:mi:ss')";
            strSQL += "  Where Trim(A.PUTMEDREQID_CHR) ='" + p_strID.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        //ҽ����ҩ��ϸ��
        #region ����
        #region	����ID
        /// <summary>
        /// ���Ұ�ҩ��ϸ��	����ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">��ҩ��ϸ��ID</param>
        /// <param name="p_objResult">��ҩ��ϸ������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPutMedDetailByID(string p_strID, out clsT_Bih_Opr_Putmeddetail_VO p_objResult)
        {
            p_objResult = new clsT_Bih_Opr_Putmeddetail_VO();
            long lngRes = 0;
            string strSQL = @"
					SELECT a.*
							,(SELECT deptname_vchr FROM t_bse_deptdesc WHERE t_bse_deptdesc.deptid_chr=a.areaid_chr) AreaName
							,(SELECT BedID_Chr FROM T_Opr_Bih_Register WHERE T_Opr_Bih_Register.registerid_chr=a.registerid_chr) BedID
							,(SELECT Code_Chr FROM T_BSE_Bed WHERE T_BSE_Bed.bedid_chr=
								(SELECT BedID_Chr FROM T_Opr_Bih_Register WHERE T_Opr_Bih_Register.registerid_chr=a.registerid_chr)
							) BedNo
							,(SELECT name_vchr FROM T_BSE_Patient WHERE T_BSE_Patient.patientid_chr=a.PaientID_Chr)  PaientName
							,(SELECT Sex_Chr FROM T_BSE_Patient WHERE T_BSE_Patient.patientid_chr=a.PaientID_Chr) PaientSex
							,(SELECT TO_CHAR(SYSDATE,'YYYY')-TO_CHAR(Birth_Dat,'YYYY') FROM T_BSE_Patient WHERE T_BSE_Patient.patientid_chr=a.PaientID_Chr) PaientAge
							,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.creator_chr) CreatName
							,(select UsageName_VChr from T_Bse_UsageType where T_Bse_UsageType.usageid_chr=a.DoseTypeID_Chr)UsageName
							,(select MedSpec_VChr from T_bse_Medicine where T_bse_Medicine.MedicineID_Chr=a.MedID_Chr )MedSpec
							,(select ASSISTCODE_CHR from T_bse_Medicine where T_bse_Medicine.MedicineID_Chr=a.MedID_Chr )assistcode
							,(select FreqName_Chr from T_Aid_RecipeFreq where T_Aid_RecipeFreq.FreqID_Chr=a.ExecFreqID_Chr)FreqName
							,DECODE(a.ORDEREXECTYPE_INT,1,'����',2,'����',3,'�����¿���','')OrderExecTypeName
							,DECODE(a.PUTTYPE_INT,1,'������',2,'����ϸ(һ��ҩƷ)',3,'����ϸ(ȷ��ҩ)','')PutTypeName
							,(SELECT ratetype_int FROM t_opr_bih_order WHERE t_opr_bih_order.orderid_chr=a.orderid_chr)ratetype_int
					FROM t_bih_opr_putmeddetail a
					WHERE trim(putmeddetailid_chr)='" + p_strID.Trim() + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResult = new clsT_Bih_Opr_Putmeddetail_VO();
                    p_objResult.m_strPUTMEDDETAILID_CHR = dtbResult.Rows[0]["PUTMEDDETAILID_CHR"].ToString().Trim();
                    p_objResult.m_strAREAID_CHR = dtbResult.Rows[0]["AREAID_CHR"].ToString().Trim();
                    p_objResult.m_strPAIENTID_CHR = dtbResult.Rows[0]["PAIENTID_CHR"].ToString().Trim();
                    p_objResult.m_strREGISTERID_CHR = dtbResult.Rows[0]["REGISTERID_CHR"].ToString().Trim();
                    p_objResult.m_strORDERID_CHR = dtbResult.Rows[0]["ORDERID_CHR"].ToString().Trim();
                    p_objResult.m_strORDEREXECID_CHR = dtbResult.Rows[0]["ORDEREXECID_CHR"].ToString().Trim();
                    try
                    {
                        p_objResult.m_intORDEREXECTYPE_INT = Convert.ToInt32(dtbResult.Rows[0]["ORDEREXECTYPE_INT"].ToString().Trim());
                    }
                    catch { }
                    try
                    {
                        p_objResult.m_intRECIPENO_INT = Convert.ToInt32(dtbResult.Rows[0]["RECIPENO_INT"].ToString().Trim());
                    }
                    catch
                    { }
                    try
                    {
                        p_objResult.m_dblDOSAGE_DEC = double.Parse(dtbResult.Rows[0]["DOSAGE_DEC"].ToString().Trim());
                    }
                    catch
                    { }
                    p_objResult.m_strDOSAGEUNIT_VCHR = dtbResult.Rows[0]["DOSAGEUNIT_VCHR"].ToString().Trim();
                    p_objResult.m_strCHARGEITEMID_CHR = dtbResult.Rows[0]["CHARGEITEMID_CHR"].ToString().Trim();
                    p_objResult.m_strMEDID_CHR = dtbResult.Rows[0]["MEDID_CHR"].ToString().Trim();
                    p_objResult.m_strMEDNAME_VCHR = dtbResult.Rows[0]["MEDNAME_VCHR"].ToString().Trim();
                    try
                    {
                        p_objResult.m_intISRICH_INT = Convert.ToInt32(dtbResult.Rows[0]["ISRICH_INT"].ToString().Trim());
                    }
                    catch { }
                    p_objResult.m_strDOSETYPEID_CHR = dtbResult.Rows[0]["DOSETYPEID_CHR"].ToString().Trim();
                    p_objResult.m_strEXECFREQID_CHR = dtbResult.Rows[0]["EXECFREQID_CHR"].ToString().Trim();
                    try
                    {
                        p_objResult.m_intEXECTIMES_INT = Convert.ToInt32(dtbResult.Rows[0]["EXECTIMES_INT"].ToString().Trim());
                    }
                    catch
                    { }
                    try
                    {
                        p_objResult.m_intEXECDAYS_INT = Convert.ToInt32(dtbResult.Rows[0]["EXECDAYS_INT"].ToString().Trim());
                    }
                    catch
                    { }
                    try
                    {
                        p_objResult.m_dblUNITPRICE_MNY = double.Parse(dtbResult.Rows[0]["UNITPRICE_MNY"].ToString().Trim());
                    }
                    catch { }
                    try
                    {
                        p_objResult.m_strUNIT_VCHR = dtbResult.Rows[0]["UNIT_VCHR"].ToString().Trim();
                    }
                    catch
                    { }
                    try
                    {
                        p_objResult.m_dblGET_DEC = double.Parse(dtbResult.Rows[0]["GET_DEC"].ToString().Trim());
                    }
                    catch { }
                    p_objResult.m_strPCHARGEID_CHR = dtbResult.Rows[0]["PCHARGEID_CHR"].ToString().Trim();
                    p_objResult.m_strCREATOR_CHR = dtbResult.Rows[0]["CREATOR_CHR"].ToString().Trim();
                    try
                    {
                        p_objResult.m_strCREATE_DAT = Convert.ToDateTime(dtbResult.Rows[0]["CREATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    }
                    catch { }
                    try
                    {
                        p_objResult.m_intISPUT_INT = Convert.ToInt32(dtbResult.Rows[0]["ISPUT_INT"].ToString().Trim());
                    }
                    catch
                    { }
                    try
                    {
                        p_objResult.m_intPUTTYPE_INT = Convert.ToInt32(dtbResult.Rows[0]["PUTTYPE_INT"].ToString().Trim());
                    }
                    catch { }
                    p_objResult.m_strPUTMEDREQID_CHR = dtbResult.Rows[0]["PUTMEDREQID_CHR"].ToString().Trim();
                    p_objResult.m_strEXECTIME_VCHR = dtbResult.Rows[0]["EXECTIME_VCHR"].ToString().Trim();
                    //���ֶ�
                    p_objResult.m_strAreaName = dtbResult.Rows[0]["AreaName"].ToString().Trim();
                    p_objResult.m_strBedID = dtbResult.Rows[0]["BedID"].ToString().Trim();
                    p_objResult.m_strBedNo = dtbResult.Rows[0]["BedNo"].ToString().Trim();
                    p_objResult.m_strPaientName = dtbResult.Rows[0]["PaientName"].ToString().Trim();
                    p_objResult.m_strPaientSex = dtbResult.Rows[0]["PaientSex"].ToString().Trim();
                    try
                    {
                        p_objResult.m_intPaientAge = Int32.Parse(dtbResult.Rows[0]["PaientAge"].ToString());
                    }
                    catch { }
                    p_objResult.m_strOrderExecTypeName = dtbResult.Rows[0]["OrderExecTypeName"].ToString().Trim();
                    p_objResult.m_strCreatName = dtbResult.Rows[0]["CreatName"].ToString().Trim();
                    p_objResult.m_strPutTypeName = dtbResult.Rows[0]["PutTypeName"].ToString().Trim();
                    p_objResult.m_strUsageName = dtbResult.Rows[0]["UsageName"].ToString().Trim();
                    p_objResult.m_strMedSpec = dtbResult.Rows[0]["MedSpec"].ToString().Trim();
                    p_objResult.m_strASSISTCODE_CHR = dtbResult.Rows[0]["assistcode"].ToString().Trim();
                    p_objResult.m_strFreqName = dtbResult.Rows[0]["FreqName"].ToString().Trim();
                    try
                    {
                        p_objResult.m_intRATETYPE_INT = Int32.Parse(dtbResult.Rows[0]["ratetype_int"].ToString());
                    }
                    catch { }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
        #region ���ݰ�ҩ���뵥ID
        /// <summary>
        /// ���Ұ�ҩ��ϸ��	���ݰ�ҩ���뵥ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strPUTMEDREQID_CHR">��ҩ���뵥ID</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPutMedDetailByPutMedReqID(string p_strPUTMEDREQID_CHR, out clsT_Bih_Opr_Putmeddetail_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_Bih_Opr_Putmeddetail_VO[0];
            long lngRes = 0;
            string strSQL = @"
					SELECT a.*
							,(SELECT deptname_vchr FROM t_bse_deptdesc WHERE t_bse_deptdesc.deptid_chr=a.areaid_chr) AreaName
							,(SELECT BedID_Chr FROM T_Opr_Bih_Register WHERE T_Opr_Bih_Register.registerid_chr=a.registerid_chr) BedID
							,(SELECT Code_Chr FROM T_BSE_Bed WHERE T_BSE_Bed.bedid_chr=
								(SELECT BedID_Chr FROM T_Opr_Bih_Register WHERE T_Opr_Bih_Register.registerid_chr=a.registerid_chr)
							) BedNo
							,(SELECT name_vchr FROM T_BSE_Patient WHERE T_BSE_Patient.patientid_chr=a.PaientID_Chr)  PaientName
							,(SELECT Sex_Chr FROM T_BSE_Patient WHERE T_BSE_Patient.patientid_chr=a.PaientID_Chr) PaientSex
							,(SELECT TO_CHAR(SYSDATE,'YYYY')-TO_CHAR(Birth_Dat,'YYYY') FROM T_BSE_Patient WHERE T_BSE_Patient.patientid_chr=a.PaientID_Chr) PaientAge
							,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.creator_chr) CreatName
							,(select UsageName_VChr from T_Bse_UsageType where T_Bse_UsageType.usageid_chr=a.DoseTypeID_Chr)UsageName
							,(select MedSpec_VChr from T_bse_Medicine where T_bse_Medicine.MedicineID_Chr=a.MedID_Chr )MedSpec
							,(select ASSISTCODE_CHR from T_bse_Medicine where T_bse_Medicine.MedicineID_Chr=a.MedID_Chr )ASSISTCODE
							,(select FreqName_Chr from T_Aid_RecipeFreq where T_Aid_RecipeFreq.FreqID_Chr=a.ExecFreqID_Chr)FreqName
							,DECODE(a.ORDEREXECTYPE_INT,1,'����',2,'����',3,'�����¿���','')OrderExecTypeName
							,DECODE(a.PUTTYPE_INT,1,'������',2,'����ϸ(һ��ҩƷ)',3,'����ϸ(ȷ��ҩ)','')PutTypeName
							,(SELECT ratetype_int FROM t_opr_bih_order WHERE t_opr_bih_order.orderid_chr=a.orderid_chr)ratetype_int
					FROM t_bih_opr_putmeddetail a
					ORDER BY ISPUT_INT DESC,areaid_chr,BedNo";
            strSQL += " WHERE trim(PUTMEDREQID_CHR)='" + p_strPUTMEDREQID_CHR.Trim() + "'ORDER BY ISPUT_INT DESC";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_Bih_Opr_Putmeddetail_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_Bih_Opr_Putmeddetail_VO();
                        #region ���
                        p_objResultArr[i1].m_strPUTMEDDETAILID_CHR = dtbResult.Rows[i1]["PUTMEDDETAILID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strAREAID_CHR = dtbResult.Rows[i1]["AREAID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPAIENTID_CHR = dtbResult.Rows[i1]["PAIENTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strREGISTERID_CHR = dtbResult.Rows[i1]["REGISTERID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strORDERID_CHR = dtbResult.Rows[i1]["ORDERID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strORDEREXECID_CHR = dtbResult.Rows[i1]["ORDEREXECID_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intORDEREXECTYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["ORDEREXECTYPE_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_intRECIPENO_INT = Convert.ToInt32(dtbResult.Rows[i1]["RECIPENO_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_dblDOSAGE_DEC = double.Parse(dtbResult.Rows[i1]["DOSAGE_DEC"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strDOSAGEUNIT_VCHR = dtbResult.Rows[i1]["DOSAGEUNIT_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCHARGEITEMID_CHR = dtbResult.Rows[i1]["CHARGEITEMID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMEDID_CHR = dtbResult.Rows[i1]["MEDID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMEDNAME_VCHR = dtbResult.Rows[i1]["MEDNAME_VCHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intISRICH_INT = Convert.ToInt32(dtbResult.Rows[i1]["ISRICH_INT"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strDOSETYPEID_CHR = dtbResult.Rows[i1]["DOSETYPEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strEXECFREQID_CHR = dtbResult.Rows[i1]["EXECFREQID_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intEXECTIMES_INT = Convert.ToInt32(dtbResult.Rows[i1]["EXECTIMES_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_intEXECDAYS_INT = Convert.ToInt32(dtbResult.Rows[i1]["EXECDAYS_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_dblUNITPRICE_MNY = double.Parse(dtbResult.Rows[i1]["UNITPRICE_MNY"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strUNIT_VCHR = dtbResult.Rows[i1]["UNIT_VCHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_dblGET_DEC = double.Parse(dtbResult.Rows[i1]["GET_DEC"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strPCHARGEID_CHR = dtbResult.Rows[i1]["PCHARGEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCREATOR_CHR = dtbResult.Rows[i1]["CREATOR_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_strCREATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CREATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_intISPUT_INT = Convert.ToInt32(dtbResult.Rows[i1]["ISPUT_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_intPUTTYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["PUTTYPE_INT"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strPUTMEDREQID_CHR = dtbResult.Rows[i1]["PUTMEDREQID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strEXECTIME_VCHR = dtbResult.Rows[i1]["EXECTIME_VCHR"].ToString().Trim();
                        //���ֶ�
                        p_objResultArr[i1].m_strAreaName = dtbResult.Rows[i1]["AreaName"].ToString().Trim();
                        p_objResultArr[i1].m_strBedID = dtbResult.Rows[i1]["BedID"].ToString().Trim();
                        p_objResultArr[i1].m_strBedNo = dtbResult.Rows[i1]["BedNo"].ToString().Trim();
                        p_objResultArr[i1].m_strPaientName = dtbResult.Rows[i1]["PaientName"].ToString().Trim();
                        p_objResultArr[i1].m_strPaientSex = dtbResult.Rows[i1]["PaientSex"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intPaientAge = Int32.Parse(dtbResult.Rows[i1]["PaientAge"].ToString());
                        }
                        catch { }
                        p_objResultArr[i1].m_strOrderExecTypeName = dtbResult.Rows[i1]["OrderExecTypeName"].ToString().Trim();
                        p_objResultArr[i1].m_strCreatName = dtbResult.Rows[i1]["CreatName"].ToString().Trim();
                        p_objResultArr[i1].m_strPutTypeName = dtbResult.Rows[i1]["PutTypeName"].ToString().Trim();
                        p_objResultArr[i1].m_strUsageName = dtbResult.Rows[i1]["UsageName"].ToString().Trim();
                        p_objResultArr[i1].m_strMedSpec = dtbResult.Rows[i1]["MedSpec"].ToString().Trim();
                        p_objResultArr[i1].m_strASSISTCODE_CHR = dtbResult.Rows[i1]["ASSISTCODE"].ToString().Trim();
                        p_objResultArr[i1].m_strFreqName = dtbResult.Rows[i1]["FreqName"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intRATETYPE_INT = Int32.Parse(dtbResult.Rows[i1]["ratetype_int"].ToString());
                        }
                        catch { }
                        #endregion
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        /// <summary>
        /// ���Ұ�ҩ��ϸ��	���ݰ�ҩ���뵥ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intPutMedState">��ҩ״̬	{0=δ��ҩ;1=�Ѿ���ҩ;2=ȫ��(Ĭ��)}</param>
        /// <param name="p_strPUTMEDREQID_CHR">��ҩ���뵥ID</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPutMedDetailByPutMedReqID(int p_intPutMedState, string p_strPUTMEDREQID_CHR, out clsT_Bih_Opr_Putmeddetail_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_Bih_Opr_Putmeddetail_VO[0];
            long lngRes = 0;
            #region SQL
            string strSQL = @"
					SELECT a.*
							,(SELECT deptname_vchr FROM t_bse_deptdesc WHERE t_bse_deptdesc.deptid_chr=a.areaid_chr) AreaName
							,(SELECT BedID_Chr FROM T_Opr_Bih_Register WHERE T_Opr_Bih_Register.registerid_chr=a.registerid_chr) BedID
							,(SELECT Code_Chr FROM T_BSE_Bed WHERE T_BSE_Bed.bedid_chr=
								(SELECT BedID_Chr FROM T_Opr_Bih_Register WHERE T_Opr_Bih_Register.registerid_chr=a.registerid_chr)
							) BedNo
							,(SELECT name_vchr FROM T_BSE_Patient WHERE T_BSE_Patient.patientid_chr=a.PaientID_Chr)  PaientName
							,(SELECT Sex_Chr FROM T_BSE_Patient WHERE T_BSE_Patient.patientid_chr=a.PaientID_Chr) PaientSex
							,(SELECT TO_CHAR(SYSDATE,'YYYY')-TO_CHAR(Birth_Dat,'YYYY') FROM T_BSE_Patient WHERE T_BSE_Patient.patientid_chr=a.PaientID_Chr) PaientAge
							,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.creator_chr) CreatName
							,(select UsageName_VChr from T_Bse_UsageType where T_Bse_UsageType.usageid_chr=a.DoseTypeID_Chr)UsageName
							,(select MedSpec_VChr from T_bse_Medicine where T_bse_Medicine.MedicineID_Chr=a.MedID_Chr )MedSpec
							,(select ASSISTCODE_CHR from T_bse_Medicine where T_bse_Medicine.MedicineID_Chr=a.MedID_Chr )ASSISTCODE
							,(select FreqName_Chr from T_Aid_RecipeFreq where T_Aid_RecipeFreq.FreqID_Chr=a.ExecFreqID_Chr)FreqName
							,DECODE(a.ORDEREXECTYPE_INT,1,'����',2,'����',3,'�����¿���','')OrderExecTypeName
							,DECODE(a.PUTTYPE_INT,1,'������',2,'����ϸ(һ��ҩƷ)',3,'����ϸ(ȷ��ҩ)','')PutTypeName
							,(SELECT ratetype_int FROM t_opr_bih_order WHERE t_opr_bih_order.orderid_chr=a.orderid_chr)ratetype_int
					FROM t_bih_opr_putmeddetail a
					WHERE STATUS_INT=1 and PSTATUS_INT =0 
						[PUTMEDSTATE]
					ORDER BY ISPUT_INT DESC,areaid_chr,BedNo";
            strSQL = strSQL.Replace("[PUTMEDREQID_CHRVALUE]", p_strPUTMEDREQID_CHR.Trim());
            string str = "";
            switch (p_intPutMedState)
            {
                case 0://0=δ��ҩ
                    str = " and ISPUT_INT=0 ";
                    break;
                case 1://1=�Ѿ���ҩ
                    str = " and ISPUT_INT=1 ";
                    break;
                default://2=ȫ��(Ĭ��)
                    break;
            }
            strSQL = strSQL.Replace("[PUTMEDSTATE]", str);
            #endregion
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_Bih_Opr_Putmeddetail_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_Bih_Opr_Putmeddetail_VO();
                        #region ���
                        p_objResultArr[i1].m_strPUTMEDDETAILID_CHR = dtbResult.Rows[i1]["PUTMEDDETAILID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strAREAID_CHR = dtbResult.Rows[i1]["AREAID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPAIENTID_CHR = dtbResult.Rows[i1]["PAIENTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strREGISTERID_CHR = dtbResult.Rows[i1]["REGISTERID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strORDERID_CHR = dtbResult.Rows[i1]["ORDERID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strORDEREXECID_CHR = dtbResult.Rows[i1]["ORDEREXECID_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intORDEREXECTYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["ORDEREXECTYPE_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_intRECIPENO_INT = Convert.ToInt32(dtbResult.Rows[i1]["RECIPENO_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_dblDOSAGE_DEC = double.Parse(dtbResult.Rows[i1]["DOSAGE_DEC"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strDOSAGEUNIT_VCHR = dtbResult.Rows[i1]["DOSAGEUNIT_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCHARGEITEMID_CHR = dtbResult.Rows[i1]["CHARGEITEMID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMEDID_CHR = dtbResult.Rows[i1]["MEDID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMEDNAME_VCHR = dtbResult.Rows[i1]["MEDNAME_VCHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intISRICH_INT = Convert.ToInt32(dtbResult.Rows[i1]["ISRICH_INT"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strDOSETYPEID_CHR = dtbResult.Rows[i1]["DOSETYPEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strEXECFREQID_CHR = dtbResult.Rows[i1]["EXECFREQID_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intEXECTIMES_INT = Convert.ToInt32(dtbResult.Rows[i1]["EXECTIMES_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_intEXECDAYS_INT = Convert.ToInt32(dtbResult.Rows[i1]["EXECDAYS_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_dblUNITPRICE_MNY = double.Parse(dtbResult.Rows[i1]["UNITPRICE_MNY"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strUNIT_VCHR = dtbResult.Rows[i1]["UNIT_VCHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_dblGET_DEC = double.Parse(dtbResult.Rows[i1]["GET_DEC"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strPCHARGEID_CHR = dtbResult.Rows[i1]["PCHARGEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCREATOR_CHR = dtbResult.Rows[i1]["CREATOR_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_strCREATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CREATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_intISPUT_INT = Convert.ToInt32(dtbResult.Rows[i1]["ISPUT_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_intPUTTYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["PUTTYPE_INT"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strPUTMEDREQID_CHR = dtbResult.Rows[i1]["PUTMEDREQID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strEXECTIME_VCHR = dtbResult.Rows[i1]["EXECTIME_VCHR"].ToString().Trim();
                        //���ֶ�
                        p_objResultArr[i1].m_strAreaName = dtbResult.Rows[i1]["AreaName"].ToString().Trim();
                        p_objResultArr[i1].m_strBedID = dtbResult.Rows[i1]["BedID"].ToString().Trim();
                        p_objResultArr[i1].m_strBedNo = dtbResult.Rows[i1]["BedNo"].ToString().Trim();
                        p_objResultArr[i1].m_strPaientName = dtbResult.Rows[i1]["PaientName"].ToString().Trim();
                        p_objResultArr[i1].m_strPaientSex = dtbResult.Rows[i1]["PaientSex"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intPaientAge = Int32.Parse(dtbResult.Rows[i1]["PaientAge"].ToString());
                        }
                        catch { }
                        p_objResultArr[i1].m_strOrderExecTypeName = dtbResult.Rows[i1]["OrderExecTypeName"].ToString().Trim();
                        p_objResultArr[i1].m_strCreatName = dtbResult.Rows[i1]["CreatName"].ToString().Trim();
                        p_objResultArr[i1].m_strPutTypeName = dtbResult.Rows[i1]["PutTypeName"].ToString().Trim();
                        p_objResultArr[i1].m_strUsageName = dtbResult.Rows[i1]["UsageName"].ToString().Trim();
                        p_objResultArr[i1].m_strMedSpec = dtbResult.Rows[i1]["MedSpec"].ToString().Trim();
                        p_objResultArr[i1].m_strASSISTCODE_CHR = dtbResult.Rows[i1]["ASSISTCODE"].ToString().Trim();
                        p_objResultArr[i1].m_strFreqName = dtbResult.Rows[i1]["FreqName"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intRATETYPE_INT = Int32.Parse(dtbResult.Rows[i1]["ratetype_int"].ToString());
                        }
                        catch { }
                        #endregion
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
        #region ����ִ�е�ID
        /// <summary>
        /// ���Ұ�ҩ��ϸ��	����ִ�е�ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intPutMedState">��ҩ״̬	{0=δ��ҩ;1=�Ѿ���ҩ;2=ȫ��(Ĭ��)}</param>
        /// <param name="p_strORDEREXECID_CHR">��ҩ���뵥ID</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPutMedDetailByOrderExecID(int p_intPutMedState, string p_strORDEREXECID_CHR, out clsT_Bih_Opr_Putmeddetail_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_Bih_Opr_Putmeddetail_VO[0];
            long lngRes = 0;
            #region SQL
            string strSQL = @"
					SELECT a.*
							,(SELECT deptname_vchr FROM t_bse_deptdesc WHERE t_bse_deptdesc.deptid_chr=a.areaid_chr) AreaName
							,(SELECT BedID_Chr FROM T_Opr_Bih_Register WHERE T_Opr_Bih_Register.registerid_chr=a.registerid_chr) BedID
							,(SELECT Code_Chr FROM T_BSE_Bed WHERE T_BSE_Bed.bedid_chr=
								(SELECT BedID_Chr FROM T_Opr_Bih_Register WHERE T_Opr_Bih_Register.registerid_chr=a.registerid_chr)
							) BedNo
							,(SELECT name_vchr FROM T_BSE_Patient WHERE T_BSE_Patient.patientid_chr=a.PaientID_Chr)  PaientName
							,(SELECT Sex_Chr FROM T_BSE_Patient WHERE T_BSE_Patient.patientid_chr=a.PaientID_Chr) PaientSex
							,(SELECT TO_CHAR(SYSDATE,'YYYY')-TO_CHAR(Birth_Dat,'YYYY') FROM T_BSE_Patient WHERE T_BSE_Patient.patientid_chr=a.PaientID_Chr) PaientAge
							,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.creator_chr) CreatName
							,(select UsageName_VChr from T_Bse_UsageType where T_Bse_UsageType.usageid_chr=a.DoseTypeID_Chr)UsageName
							,(select MedSpec_VChr from T_bse_Medicine where T_bse_Medicine.MedicineID_Chr=a.MedID_Chr )MedSpec
							,(select ASSISTCODE_CHR from T_bse_Medicine where T_bse_Medicine.MedicineID_Chr=a.MedID_Chr )assistcode
							,(select FreqName_Chr from T_Aid_RecipeFreq where T_Aid_RecipeFreq.FreqID_Chr=a.ExecFreqID_Chr)FreqName
							,DECODE(a.ORDEREXECTYPE_INT,1,'����',2,'����',3,'�����¿���','')OrderExecTypeName
							,DECODE(a.PUTTYPE_INT,1,'������',2,'����ϸ(һ��ҩƷ)',3,'����ϸ(ȷ��ҩ)','')PutTypeName
							,(SELECT ratetype_int FROM t_opr_bih_order WHERE t_opr_bih_order.orderid_chr=a.orderid_chr)ratetype_int
					FROM t_bih_opr_putmeddetail a
					WHERE trim(ORDEREXECID_CHR)='[ORDEREXECID_CHRVALUE]' [PUTMEDSTATE]
					ORDER BY ISPUT_INT DESC ,areaid_chr,BedNo";
            strSQL = strSQL.Replace("[ORDEREXECID_CHRVALUE]", p_strORDEREXECID_CHR.Trim());
            string str = "";
            switch (p_intPutMedState)
            {
                case 0://0=δ��ҩ
                    str = " and ISPUT_INT=0 ";
                    break;
                case 1://1=�Ѿ���ҩ
                    str = " and ISPUT_INT=1 ";
                    break;
                default://2=ȫ��(Ĭ��)
                    break;
            }
            strSQL = strSQL.Replace("[PUTMEDSTATE]", str);
            #endregion
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_Bih_Opr_Putmeddetail_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_Bih_Opr_Putmeddetail_VO();
                        #region ���
                        p_objResultArr[i1].m_strPUTMEDDETAILID_CHR = dtbResult.Rows[i1]["PUTMEDDETAILID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strAREAID_CHR = dtbResult.Rows[i1]["AREAID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPAIENTID_CHR = dtbResult.Rows[i1]["PAIENTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strREGISTERID_CHR = dtbResult.Rows[i1]["REGISTERID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strORDERID_CHR = dtbResult.Rows[i1]["ORDERID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strORDEREXECID_CHR = dtbResult.Rows[i1]["ORDEREXECID_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intORDEREXECTYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["ORDEREXECTYPE_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_intRECIPENO_INT = Convert.ToInt32(dtbResult.Rows[i1]["RECIPENO_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_dblDOSAGE_DEC = double.Parse(dtbResult.Rows[i1]["DOSAGE_DEC"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strDOSAGEUNIT_VCHR = dtbResult.Rows[i1]["DOSAGEUNIT_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCHARGEITEMID_CHR = dtbResult.Rows[i1]["CHARGEITEMID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMEDID_CHR = dtbResult.Rows[i1]["MEDID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMEDNAME_VCHR = dtbResult.Rows[i1]["MEDNAME_VCHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intISRICH_INT = Convert.ToInt32(dtbResult.Rows[i1]["ISRICH_INT"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strDOSETYPEID_CHR = dtbResult.Rows[i1]["DOSETYPEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strEXECFREQID_CHR = dtbResult.Rows[i1]["EXECFREQID_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intEXECTIMES_INT = Convert.ToInt32(dtbResult.Rows[i1]["EXECTIMES_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_intEXECDAYS_INT = Convert.ToInt32(dtbResult.Rows[i1]["EXECDAYS_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_dblUNITPRICE_MNY = double.Parse(dtbResult.Rows[i1]["UNITPRICE_MNY"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strUNIT_VCHR = dtbResult.Rows[i1]["UNIT_VCHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_dblGET_DEC = double.Parse(dtbResult.Rows[i1]["GET_DEC"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strPCHARGEID_CHR = dtbResult.Rows[i1]["PCHARGEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCREATOR_CHR = dtbResult.Rows[i1]["CREATOR_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_strCREATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CREATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_intISPUT_INT = Convert.ToInt32(dtbResult.Rows[i1]["ISPUT_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_intPUTTYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["PUTTYPE_INT"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strPUTMEDREQID_CHR = dtbResult.Rows[i1]["PUTMEDREQID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strEXECTIME_VCHR = dtbResult.Rows[i1]["EXECTIME_VCHR"].ToString().Trim();
                        //���ֶ�
                        p_objResultArr[i1].m_strAreaName = dtbResult.Rows[i1]["AreaName"].ToString().Trim();
                        p_objResultArr[i1].m_strBedID = dtbResult.Rows[i1]["BedID"].ToString().Trim();
                        p_objResultArr[i1].m_strBedNo = dtbResult.Rows[i1]["BedNo"].ToString().Trim();
                        p_objResultArr[i1].m_strPaientName = dtbResult.Rows[i1]["PaientName"].ToString().Trim();
                        p_objResultArr[i1].m_strPaientSex = dtbResult.Rows[i1]["PaientSex"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intPaientAge = Int32.Parse(dtbResult.Rows[i1]["PaientAge"].ToString());
                        }
                        catch { }
                        p_objResultArr[i1].m_strOrderExecTypeName = dtbResult.Rows[i1]["OrderExecTypeName"].ToString().Trim();
                        p_objResultArr[i1].m_strCreatName = dtbResult.Rows[i1]["CreatName"].ToString().Trim();
                        p_objResultArr[i1].m_strPutTypeName = dtbResult.Rows[i1]["PutTypeName"].ToString().Trim();
                        p_objResultArr[i1].m_strUsageName = dtbResult.Rows[i1]["UsageName"].ToString().Trim();
                        p_objResultArr[i1].m_strMedSpec = dtbResult.Rows[i1]["MedSpec"].ToString().Trim();
                        p_objResultArr[i1].m_strASSISTCODE_CHR = dtbResult.Rows[i1]["assistcode"].ToString().Trim();
                        p_objResultArr[i1].m_strFreqName = dtbResult.Rows[i1]["FreqName"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intRATETYPE_INT = Int32.Parse(dtbResult.Rows[i1]["ratetype_int"].ToString());
                        }
                        catch { }
                        #endregion
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
        #region ����ҽ��ID
        /// <summary>
        /// ���Ұ�ҩ��ϸ��	ҽ��ID	[����]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderIDArr">ҽ��ID	[����]</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPutMedDetailByOrderID(string[] p_strOrderIDArr, out clsT_Bih_Opr_Putmeddetail_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_Bih_Opr_Putmeddetail_VO[0];
            long lngRes = 0;
            string strSQL = @"
					SELECT a.*
							,(SELECT deptname_vchr FROM t_bse_deptdesc WHERE t_bse_deptdesc.deptid_chr=a.areaid_chr) AreaName
							,(SELECT BedID_Chr FROM T_Opr_Bih_Register WHERE T_Opr_Bih_Register.registerid_chr=a.registerid_chr) BedID
							,(SELECT Code_Chr FROM T_BSE_Bed WHERE T_BSE_Bed.bedid_chr=
								(SELECT BedID_Chr FROM T_Opr_Bih_Register WHERE T_Opr_Bih_Register.registerid_chr=a.registerid_chr)
							) BedNo
							,(SELECT name_vchr FROM T_BSE_Patient WHERE T_BSE_Patient.patientid_chr=a.PaientID_Chr)  PaientName
							,(SELECT Sex_Chr FROM T_BSE_Patient WHERE T_BSE_Patient.patientid_chr=a.PaientID_Chr) PaientSex
							,(SELECT TO_CHAR(SYSDATE,'YYYY')-TO_CHAR(Birth_Dat,'YYYY') FROM T_BSE_Patient WHERE T_BSE_Patient.patientid_chr=a.PaientID_Chr) PaientAge
							,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.creator_chr) CreatName
							,(select UsageName_VChr from T_Bse_UsageType where T_Bse_UsageType.usageid_chr=a.DoseTypeID_Chr)UsageName
							,(select MedSpec_VChr from T_bse_Medicine where T_bse_Medicine.MedicineID_Chr=a.MedID_Chr )MedSpec
							,(select assistcode_chr from T_bse_Medicine where T_bse_Medicine.MedicineID_Chr=a.MedID_Chr )assistcode
							,(select FreqName_Chr from T_Aid_RecipeFreq where T_Aid_RecipeFreq.FreqID_Chr=a.ExecFreqID_Chr)FreqName
							,DECODE(a.ORDEREXECTYPE_INT,1,'����',2,'����',3,'�����¿���','')OrderExecTypeName
							,DECODE(a.PUTTYPE_INT,1,'������',2,'����ϸ(һ��ҩƷ)',3,'����ϸ(ȷ��ҩ)','')PutTypeName
							,(SELECT ratetype_int FROM t_opr_bih_order WHERE t_opr_bih_order.orderid_chr=a.orderid_chr)ratetype_int
					FROM t_bih_opr_putmeddetail a
					WHERE Trim(ORDERID_CHR) in ([GETORDERID])
					ORDER BY ISPUT_INT DESC,areaid_chr,BedNo";
            string strTem = "";
            if (p_strOrderIDArr != null)
            {
                for (int i1 = 0; i1 < p_strOrderIDArr.Length; i1++)
                {
                    if (i1 > 1) strTem += ",";
                    strTem += "'" + p_strOrderIDArr[i1].ToString() + "'";
                }
            }
            if (strTem.Trim() == "") strTem = "''";
            strSQL = strSQL.Replace("[GETORDERID]", strTem);
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_Bih_Opr_Putmeddetail_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_Bih_Opr_Putmeddetail_VO();
                        p_objResultArr[i1].m_strPUTMEDDETAILID_CHR = dtbResult.Rows[i1]["PUTMEDDETAILID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strAREAID_CHR = dtbResult.Rows[i1]["AREAID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPAIENTID_CHR = dtbResult.Rows[i1]["PAIENTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strREGISTERID_CHR = dtbResult.Rows[i1]["REGISTERID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strORDERID_CHR = dtbResult.Rows[i1]["ORDERID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strORDEREXECID_CHR = dtbResult.Rows[i1]["ORDEREXECID_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intORDEREXECTYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["ORDEREXECTYPE_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_intRECIPENO_INT = Convert.ToInt32(dtbResult.Rows[i1]["RECIPENO_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_dblDOSAGE_DEC = double.Parse(dtbResult.Rows[i1]["DOSAGE_DEC"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strDOSAGEUNIT_VCHR = dtbResult.Rows[i1]["DOSAGEUNIT_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCHARGEITEMID_CHR = dtbResult.Rows[i1]["CHARGEITEMID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMEDID_CHR = dtbResult.Rows[i1]["MEDID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMEDNAME_VCHR = dtbResult.Rows[i1]["MEDNAME_VCHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intISRICH_INT = Convert.ToInt32(dtbResult.Rows[i1]["ISRICH_INT"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strDOSETYPEID_CHR = dtbResult.Rows[i1]["DOSETYPEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strEXECFREQID_CHR = dtbResult.Rows[i1]["EXECFREQID_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intEXECTIMES_INT = Convert.ToInt32(dtbResult.Rows[i1]["EXECTIMES_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_intEXECDAYS_INT = Int32.Parse(dtbResult.Rows[i1]["EXECDAYS_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_dblUNITPRICE_MNY = double.Parse(dtbResult.Rows[i1]["UNITPRICE_MNY"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strUNIT_VCHR = dtbResult.Rows[i1]["UNIT_VCHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_dblGET_DEC = double.Parse(dtbResult.Rows[i1]["GET_DEC"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strPCHARGEID_CHR = dtbResult.Rows[i1]["PCHARGEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCREATOR_CHR = dtbResult.Rows[i1]["CREATOR_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_strCREATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CREATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_intISPUT_INT = Convert.ToInt32(dtbResult.Rows[i1]["ISPUT_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_intPUTTYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["PUTTYPE_INT"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strPUTMEDREQID_CHR = dtbResult.Rows[i1]["PUTMEDREQID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strEXECTIME_VCHR = dtbResult.Rows[i1]["EXECTIME_VCHR"].ToString().Trim();
                        //���ֶ�
                        p_objResultArr[i1].m_strAreaName = dtbResult.Rows[i1]["AreaName"].ToString().Trim();
                        p_objResultArr[i1].m_strBedID = dtbResult.Rows[i1]["BedID"].ToString().Trim();
                        p_objResultArr[i1].m_strBedNo = dtbResult.Rows[i1]["BedNo"].ToString().Trim();
                        p_objResultArr[i1].m_strPaientName = dtbResult.Rows[i1]["PaientName"].ToString().Trim();
                        p_objResultArr[i1].m_strPaientSex = dtbResult.Rows[i1]["PaientSex"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intPaientAge = Int32.Parse(dtbResult.Rows[i1]["PaientAge"].ToString());
                        }
                        catch { }
                        p_objResultArr[i1].m_strOrderExecTypeName = dtbResult.Rows[i1]["OrderExecTypeName"].ToString().Trim();
                        p_objResultArr[i1].m_strCreatName = dtbResult.Rows[i1]["CreatName"].ToString().Trim();
                        p_objResultArr[i1].m_strPutTypeName = dtbResult.Rows[i1]["PutTypeName"].ToString().Trim();
                        p_objResultArr[i1].m_strUsageName = dtbResult.Rows[i1]["UsageName"].ToString().Trim();
                        p_objResultArr[i1].m_strMedSpec = dtbResult.Rows[i1]["MedSpec"].ToString().Trim();
                        p_objResultArr[i1].m_strASSISTCODE_CHR = dtbResult.Rows[i1]["assistcode"].ToString().Trim();
                        p_objResultArr[i1].m_strFreqName = dtbResult.Rows[i1]["FreqName"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intRATETYPE_INT = Int32.Parse(dtbResult.Rows[i1]["ratetype_int"].ToString());
                        }
                        catch { }
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
        #region ���ݶ�����
        /// <summary>
        ///	���Ұ�ҩ��ϸ��	���ݶ�����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID">����ID</param>
        /// <param name="p_strBedIDArr">����ID����	[����] {��null��Length=0������Ϊ����}</param>
        /// <param name="p_intExecTypeArr">ҽ����������	[����] {��null��Length=0������Ϊ����}	{1����	2����	3�����¿���}</param>
        /// <param name="p_strDoseTypeIDArr">�÷�����	[����] {��null��Length=0������Ϊ����}</param>
        /// <param name="p_intIsRichArr">�Ƿ������Ʒ����	[����] {��null��Length=0������Ϊ����}	{0�ǹ���	1����}</param>
        /// <param name="p_intIsPutMedicineArr">��ҩ����	[����]	{��null��Length=0������Ϊ����} {0=δ��ҩ��1=�Ѱ�ҩ��}</param>
        /// <param name="dtDetailInfo">��ϸ��Ϣ	[out ����]</param>
        /// <param name="dtCollectInfo">������Ϣ	[out ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPutMedDetail(string p_strAreaID, string[] p_strBedIDArr, int[] p_intExecTypeArr, string[] p_strDoseTypeIDArr, int[] p_intIsRichArr, int[] p_intIsPutMedicineArr, out clsT_Bih_Opr_Putmeddetail_VO[] p_objResultArr)
        {
            return m_lngGetPutMedDetail(p_strAreaID, p_strBedIDArr, p_intExecTypeArr, p_strDoseTypeIDArr, p_intIsRichArr, p_intIsPutMedicineArr, DateTime.Now, out p_objResultArr);
        }
        /// <summary>
        ///	���Ұ�ҩ��ϸ��	���ݶ�����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID">����ID</param>
        /// <param name="p_strBedIDArr">����ID����	[����] {��null��Length=0������Ϊ����}</param>
        /// <param name="p_intExecTypeArr">ҽ����������	[����] {��null��Length=0������Ϊ����}	{1����	2����	3�����¿���}</param>
        /// <param name="p_strDoseTypeIDArr">�÷�����	[����] {��null��Length=0������Ϊ����}</param>
        /// <param name="p_intIsRichArr">�Ƿ������Ʒ����	[����] {��null��Length=0������Ϊ����}	{0�ǹ���	1����}</param>
        /// <param name="p_intIsPutMedicineArr">��ҩ����	[����]	{��null��Length=0������Ϊ����} {0=δ��ҩ��1=�Ѱ�ҩ��}</param>
        /// <param name="p_dtCreatDateTime">����ʱ��	[���뵥����ʱ��]</param>
        /// <param name="dtDetailInfo">��ϸ��Ϣ	[out ����]</param>
        /// <param name="dtCollectInfo">������Ϣ	[out ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPutMedDetail(string p_strAreaID, string[] p_strBedIDArr, int[] p_intExecTypeArr, string[] p_strDoseTypeIDArr, int[] p_intIsRichArr, int[] p_intIsPutMedicineArr, DateTime p_dtCreatDateTime, out clsT_Bih_Opr_Putmeddetail_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_Bih_Opr_Putmeddetail_VO[0];
            long lngRes = 0;
            #region SQL
            string strSQL = @"SELECT   a.*, b.deptname_vchr AS areaname, c.bedid_chr as bedid, d.code_chr AS bedno,
         e.name_vchr AS paientname, e.sex_chr AS paientsex,
         (TO_CHAR (SYSDATE, 'YYYY') - TO_CHAR (e.birth_dat, 'YYYY')
         ) paientage, f.lastname_vchr AS creatname,
         g.usagename_vchr AS usagename, h.medspec_vchr AS medspec,
         h.assistcode_chr AS assistcode, k.freqname_chr AS freqname,
         j.ratetype_int,
         (SELECT flgname_vchr
            FROM t_sys_flg_table
           WHERE tablename_vchr = 't_bih_opr_putmeddetail'
             AND columnname_vchr = 'orderexectype_int'
             AND flg_int = a.orderexectype_int) orderexectypename,
         (SELECT flgname_vchr
            FROM t_sys_flg_table
           WHERE tablename_vchr = 't_bih_opr_putmeddetail'
             AND columnname_vchr = 'puttype_int'
             AND flg_int = a.puttype_int) puttypename
    FROM t_bih_opr_putmeddetail a,
         t_bse_deptdesc b,
         t_opr_bih_register c,
         t_bse_bed d,
         t_bse_patient e,
         t_bse_employee f,
         t_bse_usagetype g,
         t_bse_medicine h,
         t_aid_recipefreq k,
         t_opr_bih_order j
   WHERE 1 = 1
     AND a.areaid_chr = b.deptid_chr
     AND a.registerid_chr = c.registerid_chr
     AND c.bedid_chr = d.bedid_chr
     AND a.paientid_chr = e.patientid_chr
     AND a.creator_chr = f.empid_chr
     AND a.dosetypeid_chr = g.usageid_chr
     AND a.medid_chr = h.medicineid_chr(+)
     AND k.freqid_chr = a.execfreqid_chr(+)
     AND j.orderid_chr = a.orderid_chr
						[OrderExecTypeCondition]
						[DoseTypeCondition]
						[IsRichCondition]
						[AlreadyPutCondition]	
						[AreaIDCondition]	
						[CreatDateTime]	
					ORDER BY ISPUT_INT DESC,BedNo";
            #endregion
            #region ��ѯ����	
            //[OrderExecTypeCondition]	ҽ����������
            #region ҽ����������
            if ((p_intExecTypeArr == null) || (p_intExecTypeArr.Length <= 0))
            {
                strSQL = strSQL.Replace("[OrderExecTypeCondition]", "");
            }
            else
            {
                string strTem = "";
                for (int i = 0; i < p_intExecTypeArr.Length; i++)
                {
                    if (i > 0) strTem += ",";
                    strTem += p_intExecTypeArr[i].ToString();
                }
                if (strTem.Trim() != "")
                    strSQL = strSQL.Replace("[OrderExecTypeCondition]", " and A.OrderExecType_Int in ( " + strTem + " )");
                else
                    strSQL = strSQL.Replace("[OrderExecTypeCondition]", "");
            }
            #endregion
            //[DoseTypeCondition]		�÷�����
            #region �÷�����
            if ((p_strDoseTypeIDArr == null) || (p_strDoseTypeIDArr.Length <= 0))
            {
                strSQL = strSQL.Replace("[DoseTypeCondition]", "");
            }
            else
            {
                string strTem = "";
                for (int i = 0; i < p_strDoseTypeIDArr.Length; i++)
                {
                    if (p_strDoseTypeIDArr[i].Trim() != "")
                    {
                        if (i > 0) strTem += ",";
                        strTem += "'" + p_strDoseTypeIDArr[i].Trim() + "'";
                    }
                }
                if (strTem.Trim() != "")
                    strSQL = strSQL.Replace("[DoseTypeCondition]", " and A.DoseTypeID_Chr in ( " + strTem + " )");
                else
                    strSQL = strSQL.Replace("[DoseTypeCondition]", "");
            }
            #endregion
            //[IsRichCondition]			��������
            #region ��������
            if ((p_intIsRichArr == null) || (p_intIsRichArr.Length <= 0))
            {
                strSQL = strSQL.Replace("[IsRichCondition]", "");
            }
            else
            {
                string strTem = "";
                for (int i = 0; i < p_intIsRichArr.Length; i++)
                {
                    if (i > 0) strTem += ",";
                    strTem += p_intIsRichArr[i].ToString();
                }
                if (strTem.Trim() != "")
                    strSQL = strSQL.Replace("[IsRichCondition]", " and A.IsRich_Int in ( " + strTem + " )");
                else
                    strSQL = strSQL.Replace("[IsRichCondition]", "");
            }
            #endregion
            //[AlreadyPutCondition]		��ҩ����
            #region ��ҩ����
            if ((p_intIsPutMedicineArr == null) || (p_intIsPutMedicineArr.Length <= 0))
            {
                strSQL = strSQL.Replace("[AlreadyPutCondition]", "");
            }
            else
            {
                string strTem = "";
                for (int i = 0; i < p_intIsPutMedicineArr.Length; i++)
                {
                    if (i > 0) strTem += ",";
                    strTem += p_intIsPutMedicineArr[i].ToString();
                }
                if (strTem.Trim() != "")
                    strSQL = strSQL.Replace("[AlreadyPutCondition]", " and A.IsPut_Int in ( " + strTem + " )");
                else
                    strSQL = strSQL.Replace("[AlreadyPutCondition]", "");
            }
            #endregion
            //[AreaIDCondition]			��������
            #region ��������
            strSQL = strSQL.Replace("[AreaIDCondition]", " and Trim(A.areaid_chr)='" + p_strAreaID.Trim() + "'");
            #endregion
            //[BedIDCondition]			�������� [BedIDCondition]	{���� Where}
            #region ��������
            if ((p_strBedIDArr != null && p_strBedIDArr.Length > 0))
            {
                string strTem = "";
                for (int i = 0; i < p_strBedIDArr.Length; i++)
                {
                    if (p_strBedIDArr[i].Trim() != "")
                    {
                        if (i > 0) strTem += ",";
                        strTem += "'" + p_strBedIDArr[i].Trim() + "'";
                    }
                }
                if (strTem.Trim() != "")
                {
                    strSQL = "SELECT a.* FROM (" + strSQL + ") a WHERE A.BedID IN ( " + strTem + " )";
                }
            }
            #endregion
            //[CreatDateTime]			���뵥����ʱ��
            #region ����ʱ��
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                strDateTime = p_dtCreatDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch { }
            strSQL = strSQL.Replace("[CreatDateTime]", " and A.CREATE_DAT <=To_Date('" + strDateTime + "','YYYY-MM-DD hh24:mi:ss') ");
            #endregion
            #endregion
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_Bih_Opr_Putmeddetail_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_Bih_Opr_Putmeddetail_VO();
                        #region ���
                        p_objResultArr[i1].m_strPUTMEDDETAILID_CHR = dtbResult.Rows[i1]["PUTMEDDETAILID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strAREAID_CHR = dtbResult.Rows[i1]["AREAID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPAIENTID_CHR = dtbResult.Rows[i1]["PAIENTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strREGISTERID_CHR = dtbResult.Rows[i1]["REGISTERID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strORDERID_CHR = dtbResult.Rows[i1]["ORDERID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strORDEREXECID_CHR = dtbResult.Rows[i1]["ORDEREXECID_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intORDEREXECTYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["ORDEREXECTYPE_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_intRECIPENO_INT = Convert.ToInt32(dtbResult.Rows[i1]["RECIPENO_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_dblDOSAGE_DEC = double.Parse(dtbResult.Rows[i1]["DOSAGE_DEC"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strDOSAGEUNIT_VCHR = dtbResult.Rows[i1]["DOSAGEUNIT_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCHARGEITEMID_CHR = dtbResult.Rows[i1]["CHARGEITEMID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMEDID_CHR = dtbResult.Rows[i1]["MEDID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMEDNAME_VCHR = dtbResult.Rows[i1]["MEDNAME_VCHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intISRICH_INT = Convert.ToInt32(dtbResult.Rows[i1]["ISRICH_INT"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strDOSETYPEID_CHR = dtbResult.Rows[i1]["DOSETYPEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strEXECFREQID_CHR = dtbResult.Rows[i1]["EXECFREQID_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intEXECTIMES_INT = Convert.ToInt32(dtbResult.Rows[i1]["EXECTIMES_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_intEXECDAYS_INT = Convert.ToInt32(dtbResult.Rows[i1]["EXECDAYS_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_dblUNITPRICE_MNY = double.Parse(dtbResult.Rows[i1]["UNITPRICE_MNY"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strUNIT_VCHR = dtbResult.Rows[i1]["UNIT_VCHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_dblGET_DEC = double.Parse(dtbResult.Rows[i1]["GET_DEC"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strPCHARGEID_CHR = dtbResult.Rows[i1]["PCHARGEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCREATOR_CHR = dtbResult.Rows[i1]["CREATOR_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_strCREATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CREATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_intISPUT_INT = Convert.ToInt32(dtbResult.Rows[i1]["ISPUT_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_intPUTTYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["PUTTYPE_INT"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strPUTMEDREQID_CHR = dtbResult.Rows[i1]["PUTMEDREQID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strEXECTIME_VCHR = dtbResult.Rows[i1]["EXECTIME_VCHR"].ToString().Trim();
                        //���ֶ�
                        p_objResultArr[i1].m_strAreaName = dtbResult.Rows[i1]["AreaName"].ToString().Trim();
                        p_objResultArr[i1].m_strBedID = dtbResult.Rows[i1]["BedID"].ToString().Trim();
                        p_objResultArr[i1].m_strBedNo = dtbResult.Rows[i1]["BedNo"].ToString().Trim();
                        p_objResultArr[i1].m_strPaientName = dtbResult.Rows[i1]["PaientName"].ToString().Trim();
                        p_objResultArr[i1].m_strPaientSex = dtbResult.Rows[i1]["PaientSex"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intPaientAge = Int32.Parse(dtbResult.Rows[i1]["PaientAge"].ToString());
                        }
                        catch { }
                        p_objResultArr[i1].m_strOrderExecTypeName = dtbResult.Rows[i1]["OrderExecTypeName"].ToString().Trim();
                        p_objResultArr[i1].m_strCreatName = dtbResult.Rows[i1]["CreatName"].ToString().Trim();
                        p_objResultArr[i1].m_strPutTypeName = dtbResult.Rows[i1]["PutTypeName"].ToString().Trim();
                        p_objResultArr[i1].m_strUsageName = dtbResult.Rows[i1]["UsageName"].ToString().Trim();
                        p_objResultArr[i1].m_strMedSpec = dtbResult.Rows[i1]["MedSpec"].ToString().Trim();
                        p_objResultArr[i1].m_strASSISTCODE_CHR = dtbResult.Rows[i1]["assistcode"].ToString().Trim();
                        p_objResultArr[i1].m_strFreqName = dtbResult.Rows[i1]["FreqName"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intRATETYPE_INT = Int32.Parse(dtbResult.Rows[i1]["ratetype_int"].ToString());
                        }
                        catch { }
                        #endregion
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
        #endregion
        #region ����
        /// <summary>
        /// ������ҩ��ϸ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID">[out ����]	��ҩ��ϸ��ID</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewPutMedDetail(out string p_strRecordID, clsT_Bih_Opr_Putmeddetail_VO p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = objHRPSvc.lngGenerateID(18, "PUTMEDDETAILID_CHR", "T_Bih_Opr_PutMedDetail", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "INSERT INTO T_Bih_Opr_PutMedDetail (PUTMEDDETAILID_CHR,AREAID_CHR,PAIENTID_CHR,REGISTERID_CHR,ORDERID_CHR,ORDEREXECID_CHR,ORDEREXECTYPE_INT,RECIPENO_INT,DOSAGE_DEC,DOSAGEUNIT_VCHR,CHARGEITEMID_CHR,MEDID_CHR,MEDNAME_VCHR,ISRICH_INT,DOSETYPEID_CHR,EXECFREQID_CHR,EXECTIMES_INT,EXECDAYS_INT,UNITPRICE_MNY,UNIT_VCHR,GET_DEC,PCHARGEID_CHR,CREATOR_CHR,CREATE_DAT,ISPUT_INT,PUTTYPE_INT,PUTMEDREQID_CHR,EXECTIME_VCHR) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(28, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_strRecordID;//p_objRecord.m_strPUTMEDDETAILID_CHR;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strAREAID_CHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strPAIENTID_CHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_strREGISTERID_CHR;
                objLisAddItemRefArr[4].Value = p_objRecord.m_strORDERID_CHR;
                objLisAddItemRefArr[5].Value = p_objRecord.m_strORDEREXECID_CHR;
                objLisAddItemRefArr[6].Value = p_objRecord.m_intORDEREXECTYPE_INT;
                objLisAddItemRefArr[7].Value = p_objRecord.m_intRECIPENO_INT;
                objLisAddItemRefArr[8].Value = p_objRecord.m_dblDOSAGE_DEC;
                objLisAddItemRefArr[9].Value = p_objRecord.m_strDOSAGEUNIT_VCHR;
                objLisAddItemRefArr[10].Value = p_objRecord.m_strCHARGEITEMID_CHR;
                objLisAddItemRefArr[11].Value = p_objRecord.m_strMEDID_CHR;
                objLisAddItemRefArr[12].Value = p_objRecord.m_strMEDNAME_VCHR;
                objLisAddItemRefArr[13].Value = p_objRecord.m_intISRICH_INT;
                objLisAddItemRefArr[14].Value = p_objRecord.m_strDOSETYPEID_CHR;
                objLisAddItemRefArr[15].Value = p_objRecord.m_strEXECFREQID_CHR;
                objLisAddItemRefArr[16].Value = p_objRecord.m_intEXECTIMES_INT;
                objLisAddItemRefArr[17].Value = p_objRecord.m_intEXECDAYS_INT;
                objLisAddItemRefArr[18].Value = p_objRecord.m_dblUNITPRICE_MNY;
                objLisAddItemRefArr[19].Value = p_objRecord.m_strUNIT_VCHR;
                objLisAddItemRefArr[20].Value = p_objRecord.m_dblGET_DEC;
                objLisAddItemRefArr[21].Value = p_objRecord.m_strPCHARGEID_CHR;
                objLisAddItemRefArr[22].Value = p_objRecord.m_strCREATOR_CHR;
                objLisAddItemRefArr[23].Value = DateTime.Parse(strDateTime);//p_objRecord.m_strCREATE_DAT
                objLisAddItemRefArr[24].Value = p_objRecord.m_intISPUT_INT;
                objLisAddItemRefArr[25].Value = p_objRecord.m_intPUTTYPE_INT;
                objLisAddItemRefArr[26].Value = p_objRecord.m_strPUTMEDREQID_CHR;
                objLisAddItemRefArr[27].Value = p_objRecord.m_strEXECTIME_VCHR;
                long lngRecEff = -1;
                //�������Ӽ�¼
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
        #region �޸�
        #region ��ҩ
        /// <summary>
        /// �޸İ�ҩ��ϸ��	��ҩ	û����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">��ҩ��ϸ��ID</param>
        /// <param name="p_intPutType">��ҩ��ʽ{1=������;2=����ϸ(һ��ҩƷ);3=����ϸ(ȷ��ҩ)}</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyNewPutMedDetail(string p_strID, int p_intPutType)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = @"
					UPDATE T_BIH_OPR_PUTMEDDETAIL A 
					SET 
						A.ISPUT_INT = 1
						,A.PUTTYPE_INT=" + p_intPutType.ToString() + " WHERE A.PUTMEDDETAILID_CHR='" + p_strID.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        /// <summary>
        /// �޸İ�ҩ��ϸ��	��ҩ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intPutType">��ҩ��ʽ{1=������;2=����ϸ(һ��ҩƷ);3=����ϸ(ȷ��ҩ)}</param>
        /// <param name="p_objItem">��ҩ��ϸ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyNewPutMedDetail(int p_intPutType, clsT_Bih_Opr_Putmeddetail_VO p_objItem)
        {
            long lngRes = 0;
            if (p_objItem == null || p_objItem.m_strPUTMEDDETAILID_CHR == null || p_objItem.m_strPUTMEDDETAILID_CHR.Trim() == "") return 1;

            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = @"
					UPDATE T_BIH_OPR_PUTMEDDETAIL A 
					SET 
						A.ISPUT_INT = 1
						,A.PUTTYPE_INT=[PUTTYPE_INTVAULE] 
						,PUTMEDREQID_CHR ='[PUTMEDREQID_CHRVAULE]'
						,PUBDATE_DAT =TO_DATE('[PUBDATE_DATVAULE]','YYYY-MM-DD hh24:mi:ss')
					WHERE Trim(A.PUTMEDDETAILID_CHR)='[PUTMEDDETAILID_CHRVAULE]'";
            strSQL = strSQL.Replace("[PUTTYPE_INTVAULE]", p_intPutType.ToString());
            strSQL = strSQL.Replace("[PUTMEDREQID_CHRVAULE]", p_objItem.m_strPUTMEDREQID_CHR.Trim());
            strSQL = strSQL.Replace("[PUBDATE_DATVAULE]", strDateTime.Trim());
            strSQL = strSQL.Replace("[PUTMEDDETAILID_CHRVAULE]", p_objItem.m_strPUTMEDDETAILID_CHR.Trim());
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            /* @add by xzf (05-10-25)
			 * ���¶�Ӧִ�е��ͷ�����ϸ
			 */
            int type = this.getMedChargeControlType();
            string pubMedDetailId = p_objItem.m_strPUTMEDDETAILID_CHR.Trim();
            if (type == 2)
            {
                try
                {
                    bool isCommit = this.updateChargeStatus(pubMedDetailId);
                }
                catch (Exception ex)
                {
                    string strTmp = ex.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(ex);
                }
            }
            /* <<==================================== */
            return lngRes;
        }
        #endregion
        #endregion
        #region ɾ��
        /// <summary>
        /// ɾ����ҩ��ϸ��	����ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">��ҩ��ϸ��ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeletePutMedDetailByID(string p_strID)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "DELETE FROM T_BIH_OPR_PUTMEDDETAIL A WHERE A.PUTMEDDETAILID_CHR='" + p_strID.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        //�ۺϲ�ѯ
        #region ��ȡ��ҩ��ҩƷ��ϸ|������Ϣ	������
        /// <summary>
        ///	��ȡ��ҩ��ҩƷ��ϸ|������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID">����ID</param>
        /// <param name="p_strBedIDArr">����ID����	[����] {��null��Length=0������Ϊ����}</param>
        /// <param name="p_intExecTypeArr">ҽ����������	[����] {��null��Length=0������Ϊ����}	{1����	2����	3�����¿���}</param>
        /// <param name="p_strDoseTypeIDArr">�÷�����	[����] {��null��Length=0������Ϊ����}</param>
        /// <param name="p_intIsRichArr">�Ƿ������Ʒ����	[����] {��null��Length=0������Ϊ����}	{0�ǹ���	1����}</param>
        /// <param name="p_intIsPutMedicineArr">��ҩ����	[����]	{��null��Length=0������Ϊ����} {0=δ��ҩ��1=�Ѱ�ҩ��}</param>
        /// <param name="dtDetailInfo">��ϸ��Ϣ	[out ����]</param>
        /// <param name="dtCollectInfo">������Ϣ	[out ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPutMedicineDetailAndCollectInfo(string p_strAreaID, string[] p_strBedIDArr, int[] p_intExecTypeArr, string[] p_strDoseTypeIDArr, int[] p_intIsRichArr, int[] p_intIsPutMedicineArr, out DataTable dtDetailInfo, out DataTable dtCollectInfo)
        {
            dtDetailInfo = null;
            dtCollectInfo = null;
            long lngRes = 0;
            #region SQL1
            string strSQL = @"
					SELECT a.*
							,(SELECT deptname_vchr FROM t_bse_deptdesc WHERE t_bse_deptdesc.deptid_chr=a.areaid_chr) AreaName
							,(SELECT BedID_Chr FROM T_Opr_Bih_Register WHERE T_Opr_Bih_Register.registerid_chr=a.registerid_chr) BedID
							,(SELECT Code_Chr FROM T_BSE_Bed WHERE T_BSE_Bed.bedid_chr=
								(SELECT BedID_Chr FROM T_Opr_Bih_Register WHERE T_Opr_Bih_Register.registerid_chr=a.registerid_chr)
							) BedNo
							,(SELECT name_vchr FROM T_BSE_Patient WHERE T_BSE_Patient.patientid_chr=a.PaientID_Chr)  PaientName
							,(SELECT Sex_Chr FROM T_BSE_Patient WHERE T_BSE_Patient.patientid_chr=a.PaientID_Chr) PaientSex
							,(SELECT TO_CHAR(SYSDATE,'YYYY')-TO_CHAR(Birth_Dat,'YYYY') FROM T_BSE_Patient WHERE T_BSE_Patient.patientid_chr=a.PaientID_Chr) PaientAge
							,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.creator_chr) CreatName
							,(select UsageName_VChr from T_Bse_UsageType where T_Bse_UsageType.usageid_chr=a.DoseTypeID_Chr)UsageName
							,(select MedSpec_VChr from T_bse_Medicine where T_bse_Medicine.MedicineID_Chr=a.MedID_Chr )MedSpec
							,(select ASSISTCODE_CHR from T_bse_Medicine where T_bse_Medicine.MedicineID_Chr=a.MedID_Chr )ASSISTCODE
							,(select FreqName_Chr from T_Aid_RecipeFreq where T_Aid_RecipeFreq.FreqID_Chr=a.ExecFreqID_Chr)FreqName
							,DECODE(a.ORDEREXECTYPE_INT,1,'����',2,'����',3,'�����¿���','')OrderExecTypeName
							,DECODE(a.PUTTYPE_INT,1,'������',2,'����ϸ(һ��ҩƷ)',3,'����ϸ(ȷ��ҩ)','')PutTypeName
							,0 NO
							,(Trim(A.Get_Dec)||Trim(A.Unit_Vchr)) GetUnit
							,decode(A.IsRich_Int,0,'',1,'��','')IsRichName
							,decode(A.IsPut_Int,0, '',1,'��','')IsPutName
					FROM t_bih_opr_putmeddetail a
					WHERE 1=1 
						[OrderExecTypeCondition]
						[DoseTypeCondition]
						[IsRichCondition]
						[AlreadyPutCondition]	
						[AreaIDCondition]	
					ORDER BY ISPUT_INT DESC,BedNo";
            #endregion
            #region ��ѯ����
            //[OrderExecTypeCondition]	ҽ����������
            #region ҽ����������
            if ((p_intExecTypeArr == null) || (p_intExecTypeArr.Length <= 0))
            {
                strSQL = strSQL.Replace("[OrderExecTypeCondition]", "");
            }
            else
            {
                string strTem = "";
                for (int i = 0; i < p_intExecTypeArr.Length; i++)
                {
                    if (i > 0) strTem += ",";
                    strTem += p_intExecTypeArr[i].ToString();
                }
                if (strTem.Trim() != "")
                    strSQL = strSQL.Replace("[OrderExecTypeCondition]", " and A.OrderExecType_Int in ( " + strTem + " )");
                else
                    strSQL = strSQL.Replace("[OrderExecTypeCondition]", "");
            }
            #endregion
            //[DoseTypeCondition]		�÷�����
            #region �÷�����
            if ((p_strDoseTypeIDArr == null) || (p_strDoseTypeIDArr.Length <= 0))
            {
                strSQL = strSQL.Replace("[DoseTypeCondition]", "");
            }
            else
            {
                string strTem = "";
                for (int i = 0; i < p_strDoseTypeIDArr.Length; i++)
                {
                    if (p_strDoseTypeIDArr[i].Trim() != "")
                    {
                        if (i > 0) strTem += ",";
                        strTem += "'" + p_strDoseTypeIDArr[i].Trim() + "'";
                    }
                }
                if (strTem.Trim() != "")
                    strSQL = strSQL.Replace("[DoseTypeCondition]", " and A.DoseTypeID_Chr in ( " + strTem + " )");
                else
                    strSQL = strSQL.Replace("[DoseTypeCondition]", "");
            }
            #endregion
            //[IsRichCondition]			��������
            #region ��������
            if ((p_intIsRichArr == null) || (p_intIsRichArr.Length <= 0))
            {
                strSQL = strSQL.Replace("[IsRichCondition]", "");
            }
            else
            {
                string strTem = "";
                for (int i = 0; i < p_intIsRichArr.Length; i++)
                {
                    if (i > 0) strTem += ",";
                    strTem += p_intIsRichArr[i].ToString();
                }
                if (strTem.Trim() != "")
                    strSQL = strSQL.Replace("[IsRichCondition]", " and A.IsRich_Int in ( " + strTem + " )");
                else
                    strSQL = strSQL.Replace("[IsRichCondition]", "");
            }
            #endregion
            //[AlreadyPutCondition]		��ҩ����
            #region ��ҩ����
            if ((p_intIsPutMedicineArr == null) || (p_intIsPutMedicineArr.Length <= 0))
            {
                strSQL = strSQL.Replace("[AlreadyPutCondition]", "");
            }
            else
            {
                string strTem = "";
                for (int i = 0; i < p_intIsPutMedicineArr.Length; i++)
                {
                    if (i > 0) strTem += ",";
                    strTem += p_intIsPutMedicineArr[i].ToString();
                }
                if (strTem.Trim() != "")
                    strSQL = strSQL.Replace("[AlreadyPutCondition]", " and A.IsPut_Int in ( " + strTem + " )");
                else
                    strSQL = strSQL.Replace("[AlreadyPutCondition]", "");
            }
            #endregion
            //[AreaIDCondition]			��������
            #region ��������
            strSQL = strSQL.Replace("[AreaIDCondition]", " and Trim(A.areaid_chr)='" + p_strAreaID.Trim() + "'");
            #endregion
            //[BedIDCondition]		�������� [BedIDCondition]	{���� Where}
            #region ��������
            if ((p_strBedIDArr != null && p_strBedIDArr.Length > 0))
            {
                string strTem = "";
                for (int i = 0; i < p_strBedIDArr.Length; i++)
                {
                    if (p_strBedIDArr[i].Trim() != "")
                    {
                        if (i > 0) strTem += ",";
                        strTem += "'" + p_strBedIDArr[i].Trim() + "'";
                    }
                }
                if (strTem.Trim() != "")
                {
                    strSQL = "SELECT a.* FROM (" + strSQL + ") a WHERE A.BedID IN ( " + strTem + " )";
                }
            }
            #endregion
            #endregion
            #region SQL2
            string strSQL2 = @"
				SELECT	0 NO,MEDID_CHR,Max(MEDNAME_VCHR) MEDNAME_VCHR,MedSpec
						,sum(GET_DEC) GET_DEC,Max(UNIT_VCHR) UNIT_VCHR,(sum(GET_DEC)||Trim(Max(Unit_Vchr))) GetUnit
						,UNITPRICE_MNY,sum(GET_DEC*UNITPRICE_MNY) GetMoney
				FROM ( [SQL1STRING] )
				GROUP BY MEDID_CHR,MedSpec,UNITPRICE_MNY
				";
            strSQL2 = strSQL2.Replace("[SQL1STRING]", strSQL);
            #endregion
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtDetailInfo);
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL2, ref dtCollectInfo);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        /// <summary>
        ///	��ȡ��ҩ��ҩƷ��ϸ|������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID">����ID</param>
        /// <param name="p_strBeginBed">��ʼ����</param>
        /// <param name="p_strEndBed">��������</param>
        /// <param name="p_intExecTypeArr">ҽ����������	[����] {��null��Length=0������Ϊ����}	{1����	2����	3�����¿���}</param>
        /// <param name="p_strDoseTypeIDArr">�÷�����	[����] {��null��Length=0������Ϊ����}</param>
        /// <param name="p_intIsRichArr">�Ƿ������Ʒ����	[����] {��null��Length=0������Ϊ����}	{0�ǹ���	1����}</param>
        /// <param name="p_intIsPutMedicineArr">��ҩ����	[����]	{��null��Length=0������Ϊ����} {0=δ��ҩ��1=�Ѱ�ҩ��}</param>
        /// <param name="dtDetailInfo">��ϸ��Ϣ	[out ����]</param>
        /// <param name="dtCollectInfo">������Ϣ	[out ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPutMedicineDetailAndCollectInfo(string p_strAreaID, string p_strBeginBed, string p_strEndBed, int[] p_intExecTypeArr, string[] p_strDoseTypeIDArr,
            int[] p_intIsRichArr, int[] p_intIsPutMedicineArr, out DataTable dtDetailInfo, out DataTable dtCollectInfo)
        {
            dtDetailInfo = null;
            dtCollectInfo = null;
            long lngRes = 0;
            #region SQL1 
            string strSQL = @"
			select  0 NO,A.MedID_Chr MedNo,A.MedName_VChr MedName,A.UnitPrice_Mny Price,A.Get_Dec Get ,A.Unit_Vchr Unit,A.PutMedDetailID_Chr ID
					,decode(A.OrderExecType_Int,1,'����',2,'��ʱ',3,'�����¿���','') ExecType
					,decode(A.IsRich_Int,0,'',1,'��','')IsRich
					,decode(A.IsPut_Int,0, '',1,'��','')IsPut
					,decode(A.PutType_Int,0,'',1,'������',2,'����ϸ(һ��ҩ)',3,'����ϸ(ȷ��ҩ)','')PutType
					,(SELECT name_vchr FROM T_BSE_Patient WHERE T_BSE_Patient.patientid_chr=a.PaientID_Chr) PatientName
					,(SELECT Sex_Chr FROM T_BSE_Patient WHERE T_BSE_Patient.patientid_chr=a.PaientID_Chr) PatientSex
					,(SELECT TO_CHAR(SYSDATE,'YYYY')-TO_CHAR(Birth_Dat,'YYYY') FROM T_BSE_Patient WHERE T_BSE_Patient.patientid_chr=a.PaientID_Chr) PatientAge
					,C.BedName BedNo
					,(select UsageName_VChr from T_Bse_UsageType where T_Bse_UsageType.usageid_chr=a.DoseTypeID_Chr)MedUsage
					,(select MedSpec_VChr from T_bse_Medicine where T_bse_Medicine.MedicineID_Chr=a.MedID_Chr )MedSpec
					,(select ASSISTCODE_CHR from T_bse_Medicine where T_bse_Medicine.MedicineID_Chr=a.MedID_Chr )ASSISTCODE
					,(select FreqName_Chr from T_Aid_RecipeFreq where T_Aid_RecipeFreq.FreqID_Chr=a.ExecFreqID_Chr)MedFreq
					,A.*
			from  T_bih_opr_putMedDetail A,
				  (select TA.RegisterID_Chr,TB.BedID_Chr,TB.Code_Chr BedName 
			       from T_Opr_Bih_Register TA,T_BSE_Bed TB
				   where TA.AreaID_Chr=TB.AreaID_Chr and TA.BedID_Chr=TB.BedID_Chr
				   [PatientCondition]
				  )C	
			where A.RegisterID_Chr=C.RegisterID_Chr 
				[OrderExecTypeCondition]
				[DoseTypeCondition]
				[IsRichCondition]
				[AlreadyPutCondition]		
			ORDER BY A.ISPUT_INT DESC,BedNo";
            #endregion
            #region ��ѯ����
            //[PatientCondition]		����λ������
            #region ����λ������
            string strPatientCondition = new com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService().m_strGetPatientsSQL("TB", p_strAreaID, p_strBeginBed, p_strEndBed);
            strSQL = strSQL.Replace("[PatientCondition]", strPatientCondition);
            #endregion
            //[OrderExecTypeCondition]	ҽ����������
            #region ҽ����������
            if ((p_intExecTypeArr == null) || (p_intExecTypeArr.Length <= 0))
            {
                strSQL = strSQL.Replace("[OrderExecTypeCondition]", "");
            }
            else
            {
                string strTem = "";
                for (int i = 0; i < p_intExecTypeArr.Length; i++)
                {
                    if (i > 0) strTem += ",";
                    strTem += p_intExecTypeArr[i].ToString();
                }
                if (strTem.Trim() != "")
                    strSQL = strSQL.Replace("[OrderExecTypeCondition]", " and A.OrderExecType_Int in ( " + strTem + " )");
                else
                    strSQL = strSQL.Replace("[OrderExecTypeCondition]", "");
            }
            #endregion
            //[DoseTypeCondition]		�÷�����
            #region �÷�����
            if ((p_strDoseTypeIDArr == null) || (p_strDoseTypeIDArr.Length <= 0))
            {
                strSQL = strSQL.Replace("[DoseTypeCondition]", "");
            }
            else
            {
                string strTem = "";
                for (int i = 0; i < p_strDoseTypeIDArr.Length; i++)
                {
                    if (p_strDoseTypeIDArr[i].Trim() != "")
                    {
                        if (i > 0) strTem += ",";
                        strTem += "'" + p_strDoseTypeIDArr[i].Trim() + "'";
                    }
                }
                if (strTem.Trim() != "")
                    strSQL = strSQL.Replace("[DoseTypeCondition]", " and A.DoseTypeID_Chr in ( " + strTem + " )");
                else
                    strSQL = strSQL.Replace("[DoseTypeCondition]", "");
            }
            #endregion
            //[IsRichCondition]			��������
            #region ��������
            if ((p_intIsRichArr == null) || (p_intIsRichArr.Length <= 0))
            {
                strSQL = strSQL.Replace("[IsRichCondition]", "");
            }
            else
            {
                string strTem = "";
                for (int i = 0; i < p_intIsRichArr.Length; i++)
                {
                    if (i > 0) strTem += ",";
                    strTem += p_intIsRichArr[i].ToString();
                }
                if (strTem.Trim() != "")
                    strSQL = strSQL.Replace("[IsRichCondition]", " and A.IsRich_Int in ( " + strTem + " )");
                else
                    strSQL = strSQL.Replace("[IsRichCondition]", "");
            }
            #endregion
            //[AlreadyPutCondition]		��ҩ����
            #region ��ҩ����
            if ((p_intIsPutMedicineArr == null) || (p_intIsPutMedicineArr.Length <= 0))
            {
                strSQL = strSQL.Replace("[AlreadyPutCondition]", "");
            }
            else
            {
                string strTem = "";
                for (int i = 0; i < p_intIsPutMedicineArr.Length; i++)
                {
                    if (i > 0) strTem += ",";
                    strTem += p_intIsPutMedicineArr[i].ToString();
                }
                if (strTem.Trim() != "")
                    strSQL = strSQL.Replace("[AlreadyPutCondition]", " and A.IsPut_Int in ( " + strTem + " )");
                else
                    strSQL = strSQL.Replace("[AlreadyPutCondition]", "");
            }
            #endregion
            #endregion
            #region SQL2
            string strSQL2 = @"
				select 0 NO,MedID_Chr MedNo,MedName,MedSpec,sum(Get_Dec) Get, Unit, Price,sum(Get_Dec)*UnitPrice_Mny GetMoney
				from ( [SQL1STRING] ) TA
				group by MedID_Chr,MedName,MedSpec,Unit_VChr,UnitPrice_Mny
				";
            strSQL2 = strSQL2.Replace("[SQL1STRING]", strSQL);
            #endregion
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtDetailInfo);
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL2, ref dtCollectInfo);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        //����
        #region ��ҩ
        /// <summary>
        /// ��ҩ����	ֻ����ͬһ����ҩ���İ�ҩ
        /// ҵ��˵����
        ///			1���޸�ҽ����ҩ��ϸ����	��ҩ���뵥id����־Ϊ��ҩ	if(�Ѿ��ڹ�)  then ��ҩʧ��
        ///			2��if(ִ�е��ڵ�ҩȫ��������) then �޸�ҽ��ִ�е�--�ѽ���	{1/0}
        ///			3��(�������߼����ж�)if(��ҩ���뵥�ڵ�ҩȫ��������) then �޸Ĵ���״̬	{0=δ����;1=�ѷ���;2=���ַ�ҩ;3=�ѷ�ҩ};
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intPutType">��ҩ��ʽ	{1=������;2=����ϸ(һ��ҩƷ);3=����ϸ(ȷ��ҩ)}</param>
        /// <param name="p_strHandlersID">��ҩ��ID</param>
        /// <param name="p_strHandlersName">��ҩ������</param>
        /// <param name="p_blnIsChangePutMedReq">�Ƿ�ı��ҩ���뵥״̬</param>
        /// <param name="p_strPUTMEDREQID_CHR">��ҩ��ID</param>
        /// <param name="p_objItemArr">��ҩ��ϸ������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngPutMedicine(int p_intPutType, string p_strHandlersID, string p_strHandlersName, bool p_blnIsChangePutMedReq, string p_strPUTMEDREQID_CHR, clsT_Bih_Opr_Putmeddetail_VO[] p_objItemArr)
        {
            long lngRes = 0;
            if (p_objItemArr == null || p_objItemArr.Length <= 0) return 1;

            #region ��������
            //�����Ѿ�������� �ڶ�,������;
            System.Collections.Hashtable htbWorked = new System.Collections.Hashtable();
            clsT_Bih_Opr_Putmeddetail_VO[] objTemArr;
            lngRes = 1;
            #endregion

            //1���޸�ҽ����ҩ��ϸ����	��ҩ���뵥id����־Ϊ��ҩ
            #region
            for (int i1 = 0; i1 < p_objItemArr.Length; i1++)
            {
                //if(�Ѿ��ڹ�)  then ��ҩʧ��
                if (p_objItemArr[i1].m_intISPUT_INT == 1) lngRes = -1;
                if (lngRes > 0 && p_objItemArr[i1] != null)
                {
                    lngRes = m_lngModifyNewPutMedDetail(p_intPutType, p_objItemArr[i1]);
                }
            }
            #endregion

            //2��if(ִ�е��ڵ�ҩȫ��������) then �޸�ҽ��ִ�е�--�ѽ���	{1/0}
            #region
            com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objManage = new com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService();
            htbWorked = new System.Collections.Hashtable();
            for (int i1 = 0; i1 < p_objItemArr.Length; i1++)
            {
                if (lngRes > 0 && p_objItemArr[i1] != null)
                {
                    if (!htbWorked.ContainsKey(p_objItemArr[i1].m_strORDEREXECID_CHR.Trim()))
                    {
                        //��ȡ��ִ�е���û�а�ҩ�Ķ���
                        lngRes = m_lngGetPutMedDetailByOrderExecID(0, p_objItemArr[i1].m_strORDEREXECID_CHR, out objTemArr);
                        if (lngRes > 0 && (objTemArr == null || objTemArr.Length <= 0))
                        {
                            lngRes = objManage.m_lngModifyOrderExecuteStatus(1, p_strHandlersID, p_strHandlersName, p_objItemArr[i1].m_strORDEREXECID_CHR);
                            htbWorked.Add(p_objItemArr[i1].m_strORDEREXECID_CHR.Trim(), "Worked");
                        }
                    }
                }
            }
            objManage.Dispose();
            #endregion

            //3��if(��ҩ���뵥�ڵ�ҩȫ��������) then �޸Ĵ���״̬	{0=δ����;1=�ѷ���;2=���ַ�ҩ;3=�ѷ�ҩ};
            if (lngRes > 0 && p_blnIsChangePutMedReq)
            {
                lngRes = m_lngModifyPutMedReqStatus(3, p_strHandlersID, p_strPUTMEDREQID_CHR);
            }
            else
            {
                lngRes = m_lngModifyPutMedReqStatus(2, p_strHandlersID, p_strPUTMEDREQID_CHR);
            }
            #region
            //			htbWorked =new System.Collections.Hashtable();
            //			for(int i1=0;i1<p_objItemArr.Length;i1++)
            //			{
            //				if(lngRes>0 && p_objItemArr[i1]!=null)
            //				{
            //					if(!htbWorked.ContainsKey(p_objItemArr[i1].m_strPUTMEDREQID_CHR.Trim()))
            //					{
            //						//��ȡ�ð�ҩ���뵥��û�а�ҩ�Ķ���
            //						lngRes =m_lngGetPutMedDetailByPutMedReqID(p_objPrincipal,0,p_objItemArr[i1].m_strPUTMEDREQID_CHR,out objTemArr);
            //						if(lngRes>0 && (objTemArr==null || objTemArr.Length<=0))
            //						{
            //							lngRes =m_lngModifyPutMedReqStatus(p_objPrincipal,3,p_strHandlersID,p_objItemArr[i1].m_strPUTMEDREQID_CHR);
            //							htbWorked.Add(p_objItemArr[i1].m_strPUTMEDREQID_CHR.Trim(),"Worked");
            //						}
            //					}
            //				}
            //			}
            #endregion

            if (lngRes <= 0)
            {
                throw (new System.Exception("��ҩʧ��!"));
            }
            return lngRes;
        }

        #region �÷�������ʹ�� 20180321
        /// <summary>
		/// ��ҩ����	���ݰ�ҩ��ID  ����
		/// �ر�ע�⣺	����p_intRunType������
		/// ҵ��˵����
		///			1����ȡ�ð�ҩ���뵥��û�а�ҩ�Ķ���
		///			2����ҩ
		///			3���޸Ĵ���״̬	{0=δ����;1=�ѷ���;2=���ַ�ҩ;3=�ѷ�ҩ};
		///			4��if(ִ�е��ڵ�ҩȫ��������) then �޸�ҽ��ִ�е�--�ѽ���	{1/0}
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_intRunType">ִ������	{0=�ڡ�ȫ����ҩ��ID֮�е�δ�ڵ�ҩ����1=��⡰��ҩ��ID�Ƿ���δ�ڵ�ҩ��if(��) then ��ҩʧ�� else �޸İ�ҩ��Ϊ��1=�ѷ��͡���}</param>
		/// <param name="p_intPutType">��ҩ��ʽ	{1=������;2=����ϸ(һ��ҩƷ);3=����ϸ(ȷ��ҩ)}</param>
		/// <param name="p_strHandlersID">��ҩ��ID</param>
		/// <param name="p_strHandlersName">��ҩ������</param>
		/// <param name="p_strPUTMEDREQID_CHR">��ҩ��ID</param>
		/// <returns></returns>
        //[AutoComplete]
        //public long m_lngPutMedicine(System.Security.Principal.IPrincipal p_objPrincipal,int p_intRunType,int p_intPutType,string p_strHandlersID,string p_strHandlersName,clsT_Bih_Opr_PutMedReq_VO p_objPutMedReq,string p_strPUTMEDREQID_CHR)
        //{
        //    long lngRes=0;
        //    clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //    lngRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal,"com.digitalwave.iCare.middletier.LIS.clsLisDeviceSvc","m_lngAddNewPutMedDetail");
        //    if(lngRes < 0)
        //    {
        //        return -1;
        //    }

        //    clsT_Bih_Opr_Putmeddetail_VO[] objItemArr;

        //    //1����ȡ�ð�ҩ���뵥��û�а�ҩ�Ķ���
        //    #region 
        //    lngRes =m_lngGetPutMedDetailByPutMedReqID(p_objPrincipal,0,p_strPUTMEDREQID_CHR,out objItemArr);
        //    #endregion

        //    //2����ҩ
        //    if(p_intRunType==0 && objItemArr!=null && objItemArr.Length>0)
        //    {
        //        #region
        //        for(int i1=0;i1<objItemArr.Length;i1++)
        //        {
        //            //if(�Ѿ��ڹ�)  then ��ҩʧ��
        //            if(objItemArr[i1].m_intISPUT_INT==1) lngRes =-1;
        //            if(lngRes>0 && objItemArr[i1]!=null)
        //            {
        //                lngRes =m_lngModifyNewPutMedDetail(p_objPrincipal,p_intPutType,objItemArr[i1]);
        //            }
        //        }
        //        #endregion
        //    }

        //    //3���޸Ĵ���״̬	{0=δ����;1=�ѷ���;2=���ַ�ҩ;3=�ѷ�ҩ};
        //    if(p_intRunType==0)
        //    {
        //        #region
        //        if(lngRes>0)
        //        {
        //            lngRes =m_lngModifyPutMedReqStatus(p_objPrincipal,1,p_strHandlersID,p_strPUTMEDREQID_CHR);
        //        }
        //        #endregion
        //    }
        //    else if(p_intRunType==1 && (objItemArr==null || objItemArr.Length<=0))
        //    {
        //        #region
        //        if(lngRes>0)
        //        {
        //            lngRes =m_lngModifyPutMedReqStatus(p_objPrincipal,1,p_strHandlersID,p_strPUTMEDREQID_CHR);
        //        }
        //        #endregion
        //    }

        //    //4��if(ִ�е��ڵ�ҩȫ��������) then �޸�ҽ��ִ�е�--�ѽ���	{1/0}
        //    if(objItemArr!=null && objItemArr.Length>0)//p_intRunType==0 && 
        //    {
        //        #region
        //        com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objManage =new com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService(); 
        //        System.Collections.Hashtable htbWorked =new System.Collections.Hashtable();
        //        clsT_Bih_Opr_Putmeddetail_VO[] objOrderExecArr;
        //        for(int i1=0;i1<objItemArr.Length;i1++)
        //        {
        //            if(lngRes>0 && objItemArr[i1]!=null)
        //            {					
        //                if(!htbWorked.ContainsKey(objItemArr[i1].m_strORDEREXECID_CHR.Trim()))//����û�д������
        //                {
        //                    //��ȡ��ִ�е���û�а�ҩ�Ķ���
        //                    lngRes =m_lngGetPutMedDetailByOrderExecID(p_objPrincipal,0,objItemArr[i1].m_strORDEREXECID_CHR,out objOrderExecArr);
        //                    if(lngRes>0 && (objOrderExecArr==null || objOrderExecArr.Length<=0))
        //                    {
        //                        lngRes =objManage.m_lngModifyOrderExecuteStatus(p_objPrincipal,1,p_strHandlersID,p_strHandlersName,objItemArr[i1].m_strORDEREXECID_CHR);
        //                        htbWorked.Add(objItemArr[i1].m_strORDEREXECID_CHR.Trim(),"Worked");
        //                    }
        //                }
        //            }
        //        }	
        //        objManage.Dispose();
        //        #endregion
        //    }

        //    if(lngRes<=0)
        //    {
        //        throw(new System.Exception("��ҩʧ��!"));
        //    }
        //    return lngRes;
        //}
        #endregion

        #endregion
        #region	����
        /// <summary>
		/// ������ҩ��	[����]
		/// ҵ��˵��: 
		///		1���ѷ���״̬��		ɾ��û��δ��ҩ�İ�ҩ����
		///		2�����ݷ�ҩ״̬��	�޸�û��δ��ҩ�İ�ҩ��״̬Ϊ���԰�ҩ��״̬��
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_objItemArr">[����]��ҩ������</param>
		/// <param name="p_strProcessor">������	{=��Ա.id}</param>
		/// <returns></returns>
		[AutoComplete]
        public long m_lngBlankOutPutMedReq(clsT_Bih_Opr_PutMedReq_VO[] p_objItemArr, string p_strProcessor)
        {
            long lngRes = 0;

            for (int i1 = 0; i1 < p_objItemArr.Length; i1++)
            {
                bool blnExist = false;
                //����Ƿ����δ��ҩ�ļ�¼
                if (lngRes > 0)
                {
                    lngRes = lngExistNotPutMed(p_objItemArr[i1], out blnExist);
                }
                //���ϰ�ҩ��
                if (lngRes > 0 && (!blnExist))
                {
                    //����״̬{0=δ����;1=�ѷ���;2=���ַ�ҩ;3=�ѷ�ҩ};if(this==2) ������������
                    switch (p_objItemArr[i1].m_intPSTATUS_INT)
                    {
                        case 1://�ѷ���
                            lngRes = m_lngDeletePutMedReqByID(p_strProcessor, p_objItemArr[i1].m_strPUTMEDREQID_CHR);
                            break;
                        case 2://���ְ�ҩ
                            lngRes = m_lngModifyPutMedReqStatus(3, p_strProcessor, p_objItemArr[i1].m_strPUTMEDREQID_CHR);
                            break;
                    }
                }
            }
            if (lngRes <= 0)
            {
                throw (new Exception("����ʧ�ܣ�"));
            }
            return lngRes;
        }
        /// <summary>
        /// ����Ƿ����δ��ҩ�ļ�¼
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objItem">��ҩ������</param>
        /// <param name="p_blnExist">[out]	����</param>
        /// <returns></returns>
        [AutoComplete]
        private long lngExistNotPutMed(clsT_Bih_Opr_PutMedReq_VO p_objItem, out bool p_blnExist)
        {
            long lngRes = 0;
            p_blnExist = false;

            #region SQL
            string strSQL = @"
					SELECT COUNT (*)
					FROM t_bih_opr_putmeddetail a
					WHERE (a.isput_int = NULL OR a.isput_int = 0)
					AND a.create_dat <= TO_DATE ('[CreatDateTime]', 'YYYY-MM-DD hh24:mi:ss')
					[AreaIDCondition]
					[OrderExecTypeCondition]
					[DoseTypeCondition]
					[IsRichCondition]
					[BedIDCondition]";
            #endregion
            #region ��ѯ����
            //[OrderExecTypeCondition]	ҽ����������
            #region ҽ����������
            if (p_objItem.m_strEXECTYPE_VCHR == null || p_objItem.m_strEXECTYPE_VCHR.Trim() == "")
            {
                strSQL = strSQL.Replace("[OrderExecTypeCondition]", "");
            }
            else
            {
                strSQL = strSQL.Replace("[OrderExecTypeCondition]", " AND TRIM (a.orderexectype_int) IN (" + p_objItem.m_strEXECTYPE_VCHR + ") ");
            }
            #endregion
            //[DoseTypeCondition]		�÷�����
            #region �÷�����
            if ((p_objItem.m_strUSAGETYPE_VCHR == null) || (p_objItem.m_strUSAGETYPE_VCHR == ""))
            {
                strSQL = strSQL.Replace("[DoseTypeCondition]", "");
            }
            else
            {
                strSQL = strSQL.Replace("[DoseTypeCondition]", " AND TRIM (a.dosetypeid_chr) IN (" + p_objItem.m_strUSAGETYPE_VCHR + ") ");
            }
            #endregion
            //[IsRichCondition]			��������
            #region ��������
            if ((p_objItem.m_strRICHTYPE_VCHR == null) || (p_objItem.m_strRICHTYPE_VCHR == ""))
            {
                strSQL = strSQL.Replace("[IsRichCondition]", "");
            }
            else
            {
                strSQL = strSQL.Replace("[IsRichCondition]", " AND TRIM (a.isrich_int) IN (" + p_objItem.m_strRICHTYPE_VCHR + ") ");
            }
            #endregion
            //[AreaIDCondition]			��������
            #region ��������
            strSQL = strSQL.Replace("[AreaIDCondition]", " AND TRIM (a.areaid_chr) = '" + p_objItem.m_strAREAID_CHR + "' ");
            #endregion
            //[BedIDCondition]			�������� [BedIDCondition]	{���� Where}
            #region ��������
            if ((p_objItem.m_strBEDBEGIN_VCHR == null) || (p_objItem.m_strBEDBEGIN_VCHR == ""))
            {
                strSQL = strSQL.Replace("[BedIDCondition]", "");
            }
            else
            {
                string strTem = @"
							AND EXISTS (SELECT b1.registerid_chr
								FROM t_opr_bih_register b1
							WHERE TRIM (b1.bedid_chr) IN ([BedIDCondition])
								AND TRIM (b1.registerid_chr) = TRIM (a.registerid_chr))";
                strTem = strTem.Replace("[BedIDCondition]", p_objItem.m_strBEDBEGIN_VCHR);
                strSQL = strSQL.Replace("[BedIDCondition]", strTem);
            }
            #endregion
            //[CreatDateTime]			���뵥����ʱ��
            #region ����ʱ��
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (p_objItem.m_strCREATE_DAT.Trim() != "") strDateTime = p_objItem.m_strCREATE_DAT;
            strSQL = strSQL.Replace("[CreatDateTime]", strDateTime);
            #endregion
            #endregion
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    if (dtbResult.Rows[0][0].ToString().Trim() != "0") p_blnExist = true;
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        /** @add by xzf (05-10-24)
		 * ��ȡҩƷ(������Ŀ����)�ķ�����Ч���Ʒ�ʽ{1=ҽ��ִ��ʱ;2=ִ�п���ȷ��ʱ}
		 */
        [AutoComplete]
        public int getMedChargeControlType()
        {
            int type = 1; //��Ч��ʽ{1/2},Ĭ��Ϊ1;
            long lngRes = -1;
            DataTable dt = null;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = null;
            string strSql = @"SELECT createchargetype FROM t_aid_bih_ordercate WHERE ordercateid_chr="
                 + "(SELECT ordercateid_medicine_chr FROM t_bse_bih_specordercate)";
            try
            {
                objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSql, ref dt);
                objHRPSvc.Dispose();
                /* ��ȡֵ */
                if (dt.Rows.Count > 0)
                {
                    type = Convert.ToInt32(dt.Rows[0][0]);
                }
            }
            catch (Exception ex)
            {
                string strTmp = ex.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(ex);
            }
            return type;
        }

        /** @add by xzf(05-10-24)
		 * ���ݰ�ҩ��ϸ��id,�������Ӧ��ִ�е�״̬,������ϸ��״̬
		 */
        [AutoComplete]
        public bool updateChargeStatus(string pubMedDetailId)
        {
            bool isCommit = false;
            long lngRes = -1;
            DataTable dt = null;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = null;
            string strSql = @"select ORDEREXECID_CHR,CHARGEITEMID_CHR from T_BIH_OPR_PUTMEDDETAIL"
                + " where PUTMEDDETAILID_CHR='" + pubMedDetailId + "'";
            string orderExecId = ""; //��ҩ��ϸ����Ӧ��ҽ��ִ�е�id
            string chargeItemId = ""; //��ҩ��ϸ����Ӧ�Ĳ����շ���ϸid
            try
            {
                objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSql, ref dt);
                objHRPSvc.Dispose();
                /* ��ȡ����ֵ */
                if (dt.Rows.Count > 0)
                {
                    isCommit = false;
                    return isCommit;
                }
                orderExecId = dt.Rows[0][0].ToString().Trim();
                chargeItemId = dt.Rows[0][0].ToString().Trim();
            }
            catch (Exception ex)
            {
                string strTmp = ex.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(ex);
            }
            try
            {
                /* ���¶�Ӧ��ҽ��ִ�е��Ͳ��˷�����ϸ */
                strSql = @"update T_OPR_BIH_ORDEREXECUTE"
                    + " set ISCHARGE_INT=1,ISINCEPT_INT=1"
                    + " where ORDEREXECID_CHR='" + orderExecId + "'";
                objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSql);
                strSql = @"update T_OPR_BIH_PATIENTCHARGE"
                    + "set PSTATUS_INT=1,ACTIVE_DAT=sysDate"
                    + " where ORDEREXECID_CHR='" + "' and CHARGEITEMID_CHR='" + chargeItemId + "'";
                lngRes = objHRPSvc.DoExcute(strSql);
                objHRPSvc.Dispose();
                isCommit = true;
            }
            catch (Exception ex1)
            {
                string strTmp = ex1.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(ex1);
            }
            return isCommit;
        }
        /* <<============================================ */


        #region ��������ѯ��ҩ��� @add by wjqin (05-12-2)
        /// <summary>
        /// ���ݲ���סԺ����,סԺ���ţ�ҽ��ִ��ʱ�䣨��ҩʱ��) �õ��ò��˵�������ҩ��� 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID"></param>
        /// <param name="p_strBedIDs"></param>
        /// <param name="p_dtBegin"></param>
        /// <param name="p_dtEnd"></param>
        /// <param name="BMSTATUS_INT"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBackMedDetail(string p_strREGISTERID_CHR, string p_strAreaID, string p_strBedIDs, System.DateTime p_dtBegin, System.DateTime p_dtEnd, int BMSTATUS_INT, out DataTable p_objResultArr)
        {
            p_objResultArr = new DataTable();
            long lngRes = 0;

            /*
			string strSQL = @"
			SELECT 
                    a.PCHARGEID_CHR,
					a.patientid_chr,
					(SELECT LASTNAME_VCHR FROM T_BSE_PATIENT WHERE T_BSE_PATIENT.patientid_chr=a.patientid_chr)  PATIENT_NAME,
					a.CHARGEITEMID_CHR,
					a.CHARGEITEMNAME_CHR,
					a.AMOUNT_DEC,
					(SELECT CODE_CHR FROM T_BSE_BED WHERE BEDID_CHR=(SELECT BEDID_CHR FROM T_Opr_Bih_Register WHERE T_OPR_BIH_REGISTER.registerid_chr=a.registerid_chr AND ROWNUM=1)) bedname
					from 
					T_OPR_BIH_PATIENTCHARGE  a,
					T_BSE_CHARGEITEM  b,
					T_BSE_MEDICINE  c 

					WHERE 
					a.CHARGEITEMID_CHR=b.ITEMID_CHR 
					AND b.ITEMID_CHR=c.MEDICINEID_CHR
					AND a.amount_dec<0
                    [GetBMSTATUS_INT]
					AND 
                    [EXISTS] (SELECT BEDID_CHR FROM T_BSE_BED WHERE BEDID_CHR=(SELECT BEDID_CHR FROM T_Opr_Bih_Register WHERE T_OPR_BIH_REGISTER.registerid_chr=a.registerid_chr AND ROWNUM=1 [GetAREAID_CHR] [GetINPATIENT_DAT]))  [GetCODE_CHR]
                  ";
			*/

            string strSQL = @"
			SELECT 
					a.PCHARGEID_CHR,
                    a.BMSTATUS_INT,
					a.patientid_chr,
					(SELECT LASTNAME_VCHR FROM T_BSE_PATIENT WHERE T_BSE_PATIENT.patientid_chr=a.patientid_chr)  PATIENT_NAME,
					a.CHARGEITEMID_CHR,
					a.CHARGEITEMNAME_CHR,
					a.AMOUNT_DEC,
                    (select a.lastname_vchr  from t_bse_employee a where  trim(a.empid_chr)=trim(a.CREATOR_CHR)) creater,
                    a.CREATE_DAT,
                    b.ITEMIPUNIT_CHR,
                    b.ITEMPRICE_MNY,
                    (a.AMOUNT_DEC*b.ITEMPRICE_MNY) sumprice,
					(SELECT CODE_CHR FROM T_BSE_BED WHERE BEDID_CHR=(SELECT BEDID_CHR FROM T_Opr_Bih_Register WHERE T_OPR_BIH_REGISTER.registerid_chr=a.registerid_chr AND ROWNUM=1)) bedname
					from 
					T_OPR_BIH_PATIENTCHARGE  a,
					T_BSE_CHARGEITEM  b,
					T_BSE_MEDICINE  c 

					WHERE 
					a.CHARGEITEMID_CHR=b.ITEMID_CHR 
					AND b.ITEMID_CHR=c.MEDICINEID_CHR
					AND a.amount_dec<0
                    [REGISTERID_CHR]
                    [CREATE_DAT]
					[GetBMSTATUS_INT]
					AND 
					[EXISTS] (SELECT BEDID_CHR FROM T_BSE_BED WHERE BEDID_CHR=(SELECT BEDID_CHR FROM T_Opr_Bih_Register WHERE T_OPR_BIH_REGISTER.registerid_chr=a.registerid_chr AND ROWNUM=1 [GetAREAID_CHR] ))  [GetCODE_CHR]
                    order by a.patientid_chr,a.CREATE_DAT
				  ";
            #region ����
            //��ȡסԺ���� 	=>[GetAREAID_CHR]
            string str1 = "";
            if (p_strAreaID.Trim() != "")
            {
                str1 = " AND AREAID_CHR='" + p_strAreaID.Trim() + "' ";

            }
            strSQL = strSQL.Replace("[GetAREAID_CHR]", str1);
            //��ȡסԺ��	=>[REGISTERID_CHR]	
            str1 = "";
            if (!p_strREGISTERID_CHR.ToString().Trim().Equals(""))
            {
                str1 = " AND	(SELECT TRIM(INPATIENTID_CHR) FROM T_OPR_BIH_REGISTER WHERE Trim(REGISTERID_CHR)=Trim(a.REGISTERID_CHR))='" + p_strREGISTERID_CHR.ToString().Trim() + "' ";

            }
            strSQL = strSQL.Replace("[REGISTERID_CHR]", str1);
            //��ȡ��Ժʱ��	=>[GetINPATIENT_DAT]	
            str1 = "";
            if (!p_dtBegin.ToString().Trim().Equals("") && !p_dtEnd.ToString().Trim().Equals(""))
            {
                str1 = " AND a.CREATE_DAT>=TO_DATE('" + p_dtBegin.ToString("yyyy-MM-dd") + "','YYYY-MM-DD') AND  a.CREATE_DAT<=TO_DATE('" + p_dtEnd.ToString("yyyy-MM-dd HH:mm:ss") + "','YYYY-MM-DD hh24:mi:ss') ";

            }
            strSQL = strSQL.Replace("[CREATE_DAT]", str1);
            //��ȡ����	=>[GetCODE_CHR]	
            str1 = "";
            if (p_strAreaID.Trim() != "")
            {

                if (p_strBedIDs.Trim() != "")
                {
                    string str_bed_temp = "";
                    string[] str_bedArray = p_strBedIDs.ToString().Trim().Split(",".ToCharArray());
                    for (int i = 0; i < str_bedArray.Length; i++)
                    {
                        if (i > 0)
                            str_bed_temp += ",";
                        if (!str_bedArray[i].ToString().Trim().Equals(""))
                        {
                            str_bed_temp += "'" + str_bedArray[i] + "'";
                        }
                    }

                    str1 += "  IN (" + str_bed_temp.Trim() + ")";
                }
            }
            strSQL = strSQL.Replace("[GetCODE_CHR]", str1);
            //û���Ų�ѯ	=>[EXISTS]

            if (str1.Trim().Equals(""))
            {
                str1 = "EXISTS";
                strSQL = strSQL.Replace("[EXISTS]", str1);
            }
            else
            {
                str1 = "";
                strSQL = strSQL.Replace("[EXISTS]", str1);
            }

            //��ҩ״̬����	=>[GetBMSTATUS_INT]	
            str1 = "";
            if (BMSTATUS_INT >= 0)
            {
                str1 = " AND  a.BMSTATUS_INT=" + BMSTATUS_INT.ToString().Trim() + " ";

            }
            strSQL = strSQL.Replace("[GetBMSTATUS_INT]", str1);
            #endregion


            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_objResultArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        /** 
		 * ���� ���˷�����ϸ��ˮ��,���Ĳ�����ҩ״̬�����˷�����ϸ��)
		 */
        /// <summary>
        /// �޸���ҩ��״̬   ��ҩ״̬{0=δ����;1=�ѷ���;2=��ȷ��}
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intPstatus">����״̬	{0=δ����;1=�ѷ���;2=���ַ�ҩ;3=�ѷ�ҩ};if(this==2) ������������</param>
        /// <param name="p_strProcessor">������{=��Ա.id}</param>
        /// <param name="p_strID">��ҩ��ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyBackMedReqStatus(string p_strPCHARGEID_CHR)
        {
            long lngRes = 0;

            string strSQL = "";
            strSQL = " UPDATE T_OPR_BIH_PATIENTCHARGE SET BMSTATUS_INT=1 WHERE PCHARGEID_CHR='" + p_strPCHARGEID_CHR.ToString().Trim() + "'";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
    }
}
