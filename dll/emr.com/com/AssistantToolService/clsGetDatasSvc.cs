using System;
using System.Collections;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
//using com.digitalwave.PublicMiddleTier;

namespace com.digitalwave.EMR.MakeDataToPDA
{
	/// <summary>
	/// clsGetDatas 的摘要说明。
	/// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsGetDatasSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		/// <summary>
		/// 三测表
		/// </summary>
        /// <param name="objDeptVO"></param>
        /// <param name="p_dtbResult"></param>
		/// <returns></returns>
        [AutoComplete]
        public long m_lngGetThreeMeasureRecord(clsDepart_VO objDeptVO, out DataTable p_dtbResult)
		{
			long lngRes=0;
            p_dtbResult = null;
            if (objDeptVO == null)
                return -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSql = @"select p.patientid_chr,
                                       a.inpatientdate,
                                       a.temperaturexml,
                                       a.pulsexml,
                                       a.breathxml,
                                       a.pressurexml,
                                       a.pressure2xml,
                                       a.dejectaxml,
                                       a.peexml,
                                       a.outstreamxml,
                                       a.inputxml,
                                       a.weightxml,
                                       a.eventxml,
                                       a.specialdatexml,
                                       a.otherxml,
                                       a.skintestxml,
                                       a.inpatientid,
                                       to_date(a.createdate) as recorddate,
                                       to_date(a.createdate) - to_date(p.hisinpatientdate) + 1 as indays,
                                       p.hisinpatientid_chr,
                                       p.hisinpatientdate
                                  from threemeasurerecord a
                                 inner join (select reg.deptid_chr,
                                                    '" + objDeptVO.m_strDEPTNAME_VCHR + @"' deptname_vchr, 
                                                    reg.patientid_chr,
                                                    here.emrinpatientid,
                                                    here.emrinpatientdate,
                                                    here.hisinpatientid_chr,
                                                    here.hisinpatientdate
                                               from t_opr_bih_register    reg,
                                                    t_bse_bed             bed,
                                                    t_bse_hisemr_relation here
                                              where reg.deptid_chr = ?
                                                and reg.status_int = 1
                                                and reg.pstatus_int <> 3
                                                and reg.bedid_chr = bed.bedid_chr
                                                and here.registerid_chr = reg.registerid_chr
                                                and bed.status_int = 2) p on a.inpatientid =
                                                                             p.emrinpatientid
                                                                         and a.inpatientdate =
                                                                             p.emrinpatientdate
                                 order by a.inpatientid, a.inpatientdate, a.createdate";
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = objDeptVO.m_strDEPTID_CHR;
                p_dtbResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref p_dtbResult, objDPArr);

            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLog = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLog.LogError(ex);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
			
			return lngRes;
		}

		/// <summary>
		/// 住院病历
		/// </summary>
		/// <param name="p_strEmpID"></param>
		/// <param name="p_dtbResult"></param>
		/// <returns></returns>
        [AutoComplete]
        public long m_lngGetInHospitalHistoryRecord(string p_strEmpID, out DataTable p_dtbResult)
		{
			p_dtbResult = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
                string strSql = @"select distinct patient.patientid_chr,
                                        a.inpatientid,
                                        a.inpatientdate,
                                        a.representor as representor,
                                        a.credibility as credibility,
                                        f_getempnamebyno(a.createuserid) as createname,
                                        b.maindescription as maindescription,
                                        b.currentstatus as currentstatus,
                                        b.beforetimestatus as beforetimestatus,
                                        b.ownhistory as ownhistory,
                                        b.marriagehistory as marriagehistory,
                                        b.firstcatamenia as firstcatamenia,
                                        b.catamenialasttime as catamenialasttime,
                                        b.catameniacycle as catameniacycle,
                                        b.lastcatameniatime as lastcatameniatime,
                                        b.catameniacase,
                                        b.catameniahistory as catameniahistory,
                                        b.familyhistory as familyhistory,
                                        b.temperature as temperature,
                                        b.pulse as pulse,
                                        b.breath as breath,
                                        b.sys || '/' || b.dia as bloodpressure,
                                        b.medical as medical,
                                        b.professionalcheck as professionalcheck,
                                        b.labcheck as labcheck,
                                        ss1.primarydiagnose as primarydiagnose,
                                        b.modifydiagnose as modifydiagnose,
                                        f_getempnamebyno(modifydiagnosedoctorid) as modifydiagnosedoctor,
                                        a.modifydiagnosedate as modifydiagnosedate,
                                        b.adddiagnose as adddiagnose,
                                        f_getempnamebyno(adddiagnosedoctorid) as adddiagnosedoctor,
                                        a.adddiagnosedate as adddiagnosedate,
                                        f_getempnamebyno(diretdoctor) as diretdoctor,
                                        f_getempnamebyno(chargedoctor) as chargedoctor,
                                        patient.hisinpatientid_chr,
                                        patient.hisinpatientdate
                          from inpatientcasehistory_history a
                         inner join ipcasehistory_historycontent b on b.inpatientid = a.inpatientid
                                                                  and b.inpatientdate =
                                                                      a.inpatientdate
                                                                  and b.opendate = a.opendate
                         inner join (select reg.deptid_chr,
													dep.deptname_vchr,
													bed.code_chr,
                                                    reg.patientid_chr,
                                                    here.emrinpatientid,
                                                    here.emrinpatientdate,
                                                    here.hisinpatientid_chr,
                                                    here.hisinpatientdate
											from t_opr_bih_register reg,
													t_bse_bed          bed,
													t_bse_deptdesc     dep,
                                                    t_bse_hisemr_relation here
											where reg.deptid_chr in
													(select t.deptid_chr
													from t_bse_deptemp t
													where t.empid_chr = ?)
												and reg.status_int = 1
												and reg.pstatus_int <> 3
												and reg.deptid_chr = dep.deptid_chr
												and reg.bedid_chr = bed.bedid_chr
                                                and here.registerid_chr = reg.registerid_chr
												and bed.status_int = 2) patient on a.inpatientid =
                                                                           patient.emrinpatientid
                                                                       and a.inpatientdate =
                                                                           patient.emrinpatientdate
                          left outer join (select pridia.inpatientid,
                                                  pridia.inpatientdate,
                                                  pridia.opendate,
                                                  pridia.lastmodifydate,
                                                  pridia.indexid,
                                                  pridia.primarydiagnose
                                             from ipcasehistcont_primarydiagnose pridia
                                            where pridia.lastmodifydate =
                                                  (select max(lastmodifydate)
                                                     from ipcasehistcont_primarydiagnose
                                                    where inpatientid = pridia.inpatientid
                                                      and inpatientdate = pridia.inpatientdate
                                                      and opendate = pridia.opendate)) ss1 on ss1.inpatientid =
                                                                                              a.inpatientid
                                                                                          and ss1.inpatientdate =
                                                                                              a.inpatientdate
                                                                                          and ss1.opendate =
                                                                                              a.opendate
                         where a.status = 0
                           and b.lastmodifydate = (select max(lastmodifydate)
                                                     from ipcasehistory_historycontent
                                                    where inpatientid = a.inpatientid
                                                      and inpatientdate = a.inpatientdate
                                                      and opendate = a.opendate)";
				
				IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strEmpID;
				//
				//在此对参数赋值
				//
				lngRes = objHRPServ.lngGetDataTableWithParameters(strSql,ref p_dtbResult,objDPArr);
			}
			catch(Exception ex)
			{
				com.digitalwave.Utility.clsLogText objLog = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLog.LogError(ex);
			}
            finally
            {
                //objHRPServ.Dispose();
            }
			return lngRes;
		}

		/// <summary>
		/// ICU护理记录
		/// </summary>
		/// <param name="p_strEmpID"></param>
		/// <param name="p_dtbResult"></param>
		/// <returns></returns>
        [AutoComplete]
        public long m_lngGetICUNurseRecord(string p_strEmpID, out DataTable p_dtbResult)
		{
			p_dtbResult = null;
            long lngRes = 0;
             clsHRPTableService objHRPServ = new clsHRPTableService();
             try
             {
                 string strSql = @"select patient.patientid_chr,
                                   a.inpatientid,
                                   a.inpatientdate,
                                   a.diseaseid,
                                   a.createdate,
                                   b.inamountitem_right,
                                   b.inamountstandby_right,
                                   b.inamountfact_right,
                                   b.outemiction_right,
                                   b.custom1_right,
                                   b.custom2_right,
                                   b.temperature_right,
                                   b.hr_right,
                                   b.respiration_right,
                                   b.bloodpressurea_right || '/' || b.bloodpressures_right as bloodpressure,
                                   b.a_right,
                                   b.sp02_right,
                                   b.generalinstance_right,
                                   b.summary_right,
                                   emp.lastname_vchr nursesign,
                                   a.custom1name,
                                   a.custom2name,
                                   a.custom3name,
                                   a.custom4name,
                                   a.sumintime,
                                   a.sumin,
                                   a.sumouttime,
                                   a.sumout,
                                   patient.hisinpatientid_chr,
                                   patient.hisinpatientdate
                              from icunurserecord_gxrecord a
                             inner join icunurserecord_gxcontent b on b.inpatientid = a.inpatientid
                                                                  and b.inpatientdate = a.inpatientdate
                                                                  and b.opendate = a.opendate
                             inner join t_bse_employee emp on emp.empno_chr = a.createuserid
                             inner join (select reg.deptid_chr,
													dep.deptname_vchr,
													bed.code_chr,
                                                    reg.patientid_chr,
                                                    here.emrinpatientid,
                                                    here.emrinpatientdate,
                                                    here.hisinpatientid_chr,
                                                    here.hisinpatientdate
											from t_opr_bih_register reg,
													t_bse_bed          bed,
													t_bse_deptdesc     dep,
                                                    t_bse_hisemr_relation here
											where reg.deptid_chr in
													(select t.deptid_chr
													from t_bse_deptemp t
													where t.empid_chr = ?)
												and reg.status_int = 1
												and reg.pstatus_int <> 3
												and reg.deptid_chr = dep.deptid_chr
												and reg.bedid_chr = bed.bedid_chr
                                                and here.registerid_chr = reg.registerid_chr
												and bed.status_int = 2) patient on a.inpatientid =
                                                                           patient.emrinpatientid
                                                                       and a.inpatientdate =
                                                                           patient.emrinpatientdate
                             where a.status = 0
                               and b.modifydate = (select max(modifydate)
                                                     from icunurserecord_gxcontent
                                                    where inpatientid = a.inpatientid
                                                      and inpatientdate = a.inpatientdate
                                                      and opendate = a.opendate)";

                 IDataParameter[] objDPArr = null;
                 objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                 objDPArr[0].Value = p_strEmpID;
                 //
                 //在此对参数赋值
                 //
                 lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref p_dtbResult, objDPArr);
             }
             catch (Exception ex)
             {
                 com.digitalwave.Utility.clsLogText objLog = new com.digitalwave.Utility.clsLogText();
                 bool blnRes = objLog.LogError(ex);
             }
             finally
             {
                 //objHRPServ.Dispose();
             }
			return lngRes;
		}

		/// <summary>
		/// 危重患者护理记录
		/// </summary>
		/// <param name="p_strEmpID"></param>
		/// <param name="p_dtbResult"></param>
		/// <returns></returns>
        [AutoComplete]
        public long m_lngGetIntensiveTendRecord(string p_strEmpID, out DataTable p_dtbResult)
		{
			p_dtbResult = null;
            long lngRes = 0;
             clsHRPTableService objHRPServ = new clsHRPTableService();
             try
             {
                 string strSql = @"select patient.patientid_chr,
                                   a.inpatientid,
                                   a.inpatientdate,
                                   a.diagnose,
                                   a.recorddate,
                                   b.initem_right,
                                   b.infact_right,
                                   b.outpiss_right,
                                   b.outstool_right,
                                   b.checkt_right,
                                   b.checkp_right,
                                   b.checkr_right,
                                   b.checkbpa_right || '/' || b.checkbps_right as bloodpressure,
                                   b.custom1_right,
                                   b.custom2_right,
                                   b.custom3_right,
                                   b.custom4_right,
                                   emp.lastname_vchr nursesign,
                                   a.custom1name,
                                   a.custom2name,
                                   a.custom3name,
                                   a.custom4name,
                                   a.stat_status,
                                   a.sumintime,
                                   a.sumin,
                                   a.sumouttime,
                                   a.sumout,
                                   substr(a.stat_status, length(a.stat_status), 1) as classno,
                                   patient.hisinpatientid_chr,
                                   patient.hisinpatientdate
                              from t_emr_intensivetendrecord_gx a
                             inner join t_emr_intensivetendcontent_gx b on b.inpatientid =
                                                                           a.inpatientid
                                                                       and b.inpatientdate =
                                                                           a.inpatientdate
                                                                       and b.opendate = a.opendate
                             inner join t_bse_employee emp on emp.empno_chr = a.nursesignid
                             inner join (select reg.deptid_chr,
													dep.deptname_vchr,
													bed.code_chr,
                                                    reg.patientid_chr,
                                                    here.emrinpatientid,
                                                    here.emrinpatientdate,
                                                    here.hisinpatientid_chr,
                                                    here.hisinpatientdate
											from t_opr_bih_register reg,
													t_bse_bed          bed,
													t_bse_deptdesc     dep,
                                                    t_bse_hisemr_relation here
											where reg.deptid_chr in
													(select t.deptid_chr
													from t_bse_deptemp t
													where t.empid_chr = ?)
												and reg.status_int = 1
												and reg.pstatus_int <> 3
												and reg.deptid_chr = dep.deptid_chr
												and reg.bedid_chr = bed.bedid_chr
                                                and here.registerid_chr = reg.registerid_chr
												and bed.status_int = 2) patient on a.inpatientid =
                                                                           patient.emrinpatientid
                                                                       and a.inpatientdate =
                                                                           patient.emrinpatientdate
                             where a.status = 0
                               and b.modifydate = (select max(modifydate)
                                                     from t_emr_intensivetendcontent_gx
                                                    where inpatientid = a.inpatientid
                                                      and inpatientdate = a.inpatientdate
                                                      and opendate = a.opendate)";

                 IDataParameter[] objDPArr = null;
                 objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                 objDPArr[0].Value = p_strEmpID;
                 //
                 //在此对参数赋值
                 //
                 lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref p_dtbResult, objDPArr);
             }
             catch (Exception ex)
             {
                 com.digitalwave.Utility.clsLogText objLog = new com.digitalwave.Utility.clsLogText();
                 bool blnRes = objLog.LogError(ex);
             }
             finally
             {
                 //objHRPServ.Dispose();
             }
			return lngRes;
		}

		/// <summary>
		/// 危重患者护理记录(病情记录)
		/// </summary>
		/// <param name="p_strEmpID"></param>
		/// <param name="p_dtbResult"></param>
		/// <returns></returns>
        [AutoComplete]
        public long m_lngGetIntensiveTendDetailRecord(string p_strEmpID, out DataTable p_dtbResult)
		{
			p_dtbResult = null;
            long lngRes = 0;
             clsHRPTableService objHRPServ = new clsHRPTableService();
             try
             {
                 string strSql = @"select patient.patientid_chr,
                                   a.inpatientid,
                                   a.inpatientdate,
                                   a.detailrecorddate,
                                   a.detailcontent,
                                   emp.lastname_vchr detailsign,
                                   a.stat_status,
                                   substr(a.stat_status, length(a.stat_status), 1) as classno,
                                   patient.hisinpatientid_chr,
                                   patient.hisinpatientdate
                              from t_emr_intensivetenddetail_gx a
                             inner join t_bse_employee emp on emp.empno_chr = a.createuserid
                             inner join (select reg.deptid_chr,
													dep.deptname_vchr,
													bed.code_chr,
                                                    reg.patientid_chr,
                                                    here.emrinpatientid,
                                                    here.emrinpatientdate,
                                                    here.hisinpatientid_chr,
                                                    here.hisinpatientdate
											from t_opr_bih_register reg,
													t_bse_bed          bed,
													t_bse_deptdesc     dep,
                                                    t_bse_hisemr_relation here
											where reg.deptid_chr in
													(select t.deptid_chr
													from t_bse_deptemp t
													where t.empid_chr = ?)
												and reg.status_int = 1
												and reg.pstatus_int <> 3
												and reg.deptid_chr = dep.deptid_chr
												and reg.bedid_chr = bed.bedid_chr
                                                and here.registerid_chr = reg.registerid_chr
												and bed.status_int = 2) patient on a.inpatientid =
                                                                           patient.emrinpatientid
                                                                       and a.inpatientdate =
                                                                           patient.emrinpatientdate
                             where a.status = 0";

                 IDataParameter[] objDPArr = null;
                 objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                 objDPArr[0].Value = p_strEmpID;
                 //
                 //在此对参数赋值
                 //
                 lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref p_dtbResult, objDPArr);
             }
             catch (Exception ex)
             {
                 com.digitalwave.Utility.clsLogText objLog = new com.digitalwave.Utility.clsLogText();
                 bool blnRes = objLog.LogError(ex);
             }
             finally
             {
                 //objHRPServ.Dispose();
             }
			return lngRes;
		}


		/// <summary>
		/// 一般护理记录
		/// </summary>
		/// <param name="p_strEmpID"></param>
		/// <param name="p_dtbResult"></param>
		/// <returns></returns>
        [AutoComplete]
        public long m_lngGetGeneralNurseRecord(string p_strEmpID, out DataTable p_dtbResult)
		{
			p_dtbResult = null;
            long lngRes = 0;
             clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
                string strSql = @"select patient.patientid_chr,
                                       a.inpatientid,
                                       a.inpatientdate,
                                       a.createdate,
                                       b.recordcontent_right,
                                       emp.lastname_vchr createuser,
                                       patient.hisinpatientid_chr,
                                       patient.hisinpatientdate
                                  from generalnurserecord a
                                 inner join generalnurserecordcontent b on b.inpatientid = a.inpatientid
                                                                       and b.inpatientdate =
                                                                           a.inpatientdate
                                                                       and b.opendate = a.opendate
                                 inner join t_bse_employee emp on emp.empno_chr = a.createuserid
                                 inner join (select reg.deptid_chr,
													dep.deptname_vchr,
													bed.code_chr,
                                                    reg.patientid_chr,
                                                    here.emrinpatientid,
                                                    here.emrinpatientdate,
                                                    here.hisinpatientid_chr,
                                                    here.hisinpatientdate
											from t_opr_bih_register reg,
													t_bse_bed          bed,
													t_bse_deptdesc     dep,
                                                    t_bse_hisemr_relation here
											where reg.deptid_chr in
													(select t.deptid_chr
													from t_bse_deptemp t
													where t.empid_chr = ?)
												and reg.status_int = 1
												and reg.pstatus_int <> 3
												and reg.deptid_chr = dep.deptid_chr
												and reg.bedid_chr = bed.bedid_chr
                                                and here.registerid_chr = reg.registerid_chr
												and bed.status_int = 2) patient on a.inpatientid =
                                                                           patient.emrinpatientid
                                                                       and a.inpatientdate =
                                                                           patient.emrinpatientdate
                                 where a.status = 0
                                   and b.modifydate = (select max(modifydate)
                                                         from generalnurserecordcontent
                                                        where inpatientid = a.inpatientid
                                                          and inpatientdate = a.inpatientdate
                                                          and opendate = a.opendate)";
				
				IDataParameter[] objDPArr = null;
				objHRPServ.CreateDatabaseParameter(1,out objDPArr);
                objDPArr[0].Value = p_strEmpID;
				//
				//在此对参数赋值
				//
				lngRes = objHRPServ.lngGetDataTableWithParameters(strSql,ref p_dtbResult,objDPArr);
			}
			catch(Exception ex)
			{
				com.digitalwave.Utility.clsLogText objLog = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLog.LogError(ex);
			}
            finally
            {
                //objHRPServ.Dispose();
            }
			return lngRes;
		}

		/// <summary>
		/// 一般患者护理记录
		/// </summary>
		/// <param name="p_strEmpID"></param>
		/// <param name="p_dtbResult"></param>
		/// <returns></returns>
        [AutoComplete]
        public long m_lngGetGeneralNurse_GXRecord(string p_strEmpID, out DataTable p_dtbResult)
		{
			p_dtbResult = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSql = @"select patient.patientid_chr,
                                   a.inpatientid,
                                   a.inpatientdate,
                                   a.recorddate,
                                   b.temperature_right,
                                   b.pulse_right,
                                   b.respiration_right,
                                   b.heartrate_right,
                                   b.bloodpressures_right || '/' || b.bloodpressurea_right as bloodpressure,
                                   emp.lastname_vchr createuser,
                                   a.class,
                                   substr(a.class, length(a.class), 1) as classno,
                                   patient.hisinpatientid_chr,
                                   patient.hisinpatientdate
                              from generalnurserecord_gxrecord a
                             inner join generalnurserecord_gxcontent b on b.inpatientid = a.inpatientid
                                                                      and b.inpatientdate =
                                                                          a.inpatientdate
                                                                      and b.opendate = a.opendate
                             inner join t_bse_employee emp on emp.empno_chr = a.createuserid
                             inner join (select reg.deptid_chr,
													dep.deptname_vchr,
													bed.code_chr,
                                                    reg.patientid_chr,
                                                    here.emrinpatientid,
                                                    here.emrinpatientdate,
                                                    here.hisinpatientid_chr,
                                                    here.hisinpatientdate
											from t_opr_bih_register reg,
													t_bse_bed          bed,
													t_bse_deptdesc     dep,
                                                    t_bse_hisemr_relation here
											where reg.deptid_chr in
													(select t.deptid_chr
													from t_bse_deptemp t
													where t.empid_chr = ?)
												and reg.status_int = 1
												and reg.pstatus_int <> 3
												and reg.deptid_chr = dep.deptid_chr
												and reg.bedid_chr = bed.bedid_chr
                                                and here.registerid_chr = reg.registerid_chr
												and bed.status_int = 2) patient on a.inpatientid =
                                                                           patient.emrinpatientid
                                                                       and a.inpatientdate =
                                                                           patient.emrinpatientdate
                             where a.status = 0
                               and b.modifydate = (select max(modifydate)
                                                     from generalnurserecord_gxcontent
                                                    where inpatientid = a.inpatientid
                                                      and inpatientdate = a.inpatientdate
                                                      and opendate = a.opendate)";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strEmpID;
                //
                //在此对参数赋值
                //
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref p_dtbResult, objDPArr);
            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLog = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLog.LogError(ex);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
			return lngRes;
		}

		/// <summary>
		/// 一般患者护理记录(病情记录)
		/// </summary>
		/// <param name="p_strEmpID"></param>
		/// <param name="p_dtbResult"></param>
		/// <returns></returns>
        [AutoComplete]
        public long m_lngGetGeneralNurseDetail_GXRecord(string p_strEmpID, out DataTable p_dtbResult)
		{
			p_dtbResult = null;
			long lngRes = 0;
             clsHRPTableService objHRPServ = new clsHRPTableService();
             try
             {
                 string strSql = @"select patient.patientid_chr,
										a.inpatientid,
										a.inpatientdate,
										a.recorddate,
										a.recordcontent_right,
										emp.lastname_vchr as createuser,a.createuserid,
										a.class,
										substr(a.class,length(a.class),1) as classno,
                                        patient.hisinpatientid_chr,
                                        patient.hisinpatientdate
									from generalnurserecord_gxdetail a
                                    inner join t_bse_employee emp on a.createuserid = emp.empno_chr
									inner join (select reg.deptid_chr,
													dep.deptname_vchr,
													bed.code_chr,
                                                    reg.patientid_chr,
                                                    here.emrinpatientid,
                                                    here.emrinpatientdate,
                                                    here.hisinpatientid_chr,
                                                    here.hisinpatientdate
											from t_opr_bih_register reg,
													t_bse_bed          bed,
													t_bse_deptdesc     dep,
                                                    t_bse_hisemr_relation here
											where reg.deptid_chr in
													(select t.deptid_chr
													from t_bse_deptemp t
													where t.empid_chr = ?)
												and reg.status_int = 1
												and reg.pstatus_int <> 3
												and reg.deptid_chr = dep.deptid_chr
												and reg.bedid_chr = bed.bedid_chr
                                                and here.registerid_chr = reg.registerid_chr
												and bed.status_int = 2) patient on a.inpatientid =
                                                                           patient.emrinpatientid
                                                                       and a.inpatientdate =
                                                                           patient.emrinpatientdate
									where a.status = 0";

                 IDataParameter[] objDPArr = null;
                 objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                 objDPArr[0].Value = p_strEmpID;
                 //
                 //在此对参数赋值
                 //
                 lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref p_dtbResult, objDPArr);
             }
             catch (Exception ex)
             {
                 com.digitalwave.Utility.clsLogText objLog = new com.digitalwave.Utility.clsLogText();
                 bool blnRes = objLog.LogError(ex);
             }
             finally
             {
                 //objHRPServ.Dispose();
             }
			return lngRes;
		}

        #region 导出医嘱已移到HIS中间件
        /*
		#region 根据病人ID获取医嘱信息
		/// <summary>
		/// 根据病人ID获取医嘱信息
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strPatientID">病人ID</param>
		/// <param name="p_decVisitID">住院标识</param>
		/// <param name="p_strSQL">医嘱查询筛选语句</param>
		/// <param name="p_strOrderBy">排序语句</param>
		/// <param name="p_objResultArr">返回医嘱信息</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetPatientOrderByID(string p_strDepartID,out clsDoctorOrder_VO[] p_objResultArr)
		{
			p_objResultArr = new clsDoctorOrder_VO[0];
			long lngRes = 0;

			string strSQL = @"SELECT   t1.*, t3.order_class_code, t3.order_class_name,
         t4.order_status AS cmporderstatus, t4.perform_schedule,
         TO_NUMBER (t1.dosage) AS dosage_num,t5.admission_date_time,
         t6.inp_no
    FROM doctor_orders t1,
         order_clinic_classmap t2,
         order_class_dict t3,
         orders t4,
         pats_in_hospital t5,
         pat_master_index t6
   WHERE t5.dept_code in("+p_strDepartID+@")
     AND t1.patient_id = t5.patient_id
     AND t1.visit_id = t5.visit_id
     AND t1.order_class = t2.item_class_code(+)
     AND t2.order_class_code = t3.order_class_code(+)
     AND t1.patient_id = t4.patient_id(+)
     AND t1.visit_id = t4.visit_id(+)
     AND t1.related_order_no = t4.order_no(+)
     AND t1.related_order_sub_no = t4.order_sub_no(+)
     AND t1.patient_id = t6.patient_id(+)
ORDER BY t1.patient_id,
         t1.repeat_indicator DESC,
         cmporderstatus,
         t1.order_no,
         t1.order_sub_no";
				com.digitalwave.iCare.middletier.HRPService_Orders.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService_Orders.clsHRPTableService();
                try
                {
                    DataTable dtbResult = new DataTable();
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                    //objHRPSvc.Dispose();
                    if (lngRes > 0 && dtbResult.Rows.Count > 0)
                    {
                        p_objResultArr = new clsDoctorOrder_VO[dtbResult.Rows.Count];
                        for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                        {
                            p_objResultArr[i1] = new clsDoctorOrder_VO();
                            p_objResultArr[i1].m_strPATIENT_ID = dtbResult.Rows[i1]["PATIENT_ID"].ToString().Trim();
                            p_objResultArr[i1].m_strVISIT_ID = dtbResult.Rows[i1]["VISIT_ID"].ToString().Trim();
                            p_objResultArr[i1].m_strORDER_NO = dtbResult.Rows[i1]["ORDER_NO"].ToString().Trim();
                            p_objResultArr[i1].m_strORDER_SUB_NO = dtbResult.Rows[i1]["ORDER_SUB_NO"].ToString().Trim();
                            p_objResultArr[i1].m_strSTART_DATE_TIME = dtbResult.Rows[i1]["START_DATE_TIME"].ToString().Trim();
                            p_objResultArr[i1].m_strSTART_STOP_INDICATOR = dtbResult.Rows[i1]["START_STOP_INDICATOR"].ToString().Trim();
                            p_objResultArr[i1].m_strREPEAT_INDICATOR = dtbResult.Rows[i1]["REPEAT_INDICATOR"].ToString().Trim();
                            p_objResultArr[i1].m_strORDER_CLASS = dtbResult.Rows[i1]["ORDER_CLASS"].ToString().Trim();
                            p_objResultArr[i1].m_strORDER_TEXT = dtbResult.Rows[i1]["ORDER_TEXT"].ToString().Trim();
                            p_objResultArr[i1].m_strORDER_CODE = dtbResult.Rows[i1]["ORDER_CODE"].ToString().Trim();
                            p_objResultArr[i1].m_strDOSAGE = dtbResult.Rows[i1]["dosage_num"].ToString();
                            p_objResultArr[i1].m_strDOSAGE_UNITS = dtbResult.Rows[i1]["DOSAGE_UNITS"].ToString().Trim();
                            p_objResultArr[i1].m_strADMINISTRATION = dtbResult.Rows[i1]["ADMINISTRATION"].ToString().Trim();
                            p_objResultArr[i1].m_strDURATION = dtbResult.Rows[i1]["DURATION"].ToString().Trim();
                            p_objResultArr[i1].m_strDURATION_UNITS = dtbResult.Rows[i1]["DURATION_UNITS"].ToString().Trim();
                            p_objResultArr[i1].m_strFREQUENCY = dtbResult.Rows[i1]["FREQUENCY"].ToString().Trim();
                            p_objResultArr[i1].m_strFREQ_COUNTER = dtbResult.Rows[i1]["FREQ_COUNTER"].ToString().Trim();
                            p_objResultArr[i1].m_strFREQ_INTERVAL = dtbResult.Rows[i1]["FREQ_INTERVAL"].ToString().Trim();
                            p_objResultArr[i1].m_strFREQ_INTERVAL_UNIT = dtbResult.Rows[i1]["FREQ_INTERVAL_UNIT"].ToString().Trim();
                            p_objResultArr[i1].m_strFREQ_DETAIL = dtbResult.Rows[i1]["FREQ_DETAIL"].ToString().Trim();
                            p_objResultArr[i1].m_strORDERING_DEPT = dtbResult.Rows[i1]["ORDERING_DEPT"].ToString().Trim();
                            p_objResultArr[i1].m_strDOCTOR = dtbResult.Rows[i1]["DOCTOR"].ToString().Trim();
                            p_objResultArr[i1].m_strNURSE = dtbResult.Rows[i1]["NURSE"].ToString().Trim();
                            p_objResultArr[i1].m_strORDER_STATUS = dtbResult.Rows[i1]["ORDER_STATUS"].ToString().Trim();
                            p_objResultArr[i1].m_strENTER_DATE_TIME = dtbResult.Rows[i1]["ENTER_DATE_TIME"].ToString().Trim();
                            p_objResultArr[i1].m_strPROCESSING_DATE_TIME = dtbResult.Rows[i1]["PROCESSING_DATE_TIME"].ToString().Trim();
                            p_objResultArr[i1].m_strBILLING_ATTR = dtbResult.Rows[i1]["BILLING_ATTR"].ToString().Trim();
                            p_objResultArr[i1].m_strORDER_PRINT_INDICATOR = dtbResult.Rows[i1]["ORDER_PRINT_INDICATOR"].ToString().Trim();
                            p_objResultArr[i1].m_strRELATED_ORDER_NO = dtbResult.Rows[i1]["RELATED_ORDER_NO"].ToString().Trim();
                            p_objResultArr[i1].m_strRELATED_ORDER_SUB_NO = dtbResult.Rows[i1]["RELATED_ORDER_SUB_NO"].ToString().Trim();
                            p_objResultArr[i1].m_strDRUG_BILLING_ATTR = dtbResult.Rows[i1]["DRUG_BILLING_ATTR"].ToString().Trim();
                            p_objResultArr[i1].m_strOrder_class_code = dtbResult.Rows[i1]["order_class_code"].ToString().Trim();
                            p_objResultArr[i1].m_strOrder_class_name = dtbResult.Rows[i1]["order_class_name"].ToString().Trim();
                            p_objResultArr[i1].m_strCmpOrderStatus = dtbResult.Rows[i1]["cmporderstatus"].ToString().Trim();
                            p_objResultArr[i1].m_strExcueTime = dtbResult.Rows[i1]["perform_schedule"].ToString().Trim();
                            p_objResultArr[i1].m_strInHostpitalTime = dtbResult.Rows[i1]["admission_date_time"].ToString().Trim();
                            p_objResultArr[i1].m_strInp_no = dtbResult.Rows[i1]["inp_no"].ToString().Trim();
                        }
                    }
                }
                catch (Exception ex)
                {
                    com.digitalwave.Utility.clsLogText objLog = new com.digitalwave.Utility.clsLogText();
                    bool blnRes = objLog.LogError(ex);
                }
                finally
                {
                    //objHRPSvc.Dispose();
                }
			return lngRes;
		}
		#endregion
        */
        
        #endregion

        #region 病程记录

        #region SQL语句

        #region 一般病程记录
        private const string c_strGetRecordContentSQL_Normal = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.ifconfirm,
       a.confirmreason,
       a.confirmreasonxml,
       a.status,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.firstprintdate,
       a.recordtitle,
       a.recordtitletype,
       a.recordcontent,
       a.recordcontentxml,
       a.sequence_int,
       a.markstatus,
       a.opendate as opendate_main,
       b.inpatientid,
       b.inpatientdate,
       b.opendate,
       b.modifydate,
       b.modifyuserid,
       b.recordcontent_right,
       patient.emrinpatientid,
       patient.emrinpatientdate,
       sign.empid_vchr,
       sign.lastname_vchr,
       sign.technicalrank_chr,
       sign.empno_chr,
       sign.psw_chr,
       sign.digitalsign_dta,
       sign.pycode_chr,
       sign.status_int,
       sign.technicallevel_chr,
       sign.cagetory_vchr,
       sign.formname_vchr,
       sign.registerid_vchr,
       sign.sign_int
  from generaldiseaserecord a,
       generaldiseaserecordcontent b,
       (select here.emrinpatientid, here.emrinpatientdate
          from t_opr_bih_register    reg,
               t_bse_bed             bed,
               t_bse_hisemr_relation here
         where reg.deptid_chr = ?
           and reg.status_int = 1
           and reg.pstatus_int <> 3
           and reg.bedid_chr = bed.bedid_chr
           and here.registerid_chr = reg.registerid_chr
           and bed.status_int = 2) patient,
       (select t.empid_vchr,
               d.lastname_vchr,
               d.technicalrank_chr,
               d.empno_chr,
               d.psw_chr,
               d.digitalsign_dta,
               d.pycode_chr,
               d.status_int,
               d.technicallevel_chr,
               t.cagetory_vchr,
               t.formname_vchr,
               t.registerid_vchr,
               t.sign_int
          from t_emr_signcollection t
          left outer join t_bse_employee d on t.empid_vchr = d.empid_chr
         order by d.technicallevel_chr desc) sign
 where a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and a.inpatientid = patient.emrinpatientid
   and a.inpatientdate = patient.emrinpatientdate
   and sign.sign_int = a.sequence_int
   and b.modifydate = (select max(modifydate)
                         from generaldiseaserecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";
        #endregion 一般病程记录

        #region 交班记录
        private const string c_strGetRecordContentSQL_HandOver = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.ifconfirm,
       a.confirmreason,
       a.confirmreasonxml,
       a.firstprintdate,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.originaldiagnose,
       a.originaldiagnosexml,
       a.currentdiagnose,
       a.currentdiagnosexml,
       a.casehistory,
       a.casehistoryxml,
       a.referral,
       a.referralxml,
       a.sequence_int,
       a.markstatus,
       a.opendate as opendate_main,
       b.inpatientid,
       b.inpatientdate,
       b.opendate,
       b.modifydate,
       b.modifyuserid,
       b.originaldiagnose_right,
       b.currentdiagnose_right,
       b.casehistory_right,
       b.referral_right,
       patient.emrinpatientid,
       patient.emrinpatientdate,
       sign.empid_vchr,
       sign.lastname_vchr,
       sign.technicalrank_chr,
       sign.empno_chr,
       sign.psw_chr,
       sign.digitalsign_dta,
       sign.pycode_chr,
       sign.status_int,
       sign.technicallevel_chr,
       sign.cagetory_vchr,
       sign.formname_vchr,
       sign.registerid_vchr,
       sign.sign_int
  from handoverrecord a,
       handoverrecordcontent b,
       (select here.emrinpatientid, here.emrinpatientdate
          from t_opr_bih_register    reg,
               t_bse_bed             bed,
               t_bse_hisemr_relation here
         where reg.deptid_chr = ?
           and reg.status_int = 1
           and reg.pstatus_int <> 3
           and reg.bedid_chr = bed.bedid_chr
           and here.registerid_chr = reg.registerid_chr
           and bed.status_int = 2) patient,
       (select t.empid_vchr,
               d.lastname_vchr,
               d.technicalrank_chr,
               d.empno_chr,
               d.psw_chr,
               d.digitalsign_dta,
               d.pycode_chr,
               d.status_int,
               d.technicallevel_chr,
               t.cagetory_vchr,
               t.formname_vchr,
               t.registerid_vchr,
               t.sign_int
          from t_emr_signcollection t
          left outer join t_bse_employee d on t.empid_vchr = d.empid_chr
         order by d.technicallevel_chr desc) sign
 where a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and patient.emrinpatientid = a.inpatientid
   and patient.emrinpatientdate = a.inpatientdate
   and sign.sign_int = a.sequence_int
   and b.modifydate = (select max(modifydate)
                         from handoverrecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";
        #endregion 交班记录

        #region 接班记录
        private const string c_strGetRecordContentSQL_TakeOver = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.ifconfirm,
       a.confirmreason,
       a.confirmreasonxml,
       a.firstprintdate,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.originaldiagnose,
       a.originaldiagnosexml,
       a.currentdiagnose,
       a.currentdiagnosexml,
       a.casehistory,
       a.casehistoryxml,
       a.referral,
       a.referralxml,
       a.sequence_int,
       a.markstatus,
       a.opendate as opendate_main,
       b.inpatientid,
       b.inpatientdate,
       b.opendate,
       b.modifydate,
       b.modifyuserid,
       b.originaldiagnose_right,
       b.currentdiagnose_right,
       b.casehistory_right,
       b.referral_right,
       patient.emrinpatientid,
       patient.emrinpatientdate,
       sign.empid_vchr,
       sign.lastname_vchr,
       sign.technicalrank_chr,
       sign.empno_chr,
       sign.psw_chr,
       sign.digitalsign_dta,
       sign.pycode_chr,
       sign.status_int,
       sign.technicallevel_chr,
       sign.cagetory_vchr,
       sign.formname_vchr,
       sign.registerid_vchr,
       sign.sign_int
  from takeoverrecord a,
       takeoverrecordcontent b,
       (select here.emrinpatientid, here.emrinpatientdate
          from t_opr_bih_register    reg,
               t_bse_bed             bed,
               t_bse_hisemr_relation here
         where reg.deptid_chr = ?
           and reg.status_int = 1
           and reg.pstatus_int <> 3
           and reg.bedid_chr = bed.bedid_chr
           and here.registerid_chr = reg.registerid_chr
           and bed.status_int = 2) patient,
       (select t.empid_vchr,
               d.lastname_vchr,
               d.technicalrank_chr,
               d.empno_chr,
               d.psw_chr,
               d.digitalsign_dta,
               d.pycode_chr,
               d.status_int,
               d.technicallevel_chr,
               t.cagetory_vchr,
               t.formname_vchr,
               t.registerid_vchr,
               t.sign_int
          from t_emr_signcollection t
          left outer join t_bse_employee d on t.empid_vchr = d.empid_chr
         order by d.technicallevel_chr desc) sign
 where a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and patient.emrinpatientid = a.inpatientid
   and patient.emrinpatientdate = a.inpatientdate
   and sign.sign_int = a.sequence_int
   and b.modifydate = (select max(modifydate)
                         from takeoverrecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";
        #endregion 接班记录

        #region 病程小结
        private const string c_strGetRecordContentSQL_DiseaseSummary = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.ifconfirm,
       a.confirmreason,
       a.confirmreasonxml,
       a.firstprintdate,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.inhospitalcase,
       a.inhospitalcasexml,
       a.inhospitaldiagnose,
       a.inhospitaldiagnosexml,
       a.diagnoseby,
       a.diagnosebyxml,
       a.currentcase,
       a.currentcasexml,
       a.currentdiagnose,
       a.currentdiagnosexml,
       a.referral,
       a.referralxml,
       a.sequence_int,
       a.markstatus,
       a.opendate as opendate_main,
       b.inpatientid,
       b.inpatientdate,
       b.opendate,
       b.modifydate,
       b.modifyuserid,
       b.inhospitalcase_right,
       b.inhospitaldiagnose_right,
       b.diagnoseby_right,
       b.currentcase_right,
       b.currentdiagnose_right,
       b.referral_right,
       b.doctorid,
       patient.emrinpatientid,
       patient.emrinpatientdate,
       sign.empid_vchr,
       sign.lastname_vchr,
       sign.technicalrank_chr,
       sign.empno_chr,
       sign.psw_chr,
       sign.digitalsign_dta,
       sign.pycode_chr,
       sign.status_int,
       sign.technicallevel_chr,
       sign.cagetory_vchr,
       sign.formname_vchr,
       sign.registerid_vchr,
       sign.sign_int
  from diseasesummaryrecord a,
       diseasesummaryrecordcontent b,
       (select here.emrinpatientid, here.emrinpatientdate
          from t_opr_bih_register    reg,
               t_bse_bed             bed,
               t_bse_hisemr_relation here
         where reg.deptid_chr = ?
           and reg.status_int = 1
           and reg.pstatus_int <> 3
           and reg.bedid_chr = bed.bedid_chr
           and here.registerid_chr = reg.registerid_chr
           and bed.status_int = 2) patient,
       (select t.empid_vchr,
               d.lastname_vchr,
               d.technicalrank_chr,
               d.empno_chr,
               d.psw_chr,
               d.digitalsign_dta,
               d.pycode_chr,
               d.status_int,
               d.technicallevel_chr,
               t.cagetory_vchr,
               t.formname_vchr,
               t.registerid_vchr,
               t.sign_int
          from t_emr_signcollection t
          left outer join t_bse_employee d on t.empid_vchr = d.empid_chr
         order by d.technicallevel_chr desc) sign
 where a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and patient.emrinpatientid = a.inpatientid
   and patient.emrinpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and sign.sign_int = a.sequence_int
   and b.modifydate = (select max(modifydate)
                         from diseasesummaryrecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";
        #endregion 病程小结

        #region 转出记录
        private const string c_strGetRecordContentSQL_Convey = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.ifconfirm,
       a.confirmreason,
       a.confirmreasonxml,
       a.firstprintdate,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.originaldiagnose,
       a.originaldiagnosexml,
       a.conveydiagnose,
       a.conveydiagnosexml,
       a.casehistory,
       a.casehistoryxml,
       a.consultation,
       a.consultationxml,
       a.conveyreason,
       a.conveyreasonxml,
       a.notice,
       a.noticexml,
       a.sequence_int,
       a.markstatus,
       a.opendate as opendate_main,
       b.inpatientid,
       b.inpatientdate,
       b.opendate,
       b.modifydate,
       b.modifyuserid,
       b.originaldiagnose_right,
       b.conveydiagnose_right,
       b.casehistory_right,
       b.consultation_right,
       b.conveyreason_right,
       b.notice_right,
       b.maindoctorid,
       patient.emrinpatientid,
       patient.emrinpatientdate,
       sign.empid_vchr,
       sign.lastname_vchr,
       sign.technicalrank_chr,
       sign.empno_chr,
       sign.psw_chr,
       sign.digitalsign_dta,
       sign.pycode_chr,
       sign.status_int,
       sign.technicallevel_chr,
       sign.cagetory_vchr,
       sign.formname_vchr,
       sign.registerid_vchr,
       sign.sign_int
  from conveyrecord a,
       conveyrecordcontent b,
       (select here.emrinpatientid, here.emrinpatientdate
          from t_opr_bih_register    reg,
               t_bse_bed             bed,
               t_bse_hisemr_relation here
         where reg.deptid_chr = ?
           and reg.status_int = 1
           and reg.pstatus_int <> 3
           and reg.bedid_chr = bed.bedid_chr
           and here.registerid_chr = reg.registerid_chr
           and bed.status_int = 2) patient,
       (select t.empid_vchr,
               d.lastname_vchr,
               d.technicalrank_chr,
               d.empno_chr,
               d.psw_chr,
               d.digitalsign_dta,
               d.pycode_chr,
               d.status_int,
               d.technicallevel_chr,
               t.cagetory_vchr,
               t.formname_vchr,
               t.registerid_vchr,
               t.sign_int
          from t_emr_signcollection t
          left outer join t_bse_employee d on t.empid_vchr = d.empid_chr
         order by d.technicallevel_chr desc) sign
 where a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and patient.emrinpatientid = a.inpatientid
   and patient.emrinpatientdate = a.inpatientdate
   and sign.sign_int = a.sequence_int
   and b.modifydate = (select max(modifydate)
                         from conveyrecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";
        #endregion 转出记录

        #region 转入记录
        private const string c_strGetRecordContentSQL_TurnIn = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.ifconfirm,
       a.confirmreason,
       a.confirmreasonxml,
       a.firstprintdate,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.casebeforeturnin,
       a.casebeforeturninxml,
       a.turninreason,
       a.turninreasonxml,
       a.caseafterturnin,
       a.caseafterturninxml,
       a.turnindiagnose,
       a.turnindiagnosexml,
       a.referral,
       a.referralxml,
       a.sequence_int,
       a.markstatus,
       a.opendate as opendate_main,
       b.inpatientid,
       b.inpatientdate,
       b.opendate,
       b.modifydate,
       b.modifyuserid,
       b.casebeforeturnin_right,
       b.turninreason_right,
       b.caseafterturnin_right,
       b.turnindiagnose_right,
       b.referral_right,
       patient.emrinpatientid,
       patient.emrinpatientdate,
       sign.empid_vchr,
       sign.lastname_vchr,
       sign.technicalrank_chr,
       sign.empno_chr,
       sign.psw_chr,
       sign.digitalsign_dta,
       sign.pycode_chr,
       sign.status_int,
       sign.technicallevel_chr,
       sign.cagetory_vchr,
       sign.formname_vchr,
       sign.registerid_vchr,
       sign.sign_int
  from turninrecord a,
       turninrecordcontent b,
       (select here.emrinpatientid, here.emrinpatientdate
          from t_opr_bih_register    reg,
               t_bse_bed             bed,
               t_bse_hisemr_relation here
         where reg.deptid_chr = ?
           and reg.status_int = 1
           and reg.pstatus_int <> 3
           and reg.bedid_chr = bed.bedid_chr
           and here.registerid_chr = reg.registerid_chr
           and bed.status_int = 2) patient,
       (select t.empid_vchr,
               d.lastname_vchr,
               d.technicalrank_chr,
               d.empno_chr,
               d.psw_chr,
               d.digitalsign_dta,
               d.pycode_chr,
               d.status_int,
               d.technicallevel_chr,
               t.cagetory_vchr,
               t.formname_vchr,
               t.registerid_vchr,
               t.sign_int
          from t_emr_signcollection t
          left outer join t_bse_employee d on t.empid_vchr = d.empid_chr
         order by d.technicallevel_chr desc) sign
 where a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and patient.emrinpatientid = a.inpatientid
   and patient.emrinpatientdate = a.inpatientdate
   and sign.sign_int = a.sequence_int
   and b.modifydate = (select max(modifydate)
                         from turninrecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";
        #endregion 转入记录

        #region 查房记录
        private const string c_strGetRecordContentSQL_CheckRoom = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.ifconfirm,
       a.confirmreason,
       a.confirmreasonxml,
       a.firstprintdate,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.patientstate,
       a.patientstatexml,
       a.diagnose,
       a.diagnosexml,
       a.differentiatediagnose,
       a.differentiatediagnosexml,
       a.currentcure,
       a.currentcurexml,
       a.nextcure,
       a.nextcurexml,
       a.checkroomdoctorid,
       a.checkroomdoctorslist,
       a.recorder_id,
       a.sequence_int,
       a.markstatus,
       a.opendate as opendate_main,
       b.inpatientid,
       b.inpatientdate,
       b.opendate,
       b.modifydate,
       b.modifyuserid,
       b.patientstate_right,
       b.diagnose_right,
       b.differentiatediagnose_right,
       b.currentcure_right,
       b.nextcure_right,
       b.checkroomdoctorid,
       b.checkroomdoctorslist,
       patient.emrinpatientid,
       patient.emrinpatientdate,
       sign.empid_vchr,
       sign.lastname_vchr,
       sign.technicalrank_chr,
       sign.empno_chr,
       sign.psw_chr,
       sign.digitalsign_dta,
       sign.pycode_chr,
       sign.status_int,
       sign.technicallevel_chr,
       sign.cagetory_vchr,
       sign.formname_vchr,
       sign.registerid_vchr,
       sign.sign_int
  from checkroomrecord a,
       checkroomrecordcontent b,
       (select here.emrinpatientid, here.emrinpatientdate
          from t_opr_bih_register    reg,
               t_bse_bed             bed,
               t_bse_hisemr_relation here
         where reg.deptid_chr = ?
           and reg.status_int = 1
           and reg.pstatus_int <> 3
           and reg.bedid_chr = bed.bedid_chr
           and here.registerid_chr = reg.registerid_chr
           and bed.status_int = 2) patient,
       (select t.empid_vchr,
               d.lastname_vchr,
               d.technicalrank_chr,
               d.empno_chr,
               d.psw_chr,
               d.digitalsign_dta,
               d.pycode_chr,
               d.status_int,
               d.technicallevel_chr,
               t.cagetory_vchr,
               t.formname_vchr,
               t.registerid_vchr,
               t.sign_int
          from t_emr_signcollection t
          left outer join t_bse_employee d on t.empid_vchr = d.empid_chr
         order by d.technicallevel_chr desc) sign
 where a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and patient.emrinpatientid = a.inpatientid
   and patient.emrinpatientdate = a.inpatientdate
   and sign.sign_int = a.sequence_int
   and b.modifydate = (select max(modifydate)
                         from checkroomrecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";

        #endregion 查房记录

        #region 病例讨论记录
        private const string c_strGetRecordContentSQL_CaseDiscuss = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.ifconfirm,
       a.confirmreason,
       a.confirmreasonxml,
       a.firstprintdate,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.address,
       a.addressxml,
       a.discusscontent,
       a.discusscontentxml,
       a.sequence_int,
       a.markstatus,
       a.opendate as opendate_main,
       b.inpatientid,
       b.inpatientdate,
       b.opendate,
       b.modifydate,
       b.modifyuserid,
       b.address_right,
       b.discusscontent_right,
       b.compereid,
       patient.emrinpatientid,
       patient.emrinpatientdate,
       sign.empid_vchr,
       sign.lastname_vchr,
       sign.technicalrank_chr,
       sign.empno_chr,
       sign.psw_chr,
       sign.digitalsign_dta,
       sign.pycode_chr,
       sign.status_int,
       sign.technicallevel_chr,
       sign.cagetory_vchr,
       sign.formname_vchr,
       sign.registerid_vchr,
       sign.sign_int
  from casediscussrecord a,
       casediscussrecordcontent b,
       (select here.emrinpatientid, here.emrinpatientdate
          from t_opr_bih_register    reg,
               t_bse_bed             bed,
               t_bse_hisemr_relation here
         where reg.deptid_chr = ?
           and reg.status_int = 1
           and reg.pstatus_int <> 3
           and reg.bedid_chr = bed.bedid_chr
           and here.registerid_chr = reg.registerid_chr
           and bed.status_int = 2) patient,
       (select t.empid_vchr,
               d.lastname_vchr,
               d.technicalrank_chr,
               d.empno_chr,
               d.psw_chr,
               d.digitalsign_dta,
               d.pycode_chr,
               d.status_int,
               d.technicallevel_chr,
               t.cagetory_vchr,
               t.formname_vchr,
               t.registerid_vchr,
               t.sign_int
          from t_emr_signcollection t
          left outer join t_bse_employee d on t.empid_vchr = d.empid_chr
         order by d.technicallevel_chr desc) sign
 where a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and patient.emrinpatientid = a.inpatientid
   and patient.emrinpatientdate = a.inpatientdate
   and sign.sign_int = a.sequence_int
   and b.modifydate = (select max(modifydate)
                         from casediscussrecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";
        #endregion 病例讨论记录

        #region 术前讨论记录
        private const string c_strGetRecordContentSQL_BeforeOperationDiscuss = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.ifconfirm,
       a.confirmreason,
       a.confirmreasonxml,
       a.firstprintdate,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.address,
       a.addressxml,
       a.discusscontent,
       a.discusscontentxml,
       a.sequence_int,
       a.markstatus,
       a.opendate as opendate_main,
       b.inpatientid,
       b.inpatientdate,
       b.opendate,
       b.modifydate,
       b.modifyuserid,
       b.address_right,
       b.discusscontent_right,
       b.compereid,
       patient.emrinpatientid,
       patient.emrinpatientdate,
       sign.empid_vchr,
       sign.lastname_vchr,
       sign.technicalrank_chr,
       sign.empno_chr,
       sign.psw_chr,
       sign.digitalsign_dta,
       sign.pycode_chr,
       sign.status_int,
       sign.technicallevel_chr,
       sign.cagetory_vchr,
       sign.formname_vchr,
       sign.registerid_vchr,
       sign.sign_int
  from beforeoperationdiscussrecord a,
       bfoprdiscussreccont b,
       (select here.emrinpatientid, here.emrinpatientdate
          from t_opr_bih_register    reg,
               t_bse_bed             bed,
               t_bse_hisemr_relation here
         where reg.deptid_chr = ?
           and reg.status_int = 1
           and reg.pstatus_int <> 3
           and reg.bedid_chr = bed.bedid_chr
           and here.registerid_chr = reg.registerid_chr
           and bed.status_int = 2) patient,
       (select t.empid_vchr,
               d.lastname_vchr,
               d.technicalrank_chr,
               d.empno_chr,
               d.psw_chr,
               d.digitalsign_dta,
               d.pycode_chr,
               d.status_int,
               d.technicallevel_chr,
               t.cagetory_vchr,
               t.formname_vchr,
               t.registerid_vchr,
               t.sign_int
          from t_emr_signcollection t
          left outer join t_bse_employee d on t.empid_vchr = d.empid_chr
         order by d.technicallevel_chr desc) sign
 where a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and patient.emrinpatientid = a.inpatientid
   and patient.emrinpatientdate = a.inpatientdate
   and sign.sign_int = a.sequence_int
   and b.modifydate = (select max(modifydate)
                         from bfoprdiscussreccont
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";
        #endregion 术前讨论记录

        #region 死亡病例讨论记录
        private const string c_strGetRecordContentSQL_DeadCaseDiscuss = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.ifconfirm,
       a.confirmreason,
       a.confirmreasonxml,
       a.firstprintdate,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.address,
       a.addressxml,
       a.discusscontent,
       a.discusscontentxml,
       a.deaddiagnose,
       a.deaddiagnosexml,
       a.deadreason,
       a.deadreasonxml,
       a.useforreference,
       a.useforreferencexml,
       a.sequence_int,
       a.markstatus,
       a.opendate as opendate_main,
       b.inpatientid,
       b.inpatientdate,
       b.opendate,
       b.modifydate,
       b.modifyuserid,
       b.address_right,
       b.discusscontent_right,
       b.deaddiagnose_right,
       b.deadreason_right,
       b.useforreference_right,
       b.compereid,
       b.readouterid,
       patient.emrinpatientid,
       patient.emrinpatientdate,
       sign.empid_vchr,
       sign.lastname_vchr,
       sign.technicalrank_chr,
       sign.empno_chr,
       sign.psw_chr,
       sign.digitalsign_dta,
       sign.pycode_chr,
       sign.status_int,
       sign.technicallevel_chr,
       sign.cagetory_vchr,
       sign.formname_vchr,
       sign.registerid_vchr,
       sign.sign_int
  from deadcasediscussrecord a
 inner join deadcasediscussrecordcontent b on (a.inpatientid =
                                              b.inpatientid and
                                              a.inpatientdate =
                                              b.inpatientdate and
                                              a.opendate = b.opendate)
 inner join (select here.emrinpatientid, here.emrinpatientdate
               from t_opr_bih_register    reg,
                    t_bse_bed             bed,
                    t_bse_hisemr_relation here
              where reg.deptid_chr = ?
                and reg.status_int = 1
                and reg.pstatus_int <> 3
                and reg.bedid_chr = bed.bedid_chr
                and here.registerid_chr = reg.registerid_chr
                and bed.status_int = 2) patient on patient.
 emrinpatientid =
                                                   a.inpatientid
                                               and patient.emrinpatientdate =
                                                   a.inpatientdate
inner join (select t.empid_vchr,
               d.lastname_vchr,
               d.technicalrank_chr,
               d.empno_chr,
               d.psw_chr,
               d.digitalsign_dta,
               d.pycode_chr,
               d.status_int,
               d.technicallevel_chr,
               t.cagetory_vchr,
               t.formname_vchr,
               t.registerid_vchr,
               t.sign_int
          from t_emr_signcollection t
          left outer join t_bse_employee d on t.empid_vchr = d.empid_chr
         order by d.technicallevel_chr desc) sign on sign.sign_int = a.sequence_int
 where a.status = 0
   and b.modifydate =
       (select max(modifydate)
          from deadcasediscussrecordcontent
         where (inpatientid = a.inpatientid and
               inpatientdate = a.inpatientdate and opendate = a.opendate))";
        #endregion 死亡病例讨论记录

        #region 死亡记录
        private const string c_strGetRecordContentSQL_Dead = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.ifconfirm,
       a.confirmreason,
       a.confirmreasonxml,
       a.firstprintdate,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.inhospitalcase,
       a.inhospitalcasexml,
       a.originaldiagnose,
       a.originaldiagnosexml,
       a.diagnoseby,
       a.diagnosebyxml,
       a.deaddiagnose,
       a.deaddiagnosexml,
       a.deadreason,
       a.deadreasonxml,
       a.experience,
       a.experiencexml,
       a.sequence_int,
       a.markstatus,
       a.opendate as opendate_main,
       b.inpatientid,
       b.inpatientdate,
       b.opendate,
       b.modifydate,
       b.modifyuserid,
       b.deaddate,
       b.inhospitalcase_right,
       b.originaldiagnose_right,
       b.diagnoseby_right,
       b.deaddiagnose_right,
       b.deadreason_right,
       b.experience_right,
       b.doctorid,
       patient.emrinpatientid,
       patient.emrinpatientdate,
       sign.empid_vchr,
       sign.lastname_vchr,
       sign.technicalrank_chr,
       sign.empno_chr,
       sign.psw_chr,
       sign.digitalsign_dta,
       sign.pycode_chr,
       sign.status_int,
       sign.technicallevel_chr,
       sign.cagetory_vchr,
       sign.formname_vchr,
       sign.registerid_vchr,
       sign.sign_int
  from deadrecord a,
       deadrecordcontent b,
       (select here.emrinpatientid, here.emrinpatientdate
          from t_opr_bih_register    reg,
               t_bse_bed             bed,
               t_bse_hisemr_relation here
         where reg.deptid_chr = ?
           and reg.status_int = 1
           and reg.pstatus_int <> 3
           and reg.bedid_chr = bed.bedid_chr
           and here.registerid_chr = reg.registerid_chr
           and bed.status_int = 2) patient,
       (select t.empid_vchr,
               d.lastname_vchr,
               d.technicalrank_chr,
               d.empno_chr,
               d.psw_chr,
               d.digitalsign_dta,
               d.pycode_chr,
               d.status_int,
               d.technicallevel_chr,
               t.cagetory_vchr,
               t.formname_vchr,
               t.registerid_vchr,
               t.sign_int
          from t_emr_signcollection t
          left outer join t_bse_employee d on t.empid_vchr = d.empid_chr
         order by d.technicallevel_chr desc) sign
 where a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and patient.emrinpatientid = a.inpatientid
   and patient.emrinpatientdate = a.inpatientdate
   and sign.sign_int = a.sequence_int
   and b.modifydate = (select max(modifydate)
                         from deadrecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate) ";
        #endregion 死亡记录

        #region 手术后病程记录
        private const string c_strGetRecordContentSQL_AfterOperation = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.ifconfirm,
       a.confirmreason,
       a.confirmreasonxml,
       a.firstprintdate,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.anaesthesiamode,
       a.anaesthesiamodexml,
       a.operationname,
       a.operationnamexml,
       a.operationdiagnose,
       a.operationdiagnosexml,
       a.inoperationseeing,
       a.inoperationseeingxml,
       a.afteroperationdeal,
       a.afteroperationdealxml,
       a.afteroperationnotice,
       a.afteroperationnoticexml,
       a.cuthealupstatus,
       a.cuthealupstatusxml,
       a.takeoutstitchesdate,
       a.sequence_int,
       a.markstatus,
       a.opendate as opendate_main,
       b.inpatientid,
       b.inpatientdate,
       b.opendate,
       b.modifydate,
       b.modifyuserid,
       b.anaesthesiamode_right,
       b.operationname_right,
       b.operationdiagnose_right,
       b.inoperationseeing_right,
       b.afteroperationdeal_right,
       b.afteroperationnotice_right,
       b.cuthealupstatus_right,
       patient.emrinpatientid,
       patient.emrinpatientdate,
       sign.empid_vchr,
       sign.lastname_vchr,
       sign.technicalrank_chr,
       sign.empno_chr,
       sign.psw_chr,
       sign.digitalsign_dta,
       sign.pycode_chr,
       sign.status_int,
       sign.technicallevel_chr,
       sign.cagetory_vchr,
       sign.formname_vchr,
       sign.registerid_vchr,
       sign.sign_int
  from afteroperationrecord a,
       afteroperationrecordcontent b,
       (select here.emrinpatientid, here.emrinpatientdate
          from t_opr_bih_register    reg,
               t_bse_bed             bed,
               t_bse_hisemr_relation here
         where reg.deptid_chr = ?
           and reg.status_int = 1
           and reg.pstatus_int <> 3
           and reg.bedid_chr = bed.bedid_chr
           and here.registerid_chr = reg.registerid_chr
           and bed.status_int = 2) patient,
       (select t.empid_vchr,
               d.lastname_vchr,
               d.technicalrank_chr,
               d.empno_chr,
               d.psw_chr,
               d.digitalsign_dta,
               d.pycode_chr,
               d.status_int,
               d.technicallevel_chr,
               t.cagetory_vchr,
               t.formname_vchr,
               t.registerid_vchr,
               t.sign_int
          from t_emr_signcollection t
          left outer join t_bse_employee d on t.empid_vchr = d.empid_chr
         order by d.technicallevel_chr desc) sign
 where a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and patient.emrinpatientid = a.inpatientid
   and patient.emrinpatientdate = a.inpatientdate
   and sign.sign_int = a.sequence_int
   and b.modifydate = (select max(modifydate)
                         from afteroperationrecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";
        #endregion 手术后病程记录

        #region 抢救记录
        private const string c_strGetRecordContentSQL_Save = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.ifconfirm,
       a.confirmreason,
       a.confirmreasonxml,
       a.firstprintdate,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.diseasename,
       a.diseasenamexml,
       a.diseasechangecase,
       a.diseasechangecasexml,
       a.savedeal,
       a.savedealxml,
       a.saveresult,
       a.saveresultxml,
       a.attendpeople,
       a.attendpeoplexml,
       a.sequence_int,
       a.markstatus,
       a.opendate as opendate_main,
       b.inpatientid,
       b.inpatientdate,
       b.opendate,
       b.modifydate,
       b.modifyuserid,
       b.savetime,
       b.diseasename_right,
       b.diseasechangecase_right,
       b.savedeal_right,
       b.saveresult_right,
       b.bydoctorid,
       b.attendpeople_right,
       patient.emrinpatientid,
       patient.emrinpatientdate,
       sign.empid_vchr,
       sign.lastname_vchr,
       sign.technicalrank_chr,
       sign.empno_chr,
       sign.psw_chr,
       sign.digitalsign_dta,
       sign.pycode_chr,
       sign.status_int,
       sign.technicallevel_chr,
       sign.cagetory_vchr,
       sign.formname_vchr,
       sign.registerid_vchr,
       sign.sign_int
  from saverecord a,
       saverecordcontent b,
       (select here.emrinpatientid, here.emrinpatientdate
          from t_opr_bih_register    reg,
               t_bse_bed             bed,
               t_bse_hisemr_relation here
         where reg.deptid_chr = ?
           and reg.status_int = 1
           and reg.pstatus_int <> 3
           and reg.bedid_chr = bed.bedid_chr
           and here.registerid_chr = reg.registerid_chr
           and bed.status_int = 2) patient,
       (select t.empid_vchr,
               d.lastname_vchr,
               d.technicalrank_chr,
               d.empno_chr,
               d.psw_chr,
               d.digitalsign_dta,
               d.pycode_chr,
               d.status_int,
               d.technicallevel_chr,
               t.cagetory_vchr,
               t.formname_vchr,
               t.registerid_vchr,
               t.sign_int
          from t_emr_signcollection t
          left outer join t_bse_employee d on t.empid_vchr = d.empid_chr
         order by d.technicallevel_chr desc) sign
 where a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and patient.emrinpatientid = a.inpatientid
   and patient.emrinpatientdate = a.inpatientdate
   and sign.sign_int = a.sequence_int
   and b.modifydate = (select max(modifydate)
                         from saverecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";
        #endregion 抢救记录

        #region 首次病程记录
        private const string c_strGetRecordContentSQL_FirstIllnessNote = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.ifconfirm,
       a.confirmreason,
       a.confirmreasonxml,
       a.firstprintdate,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.mostlycontent,
       a.mostlycontentxml,
       a.originaldiagnose,
       a.originaldiagnosexml,
       a.thereunderdiagnose,
       a.thereunderdiagnosexml,
       a.diagnosediffe,
       a.diagnosediffexml,
       a.cureplan,
       a.cureplanxml,
       a.sequence_int,
       a.markstatus,
       a.opendate as opendate_main,
       b.inpatientid,
       b.inpatientdate,
       b.opendate,
       b.modifydate,
       b.modifyuserid,
       b.mostlycontent_right,
       b.originaldiagnose_right,
       b.thereunderdiagnose_right,
       b.diagnosediffe_right,
       b.cureplan_right,
       b.doctorid,
       patient.emrinpatientid,
       patient.emrinpatientdate,
       sign.empid_vchr,
       sign.lastname_vchr,
       sign.technicalrank_chr,
       sign.empno_chr,
       sign.psw_chr,
       sign.digitalsign_dta,
       sign.pycode_chr,
       sign.status_int,
       sign.technicallevel_chr,
       sign.cagetory_vchr,
       sign.formname_vchr,
       sign.registerid_vchr,
       sign.sign_int
  from firstillnessnoterecord a,
       firstillnessnoterecordcontent b,
       (select here.emrinpatientid, here.emrinpatientdate
          from t_opr_bih_register    reg,
               t_bse_bed             bed,
               t_bse_hisemr_relation here
         where reg.deptid_chr = ?
           and reg.status_int = 1
           and reg.pstatus_int <> 3
           and reg.bedid_chr = bed.bedid_chr
           and here.registerid_chr = reg.registerid_chr
           and bed.status_int = 2) patient,
       (select t.empid_vchr,
               d.lastname_vchr,
               d.technicalrank_chr,
               d.empno_chr,
               d.psw_chr,
               d.digitalsign_dta,
               d.pycode_chr,
               d.status_int,
               d.technicallevel_chr,
               t.cagetory_vchr,
               t.formname_vchr,
               t.registerid_vchr,
               t.sign_int
          from t_emr_signcollection t
          left outer join t_bse_employee d on t.empid_vchr = d.empid_chr
         order by d.technicallevel_chr desc) sign
 where a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and patient.emrinpatientid = a.inpatientid
   and patient.emrinpatientdate = a.inpatientdate
   and sign.sign_int = a.sequence_int
   and b.modifydate = (select max(modifydate)
                         from firstillnessnoterecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";
        #endregion 一般病程记录

        #region 首次病程记录 中医科
        private const string c_strGetRecordContentSQL_FirstIllnessNote_ZY = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.ifconfirm,
       a.confirmreason,
       a.confirmreasonxml,
       a.firstprintdate,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.mostlycontent,
       a.mostlycontentxml,
       a.originaldiagnose,
       a.originaldiagnosexml,
       a.thereunderdiagnose,
       a.thereunderdiagnosexml,
       a.diagnosediffe,
       a.diagnosediffexml,
       a.cureplan,
       a.cureplanxml,
       a.identifyreston,
       a.identifyrestonxml,
       a.identifydiagnose,
       a.identifydiagnosexml,
       a.opendate as opendate_main,
       b.inpatientid,
       b.inpatientdate,
       b.opendate,
       b.modifydate,
       b.modifyuserid,
       b.mostlycontent_right,
       b.originaldiagnose_right,
       b.thereunderdiagnose_right,
       b.diagnosediffe_right,
       b.cureplan_right,
       b.doctorid,
       b.identifyreston_right,
       b.identifydiagnose_right,
       patient.emrinpatientid,
       patient.emrinpatientdate
  from firstillnessnoterecord_gzzy a,
       firstillnessnoterecordcon_gzzy b,
       (select here.emrinpatientid, here.emrinpatientdate
          from t_opr_bih_register    reg,
               t_bse_bed             bed,
               t_bse_hisemr_relation here
         where reg.deptid_chr = ?
           and reg.status_int = 1
           and reg.pstatus_int <> 3
           and reg.bedid_chr = bed.bedid_chr
           and here.registerid_chr = reg.registerid_chr
           and bed.status_int = 2) patient
 where a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and patient.emrinpatientid = a.inpatientid
   and patient.emrinpatientdate = a.inpatientdate
   and b.modifydate = (select max(modifydate)
                         from firstillnessnoterecordcon_gzzy
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";
        #endregion 一般病程记录

        #region 术前小结
        private const string c_strGetRecordContentSQL_SummaryBeforeOP = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.firstprintdate,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.ifconfirm,
       a.markstatus,
       a.sequence_int,
       a.recorddate,
       a.registerid_chr,
       a.diseasesummary,
       a.diseasesummaryxml,
       a.diagnosisbeforeop,
       a.diagnosisbeforeopxml,
       a.diagnosisgist,
       a.diagnosisgistxml,
       a.opindication,
       a.opindicationxml,
       a.opmode,
       a.opmodexml,
       a.anamode,
       a.anamodexml,
       a.proceeding,
       a.proceedingxml,
       a.preparebeforeop,
       a.preparebeforeopxml,
       a.emr_seq,
       b.inpatientid,
       b.inpatientdate,
       b.opendate,
       b.modifydate,
       b.modifyuserid,
       b.status,
       b.registerid_chr,
       b.diseasesummary_right,
       b.diagnosisbeforeop_right,
       b.diagnosisgist_right,
       b.opindication_right,
       b.opmode_right,
       b.anamode_right,
       b.proceeding_right,
       b.preparebeforeop_right,
       b.emr_seq,
       a.opendate as opendate_main,
       patient.emrinpatientid,
       patient.emrinpatientdate,
       sign.empid_vchr,
       sign.lastname_vchr,
       sign.technicalrank_chr,
       sign.empno_chr,
       sign.psw_chr,
       sign.digitalsign_dta,
       sign.pycode_chr,
       sign.status_int,
       sign.technicallevel_chr,
       sign.cagetory_vchr,
       sign.formname_vchr,
       sign.registerid_vchr,
       sign.sign_int
  from t_emr_summarybeforeop a,
       t_emr_summarybeforeopcon b,
       (select here.emrinpatientid, here.emrinpatientdate
          from t_opr_bih_register    reg,
               t_bse_bed             bed,
               t_bse_hisemr_relation here
         where reg.deptid_chr = ?
           and reg.status_int = 1
           and reg.pstatus_int <> 3
           and reg.bedid_chr = bed.bedid_chr
           and here.registerid_chr = reg.registerid_chr
           and bed.status_int = 2) patient,
       (select t.empid_vchr,
               d.lastname_vchr,
               d.technicalrank_chr,
               d.empno_chr,
               d.psw_chr,
               d.digitalsign_dta,
               d.pycode_chr,
               d.status_int,
               d.technicallevel_chr,
               t.cagetory_vchr,
               t.formname_vchr,
               t.registerid_vchr,
               t.sign_int
          from t_emr_signcollection t
          left outer join t_bse_employee d on t.empid_vchr = d.empid_chr
         order by d.technicallevel_chr desc) sign
 where a.status = 0
   and a.emr_seq = b.emr_seq
   and patient.emrinpatientid = a.inpatientid
   and patient.emrinpatientdate = a.inpatientdate
   and sign.sign_int = a.sequence_int
   and b.status = 0";
        #endregion 

        #region 查询子病程记录的个数
        const string c_strGetIsRecordExists = @"select 
(select count(inpatientid) from generaldiseaserecord where 
(inpatientid) = ? and inpatientdate= ?  and status=0) generaldiseaserecord,
(select count(inpatientid) from handoverrecord where 
(inpatientid) = ? and inpatientdate= ?  and status=0) handoverrecord,
(select count(inpatientid) from takeoverrecord where 
(inpatientid) = ? and inpatientdate= ?  and status=0) takeoverrecord,
(select count(inpatientid) from diseasesummaryrecord where 
(inpatientid) = ? and inpatientdate= ?  and status=0) diseasesummaryrecord,
(select count(inpatientid) from conveyrecord where 
(inpatientid) = ? and inpatientdate= ?  and status=0) conveyrecord,
(select count(inpatientid) from turninrecord where 
(inpatientid) = ? and inpatientdate= ?  and status=0) turninrecord,
(select count(inpatientid) from checkroomrecord where 
(inpatientid) = ? and inpatientdate= ?  and status=0) checkroomrecord,
(select count(inpatientid) from casediscussrecord where 
(inpatientid) = ? and inpatientdate= ?  and status=0) casediscussrecord,
(select count(inpatientid) from beforeoperationdiscussrecord where 
(inpatientid) = ? and inpatientdate= ?  and status=0) beforeopdiscussrecord,
(select count(inpatientid) from deadcasediscussrecord where 
(inpatientid) = ? and inpatientdate= ?  and status=0) deadcasediscussrecord,
(select count(inpatientid) from deadrecord where 
(inpatientid) = ? and inpatientdate= ?  and status=0) deadrecord,
(select count(inpatientid) from afteroperationrecord where 
(inpatientid) = ? and inpatientdate= ?  and status=0) afteroperationrecord,
(select count(inpatientid) from saverecord where 
(inpatientid) = ? and inpatientdate= ?  and status=0) saverecord,
(select count(inpatientid) from firstillnessnoterecord where 
(inpatientid) = ? and inpatientdate= ?  and status=0) firstillnessnoterecord,
(select count(inpatientid) from firstillnessnoterecord_gzzy where 
(inpatientid) = ? and inpatientdate= ?  and status=0) firstillnesrec_gzzy,
(select count(inpatientid) from t_emr_summarybeforeop where 
(inpatientid) = ? and inpatientdate= ?  and status=0) emr_summarybeforeop";
        #endregion  

        #endregion        

        #region 先去数据库查看写了那些病程
        /// <summary>
        /// 先去数据库查看写了那些病程
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_dtmInpatientDate"></param>
        /// <param name="p_dtbRecordCount"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngCheckExistsRecord(string p_strInPatientID, DateTime p_dtmInpatientDate, out DataTable p_dtbRecordCount)
        {
            p_dtbRecordCount = new DataTable();
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(32, out objDPArr);
                for (int kk = 0; kk < objDPArr.Length; kk++)
                {
                    objDPArr[kk++].Value = p_strInPatientID;
                    objDPArr[kk].Value = p_dtmInpatientDate;
                }
                DataTable dtbRecordCount = new DataTable();
                string strSQL = c_strGetIsRecordExists;
                if (clsHRPTableService.bytDatabase_Selector == 2)
                {
                    strSQL += " from dual";
                }
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbRecordCount, objDPArr);
                if (lngRes <= 0 || dtbRecordCount.Rows.Count == 0)
                    return lngRes;
                p_dtbRecordCount = dtbRecordCount;
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion

        #region 获取一般病程记录
        /// <summary>
        /// 获取一般病程记录
        /// </summary>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_arlTransData"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetGeneralDiseaseRecord(string p_strDeptID, ref List<clsTransDataInfo> p_arlTransData)
        {
            //一般病程记录，使用c_strGetRecordContentSQL_Normal,//注意:此时查询条件中没有OpenDate,与一般病程记录的Server中SQL条件不同,故结果会有多行
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign =
                //    (com.digitalwave.PublicMiddleTier.clsPublicMiddleTier)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.PublicMiddleTier.clsPublicMiddleTier));

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);

                objDPArr[0].Value = p_strDeptID;

                //生成DataTable
                DataTable dtbContent = new DataTable();
                //执行查询，填充结果到DataTable       
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_Normal, ref dtbContent, objDPArr);


                //循环DataTable
                if (lngRes > 0 && dtbContent.Rows.Count > 0)
                {
                    DataTable dtbValue = dtbContent.DefaultView.ToTable(true,
                        "emrinpatientid", "emrinpatientdate", "OPENDATE_MAIN", "CREATEDATE", "MODIFYDATE", "FIRSTPRINTDATE",
                        "CREATEUSERID", "MODIFYUSERID", "IFCONFIRM", "STATUS", "CONFIRMREASON", "CONFIRMREASONXML", "RECORDTITLE",
                        "RECORDTITLETYPE", "RECORDCONTENT_RIGHT", "RECORDCONTENT", "RECORDCONTENTXML", "SEQUENCE_INT");
                    DataTable dtbSign = dtbContent.DefaultView.ToTable(true,
                        "empid_vchr", "lastname_vchr", "technicalrank_chr", "empno_chr", "psw_chr", "digitalsign_dta", "pycode_chr",
                        "status_int", "technicallevel_chr", "cagetory_vchr", "formname_vchr", "registerid_vchr","sign_int");
                    
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        #region 从DataTable.Rows中获取结果
                        //设置结果
                        clsGeneralDiseaseRecordContent objRecordContent = new clsGeneralDiseaseRecordContent();
                        objRecordContent.m_strInPatientID = dtbValue.Rows[j]["emrinpatientid"].ToString();
                        objRecordContent.m_dtmInPatientDate = Convert.ToDateTime(dtbValue.Rows[j]["emrinpatientdate"]);
                        objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());

                        if (dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString() == "")
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CREATEUSERID"].ToString();
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["MODIFYUSERID"].ToString();
                        if (dtbValue.Rows[j]["IFCONFIRM"].ToString() == "")
                            objRecordContent.m_bytIfConfirm = 0;
                        else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[j]["IFCONFIRM"].ToString());
                        if (dtbValue.Rows[j]["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());
                        objRecordContent.m_strConfirmReason = dtbValue.Rows[j]["CONFIRMREASON"].ToString();
                        objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[j]["CONFIRMREASONXML"].ToString();

                        objRecordContent.m_strRecordTitle = dtbValue.Rows[j]["RECORDTITLE"].ToString();
                        if (dtbValue.Rows[j]["RECORDTITLETYPE"].ToString() == "")
                            objRecordContent.m_intRecordTitleType = -1;
                        else objRecordContent.m_intRecordTitleType = int.Parse(dtbValue.Rows[j]["RECORDTITLETYPE"].ToString());
                        objRecordContent.m_strRecordContent_Right = dtbValue.Rows[j]["RECORDCONTENT_RIGHT"].ToString();
                        objRecordContent.m_strRecordContent = dtbValue.Rows[j]["RECORDCONTENT"].ToString();
                        objRecordContent.m_strRecordContentXml = dtbValue.Rows[j]["RECORDCONTENTXML"].ToString();

                        if (dtbValue.Rows[j]["SEQUENCE_INT"] != DBNull.Value)
                        {
                            DataView dtvSign = new DataView(dtbSign);
                            dtvSign.RowFilter = "sign_int = " + dtbValue.Rows[j]["SEQUENCE_INT"].ToString();
                            m_mthGetSignVO(dtvSign, out objRecordContent.objSignerArr);
                        }
                        #endregion 从DataTable.Rows中获取结果
                        //生成 clsTransDataInfo
                        clsTransDataInfo objInfo = new clsTransDataInfo();
                        objInfo.m_intFlag = (int)enmDiseaseTrackType.GeneralDisease;

                        //设置结果到 objInfo.m_objRecordContent
                        objInfo.m_objRecordContent = objRecordContent;

                        p_arlTransData.Add(objInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion

        #region 获取交班记录
        /// <summary>
        /// 获取交班记录
        /// </summary>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_arlTransData"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetHandOverRecord(string p_strDeptID, ref List<clsTransDataInfo> p_arlTransData)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign =
                //    (com.digitalwave.PublicMiddleTier.clsPublicMiddleTier)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.PublicMiddleTier.clsPublicMiddleTier));

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strDeptID;

                //生成DataTable
                /*DataTable*/
                DataTable dtbContent = new DataTable();
                //执行查询，填充结果到DataTable       
                /*long*/
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_HandOver, ref dtbContent, objDPArr);

                //循环DataTable
                if (lngRes > 0 && dtbContent.Rows.Count > 0)
                {
                    DataTable dtbValue = dtbContent.DefaultView.ToTable(true,
                        "emrinpatientid","emrinpatientdate","OPENDATE_MAIN","CREATEDATE","MODIFYDATE","FIRSTPRINTDATE","CREATEUSERID",
                        "MODIFYUSERID", "IFCONFIRM", "STATUS", "CONFIRMREASON", "CONFIRMREASONXML", "ORIGINALDIAGNOSE_RIGHT", "ORIGINALDIAGNOSE",
                        "ORIGINALDIAGNOSEXML", "CURRENTDIAGNOSE_RIGHT", "CURRENTDIAGNOSE", "CURRENTDIAGNOSEXML", "CASEHISTORY_RIGHT",
                        "CASEHISTORY", "CASEHISTORYXML", "REFERRAL_RIGHT", "REFERRAL", "REFERRALXML", "SEQUENCE_INT");
                    DataTable dtbSign = dtbContent.DefaultView.ToTable(true,
                        "empid_vchr", "lastname_vchr", "technicalrank_chr", "empno_chr", "psw_chr", "digitalsign_dta", "pycode_chr",
                        "status_int", "technicallevel_chr", "cagetory_vchr", "formname_vchr", "registerid_vchr", "sign_int");
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        #region 从DataTable.Rows中获取结果
                        //设置结果
                        clsHandOverRecordContent objRecordContent = new clsHandOverRecordContent();
                        objRecordContent.m_strInPatientID = dtbValue.Rows[j]["emrinpatientid"].ToString();
                        objRecordContent.m_dtmInPatientDate = Convert.ToDateTime(dtbValue.Rows[j]["emrinpatientdate"]);
                        objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());

                        if (dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString() == "")
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CREATEUSERID"].ToString();
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["MODIFYUSERID"].ToString();
                        if (dtbValue.Rows[j]["IFCONFIRM"].ToString() == "")
                            objRecordContent.m_bytIfConfirm = 0;
                        else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[j]["IFCONFIRM"].ToString());
                        if (dtbValue.Rows[j]["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());
                        objRecordContent.m_strConfirmReason = dtbValue.Rows[j]["CONFIRMREASON"].ToString();
                        objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[j]["CONFIRMREASONXML"].ToString();

                        objRecordContent.m_strOriginalDiagnose_Right = dtbValue.Rows[j]["ORIGINALDIAGNOSE_RIGHT"].ToString();
                        objRecordContent.m_strOriginalDiagnose = dtbValue.Rows[j]["ORIGINALDIAGNOSE"].ToString();
                        objRecordContent.m_strOriginalDiagnoseXML = dtbValue.Rows[j]["ORIGINALDIAGNOSEXML"].ToString();
                        objRecordContent.m_strCurrentDiagnose_Right = dtbValue.Rows[j]["CURRENTDIAGNOSE_RIGHT"].ToString();
                        objRecordContent.m_strCurrentDiagnose = dtbValue.Rows[j]["CURRENTDIAGNOSE"].ToString();
                        objRecordContent.m_strCurrentDiagnoseXML = dtbValue.Rows[j]["CURRENTDIAGNOSEXML"].ToString();
                        objRecordContent.m_strCaseHistory_Right = dtbValue.Rows[j]["CASEHISTORY_RIGHT"].ToString();
                        objRecordContent.m_strCaseHistory = dtbValue.Rows[j]["CASEHISTORY"].ToString();
                        objRecordContent.m_strCaseHistoryXML = dtbValue.Rows[j]["CASEHISTORYXML"].ToString();
                        objRecordContent.m_strReferral_Right = dtbValue.Rows[j]["REFERRAL_RIGHT"].ToString();
                        objRecordContent.m_strReferral = dtbValue.Rows[j]["REFERRAL"].ToString();
                        objRecordContent.m_strReferralXML = dtbValue.Rows[j]["REFERRALXML"].ToString();

                        if (dtbValue.Rows[j]["SEQUENCE_INT"] != DBNull.Value)
                        {
                            DataView dtvSign = new DataView(dtbSign);
                            dtvSign.RowFilter = "sign_int = " + dtbValue.Rows[j]["SEQUENCE_INT"].ToString();
                            m_mthGetSignVO(dtvSign, out objRecordContent.objSignerArr);
                        }
                        #endregion 从DataTable.Rows中获取结果
                        //生成 clsTransDataInfo
                        clsTransDataInfo objInfo = new clsTransDataInfo();
                        objInfo.m_intFlag = (int)enmDiseaseTrackType.HandOver;

                        //设置结果到 objInfo.m_objRecordContent
                        objInfo.m_objRecordContent = objRecordContent;

                        p_arlTransData.Add(objInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion

        #region 获取接班记录
        /// <summary>
        /// 获取接班记录
        /// </summary>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_arlTransData"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetTakeOverRecord(string p_strDeptID, ref List<clsTransDataInfo> p_arlTransData)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign =
                //    (com.digitalwave.PublicMiddleTier.clsPublicMiddleTier)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.PublicMiddleTier.clsPublicMiddleTier));

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strDeptID;

                //生成DataTable
                /*DataTable*/
                DataTable dtbContent = new DataTable();
                //执行查询，填充结果到DataTable       
                /*long*/
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_TakeOver, ref dtbContent, objDPArr);

                //循环DataTable
                if (lngRes > 0 && dtbContent.Rows.Count > 0)
                {
                    DataTable dtbValue = dtbContent.DefaultView.ToTable(true,
                        "emrinpatientid", "emrinpatientdate", "OPENDATE_MAIN", "CREATEDATE", "MODIFYDATE", "FIRSTPRINTDATE", "CREATEUSERID",
                         "MODIFYUSERID", "IFCONFIRM", "STATUS", "CONFIRMREASON", "CONFIRMREASONXML",
                        "ORIGINALDIAGNOSE_RIGHT", "ORIGINALDIAGNOSE", "ORIGINALDIAGNOSEXML", "CURRENTDIAGNOSE_RIGHT", "CURRENTDIAGNOSE", "CURRENTDIAGNOSEXML",
                        "CASEHISTORY_RIGHT", "CASEHISTORY", "CASEHISTORYXML", "REFERRAL_RIGHT", "REFERRAL", "REFERRALXML", "SEQUENCE_INT");
                    DataTable dtbSign = dtbContent.DefaultView.ToTable(true,
                        "empid_vchr", "lastname_vchr", "technicalrank_chr", "empno_chr", "psw_chr", "digitalsign_dta", "pycode_chr",
                        "status_int", "technicallevel_chr", "cagetory_vchr", "formname_vchr", "registerid_vchr", "sign_int");
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        #region 从DataTable.Rows中获取结果
                        //设置结果
                        clsTakeOverRecordContent objRecordContent = new clsTakeOverRecordContent();
                        objRecordContent.m_strInPatientID = dtbValue.Rows[j]["emrinpatientid"].ToString();
                        objRecordContent.m_dtmInPatientDate = Convert.ToDateTime(dtbValue.Rows[j]["emrinpatientdate"]);
                        objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());

                        if (dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString() == "")
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CREATEUSERID"].ToString();
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["MODIFYUSERID"].ToString();
                        if (dtbValue.Rows[j]["IFCONFIRM"].ToString() == "")
                            objRecordContent.m_bytIfConfirm = 0;
                        else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[j]["IFCONFIRM"].ToString());
                        if (dtbValue.Rows[j]["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());
                        objRecordContent.m_strConfirmReason = dtbValue.Rows[j]["CONFIRMREASON"].ToString();
                        objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[j]["CONFIRMREASONXML"].ToString();

                        objRecordContent.m_strOriginalDiagnose_Right = dtbValue.Rows[j]["ORIGINALDIAGNOSE_RIGHT"].ToString();
                        objRecordContent.m_strOriginalDiagnose = dtbValue.Rows[j]["ORIGINALDIAGNOSE"].ToString();
                        objRecordContent.m_strOriginalDiagnoseXML = dtbValue.Rows[j]["ORIGINALDIAGNOSEXML"].ToString();
                        objRecordContent.m_strCurrentDiagnose_Right = dtbValue.Rows[j]["CURRENTDIAGNOSE_RIGHT"].ToString();
                        objRecordContent.m_strCurrentDiagnose = dtbValue.Rows[j]["CURRENTDIAGNOSE"].ToString();
                        objRecordContent.m_strCurrentDiagnoseXML = dtbValue.Rows[j]["CURRENTDIAGNOSEXML"].ToString();
                        objRecordContent.m_strCaseHistory_Right = dtbValue.Rows[j]["CASEHISTORY_RIGHT"].ToString();
                        objRecordContent.m_strCaseHistory = dtbValue.Rows[j]["CASEHISTORY"].ToString();
                        objRecordContent.m_strCaseHistoryXML = dtbValue.Rows[j]["CASEHISTORYXML"].ToString();
                        objRecordContent.m_strReferral_Right = dtbValue.Rows[j]["REFERRAL_RIGHT"].ToString();
                        objRecordContent.m_strReferral = dtbValue.Rows[j]["REFERRAL"].ToString();
                        objRecordContent.m_strReferralXML = dtbValue.Rows[j]["REFERRALXML"].ToString();

                        if (dtbValue.Rows[j]["SEQUENCE_INT"] != DBNull.Value)
                        {
                            DataView dtvSign = new DataView(dtbSign);
                            dtvSign.RowFilter = "sign_int = " + dtbValue.Rows[j]["SEQUENCE_INT"].ToString();
                            m_mthGetSignVO(dtvSign, out objRecordContent.objSignerArr);
                        }
                        #endregion 从DataTable.Rows中获取结果
                        //生成 clsTransDataInfo
                        clsTransDataInfo objInfo = new clsTransDataInfo();
                        objInfo.m_intFlag = (int)enmDiseaseTrackType.TakeOver;

                        //设置结果到 objInfo.m_objRecordContent
                        objInfo.m_objRecordContent = objRecordContent;

                        p_arlTransData.Add(objInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion

        #region 获取病程小结
        /// <summary>
        /// 获取病程小结
        /// </summary>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_arlTransData"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDiseaseSummaryRecord(string p_strDeptID, ref List<clsTransDataInfo> p_arlTransData)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();

            try
            {
                //com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign =
                //    (com.digitalwave.PublicMiddleTier.clsPublicMiddleTier)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.PublicMiddleTier.clsPublicMiddleTier));

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strDeptID;

                //生成DataTable
                /*DataTable*/
                DataTable dtbContent = new DataTable();
                //执行查询，填充结果到DataTable       
                /*long*/
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_DiseaseSummary, ref dtbContent, objDPArr);

                //循环DataTable
                if (lngRes > 0 && dtbContent.Rows.Count > 0)
                {
                    DataTable dtbValue = dtbContent.DefaultView.ToTable(true,
                        "emrinpatientid", "emrinpatientdate", "OPENDATE_MAIN", "CREATEDATE", "MODIFYDATE",  "CREATEUSERID",
                        "MODIFYUSERID", "FIRSTPRINTDATE", "IFCONFIRM", "STATUS", "CONFIRMREASON", "CONFIRMREASONXML",
                        "INHOSPITALCASE_RIGHT", "INHOSPITALCASE", "INHOSPITALCASEXML", "INHOSPITALDIAGNOSE_RIGHT", "INHOSPITALDIAGNOSE", "INHOSPITALDIAGNOSEXML",
                        "DIAGNOSEBY_RIGHT", "DIAGNOSEBY", "DIAGNOSEBYXML", "CURRENTCASE_RIGHT", "CURRENTCASE", "CURRENTCASEXML", "CURRENTDIAGNOSE_RIGHT",
                        "CURRENTDIAGNOSE", "CURRENTDIAGNOSEXML", "REFERRAL_RIGHT", "REFERRAL", "REFERRALXML", "SEQUENCE_INT");
                    DataTable dtbSign = dtbContent.DefaultView.ToTable(true,
                        "empid_vchr", "lastname_vchr", "technicalrank_chr", "empno_chr", "psw_chr", "digitalsign_dta", "pycode_chr",
                        "status_int", "technicallevel_chr", "cagetory_vchr", "formname_vchr", "registerid_vchr", "sign_int");
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        #region 从DataTable.Rows中获取结果
                        //设置结果
                        clsDiseaseSummaryRecordContent objRecordContent = new clsDiseaseSummaryRecordContent();
                        objRecordContent.m_strInPatientID = dtbValue.Rows[j]["emrinpatientid"].ToString();
                        objRecordContent.m_dtmInPatientDate = Convert.ToDateTime(dtbValue.Rows[j]["emrinpatientdate"]);
                        objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());

                        if (dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString() == "")
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CREATEUSERID"].ToString();
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["MODIFYUSERID"].ToString();
                        if (dtbValue.Rows[j]["IFCONFIRM"].ToString() == "")
                            objRecordContent.m_bytIfConfirm = 0;
                        else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[j]["IFCONFIRM"].ToString());
                        if (dtbValue.Rows[j]["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());
                        objRecordContent.m_strConfirmReason = dtbValue.Rows[j]["CONFIRMREASON"].ToString();
                        objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[j]["CONFIRMREASONXML"].ToString();

                        objRecordContent.m_strInHospitalCase_Right = dtbValue.Rows[j]["INHOSPITALCASE_RIGHT"].ToString();
                        objRecordContent.m_strInHospitalCase = dtbValue.Rows[j]["INHOSPITALCASE"].ToString();
                        objRecordContent.m_strInHospitalCaseXML = dtbValue.Rows[j]["INHOSPITALCASEXML"].ToString();
                        objRecordContent.m_strInHospitalDiagnose_Right = dtbValue.Rows[j]["INHOSPITALDIAGNOSE_RIGHT"].ToString();
                        objRecordContent.m_strInHospitalDiagnose = dtbValue.Rows[j]["INHOSPITALDIAGNOSE"].ToString();
                        objRecordContent.m_strInHospitalDiagnoseXML = dtbValue.Rows[j]["INHOSPITALDIAGNOSEXML"].ToString();
                        objRecordContent.m_strDiagnoseBy_Right = dtbValue.Rows[j]["DIAGNOSEBY_RIGHT"].ToString();
                        objRecordContent.m_strDiagnoseBy = dtbValue.Rows[j]["DIAGNOSEBY"].ToString();
                        objRecordContent.m_strDiagnoseByXML = dtbValue.Rows[j]["DIAGNOSEBYXML"].ToString();
                        objRecordContent.m_strCurrentCase_Right = dtbValue.Rows[j]["CURRENTCASE_RIGHT"].ToString();
                        objRecordContent.m_strCurrentCase = dtbValue.Rows[j]["CURRENTCASE"].ToString();
                        objRecordContent.m_strCurrentCaseXML = dtbValue.Rows[j]["CURRENTCASEXML"].ToString();
                        objRecordContent.m_strCurrentDiagnose_Right = dtbValue.Rows[j]["CURRENTDIAGNOSE_RIGHT"].ToString();
                        objRecordContent.m_strCurrentDiagnose = dtbValue.Rows[j]["CURRENTDIAGNOSE"].ToString();
                        objRecordContent.m_strCurrentDiagnoseXML = dtbValue.Rows[j]["CURRENTDIAGNOSEXML"].ToString();
                        objRecordContent.m_strReferral_Right = dtbValue.Rows[j]["REFERRAL_RIGHT"].ToString();
                        objRecordContent.m_strReferral = dtbValue.Rows[j]["REFERRAL"].ToString();
                        objRecordContent.m_strReferralXML = dtbValue.Rows[j]["REFERRALXML"].ToString();

                        if (dtbValue.Rows[j]["SEQUENCE_INT"] != DBNull.Value)
                        {
                            DataView dtvSign = new DataView(dtbSign);
                            dtvSign.RowFilter = "sign_int = " + dtbValue.Rows[j]["SEQUENCE_INT"].ToString();
                            m_mthGetSignVO(dtvSign, out objRecordContent.objSignerArr);
                        }
                        #endregion 从DataTable.Rows中获取结果

                        //生成 clsTransDataInfo
                        clsTransDataInfo objInfo = new clsTransDataInfo();
                        objInfo.m_intFlag = (int)enmDiseaseTrackType.DiseaseSummary;

                        //设置结果到 objInfo.m_objRecordContent
                        objInfo.m_objRecordContent = objRecordContent;

                        p_arlTransData.Add(objInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion

        #region 获取转出记录
        /// <summary>
        /// 获取转出记录
        /// </summary>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_arlTransData"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetConveyRecord(string p_strDeptID, ref List<clsTransDataInfo> p_arlTransData)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign =
                //    (com.digitalwave.PublicMiddleTier.clsPublicMiddleTier)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.PublicMiddleTier.clsPublicMiddleTier));

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strDeptID;

                //生成DataTable
                /*DataTable*/
                DataTable dtbContent = new DataTable();
                //执行查询，填充结果到DataTable       
                /*long*/
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_Convey, ref dtbContent, objDPArr);

                //循环DataTable
                if (lngRes > 0 && dtbContent.Rows.Count > 0)
                {
                    DataTable dtbValue = dtbContent.DefaultView.ToTable(true,
                        "emrinpatientid", "emrinpatientdate", "OPENDATE_MAIN", "CREATEDATE", "MODIFYDATE", "FIRSTPRINTDATE", 
                        "MODIFYUSERID",  "CREATEUSERID",  "IFCONFIRM", "STATUS", "CONFIRMREASON", "CONFIRMREASONXML",
                        "ORIGINALDIAGNOSE_RIGHT", "ORIGINALDIAGNOSE", "ORIGINALDIAGNOSEXML", "CONVEYDIAGNOSE_RIGHT", "CONVEYDIAGNOSE", "CONVEYDIAGNOSEXML",
                        "CASEHISTORY_RIGHT", "CASEHISTORY", "CASEHISTORYXML", "CONSULTATION_RIGHT", "CONSULTATION", "CONSULTATIONXML", "CONVEYREASON_RIGHT",
                        "CONVEYREASON", "CONVEYREASONXML", "NOTICE_RIGHT", "NOTICE", "NOTICEXML", "SEQUENCE_INT");
                    DataTable dtbSign = dtbContent.DefaultView.ToTable(true,
                        "empid_vchr", "lastname_vchr", "technicalrank_chr", "empno_chr", "psw_chr", "digitalsign_dta", "pycode_chr",
                        "status_int", "technicallevel_chr", "cagetory_vchr", "formname_vchr", "registerid_vchr", "sign_int");
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        #region 从DataTable.Rows中获取结果
                        //设置结果
                        clsConveyRecordContent objRecordContent = new clsConveyRecordContent();
                        objRecordContent.m_strInPatientID = dtbValue.Rows[j]["emrinpatientid"].ToString();
                        objRecordContent.m_dtmInPatientDate = Convert.ToDateTime(dtbValue.Rows[j]["emrinpatientdate"]);
                        objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());

                        if (dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString() == "")
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CREATEUSERID"].ToString();
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["MODIFYUSERID"].ToString();
                        if (dtbValue.Rows[j]["IFCONFIRM"].ToString() == "")
                            objRecordContent.m_bytIfConfirm = 0;
                        else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[j]["IFCONFIRM"].ToString());
                        if (dtbValue.Rows[j]["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());
                        objRecordContent.m_strConfirmReason = dtbValue.Rows[j]["CONFIRMREASON"].ToString();
                        objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[j]["CONFIRMREASONXML"].ToString();

                        objRecordContent.m_strOriginalDiagnose_Right = dtbValue.Rows[j]["ORIGINALDIAGNOSE_RIGHT"].ToString();
                        objRecordContent.m_strOriginalDiagnose = dtbValue.Rows[j]["ORIGINALDIAGNOSE"].ToString();
                        objRecordContent.m_strOriginalDiagnoseXML = dtbValue.Rows[j]["ORIGINALDIAGNOSEXML"].ToString();
                        objRecordContent.m_strConveyDiagnose_Right = dtbValue.Rows[j]["CONVEYDIAGNOSE_RIGHT"].ToString();
                        objRecordContent.m_strConveyDiagnose = dtbValue.Rows[j]["CONVEYDIAGNOSE"].ToString();
                        objRecordContent.m_strConveyDiagnoseXML = dtbValue.Rows[j]["CONVEYDIAGNOSEXML"].ToString();
                        objRecordContent.m_strCaseHistory_Right = dtbValue.Rows[j]["CASEHISTORY_RIGHT"].ToString();
                        objRecordContent.m_strCaseHistory = dtbValue.Rows[j]["CASEHISTORY"].ToString();
                        objRecordContent.m_strCaseHistoryXML = dtbValue.Rows[j]["CASEHISTORYXML"].ToString();
                        objRecordContent.m_strConsultation_Right = dtbValue.Rows[j]["CONSULTATION_RIGHT"].ToString();
                        objRecordContent.m_strConsultation = dtbValue.Rows[j]["CONSULTATION"].ToString();
                        objRecordContent.m_strConsultationXML = dtbValue.Rows[j]["CONSULTATIONXML"].ToString();
                        objRecordContent.m_strConveyReason_Right = dtbValue.Rows[j]["CONVEYREASON_RIGHT"].ToString();
                        objRecordContent.m_strConveyReason = dtbValue.Rows[j]["CONVEYREASON"].ToString();
                        objRecordContent.m_strConveyReasonXML = dtbValue.Rows[j]["CONVEYREASONXML"].ToString();
                        objRecordContent.m_strNotice_Right = dtbValue.Rows[j]["NOTICE_RIGHT"].ToString();
                        objRecordContent.m_strNotice = dtbValue.Rows[j]["NOTICE"].ToString();
                        objRecordContent.m_strNoticeXML = dtbValue.Rows[j]["NOTICEXML"].ToString();

                        //						objRecordContent.m_strMainDoctorID=dtbValue.Rows[j]["MAINDOCTORID"].ToString();
                        //						objRecordContent.m_strMainDoctorName=dtbValue.Rows[j]["MAINDOCTORNAME"].ToString();
                        if (dtbValue.Rows[j]["SEQUENCE_INT"] != DBNull.Value)
                        {
                            DataView dtvSign = new DataView(dtbSign);
                            dtvSign.RowFilter = "sign_int = " + dtbValue.Rows[j]["SEQUENCE_INT"].ToString();
                            m_mthGetSignVO(dtvSign, out objRecordContent.objSignerArr);
                        }
                        #endregion 从DataTable.Rows中获取结果
                        //生成 clsTransDataInfo
                        clsTransDataInfo objInfo = new clsTransDataInfo();
                        objInfo.m_intFlag = (int)enmDiseaseTrackType.Convey;

                        //设置结果到 objInfo.m_objRecordContent
                        objInfo.m_objRecordContent = objRecordContent;

                        p_arlTransData.Add(objInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion

        #region 获取转入记录
        /// <summary>
        /// 获取转入记录
        /// </summary>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_arlTransData"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetTurnInRecord(string p_strDeptID, ref List<clsTransDataInfo> p_arlTransData)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign =
                //    (com.digitalwave.PublicMiddleTier.clsPublicMiddleTier)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.PublicMiddleTier.clsPublicMiddleTier));

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strDeptID;

                //生成DataTable
                /*DataTable*/
                DataTable dtbContent = new DataTable();
                //执行查询，填充结果到DataTable       
                /*long*/
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_TurnIn, ref dtbContent, objDPArr);

                //循环DataTable
                if (lngRes > 0 && dtbContent.Rows.Count > 0)
                {
                    DataTable dtbValue = dtbContent.DefaultView.ToTable(true,
                        "emrinpatientid", "emrinpatientdate", "OPENDATE_MAIN", "CREATEDATE", "MODIFYDATE", "FIRSTPRINTDATE", 
                        "MODIFYUSERID",  "CREATEUSERID",  "IFCONFIRM", "STATUS", "CONFIRMREASON", "CONFIRMREASONXML",
                        "CASEBEFORETURNIN_RIGHT", "CASEBEFORETURNIN", "CASEBEFORETURNINXML", "TURNINREASON_RIGHT", "TURNINREASON", "TURNINREASONXML",
                        "CASEAFTERTURNIN_RIGHT", "CASEAFTERTURNIN", "CASEAFTERTURNINXML", "TURNINDIAGNOSE_RIGHT", "TURNINDIAGNOSE", "TURNINDIAGNOSEXML",
                        "REFERRAL_RIGHT", "REFERRAL", "REFERRALXML", "SEQUENCE_INT");
                    DataTable dtbSign = dtbContent.DefaultView.ToTable(true,
                        "empid_vchr", "lastname_vchr", "technicalrank_chr", "empno_chr", "psw_chr", "digitalsign_dta", "pycode_chr",
                        "status_int", "technicallevel_chr", "cagetory_vchr", "formname_vchr", "registerid_vchr", "sign_int");
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        #region 从DataTable.Rows中获取结果
                        //设置结果
                        clsTurnInRecordContent objRecordContent = new clsTurnInRecordContent();
                        objRecordContent.m_strInPatientID = dtbValue.Rows[j]["emrinpatientid"].ToString();
                        objRecordContent.m_dtmInPatientDate = Convert.ToDateTime(dtbValue.Rows[j]["emrinpatientdate"]);
                        objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());

                        if (dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString() == "")
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CREATEUSERID"].ToString();
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["MODIFYUSERID"].ToString();
                        if (dtbValue.Rows[j]["IFCONFIRM"].ToString() == "")
                            objRecordContent.m_bytIfConfirm = 0;
                        else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[j]["IFCONFIRM"].ToString());
                        if (dtbValue.Rows[j]["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());
                        objRecordContent.m_strConfirmReason = dtbValue.Rows[j]["CONFIRMREASON"].ToString();
                        objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[j]["CONFIRMREASONXML"].ToString();

                        objRecordContent.m_strCaseBeforeTurnIn_Right = dtbValue.Rows[j]["CASEBEFORETURNIN_RIGHT"].ToString();
                        objRecordContent.m_strCaseBeforeTurnIn = dtbValue.Rows[j]["CASEBEFORETURNIN"].ToString();
                        objRecordContent.m_strCaseBeforeTurnInXML = dtbValue.Rows[j]["CASEBEFORETURNINXML"].ToString();
                        objRecordContent.m_strTurnInReason_Right = dtbValue.Rows[j]["TURNINREASON_RIGHT"].ToString();
                        objRecordContent.m_strTurnInReason = dtbValue.Rows[j]["TURNINREASON"].ToString();
                        objRecordContent.m_strTurnInReasonXML = dtbValue.Rows[j]["TURNINREASONXML"].ToString();
                        objRecordContent.m_strCaseAfterTurnIn_Right = dtbValue.Rows[j]["CASEAFTERTURNIN_RIGHT"].ToString();
                        objRecordContent.m_strCaseAfterTurnIn = dtbValue.Rows[j]["CASEAFTERTURNIN"].ToString();
                        objRecordContent.m_strCaseAfterTurnInXML = dtbValue.Rows[j]["CASEAFTERTURNINXML"].ToString();
                        objRecordContent.m_strTurnInDiagnose_Right = dtbValue.Rows[j]["TURNINDIAGNOSE_RIGHT"].ToString();
                        objRecordContent.m_strTurnInDiagnose = dtbValue.Rows[j]["TURNINDIAGNOSE"].ToString();
                        objRecordContent.m_strTurnInDiagnoseXML = dtbValue.Rows[j]["TURNINDIAGNOSEXML"].ToString();
                        objRecordContent.m_strReferral_Right = dtbValue.Rows[j]["REFERRAL_RIGHT"].ToString();
                        objRecordContent.m_strReferral = dtbValue.Rows[j]["REFERRAL"].ToString();
                        objRecordContent.m_strReferralXML = dtbValue.Rows[j]["REFERRALXML"].ToString();

                        if (dtbValue.Rows[j]["SEQUENCE_INT"] != DBNull.Value)
                        {
                            DataView dtvSign = new DataView(dtbSign);
                            dtvSign.RowFilter = "sign_int = " + dtbValue.Rows[j]["SEQUENCE_INT"].ToString();
                            m_mthGetSignVO(dtvSign, out objRecordContent.objSignerArr);
                        }
                        #endregion 从DataTable.Rows中获取结果
                        //生成 clsTransDataInfo
                        clsTransDataInfo objInfo = new clsTransDataInfo();
                        objInfo.m_intFlag = (int)enmDiseaseTrackType.TurnIn;

                        //设置结果到 objInfo.m_objRecordContent
                        objInfo.m_objRecordContent = objRecordContent;

                        p_arlTransData.Add(objInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion

        #region 获取查房记录
        /// <summary>
        /// 获取查房记录
        /// </summary>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_arlTransData"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCheckRoomRecord(string p_strDeptID, ref List<clsTransDataInfo> p_arlTransData)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign =
                //    (com.digitalwave.PublicMiddleTier.clsPublicMiddleTier)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.PublicMiddleTier.clsPublicMiddleTier));


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strDeptID;

                //生成DataTable
                /*DataTable*/
                DataTable dtbContent = new DataTable();
                //执行查询，填充结果到DataTable       
                /*long*/
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_CheckRoom, ref dtbContent, objDPArr);

                //循环DataTable
                if (lngRes > 0 && dtbContent.Rows.Count > 0)
                {
                    DataTable dtbValue = dtbContent.DefaultView.ToTable(true,
                        "emrinpatientid", "emrinpatientdate", "OPENDATE_MAIN", "CREATEDATE", "MODIFYDATE", "FIRSTPRINTDATE", 
                        "CREATEUSERID", "MODIFYUSERID", "IFCONFIRM", "STATUS", "CONFIRMREASON", "CONFIRMREASONXML",
                        "PATIENTSTATE_RIGHT", "PATIENTSTATE", "PATIENTSTATEXML", "DIAGNOSE_RIGHT", "DIAGNOSE", "DIAGNOSEXML", "DIFFERENTIATEDIAGNOSE_RIGHT",
                        "DIFFERENTIATEDIAGNOSE", "DIFFERENTIATEDIAGNOSEXML", "CURRENTCURE_RIGHT", "CURRENTCURE", "CURRENTCUREXML", "NEXTCURE_RIGHT",
                        "NEXTCURE", "NEXTCUREXML", "SEQUENCE_INT");
                    DataTable dtbSign = dtbContent.DefaultView.ToTable(true,
                        "empid_vchr", "lastname_vchr", "technicalrank_chr", "empno_chr", "psw_chr", "digitalsign_dta", "pycode_chr",
                        "status_int", "technicallevel_chr", "cagetory_vchr", "formname_vchr", "registerid_vchr", "sign_int");
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        #region 从DataTable.Rows中获取结果
                        //设置结果
                        clsCheckRoomRecordContent objRecordContent = new clsCheckRoomRecordContent();
                        objRecordContent.m_strInPatientID = dtbValue.Rows[j]["emrinpatientid"].ToString();
                        objRecordContent.m_dtmInPatientDate = Convert.ToDateTime(dtbValue.Rows[j]["emrinpatientdate"]);
                        objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());

                        if (dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString() == "")
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CREATEUSERID"].ToString();
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["MODIFYUSERID"].ToString();
                        if (dtbValue.Rows[j]["IFCONFIRM"].ToString() == "")
                            objRecordContent.m_bytIfConfirm = 0;
                        else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[j]["IFCONFIRM"].ToString());
                        if (dtbValue.Rows[j]["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());
                        objRecordContent.m_strConfirmReason = dtbValue.Rows[j]["CONFIRMREASON"].ToString();
                        objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[j]["CONFIRMREASONXML"].ToString();

                        objRecordContent.m_strPatientState_Right = dtbValue.Rows[j]["PATIENTSTATE_RIGHT"].ToString();
                        objRecordContent.m_strPatientState = dtbValue.Rows[j]["PATIENTSTATE"].ToString();
                        objRecordContent.m_strPatientStateXML = dtbValue.Rows[j]["PATIENTSTATEXML"].ToString();
                        objRecordContent.m_strDiagnose_Right = dtbValue.Rows[j]["DIAGNOSE_RIGHT"].ToString();
                        objRecordContent.m_strDiagnose = dtbValue.Rows[j]["DIAGNOSE"].ToString();
                        objRecordContent.m_strDiagnoseXML = dtbValue.Rows[j]["DIAGNOSEXML"].ToString();
                        objRecordContent.m_strDifferentiateDiagnose_Right = dtbValue.Rows[j]["DIFFERENTIATEDIAGNOSE_RIGHT"].ToString();
                        objRecordContent.m_strDifferentiateDiagnose = dtbValue.Rows[j]["DIFFERENTIATEDIAGNOSE"].ToString();
                        objRecordContent.m_strDifferentiateDiagnoseXML = dtbValue.Rows[j]["DIFFERENTIATEDIAGNOSEXML"].ToString();
                        objRecordContent.m_strCurrentCure_Right = dtbValue.Rows[j]["CURRENTCURE_RIGHT"].ToString();
                        objRecordContent.m_strCurrentCure = dtbValue.Rows[j]["CURRENTCURE"].ToString();
                        objRecordContent.m_strCurrentCureXML = dtbValue.Rows[j]["CURRENTCUREXML"].ToString();
                        objRecordContent.m_strNextCure_Right = dtbValue.Rows[j]["NEXTCURE_RIGHT"].ToString();
                        objRecordContent.m_strNextCure = dtbValue.Rows[j]["NEXTCURE"].ToString();
                        objRecordContent.m_strNextCureXML = dtbValue.Rows[j]["NEXTCUREXML"].ToString();

                        if (dtbValue.Rows[j]["SEQUENCE_INT"] != DBNull.Value)
                        {
                            DataView dtvSign = new DataView(dtbSign);
                            dtvSign.RowFilter = "sign_int = " + dtbValue.Rows[j]["SEQUENCE_INT"].ToString();
                            m_mthGetSignVO(dtvSign, out objRecordContent.objSignerArr);
                        }
                        #endregion 从DataTable.Rows中获取结果
                        //生成 clsTransDataInfo
                        clsTransDataInfo objInfo = new clsTransDataInfo();
                        objInfo.m_intFlag = (int)enmDiseaseTrackType.CheckRoom;

                        //设置结果到 objInfo.m_objRecordContent
                        objInfo.m_objRecordContent = objRecordContent;

                        p_arlTransData.Add(objInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion

        #region 获取病例讨论记录
        /// <summary>
        /// 获取病例讨论记录
        /// </summary>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_arlTransData"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCaseDiscussRecord(string p_strDeptID, ref List<clsTransDataInfo> p_arlTransData)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();

            try
            {
                //com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign =
                //    (com.digitalwave.PublicMiddleTier.clsPublicMiddleTier)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.PublicMiddleTier.clsPublicMiddleTier));

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strDeptID;

                //生成DataTable
                /*DataTable*/
                DataTable dtbContent = new DataTable();
                //执行查询，填充结果到DataTable       
                /*long*/
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_CaseDiscuss, ref dtbContent, objDPArr);

                //循环DataTable
                if (lngRes > 0 && dtbContent.Rows.Count > 0)
                {
                     DataTable dtbValue = dtbContent.DefaultView.ToTable(true,
                        "emrinpatientid", "emrinpatientdate", "OPENDATE_MAIN", "CREATEDATE", "MODIFYDATE", "FIRSTPRINTDATE", 
                        "CREATEUSERID", "MODIFYUSERID", "IFCONFIRM", "STATUS", "CONFIRMREASON", "CONFIRMREASONXML",
                        "ADDRESS_RIGHT", "ADDRESS", "ADDRESSXML", "DISCUSSCONTENT_RIGHT", "DISCUSSCONTENT", "DISCUSSCONTENTXML", "SEQUENCE_INT");
                    DataTable dtbSign = dtbContent.DefaultView.ToTable(true,
                        "empid_vchr", "lastname_vchr", "technicalrank_chr", "empno_chr", "psw_chr", "digitalsign_dta", "pycode_chr",
                        "status_int", "technicallevel_chr", "cagetory_vchr", "formname_vchr", "registerid_vchr", "sign_int");
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        #region 从DataTable.Rows中获取结果
                        //设置结果
                        clsCaseDiscussRecordContent objRecordContent = new clsCaseDiscussRecordContent();
                        objRecordContent.m_strInPatientID = dtbValue.Rows[j]["emrinpatientid"].ToString();
                        objRecordContent.m_dtmInPatientDate = Convert.ToDateTime(dtbValue.Rows[j]["emrinpatientdate"]);
                        objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());

                        if (dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString() == "")
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CREATEUSERID"].ToString();
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["MODIFYUSERID"].ToString();
                        if (dtbValue.Rows[j]["IFCONFIRM"].ToString() == "")
                            objRecordContent.m_bytIfConfirm = 0;
                        else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[j]["IFCONFIRM"].ToString());
                        if (dtbValue.Rows[j]["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());
                        objRecordContent.m_strConfirmReason = dtbValue.Rows[j]["CONFIRMREASON"].ToString();
                        objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[j]["CONFIRMREASONXML"].ToString();

                        objRecordContent.m_strAddress_Right = dtbValue.Rows[j]["ADDRESS_RIGHT"].ToString();
                        objRecordContent.m_strAddress = dtbValue.Rows[j]["ADDRESS"].ToString();
                        objRecordContent.m_strAddressXML = dtbValue.Rows[j]["ADDRESSXML"].ToString();
                        objRecordContent.m_strDiscussContent_Right = dtbValue.Rows[j]["DISCUSSCONTENT_RIGHT"].ToString();
                        objRecordContent.m_strDiscussContent = dtbValue.Rows[j]["DISCUSSCONTENT"].ToString();
                        objRecordContent.m_strDiscussContentXML = dtbValue.Rows[j]["DISCUSSCONTENTXML"].ToString();

                        if (dtbValue.Rows[j]["SEQUENCE_INT"] != DBNull.Value)
                        {
                            DataView dtvSign = new DataView(dtbSign);
                            dtvSign.RowFilter = "sign_int = " + dtbValue.Rows[j]["SEQUENCE_INT"].ToString();
                            m_mthGetSignVO(dtvSign, out objRecordContent.objSignerArr);
                        }
                        #endregion 从DataTable.Rows中获取结果
                        //生成 clsTransDataInfo
                        clsTransDataInfo objInfo = new clsTransDataInfo();
                        objInfo.m_intFlag = (int)enmDiseaseTrackType.CaseDiscuss;

                        //设置结果到 objInfo.m_objRecordContent
                        objInfo.m_objRecordContent = objRecordContent;

                        p_arlTransData.Add(objInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion

        #region 获取术前讨论记录
        /// <summary>
        /// 获取术前讨论记录
        /// </summary>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_arlTransData"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBeforeOperationDiscussRecord(string p_strDeptID, ref List<clsTransDataInfo> p_arlTransData)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign =
                //    (com.digitalwave.PublicMiddleTier.clsPublicMiddleTier)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.PublicMiddleTier.clsPublicMiddleTier));


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strDeptID;

                //生成DataTable
                /*DataTable*/
                DataTable dtbContent = new DataTable();
                //执行查询，填充结果到DataTable       
                /*long*/
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_BeforeOperationDiscuss, ref dtbContent, objDPArr);

                //循环DataTable
                if (lngRes > 0 && dtbContent.Rows.Count > 0)
                {
                    DataTable dtbValue = dtbContent.DefaultView.ToTable(true,
                        "emrinpatientid", "emrinpatientdate", "OPENDATE_MAIN", "CREATEDATE", "MODIFYDATE", "FIRSTPRINTDATE", 
                        "CREATEUSERID", "MODIFYUSERID", "IFCONFIRM", "STATUS", "CONFIRMREASON", "CONFIRMREASONXML",
                        "ADDRESS_RIGHT", "ADDRESS", "ADDRESSXML", "DISCUSSCONTENT_RIGHT", "DISCUSSCONTENT", "DISCUSSCONTENTXML", "SEQUENCE_INT");
                    DataTable dtbSign = dtbContent.DefaultView.ToTable(true,
                        "empid_vchr", "lastname_vchr", "technicalrank_chr", "empno_chr", "psw_chr", "digitalsign_dta", "pycode_chr",
                        "status_int", "technicallevel_chr", "cagetory_vchr", "formname_vchr", "registerid_vchr", "sign_int");
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        #region 从DataTable.Rows中获取结果
                        //设置结果
                        clsBeforeOperationDiscussRecordContent objRecordContent = new clsBeforeOperationDiscussRecordContent();
                        objRecordContent.m_strInPatientID = dtbValue.Rows[j]["emrinpatientid"].ToString();
                        objRecordContent.m_dtmInPatientDate = Convert.ToDateTime(dtbValue.Rows[j]["emrinpatientdate"]);
                        objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());

                        if (dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString() == "")
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CREATEUSERID"].ToString();
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["MODIFYUSERID"].ToString();
                        if (dtbValue.Rows[j]["IFCONFIRM"].ToString() == "")
                            objRecordContent.m_bytIfConfirm = 0;
                        else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[j]["IFCONFIRM"].ToString());
                        if (dtbValue.Rows[j]["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());
                        objRecordContent.m_strConfirmReason = dtbValue.Rows[j]["CONFIRMREASON"].ToString();
                        objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[j]["CONFIRMREASONXML"].ToString();

                        objRecordContent.m_strAddress_Right = dtbValue.Rows[j]["ADDRESS_RIGHT"].ToString();
                        objRecordContent.m_strAddress = dtbValue.Rows[j]["ADDRESS"].ToString();
                        objRecordContent.m_strAddressXML = dtbValue.Rows[j]["ADDRESSXML"].ToString();
                        objRecordContent.m_strDiscussContent_Right = dtbValue.Rows[j]["DISCUSSCONTENT_RIGHT"].ToString();
                        objRecordContent.m_strDiscussContent = dtbValue.Rows[j]["DISCUSSCONTENT"].ToString();
                        objRecordContent.m_strDiscussContentXML = dtbValue.Rows[j]["DISCUSSCONTENTXML"].ToString();


                        if (dtbValue.Rows[j]["SEQUENCE_INT"] != DBNull.Value)
                        {
                            DataView dtvSign = new DataView(dtbSign);
                            dtvSign.RowFilter = "sign_int = " + dtbValue.Rows[j]["SEQUENCE_INT"].ToString();
                            m_mthGetSignVO(dtvSign, out objRecordContent.objSignerArr);
                        }
                        #endregion 从DataTable.Rows中获取结果
                        //生成 clsTransDataInfo
                        clsTransDataInfo objInfo = new clsTransDataInfo();
                        objInfo.m_intFlag = (int)enmDiseaseTrackType.BeforeOperationDiscuss;

                        //设置结果到 objInfo.m_objRecordContent
                        objInfo.m_objRecordContent = objRecordContent;

                        p_arlTransData.Add(objInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion

        #region 获取死亡病例讨论记录
        /// <summary>
        /// 获取死亡病例讨论记录
        /// </summary>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_arlTransData"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeadCaseDiscussRecord(string p_strDeptID, ref List<clsTransDataInfo> p_arlTransData)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign =
                //    (com.digitalwave.PublicMiddleTier.clsPublicMiddleTier)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.PublicMiddleTier.clsPublicMiddleTier));


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strDeptID;

                //生成DataTable
                /*DataTable*/
                DataTable dtbContent = new DataTable();
                //执行查询，填充结果到DataTable       
                /*long*/
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_DeadCaseDiscuss, ref dtbContent, objDPArr);

                //循环DataTable
                if (lngRes > 0 && dtbContent.Rows.Count > 0)
                {
                    DataTable dtbValue = dtbContent.DefaultView.ToTable(true,
                        "emrinpatientid", "emrinpatientdate", "OPENDATE_MAIN", "CREATEDATE", "MODIFYDATE", "FIRSTPRINTDATE",
                        "CREATEUSERID", "MODIFYUSERID", "IFCONFIRM", "STATUS", "CONFIRMREASON", "CONFIRMREASONXML", "SEQUENCE_INT",
                        "ADDRESS_RIGHT", "ADDRESS", "ADDRESSXML", "DISCUSSCONTENT_RIGHT", "DISCUSSCONTENT", "DISCUSSCONTENTXML", "DEADDIAGNOSE_RIGHT",
                        "DEADDIAGNOSE", "DEADDIAGNOSEXML", "DEADREASON_RIGHT", "DEADREASON", "DEADREASONXML", "USEFORREFERENCE_RIGHT", "USEFORREFERENCE",
                        "USEFORREFERENCEXML");
                    DataTable dtbSign = dtbContent.DefaultView.ToTable(true,
                        "empid_vchr", "lastname_vchr", "technicalrank_chr", "empno_chr", "psw_chr", "digitalsign_dta", "pycode_chr",
                        "status_int", "technicallevel_chr", "cagetory_vchr", "formname_vchr", "registerid_vchr", "sign_int");
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        #region 从DataTable.Rows中获取结果
                        //设置结果
                        clsDeadCaseDiscussRecordContent objRecordContent = new clsDeadCaseDiscussRecordContent();
                        objRecordContent.m_strInPatientID = dtbValue.Rows[j]["emrinpatientid"].ToString();
                        objRecordContent.m_dtmInPatientDate = Convert.ToDateTime(dtbValue.Rows[j]["emrinpatientdate"]);
                        objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());

                        if (dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString() == "")
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CREATEUSERID"].ToString();
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["MODIFYUSERID"].ToString();
                        if (dtbValue.Rows[j]["IFCONFIRM"].ToString() == "")
                            objRecordContent.m_bytIfConfirm = 0;
                        else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[j]["IFCONFIRM"].ToString());
                        if (dtbValue.Rows[j]["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());
                        objRecordContent.m_strConfirmReason = dtbValue.Rows[j]["CONFIRMREASON"].ToString();
                        objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[j]["CONFIRMREASONXML"].ToString();

                        objRecordContent.m_strAddress_Right = dtbValue.Rows[j]["ADDRESS_RIGHT"].ToString();
                        objRecordContent.m_strAddress = dtbValue.Rows[j]["ADDRESS"].ToString();
                        objRecordContent.m_strAddressXML = dtbValue.Rows[j]["ADDRESSXML"].ToString();
                        objRecordContent.m_strDiscussContent_Right = dtbValue.Rows[j]["DISCUSSCONTENT_RIGHT"].ToString();
                        objRecordContent.m_strDiscussContent = dtbValue.Rows[j]["DISCUSSCONTENT"].ToString();
                        objRecordContent.m_strDiscussContentXML = dtbValue.Rows[j]["DISCUSSCONTENTXML"].ToString();
                        objRecordContent.m_strDeadDiagnose_Right = dtbValue.Rows[j]["DEADDIAGNOSE_RIGHT"].ToString();
                        objRecordContent.m_strDeadDiagnose = dtbValue.Rows[j]["DEADDIAGNOSE"].ToString();
                        objRecordContent.m_strDeadDiagnoseXML = dtbValue.Rows[j]["DEADDIAGNOSEXML"].ToString();
                        objRecordContent.m_strDeadReason_Right = dtbValue.Rows[j]["DEADREASON_RIGHT"].ToString();
                        objRecordContent.m_strDeadReason = dtbValue.Rows[j]["DEADREASON"].ToString();
                        objRecordContent.m_strDeadReasonXML = dtbValue.Rows[j]["DEADREASONXML"].ToString();
                        objRecordContent.m_strUseForReference_Right = dtbValue.Rows[j]["USEFORREFERENCE_RIGHT"].ToString();
                        objRecordContent.m_strUseForReference = dtbValue.Rows[j]["USEFORREFERENCE"].ToString();
                        objRecordContent.m_strUseForReferenceXML = dtbValue.Rows[j]["USEFORREFERENCEXML"].ToString();


                        if (dtbValue.Rows[j]["SEQUENCE_INT"] != DBNull.Value)
                        {
                            DataView dtvSign = new DataView(dtbSign);
                            dtvSign.RowFilter = "sign_int = " + dtbValue.Rows[j]["SEQUENCE_INT"].ToString();
                            m_mthGetSignVO(dtvSign, out objRecordContent.objSignerArr);
                        }
                        #endregion 从DataTable.Rows中获取结果
                        //生成 clsTransDataInfo
                        clsTransDataInfo objInfo = new clsTransDataInfo();
                        objInfo.m_intFlag = (int)enmDiseaseTrackType.DeadCaseDiscuss;

                        //设置结果到 objInfo.m_objRecordContent
                        objInfo.m_objRecordContent = objRecordContent;

                        p_arlTransData.Add(objInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion

        #region 获取死亡记录
        /// <summary>
        /// 获取死亡记录
        /// </summary>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_arlTransData"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeadRecord(string p_strDeptID, ref List<clsTransDataInfo> p_arlTransData)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign =
                //    (com.digitalwave.PublicMiddleTier.clsPublicMiddleTier)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.PublicMiddleTier.clsPublicMiddleTier));


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strDeptID;

                //生成DataTable
                /*DataTable*/
                DataTable dtbContent = new DataTable();
                //执行查询，填充结果到DataTable       
                /*long*/
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_Dead, ref dtbContent, objDPArr);

                //循环DataTable
                if (lngRes > 0 && dtbContent.Rows.Count > 0)
                {
                    DataTable dtbValue = dtbContent.DefaultView.ToTable(true,
                        "emrinpatientid", "emrinpatientdate", "OPENDATE_MAIN", "CREATEDATE", "MODIFYDATE", "FIRSTPRINTDATE",
                        "CREATEUSERID", "MODIFYUSERID", "IFCONFIRM", "STATUS", "CONFIRMREASON", "CONFIRMREASONXML", "SEQUENCE_INT",
                        "DEADDATE", "INHOSPITALCASE_RIGHT", "INHOSPITALCASE", "INHOSPITALCASEXML", "ORIGINALDIAGNOSE_RIGHT", "ORIGINALDIAGNOSE",
                        "ORIGINALDIAGNOSEXML", "DIAGNOSEBY_RIGHT", "DIAGNOSEBY", "DIAGNOSEBYXML", "DEADDIAGNOSE_RIGHT", "DEADDIAGNOSE", "DEADDIAGNOSEXML",
                        "DEADREASON_RIGHT", "DEADREASON", "DEADREASONXML", "EXPERIENCE_RIGHT", "EXPERIENCE", "EXPERIENCEXML");
                    DataTable dtbSign = dtbContent.DefaultView.ToTable(true,
                        "empid_vchr", "lastname_vchr", "technicalrank_chr", "empno_chr", "psw_chr", "digitalsign_dta", "pycode_chr",
                        "status_int", "technicallevel_chr", "cagetory_vchr", "formname_vchr", "registerid_vchr", "sign_int");
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        #region 从DataTable.Rows中获取结果
                        //设置结果
                        clsDeadRecordContent objRecordContent = new clsDeadRecordContent();
                        objRecordContent.m_strInPatientID = dtbValue.Rows[j]["emrinpatientid"].ToString();
                        objRecordContent.m_dtmInPatientDate = Convert.ToDateTime(dtbValue.Rows[j]["emrinpatientdate"]);
                        objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());

                        if (dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString() == "")
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CREATEUSERID"].ToString();
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["MODIFYUSERID"].ToString();
                        if (dtbValue.Rows[j]["IFCONFIRM"].ToString() == "")
                            objRecordContent.m_bytIfConfirm = 0;
                        else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[j]["IFCONFIRM"].ToString());
                        if (dtbValue.Rows[j]["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());
                        objRecordContent.m_strConfirmReason = dtbValue.Rows[j]["CONFIRMREASON"].ToString();
                        objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[j]["CONFIRMREASONXML"].ToString();

                        objRecordContent.m_dtmDeadDate = DateTime.Parse(dtbValue.Rows[j]["DEADDATE"].ToString());
                        objRecordContent.m_strInHospitalCase_Right = dtbValue.Rows[j]["INHOSPITALCASE_RIGHT"].ToString();
                        objRecordContent.m_strInHospitalCase = dtbValue.Rows[j]["INHOSPITALCASE"].ToString();
                        objRecordContent.m_strInHospitalCaseXML = dtbValue.Rows[j]["INHOSPITALCASEXML"].ToString();
                        objRecordContent.m_strOriginalDiagnose_Right = dtbValue.Rows[j]["ORIGINALDIAGNOSE_RIGHT"].ToString();
                        objRecordContent.m_strOriginalDiagnose = dtbValue.Rows[j]["ORIGINALDIAGNOSE"].ToString();
                        objRecordContent.m_strOriginalDiagnoseXML = dtbValue.Rows[j]["ORIGINALDIAGNOSEXML"].ToString();
                        objRecordContent.m_strDiagnoseBy_Right = dtbValue.Rows[j]["DIAGNOSEBY_RIGHT"].ToString();
                        objRecordContent.m_strDiagnoseBy = dtbValue.Rows[j]["DIAGNOSEBY"].ToString();
                        objRecordContent.m_strDiagnoseByXML = dtbValue.Rows[j]["DIAGNOSEBYXML"].ToString();
                        objRecordContent.m_strDeadDiagnose_Right = dtbValue.Rows[j]["DEADDIAGNOSE_RIGHT"].ToString();
                        objRecordContent.m_strDeadDiagnose = dtbValue.Rows[j]["DEADDIAGNOSE"].ToString();
                        objRecordContent.m_strDeadDiagnoseXML = dtbValue.Rows[j]["DEADDIAGNOSEXML"].ToString();
                        objRecordContent.m_strDeadReason_Right = dtbValue.Rows[j]["DEADREASON_RIGHT"].ToString();
                        objRecordContent.m_strDeadReason = dtbValue.Rows[j]["DEADREASON"].ToString();
                        objRecordContent.m_strDeadReasonXML = dtbValue.Rows[j]["DEADREASONXML"].ToString();
                        objRecordContent.m_strExperience_Right = dtbValue.Rows[j]["EXPERIENCE_RIGHT"].ToString();
                        objRecordContent.m_strExperience = dtbValue.Rows[j]["EXPERIENCE"].ToString();
                        objRecordContent.m_strExperienceXML = dtbValue.Rows[j]["EXPERIENCEXML"].ToString();

                        if (dtbValue.Rows[j]["SEQUENCE_INT"] != DBNull.Value)
                        {
                            DataView dtvSign = new DataView(dtbSign);
                            dtvSign.RowFilter = "sign_int = " + dtbValue.Rows[j]["SEQUENCE_INT"].ToString();
                            m_mthGetSignVO(dtvSign, out objRecordContent.objSignerArr);
                        }
                        #endregion 从DataTable.Rows中获取结果
                        //生成 clsTransDataInfo
                        clsTransDataInfo objInfo = new clsTransDataInfo();
                        objInfo.m_intFlag = (int)enmDiseaseTrackType.Dead;

                        //设置结果到 objInfo.m_objRecordContent
                        objInfo.m_objRecordContent = objRecordContent;

                        p_arlTransData.Add(objInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion

        #region 获取手术后病程记录
        /// <summary>
        /// 获取手术后病程记录
        /// </summary>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_arlTransData"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAfterOperationRecord(string p_strDeptID, ref List<clsTransDataInfo> p_arlTransData)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign =
                //    (com.digitalwave.PublicMiddleTier.clsPublicMiddleTier)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.PublicMiddleTier.clsPublicMiddleTier));

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strDeptID;

                //生成DataTable
                /*DataTable*/
                DataTable dtbContent = new DataTable();
                //执行查询，填充结果到DataTable       
                /*long*/
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_AfterOperation, ref dtbContent, objDPArr);

                //循环DataTable
                if (lngRes > 0 && dtbContent.Rows.Count > 0)
                {
                    DataTable dtbValue = dtbContent.DefaultView.ToTable(true,
                        "emrinpatientid", "emrinpatientdate", "OPENDATE_MAIN", "CREATEDATE", "MODIFYDATE", "FIRSTPRINTDATE",
                        "CREATEUSERID", "MODIFYUSERID", "IFCONFIRM", "STATUS", "CONFIRMREASON", "CONFIRMREASONXML", "SEQUENCE_INT",
                        "ANAESTHESIAMODE_RIGHT", "ANAESTHESIAMODE", "ANAESTHESIAMODEXML", "OPERATIONNAME_RIGHT", "OPERATIONNAME", "OPERATIONNAMEXML",
                        "OPERATIONDIAGNOSE_RIGHT", "OPERATIONDIAGNOSE", "OPERATIONDIAGNOSEXML", "INOPERATIONSEEING_RIGHT", "INOPERATIONSEEING",
                        "INOPERATIONSEEINGXML", "AFTEROPERATIONDEAL_RIGHT", "AFTEROPERATIONDEAL", "AFTEROPERATIONDEALXML", "AFTEROPERATIONNOTICE_RIGHT",
                        "AFTEROPERATIONNOTICE", "AFTEROPERATIONNOTICEXML", "CUTHEALUPSTATUS_RIGHT", "CUTHEALUPSTATUS", "CUTHEALUPSTATUSXML", "TAKEOUTSTITCHESDATE");
                    DataTable dtbSign = dtbContent.DefaultView.ToTable(true,
                        "empid_vchr", "lastname_vchr", "technicalrank_chr", "empno_chr", "psw_chr", "digitalsign_dta", "pycode_chr",
                        "status_int", "technicallevel_chr", "cagetory_vchr", "formname_vchr", "registerid_vchr", "sign_int");
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        #region 从DataTable.Rows中获取结果
                        //设置结果
                        clsAfterOperationRecordContent objRecordContent = new clsAfterOperationRecordContent();
                        objRecordContent.m_strInPatientID = dtbValue.Rows[j]["emrinpatientid"].ToString();
                        objRecordContent.m_dtmInPatientDate = Convert.ToDateTime(dtbValue.Rows[j]["emrinpatientdate"]);
                        objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());

                        if (dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString() == "")
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CREATEUSERID"].ToString();
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["MODIFYUSERID"].ToString();
                        if (dtbValue.Rows[j]["IFCONFIRM"].ToString() == "")
                            objRecordContent.m_bytIfConfirm = 0;
                        else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[j]["IFCONFIRM"].ToString());
                        if (dtbValue.Rows[j]["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());
                        objRecordContent.m_strConfirmReason = dtbValue.Rows[j]["CONFIRMREASON"].ToString();
                        objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[j]["CONFIRMREASONXML"].ToString();

                        objRecordContent.m_strAnaesthesiaMode_Right = dtbValue.Rows[j]["ANAESTHESIAMODE_RIGHT"].ToString();
                        objRecordContent.m_strAnaesthesiaMode = dtbValue.Rows[j]["ANAESTHESIAMODE"].ToString();
                        objRecordContent.m_strAnaesthesiaModeXML = dtbValue.Rows[j]["ANAESTHESIAMODEXML"].ToString();
                        objRecordContent.m_strOperationName_Right = dtbValue.Rows[j]["OPERATIONNAME_RIGHT"].ToString();
                        objRecordContent.m_strOperationName = dtbValue.Rows[j]["OPERATIONNAME"].ToString();
                        objRecordContent.m_strOperationNameXML = dtbValue.Rows[j]["OPERATIONNAMEXML"].ToString();
                        objRecordContent.m_strOperationDiagnose_Right = dtbValue.Rows[j]["OPERATIONDIAGNOSE_RIGHT"].ToString();
                        objRecordContent.m_strOperationDiagnose = dtbValue.Rows[j]["OPERATIONDIAGNOSE"].ToString();
                        objRecordContent.m_strOperationDiagnoseXML = dtbValue.Rows[j]["OPERATIONDIAGNOSEXML"].ToString();
                        objRecordContent.m_strInOperationSeeing_Right = dtbValue.Rows[j]["INOPERATIONSEEING_RIGHT"].ToString();
                        objRecordContent.m_strInOperationSeeing = dtbValue.Rows[j]["INOPERATIONSEEING"].ToString();
                        objRecordContent.m_strInOperationSeeingXML = dtbValue.Rows[j]["INOPERATIONSEEINGXML"].ToString();
                        objRecordContent.m_strAfterOperationDeal_Right = dtbValue.Rows[j]["AFTEROPERATIONDEAL_RIGHT"].ToString();
                        objRecordContent.m_strAfterOperationDeal = dtbValue.Rows[j]["AFTEROPERATIONDEAL"].ToString();
                        objRecordContent.m_strAfterOperationDealXML = dtbValue.Rows[j]["AFTEROPERATIONDEALXML"].ToString();

                        objRecordContent.m_strAfterOperationNotice_Right = dtbValue.Rows[j]["AFTEROPERATIONNOTICE_RIGHT"].ToString();
                        objRecordContent.m_strAfterOperationNotice = dtbValue.Rows[j]["AFTEROPERATIONNOTICE"].ToString();
                        objRecordContent.m_strAfterOperationNoticeXML = dtbValue.Rows[j]["AFTEROPERATIONNOTICEXML"].ToString();
                        objRecordContent.m_strCutHealUpStatus_Right = dtbValue.Rows[j]["CUTHEALUPSTATUS_RIGHT"].ToString();
                        objRecordContent.m_strCutHealUpStatus = dtbValue.Rows[j]["CUTHEALUPSTATUS"].ToString();
                        objRecordContent.m_strCutHealUpStatusXML = dtbValue.Rows[j]["CUTHEALUPSTATUSXML"].ToString();
                        if (dtbValue.Rows[j]["TAKEOUTSTITCHESDATE"].ToString() == "")
                            objRecordContent.m_dtmTakeOutStitchesDate = DateTime.MinValue;
                        else objRecordContent.m_dtmTakeOutStitchesDate = DateTime.Parse(dtbValue.Rows[j]["TAKEOUTSTITCHESDATE"].ToString());

                        if (dtbValue.Rows[j]["SEQUENCE_INT"] != DBNull.Value)
                        {
                            DataView dtvSign = new DataView(dtbSign);
                            dtvSign.RowFilter = "sign_int = " + dtbValue.Rows[j]["SEQUENCE_INT"].ToString();
                            m_mthGetSignVO(dtvSign, out objRecordContent.objSignerArr);
                        }
                        #endregion 从DataTable.Rows中获取结果
                        //生成 clsTransDataInfo
                        clsTransDataInfo objInfo = new clsTransDataInfo();
                        objInfo.m_intFlag = (int)enmDiseaseTrackType.AfterOperation;

                        //设置结果到 objInfo.m_objRecordContent
                        objInfo.m_objRecordContent = objRecordContent;

                        p_arlTransData.Add(objInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion

        #region 获取抢救记录
        /// <summary>
        /// 获取抢救记录
        /// </summary>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_arlTransData"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSaveRecord(string p_strDeptID, ref List<clsTransDataInfo> p_arlTransData)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign =
                //    (com.digitalwave.PublicMiddleTier.clsPublicMiddleTier)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.PublicMiddleTier.clsPublicMiddleTier));

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strDeptID;

                //生成DataTable
                /*DataTable*/
                DataTable dtbContent = new DataTable();
                //执行查询，填充结果到DataTable       
                /*long*/
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_Save, ref dtbContent, objDPArr);

                //循环DataTable
                if (lngRes > 0 && dtbContent.Rows.Count > 0)
                {
                    DataTable dtbValue = dtbContent.DefaultView.ToTable(true,
                        "emrinpatientid", "emrinpatientdate", "OPENDATE_MAIN", "CREATEDATE", "MODIFYDATE", "FIRSTPRINTDATE",
                        "CREATEUSERID", "MODIFYUSERID", "IFCONFIRM", "STATUS", "CONFIRMREASON", "CONFIRMREASONXML", "SEQUENCE_INT",
                        "SAVETIME", "DISEASENAME_RIGHT", "DISEASENAME", "DISEASENAMEXML", "DISEASECHANGECASE_RIGHT", "DISEASECHANGECASE",
                        "DISEASECHANGECASEXML", "SAVEDEAL_RIGHT", "SAVEDEAL", "SAVEDEALXML", "SAVERESULT_RIGHT", "SAVERESULT", "SAVERESULTXML");
                    DataTable dtbSign = dtbContent.DefaultView.ToTable(true,
                        "empid_vchr", "lastname_vchr", "technicalrank_chr", "empno_chr", "psw_chr", "digitalsign_dta", "pycode_chr",
                        "status_int", "technicallevel_chr", "cagetory_vchr", "formname_vchr", "registerid_vchr", "sign_int");
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        #region 从DataTable.Rows中获取结果
                        //设置结果
                        clsSaveRecordContent objRecordContent = new clsSaveRecordContent();
                        objRecordContent.m_strInPatientID = dtbValue.Rows[j]["emrinpatientid"].ToString();
                        objRecordContent.m_dtmInPatientDate = Convert.ToDateTime(dtbValue.Rows[j]["emrinpatientdate"]);
                        objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());

                        if (dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString() == "")
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CREATEUSERID"].ToString();
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["MODIFYUSERID"].ToString();
                        if (dtbValue.Rows[j]["IFCONFIRM"].ToString() == "")
                            objRecordContent.m_bytIfConfirm = 0;
                        else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[j]["IFCONFIRM"].ToString());
                        if (dtbValue.Rows[j]["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());
                        objRecordContent.m_strConfirmReason = dtbValue.Rows[j]["CONFIRMREASON"].ToString();
                        objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[j]["CONFIRMREASONXML"].ToString();

                        objRecordContent.m_dtmSaveTime = DateTime.Parse(dtbValue.Rows[j]["SAVETIME"].ToString());
                        objRecordContent.m_strDiseaseName_Right = dtbValue.Rows[j]["DISEASENAME_RIGHT"].ToString();
                        objRecordContent.m_strDiseaseName = dtbValue.Rows[j]["DISEASENAME"].ToString();
                        objRecordContent.m_strDiseaseNameXML = dtbValue.Rows[j]["DISEASENAMEXML"].ToString();
                        objRecordContent.m_strDiseaseChangeCase_Right = dtbValue.Rows[j]["DISEASECHANGECASE_RIGHT"].ToString();
                        objRecordContent.m_strDiseaseChangeCase = dtbValue.Rows[j]["DISEASECHANGECASE"].ToString();
                        objRecordContent.m_strDiseaseChangeCaseXML = dtbValue.Rows[j]["DISEASECHANGECASEXML"].ToString();
                        objRecordContent.m_strSaveDeal_Right = dtbValue.Rows[j]["SAVEDEAL_RIGHT"].ToString();
                        objRecordContent.m_strSaveDeal = dtbValue.Rows[j]["SAVEDEAL"].ToString();
                        objRecordContent.m_strSaveDealXML = dtbValue.Rows[j]["SAVEDEALXML"].ToString();
                        objRecordContent.m_strSaveResult_Right = dtbValue.Rows[j]["SAVERESULT_RIGHT"].ToString();
                        objRecordContent.m_strSaveResult = dtbValue.Rows[j]["SAVERESULT"].ToString();
                        objRecordContent.m_strSaveResultXML = dtbValue.Rows[j]["SAVERESULTXML"].ToString();

                        if (dtbValue.Rows[j]["SEQUENCE_INT"] != DBNull.Value)
                        {
                            DataView dtvSign = new DataView(dtbSign);
                            dtvSign.RowFilter = "sign_int = " + dtbValue.Rows[j]["SEQUENCE_INT"].ToString();
                            m_mthGetSignVO(dtvSign, out objRecordContent.objSignerArr);
                        }
                        #endregion 从DataTable.Rows中获取结果
                        //生成 clsTransDataInfo
                        clsTransDataInfo objInfo = new clsTransDataInfo();
                        objInfo.m_intFlag = (int)enmDiseaseTrackType.Save;

                        //设置结果到 objInfo.m_objRecordContent
                        objInfo.m_objRecordContent = objRecordContent;

                        p_arlTransData.Add(objInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion

        #region 获取首次病程记录
        /// <summary>
        /// 获取首次病程记录
        /// </summary>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_arlTransData"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetFirstIllnessNoteRecord(string p_strDeptID, ref List<clsTransDataInfo> p_arlTransData)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign =
                //    (com.digitalwave.PublicMiddleTier.clsPublicMiddleTier)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.PublicMiddleTier.clsPublicMiddleTier));

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strDeptID;

                //生成DataTable
                /*DataTable*/
                DataTable dtbContent = new DataTable();
                //执行查询，填充结果到DataTable       
                /*long*/
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_FirstIllnessNote, ref dtbContent, objDPArr);

                //循环DataTable
                if (lngRes > 0 && dtbContent.Rows.Count > 0)
                {
                    DataTable dtbValue = dtbContent.DefaultView.ToTable(true,
                    "emrinpatientid", "emrinpatientdate", "OPENDATE_MAIN", "CREATEDATE", "MODIFYDATE", "FIRSTPRINTDATE",
                    "CREATEUSERID", "MODIFYUSERID", "IFCONFIRM", "STATUS", "CONFIRMREASON", "CONFIRMREASONXML", "SEQUENCE_INT",
                    "MostlyContent_Right", "MostlyContent", "MostlyContentXML", "OriginalDiagnose_Right", "OriginalDiagnose", "OriginalDiagnoseXML",
                    "ThereunderDiagnose_Right", "ThereunderDiagnose", "ThereunderDiagnoseXML", "DiagnoseDiffe_Right", "DiagnoseDiffe",
                    "DiagnoseDiffeXML", "CurePlan_Right", "CurePlan", "CurePlanXML");
                    DataTable dtbSign = dtbContent.DefaultView.ToTable(true,
                    "empid_vchr", "lastname_vchr", "technicalrank_chr", "empno_chr", "psw_chr", "digitalsign_dta", "pycode_chr",
                    "status_int", "technicallevel_chr", "cagetory_vchr", "formname_vchr", "registerid_vchr", "sign_int");
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        #region 从DataTable.Rows中获取结果
                        //设置结果
                        clsFirstIllnessNoteRecordContent objRecordContent = new clsFirstIllnessNoteRecordContent();
                        objRecordContent.m_strInPatientID = dtbValue.Rows[j]["emrinpatientid"].ToString();
                        objRecordContent.m_dtmInPatientDate = Convert.ToDateTime(dtbValue.Rows[j]["emrinpatientdate"]);
                        objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OpenDate_Main"].ToString());
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CreateDate"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["ModifyDate"].ToString());

                        if (dtbValue.Rows[j]["FirstPrintDate"].ToString() == "")
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FirstPrintDate"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CreateUserID"].ToString();
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["ModifyUserID"].ToString();
                        if (dtbValue.Rows[j]["IfConfirm"].ToString() == "")
                            objRecordContent.m_bytIfConfirm = 0;
                        else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[j]["IfConfirm"].ToString());
                        if (dtbValue.Rows[j]["Status"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[j]["Status"].ToString());
                        objRecordContent.m_strConfirmReason = dtbValue.Rows[j]["ConfirmReason"].ToString();
                        objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[j]["ConfirmReasonXML"].ToString();

                        objRecordContent.m_strMostlyContent_Right = dtbValue.Rows[j]["MostlyContent_Right"].ToString();
                        objRecordContent.m_strMostlyContent = dtbValue.Rows[j]["MostlyContent"].ToString();
                        objRecordContent.m_strMostlyContentXML = dtbValue.Rows[j]["MostlyContentXML"].ToString();
                        objRecordContent.m_strOriginalDiagnose_Right = dtbValue.Rows[j]["OriginalDiagnose_Right"].ToString();
                        objRecordContent.m_strOriginalDiagnose = dtbValue.Rows[j]["OriginalDiagnose"].ToString();
                        objRecordContent.m_strOriginalDiagnoseXML = dtbValue.Rows[j]["OriginalDiagnoseXML"].ToString();
                        objRecordContent.m_strThereunderDiagnose_Right = dtbValue.Rows[j]["ThereunderDiagnose_Right"].ToString();
                        objRecordContent.m_strThereunderDiagnose = dtbValue.Rows[j]["ThereunderDiagnose"].ToString();
                        objRecordContent.m_strThereunderDiagnoseXML = dtbValue.Rows[j]["ThereunderDiagnoseXML"].ToString();
                        objRecordContent.m_strDiagnoseDiffe_Right = dtbValue.Rows[j]["DiagnoseDiffe_Right"].ToString();
                        objRecordContent.m_strDiagnoseDiffe = dtbValue.Rows[j]["DiagnoseDiffe"].ToString();
                        objRecordContent.m_strDiagnoseDiffeXML = dtbValue.Rows[j]["DiagnoseDiffeXML"].ToString();
                        objRecordContent.m_strCurePlan_Right = dtbValue.Rows[j]["CurePlan_Right"].ToString();
                        objRecordContent.m_strCurePlan = dtbValue.Rows[j]["CurePlan"].ToString();
                        objRecordContent.m_strCurePlanXML = dtbValue.Rows[j]["CurePlanXML"].ToString();

                        if (dtbValue.Rows[j]["SEQUENCE_INT"] != DBNull.Value)
                        {
                            DataView dtvSign = new DataView(dtbSign);
                            dtvSign.RowFilter = "sign_int = " + dtbValue.Rows[j]["SEQUENCE_INT"].ToString();
                            m_mthGetSignVO(dtvSign, out objRecordContent.objSignerArr);
                        }
                        #endregion 从DataTable.Rows中获取结果

                        //生成 clsTransDataInfo
                        clsTransDataInfo objInfo = new clsTransDataInfo();
                        objInfo.m_intFlag = (int)enmDiseaseTrackType.FirstIllnessNote;

                        //设置结果到 objInfo.m_objRecordContent
                        objInfo.m_objRecordContent = objRecordContent;

                        p_arlTransData.Add(objInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion

        #region 获取首次病程记录(市一中医科)
        /// <summary>
        /// 获取首次病程记录(市一中医科)
        /// </summary>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_arlTransData"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetFirstIllnessNote_ZYRecord(string p_strDeptID, ref List<clsTransDataInfo> p_arlTransData)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strDeptID;

                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable       
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_FirstIllnessNote_ZY, ref dtbValue, objDPArr);

                //循环DataTable
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        #region 从DataTable.Rows中获取结果
                        //设置结果
                        clsFirstIllnessNote_ZYRecordContent objRecordContent = new clsFirstIllnessNote_ZYRecordContent();
                        objRecordContent.m_strInPatientID = dtbValue.Rows[j]["emrinpatientid"].ToString();
                        objRecordContent.m_dtmInPatientDate = Convert.ToDateTime(dtbValue.Rows[j]["emrinpatientdate"]);
                        objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OpenDate_Main"].ToString());
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CreateDate"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["ModifyDate"].ToString());

                        if (dtbValue.Rows[j]["FirstPrintDate"].ToString() == "")
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FirstPrintDate"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CreateUserID"].ToString();
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["ModifyUserID"].ToString();
                        if (dtbValue.Rows[j]["IfConfirm"].ToString() == "")
                            objRecordContent.m_bytIfConfirm = 0;
                        else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[j]["IfConfirm"].ToString());
                        if (dtbValue.Rows[j]["Status"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[j]["Status"].ToString());
                        objRecordContent.m_strConfirmReason = dtbValue.Rows[j]["ConfirmReason"].ToString();
                        objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[j]["ConfirmReasonXML"].ToString();

                        objRecordContent.m_strMostlyContent_Right = dtbValue.Rows[j]["MostlyContent_Right"].ToString();
                        objRecordContent.m_strMostlyContent = dtbValue.Rows[j]["MostlyContent"].ToString();
                        objRecordContent.m_strMostlyContentXML = dtbValue.Rows[j]["MostlyContentXML"].ToString();
                        objRecordContent.m_strOriginalDiagnose_Right = dtbValue.Rows[j]["OriginalDiagnose_Right"].ToString();
                        objRecordContent.m_strOriginalDiagnose = dtbValue.Rows[j]["OriginalDiagnose"].ToString();
                        objRecordContent.m_strOriginalDiagnoseXML = dtbValue.Rows[j]["OriginalDiagnoseXML"].ToString();
                        objRecordContent.m_strThereunderDiagnose_Right = dtbValue.Rows[j]["ThereunderDiagnose_Right"].ToString();
                        objRecordContent.m_strThereunderDiagnose = dtbValue.Rows[j]["ThereunderDiagnose"].ToString();
                        objRecordContent.m_strThereunderDiagnoseXML = dtbValue.Rows[j]["ThereunderDiagnoseXML"].ToString();
                        objRecordContent.m_strDiagnoseDiffe_Right = dtbValue.Rows[j]["DiagnoseDiffe_Right"].ToString();
                        objRecordContent.m_strDiagnoseDiffe = dtbValue.Rows[j]["DiagnoseDiffe"].ToString();
                        objRecordContent.m_strDiagnoseDiffeXML = dtbValue.Rows[j]["DiagnoseDiffeXML"].ToString();
                        objRecordContent.m_strCurePlan_Right = dtbValue.Rows[j]["CurePlan_Right"].ToString();
                        objRecordContent.m_strCurePlan = dtbValue.Rows[j]["CurePlan"].ToString();
                        objRecordContent.m_strCurePlanXML = dtbValue.Rows[j]["CurePlanXML"].ToString();
                        objRecordContent.m_strIdentifyDiagnose_Right = dtbValue.Rows[j]["IDENTIFYDIAGNOSE_RIGHT"].ToString();
                        objRecordContent.m_strIdentifyDiagnos = dtbValue.Rows[j]["IDENTIFYDIAGNOSE"].ToString();
                        objRecordContent.m_strIdentifyDiagnoseXML = dtbValue.Rows[j]["IDENTIFYDIAGNOSEXML"].ToString();
                        objRecordContent.m_strIdentifyReston_Right = dtbValue.Rows[j]["IDENTIFYRESTON_RIGHT"].ToString();
                        objRecordContent.m_strIdentifyReston = dtbValue.Rows[j]["IDENTIFYRESTON"].ToString();
                        objRecordContent.m_strIdentifyRestonXML = dtbValue.Rows[j]["IDENTIFYRESTONXML"].ToString();

                        #endregion 从DataTable.Rows中获取结果

                        //生成 clsTransDataInfo
                        clsTransDataInfo objInfo = new clsTransDataInfo();
                        objInfo.m_intFlag = (int)enmDiseaseTrackType.FirstIllnessNote_ZY;

                        //设置结果到 objInfo.m_objRecordContent
                        objInfo.m_objRecordContent = objRecordContent;

                        p_arlTransData.Add(objInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion

        #region 获取术前小结
        /// <summary>
        /// 获取术前小结
        /// </summary>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_arlTransData"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSummaryBeforeOP(string p_strDeptID, ref List<clsTransDataInfo> p_arlTransData)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strDeptID;

                //生成DataTable
                DataTable dtbContent = new DataTable();
                //执行查询，填充结果到DataTable       
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_SummaryBeforeOP, ref dtbContent, objDPArr);

                //循环DataTable
                if (lngRes > 0 && dtbContent.Rows.Count > 0)
                {
                    DataTable dtbValue = dtbContent.DefaultView.ToTable(true,
                        "emrinpatientid", "emrinpatientdate",  "CREATEDATE", "MODIFYDATE", "FIRSTPRINTDATE",
                        "CREATEUSERID", "MODIFYUSERID", "STATUS", "EMR_SEQ", "MARKSTATUS", "SEQUENCE_INT","OPENDATE",
                        "RECORDDATE", "REGISTERID_CHR", "DISEASESUMMARY", "DISEASESUMMARYXML", "DIAGNOSISBEFOREOP", "DIAGNOSISBEFOREOPXML",
                        "DIAGNOSISGIST", "DIAGNOSISGISTXML", "OPINDICATION", "OPINDICATIONXML", "OPMODE", "OPMODEXML", "ANAMODE",
                        "ANAMODEXML", "PROCEEDING", "PROCEEDINGXML", "PREPAREBEFOREOP", "PREPAREBEFOREOPXML", "DISEASESUMMARY_RIGHT",
                        "DIAGNOSISBEFOREOP_RIGHT", "DIAGNOSISGIST_RIGHT", "OPINDICATION_RIGHT", "OPMODE_RIGHT", "ANAMODE_RIGHT",
                        "PROCEEDING_RIGHT", "PREPAREBEFOREOP_RIGHT");
                    DataTable dtbSign = dtbContent.DefaultView.ToTable(true,
                    "empid_vchr", "lastname_vchr", "technicalrank_chr", "empno_chr", "psw_chr", "digitalsign_dta", "pycode_chr",
                    "status_int", "technicallevel_chr", "cagetory_vchr", "formname_vchr", "registerid_vchr", "sign_int");
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        #region 从DataTable.Rows中获取结果
                        //设置结果
                        clsEMR_SummaryBeforeOPValue objRecordContent = new clsEMR_SummaryBeforeOPValue();
                        objRecordContent.m_strInPatientID = dtbValue.Rows[j]["emrinpatientid"].ToString();
                        objRecordContent.m_dtmInPatientDate = Convert.ToDateTime(dtbValue.Rows[j]["emrinpatientdate"]);
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());
                        objRecordContent.m_dtmOpenDate = Convert.ToDateTime(dtbValue.Rows[j]["OPENDATE"]);

                        if (dtbValue.Rows[j]["FIRSTPRINTDATE"] == DBNull.Value)
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CREATEUSERID"].ToString();
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["MODIFYUSERID"].ToString();
                        if (dtbValue.Rows[j]["STATUS"] == DBNull.Value)
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());
                        if (dtbValue.Rows[j]["EMR_SEQ"] == DBNull.Value)
                            return -1;
                        objRecordContent.m_lngEMR_SEQ = Convert.ToInt64(dtbValue.Rows[j]["EMR_SEQ"]);

                        if (dtbValue.Rows[j]["MARKSTATUS"] == DBNull.Value)
                        {
                            objRecordContent.m_intMarkStatus = 0;
                        }
                        else
                        {
                            objRecordContent.m_intMarkStatus = Convert.ToInt32(dtbValue.Rows[j]["MARKSTATUS"]);
                        }
                        objRecordContent.m_dtmRECORDDATE = Convert.ToDateTime(dtbValue.Rows[j]["RECORDDATE"]);
                        objRecordContent.m_strREGISTERID_CHR = dtbValue.Rows[j]["REGISTERID_CHR"].ToString();
                        objRecordContent.m_strDISEASESUMMARY = dtbValue.Rows[j]["DISEASESUMMARY"].ToString();
                        objRecordContent.m_strDISEASESUMMARYXML = dtbValue.Rows[j]["DISEASESUMMARYXML"].ToString();
                        objRecordContent.m_strDIAGNOSISBEFOREOP = dtbValue.Rows[j]["DIAGNOSISBEFOREOP"].ToString();
                        objRecordContent.m_strDIAGNOSISBEFOREOPXML = dtbValue.Rows[j]["DIAGNOSISBEFOREOPXML"].ToString();
                        objRecordContent.m_strDIAGNOSISGIST = dtbValue.Rows[j]["DIAGNOSISGIST"].ToString();
                        objRecordContent.m_strDIAGNOSISGISTXML = dtbValue.Rows[j]["DIAGNOSISGISTXML"].ToString();
                        objRecordContent.m_strOPINDICATION = dtbValue.Rows[j]["OPINDICATION"].ToString();
                        objRecordContent.m_strOPINDICATIONXML = dtbValue.Rows[j]["OPINDICATIONXML"].ToString();
                        objRecordContent.m_strOPMODE = dtbValue.Rows[j]["OPMODE"].ToString();
                        objRecordContent.m_strOPMODEXML = dtbValue.Rows[j]["OPMODEXML"].ToString();
                        objRecordContent.m_strANAMODE = dtbValue.Rows[j]["ANAMODE"].ToString();
                        objRecordContent.m_strANAMODEXML = dtbValue.Rows[j]["ANAMODEXML"].ToString();
                        objRecordContent.m_strPROCEEDING = dtbValue.Rows[j]["PROCEEDING"].ToString();
                        objRecordContent.m_strPROCEEDINGXML = dtbValue.Rows[j]["PROCEEDINGXML"].ToString();
                        objRecordContent.m_strPREPAREBEFOREOP = dtbValue.Rows[j]["PREPAREBEFOREOP"].ToString();
                        objRecordContent.m_strPREPAREBEFOREOPXML = dtbValue.Rows[j]["PREPAREBEFOREOPXML"].ToString();

                        objRecordContent.m_strDISEASESUMMARY_RIGHT = dtbValue.Rows[j]["DISEASESUMMARY_RIGHT"].ToString();
                        objRecordContent.m_strDIAGNOSISBEFOREOP_RIGHT = dtbValue.Rows[j]["DIAGNOSISBEFOREOP_RIGHT"].ToString();
                        objRecordContent.m_strDIAGNOSISGIST_RIGHT = dtbValue.Rows[j]["DIAGNOSISGIST_RIGHT"].ToString();
                        objRecordContent.m_strOPINDICATION_RIGHT = dtbValue.Rows[j]["OPINDICATION_RIGHT"].ToString();
                        objRecordContent.m_strOPMODE_RIGHT = dtbValue.Rows[j]["OPMODE_RIGHT"].ToString();
                        objRecordContent.m_strANAMODE_RIGHT = dtbValue.Rows[j]["ANAMODE_RIGHT"].ToString();
                        objRecordContent.m_strPROCEEDING_RIGHT = dtbValue.Rows[j]["PROCEEDING_RIGHT"].ToString();
                        objRecordContent.m_strPREPAREBEFOREOP_RIGHT = dtbValue.Rows[j]["PREPAREBEFOREOP_RIGHT"].ToString();

                        if (dtbValue.Rows[j]["SEQUENCE_INT"] != DBNull.Value)
                        {
                            DataView dtvSign = new DataView(dtbSign);
                            dtvSign.RowFilter = "sign_int = " + dtbValue.Rows[j]["SEQUENCE_INT"].ToString();
                            m_mthGetSignVO(dtvSign, out objRecordContent.objSignerArr);
                        }

                        #endregion 从DataTable.Rows中获取结果

                        //生成 clsTransDataInfo
                        clsTransDataInfo objInfo = new clsTransDataInfo();
                        objInfo.m_intFlag = (int)enmDiseaseTrackType.EMR_SummaryBeforeOP;

                        //设置结果到 objInfo.m_objRecordContent
                        objInfo.m_objRecordContent = objRecordContent;

                        p_arlTransData.Add(objInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion

        /// <summary>
        /// 获取数字签名VO
        /// </summary>
        /// <param name="p_dtvSign"></param>
        /// <param name="p_objSignsArr"></param>
        private void m_mthGetSignVO(DataView p_dtvSign, out clsEmrSigns_VO[] p_objSignsArr)
        {
            p_objSignsArr = null;
            if (p_dtvSign == null || p_dtvSign.Count <= 0)
                return;
            int intSignCount = p_dtvSign.Count;
            //从DataTable.Rows中获取结果
            p_objSignsArr = new clsEmrSigns_VO[intSignCount];
            for (int i = 0; i < intSignCount; i++)
            {
                p_objSignsArr[i] = new clsEmrSigns_VO();
                p_objSignsArr[i].objEmployee = new clsEmrEmployeeBase_VO();
                p_objSignsArr[i].objEmployee.m_strEMPID_CHR = p_dtvSign[i]["empid_vchr"].ToString();
                p_objSignsArr[i].objEmployee.m_strLASTNAME_VCHR = p_dtvSign[i]["lastname_vchr"] == DBNull.Value ? "" : p_dtvSign[i]["lastname_vchr"].ToString().Trim();
                p_objSignsArr[i].objEmployee.m_strEMPNO_CHR = p_dtvSign[i]["empno_chr"] == DBNull.Value ? "" : p_dtvSign[i]["empno_chr"].ToString().Trim();
                p_objSignsArr[i].objEmployee.m_strTECHNICALRANK_CHR = p_dtvSign[i]["technicalrank_chr"] == DBNull.Value ? "" : p_dtvSign[i]["technicalrank_chr"].ToString().Trim();
                p_objSignsArr[i].objEmployee.m_strLEVEL_CHR = p_dtvSign[i]["technicallevel_chr"].ToString();
                p_objSignsArr[i].objEmployee.m_strEMPPWD_VCHR = p_dtvSign[i]["psw_chr"].ToString();
                p_objSignsArr[i].objEmployee.m_strEMPKEY_VCHR = p_dtvSign[i]["digitalsign_dta"].ToString();
                p_objSignsArr[i].objEmployee.m_strPYCODE_VCHR = p_dtvSign[i]["pycode_chr"].ToString();
                p_objSignsArr[i].objEmployee.m_intSTATUS_INT = p_dtvSign[i]["status_int"] == DBNull.Value ? 1 : int.Parse(p_dtvSign[i]["status_int"].ToString());
                p_objSignsArr[i].controlName = p_dtvSign[i]["CAGETORY_VCHR"].ToString();
                p_objSignsArr[i].m_strFORMID_VCHR = p_dtvSign[i]["FORMNAME_VCHR"].ToString();
                p_objSignsArr[i].m_strREGISTERID_CHR = p_dtvSign[i]["REGISTERID_VCHR"].ToString();
                //显示职称
                p_objSignsArr[i].objEmployee.m_strLASTNAME_VCHR = p_objSignsArr[i].objEmployee.m_strTECHNICALRANK_CHR + " " + p_objSignsArr[i].objEmployee.m_strLASTNAME_VCHR;
            }            
        }
        #endregion
    }
}
