using System;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.EnterpriseServices;
using System.Data;


namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// ȡ�����ݿ��һЩ������Ϣ������ң�ҽ��
    /// </summary>
    //	[Transaction(TransactionOption.Required)]
    //	[ObjectPooling(Enabled=true)]
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsGetBase : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        public clsGetBase()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #region ȡ�����еĿ��һ�����Ϣ
        /// <summary>
        /// ȡ�����еĿ��һ�����Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strDepID"></param>
        /// <param name="objDep"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeptList(string strDepID, out clsDepartmentVO[] objDep)
        {
            objDep = new clsDepartmentVO[0];
            long lngRes = 0;
            string strSQL = "select  deptid_chr, modify_dat, deptname_vchr, category_int, inpatientoroutpatient_int, operatorid_chr, address_vchr, pycode_chr, attributeid, parentid, createdate_dat, status_int, deactivate_dat, wbcode_chr, code_vchr, extendid_vchr, shortno_chr, stdbed_count_int, putmed_int, usercode_vchr, expertdeptflag_int from t_bse_deptdesc";
            if (strDepID != null)
                strSQL = strSQL + " And deptid_chr='" + strDepID + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    objDep = new clsDepartmentVO[dtbResult.Rows.Count];

                    for (int i1 = 0; i1 < objDep.Length; i1++)
                    {
                        objDep[i1] = new clsDepartmentVO();
                        objDep[i1].strDeptID = dtbResult.Rows[i1]["DEPTID_CHR"].ToString().Trim();
                        objDep[i1].strDeptName = dtbResult.Rows[i1]["DEPTNAME_VCHR"].ToString().Trim();
                        objDep[i1].strCreateDate = dtbResult.Rows[i1]["CREATEDATE_DAT"].ToString().Trim();
                        if (dtbResult.Rows[i1]["STATUS_INT"].ToString().Trim() != "")
                            objDep[i1].intStatus = int.Parse(dtbResult.Rows[i1]["STATUS_INT"].ToString().Trim());
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

        #region ȡ��Ա���Ļ�����Ϣ
        /// <summary>
        /// ��������ȡ��Ա���Ļ�����Ϣ
        /// strEmpID��strDepID��Ϊ��ʱ����ȫ����
        /// strDepID��Ϊ�ղ���strEmpIDΪ�շ���Ҫ����ҵ�Ա��
        /// �������strEmpID���Ҽ�¼
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strEmpID"></param>
        /// <param name="strDepID"></param>
        /// <param name="objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetEmployeeList(string strEmpID, string strDepID, out clsEmployeeVO[] objResult)
        {
            objResult = new clsEmployeeVO[0];
            long lngRes = 0;
            string strSQL = "";
            if (strDepID == null && strEmpID == null)
                strSQL = @"select empid_chr, begindate_dat, firstname_vchr, lastname_vchr, empidcard_chr, pycode_chr, sex_chr, educationallevel_chr, maritalstatus_chr, technicalrank_chr, languageability_vchr, birthdate_dat, officephone_vchr, homephone_vchr, mobile_vchr, officeaddress_vchr, officezip_chr, homeaddress_vchr, homezip_chr, email_vchr, contactname_vchr, contactphone_vchr, remark_vchr, status_int, deactivate_dat, shortname_chr, operatorid_chr, hasprescriptionright_chr, haspsychosisprescriptionright_, hasopiateprescriptionright_chr, isexpert_chr, expertfee_mny, isexternalexpert_chr, ancestoraddr_vchar, experience_vchr, psw_chr, deptcode_chr, technicallevel_chr, digitalsign_dta, extendid_vchr, isemployee_int, empno_chr, employeeidentity_int, empduty_int from t_bse_employee ";
            if (strEmpID != null) //ȡ��һԱ��
                strSQL = @"select empid_chr, begindate_dat, firstname_vchr, lastname_vchr, empidcard_chr, pycode_chr, sex_chr, educationallevel_chr, maritalstatus_chr, technicalrank_chr, languageability_vchr, birthdate_dat, officephone_vchr, homephone_vchr, mobile_vchr, officeaddress_vchr, officezip_chr, homeaddress_vchr, homezip_chr, email_vchr, contactname_vchr, contactphone_vchr, remark_vchr, status_int, deactivate_dat, shortname_chr, operatorid_chr, hasprescriptionright_chr, haspsychosisprescriptionright_, hasopiateprescriptionright_chr, isexpert_chr, expertfee_mny, isexternalexpert_chr, ancestoraddr_vchar, experience_vchr, psw_chr, deptcode_chr, technicallevel_chr, digitalsign_dta, extendid_vchr, isemployee_int, empno_chr, employeeidentity_int, empduty_int from t_bse_employee Where EMPID_CHR='" + strEmpID + "'";
            else
            {
                if (strDepID != null) //ȡ��һԱ��
                    strSQL = @"SELECT a.empid_chr, a.begindate_dat, a.firstname_vchr, a.lastname_vchr, a.empidcard_chr, 
a.pycode_chr, a.sex_chr, a.educationallevel_chr, a.maritalstatus_chr, a.technicalrank_chr,
 a.languageability_vchr, a.birthdate_dat, a.officephone_vchr, a.homephone_vchr, a.mobile_vchr,
 a.officeaddress_vchr, a.officezip_chr, a.homeaddress_vchr, a.homezip_chr, a.email_vchr,
 a.contactname_vchr, a.contactphone_vchr, a.remark_vchr, a.status_int, a.deactivate_dat, 
a.shortname_chr, a.operatorid_chr, a.hasprescriptionright_chr, a.haspsychosisprescriptionright_, 
a.hasopiateprescriptionright_chr, a.isexpert_chr, a.expertfee_mny, a.isexternalexpert_chr, 
a.ancestoraddr_vchar, a.experience_vchr, a.psw_chr, a.deptcode_chr, a.technicallevel_chr, 
a.digitalsign_dta, a.extendid_vchr, a.isemployee_int, a.empno_chr, a.employeeidentity_int,
 a.empduty_int  from t_bse_employee a,t_bse_deptemp b  " +
                        " Where a.empid_chr=b.empid_chr And b.deptid_chr='" + strDepID + "' ";
            }
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    objResult = new clsEmployeeVO[dtbResult.Rows.Count];

                    for (int i1 = 0; i1 < objResult.Length; i1++)
                    {
                        objResult[i1] = new clsEmployeeVO();
                        if (dtbResult.Rows[i1]["STATUS_INT"].ToString().Trim() != "")
                            objResult[i1].intStatus = int.Parse(dtbResult.Rows[i1]["STATUS_INT"].ToString().Trim());
                        objResult[i1].strBeginDate = dtbResult.Rows[i1]["BEGINDATE_DAT"].ToString().Trim();
                        objResult[i1].strBirthDate = dtbResult.Rows[i1]["BIRTHDATE_DAT"].ToString().Trim();
                        //objResult[i1].strBirthPlace=dtbResult.Rows[i1][""].ToString().Trim();
                        //objResult[i1].strContactAddress=dtbResult.Rows[i1][""].ToString().Trim();
                        objResult[i1].strContactName = dtbResult.Rows[i1]["CONTACTNAME_VCHR"].ToString().Trim();
                        objResult[i1].strContactPhone = dtbResult.Rows[i1]["CONTACTPHONE_VCHR"].ToString().Trim();
                        //objResult[i1].strContactRelation=dtbResult.Rows[i1][""].ToString().Trim();
                        //objResult[i1].strContactZip=dtbResult.Rows[i1][""].ToString().Trim();
                        objResult[i1].strDeactiveDate = dtbResult.Rows[i1]["DEACTIVATE_DAT"].ToString().Trim();
                        objResult[i1].strEducationalLevel = dtbResult.Rows[i1]["EDUCATIONALLEVEL_CHR"].ToString().Trim();
                        objResult[i1].strEMail = dtbResult.Rows[i1]["EMAIL_VCHR"].ToString().Trim();
                        objResult[i1].strEmpID = dtbResult.Rows[i1]["EMPID_CHR"].ToString().Trim();
                        objResult[i1].strEmpIDCard = dtbResult.Rows[i1]["EMPIDCARD_CHR"].ToString().Trim();
                        objResult[i1].strExperience = dtbResult.Rows[i1]["EXPERIENCE_VCHR"].ToString().Trim();
                        objResult[i1].strExpertFee = dtbResult.Rows[i1]["EXPERTFEE_MNY"].ToString().Trim();
                        objResult[i1].strFirstName = dtbResult.Rows[i1]["FIRSTNAME_VCHR"].ToString().Trim();
                        objResult[i1].strHasOpiatePrescriptionRight = dtbResult.Rows[i1]["HASOPIATEPRESCRIPTIONRIGHT_CHR"].ToString().Trim();
                        objResult[i1].strHasPrescriptionRight = dtbResult.Rows[i1]["HASPRESCRIPTIONRIGHT_CHR"].ToString().Trim();
                        objResult[i1].strHomeAddress = dtbResult.Rows[i1]["HOMEADDRESS_VCHR"].ToString().Trim();
                        objResult[i1].strHomePhone = dtbResult.Rows[i1]["HOMEPHONE_VCHR"].ToString().Trim();
                        objResult[i1].strHomeZip = dtbResult.Rows[i1]["HOMEZIP_CHR"].ToString().Trim();
                        objResult[i1].strIsExpert = dtbResult.Rows[i1]["ISEXPERT_CHR"].ToString().Trim();
                        objResult[i1].strIsExternalExpert = dtbResult.Rows[i1]["ISEXTERNALEXPERT_CHR"].ToString().Trim();
                        //objResult[i1].strLanguageAbility=dtbResult.Rows[i1][""].ToString().Trim();
                        objResult[i1].strLastName = dtbResult.Rows[i1]["LASTNAME_VCHR"].ToString().Trim();
                        objResult[i1].strMaritalStatus = dtbResult.Rows[i1]["MARITALSTATUS_CHR"].ToString().Trim();
                        objResult[i1].strMobile = dtbResult.Rows[i1]["MOBILE_VCHR"].ToString().Trim();
                        objResult[i1].strName = dtbResult.Rows[i1]["LASTNAME_VCHR"].ToString().Trim();
                        //objResult[i1].strNationality=dtbResult.Rows[i1][""].ToString().Trim();
                        //objResult[i1].strNativePlace=dtbResult.Rows[i1][""].ToString().Trim();
                        objResult[i1].strOfficeAddress = dtbResult.Rows[i1]["OFFICEADDRESS_VCHR"].ToString().Trim();
                        objResult[i1].strOfficePhone = dtbResult.Rows[i1]["OFFICEPHONE_VCHR"].ToString().Trim();
                        objResult[i1].strOfficeZip = dtbResult.Rows[i1]["OFFICEZIP_CHR"].ToString().Trim();
                        //objResult[i1].strOperatorID=dtbResult.Rows[i1][""].ToString().Trim();
                        objResult[i1].strPYCode = dtbResult.Rows[i1]["PYCODE_CHR"].ToString().Trim();
                        //objResult[i1].strRace=dtbResult.Rows[i1][""].ToString().Trim();
                        objResult[i1].strRemark = dtbResult.Rows[i1]["REMARK_VCHR"].ToString().Trim();
                        objResult[i1].strSex = dtbResult.Rows[i1]["SEX_CHR"].ToString().Trim();
                        objResult[i1].strShortName = dtbResult.Rows[i1]["SHORTNAME_CHR"].ToString().Trim();
                        objResult[i1].strTechnicalRank = dtbResult.Rows[i1]["TECHNICALRANK_CHR"].ToString().Trim();
                        objResult[i1].strEmpNO = dtbResult.Rows[i1]["empno_chr"].ToString().Trim();
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

        #region ����һ�����ҵ���Ϣ 
        [AutoComplete]
        public long m_lngGetOPDeptForPlan(out System.Data.DataTable dt)
        {

            dt = new DataTable();
            long lngRes = 0;
            //			string strSQL="Select a.OPDEPT_CHR,b.DEPTNAME_VCHR From t_opr_OPDoctorPlan a,t_bse_DeptBaseInfo b " +
            //                          " Where a.PLANDATE_DAT=to_date('"+strDate+"','yyyy-mm-dd') And a.planperiod_chr='"+strPerio+"' And " +
            //                          "a.OPDEPT_CHR=b.DEPTID_CHR(+) Group By a.OPDEPT_CHR,b.DEPTNAME_VCHR ";
            string strSQL = @"select * from T_BSE_DEPTDESC where INPATIENTOROUTPATIENT_INT=0";
            //			DateTime sDate=Convert.ToDateTime(strDate);
            //			System.Data.IDataParameter[] objPar=clsIDataParameterCreator.s_objConstructIDataParameterArr(new object[]{sDate,strPerio});
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    dt = dtbResult;
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
        #region ���ݸ�����ȡ���ӿ��ҵ���Ϣ
        /// <summary>
        /// ���ݸ����ҵ�IDȡ�����ӿ���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strDepID"></param>
        /// <param name="objDep"></param>
        /// <returns></returns>
        [AutoComplete]
        public long lng_getChildrenDep(string strDepID, out clsDepartmentVO[] objDep)
        {
            objDep = new clsDepartmentVO[0];
            long lngRes = 0;
            string strSQL = "Select b.deptid_chr,a.Deptname_VCHR,b.levels_int from T_BSE_DEPTDESC a,T_BSE_DEPTANDDEPT b " +
                          " Where b.parentdeptid_chr='" + strDepID + "' and b.deptid_chr=a.deptid_chr(+) ";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    objDep = new clsDepartmentVO[dtbResult.Rows.Count];

                    for (int i1 = 0; i1 < objDep.Length; i1++)
                    {
                        objDep[i1] = new clsDepartmentVO();
                        objDep[i1].strDeptID = dtbResult.Rows[i1]["deptid_chr"].ToString().Trim();
                        objDep[i1].strDeptName = dtbResult.Rows[i1]["Deptname_VCHR"].ToString().Trim();
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

        #region ��ѯ���е�λ
        /// <summary>
        /// ��ѯ���е�λ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindAllUnit(out clsUnit_VO[] p_objResultArr)
        {
            p_objResultArr = new clsUnit_VO[0];
            long lngRes = 0;
            DataTable dtbResult = new DataTable();
            string strSQL = "SELECT * FROM T_AID_UNIT ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsUnit_VO[dtbResult.Rows.Count];

                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsUnit_VO();
                        p_objResultArr[i1].m_strUnitID = dtbResult.Rows[i1]["UNITID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUnitName = dtbResult.Rows[i1]["UNITNAME_CHR"].ToString().Trim();
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��ѯ�����÷�
        /// <summary>
        /// ��ѯ�����÷�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindAllUsage(out clsUsageType_VO[] p_objResultArr, string strEx)
        {
            p_objResultArr = new clsUsageType_VO[0];
            long lngRes = 0;
            DataTable dtbResult = new DataTable();
            string strSQL = "SELECT *  FROM t_bse_usagetype  WHERE usercode_chr LIKE '" + strEx + "%' OR pycode_vchr LIKE '" + strEx + "%' OR wbcode_vchr LIKE '" + strEx + "%' ORDER BY scope_int, usercode_chr";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsUsageType_VO[dtbResult.Rows.Count];

                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsUsageType_VO();
                        p_objResultArr[i1].m_intScope = Convert.ToInt32(dtbResult.Rows[i1]["scope_int"].ToString());
                        p_objResultArr[i1].m_strUsageID = dtbResult.Rows[i1]["USAGEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUsageName = dtbResult.Rows[i1]["USAGENAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUsageCode = dtbResult.Rows[i1]["USERCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUsagePYCODE = dtbResult.Rows[i1]["PYCODE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUsageWBCODE = dtbResult.Rows[i1]["WBCODE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_intPutMed = int.Parse(dtbResult.Rows[i1]["PUTMED_INT"].ToString().Trim()) == 0 ? 0 : 1;
                        p_objResultArr[i1].m_intTest = int.Parse(dtbResult.Rows[i1]["TEST_INT"].ToString().Trim()) == 0 ? 0 : 1;
                        p_objResultArr[i1].m_strOPUsageDesc = dtbResult.Rows[i1]["OPUSAGEDESC"].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��ѯ���е���t_bse_nurseorder
        /// <summary>
        /// ��ѯ�����÷�t_bse_nurseorder
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindAllORDERIDFromT_bse_nurseorder(out clsUsageType_VO[] p_objResultArr)
        {
            p_objResultArr = new clsUsageType_VO[0];
            long lngRes = 0;
            DataTable dtbResult = new DataTable();
            string strSQL = "SELECT *  FROM t_bse_nurseorder  ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsUsageType_VO[dtbResult.Rows.Count];

                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsUsageType_VO();
                        p_objResultArr[i1].m_strUsageID = dtbResult.Rows[i1]["ORDERID_INT"].ToString().Trim();
                        p_objResultArr[i1].m_strUsageName = dtbResult.Rows[i1]["ORDERNAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_intFlag = int.Parse(dtbResult.Rows[i1]["FLAG_INT"].ToString().Trim());
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion



        #region ��ѯ�����÷�����
        /// <summary>
        /// ��ѯ�����÷�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindAllUsageSet(out clsUsageType_VO[] p_objResultArr)
        {
            p_objResultArr = new clsUsageType_VO[0];
            long lngRes = 0;
            DataTable dtbResult = new DataTable();
            string strSQL = "SELECT   distinct  t1.usageid_chr, t2.usercode_chr, t2.usagename_vchr, t1.type_int  " +
                            " FROM t_opr_setusage t1, t_bse_usagetype t2 " +
                            "where t1.usageid_chr=t2.usageid_chr " +
                            "ORDER BY t1.usageid_chr ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsUsageType_VO[dtbResult.Rows.Count];

                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsUsageType_VO();
                        p_objResultArr[i1].m_strUsageID = dtbResult.Rows[i1]["USAGEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUsageName = dtbResult.Rows[i1]["USAGENAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUsageCode = dtbResult.Rows[i1]["USERCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUsageType = dtbResult.Rows[i1]["type_int"].ToString().Trim();
                        //	p_objResultArr[i1].m_strorderid = dtbResult.Rows[i1]["orderid_vchr"].ToString().Trim();
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region  �����÷���Ŀ���� 	�Ź���	2005-2-21

        [AutoComplete]
        public long m_lngDoAddNewUsageType(string p_usageID, string p_usageType)
        {
            long lngRes = 0;

            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            //����һ���ļƻ���
            if (lngRes < 0)
                return lngRes;

            string strSQL = "INSERT INTO t_opr_setusage (USAGEID_CHR,TYPE_INT) VALUES ('" + p_usageID + "'," + p_usageType + ")";
            try
            {
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

        #region  ɾ���÷���Ŀ���� 	�Ź���	2005-2-21

        [AutoComplete]
        public long m_lngDelUsageSet(string p_usageID)
        {
            long lngRes = 0;

            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            //����һ���ļƻ���
            if (lngRes < 0)
                return lngRes;

            string strSQL = "DELETE      t_opr_setusage " +
                  "WHERE usageid_chr = '" + p_usageID + "'";
            try
            {
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


    }
}
