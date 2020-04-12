using System;
using System.EnterpriseServices;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.iCare.middletier.HRPService;

namespace com.digitalwave.PatientManagerService
{
    /// <summary>
    /// 病人管理类（针对全院）
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsWholeHosPatientManageServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// 
        /// </summary>
        public clsWholeHosPatientManageServ()
        { }


        /// <summary>
        /// 根据住院号取病人信息
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_objPatient"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientByInPatID(string p_strInPatientID, out clsPatientInfo_Value p_objPatient)
        {
            p_objPatient = null;
            long lngRes = 0;
            try
            {
                string strInPatientID = p_strInPatientID;
                if (strInPatientID == null)
                    return -1;
                if (strInPatientID.Trim().Length < 12)
                    strInPatientID = strInPatientID.Trim().PadLeft(12, '0');
                string strSql = @" and c.inpatientid_chr = '" + strInPatientID + "'";
                lngRes = m_lngGetPatient(strSql, out p_objPatient);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;

        }
        /// <summary>
        /// 根据病人卡号取病人信息
        /// </summary>
        /// <param name="p_strCardID"></param>
        /// <param name="p_objPatient"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientByCardID(string p_strCardID, out clsPatientInfo_Value p_objPatient)
        {

            p_objPatient = null;
            long lngRes = 0;
            try
            {
                string strCardID = p_strCardID;

                if (strCardID == null)
                    return -1;
                if (strCardID.Trim().Length < 10)
                    strCardID = strCardID.Trim().PadLeft(10, '0');
                string strSql = @" and b.patientcardid_chr = '" + strCardID + "'";
                lngRes = m_lngGetPatient(strSql, out p_objPatient);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;

        }
        /// <summary>
        /// 根据病人编号取病人信息
        /// </summary>
        /// <param name="p_strPatientID"></param>
        /// <param name="p_objPatient"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientByID(string p_strPatientID, out clsPatientInfo_Value p_objPatient)
        {
            long lngRes = 0;
            p_objPatient = null;
            string strSql = @"select a.patientid_chr as patientid,
       a.inpatientid_chr as inpatientid,
       a.lastname_vchr as patientname,
       a.married_chr as married,
       a.birthplace_vchr as birthplace,
       a.sex_chr as sex,
       a.idcard_chr as idcard,
       a.homeaddress_vchr as homeaddress,
       a.occupation_vchr as occupation,
       a.birth_dat as birth,
       a.nationality_vchr as nationality,
       a.nativeplace_vchr as nativeplace,
       b.patientcardid_chr as patientcardid,
       c.deptname,
       c.areaname,
       c.bedname,
       c.inpatientcount_int as inpatientcount,
       c.deptid_chr as deptid,
       c.areaid_chr as areaid,
       c.bedid_chr as bedid
  from t_bse_patient a
  left outer join t_bse_patientcard b on a.patientid_chr = b.patientid_chr
  left outer join (select a2.patientid_chr,
                          a2.deptid_chr,
                          a2.areaid_chr,
                          a2.bedid_chr,
                          a2.inpatientcount_int,
                          b2.deptname_vchr as deptname,
                          c2.deptname_vchr as areaname,
                          d2.code_chr as bedname
                     from t_opr_bih_register a2
                     left join t_bse_deptdesc b2 on a2.deptid_chr =
                                                    b2.deptid_chr
                     left join t_bse_deptdesc c2 on a2.areaid_chr =
                                                    c2.deptid_chr
                     left join t_bse_bed d2 on a2.bedid_chr = d2.bedid_chr
                    where a2.status_int = 1
                      and a2.pstatus_int <> '3') c on a.patientid_chr =
                                                      c.patientid_chr
 where b.status_int > 0
   and a.patientid_chr = ?";
            try
            {
                string strPatientID = p_strPatientID;
                if (strPatientID == null)
                    return -1;
                if (strPatientID.Trim().Length < 10)
                    strPatientID = strPatientID.Trim().PadLeft(10, '0');
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objServ = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();

                try
                {
                    DataTable dtRecords = new DataTable();
                    System.Data.IDataParameter[] paramArr = null;
                    objServ.CreateDatabaseParameter(1, out paramArr);
                    paramArr[0].Value = strPatientID;
                    lngRes = objServ.lngGetDataTableWithParameters(strSql, ref dtRecords, paramArr);
                    if (lngRes <= 0 || dtRecords.Rows.Count != 1)
                        return 0;
                    objServ.Dispose();
                    p_objPatient = new clsPatientInfo_Value();
                    p_objPatient.m_StrInPatientID = dtRecords.Rows[0]["INPATIENTID"].ToString().Trim();
                    p_objPatient.m_StrPatientID = dtRecords.Rows[0]["PATIENTID"].ToString().Trim();
                    p_objPatient.m_StrPatientCardID = dtRecords.Rows[0]["PATIENTCARDID"].ToString().Trim();
                    p_objPatient.m_StrOutPatientID = "";
                    p_objPatient.m_StrDeptID = dtRecords.Rows[0]["DEPTID"].ToString().Trim();
                    p_objPatient.m_StrAreaID = dtRecords.Rows[0]["AREAID"].ToString().Trim();
                    p_objPatient.m_StrBedID = dtRecords.Rows[0]["BEDID"].ToString().Trim();

                    p_objPatient.m_StrDeptName = dtRecords.Rows[0]["DEPTNAME"].ToString().Trim();
                    p_objPatient.m_StrAreaName = dtRecords.Rows[0]["AREANAME"].ToString().Trim();
                    p_objPatient.m_StrBedName = dtRecords.Rows[0]["BEDNAME"].ToString().Trim();
                    p_objPatient.m_ObjPeopleInfo = new clsPeopleInfo();
                    try
                    {
                        p_objPatient.m_ObjPeopleInfo.m_DtmBirth = Convert.ToDateTime(dtRecords.Rows[0]["BIRTH"]);
                    }
                    catch
                    {
                        p_objPatient.m_ObjPeopleInfo.m_DtmBirth = DateTime.Parse("1900-1-1");
                    }
                    try
                    {
                        p_objPatient.m_ObjPeopleInfo.m_IntTimes = Convert.ToInt32(dtRecords.Rows[0]["INPATIENTCOUNT"]);
                    }
                    catch
                    {
                        p_objPatient.m_ObjPeopleInfo.m_IntTimes = 0;
                    }
                    p_objPatient.m_ObjPeopleInfo.m_StrFirstName = dtRecords.Rows[0]["PATIENTNAME"].ToString().Trim();
                    p_objPatient.m_ObjPeopleInfo.m_StrHomeAddress = dtRecords.Rows[0]["AREAID"].ToString().Trim();
                    p_objPatient.m_ObjPeopleInfo.m_StrIDCard = dtRecords.Rows[0]["IDCARD"].ToString().Trim();
                    p_objPatient.m_ObjPeopleInfo.m_StrLastName = p_objPatient.m_ObjPeopleInfo.m_StrFirstName;
                    p_objPatient.m_ObjPeopleInfo.m_StrMarried = dtRecords.Rows[0]["MARRIED"].ToString().Trim();
                    p_objPatient.m_ObjPeopleInfo.m_StrNation = "";
                    p_objPatient.m_ObjPeopleInfo.m_StrNationality = dtRecords.Rows[0]["NATIONALITY"].ToString().Trim();
                    p_objPatient.m_ObjPeopleInfo.m_StrNativePlace = dtRecords.Rows[0]["NATIVEPLACE"].ToString().Trim();
                    p_objPatient.m_ObjPeopleInfo.m_StrSex = dtRecords.Rows[0]["SEX"].ToString().Trim();
                    p_objPatient.m_ObjPeopleInfo.m_StrOccupation = dtRecords.Rows[0]["OCCUPATION"].ToString().Trim();

                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
                finally
                {
                    objServ.Dispose();
                }


            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;

        }
        /// <summary>
        /// 取病人信息（全院，包括门诊、住院、电子病历）
        /// </summary>
        /// <param name="p_strSql"></param>
        /// <param name="p_objPatient"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngGetPatient(string p_strSql, out clsPatientInfo_Value p_objPatient)
        {
            p_objPatient = null;
            string strSQL = @"select a.patientid_chr as patientid,
       a.inpatientid_chr as inpatientid,
       a.lastname_vchr as patientname,
       a.married_chr as married,
       a.birthplace_vchr as birthplace,
       a.sex_chr as sex,
       a.idcard_chr as idcard,
       a.homeaddress_vchr as homeaddress,
       a.occupation_vchr as occupation,
       a.birth_dat as birth,
       a.nationality_vchr as nationality,
       a.nativeplace_vchr as nativeplace,
       b.patientcardid_chr as patientcardid,
       c.deptname,
       c.areaname,
       c.bedname,
       c.inpatientcount_int as inpatientcount,
       c.deptid_chr as deptid,
       c.areaid_chr as areaid,
       c.bedid_chr as bedid
  from t_bse_patient a
  left outer join t_bse_patientcard b on a.patientid_chr = b.patientid_chr
  left outer join (select a2.patientid_chr,
                          a2.deptid_chr,
                          a2.areaid_chr,
                          a2.bedid_chr,
                          a2.inpatientcount_int,
                          b2.deptname_vchr as deptname,
                          c2.deptname_vchr as areaname,
                          d2.code_chr as bedname
                     from t_opr_bih_register a2
                     left join t_bse_deptdesc b2 on a2.deptid_chr =
                                                    b2.deptid_chr
                     left join t_bse_deptdesc c2 on a2.areaid_chr =
                                                    c2.deptid_chr
                     left join t_bse_bed d2 on a2.bedid_chr = d2.bedid_chr
                    where a2.status_int = 1
                      and a2.pstatus_int <> '3') c on a.patientid_chr =
                                                      c.patientid_chr
 where b.status_int > 0";

            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objServ = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            if (p_strSql != null)
                strSQL += p_strSql;
            long lngRes = 0;
            try
            {
                DataTable dtRecords = new DataTable();
                lngRes = objServ.DoGetDataTable(strSQL, ref dtRecords);
                if (lngRes <= 0 || dtRecords.Rows.Count != 1)
                    return 0;
                objServ.Dispose();
                p_objPatient = new clsPatientInfo_Value();
                p_objPatient.m_StrInPatientID = dtRecords.Rows[0]["INPATIENTID"].ToString().Trim();
                p_objPatient.m_StrPatientID = dtRecords.Rows[0]["PATIENTID"].ToString().Trim();
                p_objPatient.m_StrPatientCardID = dtRecords.Rows[0]["PATIENTCARDID"].ToString().Trim();
                p_objPatient.m_StrOutPatientID = "";
                p_objPatient.m_StrDeptID = dtRecords.Rows[0]["DEPTID"].ToString().Trim();
                p_objPatient.m_StrAreaID = dtRecords.Rows[0]["AREAID"].ToString().Trim();
                p_objPatient.m_StrBedID = dtRecords.Rows[0]["BEDID"].ToString().Trim();

                p_objPatient.m_StrDeptName = dtRecords.Rows[0]["DEPTNAME"].ToString().Trim();
                p_objPatient.m_StrAreaName = dtRecords.Rows[0]["AREANAME"].ToString().Trim();
                p_objPatient.m_StrBedName = dtRecords.Rows[0]["BEDNAME"].ToString().Trim();
                p_objPatient.m_ObjPeopleInfo = new clsPeopleInfo();
                try
                {
                    p_objPatient.m_ObjPeopleInfo.m_DtmBirth = Convert.ToDateTime(dtRecords.Rows[0]["BIRTH"]);
                }
                catch
                {
                    p_objPatient.m_ObjPeopleInfo.m_DtmBirth = DateTime.Parse("1900-1-1");
                }
                try
                {
                    p_objPatient.m_ObjPeopleInfo.m_IntTimes = Convert.ToInt32(dtRecords.Rows[0]["INPATIENTCOUNT"]);
                }
                catch
                {
                    p_objPatient.m_ObjPeopleInfo.m_IntTimes = 0;
                }
                p_objPatient.m_ObjPeopleInfo.m_StrFirstName = dtRecords.Rows[0]["PATIENTNAME"].ToString().Trim();
                p_objPatient.m_ObjPeopleInfo.m_StrHomeAddress = dtRecords.Rows[0]["AREAID"].ToString().Trim();
                p_objPatient.m_ObjPeopleInfo.m_StrIDCard = dtRecords.Rows[0]["IDCARD"].ToString().Trim();
                p_objPatient.m_ObjPeopleInfo.m_StrLastName = p_objPatient.m_ObjPeopleInfo.m_StrFirstName;
                p_objPatient.m_ObjPeopleInfo.m_StrMarried = dtRecords.Rows[0]["MARRIED"].ToString().Trim();
                p_objPatient.m_ObjPeopleInfo.m_StrNation = "";
                p_objPatient.m_ObjPeopleInfo.m_StrNationality = dtRecords.Rows[0]["NATIONALITY"].ToString().Trim();
                p_objPatient.m_ObjPeopleInfo.m_StrNativePlace = dtRecords.Rows[0]["NATIVEPLACE"].ToString().Trim();
                p_objPatient.m_ObjPeopleInfo.m_StrSex = dtRecords.Rows[0]["SEX"].ToString().Trim();
                p_objPatient.m_ObjPeopleInfo.m_StrOccupation = dtRecords.Rows[0]["OCCUPATION"].ToString().Trim();

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objServ.Dispose();
            }
            //返回
            return lngRes;
        }
        /// <summary>
        /// 模糊查找病人姓名，返回病人ID和病人名称数组
        /// </summary>
        /// <param name="p_strLike"></param>
        /// <param name="p_strNameArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientNameByLike(string p_strLike, out DataTable dtResult)
        {
            dtResult = null;
            if (p_strLike == null)
                return -1;
            long lngRes = 0;
            clsHRPTableService objHRPServer = new clsHRPTableService();
            try
            {
                string strSql = @"select t.patientid_chr,t.lastname_vchr from t_bse_patient t where t.lastname_vchr like '%" + p_strLike.Trim() + @"%' order by t.lastname_vchr";
                //DataTable dtResult = new DataTable();

                lngRes = objHRPServer.DoGetDataTable(strSql, ref dtResult);
                //objHRPServer.Dispose();
                if (lngRes <= 0 || dtResult.Rows.Count <= 0)
                    return 0;
                //p_strNameArr = new string[dtResult.Rows.Count, 2];
                //for (int i = 0; i < dtResult.Rows.Count; i++)
                //{
                //    p_strNameArr[i, 0] = dtResult.Rows[i]["PATIENTID_CHR"].ToString().Trim();
                //    p_strNameArr[i, 1] = dtResult.Rows[i]["LASTNAME_VCHR"].ToString().Trim();
                //}

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServer.Dispose();
            }
            //返回
            return lngRes;
        }
        /// <summary>
        /// 模糊查找员工，返回员工ID和员工名称数组
        /// </summary>
        /// <param name="p_strLike"></param>
        /// <param name="p_strNameArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetEmployeeNameByLike(string p_strLike, out DataTable dtResult)
        {
            dtResult = null;
            long lngRes = 0;
            clsHRPTableService objHRPServer = new clsHRPTableService();
            try
            {
                if (p_strLike == null)
                    return -1;
                string strSql = @"select distinct t.empno_chr,t.lastname_vchr from t_bse_employee t inner join t_bse_deptemp a
									on t.empid_chr = a.empid_chr inner join t_bse_deptdesc b on a.deptid_chr = b.deptid_chr where (
									t.empno_chr like '" + p_strLike.Trim() + @"%' or t.lastname_vchr like '%" + p_strLike.Trim() + @"%' or t.pycode_chr like upper('" + p_strLike.Trim() + @"')||'%'
									or t.shortname_chr like '" + p_strLike.Trim() + @"%' or b.shortno_chr like '" + p_strLike.Trim() + @"%') and t.status_int = '1' order by t.lastname_vchr";
                //DataTable dtResult = new DataTable();

                lngRes = objHRPServer.DoGetDataTable(strSql, ref dtResult);
                //objHRPServer.Dispose();
                if (lngRes <= 0 || dtResult.Rows.Count <= 0)
                    return 0;
                //p_strNameArr = new string[dtResult.Rows.Count, 2];
                //for (int i = 0; i < dtResult.Rows.Count; i++)
                //{
                //    p_strNameArr[i, 0] = dtResult.Rows[i]["EMPNO_CHR"].ToString().Trim();
                //    p_strNameArr[i, 1] = dtResult.Rows[i]["LASTNAME_VCHR"].ToString().Trim();
                //}
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServer.Dispose();
            }
            //返回
            return lngRes;

        }
        /// <summary>
        /// 模糊查找床号，返回床号ID和床号名称数组
        /// </summary>
        /// <param name="p_strLike"></param>
        /// <param name="p_strNameArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBedNameByLike(string p_strLike, out DataTable dtResult)
        {
            long lngRes = 0;
            dtResult = null;
            clsHRPTableService objHRPServer = new clsHRPTableService();
            try
            {
                if (p_strLike == null)
                    return -1;
                string strSql = @"select distinct t.bedid_chr,t.code_chr from t_bse_bed t where
				(t.bedid_chr like '" + p_strLike.Trim() + @"%' or t.code_chr like '" + p_strLike.Trim() + @"%' ) and t.status_int <> '5' order by t.code_chr";
                //DataTable dtResult = new DataTable();

                lngRes = objHRPServer.DoGetDataTable(strSql, ref dtResult);
                //objHRPServer.Dispose();
                if (lngRes <= 0 || dtResult.Rows.Count <= 0)
                    return 0;
                //p_strNameArr = new string[dtResult.Rows.Count, 2];
                //for (int i = 0; i < dtResult.Rows.Count; i++)
                //{
                //    p_strNameArr[i, 0] = dtResult.Rows[i]["BEDID_CHR"].ToString().Trim();
                //    p_strNameArr[i, 1] = dtResult.Rows[i]["CODE_CHR"].ToString().Trim();
                //}

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServer.Dispose();
            }
            //返回
            return lngRes;
        }
        /// <summary>
        /// 模糊查找科室病区
        /// </summary>
        /// <param name="p_strLike"></param>
        /// <param name="p_strDeptArr"></param>
        /// <param name="p_strDeptID">取病区的科室ID，默认时传入空值</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeptAreaByLike(string p_strLike, out clsDeptAreaInfo_Value[] p_objDeptArr, string p_strDeptID)
        {
            p_objDeptArr = null;
            DataTable dtResult = m_dtbGetDept(p_strLike, p_strDeptID);
            if (dtResult == null || dtResult.Rows.Count == 0)
                return 0;
            p_objDeptArr = new clsDeptAreaInfo_Value[dtResult.Rows.Count];
            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                p_objDeptArr[i] = new clsDeptAreaInfo_Value();
                p_objDeptArr[i].m_StrDeptID = dtResult.Rows[i]["SHORTNO_CHR"].ToString().Trim();
                p_objDeptArr[i].m_StrDeptName = dtResult.Rows[i]["DEPTNAME_VCHR"].ToString().Trim();
                p_objDeptArr[i].m_StrDeptState = dtResult.Rows[i]["INPATIENTOROUTPATIENT_INT"].ToString().Trim();
                p_objDeptArr[i].m_StrAttributeID = dtResult.Rows[i]["ATTRIBUTEID"].ToString().Trim();
            }
            return 1;
        }
        /// <summary>
        /// 返回科室ID和科室名称数组
        /// </summary>
        /// <param name="p_strLike"></param>
        /// <param name="p_strDeptArr"></param>
        /// <param name="p_strDeptID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeptAreaByLike(string p_strLike, out DataTable dtResult, string p_strDeptID)
        {
            dtResult = m_dtbGetDept(p_strLike, p_strDeptID);
            if (dtResult == null || dtResult.Rows.Count == 0)
                return 0;
            //p_strDeptArr = new string[dtResult.Rows.Count, 2];
            //for (int i = 0; i < dtResult.Rows.Count; i++)
            //{
            //    p_strDeptArr[i, 0] = dtResult.Rows[i]["SHORTNO_CHR"].ToString().Trim();
            //    p_strDeptArr[i, 1] = dtResult.Rows[i]["DEPTNAME_VCHR"].ToString().Trim();
            //}
            return 1;
        }
        /// <summary>
        /// 取得科室
        /// </summary>
        /// <param name="p_strLike"></param>
        /// <param name="p_strDeptID"></param>
        /// <returns></returns>
        [AutoComplete]
        private DataTable m_dtbGetDept(string p_strLike, string p_strDeptID)
        {
            DataTable dtResult = new DataTable();
            dtResult = null;
            clsHRPTableService objHRPServer = new clsHRPTableService();
            try
            {
                if (p_strLike == null)
                    return null;
                string strSql = @"select t.deptid_chr,
       t.modify_dat,
       t.deptname_vchr,
       t.category_int,
       t.inpatientoroutpatient_int,
       t.operatorid_chr,
       t.address_vchr,
       t.pycode_chr,
       t.attributeid,
       t.parentid,
       t.createdate_dat,
       t.status_int,
       t.deactivate_dat,
       t.wbcode_chr,
       t.code_vchr,
       t.extendid_vchr,
       t.shortno_chr,
       t.stdbed_count_int,
       t.putmed_int
  from t_bse_deptdesc t";
                if (p_strLike.Trim() != "")
                    strSql += @" where (t.deptid_chr like '" + p_strLike.Trim() + @"%' or t.deptname_vchr like '%" + p_strLike.Trim() + @"%' 
								or (t.pycode_chr like upper('" + p_strLike.Trim() + @"')||'%') or (t.wbcode_chr like upper('" + p_strLike.Trim() + @"')||'%')
								or t.shortno_chr like '" + p_strLike.Trim() + @"%') and t.status_int = '1' and t.category_int = '0'";
                else
                    strSql += @" where t.status_int = '1' and t.category_int = '0'";
                if (p_strDeptID != null)
                    if (p_strDeptID.Trim() != "")
                        strSql += @" and t.attributeid = '0000003' and t.parentid = '" + p_strDeptID.Trim() + "'";
                strSql += @"order by deptname_vchr";


                long lngRes = objHRPServer.DoGetDataTable(strSql, ref dtResult);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServer.Dispose();
            }
            //返回
            return dtResult;

        }
        /// <summary>
        /// 根据病人ID获取住院号
        /// </summary>
        /// <param name="p_strPatient"></param>
        /// <returns></returns>
        [AutoComplete]
        public string m_strGetInPatientIDByPatientID(string p_strPatient)
        {
            string strRes = "";
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                if (p_strPatient == null || p_strPatient == "")
                    return "";
                string strSql = @"select t.inpatientid_chr from t_opr_bih_register t where t.patientid_chr = ? and t.status_int = 1";

                DataTable dtResult = new DataTable();
                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strPatient.Trim();

                long lngRes = objTabService.lngGetDataTableWithParameters(strSql, ref dtResult, objDPArr);

                if (lngRes > 0 && dtResult.Rows.Count == 1)
                {
                    return dtResult.Rows[0]["INPATIENTID"].ToString().Trim();
                }
                else
                    return "";

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
            //返回
            return strRes;
        }
        /// <summary>
        /// 根据住院号取病人信息
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_objPatient"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientByName(string p_strInPatientName, out clsPatientInfo_Value[] p_objPatientArr)
        {
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objServ = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            long lngRes = 0;
            p_objPatientArr = null;
            try
            {
                p_objPatientArr = null;
                if (p_strInPatientName == null || p_strInPatientName == "")
                    return -1;
                string strSql = @"select a.patientid_chr as patientid,
       a.inpatientid_chr as inpatientid,
       a.lastname_vchr as patientname,
       a.married_chr as married,
       a.birthplace_vchr as birthplace,
       a.sex_chr as sex,
       a.idcard_chr as idcard,
       a.homeaddress_vchr as homeaddress,
       a.occupation_vchr as occupation,
       a.birth_dat as birth,
       a.nativeplace_vchr as nativeplace,
       a.nationality_vchr as nationality,
       b.patientcardid_chr as patientcardid,
       c.deptname,
       c.areaname,
       c.bedname,
       c.inpatientcount_int as inpatientcount,
       c.deptid_chr as deptid,
       c.areaid_chr as areaid,
       c.bedid_chr as bedid
  from t_bse_patient a
  left outer join t_bse_patientcard b on a.patientid_chr = b.patientid_chr
  left outer join (select a2.patientid_chr,                   
                          a2.deptid_chr,  
                          a2.areaid_chr,                        
                          a2.bedid_chr,                          
                          a2.inpatientcount_int,                          
                          b2.deptname_vchr as deptname,
                          c2.deptname_vchr as areaname,
                          d2.code_chr      as bedname
                     from t_opr_bih_register a2
                     left join t_bse_deptdesc b2 on a2.deptid_chr =
                                                    b2.deptid_chr
                     left join t_bse_deptdesc c2 on a2.areaid_chr =
                                                    c2.deptid_chr
                     left join t_bse_bed d2 on a2.bedid_chr = d2.bedid_chr
                    where a2.status_int = 1) c on a.patientid_chr =
                                                  c.patientid_chr
 where b.status_int > 0
   and a.lastname_vchr = ?";

                DataTable dtRecords = new DataTable();
                IDataParameter[] objDPArr = null;
                objServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strInPatientName.Trim();

                lngRes = objServ.lngGetDataTableWithParameters(strSql, ref dtRecords, objDPArr);
                if (lngRes <= 0 || dtRecords.Rows.Count <= 0)
                    return 0;

                p_objPatientArr = new clsPatientInfo_Value[dtRecords.Rows.Count];
                for (int i = 0; i < dtRecords.Rows.Count; i++)
                {
                    p_objPatientArr[i] = new clsPatientInfo_Value();
                    p_objPatientArr[i].m_StrInPatientID = dtRecords.Rows[i]["INPATIENTID"].ToString().Trim();
                    p_objPatientArr[i].m_StrPatientID = dtRecords.Rows[i]["PATIENTID"].ToString().Trim();
                    p_objPatientArr[i].m_StrPatientCardID = dtRecords.Rows[i]["PATIENTCARDID"].ToString().Trim();
                    p_objPatientArr[i].m_StrOutPatientID = "";
                    p_objPatientArr[i].m_StrDeptID = dtRecords.Rows[i]["DEPTID"].ToString().Trim();
                    p_objPatientArr[i].m_StrAreaID = dtRecords.Rows[i]["AREAID"].ToString().Trim();
                    p_objPatientArr[i].m_StrBedID = dtRecords.Rows[i]["BEDID"].ToString().Trim();

                    p_objPatientArr[i].m_StrDeptName = dtRecords.Rows[i]["DEPTNAME"].ToString().Trim();
                    p_objPatientArr[i].m_StrAreaName = dtRecords.Rows[i]["AREANAME"].ToString().Trim();
                    p_objPatientArr[i].m_StrBedName = dtRecords.Rows[i]["BEDNAME"].ToString().Trim();
                    p_objPatientArr[i].m_ObjPeopleInfo = new clsPeopleInfo();
                    try
                    {
                        p_objPatientArr[i].m_ObjPeopleInfo.m_DtmBirth = Convert.ToDateTime(dtRecords.Rows[i]["BIRTH"]);
                    }
                    catch { p_objPatientArr[i].m_ObjPeopleInfo.m_DtmBirth = DateTime.Parse("1900-1-1"); }
                    try
                    {
                        p_objPatientArr[i].m_ObjPeopleInfo.m_IntTimes = Convert.ToInt32(dtRecords.Rows[i]["INPATIENTCOUNT"]);
                    }
                    catch { p_objPatientArr[i].m_ObjPeopleInfo.m_IntTimes = 0; }
                    p_objPatientArr[i].m_ObjPeopleInfo.m_StrFirstName = dtRecords.Rows[i]["PATIENTNAME"].ToString().Trim();
                    p_objPatientArr[i].m_ObjPeopleInfo.m_StrHomeAddress = dtRecords.Rows[i]["AREAID"].ToString().Trim();
                    p_objPatientArr[i].m_ObjPeopleInfo.m_StrIDCard = dtRecords.Rows[i]["IDCARD"].ToString().Trim();
                    p_objPatientArr[i].m_ObjPeopleInfo.m_StrLastName = p_objPatientArr[i].m_ObjPeopleInfo.m_StrFirstName;
                    p_objPatientArr[i].m_ObjPeopleInfo.m_StrMarried = dtRecords.Rows[i]["MARRIED"].ToString().Trim();
                    p_objPatientArr[i].m_ObjPeopleInfo.m_StrNation = "";
                    p_objPatientArr[i].m_ObjPeopleInfo.m_StrNationality = dtRecords.Rows[i]["NATIONALITY"].ToString().Trim();
                    p_objPatientArr[i].m_ObjPeopleInfo.m_StrNativePlace = dtRecords.Rows[i]["NATIVEPLACE"].ToString().Trim();
                    p_objPatientArr[i].m_ObjPeopleInfo.m_StrSex = dtRecords.Rows[i]["SEX"].ToString().Trim();
                    p_objPatientArr[i].m_ObjPeopleInfo.m_StrOccupation = dtRecords.Rows[i]["OCCUPATION"].ToString().Trim();
                }


            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objServ.Dispose();
            }
            //返回
            return lngRes;
        }

    }
}
