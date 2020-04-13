using System;
using System.Collections;
using System.Collections.Generic;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.EnterpriseServices;
using System.Data;
using System.Collections;
namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// clsChargeMaintenanceSvc ��ժҪ˵����
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsChargeMaintenanceSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        public clsChargeMaintenanceSvc()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #region ��ȡ���˷��������Ϣ
        [AutoComplete]
        public void m_mthGetPatientCatInfo(out DataTable p_dt)
        {
            p_dt = new DataTable();
            long lngRes = 0;
            string strSQL = @"select PAYTYPEID_CHR COPAYID_CHR, PAYTYPENAME_VCHR COPAYNAME_CHR,payflag_dec from T_BSE_PATIENTPAYTYPE order by payflag_dec";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dt);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }


        }
        #endregion
        #region �����շ���Ŀ
        [AutoComplete]
        public long m_mthFindChargeItem(string strType, string ID, out DataTable p_dt, string strCatID)
        {
            p_dt = new DataTable();
            long lngRes = 0;
            string strSQL = @"select A.itemid_chr,A.itemname_vchr,A.itemcode_vchr,A.ITEMPRICE_MNY,B.copayid_chr,B.precent_dec from T_BSE_CHARGEITEM A,T_AID_INSCHARGEITEM B
where A.itemid_chr(+)=B.itemid_chr";
            if (strType.Trim() != "" && ID.Trim() != "")
            {
                strSQL += " and A." + strType.Trim() + " Like '" + ID + "%' order by A.itemcode_vchr desc";
            }
            else
            {
                strSQL += "  order by A.itemcode_vchr desc";
            }
            if (strCatID.Trim() != "")
            {
                strSQL = @"SELECT   a.itemid_chr, a.itemname_vchr, a.itemcode_vchr, a.itemprice_mny,
         b.copayid_chr, b.precent_dec
    FROM t_aid_inschargeitem b, t_bse_chargeitem a, t_bse_chargecatmap d
   WHERE b.itemid_chr = a.itemid_chr(+)
     AND a.itemopinvtype_chr = d.catid_chr(+)
     AND d.groupid_chr = '" + strCatID + @"'
     AND a." + strType.Trim() + @" Like '" + ID + @"%'
ORDER BY a.itemcode_vchr DESC";
            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dt);
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
        #region ��������
        [AutoComplete]
        public void m_mthUpdateData(string strItemID, string strCopayID, string strValue)
        {
            long lngRes = 0;
            string strSQL = "";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                strSQL = " select * from T_AID_INSCHARGEITEM   WHERE itemid_chr = '" + strItemID + "' AND copayid_chr = '" + strCopayID + "' ";
                DataTable dt = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
                if (lngRes > 0)
                {
                    if (dt.Rows.Count == 0)
                    {
                        strSQL = "insert into T_AID_INSCHARGEITEM (ITEMID_CHR,COPAYID_CHR,PRECENT_DEC) values('" + strItemID + "','" + strCopayID + "','" + strValue + "')";
                    }
                    else
                    {
                        strSQL = @"update T_AID_INSCHARGEITEM set PRECENT_DEC='" + strValue + "' where ITEMID_CHR='" + strItemID + "' and COPAYID_CHR='" + strCopayID + "'";
                    }

                    lngRes = objHRPSvc.DoExcute(strSQL);
                }
                dt.Dispose();
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
        }
        #endregion
    }
    /// <summary>
    /// �����б�����м��
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsWaitDiagListManageSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        public clsWaitDiagListManageSvc()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #region ��ȡԱ����������
        [AutoComplete]
        public long m_mthGetDepartmentByID(string strEmpID, out DataTable p_dt)
        {
            p_dt = new DataTable();
            long lngRes = 0;
            string strSQL = @" select deptid_chr,deptname_vchr  from t_bse_deptdesc
  where INPATIENTOROUTPATIENT_INT=0 and 
  -- ATTRIBUTEID ='0000002' and
  CATEGORY_INT =0 order by DEPTNAME_VCHR";
            if (strEmpID.Trim() != "")
            {
                strSQL = @"SELECT a.deptid_chr, b.deptname_vchr
  FROM t_bse_deptemp a, t_bse_deptdesc b
 WHERE a.deptid_chr = b.deptid_chr(+) AND A.end_dat IS NULL and b.INPATIENTOROUTPATIENT_INT=0 and
-- ATTRIBUTEID ='0000002' and 
CATEGORY_INT =0 AND A.empid_chr = '" + strEmpID + "' ";
            }

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dt);
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
        #region ��ȡԱ����������
        [AutoComplete]
        public long m_mthGetDepartment(out DataTable p_dt)
        {
            p_dt = new DataTable();
            long lngRes = 0;
            string strSQL = @"SELECT distinct(b.deptid_chr), b.deptname_vchr
  FROM t_opr_outpatientrecipeinv a, t_bse_deptdesc b
 WHERE a.deptid_chr = b.deptid_chr";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dt);
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
        #region ���ݲ���ID����ҽ��,
        [AutoComplete]
        public long m_mthGetDocByDepID(string ID, out DataTable p_dt)
        {
            p_dt = new DataTable();
            long lngRes = 0;
            string strSQL = @"SELECT distinct b.empid_chr, b.lastname_vchr,b.ISEXPERT_CHR,b.EMPNO_CHR
  FROM  t_bse_employee b ,T_BSE_DEPTEMP A
 WHERE b.status_int = 1
 and b.empid_chr=a.empid_chr(+) and b.HASPRESCRIPTIONRIGHT_CHR =1 ";
            if (ID.Trim() != "")
            {
                strSQL += " and a.deptid_chr='" + ID + "'";
            }
            strSQL += " order by b.empno_chr ";


            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dt);
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
        #region ���ݲ���ID��ҽ��ID���Һ��ﲡ��
        [AutoComplete]
        public long m_mthGetWaitListByID(string strDocID, string strDepID, out DataTable p_dt, DateTime date, DateTime date2)
        {
            p_dt = new DataTable();
            long lngRes = 0;
            string strDoctorID = " = '" + strDocID + "'";
            if (strDocID.Trim() == "****")
            {
                strDoctorID = " is null";
            }
            string strSQL = @"select A.waitdiaglistid_chr,a.ORDER_INT,b.PATIENTID_CHR,c.lastname_vchr,c.sex_chr,c.BIRTH_DAT,b.PATIENTCARDID_CHR
	from T_OPR_WAITDIAGLIST A ,T_OPR_PATIENTREGISTER B, T_BSE_PATIENT C
	where A.registerid_chr=B.registerid_chr(+)
	and B.PATIENTID_CHR=C.patientid_chr(+)
	and a.PSTATUS_INT=1
	and a.WAITDIAGDEPT_CHR='" + strDepID + @"'
	and a.WAITDIAGDR_CHR " + strDoctorID + @"
	AND a.registerdate_dat BETWEEN TO_DATE('" + date.ToString("yyyy-MM-dd 00:00:00") + "','yyyy-mm-dd hh24:mi:ss') " +
    " AND TO_DATE('" + date2.ToString("yyyy-MM-dd 23:59:59") + " ','yyyy-mm-dd hh24:mi:ss')";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dt);
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
        #region ���
        /// <summary>
        /// ���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strDocID">ҽ��ID</param>
        /// <param name="strDepID">����ID</param>
        /// <param name="rowNo">����Ӻ�</param>
        /// <param name="strListID">����ID(Ψһ)</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthPrecedence(string strDocID, string strDepID, int rowNo, string strListID)
        {
            long lngRes = 0;
            string strSQL = @"
UPDATE t_opr_waitdiaglist
   SET order_int = order_int + 1
 WHERE waitdiagdept_chr = '" + strDepID + "' AND waitdiagdr_chr = '" + strDocID + "' AND order_int <= '" + rowNo.ToString() + "'";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                if (lngRes > 0)
                {
                    strSQL = "UPDATE t_opr_waitdiaglist SET order_int = 1 WHERE waitdiaglistid_chr = '" + strListID + "'";
                    lngRes = objHRPSvc.DoExcute(strSQL);
                    //				objHRPSvc.Dispose();
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
        #region �ƶ�λ��

        [AutoComplete]
        public long m_mthMoveOrder(string row1, string row2, string ID1, string ID2)
        {
            long lngRes = 0;

            string strSQL = "UPDATE t_opr_waitdiaglist SET order_int = " + row1 + " WHERE waitdiaglistid_chr = '" + ID1 + "'";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);

                if (lngRes > 0)
                {
                    strSQL = "UPDATE t_opr_waitdiaglist SET order_int = " + row2 + " WHERE waitdiaglistid_chr = '" + ID2 + "'";
                    lngRes = objHRPSvc.DoExcute(strSQL);
                    //					objHRPSvc.Dispose();
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
        #region ����ҽ��
        /// <summary>
        /// ����ҽ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strDocID">ҽ��ID</param>
        /// <param name="strListID">����ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthChangeDoc(string strDocID, string strDepID, string strListID)
        {
            long lngRes = 0;
            string strSQL = @"UPDATE t_opr_waitdiaglist SET WAITDIAGDR_CHR = '" + strDocID + "',waitdiagdept_chr = '" + strDepID + "' WHERE waitdiaglistid_chr = '" + strListID + "'";

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
        #region ����ʱ��κ�Ա��ID������ﲡ��
        /// <summary>
        ///  ����ʱ��κ�Ա��ID������ﲡ��
        /// </summary>
        /// <param name="strDocName">ҽ������ģ����ѯ</param>
        /// <param name="strDepID">����ID</param>
        /// <param name="p_dt"></param>
        /// <param name="date"></param>
        /// <param name="date2"></param>
        /// <param name="strEmpID">Ա��ID</param>
        /// <param name="flag">0�½�,1����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthGetWaitListInfoByID(string strDocName, string strDepID, out DataTable p_dt, DateTime date, DateTime date2, string strEmpID, int flag)
        {
            p_dt = new DataTable();
            long lngRes = 0;
            string strSQL = @"SELECT   a.waitdiaglistid_chr, a.order_int, b.patientid_chr, c.lastname_vchr,
         c.sex_chr, c.birth_dat, b.patientcardid_chr, a.waitdiagdept_chr,
         d.deptname_vchr, a.waitdiagdr_chr, e.lastname_vchr empname
    FROM t_opr_waitdiaglist a,
         t_opr_patientregister b,
         t_bse_patient c,
         t_bse_deptdesc d,
         t_bse_employee e,
         t_bse_deptemp f
   WHERE a.registerid_chr = b.registerid_chr(+)
     AND b.patientid_chr = c.patientid_chr(+)
     AND a.pstatus_int = " + flag.ToString() + @"
     AND a.waitdiagdept_chr = d.deptid_chr(+)
     AND a.waitdiagdr_chr = e.empid_chr(+)
     AND a.waitdiagdept_chr = f.deptid_chr(+)
     AND d.inpatientoroutpatient_int = 0
    -- AND d.attributeid = '0000002'
     AND d.category_int = 0
     AND f.empid_chr = '" + strEmpID + @"'
     AND f.end_dat IS NULL
	AND a.registerdate_dat BETWEEN TO_DATE('" + date.ToString("yyyy-MM-dd 00:00:00") + "','yyyy-mm-dd hh24:mi:ss') " +
            " AND TO_DATE('" + date2.ToString("yyyy-MM-dd 23:59:59") + " ','yyyy-mm-dd hh24:mi:ss')";
            if (strDepID.Trim() != "")
            {
                strSQL += " AND a.waitdiagdept_chr ='" + strDepID + "'";
            }
            if (strDocName.Trim() != "")
            {
                strSQL += " AND e.lastname_vchr  like '" + strDocName + "%'";
            }
            strSQL += "  ORDER BY a.order_int,a.waitdiagdr_chr";
            // strSQL += "  ORDER BY a.waitdiagdr_chr,a.order_int";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dt);
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

        #region �շ�Ա������ͳ�Ʊ��� @@@@@
        [AutoComplete]
        public long m_mthGetCheckManWorkLoad(DateTime strBeginDate, DateTime strEndDate, out clsChargeWork_VO[] objSubArr)
        {
            objSubArr = null;
            long lngRes = 0;
            string strSQL = @"SELECT   a.groupname_chr, SUM (c.tolfee_mny) tolfee_mny,c.LASTNAME_VCHR
    FROM t_aid_rpt_gop_def a,
         t_aid_rpt_gop_rla b,
         (SELECT   b.itemcatid_chr, SUM (b.tolfee_mny) tolfee_mny,e.LASTNAME_VCHR
              FROM t_opr_outpatientrecipeinv a, t_opr_outpatientrecipesumde b,T_BSE_EMPLOYEE e
             WHERE a.seqid_chr = b.seqid_chr(+)   AND a.balanceflag_int = 1 
AND  a.BALANCEEMP_CHR=e.EMPID_CHR  and  a.BALANCE_DAT BETWEEN TO_DATE('" + strBeginDate.ToString("yyyy-MM-dd 00:00:00") + @"','yyyy-mm-dd hh24:mi:ss') " +
                " AND TO_DATE('" + strEndDate.ToString("yyyy-MM-dd 23:59:59") + @" ','yyyy-mm-dd hh24:mi:ss')
          GROUP BY e.LASTNAME_VCHR,b.itemcatid_chr) c
   WHERE a.groupid_chr = b.groupid_chr(+)
     AND b.typeid_chr = c.itemcatid_chr(+)
     AND a.rptid_chr = '0068'
     AND b.rptid_chr = '0068'
GROUP BY c.LASTNAME_VCHR,a.groupid_chr, a.groupname_chr
ORDER BY a.groupid_chr";

            try
            {
                DataTable dt = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
                if (lngRes > 0 && dt.Rows.Count > 0)
                {
                    objSubArr = new clsChargeWork_VO[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        objSubArr[i] = new clsChargeWork_VO();
                        objSubArr[i].m_strCatName = dt.Rows[i]["GROUPNAME_CHR"].ToString().Trim();
                        objSubArr[i].m_strCatMoney = dt.Rows[i]["TOLFEE_MNY"].ToString().Trim();
                        objSubArr[i].m_strChargeName = dt.Rows[i]["LASTNAME_VCHR"].ToString().Trim();
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

        #region �շ�Ա������ͳ�Ʊ���
        /// <summary>
        /// �շ�Ա������ͳ�Ʊ��� 
        /// </summary>
        /// <param name="strBeginDate"></param>
        /// <param name="strEndDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthGetCheckManWorkLoad(DateTime strBeginDate, DateTime strEndDate, out DataTable dt)
        {
            long lngRes = 0;
            dt = new DataTable();

            string strSQL = @"SELECT   a.groupname_chr, SUM (c.tolfee_mny) tolfee_mny,c.LASTNAME_VCHR
                                FROM t_aid_rpt_gop_def a,
                                     t_aid_rpt_gop_rla b,
                                     (SELECT   b.itemcatid_chr, SUM (b.tolfee_mny) tolfee_mny,e.LASTNAME_VCHR
                                          FROM t_opr_outpatientrecipeinv a, t_opr_outpatientrecipesumde b,T_BSE_EMPLOYEE e
                                         WHERE a.seqid_chr = b.seqid_chr(+)   AND a.balanceflag_int = 1 
                            AND  a.BALANCEEMP_CHR=e.EMPID_CHR  and  a.BALANCE_DAT BETWEEN TO_DATE('" + strBeginDate.ToString("yyyy-MM-dd 00:00:00") + @"','yyyy-mm-dd hh24:mi:ss') " +
                                            " AND TO_DATE('" + strEndDate.ToString("yyyy-MM-dd 23:59:59") + @" ','yyyy-mm-dd hh24:mi:ss')
                                      GROUP BY e.LASTNAME_VCHR,b.itemcatid_chr) c
                               WHERE a.groupid_chr = b.groupid_chr(+)
                                 AND b.typeid_chr = c.itemcatid_chr(+)
                                 AND a.rptid_chr = '0068'
                                 AND b.rptid_chr = '0068'
                            GROUP BY c.LASTNAME_VCHR,a.groupid_chr, a.groupname_chr
                            ORDER BY c.LASTNAME_VCHR";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
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

        #region ͳ���շ�Ա������ͳ�Ʊ���Ʊ��(���������飬����շ�Աͬ����׼����ʱ��������һ���Ժ���Ҫͬһ����) @@@@@
        /// <summary>
        /// ͳ���շ�Ա������ͳ�Ʊ���Ʊ��(���������飬����շ�Աͬ����׼����ʱ��������һ���Ժ���Ҫͬһ����) @@@@@
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCheckinvoicenums(string BeginDate, string EndDate, out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;

            string strSQL = @"select b.lastname_vchr, count(case a.status_int when 1 then 1 end) as kps, count(case a.status_int when 2 then 1 end) as tps, 
                                     count(case a.status_int when 3 then 1 end) as hfs                               
                                from t_opr_outpatientrecipeinv a,
                                     t_bse_employee b
                               where a.balanceemp_chr = b.empid_chr
                                 and a.balanceflag_int = 1
                                 and (to_char(a.balance_dat,'yyyy-mm-dd') between '" + BeginDate + "' and '" + EndDate + @"')                                
                             group by b.lastname_vchr  ";

            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);

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

        #region
        [AutoComplete]
        public long m_mthGetSingleWorkLoad(string strID, DateTime strBeginDate, DateTime strEndDate, int flag, out clsSingleWorkLoadSubItem_VO[] objSubArr)
        {
            objSubArr = null;
            string tmep = "a.doctorid_chr";
            if (flag == 2)//2������
            {
                tmep = "a.DEPTID_CHR";
            }
            long lngRes = 0;
            string strSQL = @"SELECT   a.groupname_chr, SUM (c.tolfee_mny) tolfee_mny
    FROM t_aid_rpt_gop_def a,
         t_aid_rpt_gop_rla b,
         (SELECT   b.itemcatid_chr, SUM (b.tolfee_mny) tolfee_mny
              FROM t_opr_outpatientrecipeinv a, t_opr_outpatientrecipesumde b
             WHERE a.seqid_chr = b.seqid_chr(+)   AND a.balanceflag_int = 1
                   
                   AND " + tmep + @" = '" + strID + @"'
AND a.BALANCE_DAT  BETWEEN TO_DATE('" + strBeginDate.ToString("yyyy-MM-dd 00:00:00") + @"','yyyy-mm-dd hh24:mi:ss') " +
    " AND TO_DATE('" + strEndDate.ToString("yyyy-MM-dd 23:59:59") + @" ','yyyy-mm-dd hh24:mi:ss')
          GROUP BY b.itemcatid_chr) c
   WHERE a.groupid_chr = b.groupid_chr(+)
     AND b.typeid_chr = c.itemcatid_chr(+)
     AND a.rptid_chr = '0005'
     AND b.rptid_chr = '0005'
GROUP BY a.groupid_chr, a.groupname_chr
ORDER BY a.groupid_chr";

            try
            {
                DataTable dt = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dt.Rows.Count > 0)
                {
                    objSubArr = new clsSingleWorkLoadSubItem_VO[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        objSubArr[i] = new clsSingleWorkLoadSubItem_VO();
                        objSubArr[i].m_strCatName = dt.Rows[i]["GROUPNAME_CHR"].ToString().Trim();
                        objSubArr[i].m_strCatMoney = dt.Rows[i]["TOLFEE_MNY"].ToString().Trim();
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
        #region ����Ա��ID�����ڻ�ȡ�������͸�����
        /// <summary>
        /// ����Ա��ID�����ڻ�ȡ�������͸�����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strID">ҽ��ID</param>
        /// <param name="m_strBeginDate">��ʼʱ��</param>
        /// <param name="m_strEndDate">����ʱ��</param>
        /// <param name="m_objTable"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRecipeCountByIDAndDate(string m_strID, DateTime m_strBeginDate, DateTime m_strEndDate, out DataTable m_objTable)
        {
            long lngRes = 0;
            m_objTable = null;
            //��һ��Ϊ���������ڶ���Ϊ������
            string strSQL = @"SELECT COUNT (*)
  FROM t_opr_outpatientrecipeinv a,
       t_opr_reciperelation b,
       t_opr_outpatientrecipe c
 WHERE a.outpatrecipeid_chr = b.seqid
   AND b.outpatrecipeid_chr = c.outpatrecipeid_chr
   AND (a.status_int = 1 OR a.status_int = 3)
   AND a.balanceflag_int = 1
   AND a.balance_dat BETWEEN TO_DATE ('" + m_strBeginDate.ToString("yyyy-MM-dd 00:00:00") + @"',
                                      'yyyy-mm-dd hh24:mi:ss'
                                     )
                         AND TO_DATE ('" + m_strEndDate.ToString("yyyy-MM-dd 23:59:59") + @" ',
                                      'yyyy-mm-dd hh24:mi:ss'
                                     )
   AND c.recipeflag_int = 1
   AND a.doctorid_chr = '" + m_strID + @"'
UNION ALL
SELECT COUNT (*) c
  FROM t_opr_outpatientrecipeinv a,
       t_opr_reciperelation b,
       t_opr_outpatientrecipe c
 WHERE a.outpatrecipeid_chr = b.seqid
   AND b.outpatrecipeid_chr = c.outpatrecipeid_chr
   AND (a.status_int = 1 OR a.status_int = 3)
   AND a.balanceflag_int = 1
   AND a.balance_dat BETWEEN TO_DATE ('" + m_strBeginDate.ToString("yyyy-MM-dd 00:00:00") + @"',
                                      'yyyy-mm-dd hh24:mi:ss'
                                     )
                         AND TO_DATE ('" + m_strEndDate.ToString("yyyy-MM-dd 23:59:59") + @" ',
                                      'yyyy-mm-dd hh24:mi:ss'
                                     )
   AND c.recipeflag_int = 2
   AND a.doctorid_chr = '" + m_strID + "'";
            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref m_objTable);

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
        #region ȡ����������
        [AutoComplete]
        public long m_mthGetSingleWorkLoad_New(string strID, DateTime strBeginDate, DateTime strEndDate, int flag, out clsSingleWorkLoadSubItem_VO[] objSubArr, string p_identityId)
        {
            objSubArr = null;
            string tmep = "a.doctorid_chr";
            if (flag == 2)//2������
            {
                tmep = "a.DEPTID_CHR";
            }
            long lngRes = 0;
            string strSQL = @"select * from 

(
SELECT   a.groupname_chr, SUM (c.tolfee_mny) tolfee_mny
    FROM t_aid_rpt_gop_def a,
         t_aid_rpt_gop_rla b,
         (SELECT   b.itemcatid_chr, SUM (b.tolfee_mny) tolfee_mny
              FROM t_opr_outpatientrecipeinv a, t_opr_outpatientrecipesumde b , t_bse_patientPaytype p
             WHERE a.seqid_chr = b.seqid_chr(+)
           
               AND " + tmep + @" = '" + strID + @"'
               and a.PAYTYPEID_CHR = p.PAYTYPEID_CHR(+) ";
            if (p_identityId != "-1")
                strSQL += "   and p.INTERNALFLAG_INT =" + p_identityId;

            strSQL += @"   AND a.BALANCE_DAT  BETWEEN TO_DATE('" + strBeginDate.ToString("yyyy-MM-dd 00:00:00") + @"','yyyy-mm-dd hh24:mi:ss') " +
                " AND TO_DATE('" + strEndDate.ToString("yyyy-MM-dd 23:59:59") + @" ','yyyy-mm-dd hh24:mi:ss')
          GROUP BY b.itemcatid_chr) c
   WHERE a.groupid_chr = b.groupid_chr(+)
     AND b.typeid_chr = c.itemcatid_chr(+)
     AND a.rptid_chr = '0005'
     AND b.rptid_chr = '0005'
GROUP BY a.groupid_chr, a.groupname_chr
ORDER BY a.groupid_chr
)
,

(
SELECT   count(a.INVOICENO_VCHR) as NCount  --������ 
              FROM t_opr_outpatientrecipeinv a , t_bse_patientPaytype p
             WHERE  
               ";
            strSQL += @"	   " + tmep + @" = '" + strID + @"'
                and a.PAYTYPEID_CHR = p.PAYTYPEID_CHR(+) ";
            if (p_identityId != "-1")
                strSQL += "   and p.INTERNALFLAG_INT =" + p_identityId;
            strSQL += @"   AND a.BALANCE_DAT  BETWEEN TO_DATE('" + strBeginDate.ToString("yyyy-MM-dd 00:00:00") + @"','yyyy-mm-dd hh24:mi:ss') " +
                " AND TO_DATE('" + strEndDate.ToString("yyyy-MM-dd 23:59:59") + @" ','yyyy-mm-dd hh24:mi:ss')
 
 )
 ";

            try
            {
                DataTable dt = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dt.Rows.Count > 0)
                {
                    objSubArr = new clsSingleWorkLoadSubItem_VO[dt.Rows.Count + 1];
                    for (int i = 0; i < dt.Rows.Count + 1; i++)
                    {
                        objSubArr[i] = new clsSingleWorkLoadSubItem_VO();
                        if (i == 0)
                        {
                            objSubArr[i].m_strCatName = "������";
                            objSubArr[i].m_strCatMoney = dt.Rows[i]["NCOUNT"].ToString().Trim();
                        }
                        else
                        {
                            objSubArr[i].m_strCatName = dt.Rows[i - 1]["GROUPNAME_CHR"].ToString().Trim();
                            objSubArr[i].m_strCatMoney = dt.Rows[i - 1]["TOLFEE_MNY"].ToString().Trim();
                        }
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

        #region ��ȡ��Ĺ���������
        [AutoComplete]
        public long m_mthGetGroupWorkLoad(string strGroupID, DateTime strBeginDate, DateTime strEndDate, int flag, int intflag, out clsSingleWorkLoadSubItem_VO[] objSubArr)
        {
            objSubArr = null;
            long lngRes = 0;
            string strSQL = null;
            if (intflag == 0)
            {
                strSQL = @"SELECT   a.groupname_chr, SUM (c.tolfee_mny) tolfee_mny,f.groupname_vchr,c.groupid_chr
    FROM t_aid_rpt_gop_def a,
         t_aid_rpt_gop_rla b,
         t_bse_groupdesc f,
         (SELECT   b.itemcatid_chr, SUM (b.tolfee_mny) tolfee_mny,a.groupid_chr
              FROM t_opr_outpatientrecipeinv a, t_opr_outpatientrecipesumde b
             WHERE a.seqid_chr = b.seqid_chr(+)  
             and( a.isvouchers_int <> 2 or a.isvouchers_int is null)
             AND a.BALANCE_DAT BETWEEN TO_DATE('" + strBeginDate.ToString("yyyy-MM-dd 00:00:00") + @"','yyyy-mm-dd hh24:mi:ss') " +
                    " AND TO_DATE('" + strEndDate.ToString("yyyy-MM-dd 23:59:59") + @" ','yyyy-mm-dd hh24:mi:ss')           
          GROUP BY b.itemcatid_chr,a.groupid_chr) c
   WHERE a.groupid_chr = b.groupid_chr(+)
     AND b.typeid_chr = c.itemcatid_chr(+)
     and f.GROUPID_CHR =c.groupid_chr
   AND a.rptid_chr = '0066'
     AND b.rptid_chr = '0066'
GROUP BY a.groupname_chr,f.groupname_vchr,c.groupid_chr order by f.groupname_vchr";
            }
            else
            {
                strSQL = @"SELECT   a.groupname_chr, SUM (c.tolfee_mny) tolfee_mny,f.groupname_vchr,c.groupid_chr
    FROM t_aid_rpt_gop_def a,
         t_aid_rpt_gop_rla b,
         t_bse_groupdesc f,
         (SELECT   b.itemcatid_chr, SUM (b.tolfee_mny) tolfee_mny,a.groupid_chr
              FROM t_opr_outpatientrecipeinv a, t_opr_outpatientrecipesumde b
             WHERE a.seqid_chr = b.seqid_chr(+)  
             and( a.isvouchers_int <> 2 or a.isvouchers_int is null)
             AND a.BALANCE_DAT BETWEEN TO_DATE('" + strBeginDate.ToString("yyyy-MM-dd 00:00:00") + @"','yyyy-mm-dd hh24:mi:ss') " +
                       " AND TO_DATE('" + strEndDate.ToString("yyyy-MM-dd 23:59:59") + @" ','yyyy-mm-dd hh24:mi:ss')
������������and a.chargedeptid_chr not in('0000198','0000199','0000200')           
          GROUP BY b.itemcatid_chr,a.groupid_chr) c
   WHERE a.groupid_chr = b.groupid_chr(+)
     AND b.typeid_chr = c.itemcatid_chr(+)
     and f.GROUPID_CHR =c.groupid_chr
   AND a.rptid_chr = '0066'
     AND b.rptid_chr = '0066'
GROUP BY a.groupname_chr,f.groupname_vchr,c.groupid_chr order by f.groupname_vchr";
            }
            try
            {
                DataTable dt = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
                if (intflag == 0)
                {
                    #region Sql
                    strSQL = @"select tt.groupname, tt.typeid, sum(tt.total) as difftotal
                                  from (select b.groupname_vchr as groupname,
                                               'm' as typeid,
                                               sum(round(r.toldiffprice_mny, 2)) as total
                                          from t_opr_outpatientrecipeinv a
                                         inner join t_bse_groupdesc b
                                            on a.groupid_chr = b.groupid_chr
                                         inner join t_opr_reciperelation c
                                            on a.outpatrecipeid_chr = c.seqid
                                         inner join t_opr_outpatientpwmrecipede r
                                            on c.outpatrecipeid_chr = r.outpatrecipeid_chr
                                         where (a.balance_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss')) 
                                           and (a.isvouchers_int <> 2 or a.isvouchers_int is null)
                                         group by b.groupname_vchr
                                        union all
                                        select b.groupname_vchr as groupname,
                                               'm' as typeid,
                                               sum(round(r.toldiffprice_mny, 2)) as total
                                          from t_opr_outpatientrecipeinv a
                                         inner join t_bse_groupdesc b
                                            on a.groupid_chr = b.groupid_chr
                                         inner join t_opr_reciperelation c
                                            on a.outpatrecipeid_chr = c.seqid
                                         inner join t_opr_outpatientcmrecipede r
                                            on c.outpatrecipeid_chr = r.outpatrecipeid_chr
                                         where (a.balance_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss')) 
                                           and (a.isvouchers_int <> 2 or a.isvouchers_int is null)
                                         group by b.groupname_vchr
                                        union all
                                        select b.groupname_vchr as groupname,
                                               'c' as typeid,
                                               sum(round(r.toldiffprice_mny, 2)) as total
                                          from t_opr_outpatientrecipeinv a
                                         inner join t_bse_groupdesc b
                                            on a.groupid_chr = b.groupid_chr
                                         inner join t_opr_reciperelation c
                                            on a.outpatrecipeid_chr = c.seqid
                                         inner join t_opr_outpatientothrecipede r
                                            on c.outpatrecipeid_chr = r.outpatrecipeid_chr
                                         where (a.balance_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss')) 
                                           and (a.isvouchers_int <> 2 or a.isvouchers_int is null)
                                         group by b.groupname_vchr) tt
                                 group by tt.groupname, tt.typeid
                                ";
                    //                    strSQL = @"SELECT '9999' itemcatid_chr,(-1)* SUM(decode(a.STATUS_INT,1, nvl(a.totaldiffcost_mny,0), nvl(a.totaldiffcost_mny,0))) tolfee_mny,b.groupname_vchr
                    //          FROM t_opr_outpatientrecipeinv a,t_bse_groupdesc b
                    //         WHERE a.groupid_chr = b.groupid_chr
                    //          and a.BALANCE_DAT BETWEEN TO_DATE('" + strBeginDate.ToString("yyyy-MM-dd 00:00:00") + @"','yyyy-mm-dd hh24:mi:ss') " +
                    //                    " AND TO_DATE('" + strEndDate.ToString("yyyy-MM-dd 23:59:59") + @" ','yyyy-mm-dd hh24:mi:ss')  
                    //          and( a.isvouchers_int <> 2 or a.isvouchers_int is null)       
                    //         GROUP BY b.groupname_vchr";
                    #endregion
                }
                else
                {
                    #region Sql
                    strSQL = @"select tt.groupname, tt.typeid, sum(tt.total) as difftotal
                                  from (select b.groupname_vchr as groupname,
                                               'm' as typeid,
                                               sum(round(r.toldiffprice_mny, 2)) as total
                                          from t_opr_outpatientrecipeinv a
                                         inner join t_bse_groupdesc b
                                            on a.groupid_chr = b.groupid_chr
                                         inner join t_opr_reciperelation c
                                            on a.outpatrecipeid_chr = c.seqid
                                         inner join t_opr_outpatientpwmrecipede r
                                            on c.outpatrecipeid_chr = r.outpatrecipeid_chr
                                         where (a.balance_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss')) 
                                           and (a.isvouchers_int <> 2 or a.isvouchers_int is null)
                                           and a.chargedeptid_chr not in ('0000198', '0000199', '0000200')
                                         group by b.groupname_vchr
                                        union all
                                        select b.groupname_vchr as groupname,
                                               'm' as typeid,
                                               sum(round(r.toldiffprice_mny, 2)) as total
                                          from t_opr_outpatientrecipeinv a
                                         inner join t_bse_groupdesc b
                                            on a.groupid_chr = b.groupid_chr
                                         inner join t_opr_reciperelation c
                                            on a.outpatrecipeid_chr = c.seqid
                                         inner join t_opr_outpatientcmrecipede r
                                            on c.outpatrecipeid_chr = r.outpatrecipeid_chr
                                         where (a.balance_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss')) 
                                           and (a.isvouchers_int <> 2 or a.isvouchers_int is null)
                                           and a.chargedeptid_chr not in ('0000198', '0000199', '0000200')
                                         group by b.groupname_vchr
                                        union all
                                        select b.groupname_vchr as groupname,
                                               'c' as typeid,
                                               sum(round(r.toldiffprice_mny, 2)) as total
                                          from t_opr_outpatientrecipeinv a
                                         inner join t_bse_groupdesc b
                                            on a.groupid_chr = b.groupid_chr
                                         inner join t_opr_reciperelation c
                                            on a.outpatrecipeid_chr = c.seqid
                                         inner join t_opr_outpatientothrecipede r
                                            on c.outpatrecipeid_chr = r.outpatrecipeid_chr
                                         where (a.balance_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss')) 
                                           and (a.isvouchers_int <> 2 or a.isvouchers_int is null)
                                           and a.chargedeptid_chr not in ('0000198', '0000199', '0000200')
                                         group by b.groupname_vchr) tt
                                 group by tt.groupname, tt.typeid
                                ";
                    //                    strSQL = @"SELECT '9999' itemcatid_chr,
                    //           (-1) * SUM(decode(a.STATUS_INT,
                    //                  1,
                    //                  nvl(a.totaldiffcost_mny, 0),
                    //                  nvl(a.totaldiffcost_mny, 0))) tolfee_mny,
                    //       b.groupname_vchr
                    //  FROM t_opr_outpatientrecipeinv a, t_bse_groupdesc b
                    // WHERE a.groupid_chr = b.groupid_chr
                    // and( a.isvouchers_int <> 2 or a.isvouchers_int is null)   
                    //  AND a.BALANCE_DAT BETWEEN TO_DATE('" + strBeginDate.ToString("yyyy-MM-dd 00:00:00") + @"','yyyy-mm-dd hh24:mi:ss') " +
                    //                   " AND TO_DATE('" + strEndDate.ToString("yyyy-MM-dd 23:59:59") + @" ','yyyy-mm-dd hh24:mi:ss')
                    // ������������and
                    // a.chargedeptid_chr not in ('0000198', '0000199', '0000200')
                    // GROUP BY b.groupname_vchr";
                    #endregion
                }
                DataTable dtTemp = new DataTable();
                IDataParameter[] parm = null;
                objHRPSvc.CreateDatabaseParameter(6, out parm);
                parm[0].Value = strBeginDate.ToString("yyyy-MM-dd") + " 00:00:00";
                parm[1].Value = strEndDate.ToString("yyyy-MM-dd") + " 23:59:59";
                parm[2].Value = strBeginDate.ToString("yyyy-MM-dd") + " 00:00:00";
                parm[3].Value = strEndDate.ToString("yyyy-MM-dd") + " 23:59:59";
                parm[4].Value = strBeginDate.ToString("yyyy-MM-dd") + " 00:00:00";
                parm[5].Value = strEndDate.ToString("yyyy-MM-dd") + " 23:59:59";
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtTemp, parm);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dt.Rows.Count > 0)
                {
                    List<clsSingleWorkLoadSubItem_VO> lstItem = new List<clsSingleWorkLoadSubItem_VO>();
                    clsSingleWorkLoadSubItem_VO vo = null;
                    foreach (DataRow dr in dt.Rows)
                    {
                        vo = new clsSingleWorkLoadSubItem_VO();
                        vo.m_strCatName = dr["groupname_chr"].ToString().Trim();
                        vo.m_strCatMoney = dr["tolfee_mny"].ToString().Trim();
                        vo.m_strGroupName = dr["groupname_vchr"].ToString().Trim();
                        lstItem.Add(vo);
                    }
                    if (dtTemp != null && dtTemp.Rows.Count > 0)
                    {
                        List<clsSingleWorkLoadSubItem_VO> lstDiff = new List<clsSingleWorkLoadSubItem_VO>();
                        foreach (DataRow dr in dtTemp.Rows)
                        {
                            vo = new clsSingleWorkLoadSubItem_VO();
                            vo.m_strGroupName = dr["groupname"].ToString().Trim();
                            if (dr["typeid"].ToString() == "m")
                                vo.m_strCatName = "ҩƷ����";
                            else if (dr["typeid"].ToString() == "c")
                                vo.m_strCatName = "��������";
                            vo.m_strCatMoney = dr["difftotal"].ToString();
                            lstDiff.Add(vo);
                        }
                        lstItem.AddRange(lstDiff);
                    }
                    lstItem.Sort();
                    objSubArr = lstItem.ToArray();

                    #region bak
                    //int count = 0;
                    //objSubArr = new clsSingleWorkLoadSubItem_VO[dt.Rows.Count + dtTemp.Rows.Count];
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    if (i != 0 && objSubArr[i + count - 1].m_strGroupName != dt.Rows[i]["groupname_vchr"].ToString().Trim())
                    //    {
                    //        DataRow[] drr = dtTemp.Select("groupname_vchr ='" + objSubArr[i + count - 1].m_strGroupName + "'");
                    //        foreach (DataRow dr in drr)
                    //        {
                    //            objSubArr[i + count] = new clsSingleWorkLoadSubItem_VO();
                    //            objSubArr[i + count].m_strCatName = "ҩƷ����";
                    //            objSubArr[i + count].m_strCatMoney = dr["TOLFEE_MNY"].ToString().Trim();
                    //            objSubArr[i + count].m_strGroupName = dr["groupname_vchr"].ToString().Trim();
                    //            count++;
                    //        }
                    //    }
                    //    objSubArr[i + count] = new clsSingleWorkLoadSubItem_VO();
                    //    objSubArr[i + count].m_strCatName = dt.Rows[i]["GROUPNAME_CHR"].ToString().Trim();
                    //    objSubArr[i + count].m_strCatMoney = dt.Rows[i]["TOLFEE_MNY"].ToString().Trim();
                    //    objSubArr[i + count].m_strGroupName = dt.Rows[i]["groupname_vchr"].ToString().Trim();

                    //    if (i == dt.Rows.Count - 1)
                    //    {
                    //        DataRow[] drr = dtTemp.Select("groupname_vchr ='" + dt.Rows[i]["groupname_vchr"].ToString().Trim() + "'");
                    //        foreach (DataRow dr in drr)
                    //        {
                    //            objSubArr[dt.Rows.Count + dtTemp.Rows.Count - 1] = new clsSingleWorkLoadSubItem_VO();
                    //            objSubArr[dt.Rows.Count + dtTemp.Rows.Count - 1].m_strCatName = "ҩƷ����";
                    //            objSubArr[dt.Rows.Count + dtTemp.Rows.Count - 1].m_strCatMoney = dr["TOLFEE_MNY"].ToString().Trim();
                    //            objSubArr[dt.Rows.Count + dtTemp.Rows.Count - 1].m_strGroupName = dr["groupname_vchr"].ToString().Trim();
                    //        }
                    //    }
                    //}
                    #endregion
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

        #region ��ȡ�������ļ�¼��
        /// <summary>
        /// ��ȡ�������ļ�¼��
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthGetCount(out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strSQL = @"SELECT   COUNT (CASE
                   WHEN a.recipeflag_int = 1
                      THEN 1
                END) AS ����,
         COUNT (CASE
                   WHEN a.recipeflag_int = 2
                      THEN 1
                END) AS ����, b.groupname_vchr
    FROM t_opr_outpatientrecipe a, t_bse_groupdesc b
   WHERE a.groupid_chr = b.groupid_chr AND a.pstauts_int = 2
      OR a.pstauts_int = 3
GROUP BY b.groupname_vchr";

            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);

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

        #region ���ݽ���ʱ��ͳ��������������¼��
        /// <summary>
        /// ���ݽ���ʱ��ͳ��������������¼��    
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthGetCount(string BeginDate, string EndDate, int intflag, out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strSQL = null;
            if (intflag == 0)
            {
                strSQL = @"select d.groupname_vchr, count(case c.recipeflag_int when 1 then 1 end) as ����, 
                                     count(case c.recipeflag_int when 2 then 1 end) as ���� 
                                from t_opr_outpatientrecipeinv a,
                                     t_opr_reciperelation b,
                                     t_opr_outpatientrecipe c,
                                     t_bse_groupdesc d
                               where a.outpatrecipeid_chr = b.seqid
                                 and b.outpatrecipeid_chr = c.outpatrecipeid_chr
                                 and (a.status_int = 1 or a.status_int = 3)
                                 and a.balanceflag_int = 1
                                 and (a.isvouchers_int <> 2 or a.isvouchers_int is null)
                                 and (a.balance_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                                 and c.groupid_chr = d.groupid_chr
                             group by d.groupname_vchr  ";
            }
            else
            {
                strSQL = @"select d.groupname_vchr, count(case c.recipeflag_int when 1 then 1 end) as ����, 
                                     count(case c.recipeflag_int when 2 then 1 end) as ���� 
                                from t_opr_outpatientrecipeinv a,
                                     t_opr_reciperelation b,
                                     t_opr_outpatientrecipe c,
                                     t_bse_groupdesc d
                               where a.outpatrecipeid_chr = b.seqid
                                 and b.outpatrecipeid_chr = c.outpatrecipeid_chr
                                 and (a.status_int = 1 or a.status_int = 3)
��������������������������������       and a.chargedeptid_chr not in('0000198','0000199','0000200')
                                 and a.balanceflag_int = 1
                                 and (a.balance_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                                 and c.groupid_chr = d.groupid_chr
                             group by d.groupname_vchr  ";
            }

            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(2, out parm);
                parm[0].Value = Convert.ToDateTime(BeginDate).ToString("yyyy-MM-dd") + " 00:00:00";
                parm[1].Value = Convert.ToDateTime(EndDate).ToString("yyyy-MM-dd") + " 23:59:59";
                lngRes = svc.lngGetDataTableWithParameters(strSQL, ref dt, parm);

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

        #region ���ݽ���ʱ��ͳ��רҵ�飭>ҽ����������
        /// <summary>
        /// ���ݽ���ʱ��ͳ��רҵ�飭>ҽ����������
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSeeDoctorPersonNums(string BeginDate, string EndDate, int intflag, out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strSQL = null;
            if (intflag == 0)
            {
                strSQL = @"SELECT   d.groupname_vchr, COUNT (c.patientid_chr) as �������� 
                              FROM t_opr_outpatientrecipeinv a,
                              t_opr_reciperelation b,
                              t_opr_outpatientrecipe c,
                              t_bse_groupdesc d
                              WHERE a.outpatrecipeid_chr = b.seqid
                              AND b.outpatrecipeid_chr = c.outpatrecipeid_chr
                              AND (a.status_int = 1 OR a.status_int = 3)
                              and( a.isvouchers_int <> 2 or a.isvouchers_int is null)
                              AND (( c.recipeflag_int=1  AND a.balanceflag_int = 1) or (c.registerid_chr is not null and a.balanceflag_int=0))
                              and (a.balance_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                              and c.groupid_chr = d.groupid_chr
                             group by d.groupname_vchr  ";
            }
            else
            {
                strSQL = @"SELECT   d.groupname_vchr, COUNT (c.patientid_chr) as �������� 
                              FROM t_opr_outpatientrecipeinv a,
                              t_opr_reciperelation b,
                              t_opr_outpatientrecipe c,
                              t_bse_groupdesc d
                              WHERE a.outpatrecipeid_chr = b.seqid
                              AND b.outpatrecipeid_chr = c.outpatrecipeid_chr
                              AND (a.status_int = 1 OR a.status_int = 3)
                              and( a.isvouchers_int <> 2 or a.isvouchers_int is null)
                              and a.chargedeptid_chr not in('0000198','0000199','0000200')
                              AND (( c.recipeflag_int=1  AND a.balanceflag_int = 1) or (c.registerid_chr is not null and a.balanceflag_int=0))
                              and (a.balance_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                              and c.groupid_chr = d.groupid_chr
                             group by d.groupname_vchr  ";
            }

            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(2, out parm);
                parm[0].Value = Convert.ToDateTime(BeginDate).ToString("yyyy-MM-dd") + " 00:00:00";
                parm[1].Value = Convert.ToDateTime(EndDate).ToString("yyyy-MM-dd") + " 23:59:59";
                lngRes = svc.lngGetDataTableWithParameters(strSQL, ref dt, parm);

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

        #region ��ȡ�����ֶε���
        [AutoComplete]
        public long m_mthReportColumns(out DataTable dt, string strEx)
        {
            dt = new DataTable();
            long lngRes = 0;
            if (strEx.Trim() == "")
            {
                strEx = "0066";
            }
            string strSQL = @"Select * From T_AID_RPT_GOP_DEF  where RPTID_CHR = '" + strEx + "' order by GROUPID_CHR";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);

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
        #region ��������������ҩ���
        [AutoComplete]
        public long m_mthGetUsingMedicine(int Flag, out DataTable dt, string strID, DateTime date, DateTime date2, string strEx)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strSubSQL = "";
            if (Flag == 1)
            {
                strSubSQL = "	AND B.DIAGDR_CHR ='" + strID + "'";
            }
            if (Flag == 2)
            {
                strSubSQL = "AND	B.DIAGDEPT_CHR ='" + strID + "'";
            }
            string strSQL = @"SELECT   *   FROM (SELECT a.*, b.itemname_vchr, b.itemcode_vchr,b.ITEMSPEC_VCHR
  FROM (SELECT   a.itemid_chr, a.unitprice_mny, a.unitid_chr,
                 SUM (a.tolqty_dec) AS COUNT, SUM (a.tolprice_mny)
                                                                  AS summoney
            FROM t_opr_outpatientpwmrecipede a, t_opr_outpatientrecipe b
           WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
             AND b.pstauts_int = 2
            " + strSubSQL + @"
             AND b.RECORDDATE_DAT BETWEEN TO_DATE('" + date.ToString("yyyy-MM-dd 00:00:00") + "','yyyy-mm-dd hh24:mi:ss') " +
                " AND TO_DATE('" + date2.ToString("yyyy-MM-dd 23:59:59") + @" ','yyyy-mm-dd hh24:mi:ss')
        GROUP BY a.itemid_chr, a.unitprice_mny, a.unitid_chr) a,
       t_bse_chargeitem b
 WHERE a.itemid_chr = b.itemid_chr(+)
UNION
SELECT a.*, b.itemname_vchr, b.itemcode_vchr,b.ITEMSPEC_VCHR
  FROM (SELECT   a.itemid_chr, a.unitprice_mny, a.unitid_chr,
                 SUM (a.qty_dec) AS COUNT, SUM (a.tolprice_mny) AS summoney
            FROM t_opr_outpatientcmrecipede a, t_opr_outpatientrecipe b
           WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
             AND b.pstauts_int = 2
              " + strSubSQL + @"
			 AND b.RECORDDATE_DAT BETWEEN TO_DATE('" + date.ToString("yyyy-MM-dd 00:00:00") + "','yyyy-mm-dd hh24:mi:ss') " +
                " AND TO_DATE('" + date2.ToString("yyyy-MM-dd 23:59:59") + @" ','yyyy-mm-dd hh24:mi:ss')
        GROUP BY a.itemid_chr, a.unitprice_mny, a.unitid_chr) a,
       t_bse_chargeitem b
 WHERE a.itemid_chr = b.itemid_chr(+)) ORDER BY itemcode_vchr";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);

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

        #region �����¼�¼����ʿִ�м�¼��
        /// <summary>
        /// �����¼�¼����ʿִ�м�¼��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewToT_opr_nurseexecute(out string p_strRecordID, clst_opr_nurseexecute p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = objHRPSvc.m_lngGenerateNewID("t_opr_nurseexecute", "SEQ_INT", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "INSERT INTO t_opr_nurseexecute (SEQ_INT,BUSINESS_INT,TABLENAME_VCHR,OUTPATRECIPEID_CHR,ROWNO_CHR,ITEMID_CHR,EXECTIMES_INT,OPERATORTYPE_INT,OPERATORID_CHR,EXECTIME_DAT,SYSTIME_DAT,REMARK1_VCHR,REMARK2_VCHR,STATUS_INT) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(14, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = Convert.ToInt32(p_strRecordID);
                objLisAddItemRefArr[1].Value = p_objRecord.m_intBUSINESS_INT;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strTABLENAME_VCHR.Trim();
                objLisAddItemRefArr[3].Value = p_objRecord.m_strOUTPATRECIPEID_CHR.Trim();
                objLisAddItemRefArr[4].Value = p_objRecord.m_strROWNO_CHR.Trim();
                objLisAddItemRefArr[5].Value = p_objRecord.m_strITEMID_CHR.Trim();
                objLisAddItemRefArr[6].Value = p_objRecord.m_intEXECTIMES_INT;
                objLisAddItemRefArr[7].Value = p_objRecord.m_intOPERATORTYPE_INT;
                objLisAddItemRefArr[8].Value = p_objRecord.m_strOPERATORID_CHR.Trim();
                objLisAddItemRefArr[9].Value = DateTime.Parse(p_objRecord.m_strEXECTIME_DAT);
                objLisAddItemRefArr[10].Value = DateTime.Parse(strDateTime);
                objLisAddItemRefArr[11].Value = p_objRecord.m_strREMARK1_VCHR.Trim();
                objLisAddItemRefArr[12].Value = p_objRecord.m_strREMARK2_VCHR.Trim();
                objLisAddItemRefArr[13].Value = p_objRecord.m_intSTATUS_INT;
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

        #region ����������¼����ʿִ�м�¼��
        /// <summary>
        /// ����������¼����ʿִ�м�¼��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_clsDataArr"></param>
        /// <param name="p_strRecordIDArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewToT_opr_nurseexecute(clst_opr_nurseexecute[] p_clsDataArr, out string[] p_strRecordIDArr)
        {
            long lngRes = 0;
            int[] intCountArr = new int[p_clsDataArr.Length];
            p_strRecordIDArr = new string[p_clsDataArr.Length];
            int intCount = -1;
            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                for (int i = 0; i < p_clsDataArr.Length; ++i)
                {

                    //��ִ�д���
                    lngRes = m_lngQueryEXECTIMES_INT(p_clsDataArr[i], out intCount);
                    if (lngRes > 0)
                    {
                        if (intCount > 0)//����,
                        {
                            p_clsDataArr[i].m_intEXECTIMES_INT = intCount + 1;
                        }
                        else//��һ��
                        {
                            p_clsDataArr[i].m_intEXECTIMES_INT = 1;
                        }
                        lngRes = m_lngAddNewToT_opr_nurseexecute(out p_strRecordIDArr[i], p_clsDataArr[i]);
                    }
                }
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

        #region �������¼�¼����ʿִ�м�¼��
        /// <summary>
        /// �������¼�¼����ʿִ�м�¼��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_clsDataArr"></param>
        /// <param name="p_strRecordIDArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateMoreT_opr_nurseexecute(int p_intRecordID, clst_opr_nurseexecute p_clsTempData)
        {
            long lngRes = 0;
            try
            {
                clst_opr_nurseexecute p_clsData = null;
                DataTable dt = null;
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = m_lngQueryByID(out p_clsData, p_intRecordID);
                if (lngRes > 0)
                {
                    lngRes = m_lngQueryID(p_clsData, out dt);
                    if (lngRes > 0)
                    {
                        //						#region ��Ҫ���µ�����
                        //						p_clsData.m_strEXECTIME_DAT = p_clsTempData.m_strEXECTIME_DAT ;
                        //						p_clsData.m_strREMARK1_VCHR = p_clsTempData.m_strREMARK1_VCHR;
                        //						p_clsData.m_intOPERATORTYPE_INT = p_clsTempData.m_intOPERATORTYPE_INT ;//����������
                        //						p_clsData.m_strOPERATORID_CHR = p_clsTempData.m_strOPERATORID_CHR ;
                        //						#endregion 
                        //						lngRes = m_lngUpdateStateT_opr_nurseexecute(p_objPrincipal,p_intRecordID,p_clsData);
                        for (int i = 0; i < dt.Rows.Count; ++i)
                        {
                            if (dt.Rows[i]["SYSTIME_DAT"].ToString().Trim() == p_clsData.m_strSYSTIME_DAT.ToString().Trim())
                            {
                                p_intRecordID = Convert.ToInt32(dt.Rows[i]["SEQ_INT"].ToString().Trim());
                                lngRes = m_lngQueryByID(out p_clsData, p_intRecordID);
                                #region ��Ҫ���µ�����
                                p_clsData.m_strEXECTIME_DAT = p_clsTempData.m_strEXECTIME_DAT;
                                p_clsData.m_strREMARK1_VCHR = p_clsTempData.m_strREMARK1_VCHR;
                                p_clsData.m_intOPERATORTYPE_INT = p_clsTempData.m_intOPERATORTYPE_INT;//����������
                                p_clsData.m_strOPERATORID_CHR = p_clsTempData.m_strOPERATORID_CHR;
                                #endregion
                                lngRes = m_lngUpdateStateT_opr_nurseexecute(p_intRecordID, p_clsData);
                            }
                        }
                    }
                }
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

        #region ����ȡID
        /// <summary>
        /// ����ȡID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_clsData"></param>
        /// <param name="p_strEXECTIMES_INT"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngQueryID(clst_opr_nurseexecute p_clsData, out DataTable p_dt)
        {
            long lngRes = 0;
            p_dt = null;
            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                string sql = @"SELECT *
										FROM t_opr_nurseexecute
										WHERE business_int =" + p_clsData.m_intBUSINESS_INT + @" 
										AND outpatrecipeid_chr ='" + p_clsData.m_strOUTPATRECIPEID_CHR.Trim() + @"'
										AND rowno_chr = '" + p_clsData.m_strROWNO_CHR.Trim() + @"'

										AND operatortype_int =" + p_clsData.m_intOPERATORTYPE_INT + @"
										AND status_int = 1 ";
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(sql, ref p_dt);

                if (lngRes > 0)
                {
                }
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

        #region ��ѯִ�д���
        /// <summary>
        /// ��ѯִ�д���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_clsData"></param>
        /// <param name="p_strEXECTIMES_INT"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngQueryEXECTIMES_INT(clst_opr_nurseexecute p_clsData, out int p_strEXECTIMES_INT)
        {
            long lngRes = 0;
            p_strEXECTIMES_INT = 0;
            try
            {
                DataTable objDt = null;
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                string sql = @"SELECT max(exectimes_int) as exectimes_int
										FROM t_opr_nurseexecute
										WHERE business_int =" + p_clsData.m_intBUSINESS_INT + @" 
										AND outpatrecipeid_chr ='" + p_clsData.m_strOUTPATRECIPEID_CHR.Trim() + @"'
										AND rowno_chr = '" + p_clsData.m_strROWNO_CHR.Trim() + @"'
										AND itemid_chr = '" + p_clsData.m_strITEMID_CHR.Trim() + @"'
										AND operatortype_int =" + p_clsData.m_intOPERATORTYPE_INT + @"
										AND status_int = 1 ";
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(sql, ref objDt);

                if (lngRes > 0)
                {
                    if (objDt.Rows.Count > 0)
                    {
                        string str = objDt.Rows[0][0].ToString().Trim();
                        if (str != "")
                            p_strEXECTIMES_INT = Convert.ToInt32(str);
                        else
                            p_strEXECTIMES_INT = 0;
                    }
                }
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

        #region ��ѯǩ���б�
        /// <summary>
        /// ��ѯǩ���б�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_clsData"></param>
        /// <param name="p_strEXECTIMES_INT"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngQueryOPERATORID_CHRAndName(clst_opr_nurseexecute p_clsData, out DataTable p_dtbData, bool p_blnAll)
        {
            long lngRes = 0;
            p_dtbData = null;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                string sql = "";
                if (p_blnAll)//������
                {
                    sql = @"SELECT    A.SEQ_INT,A.OPERATORID_CHR,B.LASTNAME_VCHR,A.EXECTIME_DAT,A.OPERATORTYPE_INT 
										FROM t_opr_nurseexecute A,t_bse_employee B
										WHERE A.OPERATORID_CHR = B.EMPID_CHR(+) 
										AND B.Status_int=1 
										AND A.status_int = 1 ORDER BY A.SYSTIME_DAT DESC";
                }
                else
                {
                    sql = @"SELECT  A.SEQ_INT,A.OPERATORID_CHR,B.LASTNAME_VCHR,A.EXECTIME_DAT,A.OPERATORTYPE_INT 
										FROM t_opr_nurseexecute A,t_bse_employee B
										WHERE A.OPERATORID_CHR = B.EMPID_CHR(+) 
										AND B.Status_int=1 
										AND A.business_int =" + p_clsData.m_intBUSINESS_INT + @" 
										AND A.outpatrecipeid_chr ='" + p_clsData.m_strOUTPATRECIPEID_CHR.Trim() + @"'
										AND A.rowno_chr = '" + p_clsData.m_strROWNO_CHR.Trim() + @"'
										AND A.itemid_chr = '" + p_clsData.m_strITEMID_CHR.Trim() + @"'
										AND A.status_int = 1 ORDER BY A.SYSTIME_DAT DESC";
                }
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(sql, ref p_dtbData);
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

        #region ��ѯǩ��
        /// <summary>
        /// ��ѯǩ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_clsData"></param>
        /// <param name="p_strEXECTIMES_INT"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngQueryByID(out clst_opr_nurseexecute p_clsData, int p_intID)
        {
            long lngRes = 0;
            p_clsData = null;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                string sql = @"SELECT *	FROM t_opr_nurseexecute WHERE SEQ_INT =" + p_intID;
                DataTable dt = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(sql, ref dt);
                if (lngRes > 0)
                {
                    p_clsData = new clst_opr_nurseexecute();
                    if (dt.Rows.Count > 0)
                    {
                        p_clsData.m_intSEQ_INT = Convert.ToInt32(dt.Rows[0]["SEQ_INT"].ToString().Trim());
                        p_clsData.m_intBUSINESS_INT = Convert.ToInt32(dt.Rows[0]["BUSINESS_INT"].ToString().Trim());
                        p_clsData.m_strTABLENAME_VCHR = dt.Rows[0]["TABLENAME_VCHR"].ToString().Trim();
                        p_clsData.m_strOUTPATRECIPEID_CHR = dt.Rows[0]["OUTPATRECIPEID_CHR"].ToString().Trim();
                        p_clsData.m_strROWNO_CHR = dt.Rows[0]["ROWNO_CHR"].ToString().Trim();
                        p_clsData.m_strITEMID_CHR = dt.Rows[0]["ITEMID_CHR"].ToString().Trim();
                        p_clsData.m_intEXECTIMES_INT = Convert.ToInt32(dt.Rows[0]["EXECTIMES_INT"].ToString().Trim());
                        p_clsData.m_intOPERATORTYPE_INT = Convert.ToInt32(dt.Rows[0]["OPERATORTYPE_INT"].ToString().Trim());
                        p_clsData.m_strOPERATORID_CHR = dt.Rows[0]["OPERATORID_CHR"].ToString().Trim();
                        p_clsData.m_strEXECTIME_DAT = dt.Rows[0]["EXECTIME_DAT"].ToString().Trim();
                        p_clsData.m_strSYSTIME_DAT = dt.Rows[0]["SYSTIME_DAT"].ToString().Trim();
                        p_clsData.m_strREMARK1_VCHR = dt.Rows[0]["REMARK1_VCHR"].ToString().Trim();
                        p_clsData.m_strREMARK2_VCHR = dt.Rows[0]["REMARK2_VCHR"].ToString().Trim();
                        p_clsData.m_intSTATUS_INT = Convert.ToInt32(dt.Rows[0]["STATUS_INT"].ToString().Trim());
                    }
                }
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

        #region ���ϼ�¼����ʿִ�м�¼��ĳ�ֶ�,������һ����¼
        /// <summary>
        /// ���ϼ�¼����ʿִ�м�¼��ĳ�ֶβ�����һ����¼
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateStateT_opr_nurseexecute(int p_intRecordID, clst_opr_nurseexecute p_clsData)
        {
            long lngRes = 0;
            string strSQL = "UPDATE t_opr_nurseexecute SET status_int=-1 WHERE seq_int=?";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_intRecordID;
                long lngRecEff = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
                if (lngRes > 0)
                {
                    string strID = "";
                    lngRes = m_lngAddNewToT_opr_nurseexecute(out strID, p_clsData);
                }
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
        /// ���ϼ�¼����ʿִ�м�¼
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateStateT_opr_nurseexecute(clst_opr_nurseexecute p_clsData)
        {
            long lngRes = 0;
            string strSQL = "UPDATE t_opr_nurseexecute SET status_int=-1 ";
            strSQL += @"		WHERE business_int =" + p_clsData.m_intBUSINESS_INT + @" 
										AND outpatrecipeid_chr ='" + p_clsData.m_strOUTPATRECIPEID_CHR.Trim() + @"'
										AND rowno_chr = '" + p_clsData.m_strROWNO_CHR.Trim() + @"'

										AND operatortype_int =" + p_clsData.m_intOPERATORTYPE_INT + @"
										AND status_int = 1  
                                        AND SYSTIME_DAT=to_date('" + p_clsData.m_strSYSTIME_DAT + "','yyyy-MM-dd hh24:mi:ss')";
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

        #region ��ѯǩ����������
        /// <summary>
        /// ��ѯǩ���������� 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_clsData"></param>
        /// <param name="p_strEXECTIMES_INT"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngQueryOPERATORID_CHRAndNameByType(clst_opr_nurseexecute p_clsData, out DataTable p_dtbData)
        {
            long lngRes = 0;
            p_dtbData = null;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                string sql = @"SELECT  A.SEQ_INT,A.OPERATORID_CHR,B.LASTNAME_VCHR,A.EXECTIME_DAT,A.OPERATORTYPE_INT 
										FROM t_opr_nurseexecute A,t_bse_employee B
										WHERE A.OPERATORID_CHR = B.EMPID_CHR(+) 
										AND B.Status_int=1 
										AND A.business_int =" + p_clsData.m_intBUSINESS_INT + @" 
										AND A.outpatrecipeid_chr ='" + p_clsData.m_strOUTPATRECIPEID_CHR.Trim() + @"'
										AND A.rowno_chr = '" + p_clsData.m_strROWNO_CHR.Trim() + @"'
										
										AND A.status_int = 1 ORDER BY A.SYSTIME_DAT DESC";
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(sql, ref p_dtbData);
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
        /// ��ѯǩ��(�����ε�,)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_clsData"></param>
        /// <param name="p_strEXECTIMES_INT"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngQueryNameBybusiness_intAndrecipeid(int p_business_int, string p_outpatrecipeid_chr, int p_intOPERATORTYPE_INT, out DataTable p_dtbData)
        {
            long lngRes = 0;
            p_dtbData = null;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                string sql = @"SELECT  A.SEQ_INT,A.OPERATORID_CHR,B.LASTNAME_VCHR,A.EXECTIME_DAT,A.OPERATORTYPE_INT 
										FROM t_opr_nurseexecute A,t_bse_employee B
										WHERE A.OPERATORID_CHR = B.EMPID_CHR(+) 
										AND B.Status_int=1 
										AND A.business_int =" + p_business_int + @" 
										AND A.outpatrecipeid_chr ='" + p_outpatrecipeid_chr + @"'
										AND A.OPERATORTYPE_INT=" + p_intOPERATORTYPE_INT + @"
										
										AND A.status_int = 1 ORDER BY A.SYSTIME_DAT asc";
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(sql, ref p_dtbData);
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
        /// ��ѯǩ��,����Ѳ�ӵ���ӡ����ǩ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_clsData"></param>
        /// <param name="p_strEXECTIMES_INT"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngQueryNameXUNSHI(clst_opr_nurseexecute p_clsData
            , out string p_strName1, out string p_strName2, out string p_strName3
            , out string p_strREMARK1_VCHR, out string p_strREMARK1_VCHR2
            , out string p_strREMARK1_VCHR3, out string p_strEXECTIME_DAT
            , out string p_strEXECTIME_DAT2, out string p_strEXECTIME_DAT3)
        {
            long lngRes = 0;
            p_strName1 = "";
            p_strName2 = "";
            p_strName3 = "";
            p_strREMARK1_VCHR = "";
            p_strEXECTIME_DAT = "";
            p_strREMARK1_VCHR2 = "";
            p_strEXECTIME_DAT2 = "";
            p_strREMARK1_VCHR3 = "";
            p_strEXECTIME_DAT3 = "";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                string sql = @"SELECT  A.REMARK1_VCHR,A.SEQ_INT,A.OPERATORID_CHR,B.LASTNAME_VCHR,A.EXECTIME_DAT,A.OPERATORTYPE_INT 
										FROM t_opr_nurseexecute A,t_bse_employee B
										WHERE A.OPERATORID_CHR = B.EMPID_CHR(+) 
										AND B.Status_int=1 
										AND A.business_int =" + p_clsData.m_intBUSINESS_INT + @" 
										AND A.outpatrecipeid_chr ='" + p_clsData.m_strOUTPATRECIPEID_CHR.Trim() + @"'
										AND A.rowno_chr = '" + p_clsData.m_strROWNO_CHR.Trim() + @"'
										
										AND A.status_int = 1 ";
                string strOPERATORTYPE_INT1 = " AND A.OPERATORTYPE_INT=4 ORDER BY A.SYSTIME_DAT DESC";//Ѳ��1
                string strOPERATORTYPE_INT2 = " AND A.OPERATORTYPE_INT=5 ORDER BY A.SYSTIME_DAT DESC";//Ѳ��2
                string strOPERATORTYPE_INT3 = " AND A.OPERATORTYPE_INT=6 ORDER BY A.SYSTIME_DAT DESC";//Ѳ��3
                DataTable dtbData = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(sql + strOPERATORTYPE_INT1, ref dtbData);
                if (lngRes > 0)
                {
                    if (dtbData.Rows.Count > 0)
                    {
                        p_strName1 = dtbData.Rows[0]["LASTNAME_VCHR"].ToString().Trim();
                        p_strREMARK1_VCHR = dtbData.Rows[0]["REMARK1_VCHR"].ToString().Trim();
                        p_strEXECTIME_DAT = Convert.ToDateTime(dtbData.Rows[0]["EXECTIME_DAT"]).ToString("yyyy-MM-dd HH:mm");
                    }
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(sql + strOPERATORTYPE_INT2, ref dtbData);
                    if (lngRes > 0)
                    {
                        if (dtbData.Rows.Count > 0)
                        {
                            p_strName2 = dtbData.Rows[0]["LASTNAME_VCHR"].ToString().Trim();
                            p_strREMARK1_VCHR2 = dtbData.Rows[0]["REMARK1_VCHR"].ToString().Trim();
                            p_strEXECTIME_DAT2 = Convert.ToDateTime(dtbData.Rows[0]["EXECTIME_DAT"]).ToString("yyyy-MM-dd HH:mm");
                        }
                        lngRes = objHRPSvc.lngGetDataTableWithoutParameters(sql + strOPERATORTYPE_INT3, ref dtbData);
                        if (lngRes > 0)
                        {
                            if (dtbData.Rows.Count > 0)
                            {
                                p_strName3 = dtbData.Rows[0]["LASTNAME_VCHR"].ToString().Trim();
                                p_strREMARK1_VCHR3 = dtbData.Rows[0]["REMARK1_VCHR"].ToString().Trim();
                                p_strEXECTIME_DAT3 = Convert.ToDateTime(dtbData.Rows[0]["EXECTIME_DAT"]).ToString("yyyy-MM-dd HH:mm");
                            }
                        }
                    }
                }
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


    }

}
