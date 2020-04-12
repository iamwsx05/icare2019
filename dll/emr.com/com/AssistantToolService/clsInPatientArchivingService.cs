using System.EnterpriseServices;
using System;
using System.Text;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.Utility.SQLConvert;



namespace com.digitalwave.InPatientArchivingService
{
    /// <summary>
    /// 病案归档中间件
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsInPatientArchivingService : ServicedComponent
    {
        public clsInPatientArchivingService()
        { }

        /// <summary>
        /// 按住院号查询病人出院状态
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_intStatus">如果p_intStatus=0则查询未归档的病人，>=1则查询已归档病人,= -1查询全部</param>
        /// <param name="p_objArchivingValueArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long lngGetRecordByInPatientIDArr( string p_strInPatientID, string p_strInpatientDate, int p_intStatus, out clsInPatientArchivingValue p_objArchivingValue)
        {
            p_objArchivingValue = null; 

            if (p_strInPatientID == null || p_strInPatientID.Trim().Length == 0) return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;

            string str = " and re.inpatientid_chr = '" + p_strInPatientID + "' and re.inpatient_dat = " + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInpatientDate) + " ";
            clsInPatientArchivingValue[] objArchivingValueArr = null;
            lngRes = lngGetOutPatientRecordWithin7Day(str, p_intStatus, out objArchivingValueArr);
            if (objArchivingValueArr != null && objArchivingValueArr.Length > 0)
                p_objArchivingValue = objArchivingValueArr[0];
            return lngRes;
        }
        /// <summary>
        /// 按住院号模糊查询特定科室下7天那出院病人
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strDeptIDArr"></param>
        /// <param name="p_intStatus">如果p_intStatus=0则查询未归档的病人，>=1则查询已归档病人,= -1查询全部</param>
        /// <param name="p_objArchivingValueArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long lngGetRecordByInPatientIDLikeArr( string p_strInPatientID, string[] p_strDeptIDArr, int p_intStatus, out clsInPatientArchivingValue[] p_objArchivingValueArr)
        {

            p_objArchivingValueArr = null; 
            if (p_strInPatientID == null || p_strInPatientID.Trim().Length == 0) return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;

            string str = " and re.inpatientid_chr like %" + p_strInPatientID + "% ";
            if (p_strDeptIDArr != null && p_strDeptIDArr.Length > 0)
            {
                str = " and re.deptid_chr = '" + p_strDeptIDArr[0] + "' ";
                for (int j2 = 1 ; j2 < p_strDeptIDArr.Length ; j2++)
                    str += " or re.deptid_chr = '" + p_strDeptIDArr[j2] + "' ";
            }
            lngRes = lngGetOutPatientRecordWithin7Day(str, p_intStatus, out p_objArchivingValueArr);
            return lngRes;
        }

        /// <summary>
        /// 按病人姓名模糊查询特定科室下7天那出院病人
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientName"></param>
        /// <param name="p_strDeptIDArr"></param>
        /// <param name="p_intStatus">如果p_intStatus=0则查询未归档的病人，>=1则查询已归档病人,= -1查询全部</param>
        /// <param name="p_objArchivingValueArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long lngGetRecordByInPatientNameLikeArr( string p_strInPatientName, string[] p_strDeptIDArr, int p_intStatus, out clsInPatientArchivingValue[] p_objArchivingValueArr)
        {

            p_objArchivingValueArr = null; 
            if (p_strInPatientName == null || p_strInPatientName.Length == 0) return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;

            string str = " and pa.lastname_vchr like %" + p_strInPatientName + "% ";
            if (p_strDeptIDArr != null && p_strDeptIDArr.Length > 0)
            {
                str = " and re.deptid_chr = '" + p_strDeptIDArr[0] + "' ";
                for (int j2 = 1 ; j2 < p_strDeptIDArr.Length ; j2++)
                    str += " or re.deptid_chr = '" + p_strDeptIDArr[j2] + "' ";
            }
            lngRes = lngGetOutPatientRecordWithin7Day(str, p_intStatus, out p_objArchivingValueArr);
            return lngRes;
        }
        /// <summary>
        /// 获取特定临床科室下7天内出院病人列表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeptIDArr">临床科室列表，查询全院的置null</param>
        /// <param name="p_intStatus">如果p_intStatus=0则查询未归档的病人，>=1则查询已归档病人,= -1查询全部</param>
        /// <param name="p_objArchivingValueArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long lngGetRecordWithin7DayByEmpDeptArr(  string[] p_strDeptIDArr, int p_intStatus, out clsInPatientArchivingValue[] p_objArchivingValueArr)
        {
            p_objArchivingValueArr = null; 
            string str = "";
            if (p_strDeptIDArr != null && p_strDeptIDArr.Length > 0)
            {
                str = "and (re.deptid_chr = '" + p_strDeptIDArr[0] + "' ";
                for (int j2 = 1 ; j2 < p_strDeptIDArr.Length ; j2++)
                    str += "or re.deptid_chr = '" + p_strDeptIDArr[j2] + "' ";

                str += ")";
            }
            long lngRes = lngGetOutPatientRecordWithin7Day(str, p_intStatus, out p_objArchivingValueArr);
            return lngRes;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strAddSql"></param>
        /// <param name="p_intStatus">如果p_intStatus=0则查询未归档的病人，>=1则查询已归档病人,= -1查询全部</param>
        /// <param name="p_objArchivingValueArr"></param>
        /// <returns></returns>
        [AutoComplete]
        private long lngGetOutPatientRecordWithin7Day(string p_strAddSql, int p_intStatus, out clsInPatientArchivingValue[] p_objArchivingValueArr)
        {

            p_objArchivingValueArr = null;
            long lngRes = 0;
            string strSQL = "";
            try
            {
                if (clsHRPTableService.bytDatabase_Selector == 0)
                {
                    strSQL = @"select distinct re.registerid_chr,
       re.inpatientid_chr,
       re.inpatient_dat,
       re.deptid_chr,
       re.areaid_chr,
       re.bedid_chr,
       rehis.emrinpatientid,
       rehis.emrinpatientdate,
       rehis.hisinpatientid_chr,
       rehis.hisinpatientdate,
	   bed.code_chr,
       re.patientid_chr,
       pa.lastname_vchr,
       pa.sex_chr,
       datediff(year, pa.birth_dat, getdate()) as age,
       isnull(ar.ifarchived,0) as ifarchived,
       ar.archivingchangeuserid,
	   ar.opendate,
       le.modify_dat as inpatientenddate,
       (datediff(mi,le.modify_dat , getdate())) / 60 as passhour,
		le.type_int," + clsDatabaseSQLConvert.s_strGetFuntionSQL("F_GETEMPNAMEBYID(ar.ArchivingChangeUserID)")
                        + @"as changeusername from t_opr_bih_register re
 inner join t_bse_hisemr_relation rehis on rehis.registerid_chr =
                                                   re.registerid_chr
 inner join t_opr_bih_registerdetail pa on re.registerid_chr = pa.registerid_chr
 inner join t_opr_bih_leave le on re.registerid_chr = le.registerid_chr
  left join inpatientarchiving ar on re.inpatientid_chr = ar.inpatientid
                                 and re.inpatient_dat = ar.inpatientdate
 left join t_bse_bed bed on re.bedid_chr = bed.bedid_chr
 where re.pstatus_int = 3
   and re.status_int > 0
   and le.status_int = 1
   and le.pstatus_int = 1
   and (datediff(mi, le.modify_dat, getdate()) / 1440) < 7 ";
                }
                else if (clsHRPTableService.bytDatabase_Selector == 2)
                {
                    strSQL = @"select distinct re.registerid_chr,
       re.inpatientid_chr,
       re.inpatient_dat,
       re.deptid_chr,
       re.areaid_chr,
       re.bedid_chr,
       rehis.emrinpatientid,
       rehis.emrinpatientdate,
       rehis.hisinpatientid_chr,
       rehis.hisinpatientdate,
	   bed.code_chr,
       re.patientid_chr,
       pa.lastname_vchr,
       pa.sex_chr,
       (to_number(to_char(sysdate, 'yyyy')) -
       to_number(to_char(pa.birth_dat, 'yyyy'))) as age,
       nvl(ar.ifarchived,0) as ifarchived,
       ar.archivingchangeuserid,
       ar.opendate,
       le.modify_dat as inpatientenddate,
       round(to_number(sysdate - le.modify_dat) * 24) as passhour,
       le.type_int,
       f_getempnamebyid(ar.archivingchangeuserid) as changeusername
  from t_opr_bih_register re
 inner join t_bse_hisemr_relation rehis on rehis.registerid_chr =
                                                   re.registerid_chr
 inner join t_opr_bih_registerdetail pa on re.registerid_chr = pa.registerid_chr
 inner join t_opr_bih_leave le on re.registerid_chr = le.registerid_chr
  left join inpatientarchiving ar on re.inpatientid_chr = ar.inpatientid
                                 and re.inpatient_dat = ar.inpatientdate
 left join t_bse_bed bed on re.bedid_chr = bed.bedid_chr
 where re.pstatus_int = 3
   and re.status_int > 0
   and le.status_int = 1
   and le.pstatus_int = 1
   and (sysdate - le.modify_dat) < 7 ";
                }
                if (p_intStatus == 0)
                    strSQL += " and (ar.ifarchived is null or ar.ifarchived = 0)";
                else if (p_intStatus == 1)
                    strSQL += " and ar.ifarchived = 1";
                if (p_strAddSql != null)
                    strSQL += p_strAddSql;
                strSQL += " order by passHour";
                DataTable objDataTableResult = new DataTable();
                lngRes = new clsHRPTableService().DoGetDataTable(strSQL, ref objDataTableResult);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objArchivingValueArr = new clsInPatientArchivingValue[objDataTableResult.Rows.Count];
                    for (int i = 0 ; i < objDataTableResult.Rows.Count ; i++)
                    {
                        p_objArchivingValueArr[i] = new clsInPatientArchivingValue();
                        p_objArchivingValueArr[i].m_strArchivingChangeUserID = objDataTableResult.Rows[i]["ArchivingChangeUserID"].ToString();
                        if (p_objArchivingValueArr[i].m_strArchivingChangeUserID == null)
                            p_objArchivingValueArr[i].m_strArchivingChangeUserID = "";
                        p_objArchivingValueArr[i].m_strArchivingChangeUserName = objDataTableResult.Rows[i]["ChangeUserName"].ToString();
                        if (p_objArchivingValueArr[i].m_strArchivingChangeUserName != null)
                            p_objArchivingValueArr[i].m_strArchivingChangeUserName = p_objArchivingValueArr[i].m_strArchivingChangeUserName.Trim();
                        int intTYPE_INT = 3;
                        try { intTYPE_INT = int.Parse(objDataTableResult.Rows[i]["TYPE_INT"].ToString()); }
                        catch { intTYPE_INT = 3; }
                        if (intTYPE_INT == 3)//类型{1=治愈出院;2=转院;3=其它;4=死亡}
                            p_objArchivingValueArr[i].m_strEndReason = "其它";
                        else if (intTYPE_INT == 1)
                            p_objArchivingValueArr[i].m_strEndReason = "治愈出院";
                        else if (intTYPE_INT == 2)
                            p_objArchivingValueArr[i].m_strEndReason = "转院";
                        else if (intTYPE_INT == 4)
                            p_objArchivingValueArr[i].m_strEndReason = "死亡";
                        p_objArchivingValueArr[i].m_strIfArchived = objDataTableResult.Rows[i]["IFARCHIVED"].ToString().Trim();
                        p_objArchivingValueArr[i].m_strInPatientDate = objDataTableResult.Rows[i]["emrinpatientdate"].ToString();
                        p_objArchivingValueArr[i].m_strInPatientEndDate = objDataTableResult.Rows[i]["INPATIENTENDDATE"].ToString();
                        p_objArchivingValueArr[i].m_strInPatientID = objDataTableResult.Rows[i]["emrinpatientid"].ToString().Trim();
                        p_objArchivingValueArr[i].m_strOpenDate = objDataTableResult.Rows[i]["OPENDATE"].ToString();
                        p_objArchivingValueArr[i].m_strAreaID = objDataTableResult.Rows[i]["AREAID_CHR"].ToString();
                        p_objArchivingValueArr[i].m_strBedID = objDataTableResult.Rows[i]["BEDID_CHR"].ToString();
                        p_objArchivingValueArr[i].m_strBedName = objDataTableResult.Rows[i]["CODE_CHR"].ToString();
                        p_objArchivingValueArr[i].m_strDeptID = objDataTableResult.Rows[i]["DEPTID_CHR"].ToString();
                        p_objArchivingValueArr[i].m_strpatientAge = objDataTableResult.Rows[i]["Age"].ToString();
                        p_objArchivingValueArr[i].m_strInPatientName = objDataTableResult.Rows[i]["LASTNAME_VCHR"].ToString().Trim();
                        p_objArchivingValueArr[i].m_StrPassHour = objDataTableResult.Rows[i]["PASSHOUR"].ToString();
                        p_objArchivingValueArr[i].m_strPatientID = objDataTableResult.Rows[i]["PATIENTID_CHR"].ToString();
                        p_objArchivingValueArr[i].m_strPatientSex = objDataTableResult.Rows[i]["SEX_CHR"].ToString();
                        p_objArchivingValueArr[i].m_strREGISTERID_CHR = objDataTableResult.Rows[i]["REGISTERID_CHR"].ToString();

                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 归档
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strArchivingChangeUserID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long lngSetArchived( string p_strInPatientID, string p_strInPatientDate, string p_strArchivingChangeUserID, out string[] p_strIsArchived)
        {
            p_strIsArchived = null; 
            if (p_strInPatientID == null || p_strInPatientID.Length == 0
                || p_strInPatientDate == null || p_strInPatientDate.Length == 0
                || p_strArchivingChangeUserID == null || p_strArchivingChangeUserID.Length == 0) return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            try
            {
                string strSQL = @"select " + clsDatabaseSQLConvert.s_strGetFuntionSQL("F_GETEMPNAMEBYID(ArchivingChangeUserID)") +
                    @",opendate from inpatientarchiving where inpatientid = ? and inpatientdate = ? and ifarchived = 1 order by opendate desc";
                IDataParameter[] objParmArr = null;
                new clsHRPTableService().CreateDatabaseParameter(2, out objParmArr);

                objParmArr[0].Value = p_strInPatientID;
                objParmArr[1].DbType = DbType.Date;
                objParmArr[1].Value = DateTime.Parse(p_strInPatientDate);
                DataTable dt = new DataTable();
                lngRes = new clsHRPTableService().lngGetDataTableWithParameters(strSQL, ref dt, objParmArr);
                if (lngRes > 0 && dt.Rows.Count > 0)
                {
                    p_strIsArchived = new string[2];
                    p_strIsArchived[0] = dt.Rows[0][0].ToString();
                    if (p_strIsArchived[0] != null)
                        p_strIsArchived[0] = p_strIsArchived[0].Trim();
                    p_strIsArchived[1] = dt.Rows[0][1].ToString();
                    if (p_strIsArchived[1] != null)
                        p_strIsArchived[1] = p_strIsArchived[1].Trim();
                    return 10;
                }
                strSQL = @"insert into inpatientarchiving (inpatientid,inpatientdate,opendate,ifarchived,archivingchangeuserid) 
				 values(?,?,?,?,?)";
                IDataParameter[] objQueryModeArr = null;
                new clsHRPTableService().CreateDatabaseParameter(5, out objQueryModeArr);
                objQueryModeArr[0].Value = p_strInPatientID;
                objQueryModeArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objQueryModeArr[2].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                objQueryModeArr[3].Value = 1;
                objQueryModeArr[4].Value = p_strArchivingChangeUserID;
                long lngTemp = 0;
                lngRes = new clsHRPTableService().lngExecuteParameterSQL(strSQL, ref lngTemp, objQueryModeArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        /// <summary>
        /// 取消归档
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strArchivingChangeUserID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long lngUnsetArchived( string p_strInPatientID, string p_strInPatientDate, string p_strArchivingChangeUserID)
        { 
            if (p_strInPatientID == null || p_strInPatientID.Trim().Length == 0
                || p_strInPatientDate == null || p_strInPatientDate.Trim().Length == 0
                || p_strArchivingChangeUserID == null || p_strArchivingChangeUserID.Trim().Length == 0) return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            try
            {
                string strSQL = "update inpatientarchiving set ifarchived = 0,opendate = " + clsDatabaseSQLConvert.s_StrGetServDateFuncName + " where inpatientid=? and inpatientdate=?";
                IDataParameter[] objQueryModeArr = null;
                new clsHRPTableService().CreateDatabaseParameter(2, out objQueryModeArr);
                objQueryModeArr[0].Value = p_strInPatientID;
                objQueryModeArr[1].Value = DateTime.Parse(p_strInPatientDate);
                long lngTemp = 0;
                lngRes = new clsHRPTableService().lngExecuteParameterSQL(strSQL, ref lngTemp, objQueryModeArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInpatientDate"></param>
        /// <param name="p_objArchivingValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckFormReadOnly( string p_strInPatientID, string p_strInpatientDate, out clsInPatientArchivingValue p_objArchivingValue)
        {
            p_objArchivingValue = null; 
            if (p_strInPatientID == null || p_strInPatientID.Trim().Length == 0) return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            string strSql = "";
            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strSql = @"select distinct re.registerid_chr,
       re.inpatientid_chr,
       re.inpatient_dat,
       rehis.emrinpatientid,
       rehis.emrinpatientdate,
       rehis.hisinpatientid_chr,
       rehis.hisinpatientdate,
       nvl(ar.ifarchived,0) as ifarchived,
       (datediff(mi,le.modify_dat , getdate())) / 60 as passhour
	from t_opr_bih_register re
 inner join t_bse_hisemr_relation rehis on rehis.registerid_chr =
                                                   re.registerid_chr
 inner join t_opr_bih_leave le on re.registerid_chr = le.registerid_chr
  left join inpatientarchiving ar on re.inpatientid_chr = ar.inpatientid
                                 and re.inpatient_dat = ar.inpatientdate
where re.pstatus_int = 3
   and re.status_int > 0
   and le.status_int = 1
   and le.pstatus_int = 1
   and re.inpatientid_chr = ? and re.inpatient_dat = ?  order by re.inpatient_dat desc";
            }
            else if (clsHRPTableService.bytDatabase_Selector == 2)
            {
                strSql = @"select distinct re.registerid_chr,
       re.inpatientid_chr,
       re.inpatient_dat,
       rehis.emrinpatientid,
       rehis.emrinpatientdate,
       rehis.hisinpatientid_chr,
       rehis.hisinpatientdate,
       nvl(ar.ifarchived,0) as ifarchived,
       round(to_number(sysdate - le.modify_dat) * 24) as passhour
	from t_opr_bih_register re
 inner join t_bse_hisemr_relation rehis on rehis.registerid_chr =
                                                   re.registerid_chr
 inner join t_opr_bih_leave le on re.registerid_chr = le.registerid_chr
  left join inpatientarchiving ar on re.inpatientid_chr = ar.inpatientid
                                 and re.inpatient_dat = ar.inpatientdate
where re.pstatus_int = 3
   and re.status_int > 0
   and le.status_int = 1
   and le.pstatus_int = 1
   and re.inpatientid_chr = ? and re.inpatient_dat = ?  order by re.inpatient_dat desc";
            }
            try
            {
                IDataParameter[] objDPArr = null;
                new clsHRPTableService().CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInpatientDate);

                DataTable dt = new DataTable();
                lngRes = new clsHRPTableService().lngGetDataTableWithParameters(strSql, ref dt, objDPArr);
                if (lngRes > 0 && dt.Rows.Count >= 1)
                {
                    p_objArchivingValue = new clsInPatientArchivingValue();
                    p_objArchivingValue.m_strIfArchived = dt.Rows[0]["IfArchived"].ToString().Trim();
                    p_objArchivingValue.m_strInPatientDate = dt.Rows[0]["emrinpatientdate"].ToString();
                    //					p_objArchivingValue.m_strInPatientEndDate=dt.Rows[0]["INPATIENTENDDATE"].ToString();
                    p_objArchivingValue.m_strInPatientID = dt.Rows[0]["emrinpatientid"].ToString().Trim();
                    p_objArchivingValue.m_StrPassHour = dt.Rows[0]["PASSHOUR"].ToString();
                    p_objArchivingValue.m_strREGISTERID_CHR = dt.Rows[0]["REGISTERID_CHR"].ToString();
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

    }
}
