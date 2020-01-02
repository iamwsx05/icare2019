using System;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.HIS
{
	/// <summary>
	/// clsCardiogramManageSvc 的摘要说明。
	/// 作者：
	/// 时间：
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(Enabled=true)]
	public class clsPatientQuerySVC:com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		public clsPatientQuerySVC()
		{
		}
		[AutoComplete]
		public long m_lngGetPatientInfoByCondition(clsPatientQueryCondition p_QueryCondition,out DataTable p_dtbResult)
		{
			p_dtbResult = new DataTable();
			long lngRes=0; 
//原有正确的sql            string strSQL = @"SELECT a.*, c.patientcardid_chr, b.registerid_chr, b.modify_dat,
//       b.patientid_chr, b.isbooking_int, b.inpatientid_chr as InpatientNo, b.inpatient_dat,
//       b.deptid_chr, b.areaid_chr, b.bedid_chr,
//       (SELECT deptname_vchr
//          FROM t_bse_deptdesc
//         WHERE t_bse_deptdesc.deptid_chr = b.areaid_chr) AS deptname,
//       (SELECT paytypename_vchr
//          FROM t_bse_patientpaytype
//         WHERE paytypeid_chr = a.paytypeid_chr) AS paytypename,
//       (SELECT code_chr
//          FROM t_bse_bed
//         WHERE t_bse_bed.bedid_chr = b.bedid_chr) AS bedno,
//       DECODE (state_int, 1, '危', 2, '急', 3, '普通', '') AS sickstatus,
//       DECODE (pstatus_int,
//               0, '未上床',
//               1, '已上床',
//               2, '预出院',
//               3, '实际出院',
//               4, '请假',
//               ''
//              ) inhospitalstatus,
//       DECODE (type_int, 1, '门诊', 2, '急诊', 3, '他院转入', '') intype,
//       (SELECT lastname_vchr
//          FROM t_bse_employee
//         WHERE empid_chr = casedoctor_chr) doctorname, 
//         b.diagnose_vchr,
//         b.icd10diagtext_vchr
//  FROM t_bse_patient a, (SELECT a.*
//          FROM t_opr_bih_register a,
//               (SELECT   inpatientid_chr, MAX (modify_dat) AS modify_dat
//                    FROM t_opr_bih_register
//                GROUP BY inpatientid_chr) b
//         WHERE a.inpatientid_chr = b.inpatientid_chr
//           AND a.modify_dat = b.modify_dat) b, t_bse_patientcard c
// WHERE TRIM(a.patientid_chr) = Trim(b.patientid_chr) AND a.patientid_chr = c.patientid_chr
            //修改者：邓光北 2008.7.28
            string strSQL = @"select a.patientid_chr,
                                   a.inpatientid_chr,
                                   a.lastname_vchr,
                                   a.idcard_chr,
                                   a.married_chr,
                                   a.birthplace_vchr,
                                   a.homeaddress_vchr,
                                   a.sex_chr,
                                   a.nationality_vchr,
                                   a.firstname_vchr,
                                   a.birth_dat,
                                   a.race_vchr,
                                   a.nativeplace_vchr,
                                   a.occupation_vchr,
                                   a.name_vchr,
                                   a.homephone_vchr,
                                   a.officephone_vchr,
                                   a.insuranceid_vchr,
                                   a.mobile_chr,
                                   a.officeaddress_vchr,
                                   a.employer_vchr,
                                   a.officepc_vchr,
                                   a.homepc_chr,
                                   a.email_vchr,
                                   a.contactpersonfirstname_vchr,
                                   a.contactpersonlastname_vchr,
                                   a.contactpersonaddress_vchr,
                                   a.contactpersonphone_vchr,
                                   a.contactpersonpc_chr,
                                   a.patientrelation_vchr,
                                   a.firstdate_dat,
                                   a.isemployee_int,
                                   a.status_int,
                                   a.deactivate_dat,
                                   a.operatorid_chr,
                                   a.modify_dat,
                                   a.paytypeid_chr,
                                   a.optimes_int,
                                   a.govcard_chr,
                                   a.bloodtype_chr,
                                   a.ifallergic_int,
                                   a.allergicdesc_vchr,
                                   a.difficulty_vchr,
                                   a.extendid_vchr,
                                   a.inpatienttempid_vchr,
                                   a.modifytime_dat,
                                   a.modifyman_vchr,
                                   a.registertime_dat,
                                   a.registerman_vchr,
                                   b.registerid_chr,
                                   b.modify_dat,
                                   b.patientid_chr,
                                   b.isbooking_int,
                                   b.inpatientid_chr as inpatientno,
                                   b.inpatient_dat,
                                   b.deptid_chr,
                                   b.areaid_chr,
                                   b.bedid_chr,
                                   (select deptname_vchr
                                      from t_bse_deptdesc
                                     where t_bse_deptdesc.deptid_chr = b.areaid_chr) as deptname,
                                   (select paytypename_vchr
                                      from t_bse_patientpaytype
                                     where paytypeid_chr = a.paytypeid_chr) as paytypename,
                                   (select code_chr
                                      from t_bse_bed
                                     where t_bse_bed.bedid_chr = b.bedid_chr) as bedno,
                                   decode(state_int, 1, '危', 2, '急', 3, '普通', '') as sickstatus,
                                   decode(pstatus_int,
                                          0,
                                          '未上床',
                                          1,
                                          '已上床',
                                          2,
                                          '预出院',
                                          3,
                                          '实际出院',
                                          4,
                                          '请假',
                                          '') inhospitalstatus,
                                   decode(type_int, 1, '门诊', 2, '急诊', 3, '他院转入', '') intype,
                                   (select lastname_vchr
                                      from t_bse_employee
                                     where empid_chr = casedoctor_chr) as doctorname,
                                   b.diagnose_vchr,
                                   b.icd10diagtext_vchr,
                                   c.patientcardid_chr
                              from t_bse_patient a,
                                   (select a.registerid_chr,
                                           a.modify_dat,
                                           a.patientid_chr,
                                           a.isbooking_int,
                                           a.inpatientid_chr,
                                           a.inpatient_dat,
                                           a.deptid_chr,
                                           a.areaid_chr,
                                           a.bedid_chr,
                                           a.type_int,
                                           a.diagnose_vchr,
                                           a.icd10diagtext_vchr,
                                           a.status_int,
                                           a.pstatus_int,
                                           a.state_int,
                                           a.casedoctor_chr  
                                      from t_opr_bih_register a,
                                           (select inpatientid_chr, max(modify_dat) as modify_dat
                                              from t_opr_bih_register
                                             group by inpatientid_chr) b
                                     where a.inpatientid_chr = b.inpatientid_chr
                                       and a.modify_dat = b.modify_dat) b,
                                   t_bse_patientcard c
                             where a.patientid_chr = b.patientid_chr
                               and a.patientid_chr = c.patientid_chr";
            string strCondition = "";
			if(p_QueryCondition !=null)
			{
				if(p_QueryCondition.PatientName!="")
				{
					strCondition += " and trim (lower (a.lastname_vchr)) like '%"+p_QueryCondition.PatientName.Trim().ToLower()+"%'";
				}
				if(p_QueryCondition.InhospitalNo!="")
				{
                    strCondition += " and trim (lower (b.inpatientid_chr)) like '%" + p_QueryCondition.InhospitalNo.Trim().ToLower() + "'";
				}
				if(p_QueryCondition.SickCondition!=-1)
				{
					strCondition+=" and b.state_int = "+p_QueryCondition.SickCondition+"";
				}
				if(p_QueryCondition.MainDoctor!="")
				{
                    strCondition += " and trim (lower (b.casedoctor_chr)) = '" + p_QueryCondition.MainDoctor.Trim().ToLower() + "'";
				}
				if(p_QueryCondition.InHospitalContion!=-1)
				{
					strCondition+=" and b.pstatus_int = "+p_QueryCondition.InHospitalContion+"";
				}
				if(p_QueryCondition.AreaID!="")
				{
					strCondition+=" and trim (b.areaid_chr) = '"+p_QueryCondition.AreaID.Trim()+"'";
				}
				if(p_QueryCondition.strProtectType!="")
				{
					strCondition+=" and a.paytypeid_chr = '"+p_QueryCondition.strProtectType.Trim()+"'";
				}
				if(p_QueryCondition.strBeginDate!="")
				{
					strCondition+=@" and b.inpatient_dat between to_date ('"+p_QueryCondition.strBeginDate+@" 00:00:00',
                                        'yyyy-mm-dd hh24:mi:ss'
                                       )
                           and to_date ('"+p_QueryCondition.strEndDate+@" 23:59:59',
                                        'yyyy-mm-dd hh24:mi:ss'
                                       )";
				}
			}
			if(strCondition!="")
			{
				strSQL +=strCondition;
			}
			strSQL += " order by areaid_chr,inhospitalstatus,inpatient_dat";
			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
				lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);
				objHRPSvc.Dispose();
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			return lngRes;
        }

        #region
        [AutoComplete]
		public long m_lngGetMainDoctor(string p_strFilter, out clsEmployee_VO[] p_objResultArr)
		{
			p_objResultArr = new clsEmployee_VO[0];
			long lngRes=0; 
            string strSQL = @"select empid_chr,
                                   begindate_dat,
                                   firstname_vchr,
                                   lastname_vchr,
                                   empidcard_chr,
                                   pycode_chr,
                                   sex_chr,
                                   educationallevel_chr,
                                   maritalstatus_chr,
                                   technicalrank_chr,
                                   languageability_vchr,
                                   birthdate_dat,
                                   officephone_vchr,
                                   homephone_vchr,
                                   mobile_vchr,
                                   officeaddress_vchr,
                                   officezip_chr,
                                   homeaddress_vchr,
                                   homezip_chr,
                                   email_vchr,
                                   contactname_vchr,
                                   contactphone_vchr,
                                   remark_vchr,
                                   status_int,
                                   deactivate_dat,
                                   shortname_chr,
                                   operatorid_chr,
                                   hasprescriptionright_chr,
                                   haspsychosisprescriptionright_,
                                   hasopiateprescriptionright_chr,
                                   isexpert_chr,
                                   expertfee_mny,
                                   isexternalexpert_chr,
                                   ancestoraddr_vchar,
                                   experience_vchr,
                                   psw_chr,
                                   deptcode_chr,
                                   technicallevel_chr,
                                   digitalsign_dta,
                                   extendid_vchr,
                                   isemployee_int,
                                   empno_chr,
                                   employeeidentity_int,
                                   empduty_int
                              from t_bse_employee
                             where status_int = 1
                               and hasprescriptionright_chr = '1'";
			if(p_strFilter!="")
			{
				strSQL+=" and (lastname_vchr like '"+p_strFilter+"%' or pycode_chr like '"+p_strFilter+"%' or empno_chr like '"+p_strFilter+"%')";
			}
			try
			{
				DataTable dtbResult = new DataTable();
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
				lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
				objHRPSvc.Dispose();
				if(lngRes > 0 && dtbResult.Rows.Count > 0)
				{
					p_objResultArr = new clsEmployee_VO[dtbResult.Rows.Count];
					for(int i1=0;i1<p_objResultArr.Length;i1++)
					{
						p_objResultArr[i1] = new clsEmployee_VO();
						p_objResultArr[i1].m_strEMPID_CHR = dtbResult.Rows[i1]["EMPID_CHR"].ToString().Trim();
						if(dtbResult.Rows[i1]["BEGINDATE_DAT"]!=System.DBNull.Value)
						{
							p_objResultArr[i1].m_strBEGINDATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["BEGINDATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
						}
						p_objResultArr[i1].m_strFIRSTNAME_VCHR = dtbResult.Rows[i1]["FIRSTNAME_VCHR"].ToString().Trim();
						p_objResultArr[i1].m_strLASTNAME_VCHR = dtbResult.Rows[i1]["LASTNAME_VCHR"].ToString().Trim();
						p_objResultArr[i1].m_strEMPIDCARD_CHR = dtbResult.Rows[i1]["EMPIDCARD_CHR"].ToString().Trim();
						p_objResultArr[i1].m_strPYCODE_CHR = dtbResult.Rows[i1]["PYCODE_CHR"].ToString().Trim();
						p_objResultArr[i1].m_strSEX_CHR = dtbResult.Rows[i1]["SEX_CHR"].ToString().Trim();
						p_objResultArr[i1].m_strEDUCATIONALLEVEL_CHR = dtbResult.Rows[i1]["EDUCATIONALLEVEL_CHR"].ToString().Trim();
						p_objResultArr[i1].m_strMARITALSTATUS_CHR = dtbResult.Rows[i1]["MARITALSTATUS_CHR"].ToString().Trim();
						p_objResultArr[i1].m_strTECHNICALRANK_CHR = dtbResult.Rows[i1]["TECHNICALRANK_CHR"].ToString().Trim();
						p_objResultArr[i1].m_strLANGUAGEABILITY_VCHR = dtbResult.Rows[i1]["LANGUAGEABILITY_VCHR"].ToString().Trim();
						if(dtbResult.Rows[i1]["BIRTHDATE_DAT"]!=System.DBNull.Value)
						{
							p_objResultArr[i1].m_strBIRTHDATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["BIRTHDATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
						}
						p_objResultArr[i1].m_strOFFICEPHONE_VCHR = dtbResult.Rows[i1]["OFFICEPHONE_VCHR"].ToString().Trim();
						p_objResultArr[i1].m_strHOMEPHONE_VCHR = dtbResult.Rows[i1]["HOMEPHONE_VCHR"].ToString().Trim();
						p_objResultArr[i1].m_strMOBILE_VCHR = dtbResult.Rows[i1]["MOBILE_VCHR"].ToString().Trim();
						p_objResultArr[i1].m_strOFFICEADDRESS_VCHR = dtbResult.Rows[i1]["OFFICEADDRESS_VCHR"].ToString().Trim();
						p_objResultArr[i1].m_strOFFICEZIP_CHR = dtbResult.Rows[i1]["OFFICEZIP_CHR"].ToString().Trim();
						p_objResultArr[i1].m_strHOMEADDRESS_VCHR = dtbResult.Rows[i1]["HOMEADDRESS_VCHR"].ToString().Trim();
						p_objResultArr[i1].m_strHOMEZIP_CHR = dtbResult.Rows[i1]["HOMEZIP_CHR"].ToString().Trim();
						p_objResultArr[i1].m_strEMAIL_VCHR = dtbResult.Rows[i1]["EMAIL_VCHR"].ToString().Trim();
						p_objResultArr[i1].m_strCONTACTNAME_VCHR = dtbResult.Rows[i1]["CONTACTNAME_VCHR"].ToString().Trim();
						p_objResultArr[i1].m_strCONTACTPHONE_VCHR = dtbResult.Rows[i1]["CONTACTPHONE_VCHR"].ToString().Trim();
						p_objResultArr[i1].m_strREMARK_VCHR = dtbResult.Rows[i1]["REMARK_VCHR"].ToString().Trim();
						if(dtbResult.Rows[i1]["STATUS_INT"]!=System.DBNull.Value)
						{
							p_objResultArr[i1].m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["STATUS_INT"].ToString().Trim());
						}
						p_objResultArr[i1].m_strDEACTIVATE_DAT = dtbResult.Rows[i1]["DEACTIVATE_DAT"].ToString().Trim();
						p_objResultArr[i1].m_strSHORTNAME_CHR = dtbResult.Rows[i1]["SHORTNAME_CHR"].ToString().Trim();
						p_objResultArr[i1].m_strOPERATORID_CHR = dtbResult.Rows[i1]["OPERATORID_CHR"].ToString().Trim();
						p_objResultArr[i1].m_strHASPRESCRIPTIONRIGHT_CHR = dtbResult.Rows[i1]["HASPRESCRIPTIONRIGHT_CHR"].ToString().Trim();
						p_objResultArr[i1].m_strHASPSYCHOSISPRESCRIPTIONRIGHT_ = dtbResult.Rows[i1]["HASPSYCHOSISPRESCRIPTIONRIGHT_"].ToString().Trim();
						p_objResultArr[i1].m_strHASOPIATEPRESCRIPTIONRIGHT_CHR = dtbResult.Rows[i1]["HASOPIATEPRESCRIPTIONRIGHT_CHR"].ToString().Trim();
						p_objResultArr[i1].m_strISEXPERT_CHR = dtbResult.Rows[i1]["ISEXPERT_CHR"].ToString().Trim();
						if(dtbResult.Rows[i1]["EXPERTFEE_MNY"]!=System.DBNull.Value)
						{
							p_objResultArr[i1].m_fltEXPERTFEE_MNY = Convert.ToInt32(dtbResult.Rows[i1]["EXPERTFEE_MNY"].ToString().Trim());
						}
						p_objResultArr[i1].m_strISEXTERNALEXPERT_CHR = dtbResult.Rows[i1]["ISEXTERNALEXPERT_CHR"].ToString().Trim();
						p_objResultArr[i1].m_strANCESTORADDR_VCHAR = dtbResult.Rows[i1]["ANCESTORADDR_VCHAR"].ToString().Trim();
						p_objResultArr[i1].m_strEXPERIENCE_VCHR = dtbResult.Rows[i1]["EXPERIENCE_VCHR"].ToString().Trim();
						p_objResultArr[i1].m_strEMPNO_CHR = dtbResult.Rows[i1]["EMPNO_CHR"].ToString().Trim();
						p_objResultArr[i1].m_strPSW_CHR = dtbResult.Rows[i1]["PSW_CHR"].ToString().Trim();
						p_objResultArr[i1].m_strDEPTCODE_CHR = dtbResult.Rows[i1]["DEPTCODE_CHR"].ToString().Trim();
					}
				}
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			return lngRes;
		}
		#endregion

        #region 根据查询条件取病人信息 2006-11-2
        /// <summary>
        /// 根据查询条件取病人信息
        /// </summary>
        [AutoComplete]
        public long GetPatientInfoByCondition(clsPatientQueryCondition p_QueryCondition, out DataTable p_dtbResult)
        {
            p_dtbResult = new DataTable();
            long lngRes = 0; 
            string strSQL = @"select f.registerid_chr,
                                     f.inpatientid_chr,   
                                     b.deptid_chr,   
                                     b.code_vchr,   
                                     b.deptname_vchr,   
                                     c.code_chr,   
                                     a.lastname_vchr,   
                                     a.sex_chr,   
                                     a.birth_dat,   
                                     f.paytypeid_chr,   
                                     d.paytypename_vchr,   
                                     f.pstatus_int,   
                                     f.state_int,   
                                     f.casedoctor_chr,   
                                     e.lastname_vchr doctor_name,   
                                     f.diagnose_vchr,  
                                    g.patientcardid_chr,
                                    (select i.name_chr from t_opr_bih_patientnurse h, t_bse_bih_orderdic i 
                                         where h.registerid_chr = f.registerid_chr and 
                                               h.orderdicid_chr = i.orderdicid_chr and 
                                               h.type_int = 1 and
                                               h.active_int = 1 ) nursing_class,
                                    (select i.name_chr from t_opr_bih_patientnurse h, t_bse_bih_orderdic i 
                                         where h.registerid_chr = f.registerid_chr and 
                                               h.orderdicid_chr = i.orderdicid_chr and 
                                               h.type_int = 2 and
                                               h.active_int = 1 ) eat_class
                                from   
                                     t_opr_bih_registerdetail a,   
                                     t_bse_deptdesc b,   
                                     t_bse_bed c,   
                                     t_bse_patientpaytype d,   
                                     t_bse_employee e,
                                     t_bse_patientcard g,                                                                       
                                     t_opr_bih_register f  
                               where ( f.registerid_chr = a.registerid_chr ) and  
                                     ( f.areaid_chr = b.deptid_chr(+) ) and  
                                     ( f.bedid_chr = c.bedid_chr(+) ) and  
                                     ( f.paytypeid_chr = d.paytypeid_chr(+) ) and  
                                     ( f.casedoctor_chr = e.empid_chr(+) ) and
                                     (f.patientid_chr = g.patientid_chr (+)) and
                                     (g.status_int = 1 or g.status_int = 3) and
                                     ( f.status_int = 1) ";
            string strCondition = "";
            if (p_QueryCondition != null)
            {
                if (p_QueryCondition.PatientName != "")
                {
                    strCondition += " and trim (lower (a.lastname_vchr)) like '%" + p_QueryCondition.PatientName.Trim().ToLower() + "%'";
                }

                if (p_QueryCondition.InhospitalNo != "")
                {
                    strCondition += " and trim (lower (f.inpatientid_chr)) like '%" + p_QueryCondition.InhospitalNo.Trim().ToLower() + "'";
                }

                if (p_QueryCondition.SickCondition != -1)
                {
                    strCondition += " and f.state_int = " + p_QueryCondition.SickCondition + "";
                }

                if (p_QueryCondition.MainDoctor != "")
                {
                    strCondition += " and f.casedoctor_chr = '" + p_QueryCondition.MainDoctor.Trim() + "'";
                }

                if (p_QueryCondition.InHospitalContion != -1)
                {
                    strCondition += " and f.pstatus_int = " + p_QueryCondition.InHospitalContion + "";
                }

                if (p_QueryCondition.AreaID != "")
                {
                    strCondition += " and f.areaid_chr = '" + p_QueryCondition.AreaID.Trim() + "'";
                }

                if (p_QueryCondition.strProtectType != "")
                {
                    strCondition += " and f.PAYTYPEID_CHR = '" + p_QueryCondition.strProtectType.Trim() + "'";
                }

                if (p_QueryCondition.strBeginDate != "")
                {
                    strCondition += @" and f.inpatient_dat between to_date ('" + p_QueryCondition.strBeginDate + @" 00:00:00',
                                        'yyyy-mm-dd hh24:mi:ss'
                                       )
                           and to_date ('" + p_QueryCondition.strEndDate + @" 23:59:59',
                                        'yyyy-mm-dd hh24:mi:ss'
                                       )";
                }

                if (p_QueryCondition.strCardNo != "")
                {
                    strCondition += " and g.patientcardid_chr = '" + p_QueryCondition.strCardNo.Trim() + "'";
                }

            }
            if (strCondition != "")
            {
                strSQL += strCondition;
            }
            strSQL += " order by f.inpatientid_chr, f.inpatient_dat,b.code_vchr";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);
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
