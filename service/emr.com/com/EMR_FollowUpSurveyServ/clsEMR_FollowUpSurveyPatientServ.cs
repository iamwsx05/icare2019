using System;
using System.Collections.Generic;
using System.Text;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using System.Data;
using System.Security;
using weCare.Core.Entity;

namespace com.digitalwave.emr.EMR_FollowUpSurveyServ
{
    /// <summary>
    /// 获取/修改随访病人基本信息
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsEMR_FollowUpSurveyPatientServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 获取随访病人基本信息
        /// <summary>
        /// 获取随访病人基本信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">入院登记流水号</param>
        /// <param name="p_objPatient">病人信息</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientInfo(string p_strRegisterID, out clsEMR_FollowUpSurveyPatient p_objPatient)
        {
            p_objPatient = null;
            if (string.IsNullOrEmpty(p_strRegisterID))
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                #region 查询随访病人基本信息表

                string strSQL = @"select t.REGISTERID_CHR,
       t.LASTNAME_VCHR,
       t.IDCARD_CHR,
       t.MARRIED_CHR,
       t.BIRTHPLACE_VCHR,
       t.HOMEADDRESS_VCHR,
       t.SEX_CHR,
       t.NATIONALITY_VCHR,
       t.BIRTH_DAT,
       t.RACE_VCHR,
       t.NATIVEPLACE_VCHR,
       t.OCCUPATION_VCHR,
       t.HOMEPHONE_VCHR,
       t.OFFICEPHONE_VCHR,
       t.MOBILE_CHR,
       t.OFFICEADDRESS_VCHR,
       t.EMPLOYER_VCHR,
       t.OFFICEPC_VCHR,
       t.HOMEPC_CHR,
       t.EMAIL_VCHR,
       t.CONTACTPERSONFIRSTNAME_VCHR,
       t.CONTACTPERSONLASTNAME_VCHR,
       t.CONTACTPERSONADDRESS_VCHR,
       t.CONTACTPERSONPHONE_VCHR,
       t.CONTACTPERSONPC_CHR,
       t.PATIENTRELATION_VCHR,
       t.OPERATORID_CHR,
       t.MODIFY_DAT,
       t.OPTIMES_INT,
       t.BLOODTYPE_CHR,
       t.IFALLERGIC_INT,
       t.ALLERGICDESC_VCHR,
       t.ISOUTPATIENT
  from T_EMR_FOLLOWUPSURVEYPATIENT t
  where t.registerid_chr = ?";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //赋值

                objDPArr[0].Value = p_strRegisterID;

                DataTable dtbResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);
                #endregion

                if (lngRes <= 0 || dtbResult == null || dtbResult.Rows.Count <= 0)
                {
                    #region 入院登记明细信息表

                    strSQL = @"select t.REGISTERID_CHR,
       t.LASTNAME_VCHR,
       t.IDCARD_CHR,
       t.MARRIED_CHR,
       t.BIRTHPLACE_VCHR,
       t.HOMEADDRESS_VCHR,
       t.SEX_CHR,
       t.NATIONALITY_VCHR,
       t.BIRTH_DAT,
       t.RACE_VCHR,
       t.NATIVEPLACE_VCHR,
       t.OCCUPATION_VCHR,
       t.HOMEPHONE_VCHR,
       t.OFFICEPHONE_VCHR,
       t.MOBILE_CHR,
       t.OFFICEADDRESS_VCHR,
       t.EMPLOYER_VCHR,
       t.OFFICEPC_VCHR,
       t.HOMEPC_CHR,
       t.EMAIL_VCHR,
       t.CONTACTPERSONFIRSTNAME_VCHR,
       t.CONTACTPERSONLASTNAME_VCHR,
       t.CONTACTPERSONADDRESS_VCHR,
       t.CONTACTPERSONPHONE_VCHR,
       t.CONTACTPERSONPC_CHR,
       t.PATIENTRELATION_VCHR,
       t.OPERATORID_CHR,
       t.MODIFY_DAT,
       t.OPTIMES_INT,
       t.BLOODTYPE_CHR,
       t.IFALLERGIC_INT,
       t.ALLERGICDESC_VCHR,
       -1 ISOUTPATIENT
  from T_OPR_BIH_REGISTERDETAIL t
  where t.registerid_chr = ?";

                    IDataParameter[] objDPArr1 = null;
                    objHRPServ.CreateDatabaseParameter(1, out objDPArr1);
                    //赋值

                    objDPArr1[0].Value = p_strRegisterID;

                    lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr1);
                    #endregion
                }

                if (lngRes > 0 && dtbResult != null)
                {
                    int rowsCount = dtbResult.Rows.Count;
                    if (rowsCount <= 0)
                    {
                        return -1;
                    }

                    DataRow drRow = dtbResult.Rows[0];
                    p_objPatient = new clsEMR_FollowUpSurveyPatient();
                    p_objPatient.m_strREGISTERID = drRow["REGISTERID_CHR"].ToString();
                    p_objPatient.m_strLASTNAME_VCHR = drRow["LASTNAME_VCHR"].ToString();
                    p_objPatient.m_strIDCARD_CHR = drRow["IDCARD_CHR"].ToString();
                    p_objPatient.m_strMARRIED_CHR = drRow["MARRIED_CHR"].ToString();
                    p_objPatient.m_strBIRTHPLACE_VCHR = drRow["BIRTHPLACE_VCHR"].ToString();
                    p_objPatient.m_strHOMEADDRESS_VCHR = drRow["HOMEADDRESS_VCHR"].ToString();
                    p_objPatient.m_strSEX_CHR = drRow["SEX_CHR"].ToString();
                    p_objPatient.m_strNATIONALITY_VCHR = drRow["NATIONALITY_VCHR"].ToString();
                    DateTime dtTemp = DateTime.MinValue;
                    if (DateTime.TryParse(drRow["BIRTH_DAT"].ToString(), out dtTemp))
                    {
                        p_objPatient.m_dtmBIRTH_DAT = dtTemp;
                    }
                    else
                    {
                        p_objPatient.m_dtmBIRTH_DAT = DateTime.MinValue;
                    }
                    p_objPatient.m_strRACE_VCHR = drRow["RACE_VCHR"].ToString();
                    p_objPatient.m_strNATIVEPLACE_VCHR = drRow["NATIVEPLACE_VCHR"].ToString();
                    p_objPatient.m_strOCCUPATION_VCHR = drRow["OCCUPATION_VCHR"].ToString();
                    p_objPatient.m_strHOMEPHONE_VCHR = drRow["HOMEPHONE_VCHR"].ToString();
                    p_objPatient.m_strOFFICEPHONE_VCHR = drRow["OFFICEPHONE_VCHR"].ToString();
                    p_objPatient.m_strMOBILE_CHR = drRow["MOBILE_CHR"].ToString();
                    p_objPatient.m_strOFFICEADDRESS_VCHR = drRow["OFFICEADDRESS_VCHR"].ToString();
                    p_objPatient.m_strEMPLOYER_VCHR = drRow["EMPLOYER_VCHR"].ToString();
                    p_objPatient.m_strOFFICEPC_VCHR = drRow["OFFICEPC_VCHR"].ToString();
                    p_objPatient.m_strHOMEPC_CHR = drRow["HOMEPC_CHR"].ToString();
                    p_objPatient.m_strEMAIL_VCHR = drRow["EMAIL_VCHR"].ToString();
                    p_objPatient.m_strCONTACTPERSONFIRSTNAME_VCHR = drRow["CONTACTPERSONFIRSTNAME_VCHR"].ToString();
                    p_objPatient.m_strCONTACTPERSONLASTNAME_VCHR = drRow["CONTACTPERSONLASTNAME_VCHR"].ToString();
                    p_objPatient.m_strCONTACTPERSONADDRESS_VCHR = drRow["CONTACTPERSONADDRESS_VCHR"].ToString();
                    p_objPatient.m_strCONTACTPERSONPHONE_VCHR = drRow["CONTACTPERSONPHONE_VCHR"].ToString();
                    p_objPatient.m_strCONTACTPERSONPC_CHR = drRow["CONTACTPERSONPC_CHR"].ToString();
                    p_objPatient.m_strPATIENTRELATION_VCHR = drRow["PATIENTRELATION_VCHR"].ToString();
                    p_objPatient.m_strOPERATORID_CHR = drRow["OPERATORID_CHR"].ToString();
                    if (DateTime.TryParse(drRow["MODIFY_DAT"].ToString(), out dtTemp))
                    {
                        p_objPatient.m_dtmMODIFY_DAT = dtTemp;
                    }
                    else
                    {
                        p_objPatient.m_dtmMODIFY_DAT = DateTime.MinValue;
                    }
                    int intTemp = 0;
                    if (int.TryParse(drRow["OPTIMES_INT"].ToString(), out intTemp))
                    {
                        p_objPatient.m_intOPTIMES_INT = intTemp;
                    }
                    else
                    {
                        p_objPatient.m_intOPTIMES_INT = 0;
                    }
                    p_objPatient.m_strBLOODTYPE_CHR = drRow["BLOODTYPE_CHR"].ToString();
                    if (int.TryParse(drRow["IFALLERGIC_INT"].ToString(), out intTemp))
                    {
                        p_objPatient.m_intIFALLERGIC_INT = intTemp;
                    }
                    else
                    {
                        p_objPatient.m_intIFALLERGIC_INT = 2;
                    }
                    p_objPatient.m_strALLERGICDESC_VCHR = drRow["ALLERGICDESC_VCHR"].ToString();
                    p_objPatient.m_intISOUTPATIENT = Convert.ToInt32(drRow["ISOUTPATIENT"]);
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

        #region 新添病人信息至随访病人信息表
        /// <summary>
        /// 新添病人信息至随访病人信息表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objPatient">病人信息</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewPatientInfo(clsEMR_FollowUpSurveyPatient p_objPatient)
        {
            if (p_objPatient == null)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"INSERT INTO T_EMR_FOLLOWUPSURVEYPATIENT (REGISTERID_CHR,LASTNAME_VCHR,IDCARD_CHR,MARRIED_CHR,BIRTHPLACE_VCHR,HOMEADDRESS_VCHR,SEX_CHR,
NATIONALITY_VCHR,FIRSTNAME_VCHR,BIRTH_DAT,RACE_VCHR,NATIVEPLACE_VCHR,OCCUPATION_VCHR,NAME_VCHR,HOMEPHONE_VCHR,OFFICEPHONE_VCHR,MOBILE_CHR,OFFICEADDRESS_VCHR,EMPLOYER_VCHR,
OFFICEPC_VCHR,HOMEPC_CHR,EMAIL_VCHR,CONTACTPERSONFIRSTNAME_VCHR,CONTACTPERSONLASTNAME_VCHR,CONTACTPERSONADDRESS_VCHR,CONTACTPERSONPHONE_VCHR,CONTACTPERSONPC_CHR,
PATIENTRELATION_VCHR,STATUS_INT,OPERATORID_CHR,MODIFY_DAT,OPTIMES_INT,BLOODTYPE_CHR,IFALLERGIC_INT,ALLERGICDESC_VCHR,ISOUTPATIENT,ISEMPLOYEE_INT) 
VALUES (?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,0)";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(36, out objDPArr);
                //赋值

                objDPArr[0].Value = p_objPatient.m_strREGISTERID;
                objDPArr[1].Value = p_objPatient.m_strLASTNAME_VCHR;
                objDPArr[2].Value = p_objPatient.m_strIDCARD_CHR;
                objDPArr[3].Value = p_objPatient.m_strMARRIED_CHR;
                objDPArr[4].Value = p_objPatient.m_strBIRTHPLACE_VCHR;
                objDPArr[5].Value = p_objPatient.m_strHOMEADDRESS_VCHR;
                objDPArr[6].Value = p_objPatient.m_strSEX_CHR;
                objDPArr[7].Value = p_objPatient.m_strNATIONALITY_VCHR;
                objDPArr[8].Value = p_objPatient.m_strLASTNAME_VCHR;
                if (p_objPatient.m_dtmBIRTH_DAT == DateTime.MinValue)
                {
                    objDPArr[9].Value = DBNull.Value;
                }
                else
                {
                    objDPArr[9].DbType = DbType.Date;
                    objDPArr[9].Value = p_objPatient.m_dtmBIRTH_DAT;
                }
                objDPArr[10].Value = p_objPatient.m_strRACE_VCHR;
                objDPArr[11].Value = p_objPatient.m_strNATIVEPLACE_VCHR;
                objDPArr[12].Value = p_objPatient.m_strOCCUPATION_VCHR;
                objDPArr[13].Value = p_objPatient.m_strLASTNAME_VCHR;
                objDPArr[14].Value = p_objPatient.m_strHOMEPHONE_VCHR;
                objDPArr[15].Value = p_objPatient.m_strOFFICEPHONE_VCHR;
                objDPArr[16].Value = p_objPatient.m_strMOBILE_CHR;
                objDPArr[17].Value = p_objPatient.m_strOFFICEADDRESS_VCHR;
                objDPArr[18].Value = p_objPatient.m_strEMPLOYER_VCHR;
                objDPArr[19].Value = p_objPatient.m_strOFFICEPC_VCHR;
                objDPArr[20].Value = p_objPatient.m_strHOMEPC_CHR;
                objDPArr[21].Value = p_objPatient.m_strEMAIL_VCHR;
                objDPArr[22].Value = p_objPatient.m_strCONTACTPERSONFIRSTNAME_VCHR;
                objDPArr[23].Value = p_objPatient.m_strCONTACTPERSONLASTNAME_VCHR;
                objDPArr[24].Value = p_objPatient.m_strCONTACTPERSONADDRESS_VCHR;
                objDPArr[25].Value = p_objPatient.m_strCONTACTPERSONPHONE_VCHR;
                objDPArr[26].Value = p_objPatient.m_strCONTACTPERSONPC_CHR;
                objDPArr[27].Value = p_objPatient.m_strPATIENTRELATION_VCHR;
                objDPArr[28].Value = 1;
                objDPArr[29].Value = p_objPatient.m_strOPERATORID_CHR;
                objDPArr[30].DbType = DbType.DateTime;
                objDPArr[30].Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                objDPArr[31].Value = p_objPatient.m_intOPTIMES_INT;
                objDPArr[32].Value = p_objPatient.m_strBLOODTYPE_CHR;
                objDPArr[33].Value = p_objPatient.m_intIFALLERGIC_INT;
                objDPArr[34].Value = p_objPatient.m_strALLERGICDESC_VCHR;
                objDPArr[35].Value = p_objPatient.m_intISOUTPATIENT;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 修改随访病人基本信息
        /// <summary>
        /// 修改随访病人基本信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objPatient">病人信息</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyPatientInfo(clsEMR_FollowUpSurveyPatient p_objPatient)
        {
            if (p_objPatient == null)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"update T_EMR_FOLLOWUPSURVEYPATIENT set LASTNAME_VCHR = ?,IDCARD_CHR = ?,MARRIED_CHR = ?,BIRTHPLACE_VCHR = ?,HOMEADDRESS_VCHR = ?,SEX_CHR = ?,
NATIONALITY_VCHR = ?,FIRSTNAME_VCHR = ?,BIRTH_DAT = ?,RACE_VCHR = ?,NATIVEPLACE_VCHR = ?,OCCUPATION_VCHR = ?,NAME_VCHR = ?,HOMEPHONE_VCHR = ?,OFFICEPHONE_VCHR = ?,MOBILE_CHR = ?,OFFICEADDRESS_VCHR = ?,EMPLOYER_VCHR = ?,
OFFICEPC_VCHR = ?,HOMEPC_CHR = ?,EMAIL_VCHR = ?,CONTACTPERSONFIRSTNAME_VCHR = ?,CONTACTPERSONLASTNAME_VCHR = ?,CONTACTPERSONADDRESS_VCHR = ?,CONTACTPERSONPHONE_VCHR = ?,CONTACTPERSONPC_CHR = ?,
PATIENTRELATION_VCHR = ?,OPERATORID_CHR = ?,MODIFY_DAT = ?,OPTIMES_INT = ?,BLOODTYPE_CHR = ?,IFALLERGIC_INT = ?,ALLERGICDESC_VCHR = ?,ISOUTPATIENT = ?
 where REGISTERID_CHR = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(35, out objDPArr);
                //赋值

                objDPArr[0].Value = p_objPatient.m_strLASTNAME_VCHR;
                objDPArr[1].Value = p_objPatient.m_strIDCARD_CHR;
                objDPArr[2].Value = p_objPatient.m_strMARRIED_CHR;
                objDPArr[3].Value = p_objPatient.m_strBIRTHPLACE_VCHR;
                objDPArr[4].Value = p_objPatient.m_strHOMEADDRESS_VCHR;
                objDPArr[5].Value = p_objPatient.m_strSEX_CHR;
                objDPArr[6].Value = p_objPatient.m_strNATIONALITY_VCHR;
                objDPArr[7].Value = p_objPatient.m_strLASTNAME_VCHR;
                if (p_objPatient.m_dtmBIRTH_DAT == DateTime.MinValue)
                {
                    objDPArr[8].Value = DBNull.Value;
                }
                else
                {
                    objDPArr[8].DbType = DbType.Date;
                    objDPArr[8].Value = p_objPatient.m_dtmBIRTH_DAT;
                }
                objDPArr[9].Value = p_objPatient.m_strRACE_VCHR;
                objDPArr[10].Value = p_objPatient.m_strNATIVEPLACE_VCHR;
                objDPArr[11].Value = p_objPatient.m_strOCCUPATION_VCHR;
                objDPArr[12].Value = p_objPatient.m_strLASTNAME_VCHR;
                objDPArr[13].Value = p_objPatient.m_strHOMEPHONE_VCHR;
                objDPArr[14].Value = p_objPatient.m_strOFFICEPHONE_VCHR;
                objDPArr[15].Value = p_objPatient.m_strMOBILE_CHR;
                objDPArr[16].Value = p_objPatient.m_strOFFICEADDRESS_VCHR;
                objDPArr[17].Value = p_objPatient.m_strEMPLOYER_VCHR;
                objDPArr[18].Value = p_objPatient.m_strOFFICEPC_VCHR;
                objDPArr[19].Value = p_objPatient.m_strHOMEPC_CHR;
                objDPArr[20].Value = p_objPatient.m_strEMAIL_VCHR;
                objDPArr[21].Value = p_objPatient.m_strCONTACTPERSONFIRSTNAME_VCHR;
                objDPArr[22].Value = p_objPatient.m_strCONTACTPERSONLASTNAME_VCHR;
                objDPArr[23].Value = p_objPatient.m_strCONTACTPERSONADDRESS_VCHR;
                objDPArr[24].Value = p_objPatient.m_strCONTACTPERSONPHONE_VCHR;
                objDPArr[25].Value = p_objPatient.m_strCONTACTPERSONPC_CHR;
                objDPArr[26].Value = p_objPatient.m_strPATIENTRELATION_VCHR;
                objDPArr[27].Value = p_objPatient.m_strOPERATORID_CHR;
                objDPArr[28].DbType = DbType.DateTime;
                objDPArr[28].Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                objDPArr[29].Value = p_objPatient.m_intOPTIMES_INT;
                objDPArr[30].Value = p_objPatient.m_strBLOODTYPE_CHR;
                objDPArr[31].Value = p_objPatient.m_intIFALLERGIC_INT;
                objDPArr[32].Value = p_objPatient.m_strALLERGICDESC_VCHR;
                objDPArr[33].Value = p_objPatient.m_intISOUTPATIENT;
                objDPArr[34].Value = p_objPatient.m_strREGISTERID;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
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
