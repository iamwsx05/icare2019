using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
using com.digitalwave.Controls.Domain.EmrControls;
using System.ComponentModel;
using com.digitalwave.emr.BEDExplorer;
using System.Drawing;
using System.IO;
using System.Data;

namespace iCare
{
    public static class clsBaseInfo
    {
        // Fields
        private static bool s_blnIsInit;
        private static clsEmrDept_VO[] s_objCurDeptOfEmpArr;
        private static clsDepartment s_objCurrDepartment;
        private static clsPatient s_objCurrentPatient;
        private static clsEmrEmployeeBase_VO s_objLoginEmployee;
        private static string s_strEmpoyeeID;

        // Methods
        static clsBaseInfo()
        {
            s_objCurrDepartment = null;
            s_objCurrentPatient = null;
            s_blnIsInit = false;
        }

        public static long m_lngGetBedInfo(string p_strID, bool p_blnIsRoom, out clsEmrBed_VO[] p_objResultArr)
        {
            p_objResultArr = null;
            DataTable dt = null;
            long rec = (new weCare.Proxy.ProxyEmr06()).Service.m_lngGetBedInfo(p_strID, false, out dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                p_objResultArr = m_objGetEmrBed_VO(dt);
            }
            return rec;
        }

        public static long m_lngGetBedInfoByDeptID(string p_strID, out clsEmrBed_VO[] p_objResultArr)
        {
            p_objResultArr = null;
            DataTable dt = null;
            long rec = (new weCare.Proxy.ProxyEmr06()).Service.m_lngGetBedInfoByDeptID(p_strID, out dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                p_objResultArr = m_objGetEmrBed_VO(dt);
            }
            return rec;
        }

        public static long m_lngGetBedInfoLikeBedCode(string p_strID, bool p_blnIsRoom, string p_strBedCodeLike, out clsEmrBed_VO[] p_objResultArr)
        {
            p_objResultArr = null;
            DataTable dt = null;
            long rec = (new weCare.Proxy.ProxyEmr06()).Service.m_lngGetBedInfoLikeBedCode(p_strID, false, p_strBedCodeLike, out dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                p_objResultArr = m_objGetEmrBed_VO(dt);
            }
            return rec;
        }

        public static long m_lngGetDeptAreaInfo(string p_strEmployeeID, out clsEmrDept_VO[] p_objResultArr)
        {
            p_objResultArr = null;
            DataTable dt = null;
            long rec = (new weCare.Proxy.ProxyEmr06()).Service.m_lngGetDeptAreaInfo(p_strEmployeeID, out dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                p_objResultArr = new clsEmrDept_VO[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    p_objResultArr[i] = new clsEmrDept_VO();
                    p_objResultArr[i] = m_objAddToEmrDept_VO(dt.Rows[i]);
                    p_objResultArr[i].m_strATTRIBUTEID = dt.Rows[i]["attributeid"].ToString();
                    p_objResultArr[i].m_intDEFAULT_INPATIENT_DEPT_INT = Convert.ToInt32(dt.Rows[i]["DEFAULT_INPATIENT_DEPT_INT"]);
                }
            }
            return rec;
        }

        public static long m_lngGetDeptInfo(string p_strEmployeeID, out clsEmrDept_VO[] p_objResultArr)
        {
            p_objResultArr = null;
            DataTable dt = null;
            long rec = (new weCare.Proxy.ProxyEmr06()).Service.m_lngGetDeptInfo(p_strEmployeeID, out dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                p_objResultArr = new clsEmrDept_VO[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    p_objResultArr[i] = new clsEmrDept_VO();
                    p_objResultArr[i] = m_objAddToEmrDept_VO(dt.Rows[i]);
                    p_objResultArr[i].m_strATTRIBUTEID = dt.Rows[i]["attributeid"].ToString();
                    p_objResultArr[i].m_intDEFAULT_INPATIENT_DEPT_INT = Convert.ToInt32(dt.Rows[i]["DEFAULT_INPATIENT_DEPT_INT"]);
                }
            }
            return rec;
        }

        public static long m_lngGetEmpByID(string strEmpID, out clsEmrEmployeeBase_VO p_objEmployeeBase)
        {
            p_objEmployeeBase = null;
            DataTable dt = null;
            long rec = (new weCare.Proxy.ProxyEmr06()).Service.m_lngGetEmpByID(strEmpID, out dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                p_objEmployeeBase = new clsEmrEmployeeBase_VO();
                DataRow dr = dt.Rows[0];
                p_objEmployeeBase.m_strEMPID_CHR = dr["empid_chr"].ToString();
                p_objEmployeeBase.m_strLASTNAME_VCHR = dr["lastname_vchr"].ToString();
                p_objEmployeeBase.m_strTECHNICALRANK_CHR = dr["technicalrank_chr"].ToString();
                if (p_objEmployeeBase.m_strTECHNICALRANK_CHR != null)
                {
                    p_objEmployeeBase.m_strTECHNICALRANK_CHR = p_objEmployeeBase.m_strTECHNICALRANK_CHR.Trim();
                }
                p_objEmployeeBase.m_strTechnicalRank = p_objEmployeeBase.m_strTECHNICALRANK_CHR;
                p_objEmployeeBase.m_strEMPNO_CHR = dr["empno_chr"].ToString();
                p_objEmployeeBase.m_strPYCODE_VCHR = dr["pycode_chr"].ToString();
                p_objEmployeeBase.m_strEMPKEY_VCHR = dr["digitalsign_dta"].ToString();
                p_objEmployeeBase.m_strEMPPWD_VCHR = dr["psw_chr"].ToString();
                p_objEmployeeBase.m_strLEVEL_CHR = dr["technicallevel_chr"].ToString();
                p_objEmployeeBase.m_StrHistroyLevel = p_objEmployeeBase.m_strLEVEL_CHR;
                p_objEmployeeBase.m_strDefaultDeptID = dr["deptid_chr"].ToString();
                p_objEmployeeBase.m_intSTATUS_INT = 1;

                string[] strArray = null;
                string[] strArray2 = null;
                (new weCare.Proxy.ProxyEmr()).Service.m_lngGetRoleByEmpID(strEmpID, out strArray, out strArray2);
                p_objEmployeeBase.m_strRoleIDArr = strArray;
                p_objEmployeeBase.m_strRoleNameArr = strArray2;
                p_objEmployeeBase.m_blnIsShowTechnicalRank = (com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_intGetEmrSettingValue("3015") == 1) ? true : false;
            }
            return rec;
        }

        public static long m_lngGetEmpByNO(string strEmpNO, out clsEmrEmployeeBase_VO p_objEmployeeBase)
        {
            p_objEmployeeBase = null;
            DataTable dt = null;
            long rec = (new weCare.Proxy.ProxyEmr06()).Service.m_lngGetEmpByNO(strEmpNO, out dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                p_objEmployeeBase = new clsEmrEmployeeBase_VO();
                DataRow dr = dt.Rows[0];
                p_objEmployeeBase.m_strEMPID_CHR = dr["empid_chr"].ToString();
                p_objEmployeeBase.m_strLASTNAME_VCHR = dr["lastname_vchr"].ToString();
                p_objEmployeeBase.m_strTECHNICALRANK_CHR = dr["technicalrank_chr"].ToString();
                if (p_objEmployeeBase.m_strTECHNICALRANK_CHR != null)
                {
                    p_objEmployeeBase.m_strTECHNICALRANK_CHR = p_objEmployeeBase.m_strTECHNICALRANK_CHR.Trim();
                }
                p_objEmployeeBase.m_strTechnicalRank = p_objEmployeeBase.m_strTECHNICALRANK_CHR;
                p_objEmployeeBase.m_strEMPNO_CHR = dr["empno_chr"].ToString();
                p_objEmployeeBase.m_strPYCODE_VCHR = dr["pycode_chr"].ToString();
                p_objEmployeeBase.m_strEMPKEY_VCHR = dr["digitalsign_dta"].ToString();
                p_objEmployeeBase.m_strEMPPWD_VCHR = dr["psw_chr"].ToString();
                p_objEmployeeBase.m_strLEVEL_CHR = dr["technicallevel_chr"].ToString();
                p_objEmployeeBase.m_StrHistroyLevel = p_objEmployeeBase.m_strLEVEL_CHR;
                p_objEmployeeBase.m_intSTATUS_INT = 1;
                string[] strArray = null;
                string[] strArray2 = null;
                (new weCare.Proxy.ProxyEmr()).Service.m_lngGetRoleByEmpID(p_objEmployeeBase.m_strEMPID_CHR, out strArray, out strArray2);
                p_objEmployeeBase.m_strRoleIDArr = strArray;
                p_objEmployeeBase.m_strRoleNameArr = strArray2;
                p_objEmployeeBase.m_blnIsShowTechnicalRank = (com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_intGetEmrSettingValue("3015") == 1) ? true : false;
            }
            return rec;
        }

        public static long m_lngGetInBedInfo(string p_strID, bool p_blnIsRoom, out clsEmrBed_VO[] p_objResultArr)
        {
            p_objResultArr = null;
            DataTable dt = null;
            long rec = (new weCare.Proxy.ProxyEmr06()).Service.m_lngGetInBedInfo(p_strID, false, out dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                p_objResultArr = m_objGetEmrBed_VO(dt);
            }
            return rec;
        }

        private static clsEmrDept_VO m_objAddToEmrDept_VO(DataRow p_dr)
        {
            clsEmrDept_VO t_vo = new clsEmrDept_VO();
            t_vo.m_strDEPTID_CHR = p_dr["DEPTID_CHR"].ToString().Trim();
            t_vo.m_strDEPTNAME_VCHR = p_dr["DEPTNAME_VCHR"].ToString();
            t_vo.m_strADDRESS_VCHR = p_dr["ADDRESS_VCHR"].ToString();
            t_vo.m_strSHORTNO_CHR = p_dr["SHORTNO_CHR"].ToString();
            if (!string.IsNullOrEmpty(t_vo.m_strSHORTNO_CHR))
                t_vo.m_strSHORTNO_CHR = t_vo.m_strSHORTNO_CHR.Trim();
            return t_vo;
        }

        private static clsEmrBed_VO[] m_objGetEmrBed_VO(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            int num = 0;
            double num2 = 0;
            clsEmrBed_VO vo = null;
            List<clsEmrBed_VO> data = new List<clsEmrBed_VO>();
            foreach (DataRow dr in dt.Rows)
            {
                vo = new clsEmrBed_VO();
                vo.m_objInbedPatient = new clsEmrInBedPatient_VO();
                vo.m_strBEDID_CHR = dr["BEDID_CHR"].ToString();
                vo.m_strAREAID_CHR = dr["AREAID_CHR"].ToString();
                vo.m_strCODE_CHR = dr["CODE_CHR"].ToString();
                if (int.TryParse(dr["BEDSTATUS_INT"].ToString(), out num))
                {
                    vo.m_intSTATUS_INT = num;
                }
                else
                {
                    vo.m_intSTATUS_INT = 1;
                }
                if (double.TryParse(dr["RATE_MNY"].ToString(), out num2))
                {
                    vo.m_dblRATE_MNY = num2;
                }
                else
                {
                    vo.m_dblRATE_MNY = 1.0;
                }
                if (int.TryParse(dr["SEX_INT"].ToString(), out num))
                {
                    vo.m_intSEX_INT = num;
                }
                else
                {
                    vo.m_intSEX_INT = 0;
                }
                if (int.TryParse(dr["CATEGORY_INT"].ToString(), out num))
                {
                    vo.m_intCATEGORY_INT = num;
                }
                else
                {
                    vo.m_intCATEGORY_INT = 1;
                }
                if (double.TryParse(dr["AIRRATE_MNY"].ToString(), out num2))
                {
                    vo.m_dblAIRRATE_MNY = num2;
                }
                else
                {
                    vo.m_dblAIRRATE_MNY = 1.0;
                }
                vo.m_strCHARGEITEMID_CHR = dr["CHARGEITEMID_CHR"].ToString();
                if (dr["PATIENTID_CHR"] != DBNull.Value)
                {
                    vo.m_objInbedPatient.m_strPATIENTID_CHR = dr["PATIENTID_CHR"].ToString();
                    vo.m_objInbedPatient.m_strINPATIENTID_CHR = dr["EMRINPATIENTID"].ToString();
                    vo.m_objInbedPatient.m_strINPATIENT_DAT = dr["EMRINPATIENTDATE"].ToString();
                    if (int.TryParse(dr["STATE_INT"].ToString(), out num))
                    {
                        vo.m_objInbedPatient.m_intSTATE_INT = num;
                    }
                    else
                    {
                        vo.m_objInbedPatient.m_intSTATE_INT = 3;
                    }
                    if (int.TryParse(dr["NURSING_CLASS"].ToString(), out num))
                    {
                        vo.m_objInbedPatient.m_intNurseClass = num;
                    }
                    else
                    {
                        vo.m_objInbedPatient.m_intNurseClass = -1;
                    }
                    vo.m_objInbedPatient.m_strLASTNAME_VCHR = dr["LASTNAME_VCHR"].ToString();
                    vo.m_objInbedPatient.m_strSEX_CHR = dr["SEX_CHR"].ToString();
                    vo.m_objInbedPatient.m_strBIRTH_DAT = dr["BIRTH_DAT"].ToString();
                    vo.m_objInbedPatient.m_strREGISTERID_CHR = dr["REGISTERID_CHR"].ToString();
                    vo.m_objInbedPatient.m_strDEPTID_CHR = dr["DEPTID_CHR"].ToString();
                    vo.m_objInbedPatient.m_strAREAID_CHR = dr["AREAID_CHR"].ToString();
                    vo.m_objInbedPatient.m_strCODE_CHR = dr["CODE_CHR"].ToString();
                    vo.m_objInbedPatient.m_strBEDID_CHR = dr["BEDID_CHR"].ToString();
                    vo.m_objInbedPatient.m_strEXTENDID_VCHR = dr["HISINPATIENTID_CHR"].ToString();
                    if (int.TryParse(dr["INPATIENTCOUNT_INT"].ToString(), out num))
                    {
                        vo.m_objInbedPatient.m_intINPATIENTCOUNT_INT = num;
                    }
                    else
                    {
                        vo.m_objInbedPatient.m_intINPATIENTCOUNT_INT = 1;
                    }
                    if (int.TryParse(dr["PSTATUS_INT"].ToString(), out num))
                    {
                        vo.m_objInbedPatient.m_intPSTATUS_INT = num;
                    }
                    else
                    {
                        vo.m_objInbedPatient.m_intPSTATUS_INT = 1;
                    }
                    vo.m_objInbedPatient.m_strEMRInPatientID = dr["EMRINPATIENTID"].ToString();
                    vo.m_objInbedPatient.m_strHISInPatientID = dr["HISINPATIENTID_CHR"].ToString();

                    DateTime dtm = DateTime.Now;
                    if (DateTime.TryParse(dr["emrinpatientdate"].ToString(), out dtm))
                    {
                        vo.m_objInbedPatient.m_dtmEMRInDate = dtm;
                    }
                    else
                    {
                        vo.m_objInbedPatient.m_dtmEMRInDate = DateTime.MinValue;
                    }
                    if (DateTime.TryParse(dr["hisinpatientdate"].ToString(), out dtm))
                    {
                        vo.m_objInbedPatient.m_dtmHISInDate = dtm;
                    }
                    else
                    {
                        vo.m_objInbedPatient.m_dtmHISInDate = DateTime.MinValue;
                    }
                    vo.m_objInbedPatient.m_strAGESHORT_CHR = (new clsBrithdayToAge()).m_strGetAge(Convert.ToDateTime(vo.m_objInbedPatient.m_strBIRTH_DAT));
                    vo.m_objInbedPatient.m_strAGELONG_CHR = vo.m_objInbedPatient.m_strAGESHORT_CHR;
                    vo.m_objInbedPatient.m_intAge = (new clsBrithdayToAge()).m_intGetAge(Convert.ToDateTime(vo.m_objInbedPatient.m_strBIRTH_DAT));
                    vo.m_objInbedPatient.m_strCaseDoctorId = dr["casedoctor_chr"].ToString();
                    vo.m_objInbedPatient.m_strCaseDoctorName = dr["casedoctorname"].ToString();
                    vo.m_objInbedPatient.m_strDiagnos = dr["diagnose_vchr"].ToString();
                }
                else
                {
                    vo.m_objInbedPatient = null;
                }
                data.Add(vo);
            }
            return data.ToArray();
        }

        public static DateTime s_dtmGetServerTime()
        {
            return Convert.ToDateTime(s_strGetServerTime());
        }

        public static void s_mthInit(string p_strLoginID)
        {
            clsEmrEmployeeBase_VO e_vo = null;
            clsEmrDept_VO[] t_voArray = null;
            s_strEmpoyeeID = p_strLoginID;
            s_objCurDeptOfEmpArr = null;

            long num = m_lngGetEmpByID(s_strEmpoyeeID, out e_vo);
            if (e_vo == null)
            {
                s_blnIsInit = true;
                return;
            }
            e_vo.m_intSTATUS_INT = 0;
            s_objLoginEmployee = e_vo;
            com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_ObjCurrentEmployee = e_vo;

            num = m_lngGetDeptAreaInfo(s_strEmpoyeeID, out t_voArray);
            com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_ObjEmpDeptAndAreaArr = t_voArray;
            s_objCurDeptOfEmpArr = t_voArray;
            t_voArray = null;
        }

        public static string s_strGetServerTime()
        {
            return (new weCare.Proxy.ProxyEmr()).Service.m_strGetServerTime();
        }

        // Properties
        public static clsEmrEmployeeBase_VO LoginEmployee
        {
            get
            {
                if (s_objLoginEmployee == null)
                {
                    m_lngGetEmpByID(s_strEmpoyeeID, out s_objLoginEmployee);
                    if (s_objLoginEmployee != null)
                    {
                        s_objLoginEmployee.m_intSTATUS_INT = 0;
                    }
                }
                else
                {
                    s_objLoginEmployee.m_intSTATUS_INT = 0;

                }
                return s_objLoginEmployee;
            }
        }

        public static bool s_BlnIsInit
        {
            get
            {
                return s_blnIsInit;
            }
        }

        public static clsEmrDept_VO[] s_ObjCurDeptOfEmpArr
        {
            get
            {
                return s_objCurDeptOfEmpArr;
            }
        }

        public static clsDepartment s_ObjCurrDepartment
        {
            get
            {
                return s_objCurrDepartment;
            }
            set
            {
                s_objCurrDepartment = value;
            }
        }

        public static clsPatient s_ObjCurrentPatient
        {
            get
            {
                return s_objCurrentPatient;
            }
            set
            {
                s_objCurrentPatient = value;
            }
        }
    }
}
