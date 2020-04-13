using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using com.digitalwave.Utility;//Utility.dll 

namespace com.digitalwave.iCare.middletier.MFZ
{

    #region 分诊患者Svc
    /// <summary>
    /// HIS接口
    /// </summary>
    [Transaction(TransactionOption.Supported)]
    [ObjectPooling(true)]
    public class clsHISInterfaceSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region SQL
        #region 根据卡号ID查找病人挂号信息
        private const string m_sqlFindByCardID = @" select * from(   SELECT   r.patientcardid_chr AS patientcardno_vchr,
                                                             r.patientid_chr AS patient_vchr, p.lastname_vchr AS patientname_vchr,
                                                             r.registerid_chr AS registerid_vchr,
                                                             r.registerdate_dat AS registerdate_dt,
                                                             r.diagdept_chr AS registerdeptid_vchr,
                                                             d.deptname_vchr AS registerdeptname_vchr,
                                                             r.diagdoctor_chr AS registerdocid_vchr,
                                                             e.lastname_vchr AS registerdocname,
                                                             r.registertypeid_chr AS registertype_vchr,
                                                             t.takediagtime_dat AS takeddate_dt,
                                                             t.takediagdept_chr AS takeddeptid_vchr,
                                                             t.deptname_vchr AS takeddeptname_vchr,
                                                             t.takediagdr_chr AS takeddocid_vchr,
                                                             t.lastname_vchr AS takeddocname_chr, t.pstatus_int AS diagstatus_int
                                                        FROM t_opr_patientregister r,
                                                             t_bse_patient p,
                                                             (SELECT r.registerid_chr, r.diagdept_chr, d.deptname_vchr
                                                                FROM t_opr_patientregister r, t_bse_deptdesc d
                                                               WHERE r.diagdept_chr = d.deptid_chr(+)) d,
                                                            
                                                             (SELECT r.registerid_chr, r.diagdoctor_chr, e.lastname_vchr
                                                                FROM t_opr_patientregister r, t_bse_employee e
                                                               WHERE r.diagdoctor_chr = e.empid_chr(+)) e,
                                                             
                                                             (SELECT t.registerid_chr, t.takediagtime_dat, t.takediagdept_chr,
                                                                     d.deptname_vchr, t.takediagdr_chr, e.lastname_vchr,
                                                                     t.pstatus_int
                                                                FROM t_opr_takediagrec t, t_bse_deptdesc d, t_bse_employee e
                                                                WHERE t.takediagdr_chr = e.empid_chr
                                                                      AND t.takediagdept_chr = d.deptid_chr(+)) t
                                                       WHERE r.patientid_chr = p.patientid_chr
                                                             AND r.registerid_chr = d.registerid_chr
                                                             AND r.registerid_chr = e.registerid_chr
                                                             AND r.registerid_chr = t.registerid_chr(+)
                                                             AND r.registerdate_dat > SYSDATE - 3
                                                             AND r.flag_int in (1, 2)
                                                             AND r.patientcardid_chr = ?
                                                     ORDER BY r.registerdate_dat DESC) where RowNum=1

";
        #endregion

        #region 根据科室病区ID查找病人挂号信息
        private const string m_strFindByCardIDAreaID =
                                          @"
                                            SELECT *
                                              FROM (SELECT   r.patientcardid_chr AS patientcardno_vchr,
                                                             r.patientid_chr AS patient_vchr,
                                                             p.lastname_vchr AS patientname_vchr,
                                                             r.registerid_chr AS registerid_vchr,
                                                             r.registerdate_dat AS registerdate_dt,
                                                             r.diagdept_chr AS registerdeptid_vchr,
                                                             d.deptname_vchr AS registerdeptname_vchr,
                                                             r.diagdoctor_chr AS registerdocid_vchr,
                                                             e.lastname_vchr AS registerdocname,
                                                             r.registertypeid_chr AS registertype_vchr,
                                                             t.takediagtime_dat AS takeddate_dt,
                                                             t.takediagdept_chr AS takeddeptid_vchr,
                                                             t.deptname_vchr AS takeddeptname_vchr,
                                                             t.takediagdr_chr AS takeddocid_vchr,
                                                             t.lastname_vchr AS takeddocname_chr,
                                                             t.pstatus_int AS diagstatus_int
                                                        FROM t_opr_patientregister r,
                                                             t_bse_patient p,
                                                             (SELECT r.registerid_chr, r.diagdept_chr, d.deptname_vchr
                                                                FROM t_opr_patientregister r, t_bse_deptdesc d
                                                               WHERE r.diagdept_chr = d.deptid_chr(+)) d,
                                                             (SELECT r.registerid_chr, r.diagdoctor_chr, e.lastname_vchr
                                                                FROM t_opr_patientregister r, t_bse_employee e
                                                               WHERE r.diagdoctor_chr = e.empid_chr(+)) e,
                                                             (SELECT t.registerid_chr, t.takediagtime_dat,
                                                                     t.takediagdept_chr, d.deptname_vchr,
                                                                     t.takediagdr_chr, e.lastname_vchr, t.pstatus_int
                                                                FROM t_opr_takediagrec t,
                                                                     t_bse_deptdesc d,
                                                                     t_bse_employee e
                                                               WHERE t.takediagdr_chr = e.empid_chr
                                                                 AND t.takediagdept_chr = d.deptid_chr(+)) t
                                                       WHERE r.patientid_chr = p.patientid_chr
                                                         AND r.registerid_chr = d.registerid_chr
                                                         AND r.registerid_chr = e.registerid_chr
                                                         AND r.registerid_chr = t.registerid_chr(+)
                                                         AND r.registerdate_dat > SYSDATE - 3
                                                         AND r.flag_int in (1, 2)
                                                         AND r.patientcardid_chr = ?
                                                    ORDER BY r.registerdate_dat DESC)
                                             WHERE ROWNUM = 1
                                               AND registerdeptid_vchr IN (
                                                      SELECT a.deptid_chr
                                                        FROM t_mfz_bse_dept a, t_bse_deptdesc b
                                                       WHERE a.deptid_chr = b.deptid_chr AND a.diagnoseareaid_int = ?)
                                            ";
        #endregion

        private const string m_strFindPatient = @"SELECT a.patientcardid_chr, a.patientid_chr,b.lastname_vchr
                                                  FROM t_bse_patientcard a, t_bse_patient b
                                                  WHERE a.patientid_chr = b.patientid_chr
                                                  AND a.patientcardid_chr = ?";
        #endregion

        #region 构造实例和构造参数列表
        [AutoComplete]
        private void ConstructVO(DataRow p_dtrSource, ref clsMFZPatientVO objReader)
        {
            objReader.m_strPatientCardNO = p_dtrSource["PATIENTCARDNO_VCHR"].ToString();
            objReader.m_strPatientID = p_dtrSource["PATIENT_VCHR"].ToString();
            objReader.m_strPatientName = p_dtrSource["PATIENTNAME_VCHR"].ToString();
            objReader.m_strRegisterID = p_dtrSource["REGISTERID_VCHR"].ToString();
            objReader.m_datRegisterDate = DBAssist.ToDateTime(p_dtrSource["REGISTERDATE_DT"].ToString());
            objReader.m_strRegisterDeptID = p_dtrSource["REGISTERDEPTID_VCHR"].ToString();
            objReader.m_strRegisterDeptName = p_dtrSource["REGISTERDEPTNAME_VCHR"].ToString();
            objReader.m_strRegisterDocID = p_dtrSource["REGISTERDOCID_VCHR"].ToString();
            objReader.m_strRegisterDocName = p_dtrSource["REGISTERDOCNAME"].ToString();
            objReader.m_datTakedDate = DBAssist.ToDateTime(p_dtrSource["TAKEDDATE_DT"].ToString());
            objReader.m_strTakedDeptID = p_dtrSource["TAKEDDEPTID_VCHR"].ToString();
            objReader.m_strTakedDeptName = p_dtrSource["TAKEDDEPTNAME_VCHR"].ToString();
            objReader.m_strTakedDocID = p_dtrSource["TAKEDDOCID_VCHR"].ToString();
            objReader.m_strTakedDocName = p_dtrSource["TAKEDDOCNAME_CHR"].ToString();

            //只要不是挂到专家,就算是普通类型
            switch (p_dtrSource["REGISTERTYPE_VCHR"].ToString())
            {
                case "0001": objReader.m_enmRegisterType = enmMFZ_RegisterType.Common; break;
                case "0002": objReader.m_enmRegisterType = enmMFZ_RegisterType.Expert; break;
                //case "0003": objReader.m_enmRegisterType = enmMFZ_RegisterType.Alarm; break;
                //case "0004": objReader.m_enmRegisterType = enmMFZ_RegisterType.AlarmCommon; break;
                default: objReader.m_enmRegisterType = enmMFZ_RegisterType.Common; break;
            }

            // 全部默认预约挂号专家
            objReader.m_enmRegisterType = enmMFZ_RegisterType.Expert;

            try
            {
                int diagStatus = DBAssist.ToInt32(p_dtrSource["DIAGSTATUS_INT"].ToString());
                switch (diagStatus)
                {
                    case DBAssist.NullInt: objReader.m_enmDiagStatus = enmMFZ_DiagnoseStatus.UnTaked; break;
                    case 1: objReader.m_enmDiagStatus = enmMFZ_DiagnoseStatus.Taked; break;
                    case 2: objReader.m_enmDiagStatus = enmMFZ_DiagnoseStatus.Done; break;
                    default: break;
                }
            }
            catch
            {

            }
        }

        [AutoComplete]
        private clsMFZPatientVO[] ConstructVOArr(DataTable dtbResult)
        {
            clsMFZPatientVO[] p_objResultArr = new clsMFZPatientVO[dtbResult.Rows.Count];
            for (int i = 0; i < p_objResultArr.Length; i++)
            {
                p_objResultArr[i] = new clsMFZPatientVO();
                ConstructVO(dtbResult.Rows[i], ref p_objResultArr[i]);
            }
            return p_objResultArr;
        }

        #endregion

        #region FIND

        /// <summary>
        /// 根据患者卡号查找患者信息
        /// </summary>
        /// <param name="cardID"></param>
        /// <param name="objReader"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFind(string cardID, out clsMFZPatientVO objReader)
        {
            long lngRes = 0;
            objReader = null;

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                IDataParameter[] paramsArr = CreateParms.Parms(new object[] { cardID });
                DataTable dtbResult = null;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(m_sqlFindByCardID, ref dtbResult, paramsArr);
                objHRPSvc.Dispose();
                if (lngRes == 1 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    objReader = new clsMFZPatientVO();
                    ConstructVO(dtbResult.Rows[0], ref objReader);
                }
                else
                {
                    objReader = m_objFindPatient(cardID);
                }
            }
            catch (Exception objEx)
            {
                new clsLogText().LogError(objEx);

            }
            return lngRes;
        }

        /// <summary>
        /// 根据患者卡号和诊区ID查找患者信息
        /// </summary>
        /// <param name="cardID"></param>
        /// <param name="objReader"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFind(string cardID, int AreaID, out clsMFZPatientVO objReader)
        {
            long lngRes = 0;
            objReader = null;

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                IDataParameter[] paramsArr = CreateParms.Parms(new object[] { cardID, AreaID });
                DataTable dtbResult = null;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(m_strFindByCardIDAreaID, ref dtbResult, paramsArr);
                objHRPSvc.Dispose();
                if (lngRes == 1 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    objReader = new clsMFZPatientVO();
                    ConstructVO(dtbResult.Rows[0], ref objReader);
                }
            }
            catch (Exception objEx)
            {
                new clsLogText().LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 根据卡号ID查找病人信息
        /// </summary>
        /// <param name="cardID">卡号ID</param>
        /// <returns>患者VO</returns>
        [AutoComplete]
        private clsMFZPatientVO m_objFindPatient(string cardID)
        {
            long lngRes = 0;
            clsMFZPatientVO objReader = new clsMFZPatientVO();

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                IDataParameter[] paramsArr = CreateParms.Parms(new object[] { cardID });
                DataTable dtbResult = null;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(m_strFindPatient, ref dtbResult, paramsArr);
                objHRPSvc.Dispose();
                if (lngRes == 1 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    objReader = new clsMFZPatientVO();
                    objReader.m_strPatientCardNO = dtbResult.Rows[0]["patientcardid_chr"].ToString();
                    objReader.m_strPatientID = dtbResult.Rows[0]["patientid_chr"].ToString();
                    objReader.m_strPatientName = dtbResult.Rows[0]["lastname_vchr"].ToString();
                }
                return objReader;
            }
            catch (Exception objEx)
            {
                new clsLogText().LogError(objEx);
                return objReader;
            }

        }


        #endregion
    }
    #endregion
}