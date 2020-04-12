using System;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data;
using weCare.Core.Entity;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.digitalwave.Utility.SQLConvert;
using System.Security.Principal;

namespace com.digitalwave.CaseRecallServ
{
	/// <summary>
	///病历回收
	/// </summary>
	[Transaction(TransactionOption.NotSupported)]
	[ObjectPooling(true)]
	public class clsCaseRecallServ:com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		public clsCaseRecallServ()
		{ }

        #region 屏蔽旧的
        /*
        #region 病历回收
        /// <summary>
		/// 保存回收记录
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_objArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngSaveRecallValue(IPrincipal p_objPrincipal, clsRecallValues[] p_objArr)
		{
			if(p_objArr == null || p_objArr.Length ==0)
				return 0;

			string strSql = @"	insert into t_emr_caserecall
								(registerid_chr,
								inpatientid_vchr,
								outdate_dat,
								deptid_chr,
								patientname_vchr,
								doctorid_chr,
								recalldate_dat,
								overduedays_int,
								status_int,
								operator_chr,
								operationdate_dat,
								lastmodifydate_dat,
								doctorname_vchr,
								DEPTNAME_VCHR,
								inpatienttimes_int,
								areaid_chr,
								AREANAME_VCHR)
								values
								(?, ?, ?, ?, ?, ?, ?, ?, 1, ?, getdate(), getdate(), ?, ?,?,?,?)";//14
			
			long lngRes = 0;
			try
			{
				clsHRPTableService objHRPServ =new clsHRPTableService();
				IDataParameter[] objDPArr = null;
				for(int i = 0; i < p_objArr.Length; i++)
				{
					long lngEff = 0;
					if(p_objArr[i] != null)
					{
						objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
						objHRPServ.CreateDatabaseParameter(14,out objDPArr);
						objDPArr[0].Value = p_objArr[i].m_strRegidterID;
						objDPArr[1].Value = p_objArr[i].m_strInPatientID;
						objDPArr[2].DbType = DbType.DateTime;
						objDPArr[2].Value = p_objArr[i].m_dtmOutDate;
						objDPArr[3].Value = p_objArr[i].m_strDeptID;
						objDPArr[4].Value = p_objArr[i].m_strPatientName;
						objDPArr[5].Value = p_objArr[i].m_strDoctorID;
						objDPArr[6].DbType = DbType.DateTime;
						objDPArr[6].Value = p_objArr[i].m_dtmRecallDate;
						objDPArr[7].Value = p_objArr[i].m_strOverDueDays;
						objDPArr[8].Value = p_objArr[i].m_strOpratorId;
						objDPArr[9].Value = p_objArr[i].m_strDoctorName;
						objDPArr[10].Value = p_objArr[i].m_strDeptName;
						objDPArr[11].Value = p_objArr[i].m_strInTimes;
						objDPArr[12].Value = p_objArr[i].m_strAreaID;
						objDPArr[13].Value = p_objArr[i].m_strAreaName;
				
//						lngRes = objHRPServ.lngExecuteParameterSQL(strSql,ref lngEff,objDPArr);
						objHRPServ.lngExecuteParameterSQL(strSql,ref lngEff,objDPArr);
						lngRes += lngEff;
					}
				}
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			return lngRes;
		}


		/// <summary>
		/// 取消回收返,回影响行数
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strRecallIdArr"></param>
		/// <returns>返回影响行数</returns>
		[AutoComplete]
		public long m_lngCancelRecallValue(IPrincipal p_objPrincipal, string[] p_strRecallIdArr)
		{
			if(p_strRecallIdArr == null || p_strRecallIdArr.Length ==0)
				return 0;
			string strSql = @"	update t_emr_caserecall
								set status_int = 0, lastmodifydate_dat = getdate()
								where recallid_int = ?  and status_int = 1";
			long lngEff = 0;
			long lngRes = 0;
			try
			{
				clsHRPTableService objHRPServ =new clsHRPTableService();
				IDataParameter[] objDPArr = null;
				for(int i = 0; i < p_strRecallIdArr.Length; i++)
				{
					objDPArr = null;
					objHRPServ.CreateDatabaseParameter(1,out objDPArr);
					objDPArr[0].Value = p_strRecallIdArr[i];
				
					objHRPServ.lngExecuteParameterSQL(strSql,ref lngEff,objDPArr);
					lngRes += lngEff;
				}
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				objLogger.LogError(objEx);
				return 0;
			}
			return lngRes;
		}
		/// <summary>
		/// 取消回收(只限于手工回收的记录)返回影响行数
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strRecallIdArr"></param>
		/// <returns>返回影响行数</returns>
		[AutoComplete]
		public long m_lngCancelRecallByHand(IPrincipal p_objPrincipal, string[] p_strRecallIdArr)
		{
			if(p_strRecallIdArr == null || p_strRecallIdArr.Length ==0)
				return 0;
			string strSql = @"	UPDATE T_EMR_CASERECALL_HAND
								SET status_int = 0, lastmodifydate_dat = GETDATE()
								WHERE recallid_int = ?  and status_int = 1";
			long lngEff = 0;
			long lngRes = 0;
			try
			{
				clsHRPTableService objHRPServ =new clsHRPTableService();
				IDataParameter[] objDPArr = null;
				int intRecallIdLength = p_strRecallIdArr.Length;
				for(int i = 0; i < intRecallIdLength; i++)
				{
					objDPArr = null;
					objHRPServ.CreateDatabaseParameter(1,out objDPArr);
					objDPArr[0].Value = p_strRecallIdArr[i];
				
					objHRPServ.lngExecuteParameterSQL(strSql,ref lngEff,objDPArr);
					lngRes += lngEff;
				}
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				objLogger.LogError(objEx);
				return 0;
			}
			return lngRes;
		}

		/// <summary>
		/// 取消回收(根据住院登记号和回收时间,只限于手工回收的记录)返回影响行数
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strRegisterId"></param>
		/// <param name="p_dtmRecallDate"></param>
		/// <returns>返回影响行数</returns>
		[AutoComplete]
		public long m_lngCancelRecallByHand(IPrincipal p_objPrincipal, string[] p_strRecallIdArr,DateTime[] p_dtmRecallDateArr)
		{
			if(p_strRecallIdArr == null || p_strRecallIdArr.Length ==0 || p_strRecallIdArr.Length != p_dtmRecallDateArr.Length)
				return -1;

			string strSql = @"	update T_EMR_CASERECALL_HAND
								set status_int = 0, lastmodifydate_dat = getdate()
								where recallid_int = ? and recalldate_dat = ?  and status_int = 1";
			long lngEff = 0;
			long lngRes = 0;
			try
			{
				clsHRPTableService objHRPServ =new clsHRPTableService();
				IDataParameter[] objDPArr = null;
				int intRecallIdLength = p_strRecallIdArr.Length;
				for(int i = 0; i < intRecallIdLength; i++)
				{
					objDPArr = null;
					objHRPServ.CreateDatabaseParameter(2,out objDPArr);
					objDPArr[0].Value = p_strRecallIdArr[i];
					objDPArr[1].DbType = DbType.DateTime;
					objDPArr[1].Value = p_dtmRecallDateArr[i];
				
					objHRPServ.lngExecuteParameterSQL(strSql,ref lngEff,objDPArr);
					lngRes += lngEff;
				}
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				objLogger.LogError(objEx);
				return 0;
			}
			return lngRes;
		}
		/// <summary>
		/// 取消回收(根据住院登记号和回收时间)返回影响行数
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strRegisterId"></param>
		/// <param name="p_dtmRecallDate"></param>
		/// <returns>返回影响行数</returns>
		[AutoComplete]
		public long m_lngCancelRecallValue(IPrincipal p_objPrincipal, string[] p_strRegisterIdArr,DateTime[] p_dtmRecallDateArr)
		{
			if(p_strRegisterIdArr == null || p_strRegisterIdArr.Length ==0 || p_strRegisterIdArr.Length != p_dtmRecallDateArr.Length)
				return -1;
			string strSql = @"	update t_emr_caserecall
								set status_int = 0, lastmodifydate_dat = getdate()
								where registerid_chr = ? and recalldate_dat = ? and status_int = 1";
			long lngEff = 0;
			long lngRes = 0;
			try
			{
				clsHRPTableService objHRPServ =new clsHRPTableService();
				IDataParameter[] objDPArr = null;
				for(int i=0;i<p_strRegisterIdArr.Length;i++)
				{
					objDPArr = null;
					objHRPServ.CreateDatabaseParameter(2,out objDPArr);
					objDPArr[0].Value = p_strRegisterIdArr[i];
					objDPArr[1].DbType = DbType.DateTime;
					objDPArr[1].Value = p_dtmRecallDateArr[i];
				
					objHRPServ.lngExecuteParameterSQL(strSql,ref lngEff,objDPArr);
					lngRes += lngEff;
				}
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
				return 0;
			}
			return lngRes;
		}


		/// <summary>
		/// 重新回收
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_objArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngResetRecallValue( clsRecallValues[] p_objArr)
		{
			if(p_objArr == null || p_objArr.Length ==0)
				return 0;
			string strSql = @"	update t_emr_caserecall
								set inpatientid_vchr   = ?,
								outdate_dat        = ?,
								deptid_chr         = ?,
								DEPTNAME_VCHR       = ?,
								patientname_vchr   = ?,
								doctorid_chr       = ?,
								doctorname_vchr    = ?,
								recalldate_dat     = ?,
								overduedays_int    = ?,
								status_int         = ?,
								operator_chr       = ?,
								lastmodifydate_dat = getdate(),
								inpatienttimes_int = ?,
								areaid_chr = ?,
								AREANAME_VCHR = ?
								where recallid_int = ?";//15
			long lngEff = 0;
			long lngRes = 0;
			try
			{
				clsHRPTableService objHRPServ =new clsHRPTableService();
				IDataParameter[] objDPArr = null;
				for(int i = 0; i < p_objArr.Length; i++)
				{
					if(p_objArr[i] != null)
					{
						objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
						objHRPServ.CreateDatabaseParameter(15,out objDPArr);
						objDPArr[0].Value = p_objArr[i].m_strInPatientID;
						objDPArr[1].DbType = DbType.DateTime;
						objDPArr[1].Value = p_objArr[i].m_dtmOutDate;
						objDPArr[2].Value = p_objArr[i].m_strDeptID;
						objDPArr[3].Value = p_objArr[i].m_strDeptName;
						objDPArr[4].Value = p_objArr[i].m_strPatientName;
						objDPArr[5].Value = p_objArr[i].m_strDoctorID;
						objDPArr[6].Value = p_objArr[i].m_strDoctorName;
						objDPArr[7].DbType = DbType.DateTime;
						objDPArr[7].Value = p_objArr[i].m_dtmRecallDate;
						objDPArr[8].Value = p_objArr[i].m_strOverDueDays;
						objDPArr[9].Value = p_objArr[i].m_strStatus;
						objDPArr[10].Value = p_objArr[i].m_strOpratorId;
						objDPArr[11].Value = p_objArr[i].m_strInTimes;
						objDPArr[12].Value = p_objArr[i].m_strAreaID;
						objDPArr[13].Value = p_objArr[i].m_strAreaName;
						objDPArr[14].Value = p_objArr[i].m_strRecallID;
				
						lngRes = objHRPServ.lngExecuteParameterSQL(strSql,ref lngEff,objDPArr);
					}
				}
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			return lngRes;
		}


		/// <summary>
		/// 根据科室ID和住院号查询病人
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strDeptId"></param>
		/// <param name="p_strInpatientId"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetPatientByInId(string p_strDeptId,string p_strInpatientId,out clsRecallValues[] p_objRecallValues)
		{
			p_objRecallValues = null;
			if(p_strDeptId == null || p_strDeptId =="" || p_strInpatientId == null|| p_strInpatientId == "")
				return -1;
			string strSql = @"select pa.lastname_vchr,
       re.inpatientcount_int,
       re.inpatientid_chr,
       re.deptid_chr,
		re.areaid_chr,
       de.deptname_vchr deptname,
       de2.deptname_vchr areaname,
       be.CODE_CHR,
       le.modify_dat outdate,
       c.recalldate_dat,
       le.type_int,
       c.overduedays_int,
       re.registerid_chr,
       c.recallid_int,
       c.status_int
  from t_bse_patient pa
 inner join t_opr_bih_register re on pa.patientid_chr = re.patientid_chr
 inner join t_opr_bih_leave le on re.registerid_chr = le.registerid_chr
                              and le.status_int = 1
 inner join t_bse_bed be on be.areaid_chr=le.OUTAREAID_CHR and be.BEDID_CHR=le.OUTBEDID_CHR and be.STATUS_INT >= 1
  left join t_emr_caserecall c on re.registerid_chr = c.registerid_chr  and c.status_int = 1
 inner join t_bse_deptdesc de on de.deptid_chr = re.deptid_chr
 inner join t_bse_deptdesc de2 on de2.deptid_chr = re.areaid_chr
 where re.deptid_chr =?
   and re.inpatientid_chr = ? 
order by le.modify_dat desc";//2
			long lngRes = 0;
			try
			{
				clsHRPTableService objHRPServ =new clsHRPTableService();
				IDataParameter[] objDPArr = null;
				objHRPServ.CreateDatabaseParameter(2,out objDPArr);
//				objDPArr[0].Value = p_strInpatientId;
				objDPArr[0].Value = p_strDeptId;
				objDPArr[1].Value = p_strInpatientId;
//				objDPArr[3].Value = p_strDeptId;
//				objDPArr[4].Value = p_strInpatientId;

				DataTable dtbResult = new DataTable();
				lngRes = objHRPServ.lngGetDataTableWithParameters(strSql,ref dtbResult,objDPArr);
				int intRowCount = dtbResult.Rows.Count;
				if(lngRes > 0 && intRowCount > 0)
				{
					p_objRecallValues = m_objGetData(dtbResult);
				}
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			return lngRes;
		}

		/// <summary>
		/// 根据住院号查询病人(包括了手工回收表中记录的查询)
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strDeptId"></param>
		/// <param name="p_strInpatientId"></param>
		/// <param name="p_objRecallValues"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetPatientByInPatientId(IPrincipal p_objPrincipal,string p_strInpatientId,out clsRecallValues[] p_objRecallValues)
		{
			p_objRecallValues = null;
			if(p_strInpatientId == null|| p_strInpatientId == "")
				return -1;
			//-------------------------------------------------------------
			// SQL语句说明: 此语句将查询出已出院的病历记录，包含了已回收和未回收的纪录；
			//				其中，已回收记录包括正常回收和手工回收的。WQF 2007-3-2
			//-------------------------------------------------------------
			string strSql = @"	select pa.lastname_vchr,
								re.inpatientcount_int,
								re.inpatientid_chr,
								re.deptid_chr,
								re.areaid_chr,
								de.deptname_vchr deptname,
								de2.deptname_vchr areaname,
								be.CODE_CHR,
								le.modify_dat outdate,
								c.recalldate_dat,
								le.type_int,
								c.overduedays_int,
								re.registerid_chr,
								c.recallid_int,
								c.status_int,
								0 Normalstatus
								from t_bse_patient pa
								inner join t_opr_bih_register re on pa.patientid_chr = re.patientid_chr
								inner join t_opr_bih_leave le on re.registerid_chr = le.registerid_chr and le.status_int = 1
								inner join t_bse_bed be on be.areaid_chr=le.OUTAREAID_CHR and be.BEDID_CHR=le.OUTBEDID_CHR and be.STATUS_INT >= 1
								left join t_emr_caserecall c on re.registerid_chr = c.registerid_chr  and c.status_int = 1
								inner join t_bse_deptdesc de on de.deptid_chr = re.deptid_chr
								inner join t_bse_deptdesc de2 on de2.deptid_chr = re.areaid_chr
								where re.inpatientid_chr = ? 
								union 
								select c.patientname_vchr lastname_vchr,
								c.inpatienttimes_int inpatientcount_int,
								c.inpatientid_vchr inpatientid_chr,
								c.deptid_chr,
								c.areaid_chr,
								c.deptname_vchr deptname,
								c.areaname_vchr areaname,
								c.CODE_CHR,
								c.outdate_dat outdate,
								c.recalldate_dat,
								3 type_int,
								c.overduedays_int,
								c.registerid_chr,
								c.recallid_int,
								c.status_int,
								1 Normalstatus
								from T_EMR_CASERECALL_HAND c
								where c.status_int = 1 and c.inpatientid_vchr = ?
								order by le.modify_dat desc";//c.status_int = 1 and 
			long lngRes = 0;
			try
			{
				clsHRPTableService objHRPServ =new clsHRPTableService();
				IDataParameter[] objDPArr = null;
				objHRPServ.CreateDatabaseParameter(2,out objDPArr);
				objDPArr[0].Value = p_strInpatientId;
				objDPArr[1].Value = p_strInpatientId;
//				objDPArr[2].Value = p_strInpatientId;

				DataTable dtbResult = new DataTable();
				lngRes = objHRPServ.lngGetDataTableWithParameters(strSql,ref dtbResult,objDPArr);
				int intRowCount = dtbResult.Rows.Count;
				if(lngRes > 0 && intRowCount > 0)
				{
					p_objRecallValues = m_objGetData(dtbResult);
				}
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			return lngRes;
		}


		/// <summary>
		/// 根据科室ID和病人姓名查询病人
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strDeptId"></param>
		/// <param name="p_strInpatientName"></param>
		/// <param name="p_objRecallValues"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetPatientByDeptAndPatientName(string p_strDeptId,string p_strInpatientName,out clsRecallValues[] p_objRecallValues)
		{
			p_objRecallValues = null;
			if(p_strDeptId == null || p_strDeptId =="" || p_strInpatientName == null|| p_strInpatientName == "")
				return -1;
			string strSql = @"select pa.lastname_vchr,
       re.inpatientcount_int,
       re.inpatientid_chr,
       re.deptid_chr,
		re.areaid_chr,
       de.deptname_vchr deptname,
       de2.deptname_vchr areaname,
      be.CODE_CHR,
       le.modify_dat outdate,
       c.recalldate_dat,
       le.type_int,
       c.overduedays_int,
       re.registerid_chr,
       c.recallid_int,
       c.status_int
  from t_bse_patient pa
 inner join t_opr_bih_register re on pa.patientid_chr = re.patientid_chr
 inner join t_opr_bih_leave le on re.registerid_chr = le.registerid_chr
                              and le.status_int = 1
  inner join t_bse_bed be on be.areaid_chr=le.OUTAREAID_CHR and be.BEDID_CHR=le.OUTBEDID_CHR and be.STATUS_INT >= 1
  left join t_emr_caserecall c on re.registerid_chr = c.registerid_chr  and c.status_int = 1
 inner join t_bse_deptdesc de on de.deptid_chr = re.deptid_chr
 inner join t_bse_deptdesc de2 on de2.deptid_chr = re.areaid_chr
 where re.deptid_chr =?
   and pa.lastname_vchr = ? order by le.modify_dat desc";//2
			long lngRes = 0;
			try
			{
				clsHRPTableService objHRPServ =new clsHRPTableService();
				IDataParameter[] objDPArr = null;
				objHRPServ.CreateDatabaseParameter(2,out objDPArr);
				objDPArr[0].Value = p_strDeptId;
				objDPArr[1].Value = p_strInpatientName;
//				objDPArr[2].Value = p_strDeptId;
//				objDPArr[3].Value = p_strInpatientName;

				DataTable dtbResult = new DataTable();
				lngRes = objHRPServ.lngGetDataTableWithParameters(strSql,ref dtbResult,objDPArr);
				int intRowCount = dtbResult.Rows.Count;
				if(lngRes > 0 && intRowCount > 0)
				{
					p_objRecallValues = m_objGetData(dtbResult);
				}
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			return lngRes;
		}


		/// <summary>
		/// 根据病人姓名查询病人(包含了手工回收的查询)
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInpatientName"></param>
		/// <param name="p_objRecallValues"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetPatientByPatientName(string p_strInpatientName,out clsRecallValues[] p_objRecallValues)
		{
			p_objRecallValues = null;
			if(p_strInpatientName == null|| p_strInpatientName == "")
				return -1;
			//-------------------------------------------------------------
			// SQL语句说明: 此语句将查询出已出院的病历记录，包含了已回收和未回收的纪录；
			//				其中，已回收记录包括正常回收和手工回收的。WQF 2007-3-2
			//-------------------------------------------------------------
			string strSql = @"	select pa.lastname_vchr,
								re.inpatientcount_int,
								re.inpatientid_chr,
								re.deptid_chr,
								re.areaid_chr,
								de.deptname_vchr deptname,
								de2.deptname_vchr areaname,
								be.CODE_CHR,
								le.modify_dat outdate,
								c.recalldate_dat,
								le.type_int,
								c.overduedays_int,
								re.registerid_chr,
								c.recallid_int,
								c.status_int,
								0 Normalstatus
								from t_bse_patient pa
								inner join t_opr_bih_register re on pa.patientid_chr = re.patientid_chr
								inner join t_opr_bih_leave le on re.registerid_chr = le.registerid_chr and le.status_int = 1
								inner join t_bse_bed be on be.areaid_chr=le.OUTAREAID_CHR and be.BEDID_CHR=le.OUTBEDID_CHR and be.STATUS_INT >= 1
								left join t_emr_caserecall c on re.registerid_chr = c.registerid_chr  and c.status_int = 1
								inner join t_bse_deptdesc de on de.deptid_chr = re.deptid_chr
								inner join t_bse_deptdesc de2 on de2.deptid_chr = re.areaid_chr
								where pa.lastname_vchr = ? 
								union 
								select c.patientname_vchr lastname_vchr,
								c.inpatienttimes_int inpatientcount_int,
								c.inpatientid_vchr inpatientid_chr,
								c.deptid_chr,
								c.areaid_chr,
								c.deptname_vchr deptname,
								c.areaname_vchr areaname,
								c.CODE_CHR,
								c.outdate_dat outdate,
								c.recalldate_dat,
								3 type_int,
								c.overduedays_int,
								c.registerid_chr,
								c.recallid_int,
								c.status_int,
								1 Normalstatus
								from T_EMR_CASERECALL_HAND c
								where c.status_int = 1 and c.status_int = 1 and c.patientname_vchr = ? 
								order by le.modify_dat desc";//c.status_int = 1 and 
			long lngRes = 0;
			try
			{
				clsHRPTableService objHRPServ =new clsHRPTableService();
				IDataParameter[] objDPArr = null;
				objHRPServ.CreateDatabaseParameter(2,out objDPArr);
				objDPArr[0].Value = p_strInpatientName;
				objDPArr[1].Value = p_strInpatientName;

				DataTable dtbResult = new DataTable();
				lngRes = objHRPServ.lngGetDataTableWithParameters(strSql,ref dtbResult,objDPArr);
				int intRowCount = dtbResult.Rows.Count;
				if(lngRes > 0 && intRowCount > 0)
				{
					p_objRecallValues = m_objGetData(dtbResult);
				}
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			return lngRes;
		}


		/// <summary>
		/// 根据科室ID或者病区ID和出院日期段查询病人(包含了手工回收的查询)
		/// WQF 2007-3-1
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strId"></param>
		/// <param name="p_dtmStart"></param>
		/// <param name="p_dtmEnd"></param>
		/// <param name="m_strAttributeId">0000002=科室；00000003＝病区</param>
		/// <param name="p_objRecallValues"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetPatientByDeptAndOutDate(string p_strId,DateTime p_dtmStart,DateTime p_dtmEnd,string m_strAttributeId,out clsRecallValues[] p_objRecallValues)
		{
			p_objRecallValues = null;
			if(p_strId == null || p_strId =="" )
				return -1;

			//-------------------------------------------------------------
			// SQL语句说明: 此语句将查询出已出院的病历记录，包含了已回收和未回收的纪录；
			//				其中，已回收记录包括正常回收和手工回收的。WQF 2007-3-2
			//-------------------------------------------------------------
			string strSql = @"	select distinct pa.lastname_vchr,
								re.inpatientcount_int,
								re.inpatientid_chr,
								re.deptid_chr,
								re.areaid_chr,
								de.deptname_vchr deptname,
								de2.deptname_vchr areaname,
								be.CODE_CHR,
								le.modify_dat outdate,
								c.recalldate_dat,
								le.type_int,
								c.overduedays_int,
								re.registerid_chr,
								c.recallid_int,
								c.status_int,
								0 Normalstatus
								from t_bse_patient pa
								inner join t_opr_bih_register re on pa.patientid_chr = re.patientid_chr
								inner join t_opr_bih_leave le on re.registerid_chr = le.registerid_chr and le.status_int = 1
								inner join t_bse_bed be on be.areaid_chr=le.OUTAREAID_CHR and be.BEDID_CHR=le.OUTBEDID_CHR and be.STATUS_INT >= 1
								left join t_emr_caserecall c on re.registerid_chr = c.registerid_chr and c.status_int = 1
								inner join t_bse_deptdesc de on de.deptid_chr = re.deptid_chr
								inner join t_bse_deptdesc de2 on de2.deptid_chr = re.areaid_chr
								where re.[ID] = ?
								and le.modify_dat between ? and ?
								union 
								select c.patientname_vchr lastname_vchr,
								c.inpatienttimes_int inpatientcount_int,
								c.inpatientid_vchr inpatientid_chr,
								c.deptid_chr,
								c.areaid_chr,
								c.deptname_vchr deptname,
								c.areaname_vchr areaname,
								c.CODE_CHR,
								c.outdate_dat outdate,
								c.recalldate_dat,
								3 type_int,
								c.overduedays_int,
								c.registerid_chr,
								c.recallid_int,
								c.status_int,
								1 Normalstatus
								from T_EMR_CASERECALL_HAND c
								where c.[ID] = ? and c.status_int = 1
								and c.outdate_dat between  ? and ?
								order by outdate desc";// and c.status_int = 1

			if(m_strAttributeId == "0000003")
				strSql = strSql.Replace("[ID]","areaid_chr");
			else
				strSql = strSql.Replace("[ID]","deptid_chr");
			long lngRes = 0;
			try
			{
				clsHRPTableService objHRPServ =new clsHRPTableService();
				IDataParameter[] objDPArr = null;
				objHRPServ.CreateDatabaseParameter(6,out objDPArr);
				objDPArr[0].Value = p_strId;
				objDPArr[1].DbType = DbType.DateTime;
				objDPArr[1].Value = p_dtmStart;
				objDPArr[2].DbType = DbType.DateTime;
				objDPArr[2].Value = p_dtmEnd;

				objDPArr[3].Value = p_strId;
				objDPArr[4].DbType = DbType.DateTime;
				objDPArr[4].Value = p_dtmStart;
				objDPArr[5].DbType = DbType.DateTime;
				objDPArr[5].Value = p_dtmEnd;

				DataTable dtbResult = new DataTable();
				lngRes = objHRPServ.lngGetDataTableWithParameters(strSql,ref dtbResult,objDPArr);
				int intRowCount = dtbResult.Rows.Count;
				if(lngRes > 0 && intRowCount > 0)
				{
					p_objRecallValues = m_objGetData(dtbResult);
				}
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			return lngRes;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_dtbResult"></param>
		/// <returns></returns>
        [AutoComplete]
		private clsRecallValues[] m_objGetData(DataTable p_dtbResult)
		{
			clsRecallValues[] objRecallValues = new clsRecallValues[p_dtbResult.Rows.Count];
			DataRow objRow = null;
			try
			{
				DateTime dtmMinDate = DateTime.MaxValue;
				DateTime dtmMaxDate = DateTime.MinValue;
				for(int i=0;i<p_dtbResult.Rows.Count;i++)
				{
					objRow = p_dtbResult.Rows[i];
					clsRecallValues objRecallValue = new clsRecallValues();
					objRecallValue.m_strRegidterID = objRow["registerid_chr"].ToString().Trim();
					objRecallValue.m_strPatientName = objRow["lastname_vchr"].ToString();
					objRecallValue.m_strInTimes = objRow["inpatientcount_int"].ToString();
					objRecallValue.m_strInPatientID = objRow["inpatientid_chr"].ToString().Trim();
					objRecallValue.m_strDeptID = objRow["deptid_chr"].ToString();
					objRecallValue.m_strDeptName = objRow["deptname"].ToString();
					objRecallValue.m_strAreaID = objRow["areaid_chr"].ToString();
					objRecallValue.m_strAreaName = objRow["areaname"].ToString();
					objRecallValue.m_strBedName = objRow["CODE_CHR"].ToString();
					objRecallValue.m_strType = objRow["type_int"].ToString();//此处赋值必须在objRecallValue.m_dtmOutDate赋值之前
					try
					{
						objRecallValue.m_dtmOutDate = DateTime.Parse(objRow["outdate"].ToString());
						if(dtmMinDate > objRecallValue.m_dtmOutDate) dtmMinDate = objRecallValue.m_dtmOutDate;
						if(dtmMaxDate < objRecallValue.m_dtmOutDate) dtmMaxDate = objRecallValue.m_dtmOutDate;
					}
					catch{objRecallValue.m_dtmOutDate = new DateTime(1900,1,1);}
					try
					{
						objRecallValue.m_dtmRecallDate = DateTime.Parse(objRow["recalldate_dat"].ToString());
					}
					catch{objRecallValue.m_dtmRecallDate = new DateTime(1900,1,1);}
					objRecallValue.m_strOverDueDays = objRow["overduedays_int"].ToString();
					objRecallValue.m_strRecallID = objRow["recallid_int"].ToString();
					objRecallValue.m_strStatus = objRow["status_int"].ToString();
					if(p_dtbResult.Columns.Contains("Normalstatus"))
					{
						objRecallValue.m_strNormalstatus = objRow["Normalstatus"].ToString();
					}

					objRecallValues[i] = objRecallValue;
				}
				dtmMaxDate = dtmMaxDate.AddMonths(2);
				DateTime[] dtmArr = m_objGetHoliday(null,dtmMinDate,dtmMaxDate);
				if(dtmArr != null && dtmArr.Length > 0 && objRecallValues.Length > 0)
				{
					for(int j=0;j<objRecallValues.Length;j++)
					{
						objRecallValues[j].m_dtExcludeHolidayArr = dtmArr;
					}
				}
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
				return null;
			}
			return objRecallValues;
		}
		#endregion  病历回收

		#region 已回收病历查询
		/// <summary>
		/// 按科室或者病区查询某段回收时间的已回收病历
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strId"></param>
		/// <param name="m_strAttributeId"></param>
		/// <param name="p_dtmStart"></param>
		/// <param name="p_dtmEnd"></param>
		/// <param name="p_blnIsOut">true=按出院日期查；false＝按回收日期查</param>
		/// <param name="p_objRecallValues"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetIsRecallValues(IPrincipal p_objPrincipal,string p_strId,string p_strAttributeId,DateTime p_dtmStart,DateTime p_dtmEnd,bool p_blnIsOut,out clsRecallValues[] p_objRecallValues)
		{
			p_objRecallValues = null;
			if(p_strId == null || p_strId =="")
				return -1;
			string strSql = @"	select c.recallid_int,
								c.registerid_chr,
								c.inpatientid_vchr,
								c.outdate_dat,
								c.deptid_chr,
								c.areaid_chr,
								c.areaname_vchr,
								c.patientname_vchr,
								c.doctorid_chr,
								c.recalldate_dat,
								c.overduedays_int,
								c.status_int,
								c.operator_chr,
								c.operationdate_dat,
								c.lastmodifydate_dat,
								c.doctorname_vchr,
								c.deptname_vchr,
								c.inpatienttimes_int,
								be.CODE_CHR,
								0 Normalstatus
								from t_emr_caserecall c
								inner join t_opr_bih_leave le on c.registerid_chr = le.registerid_chr
								and le.status_int = 1
								inner join t_bse_bed be on be.areaid_chr=le.OUTAREAID_CHR and be.BEDID_CHR=le.OUTBEDID_CHR and be.STATUS_INT >= 1
								where c.status_int = 1 and c.[DATE] between ? and ?
								and c.[ID] = ?
								union
								select c.recallid_int,
								c.registerid_chr,
								c.inpatientid_vchr,
								c.outdate_dat,
								c.deptid_chr,
								c.areaid_chr,
								c.areaname_vchr,
								c.patientname_vchr,
								c.doctorid_chr,
								c.recalldate_dat,
								c.overduedays_int,
								c.status_int,
								c.operator_chr,
								c.operationdate_dat,
								c.lastmodifydate_dat,
								c.doctorname_vchr,
								c.deptname_vchr,
								c.inpatienttimes_int,
								c.CODE_CHR,
								1 Normalstatus
								from T_EMR_CASERECALL_HAND c
								where c.status_int = 1 and c.[DATE] between ? and ?
								and c.[ID] = ?
								order by outdate_dat";//3
			if(p_strAttributeId == "0000003")
				strSql = strSql.Replace("[ID]","areaid_chr");
			else
				strSql = strSql.Replace("[ID]","deptid_chr");
			if(p_blnIsOut)
				strSql = strSql.Replace("[DATE]","outdate_dat");
			else
				strSql = strSql.Replace("[DATE]","recalldate_dat");
			long lngRes = 0;
			try
			{
				clsHRPTableService objHRPServ =new clsHRPTableService();
				IDataParameter[] objDPArr = null;
				objHRPServ.CreateDatabaseParameter(6,out objDPArr);
				objDPArr[0].DbType = DbType.DateTime;
				objDPArr[0].Value = p_dtmStart;
				objDPArr[1].DbType = DbType.DateTime;
				objDPArr[1].Value = p_dtmEnd;
				objDPArr[2].Value = p_strId;
				objDPArr[3].DbType = DbType.DateTime;
				objDPArr[3].Value = p_dtmStart;
				objDPArr[4].DbType = DbType.DateTime;
				objDPArr[4].Value = p_dtmEnd;
				objDPArr[5].Value = p_strId;

				DataTable dtbResult = new DataTable();
				lngRes = objHRPServ.lngGetDataTableWithParameters(strSql,ref dtbResult,objDPArr);
				int intRowCount = dtbResult.Rows.Count;
				if(lngRes > 0 && intRowCount > 0)
				{
					p_objRecallValues = new clsRecallValues[intRowCount];
					DataRow objRow = null;
					for(int i=0;i<intRowCount;i++)
					{
						objRow = dtbResult.Rows[i];
						clsRecallValues objRecallValue = new clsRecallValues();
						objRecallValue.m_strRecallID = objRow["recallid_int"].ToString();
						objRecallValue.m_strRegidterID = objRow["registerid_chr"].ToString().Trim();
						objRecallValue.m_strInPatientID = objRow["inpatientid_vchr"].ToString().Trim();
						try
						{
							objRecallValue.m_dtmOutDate = DateTime.Parse(objRow["outdate_dat"].ToString());
						}
						catch{objRecallValue.m_dtmOutDate = new DateTime(1900,1,1);}
						objRecallValue.m_strDeptID = objRow["deptid_chr"].ToString();
						objRecallValue.m_strPatientName = objRow["patientname_vchr"].ToString();
						objRecallValue.m_strAreaID = objRow["areaid_chr"].ToString();
						objRecallValue.m_strAreaName = objRow["areaname_vchr"].ToString();
						objRecallValue.m_strDoctorID = objRow["doctorid_chr"].ToString().Trim();
						try
						{
							objRecallValue.m_dtmRecallDate = DateTime.Parse(objRow["recalldate_dat"].ToString());
						}
						catch{objRecallValue.m_dtmRecallDate = new DateTime(1900,1,1);}
						objRecallValue.m_strOverDueDays = objRow["overduedays_int"].ToString();
						objRecallValue.m_strStatus = "1";
						objRecallValue.m_strOpratorId = objRow["operator_chr"].ToString();
						try
						{
							objRecallValue.m_dtmOperationDate = DateTime.Parse(objRow["operationdate_dat"].ToString());
						}
						catch{objRecallValue.m_dtmOperationDate = new DateTime(1900,1,1);}
						try
						{
							objRecallValue.m_dtmLastModifyDate = DateTime.Parse(objRow["lastmodifydate_dat"].ToString());
						}
						catch{objRecallValue.m_dtmLastModifyDate = new DateTime(1900,1,1);}
						objRecallValue.m_strDoctorName = objRow["doctorname_vchr"].ToString();
						objRecallValue.m_strBedName = objRow["CODE_CHR"].ToString();
						objRecallValue.m_strDeptName = objRow["DEPTNAME_VCHR"].ToString();
						objRecallValue.m_strInTimes = objRow["inpatienttimes_int"].ToString();
						objRecallValue.m_strNormalstatus = objRow["Normalstatus"].ToString();
						p_objRecallValues[i] = objRecallValue;
					}
				}
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			return lngRes;
		}
		
		/// <summary>
		/// 按科室或者病区、超期与否等条件查询某段回收时间的已回收病历
		/// WQF	2007-02-28
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strId"></param>
		/// <param name="p_strAttributeId"></param>
		/// <param name="p_dtmStart"></param>
		/// <param name="p_dtmEnd"></param>
		/// <param name="p_blnIsOut"></param>
		/// <param name="blnIsPastDue"></param>
		/// <param name="p_objRecallValues"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetIsRecallValues(IPrincipal p_objPrincipal,string p_strId,string p_strAttributeId,DateTime p_dtmStart,DateTime p_dtmEnd,bool p_blnIsOut,bool blnIsPastDue,out clsRecallValues[] p_objRecallValues)
		{
			p_objRecallValues = null;
			if(p_strId == null || p_strId =="")
				return -1;
			string strSql = @"	select c.recallid_int,
								c.registerid_chr,
								c.inpatientid_vchr,
								c.outdate_dat,
								c.deptid_chr,
								c.areaid_chr,
								c.areaname_vchr,
								c.patientname_vchr,
								c.doctorid_chr,
								c.recalldate_dat,
								c.overduedays_int,
								c.status_int,
								c.operator_chr,
								c.operationdate_dat,
								c.lastmodifydate_dat,
								c.doctorname_vchr,
								c.deptname_vchr,
								c.inpatienttimes_int,
								be.CODE_CHR,
								0 Normalstatus
								from t_emr_caserecall c
								inner join t_opr_bih_leave le on c.registerid_chr = le.registerid_chr
								and le.status_int = 1
								inner join t_bse_bed be on be.areaid_chr=le.OUTAREAID_CHR and be.BEDID_CHR=le.OUTBEDID_CHR and be.STATUS_INT >= 1 ";

			string strSqlPart2 = @"	union
									select c.recallid_int,
									c.registerid_chr,
									c.inpatientid_vchr,
									c.outdate_dat,
									c.deptid_chr,
									c.areaid_chr,
									c.areaname_vchr,
									c.patientname_vchr,
									c.doctorid_chr,
									c.recalldate_dat,
									c.overduedays_int,
									c.status_int,
									c.operator_chr,
									c.operationdate_dat,
									c.lastmodifydate_dat,
									c.doctorname_vchr,
									c.deptname_vchr,
									c.inpatienttimes_int,
									c.CODE_CHR,
									1 Normalstatus
									from T_EMR_CASERECALL_HAND c ";
			#region
//			string strSql = @"	select c.recallid_int,
//								c.registerid_chr,
//								c.inpatientid_vchr,
//								c.outdate_dat,
//								c.deptid_chr,
//								c.areaid_chr,
//								c.areaname_vchr,
//								c.patientname_vchr,
//								c.doctorid_chr,
//								c.recalldate_dat,
//								c.overduedays_int,
//								c.status_int,
//								c.operator_chr,
//								c.operationdate_dat,
//								c.lastmodifydate_dat,
//								c.doctorname_vchr,
//								c.deptname_vchr,
//								c.inpatienttimes_int,
//								be.CODE_CHR,
//								0 Normalstatus
//								from t_emr_caserecall c
//								inner join t_opr_bih_leave le on c.registerid_chr = le.registerid_chr
//								and le.status_int = 1
//								inner join t_bse_bed be on be.areaid_chr=le.OUTAREAID_CHR and be.BEDID_CHR=le.OUTBEDID_CHR and be.STATUS_INT >= 1
//								where c.status_int = 1 and c.[DATE] between ? and ?
//								and c.[ID] = ?
//								union
//								select c.recallid_int,
//								c.registerid_chr,
//								c.inpatientid_vchr,
//								c.outdate_dat,
//								c.deptid_chr,
//								c.areaid_chr,
//								c.areaname_vchr,
//								c.patientname_vchr,
//								c.doctorid_chr,
//								c.recalldate_dat,
//								c.overduedays_int,
//								c.status_int,
//								c.operator_chr,
//								c.operationdate_dat,
//								c.lastmodifydate_dat,
//								c.doctorname_vchr,
//								c.deptname_vchr,
//								c.inpatienttimes_int,
//								c.CODE_CHR,
//								1 Normalstatus
//								from T_EMR_CASERECALL_HAND c 
//								where c.status_int = 1 and c.[DATE] between ? and ?
//								and c.[ID] = ?
//								order by outdate_dat";//3
			#endregion

			if(blnIsPastDue)
			{
				strSql += "WHERE c.status_int = 1 AND c.overduedays_int > 0 AND c.[DATE] BETWEEN ? AND ? AND c.[ID] = ? ";
				strSql += strSqlPart2;
				strSql += "WHERE c.status_int = 1 AND c.overduedays_int > 0 AND c.[DATE] BETWEEN ? AND ? AND c.[ID] = ? ORDER BY outdate_dat";
			}
			else
			{
				strSql += "WHERE c.status_int = 1 AND c.overduedays_int = 0 AND c.[DATE] BETWEEN ? AND ? AND c.[ID] = ? ";
				strSql += strSqlPart2;
				strSql += "WHERE c.status_int = 1 AND c.overduedays_int = 0 AND c.[DATE] BETWEEN ? AND ? AND c.[ID] = ? ORDER BY outdate_dat";
			}

			if(p_strAttributeId == "0000003")
				strSql = strSql.Replace("[ID]","areaid_chr");
			else
				strSql = strSql.Replace("[ID]","deptid_chr");
			if(p_blnIsOut)
				strSql = strSql.Replace("[DATE]","outdate_dat");
			else
				strSql = strSql.Replace("[DATE]","recalldate_dat");
			long lngRes = 0;
			try
			{
				clsHRPTableService objHRPServ =new clsHRPTableService();
				IDataParameter[] objDPArr = null;
				objHRPServ.CreateDatabaseParameter(6,out objDPArr);
				objDPArr[0].DbType = DbType.DateTime;
				objDPArr[0].Value = p_dtmStart;
				objDPArr[1].DbType = DbType.DateTime;
				objDPArr[1].Value = p_dtmEnd;
				objDPArr[2].Value = p_strId;
				objDPArr[3].DbType = DbType.DateTime;
				objDPArr[3].Value = p_dtmStart;
				objDPArr[4].DbType = DbType.DateTime;
				objDPArr[4].Value = p_dtmEnd;
				objDPArr[5].Value = p_strId;

				DataTable dtbResult = new DataTable();
				lngRes = objHRPServ.lngGetDataTableWithParameters(strSql,ref dtbResult,objDPArr);
				int intRowCount = dtbResult.Rows.Count;
				if(lngRes > 0 && intRowCount > 0)
				{
					p_objRecallValues = new clsRecallValues[intRowCount];
					DataRow objRow = null;
					for(int i=0;i<intRowCount;i++)
					{
						objRow = dtbResult.Rows[i];
						clsRecallValues objRecallValue = new clsRecallValues();
						objRecallValue.m_strRecallID = objRow["recallid_int"].ToString();
						objRecallValue.m_strRegidterID = objRow["registerid_chr"].ToString().Trim();
						objRecallValue.m_strInPatientID = objRow["inpatientid_vchr"].ToString().Trim();
						try
						{
							objRecallValue.m_dtmOutDate = DateTime.Parse(objRow["outdate_dat"].ToString());
						}
						catch{objRecallValue.m_dtmOutDate = new DateTime(1900,1,1);}
						objRecallValue.m_strDeptID = objRow["deptid_chr"].ToString();
						objRecallValue.m_strPatientName = objRow["patientname_vchr"].ToString();
						objRecallValue.m_strAreaID = objRow["areaid_chr"].ToString();
						objRecallValue.m_strAreaName = objRow["areaname_vchr"].ToString();
						objRecallValue.m_strDoctorID = objRow["doctorid_chr"].ToString().Trim();
						try
						{
							objRecallValue.m_dtmRecallDate = DateTime.Parse(objRow["recalldate_dat"].ToString());
						}
						catch{objRecallValue.m_dtmRecallDate = new DateTime(1900,1,1);}
						objRecallValue.m_strOverDueDays = objRow["overduedays_int"].ToString();
						objRecallValue.m_strStatus = "1";
						objRecallValue.m_strOpratorId = objRow["operator_chr"].ToString();
						try
						{
							objRecallValue.m_dtmOperationDate = DateTime.Parse(objRow["operationdate_dat"].ToString());
						}
						catch{objRecallValue.m_dtmOperationDate = new DateTime(1900,1,1);}
						try
						{
							objRecallValue.m_dtmLastModifyDate = DateTime.Parse(objRow["lastmodifydate_dat"].ToString());
						}
						catch{objRecallValue.m_dtmLastModifyDate = new DateTime(1900,1,1);}
						objRecallValue.m_strDoctorName = objRow["doctorname_vchr"].ToString();
						objRecallValue.m_strBedName = objRow["CODE_CHR"].ToString();
						objRecallValue.m_strDeptName = objRow["DEPTNAME_VCHR"].ToString();
						objRecallValue.m_strInTimes = objRow["inpatienttimes_int"].ToString();
						objRecallValue.m_strNormalstatus = objRow["Normalstatus"].ToString();
						p_objRecallValues[i] = objRecallValue;
					}
				}
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			return lngRes;
		}
		
		#endregion 已回收病历查询

		#region 应回收（未回收）病历查询
		/// <summary>
		/// 应回收（未回收）病历查询
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strId"></param>
		/// <param name="m_strAttributeId"></param>
		/// <param name="p_dtmStart"></param>
		/// <param name="p_dtmEnd"></param>
		/// <param name="p_objRecallValues"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetMuchRecallValues(string p_strId,string m_strAttributeId,DateTime p_dtmStart,DateTime p_dtmEnd,out clsRecallValues[] p_objRecallValues)
		{
			p_objRecallValues = null;
			if(p_strId == null || p_strId =="")
				return -1;
			string strSql = @"select pa.lastname_vchr,
       re.inpatientcount_int,
       re.inpatientid_chr,
       re.deptid_chr,
		re.areaid_chr,
       de.deptname_vchr deptname,
       de2.deptname_vchr areaname,
       be.CODE_CHR,
       le.modify_dat outdate,
       re.registerid_chr,
       le.type_int
  from t_bse_patient pa
 inner join t_opr_bih_register re on pa.patientid_chr = re.patientid_chr
 inner join t_opr_bih_leave le on re.registerid_chr = le.registerid_chr
                              and le.status_int = 1
 left join t_bse_bed be on be.areaid_chr=le.OUTAREAID_CHR and be.BEDID_CHR=le.OUTBEDID_CHR and be.STATUS_INT >= 1
 inner join t_bse_deptdesc de on de.deptid_chr = re.deptid_chr
 inner join t_bse_deptdesc de2 on de2.deptid_chr = re.areaid_chr
 where not exists (select c2.registerid_chr from t_emr_caserecall c2 
    where c2.outdate_dat between ? and ? and c2.status_int = 1 and c2.registerid_chr = le.registerid_chr)
and not exists (select c3.INPATIENTID_VCHR from T_EMR_CASERECALL_HAND c3 
                     where c3.outdate_dat between ? AND ?
                     and c3.status_int = 1 and c3.INPATIENTID_VCHR = re.inpatientid_chr
                                 and c3.inpatient_dat = re.inpatient_dat)
   and le.modify_dat between ? and ?
and re.inpatientid_chr not like '%.1'
and re.[ID] = ?
 order by le.modify_dat";//8
			if(m_strAttributeId == "0000003")
				strSql = strSql.Replace("[ID]","areaid_chr");
			else
				strSql = strSql.Replace("[ID]","deptid_chr");
			long lngRes = 0;
			try
			{
				clsHRPTableService objHRPServ =new clsHRPTableService();
				IDataParameter[] objDPArr = null;
				objHRPServ.CreateDatabaseParameter(7,out objDPArr);
//				objDPArr[0].Value = p_strId;
				objDPArr[0].DbType = DbType.DateTime;
				objDPArr[0].Value = p_dtmStart;
				objDPArr[1].DbType = DbType.DateTime;
				objDPArr[1].Value = p_dtmEnd;
				objDPArr[2].DbType = DbType.DateTime;
				objDPArr[2].Value = p_dtmStart;
				objDPArr[3].DbType = DbType.DateTime;
				objDPArr[3].Value = p_dtmEnd;
				objDPArr[4].DbType = DbType.DateTime;
				objDPArr[4].Value = p_dtmStart;
				objDPArr[5].DbType = DbType.DateTime;
				objDPArr[5].Value = p_dtmEnd;
				objDPArr[6].Value = p_strId;

				DataTable dtbResult = new DataTable();
				lngRes = objHRPServ.lngGetDataTableWithParameters(strSql,ref dtbResult,objDPArr);
				int intRowCount = dtbResult.Rows.Count;
				if(lngRes > 0 && intRowCount > 0)
				{
					p_objRecallValues = new clsRecallValues[intRowCount];
					DataRow objRow = null;
					DateTime dtmMinDate = DateTime.MaxValue;
					DateTime dtmMaxDate = DateTime.MinValue;
					for(int i=0;i<intRowCount;i++)
					{
						objRow = dtbResult.Rows[i];
						clsRecallValues objRecallValue = new clsRecallValues();
						objRecallValue.m_strRegidterID = objRow["registerid_chr"].ToString().Trim();
						objRecallValue.m_strInPatientID = objRow["inpatientid_chr"].ToString().Trim();
						objRecallValue.m_strType = objRow["type_int"].ToString();//此处赋值必须在objRecallValue.m_dtmOutDate赋值之前
						try
						{
							objRecallValue.m_dtmOutDate = DateTime.Parse(objRow["outdate"].ToString());
							if(dtmMinDate > objRecallValue.m_dtmOutDate) dtmMinDate = objRecallValue.m_dtmOutDate;
							if(dtmMaxDate < objRecallValue.m_dtmOutDate) dtmMaxDate = objRecallValue.m_dtmOutDate;
						}
						catch{objRecallValue.m_dtmOutDate = new DateTime(1900,1,1);}
						objRecallValue.m_strDeptID = objRow["deptid_chr"].ToString();
						objRecallValue.m_strDeptName = objRow["deptname"].ToString();
						objRecallValue.m_strAreaID = objRow["areaid_chr"].ToString();
						objRecallValue.m_strAreaName = objRow["areaname"].ToString();
						objRecallValue.m_strPatientName = objRow["lastname_vchr"].ToString();
//						if(!(objRow["hempid"] is DBNull))
//						{
//							objRecallValue.m_strDoctorID = objRow["hempid"].ToString().Trim();
//							objRecallValue.m_strDoctorName = objRow["HName"].ToString();
//						}
//						else if(!(objRow["imempid"] is DBNull))
//						{
//							objRecallValue.m_strDoctorID = objRow["imempid"].ToString().Trim();
//							objRecallValue.m_strDoctorName = objRow["imName"].ToString();
//						}
						objRecallValue.m_strInTimes = objRow["inpatientcount_int"].ToString();
						objRecallValue.m_strBedName = objRow["CODE_CHR"].ToString();
						
						p_objRecallValues[i] = objRecallValue;
					}
					dtmMaxDate = dtmMaxDate.AddMonths(2);
					DateTime[] dtmArr = m_objGetHoliday(null,dtmMinDate,dtmMaxDate);
					if(dtmArr != null && dtmArr.Length > 0 && p_objRecallValues.Length > 0)
					{
						for(int j=0;j<p_objRecallValues.Length;j++)
						{
							p_objRecallValues[j].m_dtExcludeHolidayArr = dtmArr;
						}
					}
                    //以下为周六日获取
                    DateTime[] dtmArrWeekEnd = m_objGetWorkDayInWeekEnd(null, p_dtmStart, p_dtmEnd);
                    if (dtmArrWeekEnd != null && dtmArrWeekEnd.Length > 0 && p_objRecallValues.Length > 0)
                    {
                        for (int k = 0; k < p_objRecallValues.Length; k++)
                        {
                            p_objRecallValues[k].m_dtIncludeHolidayArr = dtmArrWeekEnd;
                        }
                    }
				}
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			return lngRes;
		}
		/// <summary>
		/// 全院统计应回收病历(未回收)
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_dtmStart"></param>
		/// <param name="p_dtmEnd"></param>
		/// <param name="p_objRecallValues"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetAllMuchRecallStatis(DateTime p_dtmStart,DateTime p_dtmEnd,out clsAllMuchRecallStatisValues[] p_strAllValueArr)
		{
			p_strAllValueArr = null;
			string strSql = @"select re.areaid_chr, de.deptname_vchr,count(re.registerid_chr) casecount
  from t_bse_deptdesc de
 inner join t_opr_bih_register re on re.areaid_chr = de.deptid_chr
 inner join t_opr_bih_leave le on re.registerid_chr = le.registerid_chr
                              and le.status_int = 1
inner join t_bse_patient pa on pa.patientid_chr = re.patientid_chr
 where not exists
 (select c2.registerid_chr
          from t_emr_caserecall c2
         where c2.outdate_dat between ? and ?
           and c2.status_int = 1
           and c2.registerid_chr = le.registerid_chr)
   and le.modify_dat between ? and ?
and re.inpatientid_chr not like '%.1'
 group by re.areaid_chr,de.deptname_vchr
 order by de.deptname_vchr";//4
			long lngRes = 0;
			try
			{
				clsHRPTableService objHRPServ =new clsHRPTableService();
				IDataParameter[] objDPArr = null;
				objHRPServ.CreateDatabaseParameter(4,out objDPArr);
				objDPArr[0].DbType = DbType.DateTime;
				objDPArr[0].Value = p_dtmStart;
				objDPArr[1].DbType = DbType.DateTime;
				objDPArr[1].Value = p_dtmEnd;
				objDPArr[2].DbType = DbType.DateTime;
				objDPArr[2].Value = p_dtmStart;
				objDPArr[3].DbType = DbType.DateTime;
				objDPArr[3].Value = p_dtmEnd;
				
				DataTable dtbResult = new DataTable();
				lngRes = objHRPServ.lngGetDataTableWithParameters(strSql,ref dtbResult,objDPArr);
				int intRowCount = dtbResult.Rows.Count;
				if(lngRes > 0 && intRowCount > 0)
				{
					p_strAllValueArr = new clsAllMuchRecallStatisValues[intRowCount];
					DataRow objRow = null;
					for(int i=0;i<intRowCount;i++)
					{
						clsAllMuchRecallStatisValues objValue = new clsAllMuchRecallStatisValues();
						objRow = dtbResult.Rows[i];
						objValue.m_strDeptID = objRow["areaid_chr"].ToString().Trim();
						objValue.m_strDeptName = objRow["deptname_vchr"].ToString();
						objValue.m_strCount = objRow["casecount"].ToString();
						p_strAllValueArr[i] = objValue;
					}
				}
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			return lngRes;
		}
		
		#endregion 应回收（未回收）病历查询

		#region 手工回收病历、病历回收率 WQF
		/// <summary>
		/// 获取所输入病人ID的所有相关信息 
		/// WQF 2007-01-4
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInpatientId"></param>
		/// <param name="p_objRecallValues"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetHadRecallValueByPatientId_Hand(IPrincipal p_objPrincipal,string p_strInpatientId,out clsRecallValues[] p_objRecallValues)
		{
			p_objRecallValues = null;
			if(p_strInpatientId == null|| p_strInpatientId == "")
				return -1;
			//-----------------------------------------------------
			//选出所有未回收的纪录
			//说明：
			//		TOBL.status_int = 0 ：还未出院
			//		TBB.STATUS_INT >= 1 ：占有床位(可能大于1个床位)
			//-----------------------------------------------------
			//			string sqlQuery = @"SELECT DISTINCT
			//								TBP.lastname_vchr,	TOBR.inpatientcount_int,	TOBR.inpatientid_chr,
			//								TOBR.deptid_chr,	TOBR.areaid_chr,			TBD1.deptname_vchr deptname,
			//								TBB.CODE_CHR,		TOBL.modify_dat outdate,	TOBL.type_int,
			//								TOBR.registerid_chr,TBD1.deptname_vchr areaname,'' overduedays_int,
			//								'' recalldate_dat,	'' recallid_int,			'' status_int
			//							FROM t_opr_bih_register TOBR 
			//							LEFT JOIN t_opr_bih_leave TOBL
			//							ON TOBR.registerid_chr = TOBL.registerid_chr AND TOBL.status_int = 0
			//							INNER JOIN t_bse_patient TBP
			//							ON TBP.patientid_chr = TOBR.patientid_chr
			//							INNER JOIN t_bse_bed TBB
			//							ON TBB.areaid_chr=TOBL.OUTAREAID_CHR AND TBB.BEDID_CHR=TOBL.OUTBEDID_CHR AND TBB.STATUS_INT >= 1
			//							INNER JOIN t_bse_deptdesc TBD1
			//							ON TBD1.deptid_chr = TOBR.deptid_chr
			//							WHERE TOBR.inpatientid_chr = ? AND TOBR.REGISTERID_CHR NOT IN
			//							(SELECT DISTINCT REGISTERID_CHR FROM t_emr_caserecall TEC
			//							UNION  SELECT DISTINCT REGISTERID_CHR FROM T_EMR_CASERECALL_HAND)
			//							ORDER BY TBP.lastname_vchr ASC";
			string sqlQuery = @"SELECT TBP.lastname_vchr,	TOBR.inpatientcount_int,	TOBR.inpatientid_chr,
								TOBR.deptid_chr,	TOBR.areaid_chr,			TBD1.deptname_vchr deptname,
								TBB.CODE_CHR,		TOBL.modify_dat outdate,	TOBL.type_int,
								TOBR.registerid_chr,TBD1.deptname_vchr areaname,'' overduedays_int,
								'' recalldate_dat,	'' recallid_int,			'' status_int
							FROM t_opr_bih_register TOBR 
							LEFT JOIN t_opr_bih_leave TOBL
							ON TOBR.registerid_chr = TOBL.registerid_chr AND TOBL.status_int = 1
							INNER JOIN t_bse_patient TBP
							ON TBP.patientid_chr = TOBR.patientid_chr
							LEFT JOIN t_bse_bed TBB
							ON TBB.areaid_chr=TOBL.OUTAREAID_CHR AND TBB.BEDID_CHR=TOBL.OUTBEDID_CHR AND TBB.STATUS_INT >= 1
							INNER JOIN t_bse_deptdesc TBD1
							ON TBD1.deptid_chr = TOBR.deptid_chr
							WHERE TOBR.inpatientid_chr = ? AND TOBR.PSTATUS_INT=3 AND NOT exists 
							(SELECT REGISTERID_CHR FROM t_emr_caserecall TEC where tec.REGISTERID_CHR = TOBR.REGISTERID_CHR and tec.status_int= 1)
							and not exists(SELECT inpatientid_vchr FROM T_EMR_CASERECALL_HAND  crh where crh.inpatientid_vchr = TOBR.inpatientid_chr
											and crh.inpatient_dat = TOBR.inpatient_dat and crh.status_int= 0)
							ORDER BY TOBR.inpatient_dat ";
			long lngRes = 0;
			try
			{
				clsHRPTableService objHRPServ =new clsHRPTableService();
				IDataParameter[] objDPArr = null;
				//按照SQL语句中的参数(?)生成SQL参数 objDPArr
				objHRPServ.CreateDatabaseParameter(1,out objDPArr);
				objDPArr[0].Value = p_strInpatientId;
				DataTable dtbResult = new DataTable();
				//执行SQL语句获取所有相关纪录
				lngRes = objHRPServ.lngGetDataTableWithParameters(sqlQuery,ref dtbResult,objDPArr);
				int intRowCount = dtbResult.Rows.Count;
				if(lngRes > 0 && intRowCount > 0)
				{
					//将执行SQL语句返回的数据填充至p_objRecallValues
					p_objRecallValues = m_objGetData(dtbResult);
				}
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				objLogger.LogError(objEx);
			}
			return lngRes;
		}
		/// <summary>
		/// 获取所输入病人ID的所有已回收病厉信息 (仅适用于手工回收病历)
		/// WQF 2007-02-28
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInpatientId"></param>
		/// <param name="p_objRecallValues"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetPatientByInId_Hand(string p_strInpatientId,out clsRecallValues[] p_objRecallValues)
		{
			p_objRecallValues = null;
			if(p_strInpatientId == null|| p_strInpatientId == "")
				return -1;
			
			string sqlQuery = @"SELECT TBP.lastname_vchr,	TOBR.inpatientcount_int,	TOBR.inpatientid_chr,
								TOBR.deptid_chr,	TOBR.areaid_chr,			TBD1.deptname_vchr deptname,
								TBB.CODE_CHR,		TOBL.modify_dat outdate,	TOBL.type_int,
								TOBR.registerid_chr,TBD1.deptname_vchr areaname,'' overduedays_int,
								'' recalldate_dat,	'' recallid_int,			'' status_int
							FROM t_opr_bih_register TOBR 
							LEFT JOIN t_opr_bih_leave TOBL
							ON TOBR.registerid_chr = TOBL.registerid_chr AND TOBL.status_int = 1
							INNER JOIN t_bse_patient TBP
							ON TBP.patientid_chr = TOBR.patientid_chr
							LEFT JOIN t_bse_bed TBB
							ON TBB.areaid_chr=TOBL.OUTAREAID_CHR AND TBB.BEDID_CHR=TOBL.OUTBEDID_CHR AND TBB.STATUS_INT >= 1
							INNER JOIN t_bse_deptdesc TBD1
							ON TBD1.deptid_chr = TOBR.deptid_chr
							WHERE TOBR.inpatientid_chr = ? AND TOBR.PSTATUS_INT=3 AND NOT exists 
							(SELECT REGISTERID_CHR FROM t_emr_caserecall TEC where tec.REGISTERID_CHR = TOBR.REGISTERID_CHR and tec.status_int= 1)
							and not exists(SELECT inpatientid_vchr FROM T_EMR_CASERECALL_HAND  crh where crh.inpatientid_vchr = TOBR.inpatientid_chr
											and crh.inpatient_dat = TOBR.inpatient_dat and crh.status_int= 1)
							ORDER BY TOBR.inpatient_dat ";
			long lngRes = 0;
			try
			{
				clsHRPTableService objHRPServ =new clsHRPTableService();
				IDataParameter[] objDPArr = null;
				//按照SQL语句中的参数(?)生成SQL参数 objDPArr
				objHRPServ.CreateDatabaseParameter(1,out objDPArr);
				objDPArr[0].Value = p_strInpatientId;
				DataTable dtbResult = new DataTable();
				//执行SQL语句获取所有相关纪录
				lngRes = objHRPServ.lngGetDataTableWithParameters(sqlQuery,ref dtbResult,objDPArr);
				int intRowCount = dtbResult.Rows.Count;
				if(lngRes > 0 && intRowCount > 0)
				{
					//将执行SQL语句返回的数据填充至p_objRecallValues
					p_objRecallValues = m_objGetData(dtbResult);
				}
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				objLogger.LogError(objEx);
			}
			return lngRes;
		}
		/// <summary>
		/// 回收病历,将信息保存至 T_EMR_CASERECALL_HAND 表 
		/// WQF 2006-12-31
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_objArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngAddRecallRow(IPrincipal p_objPrincipal, clsRecallValues[] p_objArr)
		{
			long lngRes = 0;
			if(p_objArr == null || p_objArr.Length ==0)
				return 0;
			string sqlQuery = @"INSERT INTO T_EMR_CASERECALL_HAND 
					(REGISTERID_CHR,	INPATIENTID_VCHR,	OUTDATE_DAT,	DEPTID_CHR,		PATIENTNAME_VCHR,
					DOCTORID_CHR,		RECALLDATE_DAT,		OVERDUEDAYS_INT,STATUS_INT,		OPERATOR_CHR,
					OPERATIONDATE_DAT,	LASTMODIFYDATE_DAT,	DEPTNAME_VCHR,	DOCTORNAME_VCHR,INPATIENTTIMES_INT,
					areaid_chr,			AREANAME_VCHR,		INPATIENT_DAT,	BEDID_CHR,
					CODE_CHR) 
					VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?,NULL, getdate(), ?, ?, ?, ?, ?, ?, ?, ?)";//18
			try
			{
				clsHRPTableService objHRPServ =new clsHRPTableService();
				IDataParameter[] objDPArr = null;
				for(int i = 0; i < p_objArr.Length; i++)
				{
					long lngEff = 0;
					if(p_objArr[i] != null)
					{
						objDPArr = null;
						objHRPServ.CreateDatabaseParameter(18,out objDPArr);
						if(objDPArr != null)
						{
							objDPArr[0].Value = p_objArr[i].m_strRegidterID;//2
							objDPArr[1].Value = p_objArr[i].m_strInPatientID;//3
							objDPArr[2].DbType = DbType.DateTime;
							objDPArr[2].Value = p_objArr[i].m_dtmOutDate;//4
							objDPArr[3].Value = p_objArr[i].m_strDeptID;//5
							objDPArr[4].Value = p_objArr[i].m_strPatientName;//6
							objDPArr[5].Value = p_objArr[i].m_strDoctorID;//7
							objDPArr[6].DbType = DbType.DateTime;
							objDPArr[6].Value = p_objArr[i].m_dtmRecallDate;//8
							objDPArr[7].Value = p_objArr[i].m_strOverDueDays;//9
							objDPArr[8].Value = p_objArr[i].m_strStatus;//10
							objDPArr[9].Value = p_objArr[i].m_strOpratorId;//11
							objDPArr[10].Value = p_objArr[i].m_strDeptName;//14
							objDPArr[11].Value = p_objArr[i].m_strDoctorName;//15
							objDPArr[12].Value = p_objArr[i].m_strInTimes;//16
							objDPArr[13].Value = p_objArr[i].m_strAreaID;//17
							objDPArr[14].Value = p_objArr[i].m_strAreaName;//18
							objDPArr[15].DbType = DbType.DateTime;
							objDPArr[15].Value = p_objArr[i].m_dtmInPatient_dat;//20
							objDPArr[16].Value = p_objArr[i].m_strBedid_chr;//21
							objDPArr[17].Value = p_objArr[i].m_strCode_chr;//22
							
//							lngRes = objHRPServ.lngExecuteParameterSQL(sqlQuery,ref lngEff,objDPArr);
							objHRPServ.lngExecuteParameterSQL(sqlQuery,ref lngEff,objDPArr);
							lngRes += lngEff;
						}
					}
				}
			}
			catch(Exception ex)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				objLogger.LogError(ex);
			}
			return lngRes;
		}
		/// <summary>
		/// 获取出院病历及时回收率数据 
		/// WQF 2007-1-8 
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="startTime">开始时间</param>
		/// <param name="endtTime">结束时间</param>
		/// <returns>数据表</returns>
		[AutoComplete]
		public DataTable m_GetRecallRatioData(int hours,DateTime startTime,DateTime endTime)
		{
			DataTable resultDt = null;
			string sqlQuery = @"SELECT TBD1.deptName_Vchr DEPART,
								COUNT(DISTINCT TOBL.registerid_chr)+COUNT(DISTINCT TECH.REGISTERID_CHR) ALLOUT ,
								COUNT(DISTINCT TEC.REGISTERID_CHR) +
								COUNT(DISTINCT TECH.REGISTERID_CHR) REALRECALL
								FROM t_opr_bih_register TOBR
								INNER JOIN t_opr_bih_leave TOBL ON TOBR.registerid_chr = TOBL.registerid_chr AND TOBL.status_int = 1
								INNER JOIN t_bse_deptdesc TBD1 ON TBD1.deptid_chr = TOBR.AREAID_CHR
								LEFT JOIN (select * from t_emr_caserecall where OVERDUEDAYS_INT < ? and status_int = 1) TEC 
								ON TEC.REGISTERID_CHR = TOBR.REGISTERID_CHR
								LEFT JOIN (select * from T_EMR_CASERECALL_HAND where OVERDUEDAYS_INT < ? and status_int = 1) TECH 
								ON TECH.INPATIENTID_VCHR = TOBR.INPATIENTID_CHR AND TECH.INPATIENT_DAT = TOBR.INPATIENT_DAT
								WHERE TOBL.MODIFY_DAT BETWEEN ? AND ?
								and TOBR.inpatientid_chr not like '%.1'
								GROUP BY TBD1.deptName_Vchr
								ORDER BY TBD1.deptName_Vchr";
			try
			{
				clsHRPTableService objHRPServ =new clsHRPTableService();
				resultDt = new DataTable();
				IDataParameter[] objDPArr = null;
				//按照SQL语句中的参数(?)生成SQL参数 objDPArr
				objHRPServ.CreateDatabaseParameter(4,out objDPArr);
				objDPArr[0].DbType = DbType.Int32;
				objDPArr[0].Value = hours/24;
				objDPArr[1].DbType = DbType.Int32;
				objDPArr[1].Value = hours/24;
				objDPArr[2].DbType = DbType.DateTime;
				objDPArr[2].Value = startTime;
				objDPArr[3].DbType = DbType.DateTime;
				objDPArr[3].Value = endTime;

				DataTable dtbResult = new DataTable();
				//执行SQL语句获取所有相关纪录
				objHRPServ.lngGetDataTableWithParameters(sqlQuery,ref resultDt,objDPArr);
				//格式化数据表
				if(resultDt != null && resultDt.Rows.Count > 0)
				{
					resultDt.TableName = "RecallRatioTable";
					resultDt.Columns[0].ColumnName = "住院科室/病区";
					resultDt.Columns[1].ColumnName = "应回收病历数";
					resultDt.Columns[2].ColumnName = "按时回收病历数";
					DataColumn dc = new DataColumn("及时回收率");
					resultDt.Columns.Add(dc);

					foreach(DataRow dr in resultDt.Rows)
					{
						Double all = 0;
						Double real = 0;
						try
						{
							all = Double.Parse(dr[1].ToString());
							real = Double.Parse(dr[2].ToString());
						}
						catch{}
						//计算及时回收率
						if(all > 0)
							dr[3] = System.Math.Round(real / all,6) * 100;//控制结果最多只有6位有效数字
						else
							dr[3] = 0;
					}
				}
			}
			catch(Exception ex)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				objLogger.LogError(ex);
			}
			return resultDt;
		}

		#endregion
                #region 假期设置

        /// <summary>
		/// 保存节假日
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="dtArr"></param>
        /// <param name="dtArr2"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngAddHoliday(IPrincipal p_objPrincipal, DateTime[] dtArr,DateTime[] dtArr2)
		{
			long lngEff = 0;
			long lngRes = 0;
//			DataTable objHolidayValues = new DataTable();
			DateTime[] dtHolidayValues = null;
			ArrayList arlHolidayValues = new ArrayList();
			string sqlQuery = "insert into t_emr_holiday(holiday) values(?)";
            string sqlQuery2 = "insert into t_emr_holiday(workdayinweekend) values(?)";
            //1------------------------------------------------------------
            #region 对假期列进行插入
            if (dtArr != null)
            {
                #region 对假期列进行逻辑判断
                try
                {

                    DateTime dtMax = dtArr[0];
                    DateTime dtMin = dtMax;
                    foreach (DateTime dtOne in dtArr)
                    {
                        int intDayCount1 = ((TimeSpan)(dtOne - dtMax)).Days;
                        if (intDayCount1 > 0)
                        {
                            dtMax = dtOne;
                        }
                        else
                        {
                            int intDayCount2 = ((TimeSpan)(dtMin - dtOne)).Days;
                            if (intDayCount2 > 0)
                                dtMin = dtOne;
                        }
                    }
                    dtHolidayValues = this.m_objGetHoliday(p_objPrincipal, dtMin, dtMax);
                    if (dtHolidayValues != null)
                    {
                        foreach (DateTime dtOneValue in dtArr)
                        {
                            bool blnHasRow = false;
                            foreach (DateTime dtOne in dtHolidayValues)
                            {
                                if (dtOneValue.Equals(dtOne))
                                {
                                    blnHasRow = true;
                                    break;
                                }
                            }
                            if (!blnHasRow)
                                arlHolidayValues.Add(dtOneValue);
                        }
                    }
                    else
                    {
                        foreach (DateTime dtOneValue in dtArr)
                        {
                            arlHolidayValues.Add(dtOneValue);
                        }
                    }
                }
                catch (Exception ex)
                {
                    com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                    objLogger.LogError(ex);
                    return 0;
                }

                #endregion
                //------------------------------------------------------------
                clsHRPTableService objHRPServ = new clsHRPTableService();
                foreach (DateTime dtHoliday in arlHolidayValues)
                {
                    try
                    {
                        IDataParameter[] objDPArr = null;
                        objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                        objDPArr[0].DbType = DbType.DateTime;
                        objDPArr[0].Value = dtHoliday;

                        lngRes += objHRPServ.lngExecuteParameterSQL(sqlQuery, ref lngEff, objDPArr);
                    }
                    catch (Exception ex)
                    {
                        com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                        objLogger.LogError(ex);
                    }
                }
                objHRPServ.Dispose();
            }
            #endregion

            //2------------------------------------------------------------
            DateTime[] dtWorkDayInWeekend = null;
            ArrayList arlWorkDayInWeekend = new ArrayList();
            #region 对必须上班列进行插入
            if (dtArr2 != null)
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                #region 对必须上班列进行逻辑判断
                try
                {

                    DateTime dtMax = dtArr2[0];
                    DateTime dtMin = dtMax;
                    foreach (DateTime dtOne in dtArr2)
                    {
                        int intDayCount1 = ((TimeSpan)(dtOne - dtMax)).Days;
                        if (intDayCount1 > 0)
                        {
                            dtMax = dtOne;
                        }
                        else
                        {
                            int intDayCount2 = ((TimeSpan)(dtMin - dtOne)).Days;
                            if (intDayCount2 > 0)
                                dtMin = dtOne;
                        }
                    }
                    dtWorkDayInWeekend = this.m_objGetWorkDayInWeekEnd(p_objPrincipal, dtMin, dtMax);
                    if (dtWorkDayInWeekend != null)
                    {
                        foreach (DateTime dtOneValue in dtArr2)
                        {
                            bool blnHasRow = false;
                            foreach (DateTime dtOne in dtWorkDayInWeekend)
                            {
                                if (dtOneValue.Equals(dtOne))
                                {
                                    blnHasRow = true;
                                    break;
                                }
                            }
                            if (!blnHasRow)
                                arlWorkDayInWeekend.Add(dtOneValue);
                        }
                    }
                    else
                    {
                        foreach (DateTime dtOneValue in dtArr2)
                        {
                            arlWorkDayInWeekend.Add(dtOneValue);
                        }
                    }
                }
                catch (Exception ex)
                {
                    com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                    objLogger.LogError(ex);
                    return 0;
                }

                #endregion
                //------------------------------------------------------------
                foreach (DateTime dt_WorkDayInWeekend in arlWorkDayInWeekend)
                {
                    try
                    {
                        IDataParameter[] objDPArr = null;
                        objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                        objDPArr[0].DbType = DbType.DateTime;
                        objDPArr[0].Value = dt_WorkDayInWeekend;

                        lngRes += objHRPServ.lngExecuteParameterSQL(sqlQuery2, ref lngEff, objDPArr);
                    }
                    catch (Exception ex)
                    {
                        com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                        objLogger.LogError(ex);
                    }
                }
                objHRPServ.Dispose();
            }
            #endregion
            return lngRes;
		}


		/// <summary>
		/// 查询节假日
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="dtStart"></param>
		/// <param name="dtEnd"></param>
		/// <param name="objHolidayValues"></param>
		/// <returns></returns>
		[AutoComplete]
		public DateTime[] m_objGetHoliday(IPrincipal p_objPrincipal, DateTime dtStart, DateTime dtEnd)
		{
			long lngRes = 0;
			DateTime[] dtHolidayValues = null;
			string sqlQuery = "select holiday from t_emr_holiday where holiday between ? and ?";
			clsHRPTableService objHRPServ =new clsHRPTableService();
			DataTable objValues = new DataTable();

			try
			{
				IDataParameter[] objDPArr = null;
				objHRPServ.CreateDatabaseParameter(2,out objDPArr);
				objDPArr[0].DbType = DbType.DateTime;
				objDPArr[0].Value = dtStart;
				objDPArr[1].DbType = DbType.DateTime;
				objDPArr[1].Value = dtEnd;
				
				lngRes = objHRPServ.lngGetDataTableWithParameters(sqlQuery,ref objValues,objDPArr);
				int intRowCount = objValues.Rows.Count;
				if(lngRes > 0 && intRowCount > 0)
				{
					dtHolidayValues = new DateTime[intRowCount];
					for(int i = 0; i < intRowCount; i++)
					{
						dtHolidayValues[i] = (DateTime)objValues.Rows[i]["holiday"];
					}
				}
			}
			catch(Exception ex)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				objLogger.LogError(ex);
			}

			return dtHolidayValues;
		}

        /// <summary>
        /// 查询上班日
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <param name="objHolidayValues"></param>
        /// <returns></returns>
        [AutoComplete]
        public DateTime[] m_objGetWorkDayInWeekEnd(IPrincipal p_objPrincipal, DateTime dtStart, DateTime dtEnd)
        {
            long lngRes = 0;
            DateTime[] dtWorkDayInWeekEndValues = null;
            string sqlQuery = "select workdayinweekend from t_emr_holiday where workdayinweekend between ? and ?";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            DataTable objValues = new DataTable();

            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = dtStart;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = dtEnd;

                lngRes = objHRPServ.lngGetDataTableWithParameters(sqlQuery, ref objValues, objDPArr);
                int intRowCount = objValues.Rows.Count;
                if (lngRes > 0 && intRowCount > 0)
                {
                    dtWorkDayInWeekEndValues = new DateTime[intRowCount];
                    for (int i = 0; i < intRowCount; i++)
                    {
                        dtWorkDayInWeekEndValues[i] = (DateTime)objValues.Rows[i]["workDayInWeekEnd"];
                    }
                }
            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                objLogger.LogError(ex);
            }

            return dtWorkDayInWeekEndValues;
        }
		/// <summary>
		/// 删除指定的节假日
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="dtRemoveValues"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngRemoveHolidayValues(IPrincipal p_objPrincipal, DateTime[] dtRemoveValues)
		{
			if(dtRemoveValues == null || dtRemoveValues.Length ==0)
				return 0;

			string strSql = @"	delete from t_emr_holiday where Holiday = ?";
			
			long lngRes = 0;
			try
			{
				clsHRPTableService objHRPServ =new clsHRPTableService();
				IDataParameter[] objDPArr = null;
				foreach(DateTime dtValue in dtRemoveValues)
				{
					long lngEff = 0;

					objDPArr = null;
					objHRPServ.CreateDatabaseParameter(1,out objDPArr);
					
					objDPArr[0].DbType = DbType.DateTime;
					objDPArr[0].Value = dtValue;

					objHRPServ.lngExecuteParameterSQL(strSql,ref lngEff,objDPArr);
					lngRes += lngEff;
				}
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				objLogger.LogError(objEx);
			}
			return lngRes;
		}

        /// <summary>
		/// 删除指定的节假日
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="dtRemoveValues"></param>
		/// <returns></returns>
		[AutoComplete]
        public long m_lngRemoveWorkDayInWeekEndValues(IPrincipal p_objPrincipal, DateTime[] dtRemoveValues)
		{
			if(dtRemoveValues == null || dtRemoveValues.Length ==0)
				return 0;

            string strSql = @"	delete from t_emr_holiday where WorkDayInWeekEnd = ?";
			
			long lngRes = 0;
			try
			{
				clsHRPTableService objHRPServ =new clsHRPTableService();
				IDataParameter[] objDPArr = null;
				foreach(DateTime dtValue in dtRemoveValues)
				{
					long lngEff = 0;

					objDPArr = null;
					objHRPServ.CreateDatabaseParameter(1,out objDPArr);
					
					objDPArr[0].DbType = DbType.DateTime;
					objDPArr[0].Value = dtValue;

					objHRPServ.lngExecuteParameterSQL(strSql,ref lngEff,objDPArr);
					lngRes += lngEff;
				}
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				objLogger.LogError(objEx);
			}
			return lngRes;
		}
		#endregion

        #region 修改日期权限判断
        /// <summary>
        /// 根据员工流水号获取权限角色ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEmpID">员工流水号</param>
        /// <param name="p_dtbRoleID">权限角色ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRoleIDByEmpID(IPrincipal p_objPrincipal,
            string p_strEmpID, out DataTable p_dtbRoleID)
        {
            long lngRes = -1;
            p_dtbRoleID = new DataTable();
            string strSQL = @"select b.name_vchr from t_sys_emprolemap a join t_sys_role b on a.roleid_chr=b.roleid_chr
	                       where a.empid_chr='" + p_strEmpID + "'";  
            try
            {
                DataTable dtbValue = new DataTable();
                lngRes = new clsHRPTableService().DoGetDataTable(strSQL, ref dtbValue);
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_dtbRoleID = dtbValue;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion 修改日期权限判断
        */
        #endregion

        #region 病历回收
        /// <summary>
        /// 保存回收记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveRecallValue( clsRecallValues[] p_objArr)
        {
            if (p_objArr == null || p_objArr.Length == 0)
                return 0;

            string strSql = @"insert into t_emr_caserecall
								(registerid_chr,
								inpatientid_vchr,
								outdate_dat,
								deptid_chr,
								patientname_vchr,
								doctorid_chr,
								recalldate_dat,
								overduedays_int,
								status_int,
								operator_chr,
								operationdate_dat,
								lastmodifydate_dat,
								doctorname_vchr,
								deptname_vchr,
								inpatienttimes_int,
								areaid_chr,
								areaname_vchr,recallid_int)
								values
								(?,?, ?, ?, ?, ?, ?, ?, 1,?, ?, ?, ?, ?, ?,?,?,?)";//14

            long lngRes = 0;
            long lngRECALLID = 0;
            com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
            lngRes = objSign.m_lngGetRecallids("SEQ_RECALLID", p_objArr.Length, out lngRECALLID);

            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                for (int i = 0; i < p_objArr.Length; i++)
                {
                    long lngEff = 0;
                    if (p_objArr[i] != null)
                    {
                        objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
                        objHRPServ.CreateDatabaseParameter(17, out objDPArr);
                        objDPArr[0].Value = p_objArr[i].m_strRegidterID;
                        objDPArr[1].Value = p_objArr[i].m_strInPatientID;
                        objDPArr[2].DbType = DbType.DateTime;
                        objDPArr[2].Value = p_objArr[i].m_dtmOutDate;
                        objDPArr[3].Value = p_objArr[i].m_strDeptID;
                        objDPArr[4].Value = p_objArr[i].m_strPatientName;
                        objDPArr[5].Value = p_objArr[i].m_strDoctorID;
                        objDPArr[6].DbType = DbType.DateTime;
                        objDPArr[6].Value = p_objArr[i].m_dtmRecallDate;
                        objDPArr[7].Value = p_objArr[i].m_strOverDueDays;
                        objDPArr[8].Value = p_objArr[i].m_strOpratorId;
                        objDPArr[9].DbType = DbType.DateTime;
                        objDPArr[9].Value = DateTime.Now;
                        objDPArr[10].DbType = DbType.DateTime;
                        objDPArr[10].Value = DateTime.Now;
                        objDPArr[11].Value = p_objArr[i].m_strDoctorName;
                        objDPArr[12].Value = p_objArr[i].m_strDeptName;
                        objDPArr[13].Value = p_objArr[i].m_strInTimes;
                        objDPArr[14].Value = p_objArr[i].m_strAreaID;
                        objDPArr[15].Value = p_objArr[i].m_strAreaName;
                        objDPArr[16].Value = lngRECALLID - p_objArr.Length + i;
                        //						lngRes = objHRPServ.lngExecuteParameterSQL(strSql,ref lngEff,objDPArr);
                        objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
                        lngRes += lngEff;
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


        /// <summary>
        /// 取消回收返,回影响行数
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecallIdArr"></param>
        /// <returns>返回影响行数</returns>
        [AutoComplete]
        public long m_lngCancelRecallValue( string[] p_strRecallIdArr)
        {
            if (p_strRecallIdArr == null || p_strRecallIdArr.Length == 0)
                return 0;
            string strSql = @"	update t_emr_caserecall
								set status_int = 0, lastmodifydate_dat = ?
								where recallid_int = ?  and status_int = 1";
            long lngEff = 0;
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                for (int i = 0; i < p_strRecallIdArr.Length; i++)
                {
                    objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                    objDPArr[0].DbType = DbType.DateTime;
                    objDPArr[0].Value = DateTime.Now;
                    objDPArr[1].Value = p_strRecallIdArr[i];

                    objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
                    lngRes += lngEff;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                objLogger.LogError(objEx);
                return 0;
            }
            return lngRes;
        }
        /// <summary>
        /// 取消回收(只限于手工回收的记录)返回影响行数
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecallIdArr"></param>
        /// <returns>返回影响行数</returns>
        [AutoComplete]
        public long m_lngCancelRecallByHand( string[] p_strRecallIdArr)
        {
            if (p_strRecallIdArr == null || p_strRecallIdArr.Length == 0)
                return 0;
            string strSql = @"	UPDATE T_EMR_CASERECALL_HAND
								SET status_int = 0, lastmodifydate_dat = GETDATE()
								WHERE recallid_int = ?  and status_int = 1";
            long lngEff = 0;
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                int intRecallIdLength = p_strRecallIdArr.Length;
                for (int i = 0; i < intRecallIdLength; i++)
                {
                    objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                    objDPArr[0].Value = p_strRecallIdArr[i];

                    objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
                    lngRes += lngEff;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                objLogger.LogError(objEx);
                return 0;
            }
            return lngRes;
        }

        /// <summary>
        /// 取消回收(根据住院登记号和回收时间,只限于手工回收的记录)返回影响行数
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_dtmRecallDate"></param>
        /// <returns>返回影响行数</returns>
        [AutoComplete]
        public long m_lngCancelRecallByHand( string[] p_strRecallIdArr, DateTime[] p_dtmRecallDateArr)
        {
            if (p_strRecallIdArr == null || p_strRecallIdArr.Length == 0 || p_strRecallIdArr.Length != p_dtmRecallDateArr.Length)
                return -1;

            string strSql = @"	update T_EMR_CASERECALL_HAND
								set status_int = 0, lastmodifydate_dat = getdate()
								where recallid_int = ? and recalldate_dat = ?  and status_int = 1";
            long lngEff = 0;
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                int intRecallIdLength = p_strRecallIdArr.Length;
                for (int i = 0; i < intRecallIdLength; i++)
                {
                    objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                    objDPArr[0].Value = p_strRecallIdArr[i];
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = p_dtmRecallDateArr[i];

                    objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
                    lngRes += lngEff;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                objLogger.LogError(objEx);
                return 0;
            }
            return lngRes;
        }
        /// <summary>
        /// 取消回收(根据住院登记号和回收时间)返回影响行数
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_dtmRecallDate"></param>
        /// <returns>返回影响行数</returns>
        [AutoComplete]
        public long m_lngCancelRecallValue( string[] p_strRegisterIdArr, DateTime[] p_dtmRecallDateArr)
        {
            if (p_strRegisterIdArr == null || p_strRegisterIdArr.Length == 0 || p_strRegisterIdArr.Length != p_dtmRecallDateArr.Length)
                return -1;
            string strSql = @"	update t_emr_caserecall
								set status_int = 0, lastmodifydate_dat = ?
								where registerid_chr = ? and recalldate_dat = ? and status_int = 1";
            long lngEff = 0;
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                for (int i = 0; i < p_strRegisterIdArr.Length; i++)
                {
                    objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                    objDPArr[0].DbType = DbType.DateTime;
                    objDPArr[0].Value = DateTime.Now;
                    objDPArr[1].Value = p_strRegisterIdArr[i];
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = p_dtmRecallDateArr[i];

                    objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
                    lngRes += lngEff;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return 0;
            }
            return lngRes;
        }


        /// <summary>
        /// 重新回收
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngResetRecallValue(  clsRecallValues[] p_objArr)
        {
            if (p_objArr == null || p_objArr.Length == 0)
                return 0;
            string strSql = @"	update t_emr_caserecall
								set inpatientid_vchr   = ?,
								outdate_dat        = ?,
								deptid_chr         = ?,
								DEPTNAME_VCHR       = ?,
								patientname_vchr   = ?,
								doctorid_chr       = ?,
								doctorname_vchr    = ?,
								recalldate_dat     = ?,
								overduedays_int    = ?,
								status_int         = ?,
								operator_chr       = ?,
								lastmodifydate_dat = ?,
								inpatienttimes_int = ?,
								areaid_chr = ?,
								AREANAME_VCHR = ?
								where recallid_int = ?";//15
            long lngEff = 0;
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                for (int i = 0; i < p_objArr.Length; i++)
                {
                    if (p_objArr[i] != null)
                    {
                        objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
                        objHRPServ.CreateDatabaseParameter(16, out objDPArr);
                        objDPArr[0].Value = p_objArr[i].m_strInPatientID;
                        objDPArr[1].DbType = DbType.DateTime;
                        objDPArr[1].Value = p_objArr[i].m_dtmOutDate;
                        objDPArr[2].Value = p_objArr[i].m_strDeptID;
                        objDPArr[3].Value = p_objArr[i].m_strDeptName;
                        objDPArr[4].Value = p_objArr[i].m_strPatientName;
                        objDPArr[5].Value = p_objArr[i].m_strDoctorID;
                        objDPArr[6].Value = p_objArr[i].m_strDoctorName;
                        objDPArr[7].DbType = DbType.DateTime;
                        objDPArr[7].Value = p_objArr[i].m_dtmRecallDate;
                        objDPArr[8].Value = p_objArr[i].m_strOverDueDays;
                        objDPArr[9].Value = p_objArr[i].m_strStatus;
                        objDPArr[10].Value = p_objArr[i].m_strOpratorId;
                        objDPArr[11].DbType = DbType.DateTime;
                        objDPArr[11].Value = DateTime.Now;
                        objDPArr[12].Value = p_objArr[i].m_strInTimes;
                        objDPArr[13].Value = p_objArr[i].m_strAreaID;
                        objDPArr[14].Value = p_objArr[i].m_strAreaName;
                        objDPArr[15].Value = p_objArr[i].m_strRecallID;

                        lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
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


        /// <summary>
        /// 根据科室ID和住院号查询病人
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeptId"></param>
        /// <param name="p_strInpatientId"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientByInId(  string p_strDeptId, string p_strInpatientId, out clsRecallValues[] p_objRecallValues)
        {
            p_objRecallValues = null;
            if (p_strDeptId == null || p_strDeptId == "" || p_strInpatientId == null || p_strInpatientId == "")
                return -1;
            string strSql = @"select pa.lastname_vchr,
       re.inpatientcount_int,
       re.inpatientid_chr,
       re.deptid_chr,
		re.areaid_chr,
       de.deptname_vchr deptname,
       de2.deptname_vchr areaname,
       be.code_chr,
       le.modify_dat outdate,
       c.recalldate_dat,
       le.type_int,
       c.overduedays_int,
       re.registerid_chr,
       c.recallid_int,
       c.status_int
  from t_bse_patient pa
 inner join t_opr_bih_register re on pa.patientid_chr = re.patientid_chr and re.status_int !='-1'
 inner join t_opr_bih_leave le on re.registerid_chr = le.registerid_chr
                              and le.status_int = 1
 left join t_bse_bed be on be.bedid_chr=le.outbedid_chr and be.status_int >= 1
  left join t_emr_caserecall c on re.registerid_chr = c.registerid_chr  and c.status_int = 1
 inner join t_bse_deptdesc de on de.deptid_chr = re.deptid_chr
 inner join t_bse_deptdesc de2 on de2.deptid_chr = re.areaid_chr
 where re.deptid_chr =?
   and re.inpatientid_chr = ? 
order by le.modify_dat desc";//2
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                //				objDPArr[0].Value = p_strInpatientId;
                objDPArr[0].Value = p_strDeptId;
                objDPArr[1].Value = p_strInpatientId;
                //				objDPArr[3].Value = p_strDeptId;
                //				objDPArr[4].Value = p_strInpatientId;

                DataTable dtbResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbResult, objDPArr);
                int intRowCount = dtbResult.Rows.Count;
                if (lngRes > 0 && intRowCount > 0)
                {
                    p_objRecallValues = m_objGetData(dtbResult);
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
        /// 根据住院号查询病人(包括了手工回收表中记录的查询)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeptId"></param>
        /// <param name="p_strInpatientId"></param>
        /// <param name="p_objRecallValues"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientByInPatientId( string p_strInpatientId,DateTime m_dtStart,DateTime m_dtEnd, out clsRecallValues[] p_objRecallValues)
        {
            p_objRecallValues = null;
            if (p_strInpatientId == null || p_strInpatientId == "")
                return -1;
            //-------------------------------------------------------------
            // SQL语句说明: 此语句将查询出已出院的病历记录，包含了已回收和未回收的纪录；
            //				其中，已回收记录包括正常回收和手工回收的。WQF 2007-3-2
            //-------------------------------------------------------------
            string strSql = @"	select pa.lastname_vchr,
								re.inpatientcount_int,
								re.inpatientid_chr,
								re.deptid_chr,
								re.areaid_chr,
								de.deptname_vchr deptname,
								de2.deptname_vchr areaname,
								be.code_chr,
								le.outhospital_dat outdate,
								c.recalldate_dat,
								le.type_int,
								c.overduedays_int,
								re.registerid_chr,
								c.recallid_int,
								c.status_int,
								0 normalstatus
								from t_bse_patient pa
								inner join t_opr_bih_register re on pa.patientid_chr = re.patientid_chr and re.status_int !='-1'
								inner join t_opr_bih_leave le on re.registerid_chr = le.registerid_chr 
                                                              and le.status_int = 1
                                                              and le.outhospital_dat between ? and ?
								left join t_bse_bed be on be.bedid_chr=le.outbedid_chr and be.status_int >= 1
								left join t_emr_caserecall c on re.registerid_chr = c.registerid_chr  and c.status_int = 1
								inner join t_bse_deptdesc de on de.deptid_chr = re.deptid_chr
								inner join t_bse_deptdesc de2 on de2.deptid_chr = re.areaid_chr
								where re.inpatientid_chr = ? 
								union 
								select c.patientname_vchr lastname_vchr,
								c.inpatienttimes_int inpatientcount_int,
								c.inpatientid_vchr inpatientid_chr,
								c.deptid_chr,
								c.areaid_chr,
								c.deptname_vchr deptname,
								c.areaname_vchr areaname,
								c.code_chr,
								c.outdate_dat outdate,
								c.recalldate_dat,
								3 type_int,
								c.overduedays_int,
								c.registerid_chr,
								c.recallid_int,
								c.status_int,
								1 normalstatus
								from t_emr_caserecall_hand c
								where c.status_int = 1 and c.inpatientid_vchr = ?
								order by outdate desc";//c.status_int = 1 and 
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = m_dtStart;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = m_dtEnd;
                objDPArr[2].Value = p_strInpatientId;
                objDPArr[3].Value = p_strInpatientId;
                //				objDPArr[2].Value = p_strInpatientId;

                DataTable dtbResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbResult, objDPArr);
                int intRowCount = dtbResult.Rows.Count;
                if (lngRes > 0 && intRowCount > 0)
                {
                    p_objRecallValues = m_objGetData(dtbResult);
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
        /// 根据科室ID和病人姓名查询病人
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeptId"></param>
        /// <param name="p_strInpatientName"></param>
        /// <param name="p_objRecallValues"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientByDeptAndPatientName(  string p_strDeptId, string p_strInpatientName, out clsRecallValues[] p_objRecallValues)
        {
            p_objRecallValues = null;
            if (p_strDeptId == null || p_strDeptId == "" || p_strInpatientName == null || p_strInpatientName == "")
                return -1;
            string strSql = @"select pa.lastname_vchr,
       re.inpatientcount_int,
       re.inpatientid_chr,
       re.deptid_chr,
		re.areaid_chr,
       de.deptname_vchr deptname,
       de2.deptname_vchr areaname,
      be.code_chr,
       le.modify_dat outdate,
       c.recalldate_dat,
       le.type_int,
       c.overduedays_int,
       re.registerid_chr,
       c.recallid_int,
       c.status_int
  from t_bse_patient pa
 inner join t_opr_bih_register re on pa.patientid_chr = re.patientid_chr and re.status_int !='-1'
 inner join t_opr_bih_leave le on re.registerid_chr = le.registerid_chr
                              and le.status_int = 1
  left join t_bse_bed be on and be.bedid_chr=le.outbedid_chr and be.status_int >= 1
  left join t_emr_caserecall c on re.registerid_chr = c.registerid_chr  and c.status_int = 1
 inner join t_bse_deptdesc de on de.deptid_chr = re.deptid_chr
 inner join t_bse_deptdesc de2 on de2.deptid_chr = re.areaid_chr
 where re.deptid_chr =?
   and pa.lastname_vchr = ? order by le.modify_dat desc";//2
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strDeptId;
                objDPArr[1].Value = p_strInpatientName;
                //				objDPArr[2].Value = p_strDeptId;
                //				objDPArr[3].Value = p_strInpatientName;

                DataTable dtbResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbResult, objDPArr);
                int intRowCount = dtbResult.Rows.Count;
                if (lngRes > 0 && intRowCount > 0)
                {
                    p_objRecallValues = m_objGetData(dtbResult);
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
        /// 根据病人姓名查询病人(包含了手工回收的查询)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInpatientName"></param>
        /// <param name="p_objRecallValues"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientByPatientName(  string p_strInpatientName, out clsRecallValues[] p_objRecallValues)
        {
            p_objRecallValues = null;
            if (p_strInpatientName == null || p_strInpatientName == "")
                return -1;
            //-------------------------------------------------------------
            // SQL语句说明: 此语句将查询出已出院的病历记录，包含了已回收和未回收的纪录；
            //				其中，已回收记录包括正常回收和手工回收的。WQF 2007-3-2
            //-------------------------------------------------------------
            string strSql = @"	select pa.lastname_vchr,
								re.inpatientcount_int,
								re.inpatientid_chr,
								re.deptid_chr,
								re.areaid_chr,
								de.deptname_vchr deptname,
								de2.deptname_vchr areaname,
								be.code_chr,
								le.outhospital_dat outdate,
								c.recalldate_dat,
								le.type_int,
								c.overduedays_int,
								re.registerid_chr,
								c.recallid_int,
								c.status_int,
								0 normalstatus
								from t_bse_patient pa
								inner join t_opr_bih_register re on pa.patientid_chr = re.patientid_chr and re.status_int !='-1'
								inner join t_opr_bih_leave le on re.registerid_chr = le.registerid_chr and le.status_int = 1
								left join t_bse_bed be on be.bedid_chr=le.outbedid_chr and be.status_int >= 1
								left join t_emr_caserecall c on re.registerid_chr = c.registerid_chr  and c.status_int = 1
								inner join t_bse_deptdesc de on de.deptid_chr = re.deptid_chr
								inner join t_bse_deptdesc de2 on de2.deptid_chr = re.areaid_chr
								where pa.lastname_vchr = ? 
								union 
								select c.patientname_vchr lastname_vchr,
								c.inpatienttimes_int inpatientcount_int,
								c.inpatientid_vchr inpatientid_chr,
								c.deptid_chr,
								c.areaid_chr,
								c.deptname_vchr deptname,
								c.areaname_vchr areaname,
								c.code_chr,
								c.outdate_dat outdate,
								c.recalldate_dat,
								3 type_int,
								c.overduedays_int,
								c.registerid_chr,
								c.recallid_int,
								c.status_int,
								1 normalstatus
								from t_emr_caserecall_hand c
								where c.status_int = 1 and c.status_int = 1 and c.patientname_vchr = ? 
								order by outdate desc";//c.status_int = 1 and 
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInpatientName;
                objDPArr[1].Value = p_strInpatientName;

                DataTable dtbResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbResult, objDPArr);
                int intRowCount = dtbResult.Rows.Count;
                if (lngRes > 0 && intRowCount > 0)
                {
                    p_objRecallValues = m_objGetData(dtbResult);
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
        /// 根据科室ID或者病区ID和出院日期段查询病人(包含了手工回收的查询)
        /// WQF 2007-3-1
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strId"></param>
        /// <param name="p_dtmStart"></param>
        /// <param name="p_dtmEnd"></param>
        /// <param name="m_strAttributeId">0000002=科室；00000003＝病区</param>
        /// <param name="p_objRecallValues"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientByDeptAndOutDate(  string p_strId, DateTime p_dtmStart, DateTime p_dtmEnd, string m_strAttributeId, out clsRecallValues[] p_objRecallValues)
        {
            p_objRecallValues = null;
            if (p_strId == null || p_strId == "")
                return -1;

            //-------------------------------------------------------------
            // SQL语句说明: 此语句将查询出已出院的病历记录，包含了已回收和未回收的纪录；
            //				其中，已回收记录包括正常回收和手工回收的。WQF 2007-3-2
            //-------------------------------------------------------------
            string strSql = @"	select distinct pa.lastname_vchr,
								re.inpatientcount_int,
								re.inpatientid_chr,
								re.deptid_chr,
								re.areaid_chr,
								de.deptname_vchr deptname,
								de2.deptname_vchr areaname,
								be.code_chr,
								le.outhospital_dat outdate,
								c.recalldate_dat,
								le.type_int,
								c.overduedays_int,
								re.registerid_chr,
								c.recallid_int,
								c.status_int,
								0 normalstatus
								from t_bse_patient pa
								inner join t_opr_bih_register re on pa.patientid_chr = re.patientid_chr and re.status_int !='-1'
								inner join t_opr_bih_leave le on re.registerid_chr = le.registerid_chr and le.status_int = 1
								left join t_bse_bed be on be.bedid_chr=le.outbedid_chr and be.status_int >= 1
								left join t_emr_caserecall c on re.registerid_chr = c.registerid_chr and c.status_int = 1
								inner join t_bse_deptdesc de on de.deptid_chr = re.deptid_chr
								inner join t_bse_deptdesc de2 on de2.deptid_chr = re.areaid_chr
								where re.[ID] = ?
								and le.outhospital_dat between ? and ?
								union 
								select c.patientname_vchr lastname_vchr,
								c.inpatienttimes_int inpatientcount_int,
								c.inpatientid_vchr inpatientid_chr,
								c.deptid_chr,
								c.areaid_chr,
								c.deptname_vchr deptname,
								c.areaname_vchr areaname,
								c.code_chr,
								c.outdate_dat outdate,
								c.recalldate_dat,
								3 type_int,
								c.overduedays_int,
								c.registerid_chr,
								c.recallid_int,
								c.status_int,
								1 normalstatus
								from t_emr_caserecall_hand c
								where c.[ID] = ? and c.status_int = 1
								and c.outdate_dat between  ? and ?
								order by outdate desc";// and c.status_int = 1

            if (m_strAttributeId == "0000003")
                strSql = strSql.Replace("[ID]", "areaid_chr");
            else
                strSql = strSql.Replace("[ID]", "deptid_chr");
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                objDPArr[0].Value = p_strId;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmStart;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_dtmEnd;

                objDPArr[3].Value = p_strId;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = p_dtmStart;
                objDPArr[5].DbType = DbType.DateTime;
                objDPArr[5].Value = p_dtmEnd;

                DataTable dtbResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbResult, objDPArr);
                int intRowCount = dtbResult.Rows.Count;
                if (lngRes > 0 && intRowCount > 0)
                {
                    p_objRecallValues = m_objGetData(dtbResult);
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
        /// 
        /// </summary>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        private clsRecallValues[] m_objGetData(DataTable p_dtbResult)
        {
            clsRecallValues[] objRecallValues = new clsRecallValues[p_dtbResult.Rows.Count];
            DataRow objRow = null;
            try
            {
                DateTime dtmMinDate = DateTime.MaxValue;
                DateTime dtmMaxDate = DateTime.MinValue;
                for (int i = 0; i < p_dtbResult.Rows.Count; i++)
                {
                    objRow = p_dtbResult.Rows[i];
                    clsRecallValues objRecallValue = new clsRecallValues();
                    objRecallValue.m_strRegidterID = objRow["registerid_chr"].ToString().Trim();
                    objRecallValue.m_strPatientName = objRow["lastname_vchr"].ToString();
                    objRecallValue.m_strInTimes = objRow["inpatientcount_int"].ToString();
                    objRecallValue.m_strInPatientID = objRow["inpatientid_chr"].ToString().Trim();
                    objRecallValue.m_strDeptID = objRow["deptid_chr"].ToString();
                    objRecallValue.m_strDeptName = objRow["deptname"].ToString();
                    objRecallValue.m_strAreaID = objRow["areaid_chr"].ToString();
                    objRecallValue.m_strAreaName = objRow["areaname"].ToString();
                    objRecallValue.m_strBedName = objRow["CODE_CHR"].ToString();
                    objRecallValue.m_strType = objRow["type_int"].ToString();//此处赋值必须在objRecallValue.m_dtmOutDate赋值之前
                    try
                    {
                        objRecallValue.m_dtmOutDate = DateTime.Parse(objRow["outdate"].ToString());
                        if (dtmMinDate > objRecallValue.m_dtmOutDate) dtmMinDate = objRecallValue.m_dtmOutDate;
                        if (dtmMaxDate < objRecallValue.m_dtmOutDate) dtmMaxDate = objRecallValue.m_dtmOutDate;
                    }
                    catch { objRecallValue.m_dtmOutDate = new DateTime(1900, 1, 1); }
                    try
                    {
                        objRecallValue.m_dtmRecallDate = DateTime.Parse(objRow["recalldate_dat"].ToString());
                    }
                    catch { objRecallValue.m_dtmRecallDate = new DateTime(1900, 1, 1); }
                    objRecallValue.m_strOverDueDays = objRow["overduedays_int"].ToString();
                    objRecallValue.m_strRecallID = objRow["recallid_int"].ToString();
                    objRecallValue.m_strStatus = objRow["status_int"].ToString();
                    if (p_dtbResult.Columns.Contains("Normalstatus"))
                    {
                        objRecallValue.m_strNormalstatus = objRow["Normalstatus"].ToString();
                    }

                    objRecallValues[i] = objRecallValue;
                }
                dtmMaxDate = dtmMaxDate.AddMonths(2);
                DateTime[] dtmArr = m_objGetHoliday(  dtmMinDate, dtmMaxDate);
                if (dtmArr != null && dtmArr.Length > 0 && objRecallValues.Length > 0)
                {
                    for (int j = 0; j < objRecallValues.Length; j++)
                    {
                        objRecallValues[j].m_dtExcludeHolidayArr = dtmArr;
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return null;
            }
            return objRecallValues;
        }
        #endregion  病历回收

        #region 病案检索
        /// <summary>
        /// 病案检索
        /// WQF	2007-02-28
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strId"></param>
        /// <param name="p_strAttributeId"></param>
        /// <param name="p_dtmStart"></param>
        /// <param name="p_dtmEnd"></param>
        /// <param name="p_blnIsOut"></param>
        /// <param name="blnIsPastDue"></param>
        /// <param name="p_objRecallValues"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetIsRecallFindValues( string p_strId, string p_strAttributeId, clsReCallFindParimiter p_objReCallPaValue, out clsRecallFindValues[] p_objRecallValues)
        {
            p_objRecallValues = null;
            int m_intCount = 1;
            int m_intCountP = 1;
            string[] strAllArr = null;
            List<string> strFristDialogOnelis = new List<string>();
            List<string> strFristDialogTwolis = new List<string>();
            List<string> strDialogOnelis = new List<string>();
            List<string> strDialogTwolis = new List<string>();
            List<string> strOprOnelis = new List<string>();
            List<string> strOprTwolis = new List<string>();
            List<string> strInfectionIcdOnelis = new List<string>();
            List<string> strInfectionIcdTwolis = new List<string>();
            if (p_objReCallPaValue == null)
                return -1;

            string strSql = @"	select distinct pa.lastname_vchr,
                                pa.birth_dat,
                                pa.sex_chr,
                                pa.nativeplace_vchr,
								re.inpatientcount_int,
								re.inpatientid_chr,
								re.deptid_chr,
								re.areaid_chr,
                                re.inpatient_dat,
								de.deptname_vchr deptname,
								be.code_chr,
								le.outhospital_dat outdate,
								le.type_int,
								re.registerid_chr,
                                inrc.status,
                                inrc.icd_10ofmain,
                                inrc.modeofpayment,
                                inrc.chuyuanfangshi,
                                inrc.mainconditionseq,
                                inrc.maindiagnosis,
                                inrc.totalamt,
                                inrc.lastmodifydate,
                                (select count(inrcop.operationdate) from inhospitalmainrecord_operation inrcop where re.registerid_chr = inrcop.registerid_chr
 and inrcop.status = 1) operationcount
                                from t_bse_patient pa
								inner join t_opr_bih_register re on pa.patientid_chr = re.patientid_chr
								inner join t_opr_bih_leave le on re.registerid_chr = le.registerid_chr and le.status_int = 1
								left join t_bse_bed be on be.bedid_chr=le.outbedid_chr and be.status_int >= 1
								inner join t_bse_deptdesc de on de.deptid_chr = re.deptid_chr
                                left join inhospitalmainrecord_content inrc on re.registerid_chr = inrc.registerid_chr and inrc.status = 1 ";
            if (p_objReCallPaValue.m_blnOprCode)
            {
                strSql += "inner join inhospitalmainrecord_operation inrop on inrc.registerid_chr = inrop.registerid_chr ";
            }
            if (p_objReCallPaValue.m_blnDialogCode && !p_objReCallPaValue.m_blnIsFirstDialoge)
            {
                strSql += @"inner join inhospitalmainrecord_diagnosis inrcdia on inrcdia.registerid_chr =re.registerid_chr
  and inrcdia.status =1 ";
            }
            //部门ID
            if (!p_objReCallPaValue.m_blnIsAllDept)
            {
                m_intCount = 1;
                strSql += @"where re.deptid_chr = ? and (inrc.lastmodifydate =
                                                 (select max(t.lastmodifydate)
                                                    from inhospitalmainrecord_content t
                                                   where inrc.registerid_chr = t.registerid_chr
                                                   and t.status = 1
                                                   group by t.registerid_chr)or inrc.lastmodifydate is null)";
            }
            else
            {
                m_intCount = 0;
                strSql += @"where re.deptid_chr is not null  and (inrc.lastmodifydate =
                                                 (select max(t.lastmodifydate)
                                                    from inhospitalmainrecord_content t
                                                   where inrc.registerid_chr = t.registerid_chr
                                                   and t.status = 1
                                                   group by t.registerid_chr)or inrc.lastmodifydate is null)";
            }
            //出院日期
            if (p_objReCallPaValue.m_blnIsOut)
            {
                m_intCount += 2;
                strSql += "and le.outhospital_dat between ? and ? ";
            }
            //入院日期
            if (p_objReCallPaValue.m_blnIsInPatienDt)
            {
                m_intCount += 2;
                strSql += "and re.inpatient_dat between ? and ? ";
            }
            //出身日期
            if (p_objReCallPaValue.m_blnBrithDate)
            {
                m_intCount += 2;
                strSql += "and pa.birth_dat between ? and ?";
            }
            //性别
            if (!string.IsNullOrEmpty(p_objReCallPaValue.m_strSex))
            {
                m_intCount += 1;
                strSql += "and pa.sex_chr = ?";
            }
            //出院情况
            if (null != p_objReCallPaValue.m_strOutPatient && -1 != p_objReCallPaValue.m_strOutPatient && 0 != p_objReCallPaValue.m_strOutPatient)
            {
                m_intCount += 1;
                strSql += "and le.type_int = ? ";
            }
            //付款方式
            if (!string.IsNullOrEmpty(p_objReCallPaValue.m_strPayKind))
            {
                m_intCount += 1;
                strSql += "and inrc.modeofpayment like ?";
            }
            if (p_objReCallPaValue.m_blnDialogCode)
            {
                //第一诊断诊断ICD范围
                if (p_objReCallPaValue.m_blnIsFirstDialoge)
                {
                    p_objReCallPaValue.m_strDialogCodeStart = p_objReCallPaValue.m_strDialogCodeStart.Replace("，", ",");
                    strAllArr = p_objReCallPaValue.m_strDialogCodeStart.Split(',');
                    for (int i = 0; i < strAllArr.Length; i++)
                    {
                        if (strAllArr[i].Contains("-"))
                        {
                            strFristDialogTwolis.Add(strAllArr[i].Trim());
                        }
                        else
                        {
                            strFristDialogOnelis.Add(strAllArr[i].Trim());
                        }
                    }
                    strAllArr = null;

                    m_intCount += strFristDialogTwolis.Count * 2 + strFristDialogOnelis.Count;
                    if (m_intCount > 0)
                    {
                        strSql += "and (";
                    }
                    string strSql1 = "";
                    for (int l1 = 0; l1 < strFristDialogOnelis.Count; l1++)
                    {
                        if (l1 == 0)
                        {
                            strSql1 += " inrc.icd_10ofmain = ? ";
                        }
                        else
                        {
                            strSql1 += " or inrc.icd_10ofmain = ? ";
                        }
                    }
                    for (int j1 = 0; j1 < strFristDialogTwolis.Count; j1++)
                    {
                        if (strFristDialogOnelis.Count == 0 && j1 == 0)
                        {
                            strSql1 += " inrc.icd_10ofmain between ? and ?";
                        }
                        else
                        {
                            strSql1 += " or inrc.icd_10ofmain between ? and ?";
                        }
                    }
                    strSql += strSql1 + " )";
                    strSql1 = "";
                    //strSql += "and  inrc.icd_10ofmain between ? and ?";
                }
                else
                {
                    p_objReCallPaValue.m_strDialogCodeStart = p_objReCallPaValue.m_strDialogCodeStart.Replace("，", ",");
                    strAllArr = p_objReCallPaValue.m_strDialogCodeStart.Split(',');
                    for (int i = 0; i < strAllArr.Length; i++)
                    {
                        if (strAllArr[i].Contains("-"))
                        {
                            strDialogTwolis.Add(strAllArr[i].Trim());
                        }
                        else
                        {
                            strDialogOnelis.Add(strAllArr[i].Trim());
                        }
                    }
                    strAllArr = null;

                    m_intCount += strDialogTwolis.Count * 6 + strDialogOnelis.Count * 3;
                    if (m_intCount > 0)
                    {
                        strSql += "and (";
                    }
                    string strSql2 = "";
                    for (int l1 = 0; l1 < strDialogOnelis.Count; l1++)
                    {
                        if (l1 == 0)
                        {
                            strSql2 += @" (inrc.icd_10ofmain = ? 
or inrc.icdofsubsidiarydiagnosis= ? 
or inrcdia.icd10 = ? )";
                        }
                        else
                        {
                            strSql2 += @" or (inrc.icd_10ofmain = ? 
or inrc.icdofsubsidiarydiagnosis= ? 
or inrcdia.icd10 = ? )";
                        }
                    }
                    for (int j1 = 0; j1 < strDialogTwolis.Count; j1++)
                    {
                        if (strDialogOnelis.Count == 0 && j1 == 0)
                        {
                            strSql2 += @"(inrc.icd_10ofmain between ? and ? 
or inrc.icdofsubsidiarydiagnosis between ? and ? 
or inrcdia.icd10 between ? and ? )";
                        }
                        else
                        {
                            strSql2 += @"or (inrc.icd_10ofmain between ? and ? 
or inrc.icdofsubsidiarydiagnosis between ? and ? 
or inrcdia.icd10 between ? and ? )";
                        }
                    }
                    strSql += strSql2 + " )";
                    strSql2 = "";
                    //诊断ICD范围
                    //                    m_intCount += 12;
                    //                    strSql +=@"and (inrc.icd_10ofmain between ? and ? 
                    //or inrc.icdofsubsidiarydiagnosis between ? and ? 
                    //or inrc.zhuzhengicd between ? and ? 
                    //or inrc.inhospitaldiagnosis10 between ? and ? 
                    //or inrc.inhospitaldiagnosiszhong10 between ? and ?
                    //or inrcdia.icd10 between ? and ? )";
                }
            }
            //手术编码范围
            if (p_objReCallPaValue.m_blnOprCode)
            {
                p_objReCallPaValue.m_strOprCodeStart = p_objReCallPaValue.m_strOprCodeStart.Replace("，", ",");
                strAllArr = p_objReCallPaValue.m_strOprCodeStart.Split(',');
                for (int i = 0; i < strAllArr.Length; i++)
                {
                    if (strAllArr[i].Contains("-"))
                    {
                        strOprTwolis.Add(strAllArr[i].Trim());
                    }
                    else
                    {
                        strOprOnelis.Add(strAllArr[i].Trim());
                    }
                }
                strAllArr = null;

                m_intCount += strOprTwolis.Count * 2 + strOprOnelis.Count;
                if (m_intCount > 0)
                {
                    strSql += "and (";
                }
                string strSql3 = "";
                for (int l1 = 0; l1 < strOprOnelis.Count; l1++)
                {
                    if (l1 == 0)
                    {
                        strSql3 += " inrop.operationid = ? ";
                    }
                    else
                    {
                        strSql3 += " or inrop.operationid = ? ";
                    }
                }
                for (int j1 = 0; j1 < strOprTwolis.Count; j1++)
                {
                    if (strOprOnelis.Count == 0 && j1 == 0)
                    {
                        strSql3 += " inrop.operationid between ? and ?";
                    }
                    else
                    {
                        strSql3 += " or inrop.operationid between ? and ?";
                    }
                }
                strSql += strSql3 + " )";
                strSql3 = "";
                //m_intCount += 2;
                //strSql += "and inrop.operationid between ? and ?";
            }
            string[] m_strArr = null;
            string m_strPar = "";
            //传染病种类按名称
            if (p_objReCallPaValue.m_blnInfection)
            {
                if (p_objReCallPaValue.m_blnInfectionText)
                {
                    p_objReCallPaValue.m_strInfection = p_objReCallPaValue.m_strInfection.Replace("，", ",");
                    if (!string.IsNullOrEmpty(p_objReCallPaValue.m_strInfection) && !p_objReCallPaValue.m_strInfection.Contains(","))
                    {
                        m_intCount += 1;
                        strSql += "and inrc.maindiagnosis like ? ";
                        //strSql += "and inrc.maindiagnosis like '%" + p_objReCallPaValue.m_strInfection + "%'";
                    }
                    else if (p_objReCallPaValue.m_strInfection.Contains(",") && !string.IsNullOrEmpty(p_objReCallPaValue.m_strInfection))
                    {

                        m_strArr = p_objReCallPaValue.m_strInfection.Split(',');
                        m_intCount += m_strArr.Length;
                    }
                    if (null != m_strArr)
                    {
                        for (int k = 0; k < m_strArr.Length; k++)
                        {
                            if (k == 0)
                            {
                                m_strPar = "and (";
                            }
                            else
                            {
                                m_strPar = "or ";
                            }
                            if (0 != k && k == m_strArr.Length - 1)
                            {
                                strSql += m_strPar + " inrc.maindiagnosis like ? )";
                            }
                            else
                            {
                                strSql += m_strPar + " inrc.maindiagnosis like ? ";
                            }
                        }
                    }
                    //strSql += "and inrc.maindiagnosis like '%" + p_objReCallPaValue.m_strInfection + "%'";
                }
                //传染病种类按ICD
                else if (p_objReCallPaValue.m_blnInfectionIcd)
                {
                    p_objReCallPaValue.m_strInfectionIcdStart = p_objReCallPaValue.m_strInfectionIcdStart.Replace("，", ",");
                    strAllArr = p_objReCallPaValue.m_strInfectionIcdStart.Split(',');
                    for (int i = 0; i < strAllArr.Length; i++)
                    {
                        if (strAllArr[i].Contains("-"))
                        {
                            strInfectionIcdTwolis.Add(strAllArr[i].Trim());
                        }
                        else
                        {
                            strInfectionIcdOnelis.Add(strAllArr[i].Trim());
                        }
                    }
                    strAllArr = null;

                    m_intCount += strInfectionIcdTwolis.Count * 2 + strInfectionIcdOnelis.Count;
                    if (m_intCount > 0)
                    {
                        strSql += "and (";
                    }
                    string strSql3 = "";
                    for (int l1 = 0; l1 < strInfectionIcdOnelis.Count; l1++)
                    {
                        if (l1 == 0)
                        {
                            strSql3 += " inrc.icd_10ofmain = ? ";
                        }
                        else
                        {
                            strSql3 += " or inrc.icd_10ofmain = ? ";
                        }
                    }
                    for (int j1 = 0; j1 < strInfectionIcdTwolis.Count; j1++)
                    {
                        if (strInfectionIcdOnelis.Count == 0 && j1 == 0)
                        {
                            strSql3 += " inrc.icd_10ofmain between ? and ? ";
                        }
                        else
                        {
                            strSql3 += " or inrc.icd_10ofmain between ? and ? ";
                        }
                    }
                    strSql += strSql3 + " )";
                    strSql3 = "";
                    //m_intCount += 2;
                    //strSql += "and inrc.icd_10ofmain between ? and ? ";
                }
            }


            strSql += "order by outdate desc ";


            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(m_intCount, out objDPArr);

                m_intCountP = 0;

                if (!p_objReCallPaValue.m_blnIsAllDept)
                {
                    objDPArr[0].Value = p_strId;
                    //m_intCountP += 1;
                }
                else
                {
                    m_intCountP = -1;
                }

                if (p_objReCallPaValue.m_blnIsOut)
                {

                    objDPArr[m_intCountP + 1].DbType = DbType.DateTime;
                    objDPArr[m_intCountP + 1].Value = p_objReCallPaValue.m_dtOutStart;
                    objDPArr[m_intCountP + 2].DbType = DbType.DateTime;
                    objDPArr[m_intCountP + 2].Value = p_objReCallPaValue.m_dtOutEnd;
                    m_intCountP += 2;
                }
                if (p_objReCallPaValue.m_blnIsInPatienDt)
                {
                    objDPArr[m_intCountP + 1].DbType = DbType.DateTime;
                    objDPArr[m_intCountP + 1].Value = p_objReCallPaValue.m_dtInPatientStart;
                    objDPArr[m_intCountP + 2].DbType = DbType.DateTime;
                    objDPArr[m_intCountP + 2].Value = p_objReCallPaValue.m_dtInPatientEnd;
                    m_intCountP += 2;
                }
                if (p_objReCallPaValue.m_blnBrithDate)
                {
                    objDPArr[m_intCountP + 1].DbType = DbType.DateTime;
                    objDPArr[m_intCountP + 1].Value = p_objReCallPaValue.m_dtBirthStart;
                    objDPArr[m_intCountP + 2].DbType = DbType.DateTime;
                    objDPArr[m_intCountP + 2].Value = p_objReCallPaValue.m_dtBirthEnd;
                    m_intCountP += 2;
                }
                if (!string.IsNullOrEmpty(p_objReCallPaValue.m_strSex))
                {
                    objDPArr[m_intCountP + 1].Value = p_objReCallPaValue.m_strSex;
                    m_intCountP += 1;
                }
                if (null != p_objReCallPaValue.m_strOutPatient && -1 != p_objReCallPaValue.m_strOutPatient && 0 != p_objReCallPaValue.m_strOutPatient)
                {
                    objDPArr[m_intCountP + 1].DbType = DbType.Int16;
                    objDPArr[m_intCountP + 1].Value = p_objReCallPaValue.m_strOutPatient;
                    m_intCountP += 1;
                }
                if (!string.IsNullOrEmpty(p_objReCallPaValue.m_strPayKind))
                {
                    objDPArr[m_intCountP + 1].Value = p_objReCallPaValue.m_strPayKind;
                    m_intCountP += 1;
                }
                if (p_objReCallPaValue.m_blnDialogCode)
                {
                    //第一诊断诊断ICD范围
                    if (p_objReCallPaValue.m_blnIsFirstDialoge)
                    {

                        for (int k1 = 0; k1 < strFristDialogOnelis.Count; k1++)
                        {
                            objDPArr[m_intCountP + k1 + 1].Value = strFristDialogOnelis[k1];
                        }
                        m_intCountP += strFristDialogOnelis.Count;
                        string[] strArr = null;
                        for (int k2 = 0; k2 < strFristDialogTwolis.Count; k2++)
                        {
                            strArr = strFristDialogTwolis[k2].Split('-');
                            objDPArr[m_intCountP + k2 * 2 + 1].Value = strArr[0].Trim();
                            objDPArr[m_intCountP + k2 * 2 + 2].Value = strArr[1].Trim();
                        }
                        m_intCountP += strFristDialogTwolis.Count * 2;
                        //objDPArr[m_intCountP + 1].Value = p_objReCallPaValue.m_strDialogCodeStart;
                        //objDPArr[m_intCountP + 2].Value = p_objReCallPaValue.m_strDialogCodeEnd;
                        //m_intCountP += 2;
                    }
                    else
                    {
                        for (int k1 = 0; k1 < strDialogOnelis.Count; k1++)
                        {
                            objDPArr[m_intCountP + k1 * 3 + 1].Value = strDialogOnelis[k1];
                            objDPArr[m_intCountP + k1 * 3 + 2].Value = strDialogOnelis[k1];
                            objDPArr[m_intCountP + k1 * 3 + 3].Value = strDialogOnelis[k1];
                        }
                        m_intCountP += strDialogOnelis.Count * 3;
                        string[] strArr = null;
                        for (int k2 = 0; k2 < strDialogTwolis.Count; k2++)
                        {
                            strArr = strDialogTwolis[k2].Split('-');
                            objDPArr[m_intCountP + k2 * 6 + 1].Value = strArr[0].Trim();
                            objDPArr[m_intCountP + k2 * 6 + 2].Value = strArr[1].Trim();
                            objDPArr[m_intCountP + k2 * 6 + 3].Value = strArr[0].Trim();
                            objDPArr[m_intCountP + k2 * 6 + 4].Value = strArr[1].Trim();
                            objDPArr[m_intCountP + k2 * 6 + 5].Value = strArr[0].Trim();
                            objDPArr[m_intCountP + k2 * 6 + 6].Value = strArr[1].Trim();
                        }
                        m_intCountP += strDialogTwolis.Count * 6;
                        //objDPArr[m_intCountP + 1].Value = p_objReCallPaValue.m_strDialogCodeStart;
                        //objDPArr[m_intCountP + 2].Value = p_objReCallPaValue.m_strDialogCodeEnd;
                        //objDPArr[m_intCountP + 3].Value = p_objReCallPaValue.m_strDialogCodeStart;
                        //objDPArr[m_intCountP + 4].Value = p_objReCallPaValue.m_strDialogCodeEnd;
                        //objDPArr[m_intCountP + 5].Value = p_objReCallPaValue.m_strDialogCodeStart;
                        //objDPArr[m_intCountP + 6].Value = p_objReCallPaValue.m_strDialogCodeEnd;
                        //objDPArr[m_intCountP + 7].Value = p_objReCallPaValue.m_strDialogCodeStart;
                        //objDPArr[m_intCountP + 8].Value = p_objReCallPaValue.m_strDialogCodeEnd;
                        //objDPArr[m_intCountP + 9].Value = p_objReCallPaValue.m_strDialogCodeStart;
                        //objDPArr[m_intCountP + 10].Value = p_objReCallPaValue.m_strDialogCodeEnd;
                        //objDPArr[m_intCountP + 11].Value = p_objReCallPaValue.m_strDialogCodeStart;
                        //objDPArr[m_intCountP + 12].Value = p_objReCallPaValue.m_strDialogCodeEnd;
                        //m_intCountP += 12;
                    }
                }
                if (p_objReCallPaValue.m_blnOprCode)
                {
                    for (int k1 = 0; k1 < strOprOnelis.Count; k1++)
                    {
                        objDPArr[m_intCountP + k1 + 1].Value = strOprOnelis[k1];
                    }
                    m_intCountP += strOprOnelis.Count;
                    string[] strArr = null;
                    for (int k2 = 0; k2 < strOprTwolis.Count; k2++)
                    {
                        strArr = strOprTwolis[k2].Split('-');
                        objDPArr[m_intCountP + k2 * 2 + 1].Value = strArr[0].Trim();
                        objDPArr[m_intCountP + k2 * 2 + 2].Value = strArr[1].Trim();
                    }
                    m_intCountP += strOprTwolis.Count * 2;
                    //objDPArr[m_intCountP + 1].Value = p_objReCallPaValue.m_strOprCodeStart;
                    //objDPArr[m_intCountP + 2].Value = p_objReCallPaValue.m_strOprCodeEnd;
                    //m_intCountP += 2;
                }
                if (p_objReCallPaValue.m_blnInfection)
                {
                    if (p_objReCallPaValue.m_blnInfectionText)
                    {
                        string[] strArr = null;
                        p_objReCallPaValue.m_strInfection = p_objReCallPaValue.m_strInfection.Replace("，", ",");
                        if (!string.IsNullOrEmpty(p_objReCallPaValue.m_strInfection) && !p_objReCallPaValue.m_strInfection.Contains(","))
                        {
                            objDPArr[m_intCountP + 1].Value = "%" + p_objReCallPaValue.m_strInfection + "%";
                            m_intCountP += 1;
                        }
                        else if (p_objReCallPaValue.m_strInfection.Contains(",") && !string.IsNullOrEmpty(p_objReCallPaValue.m_strInfection))
                        {

                            strArr = p_objReCallPaValue.m_strInfection.Split(',');

                        }
                        if (null != strArr)
                        {
                            for (int k4 = 0; k4 < strArr.Length; k4++)
                            {
                                objDPArr[m_intCountP + k4 + 1].Value = "%" + strArr[k4] + "%";

                            }
                            m_intCountP += strArr.Length;
                        }
                    }
                }
                if (p_objReCallPaValue.m_blnInfection && p_objReCallPaValue.m_blnInfectionIcd)
                {
                    for (int k1 = 0; k1 < strInfectionIcdOnelis.Count; k1++)
                    {
                        objDPArr[m_intCountP + k1 + 1].Value = strInfectionIcdOnelis[k1];
                    }
                    m_intCountP += strInfectionIcdOnelis.Count;
                    string[] strArr = null;
                    for (int k2 = 0; k2 < strInfectionIcdTwolis.Count; k2++)
                    {
                        strArr = strInfectionIcdTwolis[k2].Split('-');
                        objDPArr[m_intCountP + k2 * 2 + 1].Value = strArr[0].Trim();
                        objDPArr[m_intCountP + k2 * 2 + 2].Value = strArr[1].Trim();
                    }
                    m_intCountP += strInfectionIcdTwolis.Count * 2;
                    //objDPArr[m_intCountP + 1].Value = p_objReCallPaValue.m_strInfectionIcdStart;
                    //objDPArr[m_intCountP + 2].Value = p_objReCallPaValue.m_strInfectionIcdEnd;
                    //m_intCountP += 2;
                }
                if (m_intCountP + 1 != m_intCount)
                {
                    return -1;
                }

                DataTable dtbResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbResult, objDPArr);
                int intRowCount = dtbResult.Rows.Count;
                if (lngRes > 0 && intRowCount > 0)
                {
                    p_objRecallValues = new clsRecallFindValues[intRowCount];
                    DataRow objRow = null;
                    for (int i = 0; i < intRowCount; i++)
                    {
                        objRow = dtbResult.Rows[i];
                        clsRecallFindValues objRecallValue = new clsRecallFindValues();
                        objRecallValue.m_strRegidterID = objRow["REGISTERID_CHR"].ToString().Trim();
                        objRecallValue.m_strInPatientID = objRow["INPATIENTID_CHR"].ToString().Trim();
                        try
                        {
                            objRecallValue.m_dtmOutDate = DateTime.Parse(objRow["OUTDATE"].ToString());
                        }
                        catch { objRecallValue.m_dtmOutDate = new DateTime(1900, 1, 1); }
                        objRecallValue.m_strDeptID = objRow["DEPTID_CHR"].ToString();
                        objRecallValue.m_strPatientName = objRow["LASTNAME_VCHR"].ToString();
                        objRecallValue.m_strAreaID = objRow["AREAID_CHR"].ToString();
                        objRecallValue.m_strSex = objRow["SEX_CHR"].ToString();
                        objRecallValue.m_strStatus = "1";
                        objRecallValue.m_strBedName = objRow["CODE_CHR"].ToString();
                        objRecallValue.m_strDeptName = objRow["DEPTNAME"].ToString();
                        try
                        {
                            objRecallValue.m_dtmBirthDate = DateTime.Parse(objRow["BIRTH_DAT"].ToString());
                        }
                        catch
                        {
                            objRecallValue.m_dtmBirthDate = new DateTime(1900, 1, 1);
                        }
                        objRecallValue.m_strNativeplace = objRow["NATIVEPLACE_VCHR"].ToString();


                        objRecallValue.m_dblTotalMoney = objRow["TOTALAMT"].ToString();
                        try
                        {
                            objRecallValue.m_dtmInPatient_dat = DateTime.Parse(objRow["INPATIENT_DAT"].ToString());
                        }
                        catch
                        {
                            objRecallValue.m_dtmInPatient_dat = new DateTime(1900, 1, 1);
                        }
                        objRecallValue.m_intOprationNum = objRow["OPERATIONCOUNT"].ToString();
                        objRecallValue.m_strMaindiagnosis = objRow["MAINDIAGNOSIS"].ToString();
                        objRecallValue.m_strModeofPayMent = objRow["MODEOFPAYMENT"].ToString();
                        objRecallValue.m_intMainconditionseq = objRow["TYPE_INT"].ToString();

                        p_objRecallValues[i] = objRecallValue;
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

        #region 获取总费用
        /*
        /// <summary>
        /// 获取总费用
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCHRCATE(IPrincipal p_objPrincipal, string p_strRegisterID, out string p_dbTotalMoney)
        {
            p_dbTotalMoney = "";
            if (string.IsNullOrEmpty(p_strRegisterID))
            {
                return -1;
            }
            string strSql = @"select sum(round(a.unitprice_dec * a.amount_dec, 2)) as totalsum
                              from t_opr_bih_patientcharge a
                             where a.status_int = 1
                               and a.pstatus_int <> 0
                               and a.chargeactive_dat is not null
                               and a.registerid_chr = ?";
            long lngRes = 0;
            
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterID.ToString().Trim();

                DataTable dtbResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbResult, objDPArr);
                int intRowCount = dtbResult.Rows.Count;
                if (lngRes > 0 && intRowCount > 0)
                {
                    p_dbTotalMoney = dtbResult.Rows[0]["TOTALSUM"].ToString();
                }

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        */
        #endregion
        #region 获取总费用
        /// <summary>
        /// 获取总费用
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCHRCATE( string p_strRegisterID, out string p_dbTotalMoney)
        {
            p_dbTotalMoney = "";
            if (string.IsNullOrEmpty(p_strRegisterID))
            {
                return -1;
            }
            string strSql = @" select a.unitprice_dec,
                                   a.amount_dec
                              from t_opr_bih_patientcharge a
                             where a.status_int <> 0
                               and a.pstatus_int <> 0
                               and a.chargeactive_dat is not null
                               and a.registerid_chr = ?";
            long lngRes = 0;

            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterID.ToString().Trim();

                DataTable dtbResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbResult, objDPArr);
                int intRowCount = dtbResult.Rows.Count;
                double m_dbMoney = 0;
                if (lngRes > 0 && intRowCount > 0)
                {
                    for (int i = 0; i < intRowCount; i++)
                    {
                        m_dbMoney += Convert.ToDouble(dtbResult.Rows[i]["unitprice_dec"].ToString()) * Convert.ToDouble(dtbResult.Rows[i]["amount_dec"].ToString());
                        m_dbMoney = double.Parse(m_dbMoney.ToString("0.00"));
                    }
                    p_dbTotalMoney = m_dbMoney.ToString("0.00");
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
        #endregion

        #region 屏蔽传染病维护
        /*

        #region 获取传染病文本信息
        /// <summary>
        /// 查询传染病所有文字信息
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInfectoin(ref List<clsEMR_InfectoinTickUpTextValue> m_lstInfectionText, ref List<clsEMR_InfectoinTickUpICDValue> m_lstInfectionICD)
        {
            m_lstInfectionText = null;
            m_lstInfectionICD = null;
            string strSql = @"select t.itemid, t.typeid, t.itemcontent
  from t_emr_infectionstickup t";
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(0, out objDPArr);

                DataTable dtbResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbResult, objDPArr);
                int intRowCount = dtbResult.Rows.Count;
                clsEMR_InfectoinTickUpTextValue m_objInfectionText = null;
                clsEMR_InfectoinTickUpICDValue m_objInfectionICD = null;
                if (lngRes > 0 && intRowCount > 0)
                {
                    m_lstInfectionText = new List<clsEMR_InfectoinTickUpTextValue>();
                    m_lstInfectionICD = new List<clsEMR_InfectoinTickUpICDValue>();
                    DataRow dataR = null;
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        dataR = dtbResult.Rows[i];
                        if (dataR["TYPEID"].ToString().Trim() == "0")
                        {
                            m_objInfectionText = new clsEMR_InfectoinTickUpTextValue();
                            m_objInfectionText.m_intTxtITEMID = dataR["ITEMID"].ToString();
                            m_objInfectionText.m_strTxtTYPEID = dataR["TYPEID"].ToString();
                            m_objInfectionText.m_strTxtITEMCONTENT = dataR["ITEMCONTENT"].ToString();
                            m_lstInfectionText.Add(m_objInfectionText);
                        }
                        else if (dataR["TYPEID"].ToString().Trim() == "1")
                        {
                            m_objInfectionICD = new clsEMR_InfectoinTickUpICDValue();
                            m_objInfectionICD.m_intIcdITEMID = dataR["ITEMID"].ToString();
                            m_objInfectionICD.m_strIcdTYPEID = dataR["TYPEID"].ToString();
                            m_objInfectionICD.m_strIcdITEMCONTENT = dataR["ITEMCONTENT"].ToString();
                            m_lstInfectionICD.Add(m_objInfectionICD);
                        }
                    }

                }
                if (m_lstInfectionText == null && m_lstInfectionICD == null)
                {
                    return -1;
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

        #region 添加传染病文本记录
        /// <summary>
        /// 添加传染病文本记录
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngTextSave(clsEMR_InfectoinTickUpTextValue m_objInfectionText)
        {
            if (m_objInfectionText == null)
            {
                return -1;
            }
            string strSql = @"insert into t_emr_infectionstickup t
                                (t.itemid, t.typeid, t.itemcontent)
                              values
                                (?, ?, ?)";

            long lngRes = 0;
            long lngInfectionID = 0;
            com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
            lngRes = objSign.m_lngGetRecallids("SEQ_INFECTIONID", 1, out lngInfectionID);

            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = lngInfectionID.ToString().Trim();
                objDPArr[1].Value = m_objInfectionText.m_strTxtTYPEID.Trim();
                objDPArr[2].Value = m_objInfectionText.m_strTxtITEMCONTENT.Trim();
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngRes, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 添加传染病ICD记录
        /// <summary>
        /// 添加传染病ICD记录
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngICDSave(clsEMR_InfectoinTickUpICDValue m_objInfectionICD)
        {
            if (m_objInfectionICD == null)
            {
                return -1;
            }
            string strSql = @"insert into t_emr_infectionstickup t
                                (t.itemid, t.typeid, t.itemcontent)
                              values
                                (?, ?, ?)";

            long lngRes = 0;
            long lngInfectionID = 0;
            com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
            lngRes = objSign.m_lngGetRecallids("SEQ_INFECTIONID", 1, out lngInfectionID);

            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = lngInfectionID.ToString().Trim();
                objDPArr[1].Value = m_objInfectionICD.m_strIcdTYPEID.Trim();
                objDPArr[2].Value = m_objInfectionICD.m_strIcdITEMCONTENT.Trim();
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngRes, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 删除传染病文本记录
        /// <summary>
        /// 删除传染病文本记录
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngTextDelete(clsEMR_InfectoinTickUpTextValue m_objInfectionText)
        {
            if (m_objInfectionText == null)
            {
                return -1;
            }
            string strSql = @"delete from t_emr_infectionstickup t where t.itemid = ?";

            long lngRes = 0;

            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = m_objInfectionText.m_intTxtITEMID.Trim();
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngRes, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 删除传染病ICD记录
        /// <summary>
        /// 删除传染病ICD记录
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngICDDelete(clsEMR_InfectoinTickUpICDValue m_objInfectionICD)
        {
            if (m_objInfectionICD == null)
            {
                return -1;
            }
            string strSql = @"delete from t_emr_infectionstickup t where t.itemid = ?";

            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = m_objInfectionICD.m_intIcdITEMID.Trim();
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngRes, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        */
        #endregion

        #region 已回收病历查询
        /// <summary>
        /// 按科室或者病区查询某段回收时间的已回收病历
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strId"></param>
        /// <param name="m_strAttributeId"></param>
        /// <param name="p_dtmStart"></param>
        /// <param name="p_dtmEnd"></param>
        /// <param name="p_blnIsOut">true=按出院日期查；false＝按回收日期查</param>
        /// <param name="p_objRecallValues"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetIsRecallValues(  string p_strId, string p_strAttributeId, DateTime p_dtmStart, DateTime p_dtmEnd, bool p_blnIsOut, out clsRecallValues[] p_objRecallValues)
        {
            p_objRecallValues = null;
            if (p_strId == null || p_strId == "")
                return -1;
            string strSql = @"	select c.recallid_int,
								c.registerid_chr,
								c.inpatientid_vchr,
								c.outdate_dat,
								c.deptid_chr,
								c.areaid_chr,
								c.areaname_vchr,
								c.patientname_vchr,
								c.doctorid_chr,
								c.recalldate_dat,
								c.overduedays_int,
								c.status_int,
								c.operator_chr,
								c.operationdate_dat,
								c.lastmodifydate_dat,
								c.doctorname_vchr,
								c.deptname_vchr,
								c.inpatienttimes_int,
								be.code_chr,
								0 normalstatus
								from t_emr_caserecall c
								inner join t_opr_bih_leave le on c.registerid_chr = le.registerid_chr
								and le.status_int = 1
								left join t_bse_bed be on be.bedid_chr=le.outbedid_chr and be.status_int >= 1
								where c.status_int = 1 and c.[DATE] between ? and ?
								and c.[ID] = ?
								union
								select c.recallid_int,
								c.registerid_chr,
								c.inpatientid_vchr,
								c.outdate_dat,
								c.deptid_chr,
								c.areaid_chr,
								c.areaname_vchr,
								c.patientname_vchr,
								c.doctorid_chr,
								c.recalldate_dat,
								c.overduedays_int,
								c.status_int,
								c.operator_chr,
								c.operationdate_dat,
								c.lastmodifydate_dat,
								c.doctorname_vchr,
								c.deptname_vchr,
								c.inpatienttimes_int,
								c.code_chr,
								1 normalstatus
								from t_emr_caserecall_hand c
								where c.status_int = 1 and c.[DATE] between ? and ?
								and c.[ID] = ?
								order by outdate_dat";//3
            if (p_strAttributeId == "0000003")
                strSql = strSql.Replace("[ID]", "areaid_chr");
            else
                strSql = strSql.Replace("[ID]", "deptid_chr");
            if (p_blnIsOut)
                strSql = strSql.Replace("[DATE]", "outdate_dat");
            else
                strSql = strSql.Replace("[DATE]", "recalldate_dat");
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmStart;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmEnd;
                objDPArr[2].Value = p_strId;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_dtmStart;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = p_dtmEnd;
                objDPArr[5].Value = p_strId;

                DataTable dtbResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbResult, objDPArr);
                int intRowCount = dtbResult.Rows.Count;
                if (lngRes > 0 && intRowCount > 0)
                {
                    p_objRecallValues = new clsRecallValues[intRowCount];
                    DataRow objRow = null;
                    for (int i = 0; i < intRowCount; i++)
                    {
                        objRow = dtbResult.Rows[i];
                        clsRecallValues objRecallValue = new clsRecallValues();
                        objRecallValue.m_strRecallID = objRow["recallid_int"].ToString();
                        objRecallValue.m_strRegidterID = objRow["registerid_chr"].ToString().Trim();
                        objRecallValue.m_strInPatientID = objRow["inpatientid_vchr"].ToString().Trim();
                        try
                        {
                            objRecallValue.m_dtmOutDate = DateTime.Parse(objRow["outdate_dat"].ToString());
                        }
                        catch { objRecallValue.m_dtmOutDate = new DateTime(1900, 1, 1); }
                        objRecallValue.m_strDeptID = objRow["deptid_chr"].ToString();
                        objRecallValue.m_strPatientName = objRow["patientname_vchr"].ToString();
                        objRecallValue.m_strAreaID = objRow["areaid_chr"].ToString();
                        objRecallValue.m_strAreaName = objRow["areaname_vchr"].ToString();
                        objRecallValue.m_strDoctorID = objRow["doctorid_chr"].ToString().Trim();
                        try
                        {
                            objRecallValue.m_dtmRecallDate = DateTime.Parse(objRow["recalldate_dat"].ToString());
                        }
                        catch { objRecallValue.m_dtmRecallDate = new DateTime(1900, 1, 1); }
                        objRecallValue.m_strOverDueDays = objRow["overduedays_int"].ToString();
                        objRecallValue.m_strStatus = "1";
                        objRecallValue.m_strOpratorId = objRow["operator_chr"].ToString();
                        try
                        {
                            objRecallValue.m_dtmOperationDate = DateTime.Parse(objRow["operationdate_dat"].ToString());
                        }
                        catch { objRecallValue.m_dtmOperationDate = new DateTime(1900, 1, 1); }
                        try
                        {
                            objRecallValue.m_dtmLastModifyDate = DateTime.Parse(objRow["lastmodifydate_dat"].ToString());
                        }
                        catch { objRecallValue.m_dtmLastModifyDate = new DateTime(1900, 1, 1); }
                        objRecallValue.m_strDoctorName = objRow["doctorname_vchr"].ToString();
                        objRecallValue.m_strBedName = objRow["CODE_CHR"].ToString();
                        objRecallValue.m_strDeptName = objRow["DEPTNAME_VCHR"].ToString();
                        objRecallValue.m_strInTimes = objRow["inpatienttimes_int"].ToString();
                        objRecallValue.m_strNormalstatus = objRow["Normalstatus"].ToString();
                        p_objRecallValues[i] = objRecallValue;
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

        /// <summary>
        /// 按科室或者病区、超期与否等条件查询某段回收时间的已回收病历
        /// WQF	2007-02-28
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strId"></param>
        /// <param name="p_strAttributeId"></param>
        /// <param name="p_dtmStart"></param>
        /// <param name="p_dtmEnd"></param>
        /// <param name="p_blnIsOut"></param>
        /// <param name="blnIsPastDue"></param>
        /// <param name="p_objRecallValues"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetIsRecallValues( string p_strId, string p_strAttributeId, DateTime p_dtmStart, DateTime p_dtmEnd, bool p_blnIsOut, bool blnIsPastDue, out clsRecallValues[] p_objRecallValues)
        {
            p_objRecallValues = null;
            if (p_strId == null || p_strId == "")
                return -1;
            string strSql = @"	select c.recallid_int,
								c.registerid_chr,
								c.inpatientid_vchr,
								c.outdate_dat,
								c.deptid_chr,
								c.areaid_chr,
								c.areaname_vchr,
								c.patientname_vchr,
								c.doctorid_chr,
								c.recalldate_dat,
								c.overduedays_int,
								c.status_int,
								c.operator_chr,
								c.operationdate_dat,
								c.lastmodifydate_dat,
								c.doctorname_vchr,
								c.deptname_vchr,
								c.inpatienttimes_int,
								be.code_chr,
								0 normalstatus
								from t_emr_caserecall c
								inner join t_opr_bih_leave le on c.registerid_chr = le.registerid_chr
								and le.status_int = 1
								left join t_bse_bed be on be.bedid_chr=le.outbedid_chr and be.status_int >= 1 ";

            string strSqlPart2 = @"	union
									select c.recallid_int,
									c.registerid_chr,
									c.inpatientid_vchr,
									c.outdate_dat,
									c.deptid_chr,
									c.areaid_chr,
									c.areaname_vchr,
									c.patientname_vchr,
									c.doctorid_chr,
									c.recalldate_dat,
									c.overduedays_int,
									c.status_int,
									c.operator_chr,
									c.operationdate_dat,
									c.lastmodifydate_dat,
									c.doctorname_vchr,
									c.deptname_vchr,
									c.inpatienttimes_int,
									c.code_chr,
									1 normalstatus
									from t_emr_caserecall_hand c ";
            #region
            #endregion

            if (blnIsPastDue)
            {
                strSql += "WHERE c.status_int = 1 AND c.overduedays_int > 0 AND c.[DATE] BETWEEN ? AND ? AND c.[ID] = ? ";
                strSql += strSqlPart2;
                strSql += "WHERE c.status_int = 1 AND c.overduedays_int > 0 AND c.[DATE] BETWEEN ? AND ? AND c.[ID] = ? ORDER BY outdate_dat";
            }
            else
            {
                strSql += "WHERE c.status_int = 1 AND c.overduedays_int = 0 AND c.[DATE] BETWEEN ? AND ? AND c.[ID] = ? ";
                strSql += strSqlPart2;
                strSql += "WHERE c.status_int = 1 AND c.overduedays_int = 0 AND c.[DATE] BETWEEN ? AND ? AND c.[ID] = ? ORDER BY outdate_dat";
            }

            if (p_strAttributeId == "0000003")
                strSql = strSql.Replace("[ID]", "areaid_chr");
            else
                strSql = strSql.Replace("[ID]", "deptid_chr");
            if (p_blnIsOut)
                strSql = strSql.Replace("[DATE]", "outdate_dat");
            else
                strSql = strSql.Replace("[DATE]", "recalldate_dat");
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmStart;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmEnd;
                objDPArr[2].Value = p_strId;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_dtmStart;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = p_dtmEnd;
                objDPArr[5].Value = p_strId;

                DataTable dtbResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbResult, objDPArr);
                int intRowCount = dtbResult.Rows.Count;
                if (lngRes > 0 && intRowCount > 0)
                {
                    p_objRecallValues = new clsRecallValues[intRowCount];
                    DataRow objRow = null;
                    for (int i = 0; i < intRowCount; i++)
                    {
                        objRow = dtbResult.Rows[i];
                        clsRecallValues objRecallValue = new clsRecallValues();
                        objRecallValue.m_strRecallID = objRow["recallid_int"].ToString();
                        objRecallValue.m_strRegidterID = objRow["registerid_chr"].ToString().Trim();
                        objRecallValue.m_strInPatientID = objRow["inpatientid_vchr"].ToString().Trim();
                        try
                        {
                            objRecallValue.m_dtmOutDate = DateTime.Parse(objRow["outdate_dat"].ToString());
                        }
                        catch { objRecallValue.m_dtmOutDate = new DateTime(1900, 1, 1); }
                        objRecallValue.m_strDeptID = objRow["deptid_chr"].ToString();
                        objRecallValue.m_strPatientName = objRow["patientname_vchr"].ToString();
                        objRecallValue.m_strAreaID = objRow["areaid_chr"].ToString();
                        objRecallValue.m_strAreaName = objRow["areaname_vchr"].ToString();
                        objRecallValue.m_strDoctorID = objRow["doctorid_chr"].ToString().Trim();
                        try
                        {
                            objRecallValue.m_dtmRecallDate = DateTime.Parse(objRow["recalldate_dat"].ToString());
                        }
                        catch { objRecallValue.m_dtmRecallDate = new DateTime(1900, 1, 1); }
                        objRecallValue.m_strOverDueDays = objRow["overduedays_int"].ToString();
                        objRecallValue.m_strStatus = "1";
                        objRecallValue.m_strOpratorId = objRow["operator_chr"].ToString();
                        try
                        {
                            objRecallValue.m_dtmOperationDate = DateTime.Parse(objRow["operationdate_dat"].ToString());
                        }
                        catch { objRecallValue.m_dtmOperationDate = new DateTime(1900, 1, 1); }
                        try
                        {
                            objRecallValue.m_dtmLastModifyDate = DateTime.Parse(objRow["lastmodifydate_dat"].ToString());
                        }
                        catch { objRecallValue.m_dtmLastModifyDate = new DateTime(1900, 1, 1); }
                        objRecallValue.m_strDoctorName = objRow["doctorname_vchr"].ToString();
                        objRecallValue.m_strBedName = objRow["CODE_CHR"].ToString();
                        objRecallValue.m_strDeptName = objRow["DEPTNAME_VCHR"].ToString();
                        objRecallValue.m_strInTimes = objRow["inpatienttimes_int"].ToString();
                        objRecallValue.m_strNormalstatus = objRow["Normalstatus"].ToString();
                        p_objRecallValues[i] = objRecallValue;
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

        #endregion 已回收病历查询

        #region 应回收（未回收）病历查询
        /// <summary>
        /// 应回收（未回收）病历查询
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strId"></param>
        /// <param name="m_strAttributeId"></param>
        /// <param name="p_dtmStart"></param>
        /// <param name="p_dtmEnd"></param>
        /// <param name="p_objRecallValues"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMuchRecallValues( string p_strId, string m_strAttributeId, DateTime p_dtmStart, DateTime p_dtmEnd, out clsRecallValues[] p_objRecallValues)
        {
            p_objRecallValues = null;
            if (p_strId == null || p_strId == "")
                return -1;

            string strSql = "";
            if (p_strId.Trim() != "全院")
            {
                strSql = @"select pa.lastname_vchr,
       re.inpatientcount_int,
       re.inpatientid_chr,
       re.deptid_chr,
		re.areaid_chr,
       de.deptname_vchr deptname,
       de2.deptname_vchr areaname,
       be.CODE_CHR,
       le.modify_dat outdate,
       re.registerid_chr,
       le.type_int
  from t_bse_patient pa
 inner join t_opr_bih_register re on pa.patientid_chr = re.patientid_chr and re.status_int !='-1'
 inner join t_opr_bih_leave le on re.registerid_chr = le.registerid_chr
                              and le.status_int = 1
 left join t_bse_bed be on be.BEDID_CHR=le.OUTBEDID_CHR and be.STATUS_INT >= 1
 inner join t_bse_deptdesc de on de.deptid_chr = re.deptid_chr
 inner join t_bse_deptdesc de2 on de2.deptid_chr = re.areaid_chr
 where not exists (select c2.registerid_chr from t_emr_caserecall c2 
    where c2.outdate_dat between ? and ? and c2.status_int = 1 and c2.registerid_chr = le.registerid_chr)
and not exists (select c3.INPATIENTID_VCHR from T_EMR_CASERECALL_HAND c3 
                     where c3.outdate_dat between ? AND ?
                     and c3.status_int = 1 and c3.INPATIENTID_VCHR = re.inpatientid_chr
                                 and c3.inpatient_dat = re.inpatient_dat)
   and le.outhospital_dat between ? and ?
and re.inpatientid_chr not like '%.1'
and re.[ID] = ?
 order by le.modify_dat";//8
                if (m_strAttributeId == "0000003")
                    strSql = strSql.Replace("[ID]", "areaid_chr");
                else
                    strSql = strSql.Replace("[ID]", "deptid_chr");
            }
            else
            {
                strSql = @"select pa.lastname_vchr,
       re.inpatientcount_int,
       re.inpatientid_chr,
       re.deptid_chr,
		re.areaid_chr,
       de.deptname_vchr deptname,
       de2.deptname_vchr areaname,
       be.CODE_CHR,
       le.modify_dat outdate,
       re.registerid_chr,
       le.type_int
  from t_bse_patient pa
 inner join t_opr_bih_register re on pa.patientid_chr = re.patientid_chr and re.status_int !='-1'
 inner join t_opr_bih_leave le on re.registerid_chr = le.registerid_chr
                              and le.status_int = 1
 left join t_bse_bed be on be.BEDID_CHR=le.OUTBEDID_CHR and be.STATUS_INT >= 1
 inner join t_bse_deptdesc de on de.deptid_chr = re.deptid_chr
 inner join t_bse_deptdesc de2 on de2.deptid_chr = re.areaid_chr
 where not exists (select c2.registerid_chr from t_emr_caserecall c2 
    where c2.outdate_dat between ? and ? and c2.status_int = 1 and c2.registerid_chr = le.registerid_chr)
and not exists (select c3.INPATIENTID_VCHR from T_EMR_CASERECALL_HAND c3 
                     where c3.outdate_dat between ? AND ?
                     and c3.status_int = 1 and c3.INPATIENTID_VCHR = re.inpatientid_chr
                                 and c3.inpatient_dat = re.inpatient_dat)
   and le.outhospital_dat between ? and ?
and re.inpatientid_chr not like '%.1'
order by areaname,le.modify_dat";//8
            }
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                if (p_strId.Trim() != "全院")
                {
                    objHRPServ.CreateDatabaseParameter(7, out objDPArr);
                    //				objDPArr[0].Value = p_strId;
                    objDPArr[0].DbType = DbType.DateTime;
                    objDPArr[0].Value = p_dtmStart;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = p_dtmEnd;
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = p_dtmStart;
                    objDPArr[3].DbType = DbType.DateTime;
                    objDPArr[3].Value = p_dtmEnd;
                    objDPArr[4].DbType = DbType.DateTime;
                    objDPArr[4].Value = p_dtmStart;
                    objDPArr[5].DbType = DbType.DateTime;
                    objDPArr[5].Value = p_dtmEnd;
                    objDPArr[6].Value = p_strId;
                }
                else
                {
                    objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                    //				objDPArr[0].Value = p_strId;
                    objDPArr[0].DbType = DbType.DateTime;
                    objDPArr[0].Value = p_dtmStart;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = p_dtmEnd;
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = p_dtmStart;
                    objDPArr[3].DbType = DbType.DateTime;
                    objDPArr[3].Value = p_dtmEnd;
                    objDPArr[4].DbType = DbType.DateTime;
                    objDPArr[4].Value = p_dtmStart;
                    objDPArr[5].DbType = DbType.DateTime;
                    objDPArr[5].Value = p_dtmEnd;
                }

                DataTable dtbResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbResult, objDPArr);
                int intRowCount = dtbResult.Rows.Count;
                if (lngRes > 0 && intRowCount > 0)
                {
                    p_objRecallValues = new clsRecallValues[intRowCount];
                    DataRow objRow = null;
                    DateTime dtmMinDate = DateTime.MaxValue;
                    DateTime dtmMaxDate = DateTime.MinValue;
                    for (int i = 0; i < intRowCount; i++)
                    {
                        objRow = dtbResult.Rows[i];
                        clsRecallValues objRecallValue = new clsRecallValues();
                        objRecallValue.m_strRegidterID = objRow["registerid_chr"].ToString().Trim();
                        objRecallValue.m_strInPatientID = objRow["inpatientid_chr"].ToString().Trim();
                        objRecallValue.m_strType = objRow["type_int"].ToString();//此处赋值必须在objRecallValue.m_dtmOutDate赋值之前
                        try
                        {
                            objRecallValue.m_dtmOutDate = DateTime.Parse(objRow["outdate"].ToString());
                            if (dtmMinDate > objRecallValue.m_dtmOutDate) dtmMinDate = objRecallValue.m_dtmOutDate;
                            if (dtmMaxDate < objRecallValue.m_dtmOutDate) dtmMaxDate = objRecallValue.m_dtmOutDate;
                        }
                        catch { objRecallValue.m_dtmOutDate = new DateTime(1900, 1, 1); }
                        objRecallValue.m_strDeptID = objRow["deptid_chr"].ToString();
                        objRecallValue.m_strDeptName = objRow["deptname"].ToString();
                        objRecallValue.m_strAreaID = objRow["areaid_chr"].ToString();
                        objRecallValue.m_strAreaName = objRow["areaname"].ToString();
                        objRecallValue.m_strPatientName = objRow["lastname_vchr"].ToString();
                        //						if(!(objRow["hempid"] is DBNull))
                        //						{
                        //							objRecallValue.m_strDoctorID = objRow["hempid"].ToString().Trim();
                        //							objRecallValue.m_strDoctorName = objRow["HName"].ToString();
                        //						}
                        //						else if(!(objRow["imempid"] is DBNull))
                        //						{
                        //							objRecallValue.m_strDoctorID = objRow["imempid"].ToString().Trim();
                        //							objRecallValue.m_strDoctorName = objRow["imName"].ToString();
                        //						}
                        objRecallValue.m_strInTimes = objRow["inpatientcount_int"].ToString();
                        objRecallValue.m_strBedName = objRow["CODE_CHR"].ToString();

                        p_objRecallValues[i] = objRecallValue;
                    }
                    dtmMaxDate = dtmMaxDate.AddMonths(2);
                    DateTime[] dtmArr = m_objGetHoliday(  dtmMinDate, dtmMaxDate);
                    if (dtmArr != null && dtmArr.Length > 0 && p_objRecallValues.Length > 0)
                    {
                        for (int j = 0; j < p_objRecallValues.Length; j++)
                        {
                            p_objRecallValues[j].m_dtExcludeHolidayArr = dtmArr;
                        }
                    }
                    //以下为周六日获取
                    DateTime[] dtmArrWeekEnd = m_objGetWorkDayInWeekEnd(  p_dtmStart, p_dtmEnd);
                    if (dtmArrWeekEnd != null && dtmArrWeekEnd.Length > 0 && p_objRecallValues.Length > 0)
                    {
                        for (int k = 0; k < p_objRecallValues.Length; k++)
                        {
                            p_objRecallValues[k].m_dtIncludeHolidayArr = dtmArrWeekEnd;
                        }
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
        /// <summary>
        /// 全院统计应回收病历(未回收)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtmStart"></param>
        /// <param name="p_dtmEnd"></param>
        /// <param name="p_objRecallValues"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllMuchRecallStatis(  DateTime p_dtmStart, DateTime p_dtmEnd, out clsAllMuchRecallStatisValues[] p_strAllValueArr)
        {
            p_strAllValueArr = null;
            string strSql = @"select re.areaid_chr, de.deptname_vchr,count(re.registerid_chr) casecount
  from t_bse_deptdesc de
 inner join t_opr_bih_register re on re.areaid_chr = de.deptid_chr and re.status_int !='-1'
 inner join t_opr_bih_leave le on re.registerid_chr = le.registerid_chr
                              and le.status_int = 1
inner join t_bse_patient pa on pa.patientid_chr = re.patientid_chr
 where not exists
 (select c2.registerid_chr
          from t_emr_caserecall c2
         where c2.outdate_dat between ? and ?
           and c2.status_int = 1
           and c2.registerid_chr = le.registerid_chr)
   and le.outhospital_dat between ? and ?
and re.inpatientid_chr not like '%.1'
 group by re.areaid_chr,de.deptname_vchr
 order by de.deptname_vchr";//4
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmStart;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmEnd;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_dtmStart;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_dtmEnd;

                DataTable dtbResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbResult, objDPArr);
                int intRowCount = dtbResult.Rows.Count;
                if (lngRes > 0 && intRowCount > 0)
                {
                    p_strAllValueArr = new clsAllMuchRecallStatisValues[intRowCount];
                    DataRow objRow = null;
                    for (int i = 0; i < intRowCount; i++)
                    {
                        clsAllMuchRecallStatisValues objValue = new clsAllMuchRecallStatisValues();
                        objRow = dtbResult.Rows[i];
                        objValue.m_strDeptID = objRow["areaid_chr"].ToString().Trim();
                        objValue.m_strDeptName = objRow["deptname_vchr"].ToString();
                        objValue.m_strCount = objRow["casecount"].ToString();
                        p_strAllValueArr[i] = objValue;
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

        #endregion 应回收（未回收）病历查询

        #region 手工回收病历、病历回收率 WQF
        /// <summary>
        /// 获取所输入病人ID的所有相关信息 
        /// WQF 2007-01-4
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInpatientId"></param>
        /// <param name="p_objRecallValues"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetHadRecallValueByPatientId_Hand( string p_strInpatientId, out clsRecallValues[] p_objRecallValues)
        {
            p_objRecallValues = null;
            if (p_strInpatientId == null || p_strInpatientId == "")
                return -1;
            //-----------------------------------------------------
            //选出所有未回收的纪录
            //说明：
            //		TOBL.status_int = 0 ：还未出院
            //		TBB.STATUS_INT >= 1 ：占有床位(可能大于1个床位)
            //-----------------------------------------------------
            //			string sqlQuery = @"SELECT DISTINCT
            //								TBP.lastname_vchr,	TOBR.inpatientcount_int,	TOBR.inpatientid_chr,
            //								TOBR.deptid_chr,	TOBR.areaid_chr,			TBD1.deptname_vchr deptname,
            //								TBB.CODE_CHR,		TOBL.modify_dat outdate,	TOBL.type_int,
            //								TOBR.registerid_chr,TBD1.deptname_vchr areaname,'' overduedays_int,
            //								'' recalldate_dat,	'' recallid_int,			'' status_int
            //							FROM t_opr_bih_register TOBR 
            //							LEFT JOIN t_opr_bih_leave TOBL
            //							ON TOBR.registerid_chr = TOBL.registerid_chr AND TOBL.status_int = 0
            //							INNER JOIN t_bse_patient TBP
            //							ON TBP.patientid_chr = TOBR.patientid_chr
            //							INNER JOIN t_bse_bed TBB
            //							ON TBB.areaid_chr=TOBL.OUTAREAID_CHR AND TBB.BEDID_CHR=TOBL.OUTBEDID_CHR AND TBB.STATUS_INT >= 1
            //							INNER JOIN t_bse_deptdesc TBD1
            //							ON TBD1.deptid_chr = TOBR.deptid_chr
            //							WHERE TOBR.inpatientid_chr = ? AND TOBR.REGISTERID_CHR NOT IN
            //							(SELECT DISTINCT REGISTERID_CHR FROM t_emr_caserecall TEC
            //							UNION  SELECT DISTINCT REGISTERID_CHR FROM T_EMR_CASERECALL_HAND)
            //							ORDER BY TBP.lastname_vchr ASC";
            string sqlQuery = @"SELECT TBP.lastname_vchr,	TOBR.inpatientcount_int,	TOBR.inpatientid_chr,
								TOBR.deptid_chr,	TOBR.areaid_chr,			TBD1.deptname_vchr deptname,
								TBB.CODE_CHR,		TOBL.modify_dat outdate,	TOBL.type_int,
								TOBR.registerid_chr,TBD1.deptname_vchr areaname,'' overduedays_int,
								'' recalldate_dat,	'' recallid_int,			'' status_int
							FROM t_opr_bih_register TOBR 
							LEFT JOIN t_opr_bih_leave TOBL
							ON TOBR.registerid_chr = TOBL.registerid_chr AND TOBL.status_int = 1
							INNER JOIN t_bse_patient TBP
							ON TBP.patientid_chr = TOBR.patientid_chr
							LEFT JOIN t_bse_bed TBB
							ON TBB.areaid_chr=TOBL.OUTAREAID_CHR AND TBB.BEDID_CHR=TOBL.OUTBEDID_CHR AND TBB.STATUS_INT >= 1
							INNER JOIN t_bse_deptdesc TBD1
							ON TBD1.deptid_chr = TOBR.deptid_chr
							WHERE TOBR.inpatientid_chr = ? AND TOBR.PSTATUS_INT=3 AND NOT exists 
							(SELECT REGISTERID_CHR FROM t_emr_caserecall TEC where tec.REGISTERID_CHR = TOBR.REGISTERID_CHR and tec.status_int= 1)
							and not exists(SELECT inpatientid_vchr FROM T_EMR_CASERECALL_HAND  crh where crh.inpatientid_vchr = TOBR.inpatientid_chr
											and crh.inpatient_dat = TOBR.inpatient_dat and crh.status_int= 0)
							ORDER BY TOBR.inpatient_dat ";
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                //按照SQL语句中的参数(?)生成SQL参数 objDPArr
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strInpatientId;
                DataTable dtbResult = new DataTable();
                //执行SQL语句获取所有相关纪录
                lngRes = objHRPServ.lngGetDataTableWithParameters(sqlQuery, ref dtbResult, objDPArr);
                int intRowCount = dtbResult.Rows.Count;
                if (lngRes > 0 && intRowCount > 0)
                {
                    //将执行SQL语句返回的数据填充至p_objRecallValues
                    p_objRecallValues = m_objGetData(dtbResult);
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                objLogger.LogError(objEx);
            }
            return lngRes;
        }
        /// <summary>
        /// 获取所输入病人ID的所有已回收病厉信息 (仅适用于手工回收病历)
        /// WQF 2007-02-28
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInpatientId"></param>
        /// <param name="p_objRecallValues"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientByInId_Hand( string p_strInpatientId, out clsRecallValues[] p_objRecallValues)
        {
            p_objRecallValues = null;
            if (p_strInpatientId == null || p_strInpatientId == "")
                return -1;

            string sqlQuery = @"SELECT TBP.lastname_vchr,	TOBR.inpatientcount_int,	TOBR.inpatientid_chr,
								TOBR.deptid_chr,	TOBR.areaid_chr,			TBD1.deptname_vchr deptname,
								TBB.CODE_CHR,		TOBL.modify_dat outdate,	TOBL.type_int,
								TOBR.registerid_chr,TBD1.deptname_vchr areaname,'' overduedays_int,
								'' recalldate_dat,	'' recallid_int,			'' status_int
							FROM t_opr_bih_register TOBR 
							LEFT JOIN t_opr_bih_leave TOBL
							ON TOBR.registerid_chr = TOBL.registerid_chr AND TOBL.status_int = 1
							INNER JOIN t_bse_patient TBP
							ON TBP.patientid_chr = TOBR.patientid_chr
							LEFT JOIN t_bse_bed TBB
							ON TBB.areaid_chr=TOBL.OUTAREAID_CHR AND TBB.BEDID_CHR=TOBL.OUTBEDID_CHR AND TBB.STATUS_INT >= 1
							INNER JOIN t_bse_deptdesc TBD1
							ON TBD1.deptid_chr = TOBR.deptid_chr
							WHERE TOBR.inpatientid_chr = ? AND TOBR.PSTATUS_INT=3 AND NOT exists 
							(SELECT REGISTERID_CHR FROM t_emr_caserecall TEC where tec.REGISTERID_CHR = TOBR.REGISTERID_CHR and tec.status_int= 1)
							and not exists(SELECT inpatientid_vchr FROM T_EMR_CASERECALL_HAND  crh where crh.inpatientid_vchr = TOBR.inpatientid_chr
											and crh.inpatient_dat = TOBR.inpatient_dat and crh.status_int= 1)
							ORDER BY TOBR.inpatient_dat ";
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                //按照SQL语句中的参数(?)生成SQL参数 objDPArr
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strInpatientId;
                DataTable dtbResult = new DataTable();
                //执行SQL语句获取所有相关纪录
                lngRes = objHRPServ.lngGetDataTableWithParameters(sqlQuery, ref dtbResult, objDPArr);
                int intRowCount = dtbResult.Rows.Count;
                if (lngRes > 0 && intRowCount > 0)
                {
                    //将执行SQL语句返回的数据填充至p_objRecallValues
                    p_objRecallValues = m_objGetData(dtbResult);
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                objLogger.LogError(objEx);
            }
            return lngRes;
        }
        /// <summary>
        /// 回收病历,将信息保存至 T_EMR_CASERECALL_HAND 表 
        /// WQF 2006-12-31
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddRecallRow( clsRecallValues[] p_objArr)
        {
            long lngRes = 0;
            if (p_objArr == null || p_objArr.Length == 0)
                return 0;
            string sqlQuery = @"insert into t_emr_caserecall_hand 
					(registerid_chr,	inpatientid_vchr,	outdate_dat,	deptid_chr,		patientname_vchr,
					doctorid_chr,		recalldate_dat,		overduedays_int,status_int,		operator_chr,
					operationdate_dat,	lastmodifydate_dat,	deptname_vchr,	doctorname_vchr,inpatienttimes_int,
					areaid_chr,			areaname_vchr,		inpatient_dat,	bedid_chr,
					code_chr) 
					values (?, ?, ?, ?, ?, ?, ?, ?, ?, ?,null, getdate(), ?, ?, ?, ?, ?, ?, ?, ?)";//18
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                for (int i = 0; i < p_objArr.Length; i++)
                {
                    long lngEff = 0;
                    if (p_objArr[i] != null)
                    {
                        objDPArr = null;
                        objHRPServ.CreateDatabaseParameter(18, out objDPArr);
                        if (objDPArr != null)
                        {
                            objDPArr[0].Value = p_objArr[i].m_strRegidterID;//2
                            objDPArr[1].Value = p_objArr[i].m_strInPatientID;//3
                            objDPArr[2].DbType = DbType.DateTime;
                            objDPArr[2].Value = p_objArr[i].m_dtmOutDate;//4
                            objDPArr[3].Value = p_objArr[i].m_strDeptID;//5
                            objDPArr[4].Value = p_objArr[i].m_strPatientName;//6
                            objDPArr[5].Value = p_objArr[i].m_strDoctorID;//7
                            objDPArr[6].DbType = DbType.DateTime;
                            objDPArr[6].Value = p_objArr[i].m_dtmRecallDate;//8
                            objDPArr[7].Value = p_objArr[i].m_strOverDueDays;//9
                            objDPArr[8].Value = p_objArr[i].m_strStatus;//10
                            objDPArr[9].Value = p_objArr[i].m_strOpratorId;//11
                            objDPArr[10].Value = p_objArr[i].m_strDeptName;//14
                            objDPArr[11].Value = p_objArr[i].m_strDoctorName;//15
                            objDPArr[12].Value = p_objArr[i].m_strInTimes;//16
                            objDPArr[13].Value = p_objArr[i].m_strAreaID;//17
                            objDPArr[14].Value = p_objArr[i].m_strAreaName;//18
                            objDPArr[15].DbType = DbType.DateTime;
                            objDPArr[15].Value = p_objArr[i].m_dtmInPatient_dat;//20
                            objDPArr[16].Value = p_objArr[i].m_strBedid_chr;//21
                            objDPArr[17].Value = p_objArr[i].m_strCode_chr;//22

                            //							lngRes = objHRPServ.lngExecuteParameterSQL(sqlQuery,ref lngEff,objDPArr);
                            objHRPServ.lngExecuteParameterSQL(sqlQuery, ref lngEff, objDPArr);
                            lngRes += lngEff;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                objLogger.LogError(ex);
            }
            return lngRes;
        }
        /// <summary>
        /// 获取出院病历及时回收率数据 
        /// WQF 2007-1-8 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endtTime">结束时间</param>
        /// <returns>数据表</returns>
        [AutoComplete]
        public DataTable m_GetRecallRatioData(  int hours, DateTime startTime, DateTime endTime)
        {
            DataTable resultDt = null;
            string sqlQuery = @"select tbd1.deptname_vchr depart,
								count(distinct tobl.registerid_chr)+count(distinct tech.registerid_chr) allout ,
								count(distinct tec.registerid_chr) +
								count(distinct tech.registerid_chr) realrecall
								from t_opr_bih_register tobr
								inner join t_opr_bih_leave tobl on tobr.registerid_chr = tobl.registerid_chr and tobl.status_int = 1
								inner join t_bse_deptdesc tbd1 on tbd1.deptid_chr = tobr.areaid_chr
								left join (select crc.* from t_emr_caserecall crc where overduedays_int < 1 and status_int = 1) tec 
								on tec.registerid_chr = tobr.registerid_chr
								left join (select crchand.* from t_emr_caserecall_hand crchand where overduedays_int < 1 and status_int = 1) tech 
								on tech.inpatientid_vchr = tobr.inpatientid_chr and tech.inpatient_dat = tobr.inpatient_dat
								where tobl.outhospital_dat between ? and ?
								and tobr.inpatientid_chr not like '%.1'
								group by tbd1.deptname_vchr
								order by tbd1.deptname_vchr";
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                resultDt = new DataTable();
                IDataParameter[] objDPArr = null;
                //按照SQL语句中的参数(?)生成SQL参数 objDPArr
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                //objDPArr[0].DbType = DbType.Int32;
                //objDPArr[0].Value = hours / 24;
                //objDPArr[1].DbType = DbType.Int32;
                //objDPArr[1].Value = hours / 24;
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = startTime;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = endTime;
                //objDPArr[4].DbType = DbType.DateTime;
                //objDPArr[4].Value = startTime;
                //objDPArr[5].DbType = DbType.DateTime;
                //objDPArr[5].Value = endTime;

                DataTable dtbResult = new DataTable();
                //执行SQL语句获取所有相关纪录
                objHRPServ.lngGetDataTableWithParameters(sqlQuery, ref resultDt, objDPArr);
                //格式化数据表
                if (resultDt != null && resultDt.Rows.Count > 0)
                {
                    resultDt.TableName = "RecallRatioTable";
                    resultDt.Columns[0].ColumnName = "住院科室/病区";
                    resultDt.Columns[1].ColumnName = "应回收病历数";
                    resultDt.Columns[2].ColumnName = "按时回收病历数";
                    DataColumn dc = new DataColumn("及时回收率");
                    resultDt.Columns.Add(dc);

                    foreach (DataRow dr in resultDt.Rows)
                    {
                        Double all = 0;
                        Double real = 0;
                        try
                        {
                            all = Double.Parse(dr[1].ToString());
                            real = Double.Parse(dr[2].ToString());
                        }
                        catch { }
                        //计算及时回收率
                        if (all > 0)
                            dr[3] = System.Math.Round(real / all, 6) * 100;//控制结果最多只有6位有效数字
                        else
                            dr[3] = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                objLogger.LogError(ex);
            }
            return resultDt;
        }

        #endregion

        #region 假期设置

        /// <summary>
        /// 保存节假日
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtArr"></param>
        /// <param name="dtArr2"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddHoliday(  DateTime[] dtArr, DateTime[] dtArr2)
        {
            long lngEff = 0;
            long lngRes = 0;
            //			DataTable objHolidayValues = new DataTable();
            DateTime[] dtHolidayValues = null;
            ArrayList arlHolidayValues = new ArrayList();
            string sqlQuery = "insert into t_emr_holiday(holiday) values(?)";
            string sqlQuery2 = "insert into t_emr_holiday(workdayinweekend) values(?)";
            //1------------------------------------------------------------
            #region 对假期列进行插入
            if (dtArr != null)
            {
                #region 对假期列进行逻辑判断
                try
                {

                    DateTime dtMax = dtArr[0];
                    DateTime dtMin = dtMax;
                    foreach (DateTime dtOne in dtArr)
                    {
                        int intDayCount1 = ((TimeSpan)(dtOne - dtMax)).Days;
                        if (intDayCount1 > 0)
                        {
                            dtMax = dtOne;
                        }
                        else
                        {
                            int intDayCount2 = ((TimeSpan)(dtMin - dtOne)).Days;
                            if (intDayCount2 > 0)
                                dtMin = dtOne;
                        }
                    }
                    dtHolidayValues = this.m_objGetHoliday( dtMin, dtMax);
                    if (dtHolidayValues != null)
                    {
                        foreach (DateTime dtOneValue in dtArr)
                        {
                            bool blnHasRow = false;
                            foreach (DateTime dtOne in dtHolidayValues)
                            {
                                if (dtOneValue.Equals(dtOne))
                                {
                                    blnHasRow = true;
                                    break;
                                }
                            }
                            if (!blnHasRow)
                                arlHolidayValues.Add(dtOneValue);
                        }
                    }
                    else
                    {
                        foreach (DateTime dtOneValue in dtArr)
                        {
                            arlHolidayValues.Add(dtOneValue);
                        }
                    }
                }
                catch (Exception ex)
                {
                    com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                    objLogger.LogError(ex);
                    return 0;
                }

                #endregion
                //------------------------------------------------------------
                clsHRPTableService objHRPServ = new clsHRPTableService();
                foreach (DateTime dtHoliday in arlHolidayValues)
                {
                    try
                    {
                        IDataParameter[] objDPArr = null;
                        objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                        objDPArr[0].DbType = DbType.DateTime;
                        objDPArr[0].Value = dtHoliday;

                        lngRes += objHRPServ.lngExecuteParameterSQL(sqlQuery, ref lngEff, objDPArr);
                    }
                    catch (Exception ex)
                    {
                        com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                        objLogger.LogError(ex);
                    }
                }
                objHRPServ.Dispose();
            }
            #endregion

            //2------------------------------------------------------------
            DateTime[] dtWorkDayInWeekend = null;
            ArrayList arlWorkDayInWeekend = new ArrayList();
            #region 对必须上班列进行插入
            if (dtArr2 != null)
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                #region 对必须上班列进行逻辑判断
                try
                {

                    DateTime dtMax = dtArr2[0];
                    DateTime dtMin = dtMax;
                    foreach (DateTime dtOne in dtArr2)
                    {
                        int intDayCount1 = ((TimeSpan)(dtOne - dtMax)).Days;
                        if (intDayCount1 > 0)
                        {
                            dtMax = dtOne;
                        }
                        else
                        {
                            int intDayCount2 = ((TimeSpan)(dtMin - dtOne)).Days;
                            if (intDayCount2 > 0)
                                dtMin = dtOne;
                        }
                    }
                    dtWorkDayInWeekend = this.m_objGetWorkDayInWeekEnd( dtMin, dtMax);
                    if (dtWorkDayInWeekend != null)
                    {
                        foreach (DateTime dtOneValue in dtArr2)
                        {
                            bool blnHasRow = false;
                            foreach (DateTime dtOne in dtWorkDayInWeekend)
                            {
                                if (dtOneValue.Equals(dtOne))
                                {
                                    blnHasRow = true;
                                    break;
                                }
                            }
                            if (!blnHasRow)
                                arlWorkDayInWeekend.Add(dtOneValue);
                        }
                    }
                    else
                    {
                        foreach (DateTime dtOneValue in dtArr2)
                        {
                            arlWorkDayInWeekend.Add(dtOneValue);
                        }
                    }
                }
                catch (Exception ex)
                {
                    com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                    objLogger.LogError(ex);
                    return 0;
                }

                #endregion
                //------------------------------------------------------------
                foreach (DateTime dt_WorkDayInWeekend in arlWorkDayInWeekend)
                {
                    try
                    {
                        IDataParameter[] objDPArr = null;
                        objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                        objDPArr[0].DbType = DbType.DateTime;
                        objDPArr[0].Value = dt_WorkDayInWeekend;

                        lngRes += objHRPServ.lngExecuteParameterSQL(sqlQuery2, ref lngEff, objDPArr);
                    }
                    catch (Exception ex)
                    {
                        com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                        objLogger.LogError(ex);
                    }
                }
                objHRPServ.Dispose();
            }
            #endregion
            return lngRes;
        }


        /// <summary>
        /// 查询节假日
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <param name="objHolidayValues"></param>
        /// <returns></returns>
        [AutoComplete]
        public DateTime[] m_objGetHoliday( DateTime dtStart, DateTime dtEnd)
        {
            long lngRes = 0;
            List<DateTime> m_arrDtList = null;
            DateTime[] dtBeginEndValues = null;
            DateTime[] m_dtHolidaysArr = null;
            string sqlQuery = @"select t.begindate_dat,t.enddate_dat from t_emr_holiday t 
            where t.type_int ='1'and t.status_int ='1'";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            DataTable objValues = new DataTable();

            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(0, out objDPArr);
                //objDPArr[0].DbType = DbType.DateTime;
                //objDPArr[0].Value = dtStart;
                //objDPArr[1].DbType = DbType.DateTime;
                //objDPArr[1].Value = dtEnd;

                lngRes = objHRPServ.lngGetDataTableWithParameters(sqlQuery, ref objValues, objDPArr);
                int intRowCount = objValues.Rows.Count;
                if (lngRes > 0 && intRowCount > 0)
                {
                    m_arrDtList = new List<DateTime>();

                    for (int i = 0; i < intRowCount; i++)
                    {
                        dtBeginEndValues = new DateTime[2];
                        if (dtStart <= (DateTime)objValues.Rows[i]["BEGINDATE_DAT"] && (DateTime)objValues.Rows[i]["BEGINDATE_DAT"] <= dtEnd)
                        {
                            dtBeginEndValues[0] = (DateTime)objValues.Rows[i]["BEGINDATE_DAT"];
                            //dtBeginEndValues[1] = (DateTime)objValues.Rows[i]["ENDDATE_DAT"];
                        }
                        else if ((DateTime)objValues.Rows[i]["BEGINDATE_DAT"] <= dtStart && dtStart <= (DateTime)objValues.Rows[i]["ENDDATE_DAT"])
                        {
                            dtBeginEndValues[0] = dtStart;
                        }
                        else
                        {
                            dtBeginEndValues = null;
                        }
                        if ((DateTime)objValues.Rows[i]["ENDDATE_DAT"] >= dtStart && (DateTime)objValues.Rows[i]["ENDDATE_DAT"] <= dtEnd)
                        {
                            dtBeginEndValues[1] = (DateTime)objValues.Rows[i]["ENDDATE_DAT"];
                        }
                        else if ((DateTime)objValues.Rows[i]["BEGINDATE_DAT"] <= dtEnd && dtEnd <= (DateTime)objValues.Rows[i]["ENDDATE_DAT"])
                        {
                            dtBeginEndValues[1] = dtEnd;
                        }
                        else
                        {
                            dtBeginEndValues = null;
                        }
                        if (dtBeginEndValues != null)
                        {
                            DateTime dtValue = new DateTime();
                            dtValue = dtBeginEndValues[0];
                            while (dtValue <= dtBeginEndValues[1])
                            {
                                if (!m_arrDtList.Contains(dtValue))
                                {
                                    m_arrDtList.Add(dtValue);
                                }

                                dtValue = dtValue.AddDays(1);
                            }

                        }
                    }

                }
                if (m_arrDtList != null && m_arrDtList.Count != 0)
                {
                    m_dtHolidaysArr = m_arrDtList.ToArray();
                }

            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                objLogger.LogError(ex);
            }

            return m_dtHolidaysArr;

        }

        /// <summary>
        /// 查询上班日
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <param name="objHolidayValues"></param>
        /// <returns></returns>
        [AutoComplete]
        public DateTime[] m_objGetWorkDayInWeekEnd( DateTime dtStart, DateTime dtEnd)
        {
            long lngRes = 0;
            List<DateTime> m_arrDtList = null;
            DateTime[] dtBeginEndValues = null;
            DateTime[] m_dtHolidaysArr = null;
            string sqlQuery = @"select t.begindate_dat,t.enddate_dat from t_emr_holiday t 
            where t.type_int ='2'and t.status_int ='1'";
            //string sqlQuery = "select workdayinweekend from t_emr_holiday where workdayinweekend between ? and ?";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            DataTable objValues = new DataTable();

            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(0, out objDPArr);
                //objDPArr[0].DbType = DbType.DateTime;
                //objDPArr[0].Value = dtStart;
                //objDPArr[1].DbType = DbType.DateTime;
                //objDPArr[1].Value = dtEnd;

                lngRes = objHRPServ.lngGetDataTableWithParameters(sqlQuery, ref objValues, objDPArr);
                int intRowCount = objValues.Rows.Count;
                if (lngRes > 0 && intRowCount > 0)
                {
                    m_arrDtList = new List<DateTime>();
                    for (int i = 0; i < intRowCount; i++)
                    {
                        dtBeginEndValues = new DateTime[2];
                        if (dtStart <= (DateTime)objValues.Rows[i]["BEGINDATE_DAT"] && (DateTime)objValues.Rows[i]["BEGINDATE_DAT"] <= dtEnd)
                        {
                            dtBeginEndValues[0] = (DateTime)objValues.Rows[i]["BEGINDATE_DAT"];
                        }
                        else if ((DateTime)objValues.Rows[i]["BEGINDATE_DAT"] <= dtStart && dtStart <= (DateTime)objValues.Rows[i]["ENDDATE_DAT"])
                        {
                            dtBeginEndValues[0] = dtStart;
                        }
                        else
                        {
                            dtBeginEndValues = null;
                        }
                        if ((DateTime)objValues.Rows[i]["ENDDATE_DAT"] >= dtStart && (DateTime)objValues.Rows[i]["ENDDATE_DAT"] <= dtEnd)
                        {
                            dtBeginEndValues[1] = (DateTime)objValues.Rows[i]["ENDDATE_DAT"];
                        }
                        else if ((DateTime)objValues.Rows[i]["BEGINDATE_DAT"] <= dtEnd && dtEnd <= (DateTime)objValues.Rows[i]["ENDDATE_DAT"])
                        {
                            dtBeginEndValues[1] = dtEnd;
                        }
                        else
                        {
                            dtBeginEndValues = null;
                        }
                        if (dtBeginEndValues != null)
                        {
                            DateTime dtValue = new DateTime();
                            dtValue = dtBeginEndValues[0];
                            while (dtValue <= dtBeginEndValues[1])
                            {
                                if (!m_arrDtList.Contains(dtValue))
                                {
                                    m_arrDtList.Add(dtValue);
                                }

                                dtValue = dtValue.AddDays(1);
                            }

                        }
                    }

                }
                if (m_arrDtList != null && m_arrDtList.Count != 0)
                {
                    m_dtHolidaysArr = m_arrDtList.ToArray();
                }

            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                objLogger.LogError(ex);
            }

            return m_dtHolidaysArr;
        }
        /// <summary>
        /// 删除指定的节假日
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtRemoveValues"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngRemoveHolidayValues( DateTime[] dtRemoveValues)
        {
            if (dtRemoveValues == null || dtRemoveValues.Length == 0)
                return 0;

            string strSql = @"	delete from t_emr_holiday where Holiday = ?";

            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                foreach (DateTime dtValue in dtRemoveValues)
                {
                    long lngEff = 0;

                    objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(1, out objDPArr);

                    objDPArr[0].DbType = DbType.DateTime;
                    objDPArr[0].Value = dtValue;

                    objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
                    lngRes += lngEff;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 删除指定的节假日
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtRemoveValues"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngRemoveWorkDayInWeekEndValues( DateTime[] dtRemoveValues)
        {
            if (dtRemoveValues == null || dtRemoveValues.Length == 0)
                return 0;

            string strSql = @"	delete from t_emr_holiday where WorkDayInWeekEnd = ?";

            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                foreach (DateTime dtValue in dtRemoveValues)
                {
                    long lngEff = 0;

                    objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(1, out objDPArr);

                    objDPArr[0].DbType = DbType.DateTime;
                    objDPArr[0].Value = dtValue;

                    objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
                    lngRes += lngEff;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 修改日期权限判断
        /// <summary>
        /// 根据员工流水号获取权限角色ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEmpID">员工流水号</param>
        /// <param name="p_dtbRoleID">权限角色ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRoleIDByEmpID(  string p_strEmpID, out DataTable p_dtbRoleID)
        {
            long lngRes = -1;
            p_dtbRoleID = new DataTable();
            string strSQL = @"select b.name_vchr from t_sys_emprolemap a join t_sys_role b on a.roleid_chr=b.roleid_chr
	                       where a.empid_chr='" + p_strEmpID + "'";
            try
            {
                DataTable dtbValue = new DataTable();
                lngRes = new clsHRPTableService().DoGetDataTable(strSQL, ref dtbValue);
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_dtbRoleID = dtbValue;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion 修改日期权限判断

    }
}
