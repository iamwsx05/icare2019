using System;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using com.digitalwave.iCare.middletier.BIHOrderServer;

namespace com.digitalwave.iCare.middletier.HIS
{
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsReport_d_bihSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 根据病区ID查询病区治疗单8
        /// <summary>
        /// 根据病区ID查询病区治疗单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID">病区ID</param>
        /// <param name="p_dtbRecord">查询结果数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBihTreatBill(string p_strAreaID, string p_strBedIDarr, DateTime m_dtBeginDate, DateTime m_dtEndDate, int m_intEXECUTETYPE, int ISRECRUIT_INT, string p_strORDERID_VCHR, out DataTable p_dtbRecord)
        {
            p_dtbRecord = new DataTable();
            string m_strCURBEDID_CHR = "";
            string m_strEXECUTETYPE_INT = "";
            string m_strISRECRUIT_INT = "";


            long lngRes = 0;
            string strSQL = @"
         SELECT     
         T_OPR_BIH_ORDER.ORDERID_CHR, 
         T_BSE_DEPTDESC.DEPTNAME_VCHR,   
         T_BSE_BED.CODE_CHR,   
         t_opr_bih_registerdetail.LASTNAME_VCHR,   
         T_OPR_SETUSAGE.ORDERID_VCHR,   
         T_OPR_BIH_ORDER.EXECUTETYPE_INT,   
         T_OPR_BIH_ORDER.RECIPENO_INT,   
         T_OPR_BIH_ORDER.REGISTERID_CHR,   
         T_OPR_BIH_ORDER.NAME_VCHR,   
         T_OPR_BIH_ORDER.DOSAGE_DEC,   
         T_OPR_BIH_ORDER.DOSAGEUNIT_CHR,   
         T_OPR_BIH_ORDER.EXECFREQNAME_CHR,   
         T_OPR_BIH_ORDER.DOSETYPENAME_CHR,   
         T_OPR_BIH_ORDEREXECUTE.EXECUTEDATE_VCHR,   
         T_OPR_BIH_ORDEREXECUTE.CREATOR_CHR,   
         T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT,
         T_OPR_BIH_ORDEREXECUTE.CREATEDATE_DAT,
         T_OPR_BIH_ORDER.GET_DEC,
         T_OPR_BIH_ORDEREXECUTE.EXECUTEDAYS_INT,
         T_OPR_BIH_ORDEREXECUTE.ISFIRST_INT,
         T_OPR_BIH_ORDER.OUTGETMEDDAYS_INT,
         T_OPR_BIH_ORDER.GETUNIT_CHR
    FROM T_BSE_BED,   
         T_BSE_DEPTDESC,   
         t_opr_bih_registerdetail,   
         T_OPR_BIH_ORDER,   
         T_OPR_BIH_ORDEREXECUTE,   
         T_OPR_SETUSAGE  
   WHERE   
         ( T_OPR_BIH_ORDEREXECUTE.ORDERID_CHR = T_OPR_BIH_ORDER.ORDERID_CHR ) and  
         ( T_OPR_BIH_ORDER.registerid_chr = t_opr_bih_registerdetail.registerid_chr ) and 
         ( T_OPR_BIH_ORDER.CURBEDID_CHR = T_BSE_BED.BEDID_CHR ) and  
         ( T_OPR_BIH_ORDER.CURAREAID_CHR = T_BSE_DEPTDESC.DEPTID_CHR ) and  
         ( T_OPR_SETUSAGE.USAGEID_CHR = T_OPR_BIH_ORDER.DOSETYPEID_CHR ) and  
         (  T_OPR_SETUSAGE.ORDERID_VCHR = ?  ) 
         and
         ( T_OPR_BIH_ORDEREXECUTE.CREATEDATE_DAT between to_date('[startdate]','YYYY-mm-dd hh24:mi:ss')
          AND   to_date('[enddate]','YYYY-mm-dd hh24:mi:ss') ) 
         [CURBEDID_CHR]
         [EXECUTETYPE_INT]
         [ISRECRUIT_INT] 
　　　　　AND  
         ( T_OPR_BIH_ORDER.CURAREAID_CHR = '[CURAREAID_CHR]')
         ORDER BY T_OPR_BIH_ORDER.CURAREAID_CHR ASC,   
         T_OPR_BIH_ORDER.CURBEDID_CHR ASC,   
         T_OPR_BIH_ORDER.PATIENTID_CHR ASC,   
         T_OPR_BIH_ORDER.RECIPENO_INT ASC, 
         T_OPR_BIH_ORDER.ORDERID_CHR ASC,  
         T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT ASC";
            if (!p_strBedIDarr.Trim().Equals(""))
            {
                m_strCURBEDID_CHR = " and T_OPR_BIH_ORDER.CURBEDID_CHR in (" + p_strBedIDarr.Trim() + ")";

            }
            if (m_intEXECUTETYPE != 0)
            {
                m_strEXECUTETYPE_INT = "  and (( T_OPR_BIH_ORDER.EXECUTETYPE_INT=" + m_intEXECUTETYPE.ToString() + " AND T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT=0) ";
            }
            if (ISRECRUIT_INT == 1)
            {
                //m_strISRECRUIT_INT = " OR ( T_OPR_BIH_ORDER.EXECUTETYPE_INT=1 AND T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT=" + ISRECRUIT_INT.ToString() + ")";
                m_strEXECUTETYPE_INT += " OR ( T_OPR_BIH_ORDER.EXECUTETYPE_INT=1 AND T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT=" + ISRECRUIT_INT.ToString() + ")";
            }
            if (m_intEXECUTETYPE != 0)
            {
                m_strEXECUTETYPE_INT += ")";
            }
            if (p_strAreaID != "")
            {
                strSQL = strSQL.Replace("[CURAREAID_CHR]", p_strAreaID);
                strSQL = strSQL.Replace("[startdate]", m_dtBeginDate.ToString());
                strSQL = strSQL.Replace("[enddate]", m_dtEndDate.ToString());
                strSQL = strSQL.Replace("[CURBEDID_CHR]", m_strCURBEDID_CHR);
                strSQL = strSQL.Replace("[EXECUTETYPE_INT]", m_strEXECUTETYPE_INT);
                strSQL = strSQL.Replace("[ISRECRUIT_INT]", m_strISRECRUIT_INT);
            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbRecord);

                System.Data.IDataParameter[] arrParams = null;
                new clsHRPTableService().CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = p_strORDERID_VCHR;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtbRecord, arrParams);

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

        #region 根据病区ID查询静脉推注单 9
        /// <summary>
        /// 根据病区ID查询静脉推注单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID">病区ID</param>
        /// <param name="p_dtbRecord">查询结果数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBihmainline(string p_strAreaID, string p_strBedIDarr, DateTime m_dtBeginDate, DateTime m_dtEndDate, int m_intEXECUTETYPE, int ISRECRUIT_INT, string p_strORDERID_VCHR, out DataTable p_dtbRecord)
        {
            p_dtbRecord = new DataTable();
            string m_strCURBEDID_CHR = "";
            string m_strEXECUTETYPE_INT = "";
            string m_strISRECRUIT_INT = "";

            long lngRes = 0;
            string strSQL = @"SELECT     
                                 T_OPR_BIH_ORDER.ORDERID_CHR, 
                                 T_BSE_DEPTDESC.DEPTNAME_VCHR,   
                                 T_BSE_BED.CODE_CHR,   
                                 t_opr_bih_registerdetail.LASTNAME_VCHR,   
                                 T_OPR_SETUSAGE.ORDERID_VCHR,   
                                 T_OPR_BIH_ORDER.EXECUTETYPE_INT,   
                                 T_OPR_BIH_ORDER.RECIPENO_INT,   
                                 T_OPR_BIH_ORDER.RECIPENO2_INT,   
                                 T_OPR_BIH_ORDER.REGISTERID_CHR,   
                                 T_OPR_BIH_ORDER.NAME_VCHR,   
                                 T_OPR_BIH_ORDER.DOSAGE_DEC,   
                                 T_OPR_BIH_ORDER.DOSAGEUNIT_CHR,   
                                 T_OPR_BIH_ORDER.EXECFREQNAME_CHR,   
                                 T_OPR_BIH_ORDER.DOSETYPENAME_CHR,   
                                 T_OPR_BIH_ORDEREXECUTE.EXECUTEDATE_VCHR,   
                                 T_OPR_BIH_ORDEREXECUTE.CREATOR_CHR,   
                                 T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT,
                                 T_OPR_BIH_ORDEREXECUTE.CREATEDATE_DAT
                                 ,T_OPR_BIH_ORDER.GET_DEC
                                 ,T_OPR_BIH_ORDEREXECUTE.EXECUTEDAYS_INT
                                 ,T_OPR_BIH_ORDEREXECUTE.ISFIRST_INT
                                 ,T_OPR_BIH_ORDER.OUTGETMEDDAYS_INT
                                 ,T_OPR_BIH_ORDER.GETUNIT_CHR
                            FROM T_BSE_BED,   
                                 T_BSE_DEPTDESC,   
                                 t_opr_bih_registerdetail,   
                                 T_OPR_BIH_ORDER,   
                                 T_OPR_BIH_ORDEREXECUTE,   
                                 T_OPR_SETUSAGE  
                           WHERE   
                                 ( T_OPR_BIH_ORDEREXECUTE.ORDERID_CHR = T_OPR_BIH_ORDER.ORDERID_CHR ) and  
                                 ( T_OPR_BIH_ORDER.registerid_chr = t_opr_bih_registerdetail.registerid_chr ) and 
                                 ( T_OPR_BIH_ORDER.CURBEDID_CHR = T_BSE_BED.BEDID_CHR ) and  
                                 ( T_OPR_BIH_ORDER.CURAREAID_CHR = T_BSE_DEPTDESC.DEPTID_CHR ) and  
                                 ( T_OPR_SETUSAGE.USAGEID_CHR = T_OPR_BIH_ORDER.DOSETYPEID_CHR ) and  
                                 ( ( T_OPR_SETUSAGE.ORDERID_VCHR = ? ) )   and
                                 ( T_OPR_BIH_ORDEREXECUTE.CREATEDATE_DAT between to_date('[startdate]','YYYY-mm-dd hh24:mi:ss')
                         AND   to_date('[enddate]','YYYY-mm-dd hh24:mi:ss') )
                                 [CURBEDID_CHR]
                                 [EXECUTETYPE_INT]
                                 [ISRECRUIT_INT]  
                                 AND  
                                 ( T_OPR_BIH_ORDER.CURAREAID_CHR = '[CURAREAID_CHR]')

                        ORDER BY T_OPR_BIH_ORDER.CURAREAID_CHR ASC,   
                                 T_OPR_BIH_ORDER.CURBEDID_CHR ASC,   
                                 T_OPR_BIH_ORDER.PATIENTID_CHR ASC,   
                                 T_OPR_BIH_ORDER.RECIPENO_INT ASC,  
                                 T_OPR_BIH_ORDER.ORDERID_CHR ASC, 
                                 T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT ASC";

            if (!p_strBedIDarr.Trim().Equals(""))
            {
                m_strCURBEDID_CHR = " and T_OPR_BIH_ORDER.CURBEDID_CHR in (" + p_strBedIDarr.Trim() + ")";

            }
            if (m_intEXECUTETYPE != 0)
            {
                m_strEXECUTETYPE_INT = "  and (( T_OPR_BIH_ORDER.EXECUTETYPE_INT=" + m_intEXECUTETYPE.ToString() + " AND T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT=0) ";
            }
            if (ISRECRUIT_INT == 1)
            {
                //m_strISRECRUIT_INT = " OR ( T_OPR_BIH_ORDER.EXECUTETYPE_INT=1 AND T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT=" + ISRECRUIT_INT.ToString() + ")";
                m_strEXECUTETYPE_INT += " OR ( T_OPR_BIH_ORDER.EXECUTETYPE_INT=1 AND T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT=" + ISRECRUIT_INT.ToString() + ")";
            }
            if (m_intEXECUTETYPE != 0)
            {
                m_strEXECUTETYPE_INT += ")";
            }
            if (p_strAreaID != "")
            {
                strSQL = strSQL.Replace("[CURAREAID_CHR]", p_strAreaID);
                strSQL = strSQL.Replace("[startdate]", m_dtBeginDate.ToString());
                strSQL = strSQL.Replace("[enddate]", m_dtEndDate.ToString());
                strSQL = strSQL.Replace("[CURBEDID_CHR]", m_strCURBEDID_CHR);
                strSQL = strSQL.Replace("[EXECUTETYPE_INT]", m_strEXECUTETYPE_INT);
                strSQL = strSQL.Replace("[ISRECRUIT_INT]", m_strISRECRUIT_INT);
            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbRecord);

                System.Data.IDataParameter[] arrParams = null;
                new clsHRPTableService().CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = p_strORDERID_VCHR;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtbRecord, arrParams);

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

        #region 根据病区ID查询病区服药单 10
        /// <summary>
        /// 根据病区ID查询病区服药单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID">病区ID</param>
        /// <param name="p_dtbRecord">查询结果数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBihmedicine(string p_strAreaID, string p_strBedIDarr, DateTime m_dtBeginDate, DateTime m_dtEndDate, int m_intEXECUTETYPE, int ISRECRUIT_INT, string p_strORDERID_VCHR, out DataTable p_dtbRecord)
        {
            p_dtbRecord = new DataTable();
            string m_strCURBEDID_CHR = "";
            string m_strEXECUTETYPE_INT = "";
            string m_strISRECRUIT_INT = "";

            long lngRes = 0;
            string strSQL = @"
        SELECT     
         T_OPR_BIH_ORDER.ORDERID_CHR, 
         T_BSE_DEPTDESC.DEPTNAME_VCHR,   
         T_BSE_BED.CODE_CHR,   
         t_opr_bih_registerdetail.LASTNAME_VCHR,   
         T_OPR_SETUSAGE.ORDERID_VCHR,   
         T_OPR_BIH_ORDER.EXECUTETYPE_INT,   
         T_OPR_BIH_ORDER.RECIPENO_INT,   
         T_OPR_BIH_ORDER.REGISTERID_CHR,   
         T_OPR_BIH_ORDER.NAME_VCHR,   
         T_OPR_BIH_ORDER.DOSAGE_DEC,   
         T_OPR_BIH_ORDER.DOSAGEUNIT_CHR,   
         T_OPR_BIH_ORDER.EXECFREQNAME_CHR,   
         T_OPR_BIH_ORDER.DOSETYPENAME_CHR,   
         T_OPR_BIH_ORDEREXECUTE.EXECUTEDATE_VCHR,   
         T_OPR_BIH_ORDEREXECUTE.CREATOR_CHR,   
         T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT,
         T_OPR_BIH_ORDEREXECUTE.CREATEDATE_DAT
         ,T_OPR_BIH_ORDER.GET_DEC
         ,T_OPR_BIH_ORDEREXECUTE.EXECUTEDAYS_INT
         ,T_OPR_BIH_ORDEREXECUTE.ISFIRST_INT
         ,T_OPR_BIH_ORDER.OUTGETMEDDAYS_INT
         ,T_OPR_BIH_ORDER.GETUNIT_CHR
    FROM T_BSE_BED,   
         T_BSE_DEPTDESC,   
         t_opr_bih_registerdetail,   
         T_OPR_BIH_ORDER,   
         T_OPR_BIH_ORDEREXECUTE,   
         T_OPR_SETUSAGE  
   WHERE   
         ( T_OPR_BIH_ORDEREXECUTE.ORDERID_CHR = T_OPR_BIH_ORDER.ORDERID_CHR ) and  
         ( T_OPR_BIH_ORDER.registerid_chr = t_opr_bih_registerdetail.registerid_chr ) and 
         ( T_OPR_BIH_ORDER.CURBEDID_CHR = T_BSE_BED.BEDID_CHR ) and  
         ( T_OPR_BIH_ORDER.CURAREAID_CHR = T_BSE_DEPTDESC.DEPTID_CHR ) and  
         ( T_OPR_SETUSAGE.USAGEID_CHR = T_OPR_BIH_ORDER.DOSETYPEID_CHR ) and  
         (  T_OPR_SETUSAGE.ORDERID_VCHR = ?  )   and
( T_OPR_BIH_ORDEREXECUTE.CREATEDATE_DAT between to_date('[startdate]','YYYY-mm-dd hh24:mi:ss')
 AND   to_date('[enddate]','YYYY-mm-dd hh24:mi:ss') ) 
          [CURBEDID_CHR]
         [EXECUTETYPE_INT]
         [ISRECRUIT_INT]  
          AND  
         ( T_OPR_BIH_ORDER.CURAREAID_CHR = '[CURAREAID_CHR]')
ORDER BY T_OPR_BIH_ORDER.CURAREAID_CHR ASC,   
         T_OPR_BIH_ORDER.CURBEDID_CHR ASC,   
         T_OPR_BIH_ORDER.PATIENTID_CHR ASC,   
         T_OPR_BIH_ORDER.RECIPENO_INT ASC,   
         T_OPR_BIH_ORDER.ORDERID_CHR ASC,
         T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT ASC
            ";

            if (!p_strBedIDarr.Trim().Equals(""))
            {
                m_strCURBEDID_CHR = " and T_OPR_BIH_ORDER.CURBEDID_CHR in (" + p_strBedIDarr.Trim() + ")";

            }
            if (m_intEXECUTETYPE != 0)
            {
                m_strEXECUTETYPE_INT = "  and (( T_OPR_BIH_ORDER.EXECUTETYPE_INT=" + m_intEXECUTETYPE.ToString() + " AND T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT=0) ";
            }
            if (ISRECRUIT_INT == 1)
            {
                //m_strISRECRUIT_INT = " OR ( T_OPR_BIH_ORDER.EXECUTETYPE_INT=1 AND T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT=" + ISRECRUIT_INT.ToString() + ")";
                m_strEXECUTETYPE_INT += " OR ( T_OPR_BIH_ORDER.EXECUTETYPE_INT=1 AND T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT=" + ISRECRUIT_INT.ToString() + ")";
            }
            if (m_intEXECUTETYPE != 0)
            {
                m_strEXECUTETYPE_INT += ")";
            }
            if (p_strAreaID != "")
            {
                strSQL = strSQL.Replace("[CURAREAID_CHR]", p_strAreaID);
                strSQL = strSQL.Replace("[startdate]", m_dtBeginDate.ToString());
                strSQL = strSQL.Replace("[enddate]", m_dtEndDate.ToString());
                strSQL = strSQL.Replace("[CURBEDID_CHR]", m_strCURBEDID_CHR);
                strSQL = strSQL.Replace("[EXECUTETYPE_INT]", m_strEXECUTETYPE_INT);
                strSQL = strSQL.Replace("[ISRECRUIT_INT]", m_strISRECRUIT_INT);
            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbRecord);

                System.Data.IDataParameter[] arrParams = null;
                new clsHRPTableService().CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = p_strORDERID_VCHR;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtbRecord, arrParams);
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

        #region  获取用法列表
        /// <summary>
        /// 获取用法列表
        /// </summary>
        /// <param name="dtUsage"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllUsage(out DataTable dtUsage)
        {
            dtUsage = new DataTable();
            long lngRes = -1;
            string strSQL = @"select   '-1' as usageid_chr, '全部' as usagename_vchr
                                from dual
                            union all
                            select   usageid_chr, usagename_vchr
                                from t_bse_usagetype ";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtUsage);
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

        #region 根据病区ID查询静脉滴注单 11
        /// <summary>
        /// 根据病区ID查询静脉滴注单 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID">病区ID</param>
        /// <param name="p_strBedIDarr">床位数组</param>
        /// <param name="p_strBeginDate">统计时间</param>
        /// <param name="p_intSerchType">统计方式</param>
        /// <param name="p_strORDERID_VCHR">工作单Id</param>
        /// <param name="p_dtbRecord">查询结果数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBihdriopping(string p_strAreaID, string p_strBedIDarr, string p_strBeginDate, int p_intSerchType, string p_strORDERID_VCHR, out DataTable p_dtbRecord)
        {
            p_dtbRecord = new DataTable();
            string m_strCURBEDID_CHR = "";
            string strPrintFlag = "";
            string strSerchType = "";

            long lngRes = 0;
            string strSQL1 = @"select     
                                 a.code_chr, 
                                 b.deptid_chr,
                                 b.deptname_vchr,   
                                 c.lastname_vchr,   
                                 d.orderid_chr,   
                                 d.executetype_int,   
                                 d.recipeno_int,   
                                 d.recipeno2_int,   
                                 d.registerid_chr,   
                                 d.name_vchr,   
                                 d.dosage_dec,   
                                 d.dosageunit_chr,   
                                 d.execfreqname_chr,   
                                 d.dosetypename_chr,
                                 d.get_dec,
                                 d.outgetmeddays_int,
                                 d.getunit_chr,
                                 d.ifparentid_int,
                                 d.spec_vchr,
                                 d.remark_vchr, 
                                 d.status_int,
                                 e.orderexecid_chr,     
                                 e.executedate_vchr,   
                                 e.creator_chr,   
                                 e.isrecruit_int,
                                 e.createdate_dat,
                                 e.executedays_int,
                                 e.isfirst_int,
                                 e.repare_int,
                                 e.exeareaid_chr,
                                 e.exebedid_chr,
                                 f.orderid_vchr,
                                 g.areaid_chr,
                                 g.bedid_chr,
                                 g.inpatientid_chr,
                                 g.pstatus_int,
                                 c.sex_chr,
                                 d.dosetypeid_chr,
                                 d.curareaid_chr,
                                 d.curbedid_chr,
                                 g.patientid_chr
                            from t_bse_bed a,   
                                 t_bse_deptdesc b,   
                                 t_opr_bih_registerdetail c,   
                                 t_opr_bih_order d,   
                                 t_opr_bih_orderexecute e,   
                                 t_opr_setusage f,
                                 t_opr_bih_register g  
                           WHERE [printFlag] 
                                 c.registerid_chr = g.registerid_chr and
                                 e.orderid_chr = d.orderid_chr and  
                                 d.registerid_chr = c.registerid_chr and                                
                                 f.usageid_chr = d.dosetypeid_chr and
                                 g.areaid_chr = b.deptid_chr  and
                                 g.bedid_chr = a.bedid_chr(+) and   
                                 f.orderid_vchr = ? and 
                                 d.sourcetype_int = 0 and                   
                                 g.areaid_chr = ? 
                                 [CURBEDID_CHR]
                                 [serchType] ";

            string strSQL2 = @"select     
                                 a.code_chr, 
                                 b.deptid_chr,
                                 b.deptname_vchr,   
                                 c.lastname_vchr,   
                                 d.orderid_chr,   
                                 d.executetype_int,   
                                 d.recipeno_int,   
                                 d.recipeno2_int,   
                                 d.registerid_chr,   
                                 d.name_vchr,   
                                 d.dosage_dec,   
                                 d.dosageunit_chr,   
                                 d.execfreqname_chr,   
                                 d.dosetypename_chr,
                                 d.get_dec,
                                 d.outgetmeddays_int,
                                 d.getunit_chr,
                                 d.ifparentid_int,
                                 d.spec_vchr,
                                 d.remark_vchr, 
                                 d.status_int,
                                 e.orderexecid_chr,     
                                 e.executedate_vchr,   
                                 e.creator_chr,   
                                 e.isrecruit_int,
                                 e.createdate_dat,
                                 e.executedays_int,
                                 e.isfirst_int,
                                 e.repare_int,
                                 e.exeareaid_chr,
                                 e.exebedid_chr,
                                 f.orderid_vchr,
                                 g.areaid_chr,
                                 g.bedid_chr,
                                 g.inpatientid_chr,
                                 g.pstatus_int,
                                 c.sex_chr,
                                 d.dosetypeid_chr,
                                 d.curareaid_chr,
                                 d.curbedid_chr,
                                 g.patientid_chr
                            from t_bse_bed a,   
                                 t_bse_deptdesc b,   
                                 t_opr_bih_registerdetail c,   
                                 t_opr_bih_order d,   
                                 t_opr_bih_orderexecute e,   
                                 t_opr_setusage f,
                                 t_opr_bih_register g  
                           WHERE [printFlag] 
                                 c.registerid_chr = g.registerid_chr and
                                 e.orderid_chr = d.orderid_chr and  
                                 d.registerid_chr = c.registerid_chr and 
                                 d.createareaid_chr = b.deptid_chr and
                                 f.usageid_chr = d.dosetypeid_chr and  
                                 g.bedid_chr = a.bedid_chr(+) and
                                 f.orderid_vchr = ? and     
                                 d.sourcetype_int = 1 and                      
                                 d.createareaid_chr = ? 
                                 [CURBEDID_CHR]
                                 [serchType] ";

            if (!p_strBedIDarr.Trim().Equals(""))
            {
                m_strCURBEDID_CHR = " and c.registerid_chr in (" + p_strBedIDarr.Trim() + ")";

            }

            if (p_intSerchType == 0)
            {
                p_strBeginDate = DateTime.Parse(p_strBeginDate).ToShortDateString();

                strSerchType = " and e.createdate_dat between to_date('" + p_strBeginDate + " 00:00:00" + @"', 'YYYY-mm-dd hh24:mi:ss') and
                                      to_date('" + p_strBeginDate + " 23:59:59" + "', 'YYYY-mm-dd hh24:mi:ss')";
            }
            else if (p_intSerchType == 1)
            {
                strPrintFlag = @"not exists ( select t2.seq_int from t_opr_bih_oeprint t2 where t2.orderid_vchr = f.orderid_vchr and
                                                   t2.orderexecid_chr = e.orderexecid_chr and
                                                   t2.areaid_chr = d.curareaid_chr ) and";

                p_strBeginDate = DateTime.Today.ToShortDateString();
                strSerchType = @" and e.createdate_dat between to_date('" + p_strBeginDate + " 00:00:00" + @"', 'YYYY-mm-dd hh24:mi:ss')
                                  and to_date('" + p_strBeginDate + " 23:59:59" + "', 'YYYY-mm-dd hh24:mi:ss')";
            }
            else if (p_intSerchType == 2)
            {
                strSerchType = " and e.createdate_dat > to_date('" + p_strBeginDate + "', 'YYYY-mm-dd hh24:mi:ss')";
            }
            else if (p_intSerchType == 3)
            {
                strPrintFlag = @"not exists ( select t2.seq_int from t_opr_bih_oeprint t2 where t2.orderid_vchr = f.orderid_vchr and
                                                   t2.orderexecid_chr = e.orderexecid_chr and
                                                   t2.areaid_chr = d.curareaid_chr ) and";

                p_strBeginDate = DateTime.Today.ToShortDateString();
                strSerchType = @" and (e.createdate_dat is not null) 
                                  and (d.createdate_dat between to_date('" + p_strBeginDate + " 00:00:00" + @"', 'yyyy-mm-dd hh24:mi:ss')
                                                            and to_date('" + p_strBeginDate + " 23:59:59" + "', 'YYYY-mm-dd hh24:mi:ss'))";
            }

            if (p_strAreaID != "")
            {
                strSQL1 = strSQL1.Replace("[printFlag]", strPrintFlag);
                strSQL1 = strSQL1.Replace("[serchType]", strSerchType);
                strSQL1 = strSQL1.Replace("[CURBEDID_CHR]", m_strCURBEDID_CHR);
                strSQL2 = strSQL2.Replace("[printFlag]", strPrintFlag);
                strSQL2 = strSQL2.Replace("[serchType]", strSerchType);
                strSQL2 = strSQL2.Replace("[CURBEDID_CHR]", m_strCURBEDID_CHR);
            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbRecord);

                System.Data.IDataParameter[] arrParams = null;
                objHRPSvc.CreateDatabaseParameter(2, out arrParams);
                arrParams[0].Value = p_strORDERID_VCHR;
                arrParams[1].Value = p_strAreaID;
                //arrParams[2].Value = p_strAreaID;

                DataTable dtbTemp = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL1, ref dtbTemp, arrParams);
                p_dtbRecord = dtbTemp.Clone();
                if (lngRes > 0 && dtbTemp.Rows.Count > 0)
                {
                    p_dtbRecord.BeginLoadData();
                    if (p_dtbRecord != null && dtbTemp != null && dtbTemp.Rows.Count > 0)
                        p_dtbRecord.Merge(dtbTemp);
                    p_dtbRecord.EndLoadData();
                    p_dtbRecord.AcceptChanges();
                }

                arrParams = null;
                objHRPSvc.CreateDatabaseParameter(2, out arrParams);
                arrParams[0].Value = p_strORDERID_VCHR;
                arrParams[1].Value = p_strAreaID;
                dtbTemp = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL2, ref dtbTemp, arrParams);
                if (lngRes > 0 && dtbTemp.Rows.Count > 0)
                {
                    p_dtbRecord.BeginLoadData();
                    if (p_dtbRecord != null && dtbTemp != null && dtbTemp.Rows.Count > 0)
                        p_dtbRecord.Merge(dtbTemp);
                    p_dtbRecord.EndLoadData();
                    p_dtbRecord.AcceptChanges();
                }

                objHRPSvc.Dispose();
                objHRPSvc = null;
                strSQL1 = null;
                strSQL2 = null;

                if (p_dtbRecord.Rows.Count > 0)
                {
                    p_dtbRecord.DefaultView.Sort = "curareaid_chr asc,curbedid_chr asc,patientid_chr asc,dosetypeid_chr,recipeno_int asc,orderid_chr asc,isrecruit_int asc,ifparentid_int asc";
                    p_dtbRecord = p_dtbRecord.DefaultView.ToTable();
                    p_dtbRecord.Columns.Remove("curareaid_chr");
                    p_dtbRecord.Columns.Remove("curbedid_chr");
                    p_dtbRecord.Columns.Remove("patientid_chr");
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

        #region 根据病区ID查询肌肉注射单 13
        /// <summary>
        /// 根据病区ID查询肌肉注射单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID">病区ID</param>
        /// <param name="p_dtbRecord">查询结果数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBihject(string p_strAreaID, string p_strBedIDarr, DateTime m_dtBeginDate, DateTime m_dtEndDate, int m_intEXECUTETYPE, int ISRECRUIT_INT, string p_strORDERID_VCHR, out DataTable p_dtbRecord)
        {
            p_dtbRecord = new DataTable();
            string m_strCURBEDID_CHR = "";
            string m_strEXECUTETYPE_INT = "";
            string m_strISRECRUIT_INT = "";

            long lngRes = 0;
            string strSQL = @"
        SELECT     
         T_OPR_BIH_ORDER.ORDERID_CHR, 
         T_BSE_DEPTDESC.DEPTNAME_VCHR,   
         T_BSE_BED.CODE_CHR,   
         t_opr_bih_registerdetail.LASTNAME_VCHR,   
         T_OPR_SETUSAGE.ORDERID_VCHR,   
         T_OPR_BIH_ORDER.EXECUTETYPE_INT,   
         T_OPR_BIH_ORDER.RECIPENO_INT,   
         T_OPR_BIH_ORDER.RECIPENO2_INT,   
         T_OPR_BIH_ORDER.REGISTERID_CHR,   
         T_OPR_BIH_ORDER.NAME_VCHR,   
         T_OPR_BIH_ORDER.DOSAGE_DEC,   
         T_OPR_BIH_ORDER.DOSAGEUNIT_CHR,   
         T_OPR_BIH_ORDER.EXECFREQNAME_CHR,   
         T_OPR_BIH_ORDER.DOSETYPENAME_CHR,   
         T_OPR_BIH_ORDEREXECUTE.EXECUTEDATE_VCHR,   
         T_OPR_BIH_ORDEREXECUTE.CREATOR_CHR,   
         T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT,
         T_OPR_BIH_ORDEREXECUTE.CREATEDATE_DAT
         ,T_OPR_BIH_ORDER.GET_DEC
         ,T_OPR_BIH_ORDEREXECUTE.EXECUTEDAYS_INT
         ,T_OPR_BIH_ORDEREXECUTE.ISFIRST_INT
         ,T_OPR_BIH_ORDER.OUTGETMEDDAYS_INT
         ,T_OPR_BIH_ORDER.GETUNIT_CHR
    FROM T_BSE_BED,   
         T_BSE_DEPTDESC,   
         t_opr_bih_registerdetail,   
         T_OPR_BIH_ORDER,   
         T_OPR_BIH_ORDEREXECUTE,   
         T_OPR_SETUSAGE  
   WHERE   
         ( T_OPR_BIH_ORDEREXECUTE.ORDERID_CHR = T_OPR_BIH_ORDER.ORDERID_CHR ) and  
         ( T_OPR_BIH_ORDER.registerid_chr = t_opr_bih_registerdetail.registerid_chr ) and 
         ( T_OPR_BIH_ORDER.CURBEDID_CHR = T_BSE_BED.BEDID_CHR ) and  
         ( T_OPR_BIH_ORDER.CURAREAID_CHR = T_BSE_DEPTDESC.DEPTID_CHR ) and  
         ( T_OPR_SETUSAGE.USAGEID_CHR = T_OPR_BIH_ORDER.DOSETYPEID_CHR ) and  
         ( ( T_OPR_SETUSAGE.ORDERID_VCHR = ? ) )   and
         ( T_OPR_BIH_ORDEREXECUTE.CREATEDATE_DAT between to_date('[startdate]','YYYY-mm-dd hh24:mi:ss')
 AND   to_date('[enddate]','YYYY-mm-dd hh24:mi:ss') ) 
         [CURBEDID_CHR]
         [EXECUTETYPE_INT]
         [ISRECRUIT_INT]  
         AND  
         ( T_OPR_BIH_ORDER.CURAREAID_CHR = '[CURAREAID_CHR]')
         
ORDER BY T_OPR_BIH_ORDER.CURAREAID_CHR ASC,   
         T_OPR_BIH_ORDER.CURBEDID_CHR ASC,   
         T_OPR_BIH_ORDER.PATIENTID_CHR ASC,   
         T_OPR_BIH_ORDER.RECIPENO_INT ASC, 
         T_OPR_BIH_ORDER.ORDERID_CHR ASC,  
         T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT ASC
            ";

            if (!p_strBedIDarr.Trim().Equals(""))
            {
                m_strCURBEDID_CHR = " and T_OPR_BIH_ORDER.CURBEDID_CHR in (" + p_strBedIDarr.Trim() + ")";

            }
            if (m_intEXECUTETYPE != 0)
            {
                m_strEXECUTETYPE_INT = "  and (( T_OPR_BIH_ORDER.EXECUTETYPE_INT=" + m_intEXECUTETYPE.ToString() + " AND T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT=0) ";
            }
            if (ISRECRUIT_INT == 1)
            {
                //m_strISRECRUIT_INT = " OR ( T_OPR_BIH_ORDER.EXECUTETYPE_INT=1 AND T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT=" + ISRECRUIT_INT.ToString() + ")";
                m_strEXECUTETYPE_INT += " OR ( T_OPR_BIH_ORDER.EXECUTETYPE_INT=1 AND T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT=" + ISRECRUIT_INT.ToString() + ")";
            }
            if (m_intEXECUTETYPE != 0)
            {
                m_strEXECUTETYPE_INT += ")";
            }
            if (p_strAreaID != "")
            {
                strSQL = strSQL.Replace("[CURAREAID_CHR]", p_strAreaID);
                strSQL = strSQL.Replace("[startdate]", m_dtBeginDate.ToString());
                strSQL = strSQL.Replace("[enddate]", m_dtEndDate.ToString());
                strSQL = strSQL.Replace("[CURBEDID_CHR]", m_strCURBEDID_CHR);
                strSQL = strSQL.Replace("[EXECUTETYPE_INT]", m_strEXECUTETYPE_INT);
                strSQL = strSQL.Replace("[ISRECRUIT_INT]", m_strISRECRUIT_INT);
            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbRecord);

                System.Data.IDataParameter[] arrParams = null;
                new clsHRPTableService().CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = p_strORDERID_VCHR;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtbRecord, arrParams);

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

        #region 根据病区ID查询病区检验单 17
        /// <summary>
        /// 根据病区ID查询病区检验单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID">病区ID</param>
        /// <param name="p_dtbRecord">查询结果数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBihtest(string p_strAreaID, string p_strBedIDarr, DateTime m_dtBeginDate, DateTime m_dtEndDate, int m_intEXECUTETYPE, int ISRECRUIT_INT, string p_strORDERID_VCHR, out DataTable p_dtbRecord)
        {
            p_dtbRecord = new DataTable();
            string m_strCURBEDID_CHR = "";
            string m_strEXECUTETYPE_INT = "";
            string m_strISRECRUIT_INT = "";

            long lngRes = 0;
            string strSQL = @"
     SELECT     
         T_OPR_BIH_ORDER.ORDERID_CHR, 
         T_BSE_DEPTDESC.DEPTNAME_VCHR,   
         T_BSE_BED.CODE_CHR,   
         t_opr_bih_registerdetail.LASTNAME_VCHR,   
         T_OPR_SETUSAGE.ORDERID_VCHR,   
         T_OPR_BIH_ORDER.EXECUTETYPE_INT,   
         T_OPR_BIH_ORDER.RECIPENO_INT,   
         T_OPR_BIH_ORDER.REGISTERID_CHR,   
         T_OPR_BIH_ORDER.NAME_VCHR,   
         T_OPR_BIH_ORDER.DOSAGE_DEC,   
         T_OPR_BIH_ORDER.DOSAGEUNIT_CHR,   
         T_OPR_BIH_ORDER.EXECFREQNAME_CHR,   
         T_OPR_BIH_ORDER.DOSETYPENAME_CHR,   
         T_OPR_BIH_ORDEREXECUTE.EXECUTEDATE_VCHR,   
         T_OPR_BIH_ORDEREXECUTE.CREATOR_CHR,   
         T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT,
         T_OPR_BIH_ORDEREXECUTE.CREATEDATE_DAT
         ,T_OPR_BIH_ORDER.GET_DEC
         ,T_OPR_BIH_ORDEREXECUTE.EXECUTEDAYS_INT
         ,T_OPR_BIH_ORDEREXECUTE.ISFIRST_INT
         ,T_OPR_BIH_ORDER.OUTGETMEDDAYS_INT
         ,T_OPR_BIH_ORDER.GETUNIT_CHR
    FROM T_BSE_BED,   
         T_BSE_DEPTDESC,   
         t_opr_bih_registerdetail,   
         T_OPR_BIH_ORDER,   
         T_OPR_BIH_ORDEREXECUTE,   
         T_OPR_SETUSAGE  
   WHERE   
         ( T_OPR_BIH_ORDEREXECUTE.ORDERID_CHR = T_OPR_BIH_ORDER.ORDERID_CHR ) and  
         ( T_OPR_BIH_ORDER.registerid_chr = t_opr_bih_registerdetail.registerid_chr ) and 
         ( T_OPR_BIH_ORDER.CURBEDID_CHR = T_BSE_BED.BEDID_CHR ) and  
         ( T_OPR_BIH_ORDER.CURAREAID_CHR = T_BSE_DEPTDESC.DEPTID_CHR ) and  
         ( T_OPR_SETUSAGE.USAGEID_CHR = T_OPR_BIH_ORDER.DOSETYPEID_CHR ) and   
         (  T_OPR_SETUSAGE.ORDERID_VCHR = ?  )   and
( T_OPR_BIH_ORDEREXECUTE.CREATEDATE_DAT between to_date('[startdate]','YYYY-mm-dd hh24:mi:ss')
 AND   to_date('[enddate]','YYYY-mm-dd hh24:mi:ss') ) 
         [CURBEDID_CHR]
         [EXECUTETYPE_INT]
         [ISRECRUIT_INT]   
         AND  
         ( T_OPR_BIH_ORDER.CURAREAID_CHR = '[CURAREAID_CHR]')
ORDER BY T_OPR_BIH_ORDER.CURAREAID_CHR ASC,   
         T_OPR_BIH_ORDER.CURBEDID_CHR ASC,   
         T_OPR_BIH_ORDER.PATIENTID_CHR ASC,   
         T_OPR_BIH_ORDER.RECIPENO_INT ASC,  
         T_OPR_BIH_ORDER.ORDERID_CHR ASC, 
         T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT ASC
            ";

            if (!p_strBedIDarr.Trim().Equals(""))
            {
                m_strCURBEDID_CHR = " and T_OPR_BIH_ORDER.CURBEDID_CHR in (" + p_strBedIDarr.Trim() + ")";

            }
            if (m_intEXECUTETYPE != 0)
            {
                m_strEXECUTETYPE_INT = "  and (( T_OPR_BIH_ORDER.EXECUTETYPE_INT=" + m_intEXECUTETYPE.ToString() + " AND T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT=0) ";
            }
            if (ISRECRUIT_INT == 1)
            {
                //m_strISRECRUIT_INT = " OR ( T_OPR_BIH_ORDER.EXECUTETYPE_INT=1 AND T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT=" + ISRECRUIT_INT.ToString() + ")";
                m_strEXECUTETYPE_INT += " OR ( T_OPR_BIH_ORDER.EXECUTETYPE_INT=1 AND T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT=" + ISRECRUIT_INT.ToString() + ")";
            }
            if (m_intEXECUTETYPE != 0)
            {
                m_strEXECUTETYPE_INT += ")";
            }
            if (p_strAreaID != "")
            {
                strSQL = strSQL.Replace("[CURAREAID_CHR]", p_strAreaID);
                strSQL = strSQL.Replace("[startdate]", m_dtBeginDate.ToString());
                strSQL = strSQL.Replace("[enddate]", m_dtEndDate.ToString());
                strSQL = strSQL.Replace("[CURBEDID_CHR]", m_strCURBEDID_CHR);
                strSQL = strSQL.Replace("[EXECUTETYPE_INT]", m_strEXECUTETYPE_INT);
                strSQL = strSQL.Replace("[ISRECRUIT_INT]", m_strISRECRUIT_INT);
            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbRecord);

                System.Data.IDataParameter[] arrParams = null;
                new clsHRPTableService().CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = p_strORDERID_VCHR;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtbRecord, arrParams);

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

        #region 根据病区ID查询病区检查单 18
        /// <summary>
        /// 根据病区ID查询病区检查单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID">病区ID</param>
        /// <param name="p_dtbRecord">查询结果数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBihcheck(string p_strAreaID, string p_strBedIDarr, DateTime m_dtBeginDate, DateTime m_dtEndDate, int m_intEXECUTETYPE, int ISRECRUIT_INT, string p_strORDERID_VCHR, out DataTable p_dtbRecord)
        {
            p_dtbRecord = new DataTable();
            string m_strCURBEDID_CHR = "";
            string m_strEXECUTETYPE_INT = "";
            string m_strISRECRUIT_INT = "";


            long lngRes = 0;
            string strSQL = @"
     SELECT     
         T_OPR_BIH_ORDER.ORDERID_CHR, 
         T_BSE_DEPTDESC.DEPTNAME_VCHR,   
         T_BSE_BED.CODE_CHR,   
         t_opr_bih_registerdetail.LASTNAME_VCHR,   
         T_OPR_SETUSAGE.ORDERID_VCHR,   
         T_OPR_BIH_ORDER.EXECUTETYPE_INT,   
         T_OPR_BIH_ORDER.RECIPENO_INT,   
         T_OPR_BIH_ORDER.REGISTERID_CHR,   
         T_OPR_BIH_ORDER.NAME_VCHR,   
         T_OPR_BIH_ORDER.DOSAGE_DEC,   
         T_OPR_BIH_ORDER.DOSAGEUNIT_CHR,   
         T_OPR_BIH_ORDER.EXECFREQNAME_CHR,   
         T_OPR_BIH_ORDER.DOSETYPENAME_CHR,   
         T_OPR_BIH_ORDEREXECUTE.EXECUTEDATE_VCHR,   
         T_OPR_BIH_ORDEREXECUTE.CREATOR_CHR,   
         T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT,
         T_OPR_BIH_ORDEREXECUTE.CREATEDATE_DAT
         ,T_OPR_BIH_ORDER.GET_DEC
         ,T_OPR_BIH_ORDEREXECUTE.EXECUTEDAYS_INT
         ,T_OPR_BIH_ORDEREXECUTE.ISFIRST_INT
         ,T_OPR_BIH_ORDER.OUTGETMEDDAYS_INT
         ,T_OPR_BIH_ORDER.GETUNIT_CHR
    FROM T_BSE_BED,   
         T_BSE_DEPTDESC,   
         t_opr_bih_registerdetail,   
         T_OPR_BIH_ORDER,   
         T_OPR_BIH_ORDEREXECUTE,   
         T_OPR_SETUSAGE  
   WHERE   
         ( T_OPR_BIH_ORDEREXECUTE.ORDERID_CHR = T_OPR_BIH_ORDER.ORDERID_CHR ) and  
         ( T_OPR_BIH_ORDER.registerid_chr = t_opr_bih_registerdetail.registerid_chr ) and 
         ( T_OPR_BIH_ORDER.CURBEDID_CHR = T_BSE_BED.BEDID_CHR ) and  
         ( T_OPR_BIH_ORDER.CURAREAID_CHR = T_BSE_DEPTDESC.DEPTID_CHR ) and  
         ( T_OPR_SETUSAGE.USAGEID_CHR = T_OPR_BIH_ORDER.DOSETYPEID_CHR ) and  
         ( ( T_OPR_SETUSAGE.ORDERID_VCHR = ? ) )  and 
		 ( T_OPR_BIH_ORDEREXECUTE.CREATEDATE_DAT between to_date('[startdate]','YYYY-mm-dd hh24:mi:ss')
 AND   to_date('[enddate]','YYYY-mm-dd hh24:mi:ss') ) AND  
         ( T_OPR_BIH_ORDER.CURAREAID_CHR = '[CURAREAID_CHR]') 
         [CURBEDID_CHR]
         [EXECUTETYPE_INT]
         [ISRECRUIT_INT]
ORDER BY T_OPR_BIH_ORDER.CURAREAID_CHR ASC,   
         T_OPR_BIH_ORDER.CURBEDID_CHR ASC,   
         T_OPR_BIH_ORDER.PATIENTID_CHR ASC,   
         T_OPR_BIH_ORDER.RECIPENO_INT ASC,   
         T_OPR_BIH_ORDER.ORDERID_CHR ASC,
         T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT ASC
            ";

            if (!p_strBedIDarr.Trim().Equals(""))
            {
                m_strCURBEDID_CHR = " and T_OPR_BIH_ORDER.CURBEDID_CHR in (" + p_strBedIDarr.Trim() + ")";

            }
            if (m_intEXECUTETYPE != 0)
            {
                m_strEXECUTETYPE_INT = "  and (( T_OPR_BIH_ORDER.EXECUTETYPE_INT=" + m_intEXECUTETYPE.ToString() + " AND T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT=0) ";
            }
            if (ISRECRUIT_INT == 1)
            {
                //m_strISRECRUIT_INT = " OR ( T_OPR_BIH_ORDER.EXECUTETYPE_INT=1 AND T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT=" + ISRECRUIT_INT.ToString() + ")";
                m_strEXECUTETYPE_INT += " OR ( T_OPR_BIH_ORDER.EXECUTETYPE_INT=1 AND T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT=" + ISRECRUIT_INT.ToString() + ")";
            }
            if (m_intEXECUTETYPE != 0)
            {
                m_strEXECUTETYPE_INT += ")";
            }
            if (p_strAreaID != "")
            {
                strSQL = strSQL.Replace("[CURAREAID_CHR]", p_strAreaID);
                strSQL = strSQL.Replace("[startdate]", m_dtBeginDate.ToString());
                strSQL = strSQL.Replace("[enddate]", m_dtEndDate.ToString());
                strSQL = strSQL.Replace("[CURBEDID_CHR]", m_strCURBEDID_CHR);
                strSQL = strSQL.Replace("[EXECUTETYPE_INT]", m_strEXECUTETYPE_INT);
                strSQL = strSQL.Replace("[ISRECRUIT_INT]", m_strISRECRUIT_INT);
            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbRecord);

                System.Data.IDataParameter[] arrParams = null;
                new clsHRPTableService().CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = p_strORDERID_VCHR;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtbRecord, arrParams);

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

        #region 根据病区ID查询病区手术单 10
        /// <summary>
        /// 根据病区ID查询病区手术单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID">病区ID</param>
        /// <param name="p_dtbRecord">查询结果数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBihoperation(string p_strAreaID, string p_strBedIDarr, DateTime m_dtBeginDate, DateTime m_dtEndDate, int m_intEXECUTETYPE, int ISRECRUIT_INT, string p_strORDERID_VCHR, out DataTable p_dtbRecord)
        {
            p_dtbRecord = new DataTable();
            string m_strCURBEDID_CHR = "";
            string m_strEXECUTETYPE_INT = "";
            string m_strISRECRUIT_INT = "";

            long lngRes = 0;
            string strSQL = @"
     SELECT     
         T_OPR_BIH_ORDER.ORDERID_CHR, 
         T_BSE_DEPTDESC.DEPTNAME_VCHR,   
         T_BSE_BED.CODE_CHR,   
         t_opr_bih_registerdetail.LASTNAME_VCHR,   
         T_OPR_SETUSAGE.ORDERID_VCHR,   
         T_OPR_BIH_ORDER.EXECUTETYPE_INT,   
         T_OPR_BIH_ORDER.RECIPENO_INT,   
         T_OPR_BIH_ORDER.REGISTERID_CHR,   
         T_OPR_BIH_ORDER.NAME_VCHR,   
         T_OPR_BIH_ORDER.DOSAGE_DEC,   
         T_OPR_BIH_ORDER.DOSAGEUNIT_CHR,   
         T_OPR_BIH_ORDER.EXECFREQNAME_CHR,   
         T_OPR_BIH_ORDER.DOSETYPENAME_CHR,   
         T_OPR_BIH_ORDEREXECUTE.EXECUTEDATE_VCHR,   
         T_OPR_BIH_ORDEREXECUTE.CREATOR_CHR,   
         T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT,
         T_OPR_BIH_ORDEREXECUTE.CREATEDATE_DAT
         ,T_OPR_BIH_ORDER.GET_DEC
         ,T_OPR_BIH_ORDEREXECUTE.EXECUTEDAYS_INT
         ,T_OPR_BIH_ORDEREXECUTE.ISFIRST_INT
         ,T_OPR_BIH_ORDER.OUTGETMEDDAYS_INT
         ,T_OPR_BIH_ORDER.GETUNIT_CHR
    FROM T_BSE_BED,   
         T_BSE_DEPTDESC,   
         t_opr_bih_registerdetail,   
         T_OPR_BIH_ORDER,   
         T_OPR_BIH_ORDEREXECUTE,   
         T_OPR_SETUSAGE  
   WHERE   
         ( T_OPR_BIH_ORDEREXECUTE.ORDERID_CHR = T_OPR_BIH_ORDER.ORDERID_CHR ) and  
         ( T_OPR_BIH_ORDER.registerid_chr = t_opr_bih_registerdetail.registerid_chr ) and 
         ( T_OPR_BIH_ORDER.CURBEDID_CHR = T_BSE_BED.BEDID_CHR ) and  
         ( T_OPR_BIH_ORDER.CURAREAID_CHR = T_BSE_DEPTDESC.DEPTID_CHR ) and  
         ( T_OPR_SETUSAGE.USAGEID_CHR = T_OPR_BIH_ORDER.DOSETYPEID_CHR ) and  
         (  T_OPR_SETUSAGE.ORDERID_VCHR = ?  )   and
( T_OPR_BIH_ORDEREXECUTE.CREATEDATE_DAT between to_date('[startdate]','YYYY-mm-dd hh24:mi:ss')
 AND   to_date('[enddate]','YYYY-mm-dd hh24:mi:ss') )
         [CURBEDID_CHR]
         [EXECUTETYPE_INT]
         [ISRECRUIT_INT]   
         AND  
         ( T_OPR_BIH_ORDER.CURAREAID_CHR = '[CURAREAID_CHR]')
ORDER BY T_OPR_BIH_ORDER.CURAREAID_CHR ASC,   
         T_OPR_BIH_ORDER.CURBEDID_CHR ASC,   
         T_OPR_BIH_ORDER.PATIENTID_CHR ASC,   
         T_OPR_BIH_ORDER.RECIPENO_INT ASC,   
         T_OPR_BIH_ORDER.ORDERID_CHR ASC,
         T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT ASC
            ";

            if (!p_strBedIDarr.Trim().Equals(""))
            {
                m_strCURBEDID_CHR = " and T_OPR_BIH_ORDER.CURBEDID_CHR in (" + p_strBedIDarr.Trim() + ")";

            }
            if (m_intEXECUTETYPE != 0)
            {
                m_strEXECUTETYPE_INT = "  and (( T_OPR_BIH_ORDER.EXECUTETYPE_INT=" + m_intEXECUTETYPE.ToString() + " AND T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT=0) ";
            }
            if (ISRECRUIT_INT == 1)
            {
                //m_strISRECRUIT_INT = " OR ( T_OPR_BIH_ORDER.EXECUTETYPE_INT=1 AND T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT=" + ISRECRUIT_INT.ToString() + ")";
                m_strEXECUTETYPE_INT += " OR ( T_OPR_BIH_ORDER.EXECUTETYPE_INT=1 AND T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT=" + ISRECRUIT_INT.ToString() + ")";
            }
            if (m_intEXECUTETYPE != 0)
            {
                m_strEXECUTETYPE_INT += ")";
            }
            if (p_strAreaID != "")
            {
                strSQL = strSQL.Replace("[CURAREAID_CHR]", p_strAreaID);
                strSQL = strSQL.Replace("[startdate]", m_dtBeginDate.ToString());
                strSQL = strSQL.Replace("[enddate]", m_dtEndDate.ToString());
                strSQL = strSQL.Replace("[CURBEDID_CHR]", m_strCURBEDID_CHR);
                strSQL = strSQL.Replace("[EXECUTETYPE_INT]", m_strEXECUTETYPE_INT);
                strSQL = strSQL.Replace("[ISRECRUIT_INT]", m_strISRECRUIT_INT);
            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbRecord);

                System.Data.IDataParameter[] arrParams = null;
                new clsHRPTableService().CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = p_strORDERID_VCHR;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtbRecord, arrParams);

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

        #region 根据病区ID查询点滴贴瓶单
        /// <summary>
        /// 根据病区ID查询点滴贴瓶单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID">病区ID</param>
        /// <param name="p_strBedIDarr">床位数组</param>
        /// <param name="p_strBeginDate">统计时间</param>
        /// <param name="p_intSerchType">统计方式</param>
        /// <param name="p_ORDERID_VCHR">工作单Id</param>
        /// <param name="p_dtbRecord">查询结果数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBihdroptie(string p_strAreaID, string p_strBedIDarr, string p_strBeginDate, int p_intSerchType, string p_ORDERID_VCHR, out DataTable p_dtbRecord)
        {
            p_dtbRecord = new DataTable();
            string m_strCURBEDID_CHR = "";
            string strPrintFlag = "";
            string strSerchType = "";

            long lngRes = 0;
            string strSQL = @"  select 
                                       f.deptid_chr,
                                       f.deptname_vchr,
                                       e.code_chr,
                                       a.lastname_vchr,
                                       a.sex_chr,
                                       d.orderid_vchr,
                                       b.orderid_chr,
                                       b.executetype_int,
                                       b.recipeno_int,
                                       b.recipeno2_int,
                                       b.registerid_chr,
                                       b.name_vchr,
                                       b.dosage_dec,
                                       b.dosageunit_chr,
                                       b.execfreqid_chr,
                                       b.execfreqname_chr,
                                       b.dosetypename_chr,
                                       b.get_dec,
                                       b.outgetmeddays_int,
                                       b.getunit_chr,
                                       b.attachtimes_int,
                                       b.status_int,
                                       b.orderdicid_chr,
                                       c.executedate_vchr,
                                       c.creator_chr,
                                       c.isrecruit_int,
                                       c.createdate_dat,
                                       c.executedays_int,
                                       c.isfirst_int,
                                       c.orderexecid_chr,
                                       c.repare_int,
                                       c.exeareaid_chr,
                                       c.exebedid_chr,
                                       g.lexectime_vchr,
                                       g.texectime_vchr,
                                       g.times_int,
                                       h.ordername_vchr,
                                       b.spec_vchr,
                                       i.inpatientid_chr,
                                       i.pstatus_int,
                                       i.areaid_chr,
                                       i.bedid_chr
                                  from t_opr_bih_registerdetail a,
                                       t_opr_bih_order          b,
                                       t_opr_bih_orderexecute   c,
                                       t_opr_setusage           d,
                                       t_bse_bed                e,
                                       t_bse_deptdesc           f,
                                       t_aid_recipefreq         g,
                                       t_bse_nurseorder         h,
                                       t_opr_bih_register       i 
                                 where [printflag]
                                       a.registerid_chr = i.registerid_chr and
                                       (c.orderid_chr = b.orderid_chr) and
                                       (b.registerid_chr = a.registerid_chr) and
                                        (i.bedid_chr = e.bedid_chr(+)) and                                     
                                       (b.execfreqid_chr = g.freqid_chr) and
                                        (d.usageid_chr = b.dosetypeid_chr) and
                                        (d.orderid_vchr = h.orderid_int) and
                                        (d.orderid_vchr in (" + p_ORDERID_VCHR + @")) and                                      
                                        ((i.areaid_chr = ? and b.sourcetype_int = 0 and i.areaid_chr = f.deptid_chr) or (b.createareaid_chr = ? and b.sourcetype_int = 1 and b.createareaid_chr = f.deptid_chr))
                                        [curbedid_chr]                                        
                                        [serchtype]
                                      
                                    order by f.code_vchr,   
                                             e.code_chr, 
                                             b.recipeno_int,
                                             c.createdate_dat,   
                                             c.isrecruit_int";

            if (!p_strBedIDarr.Trim().Equals(""))
            {
                m_strCURBEDID_CHR = " and a.registerid_chr in (" + p_strBedIDarr.Trim() + ")";

            }

            if (p_intSerchType == 0)
            {
                p_strBeginDate = DateTime.Parse(p_strBeginDate).ToShortDateString();

                strSerchType = " and c.createdate_dat > to_date('" + p_strBeginDate + " 00:00:00" + @"', 'YYYY-mm-dd hh24:mi:ss') and
                                     c.createdate_dat < to_date('" + p_strBeginDate + " 23:59:59" + "', 'YYYY-mm-dd hh24:mi:ss')";
            }
            else if (p_intSerchType == 1)
            {
                strPrintFlag = @"not exists ( select null from t_opr_bih_oeprint t2 where t2.orderid_vchr = d.orderid_vchr and
                                                               t2.orderexecid_chr = c.orderexecid_chr and
                                                               t2.areaid_chr = b.curareaid_chr) and";

                p_strBeginDate = DateTime.Today.ToShortDateString();
                strSerchType = @" and c.createdate_dat > to_date('" + p_strBeginDate + " 00:00:00" + @"', 'YYYY-mm-dd hh24:mi:ss') 
                                  and c.createdate_dat < to_date('" + p_strBeginDate + " 23:59:59" + "', 'YYYY-mm-dd hh24:mi:ss')";
            }
            else if (p_intSerchType == 2)
            {
                strSerchType = " and c.createdate_dat > to_date('" + p_strBeginDate + "', 'YYYY-mm-dd hh24:mi:ss')";
            }
            else if (p_intSerchType == 3)
            {
                strPrintFlag = @"not exists ( select null from t_opr_bih_oeprint t2 where t2.orderid_vchr = d.orderid_vchr and
                                                               t2.orderexecid_chr = c.orderexecid_chr and
                                                               t2.areaid_chr = b.curareaid_chr) and";

                p_strBeginDate = DateTime.Today.ToShortDateString();
                strSerchType = @" and (c.createdate_dat is not null) 
                                  and (b.createdate_dat between to_date('" + p_strBeginDate + " 00:00:00" + @"', 'yyyy-mm-dd hh24:mi:ss')
                                                            and to_date('" + p_strBeginDate + " 23:59:59" + "', 'YYYY-mm-dd hh24:mi:ss'))";
            }

            if (p_strAreaID != "")
            {
                strSQL = strSQL.Replace("[printflag]", strPrintFlag);
                strSQL = strSQL.Replace("[serchtype]", strSerchType);
                strSQL = strSQL.Replace("[curbedid_chr]", m_strCURBEDID_CHR);

            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbRecord);
                //objHRPSvc.Dispose();

                System.Data.IDataParameter[] parms = null;
                //new clsHRPTableService().CreateDatabaseParameter(3, out arrParams);
                //arrParams[0].Value = p_ORDERID_VCHR;
                //arrParams[1].Value = p_strAreaID;
                //arrParams[2].Value = p_strAreaID;
                new clsHRPTableService().CreateDatabaseParameter(2, out parms);
                parms[0].Value = p_strAreaID;
                parms[1].Value = p_strAreaID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtbRecord, parms);

                if (p_dtbRecord != null && p_dtbRecord.Rows.Count > 0)
                {
                    string Sql = @"select a.orderdicid_chr, b.itemid_chr, b.itemspec_vchr
                                      from t_bse_bih_orderdic a
                                     inner join t_bse_chargeitem b
                                        on a.itemid_chr = b.itemid_chr
                                     where a.orderdicid_chr = ?";
                    DataTable dtTmp = null;
                    foreach (DataRow dr in p_dtbRecord.Rows)
                    {
                        if (dr["spec_vchr"] == DBNull.Value || string.IsNullOrEmpty(dr["spec_vchr"].ToString()) || dr["spec_vchr"].ToString().Trim() == "")
                        {
                            objHRPSvc.CreateDatabaseParameter(1, out parms);
                            parms[0].Value = dr["orderdicid_chr"].ToString();
                            objHRPSvc.lngGetDataTableWithParameters(Sql, ref dtTmp, parms);
                            if (dtTmp != null && dtTmp.Rows.Count > 0)
                            {
                                dr["spec_vchr"] = dtTmp.Rows[0]["itemspec_vchr"];
                            }
                        }
                    }
                    p_dtbRecord.AcceptChanges();
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

        #region 根据病区ID查询饮食单 
        /// <summary>
        /// 根据病区ID查询饮食单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID">病区ID</param>
        /// <param name="p_dtbRecord">查询结果数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBiheat(string p_strAreaID, string p_strBedIDarr, DateTime m_dtBeginDate, DateTime m_dtEndDate, int m_intEXECUTETYPE, int ISRECRUIT_INT, out DataTable p_dtbRecord)
        {
            p_dtbRecord = new DataTable();
            string m_strCURBEDID_CHR = "";
            string m_strEXECUTETYPE_INT = "";
            string m_strISRECRUIT_INT = "";

            long lngRes = 0;
            string strSQL = @"SELECT     
                                 d.ORDERDICID_CHR, 
                                 b.DEPTNAME_VCHR,   
                                 a.CODE_CHR,   
                                 c.LASTNAME_VCHR,   
                                 f.NAME_CHR,
                                 d.ACTIVE_DAT,
                                 e.inpatientid_chr
                            FROM T_BSE_BED a,   
                                 T_BSE_DEPTDESC b,   
                                 t_opr_bih_registerdetail c,   
                                 T_OPR_BIH_PATIENTNURSE d,
                                 T_OPR_BIH_REGISTER e,
                                 T_BSE_BIH_ORDERDIC f,
                                 T_OPR_BIH_ORDEREXECUTE g
                           WHERE  (c.registerid_chr = e.registerid_chr) and 
                                 ( d.registerid_chr = e.registerid_chr ) and 
                                -- ( g.EXEBEDID_CHR = a.BEDID_CHR ) and  
                                -- ( g.EXEAREAID_CHR = b.DEPTID_CHR ) and 
                                  (e.bedid_chr = a.BEDID_CHR(+)) and 
                                  (e.AREAID_CHR = b.DEPTID_CHR)  and  
                                  d.ORDERDICID_CHR = f.ORDERDICID_CHR and
                                  d.ORDEREXECID_CHR = g.ORDEREXECID_CHR and
                                  d.TYPE_INT = 2 and
                                  d.active_int = 1 and
                                  g.REPARE_INT = 0 and
                                 ( d.ACTIVE_DAT between ? AND ?) 
                                  [CURBEDID_CHR]
                                
                                  AND  
                                 -- ( g.EXEAREAID_CHR = ?)
                                 e.AREAID_CHR = ?
                        ORDER BY b.CODE_VCHR ASC,   
                                 a.BED_NO ASC,   
                                 e.INPATIENTID_CHR ASC ";

            if (!p_strBedIDarr.Trim().Equals(""))
            {
                m_strCURBEDID_CHR = " and e.registerid_chr in (" + p_strBedIDarr.Trim() + ")";

            }

            if (p_strAreaID != "")
            {
                strSQL = strSQL.Replace("[CURBEDID_CHR]", m_strCURBEDID_CHR);
            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbRecord);

                System.Data.IDataParameter[] arrParams = null;
                new clsHRPTableService().CreateDatabaseParameter(3, out arrParams);
                arrParams[0].Value = m_dtBeginDate;
                arrParams[1].Value = m_dtEndDate;
                arrParams[2].Value = p_strAreaID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtbRecord, arrParams);
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

        #region 根据病区ID查询新开饮食、护理单
        /// <summary>
        /// 根据病区ID查询新开饮食、护理单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID">病区ID</param>
        /// <param name="p_dtbRecord">查询结果数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBihnewcare(string p_strAreaID, string p_strBedIDarr, string p_strBeginDate, int p_intSerchType, string p_strORDERID_VCHR, out DataTable p_dtbRecord)
        {
            p_dtbRecord = new DataTable();
            string m_strCURBEDID_CHR = "";
            string strPrintFlag = "";
            string strSerchType = "";

            long lngRes = 0;
            string strSQL = @"SELECT
                                 d.ORDEREXECID_CHR,     
                                 d.ORDERDICID_CHR, 
                                 b.DEPTID_CHR,
                                 b.DEPTNAME_VCHR,   
                                 a.CODE_CHR,
                                 c.REGISTERID_CHR,   
                                 c.LASTNAME_VCHR,
                                 c.SEX_CHR,   
                                 f.NAME_CHR,
                                 d.TYPE_INT,
                                 d.ACTIVE_DAT,
                                 e.INPATIENTID_CHR,
                                 e.PSTATUS_INT,
                                 e.AREAID_CHR,
                                 e.BEDID_CHR,
                                 g.REPARE_INT,
                                 g.EXEAREAID_CHR,
                                 g.EXEBEDID_CHR,
                                 h.EXECUTETYPE_INT,
                                 h.STATUS_INT,
                                 '[ORDERID_VCHR]' ORDERID_VCHR 
                            FROM T_BSE_BED a,   
                                 T_BSE_DEPTDESC b,   
                                 t_opr_bih_registerdetail c,   
                                 T_OPR_BIH_PATIENTNURSE d,
                                 T_OPR_BIH_REGISTER e,
                                 T_BSE_BIH_ORDERDIC f,
                                 T_OPR_BIH_ORDEREXECUTE g,
                                 T_OPR_BIH_ORDER h
                           WHERE [printFlag] 
                                 (c.registerid_chr = e.registerid_chr) and 
                                 ( d.registerid_chr = e.registerid_chr ) and 
                                -- ( g.EXEBEDID_CHR = a.BEDID_CHR ) and  
                                -- ( g.EXEAREAID_CHR = b.DEPTID_CHR ) and
                                  (e.bedid_chr = a.BEDID_CHR(+)) and 
                                --  (e.AREAID_CHR = b.DEPTID_CHR)  and    
                                  d.ORDERDICID_CHR = f.ORDERDICID_CHR and
                                  d.ORDEREXECID_CHR = g.ORDEREXECID_CHR and
                                  g.ORDERID_CHR = h.ORDERID_CHR and
                                  g.ISFIRST_INT = 1 and
                                  g.REPARE_INT = 0 and
                                  d.active_int = 1 
                                  [CURBEDID_CHR] AND 
                                 -- (e.AREAID_CHR = ?)
                                  ((e.AREAID_CHR = ? and h.SOURCETYPE_INT = 0 and e.AREAID_CHR = b.DEPTID_CHR) or (h.CREATEAREAID_CHR = ? and h.SOURCETYPE_INT = 1 and h.CREATEAREAID_CHR = b.DEPTID_CHR))
                                -- ( g.EXEAREAID_CHR = ?)
                                 [serchType] ";

            if (!p_strBedIDarr.Trim().Equals(""))
            {
                m_strCURBEDID_CHR = " and e.registerid_chr in (" + p_strBedIDarr.Trim() + ")";

            }

            if (p_intSerchType == 0)
            {
                p_strBeginDate = DateTime.Parse(p_strBeginDate).ToShortDateString();

                strSerchType = " and d.ACTIVE_DAT > to_date('" + p_strBeginDate + " 00:00:00" + @"', 'YYYY-mm-dd hh24:mi:ss') and
                                     d.ACTIVE_DAT < to_date('" + p_strBeginDate + " 23:59:59" + "', 'YYYY-mm-dd hh24:mi:ss')";
            }
            else if (p_intSerchType == 1)
            {
                strPrintFlag = @"not exists ( select t2.seq_int from t_opr_bih_oeprint t2 where t2.orderid_vchr = '" + p_strORDERID_VCHR + @"' and
                                                   t2.orderexecid_chr = d.orderexecid_chr and
                                                   t2.areaid_chr = e.AREAID_CHR ) and";

                p_strBeginDate = DateTime.Today.ToShortDateString();
                strSerchType = @" and d.ACTIVE_DAT > to_date('" + p_strBeginDate + " 00:00:00" + @"', 'YYYY-mm-dd hh24:mi:ss')
                                  and d.ACTIVE_DAT < to_date('" + p_strBeginDate + " 23:59:59" + "', 'YYYY-mm-dd hh24:mi:ss')";
            }
            else if (p_intSerchType == 2)
            {
                strSerchType = " and d.ACTIVE_DAT > to_date('" + p_strBeginDate + "', 'YYYY-mm-dd hh24:mi:ss')";
            }

            if (p_strAreaID != "")
            {
                strSQL = strSQL.Replace("[ORDERID_VCHR]", p_strORDERID_VCHR);
                strSQL = strSQL.Replace("[printFlag]", strPrintFlag);
                strSQL = strSQL.Replace("[serchType]", strSerchType);
                strSQL = strSQL.Replace("[CURBEDID_CHR]", m_strCURBEDID_CHR);
            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbRecord);

                System.Data.IDataParameter[] arrParams = null;
                new clsHRPTableService().CreateDatabaseParameter(2, out arrParams);

                arrParams[0].Value = p_strAreaID;
                arrParams[1].Value = p_strAreaID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtbRecord, arrParams);

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

        #region 根据病区ID查询新开饮食、护理单(带参数25)
        /// <summary>
        /// 根据病区ID查询新开饮食、护理单(带参数25)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID"></param>
        /// <param name="p_strBedIDarr"></param>
        /// <param name="p_strBeginDate"></param>
        /// <param name="p_intSerchType"></param>
        /// <param name="p_strORDERID_VCHR">工作单ID25(新开饮食护理单)</param>
        /// <param name="p_dtbRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBihnewcare_WithParam(string p_strAreaID, string p_strBedIDarr, string p_strBeginDate, int p_intSerchType, string p_strORDERID_VCHR, out DataTable p_dtbRecord)
        {
            p_dtbRecord = new DataTable();
            string m_strCURBEDID_CHR = "";
            string strPrintFlag = "";
            string strSerchType = "";

            long lngRes = 0;
            string strSQL = @"SELECT
                                 g.ORDEREXECID_CHR,     
                                 h.ORDERDICID_CHR, 
                                 b.DEPTID_CHR,
                                 b.DEPTNAME_VCHR,   
                                 a.CODE_CHR,
                                 c.REGISTERID_CHR,   
                                 c.LASTNAME_VCHR,
                                 c.SEX_CHR,   
                                 f.NAME_CHR,
                                 g.createdate_dat ,
                                 e.INPATIENTID_CHR,
                                 e.PSTATUS_INT,
                                 e.AREAID_CHR,
                                 e.BEDID_CHR,
                                 g.REPARE_INT,
                                 g.EXEAREAID_CHR,
                                 g.EXEBEDID_CHR,
                                 h.EXECUTETYPE_INT,
                                 h.STATUS_INT,
                                 h.dosetypename_chr,
                                 '[ORDERID_VCHR]' ORDERID_VCHR 
                            FROM T_BSE_BED a,   
                                 T_BSE_DEPTDESC b,   
                                 t_opr_bih_registerdetail c,
                                 T_OPR_BIH_REGISTER e,
                                 T_BSE_BIH_ORDERDIC f,
                                 T_OPR_BIH_ORDEREXECUTE g,
                                 T_OPR_BIH_ORDER h, 
                                 t_opr_setusage i
                           WHERE [printFlag] 
                               (c.registerid_chr = e.registerid_chr)
                               and (e.bedid_chr = a.bedid_chr(+))
                               and  h.registerid_chr=e.registerid_chr
                               and h.ORDERDICID_CHR = f.ORDERDICID_CHR     
                               and g.orderid_chr = h.orderid_chr
                               and h.dosetypeid_chr = i.usageid_chr
                               and g.isfirst_int = 1
                               and g.repare_int = 0
                               and i.orderid_vchr = ?
                                  [CURBEDID_CHR] AND 
                                  ((e.AREAID_CHR = ? and h.SOURCETYPE_INT = 0 and e.AREAID_CHR = b.DEPTID_CHR) or (h.CREATEAREAID_CHR = ? and h.SOURCETYPE_INT = 1 and h.CREATEAREAID_CHR = b.DEPTID_CHR))
                                 [serchType] ";

            if (!p_strBedIDarr.Trim().Equals(""))
            {
                m_strCURBEDID_CHR = " and e.registerid_chr in (" + p_strBedIDarr.Trim() + ")";

            }

            if (p_intSerchType == 0)
            {
                p_strBeginDate = DateTime.Parse(p_strBeginDate).ToShortDateString();

                strSerchType = " and g.createdate_dat > to_date('" + p_strBeginDate + " 00:00:00" + @"', 'YYYY-mm-dd hh24:mi:ss') and
                                     g.createdate_dat < to_date('" + p_strBeginDate + " 23:59:59" + "', 'YYYY-mm-dd hh24:mi:ss')";
            }
            else if (p_intSerchType == 1)
            {
                strPrintFlag = @"not exists ( select t2.seq_int from t_opr_bih_oeprint t2 where t2.orderid_vchr = '" + p_strORDERID_VCHR + @"' and
                                                   t2.orderexecid_chr = g.orderexecid_chr and
                                                   t2.areaid_chr = e.AREAID_CHR ) and ";

                p_strBeginDate = DateTime.Today.ToShortDateString();
                strSerchType = @" and g.createdate_dat > to_date('" + p_strBeginDate + " 00:00:00" + @"', 'YYYY-mm-dd hh24:mi:ss')
                                  and g.createdate_dat < to_date('" + p_strBeginDate + " 23:59:59" + "', 'YYYY-mm-dd hh24:mi:ss')";
            }
            else if (p_intSerchType == 2)
            {
                strSerchType = " and g.createdate_dat > to_date('" + p_strBeginDate + "', 'YYYY-mm-dd hh24:mi:ss')";
            }

            if (p_strAreaID != "")
            {
                strSQL = strSQL.Replace("[ORDERID_VCHR]", p_strORDERID_VCHR);
                strSQL = strSQL.Replace("[printFlag]", strPrintFlag);
                strSQL = strSQL.Replace("[serchType]", strSerchType);
                strSQL = strSQL.Replace("[CURBEDID_CHR]", m_strCURBEDID_CHR);
            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

                System.Data.IDataParameter[] arrParams = null;
                new clsHRPTableService().CreateDatabaseParameter(3, out arrParams);

                arrParams[0].Value = p_strORDERID_VCHR;
                arrParams[1].Value = p_strAreaID;
                arrParams[2].Value = p_strAreaID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtbRecord, arrParams);

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

        #region 根据病区ID查询饮食护理单
        /// <summary>
        /// 根据病区ID查询饮食护理单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID">病区ID</param>
        /// <param name="p_dtbRecord">查询结果数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBiheatcare(string p_strAreaID, string p_strBedIDarr, string p_strBeginDate, int p_intSerchType, string p_strORDERID_VCHR, out DataTable p_dtbRecord)
        {
            p_dtbRecord = new DataTable();
            string m_strCURBEDID_CHR = "";
            string strPrintFlag = "";
            string strSerchType = "";

            long lngRes = 0;
            string strSQL = @"SELECT
                                 d.ORDEREXECID_CHR,     
                                 d.ORDERDICID_CHR, 
                                 b.DEPTID_CHR,
                                 b.DEPTNAME_VCHR,   
                                 a.CODE_CHR,
                                 c.REGISTERID_CHR,   
                                 c.LASTNAME_VCHR,
                                 c.SEX_CHR,   
                                 f.NAME_CHR,
                                 d.TYPE_INT,
                                 d.ACTIVE_DAT,
                                 e.INPATIENTID_CHR,
                                 e.PSTATUS_INT,
                                 e.AREAID_CHR,
                                 e.BEDID_CHR,
                                 g.REPARE_INT,
                                 g.EXEAREAID_CHR,
                                 g.EXEBEDID_CHR,
                                 h.EXECUTETYPE_INT,
                                 h.STATUS_INT,
                                 '[ORDERID_VCHR]' ORDERID_VCHR 
                            FROM T_BSE_BED a,   
                                 T_BSE_DEPTDESC b,   
                                 t_opr_bih_registerdetail c,   
                                 T_OPR_BIH_PATIENTNURSE d,
                                 T_OPR_BIH_REGISTER e,
                                 T_BSE_BIH_ORDERDIC f,
                                 T_OPR_BIH_ORDEREXECUTE g,
                                 T_OPR_BIH_ORDER h
                           WHERE [printFlag] 
                                 (c.registerid_chr = e.registerid_chr) and 
                                 ( d.registerid_chr = e.registerid_chr ) and 
                                -- ( g.EXEBEDID_CHR = a.BEDID_CHR ) and  
                                -- ( g.EXEAREAID_CHR = b.DEPTID_CHR ) and
                                  (e.bedid_chr = a.BEDID_CHR(+)) and 
                                --  (e.AREAID_CHR = b.DEPTID_CHR)  and    
                                  d.ORDERDICID_CHR = f.ORDERDICID_CHR and
                                  d.ORDEREXECID_CHR = g.ORDEREXECID_CHR and
                                  g.ORDERID_CHR = h.ORDERID_CHR and
                                --  g.ISFIRST_INT = 1 and
                                  g.REPARE_INT = 0 and
                                  d.active_int = 1 
                                  [CURBEDID_CHR] AND 
                                --  (e.AREAID_CHR = ?)
                                  ((e.AREAID_CHR = ? and h.SOURCETYPE_INT = 0 and e.AREAID_CHR = b.DEPTID_CHR) or (h.CREATEAREAID_CHR = ? and h.SOURCETYPE_INT = 1 and h.CREATEAREAID_CHR = b.DEPTID_CHR))
                                -- ( g.EXEAREAID_CHR = ?)
                                 [serchType] ";

            if (!p_strBedIDarr.Trim().Equals(""))
            {
                m_strCURBEDID_CHR = " and e.registerid_chr in (" + p_strBedIDarr.Trim() + ")";

            }

            if (p_intSerchType == 0)
            {
                p_strBeginDate = DateTime.Parse(p_strBeginDate).ToShortDateString();

                strSerchType = " and d.ACTIVE_DAT > to_date('" + p_strBeginDate + " 00:00:00" + @"', 'YYYY-mm-dd hh24:mi:ss') and
                                     d.ACTIVE_DAT < to_date('" + p_strBeginDate + " 23:59:59" + "', 'YYYY-mm-dd hh24:mi:ss')";
            }
            else if (p_intSerchType == 1)
            {
                strPrintFlag = @"not exists ( select t2.seq_int from t_opr_bih_oeprint t2 where t2.orderid_vchr = '" + p_strORDERID_VCHR + @"' and
                                                   t2.orderexecid_chr = d.orderexecid_chr and
                                                   t2.areaid_chr = e.AREAID_CHR ) and";

                p_strBeginDate = DateTime.Today.ToShortDateString();
                strSerchType = @" and e.PSTATUS_INT = 1
                                  and d.ACTIVE_DAT > to_date('" + p_strBeginDate + " 00:00:00" + @"', 'YYYY-mm-dd hh24:mi:ss')
                                  and d.ACTIVE_DAT < to_date('" + p_strBeginDate + " 23:59:59" + "', 'YYYY-mm-dd hh24:mi:ss')";
            }
            else if (p_intSerchType == 2)
            {
                strSerchType = " and d.ACTIVE_DAT > to_date('" + p_strBeginDate + "', 'YYYY-mm-dd hh24:mi:ss')";
            }

            if (p_strAreaID != "")
            {
                strSQL = strSQL.Replace("[ORDERID_VCHR]", p_strORDERID_VCHR);
                strSQL = strSQL.Replace("[printFlag]", strPrintFlag);
                strSQL = strSQL.Replace("[serchType]", strSerchType);
                strSQL = strSQL.Replace("[CURBEDID_CHR]", m_strCURBEDID_CHR);
            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbRecord);

                System.Data.IDataParameter[] arrParams = null;
                new clsHRPTableService().CreateDatabaseParameter(2, out arrParams);

                arrParams[0].Value = p_strAreaID;
                arrParams[1].Value = p_strAreaID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtbRecord, arrParams);

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

        #region 根据病区ID查询饮食护理
        /// <summary>
        /// 根据病区ID查询饮食护理
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID">病区ID</param>
        /// <param name="p_dtbRecord">查询结果数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBiheatcare(string p_strAreaID, string p_strBedIDarr, DateTime m_dtBeginDate, DateTime m_dtEndDate, int m_intEXECUTETYPE, int ISRECRUIT_INT, out DataTable p_dtbRecord)
        {
            p_dtbRecord = new DataTable();
            string m_strCURBEDID_CHR = "";
            string m_strEXECUTETYPE_INT = "";
            string m_strISRECRUIT_INT = "";

            long lngRes = 0;
            string strSQL = @"
                 select a.DEPTNAME_VCHR,a.CODE_CHR,a.LASTNAME_VCHR,b.name_vchr carename1,c.name_vchr carename2 from
                      ( SELECT     
                         distinct
                         T_OPR_BIH_ORDER.registerid_chr,
                         T_BSE_DEPTDESC.DEPTNAME_VCHR,   
                         T_BSE_BED.CODE_CHR,   
                         t_opr_bih_registerdetail.LASTNAME_VCHR
                    FROM T_BSE_BED,   
                         T_BSE_DEPTDESC,   
                         t_opr_bih_registerdetail,   
                         T_OPR_BIH_ORDER,   
                         T_OPR_BIH_ORDEREXECUTE,   
                         T_OPR_SETUSAGE  
                   WHERE   
                         ( T_OPR_BIH_ORDEREXECUTE.ORDERID_CHR = T_OPR_BIH_ORDER.ORDERID_CHR ) and  
                         ( T_OPR_BIH_ORDER.registerid_chr = t_opr_bih_registerdetail.registerid_chr ) and 
                         ( T_OPR_BIH_ORDER.CURBEDID_CHR = T_BSE_BED.BEDID_CHR ) and  
                         ( T_OPR_BIH_ORDER.CURAREAID_CHR = T_BSE_DEPTDESC.DEPTID_CHR ) and  
                         ( T_OPR_SETUSAGE.USAGEID_CHR = T_OPR_BIH_ORDER.DOSETYPEID_CHR ) and  
                         (  T_OPR_SETUSAGE.ORDERID_VCHR  in ('15','16')  )     and
                ( T_OPR_BIH_ORDEREXECUTE.CREATEDATE_DAT between to_date('[startdate]','YYYY-mm-dd hh24:mi:ss')
                 AND   to_date('[enddate]','YYYY-mm-dd hh24:mi:ss') ) 
                         [CURBEDID_CHR]
                         [EXECUTETYPE_INT]
                         [ISRECRUIT_INT]   
                         AND  
                         ( T_OPR_BIH_ORDER.CURAREAID_CHR = '[CURAREAID_CHR]')
                ) 
                a,
                  (
                            SELECT     
                         distinct
                         T_OPR_BIH_ORDER.registerid_chr,
                         T_OPR_BIH_ORDER.name_vchr
                         
                    FROM T_OPR_BIH_ORDER,   
                         T_OPR_BIH_ORDEREXECUTE,   
                         T_OPR_SETUSAGE  ,
                        (SELECT  
                        distinct  
                        T_OPR_BIH_ORDER.registerid_chr,
                        max(T_OPR_BIH_ORDER.executedate_dat) executedate_dat
                         
                    FROM  
                         T_OPR_BIH_ORDER,  
                         T_OPR_BIH_ORDEREXECUTE,    
                         T_OPR_SETUSAGE  
                   WHERE   
                         ( T_OPR_BIH_ORDEREXECUTE.ORDERID_CHR = T_OPR_BIH_ORDER.ORDERID_CHR ) and  
                         ( T_OPR_SETUSAGE.USAGEID_CHR = T_OPR_BIH_ORDER.DOSETYPEID_CHR ) and  
                         (  T_OPR_SETUSAGE.ORDERID_VCHR  in ('15')  )      and
                ( T_OPR_BIH_ORDEREXECUTE.CREATEDATE_DAT between to_date('[startdate]','YYYY-mm-dd hh24:mi:ss')
                 AND   to_date('[enddate]','YYYY-mm-dd hh24:mi:ss') ) 
                         [CURBEDID_CHR]
                         [EXECUTETYPE_INT]
                         [ISRECRUIT_INT]   
                         AND  
                         ( T_OPR_BIH_ORDER.CURAREAID_CHR = '[CURAREAID_CHR]')
                         group by T_OPR_BIH_ORDER.registerid_chr
                         ) k
                  
                   WHERE   
                         ( T_OPR_BIH_ORDEREXECUTE.ORDERID_CHR = T_OPR_BIH_ORDER.ORDERID_CHR ) and  
                        
                         ( T_OPR_SETUSAGE.USAGEID_CHR = T_OPR_BIH_ORDER.DOSETYPEID_CHR ) and  
                         ( T_OPR_BIH_ORDER.registerid_chr=k.registerid_chr and  T_OPR_BIH_ORDER.executedate_dat=k.executedate_dat)
                         and
                         (  T_OPR_SETUSAGE.ORDERID_VCHR  in ('15')  )       and
                ( T_OPR_BIH_ORDEREXECUTE.CREATEDATE_DAT between to_date('[startdate]','YYYY-mm-dd hh24:mi:ss')
                 AND   to_date('[enddate]','YYYY-mm-dd hh24:mi:ss') ) 
                         [CURBEDID_CHR]
                         [EXECUTETYPE_INT]
                         [ISRECRUIT_INT]   
                         AND  
                         ( T_OPR_BIH_ORDER.CURAREAID_CHR = '[CURAREAID_CHR]')
                         ) 
                         b,
                 (   SELECT     
                         distinct
                         T_OPR_BIH_ORDER.registerid_chr,
                         T_OPR_BIH_ORDER.name_vchr
                         
                    FROM T_OPR_BIH_ORDER,   
                         T_OPR_BIH_ORDEREXECUTE,   
                         T_OPR_SETUSAGE  ,
                        (SELECT  
                        distinct  
                        T_OPR_BIH_ORDER.registerid_chr,
                        max(T_OPR_BIH_ORDER.executedate_dat) executedate_dat
                         
                    FROM  
                         T_OPR_BIH_ORDER,  
                         T_OPR_BIH_ORDEREXECUTE,    
                         T_OPR_SETUSAGE  
                   WHERE   
                         ( T_OPR_BIH_ORDEREXECUTE.ORDERID_CHR = T_OPR_BIH_ORDER.ORDERID_CHR ) and  
                         ( T_OPR_SETUSAGE.USAGEID_CHR = T_OPR_BIH_ORDER.DOSETYPEID_CHR ) and  
                         (  T_OPR_SETUSAGE.ORDERID_VCHR  in ('16')  )       and
                ( T_OPR_BIH_ORDEREXECUTE.CREATEDATE_DAT between to_date('[startdate]','YYYY-mm-dd hh24:mi:ss')
                 AND   to_date('[enddate]','YYYY-mm-dd hh24:mi:ss') ) 
                         [CURBEDID_CHR]
                         [EXECUTETYPE_INT]
                         [ISRECRUIT_INT]   
                         AND  
                         ( T_OPR_BIH_ORDER.CURAREAID_CHR = '[CURAREAID_CHR]')
                         group by T_OPR_BIH_ORDER.registerid_chr
                         ) k   
                   WHERE   
                         ( T_OPR_BIH_ORDEREXECUTE.ORDERID_CHR = T_OPR_BIH_ORDER.ORDERID_CHR ) and  
                        
                         ( T_OPR_SETUSAGE.USAGEID_CHR = T_OPR_BIH_ORDER.DOSETYPEID_CHR ) and  
                         ( T_OPR_BIH_ORDER.registerid_chr=k.registerid_chr and  T_OPR_BIH_ORDER.executedate_dat=k.executedate_dat)
                         and
                         (  T_OPR_SETUSAGE.ORDERID_VCHR  in ('16')  )      and
                ( T_OPR_BIH_ORDEREXECUTE.CREATEDATE_DAT between to_date('[startdate]','YYYY-mm-dd hh24:mi:ss')
                 AND   to_date('[enddate]','YYYY-mm-dd hh24:mi:ss') ) 
                         [CURBEDID_CHR]
                         [EXECUTETYPE_INT]
                         [ISRECRUIT_INT]   
                         AND  
                         ( T_OPR_BIH_ORDER.CURAREAID_CHR = '[CURAREAID_CHR]')
                         )
                         c
                         where
                         a.registerid_chr=b.registerid_chr(+)
                         and
                         a.registerid_chr=c.registerid_chr(+)
                ORDER BY   a.DEPTNAME_VCHR asc,   
                         a.CODE_CHR asc,   
                         a.LASTNAME_VCHR asc
            ";

            if (!p_strBedIDarr.Trim().Equals(""))
            {
                m_strCURBEDID_CHR = " and T_OPR_BIH_ORDER.CURBEDID_CHR in (" + p_strBedIDarr.Trim() + ")";

            }
            if (m_intEXECUTETYPE != 0)
            {
                m_strEXECUTETYPE_INT = "  and (( T_OPR_BIH_ORDER.EXECUTETYPE_INT=" + m_intEXECUTETYPE.ToString() + " AND T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT=0) ";
            }
            if (ISRECRUIT_INT == 1)
            {
                //m_strISRECRUIT_INT = " OR ( T_OPR_BIH_ORDER.EXECUTETYPE_INT=1 AND T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT=" + ISRECRUIT_INT.ToString() + ")";
                m_strEXECUTETYPE_INT += " OR ( T_OPR_BIH_ORDER.EXECUTETYPE_INT=1 AND T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT=" + ISRECRUIT_INT.ToString() + ")";
            }
            if (m_intEXECUTETYPE != 0)
            {
                m_strEXECUTETYPE_INT += ")";
            }
            if (p_strAreaID != "")
            {
                strSQL = strSQL.Replace("[CURAREAID_CHR]", p_strAreaID);
                strSQL = strSQL.Replace("[startdate]", m_dtBeginDate.ToString());
                strSQL = strSQL.Replace("[enddate]", m_dtEndDate.ToString());
                strSQL = strSQL.Replace("[CURBEDID_CHR]", m_strCURBEDID_CHR);
                strSQL = strSQL.Replace("[EXECUTETYPE_INT]", m_strEXECUTETYPE_INT);
                strSQL = strSQL.Replace("[ISRECRUIT_INT]", m_strISRECRUIT_INT);
            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbRecord);
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

        #region 根据病区ID查询输液巡视卡（简明）14
        /// <summary>
        /// 根据病区ID查询输液巡视卡（简明）
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID">病区ID</param>
        /// <param name="p_dtbRecord">查询结果数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBihliquidsee(string p_strAreaID, string p_strBedIDarr, DateTime m_dtBeginDate, DateTime m_dtEndDate, int m_intEXECUTETYPE, int ISRECRUIT_INT, string p_strORDERID_VCHR, out DataTable p_dtbRecord)
        {
            p_dtbRecord = new DataTable();
            string m_strCURBEDID_CHR = "";
            string m_strEXECUTETYPE_INT = "";
            string m_strISRECRUIT_INT = "";

            long lngRes = 0;
            string strSQL = @"SELECT     
                                 T_OPR_BIH_ORDER.ORDERID_CHR, 
                                 T_BSE_DEPTDESC.DEPTNAME_VCHR,   
                                 T_BSE_BED.CODE_CHR,   
                                 t_opr_bih_registerdetail.LASTNAME_VCHR,   
                                 T_OPR_SETUSAGE.ORDERID_VCHR,   
                                 T_OPR_BIH_ORDER.EXECUTETYPE_INT,   
                                 T_OPR_BIH_ORDER.RECIPENO_INT,   
                                 T_OPR_BIH_ORDER.REGISTERID_CHR,   
                                 T_OPR_BIH_ORDER.NAME_VCHR,   
                                 T_OPR_BIH_ORDER.DOSAGE_DEC,   
                                 T_OPR_BIH_ORDER.DOSAGEUNIT_CHR,   
                                 T_OPR_BIH_ORDER.EXECFREQNAME_CHR,   
                                 T_OPR_BIH_ORDER.DOSETYPENAME_CHR,   
                                 T_OPR_BIH_ORDEREXECUTE.EXECUTEDATE_VCHR,   
                                 T_OPR_BIH_ORDEREXECUTE.CREATOR_CHR,   
                                 T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT,
                                 T_OPR_BIH_ORDEREXECUTE.CREATEDATE_DAT
                                 ,T_OPR_BIH_ORDER.GET_DEC
                                 ,T_OPR_BIH_ORDEREXECUTE.EXECUTEDAYS_INT
                                 ,T_OPR_BIH_ORDEREXECUTE.ISFIRST_INT
                                 ,T_OPR_BIH_ORDER.OUTGETMEDDAYS_INT
                                 ,T_OPR_BIH_ORDER.GETUNIT_CHR
                            FROM T_BSE_BED,   
                                 T_BSE_DEPTDESC,   
                                 t_opr_bih_registerdetail,   
                                 T_OPR_BIH_ORDER,   
                                 T_OPR_BIH_ORDEREXECUTE,   
                                 T_OPR_SETUSAGE  
                           WHERE   
                                 ( T_OPR_BIH_ORDEREXECUTE.ORDERID_CHR = T_OPR_BIH_ORDER.ORDERID_CHR ) and  
                                 ( T_OPR_BIH_ORDER.registerid_chr = t_opr_bih_registerdetail.registerid_chr ) and 
                                 ( T_OPR_BIH_ORDER.CURBEDID_CHR = T_BSE_BED.BEDID_CHR ) and  
                                 ( T_OPR_BIH_ORDER.CURAREAID_CHR = T_BSE_DEPTDESC.DEPTID_CHR ) and  
                                 ( T_OPR_SETUSAGE.USAGEID_CHR = T_OPR_BIH_ORDER.DOSETYPEID_CHR ) and  
                                 (  T_OPR_SETUSAGE.ORDERID_VCHR = ?  )   and
                        ( T_OPR_BIH_ORDEREXECUTE.CREATEDATE_DAT between to_date('[startdate]','YYYY-mm-dd hh24:mi:ss')
                         AND   to_date('[enddate]','YYYY-mm-dd hh24:mi:ss') )
                                 [CURBEDID_CHR]
                                 [EXECUTETYPE_INT]
                                 [ISRECRUIT_INT]   
                                 AND  
                                 ( T_OPR_BIH_ORDER.CURAREAID_CHR = '[CURAREAID_CHR]')
                        ORDER BY T_OPR_BIH_ORDER.CURAREAID_CHR ASC,   
                                 T_OPR_BIH_ORDER.CURBEDID_CHR ASC,   
                                 T_OPR_BIH_ORDER.PATIENTID_CHR ASC,   
                                 T_OPR_BIH_ORDER.RECIPENO_INT ASC,   
                                 T_OPR_BIH_ORDER.ORDERID_CHR ASC,
                                 T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT ASC
                                    ";

            if (!p_strBedIDarr.Trim().Equals(""))
            {
                m_strCURBEDID_CHR = " and T_OPR_BIH_ORDER.CURBEDID_CHR in (" + p_strBedIDarr.Trim() + ")";

            }
            if (m_intEXECUTETYPE != 0)
            {
                m_strEXECUTETYPE_INT = "  and (( T_OPR_BIH_ORDER.EXECUTETYPE_INT=" + m_intEXECUTETYPE.ToString() + " AND T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT=0) ";
            }
            if (ISRECRUIT_INT == 1)
            {
                //m_strISRECRUIT_INT = " OR ( T_OPR_BIH_ORDER.EXECUTETYPE_INT=1 AND T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT=" + ISRECRUIT_INT.ToString() + ")";
                m_strEXECUTETYPE_INT += " OR ( T_OPR_BIH_ORDER.EXECUTETYPE_INT=1 AND T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT=" + ISRECRUIT_INT.ToString() + ")";
            }
            if (m_intEXECUTETYPE != 0)
            {
                m_strEXECUTETYPE_INT += ")";
            }
            if (p_strAreaID != "")
            {
                strSQL = strSQL.Replace("[CURAREAID_CHR]", p_strAreaID);
                strSQL = strSQL.Replace("[startdate]", m_dtBeginDate.ToString());
                strSQL = strSQL.Replace("[enddate]", m_dtEndDate.ToString());
                strSQL = strSQL.Replace("[CURBEDID_CHR]", m_strCURBEDID_CHR);
                strSQL = strSQL.Replace("[EXECUTETYPE_INT]", m_strEXECUTETYPE_INT);
                strSQL = strSQL.Replace("[ISRECRUIT_INT]", m_strISRECRUIT_INT);
            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbRecord);

                System.Data.IDataParameter[] arrParams = null;
                new clsHRPTableService().CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = p_strORDERID_VCHR;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtbRecord, arrParams);
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

        #region 根据病区ID查询输液巡视卡（详细）
        /// <summary>
        /// 根据病区ID查询输液巡视卡（详细）
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID">病区ID</param>
        /// <param name="p_dtbRecord">查询结果数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBihliquidseedetail(string p_strAreaID, string p_strBedIDarr, DateTime m_dtBeginDate, DateTime m_dtEndDate, int m_intEXECUTETYPE, int ISRECRUIT_INT, string p_strORDERID_VCHR, out DataTable p_dtbRecord)
        {
            p_dtbRecord = new DataTable();
            string m_strCURBEDID_CHR = "";
            string m_strEXECUTETYPE_INT = "";
            string m_strISRECRUIT_INT = "";

            long lngRes = 0;
            string strSQL = @"
         SELECT     
         T_OPR_BIH_ORDER.ORDERID_CHR, 
         T_BSE_DEPTDESC.DEPTNAME_VCHR,   
         T_BSE_BED.CODE_CHR,   
         t_opr_bih_registerdetail.LASTNAME_VCHR,   
         T_OPR_SETUSAGE.ORDERID_VCHR,   
         T_OPR_BIH_ORDER.EXECUTETYPE_INT,   
         T_OPR_BIH_ORDER.RECIPENO_INT,   
         T_OPR_BIH_ORDER.REGISTERID_CHR,   
         T_OPR_BIH_ORDER.NAME_VCHR,   
         T_OPR_BIH_ORDER.DOSAGE_DEC,   
         T_OPR_BIH_ORDER.DOSAGEUNIT_CHR,   
         T_OPR_BIH_ORDER.EXECFREQNAME_CHR,   
         T_OPR_BIH_ORDER.DOSETYPENAME_CHR,   
         T_OPR_BIH_ORDEREXECUTE.EXECUTEDATE_VCHR,   
         T_OPR_BIH_ORDEREXECUTE.CREATOR_CHR,   
         T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT,
         T_OPR_BIH_ORDEREXECUTE.CREATEDATE_DAT
         ,T_OPR_BIH_ORDER.GET_DEC
         ,T_OPR_BIH_ORDEREXECUTE.EXECUTEDAYS_INT
         ,T_OPR_BIH_ORDEREXECUTE.ISFIRST_INT
         ,T_OPR_BIH_ORDER.OUTGETMEDDAYS_INT
         ,T_OPR_BIH_ORDER.GETUNIT_CHR
    FROM T_BSE_BED,   
         T_BSE_DEPTDESC,   
         t_opr_bih_registerdetail,   
         T_OPR_BIH_ORDER,   
         T_OPR_BIH_ORDEREXECUTE,   
         T_OPR_SETUSAGE  
   WHERE   
         ( T_OPR_BIH_ORDEREXECUTE.ORDERID_CHR = T_OPR_BIH_ORDER.ORDERID_CHR ) and  
         ( T_OPR_BIH_ORDER.registerid_chr = t_opr_bih_registerdetail.registerid_chr ) and 
         ( T_OPR_BIH_ORDER.CURBEDID_CHR = T_BSE_BED.BEDID_CHR ) and  
         ( T_OPR_BIH_ORDER.CURAREAID_CHR = T_BSE_DEPTDESC.DEPTID_CHR ) and  
         ( T_OPR_SETUSAGE.USAGEID_CHR = T_OPR_BIH_ORDER.DOSETYPEID_CHR ) and  
         (  T_OPR_SETUSAGE.ORDERID_VCHR = ?  )   and
( T_OPR_BIH_ORDEREXECUTE.CREATEDATE_DAT between to_date('[startdate]','YYYY-mm-dd hh24:mi:ss')
 AND   to_date('[enddate]','YYYY-mm-dd hh24:mi:ss') ) 
         [CURBEDID_CHR]
         [EXECUTETYPE_INT]
         [ISRECRUIT_INT]  
         AND  
         ( T_OPR_BIH_ORDER.CURAREAID_CHR = '[CURAREAID_CHR]')
ORDER BY T_OPR_BIH_ORDER.CURAREAID_CHR ASC,   
         T_OPR_BIH_ORDER.CURBEDID_CHR ASC,   
         T_OPR_BIH_ORDER.PATIENTID_CHR ASC,   
         T_OPR_BIH_ORDER.RECIPENO_INT ASC, 
         T_OPR_BIH_ORDER.ORDERID_CHR ASC,  
         T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT ASC
            ";

            if (!p_strBedIDarr.Trim().Equals(""))
            {
                m_strCURBEDID_CHR = " and T_OPR_BIH_ORDER.CURBEDID_CHR in (" + p_strBedIDarr.Trim() + ")";

            }
            if (m_intEXECUTETYPE != 0)
            {
                m_strEXECUTETYPE_INT = "  and (( T_OPR_BIH_ORDER.EXECUTETYPE_INT=" + m_intEXECUTETYPE.ToString() + " AND T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT=0) ";
            }
            if (ISRECRUIT_INT == 1)
            {
                //m_strISRECRUIT_INT = " OR ( T_OPR_BIH_ORDER.EXECUTETYPE_INT=1 AND T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT=" + ISRECRUIT_INT.ToString() + ")";
                m_strEXECUTETYPE_INT += " OR ( T_OPR_BIH_ORDER.EXECUTETYPE_INT=1 AND T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT=" + ISRECRUIT_INT.ToString() + ")";
            }
            if (m_intEXECUTETYPE != 0)
            {
                m_strEXECUTETYPE_INT += ")";
            }
            if (p_strAreaID != "")
            {
                strSQL = strSQL.Replace("[CURAREAID_CHR]", p_strAreaID);
                strSQL = strSQL.Replace("[startdate]", m_dtBeginDate.ToString());
                strSQL = strSQL.Replace("[enddate]", m_dtEndDate.ToString());
                strSQL = strSQL.Replace("[CURBEDID_CHR]", m_strCURBEDID_CHR);
                strSQL = strSQL.Replace("[EXECUTETYPE_INT]", m_strEXECUTETYPE_INT);
                strSQL = strSQL.Replace("[ISRECRUIT_INT]", m_strISRECRUIT_INT);
            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbRecord);
                System.Data.IDataParameter[] arrParams = null;
                new clsHRPTableService().CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = p_strORDERID_VCHR;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtbRecord, arrParams);
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


        #region 根据病区ID查询确认记帐单
        /// <summary>
        /// 根据病区ID查询确认记帐单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID">病区ID</param>
        /// <param name="p_dtbRecord">查询结果数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBihCharge_zj(string p_strAreaID, string p_strBedIDarr, DateTime m_dtBeginDate, DateTime m_dtEndDate, int m_intEXECUTETYPE, int ISRECRUIT_INT, int NEEDCONFIRM_INT, out DataTable p_dtbRecord)
        {
            p_dtbRecord = new DataTable();
            string m_strCURBEDID_CHR = "";
            string m_strEXECUTETYPE_INT = "";
            string m_strISRECRUIT_INT = "";
            string m_strNEEDCONFIRM_INT = "";

            long lngRes = 0;
            string strSQL = @"select a.ORDEREXECID_CHR,
                               a.CREATEDATE_DAT,
                               a.ISRECRUIT_INT,
                               b.registerid_chr,
                               b.chargeitemid_chr,
                               b.chargeitemname_chr,
                               b.spec_vchr,
                               b.unit_vchr,
                               b.unitprice_dec,
                               b.amount_dec,
                               b.confirm_dat,
                               b.STATUS_INT,
                               c.deptname_vchr CURAREAName_CHR,
                               d.sex_chr,
                               d.lastname_vchr,
                               e.recipeno_int,
                               e.recipeno2_int,
                               f.inpatientid_chr,
                               g.ITEMCODE_VCHR,
                               i.bedid_chr,
                               i.code_chr
                          from T_Opr_Bih_OrderExecute    a,
                               T_Opr_Bih_PatientCharge   b,
                               t_bse_deptdesc            c,
                               t_opr_bih_registerdetail  d,
                               T_OPR_BIH_ORDER           e,
                               t_opr_bih_register        f,
                               t_bse_chargeitem          g,
                               T_OPR_BIH_ORDERCHARGEDEPT h,
                               t_bse_bed                 i
                         where a.orderexecid_chr = b.orderexecid_chr
                              -- and a.EXEAREAID_CHR = c.deptid_chr
                              -- and f.AREAID_CHR = c.deptid_chr
                           and b.registerid_chr = d.registerid_chr
                           and b.registerid_chr = f.registerid_chr
                           and a.orderid_chr = e.orderid_chr
                           and b.chargeitemid_chr = g.itemid_chr
                           and h.orderid_chr = b.orderid_chr
                           and h.chargeitemid_chr = b.chargeitemid_chr
                           and f.bedid_chr = i.bedid_chr(+)
                           and h.FLAG_INT <> 2
                           and a.NEEDCONFIRM_INT = 1
                           and b.ACTIVATETYPE_INT = 3
                           and ( a.CREATEDATE_DAT between to_date('[startdate]','YYYY-mm-dd hh24:mi:ss')
                     AND   to_date('[enddate]','YYYY-mm-dd hh24:mi:ss') ) 
                             [CURBEDID_CHR]
                             [EXECUTETYPE_INT]
                             [ISRECRUIT_INT]  
                             [NEEDCONFIRM_INT]
                            -- ( a.EXEAREAID_CHR = '[CURAREAID_CHR]')
                            -- ( f.AREAID_CHR = '[CURAREAID_CHR]')
and ((f.AREAID_CHR = [CURAREAID_CHR] and e.SOURCETYPE_INT = 0 and f.AREAID_CHR = c.DEPTID_CHR) or (e.CREATEAREAID_CHR = [CURAREAID_CHR] and e.SOURCETYPE_INT = 1 and e.CREATEAREAID_CHR = c.DEPTID_CHR))      
                    ORDER BY c.CODE_VCHR ASC,   
                             a.EXEBEDID_CHR ASC,   
                             e.PATIENTID_CHR ASC,   
                             e.RECIPENO_INT ASC,   
                             a.ISRECRUIT_INT ASC";

            if (!p_strBedIDarr.Trim().Equals(""))
            {
                m_strCURBEDID_CHR = " and f.registerid_chr in (" + p_strBedIDarr.Trim() + ")";

            }
            if (m_intEXECUTETYPE != 0)
            {
                m_strEXECUTETYPE_INT = "  and (( e.EXECUTETYPE_INT=" + m_intEXECUTETYPE.ToString() + " AND a.ISRECRUIT_INT=0) ";
            }
            if (ISRECRUIT_INT == 1)
            {
                //m_strISRECRUIT_INT = " OR ( T_OPR_BIH_ORDER.EXECUTETYPE_INT=1 AND T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT=" + ISRECRUIT_INT.ToString() + ")";
                m_strEXECUTETYPE_INT += " OR ( e.EXECUTETYPE_INT=1 AND a.ISRECRUIT_INT=" + ISRECRUIT_INT.ToString() + ")";
            }
            if (m_intEXECUTETYPE != 0)
            {
                m_strEXECUTETYPE_INT += ")";
            }
            switch (NEEDCONFIRM_INT)//NEEDCONFIRM_INT   0未确认，1已确认，2已作废，3全部
            {
                case -1:
                    m_strNEEDCONFIRM_INT = " and b.confirm_dat is not null";
                    break;
                case 0:
                    m_strNEEDCONFIRM_INT = " and b.confirm_dat is  null ";
                    break;
                case 1:
                    m_strNEEDCONFIRM_INT = " and b.confirm_dat is not null and b.STATUS_INT=1 ";
                    break;
                case 2:
                    m_strNEEDCONFIRM_INT = " and b.confirm_dat is not null and b.STATUS_INT=0 ";
                    break;
                case 3:
                    m_strNEEDCONFIRM_INT = " ";
                    break;
            }
            if (p_strAreaID != "")
            {
                strSQL = strSQL.Replace("[CURAREAID_CHR]", p_strAreaID);
                strSQL = strSQL.Replace("[startdate]", m_dtBeginDate.ToString());
                strSQL = strSQL.Replace("[enddate]", m_dtEndDate.ToString());
                strSQL = strSQL.Replace("[CURBEDID_CHR]", m_strCURBEDID_CHR);
                strSQL = strSQL.Replace("[EXECUTETYPE_INT]", m_strEXECUTETYPE_INT);
                strSQL = strSQL.Replace("[ISRECRUIT_INT]", m_strISRECRUIT_INT);
                //NEEDCONFIRM_INT   0未确认，1已确认，2已作废，3全部
                strSQL = strSQL.Replace("[NEEDCONFIRM_INT]", m_strNEEDCONFIRM_INT);

            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbRecord);
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

        #region 根据病区ID查询确认收费单
        /// <summary>
        /// 根据病区ID查询确认收费单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID">病区ID</param>
        /// <param name="p_dtbRecord">查询结果数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBihCharge_sf(string p_strAreaID, string p_strBedIDarr, DateTime m_dtBeginDate, DateTime m_dtEndDate, int m_intEXECUTETYPE, int ISRECRUIT_INT, int NEEDCONFIRM_INT, out DataTable p_dtbRecord)
        {
            p_dtbRecord = new DataTable();
            string m_strCURBEDID_CHR = "";
            string m_strEXECUTETYPE_INT = "";
            string m_strISRECRUIT_INT = "";
            string m_strNEEDCONFIRM_INT = "";

            long lngRes = 0;
            string strSQL = @"select a.ORDEREXECID_CHR,
                               a.CREATEDATE_DAT,
                               a.ISRECRUIT_INT,
                               b.registerid_chr,
                               b.chargeitemid_chr,
                               b.chargeitemname_chr,
                               b.spec_vchr,
                               b.unit_vchr,
                               b.unitprice_dec,
                               b.amount_dec,
                               b.confirm_dat,
                               b.STATUS_INT,
                               c.deptname_vchr CURAREAName_CHR,
                               d.sex_chr,
                               d.lastname_vchr,
                               e.recipeno_int,
                               e.recipeno2_int,
                               f.inpatientid_chr,
                               g.ITEMCODE_VCHR,
                               i.bedid_chr,
                               i.code_chr
                          from T_Opr_Bih_OrderExecute    a,
                               T_Opr_Bih_PatientCharge   b,
                               t_bse_deptdesc            c,
                               t_opr_bih_registerdetail  d,
                               T_OPR_BIH_ORDER           e,
                               t_opr_bih_register        f,
                               t_bse_chargeitem          g,
                               T_OPR_BIH_ORDERCHARGEDEPT h,
                               t_bse_bed                 i
                         where a.orderexecid_chr = b.orderexecid_chr
                              -- and a.EXEAREAID_CHR = c.deptid_chr
                              -- and f.AREAID_CHR = c.deptid_chr
                           and b.registerid_chr = d.registerid_chr
                           and b.registerid_chr = f.registerid_chr
                           and a.orderid_chr = e.orderid_chr
                           and b.chargeitemid_chr = g.itemid_chr
                           and h.orderid_chr = b.orderid_chr
                           and h.chargeitemid_chr = b.chargeitemid_chr
                           and f.bedid_chr = i.bedid_chr(+)
                           and h.FLAG_INT <> 2
                           and a.NEEDCONFIRM_INT = 1
                           and b.ACTIVATETYPE_INT = 4
                        --and
                        --b.confirmerid_chr is not null
                        --and
                        --b.confirm_dat is not null
                                 and
                             ( a.CREATEDATE_DAT between to_date('[startdate]','YYYY-mm-dd hh24:mi:ss')
                     AND   to_date('[enddate]','YYYY-mm-dd hh24:mi:ss') ) 
                             [CURBEDID_CHR]
                             [EXECUTETYPE_INT]
                             [ISRECRUIT_INT]  
                             [NEEDCONFIRM_INT]
                            --( a.EXEAREAID_CHR = '[CURAREAID_CHR]')
                            -- ( f.AREAID_CHR = '[CURAREAID_CHR]')
and ((f.AREAID_CHR = [CURAREAID_CHR] and e.SOURCETYPE_INT = 0 and f.AREAID_CHR = c.DEPTID_CHR) or (e.CREATEAREAID_CHR = [CURAREAID_CHR] and e.SOURCETYPE_INT = 1 and e.CREATEAREAID_CHR = c.DEPTID_CHR))
                    ORDER BY c.CODE_VCHR ASC,   
                             a.EXEBEDID_CHR ASC,   
                             e.PATIENTID_CHR ASC,   
                             e.RECIPENO_INT ASC,   
                             a.ISRECRUIT_INT ASC";

            if (!p_strBedIDarr.Trim().Equals(""))
            {
                m_strCURBEDID_CHR = " and f.registerid_chr in (" + p_strBedIDarr.Trim() + ")";

            }
            if (m_intEXECUTETYPE != 0)
            {
                m_strEXECUTETYPE_INT = "  and (( e.EXECUTETYPE_INT=" + m_intEXECUTETYPE.ToString() + " AND a.ISRECRUIT_INT=0) ";
            }
            if (ISRECRUIT_INT == 1)
            {
                //m_strISRECRUIT_INT = " OR ( T_OPR_BIH_ORDER.EXECUTETYPE_INT=1 AND T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT=" + ISRECRUIT_INT.ToString() + ")";
                m_strEXECUTETYPE_INT += " OR ( e.EXECUTETYPE_INT=1 AND a.ISRECRUIT_INT=" + ISRECRUIT_INT.ToString() + ")";
            }
            if (m_intEXECUTETYPE != 0)
            {
                m_strEXECUTETYPE_INT += ")";
            }
            switch (NEEDCONFIRM_INT)//NEEDCONFIRM_INT   0未确认，1已确认，2已作废，3全部
            {
                case -1:
                    m_strNEEDCONFIRM_INT = " and b.confirm_dat is not null";
                    break;
                case 0:
                    m_strNEEDCONFIRM_INT = " and b.confirm_dat is  null ";
                    break;
                case 1:
                    m_strNEEDCONFIRM_INT = " and b.confirm_dat is not null and b.STATUS_INT=1 ";
                    break;
                case 2:
                    m_strNEEDCONFIRM_INT = " and b.confirm_dat is not null and b.STATUS_INT=0 ";
                    break;
                case 3:
                    m_strNEEDCONFIRM_INT = " ";
                    break;
            }
            if (p_strAreaID != "")
            {
                strSQL = strSQL.Replace("[CURAREAID_CHR]", p_strAreaID);
                strSQL = strSQL.Replace("[startdate]", m_dtBeginDate.ToString());
                strSQL = strSQL.Replace("[enddate]", m_dtEndDate.ToString());
                strSQL = strSQL.Replace("[CURBEDID_CHR]", m_strCURBEDID_CHR);
                strSQL = strSQL.Replace("[EXECUTETYPE_INT]", m_strEXECUTETYPE_INT);
                strSQL = strSQL.Replace("[ISRECRUIT_INT]", m_strISRECRUIT_INT);
                //NEEDCONFIRM_INT   0未确认，1已确认，2已作废，3全部
                strSQL = strSQL.Replace("[NEEDCONFIRM_INT]", m_strNEEDCONFIRM_INT);
            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbRecord);
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

        #region 根据病区ID查询病区总执行单
        /// <summary>
        /// 根据病区ID查询病区总执行单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID">病区ID</param>
        /// <param name="p_dtbRecord">查询结果数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBihExec_Bill(string p_strAreaID, string p_strBedIDarr, DateTime m_dtBeginDate, DateTime m_dtEndDate, int m_intEXECUTETYPE, int ISRECRUIT_INT, out DataTable p_dtbRecord)
        {
            p_dtbRecord = new DataTable();
            string m_strCURBEDID_CHR = "";
            string m_strEXECUTETYPE_INT = "";
            string m_strISRECRUIT_INT = "";

            long lngRes = 0;
            string strSQL = @"select     
                                     t5.orderid_chr, 
                                     t2.deptname_vchr,   
                                     t1.code_chr,
                                     t3.inpatientid_chr,  
                                     t4.lastname_vchr,
                                     t4.sex_chr,
                                     t5.spec_vchr, 
                                     t5.executetype_int,   
                                     t5.recipeno_int,
                                     t5.recipeno2_int,    
                                     t5.registerid_chr,   
                                     t5.name_vchr,   
                                     t5.dosage_dec,   
                                     t5.dosageunit_chr,   
                                     t5.execfreqname_chr,   
                                     t5.dosetypename_chr,   
                                     t6.executedate_vchr,   
                                     t6.creator_chr,   
                                     t6.isrecruit_int,
                                     t6.createdate_dat,
                                     trunc(t6.createdate_dat) as create_dat2
                                     ,t5.get_dec
                                     ,t6.executedays_int
                                     ,t6.isfirst_int
                                     ,t5.outgetmeddays_int
                                     ,t5.getunit_chr,
                                     t5.remark_vchr,
                                     t8.ordercateid_chr,
                                     t8.name_chr
                                from t_bse_bed t1,   
                                     t_bse_deptdesc t2, 
                                     t_opr_bih_register t3,  
                                     t_opr_bih_registerdetail t4,   
                                     t_opr_bih_order t5,   
                                     t_opr_bih_orderexecute t6,
                                     t_bse_bih_orderdic t7,
                                     t_aid_bih_ordercate t8
                               where   
                                     ( t6.orderid_chr = t5.orderid_chr ) and  
                                     ( t3.registerid_chr = t4.registerid_chr) and                                    
                                     ( t5.registerid_chr = t4.registerid_chr ) and 
                                    --( t6.exebedid_chr = t1.bedid_chr ) and
                                     (t3.bedid_chr =t1.bedid_chr) and
                                     ( t6.exebedid_chr = t1.bedid_chr ) and  
                                     ( t5.orderdicid_chr = t7.orderdicid_chr ) and
                                     ( t8.ordercateid_chr = t7.ordercateid_chr) and 
                                     --( t6.exeareaid_chr = t2.deptid_chr ) and
                                     ( t6.createdate_dat between to_date('[startdate]','yyyy-mm-dd hh24:mi:ss')
                             and   to_date('[enddate]','yyyy-mm-dd hh24:mi:ss') ) 
                                     [curbedid_chr]
                                     [executetype_int]
                                     [isrecruit_int]  
                                     and  
                                     --( t6.exeareaid_chr = '[curareaid_chr]')
                                     --(t3.areaid_chr = '[curareaid_chr]')
((t3.areaid_chr = '[curareaid_chr]' and t5.sourcetype_int = 0 and t3.areaid_chr = t2.deptid_chr) 
or (t5.createareaid_chr = '[curareaid_chr]' and t5.sourcetype_int = 1 and t5.createareaid_chr = t2.deptid_chr))";

            //                            order by t2.code_vchr asc,   
            //                                     t1.code_chr asc,   
            //                                     t5.patientid_chr asc,   
            //                                     t5.recipeno_int asc,   
            //                                     t5.orderid_chr asc,
            //                                     t6.isrecruit_int asc";


            if (!p_strBedIDarr.Trim().Equals(""))
            {
                m_strCURBEDID_CHR = " and t4.registerid_chr in (" + p_strBedIDarr.Trim() + ")";

            }
            if (m_intEXECUTETYPE != 0)
            {
                m_strEXECUTETYPE_INT = "  and (( t5.executetype_int=" + m_intEXECUTETYPE.ToString() + " and t6.isrecruit_int=0)) and not(t5.status_int=3) ";
            }
            if (m_intEXECUTETYPE == 4)
            {
                m_strEXECUTETYPE_INT = " and t5.executetype_int=1";
            }
            if (m_intEXECUTETYPE == 5)
            {
                m_strEXECUTETYPE_INT = "  and (t5.executetype_int=2 or t5.executetype_int=1) and not(t5.status_int=3)";
            }
            if (m_intEXECUTETYPE == 6)
            {
                m_strEXECUTETYPE_INT = " and (t5.executetype_int=1 or t5.executetype_int=2)";
            }
            if (ISRECRUIT_INT == 1)
            {
                //m_strISRECRUIT_INT = " OR ( t5.EXECUTETYPE_INT=1 AND t6.ISRECRUIT_INT=" + ISRECRUIT_INT.ToString() + ")";
                m_strEXECUTETYPE_INT += " or ( t5.executetype_int=1 and t6.isrecruit_int=" + ISRECRUIT_INT.ToString() + ")";
            }
            //if (m_intEXECUTETYPE != 0)
            //{
            //    m_strEXECUTETYPE_INT += ")";
            //}

            if (p_strAreaID != "")
            {
                strSQL = strSQL.Replace("[curareaid_chr]", p_strAreaID);
                strSQL = strSQL.Replace("[startdate]", m_dtBeginDate.ToString());
                strSQL = strSQL.Replace("[enddate]", m_dtEndDate.ToString());
                strSQL = strSQL.Replace("[curbedid_chr]", m_strCURBEDID_CHR);
                strSQL = strSQL.Replace("[executetype_int]", m_strEXECUTETYPE_INT);
                strSQL = strSQL.Replace("[isrecruit_int]", m_strISRECRUIT_INT);

            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbRecord);
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

        #region 根据病区ID查询病区停止医嘱
        /// <summary>
        /// 根据病区ID查询病区停止医嘱
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID">病区ID</param>
        /// <param name="p_dtbRecord">查询结果数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long GetStopOrderList(string p_strAreaID, string p_strBedIDarr, DateTime m_dtBeginDate, DateTime m_dtEndDate, int m_intEXECUTETYPE, int ISRECRUIT_INT, out DataTable p_dtbRecord)
        {
            p_dtbRecord = new DataTable();
            string m_strCURBEDID_CHR = "";
            string m_strEXECUTETYPE_INT = "";
            string m_strISRECRUIT_INT = "";

            long lngRes = 0;
            string strSQL = @"SELECT     
                                     d.ORDERID_CHR, 
                                     b.DEPTNAME_VCHR,   
                                     a.CODE_CHR,   
                                     c.LASTNAME_VCHR,
                                     c.SEX_CHR,
                                     e.INPATIENTID_CHR, 
                                     d.SPEC_VCHR, 
                                     d.EXECUTETYPE_INT,   
                                     d.RECIPENO_INT, 
                                     d.RECIPENO2_INT,     
                                     d.REGISTERID_CHR,   
                                     d.NAME_VCHR,   
                                     d.DOSAGE_DEC,   
                                     d.DOSAGEUNIT_CHR,   
                                     d.EXECFREQNAME_CHR,   
                                     d.DOSETYPENAME_CHR, 
                                     d.OUTGETMEDDAYS_INT,
                                     d.GETUNIT_CHR,
                                     d.GET_DEC,
                                     d.STOPER_CHR,
                                     d.ASSESSORFORSTOP_CHR,
                                     d.FINISHDATE_DAT,
                                     d.IFPARENTID_INT,
                                     d.TYPE_INT,
                                     g.ORDERCATEID_CHR,
                                     g.NAME_CHR
                                FROM T_BSE_BED a,   
                                     T_BSE_DEPTDESC b,   
                                     t_opr_bih_registerdetail c,   
                                     T_OPR_BIH_ORDER d,   
                                     T_BSE_BIH_ORDERDIC f,
                                     T_AID_BIH_ORDERCATE g,
                                     t_opr_bih_register e
                               WHERE   
                                      c.registerid_chr = e.registerid_chr and
                                     ( d.registerid_chr = c.registerid_chr ) and 
                                     ( e.BEDID_CHR = a.BEDID_CHR ) and  
                                      -- ( e.AREAID_CHR = b.DEPTID_CHR ) and
                                      -- e.AREAID_CHR = d.CURAREAID_CHR and
                                       d.ORDERDICID_CHR = f.ORDERDICID_CHR(+) and
                                       f.ORDERCATEID_CHR = g.ORDERCATEID_CHR(+) and
                                       d.EXECUTETYPE_INT = 1 and
                                     ( d.FINISHDATE_DAT between to_date('[startdate]','YYYY-mm-dd hh24:mi:ss')
                             AND   to_date('[enddate]','YYYY-mm-dd hh24:mi:ss') ) 
                                     [CURBEDID_CHR]
                                     [EXECUTETYPE_INT]
                                     [ISRECRUIT_INT]  
                                     AND  
                                    -- ( d.CURAREAID_CHR = '[CURAREAID_CHR]')
                            ((d.CURAREAID_CHR = [CURAREAID_CHR] and d.SOURCETYPE_INT = 0 and d.CURAREAID_CHR = b.DEPTID_CHR) or (d.CREATEAREAID_CHR = [CURAREAID_CHR] and d.SOURCETYPE_INT = 1 and d.CREATEAREAID_CHR = b.DEPTID_CHR))         
                            ORDER BY a.BED_NO,   
                                     a.CODE_CHR,     
                                     d.RECIPENO_INT ";


            if (!p_strBedIDarr.Trim().Equals(""))
            {
                m_strCURBEDID_CHR = " and e.registerid_chr in (" + p_strBedIDarr.Trim() + ")";

            }
            if (m_intEXECUTETYPE != 0)
            {
                m_strEXECUTETYPE_INT = "  and (( d.EXECUTETYPE_INT=" + m_intEXECUTETYPE.ToString() + " AND e.ISRECRUIT_INT=0) ";
            }
            if (ISRECRUIT_INT == 1)
            {
                //m_strISRECRUIT_INT = " OR ( T_OPR_BIH_ORDER.EXECUTETYPE_INT=1 AND T_OPR_BIH_ORDEREXECUTE.ISRECRUIT_INT=" + ISRECRUIT_INT.ToString() + ")";
                m_strEXECUTETYPE_INT += " OR ( d.EXECUTETYPE_INT=1 AND e.ISRECRUIT_INT=" + ISRECRUIT_INT.ToString() + ")";
            }
            if (m_intEXECUTETYPE != 0)
            {
                m_strEXECUTETYPE_INT += ")";
            }

            if (p_strAreaID != "")
            {
                strSQL = strSQL.Replace("[CURAREAID_CHR]", p_strAreaID);
                strSQL = strSQL.Replace("[startdate]", m_dtBeginDate.ToString());
                strSQL = strSQL.Replace("[enddate]", m_dtEndDate.ToString());
                strSQL = strSQL.Replace("[CURBEDID_CHR]", m_strCURBEDID_CHR);
                strSQL = strSQL.Replace("[EXECUTETYPE_INT]", m_strEXECUTETYPE_INT);
                strSQL = strSQL.Replace("[ISRECRUIT_INT]", m_strISRECRUIT_INT);

            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbRecord);
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

        #region 根据病区ID查询病区新开医嘱
        /// <summary>
        /// 根据病区ID查询病区新开医嘱
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID">病区ID</param>
        /// <param name="p_dtbRecord">查询结果数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long GetNewOrderList(string p_strAreaID, string p_strBedIDarr, DateTime m_dtBeginDate, DateTime m_dtEndDate, int m_intEXECUTETYPE, int ISRECRUIT_INT, out DataTable p_dtbRecord)
        {
            p_dtbRecord = new DataTable();
            string m_strCURBEDID_CHR = "";
            string m_strEXECUTETYPE_INT = "";
            string m_strISRECRUIT_INT = "";

            long lngRes = 0;
            string strSQL = @"SELECT     
                                     d.ORDERID_CHR, 
                                     b.DEPTNAME_VCHR,   
                                     a.CODE_CHR,   
                                     c.LASTNAME_VCHR, 
                                     c.SEX_CHR,
                                     e.INPATIENTID_CHR,
                                     d.SPEC_VCHR, 
                                     d.EXECUTETYPE_INT,   
                                     d.RECIPENO_INT, 
                                     d.RECIPENO2_INT,     
                                     d.REGISTERID_CHR,   
                                     d.NAME_VCHR,   
                                     d.DOSAGE_DEC,   
                                     d.DOSAGEUNIT_CHR,   
                                     d.EXECFREQNAME_CHR,   
                                     d.DOSETYPENAME_CHR, 
                                     d.OUTGETMEDDAYS_INT,
                                     d.GETUNIT_CHR,
                                     d.GET_DEC,
                                     d.STOPER_CHR,
                                     d.ASSESSORFORSTOP_CHR,
                                     d.FINISHDATE_DAT,
                                     d.IFPARENTID_INT,
                                     d.CREATEDATE_DAT,
                                     d.CREATORID_CHR,
                                     d.CREATOR_CHR,
                                     d.CONFIRMERID_CHR,
                                     d.CONFIRMER_VCHR,
                                     d.STATUS_INT,
                                     g.ORDERCATEID_CHR,
                                     g.NAME_CHR
                                FROM T_BSE_BED a,   
                                     T_BSE_DEPTDESC b,   
                                     t_opr_bih_registerdetail c,   
                                     T_OPR_BIH_ORDER d,   
                                     T_BSE_BIH_ORDERDIC f,
                                     T_AID_BIH_ORDERCATE g,
                                     t_opr_bih_register e
                               WHERE   
                                      c.registerid_chr = e.registerid_chr and
                                     ( d.registerid_chr = c.registerid_chr ) and 
                                     ( e.BEDID_CHR = a.BEDID_CHR ) and  
                                   --  ( e.AREAID_CHR = b.DEPTID_CHR ) and
                                   --  e.AREAID_CHR = d.CURAREAID_CHR and
                                       d.ORDERDICID_CHR = f.ORDERDICID_CHR and
                                       f.ORDERCATEID_CHR = g.ORDERCATEID_CHR and
                                     d.STATUS_INT <> -2 and
                                     ( d.CREATEDATE_DAT between to_date(?, 'yyyy-mm-dd hh24:mi:ss') AND to_date(?, 'yyyy-mm-dd hh24:mi:ss') )
                                     [CURBEDID_CHR]
                                     [EXECUTETYPE_INT]
                                     [ISRECRUIT_INT]  
                                     AND  
                                    -- ( d.CURAREAID_CHR = ?)
                            ((d.CURAREAID_CHR = ? and d.SOURCETYPE_INT = 0 and d.CURAREAID_CHR = b.DEPTID_CHR) or (d.CREATEAREAID_CHR = ? and d.SOURCETYPE_INT = 1 and d.CREATEAREAID_CHR = b.DEPTID_CHR))         
                            ORDER BY a.BED_NO,   
                                     a.CODE_CHR,     
                                     d.RECIPENO_INT ";


            if (!p_strBedIDarr.Trim().Equals(""))
            {
                m_strCURBEDID_CHR = " and e.registerid_chr in (" + p_strBedIDarr.Trim() + ")";

            }
            if (m_intEXECUTETYPE != 0)
            {
                m_strEXECUTETYPE_INT = "  and d.EXECUTETYPE_INT=" + m_intEXECUTETYPE.ToString();
            }

            if (p_strAreaID != "")
            {
                //strSQL = strSQL.Replace("[CURAREAID_CHR]", p_strAreaID);
                //strSQL = strSQL.Replace("[startdate]", m_dtBeginDate.ToString());
                //strSQL = strSQL.Replace("[enddate]", m_dtEndDate.ToString());
                strSQL = strSQL.Replace("[CURBEDID_CHR]", m_strCURBEDID_CHR);
                strSQL = strSQL.Replace("[EXECUTETYPE_INT]", m_strEXECUTETYPE_INT);
                strSQL = strSQL.Replace("[ISRECRUIT_INT]", m_strISRECRUIT_INT);

            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;
                objHRPSvc.CreateDatabaseParameter(4, out arrParams);
                arrParams[0].Value = m_dtBeginDate.ToString("yyyy-MM-dd HH:mm:ss");
                arrParams[1].Value = m_dtEndDate.ToString("yyyy-MM-dd HH:mm:ss");
                arrParams[2].Value = p_strAreaID;
                arrParams[3].Value = p_strAreaID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtbRecord, arrParams);

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

        #region 更新医嘱执行单打印时间
        /// <summary>
        /// 更新医嘱执行单打印时间
        /// </summary>
        /// <param name="p_dvRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long UpdateBihOrderExecPrintDate(DataTable p_dtRecord)
        {
            long lngRes = 0;
            DateTime CreateDate = DateTime.MinValue;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = "";

            try
            {
                int n = 0;
                DataView p_dvRecord = new DataView(p_dtRecord);

                strSQL = @"INSERT INTO t_opr_bih_oeprint(SEQ_INT, AREAID_CHR, ORDERID_VCHR, ORDEREXECID_CHR, PRINT_DATE) 
                                                      VALUES (seq_bih_oeprint.nextval, ?, ?, ?, sysdate)";


                DbType[] dbTypes = new DbType[] {
                    DbType.String, DbType.String,DbType.String
                    };
                object[][] objValues = new object[3][];



                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[p_dvRecord.Count];//初始化
                }

                for (int k1 = 0; k1 < p_dvRecord.Count; k1++)
                {

                    n = -1;

                    objValues[++n][k1] = p_dvRecord[k1]["DEPTID_CHR"].ToString();
                    objValues[++n][k1] = p_dvRecord[k1]["ORDERID_VCHR"].ToString();
                    objValues[++n][k1] = p_dvRecord[k1]["ORDEREXECID_CHR"].ToString();


                }

                if (p_dvRecord.Count > 0)
                {
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
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

        #region  获取医嘱病床Vo信息
        /// <summary>
        /// 获取医嘱病床Vo信息
        /// </summary>
        /// <param name="strAreaID">病区ID</param>
        /// <param name="strInputCode">查询字符串</param>
        /// <param name="arrBed"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBihBedByArea(string strAreaID, string strInputCode, out clsBIHBed[] arrBed)
        {
            long lngRes = 0;
            arrBed = new clsBIHBed[0];
            string strSql = @" SELECT  c.INPATIENTID_CHR,
                                       c.AREAID_CHR,
                                       c.BEDID_CHR,
                                       c.registerid_chr,
                                       a.code_chr, 
                                       e.deptname_vchr, 
                                       b.lastname_vchr, 
                                       b.sex_chr
                                FROM t_bse_bed a, 
                                    t_opr_bih_registerdetail b, 
                                    t_opr_bih_register c,
                                    t_bse_deptdesc e
                                WHERE 
                                    c.AREAID_CHR = e.DEPTID_CHR(+) AND
                                    c.BEDID_CHR = a.bedid_chr(+) AND
                                    c.registerid_chr = b.registerid_chr and
                                    TRIM (LOWER (a.code_chr)) LIKE ? AND
                                    c.PSTATUS_INT <> 3 and
                                    (a.status_int = 2 or a.status_int = 6) and
                                    c.AREAID_CHR = ? 
                                    ORDER BY a.code_chr";

            string InputCode = strInputCode.ToLower().Trim() + "%";
            System.Data.IDataParameter[] arrParams = null;
            new clsHRPTableService().CreateDatabaseParameter(2, out arrParams);
            arrParams[0].Value = InputCode;
            arrParams[1].Value = strAreaID.Trim();
            DataTable objDT = new DataTable();
            long ret = 0;
            try
            {
                ret = 0;
                // ret = new clsHRPTableService().DoGetDataTable(strSql, ref objDT);
                ret = new clsHRPTableService().lngGetDataTableWithParameters(strSql, ref objDT, arrParams);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            /*<--------------------------------------------------*/

            if ((ret > 0) && (objDT != null))
            {
                arrBed = new clsBIHBed[objDT.Rows.Count];
                for (int i = 0; i < arrBed.Length; i++)
                {
                    arrBed[i] = new clsBIHBed();
                    arrBed[i].m_strAreaID = Convert.ToString(objDT.Rows[i]["AREAID_CHR"]).Trim();

                    arrBed[i].m_strBedID = Convert.ToString(objDT.Rows[i]["BEDID_CHR"]).Trim();
                    arrBed[i].m_strBedName = Convert.ToString(objDT.Rows[i]["Code_Chr"]).Trim();
                    clsBIHPatientInfo patient = new clsBIHPatientInfo();
                    patient.m_strRegisterID = Convert.ToString(objDT.Rows[i]["registerid_chr"].ToString().Trim());
                    patient.m_strPatientName = Convert.ToString(objDT.Rows[i]["lastname_vchr"].ToString()).Trim();
                    patient.m_strSex = Convert.ToString(objDT.Rows[i]["sex_chr"].ToString()).Trim();
                    patient.m_strAreaID = Convert.ToString(objDT.Rows[i]["AREAID_CHR"].ToString()).Trim();
                    patient.m_strAreaName = Convert.ToString(objDT.Rows[i]["deptname_vchr"].ToString()).Trim();
                    patient.m_strInHospitalNo = Convert.ToString(objDT.Rows[i]["INPATIENTID_CHR"].ToString().Trim());
                    arrBed[i].m_objPatient = patient;
                }
                return 1;
            }
            else
            {
                arrBed = null;
                return 0;
            }

        }
        #endregion

        #region 根据病区ID查询出院病人列表
        /// <summary>
        /// 根据病区ID查询出院病人列表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID">病区ID</param>
        /// <param name="m_dtBeginDate">开始时间</param>
        /// <param name="m_dtEndDate">结束时间</param>
        /// <param name="p_dtbRecord">查询结果数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetReport_d_bih_outHospital(string p_strAreaID, DateTime m_dtBeginDate, DateTime m_dtEndDate, out DataTable p_dtbRecord)
        {
            p_dtbRecord = new DataTable();
            string m_strCURBEDID_CHR = "";

            long lngRes = 0;
            string strSQL = @" select distinct  a.creator_chr, a.name_vchr, a.createdate_dat, b.patientid_chr,
                                                c.lastname_vchr, c.registerid_chr, c.sex_chr, d.bedid_chr,
                                                d.code_chr, e.ordercateid_chr, g.deptname_vchr curareaname,
                                                b.inpatientid_chr
                                           from t_opr_bih_order a,
                                                t_opr_bih_register b,
                                                t_opr_bih_registerdetail c,
                                                t_bse_bed d,
                                                t_bse_bih_orderdic e,
                                                t_bse_deptdesc g
                                          where a.registerid_chr = b.registerid_chr
                                            and b.registerid_chr = c.registerid_chr
                                            and b.bedid_chr = d.bedid_chr(+)
                                            and a.orderdicid_chr = e.orderdicid_chr(+)
                                            and a.curareaid_chr = g.deptid_chr(+)
                                            and a.status_int <> -2
                                            --and b.pstatus_int!=3
                                            and (a.createdate_dat >= ? and a.createdate_dat <= ?)
                                            and (a.type_int = 3 or a.type_int = 4)
                                            and a.createareaid_chr = ?
                                       order by d.code_chr asc ";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                clsHRPTableService HRPService = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;
                HRPService.CreateDatabaseParameter(3, out arrParams);
                arrParams[0].Value = m_dtBeginDate;
                arrParams[1].Value = m_dtEndDate;
                arrParams[2].Value = p_strAreaID.Trim();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtbRecord, arrParams);
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

        #region 根据住院登记号取病人登记修改记录
        /// <summary>
        /// 根据住院登记号取病人登记修改记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterid">住院登记号</param>
        /// <param name="p_dtbRecord">查询结果数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long GetRegisterLogByRegId(string p_strRegisterid, out DataTable p_dtbRecord)
        {
            p_dtbRecord = new DataTable();

            long lngRes = 0;
            string strSQL = @"SELECT     
                                    r.registerid_chr, 
                                    r.modify_dat, 
                                    r.patientid_chr, 
                                    r.isbooking_int, 
                                    r.inpatientid_chr, 
                                    r.INPATIENT_DAT, 
                                    r.DEPTID_CHR, 
                                    r.AREAID_CHR, 
                                    r.BEDID_CHR, 
                                    r.TYPE_INT, 
                                    r.DIAGNOSE_VCHR, 
                                    r.LIMITRATE_MNY, 
                                    r.INPATIENTCOUNT_INT, 
                                    r.STATE_INT, 
                                    r.STATUS_INT, 
                                    r.OPERATORID_CHR, 
                                    r.PSTATUS_INT, 
                                    r.CASEDOCTOR_CHR, 
                                    r.INPATIENTNOTYPE_INT, 
                                    r.DES_VCHR, 
                                    r.INAREADATE_DAT, 
                                    r.MZDOCTOR_CHR, 
                                    r.MZDIAGNOSE_VCHR, 
                                    r.DIAGNOSEID_CHR, 
                                    r.ICD10DIAGID_VCHR, 
                                    r.ICD10DIAGTEXT_VCHR, 
                                    r.ISFROMCLINIC, 
                                    r.CLINICSAYPREPAY, 
                                    r.PAYTYPEID_CHR, 
                                    r.BORNNUM_INT, 
                                    r.RELATEREGISTERID_CHR, 
                                    r.FEESTATUS_INT, 
                                    r.EXTENDID_VCHR, 
                                    r.NURSING_CLASS, 
                                    r.CASEDOCTORDEPT_CHR, 
                                    r.CANCELERID_CHR, 
                                    r.CANCEL_DAT, 
                                    r.SEQ_INT, 
                                    r.ACTION_TYPE,
                                    r.operation_date,
                                    p.PAYTYPENAME_VCHR
                                FROM T_BSE_PATIENTPAYTYPE p,
                                     t_opr_bih_registerlog r
                               WHERE r.PAYTYPEID_CHR = p.PAYTYPEID_CHR(+) and  
                                     r.registerid_chr = ?
                                     
                            ORDER BY r.operation_date ";



            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;
                objHRPSvc.CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = p_strRegisterid;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtbRecord, arrParams);

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

        #region 根据住院登记号取病人登记明细表修改记录
        /// <summary>
        /// 根据住院登记号取病人登记明细表修改记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterid">住院登记号</param>
        /// <param name="p_dtbRecord">查询结果数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long GetRegisterDetailLogByRegId(string p_strRegisterid, out DataTable p_dtbRecord)
        {
            p_dtbRecord = new DataTable();

            long lngRes = 0;
            string strSQL = @"SELECT     
                                    d.REGISTERID_CHR, 
                                    d.LASTNAME_VCHR, 
                                    d.IDCARD_CHR, 
                                    d.MARRIED_CHR, 
                                    d.BIRTHPLACE_VCHR, 
                                    d.HOMEADDRESS_VCHR, 
                                    d.SEX_CHR, 
                                    d.NATIONALITY_VCHR, 
                                    d.FIRSTNAME_VCHR, 
                                    d.BIRTH_DAT, 
                                    d.RACE_VCHR, 
                                    d.NATIVEPLACE_VCHR, 
                                    d.OCCUPATION_VCHR, 
                                    d.NAME_VCHR, 
                                    d.HOMEPHONE_VCHR, 
                                    d.OFFICEPHONE_VCHR, 
                                    d.INSURANCEID_VCHR, 
                                    d.MOBILE_CHR, 
                                    d.OFFICEADDRESS_VCHR, 
                                    d.EMPLOYER_VCHR, 
                                    d.OFFICEPC_VCHR, 
                                    d.HOMEPC_CHR, 
                                    d.EMAIL_VCHR, 
                                    d.CONTACTPERSONFIRSTNAME_VCHR, 
                                    d.CONTACTPERSONLASTNAME_VCHR, 
                                    d.CONTACTPERSONADDRESS_VCHR, 
                                    d.CONTACTPERSONPHONE_VCHR, 
                                    d.CONTACTPERSONPC_CHR, 
                                    d.PATIENTRELATION_VCHR, 
                                    d.FIRSTDATE_DAT, 
                                    d.ISEMPLOYEE_INT, 
                                    d.STATUS_INT, 
                                    d.DEACTIVATE_DAT, 
                                    d.OPERATORID_CHR, 
                                    d.MODIFY_DAT, 
                                    d.OPTIMES_INT, 
                                    d.GOVCARD_CHR, 
                                    d.BLOODTYPE_CHR, 
                                    d.IFALLERGIC_INT, 
                                    d.ALLERGICDESC_VCHR, 
                                    d.DIFFICULTY_VCHR, 
                                    d.INSUREDTOTALMONEY_MNY, 
                                    d.INSUREDPAYMONEY_MNY , 
                                    d.INSUREDPAYTIME_INT, 
                                    d.INSUREDPAYSCALE_DEC, 
                                    d.seq_int, 
                                    d.action_type,
                                    d.operation_date
                                FROM 
                                     t_opr_bih_registerdetailLog d
                               WHERE   
                                     d.registerid_chr = ?
                                     
                            ORDER BY d.operation_date ";



            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;
                objHRPSvc.CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = p_strRegisterid;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtbRecord, arrParams);

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

        #region 获取医嘱
        /// <summary>
        /// 获取医嘱	根据入院登记流水号
        /// </summary>
        /// <param name="strRegisterId">入院登记流水号</param>
        /// <param name="arrOrder">医嘱Vo对象[out 参数]</param>
        /// <returns></returns>
        [AutoComplete]
        public long GetOrderByRegisterId(string strRegisterId, out clsBIHOrder[] arrOrder)
        {
            long lngRes = -1;
            arrOrder = null;
            string Sql = @"select sysdate,
                                   trunc(sysdate) as today,
                                   trunc(a.createdate_dat) as creatday,
                                   c.sample_type_desc_vchr,
                                   d.partname,
                                   e.ordercateid_chr,
                                   e.itemid_chr as chargeitemid_chr,
                                   e.lisapplyunitid_chr,
                                   e.applytypeid_chr,
                                   f.ipchargeflg_int,
                                   f.packqty_dec,
                                   g.mednormalname_vchr,
                                   a.*,
                                   (select t.sign_grp
                                      from t_bse_empsign t
                                     where t.empid_chr = a.creatorid_chr) as creatorsign,
                                   (select t.sign_grp
                                      from t_bse_empsign t
                                     where t.empid_chr = a.confirmerid_chr) as confirmersign,
                                   (select t.sign_grp
                                      from t_bse_empsign t
                                     where t.empid_chr = a.stoperid_chr) as stopersign,
                                   (select t.sign_grp
                                      from t_bse_empsign t
                                     where t.empid_chr = a.assessoridforstop_chr) as assessorsign,
                                   p.itemchargetype_vchr
                              from t_opr_bih_order a
                              left join t_aid_lis_sampletype c
                                on a.sampleid_vchr = c.sample_type_id_chr
                              left join ar_apply_partlist d
                                on a.partid_vchr = d.partid
                              left join t_bse_bih_orderdic e
                                on a.orderdicid_chr = e.orderdicid_chr
                              left join t_bse_chargeitem f
                                on e.itemid_chr = f.itemid_chr
                              left join t_bse_medicine g
                                on f.ITEMSRCID_VCHR = g.medicineid_chr
                              left join t_opr_bih_orderchargedept p
                                on a.orderid_chr = p.orderid_chr
                               and e.orderdicid_chr = p.orderdicid_chr
                               and e.itemid_chr = p.chargeitemid_chr
                             where a.STATUS_INT <> -2
                               and a.registerid_chr = ?";

            try
            {
                clsHRPTableService HRPService = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;
                HRPService.CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = strRegisterId;
                DataTable objDT = new DataTable();
                lngRes = 0;
                lngRes = HRPService.lngGetDataTableWithParameters(Sql, ref objDT, arrParams);
                if (objDT.Rows.Count > 0)
                {
                    DataView myDataView = new DataView(objDT);
                    myDataView.Sort = "recipeno_int, ORDERID_CHR";
                    if (myDataView.Count <= 0)
                    {
                        return lngRes;
                    }
                    DataTable m_dtOrder = myDataView.ToTable();
                    if (m_dtOrder.Rows.Count > 0)
                    {
                        clsBIHOrderService orderSvc = new clsBIHOrderService();
                        orderSvc.m_lngGetOrderArrFromDataTableNew2(m_dtOrder, out arrOrder);
                    }
                }
                HRPService.Dispose();
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

        #region 通过DATATABLE转换成医嘱对象
        /// <summary>
        /// 获取医嘱信息	根据DataTable
        /// </summary>
        /// <param name="objDT">DataTable</param>
        /// <param name="arrOrder"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngGetOrderArrFromDataTableNew(DataTable objDT, out clsBIHOrder[] arrOrder)
        {
            arrOrder = null;
            if (objDT == null) return 0;

            arrOrder = new clsBIHOrder[objDT.Rows.Count];
            for (int i = 0; i < arrOrder.Length; i++)
            {
                arrOrder[i] = new clsBIHOrder();
                DataRow objRow = objDT.Rows[i];
                arrOrder[i].m_strOrderID = clsConverter.ToString(objRow["Orderid_Chr"]).Trim();
                arrOrder[i].m_strOrderDicID = clsConverter.ToString(objRow["Orderdicid_Chr"]).Trim();
                arrOrder[i].m_strRegisterID = clsConverter.ToString(objRow["Registerid_Chr"]).Trim();
                arrOrder[i].m_strPatientID = clsConverter.ToString(objRow["Patientid_Chr"]).Trim();
                arrOrder[i].m_intExecuteType = clsConverter.ToInt(objRow["Executetype_Int"]);
                arrOrder[i].m_intRecipenNo = clsConverter.ToInt(objRow["Recipeno_Int"]);
                arrOrder[i].m_intRecipenNo2 = clsConverter.ToInt(objRow["Recipeno2_Int"]);

                arrOrder[i].m_strName = clsConverter.ToString(objRow["Name_Vchr"]).Trim();
                arrOrder[i].m_strSpec = clsConverter.ToString(objRow["Spec_Vchr"]).Trim();
                arrOrder[i].m_strExecFreqID = clsConverter.ToString(objRow["Execfreqid_Chr"]).Trim();
                arrOrder[i].m_strExecFreqName = clsConverter.ToString(objRow["Execfreqname_Chr"]).Trim();
                arrOrder[i].m_dmlDosage = clsConverter.ToDecimal(objRow["Dosage_Dec"]);
                arrOrder[i].m_strDosageUnit = clsConverter.ToString(objRow["Dosageunit_Chr"]).Trim();

                arrOrder[i].m_dmlUse = clsConverter.ToDecimal(objRow["Use_Dec"]);
                arrOrder[i].m_strUseunit = clsConverter.ToString(objRow["Useunit_Chr"]).Trim();
                arrOrder[i].m_dmlGet = clsConverter.ToDecimal(objRow["Get_Dec"]);
                arrOrder[i].m_strGetunit = clsConverter.ToString(objRow["Getunit_Chr"]).Trim();
                arrOrder[i].m_strDosetypeID = clsConverter.ToString(objRow["Dosetypeid_Chr"]).Trim();
                arrOrder[i].m_strDosetypeName = clsConverter.ToString(objRow["Dosetypename_Chr"]).Trim();

                if (!objRow["Startdate_Dat"].ToString().Equals(""))
                {
                    arrOrder[i].m_dtStartDate = clsConverter.ToDateTime(objRow["Startdate_Dat"]);
                }
                else
                {
                    arrOrder[i].m_dtStartDate = DateTime.MinValue;
                }

                arrOrder[i].m_dtFinishDate = clsConverter.ToDateTime(objRow["Finishdate_Dat"]);
                arrOrder[i].m_strExecDeptID = clsConverter.ToString(objRow["Execdeptid_Chr"]).Trim();
                arrOrder[i].m_strExecDeptName = clsConverter.ToString(objRow["Execdeptname_Chr"]).Trim();
                arrOrder[i].m_strEntrust = clsConverter.ToString(objRow["Entrust_Vchr"]).Trim();
                arrOrder[i].m_strParentID = clsConverter.ToString(objRow["Parentid_Chr"]).Trim();
                if (objDT.Columns.Contains("ParentName"))
                    arrOrder[i].m_strParentName = clsConverter.ToString(objRow["ParentName"]).Trim();

                arrOrder[i].m_intStatus = clsConverter.ToInt(objRow["Status_Int"]);
                arrOrder[i].m_intIsRich = clsConverter.ToInt(objRow["Isrich_Int"]);
                arrOrder[i].RateType = clsConverter.ToInt(objRow["Ratetype_Int"]);
                arrOrder[i].m_intOUTGETMEDDAYS_INT = clsConverter.ToInt(objRow["OUTGETMEDDAYS_INT"]);
                arrOrder[i].m_intIsRepare = clsConverter.ToInt(objRow["Isrepare_Int"]);

                arrOrder[i].m_strCreatorID = clsConverter.ToString(objRow["Creatorid_Chr"]).Trim();
                arrOrder[i].m_strCreator = clsConverter.ToString(objRow["Creator_Chr"]).Trim();
                arrOrder[i].m_dtCreatedate = clsConverter.ToDateTime(objRow["Createdate_Dat"]);

                arrOrder[i].m_strPosterId = clsConverter.ToString(objRow["Posterid_Chr"]).Trim();
                arrOrder[i].m_strPoster = clsConverter.ToString(objRow["Poster_Chr"]).Trim();
                arrOrder[i].m_dtPostdate = clsConverter.ToDateTime(objRow["Postdate_Dat"]);

                arrOrder[i].m_strExecutorID = clsConverter.ToString(objRow["Executorid_Chr"]).Trim();
                arrOrder[i].m_strExecutor = clsConverter.ToString(objRow["Executor_Chr"]).Trim();
                arrOrder[i].m_dtExecutedate = clsConverter.ToDateTime(objRow["Executedate_Dat"]);

                arrOrder[i].m_strStoperID = clsConverter.ToString(objRow["Stoperid_Chr"]).Trim();
                arrOrder[i].m_strStoper = clsConverter.ToString(objRow["Stoper_Chr"]).Trim();
                if (!objRow["Stopdate_Dat"].ToString().Equals(""))
                {
                    arrOrder[i].m_dtStopdate = clsConverter.ToDateTime(objRow["Stopdate_Dat"]);
                }
                else
                {
                    arrOrder[i].m_dtStopdate = DateTime.MinValue;
                }

                arrOrder[i].m_strRetractorID = clsConverter.ToString(objRow["Retractorid_Chr"]).Trim();
                arrOrder[i].m_strRetractor = clsConverter.ToString(objRow["Retractor_Chr"]).Trim();
                arrOrder[i].m_dtRetractdate = clsConverter.ToDateTime(objRow["Retractdate_Dat"]);
                arrOrder[i].m_dmlPrice = clsConverter.ToDecimal(objRow["ItemPrice"]);
                if (objDT.Columns.Contains("DosageRate"))
                {
                    arrOrder[i].m_dmlDosageRate = clsConverter.ToDecimal(objRow["DosageRate"]);
                }
                else
                {
                    arrOrder[i].m_dmlDosageRate = arrOrder[i].m_dmlUse;
                }
                arrOrder[i].m_strOrderDicCateID = clsConverter.ToString(objRow["OrderCateID_Chr"]).Trim();
                arrOrder[i].m_intISNEEDFEEL = clsConverter.ToInt(objRow["ISNEEDFEEL"]);
                arrOrder[i].m_intFEEL_INT = clsConverter.ToInt(objRow["FEEL_INT"]);
                // 增加检验类型信息
                arrOrder[i].m_strSAMPLEID_VCHR = clsConverter.ToString(objRow["SAMPLEID_VCHR"]).Trim();
                arrOrder[i].m_strSAMPLEName_VCHR = clsConverter.ToString(objRow["SAMPLE_TYPE_DESC_VCHR"]).Trim();
                // 增加检查部位信息
                arrOrder[i].m_strPARTID_VCHR = clsConverter.ToString(objRow["PARTID_VCHR"]).Trim();
                arrOrder[i].m_strPARTNAME_VCHR = clsConverter.ToString(objRow["partname"]).Trim();
                //医嘱修改者
                arrOrder[i].m_strChangedID_CHR = clsConverter.ToString(objRow["ASSESSORIDFOREXEC_CHR"]).Trim();
                arrOrder[i].m_strChangedName_CHR = clsConverter.ToString(objRow["ASSESSORFOREXEC_CHR"]).Trim();
                if (objRow["ASSESSORFOREXEC_DAT"] != System.DBNull.Value)
                {
                    try
                    {
                        arrOrder[i].m_dtChanged_DAT = Convert.ToDateTime(objRow["ASSESSORFOREXEC_DAT"]);
                    }
                    catch { }
                }
                /*<=========================*/
                arrOrder[i].m_strASSESSORIDFOREXEC_CHR = clsConverter.ToString(objRow["CONFIRMERID_CHR"]).Trim();
                arrOrder[i].m_strASSESSORFOREXEC_CHR = clsConverter.ToString(objRow["CONFIRMER_VCHR"]).Trim();
                if (objRow["CONFIRM_DAT"] != System.DBNull.Value)
                {
                    try
                    {
                        arrOrder[i].m_strASSESSORFOREXEC_DAT = Convert.ToDateTime(objRow["CONFIRM_DAT"]).ToString("yyyy-MM-dd 24HH:mm:ss").Trim();
                    }
                    catch { }
                }
                //审核停止
                arrOrder[i].m_strASSESSORIDFORSTOP_CHR = clsConverter.ToString(objRow["ASSESSORIDFORSTOP_CHR"]).Trim();
                arrOrder[i].m_strASSESSORFORSTOP_CHR = clsConverter.ToString(objRow["ASSESSORFORSTOP_CHR"]).Trim();
                if (objRow["ASSESSORFORSTOP_DAT"] != System.DBNull.Value)
                {
                    try
                    {
                        arrOrder[i].m_strASSESSORFORSTOP_DAT = Convert.ToDateTime(objRow["ASSESSORFORSTOP_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    }
                    catch { }
                }
                //退回
                if (objRow["BACKREASON"] != System.DBNull.Value)
                {
                    arrOrder[i].m_strBACKREASON = clsConverter.ToString(objRow["BACKREASON"]).Trim();
                }
                if (objRow["SENDBACKID_CHR"] != System.DBNull.Value)
                {
                    arrOrder[i].m_strSENDBACKID_CHR = clsConverter.ToString(objRow["SENDBACKID_CHR"]).Trim();
                }
                if (objRow["SENDBACKER_CHR"] != System.DBNull.Value)
                {
                    arrOrder[i].m_strSENDBACKER_CHR = clsConverter.ToString(objRow["SENDBACKER_CHR"]).Trim();
                }
                if (objRow["SENDBACK_DAT"] != System.DBNull.Value)
                {
                    arrOrder[i].m_strSENDBACK_DAT = clsConverter.ToString(objRow["SENDBACK_DAT"]).Trim();
                }
                if (objRow["isYB_int"] != System.DBNull.Value)
                {
                    arrOrder[i].isYB_int = clsConverter.ToString(objRow["isYB_int"]).Trim();
                }
                /*  是否父医嘱*/
                arrOrder[i].m_intIFPARENTID_INT = clsConverter.ToInt(objRow["IFPARENTID_INT"].ToString());
                arrOrder[i].m_strCREATEAREA_ID = clsConverter.ToString(objRow["CREATEAREAID_CHR"].ToString());
                arrOrder[i].m_strCREATEAREA_Name = clsConverter.ToString(objRow["CREATEAREANAME_VCHR"].ToString());
                /* 医嘱类型(如检查)*/
                if (objDT.Columns.Contains("viewname_vchr"))
                    arrOrder[i].m_strOrderDicCateName = clsConverter.ToString(objRow["viewname_vchr"].ToString());
                /* 医保信息*/
                arrOrder[i].m_strMedicareTypeName = clsConverter.ToString(objRow["MedicareTypeName"].ToString());
                /* 补次次数*/
                arrOrder[i].m_intATTACHTIMES_INT = clsConverter.ToInt(objRow["ATTACHTIMES_INT"].ToString());
                arrOrder[i].m_strDOCTORID_CHR = clsConverter.ToString(objRow["DOCTORID_CHR"].ToString());
                //医生名称
                arrOrder[i].m_strDOCTOR_VCHR = clsConverter.ToString(objRow["DOCTOR_VCHR"].ToString());
                //作废人ID
                arrOrder[i].m_strDELETERID_CHR = clsConverter.ToString(objRow["DELETERID_CHR"].ToString());
                //作废人名
                arrOrder[i].m_strDELETERNAME_VCHR = clsConverter.ToString(objRow["DELETERNAME_VCHR"].ToString());
                //作废时间
                arrOrder[i].m_strDELETE_DAT = clsConverter.ToString(objRow["DELETE_DAT"].ToString());
                if (objRow["SIGN_GRP"] != System.DBNull.Value)
                {
                    Byte[] sign_grp = (byte[])objRow["SIGN_GRP"];

                    arrOrder[i].SIGN_GRP = sign_grp;
                }

                if (!objDT.Columns.Contains("SIGN_INT").ToString().Equals(""))
                {
                    arrOrder[i].SIGN_INT = clsConverter.ToInt(objRow["SIGN_INT"].ToString());
                }
                // 主收费项目ID
                arrOrder[i].m_strCHARGEITEMID_CHR = clsConverter.ToString(objRow["CHARGEITEMID_CHR"].ToString());
                // 主收费项目名称
                arrOrder[i].m_strCHARGEITEMNAME_CHR = clsConverter.ToString(objRow["CHARGEITEMNAME_CHR"].ToString());
                // 术后标志（0-术前，1-术后) 
                arrOrder[i].m_intOPERATION_INT = clsConverter.ToInt(objRow["OPERATION_INT"].ToString());
                // 医嘱说明
                arrOrder[i].m_strREMARK_VCHR = clsConverter.ToString(objRow["REMARK_VCHR"].ToString());
                //修改标志(0-普通状态,1-频率修改)
                arrOrder[i].m_intCHARGE_INT = clsConverter.ToInt(objRow["CHARGE_INT"].ToString());
                //医嘱归类(0-普通,1-术后医嘱,2-转科医嘱,3-出院(今日),4-出院(明日))
                arrOrder[i].m_intTYPE_INT = clsConverter.ToInt(objRow["TYPE_INT"].ToString());
            }
            return 1;
        }
        #endregion


        #region 根据病区ID查询材料发放列表
        /// <summary>
        /// 根据病区ID查询材料发放列表 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID">病区ID</param>
        /// <param name="p_strBeginDate">统计时间</param>
        /// <param name="p_intSerchType">统计方式</param>
        /// <param name="p_intMedType">药房类型分类</param>
        /// <param name="p_dtbRecord">查询结果数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long GetMaterialByAreaId(string p_strAreaID, string p_strBeginDate, int p_intSerchType, int p_intMedType, out DataTable p_dtbRecord)
        {
            p_dtbRecord = new DataTable();
            string strPrintFlag = "";
            string strSerchType = "";

            long lngRes = 0;
            string strSQL = @"SELECT    
                                 a.CODE_CHR, 
                                 b.DEPTID_CHR,
                                 b.DEPTNAME_VCHR,   
                                 c.LASTNAME_VCHR,   
                                 d.ORDERID_CHR,   
                                 d.EXECUTETYPE_INT,   
                                 d.RECIPENO_INT,   
                                 d.RECIPENO2_INT,   
                                 d.REGISTERID_CHR,   
                                 d.NAME_VCHR,   
                                 d.DOSAGE_DEC,   
                                 d.DOSAGEUNIT_CHR,   
                                 d.EXECFREQNAME_CHR,   
                                 d.DOSETYPENAME_CHR,
                                 d.GET_DEC,
                                 d.OUTGETMEDDAYS_INT,
                                 d.GETUNIT_CHR,
                                 d.IFPARENTID_INT,
                                 d.SPEC_VCHR,
                                 d.REMARK_VCHR, 
                                 e.ORDEREXECID_CHR,     
                                 e.EXECUTEDATE_VCHR,   
                                 e.CREATOR_CHR,   
                                 e.ISRECRUIT_INT,
                                 e.CREATEDATE_DAT,
                                 e.EXECUTEDAYS_INT,
                                 e.ISFIRST_INT,
                                 g.INPATIENTID_CHR,
                                 c.SEX_CHR
                            FROM T_BSE_BED a,   
                                 T_BSE_DEPTDESC b,   
                                 t_opr_bih_registerdetail c,   
                                 T_OPR_BIH_ORDER d,   
                                 T_OPR_BIH_ORDEREXECUTE e,  
                                 t_opr_bih_register g, 
                                 T_BSE_CHARGEITEM h,
                                 T_BSE_MEDICINE i
                           WHERE [printFlag]
                                 c.registerid_chr = g.registerid_chr
                               and (e.ORDERID_CHR = d.ORDERID_CHR)
                               and (d.registerid_chr = c.registerid_chr)
                               and (g.BEDID_CHR = a.BEDID_CHR)
                               --and (g.AREAID_CHR = b.DEPTID_CHR)
                               and d.orderdicid_chr = h.itemid_chr
                               and h.ITEMSRCID_VCHR = i.medicineid_chr
                               and (i.MEDICNETYPE_INT = ?)
                               --and (g.AREAID_CHR = '')
                               and ((g.AREAID_CHR = ? and d.SOURCETYPE_INT = 0 and
                                   g.AREAID_CHR = b.DEPTID_CHR) or
                                   (d.CREATEAREAID_CHR = ? and d.SOURCETYPE_INT = 1 and
                                   d.CREATEAREAID_CHR = b.DEPTID_CHR))
                                 [serchType]
                        ORDER BY d.CURAREAID_CHR ASC,   
                                 d.CURBEDID_CHR ASC,   
                                 d.PATIENTID_CHR ASC,   
                                 d.RECIPENO_INT ASC,  
                                 d.ORDERID_CHR ASC,
                                 e.ISRECRUIT_INT ASC,
                                 d.IFPARENTID_INT ";

            if (p_intSerchType == 0)
            {
                p_strBeginDate = DateTime.Parse(p_strBeginDate).ToShortDateString();

                strSerchType = " and e.CREATEDATE_DAT > to_date('" + p_strBeginDate + " 00:00:00" + @"', 'YYYY-mm-dd hh24:mi:ss') and
                                     e.CREATEDATE_DAT < to_date('" + p_strBeginDate + " 23:59:59" + "', 'YYYY-mm-dd hh24:mi:ss')";
            }
            else if (p_intSerchType == 1)
            {
                strPrintFlag = @"not exists ( select t2.seq_int from t_opr_bih_oeprint t2 where 
                                                   t2.orderexecid_chr = e.orderexecid_chr and
                                                   t2.areaid_chr = d.CURAREAID_CHR ) and";

                p_strBeginDate = DateTime.Today.ToShortDateString();
                strSerchType = @" and 
                                     e.CREATEDATE_DAT > to_date('" + p_strBeginDate + " 00:00:00" + @"', 'YYYY-mm-dd hh24:mi:ss') and
                                     e.CREATEDATE_DAT < to_date('" + p_strBeginDate + " 23:59:59" + "', 'YYYY-mm-dd hh24:mi:ss')";
            }
            else if (p_intSerchType == 2)
            {
                strSerchType = " and e.CREATEDATE_DAT > to_date('" + p_strBeginDate + "', 'YYYY-mm-dd hh24:mi:ss')";
            }

            if (p_strAreaID != "")
            {
                strSQL = strSQL.Replace("[printFlag]", strPrintFlag);
                strSQL = strSQL.Replace("[serchType]", strSerchType);
                //strSQL = strSQL.Replace("[CURBEDID_CHR]", m_strCURBEDID_CHR);

            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbRecord);

                System.Data.IDataParameter[] arrParams = null;
                new clsHRPTableService().CreateDatabaseParameter(3, out arrParams);
                arrParams[0].Value = p_intMedType;
                arrParams[1].Value = p_strAreaID;
                arrParams[2].Value = p_strAreaID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtbRecord, arrParams);

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

        #region 获取皮试的医嘱  
        /// <summary>
        /// 获取皮试的医嘱 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strAreaid_chr"></param>
        /// <param name="m_dtExecOrder"></param>
        /// <returns></returns>         
        [AutoComplete]
        public long m_lngGetFeelOrderByAreaID(string m_strAreaid_chr, out DataTable m_dtExecOrder)
        {
            long lngRes = -1;
            m_dtExecOrder = new DataTable();
            string strSql = @"select a.*,
                                   b.patientid_chr,
                                   c.LASTNAME_VCHR,
                                   c.registerid_chr,
                                   c.SEX_CHR,
                                   d.bedid_chr,
                                   d.code_chr,
                                   e.ordercateid_chr,
                                   g.deptname_vchr CURAREAName,
                                   h.sample_type_desc_vchr,
                                   j.partname

                              from t_opr_bih_order          a,
                                   t_opr_bih_register       b,
                                   t_opr_bih_registerdetail c,
                                   T_BSE_Bed                d,
                                   t_bse_bih_orderdic       e,
                                   T_BSE_DeptDesc           g,
                                   t_aid_lis_sampletype     h,
                                   ar_apply_partlist        j
                             where a.registerid_chr = b.registerid_chr
                               and b.registerid_chr = c.registerid_chr
                               and b.BEDID_CHR = d.bedid_chr(+)
                               and a.orderdicid_chr = e.orderdicid_chr(+)
                              -- and a.CURAREAID_CHR = g.deptid_chr(+)
                               and a.sampleid_vchr = h.sample_type_id_chr(+)
                               and a.partid_vchr = j.partid(+)
                               and b.pstatus_int != 3
                               and ((a.CURAREAID_CHR = ? and a.SOURCETYPE_INT = 0 and a.CURAREAID_CHR = g.DEPTID_CHR(+)) or (a.CREATEAREAID_CHR = ? and a.SOURCETYPE_INT = 1 and a.CREATEAREAID_CHR = g.DEPTID_CHR(+)))
                               -- and a.CREATEAREAID_CHR = 
                               and a.STATUS_INT = 1
                               and a.ISNEEDFEEL = 1
                               and a.FEEL_INT = 0
                             order by a.CURAREAID_CHR, d.code_chr, a.recipeno_int, a.orderid_chr asc";
            /*<====================================================================*/
            try
            {
                clsHRPTableService HRPService = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;
                HRPService.CreateDatabaseParameter(2, out arrParams);
                arrParams[0].Value = m_strAreaid_chr.Trim();
                arrParams[1].Value = m_strAreaid_chr.Trim();
                lngRes = 0;
                lngRes = HRPService.lngGetDataTableWithParameters(strSql, ref m_dtExecOrder, arrParams);
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

        #region 住院医生常用药品统计
        /// <summary>
        /// 住院医生常用药品统计
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strBegin"></param>
        /// <param name="p_strEnd"></param>
        /// <param name="p_strAreaId"></param>
        /// <param name="p_strMedType"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetMedByDate(string p_strBegin, string p_strEnd, string p_strAreaId, string p_strMedType, out DataTable p_dtResult)
        {
            long lngRes = 0;

            p_dtResult = new DataTable();
            string strSQL = @"select c.medicinename_vchr,
                                   c.MEDSPEC_VCHR,
                                   c.ASSISTCODE_CHR,
                                   a.unit_vchr,
                                   d.deptname_vchr,
                                   d.empno_chr,
                                   d.lastname_vchr,
                                   sum (a.AMOUNT_DEC) acount,
                                   sum(a.TOTALMONEY_DEC) totalmoney
                              from T_Opr_Bih_PatientCharge a, 
                                  t_bse_chargeitem b, 
                                  T_BSE_MEDICINE c,
                                  (select e1.empid_chr,
                                           e1.empno_chr,
                                           e1.lastname_vchr,
                                           r1.deptid_chr,
                                           d1.code_vchr,
                                           d1.deptname_vchr
                                      from t_bse_employee e1, T_BSE_DEPTEMP r1, T_bse_DeptDesc d1
                                     where r1.deptid_chr = d1.deptid_chr
                                       and e1.empid_chr = r1.empid_chr
                                       and r1.DEFAULT_INPATIENT_DEPT_INT = 1
                                    union all
                                    select e2.empid_chr,
                                           e2.empno_chr,
                                           e2.lastname_vchr,
                                           '' deptid_chr,
                                           '' code_vchr,
                                           '' deptname_vchr
                                      from t_bse_employee e2
                                     where not exists (select ''
                                              from T_BSE_DEPTEMP r2
                                             where r2.empid_chr = e2.empid_chr
                                               and r2.DEFAULT_INPATIENT_DEPT_INT = 1)) d
                             where a.chargeitemid_chr = b.itemid_chr
                               and b.itemsrcid_vchr = c.MEDICINEID_CHR
                               and a.CHARGEDOCTORID_CHR = d.empid_chr(+) 
                               and a.STATUS_INT = 1
                               and a.pstatus_int in(2,3,4)
                               [medicinetype]
                               [area] 
                               and a.create_dat >= ?
                               and a.create_dat <= ?
                              group by  d.empno_chr,
                                       d.lastname_vchr,
                                       d.deptname_vchr,
                                       c.ASSISTCODE_CHR,
                                       c.medicinename_vchr,
                                       c.MEDSPEC_VCHR,
                                       a.unit_vchr";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;

                if (p_strMedType != null && p_strMedType != "")
                {
                    strSQL = strSQL.Replace("[medicinetype]", "and c.medicinetypeid_chr in (" + p_strMedType + ")");
                }
                else
                {
                    strSQL = strSQL.Replace("[medicinetype]", "");
                }

                if (p_strAreaId != null && p_strAreaId != "")
                {
                    strSQL = strSQL.Replace("[area]", "and a.CHARGEDOCTORID_CHR in (" + p_strAreaId + ")");
                }
                else
                {
                    strSQL = strSQL.Replace("[area]", "");
                }

                objHRPSvc.CreateDatabaseParameter(2, out arrParams);
                arrParams[0].Value = DateTime.Parse(p_strBegin);
                arrParams[1].Value = DateTime.Parse(p_strEnd);
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);
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

        #region 住院科室常用药品统计
        /// <summary>
        /// 住院科室常用药品统计
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strBegin"></param>
        /// <param name="p_strEnd"></param>
        /// <param name="p_strAreaId"></param>
        /// <param name="p_strMedType"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetDeptMedByDate(string p_strBegin, string p_strEnd, string p_strAreaId, string p_strMedType, out DataTable p_dtResult)
        {
            long lngRes = 0;

            p_dtResult = new DataTable();

            string strSQL = @"select c.medicinename_vchr,
                                   c.MEDSPEC_VCHR,
                                   c.ASSISTCODE_CHR,
                                   a.unit_vchr,
                                   d.code_vchr,
                                   d.deptname_vchr,
                                   sum (a.AMOUNT_DEC) acount,
                                   sum(a.TOTALMONEY_DEC) totalmoney
                              from T_Opr_Bih_PatientCharge a, 
                                  t_bse_chargeitem b, 
                                  T_BSE_MEDICINE c,
                                  t_bse_deptdesc d
                             where a.chargeitemid_chr = b.itemid_chr
                               and b.itemsrcid_vchr = c.MEDICINEID_CHR
                               and a.createarea_chr = d.deptid_chr
                               and a.STATUS_INT = 1
                               and a.pstatus_int in(2,3,4)
                               [medicinetype]
                               [area] 
                               and a.create_dat > ?
                               and a.create_dat < ?
                              group by d.code_vchr,
                                       d.deptname_vchr,
                                       c.ASSISTCODE_CHR,
                                       c.medicinename_vchr,
                                       c.MEDSPEC_VCHR,
                                       a.unit_vchr";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;

                if (p_strMedType != null && p_strMedType != "")
                {
                    strSQL = strSQL.Replace("[medicinetype]", "and c.medicinetypeid_chr in (" + p_strMedType + ")");
                }
                else
                {
                    strSQL = strSQL.Replace("[medicinetype]", "");
                }

                if (p_strAreaId != null && p_strAreaId != "")
                {
                    strSQL = strSQL.Replace("[area]", "and a.createarea_chr in (" + p_strAreaId + ")");
                }
                else
                {
                    strSQL = strSQL.Replace("[area]", "");
                }

                objHRPSvc.CreateDatabaseParameter(2, out arrParams);
                arrParams[0].Value = DateTime.Parse(p_strBegin);
                arrParams[1].Value = DateTime.Parse(p_strEnd);
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);
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

        #region 住院药品统计
        /// <summary>
        /// 住院药品统计
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strBegin"></param>
        /// <param name="p_strEnd"></param>
        /// <param name="p_strAreaId"></param>
        /// <param name="p_strMedType"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetMedicineByDate(string p_strBegin, string p_strEnd, string p_strAreaId, string p_strMedType, out DataTable p_dtResult)
        {
            long lngRes = 0;

            p_dtResult = new DataTable();
            string strSQL = @"select c.medicinename_vchr,
                                   c.MEDSPEC_VCHR,
                                   c.ASSISTCODE_CHR,
                                   a.unit_vchr,
                                   sum (a.AMOUNT_DEC) acount,
                                   sum(a.TOTALMONEY_DEC) totalmoney
                              from T_Opr_Bih_PatientCharge a, 
                                  t_bse_chargeitem b, 
                                  T_BSE_MEDICINE c,
                                  t_bse_deptdesc d
                             where a.chargeitemid_chr = b.itemid_chr
                               and b.itemsrcid_vchr = c.MEDICINEID_CHR
                               and a.createarea_chr = d.deptid_chr
                               and a.STATUS_INT = 1
                               and a.pstatus_int in(2,3,4)
                               [area]
                               [medicinetype] 
                               and a.create_dat > ?
                               and a.create_dat < ?
                              group by c.ASSISTCODE_CHR,
                                       c.medicinename_vchr,
                                       c.MEDSPEC_VCHR,
                                       a.unit_vchr";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;

                if (p_strMedType != null && p_strMedType != "")
                {
                    strSQL = strSQL.Replace("[medicinetype]", "and c.medicinetypeid_chr in (" + p_strMedType + ")");
                }
                else
                {
                    strSQL = strSQL.Replace("[medicinetype]", "");
                }

                if (p_strAreaId != null && p_strAreaId != "")
                {
                    strSQL = strSQL.Replace("[area]", "and a.createarea_chr in (" + p_strAreaId + ")");
                }
                else
                {
                    strSQL = strSQL.Replace("[area]", "");
                }

                objHRPSvc.CreateDatabaseParameter(2, out arrParams);
                arrParams[0].Value = DateTime.Parse(p_strBegin);
                arrParams[1].Value = DateTime.Parse(p_strEnd);
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);
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

        #region 住院科室药品金额汇总
        /// <summary>
        /// 住院科室药品金额汇总
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strBegin"></param>
        /// <param name="p_strEnd"></param>
        /// <param name="p_strAreaId"></param>
        /// <param name="p_strMedType"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetMedicineMoneyByDate(string p_strBegin, string p_strEnd, string p_strAreaId, string p_strMedType, out DataTable p_dtResult)
        {
            long lngRes = 0;

            p_dtResult = new DataTable();
            string strSQL = @"select 
                                   d.code_vchr,
                                   d.deptname_vchr,
                                   sum (a.AMOUNT_DEC) med_acount,
                                   sum(a.TOTALMONEY_DEC) med_totalmoney
                              from T_Opr_Bih_PatientCharge a, 
                                  t_bse_chargeitem b, 
                                  T_BSE_MEDICINE c,
                                  t_bse_deptdesc d
                             where a.chargeitemid_chr = b.itemid_chr
                               and b.itemsrcid_vchr = c.MEDICINEID_CHR
                               and a.createarea_chr = d.deptid_chr
                               and a.STATUS_INT = 1
                               and a.pstatus_int in(2,3,4)
                               [medicinetype]
                               [area] 
                               and a.create_dat > ?
                               and a.create_dat < ?
                              group by d.code_vchr,
                                       d.deptname_vchr";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;

                if (p_strMedType != null && p_strMedType != "")
                {
                    strSQL = strSQL.Replace("[medicinetype]", "and c.medicinetypeid_chr in (" + p_strMedType + ")");
                }
                else
                {
                    strSQL = strSQL.Replace("[medicinetype]", "");
                }

                if (p_strAreaId != null && p_strAreaId != "")
                {
                    strSQL = strSQL.Replace("[area]", "and a.createarea_chr in (" + p_strAreaId + ")");
                }
                else
                {
                    strSQL = strSQL.Replace("[area]", "");
                }

                objHRPSvc.CreateDatabaseParameter(2, out arrParams);
                arrParams[0].Value = DateTime.Parse(p_strBegin);
                arrParams[1].Value = DateTime.Parse(p_strEnd);
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);
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

        #region 住院科室金额汇总
        /// <summary>
        /// 住院科室金额汇总
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strBegin"></param>
        /// <param name="p_strEnd"></param>
        /// <param name="p_strAreaId"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetTotalMoneyByDate(string p_strBegin, string p_strEnd, string p_strAreaId, out DataTable p_dtResult)
        {
            long lngRes = 0;

            p_dtResult = new DataTable();
            string strSQL = @"select 
                                   d.code_vchr,
                                   d.deptname_vchr,
                                   sum (a.AMOUNT_DEC) acount,
                                   sum(a.TOTALMONEY_DEC) totalmoney
                              from T_Opr_Bih_PatientCharge a, 
                                  t_bse_chargeitem b, 
                                  t_bse_deptdesc d
                             where a.chargeitemid_chr = b.itemid_chr
                               and a.createarea_chr = d.deptid_chr
                               and a.STATUS_INT = 1
                               and a.pstatus_int in(2,3,4)
                               [area] 
                               and a.create_dat > ?
                               and a.create_dat < ?
                              group by d.code_vchr,
                                       d.deptname_vchr";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;

                if (p_strAreaId != null && p_strAreaId != "")
                {
                    strSQL = strSQL.Replace("[area]", "and a.createarea_chr in (" + p_strAreaId + ")");
                }
                else
                {
                    strSQL = strSQL.Replace("[area]", "");
                }

                objHRPSvc.CreateDatabaseParameter(2, out arrParams);
                arrParams[0].Value = DateTime.Parse(p_strBegin);
                arrParams[1].Value = DateTime.Parse(p_strEnd);
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);
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


        #region 医院工作日志（住院）

        [AutoComplete]
        public long GetTotalbeinHospitaldept(string attributeids, string PAYTYPEID_CHR, string p_strDate, out DataTable p_dtResult)
        {
            long lngRes = 0;

            p_dtResult = new DataTable();
            /*
            
            string strSQL = @"
             select k1.deptid_chr area_id,k1.deptname_vchr area_name,
   k1.parentid,
   (select deptname_vchr from t_bse_deptdesc k where k.deptid_chr= k1.parentid) 

deptname_vchr,
   k2.num beCount,k3.num inCount,k4.num outCount,'' opCount,'' deadCount,k6.num ybCount,k5.num 

bedCount,k1.CODE_VCHR
   from t_bse_deptdesc k1,
   --在院人数
   (
 select a.deptid_chr,a.deptname_vchr,a.parentid,b.num from 
t_bse_deptdesc a,
(     
SELECT count(t2.registerid_chr) num,t2.targetareaid_chr areaid_chr
                  FROM t_opr_bih_register t1,t_opr_bih_transfer t2
                WHERE 
                t1.registerid_chr=t2.registerid_chr
                and t2.modify_dat<=?
                and t2.type_int in(3,5)
                and  not exists --不算出院的病人
                (select a.registerid_chr
                         from t_opr_bih_transfer a
                        where a.registerid_chr = t1.registerid_chr
                          and a.type_int in ( 6, 7)
                          and a.modify_dat <?)
                and  not exists --不算转出的病人
                (select b.registerid_chr
                         from t_opr_bih_transfer b
                        where b.registerid_chr = t1.registerid_chr
                          and b.type_int in ( 3)
                          and b.modify_dat <?
                          and b.targetareaid_chr<>t2.targetareaid_chr
                          and exists
                               (
                                 select a.transferid_chr from t_opr_bih_transfer a where a.type_int in (3,5)
                                 and
                                 a.modify_dat<b.modify_dat
                                 and
                                 a.registerid_chr=b.registerid_chr
                                 and
                                 a.targetareaid_chr=b.targetareaid_chr
                                 )
 


                )
                   and t1.status_int = 1
                   and t1.inpatient_dat <?
                     
                   and t1.inpatient_dat >?
                        
                    group by t2.targetareaid_chr) b
                    where a.deptid_chr=b.areaid_chr(+)
                     and a.attributeid = [attributeid]
                   ) k2,
                   
      (             
    select a.deptid_chr,a.deptname_vchr,a.parentid,b.num from 
t_bse_deptdesc a,
(                 
                   --入院人数 
SELECT COUNT (DISTINCT tr.registerid_chr) num,tr.targetareaid_chr areaid_chr
                  FROM t_opr_bih_transfer tr, t_opr_bih_register reg
                 WHERE tr.registerid_chr = reg.registerid_chr
                   AND tr.type_int = 5
                   AND reg.status_int = 1 
                   
                   AND reg.INPATIENT_DAT >=?
                          
                   AND reg.INPATIENT_DAT <=?
                         
                                  group by tr.targetareaid_chr)b
                    where a.deptid_chr=b.areaid_chr(+)
                     and a.attributeid = [attributeid]
             
             )k3,
             
             (
               select a.deptid_chr,a.deptname_vchr,a.parentid,b.num from 
t_bse_deptdesc a,
(                 
            
               --出院
               SELECT COUNT (DISTINCT registerid_chr) num,OUTAREAID_CHR areaid_chr
                  FROM t_opr_bih_leave
                 WHERE status_int = 1
                   AND outhospital_dat >=?
                          
                   AND outhospital_dat <=?
                        
                   group by   OUTAREAID_CHR
                   ) b
                    where a.deptid_chr=b.areaid_chr(+)
                     and a.attributeid = [attributeid]
                     )k4,
                     --病区病床总数 
                     (
                      select a.deptid_chr,a.deptname_vchr,a.parentid,b.num from 
              t_bse_deptdesc a,
                (     
            select count(bedid_chr)num ,areaid_chr from
                  t_bse_bed 
                  group by areaid_chr
                  ) b
                     where a.deptid_chr=b.areaid_chr(+)
                     and a.attributeid = [attributeid]
                     ) k5,
--医保人数
 (
 select a.deptid_chr,a.deptname_vchr,a.parentid,b.num from 
t_bse_deptdesc a,  
(     
SELECT count(t2.registerid_chr) num,t2.targetareaid_chr areaid_chr
                  FROM t_opr_bih_register t1,t_opr_bih_transfer t2
                WHERE 
                t1.registerid_chr=t2.registerid_chr
                and t2.modify_dat<=?
                and t2.type_int in(3,5)
                and  not exists --不算出院的病人
                (select a.registerid_chr
                         from t_opr_bih_transfer a
                        where a.registerid_chr = t1.registerid_chr
                          and a.type_int in ( 6, 7)
                          and a.modify_dat <?)
                and  not exists --不算转出的病人
                (select b.registerid_chr
                         from t_opr_bih_transfer b
                        where b.registerid_chr = t1.registerid_chr
                          and b.type_int in ( 3)
                          and b.modify_dat <?
                          and b.targetareaid_chr<>t2.targetareaid_chr
                          and exists
                               (
                                 select a.transferid_chr from t_opr_bih_transfer a where a.type_int in (3,5)
                                 and
                                 a.modify_dat<b.modify_dat
                                 and
                                 a.registerid_chr=b.registerid_chr
                                 and
                                 a.targetareaid_chr=b.targetareaid_chr
                                 )
 


                )
                   and t1.status_int = 1
                   and t1.inpatient_dat <?
                     
                   and t1.inpatient_dat >?
                     and t1.PAYTYPEID_CHR in ([PAYTYPEID_CHR])    
                    group by t2.targetareaid_chr) b
                    where a.deptid_chr=b.areaid_chr(+)
                     and a.attributeid = [attributeid]
                   ) k6

                      where 
                    k1.DEPTID_CHR=k2.DEPTID_CHR
                    and
                    k1.DEPTID_CHR=k3.DEPTID_CHR
                    and
                    k1.DEPTID_CHR=k4.DEPTID_CHR
                      and
                    k1.DEPTID_CHR=k5.DEPTID_CHR
                     and
                    k1.DEPTID_CHR=k6.DEPTID_CHR
             ";

            try
            {
                string p_strStart = "2007-1-1 00:00:00";
                DateTime m_dtStart;
                DateTime.TryParse(p_strStart, out m_dtStart);
                DateTime m_dtDate;
                DateTime.TryParse(p_strDate, out m_dtDate);
                DateTime m_dtDate1;
                DateTime m_dtDate2;
                m_dtDate1 = m_dtDate.Date;
                m_dtDate2 = m_dtDate.Date.AddHours(24);
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;


                objHRPSvc.CreateDatabaseParameter(14, out arrParams);
                int n = -1;
                //在院人数 5
                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = m_dtDate2;

                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = m_dtStart;

                //入院人数 2

                arrParams[++n].Value = m_dtDate1;
                arrParams[++n].Value = m_dtDate2;

                //出院 2
                arrParams[++n].Value = m_dtDate1;
                arrParams[++n].Value = m_dtDate2;
                //病区病床总数 
               // 医保人数 5
                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = m_dtDate2;

                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = m_dtStart;
                

                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
                strSQL = strSQL.Replace("[attributeid]", attributeids).Replace("[PAYTYPEID_CHR]", PAYTYPEID_CHR);
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);


                objHRPSvc.Dispose();


            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
             */



            string strSQL = @"
 select k1.deptid_chr area_id,k1.deptname_vchr area_name,k1.parentid,
   (select deptname_vchr from t_bse_deptdesc k where k.deptid_chr= k1.parentid) deptname_vchr,
   k2.num becount,k3.num incount,k4.num outcount,'' opcount,'' deadcount,k6.num ybcount,k5.num bedcount,k1.code_vchr
   from t_bse_deptdesc k1,
                     --在院人数
   (
     select a.deptid_chr,a.deptname_vchr,a.parentid,b.num from 
    t_bse_deptdesc a,
    (
    select count(distinct registerid_chr) num,areaid_chr

    from
    (

         select  a.registerid_chr,a.targetareaid_chr areaid_chr
           from t_opr_bih_transfer a,
                t_opr_bih_register a2,
                (
                 --最近一次的流转状态
                 select   max (b.transferid_chr) transferid_chr,
                          b.registerid_chr
                     from t_opr_bih_transfer b
                    where b.modify_dat >=?
                      and b.modify_dat <?
                      and b.type_int<>2
                 group by b.registerid_chr) b
          where a.transferid_chr = b.transferid_chr
            and a.registerid_chr = a2.registerid_chr
            and a2.status_int <> -1
            and a2.relateregisterid_chr is  null
            and a.type_int not in (6, 7)
            --清除有在病人登记表中已实院出院病人，但在出院表没有有效记录的垃极数据
            and not exists (
                   select m.registerid_chr
                     from t_opr_bih_register m
                    where m.status_int = 1
                      and m.registerid_chr = b.registerid_chr
                      and m.pstatus_int in (2, 3)
                      and not exists (
                             select v.registerid_chr
                               from t_opr_bih_leave v
                              where v.modify_dat <sysdate
                                and v.status_int = 1
                                and v.registerid_chr = b.registerid_chr))
                              --撤消出院
                    union all
                    
                    
                    (
                    
                    
                    
                    select d1.registerid_chr,d1.outareaid_chr areaid_chr
                       from t_opr_bih_leave d1,
                            (select   max (v.leaveid_chr) leaveid_chr, registerid_chr
                                 from t_opr_bih_leave v
                                where v.modify_dat <?
                                group by v.registerid_chr) d2
                      where d1.leaveid_chr = d2.leaveid_chr and d1.status_int = 0
                            and exists
                            (
                            select registerid_chr from t_opr_bih_transfer w where w.registerid_chr=d1.registerid_chr 
                             and w.transferid_chr=(select max(transferid_chr) from t_opr_bih_transfer where registerid_chr=w.registerid_chr 
                             and modify_dat >=?
                             and modify_dat <?
                             and w.TARGETAREAID_CHR=d1.outareaid_chr
                             
                             
                              )
                             
                            )


union all
--当日预出院人数
                      select  a.registerid_chr,a.targetareaid_chr
           from t_opr_bih_transfer a,
                t_opr_bih_register a2,
                (
                 --最近一次的流转状态
                 select   max (b.transferid_chr) transferid_chr,
                          b.registerid_chr
                     from t_opr_bih_transfer b
                     where
                          b.modify_dat >=?
                      and b.modify_dat <?
                      and b.type_int<>2
                 group by b.registerid_chr) b
          where 
             a.registerid_chr = a2.registerid_chr
            and a.transferid_chr=b.transferid_chr
            and a2.status_int <> -1
            and a2.relateregisterid_chr is  null
            and a.type_int=7
            and exists
            (
               SELECT a.registerid_chr
                  FROM t_opr_bih_leave a,t_opr_bih_transfer b,t_opr_bih_register reg
                 WHERE 
                   a.registerid_chr=b.registerid_chr
                   and a.registerid_chr=reg.registerid_chr
                   and reg.relateregisterid_chr is  null
                   and b.type_int=7
                   AND a.outhospital_dat >=?
                   and a.outareaid_chr=a.targetareaid_chr
                   and a.registerid_chr=a2.registerid_chr
            )

                      
                         )
                      )
                      group by areaid_chr
                      
                      ) b
                         where a.deptid_chr=b.areaid_chr(+)
                          and a.attributeid = [attributeid]
                       ) k2,
  (             
    select a.deptid_chr,a.deptname_vchr,a.parentid,b.num from 
t_bse_deptdesc a,
(                 
                   --入院人数 
select count (distinct tr.registerid_chr) num,tr.targetareaid_chr areaid_chr
                  from t_opr_bih_transfer tr, t_opr_bih_register reg
                 where tr.registerid_chr = reg.registerid_chr
                   and tr.type_int in(4, 5)
                   and reg.status_int = 1 
                   AND reg.relateregisterid_chr is  null
                   and tr.modify_dat >=?
                          
                   and tr.modify_dat <?
                         
                   group by tr.targetareaid_chr)b
                    where a.deptid_chr=b.areaid_chr(+)
                     and a.attributeid = [attributeid]
             
             )k3,
             
             (
               select a.deptid_chr,a.deptname_vchr,a.parentid,b.num from 
t_bse_deptdesc a,
(                 
            
               --出院
               select count (distinct a.registerid_chr) num,a.outareaid_chr areaid_chr
                  from t_opr_bih_leave a,t_opr_bih_transfer b,t_opr_bih_register reg
                 WHERE 
                   a.registerid_chr=b.registerid_chr
                   and a.registerid_chr=reg.registerid_chr
                   and reg.relateregisterid_chr is  null
                   and b.type_int in (6,7)
                   AND a.status_int = 1
                   AND a.outhospital_dat >=?
                   AND a.outhospital_dat <?
                   group by   a.outareaid_chr
                   ) b
                    where a.deptid_chr=b.areaid_chr(+)
                     and a.attributeid = [attributeid]
                     )k4,
                     --病区病床总数 
                     (
                      select a.deptid_chr,a.deptname_vchr,a.parentid,STDBED_COUNT_INT num from 
              t_bse_deptdesc a 
                     where a.attributeid = [attributeid]
                     ) k5,
--医保人数
 (
 select a.deptid_chr,a.deptname_vchr,a.parentid,b.num from 
t_bse_deptdesc a,  
 (
    select count(distinct a.registerid_chr) num,a.areaid_chr

    from
    (

         select  a.registerid_chr,a.targetareaid_chr areaid_chr
           from t_opr_bih_transfer a,
                t_opr_bih_register a2,
                (
                 --最近一次的流转状态
                 select   max (b.transferid_chr) transferid_chr,
                          b.registerid_chr
                     from t_opr_bih_transfer b
                    where b.modify_dat >=?
                      and b.modify_dat <?
                 group by b.registerid_chr) b
          where a.transferid_chr = b.transferid_chr
            and a.registerid_chr = a2.registerid_chr
            and a2.status_int <> -1
            and a2.relateregisterid_chr is  null
            and a.type_int not in (6, 7)
            --清除有在病人登记表中已实院出院病人，但在出院表没有有效记录的垃极数据
            and not exists (
                   select m.registerid_chr
                     from t_opr_bih_register m
                    where m.status_int = 1
                      and m.registerid_chr = b.registerid_chr
                      and m.pstatus_int in (2, 3)
                      and not exists (
                             select v.registerid_chr
                               from t_opr_bih_leave v
                              where v.modify_dat <sysdate
                                and v.status_int = 1
                                and v.registerid_chr = b.registerid_chr))
                       

                   --撤消出院
                    union all
                    
                    
                    (
                    
                    
                    
                    select d1.registerid_chr,d1.outareaid_chr areaid_chr
                       from t_opr_bih_leave d1,t_opr_bih_register t1,
                            (select   max (v.leaveid_chr) leaveid_chr, registerid_chr
                                 from t_opr_bih_leave v
                                where v.modify_dat <?
                                group by v.registerid_chr) d2
                      where d1.registerid_chr=t1.registerid_chr and d1.leaveid_chr = d2.leaveid_chr and d1.status_int = 0
                            
                            and exists
                            (
                            select registerid_chr from t_opr_bih_transfer w where w.registerid_chr=d1.registerid_chr 
                             and w.transferid_chr=(select max(transferid_chr) from t_opr_bih_transfer where registerid_chr=w.registerid_chr 
                             and modify_dat >=?
                             and modify_dat <?
                             and w.TARGETAREAID_CHR=d1.outareaid_chr
                             
                             
                              )
                             
                            )
                      
                         )


union all
--当日预出院人数
                      select  a.registerid_chr,a.targetareaid_chr
           from t_opr_bih_transfer a,
                t_opr_bih_register a2,
                (
                 --最近一次的流转状态
                 select   max (b.transferid_chr) transferid_chr,
                          b.registerid_chr
                     from t_opr_bih_transfer b
                     where
                          b.modify_dat >=?
                      and b.modify_dat <?
                      and b.type_int<>2
                 group by b.registerid_chr) b
          where 
             a.registerid_chr = a2.registerid_chr
            and a.transferid_chr=b.transferid_chr
            and a2.status_int <> -1
            and a2.relateregisterid_chr is  null
            and a.type_int=7
            and exists
            (
               SELECT a.registerid_chr
                  FROM t_opr_bih_leave a,t_opr_bih_transfer b,t_opr_bih_register reg
                 WHERE 
                   a.registerid_chr=b.registerid_chr
                   and a.registerid_chr=reg.registerid_chr
                   and reg.relateregisterid_chr is  null
                   and b.type_int=7
                   AND a.outhospital_dat >=?
                   and a.outareaid_chr=a.targetareaid_chr
                   and a.registerid_chr=a2.registerid_chr
            )

                      ) a, t_opr_bih_register b
                      where a.registerid_chr=b.registerid_chr
                      and b.paytypeid_chr in ([paytypeid_chr])
                      group by a.areaid_chr
                      
                      ) b
                    where a.deptid_chr=b.areaid_chr(+)
                     and a.attributeid = [attributeid]
                   ) k6

                      where 
                    k1.deptid_chr=k2.deptid_chr
                    and
                    k1.deptid_chr=k3.deptid_chr
                    and
                    k1.deptid_chr=k4.deptid_chr
                      and
                    k1.deptid_chr=k5.deptid_chr
                     and
                    k1.deptid_chr=k6.deptid_chr
                     ";

            try
            {
                string p_strStart = "2007-1-1 00:00:00";
                DateTime m_dtStart;
                DateTime.TryParse(p_strStart, out m_dtStart);
                DateTime m_dtDate;
                DateTime.TryParse(p_strDate, out m_dtDate);
                DateTime m_dtDate1;
                DateTime m_dtDate2;
                m_dtDate1 = m_dtDate.Date;
                m_dtDate2 = m_dtDate.Date.AddHours(24);
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;


                objHRPSvc.CreateDatabaseParameter(20, out arrParams);
                int n = -1;
                //在院人数 6
                arrParams[++n].Value = m_dtStart;
                arrParams[++n].Value = m_dtDate2;

                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = m_dtStart;
                arrParams[++n].Value = m_dtDate2;


                arrParams[++n].Value = m_dtStart;
                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = m_dtDate2;
                /*<===============*/
                /*<===============*/



                //入院人数 2
                arrParams[++n].Value = m_dtDate1;
                arrParams[++n].Value = m_dtDate2;

                //出院 2
                arrParams[++n].Value = m_dtDate1;
                arrParams[++n].Value = m_dtDate2;
                //病区病床总数 
                //在院人数 6
                arrParams[++n].Value = m_dtStart;
                arrParams[++n].Value = m_dtDate2;

                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = m_dtStart;
                arrParams[++n].Value = m_dtDate2;

                arrParams[++n].Value = m_dtStart;
                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = m_dtDate2;
                /*<===============*/

                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
                strSQL = strSQL.Replace("[attributeid]", attributeids).Replace("[paytypeid_chr]", PAYTYPEID_CHR);
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);


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


        #region 专业组工作日志1-专业组
        [AutoComplete]
        public long GetTranfergroup(string doctorgroupid_chr, string PAYTYPEID_CHR, string p_strDate, out DataTable p_dtResult)
        {
            DateTime dtmBegin = DateTime.Parse(p_strDate);
            DateTime dtmEnd = DateTime.Parse(dtmBegin.ToString("yyyy-MM-dd") + " 23:59:59");
            long lngRes = -1;
            string strSQL = "";
            p_dtResult = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] param = null;
                // 1 昨天在院人数
                DataTable dtYesterday = new DataTable();

                strSQL = @"select count(a.registerid_chr) as onnums
                              from (select c.targetareaid_chr as areaid_chr,
                                           a.registerid_chr,
                                           a.paytypeid_chr,
                                           a.inpatient_dat as indate,
                                           nvl(b.outhospital_dat, sysdate + 100) as outdate
                                      from t_opr_bih_register a,
                                           (select registerid_chr, outhospital_dat
                                              from t_opr_bih_leave
                                             where status_int = 1) b,
                                           (select a.registerid_chr, a.targetareaid_chr
                                              from t_opr_bih_transfer a
                                             where a.modify_dat =
                                                   (select max(b.modify_dat)
                                                      from t_opr_bih_transfer b
                                                     where a.registerid_chr = b.registerid_chr
                                                           and (b.type_int <> 0 and b.type_int <> 1)
                                                           and b.modify_dat between ? and ? )
                                                   and a.doctorgroupid_chr = ?) c
                                     where a.registerid_chr = b.registerid_chr(+)
                                           and a.registerid_chr = c.registerid_chr
                                           and a.relateregisterid_chr is null
                                           and a.bedid_chr is not null
                                           and a.status_int = 1) a
                             where (a.indate < ? and
                                   a.outdate >= ? )";
                objHRPSvc.CreateDatabaseParameter(5, out param);
                param[0].DbType = DbType.DateTime;
                param[0].Value = dtmBegin - TimeSpan.FromHours(24);
                param[1].DbType = DbType.DateTime;
                param[1].Value = dtmEnd - TimeSpan.FromHours(24);
                param[2].Value = doctorgroupid_chr;
                param[3].DbType = DbType.DateTime;
                param[3].Value = dtmBegin - TimeSpan.FromHours(24); ;
                param[4].DbType = DbType.DateTime;
                param[4].Value = dtmEnd - TimeSpan.FromHours(24); ;

                #region 
                /*
                strSQL = @"select count (distinct registerid_chr) num
  from (select a.registerid_chr
          from t_opr_bih_transfer a,
               t_opr_bih_register a2,
               (select   max (b.transferid_chr) transferid_chr,
                         b.registerid_chr
                    from t_opr_bih_transfer b
                   where b.modify_dat >= ?
                     and b.modify_dat < ?
                     and b.type_int <> 2
                group by b.registerid_chr) b
         where a.doctorgroupid_chr = ?
           and a.registerid_chr = a2.registerid_chr
           and a.transferid_chr = b.transferid_chr
           and a2.status_int <> -1
           and a2.relateregisterid_chr is null
           and a.type_int not in (6, 7)
           and not exists (
                  select m.registerid_chr
                    from t_opr_bih_register m
                   where m.status_int = 1
                     and m.registerid_chr = b.registerid_chr
                     and m.pstatus_int in (2, 3)
                     and not exists (
                            select v.registerid_chr
                              from t_opr_bih_leave v
                             where v.modify_dat < sysdate
                               and v.status_int = 1
                               and v.registerid_chr = b.registerid_chr))
        union all
        select distinct a.registerid_chr
                   from t_opr_bih_transfer trf,
                        (select   max (a.transferid_chr) transferid_chr,
                                  a.registerid_chr
                             from t_opr_bih_transfer a
                            where a.modify_dat >= ?
                              and a.modify_dat < ?
                              and a.type_int <> 2
                         group by a.registerid_chr) a
                  where trf.doctorgroupid_chr = ?
                    and trf.transferid_chr = a.transferid_chr
                    and trf.registerid_chr = a.registerid_chr
                    and trf.type_int in (6, 7)
                    and exists (
                           select d1.registerid_chr
                             from t_opr_bih_leave d1
                            where d1.status_int = 0
                              and d1.leaveid_chr =
                                     (select max (leaveid_chr) leaveid_chr
                                        from t_opr_bih_leave k
                                       where k.registerid_chr =
                                                              a.registerid_chr
                                         and k.modify_dat >= ?
                                         and k.modify_dat < ?))
        union all
        select a.registerid_chr
          from t_opr_bih_transfer a,
               t_opr_bih_register a2,
               (select   max (b.transferid_chr) transferid_chr,
                         b.registerid_chr
                    from t_opr_bih_transfer b
                   where b.modify_dat >= ?
                     and b.modify_dat < ?
                     and (b.type_int <> 2 or b.type_int <> -1)
                group by b.registerid_chr) b
         where a.doctorgroupid_chr = ?
           and a.registerid_chr = a2.registerid_chr
           and a.transferid_chr = b.transferid_chr
           and a2.status_int <> -1
           and a2.relateregisterid_chr is null
           and a.type_int = 7
           and exists (
                  select a.registerid_chr
                    from t_opr_bih_leave a,
                         t_opr_bih_transfer b,
                         t_opr_bih_register reg
                   where a.registerid_chr = b.registerid_chr
                     and a.registerid_chr = reg.registerid_chr
                     and reg.relateregisterid_chr is null
                     and b.type_int = 7
                     and a.outhospital_dat >= ?
                     and b.doctorgroupid_chr = ?
                     and a.registerid_chr = a2.registerid_chr))";
                //strSQL = strSQL.Replace("[PAYTYPEID_CHR]", PAYTYPEID_CHR);

                IDataParameter[] param = null;
                objHRPSvc.CreateDatabaseParameter(13, out param);
                param[0].DbType = DbType.DateTime;
                param[0].Value = dtmBegin;
                param[1].DbType = DbType.DateTime;
                param[1].Value = dtmEnd;
                param[2].Value = doctorgroupid_chr;

                param[3].DbType = DbType.DateTime;
                param[3].Value = dtmBegin;
                param[4].DbType = DbType.DateTime;
                param[4].Value = dtmEnd;
                param[5].Value = doctorgroupid_chr;

                param[6].DbType = DbType.DateTime;
                param[6].Value = dtmBegin;
                param[7].DbType = DbType.DateTime;
                param[7].Value = dtmEnd;


                param[8].DbType = DbType.DateTime;
                param[8].Value = dtmBegin;
                param[9].DbType = DbType.DateTime;
                param[9].Value = dtmEnd;
                param[10].Value = doctorgroupid_chr;

                param[11].DbType = DbType.DateTime;
                param[11].Value = dtmBegin;
                param[12].Value = doctorgroupid_chr;
                */
                #endregion

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtYesterday, param);

                // 2 入院人数

                strSQL = @"select count (a.registerid_chr) as innums
                              from t_opr_bih_transfer a, t_opr_bih_register c
                             where a.registerid_chr = c.registerid_chr
                               and c.relateregisterid_chr is null
                               and c.bedid_chr is not null
                               and c.status_int = 1
                               and a.modify_dat =
                                      (select min (b.modify_dat)
                                         from t_opr_bih_transfer b
                                        where a.registerid_chr = b.registerid_chr
                                          and b.type_int = 5
                                          and (b.modify_dat between ? and ?)) 
                               and a.doctorgroupid_chr = ? ";
                //strSQL = strSQL.Replace("[PAYTYPEID_CHR]", PAYTYPEID_CHR);
                DataTable dtInnums = new DataTable();
                objHRPSvc.CreateDatabaseParameter(3, out param);
                param[0].DbType = DbType.DateTime;
                param[0].Value = dtmBegin;
                param[1].DbType = DbType.DateTime;
                param[1].Value = dtmEnd;
                param[2].Value = doctorgroupid_chr;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtInnums, param);

                // 3 转入人数
                strSQL = @"select count (a.registerid_chr) as transinnums
                              from t_opr_bih_transfer a, t_opr_bih_register b
                             where a.registerid_chr = b.registerid_chr
                               and b.relateregisterid_chr is null
                               and b.bedid_chr is not null
                               and b.status_int = 1
                               and a.type_int = 3 
                               and a.doctorgroupid_chr = ?
                               and (a.modify_dat between ? and ?) ";
                //strSQL = strSQL.Replace("[PAYTYPEID_CHR]", PAYTYPEID_CHR);
                DataTable dtTransinnums = new DataTable();
                objHRPSvc.CreateDatabaseParameter(3, out param);
                param[0].Value = doctorgroupid_chr;
                param[1].DbType = DbType.DateTime;
                param[1].Value = dtmBegin;
                param[2].DbType = DbType.DateTime;
                param[2].Value = dtmEnd;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtTransinnums, param);

                // 4 转出人数1
                strSQL = @"select count (registerid_chr)
                  from (select distinct reg.registerid_chr,
                                        reg.inpatientid_chr as inpatientid,
                                        pat.lastname_vchr as lastname, pat.sex_chr as sex,
                                        bed.code_chr, gro.groupname_vchr,
                                        s_bed.code_chr s_sourcebedid_chr,
                                        '' s_targetbedid_chr, '' s_sourceareaid_chr,
                                        '' s_targetareaid_chr,
                                        (select groupname_vchr
                                           from t_opr_bih_transfer a,
                                                t_bse_groupdesc b
                                          where a.doctorgroupid_chr = b.groupid_chr(+)
                                            and a.type_int in (3, 5)
                                            and a.modify_dat < trf.modify_dat
                                            and a.registerid_chr = trf.registerid_chr
                                            and a.targetareaid_chr = trf.sourceareaid_chr
                                            and rownum = 1) s_groupname_vchr
                                   from t_bse_bed bed,
                                        t_opr_bih_transfer trf,
                                        t_opr_bih_register reg,
                                        t_opr_bih_registerdetail pat,
                                        t_bse_groupdesc gro,
                                        t_bse_bed s_bed
                                  where trf.registerid_chr = reg.registerid_chr
                                    and reg.registerid_chr = pat.registerid_chr
                                    and trf.targetbedid_chr = bed.bedid_chr(+)
                                    and trf.doctorgroupid_chr = gro.groupid_chr(+)
                                    and trf.sourcebedid_chr = s_bed.bedid_chr(+)
                                    and exists (
                                           select s.transferid_chr
                                             from t_opr_bih_transfer s, t_opr_bih_transfer t
                                            where s.registerid_chr = t.registerid_chr
                                              and s.targetareaid_chr = t.sourceareaid_chr
                                              and trf.transferid_chr = s.transferid_chr
                                              and s.doctorgroupid_chr = ?
                                              and t.doctorgroupid_chr <> ?
                                              and t.modify_dat > s.modify_dat
                                              and s.type_int in (3, 5)
                                              and t.type_int = 3
                                              and (t.modify_dat between ? and ?))) ";
                //strSQL = strSQL.Replace("[PAYTYPEID_CHR]", PAYTYPEID_CHR);
                DataTable dtTransoutnums = new DataTable();
                objHRPSvc.CreateDatabaseParameter(4, out param);
                param[0].Value = doctorgroupid_chr;
                param[1].Value = doctorgroupid_chr;
                param[2].DbType = DbType.DateTime;
                param[2].Value = dtmBegin;
                param[3].DbType = DbType.DateTime;
                param[3].Value = dtmEnd;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtTransoutnums, param);

                // 5 出院人数
                strSQL = @"select count (*)
  from (select   max (e.modify_dat) as maxdate, e.registerid_chr
            from t_opr_bih_leave a, t_opr_bih_register b,
                 t_opr_bih_transfer e
           where a.registerid_chr = b.registerid_chr
             and a.registerid_chr = e.registerid_chr
             and b.relateregisterid_chr is null
             and e.type_int in (6, 7)
             and a.status_int = 1
             and b.status_int = 1
             and e.doctorgroupid_chr = ?
             and (a.outhospital_dat between ? and ?)
        group by e.registerid_chr) ";
                //strSQL = strSQL.Replace("[PAYTYPEID_CHR]", PAYTYPEID_CHR);
                DataTable dtOutNums = new DataTable();
                objHRPSvc.CreateDatabaseParameter(3, out param);
                param[0].Value = doctorgroupid_chr;
                param[1].DbType = DbType.DateTime;
                param[1].Value = dtmBegin;
                param[2].DbType = DbType.DateTime;
                param[2].Value = dtmEnd;

                objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtOutNums, param);

                // 6 出院死亡人数
                strSQL = @"select count (a.registerid_chr) as outdeadnums
                              from t_opr_bih_leave a, t_opr_bih_register b, t_opr_bih_transfer c
                             where a.registerid_chr = b.registerid_chr
                               and b.registerid_chr = c.registerid_chr
                               and b.relateregisterid_chr is null
                               and c.type_int in ('6', '7')
                               and a.status_int = 1
                               and b.status_int = 1
                               and a.type_int = 4
                               and c.doctorgroupid_chr = ?
                               and (a.outhospital_dat between ? and ?)";
                //strSQL = strSQL.Replace("[PAYTYPEID_CHR]", PAYTYPEID_CHR);
                DataTable dtOutdeadNums = new DataTable();
                objHRPSvc.CreateDatabaseParameter(3, out param);
                param[0].Value = doctorgroupid_chr;
                param[1].DbType = DbType.DateTime;
                param[1].Value = dtmBegin;
                param[2].DbType = DbType.DateTime;
                param[2].Value = dtmEnd;

                objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtOutdeadNums, param);

                // 7 出院24小时死亡
                strSQL = @"select count (a.registerid_chr) as outdead24nums
                          from t_opr_bih_leave a, t_opr_bih_register b, t_opr_bih_transfer c
                         where a.registerid_chr = b.registerid_chr
                           and b.registerid_chr = c.registerid_chr
                           and b.relateregisterid_chr is null
                           and c.type_int in ('6', '7')
                           and a.status_int = 1
                           and b.status_int = 1
                           and a.type_int = 4
                           and c.doctorgroupid_chr = ?
                           and floor ((a.outhospital_dat - b.inpatient_dat) * 24) <= 12
                           and (a.outhospital_dat between ? and ?)";
                //strSQL = strSQL.Replace("[PAYTYPEID_CHR]", PAYTYPEID_CHR);
                DataTable dtOutdead24Nums = new DataTable();
                objHRPSvc.CreateDatabaseParameter(3, out param);
                param[0].Value = doctorgroupid_chr;
                param[1].DbType = DbType.DateTime;
                param[1].Value = dtmBegin;
                param[2].DbType = DbType.DateTime;
                param[2].Value = dtmEnd;

                objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtOutdead24Nums, param);

                //                // 8 在院人数
                strSQL = @"select count(a.registerid_chr) as onnums
                              from (select c.targetareaid_chr as areaid_chr,
                                           a.registerid_chr,
                                           a.paytypeid_chr,
                                           a.inpatient_dat as indate,
                                           nvl(b.outhospital_dat, sysdate + 100) as outdate
                                      from t_opr_bih_register a,
                                           (select registerid_chr, outhospital_dat
                                              from t_opr_bih_leave
                                             where status_int = 1) b,
                                           (select a.registerid_chr, a.targetareaid_chr
                                              from t_opr_bih_transfer a
                                             where a.modify_dat =
                                                   (select max(b.modify_dat)
                                                      from t_opr_bih_transfer b
                                                     where a.registerid_chr = b.registerid_chr
                                                           and (b.type_int <> 0 and b.type_int <> 1)
                                                           and b.modify_dat between ? and ? )
                                                   and a.doctorgroupid_chr = ?) c
                                     where a.registerid_chr = b.registerid_chr(+)
                                           and a.registerid_chr = c.registerid_chr
                                           and a.relateregisterid_chr is null
                                           and a.bedid_chr is not null
                                           and a.status_int = 1) a
                             where (a.indate < ? and
                                   a.outdate >= ? )";
                DataTable dtOnnums = new DataTable();
                objHRPSvc.CreateDatabaseParameter(5, out param);
                param[0].DbType = DbType.DateTime;
                param[0].Value = dtmBegin;
                param[1].DbType = DbType.DateTime;
                param[1].Value = dtmEnd;
                param[2].Value = doctorgroupid_chr;
                param[3].DbType = DbType.DateTime;
                param[3].Value = dtmBegin;
                param[4].DbType = DbType.DateTime;
                param[4].Value = dtmEnd;

                objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtOnnums, param);

                //                // 9 病保人数
                //                strSQL = @"select count (a.registerid_chr) as onnums
                //                              from (select c.targetareaid_chr as areaid_chr, a.registerid_chr,
                //                                           a.paytypeid_chr, a.inpatient_dat as indate,
                //                                           nvl (b.outhospital_dat, sysdate + 100) as outdate
                //                                      from t_opr_bih_register a,
                //                                           (select registerid_chr, outhospital_dat
                //                                              from t_opr_bih_leave
                //                                             where status_int = 1) b,
                //                                           (select a.registerid_chr, a.targetareaid_chr
                //                                              from t_opr_bih_transfer a
                //                                             where a.doctorgroupid_chr = ?
                //                                               and a.modify_dat =
                //                                                      (select max (b.modify_dat)
                //                                                         from t_opr_bih_transfer b
                //                                                        where a.registerid_chr = b.registerid_chr
                //                                                          and (b.type_int <> 0 and b.type_int <> 1)
                //                                                          and (b.modify_dat between ? and ?))) c
                //                                     where a.registerid_chr = b.registerid_chr(+)
                //                                       and a.registerid_chr = c.registerid_chr
                //                                       and a.relateregisterid_chr is null
                //                                       and a.bedid_chr is not null
                //                                       and a.status_int = 1) a
                //                             where (a.indate < ? and a.outdate >= ?)
                //                               and a.paytypeid_chr in ([PAYTYPEID_CHR]) ";
                //                strSQL = strSQL.Replace("[PAYTYPEID_CHR]", PAYTYPEID_CHR);
                //                DataTable dtYBnums = new DataTable();
                //                objHRPSvc.CreateDatabaseParameter(5, out param);
                //                param[0].Value = doctorgroupid_chr;
                //                param[1].DbType = DbType.DateTime;
                //                param[1].Value = dtmBegin;
                //                param[2].DbType = DbType.DateTime;
                //                param[2].Value = dtmEnd;
                //                param[3].DbType = DbType.DateTime;
                //                param[3].Value = dtmBegin;
                //                param[4].DbType = DbType.DateTime;
                //                param[4].Value = dtmEnd;

                strSQL = @"select count (a.registerid_chr)
  from t_opr_bih_transfer a, t_opr_bih_register b
 where b.status_int = 1
   and a.registerid_chr = b.registerid_chr
   and b.pstatus_int not in (2, 3)
   and a.type_int in (3, 4, 5)
   and a.doctorgroupid_chr = ?
   and b.inpatient_dat < ?
   and b.paytypeid_chr in (" + PAYTYPEID_CHR + @")
   and not exists (
           select ta.transferid_chr
             from t_opr_bih_transfer ta
            where ta.type_int in (6, 7)
              and a.registerid_chr = ta.registerid_chr) ";
                DataTable dtYBnums = new DataTable();
                objHRPSvc.CreateDatabaseParameter(2, out param);
                param[0].Value = doctorgroupid_chr;
                param[1].DbType = DbType.DateTime;
                param[1].Value = dtmEnd;
                //param[2].DbType = DbType.DateTime;
                //param[2].Value = dtmEnd;

                objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtYBnums, param);

                objHRPSvc.Dispose();


                p_dtResult.BeginLoadData();

                p_dtResult.Columns.Add("yesterdaycount");
                p_dtResult.Columns.Add("inhospitalcount");
                p_dtResult.Columns.Add("incount");
                p_dtResult.Columns.Add("outcount");
                p_dtResult.Columns.Add("outhospitalcount");
                p_dtResult.Columns.Add("deadcount");
                p_dtResult.Columns.Add("deadcountin24");
                p_dtResult.Columns.Add("todaycount");
                p_dtResult.Columns.Add("ybcount");

                DataRow dr = p_dtResult.NewRow();

                dr["yesterdaycount"] = dtYesterday.Rows[0][0].ToString();
                dr["inhospitalcount"] = dtInnums.Rows[0][0].ToString();
                dr["incount"] = dtTransinnums.Rows[0][0].ToString();
                dr["outcount"] = dtTransoutnums.Rows[0][0].ToString();
                dr["outhospitalcount"] = dtOutNums.Rows[0][0].ToString();
                dr["deadcount"] = dtOutdeadNums.Rows[0][0].ToString();
                dr["deadcountin24"] = dtOutdead24Nums.Rows[0][0].ToString();
                dr["todaycount"] = dtOnnums.Rows[0][0].ToString();
                dr["ybcount"] = dtYBnums.Rows[0][0].ToString();
                p_dtResult.Rows.Add(dr);

                p_dtResult.EndLoadData();

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;



        }

        [AutoComplete]
        public long GetTranfergroup2(string doctorgroupid_chr, string PAYTYPEID_CHR, string p_strDate, out DataTable p_dtResult)
        {
            long lngRes = 0;

            p_dtResult = new DataTable();
            string strSQL = @"
           SELECT yesterdaycount,
        inhospitalnum+outBacknum AS inhospitalcount,
       innum AS incount, outnum AS outcount,
       outhospitalnum AS outhospitalcount, deadoutnum AS deadcount,
       deadin24 AS deadcountin24,totalnum AS todaycount, ybcount,yesterdaycount+inhospitalnum+outBacknum+innum-outnum- outhospitalnum-totalnum as precount
  FROM (SELECT     



  --昨日在院人数
               (

    SELECT count(distinct registerid_chr) num
         from
         (
         select  a.registerid_chr
           from t_opr_bih_transfer a,
                t_opr_bih_register a2,
                (
                 --最近一次的流转状态
                 select   max (b.transferid_chr) transferid_chr,
                          b.registerid_chr
                     from t_opr_bih_transfer b
                     where
                          b.modify_dat >=?
                      and b.modify_dat <?
                      and b.type_int<>2
                 group by b.registerid_chr) b
          where 
             a.DOCTORGROUPID_CHR =?
            and a.registerid_chr = a2.registerid_chr
            and a.transferid_chr=b.transferid_chr
            and a2.status_int <> -1
            and a2.relateregisterid_chr is  null
            and a.type_int not in (6, 7)
            --清除有在病人登记表中已实院出院病人，但在出院表没有有效记录的垃极数据
            and not exists (
                   select m.registerid_chr
                     from t_opr_bih_register m
                    where m.status_int = 1
                      and m.registerid_chr = b.registerid_chr
                      and m.pstatus_int in (2, 3)
                      and not exists (
                             select v.registerid_chr
                               from t_opr_bih_leave v
                              where 
                              v.modify_dat <sysdate
                                and 
                                v.status_int = 1
                                and v.registerid_chr = b.registerid_chr))
                              --撤消出院
            union all
                  --撤消出院
                  select distinct a.registerid_chr
                  from t_opr_bih_transfer trf,
                  (
                  select max (a.transferid_chr) transferid_chr, a.registerid_chr 
                  from t_opr_bih_transfer a 
                  where 
                  a.modify_dat>=?
                  and
                  a.modify_dat<?
                  and a.type_int<>2
                  group by a.registerid_chr
                  ) a 
                  where
                  trf.doctorgroupid_chr=?
                  and trf.transferid_chr=a.transferid_chr
                  and
                  trf.registerid_chr=a.registerid_chr
                  and
                  trf.type_int in (6,7)
                  and exists
                  (
                      select d1.registerid_chr
                                       from t_opr_bih_leave d1
                                       where d1.status_int=0 
                                       and d1.leaveid_chr=(select max(leaveid_chr) leaveid_chr from t_opr_bih_leave k
                                                              where 
                                                              k.registerid_chr=a.registerid_chr
                                                              and
                                                              k.modify_dat>=?
                                                              and
                                                              k.modify_dat<?)
                  )


                    union all
--当日预出院人数
                      select  a.registerid_chr
           from t_opr_bih_transfer a,
                t_opr_bih_register a2,
                (
                 --最近一次的流转状态
                 select   max (b.transferid_chr) transferid_chr,
                          b.registerid_chr
                     from t_opr_bih_transfer b
                     where
                          b.modify_dat >=?
                      and b.modify_dat <?
                      and b.type_int<>2
                 group by b.registerid_chr) b
          where 
             a.doctorgroupid_chr=?
            and a.registerid_chr = a2.registerid_chr
            and a.transferid_chr=b.transferid_chr
            and a2.status_int <> -1
            and a2.relateregisterid_chr is  null
            and a.type_int=7
            and exists
            (
               SELECT a.registerid_chr
                  FROM t_opr_bih_leave a,t_opr_bih_transfer b,t_opr_bih_register reg
                 WHERE 
                   a.registerid_chr=b.registerid_chr
                   and a.registerid_chr=reg.registerid_chr
                   and reg.relateregisterid_chr is  null
                   and b.type_int=7
                   AND a.outhospital_dat >=?
                   and b.doctorgroupid_chr=?
                   and a.registerid_chr=a2.registerid_chr
            )



                     
                  )


                ) AS yesterdaycount,


--今日在院人数
               (

      SELECT count(distinct registerid_chr) num
         from
         (
         select  a.registerid_chr
           from t_opr_bih_transfer a,
                t_opr_bih_register a2,
                (
                 --最近一次的流转状态
                 select   max (b.transferid_chr) transferid_chr,
                          b.registerid_chr
                     from t_opr_bih_transfer b
                     where
                          b.modify_dat >=?
                      and b.modify_dat <?
                      and b.type_int<>2
                 group by b.registerid_chr) b
          where 
             a.DOCTORGROUPID_CHR =?
            and a.registerid_chr = a2.registerid_chr
            and a.transferid_chr=b.transferid_chr
            and a2.status_int <> -1
            and a2.relateregisterid_chr is  null
            and a.type_int not in (6, 7)
            --清除有在病人登记表中已实院出院病人，但在出院表没有有效记录的垃极数据
            and not exists (
                   select m.registerid_chr
                     from t_opr_bih_register m
                    where m.status_int = 1
                      and m.registerid_chr = b.registerid_chr
                      and m.pstatus_int in (2, 3)
                      and not exists (
                             select v.registerid_chr
                               from t_opr_bih_leave v
                              where 
                              v.modify_dat <sysdate
                                and 
                                v.status_int = 1
                                and v.registerid_chr = b.registerid_chr))
                              --撤消出院
            union all
                  --撤消出院
                  select distinct a.registerid_chr
                  from t_opr_bih_transfer trf,
                  (
                  select max (a.transferid_chr) transferid_chr, a.registerid_chr 
                  from t_opr_bih_transfer a 
                  where 
                  a.modify_dat>=?
                  and
                  a.modify_dat<?
                  and a.type_int<>2
                  group by a.registerid_chr
                  ) a 
                  where
                  trf.doctorgroupid_chr=?
                  and trf.transferid_chr=a.transferid_chr
                  and
                  trf.registerid_chr=a.registerid_chr
                  and
                  trf.type_int in (6,7)
                  and exists
                  (
                      select d1.registerid_chr
                                       from t_opr_bih_leave d1
                                       where d1.status_int=0 
                                       and d1.leaveid_chr=(select max(leaveid_chr) leaveid_chr from t_opr_bih_leave k
                                                              where 
                                                              k.registerid_chr=a.registerid_chr
                                                              and
                                                              k.modify_dat>=?
                                                              and
                                                              k.modify_dat<?)
                  )

    union all
--当日预出院人数
                      select  a.registerid_chr
           from t_opr_bih_transfer a,
                t_opr_bih_register a2,
                (
                 --最近一次的流转状态
                 select   max (b.transferid_chr) transferid_chr,
                          b.registerid_chr
                     from t_opr_bih_transfer b
                     where
                          b.modify_dat >=?
                      and b.modify_dat <?
                      and b.type_int<>2
                 group by b.registerid_chr) b
          where 
             a.doctorgroupid_chr=?
            and a.registerid_chr = a2.registerid_chr
            and a.transferid_chr=b.transferid_chr
            and a2.status_int <> -1
            and a2.relateregisterid_chr is  null
            and a.type_int=7
            and exists
            (
               SELECT a.registerid_chr
                  FROM t_opr_bih_leave a,t_opr_bih_transfer b,t_opr_bih_register reg
                 WHERE 
                   a.registerid_chr=b.registerid_chr
                   and a.registerid_chr=reg.registerid_chr
                   and reg.relateregisterid_chr is  null
                   and b.type_int=7
                   AND a.outhospital_dat >=?
                   and b.doctorgroupid_chr=?
                   and a.registerid_chr=a2.registerid_chr
            )
                     
                  )


                ) AS totalnum,
               
               --今日入院人数
               (SELECT COUNT (DISTINCT tr.registerid_chr)
                  FROM t_opr_bih_transfer tr, t_opr_bih_register reg
                 WHERE tr.registerid_chr = reg.registerid_chr
                   AND tr.type_int = 5
                   AND reg.status_int = 1 
                   AND reg.relateregisterid_chr is  null
                   AND tr.modify_dat >=?
                   AND tr.modify_dat <?
                   and tr.doctorgroupid_chr=?
                   ) AS inhospitalnum,
               
               --转入人数
               (SELECT COUNT (registerid_chr)
                  FROM t_opr_bih_transfer
                 WHERE type_int = 3
				   AND targetbedid_chr IS NOT NULL	
                   AND modify_dat >=?
                   AND modify_dat <?
                   and doctorgroupid_chr=?
                   ) AS innum,
               
               -- 转出人数
               (SELECT COUNT (registerid_chr)
                  FROM t_opr_bih_transfer trf
                 WHERE type_int = 3
                   AND modify_dat >=?
                   AND modify_dat <?
                   and doctorgroupid_chr<>?
                    and
                                exists
                                (
                                  select a.transferid_chr from t_opr_bih_transfer a where a.type_int in (3,5)
                                  and
                                  a.modify_dat<trf.modify_dat
                                  and
                                  a.doctorgroupid_chr=?
                                  and
                                  a.registerid_chr=trf.registerid_chr
                                  and
                                  a.targetareaid_chr=trf.sourceareaid_chr
                                  )
                                  ) AS outnum,
               
               --出院
               (SELECT COUNT (DISTINCT a.registerid_chr)
                  FROM t_opr_bih_leave a,t_opr_bih_transfer b,t_opr_bih_register reg
                 WHERE 
                   a.registerid_chr=b.registerid_chr
                   and a.registerid_chr=reg.registerid_chr
                   and reg.relateregisterid_chr is  null
                   and b.type_int in (6,7)
                   AND a.status_int = 1
                   AND a.outhospital_dat >=?
                   AND a.outhospital_dat <?
                   and b.doctorgroupid_chr=?
                                  ) AS outhospitalnum,
                --出院召回//如果已在院人数已包括了召回的，就去掉已存在的相同的登记号
               (SELECT COUNT (DISTINCT b.registerid_chr)
                  FROM t_opr_bih_transfer b
                 WHERE  b.type_int in (4)
                     AND b.modify_dat >=?
                   AND b.modify_dat <?
                   and b.doctorgroupid_chr=? 
                  ) AS outBacknum,
               
                --出院死亡
               (SELECT COUNT (DISTINCT a.registerid_chr)
                  FROM t_opr_bih_transfer a, t_opr_bih_leave b,t_opr_bih_register reg
                 WHERE  a.registerid_chr=b.registerid_chr
                   and a.registerid_chr=reg.registerid_chr
                   and reg.relateregisterid_chr is  null
                   AND a.type_int in ( 6,7)
                   AND b.status_int =1
                   AND b.type_int = 4
                   AND b.outhospital_dat >=?
                   AND b.outhospital_dat <?
                   and a.doctorgroupid_chr=?
                                  ) AS deadoutnum,
               --24小时死亡
               (SELECT COUNT (DISTINCT a.registerid_chr)
                  FROM t_opr_bih_transfer a,
                       t_opr_bih_leave b,
                       (SELECT   MAX (modify_dat) AS modify_dat,
                                 registerid_chr
                            FROM t_opr_bih_transfer
                           WHERE (type_int = 3 OR type_int = 5)
                             AND doctorgroupid_chr = ?
                        GROUP BY registerid_chr) c
                 WHERE a.registerid_chr = b.registerid_chr
                   AND a.modify_dat = b.modify_dat
                   AND a.registerid_chr = c.registerid_chr
                   AND a.type_int in ( 6,7)
                   AND b.status_int =1
                   AND b.type_int = 4
                   AND (a.modify_dat - c.modify_dat) < 1
                   AND a.modify_dat >=?
                   AND a.modify_dat <?
                   and a.doctorgroupid_chr=?
                                  ) AS deadin24,
                 --医保人数
                 (
                    
                    
  SELECT count(distinct a.registerid_chr) num
         from
         (
          SELECT distinct registerid_chr
         from
         (
         select  a.registerid_chr
           from t_opr_bih_transfer a,
                t_opr_bih_register a2,
                (
                 --最近一次的流转状态
                 select   max (b.transferid_chr) transferid_chr,
                          b.registerid_chr
                     from t_opr_bih_transfer b
                     where
                          b.modify_dat >=?
                      and b.modify_dat <?
                      and b.type_int<>2
                 group by b.registerid_chr) b
          where 
             a.DOCTORGROUPID_CHR =?
            and a.registerid_chr = a2.registerid_chr
            and a.transferid_chr=b.transferid_chr
            and a2.status_int <> -1
            and a2.relateregisterid_chr is  null
            and a.type_int not in (6, 7)
            --清除有在病人登记表中已实院出院病人，但在出院表没有有效记录的垃极数据
            and not exists (
                   select m.registerid_chr
                     from t_opr_bih_register m
                    where m.status_int = 1
                      and m.registerid_chr = b.registerid_chr
                      and m.pstatus_int in (2, 3)
                      and not exists (
                             select v.registerid_chr
                               from t_opr_bih_leave v
                              where 
                              v.modify_dat <sysdate
                                and 
                                v.status_int = 1
                                and v.registerid_chr = b.registerid_chr))
                              --撤消出院
            union all
                  --撤消出院
                  select distinct a.registerid_chr
                  from t_opr_bih_transfer trf,
                  (
                  select max (a.transferid_chr) transferid_chr, a.registerid_chr 
                  from t_opr_bih_transfer a 
                  where 
                  a.modify_dat>=?
                  and
                  a.modify_dat<?
                  and a.type_int<>2
                  group by a.registerid_chr
                  ) a 
                  where
                  trf.doctorgroupid_chr=?
                  and trf.transferid_chr=a.transferid_chr
                  and
                  trf.registerid_chr=a.registerid_chr
                  and
                  trf.type_int in (6,7)
                  and exists
                  (
                      select d1.registerid_chr
                                       from t_opr_bih_leave d1
                                       where d1.status_int=0 
                                       and d1.leaveid_chr=(select max(leaveid_chr) leaveid_chr from t_opr_bih_leave k
                                                              where 
                                                              k.registerid_chr=a.registerid_chr
                                                              and
                                                              k.modify_dat>=?
                                                              and
                                                              k.modify_dat<?)
                  )
                     
                  )

    union all
--当日预出院人数
                      select  a.registerid_chr
           from t_opr_bih_transfer a,
                t_opr_bih_register a2,
                (
                 --最近一次的流转状态
                 select   max (b.transferid_chr) transferid_chr,
                          b.registerid_chr
                     from t_opr_bih_transfer b
                     where
                          b.modify_dat >=?
                      and b.modify_dat <?
                      and b.type_int<>2
                 group by b.registerid_chr) b
          where 
             a.doctorgroupid_chr=?
            and a.registerid_chr = a2.registerid_chr
            and a.transferid_chr=b.transferid_chr
            and a2.status_int <> -1
            and a2.relateregisterid_chr is  null
            and a.type_int=7
            and exists
            (
               SELECT a.registerid_chr
                  FROM t_opr_bih_leave a,t_opr_bih_transfer b,t_opr_bih_register reg
                 WHERE 
                   a.registerid_chr=b.registerid_chr
                   and a.registerid_chr=reg.registerid_chr
                   and reg.relateregisterid_chr is  null
                   and b.type_int=7
                   AND a.outhospital_dat >=?
                   and b.doctorgroupid_chr=?
                   and a.registerid_chr=a2.registerid_chr
            )
                  ) a, t_opr_bih_register b
                   where a.registerid_chr=b.registerid_chr
                   and b.PAYTYPEID_CHR in ([PAYTYPEID_CHR])

                     ) AS ybcount
          FROM t_opr_bih_transfer
         WHERE ROWNUM = 1)
             ";

            try
            {
                string p_strStart = "2007-1-1 00:00:00";
                DateTime m_dtStart;
                DateTime.TryParse(p_strStart, out m_dtStart);
                DateTime m_dtDate;
                DateTime.TryParse(p_strDate, out m_dtDate);
                DateTime m_dtDate1;
                DateTime m_dtDate2;
                m_dtDate1 = m_dtDate.Date;
                m_dtDate2 = m_dtDate.Date.AddHours(24);
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;


                objHRPSvc.CreateDatabaseParameter(62, out arrParams);
                int n = -1;
                //昨日在院人数 8
                arrParams[++n].Value = m_dtStart;
                arrParams[++n].Value = m_dtDate1;
                arrParams[++n].Value = doctorgroupid_chr;
                arrParams[++n].Value = m_dtStart;
                arrParams[++n].Value = m_dtDate1;
                arrParams[++n].Value = doctorgroupid_chr;
                arrParams[++n].Value = m_dtStart;
                arrParams[++n].Value = m_dtDate1;
                /*=====>当日预出院人数<======*/
                arrParams[++n].Value = m_dtStart;
                arrParams[++n].Value = m_dtDate1;
                arrParams[++n].Value = doctorgroupid_chr;
                arrParams[++n].Value = m_dtDate1;
                arrParams[++n].Value = doctorgroupid_chr;
                //今日在院人数 8
                arrParams[++n].Value = m_dtStart;
                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = doctorgroupid_chr;
                arrParams[++n].Value = m_dtStart;
                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = doctorgroupid_chr;
                arrParams[++n].Value = m_dtStart;
                arrParams[++n].Value = m_dtDate2;
                /*=====>当日预出院人数<======*/
                arrParams[++n].Value = m_dtStart;
                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = doctorgroupid_chr;
                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = doctorgroupid_chr;
                //今日入院人数 3
                arrParams[++n].Value = m_dtDate1;
                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = doctorgroupid_chr;
                //转入人数    3
                arrParams[++n].Value = m_dtDate1;
                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = doctorgroupid_chr;

                //转出人数       4 
                arrParams[++n].Value = m_dtDate1;
                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = doctorgroupid_chr;
                arrParams[++n].Value = doctorgroupid_chr;

                //出院 3
                arrParams[++n].Value = m_dtDate1;
                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = doctorgroupid_chr;
                //出院召回 3
                arrParams[++n].Value = m_dtDate1;
                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = doctorgroupid_chr;

                //出院死亡      3
                arrParams[++n].Value = m_dtDate1;
                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = doctorgroupid_chr;

                //24小时死亡 4
                arrParams[++n].Value = doctorgroupid_chr;
                arrParams[++n].Value = m_dtDate1;
                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = doctorgroupid_chr;

                //医保人数 8
                arrParams[++n].Value = m_dtStart;
                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = doctorgroupid_chr;
                arrParams[++n].Value = m_dtStart;
                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = doctorgroupid_chr;
                arrParams[++n].Value = m_dtStart;
                arrParams[++n].Value = m_dtDate2;
                /*=====>当日预出院人数<======*/
                arrParams[++n].Value = m_dtStart;
                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = doctorgroupid_chr;
                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = doctorgroupid_chr;
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
                strSQL = strSQL.Replace("[PAYTYPEID_CHR]", PAYTYPEID_CHR);
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);


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

        #region 专业组工作日志2-入院表

        [AutoComplete]
        public long GetBeinhospitalgroup(string doctorgroupid_chr, string p_strDate, out DataTable p_dtResult)
        {
            long lngRes = 0;

            p_dtResult = new DataTable();
            string strSQL = @" select c.inpatientid_chr as inpatientid, d.lastname_vchr as lastname,
       d.sex_chr as sex, e.code_chr, c.mzdiagnose_vchr as mzdiagnose_vchr,
       c.registerid_chr, f.groupname_vchr
  from t_opr_bih_transfer a,
       t_opr_bih_register c,
       t_opr_bih_registerdetail d,
       t_bse_bed e,
       t_bse_groupdesc f
 where a.registerid_chr = c.registerid_chr
   and c.registerid_chr = d.registerid_chr
   and a.targetbedid_chr = e.bedid_chr(+)
   and a.doctorgroupid_chr = f.groupid_chr
   and c.relateregisterid_chr is null
   and c.bedid_chr is not null
   and a.type_int in (4, 5)
   and c.status_int = 1
   and a.doctorgroupid_chr = ?
   and a.modify_dat =
          (select min (b.modify_dat)
             from t_opr_bih_transfer b
            where a.registerid_chr = b.registerid_chr
              and b.type_int = 5
              and b.modify_dat between ?
                                   and ?) ";


            try
            {
                string p_strStart = "2007-1-1 00:00:00";
                DateTime m_dtStart;
                DateTime.TryParse(p_strStart, out m_dtStart);
                DateTime m_dtDate;
                DateTime.TryParse(p_strDate, out m_dtDate);
                DateTime m_dtDate1;
                DateTime m_dtDate2;
                m_dtDate1 = m_dtDate.Date;
                m_dtDate2 = DateTime.Parse(m_dtDate.Date.ToString("yyyy-MM-dd") + " 23:59:59");
                //m_dtDate2 = m_dtDate.Date.AddHours(24);
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;


                objHRPSvc.CreateDatabaseParameter(3, out arrParams);
                int n = -1;
                //今日在院人数
                arrParams[++n].Value = doctorgroupid_chr;
                arrParams[++n].Value = m_dtDate1;
                arrParams[n].DbType = DbType.DateTime;
                arrParams[++n].Value = m_dtDate2;
                arrParams[n].DbType = DbType.DateTime;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);


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

        #region 专业组工作日志3-转入表

        [AutoComplete]
        public long GetTranfergroupin(string doctorgroupid_chr, string p_strDate, out DataTable p_dtResult)
        {
            long lngRes = 0;

            p_dtResult = new DataTable();
            string strSQL = @"select   b.inpatientid_chr as inpatientid, c.lastname_vchr as lastname,
                                       c.sex_chr as sex, b.registerid_chr, g.groupname_vchr,
                                       f.code_chr as s_sourcebedid_chr, d.code_chr as s_targetbedid_chr,
                                       '' s_sourceareaid_chr, e1.deptname_vchr as s_targetareaid_chr,
                                       e.deptname_vchr, d.code_chr as targetbedno
                                  from t_opr_bih_transfer a,
                                       t_opr_bih_register b,
                                       t_opr_bih_registerdetail c,
                                       t_bse_bed d,
                                       t_bse_deptdesc e,
                                       t_bse_deptdesc e1,
                                       t_bse_bed f,
                                       t_bse_groupdesc g
                                 where a.registerid_chr = b.registerid_chr
                                   and b.registerid_chr = c.registerid_chr
                                   and a.targetbedid_chr = d.bedid_chr(+)
                                   and a.sourceareaid_chr = e.deptid_chr(+)
                                   and a.targetareaid_chr = e1.deptid_chr(+)
                                   and a.sourcebedid_chr = f.bedid_chr(+)
                                   and a.doctorgroupid_chr = g.groupid_chr
                                   and a.sourceareaid_chr <> a.targetareaid_chr
                                   and b.relateregisterid_chr is null
                                   and b.bedid_chr is not null
                                   and b.status_int = 1
                                   and a.type_int = 3
                                   and a.doctorgroupid_chr = ?
                                   and (a.modify_dat between ? and ?) ";

            try
            {
                string p_strStart = "2007-1-1 00:00:00";
                DateTime m_dtStart;
                DateTime.TryParse(p_strStart, out m_dtStart);
                DateTime m_dtDate;
                DateTime.TryParse(p_strDate, out m_dtDate);
                DateTime m_dtDate1;
                DateTime m_dtDate2;
                m_dtDate1 = m_dtDate.Date;
                m_dtDate2 = DateTime.Parse(m_dtDate.Date.ToString("yyyy-MM-dd") + " 23:59:59");
                //m_dtDate2 = m_dtDate.Date.AddHours(24);
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;


                objHRPSvc.CreateDatabaseParameter(3, out arrParams);
                int n = -1;
                //今日在院人数
                arrParams[++n].Value = doctorgroupid_chr;
                arrParams[++n].Value = m_dtDate1;
                arrParams[n].DbType = DbType.DateTime;
                arrParams[++n].Value = m_dtDate2;
                arrParams[n].DbType = DbType.DateTime;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);


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

        #region 专业组工作日志4-转出表

        [AutoComplete]
        public long GetTranfergroupout(string doctorgroupid_chr, string p_strDate, out DataTable p_dtResult)
        {
            long lngRes = 0;

            p_dtResult = new DataTable();
            string strSQL = @"select distinct reg.registerid_chr, reg.inpatientid_chr as inpatientid,
                pat.lastname_vchr as lastname, pat.sex_chr as sex,
                bed.code_chr, gro.groupname_vchr,
                s_bed.code_chr s_sourcebedid_chr, '' s_targetbedid_chr,
                '' s_sourceareaid_chr, '' s_targetareaid_chr,
                (select groupname_vchr
                   from t_opr_bih_transfer a,
                        t_bse_groupdesc b
                  where a.doctorgroupid_chr = b.groupid_chr(+)
                    and a.type_int in (3, 5)
                    and a.modify_dat < trf.modify_dat
                    and a.registerid_chr = trf.registerid_chr
                    and a.targetareaid_chr = trf.sourceareaid_chr
                    and rownum = 1) s_groupname_vchr
           from t_bse_bed bed,
                t_opr_bih_transfer trf,
                t_opr_bih_register reg,
                t_opr_bih_registerdetail pat,
                t_bse_groupdesc gro,
                t_bse_bed s_bed
          where trf.registerid_chr = reg.registerid_chr
            and reg.registerid_chr = pat.registerid_chr
            and trf.targetbedid_chr = bed.bedid_chr(+)
            and trf.doctorgroupid_chr = gro.groupid_chr(+)
            and trf.sourcebedid_chr = s_bed.bedid_chr(+)
            and exists (
                   select s.transferid_chr
                     from t_opr_bih_transfer s, t_opr_bih_transfer t
                    where s.registerid_chr = t.registerid_chr
                      and s.targetareaid_chr = t.sourceareaid_chr
                      and trf.transferid_chr = s.transferid_chr
                      and s.doctorgroupid_chr = ?
                      and t.doctorgroupid_chr <> ?
                      and t.modify_dat > s.modify_dat
                      and s.type_int in (3, 5)
                      and t.type_int = 3
                      and (t.modify_dat between ? and ?))";

            try
            {
                string p_strStart = "2007-1-1 00:00:00";
                DateTime m_dtStart;
                DateTime.TryParse(p_strStart, out m_dtStart);
                DateTime m_dtDate;
                DateTime.TryParse(p_strDate, out m_dtDate);
                DateTime m_dtDate1;
                DateTime m_dtDate2;
                m_dtDate1 = m_dtDate.Date;
                m_dtDate2 = DateTime.Parse(m_dtDate.Date.ToString("yyyy-MM-dd") + " 23:59:59");
                //m_dtDate2 = m_dtDate.Date.AddHours(24);
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;


                objHRPSvc.CreateDatabaseParameter(4, out arrParams);
                int n = -1;
                //今日在院人数

                arrParams[++n].Value = doctorgroupid_chr;
                arrParams[++n].Value = doctorgroupid_chr;
                arrParams[++n].Value = m_dtDate1;
                arrParams[n].DbType = DbType.DateTime;
                arrParams[++n].Value = m_dtDate2;
                arrParams[n].DbType = DbType.DateTime;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);


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

        #region 专业组工作日志5-出院表

        [AutoComplete]
        public long GetBeouthospitalgroup(string doctorgroupid_chr, string p_strDate, out DataTable p_dtResult)
        {
            long lngRes = 0;

            p_dtResult = new DataTable();
            string strSQL = @"select b.inpatientid_chr as inpatientid,
                                       c.lastname_vchr as lastname,
                                       c.sex_chr as sex,
                                       d.code_chr as sourcebedno,
                                       g.diagnose_vchr,
                                       f.groupname_vchr as groupname_vchr,
                                       decode(g.type_int, 1, '治愈出院', 2, '转院', 3, '其它', 4, '死亡') type_vchr
                                  from (select max(e.modify_dat) as maxdate, e.registerid_chr
                                          from t_opr_bih_leave a, t_opr_bih_register b, t_opr_bih_transfer e
                                         where a.registerid_chr = b.registerid_chr
                                               and a.registerid_chr = e.registerid_chr
                                               and b.relateregisterid_chr is null
                                               and e.type_int in (6, 7)
                                               and a.status_int = 1
                                               and b.status_int = 1
                                               and e.doctorgroupid_chr = ?
                                               and (a.outhospital_dat between ? and ?)
                                         group by e.registerid_chr) ta,
                                       t_opr_bih_register b,
                                       t_opr_bih_registerdetail c,
                                       t_bse_bed d,
                                       t_opr_bih_transfer e,
                                       t_bse_groupdesc f,
                                       t_opr_bih_leave g
                                 where ta.registerid_chr = b.registerid_chr
                                       and ta.registerid_chr = c.registerid_chr
                                       and ta.registerid_chr = g.registerid_chr
                                       and ta.registerid_chr = e.registerid_chr
                                       and ta.maxdate = e.modify_dat
                                       and e.doctorgroupid_chr = f.groupid_chr
                                       and e.sourcebedid_chr = d.bedid_chr
                                       and g.status_int = 1 ";

            try
            {
                string p_strStart = "2007-1-1 00:00:00";
                DateTime m_dtStart;
                DateTime.TryParse(p_strStart, out m_dtStart);
                DateTime m_dtDate;
                DateTime.TryParse(p_strDate, out m_dtDate);
                DateTime m_dtDate1;
                DateTime m_dtDate2;
                m_dtDate1 = m_dtDate.Date;
                m_dtDate2 = DateTime.Parse(m_dtDate.Date.ToString("yyyy-MM-dd") + " 23:59:59");
                //m_dtDate2 = m_dtDate.Date.AddHours(24);
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;


                objHRPSvc.CreateDatabaseParameter(3, out arrParams);
                int n = -1;
                //今日在院人数
                arrParams[++n].Value = doctorgroupid_chr;
                arrParams[++n].Value = m_dtDate1;
                arrParams[n].DbType = DbType.DateTime;
                arrParams[++n].Value = m_dtDate2;
                arrParams[n].DbType = DbType.DateTime;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);

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




        #region 病区工作日志1-病区

        [AutoComplete]
        public long GetTranferDept(string m_strAREAID_CHR, string PAYTYPEID_CHR, string p_strDate, out DataTable p_dtResult)
        {
            long lngRes = 0;

            p_dtResult = new DataTable();

            string strSQL = @"
            SELECT yesterdaycount,
        inhospitalnum+outBacknum AS inhospitalcount,
       innum AS incount, outnum AS outcount,
       outhospitalnum AS outhospitalcount, deadoutnum AS deadcount,
       deadin24 AS deadcountin24,totalnum AS todaycount, ybcount,yesterdaycount+inhospitalnum+outBacknum+innum-outnum- outhospitalnum-totalnum as precount
  FROM (SELECT 

   --昨日在院人数
               (

         SELECT count(distinct registerid_chr) num
         from
         (
         select  a.registerid_chr
           from t_opr_bih_transfer a,
                t_opr_bih_register a2,
                (
                 --最近一次的流转状态
                 select   max (b.transferid_chr) transferid_chr,
                          b.registerid_chr
                     from t_opr_bih_transfer b
                     where
                          b.modify_dat >=?
                      and b.modify_dat <?
                       and b.type_int<>2
                 group by b.registerid_chr) b
          where 
             a.targetareaid_chr = ?
            and a.registerid_chr = a2.registerid_chr
            and a.transferid_chr=b.transferid_chr
            and a2.status_int <> -1
            and a2.relateregisterid_chr is  null
            and a.type_int not in (6, 7)
            --清除有在病人登记表中已实院出院病人，但在出院表没有有效记录的垃极数据
            and not exists (
                   select m.registerid_chr
                     from t_opr_bih_register m
                    where m.status_int = 1
                      and m.registerid_chr = b.registerid_chr
                      and m.pstatus_int in (2, 3)
                      and not exists (
                             select v.registerid_chr
                               from t_opr_bih_leave v
                              where 
                              v.modify_dat <sysdate
                                and 
                                v.status_int = 1
                                and v.registerid_chr = b.registerid_chr))
                              --撤消出院
                    union all
                    
                  
                    
                    select d1.registerid_chr
                       from t_opr_bih_leave d1,
                            (select   max (v.leaveid_chr) leaveid_chr, registerid_chr
                                 from t_opr_bih_leave v
                                 where v.outareaid_chr=?
                                 and v.modify_dat <?
                                group by v.registerid_chr
                               ) d2
                      where d1.leaveid_chr = d2.leaveid_chr and d1.status_int = 0
                            and d1.outareaid_chr=?
                            and exists
                            (
                            select registerid_chr from t_opr_bih_transfer w where w.registerid_chr=d1.registerid_chr 
                             and w.transferid_chr=(select max(transferid_chr) from t_opr_bih_transfer where registerid_chr=w.registerid_chr 
                             and modify_dat >=?
                             and modify_dat <?
                             and w.TARGETAREAID_CHR=d1.outareaid_chr
                             
                              )
                             
                            )


                  union all
--当日预出院人数
                      select  a.registerid_chr
           from t_opr_bih_transfer a,
                t_opr_bih_register a2,
                (
                 --最近一次的流转状态
                 select   max (b.transferid_chr) transferid_chr,
                          b.registerid_chr
                     from t_opr_bih_transfer b
                     where
                          b.modify_dat >=?
                      and b.modify_dat <?
                      and b.type_int<>2
                 group by b.registerid_chr) b
          where 
             a.targetareaid_chr = ?
            and a.registerid_chr = a2.registerid_chr
            and a.transferid_chr=b.transferid_chr
            and a2.status_int <> -1
            and a2.relateregisterid_chr is  null
            and a.type_int=7
            and exists
            (
               SELECT a.registerid_chr
                  FROM t_opr_bih_leave a,t_opr_bih_transfer b,t_opr_bih_register reg
                 WHERE 
                   a.registerid_chr=b.registerid_chr
                   and a.registerid_chr=reg.registerid_chr
                   and reg.relateregisterid_chr is  null
                   and b.type_int=7
                   AND a.outhospital_dat >=?
                   and a.outareaid_chr=?
                   and a.registerid_chr=a2.registerid_chr
            )

                         )
                    ) AS yesterdaycount,



               --今日在院人数
               (

         SELECT count(distinct registerid_chr) num
         from
         (
         select  a.registerid_chr
           from t_opr_bih_transfer a,
                t_opr_bih_register a2,
                (
                 --最近一次的流转状态
                 select   max (b.transferid_chr) transferid_chr,
                          b.registerid_chr
                     from t_opr_bih_transfer b
                     where
                          b.modify_dat >=?
                      and b.modify_dat <?
                      and b.type_int<>2
                 group by b.registerid_chr) b
          where 
             a.targetareaid_chr = ?
            and a.registerid_chr = a2.registerid_chr
            and a.transferid_chr=b.transferid_chr
            and a2.status_int <> -1
            and a2.relateregisterid_chr is  null
            and a.type_int not in (6, 7)
            --清除有在病人登记表中已实院出院病人，但在出院表没有有效记录的垃极数据
            and not exists (
                   select m.registerid_chr
                     from t_opr_bih_register m
                    where m.status_int = 1
                      and m.registerid_chr = b.registerid_chr
                      and m.pstatus_int in (2, 3)
                      and not exists (
                             select v.registerid_chr
                               from t_opr_bih_leave v
                              where 
                              v.modify_dat <sysdate
                                and 
                                v.status_int = 1
                                and v.registerid_chr = b.registerid_chr))
                              --撤消出院
                    union all
                    
                  
                    
                    select d1.registerid_chr
                       from t_opr_bih_leave d1,
                            (select   max (v.leaveid_chr) leaveid_chr, registerid_chr
                                 from t_opr_bih_leave v
                                 where v.outareaid_chr=?
                                 and v.modify_dat <?
                                group by v.registerid_chr
                               ) d2
                      where d1.leaveid_chr = d2.leaveid_chr and d1.status_int = 0
                            and d1.outareaid_chr=?
                            and exists
                            (
                            select registerid_chr from t_opr_bih_transfer w where w.registerid_chr=d1.registerid_chr 
                             and w.transferid_chr=(select max(transferid_chr) from t_opr_bih_transfer where registerid_chr=w.registerid_chr 
                             and modify_dat >=?
                             and modify_dat <?
                             and w.TARGETAREAID_CHR=d1.outareaid_chr
                             
                              )
                             
                            )
                         union all
--当日预出院人数
                      select  a.registerid_chr
           from t_opr_bih_transfer a,
                t_opr_bih_register a2,
                (
                 --最近一次的流转状态
                 select   max (b.transferid_chr) transferid_chr,
                          b.registerid_chr
                     from t_opr_bih_transfer b
                     where
                          b.modify_dat >=?
                      and b.modify_dat <?
                      and b.type_int<>2
                 group by b.registerid_chr) b
          where 
             a.targetareaid_chr = ?
            and a.registerid_chr = a2.registerid_chr
            and a.transferid_chr=b.transferid_chr
            and a2.status_int <> -1
            and a2.relateregisterid_chr is  null
            and a.type_int=7
            and exists
            (
               SELECT a.registerid_chr
                  FROM t_opr_bih_leave a,t_opr_bih_transfer b,t_opr_bih_register reg
                 WHERE 
                   a.registerid_chr=b.registerid_chr
                   and a.registerid_chr=reg.registerid_chr
                   and reg.relateregisterid_chr is  null
                   and b.type_int=7
                   AND a.outhospital_dat >=?
                   and a.outareaid_chr=?
                   and a.registerid_chr=a2.registerid_chr
            )



                         )
                    ) AS totalnum,
               
               --今日入院人数
               (SELECT COUNT (DISTINCT tr.registerid_chr)
                  FROM t_opr_bih_transfer tr, t_opr_bih_register reg
                 WHERE tr.registerid_chr = reg.registerid_chr
                   AND tr.type_int = 5
                   AND reg.status_int = 1 
                   AND reg.relateregisterid_chr is  null
                   AND tr.modify_dat >=?
                   AND tr.modify_dat <?
                   and tr.targetareaid_chr=?
                   ) AS inhospitalnum,
               
               --转入人数
               (SELECT COUNT (registerid_chr)
                  FROM t_opr_bih_transfer
                 WHERE type_int = 3
				    AND modify_dat >=?
                   AND modify_dat <?
                   and targetareaid_chr=?
                   ) AS innum,
               
               -- 转出人数
               (SELECT COUNT (registerid_chr)
                  FROM t_opr_bih_transfer trf
                 WHERE type_int = 3
                   AND modify_dat >=?
                   AND modify_dat <?
                   and targetareaid_chr<>?
                    and
                                exists
                                (
                                  select a.transferid_chr from t_opr_bih_transfer a where a.type_int in (3,5)
                                  and
                                  a.modify_dat<trf.modify_dat
                                  and
                                  a.targetareaid_chr=?
                                  and
                                  a.registerid_chr=trf.registerid_chr
                                  and
                                  a.targetareaid_chr=trf.sourceareaid_chr
                                  )
                                  ) AS outnum,
              
               
               --出院
               (SELECT COUNT (DISTINCT a.registerid_chr)
                  FROM t_opr_bih_leave a,t_opr_bih_transfer b,t_opr_bih_register reg
                 WHERE 
                   a.registerid_chr=b.registerid_chr
                   and a.registerid_chr=reg.registerid_chr
                   and reg.relateregisterid_chr is  null
                   and b.type_int in (6,7)
                   AND a.status_int = 1
                   AND a.outhospital_dat >=?
                   AND a.outhospital_dat <?
                   and b.targetareaid_chr=?
                                  ) AS outhospitalnum,
                --出院召回//如果已在院人数已包括了召回的，就去掉已存在的相同的登记号
               (SELECT COUNT (DISTINCT b.registerid_chr)
                  FROM t_opr_bih_transfer b
                 WHERE  b.type_int in (4)
                     AND b.modify_dat >=?
                   AND b.modify_dat <?
                   and b.targetareaid_chr=? 
                  ) AS outBacknum,
               --出院死亡
               (SELECT COUNT (DISTINCT a.registerid_chr)
                  FROM t_opr_bih_transfer a, t_opr_bih_leave b,t_opr_bih_register reg
                 WHERE  a.registerid_chr=b.registerid_chr
                   and a.registerid_chr=reg.registerid_chr
                   and reg.relateregisterid_chr is  null
                   AND a.type_int in ( 6,7)
                   AND b.status_int =1
                   AND b.type_int = 4
                   AND b.outhospital_dat >=?
                   AND b.outhospital_dat <?
                   and a.targetareaid_chr=?
                                  ) AS deadoutnum,
               
               --24小时死亡
               (SELECT COUNT (DISTINCT a.registerid_chr)
                  FROM t_opr_bih_transfer a,
                       t_opr_bih_leave b,
                       (SELECT   MAX (modify_dat) AS modify_dat,
                                 registerid_chr
                            FROM t_opr_bih_transfer
                           WHERE (type_int = 3 OR type_int = 5)
                             AND targetareaid_chr = ?
                        GROUP BY registerid_chr) c
                 WHERE a.registerid_chr = b.registerid_chr
                   AND a.modify_dat = b.modify_dat
                   AND a.registerid_chr = c.registerid_chr
                   AND a.type_int in ( 6,7)
                   AND b.status_int =1
                   AND b.type_int = 4
                   AND (a.modify_dat - c.modify_dat) < 1
                   AND a.modify_dat >=?
                   AND a.modify_dat <?
                   and a.targetareaid_chr=?
                                  ) AS deadin24,
                 --医保人数
                 (
        SELECT count(distinct a.registerid_chr) num
         from
         (
         select  a.registerid_chr
           from t_opr_bih_transfer a,
                t_opr_bih_register a2,
                (
                 --最近一次的流转状态
                 select   max (b.transferid_chr) transferid_chr,
                          b.registerid_chr
                     from t_opr_bih_transfer b
                     where
                          b.modify_dat >=?
                      and b.modify_dat <?
                      and b.type_int<>2
                 group by b.registerid_chr) b
          where 
             a.targetareaid_chr = ?
            and a.registerid_chr = a2.registerid_chr
            and a.transferid_chr=b.transferid_chr
            and a2.status_int <> -1
            and a2.relateregisterid_chr is  null
            and a.type_int not in (6, 7)
            --清除有在病人登记表中已实院出院病人，但在出院表没有有效记录的垃极数据
            and not exists (
                   select m.registerid_chr
                     from t_opr_bih_register m
                    where m.status_int = 1
                      and m.registerid_chr = b.registerid_chr
                      and m.pstatus_int in (2, 3)
                      and not exists (
                             select v.registerid_chr
                               from t_opr_bih_leave v
                              where 
                              v.modify_dat <sysdate
                                and 
                                v.status_int = 1
                                and v.registerid_chr = b.registerid_chr))
                              --撤消出院
                    union all
                    
                  
                    
                    select d1.registerid_chr
                       from t_opr_bih_leave d1,
                            (select   max (v.leaveid_chr) leaveid_chr, registerid_chr
                                 from t_opr_bih_leave v
                                 where v.outareaid_chr=?
                                 and v.modify_dat <?
                                group by v.registerid_chr
                               ) d2
                      where d1.leaveid_chr = d2.leaveid_chr and d1.status_int = 0
                            and d1.outareaid_chr=?
                            and exists
                            (
                            select registerid_chr from t_opr_bih_transfer w where w.registerid_chr=d1.registerid_chr 
                             and w.transferid_chr=(select max(transferid_chr) from t_opr_bih_transfer where registerid_chr=w.registerid_chr 
                             and modify_dat >=?
                             and modify_dat <?
                             and w.TARGETAREAID_CHR=d1.outareaid_chr
                             
                              )
                             
                            )


                         union all
                   --当日预出院人数
                      select  a.registerid_chr
           from t_opr_bih_transfer a,
                t_opr_bih_register a2,
                (
                 --最近一次的流转状态
                 select   max (b.transferid_chr) transferid_chr,
                          b.registerid_chr
                     from t_opr_bih_transfer b
                     where
                          b.modify_dat >=?
                      and b.modify_dat <?
                      and b.type_int<>2
                 group by b.registerid_chr) b
          where 
             a.targetareaid_chr = ?
            and a.registerid_chr = a2.registerid_chr
            and a.transferid_chr=b.transferid_chr
            and a2.status_int <> -1
            and a2.relateregisterid_chr is  null
            and a.type_int=7
            and exists
            (
               SELECT a.registerid_chr
                  FROM t_opr_bih_leave a,t_opr_bih_transfer b,t_opr_bih_register reg
                 WHERE 
                   a.registerid_chr=b.registerid_chr
                   and a.registerid_chr=reg.registerid_chr
                   and reg.relateregisterid_chr is  null
                   and b.type_int=7
                   AND a.outhospital_dat >=?
                   and a.outareaid_chr=?
                   and a.registerid_chr=a2.registerid_chr
            )


                      
                         ) a,t_opr_bih_register b
                   where a.registerid_chr=b.registerid_chr
                   and b.PAYTYPEID_CHR in ([PAYTYPEID_CHR])
                    ) AS ybcount
          FROM t_opr_bih_transfer
         WHERE ROWNUM = 1)
             ";

            try
            {
                string p_strStart = "2007-1-1 00:00:00";
                DateTime m_dtStart;
                DateTime.TryParse(p_strStart, out m_dtStart);
                DateTime m_dtDate;
                DateTime.TryParse(p_strDate, out m_dtDate);
                DateTime m_dtDate1;
                DateTime m_dtDate2;
                m_dtDate1 = m_dtDate.Date;
                m_dtDate2 = m_dtDate.Date.AddHours(24);
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;


                objHRPSvc.CreateDatabaseParameter(62, out arrParams);
                int n = -1;
                //昨日在院人数 8
                arrParams[++n].Value = m_dtStart;
                arrParams[++n].Value = m_dtDate1;
                arrParams[++n].Value = m_strAREAID_CHR;


                arrParams[++n].Value = m_strAREAID_CHR;
                arrParams[++n].Value = m_dtDate1;
                arrParams[++n].Value = m_strAREAID_CHR;
                arrParams[++n].Value = m_dtStart;
                arrParams[++n].Value = m_dtDate1;

                /*=====>当日预出院人数<======*/
                arrParams[++n].Value = m_dtStart;
                arrParams[++n].Value = m_dtDate1;
                arrParams[++n].Value = m_strAREAID_CHR;
                arrParams[++n].Value = m_dtDate1;
                arrParams[++n].Value = m_strAREAID_CHR;

                //今日在院人数 8
                arrParams[++n].Value = m_dtStart;
                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = m_strAREAID_CHR;


                arrParams[++n].Value = m_strAREAID_CHR;
                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = m_strAREAID_CHR;
                arrParams[++n].Value = m_dtStart;
                arrParams[++n].Value = m_dtDate2;

                /*=====>当日预出院人数<======*/
                arrParams[++n].Value = m_dtStart;
                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = m_strAREAID_CHR;
                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = m_strAREAID_CHR;

                //今日入院人数 3
                arrParams[++n].Value = m_dtDate1;
                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = m_strAREAID_CHR;
                //转入人数    3
                arrParams[++n].Value = m_dtDate1;
                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = m_strAREAID_CHR;

                //转出人数       4 
                arrParams[++n].Value = m_dtDate1;
                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = m_strAREAID_CHR;
                arrParams[++n].Value = m_strAREAID_CHR;

                //出院 3
                arrParams[++n].Value = m_dtDate1;
                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = m_strAREAID_CHR;
                // 出院召回
                arrParams[++n].Value = m_dtDate1;
                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = m_strAREAID_CHR;

                //arrParams[++n].Value = m_strAREAID_CHR;
                //arrParams[++n].Value = m_dtDate1;
                //arrParams[++n].Value = m_strAREAID_CHR;
                //arrParams[++n].Value = m_dtStart;
                //arrParams[++n].Value = m_dtDate1;

                //出院死亡      3
                arrParams[++n].Value = m_dtDate1;
                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = m_strAREAID_CHR;

                //24小时死亡 4
                arrParams[++n].Value = m_strAREAID_CHR;
                arrParams[++n].Value = m_dtDate1;
                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = m_strAREAID_CHR;

                //医保人数 8
                arrParams[++n].Value = m_dtStart;
                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = m_strAREAID_CHR;


                arrParams[++n].Value = m_strAREAID_CHR;
                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = m_strAREAID_CHR;
                arrParams[++n].Value = m_dtStart;
                arrParams[++n].Value = m_dtDate2;

                /*=====>当日预出院人数<======*/
                arrParams[++n].Value = m_dtStart;
                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = m_strAREAID_CHR;
                arrParams[++n].Value = m_dtDate2;
                arrParams[++n].Value = m_strAREAID_CHR;

                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
                strSQL = strSQL.Replace("[PAYTYPEID_CHR]", PAYTYPEID_CHR);
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);


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

        #region 病区工作日志2-入院表

        [AutoComplete]
        public long GetBeinhospitalDept(string m_strAREAID_CHR, string p_strDate, out DataTable p_dtResult)
        {
            long lngRes = 0;

            p_dtResult = new DataTable();
            string strSQL = @"select distinct reg.registerid_chr, reg.inpatientid_chr as inpatientid,
                                    pat.lastname_vchr as lastname, pat.sex_chr as sex,
                                    bed.code_chr, reg.mzdiagnose_vchr diagnose_vchr,
                                    dep.deptname_vchr
                               from t_bse_bed bed,
                                    t_opr_bih_transfer trf,
                                    t_opr_bih_register reg,
                                    t_opr_bih_registerdetail pat,
                                    t_bse_deptdesc dep
                              where trf.registerid_chr = reg.registerid_chr
                                and reg.registerid_chr = pat.registerid_chr
                                and trf.targetbedid_chr = bed.bedid_chr(+)
                                and trf.targetareaid_chr = dep.deptid_chr(+)
                                and reg.status_int = 1
                                and reg.relateregisterid_chr is null
                                and (trf.type_int = 4 or trf.type_int = 5)
                                and (trf.modify_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                                and trf.targetareaid_chr = ?";

            try
            {
                string p_strStart = "2007-01-01 00:00:00";
                DateTime m_dtStart;
                DateTime.TryParse(p_strStart, out m_dtStart);
                DateTime m_dtDate;
                DateTime.TryParse(p_strDate, out m_dtDate);
                DateTime m_dtDate1;
                DateTime m_dtDate2;
                m_dtDate1 = m_dtDate.Date;
                m_dtDate2 = m_dtDate.Date.AddHours(24);
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;


                objHRPSvc.CreateDatabaseParameter(3, out arrParams);
                int n = -1;
                //今日在院人数
                arrParams[++n].Value = m_dtDate1.ToString("yyyy-MM-dd") + " 00:00:00";
                arrParams[++n].Value = m_dtDate2.ToString("yyyy-MM-dd") + " 23:59:59";
                arrParams[++n].Value = m_strAREAID_CHR;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);
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

        #region 病区工作日志3-转入表

        [AutoComplete]
        public long GetTranferDeptin(string m_strAREAID_CHR, string p_strDate, out DataTable p_dtResult)
        {
            long lngRes = 0;

            p_dtResult = new DataTable();
            string strSQL = @"select distinct reg.registerid_chr, reg.inpatientid_chr as inpatientid,
                                            pat.lastname_vchr as lastname, pat.sex_chr as sex,
                                            bed.code_chr, dep.deptname_vchr,
                                            s_bed.code_chr s_sourcebedid_chr, '' s_targetbedid_chr,
                                            dep2.deptname_vchr s_sourceareaid_chr, '' s_targetareaid_chr,
                                            '' s_groupname_vchr
                                       from t_bse_bed bed,
                                            t_opr_bih_transfer trf,
                                            t_opr_bih_register reg,
                                            t_opr_bih_registerdetail pat,
                                            t_bse_deptdesc dep,
                                            t_bse_deptdesc dep2,
                                            t_bse_bed s_bed
                                      where trf.registerid_chr = reg.registerid_chr
                                        and reg.registerid_chr = pat.registerid_chr
                                        and trf.targetbedid_chr = bed.bedid_chr(+)
                                        and trf.targetareaid_chr = dep.deptid_chr(+)
                                        and trf.sourcebedid_chr = s_bed.bedid_chr(+)
                                        and trf.sourceareaid_chr = dep2.deptid_chr(+)
                                        and trf.type_int = 3
                                        and trf.sourceareaid_chr <> trf.targetareaid_chr
                                        and (trf.modify_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                                        and trf.targetareaid_chr = ? ";

            try
            {
                string p_strStart = "2007-01-01 00:00:00";
                DateTime m_dtStart;
                DateTime.TryParse(p_strStart, out m_dtStart);
                DateTime m_dtDate;
                DateTime.TryParse(p_strDate, out m_dtDate);
                DateTime m_dtDate1;
                DateTime m_dtDate2;
                m_dtDate1 = m_dtDate.Date;
                m_dtDate2 = m_dtDate.Date.AddHours(24);
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;


                objHRPSvc.CreateDatabaseParameter(3, out arrParams);
                int n = -1;
                //今日在院人数                
                arrParams[++n].Value = m_dtDate1.ToString("yyyy-MM-dd") + " 00:00:00";
                arrParams[++n].Value = m_dtDate2.ToString("yyyy-MM-dd") + " 23:59:59";
                arrParams[++n].Value = m_strAREAID_CHR;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);


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

        #region 病区工作日志4-转出表

        [AutoComplete]
        public long GetTranferDeptout(string m_strAREAID_CHR, string p_strDate, out DataTable p_dtResult)
        {
            long lngRes = 0;

            p_dtResult = new DataTable();
            string strSQL = @"select distinct reg.registerid_chr, reg.inpatientid_chr as inpatientid,
                                            pat.lastname_vchr as lastname, pat.sex_chr as sex,
                                            bed.code_chr, dep.deptname_vchr,
                                            s_bed.code_chr s_sourcebedid_chr, '' s_targetbedid_chr,
                                            dep2.deptname_vchr s_sourceareaid_chr, '' s_targetareaid_chr,
                                            '' s_groupname_vchr
                                       from t_bse_bed bed,
                                            t_opr_bih_transfer trf,
                                            t_opr_bih_register reg,
                                            t_opr_bih_registerdetail pat,
                                            t_bse_deptdesc dep,
                                            t_bse_deptdesc dep2,
                                            t_bse_bed s_bed
                                      where trf.registerid_chr = reg.registerid_chr
                                        and reg.registerid_chr = pat.registerid_chr
                                        and trf.targetbedid_chr = bed.bedid_chr(+)
                                        and trf.targetareaid_chr = dep.deptid_chr(+)
                                        and trf.sourcebedid_chr = s_bed.bedid_chr(+)
                                        and trf.sourceareaid_chr = dep2.deptid_chr(+)
                                        and trf.type_int = 3                                        
                                        and (trf.modify_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                                        and trf.targetareaid_chr <> ?
                                        and exists (
                                               select a.transferid_chr
                                                 from t_opr_bih_transfer a
                                                where (a.type_int = 3 or a.type_int = 5)
                                                  and a.modify_dat < trf.modify_dat
                                                  and a.targetareaid_chr = ?
                                                  and a.registerid_chr = trf.registerid_chr
                                                  and a.targetareaid_chr = trf.sourceareaid_chr) ";

            try
            {
                string p_strStart = "2007-01-01 00:00:00";
                DateTime m_dtStart;
                DateTime.TryParse(p_strStart, out m_dtStart);
                DateTime m_dtDate;
                DateTime.TryParse(p_strDate, out m_dtDate);
                DateTime m_dtDate1;
                DateTime m_dtDate2;
                m_dtDate1 = m_dtDate.Date;
                m_dtDate2 = m_dtDate.Date.AddHours(24);
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;


                objHRPSvc.CreateDatabaseParameter(4, out arrParams);
                int n = -1;
                //今日在院人数
                arrParams[++n].Value = m_dtDate1.ToString("yyyy-MM-dd") + " 00:00:00";
                arrParams[++n].Value = m_dtDate2.ToString("yyyy-MM-dd") + " 23:59:59";
                arrParams[++n].Value = m_strAREAID_CHR;
                arrParams[++n].Value = m_strAREAID_CHR;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);

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

        #region 病区工作日志5-出院表

        [AutoComplete]
        public long GetBeouthospitalDept(string m_strAREAID_CHR, string p_strDate, out DataTable p_dtResult)
        {
            long lngRes = 0;

            p_dtResult = new DataTable();
            string strSQL = @"select distinct reg.registerid_chr, reg.inpatientid_chr as inpatientid,
                                            pat.lastname_vchr as lastname, pat.sex_chr as sex,
                                            bed.code_chr sourcebedno, dep.deptname_vchr,
                                            '' groupname_vchr,                                           
                                            decode (trf.type_int,
                                                    1, '治愈出院',
                                                    2, '转院',
                                                    3, '其它',
                                                    4, '死亡'
                                                   ) type_vchr
                                       from t_opr_bih_leave trf,
                                            t_opr_bih_register reg,
                                            t_opr_bih_registerdetail pat,
                                            t_bse_bed bed,
                                            t_bse_deptdesc dep
                                      where reg.registerid_chr = pat.registerid_chr
                                        and reg.registerid_chr = trf.registerid_chr
                                        and trf.outbedid_chr = bed.bedid_chr(+)
                                        and trf.outareaid_chr = dep.deptid_chr(+)
                                        and trf.status_int = 1                                        
                                        and (trf.modify_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                                        and reg.relateregisterid_chr is null
                                        and reg.status_int = 1
                                        and exists (
                                               (select b.transferid_chr
                                                  from t_opr_bih_transfer b
                                                 where (b.type_int = 6 or b.type_int = 7)
                                                   and b.targetareaid_chr = ?
                                                   and b.registerid_chr = trf.registerid_chr))";

            try
            {
                string p_strStart = "2007-1-1 00:00:00";
                DateTime m_dtStart;
                DateTime.TryParse(p_strStart, out m_dtStart);
                DateTime m_dtDate;
                DateTime.TryParse(p_strDate, out m_dtDate);
                DateTime m_dtDate1;
                DateTime m_dtDate2;
                m_dtDate1 = m_dtDate.Date;
                m_dtDate2 = m_dtDate.Date.AddHours(24);
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;

                objHRPSvc.CreateDatabaseParameter(3, out arrParams);
                int n = -1;
                //今日在院人数
                arrParams[++n].Value = m_dtDate1.ToString("yyyy-MM-dd") + " 00:00:00";
                arrParams[++n].Value = m_dtDate2.ToString("yyyy-MM-dd") + " 23:59:59";
                arrParams[++n].Value = m_strAREAID_CHR;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);

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


        #region 病区工作日志1-病区（数据源）

        [AutoComplete]
        public long GethospitalDeptSource(string m_strAREAID_CHR, string m_strPayType, string p_strDate, out DataTable p_dtTransfer, out DataTable p_dtLeave)
        {
            long lngRes = 0;
            //p_dtResult = new DataTable(); 
            p_dtTransfer = new DataTable();
            p_dtLeave = new DataTable();
            string strSQL = @"select to_date ('[m_dtStart]', 'YYYY-MM-DD HH24:MI:SS') startday,
                                   to_date ('[m_dtDate2]', 'YYYY-MM-DD HH24:MI:SS') tomorrow,
                                   to_date ('[m_dtDate1]', 'YYYY-MM-DD HH24:MI:SS') today,
                                   a.transferid_chr, a.sourcebedid_chr, a.targetbedid_chr, a.type_int,
                                   a.registerid_chr, a.modify_dat, a.sourcedeptid_chr, a.sourceareaid_chr,
                                   a.targetdeptid_chr, a.targetareaid_chr, a.doctorid_chr,
                                   a.doctorgroupid_chr, a2.paytypeid_chr
                              from t_opr_bih_transfer a,
                                   t_opr_bih_register a2,
                                   (select   max (b.transferid_chr) transferid_chr, b.registerid_chr
                                        from t_opr_bih_transfer b
                                       where b.modify_dat >= ? and b.modify_dat < ? and b.type_int <> 2
                                    group by b.registerid_chr) b
                             where a.targetareaid_chr = ?
                               and a.registerid_chr = a2.registerid_chr
                               and a.transferid_chr = b.transferid_chr
                               and a2.status_int <> -1
                               and a2.relateregisterid_chr is null
                               and not exists (
                                      select m.registerid_chr
                                        from t_opr_bih_register m
                                       where m.status_int = 1
                                         and m.registerid_chr = b.registerid_chr
                                         and m.pstatus_int in (2, 3)
                                         and not exists (
                                                select v.registerid_chr
                                                  from t_opr_bih_leave v
                                                 where v.modify_dat < sysdate
                                                   and v.status_int = 1
                                                   and v.registerid_chr = b.registerid_chr))";

            try
            {
                string p_strStart = "2007-1-1 00:00:00";
                DateTime m_dtStart;
                DateTime.TryParse(p_strStart, out m_dtStart);
                DateTime m_dtDate;
                DateTime.TryParse(p_strDate, out m_dtDate);
                DateTime m_dtDate1;
                DateTime m_dtDate2;
                m_dtDate1 = m_dtDate.Date;
                m_dtDate2 = m_dtDate.Date.AddHours(24);
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService

objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams1 = null;


                objHRPSvc.CreateDatabaseParameter(3, out arrParams1);
                int n = -1;
                //今日在院人数
                arrParams1[++n].Value = m_dtStart;
                arrParams1[++n].Value = m_dtDate2;
                arrParams1[++n].Value = m_strAREAID_CHR;
                strSQL = strSQL.Replace("[m_dtStart]", m_dtStart.ToString()).Replace("[m_dtDate2]", m_dtDate2.ToString()).Replace("[m_dtDate1]", m_dtDate1.ToString());
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtTransfer, arrParams1);

                strSQL = @"select to_date ('[m_dtStart]', 'YYYY-MM-DD HH24:MI:SS') startday,
                               to_date ('[m_dtDate2]', 'YYYY-MM-DD HH24:MI:SS') tomorrow,
                               to_date ('[m_dtDate1]', 'YYYY-MM-DD HH24:MI:SS') today,
                               d1.registerid_chr, d1.status_int, d1.modify_dat, d1.pstatus_int,
                               d1.type_int, d1.outareaid_chr, d1.outhospital_dat, a2.paytypeid_chr
                          from t_opr_bih_leave d1,
                               (select   max (v.leaveid_chr) leaveid_chr, registerid_chr
                                    from t_opr_bih_leave v
                                   where v.modify_dat < ?
                                group by v.registerid_chr) d2,
                               t_opr_bih_register a2
                         where d1.leaveid_chr = d2.leaveid_chr
                           and d1.registerid_chr = a2.registerid_chr
                           and d1.outareaid_chr = ?";
                System.Data.IDataParameter[] arrParams2 = null;
                objHRPSvc.CreateDatabaseParameter(2, out arrParams2);
                n = -1;
                //今日在院人数

                arrParams2[++n].Value = m_dtDate2;
                arrParams2[++n].Value = m_strAREAID_CHR;
                strSQL = strSQL.Replace("[m_dtStart]", m_dtStart.ToString()).Replace("[m_dtDate2]", m_dtDate2.ToString()).Replace("[m_dtDate1]", m_dtDate1.ToString());

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtLeave, arrParams2);


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

        [AutoComplete]
        public long GethospitalDeptSource(string m_strAREAID_CHR, string m_strPayType, string p_strDate, out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = new DataTable();
            string strSQL = @"";

            try
            {
                string p_strStart = "2007-01-01 00:00:00";
                DateTime m_dtStart;
                DateTime.TryParse(p_strStart, out m_dtStart);
                DateTime m_dtDate;
                DateTime.TryParse(p_strDate, out m_dtDate);
                DateTime m_dtDate1;
                DateTime m_dtDate2;
                m_dtDate1 = m_dtDate.Date;
                m_dtDate2 = m_dtDate.Date.AddHours(24);
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

                strSQL = @"select 0 yesterdaycount, inhospitalnum + outbacknum as inhospitalcount,
                                   innum as incount, outnum as outcount,
                                   outhospitalnum as outhospitalcount, deadoutnum as deadcount,
                                   deadin24 as deadcountin24, 0 todaycount, 0 ybcount, 0 as precount
                              from (select 
                                           --今日入院人数
                                           (select count (distinct tr.registerid_chr)
                                              from t_opr_bih_transfer tr,
                                                   t_opr_bih_register reg
                                             where tr.registerid_chr = reg.registerid_chr
                                               and tr.type_int = 5
                                               and reg.status_int = 1
                                               and reg.relateregisterid_chr is null
                                               and (tr.modify_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                                               and tr.targetareaid_chr = ?) as inhospitalnum,
                                           
                                           --转入人数
                                           (select count (registerid_chr)
                                              from t_opr_bih_transfer
                                             where type_int = 3                                               
                                               and (modify_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                                               and targetareaid_chr = ?) as innum,
                                           
                                           -- 转出人数
                                           (select count (registerid_chr)
                                              from t_opr_bih_transfer trf
                                             where type_int = 3
                                               and (modify_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                                               and targetareaid_chr <> ?
                                               and exists (
                                                      select a.transferid_chr
                                                        from t_opr_bih_transfer a
                                                       where (a.type_int = 3 or a.type_int = 5)
                                                         and a.modify_dat < trf.modify_dat
                                                         and a.targetareaid_chr = ?
                                                         and a.registerid_chr = trf.registerid_chr
                                                         and a.targetareaid_chr = trf.sourceareaid_chr))
                                                                                                as outnum,
                                           
                                           --出院
                                           (select count (distinct a.registerid_chr)
                                              from t_opr_bih_leave a,
                                                   t_opr_bih_transfer b,
                                                   t_opr_bih_register reg
                                             where a.registerid_chr = b.registerid_chr
                                               and a.registerid_chr = reg.registerid_chr
                                               and reg.relateregisterid_chr is null
                                               and (b.type_int = 6 or b.type_int = 7)
                                               and a.status_int = 1
                                               and (a.outhospital_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                                               and b.targetareaid_chr = ?) as outhospitalnum,
                                           
                                           --出院召回//如果已在院人数已包括了召回的，就去掉已存在的相同的登记号
                                           (select count (distinct b.registerid_chr)
                                              from t_opr_bih_transfer b
                                             where b.type_int = 4
                                               and (b.modify_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                                               and b.targetareaid_chr = ?) as outbacknum,
                                           
                                           --出院死亡
                                           (select count (distinct a.registerid_chr)
                                              from t_opr_bih_transfer a,
                                                   t_opr_bih_leave b,
                                                   t_opr_bih_register reg
                                             where a.registerid_chr = b.registerid_chr
                                               and a.registerid_chr = reg.registerid_chr
                                               and reg.relateregisterid_chr is null
                                               and (a.type_int = 6 or a.type_int = 7)
                                               and b.status_int = 1
                                               and b.type_int = 4
                                               and (b.outhospital_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                                               and a.targetareaid_chr = ?) as deadoutnum,
                                           
                                           --24小时死亡
                                           (select count (distinct a.registerid_chr)
                                              from t_opr_bih_transfer a,
                                                   t_opr_bih_leave b,
                                                   (select   max (modify_dat) as modify_dat,
                                                             registerid_chr
                                                        from t_opr_bih_transfer
                                                       where (type_int = 3 or type_int = 5)
                                                         and targetareaid_chr = ?
                                                    group by registerid_chr) c
                                             where a.registerid_chr = b.registerid_chr
                                               and a.modify_dat = b.modify_dat
                                               and a.registerid_chr = c.registerid_chr
                                               and (a.type_int = 6 or a.type_int = 7)
                                               and b.status_int = 1
                                               and b.type_int = 4
                                               and (a.modify_dat - c.modify_dat) < 1
                                               and (a.modify_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss')) 
                                               and a.targetareaid_chr = ?) as deadin24
                                      from t_opr_bih_transfer
                                     where rownum = 1) ";

                System.Data.IDataParameter[] arrParams = null;
                objHRPSvc.CreateDatabaseParameter(23, out arrParams);
                int n = -1;

                //今日入院人数 3
                arrParams[++n].Value = m_dtDate1.ToString("yyyy-MM-dd") + " 00:00:00";
                arrParams[++n].Value = m_dtDate2.ToString("yyyy-MM-dd") + " 23:59:59";
                arrParams[++n].Value = m_strAREAID_CHR;
                //转入人数    3
                arrParams[++n].Value = m_dtDate1.ToString("yyyy-MM-dd") + " 00:00:00";
                arrParams[++n].Value = m_dtDate2.ToString("yyyy-MM-dd") + " 23:59:59";
                arrParams[++n].Value = m_strAREAID_CHR;

                //转出人数       4 
                arrParams[++n].Value = m_dtDate1.ToString("yyyy-MM-dd") + " 00:00:00";
                arrParams[++n].Value = m_dtDate2.ToString("yyyy-MM-dd") + " 23:59:59";
                arrParams[++n].Value = m_strAREAID_CHR;
                arrParams[++n].Value = m_strAREAID_CHR;

                //出院 3
                arrParams[++n].Value = m_dtDate1.ToString("yyyy-MM-dd") + " 00:00:00";
                arrParams[++n].Value = m_dtDate2.ToString("yyyy-MM-dd") + " 23:59:59";
                arrParams[++n].Value = m_strAREAID_CHR;
                // 出院召回 3
                arrParams[++n].Value = m_dtDate1.ToString("yyyy-MM-dd") + " 00:00:00";
                arrParams[++n].Value = m_dtDate2.ToString("yyyy-MM-dd") + " 23:59:59";
                arrParams[++n].Value = m_strAREAID_CHR;

                //出院死亡      3
                arrParams[++n].Value = m_dtDate1.ToString("yyyy-MM-dd") + " 00:00:00";
                arrParams[++n].Value = m_dtDate2.ToString("yyyy-MM-dd") + " 23:59:59";
                arrParams[++n].Value = m_strAREAID_CHR;

                //24小时死亡 4
                arrParams[++n].Value = m_strAREAID_CHR;
                arrParams[++n].Value = m_dtDate1.ToString("yyyy-MM-dd") + " 00:00:00";
                arrParams[++n].Value = m_dtDate2.ToString("yyyy-MM-dd") + " 23:59:59";
                arrParams[++n].Value = m_strAREAID_CHR;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);

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


        #region 根据病区ID查询静脉滴注单 11
        /// <summary>
        /// 根据病区ID查询静脉滴注单 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID">病区ID</param>
        /// <param name="p_strBedIDarr">床位数组</param>
        /// <param name="p_strBeginDate">统计时间</param>
        /// <param name="p_intSerchType">统计方式</param>
        /// <param name="p_strORDERID_VCHR">工作单Id</param>
        /// <param name="p_dtbRecord">查询结果数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBihdriopping_Med(string p_strAreaID, string p_strBedIDarr, string p_strBeginDate, int p_intSerchType, string p_strORDERID_VCHR, out DataTable p_dtbRecord)
        {
            p_dtbRecord = new DataTable();
            string m_strCURBEDID_CHR = "";
            string strPrintFlag = "";
            string strSerchType = "";

            long lngRes = 0;
            string strSQL = @"SELECT     
                                 a.CODE_CHR, 
                                 b.DEPTID_CHR,
                                 b.DEPTNAME_VCHR,   
                                 c.LASTNAME_VCHR,   
                                 d.ORDERID_CHR,   
                                 d.EXECUTETYPE_INT,   
                                 d.RECIPENO_INT,   
                                 d.RECIPENO2_INT,   
                                 d.REGISTERID_CHR,   
                                 d.NAME_VCHR,   
                                 d.DOSAGE_DEC,   
                                 d.DOSAGEUNIT_CHR,   
                                 d.EXECFREQNAME_CHR,   
                                 d.DOSETYPENAME_CHR,
                                 d.GET_DEC,
                                 d.OUTGETMEDDAYS_INT,
                                 d.GETUNIT_CHR,
                                 d.IFPARENTID_INT,
                                 d.SPEC_VCHR,
                                 d.REMARK_VCHR, 
                                 d.status_int,
                                 e.ORDEREXECID_CHR,     
                                 e.EXECUTEDATE_VCHR,   
                                 e.CREATOR_CHR,   
                                 e.ISRECRUIT_INT,
                                 e.CREATEDATE_DAT,
                                 e.EXECUTEDAYS_INT,
                                 e.ISFIRST_INT,
                                 e.REPARE_INT,
                                 e.EXEAREAID_CHR,
                                 e.EXEBEDID_CHR,
                                 f.ORDERID_VCHR,
                                 g.AREAID_CHR,
                                 g.BEDID_CHR,
                                 g.INPATIENTID_CHR,
                                 g.PSTATUS_INT,
                                 c.SEX_CHR,
                                 c.birth_dat,
                                 g.icd10diagtext_vchr 
                            FROM T_BSE_BED a,   
                                 T_BSE_DEPTDESC b,   
                                 t_opr_bih_registerdetail c,   
                                 T_OPR_BIH_ORDER d,   
                                 T_OPR_BIH_ORDEREXECUTE e,   
                                 T_OPR_SETUSAGE f,
                                 t_opr_bih_register g  
                           WHERE [printFlag] 
                                 c.registerid_chr = g.registerid_chr and
                                 ( e.ORDERID_CHR = d.ORDERID_CHR ) and  
                                 ( d.registerid_chr = c.registerid_chr ) and 
                                 (g.bedid_chr = a.BEDID_CHR(+)) and                                  
                                 ( f.USAGEID_CHR = d.DOSETYPEID_CHR ) and 
                                 (g.status_int<>-1) and
                                 ( f.ORDERID_VCHR = ? ) AND                                 
                                 ((g.AREAID_CHR = ? and d.SOURCETYPE_INT = 0 and g.AREAID_CHR = b.DEPTID_CHR) or (d.CREATEAREAID_CHR = ? and d.SOURCETYPE_INT = 1 and d.CREATEAREAID_CHR = b.DEPTID_CHR))
                                 [CURBEDID_CHR]
                                 [serchType]
                        ORDER BY d.CURAREAID_CHR ASC,   
                                 d.CURBEDID_CHR ASC,   
                                 d.PATIENTID_CHR ASC,   
                                 d.RECIPENO_INT ASC,  
                                 d.ORDERID_CHR ASC,
                                 e.ISRECRUIT_INT ASC,
                                 d.IFPARENTID_INT ";

            if (!p_strBedIDarr.Trim().Equals(""))
            {
                m_strCURBEDID_CHR = " and c.REGISTERID_CHR in (" + p_strBedIDarr.Trim() + ")";

            }

            if (p_intSerchType == 0)
            {
                p_strBeginDate = DateTime.Parse(p_strBeginDate).ToShortDateString();

                strSerchType = " and e.CREATEDATE_DAT > to_date('" + p_strBeginDate + " 00:00:00" + @"', 'YYYY-mm-dd hh24:mi:ss') and
                                     e.CREATEDATE_DAT < to_date('" + p_strBeginDate + " 23:59:59" + "', 'YYYY-mm-dd hh24:mi:ss')";
            }
            else if (p_intSerchType == 1)
            {
                strPrintFlag = @"not exists ( select t2.seq_int from t_opr_bih_oeprint t2 where t2.orderid_vchr = f.ORDERID_VCHR and
                                                   t2.orderexecid_chr = e.orderexecid_chr and
                                                   t2.areaid_chr = d.CURAREAID_CHR ) and";

                p_strBeginDate = DateTime.Today.ToShortDateString();
                strSerchType = @" and e.CREATEDATE_DAT > to_date('" + p_strBeginDate + " 00:00:00" + @"', 'YYYY-mm-dd hh24:mi:ss')
                                  and e.CREATEDATE_DAT < to_date('" + p_strBeginDate + " 23:59:59" + "', 'YYYY-mm-dd hh24:mi:ss')";
            }
            else if (p_intSerchType == 2)
            {
                strSerchType = " and e.CREATEDATE_DAT > to_date('" + p_strBeginDate + "', 'YYYY-mm-dd hh24:mi:ss')";
            }
            else if (p_intSerchType == 3)
            {
                strPrintFlag = @"not exists ( select t2.seq_int from t_opr_bih_oeprint t2 where t2.orderid_vchr = f.ORDERID_VCHR and
                                                   t2.orderexecid_chr = e.orderexecid_chr and
                                                   t2.areaid_chr = d.CURAREAID_CHR ) and";

                p_strBeginDate = DateTime.Today.ToShortDateString();
                strSerchType = @" and (e.CREATEDATE_DAT is not null) 
                                  and (d.createdate_dat between to_date('" + p_strBeginDate + " 00:00:00" + @"', 'yyyy-mm-dd hh24:mi:ss')
                                                            and to_date('" + p_strBeginDate + " 23:59:59" + "', 'YYYY-mm-dd hh24:mi:ss'))";
            }

            if (p_strAreaID != "")
            {
                strSQL = strSQL.Replace("[printFlag]", strPrintFlag);
                strSQL = strSQL.Replace("[serchType]", strSerchType);
                strSQL = strSQL.Replace("[CURBEDID_CHR]", m_strCURBEDID_CHR);

            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbRecord);

                System.Data.IDataParameter[] arrParams = null;
                new clsHRPTableService().CreateDatabaseParameter(3, out arrParams);
                arrParams[0].Value = p_strORDERID_VCHR;
                arrParams[1].Value = p_strAreaID;
                arrParams[2].Value = p_strAreaID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtbRecord, arrParams);

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

        #region 根据病区ID简明巡视卡新增汇总功能CS-425 (ID:12578)
        /// <summary>
        /// 根据病区ID查询静脉滴注单 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID">病区ID</param>
        /// <param name="p_strBedIDarr">床位数组</param>
        /// <param name="p_strBeginDate">统计时间</param>
        /// <param name="p_intSerchType">统计方式</param>
        /// <param name="p_strORDERID_VCHR">工作单Id</param>
        /// <param name="p_dtbRecord">查询结果数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBihdriopping_MedSum(string p_strAreaID, string p_strBedIDarr, string p_strBeginDate, int p_intSerchType, string p_strORDERID_VCHR, bool p_stoped, bool p_repare, bool p_out, out DataTable p_dtbRecord)
        {
            p_dtbRecord = new DataTable();
            string m_strCURBEDID_CHR = "";
            string strPrintFlag = "";
            string strSerchType = "";

            long lngRes = 0;
            string strSQL = @"select t.deptname_vchr, t.name_vchr, t.spec_vchr, sum(t.get_dec) medsum ,t.getunit_chr
                  from (select a.code_chr,
                               b.deptid_chr,
                               b.deptname_vchr,
                               c.lastname_vchr,
                               d.orderid_chr,
                               d.orderdicid_chr,
                               d.executetype_int,
                               d.recipeno_int,
                               d.recipeno2_int,
                               d.registerid_chr,
                               d.name_vchr,
                               d.dosage_dec,
                               d.dosageunit_chr,
                               d.execfreqname_chr,
                               d.dosetypename_chr,
                               d.get_dec,
                               d.outgetmeddays_int,
                               d.getunit_chr,
                               d.ifparentid_int,
                               d.spec_vchr,
                               d.remark_vchr,
                               d.status_int,
                               e.orderexecid_chr,
                               e.executedate_vchr,
                               e.creator_chr,
                               e.isrecruit_int,
                               e.createdate_dat,
                               e.executedays_int,
                               e.isfirst_int,
                               e.repare_int,
                               e.exeareaid_chr,
                               e.exebedid_chr,
                               f.orderid_vchr,
                               g.areaid_chr,
                               g.bedid_chr,
                               g.inpatientid_chr,
                               g.pstatus_int,
                               c.sex_chr,
                               c.birth_dat
                            from t_bse_bed a,   
                                 t_bse_deptdesc b,   
                                 t_opr_bih_registerdetail c,   
                                 t_opr_bih_order d,   
                                 t_opr_bih_orderexecute e,   
                                 t_opr_setusage f,
                                 t_opr_bih_register g  
                           where [printFlag] 
                                 c.registerid_chr = g.registerid_chr and
                                 ( e.orderid_chr = d.orderid_chr ) and  
                                 ( d.registerid_chr = c.registerid_chr ) and 
                                 (g.bedid_chr = a.bedid_chr(+)) and                                  
                                 ( f.usageid_chr = d.dosetypeid_chr ) and 
                                 (g.status_int<>-1) and
                                 ( f.orderid_vchr = ? ) and                                 
                                 ((g.areaid_chr = ? and d.sourcetype_int = 0 and g.areaid_chr = b.deptid_chr) or (d.createareaid_chr = ? and d.sourcetype_int = 1 and d.createareaid_chr = b.deptid_chr))
                                 [CURBEDID_CHR]
                                 [serchType]
                                 [Filterconditions]
                        ) t
 group by t.deptname_vchr,t.orderdicid_chr, t.name_vchr, t.spec_vchr,t.getunit_chr 
 order by t.name_vchr ";


            string filter = "";

            if (p_repare == false)
            {
                filter += " and e.repare_int = 0 ";
            }

            if (p_out == false)
            {
                //出院或转区的病人，原长嘱都是停止的。
                if (p_stoped == false)
                {
                    filter += " and (d.executetype_int = 2 or ( d.status_int not in (3,6) and d.executetype_int = 1 )) ";
                }

                filter += " and g.pstatus_int not in (2,3) and e.exeareaid_chr = g.areaid_chr ";

            }


            if (!p_strBedIDarr.Trim().Equals(""))
            {
                m_strCURBEDID_CHR = " and c.registerid_chr in (" + p_strBedIDarr.Trim() + ")";

            }

            if (p_intSerchType == 0)
            {
                p_strBeginDate = DateTime.Parse(p_strBeginDate).ToShortDateString();

                strSerchType = " and e.createdate_dat > to_date('" + p_strBeginDate + " 00:00:00" + @"', 'YYYY-mm-dd hh24:mi:ss') and
                                     e.createdate_dat < to_date('" + p_strBeginDate + " 23:59:59" + "', 'YYYY-mm-dd hh24:mi:ss')";
            }
            else if (p_intSerchType == 1)
            {
                strPrintFlag = @"not exists ( select t2.seq_int from t_opr_bih_oeprint t2 where t2.orderid_vchr = f.ORDERID_VCHR and
                                                   t2.orderexecid_chr = e.orderexecid_chr and
                                                   t2.areaid_chr = d.curareaid_chr ) and";

                p_strBeginDate = DateTime.Today.ToShortDateString();
                strSerchType = @" and e.createdate_dat > to_date('" + p_strBeginDate + " 00:00:00" + @"', 'YYYY-mm-dd hh24:mi:ss')
                                  and e.createdate_dat < to_date('" + p_strBeginDate + " 23:59:59" + "', 'YYYY-mm-dd hh24:mi:ss')";
            }
            else if (p_intSerchType == 2)
            {
                strSerchType = " and e.createdate_dat > to_date('" + p_strBeginDate + "', 'YYYY-mm-dd hh24:mi:ss')";
            }
            else if (p_intSerchType == 3)
            {
                strPrintFlag = @"not exists ( select t2.seq_int from t_opr_bih_oeprint t2 where t2.orderid_vchr = f.ORDERID_VCHR and
                                                   t2.orderexecid_chr = e.orderexecid_chr and
                                                   t2.areaid_chr = d.curareaid_chr ) and";

                p_strBeginDate = DateTime.Today.ToShortDateString();
                strSerchType = @" and (e.createdate_dat is not null) 
                                  and (d.createdate_dat between to_date('" + p_strBeginDate + " 00:00:00" + @"', 'yyyy-mm-dd hh24:mi:ss')
                                                            and to_date('" + p_strBeginDate + " 23:59:59" + "', 'YYYY-mm-dd hh24:mi:ss'))";
            }

            if (p_strAreaID != "")
            {
                strSQL = strSQL.Replace("[printFlag]", strPrintFlag);
                strSQL = strSQL.Replace("[serchType]", strSerchType);
                strSQL = strSQL.Replace("[CURBEDID_CHR]", m_strCURBEDID_CHR);

            }

            strSQL = strSQL.Replace("[Filterconditions]", filter); //对出院或转区的病人，原长嘱都是停止的。

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbRecord);

                System.Data.IDataParameter[] arrParams = null;
                new clsHRPTableService().CreateDatabaseParameter(3, out arrParams);
                arrParams[0].Value = p_strORDERID_VCHR;
                arrParams[1].Value = p_strAreaID;
                arrParams[2].Value = p_strAreaID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtbRecord, arrParams);

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
