using System;
using System.Collections.Generic;
using System.Text;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using System.Data;
using System.Security;
using weCare.Core.Entity;
using com.digitalwave.Utility.SQLConvert;

namespace com.digitalwave.emr.EMR_CaseArchivingService
{
    /// <summary>
    /// 病案借阅
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsEMR_BorrowArchivedCaseService : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 根据住院号查询病案已归档的病人

        /// <summary>
        /// 根据住院号查询病案已归档的病人

        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_dtbResult">查询结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetArchivedCasePatientByID( string p_strInPatientID, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;
            if (string.IsNullOrEmpty(p_strInPatientID))
            {
                return -1;
            }

            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSQL = @"select ar.*,
       rehis.hisinpatientid_chr,
       rehis.hisinpatientdate,
       rehis.emrinpatientid,
       rehis.emrinpatientdate,
       de.lastname_vchr,
       de.sex_chr,
       dept.deptname_vchr,
       dept.deptid_chr,
       outcon.MAINDIAGNOSIS outhospitaldiagnose_right
  from T_EMR_CASEARCHIVING ar
 inner join t_opr_bih_register re on ar.registerid_chr = re.registerid_chr and re.STATUS_INT = 1
 inner join t_bse_deptdesc dept on dept.deptid_chr = re.AREAID_CHR
 inner join t_opr_bih_registerdetail de on de.registerid_chr =
                                           ar.registerid_chr
 inner join t_bse_hisemr_relation rehis on rehis.registerid_chr =
                                           ar.registerid_chr
                                       and rehis.hisinpatientid_chr = ?
  left outer join T_EMR_INHOSPITALMAINREC_GXCON outcon on outcon.REGISTERID_CHR =
                                                          rehis.REGISTERID_CHR
                                                      and outcon.STATUS = 0
 where (ar.archivedstatus_int = 1 or ar.archivedstatus_int = 2)
   and ar.status_int = 1 order by ar.CASELEVEL_INT asc,ar.OUTDATE_DAT desc";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //赋值


                objDPArr[0].Value = p_strInPatientID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        } 
        #endregion

        #region 根据病人姓名查询病案已归档的病人
        /// <summary>
        /// 根据病人姓名查询病案已归档的病人
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strPatientName">病人姓名</param>
        /// <param name="p_dtbResult">查询结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetArchivedCasePatientByName( string p_strPatientName, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;
            if (string.IsNullOrEmpty(p_strPatientName))
            {
                return -1;
            }

            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSQL = @"select ar.*,
       rehis.hisinpatientid_chr,
       rehis.hisinpatientdate,
       rehis.emrinpatientid,
       rehis.emrinpatientdate,
       de.lastname_vchr,
       de.sex_chr,
       dept.deptname_vchr,
       dept.deptid_chr,
       outcon.MAINDIAGNOSIS outhospitaldiagnose_right
  from T_EMR_CASEARCHIVING ar
 inner join t_opr_bih_register re on ar.registerid_chr = re.registerid_chr and re.STATUS_INT = 1
 inner join t_bse_deptdesc dept on dept.deptid_chr = re.AREAID_CHR
 inner join t_opr_bih_registerdetail de on de.registerid_chr =
                                           ar.registerid_chr
                                       and de.lastname_vchr = ?
 inner join t_bse_hisemr_relation rehis on rehis.registerid_chr =
                                           ar.registerid_chr
  left outer join T_EMR_INHOSPITALMAINREC_GXCON outcon on outcon.REGISTERID_CHR =
                                                          rehis.REGISTERID_CHR
                                                      and outcon.STATUS = 0
 where (ar.archivedstatus_int = 1 or ar.archivedstatus_int = 2)
   and ar.status_int = 1 order by ar.CASELEVEL_INT asc,ar.OUTDATE_DAT desc";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //赋值


                objDPArr[0].Value = p_strPatientName;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 模糊查询病案已归档的病人
        /// <summary>
        /// 模糊查询病案已归档的病人
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strConditionArr">查询条件
        ///[0]病人出院科室ID;[1]查询时间起始日期;[2]查询时间结束日期;[3]出院诊断
        /// 如不对其中任一条件作限制，将数组对应项置为空</param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetArchivedCasePatient( string[] p_strConditionArr, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            long lngRes = 0;
            if (p_strConditionArr == null || p_strConditionArr.Length != 4)
            {
                return -1;
            }

            if (string.IsNullOrEmpty(p_strConditionArr[1]) || string.IsNullOrEmpty(p_strConditionArr[2]))
            {
                return -1;
            }
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strDeptSQL = string.Empty;
                string strOutDiagnoseSQL = string.Empty;

                if (p_strConditionArr[0] != null)
                {
                    strDeptSQL = " and re.AREAID_CHR = ?";
                }
                if (p_strConditionArr[3] != null)
                {
                    strOutDiagnoseSQL = " and outcon.MAINDIAGNOSIS like ?";
                }
                string strSQL = @"select ar.ARCHIVEDID_INT,
       ar.REGISTERID_CHR,
       ar.ARCHIVEDDATE_DAT,
       ar.ARCHIVEUSER_CHR,
       ar.STATUS_INT,
       ar.ARCHIVEDSTATUS_INT,
       ar.CASELEVEL_INT,
       le.modify_dat OUTDATE_DAT,
       rehis.hisinpatientid_chr,
       rehis.hisinpatientdate,
       rehis.emrinpatientid,
       rehis.emrinpatientdate,
       de.lastname_vchr,
       de.sex_chr,
       dept.deptname_vchr,
       dept.deptid_chr,
       outcon.MAINDIAGNOSIS outhospitaldiagnose_right
  from T_EMR_CASEARCHIVING ar
 inner join t_opr_bih_register re on ar.registerid_chr = re.registerid_chr and re.STATUS_INT = 1
 inner join t_bse_deptdesc dept on dept.deptid_chr = re.AREAID_CHR
 inner join t_opr_bih_registerdetail de on de.registerid_chr =
                                           ar.registerid_chr 
 inner join t_bse_hisemr_relation rehis on rehis.registerid_chr =
                                           ar.registerid_chr
 inner join t_opr_bih_leave le on le.registerid_chr = ar.registerid_chr
                              and le.status_int = 1
                              and le.pstatus_int = 1
  left outer join T_EMR_INHOSPITALMAINREC_GXCON outcon on outcon.REGISTERID_CHR =
                                                          rehis.REGISTERID_CHR
                                                      and outcon.STATUS = 0 " + strOutDiagnoseSQL + @"
 where (ar.archivedstatus_int = 1 or ar.archivedstatus_int = 2)  
   and le.modify_dat between ? and ? 
   " + strDeptSQL + @"
   and ar.status_int = 1 order by ar.CASELEVEL_INT asc,ar.OUTDATE_DAT desc";

                int intCount = 2;
                if (!string.IsNullOrEmpty(strOutDiagnoseSQL))//指定出院诊断
                {
                    strSQL = strSQL.Replace("left outer join", "inner join");
                    intCount++;
                }
                if (!string.IsNullOrEmpty(strDeptSQL))
                {
                    intCount++;
                }
                int intIndex = -1;
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(intCount, out objDPArr);
                if (!string.IsNullOrEmpty(strOutDiagnoseSQL))
                {
                    objDPArr[++intIndex].Value = "%" + p_strConditionArr[3] + "%";
                }
                int intTemp = ++intIndex;
                objDPArr[intTemp].DbType = DbType.DateTime;
                objDPArr[intTemp].Value = Convert.ToDateTime(p_strConditionArr[1]);
                intTemp = ++intIndex;
                objDPArr[intTemp].DbType = DbType.DateTime;
                objDPArr[intTemp].Value = Convert.ToDateTime(p_strConditionArr[2]);
                if (!string.IsNullOrEmpty(strDeptSQL))
                {
                    objDPArr[++intIndex].Value = p_strConditionArr[0];
                }

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        } 
        #endregion

        #region 发送病案借阅申请
        /// <summary>
        /// 发送病案借阅申请
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objCase">病案申请</param>
        /// <param name="p_lngArchivedIDArr">归档病案流水号</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSendRequest( clsEMR_CaseSubscribeValue p_objCase, long[] p_lngArchivedIDArr)
        {
            if (p_objCase == null || p_lngArchivedIDArr == null || p_lngArchivedIDArr.Length <= 0)
            {
                return -1;
            }

            long lngRes = -1;

            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();

                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSeq = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSeq.m_lngGetSequenceValue("SEQ_CASEARCHIVING", out lngSequence);

                string strSQL = @"insert into T_EMR_CASESUBSCRIB
values
  (?, ?, ?, ?, ?, " + clsDatabaseSQLConvert.s_StrGetServDateFuncName  + ", ?, 0, ?, ?, ?)";

                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(9, out objDPArr2);
                objDPArr2[0].Value = lngSequence;
                objDPArr2[1].Value = p_objCase.m_strSUBSCRIBER_CHR;
                objDPArr2[2].Value = p_objCase.m_strACCOUNTFOR_VCHR;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = p_objCase.m_dtmBEGINDATE_DAT;
                objDPArr2[4].DbType = DbType.DateTime;
                objDPArr2[4].Value = p_objCase.m_dtmENDDATE_DAT;
                objDPArr2[5].Value = p_objCase.m_strSUPERIOR_CHR;
                objDPArr2[6].Value = DBNull.Value;
                objDPArr2[7].Value = DBNull.Value;
                objDPArr2[8].Value = DBNull.Value;

                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr2);

                if (lngRes > 0)
	            {
                    string strMapSQL = @"insert into T_EMR_CASEEMPMAP(ARCHIVEDID_CHR,SUBSCRIBID_CHR) values (?, ?)";

                    if (clsHRPTableService.bytDatabase_Selector != (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                    {
                        for (int i = 0; i < p_lngArchivedIDArr.Length; i++)
                        {
                            IDataParameter[] objDPArr = null;
                            objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                            objDPArr2[0].Value = p_lngArchivedIDArr[i];
                            objDPArr2[1].Value = lngSequence;

                            lngEff = 0;
                            lngRes = objHRPServ.lngExecuteParameterSQL(strMapSQL, ref lngEff, objDPArr);
                        }
                    }
                    else
                    {
                        DbType[] dbTypes = new DbType[] { DbType.Int64, DbType.Int64 };
                        object[][] objValues = new object[2][];

                        for (int j = 0; j < objValues.Length; j++)
                        {
                            objValues[j] = new object[p_lngArchivedIDArr.Length];//初始化


                        }
                        for (int k1 = 0; k1 < p_lngArchivedIDArr.Length; k1++)
                        {
                            objValues[0][k1] = p_lngArchivedIDArr[k1];
                            objValues[1][k1] = lngSequence;
                        }
                        lngRes = objHRPServ.m_lngSaveArrayWithParameters(strMapSQL, objValues, dbTypes);
                    }
	            }
                   
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        } 
        #endregion

        #region 获取员工调阅病案记录
        /// <summary>
        /// 获取员工调阅病案记录(查阅期限未过)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSubscriber">员工ID</param>
        /// <param name="p_objValueArr">调阅记录</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRequestHistory( string p_strSubscriber, out clsEMR_CaseSubscribeValue[] p_objValueArr)
        {
            p_objValueArr = null;
            if (string.IsNullOrEmpty(p_strSubscriber))
            {
                return -1;
            }

            long lngRes = -1;

            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSQL = @"select su.*, ma.casecount
  from T_EMR_CASESUBSCRIB su
 inner join (select distinct count(a.archivedid_chr) casecount, a.subscribid_chr
               from T_EMR_CASEEMPMAP a
              group by a.subscribid_chr) ma on su.subscribid_chr =
                                                                 ma.subscribid_chr
 where su.subscriber_chr = ? and su.SUBSCRIBSTATUS_INT <> -1 and ENDDATE_DAT > " + clsDatabaseSQLConvert.s_StrGetServDateFuncName + @"
 order by su.SUBSCRIBSTATUS_INT,su.CREATEDATE_DAT";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //赋值


                objDPArr[0].Value = p_strSubscriber;

                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);

                if (lngRes > 0 && dtbResult != null)
                {
                    m_mthSetDataTableToSubscribeVO(dtbResult, out p_objValueArr);
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 获取员工已获批准的调阅病案申请记录(不查询每份申请包含几份病案)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSubscriber">员工ID</param>
        /// <param name="p_objValueArr">调阅记录</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetApprovedRequestHistory( string p_strSubscriber, out clsEMR_CaseSubscribeValue[] p_objValueArr)
        {
            p_objValueArr = null;
            if (string.IsNullOrEmpty(p_strSubscriber))
            {
                return -1;
            }

            long lngRes = -1;

            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSQL = @"select su.*, '0' casecount
  from T_EMR_CASESUBSCRIB su
 where su.subscriber_chr = ?
 and su.subscribstatus_int = 1
 and su.enddate_dat > " + clsDatabaseSQLConvert.s_StrGetServDateFuncName + " order by su.CREATEDATE_DAT";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //赋值


                objDPArr[0].Value = p_strSubscriber;

                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);

                if (lngRes > 0 && dtbResult != null)
                {
                    m_mthSetDataTableToSubscribeVO(dtbResult, out p_objValueArr);
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 设置DataTable内容至VO
        /// </summary>
        /// <param name="p_dtbResult">DataTable</param>
        /// <param name="p_objValueArr">返回结果</param>
        [AutoComplete]
        private void m_mthSetDataTableToSubscribeVO(DataTable p_dtbResult, out  clsEMR_CaseSubscribeValue[] p_objValueArr)
        {
            p_objValueArr = null;
            if (p_dtbResult == null || p_dtbResult.Rows.Count <= 0)
            {
                return;
            }
            int intRowCount = p_dtbResult.Rows.Count;
            p_objValueArr = new clsEMR_CaseSubscribeValue[intRowCount];
            DataRow dtCurrent = null;
            for (int j1 = 0; j1 < intRowCount; j1++)
            {
                dtCurrent = p_dtbResult.Rows[j1];
                p_objValueArr[j1] = new clsEMR_CaseSubscribeValue();

                if (dtCurrent["casecount"] != DBNull.Value)
                {
                    p_objValueArr[j1].m_intCaseCount = int.Parse(dtCurrent["casecount"].ToString());
                }
                else
                {
                    p_objValueArr[j1].m_intCaseCount = 0;
                }

                p_objValueArr[j1].m_lngSUBSCRIBID = Convert.ToInt64(dtCurrent["SUBSCRIBID_CHR"]);
                p_objValueArr[j1].m_strSUBSCRIBER_CHR = dtCurrent["SUBSCRIBER_CHR"].ToString();
                p_objValueArr[j1].m_strACCOUNTFOR_VCHR = dtCurrent["ACCOUNTFOR_VCHR"].ToString();

                DateTime dtm = DateTime.MinValue;
                DateTime.TryParse(dtCurrent["BEGINDATE_DAT"].ToString(), out dtm);
                p_objValueArr[j1].m_dtmBEGINDATE_DAT = dtm;

                DateTime.TryParse(dtCurrent["ENDDATE_DAT"].ToString(), out dtm);
                p_objValueArr[j1].m_dtmENDDATE_DAT = dtm;

                DateTime.TryParse(dtCurrent["CREATEDATE_DAT"].ToString(), out dtm);
                p_objValueArr[j1].m_dtmCREATEDATE_DAT = dtm;

                p_objValueArr[j1].m_strSUPERIOR_CHR = dtCurrent["SUPERIOR_CHR"].ToString();
                p_objValueArr[j1].m_intSUBSCRIBSTATUS_INT = Convert.ToInt32(dtCurrent["SUBSCRIBSTATUS_INT"]);
                p_objValueArr[j1].m_strCLINICSIGN_CHR = dtCurrent["CLINICSIGN_CHR"].ToString();
                p_objValueArr[j1].m_strAPPROVER_CHR = dtCurrent["APPROVER_CHR"].ToString();

                DateTime.TryParse(dtCurrent["APPROVEDDATE_DAT"].ToString(), out dtm);
                p_objValueArr[j1].m_dtmAPPROVEDDATE_DAT = dtm;
            }
        }
        #endregion

        #region 取消预约
        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_lngSubscribeID">申请表流水号</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCancelSubscribe( long p_lngSubscribeID)
        {
            long lngRes = -1;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSQL = @"update T_EMR_CASESUBSCRIB su
   set su.subscribstatus_int = -1
 where su.subscribstatus_int = 0
   and su.subscribid_chr = ?";

                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr2);
                objDPArr2[0].Value = p_lngSubscribeID;

                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr2);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        } 
        #endregion

        #region 根据病案调阅申请表流水号获取相应病案
        /// <summary>
        /// 根据病案调阅申请表流水号获取相应病案
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_lngSubscribID">流水号</param>
        /// <param name="p_dtbResult">查询结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetArchivingCaseBySubscribID( long p_lngSubscribID, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            long lngRes = -1;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();

                string strSQL = @"select ar.*,
       rehis.hisinpatientid_chr,
       rehis.hisinpatientdate,
       rehis.emrinpatientid,
       rehis.emrinpatientdate,
       de.lastname_vchr,
       de.sex_chr,
       dept.deptname_vchr,
       re.deptid_chr,
       re.PATIENTID_CHR,
       re.AREAID_CHR,
       outcon.MAINDIAGNOSIS outhospitaldiagnose_right
  from T_EMR_CASEARCHIVING ar
 inner join T_EMR_CASEEMPMAP ma on ar.archivedid_int = ma.archivedid_chr
 inner join t_opr_bih_register re on ar.registerid_chr = re.registerid_chr and re.STATUS_INT = 1
 inner join t_bse_deptdesc dept on dept.deptid_chr = re.AREAID_CHR
 inner join t_opr_bih_registerdetail de on de.registerid_chr =
                                           ar.registerid_chr
 inner join t_bse_hisemr_relation rehis on rehis.registerid_chr =
                                           ar.registerid_chr
  left outer join T_EMR_INHOSPITALMAINREC_GXCON outcon on outcon.REGISTERID_CHR =
                                                          rehis.REGISTERID_CHR
                                                      and outcon.STATUS = 0
 where (ar.archivedstatus_int = 1 or ar.archivedstatus_int = 2)
   and ar.status_int = 1
   and ma.subscribid_chr = ?
 order by ar.CASELEVEL_INT,
          rehis.hisinpatientid_chr,
          rehis.hisinpatientdate";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //赋值


                objDPArr[0].Value = p_lngSubscribID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        } 
        #endregion
    }
}
